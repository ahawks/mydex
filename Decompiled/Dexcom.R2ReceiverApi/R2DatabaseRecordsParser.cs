// Type: Dexcom.R2Receiver.R2DatabaseRecordsParser
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using Dexcom.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml;

namespace Dexcom.R2Receiver
{
  public class R2DatabaseRecordsParser : R2DatabaseParserBase
  {
    private R2RecordTypeFlag m_recordsFilter = R2ReceiverValues.AllR2Records;
    private List<Sensor2Record> m_priorSensor2Records = new List<Sensor2Record>();
    private Sensor2Record m_priorSensor2Record;
    private STSCalibrationRecord m_priorCalibrationRecord;
    private bool m_isManufacturingMode;
    private R2DatabaseRecords m_databaseRecords;

    public object Tag { get; set; }

    public R2DatabaseRecords DatabaseRecords
    {
      get
      {
        return this.m_databaseRecords;
      }
    }

    public R2RecordTypeFlag RecordsFilter
    {
      get
      {
        return this.m_recordsFilter;
      }
      set
      {
        this.m_recordsFilter = value;
      }
    }

    public bool IsManufacturingMode
    {
      get
      {
        return this.m_isManufacturingMode;
      }
      set
      {
        this.m_isManufacturingMode = value;
      }
    }

    public bool HasGeneration3CalibrationRecords
    {
      get
      {
        if (this.m_databaseRecords.STSCalibrationRecords.Count > 0)
          return this.m_databaseRecords.STSCalibrationRecords[0].RecordType >= R2RecordType.STS_Calibration5;
        else
          return false;
      }
    }

    public TimeSpan LastDisplayOffset
    {
      get
      {
        return this.LatestUserOffset;
      }
    }

    public DateTime FirstGlucoseDisplayTime
    {
      get
      {
        DateTime dateTime = DateTime.MaxValue;
        Sensor2Record sensor2Record = Enumerable.FirstOrDefault<Sensor2Record>((IEnumerable<Sensor2Record>) this.DatabaseRecords.Sensor2Records);
        if (sensor2Record != null)
          dateTime = sensor2Record.DisplayTime;
        return dateTime;
      }
    }

    public DateTime LastGlucoseDisplayTime
    {
      get
      {
        DateTime dateTime = DateTime.MinValue;
        Sensor2Record sensor2Record = Enumerable.LastOrDefault<Sensor2Record>((IEnumerable<Sensor2Record>) this.DatabaseRecords.Sensor2Records);
        if (sensor2Record != null)
          dateTime = sensor2Record.DisplayTime;
        return dateTime;
      }
    }

    public DateTime FirstGlucoseInternalTime
    {
      get
      {
        DateTime dateTime = DateTime.MaxValue;
        Sensor2Record sensor2Record = Enumerable.FirstOrDefault<Sensor2Record>((IEnumerable<Sensor2Record>) this.DatabaseRecords.Sensor2Records);
        if (sensor2Record != null)
          dateTime = sensor2Record.SystemTime;
        return dateTime;
      }
    }

    public DateTime LastGlucoseInternalTime
    {
      get
      {
        DateTime dateTime = DateTime.MinValue;
        Sensor2Record sensor2Record = Enumerable.LastOrDefault<Sensor2Record>((IEnumerable<Sensor2Record>) this.DatabaseRecords.Sensor2Records);
        if (sensor2Record != null)
          dateTime = sensor2Record.SystemTime;
        return dateTime;
      }
    }

    public DateTime FirstMeterMeterTime
    {
      get
      {
        DateTime dateTime = DateTime.MaxValue;
        MeterRecord meterRecord = Enumerable.FirstOrDefault<MeterRecord>((IEnumerable<MeterRecord>) this.DatabaseRecords.MeterRecords);
        if (meterRecord != null)
          dateTime = meterRecord.GlucoseTimeStamp;
        return dateTime;
      }
    }

    public DateTime LastMeterMeterTime
    {
      get
      {
        DateTime dateTime = DateTime.MinValue;
        MeterRecord meterRecord = Enumerable.LastOrDefault<MeterRecord>((IEnumerable<MeterRecord>) this.DatabaseRecords.MeterRecords);
        if (meterRecord != null)
          dateTime = meterRecord.GlucoseTimeStamp;
        return dateTime;
      }
    }

    public DateTime FirstMeterMeterDisplayTime
    {
      get
      {
        DateTime dateTime = DateTime.MaxValue;
        MeterRecord meterRecord = Enumerable.FirstOrDefault<MeterRecord>((IEnumerable<MeterRecord>) this.DatabaseRecords.MeterRecords);
        if (meterRecord != null)
          dateTime = meterRecord.WorldTimeStamp;
        return dateTime;
      }
    }

    public DateTime LastMeterMeterDisplayTime
    {
      get
      {
        DateTime dateTime = DateTime.MinValue;
        MeterRecord meterRecord = Enumerable.LastOrDefault<MeterRecord>((IEnumerable<MeterRecord>) this.DatabaseRecords.MeterRecords);
        if (meterRecord != null)
          dateTime = meterRecord.WorldTimeStamp;
        return dateTime;
      }
    }

    public DateTime FirstGlucoseInternalTimePlusLastDisplayOffset
    {
      get
      {
        if (!(this.FirstGlucoseInternalTime == DateTime.MaxValue))
          return this.FirstGlucoseInternalTime + this.LastDisplayOffset;
        else
          return DateTime.MaxValue;
      }
    }

    public DateTime LastGlucoseInternalTimePlusLastDisplayOffset
    {
      get
      {
        if (!(this.LastGlucoseInternalTime == DateTime.MinValue))
          return this.LastGlucoseInternalTime + this.LastDisplayOffset;
        else
          return DateTime.MinValue;
      }
    }

    public DateTime FirstMeterMeterTimePlusLastDisplayOffset
    {
      get
      {
        if (!(this.FirstMeterMeterTime == DateTime.MaxValue))
          return this.FirstMeterMeterTime + this.LastDisplayOffset;
        else
          return DateTime.MaxValue;
      }
    }

    public DateTime LastMeterMeterTimePlusLastDisplayOffset
    {
      get
      {
        if (!(this.LastMeterMeterTime == DateTime.MinValue))
          return this.LastMeterMeterTime + this.LastDisplayOffset;
        else
          return DateTime.MinValue;
      }
    }

    public R2DatabaseRecordsParser()
    {
      this.m_databaseRecords = new R2DatabaseRecords();
    }

    public override void Parse(byte[] data)
    {
      base.Parse(data);
    }

    public void ParseErrorLog(byte[] data)
    {
      if (data == null || data.Length < Marshal.SizeOf(typeof (R2ErrorLogHeader)))
        return;
      int num1 = 0;
      int num2 = 0;
      uint version = 0U;
      R2ErrorLogHeader r2ErrorLogHeader = (R2ErrorLogHeader) DataTools.ConvertBytesToObject(data, 0, typeof (R2ErrorLogHeader));
      if (r2ErrorLogHeader.m_signature == "DX R2 ERROR LOG")
      {
        num1 += r2ErrorLogHeader.HeaderSize;
        version = r2ErrorLogHeader.m_version;
      }
      while (num1 < data.Length)
      {
        ErrorLogRecord record = new ErrorLogRecord(data, num1, version);
        if (record.IsInvalidRecord)
        {
          num2 += Convert.ToInt32(R2ReceiverValues.ErrorSize);
          num1 = num2;
          if (num1 < data.Length - Marshal.SizeOf(typeof (R2ErrorLogHeader)))
          {
            r2ErrorLogHeader = (R2ErrorLogHeader) DataTools.ConvertBytesToObject(data, num1, typeof (R2ErrorLogHeader));
            if (r2ErrorLogHeader.m_signature == "DX R2 ERROR LOG")
            {
              num1 += r2ErrorLogHeader.HeaderSize;
              version = r2ErrorLogHeader.m_version;
            }
          }
        }
        else
        {
          this.m_databaseRecords.AddRecord(record);
          num1 += record.GetInternalRecordSize();
        }
      }
    }

    public void ParseReceiverDownloadInfo(XmlDocument xDownloadDoc)
    {
    }

    protected override void LogRecordHandler(byte[] data, int offset)
    {
      if ((this.RecordsFilter & R2RecordTypeFlag.Log) == R2RecordTypeFlag.None)
        return;
      LogRecord record = new LogRecord(data, offset);
      record.SetCalculatedTimeOffsets(this.LatestSkewOffset, this.LatestUserOffset);
      this.m_databaseRecords.AddRecord(record);
    }

    protected override void EventRecordHandler(byte[] data, int offset)
    {
      if ((this.RecordsFilter & R2RecordTypeFlag.Event) == R2RecordTypeFlag.None)
        return;
      EventRecord record = new EventRecord(data, offset);
      record.SetCalculatedTimeOffsets(this.LatestSkewOffset, this.LatestUserOffset);
      this.m_databaseRecords.AddRecord(record);
    }

    protected override void MeterRecordHandler(byte[] data, int offset)
    {
      if ((this.RecordsFilter & R2RecordTypeFlag.Meter) == R2RecordTypeFlag.None)
        return;
      MeterRecord record = new MeterRecord(data, offset);
      record.SetCalculatedTimeOffsets(this.LatestSkewOffset, this.LatestUserOffset);
      this.m_databaseRecords.AddRecord(record);
    }

    protected override void SensorRecordHandler(byte[] data, int offset)
    {
      SensorRecord record = new SensorRecord(data, offset);
      record.SetCalculatedTimeOffsets(this.LatestSkewOffset, this.LatestUserOffset);
      if (this.LatestSettingsRecord != null)
        record.SetTransmitterId(this.LatestSettingsRecord.TransmitterSerialNumber);
      this.m_databaseRecords.AddRecord(record);
    }

    protected override void Sensor2RecordHandler(byte[] data, int offset)
    {
      if ((this.RecordsFilter & R2RecordTypeFlag.Sensor2) == R2RecordTypeFlag.None)
        return;
      Sensor2Record record = new Sensor2Record(data, offset);
      record.SetCalculatedTimeOffsets(this.LatestSkewOffset, this.LatestUserOffset);
      record.SetPriorCalibrationInformation(this.m_priorCalibrationRecord);
      if (this.m_isManufacturingMode && (int) record.TransmitterId != 0)
      {
        bool flag = false;
        for (int index = 0; index < this.m_priorSensor2Records.Count; ++index)
        {
          Sensor2Record priorRecord = this.m_priorSensor2Records[index];
          if (priorRecord != null && (int) priorRecord.TransmitterId == (int) record.TransmitterId)
          {
            record.SetPriorSensorInformation(priorRecord);
            this.m_priorSensor2Records[index] = record;
            flag = true;
            break;
          }
        }
        if (!flag)
          this.m_priorSensor2Records.Add(record);
      }
      else
      {
        record.SetPriorSensorInformation(this.m_priorSensor2Record);
        this.m_priorSensor2Record = record;
      }
      this.m_databaseRecords.AddRecord(record);
    }

    protected override void SettingsRecordHandler(byte[] data, int offset)
    {
      if ((this.RecordsFilter & R2RecordTypeFlag.Settings) == R2RecordTypeFlag.None)
        return;
      this.m_databaseRecords.AddRecord(new SettingsRecord(data, offset));
    }

    protected override void MatchedPairRecordHandler(byte[] data, int offset)
    {
      if ((this.RecordsFilter & R2RecordTypeFlag.MatchedPair) == R2RecordTypeFlag.None)
        return;
      MatchedPairRecord record = new MatchedPairRecord(data, offset);
      record.SetCalculatedTimeOffsets(this.LatestSkewOffset, this.LatestUserOffset);
      this.m_databaseRecords.AddRecord(record);
    }

    protected override void STSCalibrationRecordHandler(byte[] data, int offset)
    {
      if ((this.RecordsFilter & R2RecordTypeFlag.STS_Calibration) == R2RecordTypeFlag.None)
        return;
      STSCalibrationRecord instance = STSCalibrationRecord.CreateInstance(data, offset);
      instance.SetCalculatedTimeOffsets(this.LatestSkewOffset, this.LatestUserOffset);
      this.m_priorCalibrationRecord = instance;
      this.m_databaseRecords.AddRecord(instance);
    }

    public void AdjustUserTimesOnRecordsBeforeFirstSettingsRecord()
    {
      if (this.m_databaseRecords == null || this.m_databaseRecords.SettingsRecords.Count <= 0)
        return;
      DateTime timeStampUtc = this.m_databaseRecords.SettingsRecords[0].TimeStampUtc;
      TimeSpan skewOffset = this.m_databaseRecords.SettingsRecords[0].SkewOffset;
      TimeSpan userOffset = this.m_databaseRecords.SettingsRecords[0].UserOffset;
      foreach (LogRecord logRecord in this.m_databaseRecords.LogRecords)
      {
        if (logRecord.TimeStampUtc <= timeStampUtc)
          logRecord.SetCalculatedTimeOffsets(skewOffset, userOffset);
        else
          break;
      }
      foreach (MeterRecord meterRecord in this.m_databaseRecords.MeterRecords)
      {
        if (meterRecord.TimeStampUtc <= timeStampUtc)
          meterRecord.SetCalculatedTimeOffsets(skewOffset, userOffset);
        else
          break;
      }
      foreach (SensorRecord sensorRecord in this.m_databaseRecords.SensorRecords)
      {
        if (sensorRecord.TimeStampUtc <= timeStampUtc)
          sensorRecord.SetCalculatedTimeOffsets(skewOffset, userOffset);
        else
          break;
      }
      foreach (Sensor2Record sensor2Record in this.m_databaseRecords.Sensor2Records)
      {
        if (sensor2Record.TimeStampUtc <= timeStampUtc)
          sensor2Record.SetCalculatedTimeOffsets(skewOffset, userOffset);
        else
          break;
      }
      foreach (MatchedPairRecord matchedPairRecord in this.m_databaseRecords.MatchedPairRecords)
      {
        if (matchedPairRecord.TimeStampUtc <= timeStampUtc)
          matchedPairRecord.SetCalculatedTimeOffsets(skewOffset, userOffset);
        else
          break;
      }
      foreach (STSCalibrationRecord calibrationRecord in this.m_databaseRecords.STSCalibrationRecords)
      {
        if (!(calibrationRecord.TimeStampUtc <= timeStampUtc))
          break;
        calibrationRecord.SetCalculatedTimeOffsets(skewOffset, userOffset);
      }
    }

    public void CalculateSensorSessions()
    {
      this.DatabaseRecords.SensorSessions = R2ReceiverTools.CreateSensorSessionList(this.DatabaseRecords);
    }

    public void CalculateSensorGapsBySession()
    {
      foreach (SensorSession sensorSession in this.DatabaseRecords.SensorSessions)
      {
        if (sensorSession.SessionSensorRecords.Count > 0)
        {
          sensorSession.SessionSensorRecords[0].GapCount = 0;
          sensorSession.SessionSensorRecords[0].GapTimeSpan = TimeSpan.Zero;
        }
        else if (sensorSession.HasSessionStarted)
        {
          for (int index = 0; index < this.DatabaseRecords.Sensor2Records.Count; ++index)
          {
            if (this.DatabaseRecords.Sensor2Records[index].SystemTime >= sensorSession.StartSystemTime && this.DatabaseRecords.Sensor2Records[index].SystemTime <= sensorSession.FinishSystemTime)
            {
              this.DatabaseRecords.Sensor2Records[index].GapCount = 0;
              this.DatabaseRecords.Sensor2Records[index].GapTimeSpan = TimeSpan.Zero;
              break;
            }
          }
        }
      }
    }

    public void AssignSensorRecordsToSensorSessions()
    {
      foreach (SensorSession sensorSession in this.DatabaseRecords.SensorSessions)
        sensorSession.SessionSensorRecords.Clear();
      IEnumerable<SensorSession> source = Enumerable.Where<SensorSession>((IEnumerable<SensorSession>) this.DatabaseRecords.SensorSessions, (Func<SensorSession, bool>) (session => session.HasSessionStarted));
      if (Enumerable.Count<SensorSession>(source) <= 0 || this.DatabaseRecords.Sensor2Records.Count <= 0)
        return;
      foreach (Sensor2Record sensor2Record in this.DatabaseRecords.Sensor2Records)
      {
        DateTime systemTime = sensor2Record.SystemTime;
        foreach (SensorSession sensorSession in source)
        {
          if (systemTime >= sensorSession.StartSystemTime && systemTime <= sensorSession.FinishSystemTime)
          {
            sensorSession.SessionSensorRecords.Add(sensor2Record);
            break;
          }
        }
      }
    }

    public void AssignMeterRecordsToSensorSessions()
    {
      foreach (SensorSession sensorSession in this.DatabaseRecords.SensorSessions)
        sensorSession.SessionMeterRecords.Clear();
      IEnumerable<SensorSession> source = Enumerable.Where<SensorSession>((IEnumerable<SensorSession>) this.DatabaseRecords.SensorSessions, (Func<SensorSession, bool>) (session => session.HasSessionStarted));
      if (Enumerable.Count<SensorSession>(source) <= 0 || this.DatabaseRecords.MeterRecords.Count <= 0)
        return;
      foreach (MeterRecord meterRecord in this.DatabaseRecords.MeterRecords)
      {
        DateTime systemTime = meterRecord.SystemTime;
        foreach (SensorSession sensorSession in source)
        {
          if (systemTime >= sensorSession.StartSystemTime && systemTime <= sensorSession.FinishSystemTime)
          {
            sensorSession.SessionMeterRecords.Add(meterRecord);
            break;
          }
        }
      }
    }

    public void AssignCalSetRecordsToSensorSessions()
    {
      foreach (SensorSession sensorSession in this.DatabaseRecords.SensorSessions)
        sensorSession.SessionCalSetRecords.Clear();
      IEnumerable<SensorSession> source = Enumerable.Where<SensorSession>((IEnumerable<SensorSession>) this.DatabaseRecords.SensorSessions, (Func<SensorSession, bool>) (session => session.HasSessionStarted));
      if (Enumerable.Count<SensorSession>(source) <= 0 || this.DatabaseRecords.STSCalibrationRecords.Count <= 0)
        return;
      foreach (STSCalibrationRecord calibrationRecord in this.DatabaseRecords.STSCalibrationRecords)
      {
        DateTime systemTime = calibrationRecord.SystemTime;
        foreach (SensorSession sensorSession in source)
        {
          if (systemTime >= sensorSession.StartSystemTime && systemTime <= sensorSession.FinishSystemTime)
          {
            sensorSession.SessionCalSetRecords.Add(calibrationRecord);
            break;
          }
        }
      }
    }

    public void CalculateMatchedPairsForSensorSessions()
    {
      foreach (SensorSession sensorSession in this.DatabaseRecords.SensorSessions)
        sensorSession.MatchedPairStats = (MatchedPairStat) null;
      foreach (SensorSession session in this.DatabaseRecords.SensorSessions)
      {
        List<MatchedPair> forSensorSession = R2ReceiverTools.FindMatchedPairsForSensorSession(session, (ushort) 40, (ushort) 400, true);
        MatchedPairStat matchedPairStat = new MatchedPairStat()
        {
          MinSMBG = 40,
          MaxSMBG = 400,
          MinEGV = 40,
          MaxEGV = 400,
          Name = "Overall"
        };
        foreach (MatchedPair matchedPair in forSensorSession)
          matchedPairStat.Evaluate(matchedPair);
        matchedPairStat.Calculate();
        session.MatchedPairStats = matchedPairStat;
      }
    }
  }
}

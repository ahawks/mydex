// Type: Dexcom.R2Receiver.R2DatabaseRecords
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using System;
using System.Collections.Generic;
using System.Xml;

namespace Dexcom.R2Receiver
{
  public class R2DatabaseRecords
  {
    private List<IGenericRecord> m_allRecords = new List<IGenericRecord>();
    private List<LogRecord> m_logRecords = new List<LogRecord>();
    private List<EventRecord> m_eventRecords = new List<EventRecord>();
    private List<MeterRecord> m_meterRecords = new List<MeterRecord>();
    private List<SensorRecord> m_sensorRecords = new List<SensorRecord>();
    private List<Sensor2Record> m_sensor2Records = new List<Sensor2Record>();
    private List<SettingsRecord> m_settingsRecords = new List<SettingsRecord>();
    private List<MatchedPairRecord> m_matchedPairRecords = new List<MatchedPairRecord>();
    private List<STSCalibrationRecord> m_stsCalRecords = new List<STSCalibrationRecord>();
    private List<ErrorLogRecord> m_errorLogRecords = new List<ErrorLogRecord>();
    private XmlElement m_xFirmwareHeader;
    private HardwareConfiguration m_receiverHardwareConfiguration;

    public List<IGenericRecord> AllRecords
    {
      get
      {
        return this.m_allRecords;
      }
    }

    public List<LogRecord> LogRecords
    {
      get
      {
        return this.m_logRecords;
      }
    }

    public List<EventRecord> EventRecords
    {
      get
      {
        return this.m_eventRecords;
      }
    }

    public List<MeterRecord> MeterRecords
    {
      get
      {
        return this.m_meterRecords;
      }
    }

    public List<SensorRecord> SensorRecords
    {
      get
      {
        return this.m_sensorRecords;
      }
    }

    public List<Sensor2Record> Sensor2Records
    {
      get
      {
        return this.m_sensor2Records;
      }
    }

    public List<SettingsRecord> SettingsRecords
    {
      get
      {
        return this.m_settingsRecords;
      }
    }

    public List<MatchedPairRecord> MatchedPairRecords
    {
      get
      {
        return this.m_matchedPairRecords;
      }
    }

    public List<STSCalibrationRecord> STSCalibrationRecords
    {
      get
      {
        return this.m_stsCalRecords;
      }
    }

    public List<ErrorLogRecord> ErrorLogRecords
    {
      get
      {
        return this.m_errorLogRecords;
      }
    }

    public List<SensorSession> SensorSessions { get; set; }

    public XmlElement ReceiverFirmwareHeader
    {
      get
      {
        return this.m_xFirmwareHeader;
      }
      set
      {
        this.m_xFirmwareHeader = value;
      }
    }

    public HardwareConfiguration ReceiverHardwareConfiguration
    {
      get
      {
        return this.m_receiverHardwareConfiguration;
      }
      set
      {
        this.m_receiverHardwareConfiguration = value;
      }
    }

    public R2DatabaseRecords()
    {
      this.SensorSessions = new List<SensorSession>();
    }

    private void DoAddRecord<T>(T record, List<T> recordList) where T : IGenericRecord
    {
      RecordIndex recordIndex = new RecordIndex();
      record.Tag = (object) recordIndex;
      recordIndex.IndexSameRecords = recordList.Count;
      recordIndex.IndexAllRecords = this.m_allRecords.Count;
      recordList.Add(record);
      this.m_allRecords.Add((IGenericRecord) record);
    }

    public T FindByKey<T>(int recordNumber, DateTime recordTimeStampUtc) where T : class, IGenericRecord
    {
      return this.FindByKey<T>(0, recordNumber, recordTimeStampUtc);
    }

    public T FindByKey<T>(int startingIndex, int recordNumber, DateTime recordTimeStampUtc) where T : class, IGenericRecord
    {
      int index = this.m_allRecords.BinarySearch((IGenericRecord) null, (IComparer<IGenericRecord>) new MatchRecordByKeyPredicate<IGenericRecord>(recordNumber, recordTimeStampUtc));
      if (index >= 0)
        return (T) this.m_allRecords[index];
      else
        return default (T);
    }

    public int FindIndexByKey(int recordNumber, DateTime recordTimeStampUtc)
    {
      return this.FindIndexByKey(0, recordNumber, recordTimeStampUtc);
    }

    public int FindIndexByKey(int startingIndex, int recordNumber, DateTime recordTimeStampUtc)
    {
      return this.m_allRecords.BinarySearch((IGenericRecord) null, (IComparer<IGenericRecord>) new MatchRecordByKeyPredicate<IGenericRecord>(recordNumber, recordTimeStampUtc));
    }

    public T FindByType<T>(int startingIndex, R2RecordType recordType) where T : class, IGenericRecord
    {
      for (int index = startingIndex; index < this.m_allRecords.Count; ++index)
      {
        if (this.m_allRecords[index].RecordType == recordType)
          return (T) this.m_allRecords[index];
      }
      return default (T);
    }

    public void AddRecord(LogRecord record)
    {
      this.DoAddRecord<LogRecord>(record, this.m_logRecords);
    }

    public void AddRecord(EventRecord record)
    {
      this.DoAddRecord<EventRecord>(record, this.m_eventRecords);
    }

    public void AddRecord(MeterRecord record)
    {
      this.DoAddRecord<MeterRecord>(record, this.m_meterRecords);
    }

    public void AddRecord(SensorRecord record)
    {
      this.DoAddRecord<SensorRecord>(record, this.m_sensorRecords);
    }

    public void AddRecord(Sensor2Record record)
    {
      this.DoAddRecord<Sensor2Record>(record, this.m_sensor2Records);
    }

    public void AddRecord(SettingsRecord record)
    {
      this.DoAddRecord<SettingsRecord>(record, this.m_settingsRecords);
    }

    public void AddRecord(MatchedPairRecord record)
    {
      this.DoAddRecord<MatchedPairRecord>(record, this.m_matchedPairRecords);
    }

    public void AddRecord(STSCalibrationRecord record)
    {
      this.DoAddRecord<STSCalibrationRecord>(record, this.m_stsCalRecords);
    }

    public void AddRecord(ErrorLogRecord record)
    {
      RecordIndex recordIndex = new RecordIndex();
      record.Tag = (object) recordIndex;
      recordIndex.IndexSameRecords = this.m_errorLogRecords.Count;
      recordIndex.IndexAllRecords = -1;
      this.m_errorLogRecords.Add(record);
    }
  }
}

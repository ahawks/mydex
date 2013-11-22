// Type: Dexcom.R2Receiver.STSCalibration4Record
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using Dexcom.Common;
using System;
using System.Xml;

namespace Dexcom.R2Receiver
{
  public class STSCalibration4Record : STSCalibrationRecord, IGenericRecord
  {
    private R2STSCalibration4Record m_record;

    public override XmlElement Xml
    {
      get
      {
        return this.ToXml();
      }
    }

    [ColumnInfo(DefaultWidth = 50, Ordinal = 0, Visible = true)]
    public override int RecordNumber
    {
      get
      {
        return this.m_record.m_key.m_recordNumber;
      }
    }

    [ColumnInfo(DefaultWidth = 50, Ordinal = 0, Visible = false)]
    public override uint BlockNumber
    {
      get
      {
        return this.m_record.m_key.m_block;
      }
    }

    [ColumnInfo(DefaultWidth = 160, FormatString = "yyyy'-'MM'-'dd' 'hh':'mm':'ss'.'fff tt", Ordinal = 1, Visible = true)]
    public override DateTime TimeStampUtc
    {
      get
      {
        return this.m_record.RecordTimeStamp;
      }
    }

    [ColumnInfo(DefaultWidth = 4, FormatString = "yyyy'-'MM'-'dd' 'hh':'mm':'ss'.'fff tt", Ordinal = 2, Visible = true)]
    public override DateTime CorrectedTimeStampUtc
    {
      get
      {
        return this.m_record.RecordTimeStamp + this.m_calculatedSkewOffset;
      }
    }

    [ColumnInfo(DefaultWidth = 160, FormatString = "yyyy'-'MM'-'dd' 'hh':'mm':'ss'.'fff tt", Ordinal = 3, Visible = true)]
    public override DateTime UserTimeStamp
    {
      get
      {
        return this.m_record.RecordTimeStamp + this.m_calculatedSkewOffset + this.m_calculatedUserOffset;
      }
    }

    [ColumnInfo(DefaultWidth = 100, Ordinal = 4, Visible = true)]
    public override string AlgorithmCalibrationState
    {
      get
      {
        if (Enum.IsDefined(typeof (R2AlgorithmState), (object) this.m_record.m_algorithmCalibrationState))
          return ((object) (R2AlgorithmState) this.m_record.m_algorithmCalibrationState).ToString();
        else
          return "Unknown State " + this.m_record.m_algorithmCalibrationState.ToString();
      }
    }

    [ColumnInfo(DefaultWidth = 50, Ordinal = 5, Visible = true)]
    public override byte AlgorithmCalibrationStateCode
    {
      get
      {
        return this.m_record.m_algorithmCalibrationState;
      }
    }

    [ColumnInfo(DefaultWidth = 50, Ordinal = 6, Visible = true)]
    public override byte CalSetPoints
    {
      get
      {
        return this.m_record.m_calSetPoints;
      }
    }

    [ColumnInfo(DefaultWidth = 160, FormatString = "yyyy'-'MM'-'dd' 'hh':'mm':'ss'.'fff tt", Ordinal = 11, Visible = true)]
    public override DateTime SensorInsertionTime
    {
      get
      {
        return R2ReceiverValues.R2BaseDateTime.AddMilliseconds((double) this.m_record.m_sensorInsertionTime);
      }
    }

    [ColumnInfo(DefaultWidth = 75, Ordinal = 12, Visible = true)]
    public override double Slope
    {
      get
      {
        return this.m_record.m_slope;
      }
    }

    [ColumnInfo(DefaultWidth = 75, Ordinal = 13, Visible = true)]
    public override double Intercept
    {
      get
      {
        return this.m_record.m_intercept;
      }
    }

    [ColumnInfo(DefaultWidth = 75, Ordinal = 14, Visible = true)]
    public override ushort SensorLife
    {
      get
      {
        return this.m_record.m_sensorLife;
      }
    }

    [ColumnInfo(DefaultWidth = 50, Ordinal = 20, Visible = true)]
    public byte Aberrations
    {
      get
      {
        return this.m_record.m_aberrations;
      }
    }

    [ColumnInfo(DefaultWidth = 50, Ordinal = 21, Visible = true)]
    public byte SevereResidualAberrantPoints
    {
      get
      {
        return this.m_record.m_severeResidualAberrantPoints;
      }
    }

    [ColumnInfo(DefaultWidth = 50, Ordinal = 22, Visible = true)]
    public byte SevereEgvAberrantPoints
    {
      get
      {
        return this.m_record.m_severeEgvAberrantPoints;
      }
    }

    [ColumnInfo(DefaultWidth = 50, Ordinal = 23, Visible = true)]
    public byte PointsToGetInCal
    {
      get
      {
        return this.m_record.m_pointsToGetInCal;
      }
    }

    [ColumnInfo(DefaultWidth = 50, Ordinal = 24, Visible = true)]
    public byte SevereDeltaResidualPoints
    {
      get
      {
        return this.m_record.m_severeDeltaResidualPoints;
      }
    }

    [ColumnInfo(DefaultWidth = 50, Ordinal = 25, Visible = true)]
    public bool IsNoisePeriod
    {
      get
      {
        return (int) this.m_record.m_isNoisePeriod == 1;
      }
    }

    [ColumnInfo(Visible = false)]
    public int LastValidityMeterRecordNumber
    {
      get
      {
        return this.m_record.m_lastValidityProcessedMeterRecord.m_recordNumber;
      }
    }

    [ColumnInfo(Visible = false)]
    public DateTime LastValidityMeterTimeStampUtc
    {
      get
      {
        return R2ReceiverValues.R2BaseDateTime.AddMilliseconds((double) this.m_record.m_lastValidityProcessedMeterRecord.m_timeStamp);
      }
    }

    [ColumnInfo(Visible = false)]
    public int LastCalibrationMeterRecordNumber
    {
      get
      {
        return this.m_record.m_lastCalibrationProcessedMeterRecord.m_recordNumber;
      }
    }

    [ColumnInfo(Visible = false)]
    public DateTime LastCalibrationMeterTimeStampUtc
    {
      get
      {
        return R2ReceiverValues.R2BaseDateTime.AddMilliseconds((double) this.m_record.m_lastCalibrationProcessedMeterRecord.m_timeStamp);
      }
    }

    [ColumnInfo(Visible = false)]
    public int CalSet0RecordNumber
    {
      get
      {
        return this.m_record.m_calibrationSet0.m_recordNumber;
      }
    }

    [ColumnInfo(Visible = false)]
    public int CalSet1RecordNumber
    {
      get
      {
        return this.m_record.m_calibrationSet1.m_recordNumber;
      }
    }

    [ColumnInfo(Visible = false)]
    public int CalSet2RecordNumber
    {
      get
      {
        return this.m_record.m_calibrationSet2.m_recordNumber;
      }
    }

    [ColumnInfo(Visible = false)]
    public int CalSet3RecordNumber
    {
      get
      {
        return this.m_record.m_calibrationSet3.m_recordNumber;
      }
    }

    [ColumnInfo(Visible = false)]
    public int CalSet4RecordNumber
    {
      get
      {
        return this.m_record.m_calibrationSet4.m_recordNumber;
      }
    }

    [ColumnInfo(Visible = false)]
    public int CalSet5RecordNumber
    {
      get
      {
        return this.m_record.m_calibrationSet5.m_recordNumber;
      }
    }

    [ColumnInfo(Visible = false)]
    public DateTime CalSet0TimeStampUtc
    {
      get
      {
        return R2ReceiverValues.R2BaseDateTime.AddMilliseconds((double) this.m_record.m_calibrationSet0.m_timeStamp);
      }
    }

    [ColumnInfo(Visible = false)]
    public DateTime CalSet1TimeStampUtc
    {
      get
      {
        return R2ReceiverValues.R2BaseDateTime.AddMilliseconds((double) this.m_record.m_calibrationSet1.m_timeStamp);
      }
    }

    [ColumnInfo(Visible = false)]
    public DateTime CalSet2TimeStampUtc
    {
      get
      {
        return R2ReceiverValues.R2BaseDateTime.AddMilliseconds((double) this.m_record.m_calibrationSet2.m_timeStamp);
      }
    }

    [ColumnInfo(Visible = false)]
    public DateTime CalSet3TimeStampUtc
    {
      get
      {
        return R2ReceiverValues.R2BaseDateTime.AddMilliseconds((double) this.m_record.m_calibrationSet3.m_timeStamp);
      }
    }

    [ColumnInfo(Visible = false)]
    public DateTime CalSet4TimeStampUtc
    {
      get
      {
        return R2ReceiverValues.R2BaseDateTime.AddMilliseconds((double) this.m_record.m_calibrationSet4.m_timeStamp);
      }
    }

    [ColumnInfo(Visible = false)]
    public DateTime CalSet5TimeStampUtc
    {
      get
      {
        return R2ReceiverValues.R2BaseDateTime.AddMilliseconds((double) this.m_record.m_calibrationSet5.m_timeStamp);
      }
    }

    public STSCalibration4Record(byte[] data, int offset)
    {
      this.m_recordType = (R2RecordType) data[offset];
      if (this.m_recordType == R2RecordType.STS_Calibration4)
        this.m_record = (R2STSCalibration4Record) R2ReceiverTools.DatabaseRecordFactory(this.m_recordType, data, offset);
      else if (this.m_recordType == R2RecordType.STS_Calibration3)
        this.SetR2Record((R2STSCalibration3Record) R2ReceiverTools.DatabaseRecordFactory(this.m_recordType, data, offset));
      else if (this.m_recordType == R2RecordType.STS_Calibration2)
        this.SetR2Record((R2STSCalibration2Record) R2ReceiverTools.DatabaseRecordFactory(this.m_recordType, data, offset));
      else
        this.SetR2Record((R2STSCalibration1Record) R2ReceiverTools.DatabaseRecordFactory(this.m_recordType, data, offset));
    }

    public override object GetInternalRecord()
    {
      return (object) this.m_record;
    }

    public void SetR2Record(R2STSCalibration4Record record)
    {
      this.m_recordType = R2RecordType.STS_Calibration4;
      this.m_record = record;
    }

    public void SetR2Record(R2STSCalibration3Record record)
    {
      this.m_recordType = R2RecordType.STS_Calibration3;
      this.m_record.m_header = record.m_header;
      this.m_record.m_key = record.m_key;
      this.m_record.m_algorithmCalibrationState = Convert.ToByte(record.m_algorithmCalibrationState);
      this.m_record.m_calSetPoints = Convert.ToByte(record.m_calSetPoints);
      this.m_record.m_aberrations = Convert.ToByte(record.m_aberrations);
      this.m_record.m_lastCalibrationProcessedMeterRecord = record.m_lastCalibrationProcessedMeterRecord;
      this.m_record.m_lastValidityProcessedMeterRecord = record.m_lastValidityProcessedMeterRecord;
      this.m_record.m_calibrationSet0 = record.m_calibrationSet0;
      this.m_record.m_calibrationSet1 = record.m_calibrationSet1;
      this.m_record.m_calibrationSet2 = record.m_calibrationSet2;
      this.m_record.m_calibrationSet3 = record.m_calibrationSet3;
      this.m_record.m_calibrationSet4 = record.m_calibrationSet4;
      this.m_record.m_calibrationSet5 = record.m_calibrationSet5;
      this.m_record.m_slope = record.m_slope;
      this.m_record.m_intercept = record.m_intercept;
      this.m_record.m_sensorInsertionTime = record.m_sensorInsertionTime;
      this.m_record.m_sensorLife = record.m_sensorLife;
      this.m_record.m_severeResidualAberrantPoints = record.m_severeResidualAberrantPoints;
      this.m_record.m_severeEgvAberrantPoints = record.m_severeEgvAberrantPoints;
      this.m_record.m_pointsToGetInCal = record.m_pointsToGetInCal;
      this.m_record.m_severeDeltaResidualPoints = (byte) 0;
      this.m_record.m_isNoisePeriod = (byte) 0;
      this.m_record.m_footer = record.m_footer;
    }

    public void SetR2Record(R2STSCalibration2Record record)
    {
      this.m_recordType = R2RecordType.STS_Calibration2;
      this.m_record.m_header = record.m_header;
      this.m_record.m_key = record.m_key;
      this.m_record.m_algorithmCalibrationState = Convert.ToByte(record.m_algorithmCalibrationState);
      this.m_record.m_calSetPoints = Convert.ToByte(record.m_calSetPoints);
      this.m_record.m_aberrations = Convert.ToByte(record.m_aberrations);
      this.m_record.m_lastCalibrationProcessedMeterRecord = record.m_lastCalibrationProcessedMeterRecord;
      this.m_record.m_lastValidityProcessedMeterRecord = record.m_lastValidityProcessedMeterRecord;
      this.m_record.m_calibrationSet0 = record.m_calibrationSet0;
      this.m_record.m_calibrationSet1 = record.m_calibrationSet1;
      this.m_record.m_calibrationSet2 = record.m_calibrationSet2;
      this.m_record.m_calibrationSet3 = record.m_calibrationSet3;
      this.m_record.m_calibrationSet4 = record.m_calibrationSet4;
      this.m_record.m_calibrationSet5 = record.m_calibrationSet5;
      this.m_record.m_slope = record.m_slope;
      this.m_record.m_intercept = record.m_intercept;
      this.m_record.m_sensorInsertionTime = record.m_sensorInsertionTime;
      this.m_record.m_sensorLife = record.m_sensorLife;
      this.m_record.m_severeResidualAberrantPoints = (byte) 0;
      this.m_record.m_severeEgvAberrantPoints = (byte) 0;
      this.m_record.m_pointsToGetInCal = (byte) 0;
      this.m_record.m_severeDeltaResidualPoints = (byte) 0;
      this.m_record.m_isNoisePeriod = (byte) 0;
      this.m_record.m_footer = record.m_footer;
    }

    public void SetR2Record(R2STSCalibration1Record record)
    {
      this.m_recordType = R2RecordType.STS_Calibration1;
      this.m_record.m_header = record.m_header;
      this.m_record.m_key = record.m_key;
      this.m_record.m_algorithmCalibrationState = Convert.ToByte(record.m_algorithmCalibrationState);
      this.m_record.m_calSetPoints = Convert.ToByte(record.m_calSetPoints);
      this.m_record.m_aberrations = Convert.ToByte(record.m_aberrations);
      this.m_record.m_lastCalibrationProcessedMeterRecord = record.m_lastCalibrationProcessedMeterRecord;
      this.m_record.m_lastValidityProcessedMeterRecord = record.m_lastValidityProcessedMeterRecord;
      this.m_record.m_calibrationSet0 = record.m_calibrationSet0;
      this.m_record.m_calibrationSet1 = record.m_calibrationSet1;
      this.m_record.m_calibrationSet2 = record.m_calibrationSet2;
      this.m_record.m_calibrationSet3 = record.m_calibrationSet3;
      this.m_record.m_calibrationSet4 = record.m_calibrationSet4;
      this.m_record.m_calibrationSet5 = record.m_calibrationSet5;
      this.m_record.m_slope = record.m_slope;
      this.m_record.m_intercept = record.m_intercept;
      this.m_record.m_sensorInsertionTime = record.m_sensorInsertionTime;
      this.m_record.m_sensorLife = (ushort) 0;
      this.m_record.m_severeResidualAberrantPoints = (byte) 0;
      this.m_record.m_severeEgvAberrantPoints = (byte) 0;
      this.m_record.m_pointsToGetInCal = (byte) 0;
      this.m_record.m_severeDeltaResidualPoints = (byte) 0;
      this.m_record.m_isNoisePeriod = (byte) 0;
      this.m_record.m_footer = record.m_footer;
    }

    public override XmlElement ToXml()
    {
      return this.ToXml(new XmlDocument());
    }

    public override XmlElement ToXml(XmlDocument xOwner)
    {
      XObject xobject = new XObject("STSCalibrationRecord", xOwner);
      xobject.SetAttribute("RecordType", ((object) this.RecordType).ToString());
      xobject.SetAttribute("RecordNumber", this.RecordNumber);
      xobject.SetAttribute("TimeStampUtc", this.TimeStampUtc);
      xobject.SetAttribute("CorrectedTimeStampUtc", this.CorrectedTimeStampUtc);
      xobject.SetAttribute("UserTimeStamp", this.UserTimeStamp);
      xobject.SetAttribute("AlgorithmCalibrationState", this.AlgorithmCalibrationState);
      xobject.SetAttribute("AlgorithmCalibrationStateCode", this.AlgorithmCalibrationStateCode);
      xobject.SetAttribute("CalSetPoints", this.CalSetPoints);
      xobject.SetAttribute("Aberrations", this.Aberrations);
      xobject.SetAttribute("SevereResidualAberrantPoints", this.SevereResidualAberrantPoints);
      xobject.SetAttribute("SevereEgvAberrantPoints", this.SevereEgvAberrantPoints);
      xobject.SetAttribute("PointsToGetInCal", this.PointsToGetInCal);
      xobject.SetAttribute("SevereDeltaResidualPoints", this.SevereDeltaResidualPoints);
      xobject.SetAttribute("SensorInsertionTime", this.SensorInsertionTime);
      xobject.SetAttribute("Slope", this.Slope);
      xobject.SetAttribute("Intercept", this.Intercept);
      xobject.SetAttribute("SensorLife", this.SensorLife);
      xobject.SetAttribute("IsNoisePeriod", this.IsNoisePeriod);
      return xobject.Element;
    }
  }
}

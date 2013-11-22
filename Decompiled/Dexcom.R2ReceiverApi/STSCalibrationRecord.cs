// Type: Dexcom.R2Receiver.STSCalibrationRecord
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using Dexcom.Common;
using System;
using System.Xml;

namespace Dexcom.R2Receiver
{
  public abstract class STSCalibrationRecord : IGenericRecord
  {
    protected TimeSpan m_calculatedSkewOffset = TimeSpan.Zero;
    protected TimeSpan m_calculatedUserOffset = TimeSpan.Zero;
    private object m_tag;
    protected R2RecordType m_recordType;

    public object Tag
    {
      get
      {
        return this.m_tag;
      }
      set
      {
        this.m_tag = value;
      }
    }

    public R2RecordType RecordType
    {
      get
      {
        return this.m_recordType;
      }
    }

    public abstract XmlElement Xml { get; }

    public DateTime SystemTime
    {
      get
      {
        return this.TimeStampUtc;
      }
    }

    public DateTime DisplayTime
    {
      get
      {
        return this.UserTimeStamp;
      }
    }

    [ColumnInfo(DefaultWidth = 50, Ordinal = 0, Visible = true)]
    public abstract int RecordNumber { get; }

    [ColumnInfo(DefaultWidth = 50, Ordinal = 0, Visible = false)]
    public abstract uint BlockNumber { get; }

    [ColumnInfo(DefaultWidth = 160, FormatString = "yyyy'-'MM'-'dd' 'hh':'mm':'ss'.'fff tt", Ordinal = 1, Visible = true)]
    public abstract DateTime TimeStampUtc { get; }

    [ColumnInfo(DefaultWidth = 4, FormatString = "yyyy'-'MM'-'dd' 'hh':'mm':'ss'.'fff tt", Ordinal = 2, Visible = true)]
    public abstract DateTime CorrectedTimeStampUtc { get; }

    [ColumnInfo(DefaultWidth = 160, FormatString = "yyyy'-'MM'-'dd' 'hh':'mm':'ss'.'fff tt", Ordinal = 3, Visible = true)]
    public abstract DateTime UserTimeStamp { get; }

    [ColumnInfo(DefaultWidth = 100, Ordinal = 4, Visible = true)]
    public abstract string AlgorithmCalibrationState { get; }

    [ColumnInfo(DefaultWidth = 50, Ordinal = 5, Visible = true)]
    public abstract byte AlgorithmCalibrationStateCode { get; }

    [ColumnInfo(DefaultWidth = 50, Ordinal = 6, Visible = true)]
    public abstract byte CalSetPoints { get; }

    [ColumnInfo(DefaultWidth = 160, FormatString = "yyyy'-'MM'-'dd' 'hh':'mm':'ss'.'fff tt", Ordinal = 11, Visible = true)]
    public abstract DateTime SensorInsertionTime { get; }

    [ColumnInfo(DefaultWidth = 75, Ordinal = 12, Visible = true)]
    public abstract double Slope { get; }

    [ColumnInfo(DefaultWidth = 75, Ordinal = 13, Visible = true)]
    public abstract double Intercept { get; }

    [ColumnInfo(DefaultWidth = 75, Ordinal = 14, Visible = true)]
    public abstract ushort SensorLife { get; }

    public static STSCalibrationRecord CreateInstance(byte[] data, int offset)
    {
      R2RecordType r2RecordType = (R2RecordType) data[offset];
      if ((uint) r2RecordType <= 13U)
      {
        if (r2RecordType != R2RecordType.STS_Calibration1 && r2RecordType != R2RecordType.STS_Calibration2)
          goto label_5;
      }
      else
      {
        switch (r2RecordType)
        {
          case R2RecordType.STS_Calibration3:
          case R2RecordType.STS_Calibration4:
            break;
          case R2RecordType.STS_Calibration5:
            return (STSCalibrationRecord) new STSCalibration5Record(data, offset);
          default:
            goto label_5;
        }
      }
      return (STSCalibrationRecord) new STSCalibration4Record(data, offset);
label_5:
      throw new ApplicationException("Attempt to create Calibration Record with incompatible record type.");
    }

    public void SetCalculatedTimeOffsets(TimeSpan skewOffset, TimeSpan userOffset)
    {
      this.m_calculatedSkewOffset = skewOffset;
      this.m_calculatedUserOffset = userOffset;
    }

    public int GetInternalRecordSize()
    {
      return R2ReceiverTools.GetRecordSize(this.m_recordType);
    }

    public abstract object GetInternalRecord();

    public abstract XmlElement ToXml();

    public abstract XmlElement ToXml(XmlDocument xOwner);
  }
}

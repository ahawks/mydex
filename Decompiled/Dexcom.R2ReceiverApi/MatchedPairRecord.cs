// Type: Dexcom.R2Receiver.MatchedPairRecord
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using Dexcom.Common;
using System;
using System.Xml;

namespace Dexcom.R2Receiver
{
  public class MatchedPairRecord : IGenericRecord
  {
    private TimeSpan m_calculatedSkewOffset = TimeSpan.Zero;
    private TimeSpan m_calculatedUserOffset = TimeSpan.Zero;
    private R2MatchedPairRecord m_record;
    private object m_tag;

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
        return this.m_record.RecordType;
      }
    }

    public XmlElement Xml
    {
      get
      {
        return this.ToXml();
      }
    }

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
    public int RecordNumber
    {
      get
      {
        return this.m_record.m_key.m_recordNumber;
      }
    }

    [ColumnInfo(DefaultWidth = 160, FormatString = "yyyy'-'MM'-'dd' 'hh':'mm':'ss'.'fff tt", Ordinal = 1, Visible = true)]
    public DateTime TimeStampUtc
    {
      get
      {
        return this.m_record.RecordTimeStamp;
      }
    }

    [ColumnInfo(DefaultWidth = 4, FormatString = "yyyy'-'MM'-'dd' 'hh':'mm':'ss'.'fff tt", Ordinal = 2, Visible = true)]
    public DateTime CorrectedTimeStampUtc
    {
      get
      {
        return this.m_record.RecordTimeStamp + this.m_calculatedSkewOffset;
      }
    }

    [ColumnInfo(DefaultWidth = 160, FormatString = "yyyy'-'MM'-'dd' 'hh':'mm':'ss'.'fff tt", Ordinal = 3, Visible = true)]
    public DateTime UserTimeStamp
    {
      get
      {
        return this.m_record.RecordTimeStamp + this.m_calculatedSkewOffset + this.m_calculatedUserOffset;
      }
    }

    [ColumnInfo(DefaultWidth = 50, Visible = false)]
    public int SensorRecordNumber
    {
      get
      {
        return this.m_record.m_sensorRecordkey.m_recordNumber;
      }
    }

    [ColumnInfo(DefaultWidth = 120, Visible = false)]
    public DateTime SensorTimeStampUtc
    {
      get
      {
        return R2ReceiverValues.R2BaseDateTime.AddMilliseconds((double) this.m_record.m_sensorRecordkey.m_timeStamp);
      }
    }

    [ColumnInfo(DefaultWidth = 50, Visible = false)]
    public int MeterRecordNumber
    {
      get
      {
        return this.m_record.m_meterRecordkey.m_recordNumber;
      }
    }

    [ColumnInfo(DefaultWidth = 120, Visible = false)]
    public DateTime MeterTimeStampUtc
    {
      get
      {
        return R2ReceiverValues.R2BaseDateTime.AddMilliseconds((double) this.m_record.m_meterRecordkey.m_timeStamp);
      }
    }

    [ColumnInfo(DefaultWidth = 50, Visible = false)]
    public uint BlockNumber
    {
      get
      {
        return this.m_record.m_key.m_block;
      }
    }

    public MatchedPairRecord(byte[] data, int offset)
    {
      this.m_record = (R2MatchedPairRecord) R2ReceiverTools.DatabaseRecordFactory(this.m_record.RecordType, data, offset);
    }

    public void SetCalculatedTimeOffsets(TimeSpan skewOffset, TimeSpan userOffset)
    {
      this.m_calculatedSkewOffset = skewOffset;
      this.m_calculatedUserOffset = userOffset;
    }

    public int GetInternalRecordSize()
    {
      return this.m_record.RecordSize;
    }

    public R2MatchedPairRecord GetInternalRecord()
    {
      return this.m_record;
    }

    public XmlElement ToXml()
    {
      return this.ToXml(new XmlDocument());
    }

    public XmlElement ToXml(XmlDocument xOwner)
    {
      XObject xobject = new XObject("MatchedPairRecord", xOwner);
      xobject.SetAttribute("RecordNumber", this.RecordNumber);
      xobject.SetAttribute("TimeStampUtc", this.TimeStampUtc);
      xobject.SetAttribute("CorrectedTimeStampUtc", this.CorrectedTimeStampUtc);
      xobject.SetAttribute("UserTimeStamp", this.UserTimeStamp);
      xobject.SetAttribute("SensorRecordNumber", this.SensorRecordNumber);
      xobject.SetAttribute("SensorTimeStampUtc", this.SensorTimeStampUtc);
      xobject.SetAttribute("MeterRecordNumber", this.MeterRecordNumber);
      xobject.SetAttribute("MeterTimeStampUtc", this.MeterTimeStampUtc);
      return xobject.Element;
    }
  }
}

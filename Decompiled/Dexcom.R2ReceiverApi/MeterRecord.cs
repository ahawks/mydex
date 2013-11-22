// Type: Dexcom.R2Receiver.MeterRecord
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using Dexcom.Common;
using System;
using System.Xml;

namespace Dexcom.R2Receiver
{
  public class MeterRecord : IGenericRecord
  {
    private TimeSpan m_calculatedSkewOffset = TimeSpan.Zero;
    private TimeSpan m_calculatedUserOffset = TimeSpan.Zero;
    private R2Meter2Record m_record;
    private R2RecordType m_recordType;
    private STSCalibrationRecord m_priorCalibrationRecord;
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

    public STSCalibrationRecord PriorCalibrationRecord
    {
      get
      {
        return this.m_priorCalibrationRecord;
      }
      set
      {
        this.m_priorCalibrationRecord = value;
      }
    }

    public R2RecordType RecordType
    {
      get
      {
        return this.m_recordType;
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

    public int RecordNumber
    {
      get
      {
        return this.m_record.m_key.m_recordNumber;
      }
    }

    public DateTime TimeStampUtc
    {
      get
      {
        return this.m_record.RecordTimeStamp;
      }
    }

    public DateTime CorrectedTimeStampUtc
    {
      get
      {
        return this.m_record.RecordTimeStamp + this.m_calculatedSkewOffset;
      }
    }

    public DateTime UserTimeStamp
    {
      get
      {
        return this.m_record.RecordTimeStamp + this.m_calculatedSkewOffset + this.m_calculatedUserOffset;
      }
    }

    public ushort GlucoseValue
    {
      get
      {
        if (this.m_recordType != R2RecordType.Meter3)
          return this.m_record.m_glucoseValue;
        else
          return (ushort) ((uint) this.m_record.m_glucoseValue & 4095U);
      }
    }

    public DateTime GlucoseTimeStamp
    {
      get
      {
        try
        {
          return R2ReceiverValues.R2BaseDateTime.AddMilliseconds((double) this.m_record.m_glocoseTimeStamp);
        }
        catch (ArgumentOutOfRangeException ex)
        {
          return CommonValues.EmptyDateTime;
        }
      }
    }

    public DateTime WorldTimeStamp
    {
      get
      {
        return R2ReceiverValues.R2BaseDateTime.AddMilliseconds((double) this.m_record.m_worldTimeStamp);
      }
    }

    public ushort MeterFlagCode
    {
      get
      {
        return this.m_record.m_footer.m_data;
      }
    }

    public string MeterFlag
    {
      get
      {
        string str = "Unknown Meter Flag";
        if (Enum.IsDefined(typeof (R2MeterFlags), (object) this.m_record.m_footer.m_data))
          str = ((object) (R2MeterFlags) this.m_record.m_footer.m_data).ToString();
        return str;
      }
    }

    public byte MeterTypeCode
    {
      get
      {
        return this.m_record.m_meterType;
      }
    }

    public string MeterType
    {
      get
      {
        string str = "Unknown Meter Type";
        if (Enum.IsDefined(typeof (R2MeterType), (object) this.m_record.m_meterType))
          str = ((object) (R2MeterType) this.m_record.m_meterType).ToString();
        return str;
      }
    }

    public ushort RawGlucoseValue
    {
      get
      {
        return this.m_record.m_glucoseValue;
      }
    }

    public uint BlockNumber
    {
      get
      {
        return this.m_record.m_key.m_block;
      }
    }

    public ushort MeterValue
    {
      get
      {
        return this.GlucoseValue;
      }
    }

    public DateTime MeterTime
    {
      get
      {
        return this.GlucoseTimeStamp;
      }
    }

    public DateTime MeterDisplayTime
    {
      get
      {
        return this.WorldTimeStamp;
      }
    }

    public MeterRecord(byte[] data, int offset)
    {
      this.m_recordType = (R2RecordType) data[offset];
      if (this.m_recordType == R2RecordType.Meter3)
        this.m_record = (R2Meter2Record) R2ReceiverTools.DatabaseRecordFactory(this.m_recordType, data, offset);
      else if (this.m_recordType == R2RecordType.Meter2)
        this.m_record = (R2Meter2Record) R2ReceiverTools.DatabaseRecordFactory(this.m_recordType, data, offset);
      else
        this.SetR2Record((R2MeterRecord) R2ReceiverTools.DatabaseRecordFactory(this.m_recordType, data, offset));
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

    public R2Meter2Record GetInternalRecord()
    {
      return this.m_record;
    }

    public void SetR2Record(R2Meter2Record record)
    {
      this.m_recordType = record.RecordType;
      this.m_record = record;
    }

    public void SetR2Record(R2MeterRecord record)
    {
      this.m_recordType = record.RecordType;
      this.m_record.m_header = record.m_header;
      this.m_record.m_key = record.m_key;
      this.m_record.m_meterType = (byte) 1;
      this.m_record.m_glucoseValue = record.m_glucoseValue;
      this.m_record.m_glocoseTimeStamp = record.m_glocoseTimeStamp;
      this.m_record.m_worldTimeStamp = record.m_worldTimeStamp;
      this.m_record.m_footer = record.m_footer;
    }

    public XmlElement ToXml()
    {
      return this.ToXml(new XmlDocument());
    }

    public XmlElement ToXml(XmlDocument xOwner)
    {
      XObject xobject = new XObject("MeterRecord", xOwner);
      xobject.SetAttribute("RecordNumber", this.RecordNumber);
      xobject.SetAttribute("TimeStampUtc", this.TimeStampUtc);
      xobject.SetAttribute("CorrectedTimeStampUtc", this.CorrectedTimeStampUtc);
      xobject.SetAttribute("UserTimeStamp", this.UserTimeStamp);
      xobject.SetAttribute("MeterType", this.MeterType);
      xobject.SetAttribute("MeterTypeCode", this.MeterTypeCode);
      xobject.SetAttribute("GlucoseValue", this.GlucoseValue);
      xobject.SetAttribute("GlucoseTimeStamp", this.GlucoseTimeStamp);
      xobject.SetAttribute("WorldTimeStamp", this.WorldTimeStamp);
      xobject.SetAttribute("MeterFlag", this.MeterFlag);
      xobject.SetAttribute("MeterFlagCode", this.MeterFlagCode);
      xobject.SetAttribute("RawGlucoseValue", this.RawGlucoseValue);
      return xobject.Element;
    }
  }
}

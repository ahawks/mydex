// Type: Dexcom.R2Receiver.SensorRecord
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using Dexcom.Common;
using System;
using System.Runtime.InteropServices;
using System.Xml;

namespace Dexcom.R2Receiver
{
  public class SensorRecord : IGenericRecord
  {
    private TimeSpan m_calculatedSkewOffset = TimeSpan.Zero;
    private TimeSpan m_calculatedUserOffset = TimeSpan.Zero;
    private R2SensorRecord m_record;
    private uint m_transmitterId;
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

    public short GlucoseValue
    {
      get
      {
        return (short) this.m_record.m_footer.m_updateableData;
      }
    }

    public uint BlockNumber
    {
      get
      {
        return this.m_record.m_key.m_block;
      }
    }

    public uint TransmitterId
    {
      get
      {
        return this.m_transmitterId;
      }
    }

    public string TransmitterCode
    {
      get
      {
        return R2ReceiverTools.ConvertTransmitterNumberToDisplayableCode(this.m_transmitterId);
      }
    }

    public SensorRecord(byte[] data, int offset)
    {
      this.m_record = (R2SensorRecord) R2ReceiverTools.DatabaseRecordFactory(this.m_record.RecordType, data, offset);
      if ((int) this.m_record.m_footer.m_updateableData == (int) ushort.MaxValue && (int) this.m_record.m_footer.m_updateableCrc == (int) ushort.MaxValue)
        return;
      int start = offset + Marshal.OffsetOf(this.m_record.GetType(), "m_footer").ToInt32() + Marshal.OffsetOf(this.m_record.m_footer.GetType(), "m_updateableData").ToInt32();
      int end = offset + Marshal.OffsetOf(this.m_record.GetType(), "m_footer").ToInt32() + Marshal.OffsetOf(this.m_record.m_footer.GetType(), "m_updateableCrc").ToInt32();
      if ((int) Crc.CalculateCrc16(data, start, end) != (int) this.m_record.m_footer.m_updateableCrc)
        throw new ApplicationException(string.Format("Bad updateable CRC in record {0}, Offset =  0x{1:X}", (object) this.m_record.GetType().Name, (object) offset));
    }

    public void SetCalculatedTimeOffsets(TimeSpan skewOffset, TimeSpan userOffset)
    {
      this.m_calculatedSkewOffset = skewOffset;
      this.m_calculatedUserOffset = userOffset;
    }

    public void SetTransmitterId(uint transmitterId)
    {
      this.m_transmitterId = transmitterId;
    }

    public int GetInternalRecordSize()
    {
      return this.m_record.RecordSize;
    }

    public XmlElement ToXml()
    {
      return this.ToXml(new XmlDocument());
    }

    public XmlElement ToXml(XmlDocument xOwner)
    {
      XObject xobject = new XObject("SensorRecord", xOwner);
      xobject.SetAttribute("RecordNumber", this.RecordNumber);
      xobject.SetAttribute("TimeStampUtc", this.TimeStampUtc);
      xobject.SetAttribute("CorrectedTimeStampUtc", this.CorrectedTimeStampUtc);
      xobject.SetAttribute("UserTimeStamp", this.UserTimeStamp);
      xobject.SetAttribute("GlucoseValue", this.GlucoseValue);
      xobject.SetAttribute("TransmitterId", this.TransmitterId);
      xobject.SetAttribute("TransmitterCode", this.TransmitterCode);
      return xobject.Element;
    }
  }
}

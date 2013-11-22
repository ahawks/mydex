// Type: Dexcom.R2Receiver.ErrorLogRecord
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using Dexcom.Common;
using Dexcom.Common.Data;
using System;
using System.Runtime.InteropServices;
using System.Xml;

namespace Dexcom.R2Receiver
{
  public class ErrorLogRecord : IGenericRecord
  {
    private TimeSpan m_calculatedSkewOffset = TimeSpan.Zero;
    private TimeSpan m_calculatedUserOffset = TimeSpan.Zero;
    private R2ErrorLog2Record m_record;
    private R2RecordType m_recordType;
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

    public bool IsInvalidRecord
    {
      get
      {
        return (long) this.m_record.m_timeStamp == -1L;
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
        return -1;
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

    public string Filename
    {
      get
      {
        return this.m_record.m_filename;
      }
    }

    public uint LineNumber
    {
      get
      {
        return this.m_record.m_lineNumber;
      }
    }

    public uint Address
    {
      get
      {
        return this.m_record.m_address;
      }
    }

    public ulong Argument1
    {
      get
      {
        return this.m_record.m_argData1;
      }
    }

    public ulong Argument2
    {
      get
      {
        return this.m_record.m_argData2;
      }
    }

    public ushort LogNumber
    {
      get
      {
        return this.m_record.m_logNumber;
      }
    }

    public byte Flags
    {
      get
      {
        return this.m_record.m_flags;
      }
    }

    public uint BlockNumber
    {
      get
      {
        return 0U;
      }
    }

    public ErrorLogRecord(byte[] data, int offset, uint version)
    {
      if ((int) version == 2)
        this.SetR2Record((R2ErrorLog2Record) R2ReceiverTools.DatabaseRecordFactory(R2RecordType.ErrorLog2, data, offset));
      else
        this.SetR2Record((R2ErrorLogRecord) R2ReceiverTools.DatabaseRecordFactory(R2RecordType.ErrorLog, data, offset));
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

    public R2ErrorLog2Record GetInternalRecord()
    {
      return this.m_record;
    }

    public void SetR2Record(R2ErrorLog2Record record)
    {
      this.m_recordType = R2RecordType.ErrorLog2;
      this.m_record = record;
    }

    public void SetR2Record(R2ErrorLogRecord record)
    {
      this.m_recordType = R2RecordType.ErrorLog;
      byte[] numArray = DataTools.ConvertObjectToBytes((object) record);
      byte[] bytes = DataTools.ConvertObjectToBytes((object) this.m_record);
      Array.Copy((Array) numArray, 0, (Array) bytes, 0, Marshal.OffsetOf(typeof (R2ErrorLog2Record), "m_pad0").ToInt32());
      this.m_record = (R2ErrorLog2Record) DataTools.ConvertBytesToObject(bytes, 0, typeof (R2ErrorLog2Record));
    }

    public XmlElement ToXml()
    {
      return this.ToXml(new XmlDocument());
    }

    public XmlElement ToXml(XmlDocument xOwner)
    {
      XObject xobject = new XObject("ErrorLogRecord", xOwner);
      xobject.SetAttribute("RecordNumber", this.RecordNumber);
      xobject.SetAttribute("TimeStampUtc", this.TimeStampUtc);
      xobject.SetAttribute("CorrectedTimeStampUtc", this.CorrectedTimeStampUtc);
      xobject.SetAttribute("UserTimeStamp", this.UserTimeStamp);
      xobject.SetAttribute("Filename", this.Filename);
      xobject.SetAttribute("LineNumber", this.LineNumber);
      xobject.SetAttribute("Address", this.Address);
      xobject.SetAttribute("Argument1", this.Argument1);
      xobject.SetAttribute("Argument2", this.Argument2);
      xobject.SetAttribute("LogNumber", this.LogNumber);
      xobject.SetAttribute("Flags", this.Flags);
      return xobject.Element;
    }
  }
}

// Type: Dexcom.R2Receiver.GenericRecord
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using System;
using System.Xml;

namespace Dexcom.R2Receiver
{
  public class GenericRecord
  {
    private int m_recordNumber;
    private uint m_recordBlockNumber;
    private R2RecordType m_recordType;
    private DateTime m_recordTimeStamp;
    private DateTime m_recordCorrectedTime;
    private DateTime m_recordUserTime;
    private XmlElement m_recordXml;

    public XmlElement Xml
    {
      get
      {
        return this.m_recordXml;
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

    public string RecordType
    {
      get
      {
        return ((object) this.m_recordType).ToString();
      }
    }

    public byte RecordTypeCode
    {
      get
      {
        return (byte) this.m_recordType;
      }
    }

    public int RecordNumber
    {
      get
      {
        return this.m_recordNumber;
      }
    }

    public DateTime TimeStampUtc
    {
      get
      {
        return this.m_recordTimeStamp;
      }
    }

    public DateTime CorrectedTimeStampUtc
    {
      get
      {
        return this.m_recordCorrectedTime;
      }
    }

    public DateTime UserTimeStamp
    {
      get
      {
        return this.m_recordUserTime;
      }
    }

    public string XmlString
    {
      get
      {
        return this.m_recordXml.OuterXml;
      }
    }

    public uint BlockNumber
    {
      get
      {
        return this.m_recordBlockNumber;
      }
    }

    public GenericRecord(IGenericRecord genericRecord)
    {
      this.m_recordNumber = genericRecord.RecordNumber;
      this.m_recordBlockNumber = genericRecord.BlockNumber;
      this.m_recordType = genericRecord.RecordType;
      this.m_recordTimeStamp = genericRecord.TimeStampUtc;
      this.m_recordCorrectedTime = genericRecord.CorrectedTimeStampUtc;
      this.m_recordUserTime = genericRecord.UserTimeStamp;
      this.m_recordXml = genericRecord.Xml;
    }
  }
}

// Type: Dexcom.ReceiverApi.InsertionTimeRecord
// Assembly: Dexcom.ReceiverCommercialApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 3F623F65-908D-40A7-87D3-892C5D1FFA2E
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.ReceiverCommercialApi.dll

using Dexcom.Common;
using System;
using System.Xml;

namespace Dexcom.ReceiverApi
{
  public class InsertionTimeRecord : IGenericRecord
  {
    private ReceiverInsertionTimeRecord m_internalRecord;

    public int RecordRevision { get; private set; }

    public DateTime InsertionTime
    {
      get
      {
        return this.m_internalRecord.InsertionTimeStamp;
      }
    }

    public bool IsInserted
    {
      get
      {
        return this.InsertionTime != ReceiverValues.EmptySensorInsertionTime;
      }
    }

    public SensorSessionState SessionState
    {
      get
      {
        return this.m_internalRecord.SessionState;
      }
    }

    public DateTime SystemTime
    {
      get
      {
        return this.m_internalRecord.SystemTimeStamp;
      }
    }

    public DateTime DisplayTime
    {
      get
      {
        return this.m_internalRecord.DisplayTimeStamp;
      }
    }

    public ReceiverRecordType RecordType
    {
      get
      {
        return this.m_internalRecord.RecordType;
      }
    }

    public uint RecordNumber { get; set; }

    public uint PageNumber { get; set; }

    public object Tag { get; set; }

    public InsertionTimeRecord(byte[] data, int offset)
      : this(data, offset, ReceiverTools.GetLatestSupportedRecordRevision(ReceiverRecordType.InsertionTime))
    {
    }

    public InsertionTimeRecord(byte[] data, int offset, int recordRevision)
    {
      this.m_internalRecord = (ReceiverInsertionTimeRecord) ReceiverTools.DatabaseRecordFactory(this.m_internalRecord.RecordType, recordRevision, data, offset);
      this.RecordRevision = recordRevision;
    }

    public XmlElement ToXml()
    {
      return this.ToXml(new XmlDocument());
    }

    public XmlElement ToXml(XmlDocument xOwner)
    {
      XObject xobject = new XObject("InsertionTimeRecord", xOwner);
      xobject.SetAttribute("RecordNumber", this.RecordNumber);
      xobject.SetAttribute("RecordRevision", this.RecordRevision);
      xobject.SetAttribute("SystemTime", this.SystemTime);
      xobject.SetAttribute("DisplayTime", this.DisplayTime);
      xobject.SetAttribute("SessionState", ((object) this.SessionState).ToString());
      xobject.SetAttribute("InsertionTime", this.InsertionTime);
      xobject.SetAttribute("IsInserted", this.IsInserted);
      return xobject.Element;
    }
  }
}

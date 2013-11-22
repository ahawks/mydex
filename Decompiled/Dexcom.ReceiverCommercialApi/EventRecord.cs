// Type: Dexcom.ReceiverApi.EventRecord
// Assembly: Dexcom.ReceiverCommercialApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 3F623F65-908D-40A7-87D3-892C5D1FFA2E
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.ReceiverCommercialApi.dll

using Dexcom.Common;
using System;
using System.Xml;

namespace Dexcom.ReceiverApi
{
  public class EventRecord : IGenericRecord
  {
    private ReceiverUserEventRecord m_internalRecord;

    public int RecordRevision { get; private set; }

    public UserEvent EventType
    {
      get
      {
        return this.m_internalRecord.EventType;
      }
    }

    public byte EventSubType
    {
      get
      {
        return this.m_internalRecord.EventSubType;
      }
    }

    public DateTime EventTime
    {
      get
      {
        return this.m_internalRecord.EventTimeStamp;
      }
    }

    public uint EventValue
    {
      get
      {
        return this.m_internalRecord.EventValue;
      }
    }

    public string EventTypeDescription
    {
      get
      {
        string str1 = "Unknown";
        if (Enum.IsDefined(typeof (UserEvent), (object) this.m_internalRecord.EventType))
        {
          switch (this.m_internalRecord.EventType)
          {
            case UserEvent.Carbs:
              str1 = "Carbs";
              break;
            case UserEvent.Insulin:
              str1 = "Insulin";
              break;
            case UserEvent.Health:
              string str2 = "Unknown";
              if (Enum.IsDefined(typeof (Health), (object) this.m_internalRecord.EventSubType))
                str2 = ((object) (Health) this.m_internalRecord.EventSubType).ToString();
              str1 = string.Format("Health {0}", (object) str2);
              break;
            case UserEvent.Exercise:
              string str3 = "Unknown";
              if (Enum.IsDefined(typeof (Exercise), (object) this.m_internalRecord.EventSubType))
                str3 = ((object) (Exercise) this.m_internalRecord.EventSubType).ToString();
              str1 = string.Format("Exercise {0}", (object) str3);
              break;
            default:
              str1 = "Unknown";
              break;
          }
        }
        return str1;
      }
    }

    public string EventValueDescription
    {
      get
      {
        string str = string.Empty;
        if (Enum.IsDefined(typeof (UserEvent), (object) this.m_internalRecord.EventType))
        {
          switch (this.m_internalRecord.EventType)
          {
            case UserEvent.Carbs:
              str = string.Format("{0} {1}", (object) this.m_internalRecord.EventValue, (object) "grams");
              break;
            case UserEvent.Insulin:
              str = string.Format("{0:N2} {1}", (object) ((double) this.m_internalRecord.EventValue / 100.0), (object) "units");
              break;
            case UserEvent.Health:
              str = string.Empty;
              break;
            case UserEvent.Exercise:
              str = string.Format("{0} {1}", (object) this.m_internalRecord.EventValue, (object) "minutes");
              break;
          }
        }
        return str;
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

    public EventRecord(byte[] data, int offset)
      : this(data, offset, ReceiverTools.GetLatestSupportedRecordRevision(ReceiverRecordType.UserEventData))
    {
    }

    public EventRecord(byte[] data, int offset, int recordRevision)
    {
      this.m_internalRecord = (ReceiverUserEventRecord) ReceiverTools.DatabaseRecordFactory(this.m_internalRecord.RecordType, recordRevision, data, offset);
      this.RecordRevision = recordRevision;
    }

    public XmlElement ToXml()
    {
      return this.ToXml(new XmlDocument());
    }

    public XmlElement ToXml(XmlDocument xOwner)
    {
      XObject xobject = new XObject("EventRecord", xOwner);
      xobject.SetAttribute("RecordNumber", this.RecordNumber);
      xobject.SetAttribute("RecordRevision", this.RecordRevision);
      xobject.SetAttribute("SystemTime", this.SystemTime);
      xobject.SetAttribute("DisplayTime", this.DisplayTime);
      xobject.SetAttribute("EventType", ((object) this.EventType).ToString());
      xobject.SetAttribute("EventSubType", this.EventSubType);
      xobject.SetAttribute("EventTime", this.EventTime);
      xobject.SetAttribute("EventValue", this.EventValue);
      xobject.SetAttribute("EventTypeDescription", this.EventTypeDescription);
      xobject.SetAttribute("EventValueDescription", this.EventValueDescription);
      return xobject.Element;
    }
  }
}

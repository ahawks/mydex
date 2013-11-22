// Type: Dexcom.Common.Data.XEventData
// Assembly: Dexcom.Common.Data, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 64B9BFC3-C891-4315-B216-574914861A80
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.Data.dll

using Dexcom.Common;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Security;
using System.Xml;
using System.Xml.Serialization;

namespace Dexcom.Common.Data
{
  [Serializable]
  public class XEventData : XObject, ISerializable
  {
    public const string Tag = "EventData";

    [TypeConverter(typeof (OnlineGuidConverter))]
    [XmlAttribute]
    [ColumnInfo(Visible = false)]
    public new Guid Id
    {
      get
      {
        return this.GetAttributeAsGuid("Id");
      }
      set
      {
        this.SetAttribute("Id", value);
      }
    }

    [XmlAttribute]
    [ColumnInfo(Visible = false)]
    public new string Name
    {
      get
      {
        return this.GetAttribute("Name");
      }
      set
      {
        this.SetAttribute("Name", value.Trim());
      }
    }

    [ColumnInfo(Visible = false)]
    [XmlAttribute]
    public new string Description
    {
      get
      {
        return this.GetAttribute("Description");
      }
      set
      {
        this.SetAttribute("Description", value.Trim());
      }
    }

    [ColumnInfo(Visible = false)]
    [TypeConverter(typeof (OnlineGuidConverter))]
    [XmlAttribute]
    public Guid DeviceId
    {
      get
      {
        return this.GetAttributeAsGuid("DeviceId");
      }
      set
      {
        this.SetAttribute("DeviceId", value);
      }
    }

    [ColumnInfo(Visible = false)]
    [XmlAttribute]
    [TypeConverter(typeof (OnlineGuidConverter))]
    public uint RecordNumber
    {
      get
      {
        return this.GetAttribute<uint>("RecordNumber");
      }
      set
      {
        this.SetAttribute("RecordNumber", value);
      }
    }

    [XmlAttribute]
    [ColumnInfo(Visible = false)]
    public DateTime DisplayTime
    {
      get
      {
        return this.GetAttribute<DateTime>("DisplayTime");
      }
      set
      {
        this.SetAttribute("DisplayTime", value);
      }
    }

    [XmlAttribute]
    [ColumnInfo(Visible = false)]
    public DateTime InternalTime
    {
      get
      {
        return this.GetAttribute<DateTime>("InternalTime");
      }
      set
      {
        this.SetAttribute("InternalTime", value);
      }
    }

    [ColumnInfo(Visible = false)]
    [XmlAttribute]
    public DateTime EventTime
    {
      get
      {
        if (this.HasAttribute("EventTime"))
          return this.GetAttribute<DateTime>("EventTime");
        else
          return this.InternalTime;
      }
      set
      {
        this.SetAttribute("EventTime", value);
      }
    }

    [ColumnInfo(Visible = false)]
    [XmlAttribute]
    public EventData.EventType TypeOfEvent
    {
      get
      {
        return (EventData.EventType) this.GetAttributeAsEnum("TypeOfEvent", typeof (EventData.EventType));
      }
      set
      {
        this.SetAttribute("TypeOfEvent", ((object) value).ToString());
      }
    }

    [ColumnInfo(Visible = false)]
    [XmlAttribute]
    public int Value
    {
      get
      {
        return this.GetAttribute<int>("Value");
      }
      set
      {
        this.SetAttribute("Value", value);
      }
    }

    public XEventData()
      : this(new XmlDocument())
    {
    }

    public XEventData(XmlDocument ownerDocument)
      : base("EventData", ownerDocument)
    {
      this.DeviceId = CommonValues.NoneId;
      this.RecordNumber = 0U;
      this.DisplayTime = CommonValues.EmptyDateTime;
      this.InternalTime = CommonValues.EmptyDateTime;
      this.EventTime = CommonValues.EmptyDateTime;
      this.TypeOfEvent = EventData.EventType.Unknown;
      this.Value = 0;
    }

    public XEventData(XmlElement element)
      : base(element)
    {
    }

    protected XEventData(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    [SecurityCritical]
    public new void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      base.GetObjectData(info, context);
    }
  }
}

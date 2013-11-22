// Type: Dexcom.Common.Data.XTimeOffsetData
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
  public class XTimeOffsetData : XObject, ISerializable
  {
    public const string Tag = "TimeOffsetData";

    [XmlAttribute]
    [TypeConverter(typeof (OnlineGuidConverter))]
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

    [XmlAttribute]
    [ColumnInfo(Visible = false)]
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

    [XmlAttribute]
    [TypeConverter(typeof (OnlineGuidConverter))]
    [ColumnInfo(Visible = false)]
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
    [TypeConverter(typeof (OnlineGuidConverter))]
    [XmlAttribute]
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

    [ColumnInfo(Visible = false)]
    [XmlAttribute]
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
    public TimeSpan DisplayOffset
    {
      get
      {
        return this.GetAttribute<TimeSpan>("DisplayOffset");
      }
      set
      {
        this.SetAttribute("DisplayOffset", value);
      }
    }

    public XTimeOffsetData()
      : this(new XmlDocument())
    {
    }

    public XTimeOffsetData(XmlDocument ownerDocument)
      : base("TimeOffsetData", ownerDocument)
    {
      this.DeviceId = CommonValues.NoneId;
      this.RecordNumber = 0U;
      this.InternalTime = CommonValues.EmptyDateTime;
      this.DisplayOffset = CommonValues.EmptyTimeSpan;
    }

    public XTimeOffsetData(XmlElement element)
      : base(element)
    {
    }

    protected XTimeOffsetData(SerializationInfo info, StreamingContext context)
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

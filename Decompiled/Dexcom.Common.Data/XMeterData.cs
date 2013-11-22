// Type: Dexcom.Common.Data.XMeterData
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
  public class XMeterData : XObject, ISerializable
  {
    public const string Tag = "MeterData";

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

    [XmlAttribute]
    [TypeConverter(typeof (OnlineGuidConverter))]
    [ColumnInfo(Visible = false)]
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

    [ColumnInfo(Visible = false)]
    [XmlAttribute]
    public DateTime MeterTime
    {
      get
      {
        if (this.HasAttribute("MeterTime"))
          return this.GetAttribute<DateTime>("MeterTime");
        else
          return this.InternalTime;
      }
      set
      {
        this.SetAttribute("MeterTime", value);
      }
    }

    [ColumnInfo(Visible = false)]
    [XmlAttribute]
    public DateTime MeterDisplayTime
    {
      get
      {
        return this.GetAttribute<DateTime>("MeterDisplayTime");
      }
      set
      {
        this.SetAttribute("MeterDisplayTime", value);
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

    [XmlAttribute]
    [ColumnInfo(Visible = false)]
    public short Value
    {
      get
      {
        return this.GetAttribute<short>("Value");
      }
      set
      {
        this.SetAttribute("Value", value);
      }
    }

    public XMeterData()
      : this(new XmlDocument())
    {
    }

    public XMeterData(XmlDocument ownerDocument)
      : base("MeterData", ownerDocument)
    {
      this.DeviceId = CommonValues.NoneId;
      this.RecordNumber = 0U;
      this.DisplayTime = CommonValues.EmptyDateTime;
      this.InternalTime = CommonValues.EmptyDateTime;
      this.MeterTime = CommonValues.EmptyDateTime;
      this.MeterDisplayTime = CommonValues.EmptyDateTime;
      this.Value = (short) 0;
    }

    public XMeterData(XmlElement element)
      : base(element)
    {
    }

    protected XMeterData(SerializationInfo info, StreamingContext context)
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

// Type: Dexcom.Common.Data.XGlucoseData
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
  public class XGlucoseData : XObject, ISerializable
  {
    public const string Tag = "GlucoseData";

    [ColumnInfo(Visible = false)]
    [XmlAttribute]
    [TypeConverter(typeof (OnlineGuidConverter))]
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
    [ColumnInfo(Visible = false)]
    [TypeConverter(typeof (OnlineGuidConverter))]
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

    [TypeConverter(typeof (OnlineGuidConverter))]
    [XmlAttribute]
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

    [ColumnInfo(Visible = false)]
    [XmlAttribute]
    public int GapCount
    {
      get
      {
        return this.GetAttribute<int>("GapCount");
      }
      set
      {
        this.SetAttribute("GapCount", value);
      }
    }

    [ColumnInfo(Visible = false)]
    [XmlAttribute]
    public TimeSpan GapTimeSpan
    {
      get
      {
        return this.GetAttribute<TimeSpan>("GapTimeSpan");
      }
      set
      {
        this.SetAttribute("GapTimeSpan", value);
      }
    }

    public XGlucoseData()
      : this(new XmlDocument())
    {
    }

    public XGlucoseData(XmlDocument ownerDocument)
      : base("GlucoseData", ownerDocument)
    {
      this.DeviceId = CommonValues.NoneId;
      this.RecordNumber = 0U;
      this.DisplayTime = CommonValues.EmptyDateTime;
      this.InternalTime = CommonValues.EmptyDateTime;
      this.Value = (short) 0;
      this.GapCount = 0;
      this.GapTimeSpan = CommonValues.EmptyTimeSpan;
    }

    public XGlucoseData(XmlElement element)
      : base(element)
    {
    }

    protected XGlucoseData(SerializationInfo info, StreamingContext context)
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

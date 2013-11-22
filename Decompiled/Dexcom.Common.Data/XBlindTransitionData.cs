// Type: Dexcom.Common.Data.XBlindTransitionData
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
  public class XBlindTransitionData : XObject, ISerializable
  {
    public const string Tag = "BlindTransitionData";

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

    [ColumnInfo(Visible = false)]
    [XmlAttribute]
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

    [TypeConverter(typeof (OnlineGuidConverter))]
    [ColumnInfo(Visible = false)]
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
    public bool IsBlinded
    {
      get
      {
        return this.GetAttribute<bool>("IsBlinded");
      }
      set
      {
        this.SetAttribute("IsBlinded", value);
      }
    }

    public XBlindTransitionData()
      : this(new XmlDocument())
    {
    }

    public XBlindTransitionData(XmlDocument ownerDocument)
      : base("BlindTransitionData", ownerDocument)
    {
      this.DeviceId = CommonValues.NoneId;
      this.RecordNumber = 0U;
      this.DisplayTime = CommonValues.EmptyDateTime;
      this.InternalTime = CommonValues.EmptyDateTime;
      this.IsBlinded = false;
    }

    public XBlindTransitionData(XmlElement element)
      : base(element)
    {
    }

    protected XBlindTransitionData(SerializationInfo info, StreamingContext context)
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

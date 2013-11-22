// Type: Dexcom.Common.Data.XSensorSessionData
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
  public class XSensorSessionData : XObject, ISerializable
  {
    public const string Tag = "SensorSessionData";

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
    [XmlAttribute]
    public DateTime StartDisplayTime
    {
      get
      {
        return this.GetAttribute<DateTime>("StartDisplayTime");
      }
      set
      {
        this.SetAttribute("StartDisplayTime", value);
      }
    }

    [ColumnInfo(Visible = false)]
    [XmlAttribute]
    public DateTime FinishDisplayTime
    {
      get
      {
        return this.GetAttribute<DateTime>("FinishDisplayTime");
      }
      set
      {
        this.SetAttribute("FinishDisplayTime", value);
      }
    }

    [XmlAttribute]
    [ColumnInfo(Visible = false)]
    public DateTime StartInternalTime
    {
      get
      {
        return this.GetAttribute<DateTime>("StartInternalTime");
      }
      set
      {
        this.SetAttribute("StartInternalTime", value);
      }
    }

    [ColumnInfo(Visible = false)]
    [XmlAttribute]
    public DateTime FinishInternalTime
    {
      get
      {
        return this.GetAttribute<DateTime>("FinishInternalTime");
      }
      set
      {
        this.SetAttribute("FinishInternalTime", value);
      }
    }

    [XmlAttribute]
    [ColumnInfo(Visible = false)]
    [TypeConverter(typeof (OnlineGuidConverter))]
    public uint StartRecordNumber
    {
      get
      {
        return this.GetAttribute<uint>("StartRecordNumber");
      }
      set
      {
        this.SetAttribute("StartRecordNumber", value);
      }
    }

    [TypeConverter(typeof (OnlineGuidConverter))]
    [XmlAttribute]
    [ColumnInfo(Visible = false)]
    public uint FinishRecordNumber
    {
      get
      {
        return this.GetAttribute<uint>("FinishRecordNumber");
      }
      set
      {
        this.SetAttribute("FinishRecordNumber", value);
      }
    }

    public XSensorSessionData()
      : this(new XmlDocument())
    {
    }

    public XSensorSessionData(XmlDocument ownerDocument)
      : base("SensorSessionData", ownerDocument)
    {
      this.DeviceId = CommonValues.NoneId;
      this.StartDisplayTime = CommonValues.EmptyDateTime;
      this.FinishDisplayTime = CommonValues.EmptyDateTime;
      this.StartInternalTime = CommonValues.EmptyDateTime;
      this.FinishInternalTime = CommonValues.EmptyDateTime;
      this.StartRecordNumber = 0U;
      this.FinishRecordNumber = 0U;
    }

    public XSensorSessionData(XmlElement element)
      : base(element)
    {
    }

    protected XSensorSessionData(SerializationInfo info, StreamingContext context)
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

// Type: Dexcom.Common.Data.XDeviceDownloadEntry
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
  public class XDeviceDownloadEntry : XObject, ISerializable
  {
    public const string Tag = "DeviceDownloadEntry";

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
    [ColumnInfo(Visible = false)]
    public DateTimeOffset DateCreated
    {
      get
      {
        return this.GetAttributeAsDateTimeOffset("DateCreated");
      }
      set
      {
        this.SetAttribute("DateCreated", value);
      }
    }

    [TypeConverter(typeof (OnlineGuidConverter))]
    [XmlAttribute]
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
    [ColumnInfo(Visible = false)]
    public string SerialNumber
    {
      get
      {
        return this.GetAttribute("SerialNumber");
      }
      set
      {
        this.SetAttribute("SerialNumber", value.Trim());
      }
    }

    [XmlAttribute]
    [ColumnInfo(Visible = false)]
    public uint Crc32
    {
      get
      {
        return this.GetAttribute<uint>("Crc32");
      }
      set
      {
        this.SetAttribute("Crc32", value);
      }
    }

    public XDeviceDownloadEntry()
      : this(new XmlDocument())
    {
    }

    public XDeviceDownloadEntry(XmlDocument ownerDocument)
      : base("DeviceDownloadEntry", ownerDocument)
    {
      this.Id = CommonValues.NoneId;
      this.DeviceId = CommonValues.NoneId;
      this.SerialNumber = string.Empty;
      this.DateCreated = (DateTimeOffset) CommonValues.EmptyDateTime;
      this.Crc32 = 0U;
    }

    public XDeviceDownloadEntry(XmlElement element)
      : base(element)
    {
    }

    protected XDeviceDownloadEntry(SerializationInfo info, StreamingContext context)
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

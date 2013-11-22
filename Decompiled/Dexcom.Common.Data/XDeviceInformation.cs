// Type: Dexcom.Common.Data.XDeviceInformation
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
  public class XDeviceInformation : XObject, ISerializable
  {
    public const string Tag = "DeviceInformation";
    public const string DownloadHistoryTag = "DownloadHistory";

    [XmlAttribute]
    [ColumnInfo(Visible = false)]
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

    [XmlAttribute]
    [ColumnInfo(Visible = false)]
    public bool IsActive
    {
      get
      {
        return this.GetAttributeAsBool("IsActive");
      }
      set
      {
        this.SetAttribute("IsActive", value);
      }
    }

    public DeviceInformation.DeviceType KindOfDevice
    {
      get
      {
        return (DeviceInformation.DeviceType) this.GetAttributeAsEnum("KindOfDevice", typeof (DeviceInformation.DeviceType));
      }
      set
      {
        this.SetAttribute("KindOfDevice", ((object) value).ToString());
      }
    }

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

    public string ProductName
    {
      get
      {
        return this.GetAttribute("ProductName");
      }
      set
      {
        this.SetAttribute("ProductName", value.Trim());
      }
    }

    public string ProductVersion
    {
      get
      {
        return this.GetAttribute("ProductVersion");
      }
      set
      {
        this.SetAttribute("ProductVersion", value.Trim());
      }
    }

    public string DatabaseVersion
    {
      get
      {
        return this.GetAttribute("DatabaseVersion");
      }
      set
      {
        this.SetAttribute("DatabaseVersion", value.Trim());
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

    [XmlAttribute]
    [ColumnInfo(Visible = false)]
    public DateTimeOffset DateModified
    {
      get
      {
        return this.GetAttributeAsDateTimeOffset("DateModified");
      }
      set
      {
        this.SetAttribute("DateModified", value);
      }
    }

    public XCollection<XDeviceDownloadEntry> DownloadHistory
    {
      get
      {
        return new XCollection<XDeviceDownloadEntry>(this.Element.SelectSingleNode("DownloadHistory") as XmlElement);
      }
    }

    public XDeviceInformation()
      : this(new XmlDocument())
    {
    }

    public XDeviceInformation(XmlDocument ownerDocument)
      : base("DeviceInformation", ownerDocument)
    {
      DateTimeOffset now = DateTimeOffset.Now;
      this.Id = Guid.NewGuid();
      this.DeviceId = CommonValues.NoneId;
      this.IsActive = true;
      this.KindOfDevice = DeviceInformation.DeviceType.Unknown;
      this.SerialNumber = string.Empty;
      this.ProductName = string.Empty;
      this.ProductVersion = string.Empty;
      this.DatabaseVersion = string.Empty;
      this.DateCreated = now;
      this.DateModified = now;
      this.Element.AppendChild((XmlNode) new XCollection<XDeviceDownloadEntry>("DownloadHistory", ownerDocument).Element);
    }

    public XDeviceInformation(XmlElement element)
      : base(element)
    {
    }

    protected XDeviceInformation(SerializationInfo info, StreamingContext context)
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

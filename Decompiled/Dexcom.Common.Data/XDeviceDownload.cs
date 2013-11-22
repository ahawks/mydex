// Type: Dexcom.Common.Data.XDeviceDownload
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
  public class XDeviceDownload : XObject, ISerializable
  {
    public const string Tag = "DeviceDownload";
    public const string DownloadDataTag = "DownloadData";

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

    [ColumnInfo(Visible = false)]
    [XmlAttribute]
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

    [ColumnInfo(Visible = false)]
    [XmlAttribute]
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

    public byte[] DownloadData
    {
      get
      {
        byte[] numArray = (byte[]) null;
        XmlElement sourceElement = this.Element.SelectSingleNode("DownloadData") as XmlElement;
        if (DataTools.IsCompressedElement(sourceElement))
          numArray = (byte[]) DataTools.DecompressElementWithHint(sourceElement);
        return numArray;
      }
      set
      {
        XmlElement xmlElement = this.Element.SelectSingleNode("DownloadData") as XmlElement;
        XmlElement compressedElement = DataTools.CreateCompressedElement(value, "DownloadData", this.Element.OwnerDocument, DataTools.CompressedHintType.Binary);
        if (xmlElement == null)
          this.Element.AppendChild((XmlNode) compressedElement);
        else
          this.Element.ReplaceChild((XmlNode) compressedElement, (XmlNode) xmlElement);
      }
    }

    public uint Crc32
    {
      get
      {
        uint num = 0U;
        XObject xobject = new XObject(this.Element.SelectSingleNode("DownloadData") as XmlElement);
        if (xobject.IsNotNull() && xobject.HasAttribute("Crc32"))
          num = xobject.GetAttribute<uint>("Crc32");
        return num;
      }
    }

    public int Size
    {
      get
      {
        int num = 0;
        XObject xobject = new XObject(this.Element.SelectSingleNode("DownloadData") as XmlElement);
        if (xobject.IsNotNull() && xobject.HasAttribute("Size"))
          num = xobject.GetAttribute<int>("Size");
        return num;
      }
    }

    public XDeviceDownload()
      : this(new XmlDocument())
    {
    }

    public XDeviceDownload(XmlDocument ownerDocument)
      : base("DeviceDownload", ownerDocument)
    {
      DateTimeOffset now = DateTimeOffset.Now;
      this.Id = Guid.NewGuid();
      this.Name = string.Empty;
      this.Description = string.Empty;
      this.DeviceId = CommonValues.NoneId;
      this.SerialNumber = string.Empty;
      this.DateCreated = now;
      this.DateModified = now;
      this.Element.AppendChild((XmlNode) DataTools.GetXApplicationInfo(ownerDocument).Element);
      this.Element.AppendChild((XmlNode) DataTools.GetXComputerInfo(ownerDocument).Element);
      this.DownloadData = (byte[]) null;
      if (string.Compare(Environment.UserDomainName, "DEXCOM", StringComparison.InvariantCultureIgnoreCase) == 0)
        return;
      DataTools.DeidentifyComputerInfo(this.Element);
    }

    public XDeviceDownload(XmlElement element)
      : base(element)
    {
    }

    protected XDeviceDownload(SerializationInfo info, StreamingContext context)
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

// Type: Dexcom.Common.Data.XPatientContext
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
  public class XPatientContext : XObject, ISerializable
  {
    public const string Tag = "PatientContext";
    public const string A1cDataCollectionTag = "A1cDataCollection";
    public const string DeviceHistoryTag = "DeviceHistory";

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
    [TypeConverter(typeof (OnlineGuidConverter))]
    [XmlAttribute]
    public Guid CacheId
    {
      get
      {
        return this.GetAttributeAsGuid("CacheId");
      }
      set
      {
        this.SetAttribute("CacheId", value);
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

    [ColumnInfo(Visible = false)]
    [XmlAttribute]
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

    [ColumnInfo(DefaultWidth = 50, DisplayKey = "PatientManagerForm_IsActive", DisplayName = "Is Active", Ordinal = 30, Visible = false)]
    [XmlAttribute]
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

    [XmlAttribute]
    [ColumnInfo(DefaultWidth = 50, DisplayKey = "PatientManagerForm_IsActive", DisplayName = "Is Active", Ordinal = 30, Visible = true)]
    public string IsActiveString
    {
      get
      {
        return this.IsActive.ToString();
      }
    }

    [ColumnInfo(DefaultWidth = 50, DisplayKey = "PatientManagerForm_KeepPrivate", Ordinal = 43, Visible = false)]
    [XmlAttribute]
    public bool KeepPrivate
    {
      get
      {
        bool flag = true;
        if (this.HasAttribute("KeepPrivate"))
          flag = this.GetAttributeAsBool("KeepPrivate");
        return flag;
      }
      set
      {
        this.SetAttribute("KeepPrivate", value);
      }
    }

    [ColumnInfo(DefaultWidth = 50, DisplayKey = "PatientManagerForm_KeepPrivate", Ordinal = 43, Visible = true)]
    [XmlAttribute]
    public string KeepPrivateString
    {
      get
      {
        return this.KeepPrivate.ToString();
      }
    }

    [ColumnInfo(Visible = false)]
    [XmlAttribute]
    [TypeConverter(typeof (OnlineGuidConverter))]
    public Guid LastDownloadId
    {
      get
      {
        return this.GetAttributeAsGuid("LastDownloadId");
      }
      set
      {
        this.SetAttribute("LastDownloadId", value);
      }
    }

    [ColumnInfo(Visible = false)]
    [XmlAttribute]
    public DateTimeOffset LastDownloadTimeStamp
    {
      get
      {
        return this.GetAttributeAsDateTimeOffset("LastDownloadTimeStamp");
      }
      set
      {
        this.SetAttribute("LastDownloadTimeStamp", value);
      }
    }

    [XmlAttribute]
    [ColumnInfo(Visible = false)]
    public string LastSerialNumber
    {
      get
      {
        return this.GetAttribute("LastSerialNumber");
      }
      set
      {
        this.SetAttribute("LastSerialNumber", value.Trim());
      }
    }

    [XmlAttribute]
    [ColumnInfo(Visible = false)]
    public Guid LastDeviceId
    {
      get
      {
        return this.GetAttribute<Guid>("LastDeviceId");
      }
      set
      {
        this.SetAttribute("LastDeviceId", value);
      }
    }

    public XPatientInformation PatientInformation
    {
      get
      {
        return new XPatientInformation(this.Element.SelectSingleNode("PatientInformation") as XmlElement);
      }
    }

    public XPatientSettings PatientSettings
    {
      get
      {
        return new XPatientSettings(this.Element.SelectSingleNode("PatientSettings") as XmlElement);
      }
    }

    public XCollection<XA1cData> A1cDataCollection
    {
      get
      {
        XmlElement xmlElement = this.Element.SelectSingleNode("A1cDataCollection") as XmlElement;
        if (DataTools.IsCompressedElement(xmlElement))
          xmlElement = (XmlElement) DataTools.DecompressElementWithHint(xmlElement);
        return new XCollection<XA1cData>(xmlElement);
      }
      set
      {
        XmlElement xmlElement1 = this.Element.SelectSingleNode("A1cDataCollection") as XmlElement;
        XmlElement xmlElement2;
        if (false)
        {
          xmlElement2 = DataTools.CreateCompressedXmlElement(value.Element, "A1cDataCollection", this.Element.OwnerDocument);
          xmlElement2.SetAttribute("ItemCount", value.Count.ToString());
        }
        else
        {
          xmlElement2 = this.Element.OwnerDocument.ImportNode((XmlNode) value.Element, true) as XmlElement;
          xmlElement2.SetAttribute("ItemCount", value.Count.ToString());
        }
        if (xmlElement1 == null)
          this.Element.AppendChild((XmlNode) xmlElement2);
        else
          this.Element.ReplaceChild((XmlNode) xmlElement2, (XmlNode) xmlElement1);
      }
    }

    public XCollection<XDeviceInformation> DeviceHistory
    {
      get
      {
        return new XCollection<XDeviceInformation>(this.Element.SelectSingleNode("DeviceHistory") as XmlElement);
      }
    }

    public XPatientAppData PatientAppData
    {
      get
      {
        return new XPatientAppData(this.Element.SelectSingleNode("PatientAppData") as XmlElement);
      }
    }

    public XPatientContext()
      : this(new XmlDocument())
    {
    }

    public XPatientContext(XmlDocument ownerDocument)
      : base("PatientContext", ownerDocument)
    {
      DateTimeOffset now = DateTimeOffset.Now;
      this.Id = Guid.NewGuid();
      this.CacheId = Guid.NewGuid();
      this.DateCreated = now;
      this.DateModified = now;
      this.IsActive = true;
      this.KeepPrivate = true;
      this.LastDownloadId = CommonValues.NoneId;
      this.LastDownloadTimeStamp = CommonValues.EmptyDateTimeOffset;
      this.LastSerialNumber = string.Empty;
      this.LastDeviceId = Guid.Empty;
      this.Element.AppendChild((XmlNode) new XPatientInformation(ownerDocument).Element);
      this.Element.AppendChild((XmlNode) new XPatientSettings(ownerDocument).Element);
      this.Element.AppendChild((XmlNode) new XCollection<XA1cData>("A1cDataCollection", ownerDocument).Element);
      this.Element.AppendChild((XmlNode) new XCollection<XDeviceInformation>("DeviceHistory", ownerDocument).Element);
      this.Element.AppendChild((XmlNode) DataTools.GetXApplicationInfo(ownerDocument).Element);
      this.Element.AppendChild((XmlNode) DataTools.GetXComputerInfo(ownerDocument).Element);
      if (string.Compare(Environment.UserDomainName, "DEXCOM", StringComparison.InvariantCultureIgnoreCase) != 0)
        DataTools.DeidentifyComputerInfo(this.Element);
      this.PatientInformation.Id = this.Id;
      this.PatientSettings.Id = this.Id;
    }

    public XPatientContext(XmlElement element)
      : base(element)
    {
    }

    protected XPatientContext(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    public void ApplyNewPatientId(Guid newId)
    {
      this.Id = newId;
      this.PatientInformation.Id = this.Id;
      this.PatientSettings.Id = this.Id;
    }

    [SecurityCritical]
    public new void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      base.GetObjectData(info, context);
    }
  }
}

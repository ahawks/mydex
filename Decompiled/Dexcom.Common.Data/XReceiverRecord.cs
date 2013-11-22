// Type: Dexcom.Common.Data.XReceiverRecord
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
  public class XReceiverRecord : XObject, ISerializable
  {
    public const string Tag = "ReceiverRecord";

    [TypeConverter(typeof (OnlineGuidConverter))]
    [XmlAttribute]
    public Guid DownloadId
    {
      get
      {
        return this.GetAttribute<Guid>("DownloadId");
      }
      set
      {
        this.SetAttribute("DownloadId", value);
      }
    }

    [XmlAttribute]
    [TypeConverter(typeof (OnlineGuidConverter))]
    public Guid MergedId
    {
      get
      {
        return this.GetAttribute<Guid>("MergedId");
      }
      set
      {
        this.SetAttribute("MergedId", value);
      }
    }

    [XmlAttribute]
    public bool IsSupportDownload
    {
      get
      {
        return this.GetAttribute<bool>("IsSupportDownload");
      }
      set
      {
        this.SetAttribute("IsSupportDownload", value);
      }
    }

    [XmlAttribute]
    public ReceiverDataTools.ReceiverFileType ReceiverFileType
    {
      get
      {
        return (ReceiverDataTools.ReceiverFileType) this.GetAttributeAsEnum("ReceiverFileType", typeof (ReceiverDataTools.ReceiverFileType));
      }
      set
      {
        this.SetAttribute("ReceiverFileType", ((object) value).ToString());
      }
    }

    public string ReceiverFileTypeString
    {
      get
      {
        return this.GetAttribute<string>("ReceiverFileType");
      }
      set
      {
        this.SetAttribute("ReceiverFileType", value.Trim());
      }
    }

    [XmlAttribute]
    [TypeConverter(typeof (OnlineGuidConverter))]
    public Guid StoredById
    {
      get
      {
        return this.GetAttribute<Guid>("StoredById");
      }
      set
      {
        this.SetAttribute("StoredById", value);
      }
    }

    [XmlAttribute]
    public string StoredByName
    {
      get
      {
        return this.GetAttribute<string>("StoredByName");
      }
      set
      {
        this.SetAttribute("StoredByName", value.Trim());
      }
    }

    [XmlAttribute]
    public string StoredByDisplayName
    {
      get
      {
        return this.GetAttribute<string>("StoredByDisplayName");
      }
      set
      {
        this.SetAttribute("StoredByDisplayName", value.Trim());
      }
    }

    [XmlAttribute]
    public DateTimeOffset DateTimeStored
    {
      get
      {
        return this.GetAttribute<DateTimeOffset>("DateTimeStored");
      }
      set
      {
        this.SetAttribute("DateTimeStored", value);
      }
    }

    [XmlAttribute]
    public DateTimeOffset DateTimeModified
    {
      get
      {
        return this.GetAttribute<DateTimeOffset>("DateTimeModified");
      }
      set
      {
        this.SetAttribute("DateTimeModified", value);
      }
    }

    [XmlAttribute]
    public DateTimeOffset DateTimeCreated
    {
      get
      {
        return this.GetAttribute<DateTimeOffset>("DateTimeCreated");
      }
      set
      {
        this.SetAttribute("DateTimeCreated", value);
      }
    }

    [TypeConverter(typeof (OnlineGuidConverter))]
    [XmlAttribute]
    public Guid ReceiverDatabaseId
    {
      get
      {
        return this.GetAttribute<Guid>("ReceiverDatabaseId");
      }
      set
      {
        this.SetAttribute("ReceiverDatabaseId", value);
      }
    }

    [XmlAttribute]
    public int PageCount
    {
      get
      {
        return this.GetAttribute<int>("PageCount");
      }
      set
      {
        this.SetAttribute("PageCount", value);
      }
    }

    [XmlAttribute]
    public int DownloadCount
    {
      get
      {
        return this.GetAttribute<int>("DownloadCount");
      }
      set
      {
        this.SetAttribute("DownloadCount", value);
      }
    }

    [XmlAttribute]
    public string SerialNumber
    {
      get
      {
        return this.GetAttribute<string>("SerialNumber");
      }
      set
      {
        this.SetAttribute("SerialNumber", value.Trim());
      }
    }

    [XmlAttribute]
    public string TransmitterId
    {
      get
      {
        return this.GetAttribute<string>("TransmitterId");
      }
      set
      {
        this.SetAttribute("TransmitterId", value.Trim());
      }
    }

    [XmlAttribute]
    public string DownloadType
    {
      get
      {
        return this.GetAttribute<string>("DownloadType");
      }
      set
      {
        this.SetAttribute("DownloadType", value.Trim());
      }
    }

    [XmlAttribute]
    public string PCProductName
    {
      get
      {
        return this.GetAttribute<string>("PCProductName");
      }
      set
      {
        this.SetAttribute("PCProductName", value.Trim());
      }
    }

    [XmlAttribute]
    public string PCProductVersion
    {
      get
      {
        return this.GetAttribute<string>("PCProductVersion");
      }
      set
      {
        this.SetAttribute("PCProductVersion", value.Trim());
      }
    }

    [XmlAttribute]
    public string ApiVersion
    {
      get
      {
        return this.GetAttribute<string>("ApiVersion");
      }
      set
      {
        this.SetAttribute("ApiVersion", value.Trim());
      }
    }

    [XmlAttribute]
    public string ProductId
    {
      get
      {
        return this.GetAttribute<string>("ProductId");
      }
      set
      {
        this.SetAttribute("ProductId", value.Trim());
      }
    }

    [XmlAttribute]
    public string ProductName
    {
      get
      {
        return this.GetAttribute<string>("ProductName");
      }
      set
      {
        this.SetAttribute("ProductName", value.Trim());
      }
    }

    [XmlAttribute]
    public string SoftwareNumber
    {
      get
      {
        return this.GetAttribute<string>("SoftwareNumber");
      }
      set
      {
        this.SetAttribute("SoftwareNumber", value.Trim());
      }
    }

    [XmlAttribute]
    public string FirmwareVersion
    {
      get
      {
        return this.GetAttribute<string>("FirmwareVersion");
      }
      set
      {
        this.SetAttribute("FirmwareVersion", value.Trim());
      }
    }

    [XmlAttribute]
    public string PortVersion
    {
      get
      {
        return this.GetAttribute<string>("PortVersion");
      }
      set
      {
        this.SetAttribute("PortVersion", value.Trim());
      }
    }

    [XmlAttribute]
    public string RFVersion
    {
      get
      {
        return this.GetAttribute<string>("RFVersion");
      }
      set
      {
        this.SetAttribute("RFVersion", value.Trim());
      }
    }

    [XmlAttribute]
    public string DexBootVersion
    {
      get
      {
        return this.GetAttribute<string>("DexBootVersion");
      }
      set
      {
        this.SetAttribute("DexBootVersion", value.Trim());
      }
    }

    [XmlAttribute]
    public int ViewCount
    {
      get
      {
        return this.GetAttribute<int>("ViewCount");
      }
      set
      {
        this.SetAttribute("ViewCount", value);
      }
    }

    [XmlAttribute]
    public string ServiceRequestNumber
    {
      get
      {
        string str = string.Empty;
        if (this.HasAttribute("ServiceRequestNumber"))
          str = this.GetAttribute<string>("ServiceRequestNumber");
        return str;
      }
      set
      {
        this.SetAttribute("ServiceRequestNumber", value.Trim());
      }
    }

    [XmlAttribute]
    public string ReturnGoodsAuthorization
    {
      get
      {
        string str = string.Empty;
        if (this.HasAttribute("ReturnGoodsAuthorization"))
          str = this.GetAttribute<string>("ReturnGoodsAuthorization");
        return str;
      }
      set
      {
        this.SetAttribute("ReturnGoodsAuthorization", value.Trim());
      }
    }

    [XmlAttribute]
    public string LotNumber
    {
      get
      {
        string str = string.Empty;
        if (this.HasAttribute("LotNumber"))
          str = this.GetAttribute<string>("LotNumber");
        return str;
      }
      set
      {
        this.SetAttribute("LotNumber", value.Trim());
      }
    }

    public XReceiverRecord()
      : this(new XmlDocument())
    {
    }

    public XReceiverRecord(XmlDocument ownerDocument)
      : base("ReceiverRecord", ownerDocument)
    {
      this.Id = Guid.Empty;
      this.DownloadId = Guid.Empty;
      this.MergedId = Guid.Empty;
      this.ReceiverFileType = ReceiverDataTools.ReceiverFileType.Unknown;
      this.IsSupportDownload = false;
      this.Name = string.Empty;
      this.Description = string.Empty;
      this.ServiceRequestNumber = string.Empty;
      this.ReturnGoodsAuthorization = string.Empty;
      this.LotNumber = string.Empty;
      this.DateTimeStored = CommonValues.EmptyDateTimeOffset;
      this.DateTimeModified = CommonValues.EmptyDateTimeOffset;
      this.StoredById = Guid.Empty;
      this.StoredByName = string.Empty;
      this.StoredByDisplayName = string.Empty;
      this.DateTimeCreated = CommonValues.EmptyDateTimeOffset;
      this.ReceiverDatabaseId = Guid.Empty;
      this.PageCount = 0;
      this.DownloadCount = 0;
      this.SerialNumber = string.Empty;
      this.TransmitterId = string.Empty;
      this.DownloadType = string.Empty;
      this.PCProductName = string.Empty;
      this.PCProductVersion = string.Empty;
      this.ApiVersion = string.Empty;
      this.ProductId = string.Empty;
      this.ProductName = string.Empty;
      this.SoftwareNumber = string.Empty;
      this.FirmwareVersion = string.Empty;
      this.PortVersion = string.Empty;
      this.RFVersion = string.Empty;
      this.DexBootVersion = string.Empty;
      this.ViewCount = 0;
    }

    public XReceiverRecord(XmlElement element)
      : base(element)
    {
    }

    protected XReceiverRecord(SerializationInfo info, StreamingContext context)
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

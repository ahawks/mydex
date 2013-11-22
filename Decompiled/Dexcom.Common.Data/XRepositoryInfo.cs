// Type: Dexcom.Common.Data.XRepositoryInfo
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
  public class XRepositoryInfo : XObject, ISerializable
  {
    public const string Tag = "RepositoryInfo";

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

    [TypeConverter(typeof (OnlineGuidConverter))]
    [XmlAttribute]
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

    public XRepositoryInfo()
      : this(new XmlDocument())
    {
    }

    public XRepositoryInfo(XmlDocument ownerDocument)
      : base("RepositoryInfo", ownerDocument)
    {
      this.Id = Guid.Empty;
      this.Name = string.Empty;
      this.Description = string.Empty;
      this.ServiceRequestNumber = string.Empty;
      this.ReturnGoodsAuthorization = string.Empty;
      this.LotNumber = string.Empty;
      this.IsSupportDownload = false;
      this.StoredById = Guid.Empty;
      this.StoredByName = string.Empty;
      this.StoredByDisplayName = string.Empty;
      this.DateTimeStored = CommonValues.EmptyDateTimeOffset;
      this.DateTimeModified = CommonValues.EmptyDateTimeOffset;
    }

    public XRepositoryInfo(XmlElement element)
      : base(element)
    {
    }

    protected XRepositoryInfo(SerializationInfo info, StreamingContext context)
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

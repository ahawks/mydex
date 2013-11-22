// Type: Dexcom.Common.Data.XDeviceSupportAuthorization
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
  public class XDeviceSupportAuthorization : XObject, ISerializable
  {
    public const string Tag = "DeviceSupportAuthorization";
    public const string HistoryTag = "History";

    [XmlAttribute]
    public string DeviceNumber
    {
      get
      {
        return this.GetAttribute<string>("DeviceNumber");
      }
      set
      {
        this.SetAttribute("DeviceNumber", value.Trim());
      }
    }

    [XmlAttribute]
    public bool IsAuthorized
    {
      get
      {
        return this.GetAttribute<bool>("IsAuthorized");
      }
      set
      {
        this.SetAttribute("IsAuthorized", value);
      }
    }

    [XmlAttribute]
    public DateTime DateExpired
    {
      get
      {
        return this.GetAttribute<DateTime>("DateExpired");
      }
      set
      {
        this.SetAttribute("DateExpired", value);
      }
    }

    [XmlAttribute]
    [TypeConverter(typeof (OnlineGuidConverter))]
    public Guid CreatedById
    {
      get
      {
        return this.GetAttribute<Guid>("CreatedById");
      }
      set
      {
        this.SetAttribute("CreatedById", value);
      }
    }

    [XmlAttribute]
    public string CreatedByName
    {
      get
      {
        return this.GetAttribute<string>("CreatedByName");
      }
      set
      {
        this.SetAttribute("CreatedByName", value.Trim());
      }
    }

    [XmlAttribute]
    public string CreatedByDisplayName
    {
      get
      {
        return this.GetAttribute<string>("CreatedByDisplayName");
      }
      set
      {
        this.SetAttribute("CreatedByDisplayName", value.Trim());
      }
    }

    [XmlAttribute]
    public DateTime DateCreated
    {
      get
      {
        return this.GetAttribute<DateTime>("DateCreated");
      }
      set
      {
        this.SetAttribute("DateCreated", value);
      }
    }

    [XmlAttribute]
    [TypeConverter(typeof (OnlineGuidConverter))]
    public Guid ModifiedById
    {
      get
      {
        return this.GetAttribute<Guid>("ModifiedById");
      }
      set
      {
        this.SetAttribute("ModifiedById", value);
      }
    }

    [XmlAttribute]
    public string ModifiedByName
    {
      get
      {
        return this.GetAttribute<string>("ModifiedByName");
      }
      set
      {
        this.SetAttribute("ModifiedByName", value.Trim());
      }
    }

    [XmlAttribute]
    public string ModifiedByDisplayName
    {
      get
      {
        return this.GetAttribute<string>("ModifiedByDisplayName");
      }
      set
      {
        this.SetAttribute("ModifiedByDisplayName", value.Trim());
      }
    }

    [XmlAttribute]
    public DateTime DateModified
    {
      get
      {
        return this.GetAttribute<DateTime>("DateModified");
      }
      set
      {
        this.SetAttribute("DateModified", value);
      }
    }

    public XCollection<XDeviceSupportAuthorization> History
    {
      get
      {
        return new XCollection<XDeviceSupportAuthorization>(this.Element.SelectSingleNode("History") as XmlElement);
      }
    }

    public XDeviceSupportAuthorization()
      : this(new XmlDocument())
    {
    }

    public XDeviceSupportAuthorization(XmlDocument ownerDocument)
      : base("DeviceSupportAuthorization", ownerDocument)
    {
      this.Id = Guid.Empty;
      this.Description = string.Empty;
      this.DeviceNumber = string.Empty;
      this.IsAuthorized = false;
      this.DateExpired = CommonValues.EmptyDateTime;
      this.CreatedById = Guid.Empty;
      this.CreatedByName = string.Empty;
      this.CreatedByDisplayName = string.Empty;
      this.DateCreated = CommonValues.EmptyDateTime;
      this.ModifiedById = Guid.Empty;
      this.ModifiedByName = string.Empty;
      this.ModifiedByDisplayName = string.Empty;
      this.DateModified = CommonValues.EmptyDateTime;
      this.Element.AppendChild((XmlNode) new XCollection<XDeviceSupportAuthorization>("History", ownerDocument).Element);
    }

    public XDeviceSupportAuthorization(XmlElement element)
      : base(element)
    {
    }

    protected XDeviceSupportAuthorization(SerializationInfo info, StreamingContext context)
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

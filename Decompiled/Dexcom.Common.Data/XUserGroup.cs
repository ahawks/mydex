// Type: Dexcom.Common.Data.XUserGroup
// Assembly: Dexcom.Common.Data, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 64B9BFC3-C891-4315-B216-574914861A80
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.Data.dll

using Dexcom.Common;
using System;
using System.Runtime.Serialization;
using System.Security;
using System.Xml;
using System.Xml.Serialization;

namespace Dexcom.Common.Data
{
  [Serializable]
  public class XUserGroup : XObject, ISerializable
  {
    public const string Tag = "UserGroup";

    [XmlAttribute]
    public string DisplayName
    {
      get
      {
        return this.GetAttribute("DisplayName");
      }
      set
      {
        this.SetAttribute("DisplayName", value.Trim());
      }
    }

    [XmlAttribute]
    public DateTime DateCreated
    {
      get
      {
        return this.GetAttributeAsDateTime("DateCreated");
      }
      set
      {
        this.SetAttribute("DateCreated", value);
      }
    }

    [XmlAttribute]
    public DateTime DateModified
    {
      get
      {
        return this.GetAttributeAsDateTime("DateModified");
      }
      set
      {
        this.SetAttribute("DateModified", value);
      }
    }

    public XUserGroup()
      : this(new XmlDocument())
    {
    }

    public XUserGroup(XmlDocument ownerDocument)
      : base("UserGroup", ownerDocument)
    {
      this.Id = Guid.NewGuid();
      this.Name = "";
      this.Description = "";
      this.DisplayName = "";
      this.DateCreated = CommonValues.EmptyDateTime;
      this.DateModified = CommonValues.EmptyDateTime;
    }

    public XUserGroup(XmlElement element)
      : base(element)
    {
    }

    protected XUserGroup(SerializationInfo info, StreamingContext context)
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

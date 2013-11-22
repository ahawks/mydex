// Type: Dexcom.Common.Data.XGroupMember
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
  public class XGroupMember : XObject, ISerializable
  {
    public const string Tag = "GroupMember";

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
    public XGroupMember.MemberType TypeOfMember
    {
      get
      {
        return (XGroupMember.MemberType) this.GetAttributeAsEnum("Type", typeof (XGroupMember.MemberType));
      }
      set
      {
        this.SetAttribute("Type", ((object) value).ToString());
      }
    }

    public string TypeOfMemberAsString
    {
      get
      {
        return this.GetAttributeAsEnum("Type", typeof (XGroupMember.MemberType)).ToString();
      }
      set
      {
        this.TypeOfMember = (XGroupMember.MemberType) Enum.Parse(typeof (XGroupMember.MemberType), value);
      }
    }

    public XGroupMember()
      : this(new XmlDocument())
    {
    }

    public XGroupMember(XmlDocument ownerDocument)
      : base("GroupMember", ownerDocument)
    {
      this.Id = Guid.NewGuid();
      this.Name = "";
      this.DisplayName = "";
      this.TypeOfMember = XGroupMember.MemberType.Unknown;
    }

    public XGroupMember(XmlElement element)
      : base(element)
    {
    }

    protected XGroupMember(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    [SecurityCritical]
    public new void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      base.GetObjectData(info, context);
    }

    public enum MemberType
    {
      Unknown,
      User,
      UserGroup,
    }
  }
}

// Type: Dexcom.Common.Data.XUser
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
  public class XUser : XObject, ISerializable, INotifyPropertyChanged
  {
    public const string Tag = "User";

    [ColumnInfo]
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
        EventUtils.FireEvent((Delegate) this.PropertyChanged, (object) this, (object) new PropertyChangedEventArgs("Name"));
      }
    }

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
        EventUtils.FireEvent((Delegate) this.PropertyChanged, (object) this, (object) new PropertyChangedEventArgs("DisplayName"));
      }
    }

    [XmlAttribute]
    public string FirstName
    {
      get
      {
        return this.GetAttribute("FirstName");
      }
      set
      {
        this.SetAttribute("FirstName", value.Trim());
        EventUtils.FireEvent((Delegate) this.PropertyChanged, (object) this, (object) new PropertyChangedEventArgs("FirstName"));
      }
    }

    [XmlAttribute]
    public string LastName
    {
      get
      {
        return this.GetAttribute("LastName");
      }
      set
      {
        this.SetAttribute("LastName", value.Trim());
        EventUtils.FireEvent((Delegate) this.PropertyChanged, (object) this, (object) new PropertyChangedEventArgs("LastName"));
      }
    }

    [XmlAttribute]
    public string EMail
    {
      get
      {
        return this.GetAttribute("EMail");
      }
      set
      {
        this.SetAttribute("EMail", value.Trim());
        EventUtils.FireEvent((Delegate) this.PropertyChanged, (object) this, (object) new PropertyChangedEventArgs("EMail"));
      }
    }

    [XmlAttribute]
    public string PhoneNumber
    {
      get
      {
        return this.GetAttribute("PhoneNumber");
      }
      set
      {
        this.SetAttribute("PhoneNumber", value.Trim());
      }
    }

    [XmlAttribute]
    public string PhoneExtension
    {
      get
      {
        return this.GetAttribute("PhoneExtension");
      }
      set
      {
        this.SetAttribute("PhoneExtension", value.Trim());
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
    public bool IsDisabled
    {
      get
      {
        return this.GetAttributeAsBool("IsDisabled");
      }
      set
      {
        this.SetAttribute("IsDisabled", value);
      }
    }

    [XmlAttribute]
    public bool IsRemoved
    {
      get
      {
        return this.GetAttributeAsBool("IsRemoved");
      }
      set
      {
        this.SetAttribute("IsRemoved", value);
      }
    }

    [XmlAttribute]
    public Guid PasswordKey
    {
      get
      {
        return this.GetAttributeAsGuid("PasswordKey");
      }
      set
      {
        this.SetAttribute("PasswordKey", value);
      }
    }

    [XmlAttribute]
    public DateTime DatePasswordChanged
    {
      get
      {
        return this.GetAttributeAsDateTime("DatePasswordChanged");
      }
      set
      {
        this.SetAttribute("DatePasswordChanged", value);
      }
    }

    [XmlAttribute]
    public bool IsDomainAccount
    {
      get
      {
        if (!this.HasAttribute("IsDomainAccount"))
          return false;
        else
          return this.GetAttribute<bool>("IsDomainAccount");
      }
      set
      {
        this.SetAttribute("IsDomainAccount", value);
        EventUtils.FireEvent((Delegate) this.PropertyChanged, (object) this, (object) new PropertyChangedEventArgs("IsDomainAccount"));
        EventUtils.FireEvent((Delegate) this.PropertyChanged, (object) this, (object) new PropertyChangedEventArgs("IsNotDomainAccount"));
      }
    }

    [XmlAttribute]
    public bool IsNotDomainAccount
    {
      get
      {
        if (!this.HasAttribute("IsDomainAccount"))
          return true;
        else
          return !this.GetAttribute<bool>("IsDomainAccount");
      }
      set
      {
        this.SetAttribute("IsDomainAccount", !value);
        EventUtils.FireEvent((Delegate) this.PropertyChanged, (object) this, (object) new PropertyChangedEventArgs("IsNotDomainAccount"));
        EventUtils.FireEvent((Delegate) this.PropertyChanged, (object) this, (object) new PropertyChangedEventArgs("IsDomainAccount"));
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public XUser()
      : this(new XmlDocument())
    {
    }

    public XUser(XmlDocument ownerDocument)
      : base("User", ownerDocument)
    {
      this.Id = Guid.NewGuid();
      this.Name = "";
      this.DisplayName = "";
      this.FirstName = "";
      this.LastName = "";
      this.EMail = "";
      this.PhoneNumber = "";
      this.PhoneExtension = "";
      this.DateCreated = CommonValues.EmptyDateTime;
      this.DateModified = CommonValues.EmptyDateTime;
      this.IsActive = true;
      this.IsDisabled = false;
      this.IsRemoved = false;
      this.IsDomainAccount = false;
      this.PasswordKey = Guid.Empty;
      this.DatePasswordChanged = CommonValues.EmptyDateTime;
    }

    public XUser(XmlElement element)
      : base(element)
    {
    }

    protected XUser(SerializationInfo info, StreamingContext context)
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

// Type: Dexcom.Common.Data.XApplicationInfo
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
  public class XApplicationInfo : XObject, ISerializable
  {
    public const string Tag = "ApplicationInfo";

    [XmlAttribute]
    public string ExecutablePath
    {
      get
      {
        return this.GetAttribute("ExecutablePath");
      }
      set
      {
        this.SetAttribute("ExecutablePath", value.Trim());
      }
    }

    [XmlAttribute]
    public string StartupPath
    {
      get
      {
        return this.GetAttribute("StartupPath");
      }
      set
      {
        this.SetAttribute("StartupPath", value.Trim());
      }
    }

    [XmlAttribute]
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

    [XmlAttribute]
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

    [XmlAttribute]
    public string AssemblyVersion
    {
      get
      {
        return this.GetAttribute("AssemblyVersion");
      }
      set
      {
        this.SetAttribute("AssemblyVersion", value.Trim());
      }
    }

    [XmlAttribute]
    public string AssemblyFileVersion
    {
      get
      {
        return this.GetAttribute("AssemblyFileVersion");
      }
      set
      {
        this.SetAttribute("AssemblyFileVersion", value.Trim());
      }
    }

    public XApplicationInfo()
      : this(new XmlDocument())
    {
    }

    public XApplicationInfo(XmlDocument ownerDocument)
      : base("ApplicationInfo", ownerDocument)
    {
      this.ExecutablePath = string.Empty;
      this.StartupPath = string.Empty;
      this.ProductName = string.Empty;
      this.ProductVersion = string.Empty;
      this.AssemblyVersion = string.Empty;
      this.AssemblyFileVersion = string.Empty;
    }

    public XApplicationInfo(XmlElement element)
      : base(element)
    {
    }

    protected XApplicationInfo(SerializationInfo info, StreamingContext context)
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

// Type: Dexcom.Common.Data.XComputerInfo
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
  public class XComputerInfo : XObject, ISerializable
  {
    public const string Tag = "ComputerInfo";

    [XmlAttribute]
    public string HostName
    {
      get
      {
        return this.GetAttribute("HostName");
      }
      set
      {
        this.SetAttribute("HostName", value.Trim());
      }
    }

    [XmlAttribute]
    public string HostIp
    {
      get
      {
        return this.GetAttribute("HostIp");
      }
      set
      {
        this.SetAttribute("HostIp", value.Trim());
      }
    }

    [XmlAttribute]
    public string MACAddress
    {
      get
      {
        return this.GetAttribute("MACAddress");
      }
      set
      {
        this.SetAttribute("MACAddress", value.Trim());
      }
    }

    [XmlAttribute]
    public string HardwareId
    {
      get
      {
        return this.GetAttribute("HardwareId");
      }
      set
      {
        this.SetAttribute("HardwareId", value.Trim());
      }
    }

    [XmlAttribute]
    public string DriveId
    {
      get
      {
        return this.GetAttribute("DriveId");
      }
      set
      {
        this.SetAttribute("DriveId", value.Trim());
      }
    }

    [XmlAttribute]
    public string MachineName
    {
      get
      {
        return this.GetAttribute("MachineName");
      }
      set
      {
        this.SetAttribute("MachineName", value.Trim());
      }
    }

    [XmlAttribute]
    public string OSDirectory
    {
      get
      {
        return this.GetAttribute("OSDirectory");
      }
      set
      {
        this.SetAttribute("OSDirectory", value.Trim());
      }
    }

    [XmlAttribute]
    public string OSArchitecture
    {
      get
      {
        return this.GetAttribute("OSArchitecture");
      }
      set
      {
        this.SetAttribute("OSArchitecture", value.Trim());
      }
    }

    [XmlAttribute]
    public string VolumeId
    {
      get
      {
        return this.GetAttribute("VolumeId");
      }
      set
      {
        this.SetAttribute("VolumeId", value.Trim());
      }
    }

    [XmlAttribute]
    public string OSVersion
    {
      get
      {
        return this.GetAttribute("OSVersion");
      }
      set
      {
        this.SetAttribute("OSVersion", value.Trim());
      }
    }

    [XmlAttribute]
    public string ClrVersion
    {
      get
      {
        return this.GetAttribute("ClrVersion");
      }
      set
      {
        this.SetAttribute("ClrVersion", value.Trim());
      }
    }

    [XmlAttribute]
    public string UserDomainName
    {
      get
      {
        return this.GetAttribute("UserDomainName");
      }
      set
      {
        this.SetAttribute("UserDomainName", value.Trim());
      }
    }

    [XmlAttribute]
    public string UserName
    {
      get
      {
        return this.GetAttribute("UserName");
      }
      set
      {
        this.SetAttribute("UserName", value.Trim());
      }
    }

    public XComputerInfo()
      : this(new XmlDocument())
    {
    }

    public XComputerInfo(XmlDocument ownerDocument)
      : base("ComputerInfo", ownerDocument)
    {
      this.HostName = string.Empty;
      this.HostIp = string.Empty;
      this.MACAddress = string.Empty;
      this.MachineName = string.Empty;
      this.OSVersion = string.Empty;
      this.UserDomainName = string.Empty;
      this.UserName = string.Empty;
      this.ClrVersion = string.Empty;
      this.DriveId = string.Empty;
      this.VolumeId = string.Empty;
      this.HardwareId = string.Empty;
      this.OSDirectory = string.Empty;
      this.OSArchitecture = string.Empty;
    }

    public XComputerInfo(XmlElement otherElement)
      : base(otherElement)
    {
    }

    protected XComputerInfo(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    public void Deidentify()
    {
      this.HostName = string.Empty;
      this.HostIp = string.Empty;
      this.MACAddress = string.Empty;
      this.MachineName = string.Empty;
      this.UserDomainName = string.Empty;
      this.UserName = string.Empty;
      this.DriveId = string.Empty;
      this.VolumeId = string.Empty;
      this.HardwareId = string.Empty;
    }

    [SecurityCritical]
    public new void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      base.GetObjectData(info, context);
    }
  }
}

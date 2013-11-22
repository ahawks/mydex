// Type: Dexcom.ReceiverApi.XFirmwareHeader
// Assembly: Dexcom.ReceiverCommercialApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 3F623F65-908D-40A7-87D3-892C5D1FFA2E
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.ReceiverCommercialApi.dll

using Dexcom.Common;
using System;
using System.Runtime.Serialization;
using System.Security;
using System.Xml;
using System.Xml.Serialization;

namespace Dexcom.ReceiverApi
{
  [Serializable]
  public class XFirmwareHeader : XObject, ISerializable
  {
    public const string Tag = "FirmwareHeader";

    [XmlAttribute]
    public int SchemaVersion
    {
      get
      {
        return this.GetAttribute<int>("SchemaVersion");
      }
      set
      {
        this.SetAttribute("SchemaVersion", value);
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
    public string TestApiVersion
    {
      get
      {
        if (this.HasAttribute("TestApiVersion"))
          return this.GetAttribute<string>("TestApiVersion");
        else
          return "1.0.0.0";
      }
      set
      {
        this.SetAttribute("TestApiVersion", value.Trim());
      }
    }

    [XmlAttribute]
    public uint ApiVersionNumber
    {
      get
      {
        return CommonTools.StringToRevision(this.ApiVersion);
      }
    }

    [XmlAttribute]
    public uint TestApiVersionNumber
    {
      get
      {
        return CommonTools.StringToRevision(this.TestApiVersion);
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
    public uint FirmwareVersionNumber
    {
      get
      {
        return CommonTools.StringToRevision(this.FirmwareVersion);
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
    public uint PortVersionNumber
    {
      get
      {
        return CommonTools.StringToRevision(this.PortVersion);
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
    public uint RFVersionNumber
    {
      get
      {
        return CommonTools.StringToRevision(this.RFVersion);
      }
    }

    [XmlAttribute]
    public string DexBootVersion
    {
      get
      {
        if (this.HasAttribute("DexBootVersion"))
          return this.GetAttribute<string>("DexBootVersion");
        else
          return "0";
      }
      set
      {
        this.SetAttribute("DexBootVersion", value.Trim());
      }
    }

    [XmlAttribute]
    public uint DexBootVersionNumber
    {
      get
      {
        return Convert.ToUInt32(this.DexBootVersion);
      }
    }

    public XFirmwareHeader()
      : this(new XmlDocument())
    {
    }

    public XFirmwareHeader(XmlDocument ownerDocument)
      : base("FirmwareHeader", ownerDocument)
    {
      this.SchemaVersion = 0;
      this.ApiVersion = string.Empty;
      this.TestApiVersion = string.Empty;
      this.ProductId = string.Empty;
      this.ProductName = string.Empty;
      this.SoftwareNumber = string.Empty;
      this.FirmwareVersion = string.Empty;
      this.PortVersion = string.Empty;
      this.RFVersion = string.Empty;
      this.DexBootVersion = string.Empty;
    }

    public XFirmwareHeader(XmlElement element)
      : base(element)
    {
    }

    protected XFirmwareHeader(SerializationInfo info, StreamingContext context)
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

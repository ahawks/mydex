// Type: Dexcom.Common.Data.XUpdateQueryInfo
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
  public class XUpdateQueryInfo : XObject, ISerializable
  {
    public const string Tag = "UpdateQueryInfo";

    [XmlAttribute]
    public int Version
    {
      get
      {
        return this.GetAttribute<int>("Version");
      }
      set
      {
        this.SetAttribute("Version", value);
      }
    }

    [XmlAttribute]
    public DateTime DateTimeLocal
    {
      get
      {
        return this.GetAttribute<DateTime>("DateTimeLocal");
      }
      set
      {
        this.SetAttribute("DateTimeLocal", value);
      }
    }

    [XmlAttribute]
    public DateTime DateTimeUTC
    {
      get
      {
        return this.GetAttribute<DateTime>("DateTimeUTC");
      }
      set
      {
        this.SetAttribute("DateTimeUTC", value);
      }
    }

    [XmlAttribute]
    public uint ReceiverNumber
    {
      get
      {
        return this.GetAttribute<uint>("ReceiverNumber");
      }
      set
      {
        this.SetAttribute("ReceiverNumber", value);
      }
    }

    [XmlAttribute]
    public Guid ReceiverInstanceId
    {
      get
      {
        return this.GetAttribute<Guid>("ReceiverInstanceId");
      }
      set
      {
        this.SetAttribute("ReceiverInstanceId", value);
      }
    }

    [XmlAttribute]
    public Guid ImageInstanceId
    {
      get
      {
        return this.GetAttribute<Guid>("ImageInstanceId");
      }
      set
      {
        this.SetAttribute("ImageInstanceId", value);
      }
    }

    [XmlAttribute]
    public string ImageExtension
    {
      get
      {
        return this.GetAttribute<string>("ImageExtension");
      }
      set
      {
        this.SetAttribute("ImageExtension", value);
      }
    }

    [XmlAttribute]
    public string ImageSubExtension
    {
      get
      {
        return this.GetAttribute<string>("ImageSubExtension");
      }
      set
      {
        this.SetAttribute("ImageSubExtension", value.Trim());
      }
    }

    [XmlAttribute]
    public uint HardwareProductNumber
    {
      get
      {
        return this.GetAttribute<uint>("HardwareProductNumber");
      }
      set
      {
        this.SetAttribute("HardwareProductNumber", value);
      }
    }

    [XmlAttribute]
    public byte HardwareProductRevision
    {
      get
      {
        return this.GetAttribute<byte>("HardwareProductRevision");
      }
      set
      {
        this.SetAttribute("HardwareProductRevision", value);
      }
    }

    [XmlAttribute]
    public string FirmwareFlags
    {
      get
      {
        return this.GetAttribute<string>("FirmwareFlags");
      }
      set
      {
        this.SetAttribute("FirmwareFlags", value);
      }
    }

    [XmlAttribute]
    public string HardwareFlags
    {
      get
      {
        return this.GetAttribute<string>("HardwareFlags");
      }
      set
      {
        this.SetAttribute("HardwareFlags", value);
      }
    }

    [XmlAttribute]
    public string BiosRevision
    {
      get
      {
        return this.GetAttribute<string>("BiosRevision");
      }
      set
      {
        this.SetAttribute("BiosRevision", value);
      }
    }

    [XmlAttribute]
    public string BiosProductString
    {
      get
      {
        return this.GetAttribute<string>("BiosProductString");
      }
      set
      {
        this.SetAttribute("BiosProductString", value);
      }
    }

    [XmlAttribute]
    public string FirmwareRevision
    {
      get
      {
        return this.GetAttribute<string>("FirmwareRevision");
      }
      set
      {
        this.SetAttribute("FirmwareRevision", value);
      }
    }

    [XmlAttribute]
    public string FirmwareProductString
    {
      get
      {
        return this.GetAttribute<string>("FirmwareProductString");
      }
      set
      {
        this.SetAttribute("FirmwareProductString", value);
      }
    }

    [XmlAttribute]
    public string FirmwareBuildDate
    {
      get
      {
        return this.GetAttribute<string>("FirmwareBuildDate");
      }
      set
      {
        this.SetAttribute("FirmwareBuildDate", value);
      }
    }

    [XmlAttribute]
    public string DatabaseRevision
    {
      get
      {
        return this.GetAttribute<string>("DatabaseRevision");
      }
      set
      {
        this.SetAttribute("DatabaseRevision", value);
      }
    }

    public XUpdateQueryInfo()
      : this(new XmlDocument())
    {
    }

    public XUpdateQueryInfo(XmlDocument ownerDocument)
      : base("UpdateQueryInfo", ownerDocument)
    {
      this.Id = Guid.NewGuid();
      this.Version = 1;
      this.DateTimeLocal = CommonValues.EmptyDateTime;
      this.DateTimeUTC = CommonValues.EmptyDateTime;
      this.ReceiverNumber = 0U;
      this.ReceiverInstanceId = Guid.Empty;
      this.ImageInstanceId = CommonValues.R2EmptyId;
      this.ImageExtension = string.Empty;
      this.ImageSubExtension = string.Empty;
      this.HardwareProductNumber = 0U;
      this.HardwareProductRevision = (byte) 0;
      this.FirmwareFlags = string.Empty;
      this.HardwareFlags = string.Empty;
      this.BiosRevision = string.Empty;
      this.BiosProductString = string.Empty;
      this.FirmwareRevision = string.Empty;
      this.FirmwareProductString = string.Empty;
      this.FirmwareBuildDate = string.Empty;
      this.DatabaseRevision = string.Empty;
    }

    public XUpdateQueryInfo(XmlElement element)
      : base(element)
    {
    }

    protected XUpdateQueryInfo(SerializationInfo info, StreamingContext context)
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

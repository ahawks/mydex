// Type: Dexcom.ReceiverApi.XFirmwareSettings
// Assembly: Dexcom.ReceiverCommercialApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 3F623F65-908D-40A7-87D3-892C5D1FFA2E
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.ReceiverCommercialApi.dll

using Dexcom.Common;
using System;
using System.Runtime.Serialization;
using System.Security;
using System.Xml;

namespace Dexcom.ReceiverApi
{
  [Serializable]
  public class XFirmwareSettings : XObject, ISerializable
  {
    public const string Tag = "FirmwareSettings";

    public Guid FirmwareImageId
    {
      get
      {
        return this.GetAttribute<Guid>("FirmwareImageId");
      }
      set
      {
        this.SetAttribute("FirmwareImageId", value);
      }
    }

    public XFirmwareSettings()
      : this(new XmlDocument())
    {
    }

    public XFirmwareSettings(XmlDocument ownerDocument)
      : base("FirmwareSettings", ownerDocument)
    {
      this.FirmwareImageId = Guid.Empty;
    }

    public XFirmwareSettings(XmlElement element)
      : base(element)
    {
    }

    protected XFirmwareSettings(SerializationInfo info, StreamingContext context)
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

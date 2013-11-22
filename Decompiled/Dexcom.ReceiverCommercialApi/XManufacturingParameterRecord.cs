// Type: Dexcom.ReceiverApi.XManufacturingParameterRecord
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
  public class XManufacturingParameterRecord : XObject, ISerializable
  {
    public const string Tag = "ManufacturingParameters";
    public const string SerialNumberTag = "SerialNumber";
    public const string HardwarePartNumberTag = "HardwarePartNumber";
    public const string HardwareRevisionTag = "HardwareRevision";

    public string SerialNumber
    {
      get
      {
        return this.GetAttribute("SerialNumber");
      }
      set
      {
        this.SetAttribute("SerialNumber", value.Trim());
      }
    }

    public string HardwarePartNumber
    {
      get
      {
        return this.GetAttribute("HardwarePartNumber");
      }
      set
      {
        this.SetAttribute("HardwarePartNumber", value.Trim());
      }
    }

    public string HardwareRevision
    {
      get
      {
        return this.GetAttribute("HardwareRevision");
      }
      set
      {
        this.SetAttribute("HardwareRevision", value.Trim());
      }
    }

    public DateTimeOffset DateTimeCreated
    {
      get
      {
        return this.GetAttribute<DateTimeOffset>("DateTimeCreated");
      }
      set
      {
        this.SetAttribute("DateTimeCreated", value);
      }
    }

    public Guid HardwareId
    {
      get
      {
        return this.GetAttribute<Guid>("HardwareId");
      }
      set
      {
        this.SetAttribute("HardwareId", value);
      }
    }

    public XManufacturingParameterRecord()
      : this(new XmlDocument())
    {
    }

    public XManufacturingParameterRecord(XmlDocument ownerDocument)
      : base("ManufacturingParameters", ownerDocument)
    {
      this.SerialNumber = string.Empty;
      this.HardwarePartNumber = string.Empty;
      this.HardwareRevision = string.Empty;
      this.DateTimeCreated = CommonValues.EmptyDateTimeOffset;
      this.HardwareId = Guid.Empty;
    }

    public XManufacturingParameterRecord(XmlElement element)
      : base(element)
    {
    }

    protected XManufacturingParameterRecord(SerializationInfo info, StreamingContext context)
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

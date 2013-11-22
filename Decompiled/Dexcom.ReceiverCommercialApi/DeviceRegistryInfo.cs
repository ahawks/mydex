// Type: Dexcom.ReceiverApi.DeviceRegistryInfo
// Assembly: Dexcom.ReceiverCommercialApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 3F623F65-908D-40A7-87D3-892C5D1FFA2E
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.ReceiverCommercialApi.dll

using Dexcom.Common;
using System;
using System.Xml;

namespace Dexcom.ReceiverApi
{
  public class DeviceRegistryInfo : IComparable
  {
    public string UsbDeviceClassKey { get; set; }

    public string UsbDeviceInstance { get; set; }

    public string UsbVidPid { get; set; }

    public int ReferenceCount { get; set; }

    public string PortName { get; set; }

    public int PortNumber { get; set; }

    public object Tag { get; set; }

    public XmlElement Xml
    {
      get
      {
        return this.ToXml();
      }
    }

    public override string ToString()
    {
      return this.ToXml().OuterXml;
    }

    public XmlElement ToXml()
    {
      return this.ToXml(new XmlDocument());
    }

    public XmlElement ToXml(XmlDocument ownerDocument)
    {
      XObject xobject = new XObject("DeviceRegistryInfo", ownerDocument);
      xobject.SetAttribute("UsbDeviceClassKey", this.UsbDeviceClassKey);
      xobject.SetAttribute("UsbDeviceInstance", this.UsbDeviceInstance);
      xobject.SetAttribute("UsbVidPid", this.UsbVidPid);
      xobject.SetAttribute("ReferenceCount", this.ReferenceCount);
      xobject.SetAttribute("PortName", this.PortName);
      xobject.SetAttribute("PortNumber", this.PortNumber);
      return xobject.Element;
    }

    public int CompareTo(object obj)
    {
      int num1 = 0;
      if (!(obj is DeviceRegistryInfo))
        throw new ArgumentException("obj not DeviceRegistryInfo");
      DeviceRegistryInfo deviceRegistryInfo = (DeviceRegistryInfo) obj;
      int num2 = num1 == 0 ? deviceRegistryInfo.ReferenceCount.CompareTo(this.ReferenceCount) : num1;
      int num3 = num2 == 0 ? this.PortNumber.CompareTo(deviceRegistryInfo.PortNumber) : num2;
      int num4 = num3 == 0 ? string.Compare(this.PortName, deviceRegistryInfo.PortName, StringComparison.OrdinalIgnoreCase) : num3;
      int num5 = num4 == 0 ? string.Compare(this.UsbVidPid, deviceRegistryInfo.UsbVidPid, StringComparison.OrdinalIgnoreCase) : num4;
      int num6 = num5 == 0 ? string.Compare(this.UsbDeviceInstance, deviceRegistryInfo.UsbDeviceInstance, StringComparison.OrdinalIgnoreCase) : num5;
      return num6 == 0 ? string.Compare(this.UsbDeviceClassKey, deviceRegistryInfo.UsbDeviceClassKey, StringComparison.OrdinalIgnoreCase) : num6;
    }
  }
}

// Type: Dexcom.Common.Data.DeviceInformation
// Assembly: Dexcom.Common.Data, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 64B9BFC3-C891-4315-B216-574914861A80
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.Data.dll

using System;
using System.Xml;

namespace Dexcom.Common.Data
{
  public class DeviceInformation : IComparable
  {
    public Guid Id;
    public Guid DeviceId;
    public bool IsActive;
    public DeviceInformation.DeviceType KindOfDevice;
    public string SerialNumber;
    public string ProductName;
    public string ProductVersion;
    public string DatabaseVersion;
    public DateTimeOffset DateCreated;
    public DateTimeOffset DateModified;

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
      return new XDeviceInformation(ownerDocument)
      {
        Id = this.Id,
        DeviceId = this.DeviceId,
        IsActive = this.IsActive,
        KindOfDevice = this.KindOfDevice,
        ProductName = this.ProductName,
        ProductVersion = this.ProductVersion,
        DatabaseVersion = this.DatabaseVersion,
        DateCreated = this.DateCreated,
        DateModified = this.DateModified
      }.Element;
    }

    public int CompareTo(object obj)
    {
      int num1 = 0;
      if (!(obj is DeviceInformation))
        throw new ArgumentException("obj not DeviceInformation");
      DeviceInformation deviceInformation = (DeviceInformation) obj;
      int num2 = num1 == 0 ? this.KindOfDevice.CompareTo((object) deviceInformation.KindOfDevice) : num1;
      int num3 = num2 == 0 ? this.ProductName.CompareTo(deviceInformation.ProductName) : num2;
      int num4 = num3 == 0 ? this.ProductVersion.CompareTo(deviceInformation.ProductVersion) : num3;
      int num5 = num4 == 0 ? this.DatabaseVersion.CompareTo(deviceInformation.DatabaseVersion) : num4;
      int num6 = num5 == 0 ? this.SerialNumber.CompareTo(deviceInformation.SerialNumber) : num5;
      int num7 = num6 == 0 ? this.DateCreated.CompareTo(deviceInformation.DateCreated) : num6;
      int num8 = num7 == 0 ? this.DateModified.CompareTo(deviceInformation.DateModified) : num7;
      int num9 = num8 == 0 ? this.IsActive.CompareTo(deviceInformation.IsActive) : num8;
      int num10 = num9 == 0 ? this.DeviceId.CompareTo(deviceInformation.DeviceId) : num9;
      return num10 == 0 ? this.Id.CompareTo(deviceInformation.Id) : num10;
    }

    public enum DeviceType
    {
      Unknown,
      DexComSevenSeries,
      AnimasGlucoseEngine,
      InsuletGlucoseEngine,
      DexComGlobalSeries,
    }
  }
}

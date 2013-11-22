// Type: Dexcom.Common.Data.BlindTransitionData
// Assembly: Dexcom.Common.Data, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 64B9BFC3-C891-4315-B216-574914861A80
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.Data.dll

using Dexcom.Common;
using System;
using System.Xml;

namespace Dexcom.Common.Data
{
  public class BlindTransitionData : IComparable
  {
    public Guid DeviceId;
    public uint RecordNumber;
    public DateTime DisplayTime;
    public DateTime InternalTime;
    public bool IsBlinded;

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
      return new XBlindTransitionData(ownerDocument)
      {
        DeviceId = this.DeviceId,
        RecordNumber = this.RecordNumber,
        DisplayTime = (this.DisplayTime == DateTime.MinValue ? CommonValues.EmptyDateTime : this.DisplayTime),
        InternalTime = (this.InternalTime == DateTime.MinValue ? CommonValues.EmptyDateTime : this.InternalTime),
        IsBlinded = this.IsBlinded
      }.Element;
    }

    public int CompareTo(object obj)
    {
      int num1 = 0;
      if (!(obj is BlindTransitionData))
        throw new ArgumentException("obj not BlindTransitionData");
      BlindTransitionData blindTransitionData = (BlindTransitionData) obj;
      int num2 = num1 == 0 ? this.RecordNumber.CompareTo(blindTransitionData.RecordNumber) : num1;
      int num3 = num2 == 0 ? this.InternalTime.CompareTo(blindTransitionData.InternalTime) : num2;
      int num4 = num3 == 0 ? this.DisplayTime.CompareTo(blindTransitionData.DisplayTime) : num3;
      return num4 == 0 ? this.IsBlinded.CompareTo(blindTransitionData.IsBlinded) : num4;
    }
  }
}

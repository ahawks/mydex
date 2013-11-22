// Type: Dexcom.Common.Data.A1cData
// Assembly: Dexcom.Common.Data, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 64B9BFC3-C891-4315-B216-574914861A80
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.Data.dll

using Dexcom.Common;
using System;
using System.Xml;

namespace Dexcom.Common.Data
{
  public class A1cData : IComparable
  {
    public Guid Id;
    public DateTimeOffset TimeStamp;
    public double Value;

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
      return new XA1cData(ownerDocument)
      {
        Id = this.Id,
        TimeStamp = (this.TimeStamp == DateTimeOffset.MinValue ? CommonValues.EmptyDateTimeOffset : this.TimeStamp),
        Value = this.Value
      }.Element;
    }

    public int CompareTo(object obj)
    {
      int num1 = 0;
      if (!(obj is A1cData))
        throw new ArgumentException("obj not A1cData");
      A1cData a1cData = (A1cData) obj;
      int num2 = num1 == 0 ? this.TimeStamp.CompareTo(a1cData.TimeStamp) : num1;
      int num3 = num2 == 0 ? this.Value.CompareTo(a1cData.Value) : num2;
      return num3 == 0 ? this.Id.CompareTo(a1cData.Id) : num3;
    }

    public int CompareByTimeAndValue(object obj)
    {
      int num1 = 0;
      if (!(obj is A1cData))
        throw new ArgumentException("obj not A1cData");
      A1cData a1cData = (A1cData) obj;
      int num2 = num1 == 0 ? this.TimeStamp.CompareTo(a1cData.TimeStamp) : num1;
      return num2 == 0 ? this.Value.CompareTo(a1cData.Value) : num2;
    }
  }
}

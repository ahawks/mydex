// Type: Dexcom.Common.Data.TimeOffsetData
// Assembly: Dexcom.Common.Data, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 64B9BFC3-C891-4315-B216-574914861A80
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.Data.dll

using Dexcom.Common;
using System;
using System.Xml;

namespace Dexcom.Common.Data
{
  public class TimeOffsetData : IComparable
  {
    public Guid DeviceId;
    public uint RecordNumber;
    public DateTime InternalTime;
    public TimeSpan DisplayOffset;

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
      return new XTimeOffsetData(ownerDocument)
      {
        DeviceId = this.DeviceId,
        RecordNumber = this.RecordNumber,
        InternalTime = (this.InternalTime == DateTime.MinValue ? CommonValues.EmptyDateTime : this.InternalTime),
        DisplayOffset = this.DisplayOffset
      }.Element;
    }

    public int CompareTo(object obj)
    {
      int num1 = 0;
      if (!(obj is TimeOffsetData))
        throw new ArgumentException("obj not TimeOffsetData");
      TimeOffsetData timeOffsetData = (TimeOffsetData) obj;
      int num2 = num1 == 0 ? this.RecordNumber.CompareTo(timeOffsetData.RecordNumber) : num1;
      int num3 = num2 == 0 ? this.InternalTime.CompareTo(timeOffsetData.InternalTime) : num2;
      return num3 == 0 ? this.DisplayOffset.CompareTo(timeOffsetData.DisplayOffset) : num3;
    }
  }
}

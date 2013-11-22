// Type: Dexcom.Common.Data.MeterData
// Assembly: Dexcom.Common.Data, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 64B9BFC3-C891-4315-B216-574914861A80
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.Data.dll

using Dexcom.Common;
using System;
using System.Xml;

namespace Dexcom.Common.Data
{
  public class MeterData : IGenericData, IComparable
  {
    public Guid DeviceId;
    public uint RecordNumber;
    public DateTime DisplayTime;
    private DateTime m_internalTime;
    public DateTime MeterTime;
    public short Value;

    public DateTime MeterDisplayTime
    {
      get
      {
        return this.DisplayTime - this.InternalTime - this.MeterTime;
      }
    }

    public XmlElement Xml
    {
      get
      {
        return this.ToXml();
      }
    }

    public DateTime InternalTime
    {
      get
      {
        return this.m_internalTime;
      }
      set
      {
        this.m_internalTime = value;
      }
    }

    public TimeSpan DisplayOffset
    {
      get
      {
        return this.InternalTime - this.DisplayTime;
      }
      set
      {
        this.DisplayTime = this.InternalTime + value;
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
      return new XMeterData(ownerDocument)
      {
        DeviceId = this.DeviceId,
        RecordNumber = this.RecordNumber,
        DisplayTime = (this.DisplayTime == DateTime.MinValue ? CommonValues.EmptyDateTime : this.DisplayTime),
        InternalTime = (this.InternalTime == DateTime.MinValue ? CommonValues.EmptyDateTime : this.InternalTime),
        MeterTime = (this.MeterTime == DateTime.MinValue ? CommonValues.EmptyDateTime : this.MeterTime),
        MeterDisplayTime = (this.MeterTime == DateTime.MinValue ? CommonValues.EmptyDateTime : this.MeterDisplayTime),
        Value = this.Value
      }.Element;
    }

    public int CompareTo(object obj)
    {
      int num1 = 0;
      if (!(obj is MeterData))
        throw new ArgumentException("obj not MeterData");
      MeterData meterData = (MeterData) obj;
      int num2 = num1 == 0 ? this.RecordNumber.CompareTo(meterData.RecordNumber) : num1;
      int num3 = num2 == 0 ? this.InternalTime.CompareTo(meterData.InternalTime) : num2;
      int num4 = num3 == 0 ? this.DisplayTime.CompareTo(meterData.DisplayTime) : num3;
      int num5 = num4 == 0 ? this.MeterTime.CompareTo(meterData.MeterTime) : num4;
      return num5 == 0 ? this.Value.CompareTo(meterData.Value) : num5;
    }
  }
}

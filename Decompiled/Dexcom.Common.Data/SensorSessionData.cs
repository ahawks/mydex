// Type: Dexcom.Common.Data.SensorSessionData
// Assembly: Dexcom.Common.Data, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 64B9BFC3-C891-4315-B216-574914861A80
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.Data.dll

using Dexcom.Common;
using System;
using System.Xml;

namespace Dexcom.Common.Data
{
  public class SensorSessionData : IGenericData, IComparable
  {
    public Guid DeviceId;
    public DateTime StartDisplayTime;
    public DateTime StartInternalTime;
    public DateTime FinishDisplayTime;
    public DateTime FinishInternalTime;
    public uint StartRecordNumber;
    public uint FinishRecordNumber;

    public bool HasSessionStarted
    {
      get
      {
        return this.StartInternalTime != DateTime.MinValue;
      }
    }

    public bool HasSessionFinished
    {
      get
      {
        return this.FinishInternalTime != DateTime.MaxValue;
      }
    }

    public TimeSpan Duration
    {
      get
      {
        if (this.HasSessionStarted && this.HasSessionFinished)
          return this.FinishInternalTime - this.StartInternalTime;
        else
          return TimeSpan.Zero;
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
        return this.StartInternalTime;
      }
      set
      {
        this.StartInternalTime = value;
      }
    }

    public TimeSpan DisplayOffset
    {
      get
      {
        return this.StartInternalTime - this.StartDisplayTime;
      }
      set
      {
        this.StartDisplayTime = this.StartInternalTime + value;
      }
    }

    public TimeSpan FinishDisplayOffset
    {
      get
      {
        return this.FinishInternalTime - this.FinishDisplayTime;
      }
      set
      {
        this.FinishDisplayTime = this.FinishInternalTime + value;
      }
    }

    public SensorSessionData()
    {
      this.StartInternalTime = DateTime.MinValue;
      this.FinishInternalTime = DateTime.MaxValue;
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
      return new XSensorSessionData(ownerDocument)
      {
        DeviceId = this.DeviceId,
        StartDisplayTime = (this.StartDisplayTime == DateTime.MinValue ? CommonValues.EmptyDateTime : this.StartDisplayTime),
        StartInternalTime = (this.StartInternalTime == DateTime.MinValue ? CommonValues.EmptyDateTime : this.StartInternalTime),
        FinishDisplayTime = (this.FinishDisplayTime == DateTime.MinValue ? CommonValues.EmptyDateTime : this.FinishDisplayTime),
        FinishInternalTime = (this.FinishInternalTime == DateTime.MinValue ? CommonValues.EmptyDateTime : this.FinishInternalTime),
        StartRecordNumber = this.StartRecordNumber,
        FinishRecordNumber = this.FinishRecordNumber
      }.Element;
    }

    public int CompareTo(object obj)
    {
      int num1 = 0;
      if (!(obj is SensorSessionData))
        throw new ArgumentException("obj not SensorSessionData");
      SensorSessionData sensorSessionData = (SensorSessionData) obj;
      int num2 = num1 == 0 ? this.StartRecordNumber.CompareTo(sensorSessionData.StartRecordNumber) : num1;
      int num3 = num2 == 0 ? this.StartInternalTime.CompareTo(sensorSessionData.StartInternalTime) : num2;
      int num4 = num3 == 0 ? this.FinishRecordNumber.CompareTo(sensorSessionData.FinishRecordNumber) : num3;
      int num5 = num4 == 0 ? this.FinishInternalTime.CompareTo(sensorSessionData.FinishInternalTime) : num4;
      int num6 = num5 == 0 ? this.StartDisplayTime.CompareTo(sensorSessionData.StartDisplayTime) : num5;
      return num6 == 0 ? this.FinishDisplayTime.CompareTo(sensorSessionData.FinishDisplayTime) : num6;
    }
  }
}

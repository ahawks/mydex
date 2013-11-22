// Type: Dexcom.Common.Data.GlucoseData
// Assembly: Dexcom.Common.Data, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 64B9BFC3-C891-4315-B216-574914861A80
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.Data.dll

using Dexcom.Common;
using System;
using System.Xml;

namespace Dexcom.Common.Data
{
  public class GlucoseData : IGenericData, IComparable
  {
    public GlucoseDataStruct m_glucoseDataStruct;

    public Guid DeviceId
    {
      get
      {
        return this.m_glucoseDataStruct.DeviceId;
      }
      set
      {
        this.m_glucoseDataStruct.DeviceId = value;
      }
    }

    public uint RecordNumber
    {
      get
      {
        return this.m_glucoseDataStruct.RecordNumber;
      }
      set
      {
        this.m_glucoseDataStruct.RecordNumber = value;
      }
    }

    public DateTime DisplayTime
    {
      get
      {
        return this.m_glucoseDataStruct.DisplayTime;
      }
      set
      {
        this.m_glucoseDataStruct.DisplayTime = value;
      }
    }

    public DateTime InternalTime
    {
      get
      {
        return this.m_glucoseDataStruct.InternalTime;
      }
      set
      {
        this.m_glucoseDataStruct.InternalTime = value;
      }
    }

    public short Value
    {
      get
      {
        return this.m_glucoseDataStruct.Value;
      }
      set
      {
        this.m_glucoseDataStruct.Value = value;
      }
    }

    public int GapCount
    {
      get
      {
        return this.m_glucoseDataStruct.GapCount;
      }
      set
      {
        this.m_glucoseDataStruct.GapCount = value;
      }
    }

    public TimeSpan GapTimeSpan
    {
      get
      {
        return this.m_glucoseDataStruct.GapTimeSpan;
      }
      set
      {
        this.m_glucoseDataStruct.GapTimeSpan = value;
      }
    }

    public XmlElement Xml
    {
      get
      {
        return this.ToXml();
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

    public GlucoseData()
    {
    }

    public GlucoseData(GlucoseDataStruct glucoseDataStruct)
    {
      this.m_glucoseDataStruct = glucoseDataStruct;
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
      return new XGlucoseData(ownerDocument)
      {
        DeviceId = this.DeviceId,
        RecordNumber = this.RecordNumber,
        DisplayTime = (this.DisplayTime == DateTime.MinValue ? CommonValues.EmptyDateTime : this.DisplayTime),
        InternalTime = (this.InternalTime == DateTime.MinValue ? CommonValues.EmptyDateTime : this.InternalTime),
        Value = this.Value,
        GapCount = this.GapCount,
        GapTimeSpan = this.GapTimeSpan
      }.Element;
    }

    public int CompareTo(object obj)
    {
      int num1 = 0;
      if (!(obj is GlucoseData))
        throw new ArgumentException("obj not GlucoseData");
      GlucoseData glucoseData = (GlucoseData) obj;
      int num2 = num1 == 0 ? this.RecordNumber.CompareTo(glucoseData.RecordNumber) : num1;
      int num3 = num2 == 0 ? this.InternalTime.CompareTo(glucoseData.InternalTime) : num2;
      int num4 = num3 == 0 ? this.DisplayTime.CompareTo(glucoseData.DisplayTime) : num3;
      int num5 = num4 == 0 ? this.Value.CompareTo(glucoseData.Value) : num4;
      int num6 = num5 == 0 ? this.GapCount.CompareTo(glucoseData.GapCount) : num5;
      return num6 == 0 ? this.GapTimeSpan.CompareTo(glucoseData.GapTimeSpan) : num6;
    }
  }
}

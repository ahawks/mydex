// Type: Dexcom.R2Receiver.MatchedPair
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using Dexcom.Common;
using System;
using System.Xml;

namespace Dexcom.R2Receiver
{
  public class MatchedPair : IGenericRecord
  {
    public MeterRecord Meter { get; set; }

    public Sensor2Record Sensor { get; set; }

    public STSCalibrationRecord MeterPriorCalSet { get; set; }

    public STSCalibrationRecord SensorPriorCalSet { get; set; }

    public ushort MeterValue
    {
      get
      {
        return this.Meter.MeterValue;
      }
    }

    public ushort CalculatedGlucoseValue { get; set; }

    public string AccuracyRegion { get; set; }

    public bool Is2020 { get; set; }

    public DateTime SystemTime
    {
      get
      {
        return this.Meter.GlucoseTimeStamp;
      }
    }

    public DateTime DisplayTime
    {
      get
      {
        return this.Meter.WorldTimeStamp;
      }
    }

    public R2RecordType RecordType
    {
      get
      {
        return R2RecordType.Unassigned;
      }
    }

    public int RecordNumber
    {
      get
      {
        return this.Meter.RecordNumber;
      }
      set
      {
      }
    }

    public uint BlockNumber
    {
      get
      {
        return this.Meter.BlockNumber;
      }
      set
      {
      }
    }

    public object Tag { get; set; }

    public DateTime TimeStampUtc
    {
      get
      {
        return this.SystemTime;
      }
    }

    public DateTime CorrectedTimeStampUtc
    {
      get
      {
        return this.Meter.CorrectedTimeStampUtc;
      }
    }

    public DateTime UserTimeStamp
    {
      get
      {
        return this.DisplayTime;
      }
    }

    public XmlElement Xml
    {
      get
      {
        return this.ToXml();
      }
    }

    public XmlElement ToXml()
    {
      return this.ToXml(new XmlDocument());
    }

    public XmlElement ToXml(XmlDocument xOwner)
    {
      XObject xobject = new XObject("MatchedPair", xOwner);
      xobject.SetAttribute("BlockNumber", this.BlockNumber);
      xobject.SetAttribute("RecordNumber", this.RecordNumber);
      xobject.SetAttribute("SystemTime", this.SystemTime);
      xobject.SetAttribute("DisplayTime", this.DisplayTime);
      xobject.SetAttribute("MeterValue", this.MeterValue);
      xobject.SetAttribute("CalculatedGlucoseValue", this.CalculatedGlucoseValue);
      xobject.SetAttribute("AccuracyRegion", this.AccuracyRegion);
      xobject.SetAttribute("Is2020", this.Is2020);
      return xobject.Element;
    }
  }
}

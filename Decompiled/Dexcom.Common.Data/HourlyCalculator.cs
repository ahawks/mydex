// Type: Dexcom.Common.Data.HourlyCalculator
// Assembly: Dexcom.Common.Data, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 64B9BFC3-C891-4315-B216-574914861A80
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.Data.dll

using Dexcom.Common;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Dexcom.Common.Data
{
  public class HourlyCalculator
  {
    private SensorCriteria m_sensorCriteria = new SensorCriteria();
    private SensorStats m_summaryStats = new SensorStats();
    private List<SensorStats> m_hourlyStats = new List<SensorStats>();

    public SensorCriteria SensorCriteria
    {
      get
      {
        return this.m_sensorCriteria;
      }
      set
      {
        this.m_sensorCriteria = value;
      }
    }

    public SensorStats SummaryStats
    {
      get
      {
        return this.m_summaryStats;
      }
      set
      {
        this.m_summaryStats = value;
      }
    }

    public List<SensorStats> HourlyStats
    {
      get
      {
        return this.m_hourlyStats;
      }
      set
      {
        this.m_hourlyStats = value;
      }
    }

    public HourlyCalculator()
    {
      for (int index = 0; index < 24; ++index)
        this.m_hourlyStats.Add(new SensorStats());
    }

    public void Add(DateTime time, double value)
    {
      if (!this.m_sensorCriteria.IsMatch(time, value))
        return;
      int hour = time.Hour;
      if (hour >= this.m_hourlyStats.Count || this.m_hourlyStats[hour] == null)
        return;
      this.m_hourlyStats[hour].Add(time, value);
      this.m_summaryStats.Add(time, value);
    }

    public void Calculate()
    {
      this.m_summaryStats.Calculate();
      foreach (SensorStats sensorStats in this.m_hourlyStats)
      {
        sensorStats.TotalCount = this.m_summaryStats.Count;
        sensorStats.Calculate();
      }
    }

    public XmlElement ToXml()
    {
      return this.ToXml(new XmlDocument());
    }

    public XmlElement ToXml(XmlDocument ownerDocument)
    {
      XObject xobject = new XObject("HourlyCalculator", ownerDocument);
      XmlElement xmlElement1 = this.m_sensorCriteria.ToXml(ownerDocument);
      xobject.Element.AppendChild((XmlNode) xmlElement1);
      XmlElement xmlElement2 = this.m_summaryStats.ToXml(ownerDocument);
      xobject.Element.AppendChild((XmlNode) xmlElement2);
      XObject xChildObject = new XObject("HourlyStats", ownerDocument);
      xobject.AppendChild(xChildObject);
      foreach (SensorStats sensorStats in this.m_hourlyStats)
      {
        XmlElement xmlElement3 = sensorStats.ToXml(ownerDocument);
        xChildObject.Element.AppendChild((XmlNode) xmlElement3);
      }
      return xobject.Element;
    }
  }
}

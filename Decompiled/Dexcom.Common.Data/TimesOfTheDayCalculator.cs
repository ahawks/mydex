// Type: Dexcom.Common.Data.TimesOfTheDayCalculator
// Assembly: Dexcom.Common.Data, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 64B9BFC3-C891-4315-B216-574914861A80
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.Data.dll

using Dexcom.Common;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Dexcom.Common.Data
{
  public class TimesOfTheDayCalculator
  {
    private SensorCriteria m_sensorCriteria = new SensorCriteria();
    private SensorStats m_summaryStats = new SensorStats();
    private List<TimesOfTheDayStatsPair> m_timesOfTheDayStats = new List<TimesOfTheDayStatsPair>();

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

    public List<TimesOfTheDayStatsPair> TimesOfTheDayStats
    {
      get
      {
        return this.m_timesOfTheDayStats;
      }
      set
      {
        this.m_timesOfTheDayStats = value;
      }
    }

    public void Add(DateTime time, double value)
    {
      if (!this.m_sensorCriteria.IsMatch(time, value))
        return;
      bool flag = false;
      foreach (TimesOfTheDayStatsPair ofTheDayStatsPair in this.m_timesOfTheDayStats)
      {
        if (ofTheDayStatsPair.Range.IsMatch(time))
        {
          ofTheDayStatsPair.Stats.Add(time, value);
          flag = true;
        }
      }
      if (!flag)
        return;
      this.m_summaryStats.Add(time, value);
    }

    public void Calculate()
    {
      this.m_summaryStats.Calculate();
      foreach (TimesOfTheDayStatsPair ofTheDayStatsPair in this.m_timesOfTheDayStats)
      {
        ofTheDayStatsPair.Stats.TotalCount = this.m_summaryStats.Count;
        ofTheDayStatsPair.Stats.Calculate();
      }
    }

    public XmlElement ToXml()
    {
      return this.ToXml(new XmlDocument());
    }

    public XmlElement ToXml(XmlDocument ownerDocument)
    {
      XObject xobject = new XObject("TimesOfTheDayCalculator", ownerDocument);
      XmlElement xmlElement1 = this.m_sensorCriteria.ToXml(ownerDocument);
      xobject.Element.AppendChild((XmlNode) xmlElement1);
      XmlElement xmlElement2 = this.m_summaryStats.ToXml(ownerDocument);
      xobject.Element.AppendChild((XmlNode) xmlElement2);
      XObject xChildObject = new XObject("TimesOfTheDayStats", ownerDocument);
      xobject.AppendChild(xChildObject);
      foreach (TimesOfTheDayStatsPair ofTheDayStatsPair in this.m_timesOfTheDayStats)
      {
        XmlElement xmlElement3 = ofTheDayStatsPair.ToXml(ownerDocument);
        xChildObject.Element.AppendChild((XmlNode) xmlElement3);
      }
      return xobject.Element;
    }
  }
}

// Type: Dexcom.Common.Data.TimesOfTheDayWithGlucoseRangesCalculator
// Assembly: Dexcom.Common.Data, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 64B9BFC3-C891-4315-B216-574914861A80
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.Data.dll

using Dexcom.Common;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Dexcom.Common.Data
{
  public class TimesOfTheDayWithGlucoseRangesCalculator
  {
    private bool m_calculateSummaryStatsForAnyMatch = true;
    private SensorCriteria m_sensorCriteria = new SensorCriteria();
    private TimesOfTheDayWithGlucoseRangeStats m_summaryStats = new TimesOfTheDayWithGlucoseRangeStats();
    private List<TimesOfTheDayWithGlucoseRangeStats> m_timesOfTheDayWithGlucoseRangesStats = new List<TimesOfTheDayWithGlucoseRangeStats>();

    public bool CalculateSummaryStatsForAnyMatch
    {
      get
      {
        return this.m_calculateSummaryStatsForAnyMatch;
      }
      set
      {
        this.m_calculateSummaryStatsForAnyMatch = value;
      }
    }

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

    public TimesOfTheDayWithGlucoseRangeStats SummaryStats
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

    public List<TimesOfTheDayWithGlucoseRangeStats> TimesOfTheDayWithGlucoseRangesStats
    {
      get
      {
        return this.m_timesOfTheDayWithGlucoseRangesStats;
      }
      set
      {
        this.m_timesOfTheDayWithGlucoseRangesStats = value;
      }
    }

    public void Add(DateTime time, double value)
    {
      if (!this.m_sensorCriteria.IsMatch(time, value))
        return;
      bool flag = false;
      foreach (TimesOfTheDayWithGlucoseRangeStats glucoseRangeStats in this.m_timesOfTheDayWithGlucoseRangesStats)
      {
        if (glucoseRangeStats.Add(time, value))
          flag = true;
      }
      if (!flag && !this.CalculateSummaryStatsForAnyMatch)
        return;
      this.m_summaryStats.Add(time, value);
    }

    public void Calculate()
    {
      this.m_summaryStats.Calculate();
      foreach (TimesOfTheDayWithGlucoseRangeStats glucoseRangeStats in this.m_timesOfTheDayWithGlucoseRangesStats)
      {
        glucoseRangeStats.TotalCount = this.m_summaryStats.Count;
        glucoseRangeStats.Calculate();
      }
    }

    public XmlElement ToXml()
    {
      return this.ToXml(new XmlDocument());
    }

    public XmlElement ToXml(XmlDocument ownerDocument)
    {
      XObject xobject = new XObject("TimesOfTheDayWithGlucoseRangesCalculator", ownerDocument);
      xobject.SetAttribute("CalculateSummaryStatsForAnyMatch", this.m_calculateSummaryStatsForAnyMatch);
      XmlElement xmlElement1 = this.m_sensorCriteria.ToXml(ownerDocument);
      xobject.Element.AppendChild((XmlNode) xmlElement1);
      XmlElement xmlElement2 = this.m_summaryStats.ToXml(ownerDocument);
      xobject.Element.AppendChild((XmlNode) xmlElement2);
      XObject xChildObject = new XObject("TimesOfTheDayWithGlucoseRangesStats", ownerDocument);
      xobject.AppendChild(xChildObject);
      foreach (TimesOfTheDayWithGlucoseRangeStats glucoseRangeStats in this.m_timesOfTheDayWithGlucoseRangesStats)
      {
        XmlElement xmlElement3 = glucoseRangeStats.ToXml(ownerDocument);
        xChildObject.Element.AppendChild((XmlNode) xmlElement3);
      }
      return xobject.Element;
    }
  }
}

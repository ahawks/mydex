// Type: Dexcom.Common.Data.GlucoseRangesCalculator
// Assembly: Dexcom.Common.Data, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 64B9BFC3-C891-4315-B216-574914861A80
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.Data.dll

using Dexcom.Common;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Dexcom.Common.Data
{
  public class GlucoseRangesCalculator
  {
    private bool m_calculateSummaryStatsForAnyMatch = true;
    private SensorCriteria m_sensorCriteria = new SensorCriteria();
    private SensorStats m_summaryStats = new SensorStats();
    private List<GlucoseRangeStatsPair> m_glucoseRangeStats = new List<GlucoseRangeStatsPair>();
    private bool m_isCalculated;

    public bool IsCalculated
    {
      get
      {
        return this.m_isCalculated;
      }
      set
      {
        this.m_isCalculated = value;
      }
    }

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

    public List<GlucoseRangeStatsPair> GlucoseRangeStats
    {
      get
      {
        return this.m_glucoseRangeStats;
      }
      set
      {
        this.m_glucoseRangeStats = value;
      }
    }

    public void Add(DateTime time, double value)
    {
      if (!this.m_sensorCriteria.IsMatch(time, value))
        return;
      bool flag = false;
      foreach (GlucoseRangeStatsPair glucoseRangeStatsPair in this.m_glucoseRangeStats)
      {
        if (glucoseRangeStatsPair.Range.IsMatch(value))
        {
          glucoseRangeStatsPair.Stats.Add(time, value);
          flag = true;
        }
      }
      if (!flag && !this.CalculateSummaryStatsForAnyMatch)
        return;
      this.m_summaryStats.Add(time, value);
    }

    public void Calculate()
    {
      this.m_summaryStats.Calculate();
      foreach (GlucoseRangeStatsPair glucoseRangeStatsPair in this.m_glucoseRangeStats)
      {
        glucoseRangeStatsPair.Stats.TotalCount = this.m_summaryStats.Count;
        glucoseRangeStatsPair.Stats.Calculate();
      }
      this.m_isCalculated = true;
    }

    public XmlElement ToXml()
    {
      return this.ToXml(new XmlDocument());
    }

    public XmlElement ToXml(XmlDocument ownerDocument)
    {
      XObject xobject = new XObject("GlucoseRangesCalculator", ownerDocument);
      xobject.SetAttribute("IsCalculated", this.m_isCalculated);
      xobject.SetAttribute("CalculateSummaryStatsForAnyMatch", this.m_calculateSummaryStatsForAnyMatch);
      XmlElement xmlElement1 = this.m_sensorCriteria.ToXml(ownerDocument);
      xobject.Element.AppendChild((XmlNode) xmlElement1);
      XmlElement xmlElement2 = this.m_summaryStats.ToXml(ownerDocument);
      xobject.Element.AppendChild((XmlNode) xmlElement2);
      XObject xChildObject = new XObject("GlucoseRangeStats", ownerDocument);
      xobject.AppendChild(xChildObject);
      foreach (GlucoseRangeStatsPair glucoseRangeStatsPair in this.m_glucoseRangeStats)
      {
        XmlElement xmlElement3 = glucoseRangeStatsPair.ToXml(ownerDocument);
        xChildObject.Element.AppendChild((XmlNode) xmlElement3);
      }
      return xobject.Element;
    }
  }
}

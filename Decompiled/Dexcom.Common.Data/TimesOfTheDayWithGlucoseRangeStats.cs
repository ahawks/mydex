// Type: Dexcom.Common.Data.TimesOfTheDayWithGlucoseRangeStats
// Assembly: Dexcom.Common.Data, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 64B9BFC3-C891-4315-B216-574914861A80
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.Data.dll

using Dexcom.Common;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Dexcom.Common.Data
{
  public class TimesOfTheDayWithGlucoseRangeStats
  {
    private SensorStats m_summaryStats = new SensorStats();
    private List<GlucoseRangeStatsPair> m_glucoseRangeStats = new List<GlucoseRangeStatsPair>();
    private TimeOfDayRange m_range;

    public int Count
    {
      get
      {
        return this.m_summaryStats.Count;
      }
    }

    public int TotalCount
    {
      get
      {
        return this.m_summaryStats.TotalCount;
      }
      set
      {
        this.m_summaryStats.TotalCount = value;
      }
    }

    public SensorStats Stats
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

    public TimeOfDayRange Range
    {
      get
      {
        return this.m_range;
      }
      set
      {
        this.m_range = value;
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

    public TimesOfTheDayWithGlucoseRangeStats()
    {
    }

    public TimesOfTheDayWithGlucoseRangeStats(TimeOfDayRange range)
    {
      this.m_range = range;
    }

    public bool Add(DateTime time, double value)
    {
      bool flag1 = false;
      if (this.m_range == null || this.m_range.IsMatch(time))
      {
        bool flag2 = false;
        foreach (GlucoseRangeStatsPair glucoseRangeStatsPair in this.m_glucoseRangeStats)
        {
          if (glucoseRangeStatsPair.Range.IsMatch(value))
          {
            glucoseRangeStatsPair.Stats.Add(time, value);
            flag2 = true;
          }
        }
        if (flag2)
        {
          this.m_summaryStats.Add(time, value);
          flag1 = true;
        }
      }
      return flag1;
    }

    public void Calculate()
    {
      this.m_summaryStats.Calculate();
      foreach (GlucoseRangeStatsPair glucoseRangeStatsPair in this.m_glucoseRangeStats)
      {
        glucoseRangeStatsPair.Stats.TotalCount = this.m_summaryStats.Count;
        glucoseRangeStatsPair.Stats.Calculate();
      }
    }

    public XmlElement ToXml()
    {
      return this.ToXml(new XmlDocument());
    }

    public XmlElement ToXml(XmlDocument ownerDocument)
    {
      XObject xobject = new XObject("TimesOfTheDayWithGlucoseRangeStats", ownerDocument);
      xobject.SetAttribute("IsRangeNull", this.m_range == null);
      if (this.m_range != null)
      {
        XmlElement xmlElement = this.m_range.ToXml(ownerDocument);
        xobject.Element.AppendChild((XmlNode) xmlElement);
      }
      XmlElement xmlElement1 = this.m_summaryStats.ToXml(ownerDocument);
      xobject.Element.AppendChild((XmlNode) xmlElement1);
      XObject xChildObject = new XObject("GlucoseRangeStats", ownerDocument);
      xobject.AppendChild(xChildObject);
      foreach (GlucoseRangeStatsPair glucoseRangeStatsPair in this.m_glucoseRangeStats)
      {
        XmlElement xmlElement2 = glucoseRangeStatsPair.ToXml(ownerDocument);
        xChildObject.Element.AppendChild((XmlNode) xmlElement2);
      }
      return xobject.Element;
    }
  }
}

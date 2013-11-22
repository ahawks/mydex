// Type: Dexcom.Common.Data.SensorCriteria
// Assembly: Dexcom.Common.Data, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 64B9BFC3-C891-4315-B216-574914861A80
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.Data.dll

using Dexcom.Common;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Dexcom.Common.Data
{
  public class SensorCriteria
  {
    private List<GlucoseRange> m_glucoseRanges = new List<GlucoseRange>();
    private List<TimeOfDayRange> m_timesOfDay = new List<TimeOfDayRange>();
    private List<DayOfWeek> m_daysOfTheWeek = new List<DayOfWeek>();
    private DateTime m_startDateCriteria = CommonValues.EmptyDateTime;
    private DateTime m_endDateCriteria = CommonValues.EmptyDateTime;

    public DateTime StartDateCriteria
    {
      get
      {
        return this.m_startDateCriteria;
      }
      set
      {
        this.m_startDateCriteria = value;
      }
    }

    public DateTime EndDateCriteria
    {
      get
      {
        return this.m_endDateCriteria;
      }
      set
      {
        this.m_endDateCriteria = value;
      }
    }

    public List<TimeOfDayRange> TimesOfTheDay
    {
      get
      {
        return this.m_timesOfDay;
      }
      set
      {
        this.m_timesOfDay = value;
      }
    }

    public List<DayOfWeek> DaysOfTheWeek
    {
      get
      {
        return this.m_daysOfTheWeek;
      }
      set
      {
        this.m_daysOfTheWeek = value;
      }
    }

    public List<GlucoseRange> GlucoseRanges
    {
      get
      {
        return this.m_glucoseRanges;
      }
      set
      {
        this.m_glucoseRanges = value;
      }
    }

    public bool IsMatch(DateTime time, double value)
    {
      return this.IsMatchStartEndCriteria(time) && this.IsMatchDaysOfTheWeek(time) && this.IsMatchTimesOfTheDay(time) && this.IsMatchGlucoseRanges(value);
    }

    public bool IsMatchStartEndCriteria(DateTime time)
    {
      bool flag = true;
      if (this.m_startDateCriteria != CommonValues.EmptyDateTime)
        flag = flag && time >= this.m_startDateCriteria;
      if (this.m_endDateCriteria != CommonValues.EmptyDateTime)
        flag = flag && time <= this.m_endDateCriteria;
      return flag;
    }

    public bool IsMatchGlucoseRanges(double value)
    {
      bool flag = false;
      if (this.m_glucoseRanges.Count > 0)
      {
        foreach (GlucoseRange glucoseRange in this.m_glucoseRanges)
        {
          if (glucoseRange.IsMatch(value))
          {
            flag = true;
            break;
          }
        }
      }
      else
        flag = true;
      return flag;
    }

    public bool IsMatchTimesOfTheDay(DateTime time)
    {
      bool flag = false;
      if (this.m_timesOfDay.Count > 0)
      {
        foreach (TimeOfDayRange timeOfDayRange in this.m_timesOfDay)
        {
          if (timeOfDayRange.IsMatch(time))
          {
            flag = true;
            break;
          }
        }
      }
      else
        flag = true;
      return flag;
    }

    public bool IsMatchDaysOfTheWeek(DateTime time)
    {
      bool flag = false;
      DayOfWeek dayOfWeek1 = time.DayOfWeek;
      if (this.m_daysOfTheWeek.Count > 0)
      {
        foreach (DayOfWeek dayOfWeek2 in this.m_daysOfTheWeek)
        {
          if (dayOfWeek1 == dayOfWeek2)
          {
            flag = true;
            break;
          }
        }
      }
      else
        flag = true;
      return flag;
    }

    public XmlElement ToXml()
    {
      return this.ToXml(new XmlDocument());
    }

    public XmlElement ToXml(XmlDocument ownerDocument)
    {
      XObject xobject = new XObject("SensorCriteria", ownerDocument);
      XObject xChildObject1 = new XObject("GlucoseRanges", ownerDocument);
      XObject xChildObject2 = new XObject("TimeOfDayRanges", ownerDocument);
      XObject xChildObject3 = new XObject("DaysOfTheWeek", ownerDocument);
      xobject.SetAttribute("StartDateCriteria", this.m_startDateCriteria);
      xobject.SetAttribute("EndDateCriteria", this.m_endDateCriteria);
      xobject.AppendChild(xChildObject1);
      xobject.AppendChild(xChildObject2);
      xobject.AppendChild(xChildObject3);
      foreach (GlucoseRange glucoseRange in this.m_glucoseRanges)
      {
        XmlElement xmlElement = glucoseRange.ToXml(ownerDocument);
        xChildObject1.Element.AppendChild((XmlNode) xmlElement);
      }
      foreach (TimeOfDayRange timeOfDayRange in this.m_timesOfDay)
      {
        XmlElement xmlElement = timeOfDayRange.ToXml(ownerDocument);
        xChildObject2.Element.AppendChild((XmlNode) xmlElement);
      }
      foreach (DayOfWeek dayOfWeek in this.m_daysOfTheWeek)
      {
        XObject xChildObject4 = new XObject("DayOfWeek", ownerDocument);
        xChildObject4.SetAttribute("Value", ((object) dayOfWeek).ToString());
        xChildObject3.AppendChild(xChildObject4);
      }
      return xobject.Element;
    }
  }
}

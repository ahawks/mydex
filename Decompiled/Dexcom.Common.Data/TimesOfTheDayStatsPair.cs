// Type: Dexcom.Common.Data.TimesOfTheDayStatsPair
// Assembly: Dexcom.Common.Data, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 64B9BFC3-C891-4315-B216-574914861A80
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.Data.dll

using Dexcom.Common;
using System.Xml;

namespace Dexcom.Common.Data
{
  public class TimesOfTheDayStatsPair
  {
    private SensorStats m_stats = new SensorStats();
    private TimeOfDayRange m_range;

    public SensorStats Stats
    {
      get
      {
        return this.m_stats;
      }
      set
      {
        this.m_stats = value;
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

    public TimesOfTheDayStatsPair()
    {
    }

    public TimesOfTheDayStatsPair(TimeOfDayRange range)
    {
      this.m_range = range;
    }

    public XmlElement ToXml()
    {
      return this.ToXml(new XmlDocument());
    }

    public XmlElement ToXml(XmlDocument ownerDocument)
    {
      XObject xobject = new XObject("TimesOfTheDayStatsPair", ownerDocument);
      xobject.SetAttribute("IsRangeNull", this.m_range == null);
      if (this.m_range != null)
      {
        XmlElement xmlElement = this.m_range.ToXml(ownerDocument);
        xobject.Element.AppendChild((XmlNode) xmlElement);
      }
      XmlElement xmlElement1 = this.m_stats.ToXml(ownerDocument);
      xobject.Element.AppendChild((XmlNode) xmlElement1);
      return xobject.Element;
    }
  }
}

// Type: Dexcom.Common.Data.GlucoseRangeStatsPair
// Assembly: Dexcom.Common.Data, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 64B9BFC3-C891-4315-B216-574914861A80
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.Data.dll

using Dexcom.Common;
using System.Xml;

namespace Dexcom.Common.Data
{
  public class GlucoseRangeStatsPair
  {
    private SensorStats m_stats = new SensorStats();
    private GlucoseRange m_range;

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

    public GlucoseRange Range
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

    public GlucoseRangeStatsPair()
    {
    }

    public GlucoseRangeStatsPair(GlucoseRange range)
    {
      this.m_range = range;
    }

    public XmlElement ToXml()
    {
      return this.ToXml(new XmlDocument());
    }

    public XmlElement ToXml(XmlDocument ownerDocument)
    {
      XObject xobject = new XObject("GlucoseRangeStatsPair", ownerDocument);
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

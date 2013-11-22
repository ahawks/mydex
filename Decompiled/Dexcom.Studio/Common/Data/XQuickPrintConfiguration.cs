// Type: Dexcom.Common.Data.XQuickPrintConfiguration
// Assembly: Dexcom.Studio, Version=12.0.3.43, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: DEB7F911-78A1-4A44-A206-F03A1B17E3DE
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Studio.exe

using Dexcom.Common;
using Dexcom.Tools.DataManager;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security;
using System.Xml;

namespace Dexcom.Common.Data
{
  [Serializable]
  public class XQuickPrintConfiguration : XObject, ISerializable
  {
    public const string Tag = "QuickPrintConfiguration";
    public const string HourlyStatsDaysOfTheWeekTag = "HourlyStatsDaysOfTheWeek";
    public const string HourlyTrendDaysOfTheWeekTag = "HourlyTrendDaysOfTheWeek";
    public const string DistributionDaysOfTheWeekTag = "DistributionDaysOfTheWeek";
    public const string DailyStatsDaysOfTheWeekTag = "DailyStatsDaysOfTheWeek";

    public DateTimeOffset DateCreated
    {
      get
      {
        return this.GetAttribute<DateTimeOffset>("DateCreated");
      }
      set
      {
        this.SetAttribute("DateCreated", value);
      }
    }

    public DateTimeOffset DateModified
    {
      get
      {
        return this.GetAttribute<DateTimeOffset>("DateModified");
      }
      set
      {
        this.SetAttribute("DateModified", value);
      }
    }

    public int ConfigurationNumber
    {
      get
      {
        return this.GetAttribute<int>("ConfigurationNumber");
      }
      set
      {
        this.SetAttribute("ConfigurationNumber", value);
      }
    }

    public bool IsIncludePatternReport
    {
      get
      {
        if (this.HasAttribute("IsIncludePatternReport"))
          return this.GetAttribute<bool>("IsIncludePatternReport");
        else
          return true;
      }
      set
      {
        this.SetAttribute("IsIncludePatternReport", value);
      }
    }

    public bool IsIncludeGlucoseTrend
    {
      get
      {
        return this.GetAttribute<bool>("IsIncludeGlucoseTrend");
      }
      set
      {
        this.SetAttribute("IsIncludeGlucoseTrend", value);
      }
    }

    public bool IsIncludeGlucoseTrendStrip
    {
      get
      {
        if (this.HasAttribute("IsIncludeGlucoseTrendStrip"))
          return this.GetAttribute<bool>("IsIncludeGlucoseTrendStrip");
        else
          return true;
      }
      set
      {
        this.SetAttribute("IsIncludeGlucoseTrendStrip", value);
      }
    }

    public bool IsIncludeHourlyStats
    {
      get
      {
        return this.GetAttribute<bool>("IsIncludeHourlyStats");
      }
      set
      {
        this.SetAttribute("IsIncludeHourlyStats", value);
      }
    }

    public bool IsIncludeHourlyTrend
    {
      get
      {
        return this.GetAttribute<bool>("IsIncludeHourlyTrend");
      }
      set
      {
        this.SetAttribute("IsIncludeHourlyTrend", value);
      }
    }

    public bool IsIncludeDistribution
    {
      get
      {
        return this.GetAttribute<bool>("IsIncludeDistribution");
      }
      set
      {
        this.SetAttribute("IsIncludeDistribution", value);
      }
    }

    public bool IsIncludeDailyStats
    {
      get
      {
        return this.GetAttribute<bool>("IsIncludeDailyStats");
      }
      set
      {
        this.SetAttribute("IsIncludeDailyStats", value);
      }
    }

    public bool IsIncludeSuccessReport
    {
      get
      {
        return this.GetAttribute<bool>("IsIncludeSuccessReport");
      }
      set
      {
        this.SetAttribute("IsIncludeSuccessReport", value);
      }
    }

    public bool IsDailyStatsDisplayPieCharts
    {
      get
      {
        return this.GetAttribute<bool>("IsDailyStatsDisplayPieCharts");
      }
      set
      {
        this.SetAttribute("IsDailyStatsDisplayPieCharts", value);
      }
    }

    public bool IsDailyStatsDisplayEmptyDays
    {
      get
      {
        return this.GetAttribute<bool>("IsDailyStatsDisplayEmptyDays");
      }
      set
      {
        this.SetAttribute("IsDailyStatsDisplayEmptyDays", value);
      }
    }

    public bool IsIgnoreDisplayTimeAdjustments
    {
      get
      {
        return this.GetAttribute<bool>("IsIgnoreDisplayTimeAdjustments");
      }
      set
      {
        this.SetAttribute("IsIgnoreDisplayTimeAdjustments", value);
      }
    }

    public bool IsIgnoreDisplayTimeAdjustmentsForStrips
    {
      get
      {
        if (this.HasAttribute("IsIgnoreDisplayTimeAdjustmentsForStrips"))
          return this.GetAttribute<bool>("IsIgnoreDisplayTimeAdjustmentsForStrips");
        else
          return true;
      }
      set
      {
        this.SetAttribute("IsIgnoreDisplayTimeAdjustmentsForStrips", value);
      }
    }

    public int PatternReportViewSize
    {
      get
      {
        if (this.HasAttribute("PatternReportViewSize"))
          return this.GetAttribute<int>("PatternReportViewSize");
        else
          return 7;
      }
      set
      {
        this.SetAttribute("PatternReportViewSize", value);
      }
    }

    public int GlucoseTrendViewSize
    {
      get
      {
        return this.GetAttribute<int>("GlucoseTrendViewSize");
      }
      set
      {
        this.SetAttribute("GlucoseTrendViewSize", value);
      }
    }

    public int GlucoseTrendStripViewSize
    {
      get
      {
        if (this.HasAttribute("GlucoseTrendStripViewSize"))
          return this.GetAttribute<int>("GlucoseTrendStripViewSize");
        else
          return 7;
      }
      set
      {
        this.SetAttribute("GlucoseTrendStripViewSize", value);
      }
    }

    public int HourlyStatsViewSize
    {
      get
      {
        return this.GetAttribute<int>("HourlyStatsViewSize");
      }
      set
      {
        this.SetAttribute("HourlyStatsViewSize", value);
      }
    }

    public int HourlyTrendViewSize
    {
      get
      {
        return this.GetAttribute<int>("HourlyTrendViewSize");
      }
      set
      {
        this.SetAttribute("HourlyTrendViewSize", value);
      }
    }

    public int DistributionViewSize
    {
      get
      {
        return this.GetAttribute<int>("DistributionViewSize");
      }
      set
      {
        this.SetAttribute("DistributionViewSize", value);
      }
    }

    public int DailyStatsViewSize
    {
      get
      {
        return this.GetAttribute<int>("DailyStatsViewSize");
      }
      set
      {
        this.SetAttribute("DailyStatsViewSize", value);
      }
    }

    internal Program.SuccessChartPeriodOptions SuccessChartPeriodOption
    {
      get
      {
        return (Program.SuccessChartPeriodOptions) this.GetAttributeAsEnum("SuccessChartPeriodOption", typeof (Program.SuccessChartPeriodOptions));
      }
      set
      {
        this.SetAttribute("SuccessChartPeriodOption", ((object) value).ToString());
      }
    }

    private XObject HourlyStatsDaysOfTheWeekElement
    {
      get
      {
        return new XObject(this.Element.SelectSingleNode("HourlyStatsDaysOfTheWeek") as XmlElement);
      }
    }

    private XObject HourlyTrendDaysOfTheWeekElement
    {
      get
      {
        return new XObject(this.Element.SelectSingleNode("HourlyTrendDaysOfTheWeek") as XmlElement);
      }
    }

    private XObject DistributionDaysOfTheWeekElement
    {
      get
      {
        return new XObject(this.Element.SelectSingleNode("DistributionDaysOfTheWeek") as XmlElement);
      }
    }

    private XObject DailyStatsDaysOfTheWeekElement
    {
      get
      {
        return new XObject(this.Element.SelectSingleNode("DailyStatsDaysOfTheWeek") as XmlElement);
      }
    }

    public List<DaysOfWeek> HourlyStatsDaysOfTheWeek
    {
      get
      {
        return this.DoGetDaysOfWeekList(this.HourlyStatsDaysOfTheWeekElement);
      }
      set
      {
        this.DoSetDaysOfWeekList(this.HourlyStatsDaysOfTheWeekElement, value);
      }
    }

    public string HourlyStatsDaysOfTheWeekDescription
    {
      get
      {
        return this.DoGetDescriptionOfDaysOfTheWeek(this.DoGetDaysOfWeekList(this.HourlyStatsDaysOfTheWeekElement));
      }
    }

    public List<DaysOfWeek> HourlyTrendDaysOfTheWeek
    {
      get
      {
        return this.DoGetDaysOfWeekList(this.HourlyTrendDaysOfTheWeekElement);
      }
      set
      {
        this.DoSetDaysOfWeekList(this.HourlyTrendDaysOfTheWeekElement, value);
      }
    }

    public string HourlyTrendDaysOfTheWeekDescription
    {
      get
      {
        return this.DoGetDescriptionOfDaysOfTheWeek(this.DoGetDaysOfWeekList(this.HourlyTrendDaysOfTheWeekElement));
      }
    }

    public List<DaysOfWeek> DistributionDaysOfTheWeek
    {
      get
      {
        return this.DoGetDaysOfWeekList(this.DistributionDaysOfTheWeekElement);
      }
      set
      {
        this.DoSetDaysOfWeekList(this.DistributionDaysOfTheWeekElement, value);
      }
    }

    public string DistributionDaysOfTheWeekDescription
    {
      get
      {
        return this.DoGetDescriptionOfDaysOfTheWeek(this.DoGetDaysOfWeekList(this.DistributionDaysOfTheWeekElement));
      }
    }

    public List<DaysOfWeek> DailyStatsDaysOfTheWeek
    {
      get
      {
        return this.DoGetDaysOfWeekList(this.DailyStatsDaysOfTheWeekElement);
      }
      set
      {
        this.DoSetDaysOfWeekList(this.DailyStatsDaysOfTheWeekElement, value);
      }
    }

    public string DailyStatsDaysOfTheWeekDescription
    {
      get
      {
        return this.DoGetDescriptionOfDaysOfTheWeek(this.DoGetDaysOfWeekList(this.DailyStatsDaysOfTheWeekElement));
      }
    }

    public bool HourlyStatsDaysOfTheWeekIsMultipleSelect
    {
      get
      {
        return this.HourlyStatsDaysOfTheWeekElement.GetAttribute<bool>("IsMultipleSelect");
      }
      set
      {
        this.HourlyStatsDaysOfTheWeekElement.SetAttribute("IsMultipleSelect", value);
      }
    }

    public bool HourlyTrendDaysOfTheWeekIsMultipleSelect
    {
      get
      {
        return this.HourlyTrendDaysOfTheWeekElement.GetAttribute<bool>("IsMultipleSelect");
      }
      set
      {
        this.HourlyTrendDaysOfTheWeekElement.SetAttribute("IsMultipleSelect", value);
      }
    }

    public bool DistributionDaysOfTheWeekIsMultipleSelect
    {
      get
      {
        return this.DistributionDaysOfTheWeekElement.GetAttribute<bool>("IsMultipleSelect");
      }
      set
      {
        this.DistributionDaysOfTheWeekElement.SetAttribute("IsMultipleSelect", value);
      }
    }

    public bool DailyStatsDaysOfTheWeekIsMultipleSelect
    {
      get
      {
        return this.DailyStatsDaysOfTheWeekElement.GetAttribute<bool>("IsMultipleSelect");
      }
      set
      {
        this.DailyStatsDaysOfTheWeekElement.SetAttribute("IsMultipleSelect", value);
      }
    }

    public XQuickPrintConfiguration()
      : this(new XmlDocument())
    {
    }

    public XQuickPrintConfiguration(XmlDocument ownerDocument)
      : base("QuickPrintConfiguration", ownerDocument)
    {
      DateTimeOffset now = DateTimeOffset.Now;
      this.AppendChild(new XObject("HourlyStatsDaysOfTheWeek", ownerDocument));
      this.AppendChild(new XObject("HourlyTrendDaysOfTheWeek", ownerDocument));
      this.AppendChild(new XObject("DistributionDaysOfTheWeek", ownerDocument));
      this.AppendChild(new XObject("DailyStatsDaysOfTheWeek", ownerDocument));
      this.Id = Guid.NewGuid();
      this.ConfigurationNumber = 0;
      this.DateCreated = now;
      this.DateModified = now;
      this.Name = string.Empty;
      this.Description = string.Empty;
      this.IsIncludePatternReport = true;
      this.IsIncludeGlucoseTrend = true;
      this.IsIncludeGlucoseTrendStrip = true;
      this.IsIncludeHourlyStats = true;
      this.IsIncludeHourlyTrend = true;
      this.IsIncludeDistribution = true;
      this.IsIncludeDailyStats = true;
      this.IsIncludeSuccessReport = true;
      this.IsDailyStatsDisplayPieCharts = true;
      this.IsDailyStatsDisplayEmptyDays = true;
      this.IsIgnoreDisplayTimeAdjustments = true;
      this.IsIgnoreDisplayTimeAdjustmentsForStrips = true;
      this.PatternReportViewSize = 7;
      this.GlucoseTrendViewSize = 7;
      this.GlucoseTrendStripViewSize = 7;
      this.HourlyStatsViewSize = 7;
      this.HourlyTrendViewSize = 7;
      this.DistributionViewSize = 7;
      this.DailyStatsViewSize = 7;
      this.SuccessChartPeriodOption = Program.SuccessChartPeriodOptions.Weekly;
      this.HourlyStatsDaysOfTheWeekIsMultipleSelect = false;
      this.HourlyTrendDaysOfTheWeekIsMultipleSelect = false;
      this.DistributionDaysOfTheWeekIsMultipleSelect = false;
      this.DailyStatsDaysOfTheWeekIsMultipleSelect = false;
      this.HourlyStatsDaysOfTheWeek = new List<DaysOfWeek>();
      this.HourlyTrendDaysOfTheWeek = new List<DaysOfWeek>();
      this.DistributionDaysOfTheWeek = new List<DaysOfWeek>();
      this.DailyStatsDaysOfTheWeek = new List<DaysOfWeek>();
    }

    public XQuickPrintConfiguration(XmlElement element)
      : base(element)
    {
    }

    protected XQuickPrintConfiguration(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    private List<DaysOfWeek> DoGetDaysOfWeekList(XObject xContainer)
    {
      List<DaysOfWeek> list = new List<DaysOfWeek>();
      foreach (XmlElement element in xContainer.Element.SelectNodes("DaysOfWeek"))
      {
        DaysOfWeek daysOfWeek = (DaysOfWeek) new XObject(element).GetAttributeAsEnum("Value", typeof (DaysOfWeek));
        list.Add(daysOfWeek);
      }
      return list;
    }

    private void DoSetDaysOfWeekList(XObject xContainer, List<DaysOfWeek> list)
    {
      bool attribute = xContainer.GetAttribute<bool>("IsMultipleSelect");
      xContainer.Element.RemoveAll();
      xContainer.SetAttribute("IsMultipleSelect", attribute);
      foreach (DaysOfWeek daysOfWeek in list)
      {
        XObject xChildObject = new XObject("DaysOfWeek", this.Element.OwnerDocument);
        xChildObject.SetAttribute("Value", ((object) daysOfWeek).ToString());
        xContainer.AppendChild(xChildObject);
      }
    }

    private string DoGetDescriptionOfDaysOfTheWeek(List<DaysOfWeek> list)
    {
      return list.Count <= 0 ? Program.ResourceLookup(ResourceKeyTypes.Common_AllWeek) : string.Join(",", list.ConvertAll<string>((Converter<DaysOfWeek, string>) (item => Program.GetLocalizedEnumString((Enum) item))).ToArray());
    }

    [SecurityCritical]
    public new void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      base.GetObjectData(info, context);
    }
  }
}

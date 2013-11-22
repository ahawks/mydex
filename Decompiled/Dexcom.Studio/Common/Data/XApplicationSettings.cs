// Type: Dexcom.Common.Data.XApplicationSettings
// Assembly: Dexcom.Studio, Version=12.0.3.43, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: DEB7F911-78A1-4A44-A206-F03A1B17E3DE
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Studio.exe

using Dexcom.Common;
using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security;
using System.Xml;

namespace Dexcom.Common.Data
{
  [Serializable]
  public class XApplicationSettings : XObject, ISerializable
  {
    public const string Tag = "ApplicationSettings";
    public const string SettingsTag = "Settings";
    public const string GlucoseRangeFiltersTag = "GlucoseRangeFilters";
    public const string QuickPrintConfigurationsTag = "QuickPrintConfigurations";
    public const string DefaultTargetGlucoseRangeTag = "DefaultTargetGlucoseRange";
    public const string PredefinedTargetGlucoseRangesTag = "PredefinedTargetGlucoseRanges";
    public const string CurrentApplicationSettingsVersion = "1";

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

    public string Version
    {
      get
      {
        return this.GetAttribute<string>("Version");
      }
      set
      {
        this.SetAttribute("Version", value.Trim());
      }
    }

    public XObject Settings
    {
      get
      {
        return new XObject(this.Element.SelectSingleNode("Settings") as XmlElement);
      }
    }

    public string DefaultCultureName
    {
      get
      {
        return this.Settings.GetAttribute<string>("DefaultCulture");
      }
      set
      {
        this.Settings.SetAttribute("DefaultCulture", value.Trim());
      }
    }

    public CultureInfo DefaultCulture
    {
      get
      {
        return new CultureInfo(this.DefaultCultureName, true);
      }
      set
      {
        this.DefaultCultureName = value.Name;
      }
    }

    public bool IsDisplayMMolUnits
    {
      get
      {
        return this.Settings.GetAttribute<bool>("IsDisplayMMolUnits");
      }
      set
      {
        this.Settings.SetAttribute("IsDisplayMMolUnits", value);
      }
    }

    public string DefaultChartAfterDownload
    {
      get
      {
        if (this.Settings.HasAttribute("DefaultChartAfterDownload"))
          return this.Settings.GetAttribute<string>("DefaultChartAfterDownload");
        else
          return "GlucoseTrend";
      }
      set
      {
        this.Settings.SetAttribute("DefaultChartAfterDownload", value.Trim());
      }
    }

    public string ApplicationTabAppearance
    {
      get
      {
        if (this.Settings.HasAttribute("ApplicationTabAppearance"))
          return this.Settings.GetAttribute<string>("ApplicationTabAppearance");
        else
          return "ImageAndText";
      }
      set
      {
        this.Settings.SetAttribute("ApplicationTabAppearance", value.Trim());
      }
    }

    public bool IsPatientDataExportFormatXml
    {
      get
      {
        if (this.Settings.HasAttribute("IsPatientDataExportFormatXml"))
          return this.Settings.GetAttribute<bool>("IsPatientDataExportFormatXml");
        else
          return false;
      }
      set
      {
        this.Settings.SetAttribute("IsPatientDataExportFormatXml", value);
      }
    }

    public bool IsForceDisplayOfBlindedData
    {
      get
      {
        if (this.Settings.HasAttribute("IsForceDisplayOfBlindedData"))
          return this.Settings.GetAttribute<bool>("IsForceDisplayOfBlindedData");
        else
          return true;
      }
      set
      {
        this.Settings.SetAttribute("IsForceDisplayOfBlindedData", value);
      }
    }

    public bool IsAutoLoadPatientAtStartup
    {
      get
      {
        if (this.Settings.HasAttribute("IsAutoLoadPatientAtStartup"))
          return this.Settings.GetAttribute<bool>("IsAutoLoadPatientAtStartup");
        else
          return true;
      }
      set
      {
        this.Settings.SetAttribute("IsAutoLoadPatientAtStartup", value);
      }
    }

    public bool IsPromptForResetAfterDownload
    {
      get
      {
        if (this.Settings.HasAttribute("IsPromptForResetAfterDownload"))
          return this.Settings.GetAttribute<bool>("IsPromptForResetAfterDownload");
        else
          return false;
      }
      set
      {
        this.Settings.SetAttribute("IsPromptForResetAfterDownload", value);
      }
    }

    public bool IsDisplayWebPortal
    {
      get
      {
        if (this.Settings.HasAttribute("IsDisplayWebPortal"))
          return this.Settings.GetAttribute<bool>("IsDisplayWebPortal");
        else
          return true;
      }
      set
      {
        this.Settings.SetAttribute("IsDisplayWebPortal", value);
      }
    }

    public double ApplicationFontSize
    {
      get
      {
        return this.Settings.GetAttribute<double>("ApplicationFontSize");
      }
      set
      {
        this.Settings.SetAttribute("ApplicationFontSize", value);
      }
    }

    public XCollection<XGlucoseRange> GlucoseRangeFilters
    {
      get
      {
        return new XCollection<XGlucoseRange>(this.Element.SelectSingleNode("GlucoseRangeFilters") as XmlElement);
      }
      set
      {
        XmlElement xmlElement1 = this.Element.SelectSingleNode("GlucoseRangeFilters") as XmlElement;
        XmlElement xmlElement2 = this.Element.OwnerDocument.ImportNode((XmlNode) value.Element, true) as XmlElement;
        if (xmlElement1 == null)
          this.Element.AppendChild((XmlNode) xmlElement2);
        else
          this.Element.ReplaceChild((XmlNode) xmlElement2, (XmlNode) xmlElement1);
      }
    }

    public XCollection<XGlucoseRange> PredefinedTargetGlucoseRanges
    {
      get
      {
        return new XCollection<XGlucoseRange>(this.Element.SelectSingleNode("PredefinedTargetGlucoseRanges") as XmlElement);
      }
      set
      {
        XmlElement xmlElement1 = this.Element.SelectSingleNode("PredefinedTargetGlucoseRanges") as XmlElement;
        XmlElement xmlElement2 = this.Element.OwnerDocument.ImportNode((XmlNode) value.Element, true) as XmlElement;
        if (xmlElement1 == null)
          this.Element.AppendChild((XmlNode) xmlElement2);
        else
          this.Element.ReplaceChild((XmlNode) xmlElement2, (XmlNode) xmlElement1);
      }
    }

    public XCollection<XQuickPrintConfiguration> QuickPrintConfigurations
    {
      get
      {
        return new XCollection<XQuickPrintConfiguration>(this.Element.SelectSingleNode("QuickPrintConfigurations") as XmlElement);
      }
      set
      {
        XmlElement xmlElement1 = this.Element.SelectSingleNode("QuickPrintConfigurations") as XmlElement;
        XmlElement xmlElement2 = this.Element.OwnerDocument.ImportNode((XmlNode) value.Element, true) as XmlElement;
        if (xmlElement1 == null)
          this.Element.AppendChild((XmlNode) xmlElement2);
        else
          this.Element.ReplaceChild((XmlNode) xmlElement2, (XmlNode) xmlElement1);
      }
    }

    public XGlucoseRange DefaultTargetGlucoseRange
    {
      get
      {
        return new XGlucoseRange(this.Element.SelectSingleNode("DefaultTargetGlucoseRange/GlucoseRange") as XmlElement);
      }
      set
      {
        XmlElement xmlElement1 = this.Element.SelectSingleNode("DefaultTargetGlucoseRange") as XmlElement;
        XmlElement xmlElement2 = this.Element.SelectSingleNode("DefaultTargetGlucoseRange/GlucoseRange") as XmlElement;
        XmlElement xmlElement3 = this.Element.OwnerDocument.ImportNode((XmlNode) value.Element, true) as XmlElement;
        if (xmlElement1 == null)
          (this.Element.AppendChild((XmlNode) new XObject("DefaultTargetGlucoseRange", this.Element.OwnerDocument).Element) as XmlElement).AppendChild((XmlNode) xmlElement3);
        else if (xmlElement2 == null)
          xmlElement1.AppendChild((XmlNode) xmlElement3);
        else
          xmlElement1.ReplaceChild((XmlNode) xmlElement3, (XmlNode) xmlElement2);
      }
    }

    public XPatientDisplayOptions PatientDisplayOptions
    {
      get
      {
        return new XPatientDisplayOptions(this.Element.SelectSingleNode("PatientDisplayOptions") as XmlElement);
      }
      set
      {
        XmlElement xmlElement1 = this.Element.SelectSingleNode("PatientDisplayOptions") as XmlElement;
        XmlElement xmlElement2 = this.Element.OwnerDocument.ImportNode((XmlNode) value.Element, true) as XmlElement;
        if (xmlElement1 == null)
          this.Element.AppendChild((XmlNode) xmlElement2);
        else
          this.Element.ReplaceChild((XmlNode) xmlElement2, (XmlNode) xmlElement1);
      }
    }

    public XApplicationSettings()
      : this(new XmlDocument())
    {
    }

    public XApplicationSettings(XmlDocument ownerDocument)
      : base("ApplicationSettings", ownerDocument)
    {
      DateTimeOffset now = DateTimeOffset.Now;
      this.Version = "1";
      this.Id = Guid.NewGuid();
      this.Name = string.Empty;
      this.DateCreated = now;
      this.DateModified = now;
      this.AppendChild(new XObject("Settings", ownerDocument));
      this.AppendChild(new XObject("GlucoseRangeFilters", ownerDocument));
      this.AppendChild(new XObject("QuickPrintConfigurations", ownerDocument));
      this.AppendChild(new XObject("DefaultTargetGlucoseRange", ownerDocument));
      this.AppendChild(new XObject("PredefinedTargetGlucoseRanges", ownerDocument));
      this.AppendChild((XObject) new XPatientDisplayOptions(ownerDocument));
      this.DefaultCultureName = "en-US";
      this.IsDisplayMMolUnits = false;
      this.ApplicationFontSize = 12.0;
      this.ApplicationTabAppearance = "ImageAndText";
      this.IsPatientDataExportFormatXml = false;
      this.IsForceDisplayOfBlindedData = true;
      this.IsAutoLoadPatientAtStartup = true;
      this.IsPromptForResetAfterDownload = false;
      this.DefaultChartAfterDownload = "PatternReport";
      this.IsDisplayWebPortal = true;
    }

    public XApplicationSettings(XmlElement element)
      : base(element)
    {
    }

    protected XApplicationSettings(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    [SecurityCritical]
    public new void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      base.GetObjectData(info, context);
    }
  }
}

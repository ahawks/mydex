// Type: Dexcom.Common.Data.XPatientSettings
// Assembly: Dexcom.Common.Data, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 64B9BFC3-C891-4315-B216-574914861A80
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.Data.dll

using Dexcom.Common;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Security;
using System.Xml;
using System.Xml.Serialization;

namespace Dexcom.Common.Data
{
  [Serializable]
  public class XPatientSettings : XObject, ISerializable
  {
    public const string Tag = "PatientSettings";
    public const string TargetGlucoseRangeTag = "TargetGlucoseRange";
    public const string TimeOfTheDayRangesTag = "TimeOfTheDayRanges";

    [ColumnInfo(Visible = false)]
    [XmlAttribute]
    [TypeConverter(typeof (OnlineGuidConverter))]
    public new Guid Id
    {
      get
      {
        return this.GetAttributeAsGuid("Id");
      }
      set
      {
        this.SetAttribute("Id", value);
      }
    }

    [XmlAttribute]
    [ColumnInfo(Visible = false)]
    public new string Name
    {
      get
      {
        return this.GetAttribute("Name");
      }
      set
      {
        this.SetAttribute("Name", value.Trim());
      }
    }

    [XmlAttribute]
    [ColumnInfo(Visible = false)]
    public new string Description
    {
      get
      {
        return this.GetAttribute("Description");
      }
      set
      {
        this.SetAttribute("Description", value.Trim());
      }
    }

    [ColumnInfo(Visible = false)]
    [XmlAttribute]
    public DateTimeOffset DateCreated
    {
      get
      {
        return this.GetAttributeAsDateTimeOffset("DateCreated");
      }
      set
      {
        this.SetAttribute("DateCreated", value);
      }
    }

    [ColumnInfo(Visible = false)]
    [XmlAttribute]
    public DateTimeOffset DateModified
    {
      get
      {
        return this.GetAttributeAsDateTimeOffset("DateModified");
      }
      set
      {
        this.SetAttribute("DateModified", value);
      }
    }

    public XGlucoseRange TargetGlucoseRange
    {
      get
      {
        return new XGlucoseRange(this.Element.SelectSingleNode("TargetGlucoseRange/GlucoseRange") as XmlElement);
      }
      set
      {
        XmlElement xmlElement1 = this.Element.SelectSingleNode("TargetGlucoseRange") as XmlElement;
        XmlElement xmlElement2 = this.Element.SelectSingleNode("TargetGlucoseRange/GlucoseRange") as XmlElement;
        XmlElement xmlElement3 = this.Element.OwnerDocument.ImportNode((XmlNode) value.Element, true) as XmlElement;
        if (xmlElement1 == null)
          (this.Element.AppendChild((XmlNode) new XObject("TargetGlucoseRange", this.Element.OwnerDocument).Element) as XmlElement).AppendChild((XmlNode) xmlElement3);
        else if (xmlElement2 == null)
          xmlElement1.AppendChild((XmlNode) xmlElement3);
        else
          xmlElement1.ReplaceChild((XmlNode) xmlElement3, (XmlNode) xmlElement2);
      }
    }

    public XCollection<XTimeOfDayRange> TimeOfTheDayRanges
    {
      get
      {
        return new XCollection<XTimeOfDayRange>(this.Element.SelectSingleNode("TimeOfTheDayRanges") as XmlElement);
      }
      set
      {
        if (value.Element == null || !(value.Element.Name == "TimeOfTheDayRanges"))
          return;
        XmlElement xmlElement1 = this.Element.SelectSingleNode("TimeOfTheDayRanges") as XmlElement;
        XmlElement xmlElement2 = this.Element.OwnerDocument.ImportNode((XmlNode) value.Element, true) as XmlElement;
        if (xmlElement1 == null)
          this.Element.AppendChild((XmlNode) xmlElement2);
        else
          this.Element.ReplaceChild((XmlNode) xmlElement2, (XmlNode) xmlElement1);
      }
    }

    public XPatientSettings()
      : this(new XmlDocument())
    {
    }

    public XPatientSettings(XmlDocument ownerDocument)
      : base("PatientSettings", ownerDocument)
    {
      DateTimeOffset now = DateTimeOffset.Now;
      this.Id = Guid.NewGuid();
      this.Name = string.Empty;
      this.DateCreated = now;
      this.DateModified = now;
      this.Element.AppendChild((XmlNode) new XObject("TargetGlucoseRange", ownerDocument).Element);
      this.Element.AppendChild((XmlNode) new XCollection<XTimeOfDayRange>("TimeOfTheDayRanges", ownerDocument).Element);
    }

    public XPatientSettings(XmlElement element)
      : base(element)
    {
    }

    protected XPatientSettings(SerializationInfo info, StreamingContext context)
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

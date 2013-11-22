// Type: Dexcom.Common.Data.XPatientInformation
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
  public class XPatientInformation : XObject, ISerializable
  {
    public const string Tag = "PatientInformation";

    [XmlAttribute]
    [TypeConverter(typeof (OnlineGuidConverter))]
    [ColumnInfo(Visible = false)]
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

    [ColumnInfo(DefaultWidth = 50, DisplayKey = "PatientInfo_FirstName", DisplayName = "First Name", Ordinal = 0, Visible = true)]
    [XmlAttribute]
    public string FirstName
    {
      get
      {
        return this.GetAttribute("FirstName");
      }
      set
      {
        this.SetAttribute("FirstName", value.Trim());
      }
    }

    [XmlAttribute]
    [ColumnInfo(DefaultWidth = 50, DisplayKey = "PatientInfo_MiddleName", DisplayName = "Middle Name", Ordinal = 1, Visible = true)]
    public string MiddleName
    {
      get
      {
        return this.GetAttribute("MiddleName");
      }
      set
      {
        this.SetAttribute("MiddleName", value.Trim());
      }
    }

    [XmlAttribute]
    [ColumnInfo(DefaultWidth = 50, DisplayKey = "PatientInfo_LastName", DisplayName = "Last Name", Ordinal = 2, Visible = true)]
    public string LastName
    {
      get
      {
        return this.GetAttribute("LastName");
      }
      set
      {
        this.SetAttribute("LastName", value.Trim());
      }
    }

    [ColumnInfo(DefaultWidth = 50, DisplayKey = "PatientInfo_Initials", DisplayName = "Initials", Ordinal = 3, Visible = true)]
    [XmlAttribute]
    public string Initials
    {
      get
      {
        return this.GetAttribute("Initials");
      }
      set
      {
        this.SetAttribute("Initials", value.Trim());
      }
    }

    [XmlAttribute]
    [ColumnInfo(DefaultWidth = 50, DisplayKey = "PatientInfo_SerialNumber", DisplayName = "Serial Number", Ordinal = 4, Visible = true)]
    public string SerialNumber
    {
      get
      {
        return this.GetAttribute("SerialNumber");
      }
      set
      {
        this.SetAttribute("SerialNumber", value.Trim());
      }
    }

    [XmlAttribute]
    [ColumnInfo(DefaultWidth = 50, DisplayKey = "PatientInfo_PatientNumber", DisplayName = "Patient Number", Ordinal = 10, Visible = true)]
    public int PatientNumber
    {
      get
      {
        return this.GetAttributeAsInt("PatientNumber");
      }
      set
      {
        this.SetAttribute("PatientNumber", value);
      }
    }

    [ColumnInfo(DefaultWidth = 50, DisplayKey = "PatientInfo_PatientIdentifier", DisplayName = "Patient Identifier", Ordinal = 11, Visible = true)]
    [XmlAttribute]
    public string PatientIdentifier
    {
      get
      {
        return this.GetAttribute("PatientIdentifier");
      }
      set
      {
        this.SetAttribute("PatientIdentifier", value.Trim());
      }
    }

    [ColumnInfo(DefaultWidth = 50, DisplayKey = "PatientInfo_OtherIdentifier", DisplayName = "Other Identifier", Ordinal = 12, Visible = true)]
    [XmlAttribute]
    public string OtherIdentifier
    {
      get
      {
        return this.GetAttribute("OtherIdentifier");
      }
      set
      {
        this.SetAttribute("OtherIdentifier", value.Trim());
      }
    }

    [XmlAttribute]
    [ColumnInfo(DefaultWidth = 50, DisplayKey = "PatientInfo_DisplayName", DisplayName = "Preferred Name", Ordinal = 13, Visible = true)]
    public string DisplayName
    {
      get
      {
        return this.GetAttribute("DisplayName");
      }
      set
      {
        this.SetAttribute("DisplayName", value.Trim());
      }
    }

    [ColumnInfo(DefaultWidth = 50, Ordinal = 14, Visible = false)]
    [XmlAttribute]
    public GenderType Gender
    {
      get
      {
        GenderType genderType = GenderType.Unknown;
        if (this.HasAttribute("Gender") && !this.GetAttribute("Gender").Equals(string.Empty))
          genderType = (GenderType) this.GetAttributeAsEnum("Gender", typeof (GenderType));
        return genderType;
      }
      set
      {
        this.SetAttribute("Gender", ((object) value).ToString());
      }
    }

    [ColumnInfo(DefaultWidth = 50, DisplayKey = "PatientInfo_Gender", DisplayName = "Gender", Ordinal = 15, Visible = true)]
    [XmlAttribute]
    public string GenderString
    {
      get
      {
        return ((object) this.Gender).ToString();
      }
    }

    [XmlAttribute]
    [ColumnInfo(DefaultWidth = 50, DisplayKey = "PatientInfo_DateOfBirth", DisplayName = "Date Of Birth", Ordinal = 16, Visible = true)]
    public DateTime DateOfBirth
    {
      get
      {
        return this.GetAttributeAsDateTime("DateOfBirth");
      }
      set
      {
        this.SetAttribute("DateOfBirth", value);
      }
    }

    [ColumnInfo(DefaultWidth = 50, DisplayKey = "PatientInfo_DoctorsName", DisplayName = "Doctor Name", Ordinal = 20, Visible = true)]
    [XmlAttribute]
    public string DoctorsName
    {
      get
      {
        return this.GetAttribute("DoctorsName");
      }
      set
      {
        this.SetAttribute("DoctorsName", value.Trim());
      }
    }

    [ColumnInfo(DefaultWidth = 50, DisplayKey = "PatientInfo_EMailAddress", DisplayName = "E-Mail Address", Ordinal = 21, Visible = true)]
    [XmlAttribute]
    public string EMail
    {
      get
      {
        return this.GetAttribute("EMail");
      }
      set
      {
        this.SetAttribute("EMail", value.Trim());
      }
    }

    [ColumnInfo(DefaultWidth = 50, DisplayKey = "PatientInfo_PhoneNumber", DisplayName = "Phone Number", Ordinal = 22, Visible = true)]
    [XmlAttribute]
    public string PhoneNumber
    {
      get
      {
        return this.GetAttribute("PhoneNumber");
      }
      set
      {
        this.SetAttribute("PhoneNumber", value.Trim());
      }
    }

    [ColumnInfo(DefaultWidth = 50, DisplayKey = "PatientInfo_PhoneExtension", DisplayName = "Phone Extension", Ordinal = 23, Visible = true)]
    [XmlAttribute]
    public string PhoneExtension
    {
      get
      {
        return this.GetAttribute("PhoneExtension");
      }
      set
      {
        this.SetAttribute("PhoneExtension", value.Trim());
      }
    }

    [ColumnInfo(DefaultWidth = 50, DisplayKey = "PatientInfo_SiteIdentifier", DisplayName = "Site Identifier", Ordinal = 24, Visible = true)]
    [XmlAttribute]
    public string SiteIdentifier
    {
      get
      {
        if (this.HasAttribute("SiteIdentifier"))
          return this.GetAttribute("SiteIdentifier");
        else
          return string.Empty;
      }
      set
      {
        this.SetAttribute("SiteIdentifier", value.Trim());
      }
    }

    [ColumnInfo(DefaultWidth = 50, DisplayKey = "PatientInfo_StudyIdentifier", DisplayName = "Study Identifier", Ordinal = 25, Visible = true)]
    [XmlAttribute]
    public string StudyIdentifier
    {
      get
      {
        if (this.HasAttribute("StudyIdentifier"))
          return this.GetAttribute("StudyIdentifier");
        else
          return string.Empty;
      }
      set
      {
        this.SetAttribute("StudyIdentifier", value.Trim());
      }
    }

    [ColumnInfo(DefaultWidth = 50, Ordinal = 26, Visible = false)]
    [XmlAttribute]
    public DateTimeOffset DateCreated
    {
      get
      {
        return (DateTimeOffset) this.GetAttributeAsDateTime("DateCreated");
      }
      set
      {
        this.SetAttribute("DateCreated", value);
      }
    }

    [ColumnInfo(DefaultWidth = 50, Ordinal = 27, Visible = false)]
    [XmlAttribute]
    public DateTimeOffset DateModified
    {
      get
      {
        return (DateTimeOffset) this.GetAttributeAsDateTime("DateModified");
      }
      set
      {
        this.SetAttribute("DateModified", value);
      }
    }

    [ColumnInfo(DefaultWidth = 50, DisplayKey = "PatientInfo_Comments", Ordinal = 42, Visible = true)]
    [XmlAttribute]
    public string Comments
    {
      get
      {
        return this.GetAttribute("Comments");
      }
      set
      {
        this.SetAttribute("Comments", value.Trim());
      }
    }

    public XPatientInformation()
      : this(new XmlDocument())
    {
    }

    public XPatientInformation(XmlDocument ownerDocument)
      : base("PatientInformation", ownerDocument)
    {
      DateTimeOffset now = DateTimeOffset.Now;
      this.Id = Guid.NewGuid();
      this.Name = string.Empty;
      this.FirstName = string.Empty;
      this.LastName = string.Empty;
      this.MiddleName = string.Empty;
      this.Initials = string.Empty;
      this.SerialNumber = string.Empty;
      this.DisplayName = string.Empty;
      this.PatientNumber = 0;
      this.PatientIdentifier = string.Empty;
      this.OtherIdentifier = string.Empty;
      this.Gender = GenderType.Unknown;
      this.DateOfBirth = CommonValues.EmptyDateTime;
      this.DoctorsName = string.Empty;
      this.Comments = string.Empty;
      this.EMail = string.Empty;
      this.PhoneNumber = string.Empty;
      this.PhoneExtension = string.Empty;
      this.SiteIdentifier = string.Empty;
      this.StudyIdentifier = string.Empty;
      this.DateCreated = now;
      this.DateModified = now;
    }

    public XPatientInformation(XmlElement element)
      : base(element)
    {
    }

    protected XPatientInformation(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    public void Deidentify()
    {
      this.Name = string.Empty;
      this.FirstName = string.Empty;
      this.LastName = string.Empty;
      this.MiddleName = string.Empty;
      this.Initials = string.Empty;
      this.DisplayName = string.Empty;
      this.PatientNumber = 0;
      this.PatientIdentifier = string.Empty;
      this.OtherIdentifier = string.Empty;
      this.Gender = GenderType.Unknown;
      this.DateOfBirth = CommonValues.EmptyDateTime;
      this.DoctorsName = string.Empty;
      this.Comments = string.Empty;
      this.EMail = string.Empty;
      this.PhoneNumber = string.Empty;
      this.PhoneExtension = string.Empty;
      this.SiteIdentifier = string.Empty;
      this.StudyIdentifier = string.Empty;
    }

    [SecurityCritical]
    public new void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      base.GetObjectData(info, context);
    }
  }
}

// Type: Dexcom.Common.Data.XPatientDisplayOptions
// Assembly: Dexcom.Studio, Version=12.0.3.43, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: DEB7F911-78A1-4A44-A206-F03A1B17E3DE
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Studio.exe

using Dexcom.Common;
using System;
using System.Runtime.Serialization;
using System.Security;
using System.Xml;

namespace Dexcom.Common.Data
{
  [Serializable]
  public class XPatientDisplayOptions : XObject, ISerializable
  {
    public const string Tag = "PatientDisplayOptions";

    public string PatientDisplayField1
    {
      get
      {
        return this.GetAttribute<string>("PatientDisplayField1");
      }
      set
      {
        this.SetAttribute("PatientDisplayField1", value.Trim());
      }
    }

    public string PatientDisplaySeparator1
    {
      get
      {
        return this.GetAttribute<string>("PatientDisplaySeparator1");
      }
      set
      {
        this.SetAttribute("PatientDisplaySeparator1", value);
      }
    }

    public string PatientDisplayField2
    {
      get
      {
        return this.GetAttribute<string>("PatientDisplayField2");
      }
      set
      {
        this.SetAttribute("PatientDisplayField2", value.Trim());
      }
    }

    public string PatientDisplaySeparator2
    {
      get
      {
        return this.GetAttribute<string>("PatientDisplaySeparator2");
      }
      set
      {
        this.SetAttribute("PatientDisplaySeparator2", value);
      }
    }

    public string PatientDisplayField3
    {
      get
      {
        return this.GetAttribute<string>("PatientDisplayField3");
      }
      set
      {
        this.SetAttribute("PatientDisplayField3", value.Trim());
      }
    }

    public string PatientDisplaySeparator3
    {
      get
      {
        return this.GetAttribute<string>("PatientDisplaySeparator3");
      }
      set
      {
        this.SetAttribute("PatientDisplaySeparator3", value);
      }
    }

    public string PatientDisplayField4
    {
      get
      {
        return this.GetAttribute<string>("PatientDisplayField4");
      }
      set
      {
        this.SetAttribute("PatientDisplayField4", value.Trim());
      }
    }

    public string PatientDisplaySeparator4
    {
      get
      {
        return this.GetAttribute<string>("PatientDisplaySeparator4");
      }
      set
      {
        this.SetAttribute("PatientDisplaySeparator4", value);
      }
    }

    public string PatientDisplayField5
    {
      get
      {
        return this.GetAttribute<string>("PatientDisplayField5");
      }
      set
      {
        this.SetAttribute("PatientDisplayField5", value.Trim());
      }
    }

    public XPatientDisplayOptions()
      : this(new XmlDocument())
    {
    }

    public XPatientDisplayOptions(XmlDocument ownerDocument)
      : base("PatientDisplayOptions", ownerDocument)
    {
      this.PatientDisplayField1 = "LastName";
      this.PatientDisplaySeparator1 = ", ";
      this.PatientDisplayField2 = "FirstName";
      this.PatientDisplaySeparator2 = " [";
      this.PatientDisplayField3 = "SerialNumber";
      this.PatientDisplaySeparator3 = "]";
      this.PatientDisplayField4 = string.Empty;
      this.PatientDisplaySeparator4 = string.Empty;
      this.PatientDisplayField5 = string.Empty;
    }

    public XPatientDisplayOptions(XmlElement element)
      : base(element)
    {
    }

    protected XPatientDisplayOptions(SerializationInfo info, StreamingContext context)
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

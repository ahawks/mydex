// Type: Dexcom.Common.Data.XTimeOfDayRange
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
  public class XTimeOfDayRange : XObject, ISerializable
  {
    public const string Tag = "TimeOfDayRange";

    [TypeConverter(typeof (OnlineGuidConverter))]
    [XmlAttribute]
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

    public MinRangeOperator MinOperator
    {
      get
      {
        return (MinRangeOperator) this.GetAttributeAsEnum("MinOperator", typeof (MinRangeOperator));
      }
      set
      {
        this.SetAttribute("MinOperator", ((object) value).ToString());
      }
    }

    public MaxRangeOperator MaxOperator
    {
      get
      {
        return (MaxRangeOperator) this.GetAttributeAsEnum("MaxOperator", typeof (MaxRangeOperator));
      }
      set
      {
        this.SetAttribute("MaxOperator", ((object) value).ToString());
      }
    }

    public TimeSpan MinTime
    {
      get
      {
        return this.GetAttribute<TimeSpan>("MinTime");
      }
      set
      {
        this.SetAttribute("MinTime", value);
      }
    }

    public TimeSpan MaxTime
    {
      get
      {
        return this.GetAttribute<TimeSpan>("MaxTime");
      }
      set
      {
        this.SetAttribute("MaxTime", value);
      }
    }

    public XTimeOfDayRange()
      : this(new XmlDocument())
    {
    }

    public XTimeOfDayRange(XmlDocument ownerDocument)
      : base("TimeOfDayRange", ownerDocument)
    {
      this.Id = Guid.NewGuid();
      this.Name = string.Empty;
      this.MinTime = TimeSpan.MaxValue;
      this.MaxTime = TimeSpan.MaxValue;
      this.MinOperator = MinRangeOperator.GreaterThanOrEqual;
      this.MaxOperator = MaxRangeOperator.LessThan;
    }

    public XTimeOfDayRange(XmlElement element)
      : base(element)
    {
    }

    protected XTimeOfDayRange(SerializationInfo info, StreamingContext context)
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

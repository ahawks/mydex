// Type: Dexcom.Common.Data.XA1cData
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
  public class XA1cData : XObject, ISerializable
  {
    public const string Tag = "A1cData";

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

    [ColumnInfo(Visible = false)]
    [XmlAttribute]
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

    [XmlAttribute]
    [ColumnInfo(Visible = false)]
    public double Value
    {
      get
      {
        return this.GetAttribute<double>("Value");
      }
      set
      {
        this.SetAttribute("Value", value);
      }
    }

    [XmlAttribute]
    [ColumnInfo(Visible = false)]
    public DateTimeOffset TimeStamp
    {
      get
      {
        return this.GetAttribute<DateTimeOffset>("TimeStamp");
      }
      set
      {
        this.SetAttribute("TimeStamp", value);
      }
    }

    public XA1cData()
      : this(new XmlDocument())
    {
    }

    public XA1cData(XmlDocument ownerDocument)
      : base("A1cData", ownerDocument)
    {
      this.Id = Guid.NewGuid();
      this.Value = 0.0;
      this.TimeStamp = CommonValues.EmptyDateTimeOffset;
    }

    public XA1cData(XmlElement element)
      : base(element)
    {
    }

    protected XA1cData(SerializationInfo info, StreamingContext context)
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

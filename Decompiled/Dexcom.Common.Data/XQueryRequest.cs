// Type: Dexcom.Common.Data.XQueryRequest
// Assembly: Dexcom.Common.Data, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 64B9BFC3-C891-4315-B216-574914861A80
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.Data.dll

using Dexcom.Common;
using System;
using System.Runtime.Serialization;
using System.Security;
using System.Xml;
using System.Xml.Serialization;

namespace Dexcom.Common.Data
{
  [Serializable]
  public class XQueryRequest : XObject, ISerializable
  {
    public const string Tag = "QueryRequest";

    [XmlAttribute]
    public int Start
    {
      get
      {
        return this.GetAttributeAsInt("Start");
      }
      set
      {
        this.SetAttribute("Start", value);
      }
    }

    [XmlAttribute]
    public int Count
    {
      get
      {
        return this.GetAttributeAsInt("Count");
      }
      set
      {
        this.SetAttribute("Count", value);
      }
    }

    [XmlAttribute]
    public bool Distinct
    {
      get
      {
        return this.GetAttributeAsBool("Distinct");
      }
      set
      {
        this.SetAttribute("Distinct", value);
      }
    }

    [XmlAttribute]
    public string OrderBy
    {
      get
      {
        return this.GetAttribute("OrderBy");
      }
      set
      {
        this.SetAttribute("OrderBy", value);
      }
    }

    [XmlAttribute]
    public string Condition
    {
      get
      {
        return this.GetAttribute("Condition");
      }
      set
      {
        this.SetAttribute("Condition", value);
      }
    }

    [XmlAttribute]
    public bool ColumnInfo
    {
      get
      {
        return this.GetAttributeAsBool("ColumnInfo");
      }
      set
      {
        this.SetAttribute("ColumnInfo", value);
      }
    }

    public XQueryRequest()
      : this(new XmlDocument())
    {
    }

    public XQueryRequest(XmlDocument ownerDocument)
      : base("QueryRequest", ownerDocument)
    {
      this.Start = 1;
      this.Count = 0;
      this.Distinct = false;
      this.OrderBy = string.Empty;
      this.Condition = string.Empty;
      this.ColumnInfo = true;
    }

    public XQueryRequest(XmlElement element)
      : base(element)
    {
    }

    protected XQueryRequest(SerializationInfo info, StreamingContext context)
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

// Type: Dexcom.Common.Data.XQueryResult
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
  public class XQueryResult : XObject, ISerializable
  {
    public const string Tag = "QueryResult";

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
    public int NextStart
    {
      get
      {
        return this.GetAttributeAsInt("NextStart");
      }
      set
      {
        this.SetAttribute("NextStart", value);
      }
    }

    [XmlAttribute]
    public int PreviousStart
    {
      get
      {
        return this.GetAttributeAsInt("PreviousStart");
      }
      set
      {
        this.SetAttribute("PreviousStart", value);
      }
    }

    public XQueryResult()
      : this(new XmlDocument())
    {
    }

    public XQueryResult(XmlDocument ownerDocument)
      : base("QueryResult", ownerDocument)
    {
      this.Count = 0;
      this.NextStart = 0;
      this.PreviousStart = 0;
    }

    public XQueryResult(XmlElement element)
      : base(element)
    {
    }

    protected XQueryResult(SerializationInfo info, StreamingContext context)
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

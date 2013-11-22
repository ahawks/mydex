// Type: Dexcom.Common.Data.XSessionList
// Assembly: Dexcom.Common.Data, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 64B9BFC3-C891-4315-B216-574914861A80
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.Data.dll

using Dexcom.Common;
using System;
using System.Runtime.Serialization;
using System.Security;
using System.Xml;

namespace Dexcom.Common.Data
{
  [Serializable]
  public class XSessionList : XObject, ISerializable
  {
    public const string Tag = "SessionList";

    public XSessionList()
      : this(new XmlDocument())
    {
    }

    public XSessionList(XmlDocument ownerDocument)
      : base("SessionList", ownerDocument)
    {
    }

    public XSessionList(XmlElement element)
      : base(element)
    {
    }

    protected XSessionList(SerializationInfo info, StreamingContext context)
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

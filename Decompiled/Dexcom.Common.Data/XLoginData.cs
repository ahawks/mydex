﻿// Type: Dexcom.Common.Data.XLoginData
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
  public class XLoginData : XObject, ISerializable
  {
    public const string Tag = "LoginData";

    public XLoginData()
      : this(new XmlDocument())
    {
    }

    public XLoginData(XmlDocument ownerDocument)
      : base("LoginData", ownerDocument)
    {
    }

    public XLoginData(XmlElement element)
      : base(element)
    {
    }

    protected XLoginData(SerializationInfo info, StreamingContext context)
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

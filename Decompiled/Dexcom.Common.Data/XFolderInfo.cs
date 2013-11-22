// Type: Dexcom.Common.Data.XFolderInfo
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
  public class XFolderInfo : XObject, ISerializable
  {
    public const string Tag = "FolderInfo";

    [XmlAttribute]
    public string FolderPath
    {
      get
      {
        return this.GetAttribute("FolderPath");
      }
      set
      {
        this.SetAttribute("FolderPath", value.Trim());
      }
    }

    public XFolderInfo()
      : this(new XmlDocument())
    {
    }

    public XFolderInfo(XmlDocument ownerDocument)
      : base("FolderInfo", ownerDocument)
    {
      this.Name = string.Empty;
      this.FolderPath = string.Empty;
    }

    public XFolderInfo(XmlElement element)
      : base(element)
    {
    }

    protected XFolderInfo(SerializationInfo info, StreamingContext context)
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

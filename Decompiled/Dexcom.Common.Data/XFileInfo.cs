// Type: Dexcom.Common.Data.XFileInfo
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
  public class XFileInfo : XObject, ISerializable
  {
    public const string Tag = "FileInfo";

    [XmlAttribute]
    public string FilePath
    {
      get
      {
        return this.GetAttribute("FilePath");
      }
      set
      {
        this.SetAttribute("FilePath", value.Trim());
      }
    }

    [XmlAttribute]
    public string Basename
    {
      get
      {
        return this.GetAttribute("Basename");
      }
      set
      {
        this.SetAttribute("Basename", value.Trim());
      }
    }

    [XmlAttribute]
    public string Extension
    {
      get
      {
        return this.GetAttribute("Extension");
      }
      set
      {
        this.SetAttribute("Extension", value.Trim());
      }
    }

    [XmlAttribute]
    public long Length
    {
      get
      {
        return this.GetAttributeAsLong("Length");
      }
      set
      {
        this.SetAttribute("Length", value);
      }
    }

    [XmlAttribute]
    public DateTime DateCreated
    {
      get
      {
        return this.GetAttributeAsDateTime("DateCreated");
      }
      set
      {
        this.SetAttribute("DateCreated", value);
      }
    }

    [XmlAttribute]
    public DateTime DateModified
    {
      get
      {
        return this.GetAttributeAsDateTime("DateModified");
      }
      set
      {
        this.SetAttribute("DateModified", value);
      }
    }

    public XFileInfo()
      : this(new XmlDocument())
    {
    }

    public XFileInfo(XmlDocument ownerDocument)
      : base("FileInfo", ownerDocument)
    {
      this.Name = string.Empty;
      this.Basename = string.Empty;
      this.Extension = string.Empty;
      this.Length = 0L;
      this.DateCreated = CommonValues.EmptyDateTime;
      this.DateModified = CommonValues.EmptyDateTime;
      this.FilePath = string.Empty;
    }

    public XFileInfo(XmlElement element)
      : base(element)
    {
    }

    protected XFileInfo(SerializationInfo info, StreamingContext context)
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

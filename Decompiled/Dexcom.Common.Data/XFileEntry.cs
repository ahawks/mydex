// Type: Dexcom.Common.Data.XFileEntry
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
  public class XFileEntry : XObject, ISerializable
  {
    public const string Tag = "FileEntry";

    [XmlAttribute]
    public Guid FileId
    {
      get
      {
        return this.GetAttributeAsGuid("FileId");
      }
      set
      {
        this.SetAttribute("FileId", value);
      }
    }

    [XmlAttribute]
    public Guid CacheId
    {
      get
      {
        return this.GetAttributeAsGuid("CacheId");
      }
      set
      {
        this.SetAttribute("CacheId", value);
      }
    }

    [XmlAttribute]
    public Guid VolumeId
    {
      get
      {
        return this.GetAttributeAsGuid("VolumeId");
      }
      set
      {
        this.SetAttribute("VolumeId", value);
      }
    }

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

    [XmlAttribute]
    public int Status
    {
      get
      {
        return this.GetAttributeAsInt("Status");
      }
      set
      {
        this.SetAttribute("Status", value);
      }
    }

    public XFileEntry()
      : this(new XmlDocument())
    {
    }

    public XFileEntry(XmlDocument ownerDocument)
      : base("FileEntry", ownerDocument)
    {
      this.Name = string.Empty;
      this.Basename = string.Empty;
      this.Extension = string.Empty;
      this.Description = string.Empty;
      this.Length = 0L;
      this.DateCreated = CommonValues.EmptyDateTime;
      this.DateModified = CommonValues.EmptyDateTime;
      this.FileId = Guid.Empty;
      this.CacheId = Guid.Empty;
      this.VolumeId = Guid.Empty;
      this.FilePath = string.Empty;
      this.Status = 0;
    }

    public XFileEntry(XmlElement element)
      : base(element)
    {
    }

    protected XFileEntry(SerializationInfo info, StreamingContext context)
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

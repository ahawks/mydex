// Type: Dexcom.Common.Data.XFolderScan
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
  public class XFolderScan : XObject, ISerializable
  {
    public const string Tag = "FolderScan";

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

    [XmlAttribute]
    public bool IncludeSubFolders
    {
      get
      {
        return this.GetAttributeAsBool("IncludeSubFolders");
      }
      set
      {
        this.SetAttribute("IncludeSubFolders", value);
      }
    }

    [XmlAttribute]
    public bool IncludeFiles
    {
      get
      {
        return this.GetAttributeAsBool("IncludeFiles");
      }
      set
      {
        this.SetAttribute("IncludeFiles", value);
      }
    }

    [XmlAttribute]
    public string FilePattern
    {
      get
      {
        return this.GetAttribute("FilePattern");
      }
      set
      {
        this.SetAttribute("FilePattern", value.Trim());
      }
    }

    public XFolderScan()
      : this(new XmlDocument())
    {
    }

    public XFolderScan(XmlDocument ownerDocument)
      : base("FolderScan", ownerDocument)
    {
      this.FolderPath = string.Empty;
      this.IncludeSubFolders = false;
      this.IncludeFiles = false;
      this.FilePattern = string.Empty;
    }

    public XFolderScan(XmlElement element)
      : base(element)
    {
    }

    protected XFolderScan(SerializationInfo info, StreamingContext context)
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

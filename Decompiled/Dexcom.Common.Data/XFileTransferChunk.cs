// Type: Dexcom.Common.Data.XFileTransferChunk
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
  public class XFileTransferChunk : XObject, ISerializable
  {
    public const string Tag = "FileTransferChunk";

    [XmlAttribute]
    public int Size
    {
      get
      {
        return this.GetAttributeAsInt("Size");
      }
      set
      {
        this.SetAttribute("Size", value);
      }
    }

    [XmlAttribute]
    public int CompressedSize
    {
      get
      {
        return this.GetAttributeAsInt("CompressedSize");
      }
      set
      {
        this.SetAttribute("CompressedSize", value);
      }
    }

    [XmlAttribute]
    public bool IsEncrypted
    {
      get
      {
        return this.GetAttributeAsBool("IsEncrypted");
      }
      set
      {
        this.SetAttribute("IsEncrypted", value);
      }
    }

    [XmlAttribute]
    public bool IsCompressed
    {
      get
      {
        return this.GetAttributeAsBool("IsCompressed");
      }
      set
      {
        this.SetAttribute("IsCompressed", value);
      }
    }

    [XmlAttribute]
    public bool HasMoreData
    {
      get
      {
        return this.GetAttributeAsBool("HasMoreData");
      }
      set
      {
        this.SetAttribute("HasMoreData", value);
      }
    }

    public XFileTransferChunk()
      : this(new XmlDocument())
    {
    }

    public XFileTransferChunk(XmlDocument ownerDocument)
      : base("FileTransferChunk", ownerDocument)
    {
      this.Size = 0;
      this.CompressedSize = 0;
      this.IsEncrypted = false;
      this.IsCompressed = false;
      this.HasMoreData = false;
    }

    public XFileTransferChunk(XmlElement element)
      : base(element)
    {
    }

    protected XFileTransferChunk(SerializationInfo info, StreamingContext context)
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

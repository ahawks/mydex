// Type: Dexcom.Common.Data.XFileTransfer
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
  public class XFileTransfer : XObject, ISerializable
  {
    public const string Tag = "FileTransfer";

    [XmlAttribute]
    public Guid TransferId
    {
      get
      {
        return this.GetAttributeAsGuid("TransferId");
      }
      set
      {
        this.SetAttribute("TransferId", value);
      }
    }

    [XmlAttribute]
    public string TransferTo
    {
      get
      {
        return this.GetAttributeAsString("TransferTo");
      }
      set
      {
        this.SetAttribute("TransferTo", value.Trim());
      }
    }

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
    public string FileName
    {
      get
      {
        return this.GetAttributeAsString("FileName");
      }
      set
      {
        this.SetAttribute("FileName", value.Trim());
      }
    }

    [XmlAttribute]
    public int Retry
    {
      get
      {
        return this.GetAttributeAsInt("Retry");
      }
      set
      {
        this.SetAttribute("Retry", value);
      }
    }

    [XmlAttribute]
    public int FileSize
    {
      get
      {
        return this.GetAttributeAsInt("FileSize");
      }
      set
      {
        this.SetAttribute("FileSize", value);
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
    public int ChunkOffset
    {
      get
      {
        return this.GetAttributeAsInt("ChunkOffset");
      }
      set
      {
        this.SetAttribute("ChunkOffset", value);
      }
    }

    [XmlAttribute]
    public int ChunkSize
    {
      get
      {
        return this.GetAttributeAsInt("ChunkSize");
      }
      set
      {
        this.SetAttribute("ChunkSize", value);
      }
    }

    [XmlAttribute]
    public long ChunkCrc
    {
      get
      {
        return (long) this.GetAttributeAsUInt("ChunkCrc");
      }
      set
      {
        this.SetAttribute("ChunkCrc", value);
      }
    }

    [XmlAttribute]
    public int ChunkMaxSize
    {
      get
      {
        return this.GetAttributeAsInt("ChunkMaxSize");
      }
      set
      {
        this.SetAttribute("ChunkMaxSize", value);
      }
    }

    [XmlAttribute]
    public string TemporaryFolder
    {
      get
      {
        return this.GetAttributeAsString("TemporaryFolder");
      }
      set
      {
        this.SetAttribute("TemporaryFolder", value.Trim());
      }
    }

    [XmlAttribute]
    public bool IsCheckSession
    {
      get
      {
        return this.GetAttributeAsBool("IsCheckSession");
      }
      set
      {
        this.SetAttribute("IsCheckSession", value);
      }
    }

    [XmlAttribute]
    public bool IsFinished
    {
      get
      {
        return this.GetAttributeAsBool("IsFinished");
      }
      set
      {
        this.SetAttribute("IsFinished", value);
      }
    }

    [XmlAttribute]
    public bool IsAborted
    {
      get
      {
        return this.GetAttributeAsBool("IsAborted");
      }
      set
      {
        this.SetAttribute("IsAborted", value);
      }
    }

    public XFileTransfer()
      : this(new XmlDocument())
    {
    }

    public XFileTransfer(XmlDocument ownerDocument)
      : base("FileTransfer", ownerDocument)
    {
      this.Id = Guid.Empty;
      this.TransferId = Guid.Empty;
      this.TransferTo = string.Empty;
      this.FileId = Guid.Empty;
      this.FileName = string.Empty;
      this.Retry = 0;
      this.FileSize = 0;
      this.ChunkOffset = 0;
      this.ChunkSize = 0;
      this.ChunkCrc = 0L;
      this.ChunkMaxSize = 0;
      this.TemporaryFolder = string.Empty;
      this.IsEncrypted = false;
      this.IsCompressed = false;
      this.IsCheckSession = false;
      this.IsFinished = false;
      this.IsAborted = false;
    }

    public XFileTransfer(XmlElement element)
      : base(element)
    {
    }

    protected XFileTransfer(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    public XFileTransferChunk GetFileTransferChunk()
    {
      XFileTransferChunk xfileTransferChunk = (XFileTransferChunk) null;
      if (this.IsNotNull())
      {
        XmlElement element = this.Element.SelectSingleNode("FileTransferChunk") as XmlElement;
        if (element != null)
          xfileTransferChunk = new XFileTransferChunk(element);
      }
      return xfileTransferChunk;
    }

    public XFileTransferChunk SetFileTransferChunk(XFileTransferChunk xFileTransferChunk)
    {
      XFileTransferChunk xfileTransferChunk = (XFileTransferChunk) null;
      XmlNode oldChild = this.Element.SelectSingleNode("FileTransferChunk");
      if (oldChild != null)
        this.Element.RemoveChild(oldChild);
      if (xFileTransferChunk != null && xFileTransferChunk.IsNotNull())
        xfileTransferChunk = new XFileTransferChunk(this.Element.AppendChild(this.Element.OwnerDocument.ImportNode((XmlNode) xFileTransferChunk.Element, true)) as XmlElement);
      return xfileTransferChunk;
    }

    public XFileInfo GetFileInfo()
    {
      XFileInfo xfileInfo = (XFileInfo) null;
      if (this.IsNotNull())
      {
        XmlElement element = this.Element.SelectSingleNode("FileInfo") as XmlElement;
        if (element != null)
          xfileInfo = new XFileInfo(element);
      }
      return xfileInfo;
    }

    public XFileInfo SetFileInfo(XFileInfo xFileInfo)
    {
      XFileInfo xfileInfo = (XFileInfo) null;
      XmlNode oldChild = this.Element.SelectSingleNode("FileInfo");
      if (oldChild != null)
        this.Element.RemoveChild(oldChild);
      if (xFileInfo != null && xFileInfo.IsNotNull())
        xfileInfo = new XFileInfo(this.Element.AppendChild(this.Element.OwnerDocument.ImportNode((XmlNode) xFileInfo.Element, true)) as XmlElement);
      return xfileInfo;
    }

    [SecurityCritical]
    public new void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      base.GetObjectData(info, context);
    }
  }
}

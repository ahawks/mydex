// Type: Dexcom.ReceiverApi.XPartitionInfo
// Assembly: Dexcom.ReceiverCommercialApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 3F623F65-908D-40A7-87D3-892C5D1FFA2E
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.ReceiverCommercialApi.dll

using Dexcom.Common;
using System;
using System.Runtime.Serialization;
using System.Security;
using System.Xml;
using System.Xml.Serialization;

namespace Dexcom.ReceiverApi
{
  [Serializable]
  public class XPartitionInfo : XObject, ISerializable
  {
    public const string Tag = "PartitionInfo";

    [XmlAttribute]
    public byte SchemaVersion
    {
      get
      {
        return this.GetAttribute<byte>("SchemaVersion");
      }
      set
      {
        this.SetAttribute("SchemaVersion", value);
      }
    }

    [XmlAttribute]
    public byte PageHeaderVersion
    {
      get
      {
        return this.GetAttribute<byte>("PageHeaderVersion");
      }
      set
      {
        this.SetAttribute("PageHeaderVersion", value);
      }
    }

    [XmlAttribute]
    public int PageDataLength
    {
      get
      {
        return (int) this.GetAttribute<byte>("PageDataLength");
      }
      set
      {
        this.SetAttribute("PageDataLength", value);
      }
    }

    public XCollection<XPartition> Partitions
    {
      get
      {
        return new XCollection<XPartition>(this.Element);
      }
    }

    public XPartitionInfo()
      : this(new XmlDocument())
    {
    }

    public XPartitionInfo(XmlDocument ownerDocument)
      : base("PartitionInfo", ownerDocument)
    {
      this.SchemaVersion = (byte) 1;
      this.PageHeaderVersion = (byte) 1;
      this.PageDataLength = 500;
    }

    public XPartitionInfo(XmlElement element)
      : base(element)
    {
    }

    protected XPartitionInfo(SerializationInfo info, StreamingContext context)
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

// Type: Dexcom.ReceiverApi.XPartition
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
  public class XPartition : XObject, ISerializable
  {
    public const string Tag = "Partition";

    [XmlAttribute]
    public byte Id
    {
      get
      {
        return this.GetAttribute<byte>("Id");
      }
      set
      {
        this.SetAttribute("Id", value);
      }
    }

    [XmlAttribute]
    public byte RecordRevision
    {
      get
      {
        return this.GetAttribute<byte>("RecordRevision");
      }
      set
      {
        this.SetAttribute("RecordRevision", value);
      }
    }

    [XmlAttribute]
    public int RecordLength
    {
      get
      {
        return this.GetAttribute<int>("RecordLength");
      }
      set
      {
        this.SetAttribute("RecordLength", value);
      }
    }

    public XPartition()
      : this(new XmlDocument())
    {
    }

    public XPartition(XmlDocument ownerDocument)
      : base("Partition", ownerDocument)
    {
      this.Name = string.Empty;
      this.Id = (byte) 0;
      this.RecordRevision = (byte) 0;
      this.RecordLength = 0;
    }

    public XPartition(XmlElement element)
      : base(element)
    {
    }

    protected XPartition(SerializationInfo info, StreamingContext context)
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

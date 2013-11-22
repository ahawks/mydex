// Type: Dexcom.ReceiverApi.XPageHeader
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
  public class XPageHeader : XObject, ISerializable
  {
    public const string Tag = "PageHeader";

    [XmlAttribute]
    public uint PageNumber
    {
      get
      {
        return this.GetAttribute<uint>("PageNumber");
      }
      set
      {
        this.SetAttribute("PageNumber", value);
      }
    }

    [XmlAttribute]
    public string RecordType
    {
      get
      {
        return this.GetAttribute<string>("RecordType");
      }
      set
      {
        this.SetAttribute("RecordType", value.Trim());
      }
    }

    [XmlAttribute]
    public int RecordTypeId
    {
      get
      {
        return this.GetAttribute<int>("RecordTypeId");
      }
      set
      {
        this.SetAttribute("RecordTypeId", value);
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
    public uint FirstRecordIndex
    {
      get
      {
        return this.GetAttribute<uint>("FirstRecordIndex");
      }
      set
      {
        this.SetAttribute("FirstRecordIndex", value);
      }
    }

    [XmlAttribute]
    public uint NumberOfRecords
    {
      get
      {
        return this.GetAttribute<uint>("NumberOfRecords");
      }
      set
      {
        this.SetAttribute("NumberOfRecords", value);
      }
    }

    [XmlAttribute]
    public ushort Crc
    {
      get
      {
        return this.GetAttribute<ushort>("Crc");
      }
      set
      {
        this.SetAttribute("Crc", value);
      }
    }

    public XPageHeader()
      : this(new XmlDocument())
    {
    }

    public XPageHeader(XmlDocument ownerDocument)
      : base("PageHeader", ownerDocument)
    {
      this.PageNumber = 0U;
      this.RecordType = string.Empty;
      this.RecordTypeId = 0;
      this.RecordRevision = (byte) 0;
      this.FirstRecordIndex = 0U;
      this.NumberOfRecords = 0U;
      this.Crc = (ushort) 0;
    }

    public XPageHeader(XmlElement element)
      : base(element)
    {
    }

    protected XPageHeader(SerializationInfo info, StreamingContext context)
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

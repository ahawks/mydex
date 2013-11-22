// Type: Dexcom.ReceiverApi.PCParameterRecord
// Assembly: Dexcom.ReceiverCommercialApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 3F623F65-908D-40A7-87D3-892C5D1FFA2E
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.ReceiverCommercialApi.dll

using Dexcom.Common;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Dexcom.ReceiverApi
{
  public class PCParameterRecord : IGenericRecord
  {
    private ReceiverPCParameterRecord m_internalRecord;
    private XPCParameterRecord m_xmlRecord;

    public int RecordRevision { get; private set; }

    public XPCParameterRecord Parameters
    {
      get
      {
        return this.m_xmlRecord;
      }
    }

    public List<KeyValuePair<string, string>> ParameterValues
    {
      get
      {
        return CommonTools.CreateFlattenedXmlAttributesList(this.m_xmlRecord.Element);
      }
    }

    public DateTime SystemTime
    {
      get
      {
        return this.m_internalRecord.SystemTimeStamp;
      }
    }

    public DateTime DisplayTime
    {
      get
      {
        return this.m_internalRecord.DisplayTimeStamp;
      }
    }

    public ReceiverRecordType RecordType
    {
      get
      {
        return this.m_internalRecord.RecordType;
      }
    }

    public uint RecordNumber { get; set; }

    public uint PageNumber { get; set; }

    public object Tag { get; set; }

    public PCParameterRecord(byte[] data, int offset)
      : this(data, offset, ReceiverTools.GetLatestSupportedRecordRevision(ReceiverRecordType.PCSoftwareParameter))
    {
    }

    public PCParameterRecord(byte[] data, int offset, int recordRevision)
    {
      this.m_internalRecord = (ReceiverPCParameterRecord) ReceiverTools.DatabaseRecordFactory(this.m_internalRecord.RecordType, recordRevision, data, offset);
      this.RecordRevision = recordRevision;
      try
      {
        this.m_xmlRecord = this.DoCreateXmlRecord(this.m_internalRecord.XmlData);
      }
      catch (Exception ex)
      {
        throw new DexComException(ex.Message);
      }
    }

    public PCParameterRecord()
    {
      this.m_xmlRecord = new XPCParameterRecord();
    }

    private XPCParameterRecord DoCreateXmlRecord(string xmlData)
    {
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.LoadXml(xmlData);
      return new XPCParameterRecord(xmlDocument.SelectSingleNode("PCParameterRecord") as XmlElement);
    }

    public XmlElement ToXml()
    {
      return this.ToXml(new XmlDocument());
    }

    public XmlElement ToXml(XmlDocument xOwner)
    {
      XObject xobject = new XObject(xOwner.ImportNode((XmlNode) this.m_xmlRecord.Element, true) as XmlElement);
      xobject.SetAttribute("RecordNumber", this.RecordNumber);
      xobject.SetAttribute("RecordRevision", this.RecordRevision);
      xobject.SetAttribute("SystemTime", this.SystemTime);
      xobject.SetAttribute("DisplayTime", this.DisplayTime);
      return xobject.Element;
    }
  }
}

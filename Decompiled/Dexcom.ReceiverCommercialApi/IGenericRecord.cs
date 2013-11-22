// Type: Dexcom.ReceiverApi.IGenericRecord
// Assembly: Dexcom.ReceiverCommercialApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 3F623F65-908D-40A7-87D3-892C5D1FFA2E
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.ReceiverCommercialApi.dll

using System;
using System.Xml;

namespace Dexcom.ReceiverApi
{
  public interface IGenericRecord
  {
    DateTime SystemTime { get; }

    DateTime DisplayTime { get; }

    ReceiverRecordType RecordType { get; }

    uint RecordNumber { get; set; }

    uint PageNumber { get; set; }

    object Tag { get; set; }

    XmlElement ToXml();

    XmlElement ToXml(XmlDocument xOwner);
  }
}

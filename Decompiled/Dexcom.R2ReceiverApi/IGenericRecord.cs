// Type: Dexcom.R2Receiver.IGenericRecord
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using System;
using System.Xml;

namespace Dexcom.R2Receiver
{
  public interface IGenericRecord
  {
    R2RecordType RecordType { get; }

    int RecordNumber { get; }

    uint BlockNumber { get; }

    DateTime TimeStampUtc { get; }

    DateTime CorrectedTimeStampUtc { get; }

    DateTime UserTimeStamp { get; }

    DateTime SystemTime { get; }

    DateTime DisplayTime { get; }

    XmlElement Xml { get; }

    object Tag { get; set; }

    XmlElement ToXml();

    XmlElement ToXml(XmlDocument xOwner);
  }
}

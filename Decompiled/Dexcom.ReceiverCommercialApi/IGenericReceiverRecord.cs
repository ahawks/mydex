// Type: Dexcom.ReceiverApi.IGenericReceiverRecord
// Assembly: Dexcom.ReceiverCommercialApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 3F623F65-908D-40A7-87D3-892C5D1FFA2E
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.ReceiverCommercialApi.dll

using System;

namespace Dexcom.ReceiverApi
{
  public interface IGenericReceiverRecord
  {
    DateTime SystemTimeStamp { get; }

    DateTime DisplayTimeStamp { get; }

    ReceiverRecordType RecordType { get; }

    ushort RecordedCrc { get; }

    int RecordSize { get; }
  }
}

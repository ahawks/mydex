﻿// Type: Dexcom.ReceiverApi.ReceiverRecordType
// Assembly: Dexcom.ReceiverCommercialApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 3F623F65-908D-40A7-87D3-892C5D1FFA2E
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.ReceiverCommercialApi.dll

namespace Dexcom.ReceiverApi
{
  public enum ReceiverRecordType : byte
  {
    ManufacturingData,
    FirmwareParameterData,
    PCSoftwareParameter,
    SensorData,
    EGVData,
    CalSet,
    Aberration,
    InsertionTime,
    ReceiverLogData,
    ReceiverErrorData,
    MeterData,
    UserEventData,
    UserSettingData,
    MaxValue,
  }
}

// Type: Dexcom.ReceiverApi.ReceiverRecordTypeFlags
// Assembly: Dexcom.ReceiverCommercialApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 3F623F65-908D-40A7-87D3-892C5D1FFA2E
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.ReceiverCommercialApi.dll

using System;

namespace Dexcom.ReceiverApi
{
  [Flags]
  public enum ReceiverRecordTypeFlags
  {
    None = 0,
    ManufacturingData = 1,
    FirmwareParameterData = 2,
    PCSoftwareParameter = 4,
    SensorData = 8,
    EGVData = 16,
    CalSet = 32,
    Aberration = 64,
    InsertionTime = 128,
    ReceiverLogData = 256,
    ReceiverErrorData = 512,
    MeterData = 1024,
    UserEventData = 2048,
    UserSettingData = 4096,
    ProcessorErrors = 8192,
  }
}

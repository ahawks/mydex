// Type: Dexcom.R2Receiver.R2RecordTypeFlag
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using System;

namespace Dexcom.R2Receiver
{
  [Flags]
  public enum R2RecordTypeFlag
  {
    None = 0,
    EndOfBlock = 1,
    LTS_Calibration = 2,
    Log = 4,
    Meter = 8,
    Sensor = 16,
    MatchedPair = 32,
    Settings = 64,
    STS_Calibration = 128,
    Sensor2 = 256,
    ErrorLog = 512,
    Generic = 1024,
    Event = 2048,
  }
}

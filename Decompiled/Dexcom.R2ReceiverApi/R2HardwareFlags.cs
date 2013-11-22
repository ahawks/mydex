// Type: Dexcom.R2Receiver.R2HardwareFlags
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using System;

namespace Dexcom.R2Receiver
{
  [Flags]
  public enum R2HardwareFlags
  {
    Unknown = 0,
    G2_RF = 1,
    G3_RF = 2,
    DataLogger = 4,
    ShortTermSensor = 8,
    LongTermSensor = 16,
    GTX_RF = 32,
  }
}

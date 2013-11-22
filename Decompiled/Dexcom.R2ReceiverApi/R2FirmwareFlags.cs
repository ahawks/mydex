// Type: Dexcom.R2Receiver.R2FirmwareFlags
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using System;

namespace Dexcom.R2Receiver
{
  [Flags]
  public enum R2FirmwareFlags
  {
    Unknown = 0,
    OneTouch = 1,
    ManualBG = 2,
    GlucoseMmolUnits = 4,
    Clock24HourMode = 8,
    Supports3Day = 16,
    Supports5Day = 32,
    Supports7Day = 64,
  }
}

// Type: Dexcom.R2Receiver.R2BootFailureReasons
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using System;

namespace Dexcom.R2Receiver
{
  [Flags]
  public enum R2BootFailureReasons
  {
    Unknown = 0,
    InvalidConfig = 1,
    NoFirmware = 2,
    FirmwareIncompatible = 4,
    InvalidFirmware = 8,
    RtcOscillatorStopped = 16,
    RtcError = 32,
    LcdError = 64,
    RfError = 128,
    BackdoorReceived = 256,
    SramFailure = 512,
    CpldError = 1024,
    LowBattery = 2048,
    BiosConfigIncompatible = 4096,
    FirmwareBiosIncompatible = 8192,
  }
}

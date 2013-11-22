// Type: Dexcom.R2Receiver.R2ButtonFlags
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using System;

namespace Dexcom.R2Receiver
{
  [Flags]
  public enum R2ButtonFlags : byte
  {
    Unknown = (byte) 0,
    Right = (byte) 1,
    Left = (byte) 2,
    Down = (byte) 4,
    Up = (byte) 8,
  }
}

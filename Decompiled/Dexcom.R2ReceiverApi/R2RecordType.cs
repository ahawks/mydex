// Type: Dexcom.R2Receiver.R2RecordType
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

namespace Dexcom.R2Receiver
{
  public enum R2RecordType : byte
  {
    EndOfBlock = (byte) 0,
    Corrupted = (byte) 1,
    Reserved0x02 = (byte) 2,
    Log = (byte) 3,
    Meter = (byte) 4,
    BadTimeMeter = (byte) 5,
    Sensor = (byte) 6,
    MatchedPair = (byte) 7,
    Settings = (byte) 8,
    STS_Calibration1 = (byte) 9,
    Sensor2 = (byte) 10,
    Unknown1 = (byte) 11,
    Settings2 = (byte) 12,
    STS_Calibration2 = (byte) 13,
    Reserved0x0E = (byte) 14,
    Reserved0x0F = (byte) 15,
    STS_Calibration3 = (byte) 16,
    Meter2 = (byte) 17,
    STS_Calibration4 = (byte) 18,
    Settings3 = (byte) 19,
    Event = (byte) 20,
    Reserved0x15 = (byte) 21,
    Meter3 = (byte) 22,
    Sensor3 = (byte) 23,
    STS_Calibration5 = (byte) 24,
    Settings4 = (byte) 25,
    Settings5 = (byte) 26,
    Settings6 = (byte) 27,
    ErrorLog = (byte) 32,
    Generic = (byte) 33,
    ErrorLog2 = (byte) 34,
    Unassigned = (byte) 255,
  }
}

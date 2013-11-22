// Type: Dexcom.R2Receiver.R2MeterFlags
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

namespace Dexcom.R2Receiver
{
  public enum R2MeterFlags : ushort
  {
    Unmatchable = (ushort) 4097,
    UseForOutlierDetectionOnly = (ushort) 49155,
    ValidReadingButPossibleOutlier = (ushort) 57351,
    ValidReading = (ushort) 61455,
    Outlier = (ushort) 63519,
    PossibleOutlier = (ushort) 64575,
    BadTime = (ushort) 65151,
    Unprocessed = (ushort) 65535,
  }
}

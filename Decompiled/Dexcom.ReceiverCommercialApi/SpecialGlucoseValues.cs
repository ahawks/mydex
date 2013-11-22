﻿// Type: Dexcom.ReceiverApi.SpecialGlucoseValues
// Assembly: Dexcom.ReceiverCommercialApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 3F623F65-908D-40A7-87D3-892C5D1FFA2E
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.ReceiverCommercialApi.dll

namespace Dexcom.ReceiverApi
{
  public enum SpecialGlucoseValues : ushort
  {
    None = (ushort) 0,
    SensorNotActive = (ushort) 1,
    MinimallyEGVAberration = (ushort) 2,
    NoAntenna = (ushort) 3,
    SensorOutOfCal = (ushort) 5,
    CountsAberration = (ushort) 6,
    AbsoluteAberration = (ushort) 9,
    PowerAberration = (ushort) 10,
    RFBadStatus = (ushort) 12,
  }
}

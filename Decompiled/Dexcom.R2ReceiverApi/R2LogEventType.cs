// Type: Dexcom.R2Receiver.R2LogEventType
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

namespace Dexcom.R2Receiver
{
  public enum R2LogEventType : byte
  {
    SystemReset,
    ScreenDisplayed,
    MeterDownload,
    MeterFailure,
    RFOpen,
    RFClose,
    RFBadPacket,
    RFWrongID,
    LogHeapMemoryAvailable,
    LogUnintentionalReset,
    LogRFRegisters,
    MeterUse,
    EnergyLevel,
    RFWindowMode,
    RFBadPacketV2,
    IntermediateGlucose,
    AlgoNote,
    Shutdown,
    ArrowDisplayed,
    RtcTimeLoss,
    MissedMessage,
  }
}

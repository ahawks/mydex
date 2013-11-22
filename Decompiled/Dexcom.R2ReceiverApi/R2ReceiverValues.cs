// Type: Dexcom.R2Receiver.R2ReceiverValues
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using System;

namespace Dexcom.R2Receiver
{
  public class R2ReceiverValues
  {
    public static readonly string TransmitterIdValidChars = "0123456789ABCDEFGHJKLMNPQRSTUWXY";
    public static readonly string TransmitterIdInvalidChars = "IOVZ";
    public static readonly R2RecordTypeFlag AllR2Records = R2RecordTypeFlag.EndOfBlock | R2RecordTypeFlag.LTS_Calibration | R2RecordTypeFlag.Log | R2RecordTypeFlag.Meter | R2RecordTypeFlag.Sensor | R2RecordTypeFlag.MatchedPair | R2RecordTypeFlag.Settings | R2RecordTypeFlag.STS_Calibration | R2RecordTypeFlag.Sensor2 | R2RecordTypeFlag.ErrorLog | R2RecordTypeFlag.Generic | R2RecordTypeFlag.Event;
    public static readonly int BackgroundWorkerIdForDatabase = -1;
    public static readonly int BackgroundWorkerIdForEventLog = -2;
    public static readonly int BackgroundWorkerIdForErrorLog = -3;
    public static readonly DateTime R2BaseDateTime = new DateTime(1970, 1, 1);
    public static readonly DateTime EmptySensorInsertionTime = new DateTime(2049, 12, 12);
    public static readonly DateTime EmptyMeterTransferTime = new DateTime(2004, 1, 1);
    public static readonly int BlockSize = 65536;
    public static readonly ushort BlockHeaderTag = (ushort) 22596;
    public static readonly ushort BlockStatusUsed = (ushort) 49164;
    public static readonly ushort BlockStatusReadyToErase = (ushort) 32776;
    public static readonly ushort BlockStatusErased = (ushort) 61455;
    public static readonly int TransmitterIntervalMsec = 300000;
    public static readonly int TransmitterMinimumGapMsec = 360000;
    public static readonly int MaximumHighGlucoseThreshold = 402;
    public static readonly int MinimumLowGlucoseThreshold = 55;
    public static readonly uint FlashStartAddress = 1073741824U;
    public static readonly uint FlashEndAddress = 1077936128U;
    public static readonly uint FlashSize = R2ReceiverValues.FlashEndAddress - R2ReceiverValues.FlashStartAddress;
    public static readonly uint BiosStartAddress = 1073741824U;
    public static readonly uint BiosEndAddress = 1073807360U;
    public static readonly uint BiosSize = R2ReceiverValues.BiosEndAddress - R2ReceiverValues.BiosStartAddress;
    public static readonly uint ConfigStartAddress = 1073807360U;
    public static readonly uint ConfigEndAddress = 1073872896U;
    public static readonly uint ConfigSize = R2ReceiverValues.ConfigEndAddress - R2ReceiverValues.ConfigStartAddress;
    public static readonly uint ErrorStartAddress = 1073872896U;
    public static readonly uint ErrorEndAddress = 1073938432U;
    public static readonly uint ErrorSize = R2ReceiverValues.ErrorEndAddress - R2ReceiverValues.ErrorStartAddress;
    public static readonly uint EventStartAddress = 1073938432U;
    public static readonly uint EventEndAddress = 1074462720U;
    public static readonly uint EventSize = R2ReceiverValues.EventEndAddress - R2ReceiverValues.EventStartAddress;
    public static readonly uint LicenseStartAddress = 1074462720U;
    public static readonly uint LicenseEndAddress = 1074528256U;
    public static readonly uint LicenseSize = R2ReceiverValues.LicenseEndAddress - R2ReceiverValues.LicenseStartAddress;
    public static readonly uint UnusedStartAddress = 1074528256U;
    public static readonly uint UnusedEndAddress = 1074790400U;
    public static readonly uint UnusedSize = R2ReceiverValues.UnusedEndAddress - R2ReceiverValues.UnusedStartAddress;
    public static readonly uint FirmwareStartAddress = 1074790400U;
    public static readonly uint FirmwareEndAddress = 1075838976U;
    public static readonly uint FirmwareSize = R2ReceiverValues.FirmwareEndAddress - R2ReceiverValues.FirmwareStartAddress;
    public static readonly uint DatabaseStartAddress = 1075838976U;
    public static readonly uint DatabaseEndAddress = 1077936128U;
    public static readonly uint DatabaseSize = R2ReceiverValues.DatabaseEndAddress - R2ReceiverValues.DatabaseStartAddress;
    public static readonly uint BiosStartAddressV2 = 1073741824U;
    public static readonly uint BiosEndAddressV2 = 1073807360U;
    public static readonly uint BiosSizeV2 = R2ReceiverValues.BiosEndAddressV2 - R2ReceiverValues.BiosStartAddressV2;
    public static readonly uint ConfigStartAddressV2 = 1073807360U;
    public static readonly uint ConfigEndAddressV2 = 1073872896U;
    public static readonly uint ConfigSizeV2 = R2ReceiverValues.ConfigEndAddressV2 - R2ReceiverValues.ConfigStartAddressV2;
    public static readonly uint ErrorStartAddressV2 = 1073872896U;
    public static readonly uint ErrorEndAddressV2 = 1073938432U;
    public static readonly uint ErrorSizeV2 = R2ReceiverValues.ErrorEndAddressV2 - R2ReceiverValues.ErrorStartAddressV2;
    public static readonly uint LicenseStartAddressV2 = 1073938432U;
    public static readonly uint LicenseEndAddressV2 = 1074003968U;
    public static readonly uint LicenseSizeV2 = R2ReceiverValues.LicenseEndAddressV2 - R2ReceiverValues.LicenseStartAddressV2;
    public static readonly uint EventStartAddressV2 = 1074003968U;
    public static readonly uint EventEndAddressV2 = 1074266112U;
    public static readonly uint EventSizeV2 = R2ReceiverValues.EventEndAddressV2 - R2ReceiverValues.EventStartAddressV2;
    public static readonly uint UnusedStartAddressV2 = 1074266112U;
    public static readonly uint UnusedEndAddressV2 = 1074790400U;
    public static readonly uint UnusedSizeV2 = R2ReceiverValues.UnusedEndAddressV2 - R2ReceiverValues.UnusedStartAddressV2;
    public static readonly uint FirmwareStartAddressV2 = 1074790400U;
    public static readonly uint FirmwareEndAddressV2 = 1075838976U;
    public static readonly uint FirmwareSizeV2 = R2ReceiverValues.FirmwareEndAddressV2 - R2ReceiverValues.FirmwareStartAddressV2;
    public static readonly uint DatabaseStartAddressV2 = 1075838976U;
    public static readonly uint DatabaseEndAddressV2 = 1077936128U;
    public static readonly uint DatabaseSizeV2 = R2ReceiverValues.DatabaseEndAddressV2 - R2ReceiverValues.DatabaseStartAddressV2;

    static R2ReceiverValues()
    {
    }
  }
}

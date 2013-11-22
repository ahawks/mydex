// Type: Dexcom.ReceiverApi.ReceiverValues
// Assembly: Dexcom.ReceiverCommercialApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 3F623F65-908D-40A7-87D3-892C5D1FFA2E
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.ReceiverCommercialApi.dll

using System;

namespace Dexcom.ReceiverApi
{
  public class ReceiverValues
  {
    public static readonly DateTime ReceiverBaseDateTime = new DateTime(2009, 1, 1);
    public static readonly string TransmitterIdValidChars = "0123456789ABCDEFGHJKLMNPQRSTUWXY";
    public static readonly string TransmitterIdInvalidChars = "IOVZ";
    public static readonly string ComPortNamePrefix = "COM";
    public static readonly ReceiverRecordTypeFlags MatchAnythingReceiverRecordTypeFlags = (ReceiverRecordTypeFlags) 2147483647;
    public static readonly ReceiverRecordTypeFlags AllKnownReceiverRecords = ReceiverRecordTypeFlags.ManufacturingData | ReceiverRecordTypeFlags.FirmwareParameterData | ReceiverRecordTypeFlags.PCSoftwareParameter | ReceiverRecordTypeFlags.SensorData | ReceiverRecordTypeFlags.EGVData | ReceiverRecordTypeFlags.CalSet | ReceiverRecordTypeFlags.Aberration | ReceiverRecordTypeFlags.InsertionTime | ReceiverRecordTypeFlags.ReceiverLogData | ReceiverRecordTypeFlags.ReceiverErrorData | ReceiverRecordTypeFlags.MeterData | ReceiverRecordTypeFlags.UserEventData | ReceiverRecordTypeFlags.UserSettingData | ReceiverRecordTypeFlags.ProcessorErrors;
    public static readonly ReceiverRecordTypeFlags CommercialReceiverRecords = ReceiverRecordTypeFlags.ManufacturingData | ReceiverRecordTypeFlags.PCSoftwareParameter | ReceiverRecordTypeFlags.EGVData | ReceiverRecordTypeFlags.InsertionTime | ReceiverRecordTypeFlags.MeterData | ReceiverRecordTypeFlags.UserEventData;
    public static readonly DateTime EmptySensorInsertionTime = ReceiverTools.ConvertReceiverTimeToDateTime(uint.MaxValue);
    public static readonly int TransmitterIntervalMinutes = 5;
    public static readonly int TransmitterIntervalMsec = 300000;
    public static readonly int TransmitterMinimumGapMsec = 360000;
    public static readonly byte TrendArrowMask = (byte) 15;
    public static readonly byte NoiseMask = (byte) 112;
    public static readonly byte ImmediateMatchMask = (byte) sbyte.MinValue;
    public static readonly ushort IsDisplayOnlyEgvMask = (ushort) short.MinValue;
    public static readonly ushort EgvValueMask = (ushort) 1023;
    public static readonly byte FrequencyMask = (byte) 3;
    public static readonly byte TransmitterIsBadStatusMask = (byte) sbyte.MinValue;
    public static readonly byte TransmitterIsLowBatteryMask = (byte) 64;
    public static readonly uint TransmitterBadStatusValueMask = 4278190080U;
    public static readonly uint TransmitterFilteredCountsMask = 16777215U;
    public const int DatabasePageSize = 528;
    public const int DatabaseBlockSize = 4224;
    public const int DatabasePageHeaderSize = 28;
    public const int DatabasePageDataSize = 500;
    public const int DatabaseRecordOverhead = 10;
    public const int DatabaseRecordMaxPayloadSize = 490;
    public const int MinimumCalculatedEgv = 39;
    public const int MaximumCalculatedEgv = 401;

    static ReceiverValues()
    {
    }

    public class TinyBooterModeAddresses
    {
      public const uint ProcessorErrorPageByteAddress = 4321152U;
      public const uint ReservedPageByteAddress = 4321680U;
      public const uint FirmwareParametersByteAddress = 4322208U;
      public const uint PcUpdateStatePageByteAddress0 = 4322736U;
      public const uint PcUpdateStatePageByteAddress1 = 4323264U;
      public const uint PcUpdateStatePageByteAddress2 = 4323792U;
      public const uint PcUpdateStatePageByteAddress3 = 4324320U;
      public const uint BootModeStatusByteAddress = 4324848U;
      public const uint FileSystemStartByteAddress = 3649536U;
      public const uint TotalDataFlashSizeBytes = 4325376U;
    }

    public class FirmwareAppModeAddresses
    {
      public const uint SpecialInfoAreaFirstPageIndex = 1272U;
      public const uint ProcessorErrorPageIndex = 1272U;
      public const uint ReservedPageIndex = 1273U;
      public const uint FirmWareParameterPageIndex = 1274U;
      public const uint PcUpdateStatePageIndex0 = 1275U;
      public const uint PcUpdateStatePageIndex1 = 1276U;
      public const uint PcUpdateStatePageIndex2 = 1277U;
      public const uint PcUpdateStatePageIndex3 = 1278U;
      public const uint BootModeStatusPageIndex = 1279U;
    }
  }
}

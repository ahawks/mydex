#DateTime ReceiverBaseDateTime = new DateTime(2009, 1, 1)
TransmitterIdValidChars = "0123456789ABCDEFGHJKLMNPQRSTUWXY"
TransmitterIdInvalidChars = "IOVZ"
ComPortNamePrefix = "COM"

MatchAnythingReceiverRecordTypeFlags = 2147483647

class ReceiverRecordTypeFlags():
    NONE = 0
    ManufacturingData = 1
    FirmwareParameterData = 2
    PCSoftwareParameter = 4
    SensorData = 8
    EGVData = 16
    CalSet = 32
    Aberration = 64
    InsertionTime = 128
    ReceiverLogData = 256
    ReceiverErrorData = 512
    MeterData = 1024
    UserEventData = 2048
    UserSettingData = 4096
    ProcessorErrors = 8192


AllKnownReceiverRecords = ReceiverRecordTypeFlags.ManufacturingData | ReceiverRecordTypeFlags.FirmwareParameterData | ReceiverRecordTypeFlags.PCSoftwareParameter | ReceiverRecordTypeFlags.SensorData | ReceiverRecordTypeFlags.EGVData | ReceiverRecordTypeFlags.CalSet | ReceiverRecordTypeFlags.Aberration | ReceiverRecordTypeFlags.InsertionTime | ReceiverRecordTypeFlags.ReceiverLogData | ReceiverRecordTypeFlags.ReceiverErrorData | ReceiverRecordTypeFlags.MeterData | ReceiverRecordTypeFlags.UserEventData | ReceiverRecordTypeFlags.UserSettingData | ReceiverRecordTypeFlags.ProcessorErrors
CommercialReceiverRecords = ReceiverRecordTypeFlags.ManufacturingData | ReceiverRecordTypeFlags.PCSoftwareParameter | ReceiverRecordTypeFlags.EGVData | ReceiverRecordTypeFlags.InsertionTime | ReceiverRecordTypeFlags.MeterData | ReceiverRecordTypeFlags.UserEventData
#EmptySensorInsertionTime = ReceiverTools.ConvertReceiverTimeToDateTime(uint.MaxValue)
TransmitterIntervalMinutes = 5
TransmitterIntervalMsec = 300000
TransmitterMinimumGapMsec = 360000
TrendArrowMask = 15
NoiseMask = 112
ImmediateMatchMask = -128
IsDisplayOnlyEgvMask = 32768
EgvValueMask = 1023
FrequencyMask = 3
TransmitterIsBadStatusMask = -128
TransmitterIsLowBatteryMask = 64
TransmitterBadStatusValueMask = 4278190080
TransmitterFilteredCountsMask = 16777215
DatabasePageSize = 528
DatabaseBlockSize = 4224
DatabasePageHeaderSize = 28
DatabasePageDataSize = 500
DatabaseRecordOverhead = 10
DatabaseRecordMaxPayloadSize = 490
MinimumCalculatedEgv = 39
MaximumCalculatedEgv = 401
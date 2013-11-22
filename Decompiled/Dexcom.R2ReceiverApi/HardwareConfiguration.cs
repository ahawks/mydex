// Type: Dexcom.R2Receiver.HardwareConfiguration
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using Dexcom.Common;
using Dexcom.Common.Data;
using System;
using System.Runtime.InteropServices;

namespace Dexcom.R2Receiver
{
  public class HardwareConfiguration
  {
    private const uint MinVersion = 1U;
    private const uint MaxVersion = 7U;
    private R2HWConfigV3 m_R2HWConfig;
    private R2HWConfigV7 m_R2HWConfigV6;
    private uint m_configurationVersion;

    public uint ReceiverId
    {
      get
      {
        return this.m_R2HWConfig.m_deviceId;
      }
      set
      {
        this.m_R2HWConfig.m_deviceId = this.m_R2HWConfigV6.m_deviceId = value;
      }
    }

    public R2FirmwareFlags FirmwareFlags
    {
      get
      {
        return (R2FirmwareFlags) ~(int) this.FirmwareFlags32;
      }
    }

    public ulong HardwareFlagsLow64
    {
      get
      {
        return this.m_R2HWConfig.m_hardwareFlagsLow64;
      }
      set
      {
        this.m_R2HWConfig.m_hardwareFlagsLow64 = this.m_R2HWConfigV6.m_hardwareFlagsLow64 = value;
      }
    }

    public ulong HardwareFlagsHigh64
    {
      get
      {
        return this.m_R2HWConfig.m_hardwareFlagsHigh64;
      }
      set
      {
        this.m_R2HWConfig.m_hardwareFlagsHigh64 = this.m_R2HWConfigV6.m_hardwareFlagsHigh64 = value;
      }
    }

    public uint FirmwareFlags32
    {
      get
      {
        return this.m_R2HWConfig.m_firmwareFlags;
      }
      set
      {
        this.m_R2HWConfig.m_firmwareFlags = this.m_R2HWConfigV6.m_firmwareFlags = value;
      }
    }

    public byte XtalTrim
    {
      get
      {
        return this.m_R2HWConfig.m_astrx2XtalTrim;
      }
      set
      {
        this.m_R2HWConfig.m_astrx2XtalTrim = this.m_R2HWConfigV6.m_astrx2XtalTrim = value;
      }
    }

    public uint MaxSlope
    {
      get
      {
        return this.m_R2HWConfig.m_maxSlope;
      }
      set
      {
        this.m_R2HWConfig.m_maxSlope = this.m_R2HWConfigV6.m_maxSlope = value;
      }
    }

    public uint MinSlope
    {
      get
      {
        return this.m_R2HWConfig.m_minSlope;
      }
      set
      {
        this.m_R2HWConfig.m_minSlope = this.m_R2HWConfigV6.m_minSlope = value;
      }
    }

    public int MinBaseline
    {
      get
      {
        return this.m_R2HWConfig.m_minBaseline;
      }
      set
      {
        this.m_R2HWConfig.m_minBaseline = this.m_R2HWConfigV6.m_minBaseline = value;
      }
    }

    public int MaxBaseline
    {
      get
      {
        return this.m_R2HWConfig.m_maxBaseline;
      }
      set
      {
        this.m_R2HWConfig.m_maxBaseline = this.m_R2HWConfigV6.m_maxBaseline = value;
      }
    }

    public uint MinCounts
    {
      get
      {
        return this.m_R2HWConfig.m_minCounts;
      }
      set
      {
        this.m_R2HWConfig.m_minCounts = this.m_R2HWConfigV6.m_minCounts = value;
      }
    }

    public uint MaxCounts
    {
      get
      {
        return this.m_R2HWConfig.m_maxCounts;
      }
      set
      {
        this.m_R2HWConfig.m_maxCounts = this.m_R2HWConfigV6.m_maxCounts = value;
      }
    }

    public int LowAssumedBaseline
    {
      get
      {
        return this.m_R2HWConfig.m_lowAssumedBaseline;
      }
      set
      {
        this.m_R2HWConfig.m_lowAssumedBaseline = this.m_R2HWConfigV6.m_lowAssumedBaseline = value;
      }
    }

    public int MidAssumedBaseline
    {
      get
      {
        return this.m_R2HWConfig.m_midAssumedBaseline;
      }
      set
      {
        this.m_R2HWConfig.m_midAssumedBaseline = this.m_R2HWConfigV6.m_midAssumedBaseline = value;
      }
    }

    public int HighAssumedBaseline
    {
      get
      {
        return this.m_R2HWConfig.m_highAssumedBaseline;
      }
      set
      {
        this.m_R2HWConfig.m_highAssumedBaseline = this.m_R2HWConfigV6.m_highAssumedBaseline = value;
      }
    }

    public ushort Calibrated5VoltAdcReading
    {
      get
      {
        return this.m_R2HWConfig.m_calibratedAdcReading;
      }
      set
      {
        this.m_R2HWConfig.m_calibratedAdcReading = this.m_R2HWConfigV6.m_calibratedAdcReading = value;
      }
    }

    public uint HardwareProductNumber
    {
      get
      {
        return this.m_R2HWConfig.m_hardwareProductNumber;
      }
      set
      {
        this.m_R2HWConfig.m_hardwareProductNumber = this.m_R2HWConfigV6.m_hardwareProductNumber = value;
      }
    }

    public byte HardwareProductRevision
    {
      get
      {
        return this.m_R2HWConfig.m_hardwareProductRevision;
      }
      set
      {
        this.m_R2HWConfig.m_hardwareProductRevision = this.m_R2HWConfigV6.m_hardwareProductRevision = value;
      }
    }

    public Guid ReceiverInstanceId
    {
      get
      {
        return this.m_R2HWConfig.m_receiverInstanceId;
      }
      set
      {
        this.m_R2HWConfig.m_receiverInstanceId = this.m_R2HWConfigV6.m_receiverInstanceId = value;
      }
    }

    public uint ConfigurationVersion
    {
      get
      {
        return this.m_R2HWConfig.m_configVersion;
      }
    }

    public string Signature
    {
      get
      {
        return this.m_R2HWConfig.m_configSignature;
      }
      set
      {
        this.m_R2HWConfig.m_configSignature = this.m_R2HWConfigV6.m_configSignature = value;
      }
    }

    public uint Size
    {
      get
      {
        return this.m_R2HWConfig.m_configSize;
      }
      set
      {
        this.m_R2HWConfig.m_configSize = this.m_R2HWConfigV6.m_configSize = value;
      }
    }

    public uint BiosCompatibilityNumber
    {
      get
      {
        return this.m_R2HWConfig.m_biosCompatibilityNumber;
      }
      set
      {
        this.m_R2HWConfig.m_biosCompatibilityNumber = this.m_R2HWConfigV6.m_biosCompatibilityNumber = value;
      }
    }

    public uint ErrorLogAddress
    {
      get
      {
        return this.m_R2HWConfig.m_errorLogAddress;
      }
      set
      {
        this.m_R2HWConfig.m_errorLogAddress = this.m_R2HWConfigV6.m_errorLogAddress = value;
      }
    }

    public uint ErrorLogSize
    {
      get
      {
        return this.m_R2HWConfig.m_errorLogSize;
      }
      set
      {
        this.m_R2HWConfig.m_errorLogSize = this.m_R2HWConfigV6.m_errorLogSize = value;
      }
    }

    public uint EventLogAddress
    {
      get
      {
        return this.m_R2HWConfig.m_eventLogAddress;
      }
      set
      {
        this.m_R2HWConfig.m_eventLogAddress = this.m_R2HWConfigV6.m_eventLogAddress = value;
      }
    }

    public uint EventLogSize
    {
      get
      {
        return this.m_R2HWConfig.m_eventLogSize;
      }
      set
      {
        this.m_R2HWConfig.m_eventLogSize = this.m_R2HWConfigV6.m_eventLogSize = value;
      }
    }

    public uint LicenseLogAddress
    {
      get
      {
        return this.m_R2HWConfig.m_licenseLogAddress;
      }
      set
      {
        this.m_R2HWConfig.m_licenseLogAddress = this.m_R2HWConfigV6.m_licenseLogAddress = value;
      }
    }

    public uint LicenseLogSize
    {
      get
      {
        return this.m_R2HWConfig.m_licenseLogSize;
      }
      set
      {
        this.m_R2HWConfig.m_licenseLogSize = this.m_R2HWConfigV6.m_licenseLogSize = value;
      }
    }

    public uint DatabaseAddress
    {
      get
      {
        return this.m_R2HWConfig.m_databaseAddress;
      }
      set
      {
        this.m_R2HWConfig.m_databaseAddress = this.m_R2HWConfigV6.m_databaseAddress = value;
      }
    }

    public uint DatabaseSize
    {
      get
      {
        return this.m_R2HWConfig.m_databaseSize;
      }
      set
      {
        this.m_R2HWConfig.m_databaseSize = this.m_R2HWConfigV6.m_databaseSize = value;
      }
    }

    public uint ResidualCountsForMinimalAberration
    {
      get
      {
        return this.m_R2HWConfig.m_residualCountsForMinimalAberration;
      }
      set
      {
        this.m_R2HWConfig.m_residualCountsForMinimalAberration = value;
      }
    }

    public uint ResidualCountsForSevereAberration
    {
      get
      {
        return this.m_R2HWConfig.m_residualCountsForSevereAberration;
      }
      set
      {
        this.m_R2HWConfig.m_residualCountsForSevereAberration = value;
      }
    }

    public byte AbsoluteEgvDeltaForMinimalAberration
    {
      get
      {
        return this.m_R2HWConfig.m_absoluteEgvDeltaForMinimalAberration;
      }
      set
      {
        this.m_R2HWConfig.m_absoluteEgvDeltaForMinimalAberration = this.m_R2HWConfigV6.m_absoluteEgvDeltaForMinimalAberration = value;
      }
    }

    public byte PercentageEgvDeltaForMinimalAberration
    {
      get
      {
        return this.m_R2HWConfig.m_percentageEgvDeltaForMinimalAberration;
      }
      set
      {
        this.m_R2HWConfig.m_percentageEgvDeltaForMinimalAberration = this.m_R2HWConfigV6.m_percentageEgvDeltaForMinimalAberration = value;
      }
    }

    public byte AbsoluteEgvDeltaForSevereAberration
    {
      get
      {
        return this.m_R2HWConfig.m_absoluteEgvDeltaForSevereAberration;
      }
      set
      {
        this.m_R2HWConfig.m_absoluteEgvDeltaForSevereAberration = this.m_R2HWConfigV6.m_absoluteEgvDeltaForSevereAberration = value;
      }
    }

    public byte PercentageEgvDeltaForSevereAberration
    {
      get
      {
        return this.m_R2HWConfig.m_percentageEgvDeltaForSevereAberration;
      }
      set
      {
        this.m_R2HWConfig.m_percentageEgvDeltaForSevereAberration = this.m_R2HWConfigV6.m_percentageEgvDeltaForSevereAberration = value;
      }
    }

    public ushort CountsAberrationWindow
    {
      get
      {
        return this.m_R2HWConfig.m_countsAberrationWindow;
      }
      set
      {
        this.m_R2HWConfig.m_countsAberrationWindow = this.m_R2HWConfigV6.m_countsAberrationWindow = value;
      }
    }

    public ushort ResidualAberrationWindow
    {
      get
      {
        return this.m_R2HWConfig.m_residualAberrationWindow;
      }
      set
      {
        this.m_R2HWConfig.m_residualAberrationWindow = this.m_R2HWConfigV6.m_residualAberrationWindow = value;
      }
    }

    public ushort EgvAberrationWindow
    {
      get
      {
        return this.m_R2HWConfig.m_egvAberrationWindow;
      }
      set
      {
        this.m_R2HWConfig.m_egvAberrationWindow = this.m_R2HWConfigV6.m_egvAberrationWindow = value;
      }
    }

    public byte MaxAllowedCountsAberrations
    {
      get
      {
        return this.m_R2HWConfig.m_maxAllowedCountsAberrations;
      }
      set
      {
        this.m_R2HWConfig.m_maxAllowedCountsAberrations = this.m_R2HWConfigV6.m_maxAllowedCountsAberrations = value;
      }
    }

    public byte MaxAllowedResidualAberrations
    {
      get
      {
        return this.m_R2HWConfig.m_maxAllowedResidualAberrations;
      }
      set
      {
        this.m_R2HWConfig.m_maxAllowedResidualAberrations = this.m_R2HWConfigV6.m_maxAllowedResidualAberrations = value;
      }
    }

    public byte MaxAllowedEgvAberrations
    {
      get
      {
        return this.m_R2HWConfig.m_maxAllowedEgvAberrations;
      }
      set
      {
        this.m_R2HWConfig.m_maxAllowedEgvAberrations = this.m_R2HWConfigV6.m_maxAllowedEgvAberrations = value;
      }
    }

    public byte MaxAllowedPointsToGetInCal
    {
      get
      {
        return this.m_R2HWConfig.m_maxAllowedPointsToGetInCal;
      }
      set
      {
        this.m_R2HWConfig.m_maxAllowedPointsToGetInCal = this.m_R2HWConfigV6.m_maxAllowedPointsToGetInCal = value;
      }
    }

    public byte PercentageVenousAdjustment
    {
      get
      {
        return this.m_R2HWConfig.m_percentageVenousAdjustment;
      }
      set
      {
        this.m_R2HWConfig.m_percentageVenousAdjustment = this.m_R2HWConfigV6.m_percentageVenousAdjustment = value;
      }
    }

    public byte CullPercentage
    {
      get
      {
        return this.m_R2HWConfig.m_cullPercentage;
      }
      set
      {
        this.m_R2HWConfig.m_cullPercentage = value;
      }
    }

    public byte CullDelta
    {
      get
      {
        return this.m_R2HWConfig.m_cullDelta;
      }
      set
      {
        this.m_R2HWConfig.m_cullDelta = value;
      }
    }

    public byte MaxTotalAberrations
    {
      get
      {
        return this.m_R2HWConfig.m_maxTotalAberrations;
      }
      set
      {
        this.m_R2HWConfig.m_maxTotalAberrations = this.m_R2HWConfigV6.m_maxTotalAberrations = value;
      }
    }

    public byte ManualBGBackDateOffset
    {
      get
      {
        return this.m_R2HWConfig.m_manualBGBackDateOffset;
      }
      set
      {
        this.m_R2HWConfig.m_manualBGBackDateOffset = this.m_R2HWConfigV6.m_manualBGBackDateOffset = value;
      }
    }

    public byte ResidualPercentageForMinimalAberration
    {
      get
      {
        return this.m_R2HWConfigV6.m_residualPercentageForMinimalAberration;
      }
      set
      {
        this.m_R2HWConfigV6.m_residualPercentageForMinimalAberration = value;
      }
    }

    public byte ResidualPercentageForSevereAberration
    {
      get
      {
        return this.m_R2HWConfigV6.m_residualPercentageForSevereAberration;
      }
      set
      {
        this.m_R2HWConfigV6.m_residualPercentageForSevereAberration = value;
      }
    }

    public byte DeltaResidualPercentageForMinimalAberration
    {
      get
      {
        return this.m_R2HWConfigV6.m_deltaResidualPercentageForMinimalAberration;
      }
      set
      {
        this.m_R2HWConfigV6.m_deltaResidualPercentageForMinimalAberration = value;
      }
    }

    public byte DeltaResidualPercentageForSevereAberration
    {
      get
      {
        return this.m_R2HWConfigV6.m_deltaResidualPercentageForSevereAberration;
      }
      set
      {
        this.m_R2HWConfigV6.m_deltaResidualPercentageForSevereAberration = value;
      }
    }

    public ushort DeltaResidualAberrationWindow
    {
      get
      {
        return this.m_R2HWConfigV6.m_deltaResidualAberrationWindow;
      }
      set
      {
        this.m_R2HWConfigV6.m_deltaResidualAberrationWindow = value;
      }
    }

    public byte MaxAllowedDeltaResidualAberrations
    {
      get
      {
        return this.m_R2HWConfigV6.m_maxAllowedDeltaResidualAberrations;
      }
      set
      {
        this.m_R2HWConfigV6.m_maxAllowedDeltaResidualAberrations = value;
      }
    }

    public byte MaxTotalAberrationsType1
    {
      get
      {
        return this.m_R2HWConfigV6.m_maxTotalAberrationsType1;
      }
      set
      {
        this.m_R2HWConfigV6.m_maxTotalAberrationsType1 = value;
      }
    }

    public byte MaxTotalAberrationsType2
    {
      get
      {
        return this.m_R2HWConfigV6.m_maxTotalAberrationsType2;
      }
      set
      {
        this.m_R2HWConfigV6.m_maxTotalAberrationsType2 = value;
      }
    }

    public byte MaxTotalAberrationsType3
    {
      get
      {
        return this.m_R2HWConfigV6.m_maxTotalAberrationsType3;
      }
      set
      {
        this.m_R2HWConfigV6.m_maxTotalAberrationsType3 = value;
      }
    }

    public Guid ImageInstanceId
    {
      get
      {
        return this.m_R2HWConfigV6.m_imageInstanceId;
      }
      set
      {
        this.m_R2HWConfigV6.m_imageInstanceId = value;
      }
    }

    public string ImageExtension
    {
      get
      {
        return this.m_R2HWConfigV6.m_imageExtension;
      }
      set
      {
        if (value.Length > 3 && (int) this.ConfigurationVersion == 4)
          this.m_R2HWConfigV6.m_imageExtension = value.Remove(3);
        else if (value.Length > 9 && ((int) this.ConfigurationVersion == 5 || (int) this.ConfigurationVersion == 6 || (int) this.ConfigurationVersion == 7))
          this.m_R2HWConfigV6.m_imageExtension = value.Remove(9);
        else
          this.m_R2HWConfigV6.m_imageExtension = value;
      }
    }

    public uint HighWedgeMinSlope
    {
      get
      {
        return this.m_R2HWConfigV6.m_highWedgeMinSlope;
      }
      set
      {
        this.m_R2HWConfigV6.m_highWedgeMinSlope = value;
      }
    }

    public uint HighWedgeMaxSlope
    {
      get
      {
        return this.m_R2HWConfigV6.m_highWedgeMaxSlope;
      }
      set
      {
        this.m_R2HWConfigV6.m_highWedgeMaxSlope = value;
      }
    }

    public int HighWedgeMinBaseline
    {
      get
      {
        return this.m_R2HWConfigV6.m_highWedgeMinBaseline;
      }
      set
      {
        this.m_R2HWConfigV6.m_highWedgeMinBaseline = value;
      }
    }

    public int HighWedgeMaxBaseline
    {
      get
      {
        return this.m_R2HWConfigV6.m_highWedgeMaxBaseline;
      }
      set
      {
        this.m_R2HWConfigV6.m_highWedgeMaxBaseline = value;
      }
    }

    public int HighWedgeLowAssumedBaseline
    {
      get
      {
        return this.m_R2HWConfigV6.m_highWedgeLowAssumedBaseline;
      }
      set
      {
        this.m_R2HWConfigV6.m_highWedgeLowAssumedBaseline = value;
      }
    }

    public int HighWedgeMidAssumedBaseline
    {
      get
      {
        return this.m_R2HWConfigV6.m_highWedgeMidAssumedBaseline;
      }
      set
      {
        this.m_R2HWConfigV6.m_highWedgeMidAssumedBaseline = value;
      }
    }

    public int HighWedgeHighAssumedBaseline
    {
      get
      {
        return this.m_R2HWConfigV6.m_highWedgeHighAssumedBaseline;
      }
      set
      {
        this.m_R2HWConfigV6.m_highWedgeHighAssumedBaseline = value;
      }
    }

    public uint LowWedgeMinSlope
    {
      get
      {
        return this.m_R2HWConfigV6.m_lowWedgeMinSlope;
      }
      set
      {
        this.m_R2HWConfigV6.m_lowWedgeMinSlope = value;
      }
    }

    public uint LowWedgeMaxSlope
    {
      get
      {
        return this.m_R2HWConfigV6.m_lowWedgeMaxSlope;
      }
      set
      {
        this.m_R2HWConfigV6.m_lowWedgeMaxSlope = value;
      }
    }

    public int LowWedgeMinBaseline
    {
      get
      {
        return this.m_R2HWConfigV6.m_lowWedgeMinBaseline;
      }
      set
      {
        this.m_R2HWConfigV6.m_lowWedgeMinBaseline = value;
      }
    }

    public int LowWedgeMaxBaseline
    {
      get
      {
        return this.m_R2HWConfigV6.m_lowWedgeMaxBaseline;
      }
      set
      {
        this.m_R2HWConfigV6.m_lowWedgeMaxBaseline = value;
      }
    }

    public int LowWedgeLowAssumedBaseline
    {
      get
      {
        return this.m_R2HWConfigV6.m_lowWedgeLowAssumedBaseline;
      }
      set
      {
        this.m_R2HWConfigV6.m_lowWedgeLowAssumedBaseline = value;
      }
    }

    public int LowWedgeMidAssumedBaseline
    {
      get
      {
        return this.m_R2HWConfigV6.m_lowWedgeMidAssumedBaseline;
      }
      set
      {
        this.m_R2HWConfigV6.m_lowWedgeMidAssumedBaseline = value;
      }
    }

    public int LowWedgeHighAssumedBaseline
    {
      get
      {
        return this.m_R2HWConfigV6.m_lowWedgeHighAssumedBaseline;
      }
      set
      {
        this.m_R2HWConfigV6.m_lowWedgeHighAssumedBaseline = value;
      }
    }

    public uint RMSTimeWindow
    {
      get
      {
        return this.m_R2HWConfigV6.m_rmsTimeWindow;
      }
      set
      {
        this.m_R2HWConfigV6.m_rmsTimeWindow = value;
      }
    }

    public uint NumberPointsPowerCalculation
    {
      get
      {
        return this.m_R2HWConfigV6.m_numberPointsPowerCalculation;
      }
      set
      {
        this.m_R2HWConfigV6.m_numberPointsPowerCalculation = value;
      }
    }

    public uint DeltaResidualNoiseThreshold
    {
      get
      {
        return this.m_R2HWConfigV6.m_deltaResidualNoiseThreshold;
      }
      set
      {
        this.m_R2HWConfigV6.m_deltaResidualNoiseThreshold = value;
      }
    }

    public uint ResidualNoiseThreshold
    {
      get
      {
        return this.m_R2HWConfigV6.m_residualNoiseThreshold;
      }
      set
      {
        this.m_R2HWConfigV6.m_residualNoiseThreshold = value;
      }
    }

    public ushort AbsoluteAberrationWindow
    {
      get
      {
        return this.m_R2HWConfigV6.m_absoluteAberrationWindow;
      }
      set
      {
        this.m_R2HWConfigV6.m_absoluteAberrationWindow = value;
      }
    }

    public ushort PowerAberrationWindow
    {
      get
      {
        return this.m_R2HWConfigV6.m_powerAberrationWindow;
      }
      set
      {
        this.m_R2HWConfigV6.m_powerAberrationWindow = value;
      }
    }

    public uint AbsoluteResidualAberrationCounts
    {
      get
      {
        return this.m_R2HWConfigV6.m_absoluteResidualAberrationCounts;
      }
      set
      {
        this.m_R2HWConfigV6.m_absoluteResidualAberrationCounts = value;
      }
    }

    public uint AbsoluteDeltaResidualAberrationCounts
    {
      get
      {
        return this.m_R2HWConfigV6.m_absoluteDeltaResidualAberrationCounts;
      }
      set
      {
        this.m_R2HWConfigV6.m_absoluteDeltaResidualAberrationCounts = value;
      }
    }

    public byte PowerAberrationResidualPercentage
    {
      get
      {
        return this.m_R2HWConfigV6.m_powerAberrationResidualPercentage;
      }
      set
      {
        this.m_R2HWConfigV6.m_powerAberrationResidualPercentage = value;
      }
    }

    public byte PowerAberrationDeltaResidualPercentage
    {
      get
      {
        return this.m_R2HWConfigV6.m_powerAberrationDeltaResidualPercentage;
      }
      set
      {
        this.m_R2HWConfigV6.m_powerAberrationDeltaResidualPercentage = value;
      }
    }

    public byte MaxAllowedAbsoluteAberrations
    {
      get
      {
        return this.m_R2HWConfigV6.m_maxAllowedAbsoluteAberrations;
      }
      set
      {
        this.m_R2HWConfigV6.m_maxAllowedAbsoluteAberrations = value;
      }
    }

    public byte MaxAllowedPowerAberrations
    {
      get
      {
        return this.m_R2HWConfigV6.m_maxAllowedPowerAberrations;
      }
      set
      {
        this.m_R2HWConfigV6.m_maxAllowedPowerAberrations = value;
      }
    }

    public string ImageSubExtension
    {
      get
      {
        return this.m_R2HWConfigV6.m_imageSubExtension;
      }
      set
      {
        if (value.Length > 7)
          this.m_R2HWConfigV6.m_imageSubExtension = value.Remove(7);
        else
          this.m_R2HWConfigV6.m_imageSubExtension = value;
      }
    }

    public byte UpperAdjustBoundPercent
    {
      get
      {
        return this.m_R2HWConfigV6.m_upperAdjustBoundPercent;
      }
      set
      {
        this.m_R2HWConfigV6.m_upperAdjustBoundPercent = value;
      }
    }

    public byte LowerAdjustBoundPercent
    {
      get
      {
        return this.m_R2HWConfigV6.m_lowerAdjustBoundPercent;
      }
      set
      {
        this.m_R2HWConfigV6.m_lowerAdjustBoundPercent = value;
      }
    }

    public byte MaxUpwardAdjustPercent
    {
      get
      {
        return this.m_R2HWConfigV6.m_maxUpwardAdjustPercent;
      }
      set
      {
        this.m_R2HWConfigV6.m_maxUpwardAdjustPercent = value;
      }
    }

    public byte MaxDownwardAdjustPercent
    {
      get
      {
        return this.m_R2HWConfigV6.m_maxDownwardAdjustPercent;
      }
      set
      {
        this.m_R2HWConfigV6.m_maxDownwardAdjustPercent = value;
      }
    }

    public double AlphaLight
    {
      get
      {
        return this.m_R2HWConfigV6.m_alphaLight;
      }
      set
      {
        this.m_R2HWConfigV6.m_alphaLight = value;
      }
    }

    public double AlphaMedium
    {
      get
      {
        return this.m_R2HWConfigV6.m_alphaMedium;
      }
      set
      {
        this.m_R2HWConfigV6.m_alphaMedium = value;
      }
    }

    public double AlphaSevere
    {
      get
      {
        return this.m_R2HWConfigV6.m_alphaSevere;
      }
      set
      {
        this.m_R2HWConfigV6.m_alphaSevere = value;
      }
    }

    public double AlphaSignal
    {
      get
      {
        return this.m_R2HWConfigV6.m_alphaSignal;
      }
      set
      {
        this.m_R2HWConfigV6.m_alphaSignal = value;
      }
    }

    public double ThresholdLight
    {
      get
      {
        return this.m_R2HWConfigV6.m_thresholdLight;
      }
      set
      {
        this.m_R2HWConfigV6.m_thresholdLight = value;
      }
    }

    public double ThresholdMedium
    {
      get
      {
        return this.m_R2HWConfigV6.m_thresholdMedium;
      }
      set
      {
        this.m_R2HWConfigV6.m_thresholdMedium = value;
      }
    }

    public double ThresholdSevere
    {
      get
      {
        return this.m_R2HWConfigV6.m_thresholdSevere;
      }
      set
      {
        this.m_R2HWConfigV6.m_thresholdSevere = value;
      }
    }

    public HardwareConfiguration(uint version)
    {
      this.m_configurationVersion = version;
      if (version < 1U || version > 7U)
        throw new ApplicationException("Unknown or invalid DexCom R2 Receiver hardware configuration version.");
      this.m_R2HWConfig.m_configSignature = "R2 CFG";
      this.m_R2HWConfig.m_configSize = 1024U;
      this.m_R2HWConfig.m_configVersion = version;
      this.m_R2HWConfig.m_biosCompatibilityNumber = 1U;
      this.m_R2HWConfig.m_errorLogAddress = R2ReceiverValues.ErrorStartAddressV2;
      this.m_R2HWConfig.m_errorLogSize = R2ReceiverValues.ErrorSizeV2;
      this.m_R2HWConfig.m_eventLogAddress = R2ReceiverValues.EventStartAddressV2;
      this.m_R2HWConfig.m_eventLogSize = R2ReceiverValues.EventSizeV2;
      this.m_R2HWConfig.m_licenseLogAddress = R2ReceiverValues.LicenseStartAddressV2;
      this.m_R2HWConfig.m_licenseLogSize = R2ReceiverValues.LicenseSizeV2;
      this.m_R2HWConfig.m_databaseAddress = R2ReceiverValues.DatabaseStartAddressV2;
      this.m_R2HWConfig.m_databaseSize = R2ReceiverValues.DatabaseSizeV2;
      this.m_R2HWConfig.m_hardwareFlagsLow64 = 18446744073709551575UL;
      this.m_R2HWConfig.m_hardwareFlagsHigh64 = ulong.MaxValue;
      this.m_R2HWConfig.m_firmwareFlags = uint.MaxValue;
      this.m_R2HWConfig.m_deviceId = 0U;
      this.m_R2HWConfig.m_hardwareProductNumber = 8205U;
      this.m_R2HWConfig.m_astrx2XtalTrim = (byte) 6;
      this.m_R2HWConfig.m_hardwareProductRevision = (byte) 21;
      this.m_R2HWConfig.m_manualBGBackDateOffset = byte.MaxValue;
      this.m_R2HWConfig.m_receiverInstanceId = CommonValues.R2EmptyId;
      this.m_R2HWConfig.m_minSlope = 15U;
      this.m_R2HWConfig.m_maxSlope = 80U;
      this.m_R2HWConfig.m_minBaseline = 1500;
      this.m_R2HWConfig.m_maxBaseline = 5500;
      this.m_R2HWConfig.m_lowAssumedBaseline = 2500;
      this.m_R2HWConfig.m_midAssumedBaseline = 3500;
      this.m_R2HWConfig.m_highAssumedBaseline = 4500;
      this.m_R2HWConfig.m_minCounts = 1000U;
      this.m_R2HWConfig.m_maxCounts = 60000U;
      this.m_R2HWConfig.m_residualCountsForMinimalAberration = 500U;
      this.m_R2HWConfig.m_residualCountsForSevereAberration = 5000U;
      this.m_R2HWConfig.m_absoluteEgvDeltaForMinimalAberration = (byte) 20;
      this.m_R2HWConfig.m_percentageEgvDeltaForMinimalAberration = (byte) 20;
      this.m_R2HWConfig.m_absoluteEgvDeltaForSevereAberration = (byte) 40;
      this.m_R2HWConfig.m_percentageEgvDeltaForSevereAberration = (byte) 40;
      this.m_R2HWConfig.m_countsAberrationWindow = (ushort) 1441;
      this.m_R2HWConfig.m_residualAberrationWindow = (ushort) 1441;
      this.m_R2HWConfig.m_egvAberrationWindow = (ushort) 1441;
      this.m_R2HWConfig.m_calibratedAdcReading = ushort.MaxValue;
      this.m_R2HWConfig.m_maxAllowedCountsAberrations = (int) version == 2 ? (byte) 6 : (byte) 0;
      this.m_R2HWConfig.m_maxAllowedResidualAberrations = (byte) 0;
      this.m_R2HWConfig.m_maxAllowedEgvAberrations = (byte) 0;
      this.m_R2HWConfig.m_maxAllowedPointsToGetInCal = (byte) 0;
      this.m_R2HWConfig.m_percentageVenousAdjustment = (byte) 90;
      this.m_R2HWConfig.m_cullPercentage = (byte) 0;
      this.m_R2HWConfig.m_cullDelta = (byte) 0;
      this.m_R2HWConfig.m_maxTotalAberrations = (byte) 6;
      if ((int) version == 1)
      {
        this.m_R2HWConfig.m_configSize = 128U;
        this.m_R2HWConfig.m_errorLogAddress = R2ReceiverValues.ErrorStartAddress;
        this.m_R2HWConfig.m_errorLogSize = R2ReceiverValues.ErrorSize;
        this.m_R2HWConfig.m_eventLogAddress = R2ReceiverValues.EventStartAddress;
        this.m_R2HWConfig.m_eventLogSize = R2ReceiverValues.EventSize;
        this.m_R2HWConfig.m_licenseLogAddress = R2ReceiverValues.LicenseStartAddress;
        this.m_R2HWConfig.m_licenseLogSize = R2ReceiverValues.LicenseSize;
        this.m_R2HWConfig.m_databaseAddress = R2ReceiverValues.DatabaseStartAddress;
        this.m_R2HWConfig.m_databaseSize = R2ReceiverValues.DatabaseSize;
        this.m_R2HWConfig.m_hardwareFlagsLow64 = 18446744073709551605UL;
        this.m_R2HWConfig.m_minSlope = 50U;
        this.m_R2HWConfig.m_maxSlope = 350U;
        this.m_R2HWConfig.m_minBaseline = 8000;
        this.m_R2HWConfig.m_maxBaseline = 40000;
        this.m_R2HWConfig.m_lowAssumedBaseline = 15000;
        this.m_R2HWConfig.m_midAssumedBaseline = 23000;
        this.m_R2HWConfig.m_highAssumedBaseline = 30000;
        this.m_R2HWConfig.m_minCounts = 10000U;
        this.m_R2HWConfig.m_maxCounts = 600000U;
      }
      this.m_R2HWConfig.m_crc = 0U;
      this.m_R2HWConfig.UpdateCrc();
      this.m_R2HWConfigV6.m_configSignature = "R2 CFG";
      this.m_R2HWConfigV6.m_configSize = 1024U;
      this.m_R2HWConfigV6.m_configVersion = version;
      this.m_R2HWConfigV6.m_biosCompatibilityNumber = 1U;
      this.m_R2HWConfigV6.m_errorLogAddress = R2ReceiverValues.ErrorStartAddressV2;
      this.m_R2HWConfigV6.m_errorLogSize = R2ReceiverValues.ErrorSizeV2;
      this.m_R2HWConfigV6.m_eventLogAddress = R2ReceiverValues.EventStartAddressV2;
      this.m_R2HWConfigV6.m_eventLogSize = R2ReceiverValues.EventSizeV2;
      this.m_R2HWConfigV6.m_licenseLogAddress = R2ReceiverValues.LicenseStartAddressV2;
      this.m_R2HWConfigV6.m_licenseLogSize = R2ReceiverValues.LicenseSizeV2;
      this.m_R2HWConfigV6.m_databaseAddress = R2ReceiverValues.DatabaseStartAddressV2;
      this.m_R2HWConfigV6.m_databaseSize = R2ReceiverValues.DatabaseSizeV2;
      this.m_R2HWConfigV6.m_hardwareFlagsLow64 = 18446744073709551575UL;
      this.m_R2HWConfigV6.m_hardwareFlagsHigh64 = ulong.MaxValue;
      this.m_R2HWConfigV6.m_firmwareFlags = 4294967228U;
      this.m_R2HWConfigV6.m_deviceId = 0U;
      this.m_R2HWConfigV6.m_hardwareProductNumber = 8205U;
      this.m_R2HWConfigV6.m_astrx2XtalTrim = (byte) 6;
      this.m_R2HWConfigV6.m_hardwareProductRevision = (byte) 21;
      this.m_R2HWConfigV6.m_manualBGBackDateOffset = (byte) 3;
      this.m_R2HWConfigV6.m_receiverInstanceId = CommonValues.R2EmptyId;
      this.m_R2HWConfigV6.m_minSlope = 100U;
      this.m_R2HWConfigV6.m_maxSlope = 600U;
      this.m_R2HWConfigV6.m_minBaseline = 5000;
      this.m_R2HWConfigV6.m_maxBaseline = 45000;
      this.m_R2HWConfigV6.m_lowAssumedBaseline = 15000;
      this.m_R2HWConfigV6.m_midAssumedBaseline = 25000;
      this.m_R2HWConfigV6.m_highAssumedBaseline = 35000;
      this.m_R2HWConfigV6.m_minCounts = 5000U;
      this.m_R2HWConfigV6.m_maxCounts = 600000U;
      this.m_R2HWConfigV6.m_residualPercentageForMinimalAberration = (byte) 20;
      this.m_R2HWConfigV6.m_residualPercentageForSevereAberration = (byte) 40;
      this.m_R2HWConfigV6.m_deltaResidualPercentageForMinimalAberration = (byte) 20;
      this.m_R2HWConfigV6.m_deltaResidualPercentageForSevereAberration = (byte) 40;
      this.m_R2HWConfigV6.m_absoluteEgvDeltaForMinimalAberration = (byte) 40;
      this.m_R2HWConfigV6.m_percentageEgvDeltaForMinimalAberration = (byte) 40;
      this.m_R2HWConfigV6.m_absoluteEgvDeltaForSevereAberration = (byte) 40;
      this.m_R2HWConfigV6.m_percentageEgvDeltaForSevereAberration = (byte) 0;
      this.m_R2HWConfigV6.m_countsAberrationWindow = (ushort) 61;
      this.m_R2HWConfigV6.m_residualAberrationWindow = (ushort) 1441;
      this.m_R2HWConfigV6.m_deltaResidualAberrationWindow = (ushort) 1441;
      this.m_R2HWConfigV6.m_egvAberrationWindow = (ushort) 1441;
      this.m_R2HWConfigV6.m_calibratedAdcReading = ushort.MaxValue;
      this.m_R2HWConfigV6.m_maxAllowedCountsAberrations = (byte) 6;
      this.m_R2HWConfigV6.m_maxAllowedDeltaResidualAberrations = (byte) 0;
      this.m_R2HWConfigV6.m_maxAllowedResidualAberrations = (byte) 0;
      this.m_R2HWConfigV6.m_maxAllowedEgvAberrations = (byte) 0;
      this.m_R2HWConfigV6.m_maxAllowedPointsToGetInCal = (byte) 0;
      this.m_R2HWConfigV6.m_percentageVenousAdjustment = (byte) 97;
      this.m_R2HWConfigV6.m_maxTotalAberrationsType1 = (byte) 0;
      this.m_R2HWConfigV6.m_maxTotalAberrationsType2 = (byte) 0;
      this.m_R2HWConfigV6.m_maxTotalAberrationsType3 = (byte) 0;
      this.m_R2HWConfigV6.m_maxTotalAberrations = (byte) 6;
      this.m_R2HWConfigV6.m_imageInstanceId = CommonValues.R2EmptyId;
      this.m_R2HWConfigV6.m_imageExtension = string.Empty;
      this.m_R2HWConfigV6.m_highWedgeMinSlope = 0U;
      this.m_R2HWConfigV6.m_highWedgeMaxSlope = 0U;
      this.m_R2HWConfigV6.m_highWedgeMinBaseline = 0;
      this.m_R2HWConfigV6.m_highWedgeMaxBaseline = 0;
      this.m_R2HWConfigV6.m_highWedgeLowAssumedBaseline = 0;
      this.m_R2HWConfigV6.m_highWedgeMidAssumedBaseline = 0;
      this.m_R2HWConfigV6.m_highWedgeHighAssumedBaseline = 0;
      this.m_R2HWConfigV6.m_lowWedgeMinSlope = 0U;
      this.m_R2HWConfigV6.m_lowWedgeMaxSlope = 0U;
      this.m_R2HWConfigV6.m_lowWedgeMinBaseline = 0;
      this.m_R2HWConfigV6.m_lowWedgeMaxBaseline = 0;
      this.m_R2HWConfigV6.m_lowWedgeLowAssumedBaseline = 0;
      this.m_R2HWConfigV6.m_lowWedgeMidAssumedBaseline = 0;
      this.m_R2HWConfigV6.m_lowWedgeHighAssumedBaseline = 0;
      this.m_R2HWConfigV6.m_rmsTimeWindow = 30U;
      this.m_R2HWConfigV6.m_numberPointsPowerCalculation = 3U;
      this.m_R2HWConfigV6.m_deltaResidualNoiseThreshold = 10U;
      this.m_R2HWConfigV6.m_residualNoiseThreshold = 15U;
      this.m_R2HWConfigV6.m_absoluteAberrationWindow = (ushort) 61;
      this.m_R2HWConfigV6.m_powerAberrationWindow = (ushort) 1441;
      this.m_R2HWConfigV6.m_absoluteResidualAberrationCounts = 60000U;
      this.m_R2HWConfigV6.m_absoluteDeltaResidualAberrationCounts = 60000U;
      this.m_R2HWConfigV6.m_powerAberrationResidualPercentage = (byte) 20;
      this.m_R2HWConfigV6.m_powerAberrationDeltaResidualPercentage = (byte) 30;
      this.m_R2HWConfigV6.m_maxAllowedAbsoluteAberrations = (byte) 6;
      this.m_R2HWConfigV6.m_maxAllowedPowerAberrations = (byte) 0;
      this.m_R2HWConfigV6.m_imageSubExtension = "-0";
      this.m_R2HWConfigV6.m_upperAdjustBoundPercent = (byte) 110;
      this.m_R2HWConfigV6.m_lowerAdjustBoundPercent = (byte) 90;
      this.m_R2HWConfigV6.m_maxUpwardAdjustPercent = (byte) 110;
      this.m_R2HWConfigV6.m_maxDownwardAdjustPercent = (byte) 90;
      this.m_R2HWConfigV6.m_alphaLight = 0.5;
      this.m_R2HWConfigV6.m_alphaMedium = 0.3;
      this.m_R2HWConfigV6.m_alphaSevere = 0.1;
      this.m_R2HWConfigV6.m_alphaSignal = 1.0 / 400.0;
      this.m_R2HWConfigV6.m_thresholdLight = 10.0;
      this.m_R2HWConfigV6.m_thresholdMedium = 10.0;
      this.m_R2HWConfigV6.m_thresholdSevere = 10.0;
      this.m_R2HWConfigV6.m_crc = 0U;
      this.m_R2HWConfigV6.UpdateCrc();
    }

    public HardwareConfiguration(R2HWConfigV7 r2HWConfig)
      : this(7U)
    {
      this.m_R2HWConfigV6 = r2HWConfig;
      this.m_R2HWConfig.m_configSignature = r2HWConfig.m_configSignature;
      this.m_R2HWConfig.m_configSize = r2HWConfig.m_configSize;
      this.m_R2HWConfig.m_configVersion = r2HWConfig.m_configVersion;
      this.m_R2HWConfig.m_biosCompatibilityNumber = r2HWConfig.m_biosCompatibilityNumber;
      this.m_R2HWConfig.m_errorLogAddress = r2HWConfig.m_errorLogAddress;
      this.m_R2HWConfig.m_errorLogSize = r2HWConfig.m_errorLogSize;
      this.m_R2HWConfig.m_eventLogAddress = r2HWConfig.m_eventLogAddress;
      this.m_R2HWConfig.m_eventLogSize = r2HWConfig.m_eventLogSize;
      this.m_R2HWConfig.m_licenseLogAddress = r2HWConfig.m_licenseLogAddress;
      this.m_R2HWConfig.m_licenseLogSize = r2HWConfig.m_licenseLogSize;
      this.m_R2HWConfig.m_databaseAddress = r2HWConfig.m_databaseAddress;
      this.m_R2HWConfig.m_databaseSize = r2HWConfig.m_databaseSize;
      this.m_R2HWConfig.m_hardwareFlagsLow64 = r2HWConfig.m_hardwareFlagsLow64;
      this.m_R2HWConfig.m_hardwareFlagsHigh64 = r2HWConfig.m_hardwareFlagsHigh64;
      this.m_R2HWConfig.m_firmwareFlags = r2HWConfig.m_firmwareFlags;
      this.m_R2HWConfig.m_deviceId = r2HWConfig.m_deviceId;
      this.m_R2HWConfig.m_hardwareProductNumber = r2HWConfig.m_hardwareProductNumber;
      this.m_R2HWConfig.m_astrx2XtalTrim = r2HWConfig.m_astrx2XtalTrim;
      this.m_R2HWConfig.m_hardwareProductRevision = r2HWConfig.m_hardwareProductRevision;
      this.m_R2HWConfig.m_manualBGBackDateOffset = r2HWConfig.m_manualBGBackDateOffset;
      this.m_R2HWConfig.m_receiverInstanceId = r2HWConfig.m_receiverInstanceId;
      this.m_R2HWConfig.m_minSlope = r2HWConfig.m_minSlope;
      this.m_R2HWConfig.m_maxSlope = r2HWConfig.m_maxSlope;
      this.m_R2HWConfig.m_minBaseline = r2HWConfig.m_minBaseline;
      this.m_R2HWConfig.m_maxBaseline = r2HWConfig.m_maxBaseline;
      this.m_R2HWConfig.m_lowAssumedBaseline = r2HWConfig.m_lowAssumedBaseline;
      this.m_R2HWConfig.m_midAssumedBaseline = r2HWConfig.m_midAssumedBaseline;
      this.m_R2HWConfig.m_highAssumedBaseline = r2HWConfig.m_highAssumedBaseline;
      this.m_R2HWConfig.m_minCounts = r2HWConfig.m_minCounts;
      this.m_R2HWConfig.m_maxCounts = r2HWConfig.m_maxCounts;
      this.m_R2HWConfig.m_absoluteEgvDeltaForMinimalAberration = r2HWConfig.m_absoluteEgvDeltaForMinimalAberration;
      this.m_R2HWConfig.m_percentageEgvDeltaForMinimalAberration = r2HWConfig.m_percentageEgvDeltaForMinimalAberration;
      this.m_R2HWConfig.m_absoluteEgvDeltaForSevereAberration = r2HWConfig.m_absoluteEgvDeltaForSevereAberration;
      this.m_R2HWConfig.m_percentageEgvDeltaForSevereAberration = r2HWConfig.m_percentageEgvDeltaForSevereAberration;
      this.m_R2HWConfig.m_countsAberrationWindow = r2HWConfig.m_countsAberrationWindow;
      this.m_R2HWConfig.m_residualAberrationWindow = r2HWConfig.m_residualAberrationWindow;
      this.m_R2HWConfig.m_egvAberrationWindow = r2HWConfig.m_egvAberrationWindow;
      this.m_R2HWConfig.m_calibratedAdcReading = r2HWConfig.m_calibratedAdcReading;
      this.m_R2HWConfig.m_maxAllowedCountsAberrations = r2HWConfig.m_maxAllowedCountsAberrations;
      this.m_R2HWConfig.m_maxAllowedResidualAberrations = r2HWConfig.m_maxAllowedResidualAberrations;
      this.m_R2HWConfig.m_maxAllowedEgvAberrations = r2HWConfig.m_maxAllowedEgvAberrations;
      this.m_R2HWConfig.m_maxAllowedPointsToGetInCal = r2HWConfig.m_maxAllowedPointsToGetInCal;
      this.m_R2HWConfig.m_percentageVenousAdjustment = r2HWConfig.m_percentageVenousAdjustment;
      this.m_R2HWConfig.m_maxTotalAberrations = r2HWConfig.m_maxTotalAberrations;
    }

    public HardwareConfiguration(R2HWConfigV6 r2HWConfig)
      : this(6U)
    {
      byte[] numArray = DataTools.ConvertObjectToBytes((object) r2HWConfig);
      byte[] bytes = DataTools.ConvertObjectToBytes((object) this.m_R2HWConfigV6);
      Array.Copy((Array) numArray, 0, (Array) bytes, 0, Marshal.OffsetOf(typeof (R2HWConfigV6), "m_reserved2").ToInt32());
      this.m_R2HWConfigV6 = (R2HWConfigV7) DataTools.ConvertBytesToObject(bytes, 0, typeof (R2HWConfigV7));
      this.m_R2HWConfig.m_configSignature = r2HWConfig.m_configSignature;
      this.m_R2HWConfig.m_configSize = r2HWConfig.m_configSize;
      this.m_R2HWConfig.m_configVersion = r2HWConfig.m_configVersion;
      this.m_R2HWConfig.m_biosCompatibilityNumber = r2HWConfig.m_biosCompatibilityNumber;
      this.m_R2HWConfig.m_errorLogAddress = r2HWConfig.m_errorLogAddress;
      this.m_R2HWConfig.m_errorLogSize = r2HWConfig.m_errorLogSize;
      this.m_R2HWConfig.m_eventLogAddress = r2HWConfig.m_eventLogAddress;
      this.m_R2HWConfig.m_eventLogSize = r2HWConfig.m_eventLogSize;
      this.m_R2HWConfig.m_licenseLogAddress = r2HWConfig.m_licenseLogAddress;
      this.m_R2HWConfig.m_licenseLogSize = r2HWConfig.m_licenseLogSize;
      this.m_R2HWConfig.m_databaseAddress = r2HWConfig.m_databaseAddress;
      this.m_R2HWConfig.m_databaseSize = r2HWConfig.m_databaseSize;
      this.m_R2HWConfig.m_hardwareFlagsLow64 = r2HWConfig.m_hardwareFlagsLow64;
      this.m_R2HWConfig.m_hardwareFlagsHigh64 = r2HWConfig.m_hardwareFlagsHigh64;
      this.m_R2HWConfig.m_firmwareFlags = r2HWConfig.m_firmwareFlags;
      this.m_R2HWConfig.m_deviceId = r2HWConfig.m_deviceId;
      this.m_R2HWConfig.m_hardwareProductNumber = r2HWConfig.m_hardwareProductNumber;
      this.m_R2HWConfig.m_astrx2XtalTrim = r2HWConfig.m_astrx2XtalTrim;
      this.m_R2HWConfig.m_hardwareProductRevision = r2HWConfig.m_hardwareProductRevision;
      this.m_R2HWConfig.m_manualBGBackDateOffset = r2HWConfig.m_manualBGBackDateOffset;
      this.m_R2HWConfig.m_receiverInstanceId = r2HWConfig.m_receiverInstanceId;
      this.m_R2HWConfig.m_minSlope = r2HWConfig.m_minSlope;
      this.m_R2HWConfig.m_maxSlope = r2HWConfig.m_maxSlope;
      this.m_R2HWConfig.m_minBaseline = r2HWConfig.m_minBaseline;
      this.m_R2HWConfig.m_maxBaseline = r2HWConfig.m_maxBaseline;
      this.m_R2HWConfig.m_lowAssumedBaseline = r2HWConfig.m_lowAssumedBaseline;
      this.m_R2HWConfig.m_midAssumedBaseline = r2HWConfig.m_midAssumedBaseline;
      this.m_R2HWConfig.m_highAssumedBaseline = r2HWConfig.m_highAssumedBaseline;
      this.m_R2HWConfig.m_minCounts = r2HWConfig.m_minCounts;
      this.m_R2HWConfig.m_maxCounts = r2HWConfig.m_maxCounts;
      this.m_R2HWConfig.m_absoluteEgvDeltaForMinimalAberration = r2HWConfig.m_absoluteEgvDeltaForMinimalAberration;
      this.m_R2HWConfig.m_percentageEgvDeltaForMinimalAberration = r2HWConfig.m_percentageEgvDeltaForMinimalAberration;
      this.m_R2HWConfig.m_absoluteEgvDeltaForSevereAberration = r2HWConfig.m_absoluteEgvDeltaForSevereAberration;
      this.m_R2HWConfig.m_percentageEgvDeltaForSevereAberration = r2HWConfig.m_percentageEgvDeltaForSevereAberration;
      this.m_R2HWConfig.m_countsAberrationWindow = r2HWConfig.m_countsAberrationWindow;
      this.m_R2HWConfig.m_residualAberrationWindow = r2HWConfig.m_residualAberrationWindow;
      this.m_R2HWConfig.m_egvAberrationWindow = r2HWConfig.m_egvAberrationWindow;
      this.m_R2HWConfig.m_calibratedAdcReading = r2HWConfig.m_calibratedAdcReading;
      this.m_R2HWConfig.m_maxAllowedCountsAberrations = r2HWConfig.m_maxAllowedCountsAberrations;
      this.m_R2HWConfig.m_maxAllowedResidualAberrations = r2HWConfig.m_maxAllowedResidualAberrations;
      this.m_R2HWConfig.m_maxAllowedEgvAberrations = r2HWConfig.m_maxAllowedEgvAberrations;
      this.m_R2HWConfig.m_maxAllowedPointsToGetInCal = r2HWConfig.m_maxAllowedPointsToGetInCal;
      this.m_R2HWConfig.m_percentageVenousAdjustment = r2HWConfig.m_percentageVenousAdjustment;
      this.m_R2HWConfig.m_maxTotalAberrations = r2HWConfig.m_maxTotalAberrations;
    }

    public HardwareConfiguration(R2HWConfigV5 r2HWConfig)
      : this(5U)
    {
      byte[] numArray = DataTools.ConvertObjectToBytes((object) r2HWConfig);
      byte[] bytes = DataTools.ConvertObjectToBytes((object) this.m_R2HWConfigV6);
      Array.Copy((Array) numArray, 0, (Array) bytes, 0, Marshal.OffsetOf(typeof (R2HWConfigV5), "m_reserved2").ToInt32());
      this.m_R2HWConfigV6 = (R2HWConfigV7) DataTools.ConvertBytesToObject(bytes, 0, typeof (R2HWConfigV7));
      this.m_R2HWConfig.m_configSignature = r2HWConfig.m_configSignature;
      this.m_R2HWConfig.m_configSize = r2HWConfig.m_configSize;
      this.m_R2HWConfig.m_configVersion = r2HWConfig.m_configVersion;
      this.m_R2HWConfig.m_biosCompatibilityNumber = r2HWConfig.m_biosCompatibilityNumber;
      this.m_R2HWConfig.m_errorLogAddress = r2HWConfig.m_errorLogAddress;
      this.m_R2HWConfig.m_errorLogSize = r2HWConfig.m_errorLogSize;
      this.m_R2HWConfig.m_eventLogAddress = r2HWConfig.m_eventLogAddress;
      this.m_R2HWConfig.m_eventLogSize = r2HWConfig.m_eventLogSize;
      this.m_R2HWConfig.m_licenseLogAddress = r2HWConfig.m_licenseLogAddress;
      this.m_R2HWConfig.m_licenseLogSize = r2HWConfig.m_licenseLogSize;
      this.m_R2HWConfig.m_databaseAddress = r2HWConfig.m_databaseAddress;
      this.m_R2HWConfig.m_databaseSize = r2HWConfig.m_databaseSize;
      this.m_R2HWConfig.m_hardwareFlagsLow64 = r2HWConfig.m_hardwareFlagsLow64;
      this.m_R2HWConfig.m_hardwareFlagsHigh64 = r2HWConfig.m_hardwareFlagsHigh64;
      this.m_R2HWConfig.m_firmwareFlags = r2HWConfig.m_firmwareFlags;
      this.m_R2HWConfig.m_deviceId = r2HWConfig.m_deviceId;
      this.m_R2HWConfig.m_hardwareProductNumber = r2HWConfig.m_hardwareProductNumber;
      this.m_R2HWConfig.m_astrx2XtalTrim = r2HWConfig.m_astrx2XtalTrim;
      this.m_R2HWConfig.m_hardwareProductRevision = r2HWConfig.m_hardwareProductRevision;
      this.m_R2HWConfig.m_manualBGBackDateOffset = r2HWConfig.m_manualBGBackDateOffset;
      this.m_R2HWConfig.m_receiverInstanceId = r2HWConfig.m_receiverInstanceId;
      this.m_R2HWConfig.m_minSlope = r2HWConfig.m_minSlope;
      this.m_R2HWConfig.m_maxSlope = r2HWConfig.m_maxSlope;
      this.m_R2HWConfig.m_minBaseline = r2HWConfig.m_minBaseline;
      this.m_R2HWConfig.m_maxBaseline = r2HWConfig.m_maxBaseline;
      this.m_R2HWConfig.m_lowAssumedBaseline = r2HWConfig.m_lowAssumedBaseline;
      this.m_R2HWConfig.m_midAssumedBaseline = r2HWConfig.m_midAssumedBaseline;
      this.m_R2HWConfig.m_highAssumedBaseline = r2HWConfig.m_highAssumedBaseline;
      this.m_R2HWConfig.m_minCounts = r2HWConfig.m_minCounts;
      this.m_R2HWConfig.m_maxCounts = r2HWConfig.m_maxCounts;
      this.m_R2HWConfig.m_absoluteEgvDeltaForMinimalAberration = r2HWConfig.m_absoluteEgvDeltaForMinimalAberration;
      this.m_R2HWConfig.m_percentageEgvDeltaForMinimalAberration = r2HWConfig.m_percentageEgvDeltaForMinimalAberration;
      this.m_R2HWConfig.m_absoluteEgvDeltaForSevereAberration = r2HWConfig.m_absoluteEgvDeltaForSevereAberration;
      this.m_R2HWConfig.m_percentageEgvDeltaForSevereAberration = r2HWConfig.m_percentageEgvDeltaForSevereAberration;
      this.m_R2HWConfig.m_countsAberrationWindow = r2HWConfig.m_countsAberrationWindow;
      this.m_R2HWConfig.m_residualAberrationWindow = r2HWConfig.m_residualAberrationWindow;
      this.m_R2HWConfig.m_egvAberrationWindow = r2HWConfig.m_egvAberrationWindow;
      this.m_R2HWConfig.m_calibratedAdcReading = r2HWConfig.m_calibratedAdcReading;
      this.m_R2HWConfig.m_maxAllowedCountsAberrations = r2HWConfig.m_maxAllowedCountsAberrations;
      this.m_R2HWConfig.m_maxAllowedResidualAberrations = r2HWConfig.m_maxAllowedResidualAberrations;
      this.m_R2HWConfig.m_maxAllowedEgvAberrations = r2HWConfig.m_maxAllowedEgvAberrations;
      this.m_R2HWConfig.m_maxAllowedPointsToGetInCal = r2HWConfig.m_maxAllowedPointsToGetInCal;
      this.m_R2HWConfig.m_percentageVenousAdjustment = r2HWConfig.m_percentageVenousAdjustment;
      this.m_R2HWConfig.m_maxTotalAberrations = r2HWConfig.m_maxTotalAberrations;
    }

    public HardwareConfiguration(R2HWConfigV4 r2HWConfig)
      : this(4U)
    {
      this.m_R2HWConfig.m_configSignature = r2HWConfig.m_configSignature;
      this.m_R2HWConfig.m_configSize = r2HWConfig.m_configSize;
      this.m_R2HWConfig.m_configVersion = r2HWConfig.m_configVersion;
      this.m_R2HWConfig.m_biosCompatibilityNumber = r2HWConfig.m_biosCompatibilityNumber;
      this.m_R2HWConfig.m_errorLogAddress = r2HWConfig.m_errorLogAddress;
      this.m_R2HWConfig.m_errorLogSize = r2HWConfig.m_errorLogSize;
      this.m_R2HWConfig.m_eventLogAddress = r2HWConfig.m_eventLogAddress;
      this.m_R2HWConfig.m_eventLogSize = r2HWConfig.m_eventLogSize;
      this.m_R2HWConfig.m_licenseLogAddress = r2HWConfig.m_licenseLogAddress;
      this.m_R2HWConfig.m_licenseLogSize = r2HWConfig.m_licenseLogSize;
      this.m_R2HWConfig.m_databaseAddress = r2HWConfig.m_databaseAddress;
      this.m_R2HWConfig.m_databaseSize = r2HWConfig.m_databaseSize;
      this.m_R2HWConfig.m_hardwareFlagsLow64 = r2HWConfig.m_hardwareFlagsLow64;
      this.m_R2HWConfig.m_hardwareFlagsHigh64 = r2HWConfig.m_hardwareFlagsHigh64;
      this.m_R2HWConfig.m_firmwareFlags = r2HWConfig.m_firmwareFlags;
      this.m_R2HWConfig.m_deviceId = r2HWConfig.m_deviceId;
      this.m_R2HWConfig.m_hardwareProductNumber = r2HWConfig.m_hardwareProductNumber;
      this.m_R2HWConfig.m_astrx2XtalTrim = r2HWConfig.m_astrx2XtalTrim;
      this.m_R2HWConfig.m_hardwareProductRevision = r2HWConfig.m_hardwareProductRevision;
      this.m_R2HWConfig.m_manualBGBackDateOffset = r2HWConfig.m_manualBGBackDateOffset;
      this.m_R2HWConfig.m_receiverInstanceId = r2HWConfig.m_receiverInstanceId;
      this.m_R2HWConfig.m_minSlope = r2HWConfig.m_minSlope;
      this.m_R2HWConfig.m_maxSlope = r2HWConfig.m_maxSlope;
      this.m_R2HWConfig.m_minBaseline = r2HWConfig.m_minBaseline;
      this.m_R2HWConfig.m_maxBaseline = r2HWConfig.m_maxBaseline;
      this.m_R2HWConfig.m_lowAssumedBaseline = r2HWConfig.m_lowAssumedBaseline;
      this.m_R2HWConfig.m_midAssumedBaseline = r2HWConfig.m_midAssumedBaseline;
      this.m_R2HWConfig.m_highAssumedBaseline = r2HWConfig.m_highAssumedBaseline;
      this.m_R2HWConfig.m_minCounts = r2HWConfig.m_minCounts;
      this.m_R2HWConfig.m_maxCounts = r2HWConfig.m_maxCounts;
      this.m_R2HWConfig.m_absoluteEgvDeltaForMinimalAberration = r2HWConfig.m_absoluteEgvDeltaForMinimalAberration;
      this.m_R2HWConfig.m_percentageEgvDeltaForMinimalAberration = r2HWConfig.m_percentageEgvDeltaForMinimalAberration;
      this.m_R2HWConfig.m_absoluteEgvDeltaForSevereAberration = r2HWConfig.m_absoluteEgvDeltaForSevereAberration;
      this.m_R2HWConfig.m_percentageEgvDeltaForSevereAberration = r2HWConfig.m_percentageEgvDeltaForSevereAberration;
      this.m_R2HWConfig.m_countsAberrationWindow = r2HWConfig.m_countsAberrationWindow;
      this.m_R2HWConfig.m_residualAberrationWindow = r2HWConfig.m_residualAberrationWindow;
      this.m_R2HWConfig.m_egvAberrationWindow = r2HWConfig.m_egvAberrationWindow;
      this.m_R2HWConfig.m_calibratedAdcReading = r2HWConfig.m_calibratedAdcReading;
      this.m_R2HWConfig.m_maxAllowedCountsAberrations = r2HWConfig.m_maxAllowedCountsAberrations;
      this.m_R2HWConfig.m_maxAllowedResidualAberrations = r2HWConfig.m_maxAllowedResidualAberrations;
      this.m_R2HWConfig.m_maxAllowedEgvAberrations = r2HWConfig.m_maxAllowedEgvAberrations;
      this.m_R2HWConfig.m_maxAllowedPointsToGetInCal = r2HWConfig.m_maxAllowedPointsToGetInCal;
      this.m_R2HWConfig.m_percentageVenousAdjustment = r2HWConfig.m_percentageVenousAdjustment;
      this.m_R2HWConfig.m_maxTotalAberrations = r2HWConfig.m_maxTotalAberrations;
      this.m_R2HWConfigV6.m_configSignature = r2HWConfig.m_configSignature;
      this.m_R2HWConfigV6.m_configSize = r2HWConfig.m_configSize;
      this.m_R2HWConfigV6.m_configVersion = r2HWConfig.m_configVersion;
      this.m_R2HWConfigV6.m_biosCompatibilityNumber = r2HWConfig.m_biosCompatibilityNumber;
      this.m_R2HWConfigV6.m_errorLogAddress = r2HWConfig.m_errorLogAddress;
      this.m_R2HWConfigV6.m_errorLogSize = r2HWConfig.m_errorLogSize;
      this.m_R2HWConfigV6.m_eventLogAddress = r2HWConfig.m_eventLogAddress;
      this.m_R2HWConfigV6.m_eventLogSize = r2HWConfig.m_eventLogSize;
      this.m_R2HWConfigV6.m_licenseLogAddress = r2HWConfig.m_licenseLogAddress;
      this.m_R2HWConfigV6.m_licenseLogSize = r2HWConfig.m_licenseLogSize;
      this.m_R2HWConfigV6.m_databaseAddress = r2HWConfig.m_databaseAddress;
      this.m_R2HWConfigV6.m_databaseSize = r2HWConfig.m_databaseSize;
      this.m_R2HWConfigV6.m_hardwareFlagsLow64 = r2HWConfig.m_hardwareFlagsLow64;
      this.m_R2HWConfigV6.m_hardwareFlagsHigh64 = r2HWConfig.m_hardwareFlagsHigh64;
      this.m_R2HWConfigV6.m_firmwareFlags = r2HWConfig.m_firmwareFlags;
      this.m_R2HWConfigV6.m_deviceId = r2HWConfig.m_deviceId;
      this.m_R2HWConfigV6.m_hardwareProductNumber = r2HWConfig.m_hardwareProductNumber;
      this.m_R2HWConfigV6.m_astrx2XtalTrim = r2HWConfig.m_astrx2XtalTrim;
      this.m_R2HWConfigV6.m_hardwareProductRevision = r2HWConfig.m_hardwareProductRevision;
      this.m_R2HWConfigV6.m_manualBGBackDateOffset = r2HWConfig.m_manualBGBackDateOffset;
      this.m_R2HWConfigV6.m_receiverInstanceId = r2HWConfig.m_receiverInstanceId;
      this.m_R2HWConfigV6.m_minSlope = r2HWConfig.m_minSlope;
      this.m_R2HWConfigV6.m_maxSlope = r2HWConfig.m_maxSlope;
      this.m_R2HWConfigV6.m_minBaseline = r2HWConfig.m_minBaseline;
      this.m_R2HWConfigV6.m_maxBaseline = r2HWConfig.m_maxBaseline;
      this.m_R2HWConfigV6.m_lowAssumedBaseline = r2HWConfig.m_lowAssumedBaseline;
      this.m_R2HWConfigV6.m_midAssumedBaseline = r2HWConfig.m_midAssumedBaseline;
      this.m_R2HWConfigV6.m_highAssumedBaseline = r2HWConfig.m_highAssumedBaseline;
      this.m_R2HWConfigV6.m_minCounts = r2HWConfig.m_minCounts;
      this.m_R2HWConfigV6.m_maxCounts = r2HWConfig.m_maxCounts;
      this.m_R2HWConfigV6.m_residualPercentageForMinimalAberration = r2HWConfig.m_residualPercentageForMinimalAberration;
      this.m_R2HWConfigV6.m_residualPercentageForSevereAberration = r2HWConfig.m_residualPercentageForSevereAberration;
      this.m_R2HWConfigV6.m_deltaResidualPercentageForMinimalAberration = r2HWConfig.m_deltaResidualPercentageForMinimalAberration;
      this.m_R2HWConfigV6.m_deltaResidualPercentageForSevereAberration = r2HWConfig.m_deltaResidualPercentageForSevereAberration;
      this.m_R2HWConfigV6.m_absoluteEgvDeltaForMinimalAberration = r2HWConfig.m_absoluteEgvDeltaForMinimalAberration;
      this.m_R2HWConfigV6.m_percentageEgvDeltaForMinimalAberration = r2HWConfig.m_percentageEgvDeltaForMinimalAberration;
      this.m_R2HWConfigV6.m_absoluteEgvDeltaForSevereAberration = r2HWConfig.m_absoluteEgvDeltaForSevereAberration;
      this.m_R2HWConfigV6.m_percentageEgvDeltaForSevereAberration = r2HWConfig.m_percentageEgvDeltaForSevereAberration;
      this.m_R2HWConfigV6.m_countsAberrationWindow = r2HWConfig.m_countsAberrationWindow;
      this.m_R2HWConfigV6.m_residualAberrationWindow = r2HWConfig.m_residualAberrationWindow;
      this.m_R2HWConfigV6.m_deltaResidualAberrationWindow = r2HWConfig.m_deltaResidualAberrationWindow;
      this.m_R2HWConfigV6.m_egvAberrationWindow = r2HWConfig.m_egvAberrationWindow;
      this.m_R2HWConfigV6.m_calibratedAdcReading = r2HWConfig.m_calibratedAdcReading;
      this.m_R2HWConfigV6.m_maxAllowedCountsAberrations = r2HWConfig.m_maxAllowedCountsAberrations;
      this.m_R2HWConfigV6.m_maxAllowedDeltaResidualAberrations = r2HWConfig.m_maxAllowedDeltaResidualAberrations;
      this.m_R2HWConfigV6.m_maxAllowedResidualAberrations = r2HWConfig.m_maxAllowedResidualAberrations;
      this.m_R2HWConfigV6.m_maxAllowedEgvAberrations = r2HWConfig.m_maxAllowedEgvAberrations;
      this.m_R2HWConfigV6.m_maxAllowedPointsToGetInCal = r2HWConfig.m_maxAllowedPointsToGetInCal;
      this.m_R2HWConfigV6.m_percentageVenousAdjustment = r2HWConfig.m_percentageVenousAdjustment;
      this.m_R2HWConfigV6.m_maxTotalAberrationsType1 = r2HWConfig.m_maxTotalAberrationsType1;
      this.m_R2HWConfigV6.m_maxTotalAberrationsType2 = r2HWConfig.m_maxTotalAberrationsType2;
      this.m_R2HWConfigV6.m_maxTotalAberrationsType3 = r2HWConfig.m_maxTotalAberrationsType3;
      this.m_R2HWConfigV6.m_maxTotalAberrations = r2HWConfig.m_maxTotalAberrations;
      this.m_R2HWConfigV6.m_imageInstanceId = r2HWConfig.m_imageInstanceId;
      this.m_R2HWConfigV6.m_imageExtension = r2HWConfig.m_imageExtension;
    }

    public HardwareConfiguration(R2HWConfigV3 r2HWConfig)
      : this(3U)
    {
      this.m_R2HWConfig = r2HWConfig;
      this.m_R2HWConfigV6.m_configSignature = r2HWConfig.m_configSignature;
      this.m_R2HWConfigV6.m_configSize = r2HWConfig.m_configSize;
      this.m_R2HWConfigV6.m_configVersion = r2HWConfig.m_configVersion;
      this.m_R2HWConfigV6.m_biosCompatibilityNumber = r2HWConfig.m_biosCompatibilityNumber;
      this.m_R2HWConfigV6.m_errorLogAddress = r2HWConfig.m_errorLogAddress;
      this.m_R2HWConfigV6.m_errorLogSize = r2HWConfig.m_errorLogSize;
      this.m_R2HWConfigV6.m_eventLogAddress = r2HWConfig.m_eventLogAddress;
      this.m_R2HWConfigV6.m_eventLogSize = r2HWConfig.m_eventLogSize;
      this.m_R2HWConfigV6.m_licenseLogAddress = r2HWConfig.m_licenseLogAddress;
      this.m_R2HWConfigV6.m_licenseLogSize = r2HWConfig.m_licenseLogSize;
      this.m_R2HWConfigV6.m_databaseAddress = r2HWConfig.m_databaseAddress;
      this.m_R2HWConfigV6.m_databaseSize = r2HWConfig.m_databaseSize;
      this.m_R2HWConfigV6.m_hardwareFlagsLow64 = r2HWConfig.m_hardwareFlagsLow64;
      this.m_R2HWConfigV6.m_hardwareFlagsHigh64 = r2HWConfig.m_hardwareFlagsHigh64;
      this.m_R2HWConfigV6.m_firmwareFlags = r2HWConfig.m_firmwareFlags;
      this.m_R2HWConfigV6.m_deviceId = r2HWConfig.m_deviceId;
      this.m_R2HWConfigV6.m_hardwareProductNumber = r2HWConfig.m_hardwareProductNumber;
      this.m_R2HWConfigV6.m_astrx2XtalTrim = r2HWConfig.m_astrx2XtalTrim;
      this.m_R2HWConfigV6.m_hardwareProductRevision = r2HWConfig.m_hardwareProductRevision;
      this.m_R2HWConfigV6.m_manualBGBackDateOffset = r2HWConfig.m_manualBGBackDateOffset;
      this.m_R2HWConfigV6.m_receiverInstanceId = r2HWConfig.m_receiverInstanceId;
      this.m_R2HWConfigV6.m_minSlope = r2HWConfig.m_minSlope;
      this.m_R2HWConfigV6.m_maxSlope = r2HWConfig.m_maxSlope;
      this.m_R2HWConfigV6.m_minBaseline = r2HWConfig.m_minBaseline;
      this.m_R2HWConfigV6.m_maxBaseline = r2HWConfig.m_maxBaseline;
      this.m_R2HWConfigV6.m_lowAssumedBaseline = r2HWConfig.m_lowAssumedBaseline;
      this.m_R2HWConfigV6.m_midAssumedBaseline = r2HWConfig.m_midAssumedBaseline;
      this.m_R2HWConfigV6.m_highAssumedBaseline = r2HWConfig.m_highAssumedBaseline;
      this.m_R2HWConfigV6.m_minCounts = r2HWConfig.m_minCounts;
      this.m_R2HWConfigV6.m_maxCounts = r2HWConfig.m_maxCounts;
      this.m_R2HWConfigV6.m_absoluteEgvDeltaForMinimalAberration = r2HWConfig.m_absoluteEgvDeltaForMinimalAberration;
      this.m_R2HWConfigV6.m_percentageEgvDeltaForMinimalAberration = r2HWConfig.m_percentageEgvDeltaForMinimalAberration;
      this.m_R2HWConfigV6.m_absoluteEgvDeltaForSevereAberration = r2HWConfig.m_absoluteEgvDeltaForSevereAberration;
      this.m_R2HWConfigV6.m_percentageEgvDeltaForSevereAberration = r2HWConfig.m_percentageEgvDeltaForSevereAberration;
      this.m_R2HWConfigV6.m_countsAberrationWindow = r2HWConfig.m_countsAberrationWindow;
      this.m_R2HWConfigV6.m_residualAberrationWindow = r2HWConfig.m_residualAberrationWindow;
      this.m_R2HWConfigV6.m_egvAberrationWindow = r2HWConfig.m_egvAberrationWindow;
      this.m_R2HWConfigV6.m_calibratedAdcReading = r2HWConfig.m_calibratedAdcReading;
      this.m_R2HWConfigV6.m_maxAllowedCountsAberrations = r2HWConfig.m_maxAllowedCountsAberrations;
      this.m_R2HWConfigV6.m_maxAllowedResidualAberrations = r2HWConfig.m_maxAllowedResidualAberrations;
      this.m_R2HWConfigV6.m_maxAllowedEgvAberrations = r2HWConfig.m_maxAllowedEgvAberrations;
      this.m_R2HWConfigV6.m_maxAllowedPointsToGetInCal = r2HWConfig.m_maxAllowedPointsToGetInCal;
      this.m_R2HWConfigV6.m_percentageVenousAdjustment = r2HWConfig.m_percentageVenousAdjustment;
      this.m_R2HWConfigV6.m_maxTotalAberrations = r2HWConfig.m_maxTotalAberrations;
    }

    public HardwareConfiguration(R2HWConfigV2 r2HWConfig)
      : this(2U)
    {
      this.m_R2HWConfig.m_configSignature = r2HWConfig.m_configSignature;
      this.m_R2HWConfig.m_configSize = r2HWConfig.m_configSize;
      this.m_R2HWConfig.m_configVersion = r2HWConfig.m_configVersion;
      this.m_R2HWConfig.m_biosCompatibilityNumber = r2HWConfig.m_biosCompatibilityNumber;
      this.m_R2HWConfig.m_errorLogAddress = r2HWConfig.m_errorLogAddress;
      this.m_R2HWConfig.m_errorLogSize = r2HWConfig.m_errorLogSize;
      this.m_R2HWConfig.m_eventLogAddress = r2HWConfig.m_eventLogAddress;
      this.m_R2HWConfig.m_eventLogSize = r2HWConfig.m_eventLogSize;
      this.m_R2HWConfig.m_licenseLogAddress = r2HWConfig.m_licenseLogAddress;
      this.m_R2HWConfig.m_licenseLogSize = r2HWConfig.m_licenseLogSize;
      this.m_R2HWConfig.m_databaseAddress = r2HWConfig.m_databaseAddress;
      this.m_R2HWConfig.m_databaseSize = r2HWConfig.m_databaseSize;
      this.m_R2HWConfig.m_hardwareFlagsLow64 = r2HWConfig.m_hardwareFlagsLow64;
      this.m_R2HWConfig.m_hardwareFlagsHigh64 = r2HWConfig.m_hardwareFlagsHigh64;
      this.m_R2HWConfig.m_firmwareFlags = r2HWConfig.m_firmwareFlags;
      this.m_R2HWConfig.m_deviceId = r2HWConfig.m_deviceId;
      this.m_R2HWConfig.m_hardwareProductNumber = r2HWConfig.m_hardwareProductNumber;
      this.m_R2HWConfig.m_astrx2XtalTrim = r2HWConfig.m_astrx2XtalTrim;
      this.m_R2HWConfig.m_hardwareProductRevision = r2HWConfig.m_hardwareProductRevision;
      this.m_R2HWConfig.m_receiverInstanceId = r2HWConfig.m_receiverInstanceId;
      this.m_R2HWConfig.m_minSlope = r2HWConfig.m_minSlope;
      this.m_R2HWConfig.m_maxSlope = r2HWConfig.m_maxSlope;
      this.m_R2HWConfig.m_minBaseline = r2HWConfig.m_minBaseline;
      this.m_R2HWConfig.m_maxBaseline = r2HWConfig.m_maxBaseline;
      this.m_R2HWConfig.m_lowAssumedBaseline = r2HWConfig.m_lowAssumedBaseline;
      this.m_R2HWConfig.m_midAssumedBaseline = r2HWConfig.m_midAssumedBaseline;
      this.m_R2HWConfig.m_highAssumedBaseline = r2HWConfig.m_highAssumedBaseline;
      this.m_R2HWConfig.m_minCounts = r2HWConfig.m_minCounts;
      this.m_R2HWConfig.m_maxCounts = r2HWConfig.m_maxCounts;
      this.m_R2HWConfig.m_residualCountsForMinimalAberration = r2HWConfig.m_residualCountsForMinimalAberration;
      this.m_R2HWConfig.m_residualCountsForSevereAberration = r2HWConfig.m_residualCountsForSevereAberration;
      this.m_R2HWConfig.m_absoluteEgvDeltaForMinimalAberration = r2HWConfig.m_absoluteEgvDeltaForMinimalAberration;
      this.m_R2HWConfig.m_percentageEgvDeltaForMinimalAberration = r2HWConfig.m_percentageEgvDeltaForMinimalAberration;
      this.m_R2HWConfig.m_absoluteEgvDeltaForSevereAberration = r2HWConfig.m_absoluteEgvDeltaForSevereAberration;
      this.m_R2HWConfig.m_percentageEgvDeltaForSevereAberration = r2HWConfig.m_percentageEgvDeltaForSevereAberration;
      this.m_R2HWConfig.m_countsAberrationWindow = r2HWConfig.m_countsAberrationWindow;
      this.m_R2HWConfig.m_residualAberrationWindow = r2HWConfig.m_residualAberrationWindow;
      this.m_R2HWConfig.m_egvAberrationWindow = r2HWConfig.m_egvAberrationWindow;
      this.m_R2HWConfig.m_calibratedAdcReading = r2HWConfig.m_calibratedAdcReading;
      this.m_R2HWConfig.m_maxAllowedCountsAberrations = r2HWConfig.m_maxAllowedCountsAberrations;
      this.m_R2HWConfig.m_maxAllowedResidualAberrations = r2HWConfig.m_maxAllowedResidualAberrations;
      this.m_R2HWConfig.m_maxAllowedEgvAberrations = r2HWConfig.m_maxAllowedEgvAberrations;
      this.m_R2HWConfig.m_maxAllowedPointsToGetInCal = r2HWConfig.m_maxAllowedPointsToGetInCal;
      this.m_R2HWConfigV6.m_configSignature = r2HWConfig.m_configSignature;
      this.m_R2HWConfigV6.m_configSize = r2HWConfig.m_configSize;
      this.m_R2HWConfigV6.m_configVersion = r2HWConfig.m_configVersion;
      this.m_R2HWConfigV6.m_biosCompatibilityNumber = r2HWConfig.m_biosCompatibilityNumber;
      this.m_R2HWConfigV6.m_errorLogAddress = r2HWConfig.m_errorLogAddress;
      this.m_R2HWConfigV6.m_errorLogSize = r2HWConfig.m_errorLogSize;
      this.m_R2HWConfigV6.m_eventLogAddress = r2HWConfig.m_eventLogAddress;
      this.m_R2HWConfigV6.m_eventLogSize = r2HWConfig.m_eventLogSize;
      this.m_R2HWConfigV6.m_licenseLogAddress = r2HWConfig.m_licenseLogAddress;
      this.m_R2HWConfigV6.m_licenseLogSize = r2HWConfig.m_licenseLogSize;
      this.m_R2HWConfigV6.m_databaseAddress = r2HWConfig.m_databaseAddress;
      this.m_R2HWConfigV6.m_databaseSize = r2HWConfig.m_databaseSize;
      this.m_R2HWConfigV6.m_hardwareFlagsLow64 = r2HWConfig.m_hardwareFlagsLow64;
      this.m_R2HWConfigV6.m_hardwareFlagsHigh64 = r2HWConfig.m_hardwareFlagsHigh64;
      this.m_R2HWConfigV6.m_firmwareFlags = r2HWConfig.m_firmwareFlags;
      this.m_R2HWConfigV6.m_deviceId = r2HWConfig.m_deviceId;
      this.m_R2HWConfigV6.m_hardwareProductNumber = r2HWConfig.m_hardwareProductNumber;
      this.m_R2HWConfigV6.m_astrx2XtalTrim = r2HWConfig.m_astrx2XtalTrim;
      this.m_R2HWConfigV6.m_hardwareProductRevision = r2HWConfig.m_hardwareProductRevision;
      this.m_R2HWConfigV6.m_receiverInstanceId = r2HWConfig.m_receiverInstanceId;
      this.m_R2HWConfigV6.m_minSlope = r2HWConfig.m_minSlope;
      this.m_R2HWConfigV6.m_maxSlope = r2HWConfig.m_maxSlope;
      this.m_R2HWConfigV6.m_minBaseline = r2HWConfig.m_minBaseline;
      this.m_R2HWConfigV6.m_maxBaseline = r2HWConfig.m_maxBaseline;
      this.m_R2HWConfigV6.m_lowAssumedBaseline = r2HWConfig.m_lowAssumedBaseline;
      this.m_R2HWConfigV6.m_midAssumedBaseline = r2HWConfig.m_midAssumedBaseline;
      this.m_R2HWConfigV6.m_highAssumedBaseline = r2HWConfig.m_highAssumedBaseline;
      this.m_R2HWConfigV6.m_minCounts = r2HWConfig.m_minCounts;
      this.m_R2HWConfigV6.m_maxCounts = r2HWConfig.m_maxCounts;
      this.m_R2HWConfigV6.m_absoluteEgvDeltaForMinimalAberration = r2HWConfig.m_absoluteEgvDeltaForMinimalAberration;
      this.m_R2HWConfigV6.m_percentageEgvDeltaForMinimalAberration = r2HWConfig.m_percentageEgvDeltaForMinimalAberration;
      this.m_R2HWConfigV6.m_absoluteEgvDeltaForSevereAberration = r2HWConfig.m_absoluteEgvDeltaForSevereAberration;
      this.m_R2HWConfigV6.m_percentageEgvDeltaForSevereAberration = r2HWConfig.m_percentageEgvDeltaForSevereAberration;
      this.m_R2HWConfigV6.m_countsAberrationWindow = r2HWConfig.m_countsAberrationWindow;
      this.m_R2HWConfigV6.m_residualAberrationWindow = r2HWConfig.m_residualAberrationWindow;
      this.m_R2HWConfigV6.m_egvAberrationWindow = r2HWConfig.m_egvAberrationWindow;
      this.m_R2HWConfigV6.m_calibratedAdcReading = r2HWConfig.m_calibratedAdcReading;
      this.m_R2HWConfigV6.m_maxAllowedCountsAberrations = r2HWConfig.m_maxAllowedCountsAberrations;
      this.m_R2HWConfigV6.m_maxAllowedResidualAberrations = r2HWConfig.m_maxAllowedResidualAberrations;
      this.m_R2HWConfigV6.m_maxAllowedEgvAberrations = r2HWConfig.m_maxAllowedEgvAberrations;
      this.m_R2HWConfigV6.m_maxAllowedPointsToGetInCal = r2HWConfig.m_maxAllowedPointsToGetInCal;
    }

    public HardwareConfiguration(R2HWConfig r2HWConfig)
      : this(1U)
    {
      this.ReceiverId = r2HWConfig.m_deviceId;
      this.m_R2HWConfig.m_hardwareFlagsLow64 = this.m_R2HWConfigV6.m_hardwareFlagsLow64 = r2HWConfig.m_hardwareFlagsLow64;
      this.m_R2HWConfig.m_hardwareFlagsHigh64 = this.m_R2HWConfigV6.m_hardwareFlagsHigh64 = r2HWConfig.m_hardwareFlagsHigh64;
      this.m_R2HWConfig.m_firmwareFlags = this.m_R2HWConfigV6.m_firmwareFlags = r2HWConfig.m_firmwareFlags;
      this.XtalTrim = r2HWConfig.m_astrx2XtalTrim;
      this.MaxSlope = r2HWConfig.m_maxSlope;
      this.MinSlope = r2HWConfig.m_minSlope;
      this.MinBaseline = r2HWConfig.m_minBaseline;
      this.MaxBaseline = r2HWConfig.m_maxBaseline;
      this.MinCounts = r2HWConfig.m_minCounts;
      this.MaxCounts = r2HWConfig.m_maxCounts;
      this.LowAssumedBaseline = r2HWConfig.m_lowAssumedBaseline;
      this.MidAssumedBaseline = r2HWConfig.m_midAssumedBaseline;
      this.HighAssumedBaseline = r2HWConfig.m_highAssumedBaseline;
      this.Calibrated5VoltAdcReading = r2HWConfig.m_calibratedAdcReading;
      this.HardwareProductNumber = r2HWConfig.m_hardwareProductNumber;
      this.HardwareProductRevision = r2HWConfig.m_hardwareProductRevision;
      this.ReceiverInstanceId = r2HWConfig.m_receiverInstanceId;
    }

    public HardwareConfiguration(XR2HardwareConfig xR2HardwareConfig)
      : this(xR2HardwareConfig.ConfigurationVersion)
    {
      this.ReceiverId = xR2HardwareConfig.ReceiverId;
      this.XtalTrim = xR2HardwareConfig.XtalTrim;
      this.MaxSlope = xR2HardwareConfig.MaxSlope;
      this.MinSlope = xR2HardwareConfig.MinSlope;
      this.MinBaseline = xR2HardwareConfig.MinBaseline;
      this.MaxBaseline = xR2HardwareConfig.MaxBaseline;
      this.MinCounts = xR2HardwareConfig.MinCounts;
      this.MaxCounts = xR2HardwareConfig.MaxCounts;
      this.LowAssumedBaseline = xR2HardwareConfig.LowAssumedBaseline;
      this.MidAssumedBaseline = xR2HardwareConfig.MidAssumedBaseline;
      this.HighAssumedBaseline = xR2HardwareConfig.HighAssumedBaseline;
      this.Calibrated5VoltAdcReading = xR2HardwareConfig.Calibrated5VoltAdcReading;
      this.HardwareProductNumber = xR2HardwareConfig.HardwareProductNumber;
      this.HardwareProductRevision = xR2HardwareConfig.HardwareProductRevision;
      this.ReceiverInstanceId = xR2HardwareConfig.ReceiverInstanceId;
      if ((int) this.ConfigurationVersion == 2 || (int) this.ConfigurationVersion == 3 || ((int) this.ConfigurationVersion == 4 || (int) this.ConfigurationVersion == 5) || ((int) this.ConfigurationVersion == 6 || (int) this.ConfigurationVersion == 7))
      {
        this.BiosCompatibilityNumber = xR2HardwareConfig.BiosCompatibilityNumber;
        this.Signature = xR2HardwareConfig.Signature;
        this.m_R2HWConfig.m_configSize = this.m_R2HWConfigV6.m_configSize = xR2HardwareConfig.Size;
        if ((int) this.ConfigurationVersion == 2 || (int) this.ConfigurationVersion == 3)
        {
          this.ResidualCountsForMinimalAberration = xR2HardwareConfig.ResidualCountsForMinimalAberration;
          this.ResidualCountsForSevereAberration = xR2HardwareConfig.ResidualCountsForSevereAberration;
        }
        this.AbsoluteEgvDeltaForMinimalAberration = xR2HardwareConfig.AbsoluteEgvDeltaForMinimalAberration;
        this.PercentageEgvDeltaForMinimalAberration = xR2HardwareConfig.PercentageEgvDeltaForMinimalAberration;
        this.AbsoluteEgvDeltaForSevereAberration = xR2HardwareConfig.AbsoluteEgvDeltaForSevereAberration;
        this.PercentageEgvDeltaForSevereAberration = xR2HardwareConfig.PercentageEgvDeltaForSevereAberration;
        this.CountsAberrationWindow = xR2HardwareConfig.CountsAberrationWindow;
        this.ResidualAberrationWindow = xR2HardwareConfig.ResidualAberrationWindow;
        this.EgvAberrationWindow = xR2HardwareConfig.EgvAberrationWindow;
        this.MaxAllowedCountsAberrations = xR2HardwareConfig.MaxAllowedCountsAberrations;
        this.MaxAllowedResidualAberrations = xR2HardwareConfig.MaxAllowedResidualAberrations;
        this.MaxAllowedEgvAberrations = xR2HardwareConfig.MaxAllowedEgvAberrations;
        this.MaxAllowedPointsToGetInCal = xR2HardwareConfig.MaxAllowedPointsToGetInCal;
        this.ErrorLogAddress = xR2HardwareConfig.ErrorLogAddress;
        this.ErrorLogSize = xR2HardwareConfig.ErrorLogSize;
        this.EventLogAddress = xR2HardwareConfig.EventLogAddress;
        this.EventLogSize = xR2HardwareConfig.EventLogSize;
        this.LicenseLogAddress = xR2HardwareConfig.LicenseLogAddress;
        this.LicenseLogSize = xR2HardwareConfig.LicenseLogSize;
        this.DatabaseAddress = xR2HardwareConfig.DatabaseAddress;
        this.DatabaseSize = xR2HardwareConfig.DatabaseSize;
      }
      if ((int) this.ConfigurationVersion == 3)
      {
        this.PercentageVenousAdjustment = xR2HardwareConfig.PercentageVenousAdjustment;
        this.CullPercentage = xR2HardwareConfig.CullPercentage;
        this.CullDelta = xR2HardwareConfig.CullDelta;
        this.MaxTotalAberrations = xR2HardwareConfig.MaxTotalAberrations;
        this.ManualBGBackDateOffset = xR2HardwareConfig.ManualBGBackDateOffset;
      }
      if ((int) this.ConfigurationVersion == 4 || (int) this.ConfigurationVersion == 5 || ((int) this.ConfigurationVersion == 6 || (int) this.ConfigurationVersion == 7))
      {
        this.PercentageVenousAdjustment = xR2HardwareConfig.PercentageVenousAdjustment;
        this.MaxTotalAberrations = xR2HardwareConfig.MaxTotalAberrations;
        this.ManualBGBackDateOffset = xR2HardwareConfig.ManualBGBackDateOffset;
        this.ResidualPercentageForMinimalAberration = xR2HardwareConfig.ResidualPercentageForMinimalAberration;
        this.ResidualPercentageForSevereAberration = xR2HardwareConfig.ResidualPercentageForSevereAberration;
        this.DeltaResidualPercentageForMinimalAberration = xR2HardwareConfig.DeltaResidualPercentageForMinimalAberration;
        this.DeltaResidualPercentageForSevereAberration = xR2HardwareConfig.DeltaResidualPercentageForSevereAberration;
        this.DeltaResidualAberrationWindow = xR2HardwareConfig.DeltaResidualAberrationWindow;
        this.MaxAllowedDeltaResidualAberrations = xR2HardwareConfig.MaxAllowedDeltaResidualAberrations;
        this.MaxTotalAberrationsType1 = xR2HardwareConfig.MaxTotalAberrationsType1;
        this.MaxTotalAberrationsType2 = xR2HardwareConfig.MaxTotalAberrationsType2;
        this.MaxTotalAberrationsType3 = xR2HardwareConfig.MaxTotalAberrationsType3;
        this.ImageInstanceId = xR2HardwareConfig.ImageInstanceId;
        this.ImageExtension = xR2HardwareConfig.ImageExtension;
      }
      if ((int) this.ConfigurationVersion == 5 || (int) this.ConfigurationVersion == 6 || (int) this.ConfigurationVersion == 7)
      {
        this.HighWedgeHighAssumedBaseline = xR2HardwareConfig.HighWedgeHighAssumedBaseline;
        this.HighWedgeLowAssumedBaseline = xR2HardwareConfig.HighWedgeLowAssumedBaseline;
        this.HighWedgeMaxBaseline = xR2HardwareConfig.HighWedgeMaxBaseline;
        this.HighWedgeMaxSlope = xR2HardwareConfig.HighWedgeMaxSlope;
        this.HighWedgeMidAssumedBaseline = xR2HardwareConfig.HighWedgeMidAssumedBaseline;
        this.HighWedgeMinBaseline = xR2HardwareConfig.HighWedgeMinBaseline;
        this.HighWedgeMinSlope = xR2HardwareConfig.HighWedgeMinSlope;
        this.LowWedgeHighAssumedBaseline = xR2HardwareConfig.LowWedgeHighAssumedBaseline;
        this.LowWedgeLowAssumedBaseline = xR2HardwareConfig.LowWedgeLowAssumedBaseline;
        this.LowWedgeMaxBaseline = xR2HardwareConfig.LowWedgeMaxBaseline;
        this.LowWedgeMaxSlope = xR2HardwareConfig.LowWedgeMaxSlope;
        this.LowWedgeMidAssumedBaseline = xR2HardwareConfig.LowWedgeMidAssumedBaseline;
        this.LowWedgeMinBaseline = xR2HardwareConfig.LowWedgeMinBaseline;
        this.LowWedgeMinSlope = xR2HardwareConfig.LowWedgeMinSlope;
        this.RMSTimeWindow = xR2HardwareConfig.RMSTimeWindow;
        this.NumberPointsPowerCalculation = xR2HardwareConfig.NumberPointsPowerCalculation;
        this.DeltaResidualNoiseThreshold = xR2HardwareConfig.DeltaResidualNoiseThreshold;
        this.ResidualNoiseThreshold = xR2HardwareConfig.ResidualNoiseThreshold;
      }
      if ((int) this.ConfigurationVersion == 6 || (int) this.ConfigurationVersion == 7)
      {
        this.AbsoluteAberrationWindow = xR2HardwareConfig.AbsoluteAberrationWindow;
        this.PowerAberrationWindow = xR2HardwareConfig.PowerAberrationWindow;
        this.AbsoluteResidualAberrationCounts = xR2HardwareConfig.AbsoluteResidualAberrationCounts;
        this.AbsoluteDeltaResidualAberrationCounts = xR2HardwareConfig.AbsoluteDeltaResidualAberrationCounts;
        this.PowerAberrationResidualPercentage = xR2HardwareConfig.PowerAberrationResidualPercentage;
        this.PowerAberrationDeltaResidualPercentage = xR2HardwareConfig.PowerAberrationDeltaResidualPercentage;
        this.MaxAllowedAbsoluteAberrations = xR2HardwareConfig.MaxAllowedAbsoluteAberrations;
        this.MaxAllowedPowerAberrations = xR2HardwareConfig.MaxAllowedPowerAberrations;
        this.ImageSubExtension = xR2HardwareConfig.ImageSubExtension;
        this.UpperAdjustBoundPercent = xR2HardwareConfig.UpperAdjustBoundPercent;
        this.LowerAdjustBoundPercent = xR2HardwareConfig.LowerAdjustBoundPercent;
        this.MaxUpwardAdjustPercent = xR2HardwareConfig.MaxUpwardAdjustPercent;
        this.MaxDownwardAdjustPercent = xR2HardwareConfig.MaxDownwardAdjustPercent;
      }
      if ((int) this.ConfigurationVersion == 7)
      {
        this.AlphaLight = xR2HardwareConfig.AlphaLight;
        this.AlphaMedium = xR2HardwareConfig.AlphaMedium;
        this.AlphaSevere = xR2HardwareConfig.AlphaSevere;
        this.AlphaSignal = xR2HardwareConfig.AlphaSignal;
        this.ThresholdLight = xR2HardwareConfig.ThresholdLight;
        this.ThresholdMedium = xR2HardwareConfig.ThresholdMedium;
        this.ThresholdSevere = xR2HardwareConfig.ThresholdSevere;
      }
      ulong num1;
      ulong num2;
      if (xR2HardwareConfig.HasAttribute("HardwareFlagsLow64"))
      {
        num1 = xR2HardwareConfig.HardwareFlagsLow64;
        num2 = xR2HardwareConfig.HardwareFlagsHigh64;
      }
      else
      {
        num1 = (ulong) xR2HardwareConfig.HardwareFlags;
        num2 = 0UL;
      }
      ulong num3 = ~num1;
      ulong num4 = ~num2;
      uint num5 = ~(!xR2HardwareConfig.HasAttribute("FirmwareFlags32") ? (uint) xR2HardwareConfig.FirmwareFlags : xR2HardwareConfig.FirmwareFlags32);
      this.m_R2HWConfig.m_hardwareFlagsLow64 = this.m_R2HWConfigV6.m_hardwareFlagsLow64 = num3;
      this.m_R2HWConfig.m_hardwareFlagsHigh64 = this.m_R2HWConfigV6.m_hardwareFlagsHigh64 = num4;
      this.m_R2HWConfig.m_firmwareFlags = this.m_R2HWConfigV6.m_firmwareFlags = num5;
    }

    public R2HWConfig GetR2HWConfigV1()
    {
      R2HWConfig r2HwConfig = new R2HWConfig();
      r2HwConfig.m_deviceId = this.m_R2HWConfig.m_deviceId;
      r2HwConfig.m_hardwareFlagsLow64 = this.m_R2HWConfig.m_hardwareFlagsLow64;
      r2HwConfig.m_hardwareFlagsHigh64 = this.m_R2HWConfig.m_hardwareFlagsHigh64;
      r2HwConfig.m_firmwareFlags = this.m_R2HWConfig.m_firmwareFlags;
      r2HwConfig.m_astrx2XtalTrim = this.m_R2HWConfig.m_astrx2XtalTrim;
      r2HwConfig.m_maxSlope = this.m_R2HWConfig.m_maxSlope;
      r2HwConfig.m_minSlope = this.m_R2HWConfig.m_minSlope;
      r2HwConfig.m_minBaseline = this.m_R2HWConfig.m_minBaseline;
      r2HwConfig.m_maxBaseline = this.m_R2HWConfig.m_maxBaseline;
      r2HwConfig.m_minCounts = this.m_R2HWConfig.m_minCounts;
      r2HwConfig.m_maxCounts = this.m_R2HWConfig.m_maxCounts;
      r2HwConfig.m_lowAssumedBaseline = this.m_R2HWConfig.m_lowAssumedBaseline;
      r2HwConfig.m_midAssumedBaseline = this.m_R2HWConfig.m_midAssumedBaseline;
      r2HwConfig.m_highAssumedBaseline = this.m_R2HWConfig.m_highAssumedBaseline;
      r2HwConfig.m_calibratedAdcReading = this.m_R2HWConfig.m_calibratedAdcReading;
      r2HwConfig.m_hardwareProductNumber = this.m_R2HWConfig.m_hardwareProductNumber;
      r2HwConfig.m_hardwareProductRevision = this.m_R2HWConfig.m_hardwareProductRevision;
      r2HwConfig.m_receiverInstanceId = this.m_R2HWConfig.m_receiverInstanceId;
      r2HwConfig.UpdateCrc();
      return r2HwConfig;
    }

    public R2HWConfigV2 GetR2HWConfigV2()
    {
      R2HWConfigV2 r2HwConfigV2_1 = new R2HWConfigV2();
      R2HWConfigV2 r2HwConfigV2_2 = (R2HWConfigV2) DataTools.ConvertBytesToObject(DataTools.ConvertObjectToBytes((object) this.m_R2HWConfig), 0, typeof (R2HWConfigV2));
      r2HwConfigV2_2.m_configVersion = 2U;
      for (int index = 0; index < r2HwConfigV2_2.m_reserved2.Length; ++index)
        r2HwConfigV2_2.m_reserved2[index] = (byte) 0;
      return r2HwConfigV2_2;
    }

    public R2HWConfigV3 GetR2HWConfigV3()
    {
      return this.m_R2HWConfig;
    }

    public R2HWConfigV4 GetR2HWConfigV4()
    {
      R2HWConfigV4 r2HwConfigV4_1 = new R2HWConfigV4();
      byte[] numArray = DataTools.ConvertObjectToBytes((object) this.m_R2HWConfigV6);
      byte[] bytes = DataTools.ConvertObjectToBytes((object) r2HwConfigV4_1);
      Array.Copy((Array) numArray, 0, (Array) bytes, 0, Marshal.OffsetOf(typeof (R2HWConfigV4), "m_reserved2").ToInt32());
      R2HWConfigV4 r2HwConfigV4_2 = (R2HWConfigV4) DataTools.ConvertBytesToObject(bytes, 0, typeof (R2HWConfigV4));
      r2HwConfigV4_2.m_configVersion = 4U;
      return r2HwConfigV4_2;
    }

    public R2HWConfigV5 GetR2HWConfigV5()
    {
      R2HWConfigV5 r2HwConfigV5_1 = new R2HWConfigV5();
      byte[] numArray = DataTools.ConvertObjectToBytes((object) this.m_R2HWConfigV6);
      byte[] bytes = DataTools.ConvertObjectToBytes((object) r2HwConfigV5_1);
      Array.Copy((Array) numArray, 0, (Array) bytes, 0, Marshal.OffsetOf(typeof (R2HWConfigV5), "m_reserved2").ToInt32());
      R2HWConfigV5 r2HwConfigV5_2 = (R2HWConfigV5) DataTools.ConvertBytesToObject(bytes, 0, typeof (R2HWConfigV5));
      r2HwConfigV5_2.m_configVersion = 5U;
      return r2HwConfigV5_2;
    }

    public R2HWConfigV6 GetR2HWConfigV6()
    {
      R2HWConfigV6 r2HwConfigV6_1 = new R2HWConfigV6();
      byte[] numArray = DataTools.ConvertObjectToBytes((object) this.m_R2HWConfigV6);
      byte[] bytes = DataTools.ConvertObjectToBytes((object) r2HwConfigV6_1);
      Array.Copy((Array) numArray, 0, (Array) bytes, 0, Marshal.OffsetOf(typeof (R2HWConfigV6), "m_reserved2").ToInt32());
      R2HWConfigV6 r2HwConfigV6_2 = (R2HWConfigV6) DataTools.ConvertBytesToObject(bytes, 0, typeof (R2HWConfigV6));
      r2HwConfigV6_2.m_configVersion = 6U;
      return r2HwConfigV6_2;
    }

    public R2HWConfigV7 GetR2HWConfigV7()
    {
      return this.m_R2HWConfigV6;
    }

    public XR2HardwareConfig GetXR2HardwareConfig()
    {
      if ((int) this.ConfigurationVersion == 4 || (int) this.ConfigurationVersion == 5 || ((int) this.ConfigurationVersion == 6 || (int) this.ConfigurationVersion == 7))
        return this.DoGetXR2HardwareConfigV7();
      else
        return this.DoGetXR2HardwareConfig();
    }

    private XR2HardwareConfig DoGetXR2HardwareConfig()
    {
      XR2HardwareConfig xr2HardwareConfig = new XR2HardwareConfig(this.ConfigurationVersion);
      ulong num1 = ~this.m_R2HWConfig.m_hardwareFlagsLow64;
      ulong num2 = ~this.m_R2HWConfig.m_hardwareFlagsHigh64;
      R2HardwareFlags r2HardwareFlags = R2HardwareFlags.Unknown;
      foreach (int num3 in Enum.GetValues(typeof (R2HardwareFlags)))
      {
        if (((long) num1 & (long) num3) == (long) num3)
          r2HardwareFlags |= (R2HardwareFlags) num3;
      }
      uint num4 = ~this.m_R2HWConfig.m_firmwareFlags;
      R2FirmwareFlags r2FirmwareFlags = R2FirmwareFlags.Unknown;
      foreach (int num3 in Enum.GetValues(typeof (R2FirmwareFlags)))
      {
        if (((int) num4 & num3) == num3)
          r2FirmwareFlags |= (R2FirmwareFlags) num3;
      }
      xr2HardwareConfig.HardwareFlags = r2HardwareFlags;
      xr2HardwareConfig.HardwareFlagsLow64 = num1;
      xr2HardwareConfig.HardwareFlagsHigh64 = num2;
      xr2HardwareConfig.FirmwareFlags = r2FirmwareFlags;
      xr2HardwareConfig.FirmwareFlags32 = num4;
      xr2HardwareConfig.ReceiverId = this.m_R2HWConfig.m_deviceId;
      xr2HardwareConfig.XtalTrim = this.m_R2HWConfig.m_astrx2XtalTrim;
      xr2HardwareConfig.MaxSlope = this.m_R2HWConfig.m_maxSlope;
      xr2HardwareConfig.MinSlope = this.m_R2HWConfig.m_minSlope;
      xr2HardwareConfig.MinBaseline = this.m_R2HWConfig.m_minBaseline;
      xr2HardwareConfig.MaxBaseline = this.m_R2HWConfig.m_maxBaseline;
      xr2HardwareConfig.MinCounts = this.m_R2HWConfig.m_minCounts;
      xr2HardwareConfig.MaxCounts = this.m_R2HWConfig.m_maxCounts;
      xr2HardwareConfig.LowAssumedBaseline = this.m_R2HWConfig.m_lowAssumedBaseline;
      xr2HardwareConfig.MidAssumedBaseline = this.m_R2HWConfig.m_midAssumedBaseline;
      xr2HardwareConfig.HighAssumedBaseline = this.m_R2HWConfig.m_highAssumedBaseline;
      xr2HardwareConfig.Calibrated5VoltAdcReading = this.m_R2HWConfig.m_calibratedAdcReading;
      xr2HardwareConfig.HardwareProductNumber = this.m_R2HWConfig.m_hardwareProductNumber;
      xr2HardwareConfig.HardwareProductRevision = this.m_R2HWConfig.m_hardwareProductRevision;
      xr2HardwareConfig.ReceiverInstanceId = this.m_R2HWConfig.m_receiverInstanceId;
      if ((int) this.ConfigurationVersion == 2 || (int) this.ConfigurationVersion == 3)
      {
        xr2HardwareConfig.BiosCompatibilityNumber = this.m_R2HWConfig.m_biosCompatibilityNumber;
        xr2HardwareConfig.Signature = this.m_R2HWConfig.m_configSignature;
        xr2HardwareConfig.Size = this.m_R2HWConfig.m_configSize;
        xr2HardwareConfig.ResidualCountsForMinimalAberration = this.m_R2HWConfig.m_residualCountsForMinimalAberration;
        xr2HardwareConfig.ResidualCountsForSevereAberration = this.m_R2HWConfig.m_residualCountsForSevereAberration;
        xr2HardwareConfig.AbsoluteEgvDeltaForMinimalAberration = this.m_R2HWConfig.m_absoluteEgvDeltaForMinimalAberration;
        xr2HardwareConfig.PercentageEgvDeltaForMinimalAberration = this.m_R2HWConfig.m_percentageEgvDeltaForMinimalAberration;
        xr2HardwareConfig.AbsoluteEgvDeltaForSevereAberration = this.m_R2HWConfig.m_absoluteEgvDeltaForSevereAberration;
        xr2HardwareConfig.PercentageEgvDeltaForSevereAberration = this.m_R2HWConfig.m_percentageEgvDeltaForSevereAberration;
        xr2HardwareConfig.CountsAberrationWindow = this.m_R2HWConfig.m_countsAberrationWindow;
        xr2HardwareConfig.ResidualAberrationWindow = this.m_R2HWConfig.m_residualAberrationWindow;
        xr2HardwareConfig.EgvAberrationWindow = this.m_R2HWConfig.m_egvAberrationWindow;
        xr2HardwareConfig.MaxAllowedCountsAberrations = this.m_R2HWConfig.m_maxAllowedCountsAberrations;
        xr2HardwareConfig.MaxAllowedResidualAberrations = this.m_R2HWConfig.m_maxAllowedResidualAberrations;
        xr2HardwareConfig.MaxAllowedEgvAberrations = this.m_R2HWConfig.m_maxAllowedEgvAberrations;
        xr2HardwareConfig.MaxAllowedPointsToGetInCal = this.m_R2HWConfig.m_maxAllowedPointsToGetInCal;
        xr2HardwareConfig.ErrorLogAddress = this.m_R2HWConfig.m_errorLogAddress;
        xr2HardwareConfig.ErrorLogSize = this.m_R2HWConfig.m_errorLogSize;
        xr2HardwareConfig.EventLogAddress = this.m_R2HWConfig.m_eventLogAddress;
        xr2HardwareConfig.EventLogSize = this.m_R2HWConfig.m_eventLogSize;
        xr2HardwareConfig.LicenseLogAddress = this.m_R2HWConfig.m_licenseLogAddress;
        xr2HardwareConfig.LicenseLogSize = this.m_R2HWConfig.m_licenseLogSize;
        xr2HardwareConfig.DatabaseAddress = this.m_R2HWConfig.m_databaseAddress;
        xr2HardwareConfig.DatabaseSize = this.m_R2HWConfig.m_databaseSize;
      }
      if ((int) this.ConfigurationVersion == 3)
      {
        xr2HardwareConfig.PercentageVenousAdjustment = this.m_R2HWConfig.m_percentageVenousAdjustment;
        xr2HardwareConfig.CullPercentage = this.m_R2HWConfig.m_cullPercentage;
        xr2HardwareConfig.CullDelta = this.m_R2HWConfig.m_cullDelta;
        xr2HardwareConfig.MaxTotalAberrations = this.m_R2HWConfig.m_maxTotalAberrations;
        xr2HardwareConfig.ManualBGBackDateOffset = this.m_R2HWConfig.m_manualBGBackDateOffset;
      }
      return xr2HardwareConfig;
    }

    private XR2HardwareConfig DoGetXR2HardwareConfigV7()
    {
      XR2HardwareConfig xr2HardwareConfig = new XR2HardwareConfig(this.ConfigurationVersion);
      ulong num1 = ~this.m_R2HWConfigV6.m_hardwareFlagsLow64;
      ulong num2 = ~this.m_R2HWConfigV6.m_hardwareFlagsHigh64;
      R2HardwareFlags r2HardwareFlags = R2HardwareFlags.Unknown;
      foreach (int num3 in Enum.GetValues(typeof (R2HardwareFlags)))
      {
        if (((long) num1 & (long) num3) == (long) num3)
          r2HardwareFlags |= (R2HardwareFlags) num3;
      }
      uint num4 = ~this.m_R2HWConfigV6.m_firmwareFlags;
      R2FirmwareFlags r2FirmwareFlags = R2FirmwareFlags.Unknown;
      foreach (int num3 in Enum.GetValues(typeof (R2FirmwareFlags)))
      {
        if (((int) num4 & num3) == num3)
          r2FirmwareFlags |= (R2FirmwareFlags) num3;
      }
      xr2HardwareConfig.HardwareFlags = r2HardwareFlags;
      xr2HardwareConfig.HardwareFlagsLow64 = num1;
      xr2HardwareConfig.HardwareFlagsHigh64 = num2;
      xr2HardwareConfig.FirmwareFlags = r2FirmwareFlags;
      xr2HardwareConfig.FirmwareFlags32 = num4;
      xr2HardwareConfig.ReceiverId = this.m_R2HWConfigV6.m_deviceId;
      xr2HardwareConfig.XtalTrim = this.m_R2HWConfigV6.m_astrx2XtalTrim;
      xr2HardwareConfig.MaxSlope = this.m_R2HWConfigV6.m_maxSlope;
      xr2HardwareConfig.MinSlope = this.m_R2HWConfigV6.m_minSlope;
      xr2HardwareConfig.MinBaseline = this.m_R2HWConfigV6.m_minBaseline;
      xr2HardwareConfig.MaxBaseline = this.m_R2HWConfigV6.m_maxBaseline;
      xr2HardwareConfig.MinCounts = this.m_R2HWConfigV6.m_minCounts;
      xr2HardwareConfig.MaxCounts = this.m_R2HWConfigV6.m_maxCounts;
      xr2HardwareConfig.LowAssumedBaseline = this.m_R2HWConfigV6.m_lowAssumedBaseline;
      xr2HardwareConfig.MidAssumedBaseline = this.m_R2HWConfigV6.m_midAssumedBaseline;
      xr2HardwareConfig.HighAssumedBaseline = this.m_R2HWConfigV6.m_highAssumedBaseline;
      xr2HardwareConfig.Calibrated5VoltAdcReading = this.m_R2HWConfigV6.m_calibratedAdcReading;
      xr2HardwareConfig.HardwareProductNumber = this.m_R2HWConfigV6.m_hardwareProductNumber;
      xr2HardwareConfig.HardwareProductRevision = this.m_R2HWConfigV6.m_hardwareProductRevision;
      xr2HardwareConfig.ReceiverInstanceId = this.m_R2HWConfigV6.m_receiverInstanceId;
      xr2HardwareConfig.BiosCompatibilityNumber = this.m_R2HWConfigV6.m_biosCompatibilityNumber;
      xr2HardwareConfig.Signature = this.m_R2HWConfigV6.m_configSignature;
      xr2HardwareConfig.Size = this.m_R2HWConfigV6.m_configSize;
      xr2HardwareConfig.AbsoluteEgvDeltaForMinimalAberration = this.m_R2HWConfigV6.m_absoluteEgvDeltaForMinimalAberration;
      xr2HardwareConfig.PercentageEgvDeltaForMinimalAberration = this.m_R2HWConfigV6.m_percentageEgvDeltaForMinimalAberration;
      xr2HardwareConfig.AbsoluteEgvDeltaForSevereAberration = this.m_R2HWConfigV6.m_absoluteEgvDeltaForSevereAberration;
      xr2HardwareConfig.PercentageEgvDeltaForSevereAberration = this.m_R2HWConfigV6.m_percentageEgvDeltaForSevereAberration;
      xr2HardwareConfig.CountsAberrationWindow = this.m_R2HWConfigV6.m_countsAberrationWindow;
      xr2HardwareConfig.ResidualAberrationWindow = this.m_R2HWConfigV6.m_residualAberrationWindow;
      xr2HardwareConfig.EgvAberrationWindow = this.m_R2HWConfigV6.m_egvAberrationWindow;
      xr2HardwareConfig.MaxAllowedCountsAberrations = this.m_R2HWConfigV6.m_maxAllowedCountsAberrations;
      xr2HardwareConfig.MaxAllowedResidualAberrations = this.m_R2HWConfigV6.m_maxAllowedResidualAberrations;
      xr2HardwareConfig.MaxAllowedEgvAberrations = this.m_R2HWConfigV6.m_maxAllowedEgvAberrations;
      xr2HardwareConfig.MaxAllowedPointsToGetInCal = this.m_R2HWConfigV6.m_maxAllowedPointsToGetInCal;
      xr2HardwareConfig.ErrorLogAddress = this.m_R2HWConfigV6.m_errorLogAddress;
      xr2HardwareConfig.ErrorLogSize = this.m_R2HWConfigV6.m_errorLogSize;
      xr2HardwareConfig.EventLogAddress = this.m_R2HWConfigV6.m_eventLogAddress;
      xr2HardwareConfig.EventLogSize = this.m_R2HWConfigV6.m_eventLogSize;
      xr2HardwareConfig.LicenseLogAddress = this.m_R2HWConfigV6.m_licenseLogAddress;
      xr2HardwareConfig.LicenseLogSize = this.m_R2HWConfigV6.m_licenseLogSize;
      xr2HardwareConfig.DatabaseAddress = this.m_R2HWConfigV6.m_databaseAddress;
      xr2HardwareConfig.DatabaseSize = this.m_R2HWConfigV6.m_databaseSize;
      xr2HardwareConfig.PercentageVenousAdjustment = this.m_R2HWConfigV6.m_percentageVenousAdjustment;
      xr2HardwareConfig.MaxTotalAberrations = this.m_R2HWConfigV6.m_maxTotalAberrations;
      xr2HardwareConfig.ManualBGBackDateOffset = this.m_R2HWConfigV6.m_manualBGBackDateOffset;
      xr2HardwareConfig.ResidualPercentageForMinimalAberration = this.m_R2HWConfigV6.m_residualPercentageForMinimalAberration;
      xr2HardwareConfig.ResidualPercentageForSevereAberration = this.m_R2HWConfigV6.m_residualPercentageForSevereAberration;
      xr2HardwareConfig.DeltaResidualPercentageForMinimalAberration = this.m_R2HWConfigV6.m_deltaResidualPercentageForMinimalAberration;
      xr2HardwareConfig.DeltaResidualPercentageForSevereAberration = this.m_R2HWConfigV6.m_deltaResidualPercentageForSevereAberration;
      xr2HardwareConfig.DeltaResidualAberrationWindow = this.m_R2HWConfigV6.m_deltaResidualAberrationWindow;
      xr2HardwareConfig.MaxAllowedDeltaResidualAberrations = this.m_R2HWConfigV6.m_maxAllowedDeltaResidualAberrations;
      xr2HardwareConfig.MaxTotalAberrationsType1 = this.m_R2HWConfigV6.m_maxTotalAberrationsType1;
      xr2HardwareConfig.MaxTotalAberrationsType2 = this.m_R2HWConfigV6.m_maxTotalAberrationsType2;
      xr2HardwareConfig.MaxTotalAberrationsType3 = this.m_R2HWConfigV6.m_maxTotalAberrationsType3;
      xr2HardwareConfig.ImageInstanceId = this.m_R2HWConfigV6.m_imageInstanceId;
      xr2HardwareConfig.ImageExtension = this.m_R2HWConfigV6.m_imageExtension;
      if ((int) this.ConfigurationVersion == 5 || (int) this.ConfigurationVersion == 6 || (int) this.ConfigurationVersion == 7)
      {
        xr2HardwareConfig.HighWedgeHighAssumedBaseline = this.m_R2HWConfigV6.m_highWedgeHighAssumedBaseline;
        xr2HardwareConfig.HighWedgeLowAssumedBaseline = this.m_R2HWConfigV6.m_highWedgeLowAssumedBaseline;
        xr2HardwareConfig.HighWedgeMaxBaseline = this.m_R2HWConfigV6.m_highWedgeMaxBaseline;
        xr2HardwareConfig.HighWedgeMaxSlope = this.m_R2HWConfigV6.m_highWedgeMaxSlope;
        xr2HardwareConfig.HighWedgeMidAssumedBaseline = this.m_R2HWConfigV6.m_highWedgeMidAssumedBaseline;
        xr2HardwareConfig.HighWedgeMinBaseline = this.m_R2HWConfigV6.m_highWedgeMinBaseline;
        xr2HardwareConfig.HighWedgeMinSlope = this.m_R2HWConfigV6.m_highWedgeMinSlope;
        xr2HardwareConfig.LowWedgeHighAssumedBaseline = this.m_R2HWConfigV6.m_lowWedgeHighAssumedBaseline;
        xr2HardwareConfig.LowWedgeLowAssumedBaseline = this.m_R2HWConfigV6.m_lowWedgeLowAssumedBaseline;
        xr2HardwareConfig.LowWedgeMaxBaseline = this.m_R2HWConfigV6.m_lowWedgeMaxBaseline;
        xr2HardwareConfig.LowWedgeMaxSlope = this.m_R2HWConfigV6.m_lowWedgeMaxSlope;
        xr2HardwareConfig.LowWedgeMidAssumedBaseline = this.m_R2HWConfigV6.m_lowWedgeMidAssumedBaseline;
        xr2HardwareConfig.LowWedgeMinBaseline = this.m_R2HWConfigV6.m_lowWedgeMinBaseline;
        xr2HardwareConfig.LowWedgeMinSlope = this.m_R2HWConfigV6.m_lowWedgeMinSlope;
        xr2HardwareConfig.RMSTimeWindow = this.m_R2HWConfigV6.m_rmsTimeWindow;
        xr2HardwareConfig.NumberPointsPowerCalculation = this.m_R2HWConfigV6.m_numberPointsPowerCalculation;
        xr2HardwareConfig.DeltaResidualNoiseThreshold = this.m_R2HWConfigV6.m_deltaResidualNoiseThreshold;
        xr2HardwareConfig.ResidualNoiseThreshold = this.m_R2HWConfigV6.m_residualNoiseThreshold;
      }
      if ((int) this.ConfigurationVersion == 6 || (int) this.ConfigurationVersion == 7)
      {
        xr2HardwareConfig.AbsoluteAberrationWindow = this.m_R2HWConfigV6.m_absoluteAberrationWindow;
        xr2HardwareConfig.PowerAberrationWindow = this.m_R2HWConfigV6.m_powerAberrationWindow;
        xr2HardwareConfig.AbsoluteResidualAberrationCounts = this.m_R2HWConfigV6.m_absoluteResidualAberrationCounts;
        xr2HardwareConfig.AbsoluteDeltaResidualAberrationCounts = this.m_R2HWConfigV6.m_absoluteDeltaResidualAberrationCounts;
        xr2HardwareConfig.PowerAberrationResidualPercentage = this.m_R2HWConfigV6.m_powerAberrationResidualPercentage;
        xr2HardwareConfig.PowerAberrationDeltaResidualPercentage = this.m_R2HWConfigV6.m_powerAberrationDeltaResidualPercentage;
        xr2HardwareConfig.MaxAllowedAbsoluteAberrations = this.m_R2HWConfigV6.m_maxAllowedAbsoluteAberrations;
        xr2HardwareConfig.MaxAllowedPowerAberrations = this.m_R2HWConfigV6.m_maxAllowedPowerAberrations;
        xr2HardwareConfig.ImageSubExtension = this.m_R2HWConfigV6.m_imageSubExtension;
        xr2HardwareConfig.UpperAdjustBoundPercent = this.m_R2HWConfigV6.m_upperAdjustBoundPercent;
        xr2HardwareConfig.LowerAdjustBoundPercent = this.m_R2HWConfigV6.m_lowerAdjustBoundPercent;
        xr2HardwareConfig.MaxUpwardAdjustPercent = this.m_R2HWConfigV6.m_maxUpwardAdjustPercent;
        xr2HardwareConfig.MaxDownwardAdjustPercent = this.m_R2HWConfigV6.m_maxDownwardAdjustPercent;
      }
      if ((int) this.ConfigurationVersion == 7)
      {
        xr2HardwareConfig.AlphaLight = this.m_R2HWConfigV6.m_alphaLight;
        xr2HardwareConfig.AlphaMedium = this.m_R2HWConfigV6.m_alphaMedium;
        xr2HardwareConfig.AlphaSevere = this.m_R2HWConfigV6.m_alphaSevere;
        xr2HardwareConfig.AlphaSignal = this.m_R2HWConfigV6.m_alphaSignal;
        xr2HardwareConfig.ThresholdLight = this.m_R2HWConfigV6.m_thresholdLight;
        xr2HardwareConfig.ThresholdMedium = this.m_R2HWConfigV6.m_thresholdMedium;
        xr2HardwareConfig.ThresholdSevere = this.m_R2HWConfigV6.m_thresholdSevere;
      }
      return xr2HardwareConfig;
    }
  }
}

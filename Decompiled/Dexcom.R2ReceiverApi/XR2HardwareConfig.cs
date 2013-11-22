// Type: Dexcom.R2Receiver.XR2HardwareConfig
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using Dexcom.Common;
using System;
using System.Runtime.Serialization;
using System.Security;
using System.Xml;
using System.Xml.Serialization;

namespace Dexcom.R2Receiver
{
  [Serializable]
  public class XR2HardwareConfig : XObject, ISerializable
  {
    public const string Tag = "HardwareConfig";

    [XmlAttribute]
    public uint ReceiverId
    {
      get
      {
        return this.GetAttributeAsUInt("ReceiverId");
      }
      set
      {
        this.SetAttribute("ReceiverId", value);
      }
    }

    [XmlAttribute]
    public R2HardwareFlags HardwareFlags
    {
      get
      {
        return (R2HardwareFlags) this.GetAttributeAsEnum("HardwareFlags", typeof (R2HardwareFlags));
      }
      set
      {
        this.SetAttribute("HardwareFlags", ((object) value).ToString());
      }
    }

    [XmlAttribute]
    public ulong HardwareFlagsLow64
    {
      get
      {
        return this.GetAttributeAsULong("HardwareFlagsLow64");
      }
      set
      {
        this.SetAttribute("HardwareFlagsLow64", value);
      }
    }

    [XmlAttribute]
    public ulong HardwareFlagsHigh64
    {
      get
      {
        return this.GetAttributeAsULong("HardwareFlagsHigh64");
      }
      set
      {
        this.SetAttribute("HardwareFlagsHigh64", value);
      }
    }

    [XmlAttribute]
    public R2FirmwareFlags FirmwareFlags
    {
      get
      {
        return (R2FirmwareFlags) this.GetAttributeAsEnum("FirmwareFlags", typeof (R2FirmwareFlags));
      }
      set
      {
        this.SetAttribute("FirmwareFlags", ((object) value).ToString());
      }
    }

    [XmlAttribute]
    public uint FirmwareFlags32
    {
      get
      {
        return this.GetAttributeAsUInt("FirmwareFlags32");
      }
      set
      {
        this.SetAttribute("FirmwareFlags32", value);
      }
    }

    [XmlAttribute]
    public byte XtalTrim
    {
      get
      {
        return this.GetAttributeAsByte("XtalTrim");
      }
      set
      {
        this.SetAttribute("XtalTrim", value);
      }
    }

    [XmlAttribute]
    public uint MaxSlope
    {
      get
      {
        return this.GetAttributeAsUInt("MaxSlope");
      }
      set
      {
        this.SetAttribute("MaxSlope", value);
      }
    }

    [XmlAttribute]
    public uint MinSlope
    {
      get
      {
        return this.GetAttributeAsUInt("MinSlope");
      }
      set
      {
        this.SetAttribute("MinSlope", value);
      }
    }

    [XmlAttribute]
    public int MinBaseline
    {
      get
      {
        return this.GetAttributeAsInt("MinBaseline");
      }
      set
      {
        this.SetAttribute("MinBaseline", value);
      }
    }

    [XmlAttribute]
    public int MaxBaseline
    {
      get
      {
        return this.GetAttributeAsInt("MaxBaseline");
      }
      set
      {
        this.SetAttribute("MaxBaseline", value);
      }
    }

    [XmlAttribute]
    public uint MinCounts
    {
      get
      {
        return this.GetAttributeAsUInt("MinCounts");
      }
      set
      {
        this.SetAttribute("MinCounts", value);
      }
    }

    [XmlAttribute]
    public uint MaxCounts
    {
      get
      {
        return this.GetAttributeAsUInt("MaxCounts");
      }
      set
      {
        this.SetAttribute("MaxCounts", value);
      }
    }

    [XmlAttribute]
    public int LowAssumedBaseline
    {
      get
      {
        return this.GetAttributeAsInt("LowAssumedBaseline");
      }
      set
      {
        this.SetAttribute("LowAssumedBaseline", value);
      }
    }

    [XmlAttribute]
    public int MidAssumedBaseline
    {
      get
      {
        return this.GetAttributeAsInt("MidAssumedBaseline");
      }
      set
      {
        this.SetAttribute("MidAssumedBaseline", value);
      }
    }

    [XmlAttribute]
    public int HighAssumedBaseline
    {
      get
      {
        return this.GetAttributeAsInt("HighAssumedBaseline");
      }
      set
      {
        this.SetAttribute("HighAssumedBaseline", value);
      }
    }

    [XmlAttribute]
    public ushort Calibrated5VoltAdcReading
    {
      get
      {
        return this.GetAttributeAsUShort("Calibrated5VoltAdcReading");
      }
      set
      {
        this.SetAttribute("Calibrated5VoltAdcReading", value);
      }
    }

    [XmlAttribute]
    public uint HardwareProductNumber
    {
      get
      {
        return this.GetAttributeAsUInt("HardwareProductNumber");
      }
      set
      {
        this.SetAttribute("HardwareProductNumber", value);
      }
    }

    [XmlAttribute]
    public byte HardwareProductRevision
    {
      get
      {
        return this.GetAttributeAsByte("HardwareProductRevision");
      }
      set
      {
        this.SetAttribute("HardwareProductRevision", value);
      }
    }

    [XmlAttribute]
    public byte ManualBGBackDateOffset
    {
      get
      {
        if (this.HasAttribute("ManualBGBackDateOffset"))
          return this.GetAttributeAsByte("ManualBGBackDateOffset");
        else
          return byte.MaxValue;
      }
      set
      {
        this.SetAttribute("ManualBGBackDateOffset", value);
      }
    }

    [XmlAttribute]
    public Guid ReceiverInstanceId
    {
      get
      {
        return this.GetAttributeAsGuid("ReceiverInstanceId");
      }
      set
      {
        this.SetAttribute("ReceiverInstanceId", value);
      }
    }

    [XmlAttribute]
    public uint ConfigurationVersion
    {
      get
      {
        if (this.HasAttribute("ConfigurationVersion"))
          return this.GetAttributeAsUInt("ConfigurationVersion");
        else
          return 1U;
      }
      set
      {
        this.SetAttribute("ConfigurationVersion", value);
      }
    }

    [XmlAttribute]
    public string Signature
    {
      get
      {
        return this.GetAttributeAsString("Signature");
      }
      set
      {
        this.SetAttribute("Signature", value);
      }
    }

    [XmlAttribute]
    public uint Size
    {
      get
      {
        return this.GetAttributeAsUInt("Size");
      }
      set
      {
        this.SetAttribute("Size", value);
      }
    }

    [XmlAttribute]
    public uint BiosCompatibilityNumber
    {
      get
      {
        return this.GetAttributeAsUInt("BiosCompatibilityNumber");
      }
      set
      {
        this.SetAttribute("BiosCompatibilityNumber", value);
      }
    }

    [XmlAttribute]
    public uint ErrorLogAddress
    {
      get
      {
        return this.GetAttributeAsUInt("ErrorLogAddress");
      }
      set
      {
        this.SetAttribute("ErrorLogAddress", value);
      }
    }

    [XmlAttribute]
    public uint ErrorLogSize
    {
      get
      {
        return this.GetAttributeAsUInt("ErrorLogSize");
      }
      set
      {
        this.SetAttribute("ErrorLogSize", value);
      }
    }

    [XmlAttribute]
    public uint EventLogAddress
    {
      get
      {
        return this.GetAttributeAsUInt("EventLogAddress");
      }
      set
      {
        this.SetAttribute("EventLogAddress", value);
      }
    }

    [XmlAttribute]
    public uint EventLogSize
    {
      get
      {
        return this.GetAttributeAsUInt("EventLogSize");
      }
      set
      {
        this.SetAttribute("EventLogSize", value);
      }
    }

    [XmlAttribute]
    public uint LicenseLogAddress
    {
      get
      {
        return this.GetAttributeAsUInt("LicenseLogAddress");
      }
      set
      {
        this.SetAttribute("LicenseLogAddress", value);
      }
    }

    [XmlAttribute]
    public uint LicenseLogSize
    {
      get
      {
        return this.GetAttributeAsUInt("LicenseLogSize");
      }
      set
      {
        this.SetAttribute("LicenseLogSize", value);
      }
    }

    [XmlAttribute]
    public uint DatabaseAddress
    {
      get
      {
        return this.GetAttributeAsUInt("DatabaseAddress");
      }
      set
      {
        this.SetAttribute("DatabaseAddress", value);
      }
    }

    [XmlAttribute]
    public uint DatabaseSize
    {
      get
      {
        return this.GetAttributeAsUInt("DatabaseSize");
      }
      set
      {
        this.SetAttribute("DatabaseSize", value);
      }
    }

    [XmlAttribute]
    public uint ResidualCountsForMinimalAberration
    {
      get
      {
        return this.GetAttributeAsUInt("ResidualCountsForMinimalAberration");
      }
      set
      {
        this.SetAttribute("ResidualCountsForMinimalAberration", value);
      }
    }

    [XmlAttribute]
    public uint ResidualCountsForSevereAberration
    {
      get
      {
        return this.GetAttributeAsUInt("ResidualCountsForSevereAberration");
      }
      set
      {
        this.SetAttribute("ResidualCountsForSevereAberration", value);
      }
    }

    [XmlAttribute]
    public byte AbsoluteEgvDeltaForMinimalAberration
    {
      get
      {
        return this.GetAttributeAsByte("AbsoluteEgvDeltaForMinimalAberration");
      }
      set
      {
        this.SetAttribute("AbsoluteEgvDeltaForMinimalAberration", value);
      }
    }

    [XmlAttribute]
    public byte PercentageEgvDeltaForMinimalAberration
    {
      get
      {
        return this.GetAttributeAsByte("PercentageEgvDeltaForMinimalAberration");
      }
      set
      {
        this.SetAttribute("PercentageEgvDeltaForMinimalAberration", value);
      }
    }

    [XmlAttribute]
    public byte AbsoluteEgvDeltaForSevereAberration
    {
      get
      {
        return this.GetAttributeAsByte("AbsoluteEgvDeltaForSevereAberration");
      }
      set
      {
        this.SetAttribute("AbsoluteEgvDeltaForSevereAberration", value);
      }
    }

    [XmlAttribute]
    public byte PercentageEgvDeltaForSevereAberration
    {
      get
      {
        return this.GetAttributeAsByte("PercentageEgvDeltaForSevereAberration");
      }
      set
      {
        this.SetAttribute("PercentageEgvDeltaForSevereAberration", value);
      }
    }

    [XmlAttribute]
    public ushort CountsAberrationWindow
    {
      get
      {
        return this.GetAttributeAsUShort("CountsAberrationWindow");
      }
      set
      {
        this.SetAttribute("CountsAberrationWindow", value);
      }
    }

    [XmlAttribute]
    public ushort ResidualAberrationWindow
    {
      get
      {
        return this.GetAttributeAsUShort("ResidualAberrationWindow");
      }
      set
      {
        this.SetAttribute("ResidualAberrationWindow", value);
      }
    }

    [XmlAttribute]
    public ushort EgvAberrationWindow
    {
      get
      {
        return this.GetAttributeAsUShort("EgvAberrationWindow");
      }
      set
      {
        this.SetAttribute("EgvAberrationWindow", value);
      }
    }

    [XmlAttribute]
    public byte MaxAllowedCountsAberrations
    {
      get
      {
        return this.GetAttributeAsByte("MaxAllowedCountsAberrations");
      }
      set
      {
        this.SetAttribute("MaxAllowedCountsAberrations", value);
      }
    }

    [XmlAttribute]
    public byte MaxAllowedResidualAberrations
    {
      get
      {
        return this.GetAttributeAsByte("MaxAllowedResidualAberrations");
      }
      set
      {
        this.SetAttribute("MaxAllowedResidualAberrations", value);
      }
    }

    [XmlAttribute]
    public byte MaxAllowedEgvAberrations
    {
      get
      {
        return this.GetAttributeAsByte("MaxAllowedEgvAberrations");
      }
      set
      {
        this.SetAttribute("MaxAllowedEgvAberrations", value);
      }
    }

    [XmlAttribute]
    public byte MaxAllowedPointsToGetInCal
    {
      get
      {
        return this.GetAttributeAsByte("MaxAllowedPointsToGetInCal");
      }
      set
      {
        this.SetAttribute("MaxAllowedPointsToGetInCal", value);
      }
    }

    [XmlAttribute]
    public byte PercentageVenousAdjustment
    {
      get
      {
        return this.GetAttributeAsByte("PercentageVenousAdjustment");
      }
      set
      {
        this.SetAttribute("PercentageVenousAdjustment", value);
      }
    }

    [XmlAttribute]
    public byte CullPercentage
    {
      get
      {
        return this.GetAttributeAsByte("CullPercentage");
      }
      set
      {
        this.SetAttribute("CullPercentage", value);
      }
    }

    [XmlAttribute]
    public byte CullDelta
    {
      get
      {
        return this.GetAttributeAsByte("CullDelta");
      }
      set
      {
        this.SetAttribute("CullDelta", value);
      }
    }

    [XmlAttribute]
    public byte MaxTotalAberrations
    {
      get
      {
        return this.GetAttributeAsByte("MaxTotalAberrations");
      }
      set
      {
        this.SetAttribute("MaxTotalAberrations", value);
      }
    }

    [XmlAttribute]
    public byte ResidualPercentageForMinimalAberration
    {
      get
      {
        return this.GetAttributeAsByte("ResidualPercentageForMinimalAberration");
      }
      set
      {
        this.SetAttribute("ResidualPercentageForMinimalAberration", value);
      }
    }

    [XmlAttribute]
    public byte ResidualPercentageForSevereAberration
    {
      get
      {
        return this.GetAttributeAsByte("ResidualPercentageForSevereAberration");
      }
      set
      {
        this.SetAttribute("ResidualPercentageForSevereAberration", value);
      }
    }

    [XmlAttribute]
    public byte DeltaResidualPercentageForMinimalAberration
    {
      get
      {
        return this.GetAttributeAsByte("DeltaResidualPercentageForMinimalAberration");
      }
      set
      {
        this.SetAttribute("DeltaResidualPercentageForMinimalAberration", value);
      }
    }

    [XmlAttribute]
    public byte DeltaResidualPercentageForSevereAberration
    {
      get
      {
        return this.GetAttributeAsByte("DeltaResidualPercentageForSevereAberration");
      }
      set
      {
        this.SetAttribute("DeltaResidualPercentageForSevereAberration", value);
      }
    }

    [XmlAttribute]
    public ushort DeltaResidualAberrationWindow
    {
      get
      {
        return this.GetAttributeAsUShort("DeltaResidualAberrationWindow");
      }
      set
      {
        this.SetAttribute("DeltaResidualAberrationWindow", value);
      }
    }

    [XmlAttribute]
    public byte MaxAllowedDeltaResidualAberrations
    {
      get
      {
        return this.GetAttributeAsByte("MaxAllowedDeltaResidualAberrations");
      }
      set
      {
        this.SetAttribute("MaxAllowedDeltaResidualAberrations", value);
      }
    }

    [XmlAttribute]
    public byte MaxTotalAberrationsType1
    {
      get
      {
        return this.GetAttributeAsByte("MaxTotalAberrationsType1");
      }
      set
      {
        this.SetAttribute("MaxTotalAberrationsType1", value);
      }
    }

    [XmlAttribute]
    public byte MaxTotalAberrationsType2
    {
      get
      {
        return this.GetAttributeAsByte("MaxTotalAberrationsType2");
      }
      set
      {
        this.SetAttribute("MaxTotalAberrationsType2", value);
      }
    }

    [XmlAttribute]
    public byte MaxTotalAberrationsType3
    {
      get
      {
        return this.GetAttributeAsByte("MaxTotalAberrationsType3");
      }
      set
      {
        this.SetAttribute("MaxTotalAberrationsType3", value);
      }
    }

    [XmlAttribute]
    public Guid ImageInstanceId
    {
      get
      {
        return this.GetAttribute<Guid>("ImageInstanceId");
      }
      set
      {
        this.SetAttribute("ImageInstanceId", value);
      }
    }

    [XmlAttribute]
    public string ImageExtension
    {
      get
      {
        return this.GetAttribute<string>("ImageExtension");
      }
      set
      {
        this.SetAttribute("ImageExtension", value);
      }
    }

    [XmlAttribute]
    public uint HighWedgeMinSlope
    {
      get
      {
        return this.GetAttribute<uint>("HighWedgeMinSlope");
      }
      set
      {
        this.SetAttribute("HighWedgeMinSlope", value);
      }
    }

    [XmlAttribute]
    public uint HighWedgeMaxSlope
    {
      get
      {
        return this.GetAttribute<uint>("HighWedgeMaxSlope");
      }
      set
      {
        this.SetAttribute("HighWedgeMaxSlope", value);
      }
    }

    [XmlAttribute]
    public int HighWedgeMinBaseline
    {
      get
      {
        return this.GetAttribute<int>("HighWedgeMinBaseline");
      }
      set
      {
        this.SetAttribute("HighWedgeMinBaseline", value);
      }
    }

    [XmlAttribute]
    public int HighWedgeMaxBaseline
    {
      get
      {
        return this.GetAttribute<int>("HighWedgeMaxBaseline");
      }
      set
      {
        this.SetAttribute("HighWedgeMaxBaseline", value);
      }
    }

    [XmlAttribute]
    public int HighWedgeLowAssumedBaseline
    {
      get
      {
        return this.GetAttribute<int>("HighWedgeLowAssumedBaseline");
      }
      set
      {
        this.SetAttribute("HighWedgeLowAssumedBaseline", value);
      }
    }

    [XmlAttribute]
    public int HighWedgeMidAssumedBaseline
    {
      get
      {
        return this.GetAttribute<int>("HighWedgeMidAssumedBaseline");
      }
      set
      {
        this.SetAttribute("HighWedgeMidAssumedBaseline", value);
      }
    }

    [XmlAttribute]
    public int HighWedgeHighAssumedBaseline
    {
      get
      {
        return this.GetAttribute<int>("HighWedgeHighAssumedBaseline");
      }
      set
      {
        this.SetAttribute("HighWedgeHighAssumedBaseline", value);
      }
    }

    [XmlAttribute]
    public uint LowWedgeMinSlope
    {
      get
      {
        return this.GetAttribute<uint>("LowWedgeMinSlope");
      }
      set
      {
        this.SetAttribute("LowWedgeMinSlope", value);
      }
    }

    [XmlAttribute]
    public uint LowWedgeMaxSlope
    {
      get
      {
        return this.GetAttribute<uint>("LowWedgeMaxSlope");
      }
      set
      {
        this.SetAttribute("LowWedgeMaxSlope", value);
      }
    }

    [XmlAttribute]
    public int LowWedgeMinBaseline
    {
      get
      {
        return this.GetAttribute<int>("LowWedgeMinBaseline");
      }
      set
      {
        this.SetAttribute("LowWedgeMinBaseline", value);
      }
    }

    [XmlAttribute]
    public int LowWedgeMaxBaseline
    {
      get
      {
        return this.GetAttribute<int>("LowWedgeMaxBaseline");
      }
      set
      {
        this.SetAttribute("LowWedgeMaxBaseline", value);
      }
    }

    [XmlAttribute]
    public int LowWedgeLowAssumedBaseline
    {
      get
      {
        return this.GetAttribute<int>("LowWedgeLowAssumedBaseline");
      }
      set
      {
        this.SetAttribute("LowWedgeLowAssumedBaseline", value);
      }
    }

    [XmlAttribute]
    public int LowWedgeMidAssumedBaseline
    {
      get
      {
        return this.GetAttribute<int>("LowWedgeMidAssumedBaseline");
      }
      set
      {
        this.SetAttribute("LowWedgeMidAssumedBaseline", value);
      }
    }

    [XmlAttribute]
    public int LowWedgeHighAssumedBaseline
    {
      get
      {
        return this.GetAttribute<int>("LowWedgeHighAssumedBaseline");
      }
      set
      {
        this.SetAttribute("LowWedgeHighAssumedBaseline", value);
      }
    }

    [XmlAttribute]
    public uint RMSTimeWindow
    {
      get
      {
        return this.GetAttribute<uint>("RMSTimeWindow");
      }
      set
      {
        this.SetAttribute("RMSTimeWindow", value);
      }
    }

    [XmlAttribute]
    public uint NumberPointsPowerCalculation
    {
      get
      {
        return this.GetAttribute<uint>("NumberPointsPowerCalculation");
      }
      set
      {
        this.SetAttribute("NumberPointsPowerCalculation", value);
      }
    }

    [XmlAttribute]
    public uint DeltaResidualNoiseThreshold
    {
      get
      {
        return this.GetAttribute<uint>("DeltaResidualNoiseThreshold");
      }
      set
      {
        this.SetAttribute("DeltaResidualNoiseThreshold", value);
      }
    }

    [XmlAttribute]
    public uint ResidualNoiseThreshold
    {
      get
      {
        return this.GetAttribute<uint>("ResidualNoiseThreshold");
      }
      set
      {
        this.SetAttribute("ResidualNoiseThreshold", value);
      }
    }

    [XmlAttribute]
    public ushort AbsoluteAberrationWindow
    {
      get
      {
        return this.GetAttribute<ushort>("AbsoluteAberrationWindow");
      }
      set
      {
        this.SetAttribute("AbsoluteAberrationWindow", value);
      }
    }

    [XmlAttribute]
    public ushort PowerAberrationWindow
    {
      get
      {
        return this.GetAttribute<ushort>("PowerAberrationWindow");
      }
      set
      {
        this.SetAttribute("PowerAberrationWindow", value);
      }
    }

    [XmlAttribute]
    public uint AbsoluteResidualAberrationCounts
    {
      get
      {
        return this.GetAttribute<uint>("AbsoluteResidualAberrationCounts");
      }
      set
      {
        this.SetAttribute("AbsoluteResidualAberrationCounts", value);
      }
    }

    [XmlAttribute]
    public uint AbsoluteDeltaResidualAberrationCounts
    {
      get
      {
        return this.GetAttribute<uint>("AbsoluteDeltaResidualAberrationCounts");
      }
      set
      {
        this.SetAttribute("AbsoluteDeltaResidualAberrationCounts", value);
      }
    }

    [XmlAttribute]
    public byte PowerAberrationResidualPercentage
    {
      get
      {
        return this.GetAttribute<byte>("PowerAberrationResidualPercentage");
      }
      set
      {
        this.SetAttribute("PowerAberrationResidualPercentage", value);
      }
    }

    [XmlAttribute]
    public byte PowerAberrationDeltaResidualPercentage
    {
      get
      {
        return this.GetAttribute<byte>("PowerAberrationDeltaResidualPercentage");
      }
      set
      {
        this.SetAttribute("PowerAberrationDeltaResidualPercentage", value);
      }
    }

    [XmlAttribute]
    public byte MaxAllowedAbsoluteAberrations
    {
      get
      {
        return this.GetAttribute<byte>("MaxAllowedAbsoluteAberrations");
      }
      set
      {
        this.SetAttribute("MaxAllowedAbsoluteAberrations", value);
      }
    }

    [XmlAttribute]
    public byte MaxAllowedPowerAberrations
    {
      get
      {
        return this.GetAttribute<byte>("MaxAllowedPowerAberrations");
      }
      set
      {
        this.SetAttribute("MaxAllowedPowerAberrations", value);
      }
    }

    [XmlAttribute]
    public string ImageSubExtension
    {
      get
      {
        return this.GetAttribute<string>("ImageSubExtension");
      }
      set
      {
        this.SetAttribute("ImageSubExtension", value.Trim());
      }
    }

    [XmlAttribute]
    public byte UpperAdjustBoundPercent
    {
      get
      {
        return this.GetAttribute<byte>("UpperAdjustBoundPercent");
      }
      set
      {
        this.SetAttribute("UpperAdjustBoundPercent", value);
      }
    }

    [XmlAttribute]
    public byte LowerAdjustBoundPercent
    {
      get
      {
        return this.GetAttribute<byte>("LowerAdjustBoundPercent");
      }
      set
      {
        this.SetAttribute("LowerAdjustBoundPercent", value);
      }
    }

    [XmlAttribute]
    public byte MaxUpwardAdjustPercent
    {
      get
      {
        return this.GetAttribute<byte>("MaxUpwardAdjustPercent");
      }
      set
      {
        this.SetAttribute("MaxUpwardAdjustPercent", value);
      }
    }

    [XmlAttribute]
    public byte MaxDownwardAdjustPercent
    {
      get
      {
        return this.GetAttribute<byte>("MaxDownwardAdjustPercent");
      }
      set
      {
        this.SetAttribute("MaxDownwardAdjustPercent", value);
      }
    }

    [XmlAttribute]
    public double AlphaLight
    {
      get
      {
        return this.GetAttribute<double>("AlphaLight");
      }
      set
      {
        this.SetAttribute("AlphaLight", value);
      }
    }

    [XmlAttribute]
    public double AlphaMedium
    {
      get
      {
        return this.GetAttribute<double>("AlphaMedium");
      }
      set
      {
        this.SetAttribute("AlphaMedium", value);
      }
    }

    [XmlAttribute]
    public double AlphaSevere
    {
      get
      {
        return this.GetAttribute<double>("AlphaSevere");
      }
      set
      {
        this.SetAttribute("AlphaSevere", value);
      }
    }

    [XmlAttribute]
    public double AlphaSignal
    {
      get
      {
        return this.GetAttribute<double>("AlphaSignal");
      }
      set
      {
        this.SetAttribute("AlphaSignal", value);
      }
    }

    [XmlAttribute]
    public double ThresholdLight
    {
      get
      {
        return this.GetAttribute<double>("ThresholdLight");
      }
      set
      {
        this.SetAttribute("ThresholdLight", value);
      }
    }

    [XmlAttribute]
    public double ThresholdMedium
    {
      get
      {
        return this.GetAttribute<double>("ThresholdMedium");
      }
      set
      {
        this.SetAttribute("ThresholdMedium", value);
      }
    }

    [XmlAttribute]
    public double ThresholdSevere
    {
      get
      {
        return this.GetAttribute<double>("ThresholdSevere");
      }
      set
      {
        this.SetAttribute("ThresholdSevere", value);
      }
    }

    public XR2HardwareConfig()
      : this(new XmlDocument(), 7U)
    {
    }

    public XR2HardwareConfig(uint version)
      : this(new XmlDocument(), version)
    {
    }

    public XR2HardwareConfig(XmlDocument ownerDocument, uint version)
      : base("HardwareConfig", ownerDocument)
    {
      this.ConfigurationVersion = version;
      this.ReceiverId = 0U;
      this.HardwareFlags = R2HardwareFlags.G3_RF | R2HardwareFlags.ShortTermSensor;
      this.HardwareFlagsLow64 = (ulong) this.HardwareFlags;
      this.HardwareFlagsHigh64 = 0UL;
      this.FirmwareFlags = R2FirmwareFlags.Unknown;
      this.FirmwareFlags32 = (uint) this.FirmwareFlags;
      this.XtalTrim = (byte) 6;
      this.MaxSlope = 350U;
      this.MinSlope = 50U;
      this.MinBaseline = 8000;
      this.MaxBaseline = 40000;
      this.MinCounts = 10000U;
      this.MaxCounts = 600000U;
      this.LowAssumedBaseline = 15000;
      this.MidAssumedBaseline = 23000;
      this.HighAssumedBaseline = 30000;
      this.Calibrated5VoltAdcReading = ushort.MaxValue;
      this.HardwareProductNumber = 8205U;
      this.HardwareProductRevision = (byte) 20;
      this.ReceiverInstanceId = Guid.NewGuid();
      if ((int) version == 2 || (int) version == 3 || ((int) version == 4 || (int) version == 5) || ((int) version == 6 || (int) version == 7))
      {
        this.BiosCompatibilityNumber = 1U;
        this.Signature = "R2 CFG";
        this.Size = 1024U;
        this.HardwareFlags = R2HardwareFlags.ShortTermSensor | R2HardwareFlags.GTX_RF;
        this.HardwareFlagsLow64 = (ulong) this.HardwareFlags;
        this.HardwareFlagsHigh64 = 0UL;
        this.MinSlope = 15U;
        this.MaxSlope = 80U;
        this.MinBaseline = 1500;
        this.MaxBaseline = 5500;
        this.MinCounts = 1000U;
        this.MaxCounts = 60000U;
        this.LowAssumedBaseline = 2500;
        this.MidAssumedBaseline = 3500;
        this.HighAssumedBaseline = 4500;
        if ((int) version != 4 && (int) version != 5 && ((int) version != 6 && (int) version != 7))
        {
          this.ResidualCountsForMinimalAberration = 500U;
          this.ResidualCountsForSevereAberration = 5000U;
        }
        this.AbsoluteEgvDeltaForMinimalAberration = (byte) 20;
        this.PercentageEgvDeltaForMinimalAberration = (byte) 20;
        this.AbsoluteEgvDeltaForSevereAberration = (byte) 40;
        this.PercentageEgvDeltaForSevereAberration = (byte) 40;
        this.CountsAberrationWindow = (ushort) 1441;
        this.ResidualAberrationWindow = (ushort) 1441;
        this.EgvAberrationWindow = (ushort) 1441;
        this.MaxAllowedCountsAberrations = (byte) 6;
        this.MaxAllowedResidualAberrations = (byte) 0;
        this.MaxAllowedEgvAberrations = (byte) 0;
        this.MaxAllowedPointsToGetInCal = (byte) 0;
        this.ErrorLogAddress = R2ReceiverValues.ErrorStartAddressV2;
        this.ErrorLogSize = R2ReceiverValues.ErrorSizeV2;
        this.EventLogAddress = R2ReceiverValues.EventStartAddressV2;
        this.EventLogSize = R2ReceiverValues.EventSizeV2;
        this.LicenseLogAddress = R2ReceiverValues.LicenseStartAddressV2;
        this.LicenseLogSize = R2ReceiverValues.LicenseSizeV2;
        this.DatabaseAddress = R2ReceiverValues.DatabaseStartAddressV2;
        this.DatabaseSize = R2ReceiverValues.DatabaseSizeV2;
      }
      if ((int) version == 3)
      {
        this.PercentageVenousAdjustment = (byte) 90;
        this.CullDelta = (byte) 0;
        this.CullPercentage = (byte) 0;
        this.MaxTotalAberrations = (byte) 6;
        this.ManualBGBackDateOffset = byte.MaxValue;
        this.MaxAllowedCountsAberrations = (byte) 0;
      }
      if ((int) version == 4 || (int) version == 5 || ((int) version == 6 || (int) version == 7))
      {
        this.PercentageVenousAdjustment = (byte) 97;
        this.MaxTotalAberrations = (byte) 6;
        this.ManualBGBackDateOffset = (byte) 3;
        this.MaxAllowedCountsAberrations = (byte) 0;
        this.AbsoluteEgvDeltaForMinimalAberration = (byte) 30;
        this.PercentageEgvDeltaForMinimalAberration = (byte) 30;
        this.FirmwareFlags = R2FirmwareFlags.OneTouch | R2FirmwareFlags.ManualBG | R2FirmwareFlags.Supports3Day | R2FirmwareFlags.Supports7Day;
        this.FirmwareFlags32 = (uint) this.FirmwareFlags;
        this.ResidualPercentageForMinimalAberration = (byte) 20;
        this.ResidualPercentageForSevereAberration = (byte) 40;
        this.DeltaResidualPercentageForMinimalAberration = (byte) 20;
        this.DeltaResidualPercentageForSevereAberration = (byte) 40;
        this.DeltaResidualAberrationWindow = (ushort) 1441;
        this.MaxAllowedDeltaResidualAberrations = (byte) 0;
        this.MaxTotalAberrationsType1 = (byte) 0;
        this.MaxTotalAberrationsType2 = (byte) 0;
        this.MaxTotalAberrationsType3 = (byte) 0;
        this.ImageInstanceId = CommonValues.R2EmptyId;
        this.ImageExtension = string.Empty;
      }
      if ((int) version == 5 || (int) version == 6 || (int) version == 7)
      {
        this.HighWedgeHighAssumedBaseline = 0;
        this.HighWedgeLowAssumedBaseline = 0;
        this.HighWedgeMaxBaseline = 0;
        this.HighWedgeMaxSlope = 0U;
        this.HighWedgeMidAssumedBaseline = 0;
        this.HighWedgeMinBaseline = 0;
        this.HighWedgeMinSlope = 0U;
        this.LowWedgeHighAssumedBaseline = 0;
        this.LowWedgeLowAssumedBaseline = 0;
        this.LowWedgeMaxBaseline = 0;
        this.LowWedgeMaxSlope = 0U;
        this.LowWedgeMidAssumedBaseline = 0;
        this.LowWedgeMinBaseline = 0;
        this.LowWedgeMinSlope = 0U;
        this.RMSTimeWindow = 30U;
        this.NumberPointsPowerCalculation = 3U;
        this.DeltaResidualNoiseThreshold = 10U;
        this.ResidualNoiseThreshold = 15U;
      }
      if ((int) version == 6 || (int) version == 7)
      {
        this.AbsoluteAberrationWindow = (ushort) 1441;
        this.PowerAberrationWindow = (ushort) 1441;
        this.AbsoluteResidualAberrationCounts = 60000U;
        this.AbsoluteDeltaResidualAberrationCounts = 60000U;
        this.PowerAberrationResidualPercentage = (byte) 20;
        this.PowerAberrationDeltaResidualPercentage = (byte) 30;
        this.MaxAllowedAbsoluteAberrations = (byte) 6;
        this.MaxAllowedPowerAberrations = (byte) 0;
        this.ImageSubExtension = string.Empty;
        this.UpperAdjustBoundPercent = (byte) 105;
        this.LowerAdjustBoundPercent = (byte) 85;
        this.MaxUpwardAdjustPercent = (byte) 115;
        this.MaxDownwardAdjustPercent = (byte) 85;
      }
      if ((int) version != 7)
        return;
      this.MinSlope = 100U;
      this.MaxSlope = 600U;
      this.MinBaseline = 5000;
      this.MaxBaseline = 45000;
      this.MinCounts = 5000U;
      this.MaxCounts = 600000U;
      this.LowAssumedBaseline = 15000;
      this.MidAssumedBaseline = 25000;
      this.HighAssumedBaseline = 35000;
      this.FirmwareFlags = R2FirmwareFlags.OneTouch | R2FirmwareFlags.ManualBG | R2FirmwareFlags.Supports7Day;
      this.FirmwareFlags32 = (uint) this.FirmwareFlags;
      this.AbsoluteEgvDeltaForMinimalAberration = (byte) 40;
      this.PercentageEgvDeltaForMinimalAberration = (byte) 40;
      this.CountsAberrationWindow = (ushort) 61;
      this.MaxAllowedCountsAberrations = (byte) 6;
      this.AbsoluteAberrationWindow = (ushort) 61;
      this.AbsoluteResidualAberrationCounts = 60000U;
      this.AbsoluteDeltaResidualAberrationCounts = 60000U;
      this.MaxAllowedAbsoluteAberrations = (byte) 6;
      this.ImageSubExtension = "-0";
      this.UpperAdjustBoundPercent = (byte) 110;
      this.LowerAdjustBoundPercent = (byte) 90;
      this.MaxUpwardAdjustPercent = (byte) 110;
      this.MaxDownwardAdjustPercent = (byte) 90;
      this.AlphaLight = 0.5;
      this.AlphaMedium = 0.3;
      this.AlphaSevere = 0.1;
      this.AlphaSignal = 1.0 / 400.0;
      this.ThresholdLight = 10.0;
      this.ThresholdMedium = 10.0;
      this.ThresholdSevere = 10.0;
    }

    public XR2HardwareConfig(XmlElement element)
      : base(element)
    {
    }

    protected XR2HardwareConfig(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    [SecurityCritical]
    public new void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      base.GetObjectData(info, context);
    }
  }
}

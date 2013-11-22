// Type: Dexcom.R2Receiver.R2HWConfigV2
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using Dexcom.Common;
using Dexcom.Common.Data;
using System;
using System.Runtime.InteropServices;

namespace Dexcom.R2Receiver
{
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public struct R2HWConfigV2
  {
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
    public string m_configSignature;
    public uint m_configSize;
    public uint m_configVersion;
    public uint m_biosCompatibilityNumber;
    public uint m_errorLogAddress;
    public uint m_errorLogSize;
    public uint m_eventLogAddress;
    public uint m_eventLogSize;
    public uint m_licenseLogAddress;
    public uint m_licenseLogSize;
    public uint m_databaseAddress;
    public uint m_databaseSize;
    public ulong m_hardwareFlagsLow64;
    public ulong m_hardwareFlagsHigh64;
    public uint m_firmwareFlags;
    public uint m_deviceId;
    public uint m_hardwareProductNumber;
    public byte m_astrx2XtalTrim;
    public byte m_hardwareProductRevision;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    private byte[] m_reserved1;
    public Guid m_receiverInstanceId;
    public uint m_minSlope;
    public uint m_maxSlope;
    public int m_minBaseline;
    public int m_maxBaseline;
    public int m_lowAssumedBaseline;
    public int m_midAssumedBaseline;
    public int m_highAssumedBaseline;
    public uint m_minCounts;
    public uint m_maxCounts;
    public uint m_residualCountsForMinimalAberration;
    public uint m_residualCountsForSevereAberration;
    public byte m_absoluteEgvDeltaForMinimalAberration;
    public byte m_percentageEgvDeltaForMinimalAberration;
    public byte m_absoluteEgvDeltaForSevereAberration;
    public byte m_percentageEgvDeltaForSevereAberration;
    public ushort m_countsAberrationWindow;
    public ushort m_residualAberrationWindow;
    public ushort m_egvAberrationWindow;
    public ushort m_calibratedAdcReading;
    public byte m_maxAllowedCountsAberrations;
    public byte m_maxAllowedResidualAberrations;
    public byte m_maxAllowedEgvAberrations;
    public byte m_maxAllowedPointsToGetInCal;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 860)]
    public byte[] m_reserved2;
    public uint m_crc;

    public void UpdateCrc()
    {
      byte[] buf = DataTools.ConvertObjectToBytes((object) this);
      int start = 0;
      int end = start + Marshal.OffsetOf(this.GetType(), "m_crc").ToInt32();
      this.m_crc = Crc.CalculateCrc32(buf, start, end);
    }

    public bool IsValidCrc()
    {
      byte[] buf = DataTools.ConvertObjectToBytes((object) this);
      int start = 0;
      int end = start + Marshal.OffsetOf(this.GetType(), "m_crc").ToInt32();
      return (int) this.m_crc == (int) Crc.CalculateCrc32(buf, start, end);
    }
  }
}

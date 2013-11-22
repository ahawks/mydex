// Type: Dexcom.R2Receiver.R2HWConfig
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
  public struct R2HWConfig
  {
    public uint m_deviceId;
    public ulong m_hardwareFlagsLow64;
    public ulong m_hardwareFlagsHigh64;
    public uint m_firmwareFlags;
    public byte m_astrx2XtalTrim;
    public byte m_pad0;
    public ushort m_pad1;
    public uint m_maxSlope;
    public uint m_minSlope;
    public int m_minBaseline;
    public int m_maxBaseline;
    public uint m_minCounts;
    public uint m_maxCounts;
    public int m_lowAssumedBaseline;
    public int m_midAssumedBaseline;
    public int m_highAssumedBaseline;
    public ushort m_calibratedAdcReading;
    public ushort m_reserved1;
    public uint m_hardwareProductNumber;
    public byte m_hardwareProductRevision;
    public Guid m_receiverInstanceId;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)]
    private byte[] m_reserved2;
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

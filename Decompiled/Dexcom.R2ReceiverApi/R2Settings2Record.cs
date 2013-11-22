// Type: Dexcom.R2Receiver.R2Settings2Record
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
  public struct R2Settings2Record : IGenericR2Record
  {
    public R2RecordHeader m_header;
    public R2DatabaseKey m_key;
    public long m_skewOffset;
    public long m_externalOffset;
    public uint m_lowGlucoseThreshold;
    public uint m_highGlucoseThreshold;
    public uint m_transmitterSerialNumber;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12)]
    public string m_meterSerialNumber;
    public byte m_glucoseUnits;
    public byte m_clockMode;
    public byte m_displayMode;
    public byte m_unused1;
    public uint m_recordsInLastMeterTransfer;
    public byte m_numberOf7DayLicenses;
    public byte m_numberOf3DayLicenses;
    public ushort m_unused2;
    public ulong m_longTermSensorImplantTime;
    public ulong m_lastMeterTransferTime;
    public R2RecordFooter m_footer;

    public DateTime RecordTimeStamp
    {
      get
      {
        return R2ReceiverValues.R2BaseDateTime.AddMilliseconds((double) this.m_key.m_timeStamp);
      }
    }

    public R2RecordType RecordType
    {
      get
      {
        return R2RecordType.Settings2;
      }
    }

    public bool IsMatchingHeaderFooterType
    {
      get
      {
        return (int) this.m_header.m_type == (int) this.m_footer.m_type;
      }
    }

    public ushort RecordedCrc
    {
      get
      {
        return this.m_footer.m_crc;
      }
    }

    public int RecordSize
    {
      get
      {
        return Marshal.SizeOf((object) this);
      }
    }

    public void UpdateCrc()
    {
      byte[] buf = DataTools.ConvertObjectToBytes((object) this);
      int start = 0;
      int end = start + Marshal.OffsetOf(this.GetType(), "m_footer").ToInt32();
      this.m_footer.m_crc = Crc.CalculateCrc16(buf, start, end);
    }
  }
}

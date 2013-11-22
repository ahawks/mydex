// Type: Dexcom.R2Receiver.R2Settings5Record
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
  public struct R2Settings5Record : IGenericR2Record
  {
    public R2RecordHeader m_header;
    public R2DatabaseKey m_key;
    public long m_skewOffset;
    public long m_externalOffset;
    public ushort m_lowGlucoseThreshold;
    public byte m_lowGlucoseAlertType;
    public byte m_outOfRangeAlertType;
    public ushort m_highGlucoseThreshold;
    public byte m_highGlucoseAlertType;
    public byte m_otherAlertType;
    public uint m_transmitterSerialNumber;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12)]
    public string m_meterSerialNumber;
    public byte m_beepOnReceive;
    public byte m_backlightEnabled;
    public byte m_displayMode;
    public byte m_upRateAlertType;
    public uint m_recordsInLastMeterTransfer;
    public ushort m_highSnooze;
    public byte m_meterType;
    public byte m_downRateAlertType;
    public ushort m_lowSnooze;
    public ushort m_outOfRangeMinutes;
    public byte m_upRate;
    public byte m_downRate;
    public ushort m_reserved;
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
        return R2RecordType.Settings5;
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

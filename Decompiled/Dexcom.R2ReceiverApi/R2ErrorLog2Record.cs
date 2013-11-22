// Type: Dexcom.R2Receiver.R2ErrorLog2Record
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using System;
using System.Runtime.InteropServices;

namespace Dexcom.R2Receiver
{
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public struct R2ErrorLog2Record : IGenericR2Record
  {
    public ulong m_timeStamp;
    public ulong m_argData1;
    public ulong m_argData2;
    public uint m_address;
    public uint m_lineNumber;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    public string m_filename;
    public byte m_pad0;
    public byte m_flags;
    public ushort m_logNumber;

    public DateTime RecordTimeStamp
    {
      get
      {
        return R2ReceiverValues.R2BaseDateTime.AddMilliseconds((double) this.m_timeStamp);
      }
    }

    public R2RecordType RecordType
    {
      get
      {
        return R2RecordType.ErrorLog;
      }
    }

    public bool IsMatchingHeaderFooterType
    {
      get
      {
        return true;
      }
    }

    public ushort RecordedCrc
    {
      get
      {
        return ushort.MaxValue;
      }
    }

    public int RecordSize
    {
      get
      {
        return Marshal.SizeOf((object) this);
      }
    }
  }
}

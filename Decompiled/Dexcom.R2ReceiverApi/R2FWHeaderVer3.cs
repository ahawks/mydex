// Type: Dexcom.R2Receiver.R2FWHeaderVer3
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using System.Runtime.InteropServices;

namespace Dexcom.R2Receiver
{
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public struct R2FWHeaderVer3
  {
    public ushort m_code;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
    public string m_signature;
    public uint m_revision;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public byte[] m_reserved1;
    public uint m_addressFirmwareCrc;
    public uint m_branchInstruction;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
    public byte[] m_hwConfigMask;
    public uint m_requiredMemorySize;
    public uint m_databaseRevision;
    public uint m_firmwareHeaderRevision;
    public uint m_gtxCpldRevision;
    public uint m_g3CpldRevision;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public byte[] m_reserved2;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    public string m_productString;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
    public byte[] m_reserved3;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    public string m_buildDateAndTime;
    public uint m_biosCompatibilityNumber;
    public uint m_requiredConfigImageVersion;
  }
}

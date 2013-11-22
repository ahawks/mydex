﻿// Type: Dexcom.R2Receiver.R2HWConfigHeader
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using System.Runtime.InteropServices;

namespace Dexcom.R2Receiver
{
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public struct R2HWConfigHeader
  {
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
    public string m_configSignature;
    public uint m_configSize;
    public uint m_configVersion;
    public uint m_biosCompatibilityNumber;
  }
}

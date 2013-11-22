// Type: CSUACSelfElevation.SafeTokenHandle
// Assembly: Dexcom.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B95A59FD-1598-4EA8-B1F5-D812FE5A6802
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.dll

using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;

namespace CSUACSelfElevation
{
  internal class SafeTokenHandle : SafeHandleZeroOrMinusOneIsInvalid
  {
    private SafeTokenHandle()
      : base(true)
    {
    }

    internal SafeTokenHandle(IntPtr handle)
      : base(true)
    {
      this.SetHandle(handle);
    }

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    internal static bool CloseHandle(IntPtr handle);

    protected override bool ReleaseHandle()
    {
      return SafeTokenHandle.CloseHandle(this.handle);
    }
  }
}

// Type: CSUACSelfElevation.UacSelfEvaluation
// Assembly: Dexcom.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B95A59FD-1598-4EA8-B1F5-D812FE5A6802
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace CSUACSelfElevation
{
  internal static class UacSelfEvaluation
  {
    public static bool IsUserInAdminGroup()
    {
      SafeTokenHandle hToken = (SafeTokenHandle) null;
      SafeTokenHandle DuplicateTokenHandle = (SafeTokenHandle) null;
      IntPtr num1 = IntPtr.Zero;
      IntPtr num2 = IntPtr.Zero;
      try
      {
        if (!NativeMethods.OpenProcessToken(Process.GetCurrentProcess().Handle, 10U, out hToken))
          throw new Win32Exception();
        if (Environment.OSVersion.Version.Major >= 6)
        {
          int returnLength1 = 4;
          num1 = Marshal.AllocHGlobal(returnLength1);
          if (num1 == IntPtr.Zero)
            throw new Win32Exception();
          if (!NativeMethods.GetTokenInformation(hToken, TOKEN_INFORMATION_CLASS.TokenElevationType, num1, returnLength1, out returnLength1))
            throw new Win32Exception();
          if (Marshal.ReadInt32(num1) == 3)
          {
            int returnLength2 = IntPtr.Size;
            num2 = Marshal.AllocHGlobal(returnLength2);
            if (num2 == IntPtr.Zero)
              throw new Win32Exception();
            if (!NativeMethods.GetTokenInformation(hToken, TOKEN_INFORMATION_CLASS.TokenLinkedToken, num2, returnLength2, out returnLength2))
              throw new Win32Exception();
            DuplicateTokenHandle = new SafeTokenHandle(Marshal.ReadIntPtr(num2));
          }
        }
        if (DuplicateTokenHandle == null && !NativeMethods.DuplicateToken(hToken, SECURITY_IMPERSONATION_LEVEL.SecurityIdentification, out DuplicateTokenHandle))
          throw new Win32Exception();
        else
          return new WindowsPrincipal(new WindowsIdentity(DuplicateTokenHandle.DangerousGetHandle())).IsInRole(WindowsBuiltInRole.Administrator);
      }
      finally
      {
        if (hToken != null)
          hToken.Close();
        if (DuplicateTokenHandle != null)
          DuplicateTokenHandle.Close();
        if (num1 != IntPtr.Zero)
        {
          Marshal.FreeHGlobal(num1);
          IntPtr num3 = IntPtr.Zero;
        }
        if (num2 != IntPtr.Zero)
        {
          Marshal.FreeHGlobal(num2);
          IntPtr num3 = IntPtr.Zero;
        }
      }
    }
  }
}

// Type: CSUACSelfElevation.NativeMethods
// Assembly: Dexcom.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B95A59FD-1598-4EA8-B1F5-D812FE5A6802
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.dll

using System;
using System.Runtime.InteropServices;

namespace CSUACSelfElevation
{
  internal class NativeMethods
  {
    public const uint STANDARD_RIGHTS_REQUIRED = 983040U;
    public const uint STANDARD_RIGHTS_READ = 131072U;
    public const uint TOKEN_ASSIGN_PRIMARY = 1U;
    public const uint TOKEN_DUPLICATE = 2U;
    public const uint TOKEN_IMPERSONATE = 4U;
    public const uint TOKEN_QUERY = 8U;
    public const uint TOKEN_QUERY_SOURCE = 16U;
    public const uint TOKEN_ADJUST_PRIVILEGES = 32U;
    public const uint TOKEN_ADJUST_GROUPS = 64U;
    public const uint TOKEN_ADJUST_DEFAULT = 128U;
    public const uint TOKEN_ADJUST_SESSIONID = 256U;
    public const uint TOKEN_READ = 131080U;
    public const uint TOKEN_ALL_ACCESS = 983551U;
    public const int ERROR_INSUFFICIENT_BUFFER = 122;
    public const int SECURITY_MANDATORY_UNTRUSTED_RID = 0;
    public const int SECURITY_MANDATORY_LOW_RID = 4096;
    public const int SECURITY_MANDATORY_MEDIUM_RID = 8192;
    public const int SECURITY_MANDATORY_HIGH_RID = 12288;
    public const int SECURITY_MANDATORY_SYSTEM_RID = 16384;
    public const uint BCM_SETSHIELD = 5644U;

    [DllImport("advapi32", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static bool OpenProcessToken(IntPtr hProcess, uint desiredAccess, out SafeTokenHandle hToken);

    [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static bool DuplicateToken(SafeTokenHandle ExistingTokenHandle, SECURITY_IMPERSONATION_LEVEL ImpersonationLevel, out SafeTokenHandle DuplicateTokenHandle);

    [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static bool GetTokenInformation(SafeTokenHandle hToken, TOKEN_INFORMATION_CLASS tokenInfoClass, IntPtr pTokenInfo, int tokenInfoLength, out int returnLength);

    [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
    public static int SendMessage(IntPtr hWnd, uint Msg, int wParam, IntPtr lParam);

    [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static IntPtr GetSidSubAuthority(IntPtr pSid, uint nSubAuthority);
  }
}

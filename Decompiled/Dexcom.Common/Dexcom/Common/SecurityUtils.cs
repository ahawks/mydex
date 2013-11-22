// Type: Dexcom.Common.SecurityUtils
// Assembly: Dexcom.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B95A59FD-1598-4EA8-B1F5-D812FE5A6802
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.dll

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;

namespace Dexcom.Common
{
  [SecuritySafeCritical]
  public static class SecurityUtils
  {
    [DllImport("advapi32.dll", SetLastError = true)]
    public static bool LogonUser(string lpszUsername, string lpszDomain, IntPtr lpszPassword, SecurityUtils.LogonType dwLogonType, SecurityUtils.LogonProvider dwLogonProvider, out IntPtr phToken);

    [DllImport("advapi32.dll", SetLastError = true)]
    public static bool RevertToSelf();

    [DllImport("advapi32.dll", SetLastError = true)]
    public static bool DuplicateToken(IntPtr ExistingTokenHandle, SecurityUtils.SecurityImpersonationLevel SECURITY_IMPERSONATION_LEVEL, out IntPtr DuplicateTokenHandle);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static bool CloseHandle(IntPtr handle);

    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    public static WindowsImpersonationContext ImpersonateUser(string domain, string username, SecureString password)
    {
      WindowsImpersonationContext impersonationContext = (WindowsImpersonationContext) null;
      IntPtr phToken = IntPtr.Zero;
      IntPtr DuplicateTokenHandle = IntPtr.Zero;
      IntPtr num = IntPtr.Zero;
      try
      {
        num = Marshal.SecureStringToGlobalAllocAnsi(password);
        if (!SecurityUtils.LogonUser(username, domain, num, SecurityUtils.LogonType.LOGON32_LOGON_INTERACTIVE, SecurityUtils.LogonProvider.LOGON32_PROVIDER_DEFAULT, out phToken))
          throw new Win32Exception();
        Marshal.ZeroFreeGlobalAllocAnsi(num);
        num = IntPtr.Zero;
        if (!SecurityUtils.DuplicateToken(phToken, SecurityUtils.SecurityImpersonationLevel.SecurityImpersonation, out DuplicateTokenHandle))
          throw new Win32Exception();
        using (WindowsIdentity windowsIdentity = new WindowsIdentity(phToken))
        {
          impersonationContext = windowsIdentity.Impersonate();
          try
          {
            SecurityUtils.CloseHandle(phToken);
            phToken = IntPtr.Zero;
            SecurityUtils.CloseHandle(DuplicateTokenHandle);
            DuplicateTokenHandle = IntPtr.Zero;
          }
          catch
          {
            impersonationContext.Dispose();
            impersonationContext = (WindowsImpersonationContext) null;
            throw;
          }
        }
      }
      catch
      {
        if (num != IntPtr.Zero)
          Marshal.ZeroFreeGlobalAllocAnsi(num);
        if (phToken != IntPtr.Zero)
          SecurityUtils.CloseHandle(phToken);
        if (DuplicateTokenHandle != IntPtr.Zero)
          SecurityUtils.CloseHandle(DuplicateTokenHandle);
        throw;
      }
      return impersonationContext;
    }

    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    public static WindowsIdentity GetImpersonatedUserIdentity(string domain, string username, SecureString password)
    {
      WindowsIdentity windowsIdentity1 = (WindowsIdentity) null;
      IntPtr phToken = IntPtr.Zero;
      IntPtr DuplicateTokenHandle = IntPtr.Zero;
      IntPtr num1 = IntPtr.Zero;
      WindowsIdentity windowsIdentity2;
      try
      {
        num1 = Marshal.SecureStringToGlobalAllocAnsi(password);
        if (!SecurityUtils.LogonUser(username, domain, num1, SecurityUtils.LogonType.LOGON32_LOGON_INTERACTIVE, SecurityUtils.LogonProvider.LOGON32_PROVIDER_DEFAULT, out phToken))
          throw new Win32Exception();
        Marshal.ZeroFreeGlobalAllocAnsi(num1);
        num1 = IntPtr.Zero;
        if (!SecurityUtils.DuplicateToken(phToken, SecurityUtils.SecurityImpersonationLevel.SecurityImpersonation, out DuplicateTokenHandle))
          throw new Win32Exception();
        windowsIdentity2 = new WindowsIdentity(phToken);
        try
        {
          SecurityUtils.CloseHandle(phToken);
          phToken = IntPtr.Zero;
          SecurityUtils.CloseHandle(DuplicateTokenHandle);
          IntPtr num2 = IntPtr.Zero;
        }
        catch
        {
          windowsIdentity2.Dispose();
          windowsIdentity1 = (WindowsIdentity) null;
          throw;
        }
      }
      catch
      {
        if (num1 != IntPtr.Zero)
          Marshal.ZeroFreeGlobalAllocAnsi(num1);
        if (phToken != IntPtr.Zero)
          SecurityUtils.CloseHandle(phToken);
        if (DuplicateTokenHandle != IntPtr.Zero)
          SecurityUtils.CloseHandle(DuplicateTokenHandle);
        throw;
      }
      return windowsIdentity2;
    }

    public static SecureString MakeSecureString(string unsecureString)
    {
      SecureString secureString = new SecureString();
      foreach (char c in unsecureString.ToCharArray())
        secureString.AppendChar(c);
      secureString.MakeReadOnly();
      return secureString;
    }

    public static string MakeUnsecureString(SecureString input)
    {
      string str = string.Empty;
      IntPtr num = Marshal.SecureStringToBSTR(input);
      try
      {
        return Marshal.PtrToStringBSTR(num);
      }
      finally
      {
        Marshal.ZeroFreeBSTR(num);
      }
    }

    public static string EncryptString(string input)
    {
      return SecurityUtils.EncryptString(SecurityUtils.MakeSecureString(input));
    }

    public static string EncryptString(SecureString input)
    {
      return Convert.ToBase64String(ProtectedData.Protect(Encoding.Unicode.GetBytes(SecurityUtils.MakeUnsecureString(input)), Encoding.Unicode.GetBytes("Dexcom.Common.SecurityUtils"), DataProtectionScope.CurrentUser));
    }

    public static SecureString DecryptString(string encryptedData)
    {
      try
      {
        return SecurityUtils.MakeSecureString(Encoding.Unicode.GetString(ProtectedData.Unprotect(Convert.FromBase64String(encryptedData), Encoding.Unicode.GetBytes("Dexcom.Common.SecurityUtils"), DataProtectionScope.CurrentUser)));
      }
      catch
      {
        return new SecureString();
      }
    }

    public enum LogonType
    {
      LOGON32_LOGON_INTERACTIVE = 2,
      LOGON32_LOGON_NETWORK = 3,
      LOGON32_LOGON_BATCH = 4,
      LOGON32_LOGON_SERVICE = 5,
      LOGON32_LOGON_UNLOCK = 7,
      LOGON32_LOGON_NETWORK_CLEARTEXT = 8,
      LOGON32_LOGON_NEW_CREDENTIALS = 9,
    }

    public enum LogonProvider
    {
      LOGON32_PROVIDER_DEFAULT,
    }

    public enum SecurityImpersonationLevel
    {
      SecurityAnonymous,
      SecurityIdentification,
      SecurityImpersonation,
      SecurityDelegation,
    }
  }
}

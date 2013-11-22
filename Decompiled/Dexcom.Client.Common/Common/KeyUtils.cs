// Type: Dexcom.Client.Common.KeyUtils
// Assembly: Dexcom.Client.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B0B1988C-106E-413B-9428-09D6C6A8ED63
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Client.Common.dll

using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Forms;

namespace Dexcom.Client.Common
{
  public class KeyUtils
  {
    [DllImport("User32.dll", EntryPoint = "GetKeyState", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
    private static short DoGetKeyState(Keys nVirtKey);

    [SecuritySafeCritical]
    public static short GetKeyState(Keys nVirtKey)
    {
      return KeyUtils.DoGetKeyState(nVirtKey);
    }

    public static bool IsLeftControlKeyDown()
    {
      return (int) KeyUtils.GetKeyState(Keys.LControlKey) < 0;
    }

    public static bool IsRightControlKeyDown()
    {
      return (int) KeyUtils.GetKeyState(Keys.RControlKey) < 0;
    }

    public static bool IsLeftShiftKeyDown()
    {
      return (int) KeyUtils.GetKeyState(Keys.LShiftKey) < 0;
    }

    public static bool IsRightShiftKeyDown()
    {
      return (int) KeyUtils.GetKeyState(Keys.RShiftKey) < 0;
    }

    public static bool IsLeftMouseButtonDown()
    {
      return (int) KeyUtils.GetKeyState(Keys.LButton) < 0;
    }

    public static bool IsRightMouseButtonDown()
    {
      return (int) KeyUtils.GetKeyState(Keys.RButton) < 0;
    }

    public static bool IsCapsLockOn()
    {
      return ((int) KeyUtils.GetKeyState(Keys.Capital) & 1) == 1;
    }

    public static bool IsScrollLockOn()
    {
      return ((int) KeyUtils.GetKeyState(Keys.Scroll) & 1) == 1;
    }

    public static bool IsNumLockOn()
    {
      return ((int) KeyUtils.GetKeyState(Keys.NumLock) & 1) == 1;
    }

    public static bool IsControlKeyDown()
    {
      short keyState1 = KeyUtils.GetKeyState(Keys.ControlKey);
      short keyState2 = KeyUtils.GetKeyState(Keys.LControlKey);
      short keyState3 = KeyUtils.GetKeyState(Keys.RControlKey);
      if ((int) keyState1 >= 0 && (int) keyState2 >= 0)
        return (int) keyState3 < 0;
      else
        return true;
    }

    public static bool IsShiftKeyDown()
    {
      short keyState1 = KeyUtils.GetKeyState(Keys.ShiftKey);
      short keyState2 = KeyUtils.GetKeyState(Keys.LShiftKey);
      short keyState3 = KeyUtils.GetKeyState(Keys.RShiftKey);
      if ((int) keyState1 >= 0 && (int) keyState2 >= 0)
        return (int) keyState3 < 0;
      else
        return true;
    }

    public static bool IsAltKeyDown()
    {
      return (Control.ModifierKeys & Keys.Alt) == Keys.Alt;
    }
  }
}

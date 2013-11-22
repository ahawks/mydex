// Type: Dexcom.ReceiverApi.GlobalReceiverRegistryTools
// Assembly: Dexcom.ReceiverCommercialApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 3F623F65-908D-40A7-87D3-892C5D1FFA2E
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.ReceiverCommercialApi.dll

using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dexcom.ReceiverApi
{
  public static class GlobalReceiverRegistryTools
  {
    public const string GlobalReceiverVidPid_Commercial = "VID_22A3&PID_0047";
    public const string GlobalReceiverDriverDisplayVersion = "05/24/2010 1.0.0.2";

    public static string GetPortNameForDeviceInstance(string usbVendorProduct, string usbDeviceInstance)
    {
      string str = string.Empty;
      using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(string.Format("SYSTEM\\CurrentControlSet\\Enum\\USB\\{0}\\{1}\\Device Parameters", (object) usbVendorProduct, (object) usbDeviceInstance)))
      {
        if (registryKey != null)
          str = registryKey.GetValue("PortName") as string;
      }
      return str;
    }

    public static List<string> GetListOfFailedAttachedReceivers()
    {
      List<string> list = new List<string>();
      using (RegistryKey registryKey1 = Registry.LocalMachine.OpenSubKey(string.Format("SYSTEM\\CurrentControlSet\\Enum\\USB\\{0}", (object) "VID_22A3&PID_0047")))
      {
        if (registryKey1 != null)
        {
          string[] subKeyNames = registryKey1.GetSubKeyNames();
          if (subKeyNames != null)
          {
            foreach (string name in subKeyNames)
            {
              using (RegistryKey registryKey2 = registryKey1.OpenSubKey(name))
              {
                if (registryKey2 != null)
                {
                  string str = registryKey2.GetValue("DeviceDesc") as string;
                  if (!string.IsNullOrEmpty(str))
                  {
                    if (str == "DexCom Gen4 USB Serial")
                      list.Add(name);
                  }
                }
              }
            }
          }
        }
      }
      return list;
    }

    public static List<DeviceRegistryInfo> GetGlobalReceiverDeviceList()
    {
      List<DeviceRegistryInfo> list = new List<DeviceRegistryInfo>();
      using (RegistryKey registryKey1 = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\DeviceClasses\\{a5dcbf10-6530-11d2-901f-00c04fb951ed}"))
      {
        if (registryKey1 != null)
        {
          string[] strArray1 = new string[1]
          {
            string.Format("##?#USB#{0}", (object) "VID_22A3&PID_0047")
          };
          string[] strArray2 = new string[1]
          {
            "VID_22A3&PID_0047"
          };
          string[] subKeyNames = registryKey1.GetSubKeyNames();
          for (int index = 0; index < strArray2.Length; ++index)
          {
            foreach (string str in subKeyNames)
            {
              if (str.StartsWith(strArray1[index], StringComparison.OrdinalIgnoreCase))
              {
                DeviceRegistryInfo deviceRegistryInfo = new DeviceRegistryInfo();
                deviceRegistryInfo.UsbDeviceClassKey = str;
                deviceRegistryInfo.UsbDeviceInstance = string.Empty;
                deviceRegistryInfo.UsbVidPid = strArray2[index];
                deviceRegistryInfo.PortName = string.Empty;
                deviceRegistryInfo.ReferenceCount = -1;
                string[] strArray3 = str.Split(new char[1]
                {
                  '#'
                });
                if (strArray3.Length >= 2)
                  deviceRegistryInfo.UsbDeviceInstance = strArray3[strArray3.Length - 2];
                try
                {
                  using (RegistryKey registryKey2 = registryKey1.OpenSubKey(str + "\\Control"))
                  {
                    if (registryKey2 != null)
                      deviceRegistryInfo.ReferenceCount = Convert.ToInt32(registryKey2.GetValue("ReferenceCount"));
                  }
                }
                catch
                {
                }
                deviceRegistryInfo.PortName = GlobalReceiverRegistryTools.GetPortNameForDeviceInstance(strArray2[index], deviceRegistryInfo.UsbDeviceInstance);
                if (!string.IsNullOrEmpty(deviceRegistryInfo.PortName))
                {
                  deviceRegistryInfo.PortNumber = int.Parse(deviceRegistryInfo.PortName.Substring(ReceiverValues.ComPortNamePrefix.Length));
                  list.Add(deviceRegistryInfo);
                }
              }
            }
          }
        }
      }
      list.Sort();
      return list;
    }

    public static List<DeviceRegistryInfo> GetAttatchedGlobalReceiverDeviceList()
    {
      return Enumerable.ToList<DeviceRegistryInfo>(Enumerable.Where<DeviceRegistryInfo>(Enumerable.Cast<DeviceRegistryInfo>((IEnumerable) GlobalReceiverRegistryTools.GetGlobalReceiverDeviceList()), (Func<DeviceRegistryInfo, bool>) (item =>
      {
        if (item.ReferenceCount > 0)
          return item.UsbVidPid == "VID_22A3&PID_0047";
        else
          return false;
      })));
    }

    public static bool IsDexComCommercialGlobalReceiverCurrentlyAttached()
    {
      return GlobalReceiverRegistryTools.GetAttatchedGlobalReceiverDeviceList().Count > 0;
    }

    public static bool IsEvidenceOfDexComCommercialGlobalReceiverEverAttached()
    {
      return Enumerable.ToList<DeviceRegistryInfo>(Enumerable.Where<DeviceRegistryInfo>(Enumerable.Cast<DeviceRegistryInfo>((IEnumerable) GlobalReceiverRegistryTools.GetGlobalReceiverDeviceList()), (Func<DeviceRegistryInfo, bool>) (item => item.UsbVidPid == "VID_22A3&PID_0047"))).Count > 0;
    }

    public static bool IsDexComVcp1002DriverInstalled()
    {
      string uninstallString;
      return GlobalReceiverRegistryTools.IsDexComVcp1002DriverInstalled(out uninstallString);
    }

    public static bool IsDexComVcp1002DriverInstalled(out string uninstallString)
    {
      bool flag = false;
      uninstallString = string.Empty;
      if (Enumerable.ToList<DeviceRegistryInfo>(Enumerable.Where<DeviceRegistryInfo>(Enumerable.Cast<DeviceRegistryInfo>((IEnumerable) GlobalReceiverRegistryTools.GetGlobalReceiverDeviceList()), (Func<DeviceRegistryInfo, bool>) (item =>
      {
        if (item.ReferenceCount > 0)
          return item.UsbVidPid == "VID_22A3&PID_0047";
        else
          return false;
      }))).Count > 0)
      {
        flag = true;
      }
      else
      {
        string name1 = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall";
        using (RegistryKey registryKey1 = Registry.LocalMachine.OpenSubKey(name1))
        {
          if (registryKey1 != null)
          {
            string[] subKeyNames = registryKey1.GetSubKeyNames();
            if (subKeyNames != null)
            {
              foreach (string name2 in subKeyNames)
              {
                using (RegistryKey registryKey2 = registryKey1.OpenSubKey(name2))
                {
                  if (registryKey2 != null)
                  {
                    string str1 = registryKey2.GetValue("Publisher") as string;
                    if (!string.IsNullOrEmpty(str1))
                    {
                      if (str1 == "DexCom, Inc.")
                      {
                        string str2 = registryKey2.GetValue("DisplayVersion") as string;
                        if (!string.IsNullOrEmpty(str2))
                        {
                          if (str2 == "05/24/2010 1.0.0.2")
                          {
                            uninstallString = registryKey2.GetValue("UninstallString") as string;
                            if (!string.IsNullOrEmpty(uninstallString))
                            {
                              string str3 = uninstallString;
                              string str4 = "dpinst.exe /u ";
                              int num = str3.IndexOf(str4, 0, str3.Length, StringComparison.OrdinalIgnoreCase);
                              if (num > 0)
                              {
                                if (str3.Length - (num + str4.Length) > 0)
                                {
                                  int startIndex = num + str4.Length;
                                  string path = str3.Substring(startIndex, str3.Length - startIndex);
                                  try
                                  {
                                    flag = File.Exists(Path.GetFullPath(path));
                                    break;
                                  }
                                  catch
                                  {
                                    break;
                                  }
                                }
                                else
                                  break;
                              }
                              else
                                break;
                            }
                            else
                              break;
                          }
                        }
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }
      return flag;
    }
  }
}

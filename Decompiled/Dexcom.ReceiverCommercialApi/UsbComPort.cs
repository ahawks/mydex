// Type: Dexcom.ReceiverApi.UsbComPort
// Assembly: Dexcom.ReceiverCommercialApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 3F623F65-908D-40A7-87D3-892C5D1FFA2E
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.ReceiverCommercialApi.dll

using Dexcom.Common;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security;
using System.Threading;
using System.Timers;

namespace Dexcom.ReceiverApi
{
  [SecuritySafeCritical]
  public class UsbComPort : IDisposable
  {
    private string m_strDeviceName = string.Empty;
    private string m_strPortName = string.Empty;
    private System.Timers.Timer m_timer = new System.Timers.Timer(200.0);
    private int m_portNumber;
    private bool m_isPortConnected;
    private bool m_isPortDetached;
    private Mutex m_portMutex;
    private bool m_isMutexCreator;
    private bool m_isDisposed;

    public string DeviceName
    {
      get
      {
        return this.m_strDeviceName;
      }
    }

    public string PortName
    {
      get
      {
        return this.m_strPortName;
      }
    }

    public int PortNumber
    {
      get
      {
        return this.m_portNumber;
      }
    }

    public bool IsConnected
    {
      get
      {
        lock (this)
          return this.m_isPortConnected && !this.m_isPortDetached;
      }
    }

    public bool IsMutexCreator
    {
      get
      {
        return this.m_isMutexCreator;
      }
    }

    protected bool IsDisposed
    {
      get
      {
        lock (this)
          return this.m_isDisposed;
      }
    }

    private bool IsDetached
    {
      get
      {
        lock (this)
          return this.m_isPortDetached;
      }
      set
      {
        lock (this)
          this.m_isPortDetached = value;
      }
    }

    public event UsbComPort.UsbComPortEventHandler Detached;

    public event UsbComPort.UsbComPortEventHandler Attached;

    public UsbComPort(string deviceName, int portNumber)
    {
      this.m_strDeviceName = deviceName;
      this.m_strPortName = ReceiverValues.ComPortNamePrefix + portNumber.ToString();
      this.m_portNumber = portNumber;
      this.m_isPortConnected = UsbComPort.DoCheckIfPortExists(this.DeviceName, this.PortNumber);
      if (this.m_isPortConnected)
        this.m_portMutex = new Mutex(false, "DexGlobalUsbComPort" + this.m_portNumber.ToString(), out this.m_isMutexCreator);
      this.m_timer.Elapsed += new ElapsedEventHandler(this.timer_Elapsed);
      this.m_timer.AutoReset = false;
      this.m_timer.Start();
    }

    ~UsbComPort()
    {
      this.Cleanup();
    }

    public override string ToString()
    {
      return string.Format("Port {0} ({1}\\{2})", (object) this.m_portNumber, (object) this.m_strDeviceName, (object) this.m_strPortName);
    }

    public bool Reconnect()
    {
      if (this.IsConnected)
        return true;
      if (!UsbComPort.DoCheckIfPortExists(this.DeviceName, this.PortNumber))
        return false;
      lock (this)
      {
        this.m_isPortConnected = true;
        this.m_isPortDetached = false;
      }
      return true;
    }

    public bool AquireExclusiveUse(TimeSpan waitTime)
    {
      return this.m_portMutex.WaitOne(waitTime, false);
    }

    public void ReleaseExclusiveUse()
    {
      this.m_portMutex.ReleaseMutex();
    }

    [Conditional("DEBUG")]
    protected void CheckDisposed()
    {
      if (this.IsDisposed)
        throw new ObjectDisposedException(this.GetType().FullName, "Object is already disposed!");
    }

    public void Dispose()
    {
      lock (this)
      {
        if (this.m_isDisposed)
          return;
        this.m_isDisposed = true;
        this.Cleanup();
        GC.SuppressFinalize((object) this);
      }
    }

    protected virtual void Cleanup()
    {
      if (!this.IsDisposed)
        return;
      if (this.m_timer != null)
      {
        this.m_timer.Stop();
        this.m_timer = (System.Timers.Timer) null;
      }
      if (this.m_portMutex == null)
        return;
      this.m_portMutex.Close();
      this.m_portMutex = (Mutex) null;
    }

    private void DoSetConnected(bool isConnected)
    {
      lock (this)
        this.m_isPortConnected = isConnected;
    }

    private void timer_Elapsed(object sender, ElapsedEventArgs e)
    {
      if (this.IsDisposed)
        return;
      if (this.m_timer == null)
        return;
      try
      {
        bool flag1 = !UsbComPort.DoCheckIfPortExists(this.m_strDeviceName, this.m_portNumber);
        bool flag2 = false;
        lock (this)
        {
          if (this.m_isDisposed)
            return;
          if (this.m_isPortDetached != flag1)
          {
            this.m_isPortDetached = flag1;
            if (flag1)
              this.m_isPortConnected = false;
            flag2 = true;
          }
        }
        if (!flag2)
          return;
        if (flag1)
          EventUtils.FireEvent((Delegate) this.Detached, (object) this, (object) EventArgs.Empty);
        else
          EventUtils.FireEvent((Delegate) this.Attached, (object) this, (object) EventArgs.Empty);
      }
      finally
      {
        if (this.m_timer != null)
          this.m_timer.Start();
      }
    }

    public static UsbComPort LookupPort(int portNumber)
    {
      if (portNumber <= 0)
        throw new ArgumentException("Port number must be greater than 0.", "portNumber");
      else
        return UsbComPort.DoLookupPort(portNumber);
    }

    public static UsbComPort LookupPort()
    {
      return UsbComPort.DoLookupPort(0);
    }

    public static List<int> LookupReceiverComPortList()
    {
      List<int> list = new List<int>();
      foreach (DeviceRegistryInfo deviceRegistryInfo in GlobalReceiverRegistryTools.GetGlobalReceiverDeviceList())
      {
        if (deviceRegistryInfo.ReferenceCount > 0 && deviceRegistryInfo.PortNumber > 0)
          list.Add(deviceRegistryInfo.PortNumber);
      }
      return list;
    }

    private static UsbComPort DoLookupPort(int portNumber)
    {
      UsbComPort usbComPort = (UsbComPort) null;
      foreach (DeviceRegistryInfo deviceRegistryInfo in GlobalReceiverRegistryTools.GetGlobalReceiverDeviceList())
      {
        if (deviceRegistryInfo.ReferenceCount > 0 && deviceRegistryInfo.PortNumber > 0 && (portNumber == 0 || portNumber == deviceRegistryInfo.PortNumber))
        {
          usbComPort = new UsbComPort(UsbComPort.DoGetDeviceNameForPort(deviceRegistryInfo.PortNumber), deviceRegistryInfo.PortNumber);
          break;
        }
      }
      return usbComPort;
    }

    private static string DoGetDeviceNameForPort(int portNumber)
    {
      string str1 = string.Empty;
      using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("HARDWARE\\DEVICEMAP\\SERIALCOMM"))
      {
        if (registryKey != null)
        {
          foreach (string name in registryKey.GetValueNames())
          {
            string str2 = registryKey.GetValue(name) as string;
            if (str2 != null)
            {
              if (str2.StartsWith(ReceiverValues.ComPortNamePrefix))
              {
                try
                {
                  int num = int.Parse(str2.Substring(ReceiverValues.ComPortNamePrefix.Length));
                  if (portNumber == num)
                  {
                    str1 = name;
                    break;
                  }
                }
                catch
                {
                }
              }
            }
          }
        }
      }
      return str1;
    }

    private static bool DoCheckIfPortExists(string deviceName, int portNumber)
    {
      bool flag = false;
      using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("HARDWARE\\DEVICEMAP\\SERIALCOMM"))
      {
        if (registryKey != null)
        {
          string str = registryKey.GetValue(deviceName) as string;
          if (str != null)
          {
            if (str.StartsWith(ReceiverValues.ComPortNamePrefix))
            {
              try
              {
                int num = int.Parse(str.Substring(ReceiverValues.ComPortNamePrefix.Length));
                if (portNumber == num)
                  flag = true;
              }
              catch
              {
              }
            }
          }
        }
      }
      return flag;
    }

    public delegate void UsbComPortEventHandler(object sender, EventArgs e);
  }
}

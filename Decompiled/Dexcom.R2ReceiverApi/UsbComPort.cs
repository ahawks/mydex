// Type: Dexcom.R2Receiver.UsbComPort
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using Dexcom.Common;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security;
using System.Threading;
using System.Timers;

namespace Dexcom.R2Receiver
{
  //[SecuritySafeCritical]
  public class UsbComPort : IDisposable
  {
    private static readonly string PortNamePrefix = "COM";
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

    static UsbComPort()
    {
    }

    public UsbComPort(string deviceName, int portNumber)
    {
      this.m_strDeviceName = deviceName;
      this.m_strPortName = UsbComPort.PortNamePrefix + portNumber.ToString();
      this.m_portNumber = portNumber;
      this.m_isPortConnected = UsbComPort.DoCheckIfPortExists(this.DeviceName, this.PortNumber);
      if (this.m_isPortConnected)
        this.m_portMutex = new Mutex(false, "DexUsbComPort" + this.m_portNumber.ToString(), out this.m_isMutexCreator);
      this.m_timer.Elapsed += new ElapsedEventHandler(this.timer_Elapsed);
      this.m_timer.AutoReset = true;
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
      this.m_timer.Stop();
      if (this.m_portMutex == null)
        return;
      try
      {
        this.m_portMutex.ReleaseMutex();
      }
      catch
      {
      }
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
      string deviceName = this.m_strDeviceName;
      int portNumber = this.m_portNumber;
      this.m_timer.Enabled = false;
      bool flag1 = !UsbComPort.DoCheckIfPortExists(deviceName, portNumber);
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
      if (flag2)
      {
        if (flag1)
          EventUtils.FireEvent((Delegate) this.Detached, (object) this, (object) EventArgs.Empty);
        else
          EventUtils.FireEvent((Delegate) this.Attached, (object) this, (object) EventArgs.Empty);
      }
      this.m_timer.Enabled = true;
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
      using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("HARDWARE\\DEVICEMAP\\SERIALCOMM"))
      {
        if (registryKey != null)
        {
          //string strA1 = "\\Device\\Silabser";
            string strA1 = "\\Device\\USBSER000";
          string strA2 = "\\Device\\slabser";
          Console.WriteLine("STRA1: " + strA1 + "    STRA2: " + strA2); 
          foreach (string str1 in registryKey.GetValueNames())
          {
              Console.WriteLine("Str1: " + str1);
              int a = string.Compare(strA1, 0, str1, 0, strA1.Length, true);
              int b = string.Compare(strA2, 0, str1, 0, strA2.Length, true);
            if ( a == 0 || b == 0)
            {
              string str2 = registryKey.GetValue(str1) as string;
              Console.WriteLine("Str2: " + str2);
              if (str2 != null)
              {
                if (str2.StartsWith(UsbComPort.PortNamePrefix))
                {
                  try
                  {
                    int num = int.Parse(str2.Substring(UsbComPort.PortNamePrefix.Length));
                    list.Add(num);
                  }
                  catch
                  {
                  }
                }
              }
            }
          }
        }
      }
      return list;
    }

    private static UsbComPort DoLookupPort(int portNumber)
    {
      UsbComPort usbComPort = (UsbComPort) null;
      using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("HARDWARE\\DEVICEMAP\\SERIALCOMM"))
      {
        if (registryKey != null)
        {
          string strA1 = "\\Device\\Silabser";
          string strA2 = "\\Device\\slabser";
          foreach (string str1 in registryKey.GetValueNames())
          {
            if (portNumber != 0 || string.Compare(strA1, 0, str1, 0, strA1.Length, true) == 0 || string.Compare(strA2, 0, str1, 0, strA2.Length, true) == 0)
            {
              string str2 = registryKey.GetValue(str1) as string;
              if (str2 != null)
              {
                if (str2.StartsWith(UsbComPort.PortNamePrefix))
                {
                  try
                  {
                    int portNumber1 = int.Parse(str2.Substring(UsbComPort.PortNamePrefix.Length));
                    if (portNumber != 0)
                    {
                      if (portNumber != portNumber1)
                        continue;
                    }
                    usbComPort = new UsbComPort(str1, portNumber1);
                    break;
                  }
                  catch
                  {
                  }
                }
              }
            }
          }
        }
      }
      return usbComPort;
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
            if (str.StartsWith(UsbComPort.PortNamePrefix))
            {
              try
              {
                int num = int.Parse(str.Substring(UsbComPort.PortNamePrefix.Length));
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

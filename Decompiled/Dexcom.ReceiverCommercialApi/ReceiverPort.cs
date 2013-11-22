// Type: Dexcom.ReceiverApi.ReceiverPort
// Assembly: Dexcom.ReceiverCommercialApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 3F623F65-908D-40A7-87D3-892C5D1FFA2E
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.ReceiverCommercialApi.dll

using Dexcom.Common;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Security;
using System.Threading;

namespace Dexcom.ReceiverApi
{
  [SecuritySafeCritical]
  public class ReceiverPort : IDisposable
  {
    private IntPtr m_hPort = Win32PortIO.INVALID_HANDLE_VALUE;
    private byte[] m_scratchBytes = new byte[128];
    private byte[] m_scratchBlock = new byte[32774];
    private UsbComPort m_virtualPort;
    private ReceiverPort.BaudRate m_currentBaudRate;
    private bool m_isDisposed;

    public string PortName
    {
      get
      {
        return this.m_virtualPort.PortName;
      }
    }

    public string PortDeviceName
    {
      get
      {
        return this.m_virtualPort.DeviceName;
      }
    }

    public int PortNumber
    {
      get
      {
        return this.m_virtualPort.PortNumber;
      }
    }

    public ReceiverPort.BaudRate CurrentBaudRate
    {
      get
      {
        return this.m_currentBaudRate;
      }
    }

    public bool IsOpen
    {
      get
      {
        return this.m_hPort != Win32PortIO.INVALID_HANDLE_VALUE;
      }
    }

    public bool IsReceiverAttached
    {
      get
      {
        bool flag = false;
        if (this.m_virtualPort.IsConnected)
          flag = true;
        return flag;
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

    public event BytesTransferredEventHandler BytesWritten;

    public event BytesTransferredEventHandler BytesRead;

    public ReceiverPort(UsbComPort port)
    {
      if (port == null)
        throw new ArgumentNullException("port");
      this.m_virtualPort = port;
      this.m_virtualPort.Detached += new UsbComPort.UsbComPortEventHandler(this.OnPortDetached);
    }

    ~ReceiverPort()
    {
      this.Cleanup();
    }

    private void FireBytesWrittenEvent(byte[] bytes, int length)
    {
      Delegate del = (Delegate) this.BytesWritten;
      if (del == null || del.GetInvocationList().Length <= 0)
        return;
      EventUtils.FireEvent(del, (object) bytes, (object) length);
    }

    private void FireBytesReadEvent(byte[] bytes, int length)
    {
      Delegate del = (Delegate) this.BytesRead;
      if (del == null || del.GetInvocationList().Length <= 0)
        return;
      EventUtils.FireEventAsync(del, (object) bytes, (object) length);
    }

    public bool AttachReceiver()
    {
      return this.m_virtualPort.Reconnect();
    }

    public void FlushPort()
    {
      if (this.m_hPort == Win32PortIO.INVALID_HANDLE_VALUE)
        throw new DexComException("Receiver port is closed!");
      Win32PortIO.FlushPort(this.m_hPort);
    }

    public void SetShortTimeouts()
    {
      if (this.m_hPort == Win32PortIO.INVALID_HANDLE_VALUE)
        throw new DexComException("Receiver port is closed!");
      Win32PortIO.COMMTIMEOUTS lpCommTimeouts;
      lpCommTimeouts.ReadIntervalTimeout = 0U;
      lpCommTimeouts.ReadTotalTimeoutMultiplier = 1U;
      lpCommTimeouts.ReadTotalTimeoutConstant = 100U;
      lpCommTimeouts.WriteTotalTimeoutMultiplier = 1U;
      lpCommTimeouts.WriteTotalTimeoutConstant = 100U;
      if (Win32PortIO.SetCommTimeouts(this.m_hPort, ref lpCommTimeouts))
        return;
      Win32Exception win32Exception = new Win32Exception();
      throw new Win32Exception(win32Exception.NativeErrorCode, string.Format("Failed Win32PortIO.SetCommTimeouts() : {0} (0x{0:X}) : {1}", (object) win32Exception.NativeErrorCode, (object) win32Exception.Message));
    }

    public void SetMediumTimeouts()
    {
      if (this.m_hPort == Win32PortIO.INVALID_HANDLE_VALUE)
        throw new DexComException("Receiver port is closed!");
      Win32PortIO.COMMTIMEOUTS lpCommTimeouts;
      lpCommTimeouts.ReadIntervalTimeout = 0U;
      lpCommTimeouts.ReadTotalTimeoutMultiplier = 2U;
      lpCommTimeouts.ReadTotalTimeoutConstant = 1000U;
      lpCommTimeouts.WriteTotalTimeoutMultiplier = 2U;
      lpCommTimeouts.WriteTotalTimeoutConstant = 1000U;
      if (Win32PortIO.SetCommTimeouts(this.m_hPort, ref lpCommTimeouts))
        return;
      Win32Exception win32Exception = new Win32Exception();
      throw new Win32Exception(win32Exception.NativeErrorCode, string.Format("Failed Win32PortIO.SetCommTimeouts() : {0} (0x{0:X}) : {1}", (object) win32Exception.NativeErrorCode, (object) win32Exception.Message));
    }

    public void SetNormalTimeouts()
    {
      if (this.m_hPort == Win32PortIO.INVALID_HANDLE_VALUE)
        throw new DexComException("Receiver port is closed!");
      Win32PortIO.COMMTIMEOUTS lpCommTimeouts;
      lpCommTimeouts.ReadIntervalTimeout = 0U;
      lpCommTimeouts.ReadTotalTimeoutMultiplier = 2U;
      lpCommTimeouts.ReadTotalTimeoutConstant = 5100U;
      lpCommTimeouts.WriteTotalTimeoutMultiplier = 2U;
      lpCommTimeouts.WriteTotalTimeoutConstant = 1000U;
      if (Win32PortIO.SetCommTimeouts(this.m_hPort, ref lpCommTimeouts))
        return;
      Win32Exception win32Exception = new Win32Exception();
      throw new Win32Exception(win32Exception.NativeErrorCode, string.Format("Failed Win32PortIO.SetCommTimeouts() : {0} (0x{0:X}) : {1}", (object) win32Exception.NativeErrorCode, (object) win32Exception.Message));
    }

    public bool Open(ReceiverPort.BaudRate baudRate)
    {
      if (baudRate == ReceiverPort.BaudRate.Unknown)
        throw new ArgumentException("Baud rate not set to valid value.", "baudRate");
      if (this.m_hPort != Win32PortIO.INVALID_HANDLE_VALUE)
        throw new DexComException("Receiver port already open!");
      bool flag = false;
      this.m_virtualPort.Reconnect();
      if (this.m_virtualPort.IsConnected)
      {
        IntPtr file = Win32PortIO.CreateFile(string.Format("\\\\.\\{0}", (object) this.m_virtualPort.PortName), -1073741824, 0, Win32PortIO.NULL, 3, 0, Win32PortIO.NULL);
        if (file == Win32PortIO.INVALID_HANDLE_VALUE)
        {
          Win32Exception win32Exception = new Win32Exception();
          throw new Win32Exception(win32Exception.NativeErrorCode, string.Format("Failed Win32PortIO.CreateFile() : {0} (0x{0:X}) : {1}", (object) win32Exception.NativeErrorCode, (object) win32Exception.Message));
        }
        else
        {
          try
          {
            if (!Win32PortIO.SetupComm(file, 1200, 1200))
            {
              Win32Exception win32Exception = new Win32Exception();
              throw new Win32Exception(win32Exception.NativeErrorCode, string.Format("Failed Win32PortIO.SetupComm() : {0} (0x{0:X}) : {1}", (object) win32Exception.NativeErrorCode, (object) win32Exception.Message));
            }
            else
            {
              this.DoSetBaudRate(file, baudRate);
              Win32PortIO.COMMTIMEOUTS lpCommTimeouts;
              lpCommTimeouts.ReadIntervalTimeout = 0U;
              lpCommTimeouts.ReadTotalTimeoutMultiplier = 2U;
              lpCommTimeouts.ReadTotalTimeoutConstant = 1000U;
              lpCommTimeouts.WriteTotalTimeoutMultiplier = 2U;
              lpCommTimeouts.WriteTotalTimeoutConstant = 1000U;
              if (!Win32PortIO.SetCommTimeouts(file, ref lpCommTimeouts))
              {
                Win32Exception win32Exception = new Win32Exception();
                throw new Win32Exception(win32Exception.NativeErrorCode, string.Format("Failed Win32PortIO.SetCommTimeouts() : {0} (0x{0:X}) : {1}", (object) win32Exception.NativeErrorCode, (object) win32Exception.Message));
              }
              else
              {
                Win32PortIO.FlushPort(file);
                this.m_hPort = file;
                this.SetNormalTimeouts();
                flag = true;
                this.m_currentBaudRate = baudRate;
              }
            }
          }
          catch
          {
            if (file != Win32PortIO.INVALID_HANDLE_VALUE)
            {
              lock (this)
              {
                if (Win32PortIO.CloseHandle(file))
                {
                  IntPtr local_2_1 = Win32PortIO.INVALID_HANDLE_VALUE;
                }
              }
            }
            throw;
          }
        }
      }
      return flag;
    }

    public void Close()
    {
      if (this.m_hPort != Win32PortIO.INVALID_HANDLE_VALUE)
      {
        bool flag = false;
        lock (this)
        {
          flag = Win32PortIO.CloseHandle(this.m_hPort);
          if (flag)
            this.m_hPort = Win32PortIO.INVALID_HANDLE_VALUE;
        }
        if (!flag)
        {
          Win32Exception win32Exception = new Win32Exception();
          throw new Win32Exception(win32Exception.NativeErrorCode, string.Format("Failed Win32PortIO.CloseHandle() : {0} (0x{0:X}) : {1}", (object) win32Exception.NativeErrorCode, (object) win32Exception.Message));
        }
        else
          Thread.Sleep(100);
      }
      this.m_currentBaudRate = ReceiverPort.BaudRate.Unknown;
    }

    public void WriteByte(byte byteValue)
    {
      this.m_scratchBytes[0] = byteValue;
      this.DoWriteBytes(this.m_scratchBytes, 1);
    }

    public void WriteBytes(byte[] bytes)
    {
      this.DoWriteBytes(bytes, bytes.Length);
    }

    public void WriteBytes(byte[] bytes, int length)
    {
      this.DoWriteBytes(bytes, length);
    }

    public void WriteBytes(short val)
    {
      byte[] bytes = BitConverter.GetBytes(val);
      this.DoWriteBytes(bytes, bytes.Length);
    }

    public void WriteBytes(int val)
    {
      byte[] bytes = BitConverter.GetBytes(val);
      this.DoWriteBytes(bytes, bytes.Length);
    }

    public void WriteBytes(ushort val)
    {
      byte[] bytes = BitConverter.GetBytes(val);
      this.DoWriteBytes(bytes, bytes.Length);
    }

    public void WriteBytes(uint val)
    {
      byte[] bytes = BitConverter.GetBytes(val);
      this.DoWriteBytes(bytes, bytes.Length);
    }

    private int DoWriteFile(IntPtr handle, byte[] bytes, int numBytesToWrite, out int numBytesWritten, IntPtr lpOverlapped)
    {
      int num = Win32PortIO.WriteFile(handle, bytes, numBytesToWrite, out numBytesWritten, lpOverlapped);
      if (num != 0)
        Win32PortIO.FlushFileBuffers(handle);
      this.FireBytesWrittenEvent(bytes, numBytesWritten);
      return num;
    }

    private int DoReadFile(IntPtr handle, byte[] bytes, int numBytesToRead, out int numBytesRead, IntPtr lpOverlapped)
    {
      int num = Win32PortIO.ReadFile(handle, bytes, numBytesToRead, out numBytesRead, lpOverlapped);
      this.FireBytesReadEvent(bytes, numBytesRead);
      return num;
    }

    private void DoWriteBytes(byte[] bytes, int length)
    {
      if (this.m_hPort == Win32PortIO.INVALID_HANDLE_VALUE)
        throw new DexComException("Receiver port is closed!");
      int numBytesWritten = 0;
      if (this.DoWriteFile(this.m_hPort, bytes, length, out numBytesWritten, Win32PortIO.NULL) == 0)
      {
        Win32Exception win32Exception = new Win32Exception();
        if ((long) win32Exception.NativeErrorCode == 995L)
          throw win32Exception;
        else
          throw new DexComException(string.Format("Failed Win32PortIO.WriteFile() : {0} (0x{0:X}) : {1}", (object) win32Exception.NativeErrorCode, (object) win32Exception.Message));
      }
      else if (numBytesWritten != length)
        throw new DexComException("Failed to write correct number of bytes in call to Win32PortIO.WriteFile()");
    }

    public byte ReadByte()
    {
      return this.DoReadBytes(1)[0];
    }

    public byte[] ReadBytes(int length)
    {
      return this.DoReadBytes(length);
    }

    public void ReadBytes(byte[] bytes)
    {
      Array.Copy((Array) this.DoReadBytes(bytes.Length), 0, (Array) bytes, 0, bytes.Length);
    }

    private byte[] DoReadBytes(int length)
    {
      if (this.m_hPort == Win32PortIO.INVALID_HANDLE_VALUE)
        throw new DexComException("Receiver port is closed!");
      if (length < 0 || length > this.m_scratchBlock.Length)
        throw new ArgumentOutOfRangeException("length", "Length of bytes to read must be greater than zero and less than or equal to max block size of " + this.m_scratchBlock.Length.ToString());
      byte[] bytes = length >= this.m_scratchBytes.Length ? this.m_scratchBlock : this.m_scratchBytes;
      int numBytesRead = 0;
      if (this.DoReadFile(this.m_hPort, bytes, length, out numBytesRead, Win32PortIO.NULL) == 0)
      {
        Win32Exception win32Exception = new Win32Exception();
        throw new DexComException(string.Format("Failed Win32PortIO.ReadFile() : {0} (0x{0:X}) : {1}", (object) win32Exception.NativeErrorCode, (object) win32Exception.Message), (Exception) win32Exception);
      }
      else
      {
        if (numBytesRead == 0)
          throw new TimedOutException(string.Format("Timed out reading {0} bytes from {1}.", (object) length, (object) this.m_virtualPort.PortName));
        if (numBytesRead != length)
          throw new DexComException("Failed to read correct number of bytes in call to Win32PortIO.ReadFile()");
        else
          return bytes;
      }
    }

    public void ChangeBaudRate(ReceiverPort.BaudRate baudRate)
    {
      if (baudRate == ReceiverPort.BaudRate.Unknown)
        throw new ArgumentException("Baud rate not set to valid value.", "baudRate");
      if (this.m_hPort == Win32PortIO.INVALID_HANDLE_VALUE)
        throw new DexComException("Receiver port is closed!");
      if (baudRate == this.m_currentBaudRate)
        return;
      this.DoSetBaudRate(this.m_hPort, baudRate);
      this.m_currentBaudRate = baudRate;
    }

    private void DoSetBaudRate(IntPtr hPort, ReceiverPort.BaudRate baudRate)
    {
    }

    private void OnPortDetached(object sender, EventArgs e)
    {
      this.Close();
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
      if (this.IsDisposed && this.m_virtualPort != null)
      {
        this.m_virtualPort.Detached -= new UsbComPort.UsbComPortEventHandler(this.OnPortDetached);
        this.m_virtualPort.Dispose();
      }
      if (!(this.m_hPort != Win32PortIO.INVALID_HANDLE_VALUE))
        return;
      lock (this)
      {
        if (!Win32PortIO.CloseHandle(this.m_hPort))
          return;
        this.m_hPort = Win32PortIO.INVALID_HANDLE_VALUE;
      }
    }

    public enum BaudRate
    {
      Unknown = 0,
      Firmware = 115200,
    }
  }
}

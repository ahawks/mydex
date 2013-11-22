// Type: Dexcom.R2Receiver.ReceiverPort
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using Dexcom.Common;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace Dexcom.R2Receiver
{
  [SecuritySafeCritical]
  public class ReceiverPort : IDisposable
  {
    private IntPtr m_hPort = Win32PortIO.INVALID_HANDLE_VALUE;
    private byte[] m_scratchBytes = new byte[128];
    private byte[] m_scratchBlock = new byte[32774];
    private UsbComPort m_virtualPort;
    private ReceiverPort.BaudRate m_currentBaudRate;
    private bool m_isTransferring;
    private bool m_requestCancelTransfer;
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

    private bool Transferring
    {
      get
      {
        lock (this)
          return this.m_isTransferring;
      }
      set
      {
        lock (this)
          this.m_isTransferring = value;
      }
    }

    private bool RequestCancelTransfer
    {
      get
      {
        lock (this)
          return this.m_requestCancelTransfer;
      }
      set
      {
        lock (this)
          this.m_requestCancelTransfer = value;
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

    public event ProgressEventHandler TransferProgress;

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

    public void CancelTransfer()
    {
      lock (this)
      {
        if (!this.m_isTransferring || this.m_requestCancelTransfer)
          return;
        this.m_requestCancelTransfer = true;
      }
    }

    public bool IsTransferring()
    {
      lock (this)
        return this.m_isTransferring;
    }

    private void FireProgressEvent(ProgressEventArgs.ProgressState state, int count, int total)
    {
      this.FireProgressEvent(state, count, total, (BackgroundWorker) null, 0);
    }

    private void FireProgressEvent(ProgressEventArgs.ProgressState state, int count, int total, BackgroundWorker backgroundWorker, int backgroundWorkerId)
    {
      Delegate del = (Delegate) this.TransferProgress;
      bool flag1 = del != null && del.GetInvocationList().Length > 0;
      bool flag2 = backgroundWorker != null;
      if (!flag1 && !flag2)
        return;
      ProgressEventArgs progressEventArgs = new ProgressEventArgs(state, count, total);
      if (flag1)
        EventUtils.FireEvent(del, (object) this, (object) progressEventArgs);
      if (!flag2)
        return;
      backgroundWorker.ReportProgress(backgroundWorkerId, (object) progressEventArgs);
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

    public void ResetAndFlushPort()
    {
      if (this.m_hPort == Win32PortIO.INVALID_HANDLE_VALUE)
        throw new ApplicationException("Receiver port is closed!");
      Win32PortIO.ResetAndFlushPort(this.m_hPort);
    }

    public void FlushPort()
    {
      if (this.m_hPort == Win32PortIO.INVALID_HANDLE_VALUE)
        throw new ApplicationException("Receiver port is closed!");
      Win32PortIO.FlushPort(this.m_hPort);
    }

    public void SetShortTimeouts()
    {
      if (this.m_hPort == Win32PortIO.INVALID_HANDLE_VALUE)
        throw new ApplicationException("Receiver port is closed!");
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
        throw new ApplicationException("Receiver port is closed!");
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
        throw new ApplicationException("Receiver port is closed!");
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
      return this.Open(baudRate, true);
    }

    public bool Open(ReceiverPort.BaudRate baudRate, bool doResetAndFlush)
    {
      if (baudRate == ReceiverPort.BaudRate.Unknown)
        throw new ArgumentException("Baud rate not set to valid value.", "baudRate");
      if (this.m_hPort != Win32PortIO.INVALID_HANDLE_VALUE)
        throw new ApplicationException("Receiver port already open!");
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
                if (doResetAndFlush)
                  Win32PortIO.ResetAndFlushPort(file);
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
        throw new ApplicationException("Receiver port is closed!");
      int numBytesWritten = 0;
      if (this.DoWriteFile(this.m_hPort, bytes, length, out numBytesWritten, Win32PortIO.NULL) == 0)
      {
        Win32Exception win32Exception = new Win32Exception();
        if ((long) win32Exception.NativeErrorCode == 995L)
          throw win32Exception;
        else
          throw new ApplicationException(string.Format("Failed Win32PortIO.WriteFile() : {0} (0x{0:X}) : {1}", (object) win32Exception.NativeErrorCode, (object) win32Exception.Message));
      }
      else if (numBytesWritten != length)
        throw new ApplicationException("Failed to write correct number of bytes in call to Win32PortIO.WriteFile()");
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
        throw new ApplicationException("Receiver port is closed!");
      if (length < 0 || length > this.m_scratchBlock.Length)
        throw new ArgumentOutOfRangeException("length", "Length of bytes to read must be greater than zero and less than or equal to max block size of " + this.m_scratchBlock.Length.ToString());
      byte[] bytes = length >= this.m_scratchBytes.Length ? this.m_scratchBlock : this.m_scratchBytes;
      int numBytesRead = 0;
      if (this.DoReadFile(this.m_hPort, bytes, length, out numBytesRead, Win32PortIO.NULL) == 0)
      {
        Win32Exception win32Exception = new Win32Exception();
        throw new ApplicationException(string.Format("Failed Win32PortIO.ReadFile() : {0} (0x{0:X}) : {1}", (object) win32Exception.NativeErrorCode, (object) win32Exception.Message), (Exception) win32Exception);
      }
      else
      {
        if (numBytesRead == 0)
          throw new TimedOutException(string.Format("Timed out reading {0} bytes from {1}.", (object) length, (object) this.m_virtualPort.PortName));
        if (numBytesRead != length)
          throw new ApplicationException("Failed to read correct number of bytes in call to Win32PortIO.ReadFile()");
        else
          return bytes;
      }
    }

    public void WriteXModem(byte[] transferBytes)
    {
      if (this.m_hPort == Win32PortIO.INVALID_HANDLE_VALUE)
        throw new ApplicationException("Receiver port is closed!");
      int length = transferBytes.Length;
      int num1 = 1;
      int sourceIndex = 0;
      if (length % 1024 != 0)
        throw new ApplicationException("Receiver write data transfers must be a multiple of 1024 bytes!");
      this.Transferring = true;
      this.RequestCancelTransfer = false;
      this.FireProgressEvent(ProgressEventArgs.ProgressState.Start, transferBytes.Length - length, transferBytes.Length);
      try
      {
        while (length > 0)
        {
          this.FireProgressEvent(ProgressEventArgs.ProgressState.Run, transferBytes.Length - length, transferBytes.Length);
          int num2 = 0;
          while (!this.RequestCancelTransfer)
          {
            this.WriteByte((byte) 2);
            this.WriteByte((byte) (num1 & (int) byte.MaxValue));
            this.WriteByte((byte) ~(num1 & (int) byte.MaxValue));
            Array.Copy((Array) transferBytes, sourceIndex, (Array) this.m_scratchBlock, 0, 1024);
            ushort num3 = Crc.CalculateCrc16(this.m_scratchBlock, 0, 1024);
            this.DoWriteBytes(this.m_scratchBlock, 1024);
            this.WriteByte((byte) ((uint) num3 >> 8));
            this.WriteByte((byte) ((uint) num3 & (uint) byte.MaxValue));
            byte num4 = (byte) 0;
            try
            {
              num4 = this.ReadByte();
              if ((int) num4 != 6)
                ++num2;
            }
            catch (TimedOutException ex)
            {
              ++num2;
            }
            if (num2 >= 10)
              throw new ApplicationException("Gave up sending XModem packet after max retries!");
            if ((int) num4 == 6)
              break;
          }
          if (!this.RequestCancelTransfer)
          {
            ++num1;
            sourceIndex += 1024;
            length -= 1024;
          }
          else
            break;
        }
        if (this.RequestCancelTransfer)
        {
          this.SetMediumTimeouts();
          this.ResetAndFlushPort();
          this.SetNormalTimeouts();
          this.FireProgressEvent(ProgressEventArgs.ProgressState.Canceled, transferBytes.Length - length, transferBytes.Length);
        }
        else
        {
          this.WriteByte((byte) 4);
          this.FireProgressEvent(ProgressEventArgs.ProgressState.Done, transferBytes.Length - length, transferBytes.Length);
        }
      }
      catch
      {
        if (length > 0)
        {
          this.SetMediumTimeouts();
          this.ResetAndFlushPort();
          this.SetNormalTimeouts();
        }
        this.FireProgressEvent(ProgressEventArgs.ProgressState.Aborted, transferBytes.Length - length, transferBytes.Length);
        throw;
      }
      finally
      {
        this.Transferring = false;
        this.RequestCancelTransfer = false;
      }
    }

    public void ReadXModem(byte[] transferBytes, out int transferBytesRead)
    {
      this.ReadXModem(transferBytes, out transferBytesRead, (BackgroundWorker) null, 0);
    }

    public void ReadXModem(byte[] transferBytes, out int transferBytesRead, BackgroundWorker backgroundWorker, int backgroundWorkerId)
    {
      if (this.m_hPort == Win32PortIO.INVALID_HANDLE_VALUE)
        throw new ApplicationException("Receiver port is closed!");
      if (transferBytes.Length % 1024 != 0)
        throw new ApplicationException("Receiver read data transfers must be a multiple of 1024 bytes!");
      byte[] bytes = new byte[2];
      byte[] numArray = new byte[1030];
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      bool flag = false;
      transferBytesRead = 0;
      this.Transferring = true;
      this.RequestCancelTransfer = false;
      this.FireProgressEvent(ProgressEventArgs.ProgressState.Start, transferBytesRead, transferBytes.Length, backgroundWorker, backgroundWorkerId);
      try
      {
        this.SetShortTimeouts();
        while (!flag)
        {
          this.FireProgressEvent(ProgressEventArgs.ProgressState.Run, transferBytesRead, transferBytes.Length, backgroundWorker, backgroundWorkerId);
          if (!this.RequestCancelTransfer)
          {
            if (backgroundWorker != null && backgroundWorker.CancellationPending)
            {
              this.RequestCancelTransfer = true;
              break;
            }
            else
            {
              int numBytesRead = 0;
              if (this.DoReadFile(this.m_hPort, bytes, 1, out numBytesRead, Win32PortIO.NULL) != 0 && numBytesRead == 1)
              {
                int num4 = 0;
                switch (bytes[0])
                {
                  case (byte) 1:
                    num4 = 128;
                    break;
                  case (byte) 2:
                    num4 = 1024;
                    break;
                  case (byte) 4:
                    flag = true;
                    this.FireProgressEvent(ProgressEventArgs.ProgressState.Done, transferBytesRead, transferBytes.Length, backgroundWorker, backgroundWorkerId);
                    break;
                  case (byte) 24:
                    this.ResetAndFlushPort();
                    throw new ApplicationException("Read operation canceled by sender!");
                  default:
                    if (++num1 >= 5)
                      throw new ApplicationException("Too many bad reads #1a. Unexpected byte in packet. Try starting over!?");
                    else
                      break;
                }
                if (!flag && num4 > 0)
                {
                  if (this.DoReadFile(this.m_hPort, bytes, 2, out numBytesRead, Win32PortIO.NULL) == 0 || numBytesRead != 2)
                    throw new ApplicationException(ProgramContext.TryResourceLookup("Exception_TransferAbortAfterPacketTimeout", "Transfer aborted after timeout reading packet number.", new object[0]) + "[1]");
                  byte num5 = bytes[0];
                  byte num6 = bytes[1];
                  if ((int) num5 + (int) num6 != (int) byte.MaxValue)
                    throw new ApplicationException("Invalid packet number.  Transfer aborted!");
                  if (Win32PortIO.ReadFile(this.m_hPort, numArray, num4, out numBytesRead, Win32PortIO.NULL) == 0 || numBytesRead != num4)
                    throw new ApplicationException(ProgramContext.TryResourceLookup("Exception_TransferAbortAfterPacketTimeout", "Transfer aborted after timeout reading packet.", new object[0]) + "[2]");
                  if (this.DoReadFile(this.m_hPort, bytes, 2, out numBytesRead, Win32PortIO.NULL) == 0 || numBytesRead != 2)
                    throw new ApplicationException(ProgramContext.TryResourceLookup("Exception_TransferAbortAfterPacketTimeout", "Transfer aborted after timeout reading packet CRC.", new object[0]) + "[3]");
                  if ((int) (ushort) (((uint) bytes[0] << 8) + (uint) bytes[1]) != (int) Crc.CalculateCrc16(numArray, 0, num4))
                  {
                    bytes[0] = (byte) 21;
                    int numBytesWritten = 0;
                    this.DoWriteFile(this.m_hPort, bytes, 1, out numBytesWritten, Win32PortIO.NULL);
                  }
                  else if ((num3 & (int) byte.MaxValue) != (int) num5)
                  {
                    ++num3;
                    bytes[0] = (byte) 6;
                    int numBytesWritten = 0;
                    this.DoWriteFile(this.m_hPort, bytes, 1, out numBytesWritten, Win32PortIO.NULL);
                    Array.Copy((Array) numArray, 0, (Array) transferBytes, transferBytesRead, num4);
                    transferBytesRead += num4;
                    num2 = 0;
                    num1 = 0;
                  }
                  else if ((num3 & (int) byte.MaxValue) == (int) num5)
                  {
                    bytes[0] = (byte) 6;
                    int numBytesWritten = 0;
                    this.DoWriteFile(this.m_hPort, bytes, 1, out numBytesWritten, Win32PortIO.NULL);
                  }
                }
              }
              else if (++num2 >= 5)
                throw new ApplicationException(ProgramContext.TryResourceLookup("Exception_TransferAbortAfterPacketTimeout", "Transfer aborted after timeouts reading transfer packets. Looks like receiver gave up sending. Try starting over!?", new object[0]) + "[4]");
            }
          }
          else
            break;
        }
        if (!this.RequestCancelTransfer)
          return;
        this.SetMediumTimeouts();
        this.ResetAndFlushPort();
        this.SetNormalTimeouts();
        this.FireProgressEvent(ProgressEventArgs.ProgressState.Canceled, transferBytesRead, transferBytes.Length, backgroundWorker, backgroundWorkerId);
      }
      catch
      {
        this.SetMediumTimeouts();
        this.ResetAndFlushPort();
        this.SetNormalTimeouts();
        this.FireProgressEvent(ProgressEventArgs.ProgressState.Aborted, transferBytesRead, transferBytes.Length, backgroundWorker, backgroundWorkerId);
        throw;
      }
      finally
      {
        this.Transferring = false;
        this.RequestCancelTransfer = false;
        this.SetNormalTimeouts();
      }
    }

    public void ChangeBaudRate(ReceiverPort.BaudRate baudRate)
    {
      if (baudRate == ReceiverPort.BaudRate.Unknown)
        throw new ArgumentException("Baud rate not set to valid value.", "baudRate");
      if (this.m_hPort == Win32PortIO.INVALID_HANDLE_VALUE)
        throw new ApplicationException("Receiver port is closed!");
      if (baudRate == this.m_currentBaudRate)
        return;
      this.DoSetBaudRate(this.m_hPort, baudRate);
      this.m_currentBaudRate = baudRate;
    }

    private void DoSetBaudRate(IntPtr hPort, ReceiverPort.BaudRate baudRate)
    {
      Win32PortIO.DCB lpDCB = new Win32PortIO.DCB();
      lpDCB.DCBlength = (uint) Marshal.SizeOf(typeof (Win32PortIO.DCB));
      if (!Win32PortIO.GetCommState(hPort, ref lpDCB))
      {
        Win32Exception win32Exception = new Win32Exception();
        throw new Win32Exception(win32Exception.NativeErrorCode, string.Format("Failed Win32PortIO.GetCommState() : {0} (0x{0:X}) : {1}", (object) win32Exception.NativeErrorCode, (object) win32Exception.Message));
      }
      else
      {
        lpDCB.BaudRate = (uint) baudRate;
        lpDCB.fBinary = 1U;
        lpDCB.fParity = 0U;
        lpDCB.fOutxCtsFlow = 0U;
        lpDCB.fOutxDsrFlow = 0U;
        lpDCB.fDtrControl = 1U;
        lpDCB.fDsrSensitivity = 0U;
        lpDCB.fTXContinueOnXoff = 1U;
        lpDCB.fOutX = 0U;
        lpDCB.fInX = 0U;
        lpDCB.fErrorChar = 0U;
        lpDCB.fNull = 0U;
        lpDCB.fRtsControl = 1U;
        lpDCB.fAbortOnError = 0U;
        lpDCB.ByteSize = (byte) 8;
        lpDCB.Parity = (byte) 0;
        lpDCB.StopBits = (byte) 0;
        if (Win32PortIO.SetCommState(hPort, ref lpDCB))
          return;
        Win32Exception win32Exception = new Win32Exception();
        throw new Win32Exception(win32Exception.NativeErrorCode, string.Format("Failed Win32PortIO.SetCommState() : {0} (0x{0:X}) : {1}", (object) win32Exception.NativeErrorCode, (object) win32Exception.Message));
      }
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
      Firmware = 38400,
      Bios = 115200,
    }
  }
}

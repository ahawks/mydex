// Type: Dexcom.R2Receiver.API
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using Dexcom.Common;
using Dexcom.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Xml;

namespace Dexcom.R2Receiver
{
  public class API : IDisposable
  {
    private ReceiverPort m_port;
    private uint m_cachedBiosRevision;
    private uint m_cachedFirmwareRevision;
    private HardwareConfiguration m_cachedHardwareConfig;
    private bool m_isDisposed;

    public string PortName
    {
      get
      {
        string str = string.Empty;
        if (this.m_port != null)
          str = this.m_port.PortName;
        return str;
      }
    }

    public string PortDeviceName
    {
      get
      {
        string str = string.Empty;
        if (this.m_port != null)
          str = this.m_port.PortDeviceName;
        return str;
      }
    }

    public int PortNumber
    {
      get
      {
        int num = 0;
        if (this.m_port != null)
          num = this.m_port.PortNumber;
        return num;
      }
    }

    public bool IsTransferring
    {
      get
      {
        bool flag = false;
        if (this.m_port != null)
          flag = this.m_port.IsTransferring();
        return flag;
      }
    }

    public uint CachedBiosRevision
    {
      get
      {
        if ((int) this.m_cachedBiosRevision == 0)
          this.m_cachedBiosRevision = this.ReadBiosHeader();
        return this.m_cachedBiosRevision;
      }
    }

    public uint CachedFirmwareRevision
    {
      get
      {
        if ((int) this.m_cachedFirmwareRevision == 0)
          this.m_cachedFirmwareRevision = this.ReadFirmwareHeader().m_revision;
        return this.m_cachedFirmwareRevision;
      }
    }

    public HardwareConfiguration CachedHardwareConfiguration
    {
      get
      {
        if (this.m_cachedHardwareConfig == null)
          this.m_cachedHardwareConfig = this.ReadHardwareConfiguration();
        return this.m_cachedHardwareConfig;
      }
    }

    public bool IsReceiverAttached
    {
      get
      {
        bool flag = false;
        if (this.m_port != null)
          flag = this.m_port.IsReceiverAttached;
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

    public API()
    {
      this.AttachReceiver();
    }

    public API(UsbComPort port)
    {
      if (port != null)
        this.AttachReceiver(port);
      else
        this.AttachReceiver();
    }

    ~API()
    {
      this.Cleanup();
    }

    public void CancelTransfer()
    {
      if (this.m_port == null)
        return;
      this.m_port.CancelTransfer();
    }

    public void AddTransferProgressHandler(ProgressEventHandler handler)
    {
      if (this.m_port == null)
        return;
      this.m_port.TransferProgress += handler;
    }

    public void RemoveTransferProgressHandler(ProgressEventHandler handler)
    {
      if (this.m_port == null)
        return;
      this.m_port.TransferProgress -= handler;
    }

    public void AddBytesWrittenEventHandler(BytesTransferredEventHandler handler)
    {
      if (this.m_port == null)
        return;
      this.m_port.BytesWritten += handler;
    }

    public void RemoveBytesWrittenEventHandler(BytesTransferredEventHandler handler)
    {
      if (this.m_port == null)
        return;
      this.m_port.BytesWritten -= handler;
    }

    public void AddBytesReadEventHandler(BytesTransferredEventHandler handler)
    {
      if (this.m_port == null)
        return;
      this.m_port.BytesRead += handler;
    }

    public void RemoveBytesReadEventHandler(BytesTransferredEventHandler handler)
    {
      if (this.m_port == null)
        return;
      this.m_port.BytesRead -= handler;
    }

    public bool AttachReceiver()
    {
      bool flag = false;
      if (this.m_port != null)
      {
        flag = this.m_port.AttachReceiver();
      }
      else
      {
        UsbComPort portWithReceiver = API.FindPortWithReceiver();
        if (portWithReceiver != null)
        {
          try
          {
            this.m_port = new ReceiverPort(portWithReceiver);
            flag = this.m_port.IsReceiverAttached;
          }
          catch
          {
            portWithReceiver.Dispose();
          }
        }
      }
      return flag;
    }

    public bool AttachReceiver(UsbComPort usbPort)
    {
      bool flag = false;
      if (this.m_port != null)
      {
        this.m_port.Dispose();
        this.m_port = (ReceiverPort) null;
      }
      if (usbPort != null)
      {
        try
        {
          this.m_port = new ReceiverPort(new UsbComPort(usbPort.DeviceName, usbPort.PortNumber));
          flag = this.m_port.IsReceiverAttached;
        }
        catch
        {
          if (this.m_port != null)
          {
            this.m_port.Dispose();
            this.m_port = (ReceiverPort) null;
          }
        }
      }
      return flag;
    }

    public static UsbComPort FindPortWithReceiver()
    {
      return API.FindPortWithReceiver(0);
    }

    public static UsbComPort FindPortWithReceiver(int startingPortNumber)
    {
      int portNumber1 = 0;
      if (startingPortNumber != 0 && API.IsReceiverOnPort(startingPortNumber))
        portNumber1 = startingPortNumber;
      if (portNumber1 == 0)
      {
        List<int> comportlist = UsbComPort.LookupReceiverComPortList();
        foreach (int portNumber2 in comportlist)
        {
            bool receiveronport = API.IsReceiverOnPort(portNumber2);
          if (portNumber2 != startingPortNumber && receiveronport)
          {
            portNumber1 = portNumber2;
            break;
          }
        }
      }
      if (portNumber1 != 0)
        return UsbComPort.LookupPort(portNumber1);
      else
        return (UsbComPort) null;
    }

    public static bool IsReceiverOnPort(int portNumber)
    {
      bool flag = false;
      try
      {
        using (UsbComPort port = UsbComPort.LookupPort(portNumber))
        {
          using (ReceiverPort receiverPort = new ReceiverPort(port))
          {
            receiverPort.Open(ReceiverPort.BaudRate.Firmware, false);
            receiverPort.SetMediumTimeouts();
            try
            {
              PacketAmIFirmwareCommand ifirmwareCommand = PacketAmIFirmwareCommand.Instance;
              receiverPort.WriteBytes(ifirmwareCommand.Bytes);
              PacketReadByteResponse readByteResponse = new PacketReadByteResponse(receiverPort.ReadBytes(PacketReadByteResponse.PacketSize));
              flag = true;
            }
            catch
            {
            }
            if (!flag)
            {
              receiverPort.ChangeBaudRate(ReceiverPort.BaudRate.Bios);
              try
              {
                PacketAmIFirmwareCommand ifirmwareCommand = PacketAmIFirmwareCommand.Instance;
                receiverPort.WriteBytes(ifirmwareCommand.Bytes);
                PacketReadByteResponse readByteResponse = new PacketReadByteResponse(receiverPort.ReadBytes(PacketReadByteResponse.PacketSize));
                flag = true;
              }
              catch
              {
              }
            }
            receiverPort.Close();
          }
        }
      }
      catch (Exception e)
      {
          Console.WriteLine("Exception caught... ");
      }
      return flag;
    }

    public void Disconnect()
    {
      if (this.m_port == null)
        return;
      this.m_port.Close();
      Thread.Sleep(300);
    }

    public bool Connect()
    {
      bool isReceiverAttached;
      bool isFirmwareMode;
      return this.Connect(out isReceiverAttached, out isFirmwareMode);
    }

    public bool Connect(out bool isReceiverAttached, out bool isFirmwareMode)
    {
      this.m_cachedBiosRevision = 0U;
      this.m_cachedFirmwareRevision = 0U;
      this.m_cachedHardwareConfig = (HardwareConfiguration) null;
      bool flag = false;
      isFirmwareMode = false;
      ReceiverPort.BaudRate baudRate = ReceiverPort.BaudRate.Unknown;
      isReceiverAttached = this.AttachReceiver();
      if (isReceiverAttached && this.m_port.IsOpen)
      {
        this.m_port.Close();
        Thread.Sleep(300);
      }
      isReceiverAttached = this.AttachReceiver();
      if (isReceiverAttached)
      {
        switch (this.m_port.CurrentBaudRate)
        {
          case ReceiverPort.BaudRate.Firmware:
          case ReceiverPort.BaudRate.Unknown:
            flag = this.ConnectToFirmware(false);
            if (flag)
            {
              baudRate = ReceiverPort.BaudRate.Firmware;
              isFirmwareMode = true;
              break;
            }
            else
              break;
        }
        if (!flag)
        {
          flag = this.ConnectToBios(false);
          if (flag)
          {
            baudRate = ReceiverPort.BaudRate.Bios;
            isFirmwareMode = false;
          }
        }
      }
      return flag;
    }

    public bool ConnectToBios(bool allowReceiverReset)
    {
      this.m_cachedBiosRevision = 0U;
      this.m_cachedFirmwareRevision = 0U;
      this.m_cachedHardwareConfig = (HardwareConfiguration) null;
      bool flag1 = false;
      if (this.AttachReceiver() && this.m_port.IsOpen)
      {
        this.m_port.Close();
        Thread.Sleep(300);
      }
      if (this.AttachReceiver())
      {
        try
        {
          if (this.m_port.Open(ReceiverPort.BaudRate.Bios))
          {
            bool flag2 = true;
            try
            {
              this.m_port.SetMediumTimeouts();
              flag2 = this.IsFirmwareMode();
            }
            catch (TimedOutException ex)
            {
            }
            if (!flag2)
            {
              this.m_port.SetNormalTimeouts();
              flag1 = true;
            }
            else if (allowReceiverReset)
            {
              this.m_port.ChangeBaudRate(ReceiverPort.BaudRate.Firmware);
              if (this.IsFirmwareMode())
              {
                this.ResetReceiver();
                this.m_port.ChangeBaudRate(ReceiverPort.BaudRate.Bios);
                this.m_port.SetShortTimeouts();
                Thread.Sleep(2400);
                int num = 100;
                while (num-- > 0)
                {
                  if (this.m_port.IsReceiverAttached)
                  {
                    try
                    {
                      this.m_port.WriteByte((byte) 2);
                      if ((int) this.m_port.ReadByte() == (int) Convert.ToByte('\r'))
                      {
                        try
                        {
                          do
                            ;
                          while ((int) this.m_port.ReadByte() == (int) Convert.ToByte('\r'));
                        }
                        catch (TimedOutException ex)
                        {
                        }
                        this.m_port.SetMediumTimeouts();
                        this.m_port.FlushPort();
                        this.m_port.SetNormalTimeouts();
                        flag1 = true;
                        break;
                      }
                    }
                    catch (TimedOutException ex)
                    {
                      this.m_port.FlushPort();
                    }
                  }
                  else
                    break;
                }
              }
            }
          }
        }
        catch (Win32Exception ex)
        {
          if ((long) ex.NativeErrorCode == 5L)
            throw new OnlineException(Dexcom.Common.ExceptionType.Unknown, ProgramContext.TryResourceLookup("Exception_ComPortInUseByAnotherApp", "Communication port appears to be in use by another application.", new object[0]), (Exception) ex);
          if ((long) ex.NativeErrorCode == 2L)
            throw new OnlineException(Dexcom.Common.ExceptionType.Unknown, ProgramContext.TryResourceLookup("Exception_CommunicationPortNotFound", "Communication port was not found.", new object[0]), (Exception) ex);
          throw;
        }
        catch (Exception ex)
        {
          throw;
        }
        finally
        {
          if (!flag1 && this.m_port.IsOpen)
            this.m_port.Close();
        }
      }
      return flag1;
    }

    public bool ConnectToBiosDuringManualReset()
    {
      this.m_cachedBiosRevision = 0U;
      this.m_cachedFirmwareRevision = 0U;
      this.m_cachedHardwareConfig = (HardwareConfiguration) null;
      bool flag = false;
      if (this.AttachReceiver() && this.m_port.IsOpen)
      {
        this.m_port.Close();
        Thread.Sleep(300);
      }
      if (this.AttachReceiver())
      {
        try
        {
          if (this.m_port.Open(ReceiverPort.BaudRate.Bios))
          {
            this.m_port.ChangeBaudRate(ReceiverPort.BaudRate.Bios);
            this.m_port.SetShortTimeouts();
            int num = 100;
            while (num-- > 0)
            {
              if (this.m_port.IsReceiverAttached)
              {
                try
                {
                  this.m_port.WriteByte((byte) 2);
                  if ((int) this.m_port.ReadByte() == (int) Convert.ToByte('\r'))
                  {
                    try
                    {
                      do
                        ;
                      while ((int) this.m_port.ReadByte() == (int) Convert.ToByte('\r'));
                    }
                    catch (TimedOutException ex)
                    {
                    }
                    this.m_port.SetMediumTimeouts();
                    this.m_port.FlushPort();
                    this.m_port.SetNormalTimeouts();
                    flag = true;
                    break;
                  }
                }
                catch (TimedOutException ex)
                {
                  this.m_port.FlushPort();
                }
              }
              else
                break;
            }
          }
        }
        catch (Win32Exception ex)
        {
          if ((long) ex.NativeErrorCode == 5L)
            throw new OnlineException(Dexcom.Common.ExceptionType.Unknown, ProgramContext.TryResourceLookup("Exception_ComPortInUseByAnotherApp", "Communication port appears to be in use by another application.", new object[0]), (Exception) ex);
          if ((long) ex.NativeErrorCode == 2L)
            throw new OnlineException(Dexcom.Common.ExceptionType.Unknown, ProgramContext.TryResourceLookup("Exception_CommunicationPortNotFound", "Communication port was not found.", new object[0]), (Exception) ex);
          throw;
        }
        catch (Exception ex)
        {
          throw;
        }
        finally
        {
          if (!flag && this.m_port.IsOpen)
            this.m_port.Close();
        }
      }
      return flag;
    }

    public bool ConnectToFirmware(bool allowReceiverReset)
    {
      this.m_cachedBiosRevision = 0U;
      this.m_cachedFirmwareRevision = 0U;
      this.m_cachedHardwareConfig = (HardwareConfiguration) null;
      bool flag1 = false;
      if (this.AttachReceiver() && this.m_port.IsOpen)
      {
        this.m_port.Close();
        Thread.Sleep(300);
      }
      if (this.AttachReceiver())
      {
        try
        {
          if (this.m_port.Open(ReceiverPort.BaudRate.Firmware))
          {
            bool flag2 = false;
            try
            {
              this.m_port.SetMediumTimeouts();
              flag2 = this.IsFirmwareMode();
            }
            catch (TimedOutException ex)
            {
            }
            if (flag2)
            {
              this.m_port.SetNormalTimeouts();
              flag1 = true;
            }
            else if (allowReceiverReset)
            {
              this.m_port.ChangeBaudRate(ReceiverPort.BaudRate.Bios);
              if (!this.IsFirmwareMode())
              {
                this.ResetReceiver();
                Thread.Sleep(3000);
                this.m_port.ChangeBaudRate(ReceiverPort.BaudRate.Firmware);
                this.m_port.SetShortTimeouts();
                int num = 120;
                while (num-- > 0)
                {
                  if (this.m_port.IsReceiverAttached)
                  {
                    bool flag3 = false;
                    try
                    {
                      flag3 = this.IsFirmwareMode();
                    }
                    catch (TimedOutException ex)
                    {
                    }
                    if (flag3)
                    {
                      flag1 = true;
                      this.m_port.SetNormalTimeouts();
                      break;
                    }
                    else
                      Thread.Sleep(500);
                  }
                  else
                    break;
                }
              }
            }
          }
        }
        catch (Win32Exception ex)
        {
          if ((long) ex.NativeErrorCode == 5L)
            throw new OnlineException(Dexcom.Common.ExceptionType.Unknown, ProgramContext.TryResourceLookup("Exception_ComPortInUseByAnotherApp", "Communication port appears to be in use by another application.", new object[0]), (Exception) ex);
          if ((long) ex.NativeErrorCode == 2L)
            throw new OnlineException(Dexcom.Common.ExceptionType.Unknown, ProgramContext.TryResourceLookup("Exception_CommunicationPortNotFound", "Communication port was not found.", new object[0]), (Exception) ex);
          throw;
        }
        catch (Exception ex)
        {
          throw;
        }
        finally
        {
          if (!flag1 && this.m_port.IsOpen)
            this.m_port.Close();
        }
      }
      return flag1;
    }

    public IAsyncResult AsyncReadBlock(uint address, uint size, AsyncCallback callback, object asyncParam)
    {
      if (callback == null)
        throw new ArgumentNullException("callback", "Callback may not be null!");
      else
        return new API.ReadBlockDelegate(this.ReadBlock).BeginInvoke(address, size, callback, asyncParam);
    }

    public byte[] ReadBlock(uint address, uint size)
    {
      return this.ReadBlock(address, size, (BackgroundWorker) null, 0);
    }

    public byte[] ReadBlock(uint address, uint size, BackgroundWorker backgroundWorker, int backgroundWorkerId)
    {
      byte[] numArray = (byte[]) null;
      if (backgroundWorker == null || !backgroundWorker.CancellationPending)
      {
        if (this.m_port == null || !this.m_port.IsOpen)
          throw new ApplicationException("Invalid port or port closed!");
        if ((int) size == 0 || !this.IsBlockInFlash(address, size))
          throw new ApplicationException("Requested locataion/size is not in a valid flash memory range!");
        if (this.m_port.CurrentBaudRate == ReceiverPort.BaudRate.Firmware)
        {
          uint version = 1U;
          if (this.CachedFirmwareRevision >= 83887360U)
            version = 2U;
          if (this.IsBlockInLicense(address, size, version))
            throw new ApplicationException("Requested locataion/size is not in a valid flash memory range for FIRMWARE mode!");
        }
        this.m_port.WriteBytes(new PacketReadBlockCommand(address, size).Bytes);
        PacketAckResponse packetAckResponse = new PacketAckResponse(this.m_port.ReadBytes(PacketAckResponse.PacketSize));
        int length = (int) size;
        byte[] transferBytes = new byte[length];
        int transferBytesRead = 0;
        this.m_port.ReadXModem(transferBytes, out transferBytesRead, backgroundWorker, backgroundWorkerId);
        if (transferBytesRead == length)
          numArray = transferBytes;
        else if (backgroundWorker == null || !backgroundWorker.CancellationPending)
          throw new ApplicationException("Transfer ended unexpectedly and/or incomplete data found.  Operation canceled!");
      }
      return numArray;
    }

    public byte[] ReadErrorLog()
    {
      return this.ReadErrorLog((BackgroundWorker) null);
    }

    public byte[] ReadErrorLog(BackgroundWorker backgroundWorker)
    {
      byte[] numArray = (byte[]) null;
      if (backgroundWorker == null || !backgroundWorker.CancellationPending)
      {
        if (this.m_port == null || !this.m_port.IsOpen)
          throw new ApplicationException("Invalid port or port closed!");
        uint num = 1U;
        if (this.m_port.CurrentBaudRate == ReceiverPort.BaudRate.Firmware)
        {
          if (this.CachedFirmwareRevision >= 83887360U)
            num = 2U;
        }
        else if (this.CachedBiosRevision >= 34145280U)
          num = 2U;
        numArray = this.ReadBlock((int) num == 1 ? R2ReceiverValues.ErrorStartAddress : R2ReceiverValues.ErrorStartAddressV2, (int) num == 1 ? R2ReceiverValues.ErrorSize : R2ReceiverValues.ErrorSizeV2, backgroundWorker, R2ReceiverValues.BackgroundWorkerIdForErrorLog);
      }
      return numArray;
    }

    public byte[] ReadErrorLogTop16()
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      uint num = 1U;
      if (this.m_port.CurrentBaudRate == ReceiverPort.BaudRate.Firmware)
      {
        if (this.CachedFirmwareRevision >= 83887360U)
          num = 2U;
      }
      else if (this.CachedBiosRevision >= 34145280U)
        num = 2U;
      return this.ReadBlock((int) num == 1 ? R2ReceiverValues.ErrorStartAddress : R2ReceiverValues.ErrorStartAddressV2, 1024U);
    }

    public bool EraseErrorLog()
    {
      uint num = 1U;
      if (this.m_port.CurrentBaudRate == ReceiverPort.BaudRate.Firmware)
      {
        if (this.CachedFirmwareRevision >= 83887360U)
          num = 2U;
      }
      else if (this.CachedBiosRevision >= 34145280U)
        num = 2U;
      return this.EraseBlock((int) num == 1 ? R2ReceiverValues.ErrorStartAddress : R2ReceiverValues.ErrorStartAddressV2, (int) num == 1 ? R2ReceiverValues.ErrorSize : R2ReceiverValues.ErrorSizeV2);
    }

    public byte[] ReadEventLog()
    {
      return this.ReadEventLog((BackgroundWorker) null);
    }

    public byte[] ReadEventLog(BackgroundWorker backgroundWorker)
    {
      byte[] numArray = (byte[]) null;
      if (backgroundWorker == null || !backgroundWorker.CancellationPending)
      {
        if (this.m_port == null || !this.m_port.IsOpen)
          throw new ApplicationException("Invalid port or port closed!");
        uint num = 1U;
        if (this.m_port.CurrentBaudRate == ReceiverPort.BaudRate.Firmware)
        {
          if (this.CachedFirmwareRevision >= 83887360U)
            num = 2U;
        }
        else if (this.CachedBiosRevision >= 34145280U)
          num = 2U;
        numArray = this.ReadBlock((int) num == 1 ? R2ReceiverValues.EventStartAddress : R2ReceiverValues.EventStartAddressV2, (int) num == 1 ? R2ReceiverValues.EventSize : R2ReceiverValues.EventSizeV2, backgroundWorker, R2ReceiverValues.BackgroundWorkerIdForEventLog);
      }
      return numArray;
    }

    public bool EraseEventLog()
    {
      uint num = 1U;
      if (this.m_port.CurrentBaudRate == ReceiverPort.BaudRate.Firmware)
      {
        if (this.CachedFirmwareRevision >= 83887360U)
          num = 2U;
      }
      else if (this.CachedBiosRevision >= 34145280U)
        num = 2U;
      return this.EraseBlock((int) num == 1 ? R2ReceiverValues.EventStartAddress : R2ReceiverValues.EventStartAddressV2, (int) num == 1 ? R2ReceiverValues.EventSize : R2ReceiverValues.EventSizeV2);
    }

    public byte[] ReadDatabase()
    {
      return this.ReadDatabase((BackgroundWorker) null);
    }

    public byte[] ReadDatabase(BackgroundWorker backgroundWorker)
    {
      byte[] numArray = (byte[]) null;
      if (backgroundWorker == null || !backgroundWorker.CancellationPending)
      {
        if (this.m_port == null || !this.m_port.IsOpen)
          throw new ApplicationException("Invalid port or port closed!");
        uint num = 1U;
        if (this.m_port.CurrentBaudRate == ReceiverPort.BaudRate.Firmware)
        {
          if (this.CachedFirmwareRevision >= 83887360U)
            num = 2U;
        }
        else if (this.CachedBiosRevision >= 34145280U)
          num = 2U;
        numArray = this.ReadBlock((int) num == 1 ? R2ReceiverValues.DatabaseStartAddress : R2ReceiverValues.DatabaseStartAddressV2, (int) num == 1 ? R2ReceiverValues.DatabaseSize : R2ReceiverValues.DatabaseSizeV2, backgroundWorker, R2ReceiverValues.BackgroundWorkerIdForDatabase);
      }
      return numArray;
    }

    public bool EraseDatabase()
    {
      uint num = 1U;
      if (this.m_port.CurrentBaudRate == ReceiverPort.BaudRate.Firmware)
      {
        if (this.CachedFirmwareRevision >= 83887360U)
          num = 2U;
      }
      else if (this.CachedBiosRevision >= 34145280U)
        num = 2U;
      return this.EraseBlock((int) num == 1 ? R2ReceiverValues.DatabaseStartAddress : R2ReceiverValues.DatabaseStartAddressV2, (int) num == 1 ? R2ReceiverValues.DatabaseSize : R2ReceiverValues.DatabaseSizeV2);
    }

    public void WriteBlock(uint address, byte[] data)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      uint size = (uint) data.Length;
      if ((int) size == 0 || !this.IsBlockInFlash(address, size))
        throw new ApplicationException("Requested locataion/size is not in a valid flash memory range!");
      if (this.m_port.CurrentBaudRate == ReceiverPort.BaudRate.Firmware)
      {
        uint version = 1U;
        if (this.CachedFirmwareRevision >= 83887360U)
          version = 2U;
        if (this.IsBlockInBios(address, size, version) || this.IsBlockInConfig(address, size, version) || (this.IsBlockInFirmware(address, size, version) || this.IsBlockInDatabase(address, size, version)) || (this.IsBlockInUnused(address, size, version) || this.IsBlockInLicense(address, size, version)))
          throw new ApplicationException("Requested locataion/size is not in a valid flash memory range for FIRMWARE mode!");
      }
      this.m_port.WriteBytes(new PacketWriteBlockCommand(address, (uint) data.Length).Bytes);
      PacketAckResponse packetAckResponse = new PacketAckResponse(this.m_port.ReadBytes(PacketAckResponse.PacketSize));
      this.m_port.WriteXModem(data);
    }

    public void WriteGenericCommandPacket(R2Commands command)
    {
      if (command == R2Commands.PC_Test && this.CachedFirmwareRevision < 83886848U)
        throw new ApplicationException("Function not supported in FIRMWARE revisions earlier than 5.0.3.0!");
      this.WriteGenericCommandPacket(command, (byte[]) null);
    }

    public void WriteGenericCommandPacket(R2Commands command, byte[] payload)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      int num1 = 0;
      if (payload != null && payload.Length > (int) ushort.MaxValue)
        throw new ApplicationException("Invalid payload size. Data is limited to 64K.");
      if (payload != null)
        num1 = payload.Length;
      byte[] numArray = new byte[6 + num1];
      int index1 = 0;
      numArray[index1] = (byte) 1;
      int index2 = index1 + 1;
      numArray[index2] = (byte) command;
      int destinationIndex1 = index2 + 1;
      byte[] bytes1 = BitConverter.GetBytes(Convert.ToUInt16(num1));
      Array.Copy((Array) bytes1, 0, (Array) numArray, destinationIndex1, bytes1.Length);
      int destinationIndex2 = destinationIndex1 + bytes1.Length;
      if (num1 > 0)
      {
        Array.Copy((Array) payload, 0, (Array) numArray, destinationIndex2, payload.Length);
        destinationIndex2 += num1;
      }
      byte[] bytes2 = BitConverter.GetBytes(Crc.CalculateCrc16(numArray, 0, 4 + num1));
      Array.Copy((Array) bytes2, 0, (Array) numArray, destinationIndex2, bytes2.Length);
      int num2 = destinationIndex2 + bytes2.Length;
      this.m_port.WriteBytes(numArray);
    }

    public byte[] ReadGenericCommandPacket(TimeSpan maxWait)
    {
      byte commandId;
      return this.ReadGenericCommandPacket(maxWait, out commandId);
    }

    public byte[] ReadGenericCommandPacket(TimeSpan maxWait, out byte commandId)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      byte[] numArray1 = (byte[]) null;
      byte[] buf = new byte[65541];
      int num1 = 0;
      commandId = (byte) 0;
      DateTime now = DateTime.Now;
      ApplicationException applicationException = (ApplicationException) null;
      while (DateTime.Now < now + maxWait)
      {
        try
        {
          byte[] numArray2 = this.m_port.ReadBytes(4);
          try
          {
            if ((int) numArray2[0] == 1)
            {
              Array.Copy((Array) numArray2, 0, (Array) buf, 0, 4);
              int num2 = 4;
              commandId = numArray2[1];
              ushort num3 = BitConverter.ToUInt16(numArray2, 2);
              if ((int) num3 > 0)
              {
                numArray1 = new byte[(int) num3];
                byte[] numArray3 = this.m_port.ReadBytes((int) num3);
                Array.Copy((Array) numArray3, 0, (Array) buf, num2, (int) num3);
                Array.Copy((Array) numArray3, 0, (Array) numArray1, 0, (int) num3);
                num2 += (int) num3;
              }
              else
                numArray1 = new byte[0];
              byte[] numArray4 = this.m_port.ReadBytes(2);
              ushort num4 = BitConverter.ToUInt16(numArray4, 0);
              ushort num5 = Crc.CalculateCrc16(buf, 0, num2);
              Array.Copy((Array) numArray4, 0, (Array) buf, num2, 2);
              num1 = num2 + 2;
              applicationException = (int) num4 == (int) num5 ? (ApplicationException) null : new ApplicationException("Failed CRC check in packet.");
              break;
            }
            else
            {
              applicationException = new ApplicationException("Unknown data read.  Failed to read start of packet.");
              break;
            }
          }
          catch (ApplicationException ex)
          {
            applicationException = new ApplicationException(ProgramContext.TryResourceLookup("Exception_FailedReadingPacket", "Failed to read contents of generic packets.", new object[0]), (Exception) ex);
            break;
          }
        }
        catch (TimedOutException ex)
        {
          applicationException = (ApplicationException) ex;
        }
      }
      if (applicationException != null)
        throw applicationException;
      else
        return numArray1;
    }

    public bool EraseLicenseBlock()
    {
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Bios)
        throw new ApplicationException("Function supported in BIOS mode only!");
      uint num = 1U;
      if (this.CachedBiosRevision >= 34145280U)
        num = 2U;
      return this.EraseBlock((int) num == 1 ? R2ReceiverValues.LicenseStartAddress : R2ReceiverValues.LicenseStartAddressV2, (int) num == 1 ? R2ReceiverValues.LicenseSize : R2ReceiverValues.LicenseSizeV2);
    }

    public bool EraseBlock(uint address, uint size)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if ((int) size == 0 || !this.IsBlockInFlash(address, size))
        throw new ApplicationException("Requested locataion/size is not in a valid flash memory range!");
      if (this.m_port.CurrentBaudRate == ReceiverPort.BaudRate.Firmware)
      {
        uint version = 1U;
        if (this.CachedFirmwareRevision >= 83887360U)
          version = 2U;
        if (this.IsBlockInBios(address, size, version) || this.IsBlockInConfig(address, size, version) || (this.IsBlockInFirmware(address, size, version) || this.IsBlockInDatabase(address, size, version)) || (this.IsBlockInUnused(address, size, version) || this.IsBlockInLicense(address, size, version)))
          throw new ApplicationException("Requested locataion/size is not in a valid flash memory range for FIRMWARE mode!");
      }
      this.m_port.WriteBytes(new PacketEraseBlockCommand(address, size).Bytes);
      TimeSpan maxWait = TimeSpan.FromSeconds(5.1 * (double) (size / 64U * 1024U));
      byte commandId = (byte) 0;
      this.ReadGenericCommandPacket(maxWait, out commandId);
      if ((int) commandId == 6)
        return true;
      if ((int) commandId == 21)
        return false;
      else
        throw new ApplicationException("Unexpected response id in EraseBlock packet. Id = " + commandId.ToString());
    }

    public byte[] ReadFirmware()
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      byte[] numArray = (byte[]) null;
      this.m_port.WriteBytes(PacketReadFirmwareCommand.Instance.Bytes);
      PacketAckResponse packetAckResponse = new PacketAckResponse(this.m_port.ReadBytes(PacketAckResponse.PacketSize));
      byte[] transferBytes = new byte[1048576];
      int transferBytesRead = 0;
      this.m_port.ReadXModem(transferBytes, out transferBytesRead);
      if (transferBytesRead > 0)
      {
        numArray = new byte[transferBytesRead];
        Array.Copy((Array) transferBytes, 0, (Array) numArray, 0, transferBytesRead);
      }
      return numArray;
    }

    public void WriteFirmware(byte[] data)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Bios)
        throw new ApplicationException("Function supported in BIOS mode only!");
      this.m_port.WriteBytes(PacketWriteFirmwareCommand.Instance.Bytes);
      PacketAckResponse packetAckResponse = new PacketAckResponse(this.m_port.ReadBytes(PacketAckResponse.PacketSize));
      this.m_port.WriteXModem(data);
    }

    public R2HWConfig ReadHWConfig()
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      this.m_port.WriteBytes(PacketReadHWConfigCommand.Instance.Bytes);
      return new PacketReadHWConfigResponse(this.m_port.ReadBytes(PacketReadHWConfigResponse.PacketSize)).Config;
    }

    public byte[] ReadHardwareConfigurationBinary()
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      this.m_port.WriteBytes(PacketReadHWConfigCommand.Instance.Bytes);
      byte commandId;
      byte[] bytes = this.ReadGenericCommandPacket(TimeSpan.FromSeconds(2.0), out commandId);
      if ((int) commandId != 6)
        throw new ApplicationException("Receiver did not return a HardwareConfiguration.  Receiver may have an invalid or incompatible configuration.");
      if (bytes.Length == 128)
      {
        if (!((R2HWConfig) DataTools.ConvertBytesToObject(bytes, 0, typeof (R2HWConfig))).IsValidCrc())
          throw new ApplicationException("HardwareConfiguration (V1) does not have a valid CRC!");
      }
      else
      {
        if (bytes.Length != 1024)
          throw new ApplicationException("Receiver did not return a known size for a HardwareConfiguration.  Receiver may have an invalid or incompatible configuration.");
        R2HWConfigHeader r2HwConfigHeader = (R2HWConfigHeader) DataTools.ConvertBytesToObject(bytes, 0, typeof (R2HWConfigHeader));
        if ((int) r2HwConfigHeader.m_configVersion == 2)
        {
          if (!((R2HWConfigV2) DataTools.ConvertBytesToObject(bytes, 0, typeof (R2HWConfigV2))).IsValidCrc())
            throw new ApplicationException("HardwareConfiguration (V2) does not have a valid CRC!");
        }
        else if ((int) r2HwConfigHeader.m_configVersion == 3)
        {
          if (!((R2HWConfigV3) DataTools.ConvertBytesToObject(bytes, 0, typeof (R2HWConfigV3))).IsValidCrc())
            throw new ApplicationException("HardwareConfiguration (V3) does not have a valid CRC!");
        }
        else if ((int) r2HwConfigHeader.m_configVersion == 4)
        {
          if (!((R2HWConfigV4) DataTools.ConvertBytesToObject(bytes, 0, typeof (R2HWConfigV4))).IsValidCrc())
            throw new ApplicationException("HardwareConfiguration (V4) does not have a valid CRC!");
        }
        else if ((int) r2HwConfigHeader.m_configVersion == 5)
        {
          if (!((R2HWConfigV5) DataTools.ConvertBytesToObject(bytes, 0, typeof (R2HWConfigV5))).IsValidCrc())
            throw new ApplicationException("HardwareConfiguration (V5) does not have a valid CRC!");
        }
        else if ((int) r2HwConfigHeader.m_configVersion == 6)
        {
          if (!((R2HWConfigV6) DataTools.ConvertBytesToObject(bytes, 0, typeof (R2HWConfigV6))).IsValidCrc())
            throw new ApplicationException("HardwareConfiguration (V6) does not have a valid CRC!");
        }
        else
        {
          if ((int) r2HwConfigHeader.m_configVersion != 7)
            throw new ApplicationException("Receiver did not return a known version for a HardwareConfiguration.  Receiver may have an invalid or incompatible configuration.");
          if (!((R2HWConfigV7) DataTools.ConvertBytesToObject(bytes, 0, typeof (R2HWConfigV7))).IsValidCrc())
            throw new ApplicationException("HardwareConfiguration (V7) does not have a valid CRC!");
        }
      }
      return bytes;
    }

    public HardwareConfiguration ReadHardwareConfiguration()
    {
      return R2ReceiverTools.CreateHardwareConfigurationFromBinary(this.ReadHardwareConfigurationBinary());
    }

    public void WriteHWConfig(HardwareConfiguration config)
    {
      if ((int) config.ConfigurationVersion == 1)
        this.WriteHWConfig(config.GetR2HWConfigV1());
      else if ((int) config.ConfigurationVersion == 2)
        this.WriteHWConfig(config.GetR2HWConfigV2());
      else if ((int) config.ConfigurationVersion == 3)
        this.WriteHWConfig(config.GetR2HWConfigV3());
      else if ((int) config.ConfigurationVersion == 4)
        this.WriteHWConfig(config.GetR2HWConfigV4());
      else if ((int) config.ConfigurationVersion == 5)
        this.WriteHWConfig(config.GetR2HWConfigV5());
      else if ((int) config.ConfigurationVersion == 6)
      {
        this.WriteHWConfig(config.GetR2HWConfigV6());
      }
      else
      {
        if ((int) config.ConfigurationVersion != 7)
          throw new ApplicationException("Unknown or invalid hardware configuration versino.");
        this.WriteHWConfig(config.GetR2HWConfigV7());
      }
    }

    public void WriteHWConfig(R2HWConfig config)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Bios)
        throw new ApplicationException("Function supported in BIOS mode only!");
      config.UpdateCrc();
      this.m_port.WriteBytes(new PacketWriteHWConfigCommand(config).Bytes);
      PacketAckResponse packetAckResponse = new PacketAckResponse(this.m_port.ReadBytes(PacketAckResponse.PacketSize));
    }

    public void WriteHWConfig(R2HWConfigV2 config)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Bios)
        throw new ApplicationException("Function supported in BIOS mode only!");
      config.UpdateCrc();
      this.WriteGenericCommandPacket(R2Commands.WriteHWConfig, DataTools.ConvertObjectToBytes((object) config));
      PacketAckResponse packetAckResponse = new PacketAckResponse(this.m_port.ReadBytes(PacketAckResponse.PacketSize));
    }

    public void WriteHWConfig(R2HWConfigV3 config)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Bios)
        throw new ApplicationException("Function supported in BIOS mode only!");
      config.UpdateCrc();
      this.WriteGenericCommandPacket(R2Commands.WriteHWConfig, DataTools.ConvertObjectToBytes((object) config));
      PacketAckResponse packetAckResponse = new PacketAckResponse(this.m_port.ReadBytes(PacketAckResponse.PacketSize));
    }

    public void WriteHWConfig(R2HWConfigV4 config)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Bios)
        throw new ApplicationException("Function supported in BIOS mode only!");
      config.UpdateCrc();
      this.WriteGenericCommandPacket(R2Commands.WriteHWConfig, DataTools.ConvertObjectToBytes((object) config));
      PacketAckResponse packetAckResponse = new PacketAckResponse(this.m_port.ReadBytes(PacketAckResponse.PacketSize));
    }

    public void WriteHWConfig(R2HWConfigV5 config)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Bios)
        throw new ApplicationException("Function supported in BIOS mode only!");
      config.UpdateCrc();
      this.WriteGenericCommandPacket(R2Commands.WriteHWConfig, DataTools.ConvertObjectToBytes((object) config));
      PacketAckResponse packetAckResponse = new PacketAckResponse(this.m_port.ReadBytes(PacketAckResponse.PacketSize));
    }

    public void WriteHWConfig(R2HWConfigV6 config)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Bios)
        throw new ApplicationException("Function supported in BIOS mode only!");
      config.UpdateCrc();
      this.WriteGenericCommandPacket(R2Commands.WriteHWConfig, DataTools.ConvertObjectToBytes((object) config));
      PacketAckResponse packetAckResponse = new PacketAckResponse(this.m_port.ReadBytes(PacketAckResponse.PacketSize));
    }

    public void WriteHWConfig(R2HWConfigV7 config)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Bios)
        throw new ApplicationException("Function supported in BIOS mode only!");
      config.UpdateCrc();
      this.WriteGenericCommandPacket(R2Commands.WriteHWConfig, DataTools.ConvertObjectToBytes((object) config));
      PacketAckResponse packetAckResponse = new PacketAckResponse(this.m_port.ReadBytes(PacketAckResponse.PacketSize));
    }

    public byte[] ReadBios()
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      byte[] numArray = (byte[]) null;
      this.m_port.WriteBytes(PacketReadBiosCommand.Instance.Bytes);
      PacketAckResponse packetAckResponse = new PacketAckResponse(this.m_port.ReadBytes(PacketAckResponse.PacketSize));
      byte[] transferBytes = new byte[65536];
      int transferBytesRead = 0;
      this.m_port.ReadXModem(transferBytes, out transferBytesRead);
      if (transferBytesRead > 0)
      {
        numArray = new byte[transferBytesRead];
        Array.Copy((Array) transferBytes, 0, (Array) numArray, 0, transferBytesRead);
      }
      return numArray;
    }

    public void WriteBios(byte[] data)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Bios)
        throw new ApplicationException("Function supported in BIOS mode only!");
      this.m_port.WriteBytes(PacketWriteBiosCommand.Instance.Bytes);
      PacketAckResponse packetAckResponse = new PacketAckResponse(this.m_port.ReadBytes(PacketAckResponse.PacketSize));
      this.m_port.WriteXModem(data);
    }

    public uint ReadBiosHeader()
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      this.m_port.WriteBytes(PacketReadBiosHeaderCommand.Instance.Bytes);
      return new PacketReadUInt32Response(this.m_port.ReadBytes(PacketReadUInt32Response.PacketSize)).Value;
    }

    public R2FWHeader ReadFirmwareHeader()
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      this.m_port.WriteBytes(PacketReadFirmwareHeaderCommand.Instance.Bytes);
      return new PacketReadFirmwareHeaderResponse(this.m_port.ReadBytes(PacketReadFirmwareHeaderResponse.PacketSize)).Header;
    }

    public XmlElement ReadExtendedFirmwareHeader()
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      XmlElement xmlElement = (XmlElement) null;
      this.m_port.WriteBytes(PacketReadFirmwareHeaderCommand.Instance.Bytes);
      R2FWHeader header = new PacketReadFirmwareHeaderResponse(this.m_port.ReadBytes(PacketReadFirmwareHeaderResponse.PacketSize)).Header;
      if (header.m_revision < 83886602U)
      {
        XObject xobject = new XObject("FirmwareHeader");
        xobject.SetAttribute("Code", header.m_code.ToString("X"));
        xobject.SetAttribute("Revision", R2ReceiverTools.RevisionToString(header.m_revision));
        xobject.SetAttribute("RevisionNumber", header.m_revision);
        xobject.SetAttribute("Signature", header.m_signature);
        xobject.SetAttribute("FirmwareHeaderRevision", R2ReceiverTools.RevisionToString(0U));
        xobject.SetAttribute("FirmwareHeaderRevisionNumber", 0);
        xmlElement = xobject.Element;
      }
      else if (header.m_revision >= 83886602U && header.m_revision < 83887368U)
      {
        R2FWHeaderVer2 r2FwHeaderVer2 = (R2FWHeaderVer2) DataTools.ConvertBytesToObject(this.ReadBlock(R2ReceiverValues.FirmwareStartAddress, 1024U), 0, typeof (R2FWHeaderVer2));
        XObject xobject = new XObject("FirmwareHeader");
        xobject.SetAttribute("Code", r2FwHeaderVer2.m_code.ToString("X"));
        xobject.SetAttribute("Revision", R2ReceiverTools.RevisionToString(r2FwHeaderVer2.m_revision));
        xobject.SetAttribute("RevisionNumber", r2FwHeaderVer2.m_revision);
        xobject.SetAttribute("Signature", r2FwHeaderVer2.m_signature);
        xobject.SetAttribute("FirmwareHeaderRevision", R2ReceiverTools.RevisionToString(r2FwHeaderVer2.m_firmwareHeaderRevision));
        xobject.SetAttribute("FirmwareHeaderRevisionNumber", r2FwHeaderVer2.m_firmwareHeaderRevision);
        ulong ulongValue1 = ~BitConverter.ToUInt64(r2FwHeaderVer2.m_hwConfigMask, 0);
        ulong ulongValue2 = ~BitConverter.ToUInt64(r2FwHeaderVer2.m_hwConfigMask, 8);
        xobject.SetAttribute("AddressFirmwareCrc", r2FwHeaderVer2.m_addressFirmwareCrc.ToString("X"));
        xobject.SetAttribute("BranchInstruction", r2FwHeaderVer2.m_branchInstruction.ToString("X"));
        xobject.SetAttribute("HardwareConfigMask", StringUtils.ToHexString(r2FwHeaderVer2.m_hwConfigMask));
        xobject.SetAttribute("HardwareConfigMaskLow64", ulongValue1);
        xobject.SetAttribute("HardwareConfigMaskHigh64", ulongValue2);
        xobject.SetAttribute("RequiredMemorySize", r2FwHeaderVer2.m_requiredMemorySize.ToString("X"));
        xobject.SetAttribute("DatabaseRevision", R2ReceiverTools.RevisionToString(r2FwHeaderVer2.m_databaseRevision));
        xobject.SetAttribute("DatabaseRevisionNumber", r2FwHeaderVer2.m_databaseRevision);
        xobject.SetAttribute("ProductString", r2FwHeaderVer2.m_productString);
        xobject.SetAttribute("BuildDateAndTime", r2FwHeaderVer2.m_buildDateAndTime);
        xobject.SetAttribute("CpldG3Revision", R2ReceiverTools.RevisionToString(r2FwHeaderVer2.m_g3CpldRevision));
        xobject.SetAttribute("CpldGtxRevision", R2ReceiverTools.RevisionToString(r2FwHeaderVer2.m_gtxCpldRevision));
        xobject.SetAttribute("CpldG3RevisionNumber", r2FwHeaderVer2.m_g3CpldRevision);
        xobject.SetAttribute("CpldGtxRevisionNumber", r2FwHeaderVer2.m_gtxCpldRevision);
        xmlElement = xobject.Element;
      }
      else if (header.m_revision >= 83887368U)
      {
        bool flag = true;
        if (this.m_port.CurrentBaudRate == ReceiverPort.BaudRate.Bios && this.CachedBiosRevision < 34145280U)
          flag = false;
        if (this.m_port.CurrentBaudRate == ReceiverPort.BaudRate.Firmware && this.CachedFirmwareRevision < 83887360U)
          flag = false;
        R2FWHeaderVer3 r2FwHeaderVer3 = (R2FWHeaderVer3) DataTools.ConvertBytesToObject(!flag ? this.ReadBlock(R2ReceiverValues.FirmwareStartAddressV2, 1024U) : this.PeekMemory(R2ReceiverValues.FirmwareStartAddressV2, 1024U), 0, typeof (R2FWHeaderVer3));
        XObject xobject = new XObject("FirmwareHeader");
        xobject.SetAttribute("Code", r2FwHeaderVer3.m_code.ToString("X"));
        xobject.SetAttribute("Revision", R2ReceiverTools.RevisionToString(r2FwHeaderVer3.m_revision));
        xobject.SetAttribute("RevisionNumber", r2FwHeaderVer3.m_revision);
        xobject.SetAttribute("Signature", r2FwHeaderVer3.m_signature);
        xobject.SetAttribute("FirmwareHeaderRevision", R2ReceiverTools.RevisionToString(r2FwHeaderVer3.m_firmwareHeaderRevision));
        xobject.SetAttribute("FirmwareHeaderRevisionNumber", r2FwHeaderVer3.m_firmwareHeaderRevision);
        ulong ulongValue1 = ~BitConverter.ToUInt64(r2FwHeaderVer3.m_hwConfigMask, 0);
        ulong ulongValue2 = ~BitConverter.ToUInt64(r2FwHeaderVer3.m_hwConfigMask, 8);
        xobject.SetAttribute("AddressFirmwareCrc", r2FwHeaderVer3.m_addressFirmwareCrc.ToString("X"));
        xobject.SetAttribute("BranchInstruction", r2FwHeaderVer3.m_branchInstruction.ToString("X"));
        xobject.SetAttribute("HardwareConfigMask", StringUtils.ToHexString(r2FwHeaderVer3.m_hwConfigMask));
        xobject.SetAttribute("HardwareConfigMaskLow64", ulongValue1);
        xobject.SetAttribute("HardwareConfigMaskHigh64", ulongValue2);
        xobject.SetAttribute("RequiredMemorySize", r2FwHeaderVer3.m_requiredMemorySize.ToString("X"));
        xobject.SetAttribute("DatabaseRevision", R2ReceiverTools.RevisionToString(r2FwHeaderVer3.m_databaseRevision));
        xobject.SetAttribute("DatabaseRevisionNumber", r2FwHeaderVer3.m_databaseRevision);
        xobject.SetAttribute("ProductString", r2FwHeaderVer3.m_productString);
        xobject.SetAttribute("BuildDateAndTime", r2FwHeaderVer3.m_buildDateAndTime);
        xobject.SetAttribute("CpldG3Revision", R2ReceiverTools.RevisionToString(r2FwHeaderVer3.m_g3CpldRevision));
        xobject.SetAttribute("CpldGtxRevision", R2ReceiverTools.RevisionToString(r2FwHeaderVer3.m_gtxCpldRevision));
        xobject.SetAttribute("CpldG3RevisionNumber", r2FwHeaderVer3.m_g3CpldRevision);
        xobject.SetAttribute("CpldGtxRevisionNumber", r2FwHeaderVer3.m_gtxCpldRevision);
        xobject.SetAttribute("BiosCompatibilityNumber", r2FwHeaderVer3.m_biosCompatibilityNumber);
        xobject.SetAttribute("RequiredConfigImageVersion", r2FwHeaderVer3.m_requiredConfigImageVersion);
        xmlElement = xobject.Element;
      }
      return xmlElement;
    }

    public XmlElement ReadExtendedBiosHeader()
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      XmlElement element;
      if (this.CachedBiosRevision < 34145285U)
      {
        uint num = this.ReadBiosHeader();
        XObject xobject = new XObject("BiosHeader");
        xobject.SetAttribute("BiosRevision", R2ReceiverTools.RevisionToString(num));
        xobject.SetAttribute("BiosRevisionNumber", num);
        xobject.SetAttribute("BiosHeaderRevision", 0);
        xobject.SetAttribute("BuildDateAndTime", string.Empty);
        xobject.SetAttribute("ProductString", string.Empty);
        xobject.SetAttribute("FirmwareCompatibilityNumber", 0);
        xobject.SetAttribute("ConfigCompatibilityNumber", 0);
        element = xobject.Element;
      }
      else
      {
        R2BiosHeader r2BiosHeader = (R2BiosHeader) DataTools.ConvertBytesToObject(this.PeekMemory(R2ReceiverValues.BiosStartAddressV2, 128U), 32, typeof (R2BiosHeader));
        XObject xobject = new XObject("BiosHeader");
        xobject.SetAttribute("BiosRevision", R2ReceiverTools.RevisionToString(r2BiosHeader.m_revision));
        xobject.SetAttribute("BiosRevisionNumber", r2BiosHeader.m_revision);
        xobject.SetAttribute("BiosHeaderRevision", r2BiosHeader.m_biosHeaderRevision);
        xobject.SetAttribute("BuildDateAndTime", r2BiosHeader.m_buildDateAndTime);
        xobject.SetAttribute("ProductString", r2BiosHeader.m_productString);
        xobject.SetAttribute("FirmwareCompatibilityNumber", r2BiosHeader.m_firmwareCompatibilityNumber);
        xobject.SetAttribute("ConfigCompatibilityNumber", r2BiosHeader.m_configCompatibilityNumber);
        element = xobject.Element;
      }
      return element;
    }

    public uint ReadRTC()
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      this.m_port.WriteBytes(PacketReadRTCCommand.Instance.Bytes);
      return new PacketReadUInt32Response(this.m_port.ReadBytes(PacketReadUInt32Response.PacketSize)).Value;
    }

    public void WriteRTC(uint rtc)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Bios)
        throw new ApplicationException("Function supported in BIOS mode only!");
      this.m_port.WriteBytes(new PacketWriteRTCCommand(rtc).Bytes);
      PacketAckResponse packetAckResponse = new PacketAckResponse(this.m_port.ReadBytes(PacketAckResponse.PacketSize));
    }

    public void ResetReceiver()
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      this.m_port.WriteBytes(PacketResetReceiverCommand.Instance.Bytes);
      PacketAckResponse packetAckResponse = new PacketAckResponse(this.m_port.ReadBytes(PacketAckResponse.PacketSize));
    }

    public SettingsRecord ReadSettings()
    {
      SettingsRecord settingsRecord = (SettingsRecord) null;
      if (this.CachedFirmwareRevision <= 83886854U)
      {
        R2SettingsRecord record = this.ReadSettings1();
        settingsRecord = new SettingsRecord();
        settingsRecord.SetR2Record(record);
      }
      else if (this.CachedFirmwareRevision >= 83886855U)
      {
        if (this.CachedFirmwareRevision < 83887365U)
        {
          R2Settings2Record record = this.ReadSettings2();
          settingsRecord = new SettingsRecord();
          settingsRecord.SetR2Record(record);
        }
        else if (this.CachedFirmwareRevision < 117440512U)
        {
          R2Settings2Record record = this.DoReadSettings2();
          settingsRecord = new SettingsRecord();
          settingsRecord.SetR2Record(record);
          XmlElement xmlElement = this.ReadExtendedFirmwareHeader();
          if (xmlElement.HasAttribute("ProductString") && !xmlElement.GetAttribute("ProductString").StartsWith("SW8218", StringComparison.InvariantCultureIgnoreCase))
          {
            bool flag1 = (this.CachedHardwareConfiguration.FirmwareFlags & R2FirmwareFlags.GlucoseMmolUnits) == R2FirmwareFlags.GlucoseMmolUnits;
            bool flag2 = (this.CachedHardwareConfiguration.FirmwareFlags & R2FirmwareFlags.Clock24HourMode) == R2FirmwareFlags.Clock24HourMode;
            settingsRecord.ClockMode = flag2 ? "24hr" : "AM/PM";
            settingsRecord.GlucoseUnits = flag1 ? "mmol/L" : "mg/dL";
          }
        }
        else if (this.CachedFirmwareRevision >= 117440512U && this.CachedFirmwareRevision <= 117440516U)
        {
          R2Settings3Record record = this.DoReadSettings3();
          settingsRecord = new SettingsRecord();
          settingsRecord.SetR2Record(record);
          bool flag1 = (this.CachedHardwareConfiguration.FirmwareFlags & R2FirmwareFlags.GlucoseMmolUnits) == R2FirmwareFlags.GlucoseMmolUnits;
          bool flag2 = (this.CachedHardwareConfiguration.FirmwareFlags & R2FirmwareFlags.Clock24HourMode) == R2FirmwareFlags.Clock24HourMode;
          settingsRecord.ClockMode = flag2 ? "24hr" : "AM/PM";
          settingsRecord.GlucoseUnits = flag1 ? "mmol/L" : "mg/dL";
        }
        else if (this.CachedFirmwareRevision >= 117440517U && this.CachedFirmwareRevision <= 117441290U)
        {
          R2Settings4Record record = this.DoReadSettings4();
          settingsRecord = new SettingsRecord();
          settingsRecord.SetR2Record(record);
          bool flag1 = (this.CachedHardwareConfiguration.FirmwareFlags & R2FirmwareFlags.GlucoseMmolUnits) == R2FirmwareFlags.GlucoseMmolUnits;
          bool flag2 = (this.CachedHardwareConfiguration.FirmwareFlags & R2FirmwareFlags.Clock24HourMode) == R2FirmwareFlags.Clock24HourMode;
          settingsRecord.ClockMode = flag2 ? "24hr" : "AM/PM";
          settingsRecord.GlucoseUnits = flag1 ? "mmol/L" : "mg/dL";
        }
        else if (this.CachedFirmwareRevision > 117441290U && this.CachedFirmwareRevision < 117441540U)
        {
          R2Settings5Record record = this.DoReadSettings5();
          settingsRecord = new SettingsRecord();
          settingsRecord.SetR2Record(record);
          bool flag1 = (this.CachedHardwareConfiguration.FirmwareFlags & R2FirmwareFlags.GlucoseMmolUnits) == R2FirmwareFlags.GlucoseMmolUnits;
          bool flag2 = (this.CachedHardwareConfiguration.FirmwareFlags & R2FirmwareFlags.Clock24HourMode) == R2FirmwareFlags.Clock24HourMode;
          settingsRecord.ClockMode = flag2 ? "24hr" : "AM/PM";
          settingsRecord.GlucoseUnits = flag1 ? "mmol/L" : "mg/dL";
        }
        else if (this.CachedFirmwareRevision >= 117441540U)
        {
          R2Settings6Record record = this.DoReadSettings6();
          settingsRecord = new SettingsRecord();
          settingsRecord.SetR2Record(record);
          bool flag1 = (this.CachedHardwareConfiguration.FirmwareFlags & R2FirmwareFlags.GlucoseMmolUnits) == R2FirmwareFlags.GlucoseMmolUnits;
          bool flag2 = (this.CachedHardwareConfiguration.FirmwareFlags & R2FirmwareFlags.Clock24HourMode) == R2FirmwareFlags.Clock24HourMode;
          settingsRecord.ClockMode = flag2 ? "24hr" : "AM/PM";
          settingsRecord.GlucoseUnits = flag1 ? "mmol/L" : "mg/dL";
        }
      }
      return settingsRecord;
    }

    public void WriteSettings(SettingsRecord settingsRecord)
    {
      if (this.CachedFirmwareRevision <= 83886854U)
      {
        this.WriteSettings1(settingsRecord.GetInternalRecordAsSettings1Record());
      }
      else
      {
        if (this.CachedFirmwareRevision < 83886855U)
          return;
        if (this.CachedFirmwareRevision >= 83887360U)
          throw new ApplicationException("WriteSettings() no longer supported in version 5.0.5.0 or greater.  Use the new individual API methods to update settings.");
        this.WriteSettings2(settingsRecord.GetInternalRecordAsSettings2Record());
      }
    }

    public R2SettingsRecord ReadSettings1()
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      if (this.CachedFirmwareRevision > 83886854U)
        throw new ApplicationException("Function supported in FIRMWARE Revision 5.0.3.6 or earlier only!");
      this.m_port.WriteBytes(PacketReadSettingsCommand.Instance.Bytes);
      return new PacketReadSettingsResponse(this.m_port.ReadBytes(PacketReadSettingsResponse.PacketSize)).Settings;
    }

    private R2Settings2Record DoReadSettings2()
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      if (this.CachedFirmwareRevision < 83886855U)
        throw new ApplicationException("Function not supported in FIRMWARE Revisions earlier than 5.0.3.7!");
      this.m_port.WriteBytes(PacketReadSettingsCommand.Instance.Bytes);
      return new PacketReadSettings2Response(this.m_port.ReadBytes(PacketReadSettings2Response.PacketSize)).Settings;
    }

    private R2Settings3Record DoReadSettings3()
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      if (this.CachedFirmwareRevision < 117440512U)
        throw new ApplicationException("Function not supported in FIRMWARE Revisions earlier than 7.0.0.0!");
      this.m_port.WriteBytes(PacketReadSettingsCommand.Instance.Bytes);
      return new PacketReadSettings3Response(this.m_port.ReadBytes(PacketReadSettings3Response.PacketSize)).Settings;
    }

    private R2Settings4Record DoReadSettings4()
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      if (this.CachedFirmwareRevision < 117440517U)
        throw new ApplicationException("Function not supported in FIRMWARE Revisions earlier than 7.0.0.5!");
      this.m_port.WriteBytes(PacketReadSettingsCommand.Instance.Bytes);
      return new PacketReadSettings4Response(this.m_port.ReadBytes(PacketReadSettings4Response.PacketSize)).Settings;
    }

    private R2Settings5Record DoReadSettings5()
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      if (this.CachedFirmwareRevision < 117441291U)
        throw new ApplicationException("Function not supported in FIRMWARE Revisions earlier than 7.0.3.11!");
      this.m_port.WriteBytes(PacketReadSettingsCommand.Instance.Bytes);
      return new PacketReadSettings5Response(this.m_port.ReadBytes(PacketReadSettings5Response.PacketSize)).Settings;
    }

    private R2Settings6Record DoReadSettings6()
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      if (this.CachedFirmwareRevision < 117441540U)
        throw new ApplicationException("Function not supported in FIRMWARE Revisions earlier than 7.0.4.4!");
      this.m_port.WriteBytes(PacketReadSettingsCommand.Instance.Bytes);
      return new PacketReadSettings6Response(this.m_port.ReadBytes(PacketReadSettings6Response.PacketSize)).Settings;
    }

    public R2Settings2Record ReadSettings2()
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      if (this.CachedFirmwareRevision < 83886855U || this.CachedFirmwareRevision >= 83887360U)
        throw new ApplicationException("Function supported in FIRMWARE Revisions >= 5.0.3.7 and < 5.0.5.0 only!");
      else
        return this.DoReadSettings2();
    }

    public void WriteSettings1(R2SettingsRecord settings)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      if (this.CachedFirmwareRevision > 83886854U)
        throw new ApplicationException("Function supported in FIRMWARE Revision 5.0.3.6 or earlier only!");
      this.m_port.WriteBytes(new PacketWriteSettingsCommand(settings).Bytes);
      PacketAckResponse packetAckResponse = new PacketAckResponse(this.m_port.ReadBytes(PacketAckResponse.PacketSize));
    }

    public void WriteSettings2(R2Settings2Record settings)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      if (this.CachedFirmwareRevision < 83886855U || this.CachedFirmwareRevision >= 83887360U)
        throw new ApplicationException("Function supported in FIRMWARE Revisions >= 5.0.3.7 and < 5.0.5.0 only!");
      this.m_port.WriteBytes(new PacketWriteSettings2Command(settings).Bytes);
      PacketAckResponse packetAckResponse = new PacketAckResponse(this.m_port.ReadBytes(PacketAckResponse.PacketSize));
    }

    public void WriteTrim(byte trim)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Bios)
        throw new ApplicationException("Function supported in BIOS mode only!");
      this.m_port.WriteBytes(new PacketWriteTrimCommand(trim).Bytes);
      PacketAckResponse packetAckResponse = new PacketAckResponse(this.m_port.ReadBytes(PacketAckResponse.PacketSize));
    }

    public ulong ReadInternalTime()
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      this.m_port.WriteBytes(PacketReadInternalTimeCommand.Instance.Bytes);
      return new PacketReadDateResponse(this.m_port.ReadBytes(PacketReadDateResponse.PacketSize)).Milliseconds;
    }

    public byte[] ReadDatabase(uint startBlock)
    {
      return this.ReadDatabase(startBlock, (BackgroundWorker) null);
    }

    public byte[] ReadDatabase(uint startBlock, BackgroundWorker backgroundWorker)
    {
      byte[] numArray = (byte[]) null;
      if (backgroundWorker == null || !backgroundWorker.CancellationPending)
      {
        if (this.m_port == null || !this.m_port.IsOpen)
          throw new ApplicationException("Invalid port or port closed!");
        if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
          throw new ApplicationException("Function supported in FIRMWARE mode only!");
        this.m_port.WriteBytes(new PacketReadDatabaseCommand(startBlock).Bytes);
        PacketReadDatabaseResponse databaseResponse = new PacketReadDatabaseResponse(this.m_port.ReadBytes(PacketReadDatabaseResponse.PacketSize));
        int num = (int) databaseResponse.LastFixedBlock;
        int length = (int) databaseResponse.NumberOfBlocks * 65536;
        byte[] transferBytes = new byte[length];
        int transferBytesRead = 0;
        this.m_port.ReadXModem(transferBytes, out transferBytesRead, backgroundWorker, R2ReceiverValues.BackgroundWorkerIdForDatabase);
        if (transferBytesRead == length)
          numArray = transferBytes;
        else if (backgroundWorker == null || !backgroundWorker.CancellationPending)
          throw new ApplicationException("Transfer ended unexpectedly and/or incomplete data found.  Operation canceled!");
      }
      return numArray;
    }

    public void ReadDatabaseInformationOnly(uint startBlock, out uint newLastFixedRecord, out uint numberOfBlocks)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      this.m_port.WriteBytes(new PacketReadDatabaseCommand(startBlock).Bytes);
      PacketReadDatabaseResponse databaseResponse = new PacketReadDatabaseResponse(this.m_port.ReadBytes(PacketReadDatabaseResponse.PacketSize));
      uint lastFixedBlock = databaseResponse.LastFixedBlock;
      uint numberOfBlocks1 = databaseResponse.NumberOfBlocks;
      newLastFixedRecord = lastFixedBlock;
      numberOfBlocks = numberOfBlocks1;
      this.m_port.SetMediumTimeouts();
      this.m_port.ResetAndFlushPort();
      this.m_port.SetNormalTimeouts();
    }

    public void EnableTransmitter(bool enable)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Bios)
        throw new ApplicationException("Function supported in BIOS mode only!");
      this.m_port.WriteBytes(new PacketEnableTransmitterCommand(enable).Bytes);
      PacketAckResponse packetAckResponse = new PacketAckResponse(this.m_port.ReadBytes(PacketAckResponse.PacketSize));
    }

    public bool IsEventLogEmpty()
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      this.m_port.WriteBytes(PacketIsEventLogEmptyCommand.Instance.Bytes);
      return (int) new PacketReadByteResponse(this.m_port.ReadBytes(PacketReadByteResponse.PacketSize)).Value == 1;
    }

    public bool IsErrorLogEmpty()
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      this.m_port.WriteBytes(PacketIsErrorLogEmptyCommand.Instance.Bytes);
      return (int) new PacketReadByteResponse(this.m_port.ReadBytes(PacketReadByteResponse.PacketSize)).Value == 1;
    }

    public bool IsFirmwareMode()
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      this.m_port.WriteBytes(PacketAmIFirmwareCommand.Instance.Bytes);
      return (int) new PacketReadByteResponse(this.m_port.ReadBytes(PacketReadByteResponse.PacketSize)).Value == 1;
    }

    public uint ReadDatabaseRevision()
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      this.m_port.WriteBytes(PacketReadDatabaseRevisionCommand.Instance.Bytes);
      return new PacketReadUInt32Response(this.m_port.ReadBytes(PacketReadUInt32Response.PacketSize)).Value;
    }

    public ushort ReadADC(byte channelNumber)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Bios)
        throw new ApplicationException("Function supported in BIOS mode only!");
      if (this.CachedBiosRevision < 34144516U || this.CachedBiosRevision > 34144520U && this.CachedBiosRevision < 34144768U)
        throw new ApplicationException("Function not supported in BIOS revisions earlier than 2.9.1.4 or (later than 2.9.1.8 and earlier than 2.9.2.0)!");
      this.m_port.WriteBytes(new PacketReadADCCommand(channelNumber).Bytes);
      return new PacketReadADCResponse(this.m_port.ReadBytes(PacketReadADCResponse.PacketSize)).Counts;
    }

    public R2BootFailureReasons ReadBootFailureReasons()
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Bios)
        throw new ApplicationException("Function supported in BIOS mode only!");
      if (this.CachedBiosRevision < 34144517U || this.CachedBiosRevision > 34144520U && this.CachedBiosRevision < 34144768U)
        throw new ApplicationException("Function not supported in BIOS revisions earlier than 2.9.1.5 or (later than 2.9.1.8 and earlier than 2.9.2.0)!");
      this.m_port.WriteBytes(PacketReadBootFailureReasonsCommand.Instance.Bytes);
      return (R2BootFailureReasons) new PacketReadUInt32Response(this.m_port.ReadBytes(PacketReadUInt32Response.PacketSize)).Value;
    }

    public void SleepUntilReset()
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Bios)
        throw new ApplicationException("Function supported in BIOS mode only!");
      if (this.CachedBiosRevision < 34144768U)
        throw new ApplicationException("Function not supported in BIOS revisions earlier than 2.9.2.0");
      this.m_port.WriteBytes(PacketSleepUntilResetCommand.Instance.Bytes);
      PacketAckResponse packetAckResponse = new PacketAckResponse(this.m_port.ReadBytes(PacketAckResponse.PacketSize));
    }

    [Obsolete("ReadLastButtonPressed() method has been removed from the receiver API and will be removed from the PC API in the next version.", true)]
    public R2ButtonFlags ReadLastButtonPressed()
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Bios)
        throw new ApplicationException("Function supported in BIOS mode only!");
      if (this.CachedBiosRevision < 34144768U)
        throw new ApplicationException("Function not supported in BIOS revisions earlier than 2.9.2.0");
      this.m_port.WriteBytes(PacketReadLastButtonPressedCommand.Instance.Bytes);
      return (R2ButtonFlags) new PacketReadByteResponse(this.m_port.ReadBytes(PacketReadByteResponse.PacketSize)).Value;
    }

    public byte[] PeekMemory(uint address, uint size)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate == ReceiverPort.BaudRate.Bios && this.CachedBiosRevision < 34145280U)
        throw new ApplicationException("Function supported in BIOS revisions 2.9.4.0 or later only!");
      if (this.m_port.CurrentBaudRate == ReceiverPort.BaudRate.Firmware && this.CachedFirmwareRevision < 83887360U)
        throw new ApplicationException("Function supported in FIRMWARE revisions 5.0.5.0 or later only!");
      if ((int) (address % 2U) != 0)
        throw new ApplicationException("Address argument must specify a 2 byte aligned (even) location.");
      if ((int) size == 0 || size > 1024U)
        throw new ApplicationException("Size argument must specify a non-zero value less than or equal to 1024.");
      if ((int) size == 0 || !this.IsBlockInFlash(address, size))
        throw new ApplicationException("Requested locataion/size is not in a valid flash memory range!");
      this.m_port.WriteBytes(new PacketPeekMemoryCommand(address, size).Bytes);
      byte commandId;
      byte[] numArray = this.ReadGenericCommandPacket(TimeSpan.FromSeconds(2.0), out commandId);
      if ((int) commandId == 6)
        return numArray;
      else
        throw new ApplicationException("PeekMemory did not return a valid (ACK) response!");
    }

    public void PokeMemory(uint address, byte[] data)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate == ReceiverPort.BaudRate.Bios && this.CachedBiosRevision < 34145280U)
        throw new ApplicationException("Function supported in BIOS revisions 2.9.4.0 or later only!");
      if (this.m_port.CurrentBaudRate == ReceiverPort.BaudRate.Firmware && this.CachedFirmwareRevision < 83887360U)
        throw new ApplicationException("Function supported in FIRMWARE revisions 5.0.5.0 or later only!");
      if ((int) (address % 2U) != 0)
        throw new ApplicationException("Address argument must specify a 2 byte aligned (even) location.");
      if (data.Length % 2 != 0)
        throw new ApplicationException("Data length must specify a 2 byte aligned (even) size.");
      if (data.Length == 0 || data.Length > 1024)
        throw new ApplicationException("Size argument must specify a non-zero value less than or equal to 1024.");
      if (this.m_port.CurrentBaudRate == ReceiverPort.BaudRate.Firmware)
      {
        uint version = 1U;
        if (this.CachedFirmwareRevision >= 83887360U)
          version = 2U;
        uint size = Convert.ToUInt32(data.Length);
        if (this.IsBlockInBios(address, size, version) || this.IsBlockInConfig(address, size, version) || (this.IsBlockInFirmware(address, size, version) || this.IsBlockInDatabase(address, size, version)) || (this.IsBlockInUnused(address, size, version) || this.IsBlockInLicense(address, size, version)))
          throw new ApplicationException("Requested locataion/size is not in a valid flash memory range for FIRMWARE mode!");
      }
      byte[] bytes = BitConverter.GetBytes(address);
      byte[] payload = new byte[bytes.Length + data.Length];
      Array.Copy((Array) bytes, (Array) payload, bytes.Length);
      Array.Copy((Array) data, 0, (Array) payload, bytes.Length, data.Length);
      this.WriteGenericCommandPacket(R2Commands.PokeMemory, payload);
      PacketAckResponse packetAckResponse = new PacketAckResponse(this.m_port.ReadBytes(PacketAckResponse.PacketSize));
    }

    public DateTime ReadSensorImplantDate()
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      if (this.CachedFirmwareRevision < 83887360U)
        throw new ApplicationException("Function supported in FIRMWARE revisions 5.0.5.0 or later only!");
      this.m_port.WriteBytes(PacketReadSensorImplantDateCommand.Instance.Bytes);
      PacketReadDateResponse readDateResponse = new PacketReadDateResponse(this.m_port.ReadBytes(PacketReadDateResponse.PacketSize));
      return R2ReceiverValues.R2BaseDateTime.AddMilliseconds((double) readDateResponse.Milliseconds);
    }

    public void WriteSensorImplantDate(DateTime date)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      if (this.CachedFirmwareRevision < 83887360U)
        throw new ApplicationException("Function supported in FIRMWARE revisions 5.0.5.0 or later only!");
      this.m_port.WriteBytes(new PacketWriteSensorImplantDateCommand((ulong) (date - R2ReceiverValues.R2BaseDateTime).TotalMilliseconds).Bytes);
      PacketAckResponse packetAckResponse = new PacketAckResponse(this.m_port.ReadBytes(PacketAckResponse.PacketSize));
    }

    public uint ReadNumberOf3DayLicenses()
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      if (this.CachedFirmwareRevision < 83887360U)
        throw new ApplicationException("Function supported in FIRMWARE revisions 5.0.5.0 or later only!");
      this.m_port.WriteBytes(PacketReadNumberOf3DayLicensesCommand.Instance.Bytes);
      return new PacketReadUInt32Response(this.m_port.ReadBytes(PacketReadUInt32Response.PacketSize)).Value;
    }

    public uint ReadNumberOf7DayLicenses()
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      if (this.CachedFirmwareRevision < 83887360U)
        throw new ApplicationException("Function supported in FIRMWARE revisions 5.0.5.0 or later only!");
      this.m_port.WriteBytes(PacketReadNumberOf7DayLicensesCommand.Instance.Bytes);
      return new PacketReadUInt32Response(this.m_port.ReadBytes(PacketReadUInt32Response.PacketSize)).Value;
    }

    public uint ReadTransmitterId()
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      if (this.CachedFirmwareRevision < 83887360U)
        throw new ApplicationException("Function supported in FIRMWARE revisions 5.0.5.0 or later only!");
      this.m_port.WriteBytes(PacketReadTransmitterIdCommand.Instance.Bytes);
      return new PacketReadUInt32Response(this.m_port.ReadBytes(PacketReadUInt32Response.PacketSize)).Value;
    }

    public void WriteTransmitterId(uint transmitterId)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      if (this.CachedFirmwareRevision < 83887360U)
        throw new ApplicationException("Function supported in FIRMWARE revisions 5.0.5.0 or later only!");
      this.m_port.WriteBytes(new PacketWriteTransmitterIdCommand(transmitterId).Bytes);
      PacketAckResponse packetAckResponse = new PacketAckResponse(this.m_port.ReadBytes(PacketAckResponse.PacketSize));
    }

    public uint ReadLowGlucoseThreshold()
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      if (this.CachedFirmwareRevision < 83887360U)
        throw new ApplicationException("Function supported in FIRMWARE revisions 5.0.5.0 or later only!");
      this.m_port.WriteBytes(PacketReadLowGlucoseThresholdCommand.Instance.Bytes);
      return new PacketReadUInt32Response(this.m_port.ReadBytes(PacketReadUInt32Response.PacketSize)).Value;
    }

    public void WriteLowGlucoseThreshold(uint threshold)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      if (this.CachedFirmwareRevision < 83887360U)
        throw new ApplicationException("Function supported in FIRMWARE revisions 5.0.5.0 or later only!");
      this.m_port.WriteBytes(new PacketWriteLowGlucoseThresholdCommand(threshold).Bytes);
      PacketAckResponse packetAckResponse = new PacketAckResponse(this.m_port.ReadBytes(PacketAckResponse.PacketSize));
    }

    public uint ReadHighGlucoseThreshold()
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      if (this.CachedFirmwareRevision < 83887360U)
        throw new ApplicationException("Function supported in FIRMWARE revisions 5.0.5.0 or later only!");
      this.m_port.WriteBytes(PacketReadHighGlucoseThresholdCommand.Instance.Bytes);
      return new PacketReadUInt32Response(this.m_port.ReadBytes(PacketReadUInt32Response.PacketSize)).Value;
    }

    public void WriteHighGlucoseThreshold(uint threshold)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      if (this.CachedFirmwareRevision < 83887360U)
        throw new ApplicationException("Function supported in FIRMWARE revisions 5.0.5.0 or later only!");
      this.m_port.WriteBytes(new PacketWriteHighGlucoseThresholdCommand(threshold).Bytes);
      PacketAckResponse packetAckResponse = new PacketAckResponse(this.m_port.ReadBytes(PacketAckResponse.PacketSize));
    }

    public bool IsBlinded()
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      if (this.CachedFirmwareRevision < 83887360U)
        throw new ApplicationException("Function supported in FIRMWARE revisions 5.0.5.0 or later only!");
      this.m_port.WriteBytes(PacketReadBlindedModeCommand.Instance.Bytes);
      return (int) new PacketReadByteResponse(this.m_port.ReadBytes(PacketReadByteResponse.PacketSize)).Value != 0;
    }

    public void WriteBlindedMode(bool isBlinded)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      if (this.CachedFirmwareRevision < 83887360U)
        throw new ApplicationException("Function supported in FIRMWARE revisions 5.0.5.0 or later only!");
      byte mode = (byte) 0;
      if (isBlinded)
        mode = (byte) 1;
      this.m_port.WriteBytes(new PacketWriteBlindedModeCommand(mode).Bytes);
      PacketAckResponse packetAckResponse = new PacketAckResponse(this.m_port.ReadBytes(PacketAckResponse.PacketSize));
    }

    public TimeSpan ReadDisplayTimeOffset()
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      if (this.CachedFirmwareRevision < 83887360U)
        throw new ApplicationException("Function supported in FIRMWARE revisions 5.0.5.0 or later only!");
      this.m_port.WriteBytes(PacketReadDisplayTimeOffsetCommand.Instance.Bytes);
      return TimeSpan.FromMilliseconds(Convert.ToDouble((long) new PacketReadDateResponse(this.m_port.ReadBytes(PacketReadDateResponse.PacketSize)).Milliseconds));
    }

    public void WriteDisplayTimeOffset(TimeSpan offset)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      if (this.CachedFirmwareRevision < 83887360U)
        throw new ApplicationException("Function supported in FIRMWARE revisions 5.0.5.0 or later only!");
      this.m_port.WriteBytes(new PacketWriteDisplayTimeOffsetCommand(Convert.ToInt64(offset.TotalMilliseconds)).Bytes);
      PacketAckResponse packetAckResponse = new PacketAckResponse(this.m_port.ReadBytes(PacketAckResponse.PacketSize));
    }

    public DateTime ReadGMT()
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      if (this.CachedFirmwareRevision < 83887360U)
        throw new ApplicationException("Function supported in FIRMWARE revisions 5.0.5.0 or later only!");
      this.m_port.WriteBytes(PacketReadGMTCommand.Instance.Bytes);
      PacketReadDateResponse readDateResponse = new PacketReadDateResponse(this.m_port.ReadBytes(PacketReadDateResponse.PacketSize));
      return R2ReceiverValues.R2BaseDateTime.AddMilliseconds((double) readDateResponse.Milliseconds);
    }

    public void WriteGMT(DateTime date)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      if (this.CachedFirmwareRevision < 83887360U)
        throw new ApplicationException("Function supported in FIRMWARE revisions 5.0.5.0 or later only!");
      this.m_port.WriteBytes(new PacketWriteGMTCommand((ulong) (date - R2ReceiverValues.R2BaseDateTime).TotalMilliseconds).Bytes);
      PacketAckResponse packetAckResponse = new PacketAckResponse(this.m_port.ReadBytes(PacketAckResponse.PacketSize));
    }

    public void DisableTransmitter()
    {
      this.EnableTransmitter(false);
    }

    public void EnterFunctionalTestMode()
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      this.WriteGenericCommandPacket(R2Commands.EnterFunctionalTestMode);
      byte commandId = (byte) 0;
      this.ReadGenericCommandPacket(TimeSpan.FromSeconds(5.0), out commandId);
      if ((int) commandId == 6)
        return;
      if ((int) commandId == 22)
        throw new ApplicationException("Firmware returned 'Invalid Command'.  EnterFunctionalTestMode command not supported in this firmware.");
      if (Enum.IsDefined(typeof (R2Commands), (object) commandId))
        throw new ApplicationException("Unexpected response id from EnterFunctionalTestMode. Id = " + Enum.GetName(typeof (R2Commands), (object) commandId));
      else
        throw new ApplicationException("Unexpected response id from EnterFunctionalTestMode. Id = " + commandId.ToString());
    }

    public int ReadHardwareBoardId()
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      this.WriteGenericCommandPacket(R2Commands.ReadHardwareBoardId);
      byte commandId = (byte) 0;
      byte[] numArray = this.ReadGenericCommandPacket(TimeSpan.FromSeconds(2.0), out commandId);
      int num;
      if ((int) commandId == 6)
        num = BitConverter.ToInt32(numArray, 0);
      else if ((int) commandId != 22)
      {
        if (Enum.IsDefined(typeof (R2Commands), (object) commandId))
          throw new ApplicationException("Unexpected response id from ReadHardwareBoardId. Id = " + Enum.GetName(typeof (R2Commands), (object) commandId));
        else
          throw new ApplicationException("Unexpected response id from ReadHardwareBoardId. Id = " + commandId.ToString());
      }
      else
        num = 0;
      return num;
    }

    public void WriteUserLowAlertType(R2AlertType alertType)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      this.WriteGenericCommandPacket(R2Commands.WriteUserLowAlertType, BitConverter.GetBytes((int) alertType));
      byte commandId = (byte) 0;
      this.ReadGenericCommandPacket(TimeSpan.FromSeconds(5.0), out commandId);
      if ((int) commandId == 6)
        return;
      if ((int) commandId == 22)
        throw new ApplicationException("Firmware returned 'Invalid Command'.  WriteUserLowAlertType command not supported in this firmware.");
      if (Enum.IsDefined(typeof (R2Commands), (object) commandId))
        throw new ApplicationException("Unexpected response id from WriteUserLowAlertType. Id = " + Enum.GetName(typeof (R2Commands), (object) commandId));
      else
        throw new ApplicationException("Unexpected response id from WriteUserLowAlertType. Id = " + commandId.ToString());
    }

    public void WriteUserHighAlertType(R2AlertType alertType)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      this.WriteGenericCommandPacket(R2Commands.WriteUserHighAlertType, BitConverter.GetBytes((int) alertType));
      byte commandId = (byte) 0;
      this.ReadGenericCommandPacket(TimeSpan.FromSeconds(5.0), out commandId);
      if ((int) commandId == 6)
        return;
      if ((int) commandId == 22)
        throw new ApplicationException("Firmware returned 'Invalid Command'.  WriteUserHighAlertType command not supported in this firmware.");
      if (Enum.IsDefined(typeof (R2Commands), (object) commandId))
        throw new ApplicationException("Unexpected response id from WriteUserHighAlertType. Id = " + Enum.GetName(typeof (R2Commands), (object) commandId));
      else
        throw new ApplicationException("Unexpected response id from WriteUserHighAlertType. Id = " + commandId.ToString());
    }

    public void WriteOutOfRangeAlertType(R2AlertType alertType)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      this.WriteGenericCommandPacket(R2Commands.WriteOutOfRangeAlertType, BitConverter.GetBytes((int) alertType));
      byte commandId = (byte) 0;
      this.ReadGenericCommandPacket(TimeSpan.FromSeconds(5.0), out commandId);
      if ((int) commandId == 6)
        return;
      if ((int) commandId == 22)
        throw new ApplicationException("Firmware returned 'Invalid Command'.  WriteOutOfRangeAlertType command not supported in this firmware.");
      if (Enum.IsDefined(typeof (R2Commands), (object) commandId))
        throw new ApplicationException("Unexpected response id from WriteOutOfRangeAlertType. Id = " + Enum.GetName(typeof (R2Commands), (object) commandId));
      else
        throw new ApplicationException("Unexpected response id from WriteOutOfRangeAlertType. Id = " + commandId.ToString());
    }

    public void WriteOtherAlertType(R2AlertType alertType)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      this.WriteGenericCommandPacket(R2Commands.WriteOtherAlertType, BitConverter.GetBytes((int) alertType));
      byte commandId = (byte) 0;
      this.ReadGenericCommandPacket(TimeSpan.FromSeconds(5.0), out commandId);
      if ((int) commandId == 6)
        return;
      if ((int) commandId == 22)
        throw new ApplicationException("Firmware returned 'Invalid Command'.  WriteOtherAlertType command not supported in this firmware.");
      if (Enum.IsDefined(typeof (R2Commands), (object) commandId))
        throw new ApplicationException("Unexpected response id from WriteOtherAlertType. Id = " + Enum.GetName(typeof (R2Commands), (object) commandId));
      else
        throw new ApplicationException("Unexpected response id from WriteOtherAlertType. Id = " + commandId.ToString());
    }

    public void WriteUpRateAlertType(R2AlertType alertType)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      this.WriteGenericCommandPacket(R2Commands.WriteUpRateAlertType, BitConverter.GetBytes((int) alertType));
      byte commandId = (byte) 0;
      this.ReadGenericCommandPacket(TimeSpan.FromSeconds(5.0), out commandId);
      if ((int) commandId == 6)
        return;
      if ((int) commandId == 22)
        throw new ApplicationException("Firmware returned 'Invalid Command'.  WriteUpRateAlertType command not supported in this firmware.");
      if (Enum.IsDefined(typeof (R2Commands), (object) commandId))
        throw new ApplicationException("Unexpected response id from WriteUpRateAlertType. Id = " + Enum.GetName(typeof (R2Commands), (object) commandId));
      else
        throw new ApplicationException("Unexpected response id from WriteUpRateAlertType. Id = " + commandId.ToString());
    }

    public void WriteDownRateAlertType(R2AlertType alertType)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      this.WriteGenericCommandPacket(R2Commands.WriteDownRateAlertType, BitConverter.GetBytes((int) alertType));
      byte commandId = (byte) 0;
      this.ReadGenericCommandPacket(TimeSpan.FromSeconds(5.0), out commandId);
      if ((int) commandId == 6)
        return;
      if ((int) commandId == 22)
        throw new ApplicationException("Firmware returned 'Invalid Command'.  WriteDownRateAlertType command not supported in this firmware.");
      if (Enum.IsDefined(typeof (R2Commands), (object) commandId))
        throw new ApplicationException("Unexpected response id from WriteDownRateAlertType. Id = " + Enum.GetName(typeof (R2Commands), (object) commandId));
      else
        throw new ApplicationException("Unexpected response id from WriteDownRateAlertType. Id = " + commandId.ToString());
    }

    public void WriteUserLowSnooze(int minutes)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      this.WriteGenericCommandPacket(R2Commands.WriteUserLowSnooze, BitConverter.GetBytes(minutes));
      byte commandId = (byte) 0;
      this.ReadGenericCommandPacket(TimeSpan.FromSeconds(5.0), out commandId);
      if ((int) commandId == 6)
        return;
      if ((int) commandId == 22)
        throw new ApplicationException("Firmware returned 'Invalid Command'.  WriteUserLowSnooze command not supported in this firmware.");
      if (Enum.IsDefined(typeof (R2Commands), (object) commandId))
        throw new ApplicationException("Unexpected response id from WriteUserLowSnooze. Id = " + Enum.GetName(typeof (R2Commands), (object) commandId));
      else
        throw new ApplicationException("Unexpected response id from WriteUserLowSnooze. Id = " + commandId.ToString());
    }

    public void WriteUserHighSnooze(int minutes)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      this.WriteGenericCommandPacket(R2Commands.WriteUserHighSnooze, BitConverter.GetBytes(minutes));
      byte commandId = (byte) 0;
      this.ReadGenericCommandPacket(TimeSpan.FromSeconds(5.0), out commandId);
      if ((int) commandId == 6)
        return;
      if ((int) commandId == 22)
        throw new ApplicationException("Firmware returned 'Invalid Command'.  WriteUserHighSnooze command not supported in this firmware.");
      if (Enum.IsDefined(typeof (R2Commands), (object) commandId))
        throw new ApplicationException("Unexpected response id from WriteUserHighSnooze. Id = " + Enum.GetName(typeof (R2Commands), (object) commandId));
      else
        throw new ApplicationException("Unexpected response id from WriteUserHighSnooze. Id = " + commandId.ToString());
    }

    public void WriteOutOfRangeTime(int minutes)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      this.WriteGenericCommandPacket(R2Commands.WriteOutOfRangeTime, BitConverter.GetBytes(minutes));
      byte commandId = (byte) 0;
      this.ReadGenericCommandPacket(TimeSpan.FromSeconds(5.0), out commandId);
      if ((int) commandId == 6)
        return;
      if ((int) commandId == 22)
        throw new ApplicationException("Firmware returned 'Invalid Command'.  WriteOutOfRangeTime command not supported in this firmware.");
      if (Enum.IsDefined(typeof (R2Commands), (object) commandId))
        throw new ApplicationException("Unexpected response id from WriteOutOfRangeTime. Id = " + Enum.GetName(typeof (R2Commands), (object) commandId));
      else
        throw new ApplicationException("Unexpected response id from WriteOutOfRangeTime. Id = " + commandId.ToString());
    }

    public void WriteUpRate(int rate)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      this.WriteGenericCommandPacket(R2Commands.WriteUpRate, BitConverter.GetBytes(rate));
      byte commandId = (byte) 0;
      this.ReadGenericCommandPacket(TimeSpan.FromSeconds(5.0), out commandId);
      if ((int) commandId == 6)
        return;
      if ((int) commandId == 22)
        throw new ApplicationException("Firmware returned 'Invalid Command'.  WriteUpRate command not supported in this firmware.");
      if (Enum.IsDefined(typeof (R2Commands), (object) commandId))
        throw new ApplicationException("Unexpected response id from WriteUpRate. Id = " + Enum.GetName(typeof (R2Commands), (object) commandId));
      else
        throw new ApplicationException("Unexpected response id from WriteUpRate. Id = " + commandId.ToString());
    }

    public void WriteDownRate(int rate)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      this.WriteGenericCommandPacket(R2Commands.WriteDownRate, BitConverter.GetBytes(rate));
      byte commandId = (byte) 0;
      this.ReadGenericCommandPacket(TimeSpan.FromSeconds(5.0), out commandId);
      if ((int) commandId == 6)
        return;
      if ((int) commandId == 22)
        throw new ApplicationException("Firmware returned 'Invalid Command'.  WriteDownRate command not supported in this firmware.");
      if (Enum.IsDefined(typeof (R2Commands), (object) commandId))
        throw new ApplicationException("Unexpected response id from WriteDownRate. Id = " + Enum.GetName(typeof (R2Commands), (object) commandId));
      else
        throw new ApplicationException("Unexpected response id from WriteDownRate. Id = " + commandId.ToString());
    }

    public void ClearRtcResetTime()
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      this.WriteGenericCommandPacket(R2Commands.ClearRtcResetTime);
      byte commandId = (byte) 0;
      this.ReadGenericCommandPacket(TimeSpan.FromSeconds(5.0), out commandId);
      if ((int) commandId == 6)
        return;
      if ((int) commandId == 22)
        throw new ApplicationException("Firmware returned 'Invalid Command'.  ClearRtcResetTime command not supported in this firmware.");
      if (Enum.IsDefined(typeof (R2Commands), (object) commandId))
        throw new ApplicationException("Unexpected response id from ClearRtcResetTime. Id = " + Enum.GetName(typeof (R2Commands), (object) commandId));
      else
        throw new ApplicationException("Unexpected response id from ClearRtcResetTime. Id = " + commandId.ToString());
    }

    public bool ReadErrorLogInfo(out R2ErrorLogInfo errorLogInfo)
    {
      errorLogInfo = new R2ErrorLogInfo();
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      bool flag = false;
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      this.WriteGenericCommandPacket(R2Commands.ReadErrorLogInfo);
      byte commandId = (byte) 0;
      byte[] bytes = this.ReadGenericCommandPacket(TimeSpan.FromSeconds(5.0), out commandId);
      if ((int) commandId == 6)
      {
        errorLogInfo = (R2ErrorLogInfo) DataTools.ConvertBytesToObject(bytes, 0, typeof (R2ErrorLogInfo));
        return true;
      }
      else
      {
        if ((int) commandId == 22)
          return false;
        flag = false;
        if (Enum.IsDefined(typeof (R2Commands), (object) commandId))
          throw new ApplicationException("Unexpected response id from ReadErrorLogInfo. Id = " + Enum.GetName(typeof (R2Commands), (object) commandId));
        else
          throw new ApplicationException("Unexpected response id from ReadErrorLogInfo. Id = " + commandId.ToString());
      }
    }

    public double ReadBatteryVoltage()
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new ApplicationException("Invalid port or port closed!");
      if (this.m_port.CurrentBaudRate != ReceiverPort.BaudRate.Firmware)
        throw new ApplicationException("Function supported in FIRMWARE mode only!");
      this.WriteGenericCommandPacket(R2Commands.ReadBatteryVoltage);
      byte commandId = (byte) 0;
      byte[] numArray = this.ReadGenericCommandPacket(TimeSpan.FromSeconds(5.0), out commandId);
      if ((int) commandId == 6)
        return BitConverter.ToDouble(numArray, 0);
      if (Enum.IsDefined(typeof (R2Commands), (object) commandId))
        throw new ApplicationException("Unexpected response id from ReadBatteryVoltage. Id = " + Enum.GetName(typeof (R2Commands), (object) commandId));
      else
        throw new ApplicationException("Unexpected response id from ReadBatteryVoltage. Id = " + commandId.ToString());
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
      if (!this.IsDisposed || this.m_port == null)
        return;
      this.m_port.Dispose();
    }

    private bool IsBlockInFlash(uint address, uint size)
    {
      uint num1 = address;
      uint num2 = address + size;
      uint num3 = R2ReceiverValues.FlashStartAddress;
      uint num4 = R2ReceiverValues.FlashEndAddress;
      bool flag = true;
      if (num1 < num3 || num2 < num3 || (num1 >= num4 || num2 > num4))
        flag = false;
      return flag;
    }

    private bool IsBlockInBios(uint address, uint size, uint version)
    {
      uint num1 = address;
      uint num2 = address + size;
      uint num3 = (int) version == 1 ? R2ReceiverValues.BiosStartAddress : R2ReceiverValues.BiosStartAddressV2;
      uint num4 = (int) version == 1 ? R2ReceiverValues.BiosEndAddress : R2ReceiverValues.BiosEndAddressV2;
      bool flag = true;
      if (num1 < num3 || num2 < num3 || (num1 >= num4 || num2 > num4))
        flag = false;
      return flag;
    }

    private bool IsBlockInConfig(uint address, uint size, uint version)
    {
      uint num1 = address;
      uint num2 = address + size;
      uint num3 = (int) version == 1 ? R2ReceiverValues.ConfigStartAddress : R2ReceiverValues.ConfigStartAddressV2;
      uint num4 = (int) version == 1 ? R2ReceiverValues.ConfigEndAddress : R2ReceiverValues.ConfigEndAddressV2;
      bool flag = true;
      if (num1 < num3 || num2 < num3 || (num1 >= num4 || num2 > num4))
        flag = false;
      return flag;
    }

    private bool IsBlockInError(uint address, uint size, uint version)
    {
      uint num1 = address;
      uint num2 = address + size;
      uint num3 = (int) version == 1 ? R2ReceiverValues.ErrorStartAddress : R2ReceiverValues.ErrorStartAddressV2;
      uint num4 = (int) version == 1 ? R2ReceiverValues.ErrorEndAddress : R2ReceiverValues.ErrorEndAddressV2;
      bool flag = true;
      if (num1 < num3 || num2 < num3 || (num1 >= num4 || num2 > num4))
        flag = false;
      return flag;
    }

    private bool IsBlockInEvent(uint address, uint size, uint version)
    {
      uint num1 = address;
      uint num2 = address + size;
      uint num3 = (int) version == 1 ? R2ReceiverValues.EventStartAddress : R2ReceiverValues.EventStartAddressV2;
      uint num4 = (int) version == 1 ? R2ReceiverValues.EventEndAddress : R2ReceiverValues.EventEndAddressV2;
      bool flag = true;
      if (num1 < num3 || num2 < num3 || (num1 >= num4 || num2 > num4))
        flag = false;
      return flag;
    }

    private bool IsBlockInLicense(uint address, uint size, uint version)
    {
      uint num1 = address;
      uint num2 = address + size;
      uint num3 = (int) version == 1 ? R2ReceiverValues.LicenseStartAddress : R2ReceiverValues.LicenseStartAddressV2;
      uint num4 = (int) version == 1 ? R2ReceiverValues.LicenseEndAddress : R2ReceiverValues.LicenseEndAddressV2;
      bool flag = true;
      if (num1 < num3 || num2 < num3 || (num1 >= num4 || num2 > num4))
        flag = false;
      return flag;
    }

    private bool IsBlockInUnused(uint address, uint size, uint version)
    {
      uint num1 = address;
      uint num2 = address + size;
      uint num3 = (int) version == 1 ? R2ReceiverValues.UnusedStartAddress : R2ReceiverValues.UnusedStartAddressV2;
      uint num4 = (int) version == 1 ? R2ReceiverValues.UnusedEndAddress : R2ReceiverValues.UnusedEndAddressV2;
      bool flag = true;
      if (num1 < num3 || num2 < num3 || (num1 >= num4 || num2 > num4))
        flag = false;
      return flag;
    }

    private bool IsBlockInFirmware(uint address, uint size, uint version)
    {
      uint num1 = address;
      uint num2 = address + size;
      uint num3 = (int) version == 1 ? R2ReceiverValues.FirmwareStartAddress : R2ReceiverValues.FirmwareStartAddressV2;
      uint num4 = (int) version == 1 ? R2ReceiverValues.FirmwareEndAddress : R2ReceiverValues.FirmwareEndAddressV2;
      bool flag = true;
      if (num1 < num3 || num2 < num3 || (num1 >= num4 || num2 > num4))
        flag = false;
      return flag;
    }

    private bool IsBlockInDatabase(uint address, uint size, uint version)
    {
      uint num1 = address;
      uint num2 = address + size;
      uint num3 = (int) version == 1 ? R2ReceiverValues.DatabaseStartAddress : R2ReceiverValues.DatabaseStartAddressV2;
      uint num4 = (int) version == 1 ? R2ReceiverValues.DatabaseEndAddress : R2ReceiverValues.DatabaseEndAddressV2;
      bool flag = true;
      if (num1 < num3 || num2 < num3 || (num1 >= num4 || num2 > num4))
        flag = false;
      return flag;
    }

    public delegate byte[] ReadBlockDelegate(uint address, uint size);
  }
}

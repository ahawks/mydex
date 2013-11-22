// Type: Dexcom.ReceiverApi.ReceiverApi
// Assembly: Dexcom.ReceiverCommercialApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 3F623F65-908D-40A7-87D3-892C5D1FFA2E
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.ReceiverCommercialApi.dll

using Dexcom.Common;
using Dexcom.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Xml;

namespace Dexcom.ReceiverApi
{
  public class ReceiverApi : IDisposable
  {
    public static readonly uint ImplementedApiVersion = 33685504U;
    public static readonly uint ImplementedTestApiVersion = 2113536U;
    internal readonly TimeSpan CommandTimeout = TimeSpan.FromMilliseconds(2000.0);
    private const int ScreenBufferChunkSize = 2048;
    private ReceiverPort m_port;
    private XFirmwareHeader m_cachedFirmwareHeader;
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

    public XFirmwareHeader CachedFirmwareHeader
    {
      get
      {
        if (this.m_cachedFirmwareHeader == null)
          this.m_cachedFirmwareHeader = this.ReadFirmwareHeader();
        return this.m_cachedFirmwareHeader;
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

    static ReceiverApi()
    {
    }

    public ReceiverApi()
    {
      this.AttachReceiver();
    }

    public ReceiverApi(UsbComPort port)
    {
      if (port != null)
        this.AttachReceiver(port);
      else
        this.AttachReceiver();
    }

    ~ReceiverApi()
    {
      this.Cleanup();
    }

    public void Ping()
    {
      this.WriteGenericCommandPacket(ReceiverCommands.Ping);
      byte commandId = (byte) 0;
      byte[] payload = this.ReadGenericCommandPacket(this.CommandTimeout, out commandId);
      this.VerifyResponseCommandByte(commandId, payload);
      this.VerifyPayloadLength(payload.Length, 0);
    }

    public XFirmwareHeader ReadFirmwareHeader()
    {
      this.WriteGenericCommandPacket(ReceiverCommands.ReadFirmwareHeader);
      byte commandId = (byte) 0;
      byte[] numArray = this.ReadGenericCommandPacket(this.CommandTimeout, out commandId);
      this.VerifyResponseCommandByte(commandId, numArray);
      string @string = Encoding.UTF8.GetString(numArray);
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.LoadXml(@string);
      return new XFirmwareHeader(xmlDocument.DocumentElement);
    }

    public XPartitionInfo ReadDatabasePartitionInfo()
    {
      this.WriteGenericCommandPacket(ReceiverCommands.ReadDatabaseParitionInfo);
      byte commandId = (byte) 0;
      byte[] numArray = this.ReadGenericCommandPacket(this.CommandTimeout, out commandId);
      this.VerifyResponseCommandByte(commandId, numArray);
      string @string = Encoding.UTF8.GetString(numArray);
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.LoadXml(@string);
      return new XPartitionInfo(xmlDocument.SelectSingleNode("/PartitionInfo") as XmlElement);
    }

    public DatabasePageRange ReadDatabasePageRange(ReceiverRecordType recordType)
    {
      this.WriteGenericCommandPacket(ReceiverCommands.ReadDatabasePageRange, (byte) recordType);
      byte commandId = (byte) 0;
      byte[] numArray = this.ReadGenericCommandPacket(this.CommandTimeout, out commandId);
      this.VerifyResponseCommandByte(commandId, numArray);
      this.VerifyPayloadLength(numArray.Length, Marshal.SizeOf(typeof (DatabasePageRange)));
      return (DatabasePageRange) DataTools.ConvertBytesToObject(numArray, 0, typeof (DatabasePageRange));
    }

    public DatabasePage ReadDatabasePage(ReceiverRecordType recordType, uint pageNumber)
    {
      this.WriteGenericCommandPacket(ReceiverCommands.ReadDatabasePages, (byte) recordType, pageNumber, (byte) 1);
      byte commandId = (byte) 0;
      byte[] numArray = this.ReadGenericCommandPacket(this.CommandTimeout, out commandId);
      this.VerifyResponseCommandByte(commandId, numArray);
      this.VerifyPayloadLength(numArray.Length, Marshal.SizeOf(typeof (DatabasePage)));
      DatabasePage databasePage = (DatabasePage) DataTools.ConvertBytesToObject(numArray, 0, typeof (DatabasePage));
      byte[] buf = DataTools.ConvertObjectToBytes((object) databasePage.PageHeader);
      if ((int) Crc.CalculateCrc16(buf, 0, buf.Length - 2) != (int) databasePage.PageHeader.Crc)
        throw new DexComException("Page Header CRC validation failed.");
      else
        return databasePage;
    }

    public List<DatabasePage> ReadDatabasePages(ReceiverRecordType recordType, uint pageNumber, byte numberOfPages)
    {
      this.WriteGenericCommandPacket(ReceiverCommands.ReadDatabasePages, (byte) recordType, pageNumber, numberOfPages);
      byte commandId = (byte) 0;
      byte[] numArray = this.ReadGenericCommandPacket(this.CommandTimeout, out commandId);
      this.VerifyResponseCommandByte(commandId, numArray);
      this.VerifyPayloadLength(numArray.Length, Marshal.SizeOf(typeof (DatabasePage)) * (int) numberOfPages);
      List<DatabasePage> list = new List<DatabasePage>();
      for (int index = 0; index < (int) numberOfPages; ++index)
      {
        DatabasePage databasePage = (DatabasePage) DataTools.ConvertBytesToObject(numArray, Marshal.SizeOf(typeof (DatabasePage)) * index, typeof (DatabasePage));
        byte[] buf = DataTools.ConvertObjectToBytes((object) databasePage.PageHeader);
        if ((int) Crc.CalculateCrc16(buf, 0, buf.Length - 2) != (int) databasePage.PageHeader.Crc)
          throw new DexComException("Page Header CRC validation failed.");
        list.Add(databasePage);
      }
      return list;
    }

    public DatabasePageHeader ReadDatabasePageHeader(ReceiverRecordType recordType, uint pageNumber)
    {
      this.WriteGenericCommandPacket(ReceiverCommands.ReadDatabasePageHeader, (byte) recordType, pageNumber);
      byte commandId = (byte) 0;
      byte[] numArray = this.ReadGenericCommandPacket(this.CommandTimeout, out commandId);
      this.VerifyResponseCommandByte(commandId, numArray);
      this.VerifyPayloadLength(numArray.Length, Marshal.SizeOf(typeof (DatabasePageHeader)));
      return (DatabasePageHeader) DataTools.ConvertBytesToObject(numArray, 0, typeof (DatabasePageHeader));
    }

    public string ReadTransmitterId()
    {
      this.WriteGenericCommandPacket(ReceiverCommands.ReadTransmitterID);
      byte commandId = (byte) 0;
      byte[] numArray = this.ReadGenericCommandPacket(this.CommandTimeout, out commandId);
      this.VerifyResponseCommandByte(commandId, numArray);
      this.VerifyPayloadLength(numArray.Length, 5);
      return Encoding.ASCII.GetString(numArray);
    }

    public void WriteTransmitterId(string transmitterCode)
    {
      this.WriteGenericCommandPacket(ReceiverCommands.WriteTransmitterID, Encoding.ASCII.GetBytes(transmitterCode));
      byte commandId = (byte) 0;
      byte[] payload = this.ReadGenericCommandPacket(this.CommandTimeout, out commandId);
      this.VerifyResponseCommandByte(commandId, payload);
      this.VerifyPayloadLength(payload.Length, 0);
    }

    public LanguageType ReadLanguage()
    {
      this.WriteGenericCommandPacket(ReceiverCommands.ReadLanguage);
      byte commandId = (byte) 0;
      byte[] payload = this.ReadGenericCommandPacket(this.CommandTimeout, out commandId);
      this.VerifyResponseCommandByte(commandId, payload);
      this.VerifyPayloadLength(payload.Length, 2);
      return (LanguageType) BitConverter.ToUInt16(payload, 0);
    }

    public void WriteLanguage(LanguageType languageType)
    {
      this.WriteGenericCommandPacket(ReceiverCommands.WriteLanguage, (ushort) languageType);
      byte commandId = (byte) 0;
      byte[] payload = this.ReadGenericCommandPacket(this.CommandTimeout, out commandId);
      this.VerifyResponseCommandByte(commandId, payload);
      this.VerifyPayloadLength(payload.Length, 0);
    }

    public DateTime ReadDisplayTime()
    {
      return this.ReadSystemTime() + this.ReadDisplayTimeOffset();
    }

    public TimeSpan ReadDisplayTimeOffset()
    {
      this.WriteGenericCommandPacket(ReceiverCommands.ReadDisplayTimeOffset);
      byte commandId = (byte) 0;
      byte[] payload = this.ReadGenericCommandPacket(this.CommandTimeout, out commandId);
      this.VerifyResponseCommandByte(commandId, payload);
      this.VerifyPayloadLength(payload.Length, 4);
      return TimeSpan.FromSeconds((double) BitConverter.ToInt32(payload, 0));
    }

    public void WriteDisplayTimeOffset(TimeSpan timeOffset)
    {
      this.WriteGenericCommandPacket(ReceiverCommands.WriteDisplayTimeOffset, Convert.ToInt32(timeOffset.TotalSeconds));
      byte commandId = (byte) 0;
      byte[] payload = this.ReadGenericCommandPacket(this.CommandTimeout, out commandId);
      this.VerifyResponseCommandByte(commandId, payload);
      this.VerifyPayloadLength(payload.Length, 0);
    }

    public DateTime ReadRTC()
    {
      this.WriteGenericCommandPacket(ReceiverCommands.ReadRTC);
      byte commandId = (byte) 0;
      byte[] payload = this.ReadGenericCommandPacket(this.CommandTimeout, out commandId);
      this.VerifyResponseCommandByte(commandId, payload);
      this.VerifyPayloadLength(payload.Length, 4);
      return ReceiverTools.ConvertReceiverTimeToDateTime(BitConverter.ToUInt32(payload, 0));
    }

    public void ResetReceiver()
    {
      this.WriteGenericCommandPacket(ReceiverCommands.ResetReceiver);
    }

    public uint ReadBatteryLevel()
    {
      this.WriteGenericCommandPacket(ReceiverCommands.ReadBatteryLevel);
      byte commandId = (byte) 0;
      byte[] payload = this.ReadGenericCommandPacket(this.CommandTimeout, out commandId);
      this.VerifyResponseCommandByte(commandId, payload);
      this.VerifyPayloadLength(payload.Length, 4);
      return BitConverter.ToUInt32(payload, 0);
    }

    public DateTime ReadSystemTime()
    {
      this.WriteGenericCommandPacket(ReceiverCommands.ReadSystemTime);
      byte commandId = (byte) 0;
      byte[] payload = this.ReadGenericCommandPacket(this.CommandTimeout, out commandId);
      this.VerifyResponseCommandByte(commandId, payload);
      this.VerifyPayloadLength(payload.Length, 4);
      return ReceiverTools.ConvertReceiverTimeToDateTime(BitConverter.ToUInt32(payload, 0));
    }

    public TimeSpan ReadSystemTimeOffset()
    {
      this.WriteGenericCommandPacket(ReceiverCommands.ReadSystemTimeOffset);
      byte commandId = (byte) 0;
      byte[] payload = this.ReadGenericCommandPacket(this.CommandTimeout, out commandId);
      this.VerifyResponseCommandByte(commandId, payload);
      this.VerifyPayloadLength(payload.Length, 4);
      return TimeSpan.FromSeconds((double) BitConverter.ToInt32(payload, 0));
    }

    public void WriteSystemTime(DateTime systemTime)
    {
      this.WriteGenericCommandPacket(ReceiverCommands.WriteSystemTime, ReceiverTools.ConvertDateTimeToReceiverTime(systemTime));
      byte commandId = (byte) 0;
      byte[] payload = this.ReadGenericCommandPacket(this.CommandTimeout, out commandId);
      this.VerifyResponseCommandByte(commandId, payload);
      this.VerifyPayloadLength(payload.Length, 0);
    }

    public GlucoseUnitType ReadGlucoseUnits()
    {
      this.WriteGenericCommandPacket(ReceiverCommands.ReadGlucoseUnit);
      byte commandId = (byte) 0;
      byte[] payload = this.ReadGenericCommandPacket(this.CommandTimeout, out commandId);
      this.VerifyResponseCommandByte(commandId, payload);
      this.VerifyPayloadLength(payload.Length, 1);
      return (GlucoseUnitType) payload[0];
    }

    public void WriteGlucoseUnits(GlucoseUnitType glucoseUnits)
    {
      this.WriteGenericCommandPacket(ReceiverCommands.WriteGlucoseUnit, (byte) glucoseUnits);
      byte commandId = (byte) 0;
      byte[] payload = this.ReadGenericCommandPacket(this.CommandTimeout, out commandId);
      this.VerifyResponseCommandByte(commandId, payload);
      this.VerifyPayloadLength(payload.Length, 0);
    }

    public BlindedModeType ReadBlindMode()
    {
      this.WriteGenericCommandPacket(ReceiverCommands.ReadBlindedMode);
      byte commandId = (byte) 0;
      byte[] payload = this.ReadGenericCommandPacket(this.CommandTimeout, out commandId);
      this.VerifyResponseCommandByte(commandId, payload);
      this.VerifyPayloadLength(payload.Length, 1);
      return (BlindedModeType) payload[0];
    }

    public void WriteBlindMode(BlindedModeType blindMode)
    {
      this.WriteGenericCommandPacket(ReceiverCommands.WriteBlindedMode, (byte) blindMode);
      byte commandId = (byte) 0;
      byte[] payload = this.ReadGenericCommandPacket(this.CommandTimeout, out commandId);
      this.VerifyResponseCommandByte(commandId, payload);
      this.VerifyPayloadLength(payload.Length, 0);
    }

    public bool AreMethodsAvailableForSetupWizardFlag()
    {
      return this.CachedFirmwareHeader.ApiVersionNumber >= 33619969U;
    }

    public bool ReadEnableSetUpWizardFlag()
    {
      if (this.CachedFirmwareHeader.ApiVersionNumber < 33619969U)
        throw new DexComException("Receiver Api version is not compatible with ReadEnableSetUpWizardFlag() method.");
      this.WriteGenericCommandPacket(ReceiverCommands.ReadEnableSetUpWizardFlag);
      byte commandId = (byte) 0;
      byte[] payload = this.ReadGenericCommandPacket(this.CommandTimeout, out commandId);
      this.VerifyResponseCommandByte(commandId, payload);
      this.VerifyPayloadLength(payload.Length, 1);
      bool flag = false;
      if ((int) payload[0] != 0)
        flag = true;
      return flag;
    }

    public void WriteEnableSetUpWizardFlag(bool isEnabled)
    {
      if (this.CachedFirmwareHeader.ApiVersionNumber < 33619969U)
        throw new DexComException("Receiver Api version is not compatible with WriteEnableSetUpWizardFlag() method.");
      this.WriteGenericCommandPacket(ReceiverCommands.WriteEnableSetUpWizardFlag, isEnabled ? (byte) 1 : (byte) 0);
      byte commandId = (byte) 0;
      byte[] payload = this.ReadGenericCommandPacket(this.CommandTimeout, out commandId);
      this.VerifyResponseCommandByte(commandId, payload);
      this.VerifyPayloadLength(payload.Length, 0);
    }

    public bool AreMethodsAvailableForSetupWizardState()
    {
      return this.CachedFirmwareHeader.ApiVersionNumber >= 33685504U;
    }

    public SetUpWizardState ReadSetUpWizardState()
    {
      if (this.CachedFirmwareHeader.ApiVersionNumber < 2105344U)
        throw new DexComException("Receiver Api version is not compatible with ReadSetUpWizardState() method.");
      this.WriteGenericCommandPacket(ReceiverCommands.ReadSetUpWizardState);
      byte commandId = (byte) 0;
      byte[] payload = this.ReadGenericCommandPacket(this.CommandTimeout, out commandId);
      this.VerifyResponseCommandByte(commandId, payload);
      this.VerifyPayloadLength(payload.Length, 1);
      return (SetUpWizardState) payload[0];
    }

    public void WriteSetUpWizardState(SetUpWizardState setupWizardState)
    {
      if (this.CachedFirmwareHeader.ApiVersionNumber < 2105344U)
        throw new DexComException("Receiver Api version is not compatible with WriteSetUpWizardState() method.");
      this.WriteGenericCommandPacket(ReceiverCommands.WriteSetUpWizardState, (byte) setupWizardState);
      byte commandId = (byte) 0;
      byte[] payload = this.ReadGenericCommandPacket(this.CommandTimeout, out commandId);
      this.VerifyResponseCommandByte(commandId, payload);
      this.VerifyPayloadLength(payload.Length, 0);
    }

    public ClockModeType ReadClockMode()
    {
      this.WriteGenericCommandPacket(ReceiverCommands.ReadClockMode);
      byte commandId = (byte) 0;
      byte[] payload = this.ReadGenericCommandPacket(this.CommandTimeout, out commandId);
      this.VerifyResponseCommandByte(commandId, payload);
      this.VerifyPayloadLength(payload.Length, 1);
      return (ClockModeType) payload[0];
    }

    public void WriteClockMode(ClockModeType clockMode)
    {
      this.WriteGenericCommandPacket(ReceiverCommands.WriteClockMode, (byte) clockMode);
      byte commandId = (byte) 0;
      byte[] payload = this.ReadGenericCommandPacket(this.CommandTimeout, out commandId);
      this.VerifyResponseCommandByte(commandId, payload);
      this.VerifyPayloadLength(payload.Length, 0);
    }

    public byte ReadDeviceMode()
    {
      this.WriteGenericCommandPacket(ReceiverCommands.ReadDeviceMode);
      byte commandId = (byte) 0;
      byte[] payload = this.ReadGenericCommandPacket(this.CommandTimeout, out commandId);
      this.VerifyResponseCommandByte(commandId, payload);
      this.VerifyPayloadLength(payload.Length, 1);
      return payload[0];
    }

    public bool IsManufacturingMode()
    {
      this.WriteGenericCommandPacket(ReceiverCommands.ReadDeviceMode);
      byte commandId = (byte) 0;
      byte[] payload = this.ReadGenericCommandPacket(this.CommandTimeout, out commandId);
      this.VerifyResponseCommandByte(commandId, payload);
      this.VerifyPayloadLength(payload.Length, 1);
      return (int) payload[0] == 1;
    }

    public bool HasManufacturingParameters()
    {
      return (int) this.ReadDatabasePageRange(ReceiverRecordType.ManufacturingData).LastPage != -1;
    }

    public ManufacturingParameterRecord ReadManufacturingParameters()
    {
      DatabasePageRange databasePageRange = this.ReadDatabasePageRange(ReceiverRecordType.ManufacturingData);
      if ((int) databasePageRange.LastPage == -1)
        throw new DexComException("Manufacturing Parameters Partition is empty.");
      DatabasePage databasePage = this.ReadDatabasePage(ReceiverRecordType.ManufacturingData, databasePageRange.LastPage);
      return new ManufacturingParameterRecord(databasePage.PageData, 0, (int) databasePage.PageHeader.Revision)
      {
        PageNumber = databasePage.PageHeader.PageNumber,
        RecordNumber = databasePage.PageHeader.FirstRecordIndex
      };
    }

    public void EraseDatabase()
    {
      this.WriteGenericCommandPacket(ReceiverCommands.EraseDatabase);
    }

    public void ShutDownReceiver()
    {
      this.WriteGenericCommandPacket(ReceiverCommands.ShutdownReceiver);
    }

    public void WritePcParameters(string xmlData)
    {
      byte[] bytes = Encoding.ASCII.GetBytes(xmlData);
      if (bytes.Length > 489)
        throw new DexComException("Maximum XML length exceeded.");
      this.WriteGenericCommandPacket(ReceiverCommands.WritePCParameters, bytes);
      byte commandId = (byte) 0;
      byte[] payload = this.ReadGenericCommandPacket(this.CommandTimeout, out commandId);
      this.VerifyResponseCommandByte(commandId, payload);
      this.VerifyPayloadLength(payload.Length, 0);
    }

    public bool HasPcParameters()
    {
      return (int) this.ReadDatabasePageRange(ReceiverRecordType.PCSoftwareParameter).LastPage != -1;
    }

    public PCParameterRecord ReadPcParameters()
    {
      DatabasePageRange databasePageRange = this.ReadDatabasePageRange(ReceiverRecordType.PCSoftwareParameter);
      if ((int) databasePageRange.LastPage == -1)
        throw new DexComException("PC Parameters Partition is empty.");
      DatabasePage databasePage = this.ReadDatabasePage(ReceiverRecordType.PCSoftwareParameter, databasePageRange.LastPage);
      return new PCParameterRecord(databasePage.PageData, 0, (int) databasePage.PageHeader.Revision)
      {
        PageNumber = databasePage.PageHeader.PageNumber,
        RecordNumber = databasePage.PageHeader.FirstRecordIndex
      };
    }

    public BatteryState ReadBatteryState()
    {
      this.WriteGenericCommandPacket(ReceiverCommands.ReadBatteryState);
      byte commandId = (byte) 0;
      byte[] payload = this.ReadGenericCommandPacket(this.CommandTimeout, out commandId);
      this.VerifyResponseCommandByte(commandId, payload);
      this.VerifyPayloadLength(payload.Length, 1);
      return (BatteryState) payload[0];
    }

    public ushort ReadHardwareBoardId()
    {
      this.WriteGenericCommandPacket(ReceiverCommands.ReadHardwareBoardId);
      byte commandId = (byte) 0;
      byte[] payload = this.ReadGenericCommandPacket(this.CommandTimeout, out commandId);
      this.VerifyResponseCommandByte(commandId, payload);
      this.VerifyPayloadLength(payload.Length, 2);
      return BitConverter.ToUInt16(payload, 0);
    }

    public void EnterFirmwareUpgradeMode()
    {
      this.WriteGenericCommandPacket(ReceiverCommands.EnterFirmwareUpgradeMode);
    }

    public byte[] ReadFlashPage(uint pageIndex)
    {
      this.WriteGenericCommandPacket(ReceiverCommands.ReadFlashPage, pageIndex);
      byte commandId = (byte) 0;
      byte[] payload = this.ReadGenericCommandPacket(this.CommandTimeout, out commandId);
      this.VerifyResponseCommandByte(commandId, payload);
      this.VerifyPayloadLength(payload.Length, 528);
      return payload;
    }

    public void WriteFlashPage(uint pageIndex, byte[] data)
    {
      byte[] numArray = new byte[532];
      PacketTools.StoreBytes(pageIndex, numArray, 0);
      Array.Copy((Array) data, 0, (Array) numArray, 4, 528);
      this.WriteGenericCommandPacket(ReceiverCommands.WriteFlashPage, numArray);
      byte commandId = (byte) 0;
      byte[] payload = this.ReadGenericCommandPacket(this.CommandTimeout, out commandId);
      this.VerifyResponseCommandByte(commandId, payload);
      this.VerifyPayloadLength(payload.Length, 0);
    }

    public void EnterSambaAccessMode()
    {
      this.WriteGenericCommandPacket(ReceiverCommands.EnterSambaAccessMode);
    }

    public XFirmwareSettings ReadFirmwareSettings()
    {
      if (this.CachedFirmwareHeader.ApiVersionNumber < 33619968U)
        throw new DexComException("Receiver Api version is not compatible with ReadFirmwareSettings() method.");
      this.WriteGenericCommandPacket(ReceiverCommands.ReadFirmwareSettings);
      byte commandId = (byte) 0;
      byte[] numArray = this.ReadGenericCommandPacket(this.CommandTimeout, out commandId);
      this.VerifyResponseCommandByte(commandId, numArray);
      string @string = Encoding.ASCII.GetString(numArray);
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.LoadXml(@string);
      return new XFirmwareSettings(xmlDocument.SelectSingleNode("FirmwareSettings") as XmlElement);
    }

    public XObject ReadLatestSettingsAsXml()
    {
      XObject xobject = new XObject("ReceiverSettings");
      DateTime dtValue = this.ReadSystemTime();
      TimeSpan tsValue = this.ReadDisplayTimeOffset();
      xobject.SetAttribute("SystemTime", dtValue);
      xobject.SetAttribute("DisplayTime", dtValue + tsValue);
      xobject.SetAttribute("DisplayTimeOffset", tsValue);
      xobject.SetAttribute("SystemTimeOffset", this.ReadSystemTimeOffset());
      xobject.SetAttribute("RealTimeClock", this.ReadRTC());
      xobject.SetAttribute("TransmitterId", this.ReadTransmitterId());
      xobject.SetAttribute("Language", ((object) this.ReadLanguage()).ToString());
      xobject.SetAttribute("GlucoseUnits", ((object) this.ReadGlucoseUnits()).ToString());
      xobject.SetAttribute("BlindMode", ((object) this.ReadBlindMode()).ToString());
      xobject.SetAttribute("ClockMode", ((object) this.ReadClockMode()).ToString());
      xobject.SetAttribute("DeviceMode", this.ReadDeviceMode());
      return xobject;
    }

    public string ReadReceiverSerialNumber()
    {
      string str = string.Empty;
      DatabasePageRange databasePageRange = this.ReadDatabasePageRange(ReceiverRecordType.ManufacturingData);
      if ((int) databasePageRange.LastPage != -1)
      {
        DatabasePage databasePage = this.ReadDatabasePage(ReceiverRecordType.ManufacturingData, databasePageRange.LastPage);
        str = new ManufacturingParameterRecord(databasePage.PageData, 0, (int) databasePage.PageHeader.Revision).Parameters.SerialNumber;
      }
      return str;
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
        UsbComPort portWithReceiver = ReceiverApi.FindPortWithReceiver();
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
      return ReceiverApi.FindPortWithReceiver(0);
    }

    public static UsbComPort FindPortWithReceiver(int startingPortNumber)
    {
      int portNumber1 = 0;
      if (startingPortNumber != 0 && ReceiverApi.IsReceiverOnPort(startingPortNumber))
        portNumber1 = startingPortNumber;
      if (portNumber1 == 0)
      {
        foreach (int portNumber2 in UsbComPort.LookupReceiverComPortList())
        {
          if (portNumber2 != startingPortNumber && ReceiverApi.IsReceiverOnPort(portNumber2))
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
          if (port != null)
          {
            using (ReceiverPort receiverPort = new ReceiverPort(port))
            {
              receiverPort.Open(ReceiverPort.BaudRate.Firmware);
              receiverPort.SetMediumTimeouts();
              try
              {
                PacketTools packetTools = new PacketTools();
                int length = packetTools.ComposePacket(ReceiverCommands.Ping);
                receiverPort.FlushPort();
                receiverPort.WriteBytes(packetTools.Packet, length);
                receiverPort.ReadBytes(6);
                flag = true;
              }
              catch
              {
                Thread.Sleep(50);
              }
              receiverPort.Close();
            }
          }
        }
      }
      catch (Exception ex)
      {
      }
      return flag;
    }

    public static bool WaitForReceiverOnPort(int portNumber, TimeSpan timeToWait)
    {
      bool flag = false;
      try
      {
        Thread.Sleep(10000);
        DateTime dateTime = DateTime.Now + timeToWait;
        while (!flag)
        {
          if (DateTime.Now <= dateTime)
          {
            using (UsbComPort port = UsbComPort.LookupPort(portNumber))
            {
              if (port == null)
              {
                Thread.Sleep(200);
              }
              else
              {
                using (ReceiverPort receiverPort = new ReceiverPort(port))
                {
                  receiverPort.Open(ReceiverPort.BaudRate.Firmware);
                  receiverPort.SetShortTimeouts();
                  while (!flag)
                  {
                    if (DateTime.Now <= dateTime)
                    {
                      try
                      {
                        PacketTools packetTools = new PacketTools();
                        int length = packetTools.ComposePacket(ReceiverCommands.Ping);
                        receiverPort.FlushPort();
                        receiverPort.WriteBytes(packetTools.Packet, length);
                        receiverPort.ReadBytes(6);
                        flag = true;
                      }
                      catch
                      {
                        Thread.Sleep(50);
                      }
                    }
                    else
                      break;
                  }
                  receiverPort.Close();
                }
              }
            }
          }
          else
            break;
        }
      }
      catch (Exception ex)
      {
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

    public void Connect()
    {
      bool isReceiverAttached = false;
      if (this.Connect(out isReceiverAttached))
        return;
      if (isReceiverAttached)
        throw new DexComException(ProgramContext.TryResourceLookup("Exception_FailedToOpenConnection", "Failed to open connection to the attached receiver.", new object[0]));
      else
        throw new DexComException(ProgramContext.TryResourceLookup("Exception_FailedToFindReceiver", "Failed to find a receiver or receiver not responding.", new object[0]));
    }

    public bool Connect(out bool isReceiverAttached)
    {
      this.m_cachedFirmwareHeader = (XFirmwareHeader) null;
      bool flag = false;
      isReceiverAttached = this.AttachReceiver();
      if (isReceiverAttached && this.m_port.IsOpen)
      {
        this.m_port.Close();
        Thread.Sleep(300);
      }
      isReceiverAttached = this.AttachReceiver();
      if (isReceiverAttached)
      {
        try
        {
          if (this.m_port.Open(ReceiverPort.BaudRate.Firmware))
          {
            this.m_port.SetNormalTimeouts();
            flag = true;
          }
        }
        catch (Win32Exception ex)
        {
          if ((long) ex.NativeErrorCode == 5L)
            throw new DexComException(ProgramContext.TryResourceLookup("Exception_ComPortInUseByAnotherApp", "Communication port appears to be in use by another application.", new object[0]), (Exception) ex);
          if ((long) ex.NativeErrorCode == 2L)
            throw new DexComException(ProgramContext.TryResourceLookup("Exception_CommunicationPortNotFound", "Communication port was not found.", new object[0]), (Exception) ex);
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

    public void VerifyApiVersion()
    {
      XFirmwareHeader cachedFirmwareHeader = this.CachedFirmwareHeader;
      if (cachedFirmwareHeader.ApiVersion == "2.0.0.1" || cachedFirmwareHeader.ApiVersion == "2.1.0.0" || (cachedFirmwareHeader.ApiVersion == "2.1.0.1" || cachedFirmwareHeader.ApiVersion == "2.2.0.0") || (int) cachedFirmwareHeader.ApiVersionNumber == (int) ReceiverApi.ImplementedApiVersion)
        return;
      byte major1;
      byte minor1;
      byte revision1;
      byte build1;
      CommonTools.UnpackRevisionNumber(ReceiverApi.ImplementedApiVersion, out major1, out minor1, out revision1, out build1);
      byte major2;
      byte minor2;
      byte revision2;
      byte build2;
      CommonTools.UnpackRevisionNumber(cachedFirmwareHeader.ApiVersionNumber, out major2, out minor2, out revision2, out build2);
      if ((int) major2 != (int) major1)
        throw new DexComException(ProgramContext.TryResourceLookup("CommonMessage_ReceiverNotCompatibleWithApplication", "Attached Receiver not compatible with current API.", new object[0]));
      if ((int) minor2 < (int) minor1)
        throw new DexComException(ProgramContext.TryResourceLookup("CommonMessage_ReceiverNotCompatibleWithApplication", "Attached Receiver not compatible with current API.", new object[0]));
    }

    public void VerifyTestApiVersion()
    {
      XFirmwareHeader cachedFirmwareHeader = this.CachedFirmwareHeader;
      if (cachedFirmwareHeader.TestApiVersion == "1.0.0.0" || cachedFirmwareHeader.TestApiVersion == "2.0.0.0" || (cachedFirmwareHeader.TestApiVersion == "2.1.0.0" || cachedFirmwareHeader.TestApiVersion == "2.2.0.0") || (cachedFirmwareHeader.TestApiVersion == "2.3.0.0" || cachedFirmwareHeader.TestApiVersion == "2.4.0.0" || (int) cachedFirmwareHeader.TestApiVersionNumber == (int) ReceiverApi.ImplementedTestApiVersion))
        return;
      byte major1;
      byte minor1;
      byte revision1;
      byte build1;
      CommonTools.UnpackRevisionNumber(ReceiverApi.ImplementedTestApiVersion, out major1, out minor1, out revision1, out build1);
      byte major2;
      byte minor2;
      byte revision2;
      byte build2;
      CommonTools.UnpackRevisionNumber(cachedFirmwareHeader.TestApiVersionNumber, out major2, out minor2, out revision2, out build2);
      if ((int) major2 != (int) major1)
        throw new DexComException("Attached Receiver not compatible with current Test API.");
      if ((int) minor2 < (int) minor1)
        throw new DexComException("Attached Receiver not compatible with current Test API.");
    }

    public void VerifyPayloadLength(int actual, int expected)
    {
      if (actual != expected)
        throw new DexComException(string.Format("Receiver packet response length of {0} != expected length of {1}", (object) actual, (object) expected));
    }

    public void VerifyResponseCommandByte(byte commandByte)
    {
      this.VerifyResponseCommandByte(commandByte, (byte[]) null);
    }

    public void VerifyResponseCommandByte(byte commandByte, byte[] payload)
    {
      ReceiverCommands receiverCommandFromByte = ReceiverTools.GetReceiverCommandFromByte(commandByte);
      switch (receiverCommandFromByte)
      {
        case ReceiverCommands.Ack:
          break;
        case ReceiverCommands.Nak:
          throw new DexComException("Receiver reported NAK or an invalid CRC error.");
        case ReceiverCommands.InvalidCommand:
          throw new DexComException("Receiver reported an invalid command error.");
        case ReceiverCommands.InvalidParam:
          if (payload != null && payload.Length >= 1)
            throw new DexComException(string.Format("Receiver reported an invalid parameter error for parameter {0}.", (object) payload[0].ToString()));
          else
            throw new DexComException("Receiver reported an invalid parameter error.");
        case ReceiverCommands.ReceiverError:
          throw new DexComException("Receiver reported an internal error.");
        default:
          throw new DexComException(string.Format("Unknown or invalid receiver command {0}={1}.", (object) commandByte.ToString(), (object) receiverCommandFromByte));
      }
    }

    public void WriteGenericCommandPacket(ReceiverCommands command)
    {
      PacketTools packetTools = new PacketTools();
      packetTools.ComposePacket(command);
      this.WriteGenericCommandPacket(packetTools.Packet, (int) packetTools.PacketLength);
    }

    public void WriteGenericCommandPacket(ReceiverCommands command, byte payload)
    {
      PacketTools packetTools = new PacketTools();
      packetTools.ComposePacket(command, payload);
      this.WriteGenericCommandPacket(packetTools.Packet, (int) packetTools.PacketLength);
    }

    public void WriteGenericCommandPacket(ReceiverCommands command, short payload)
    {
      PacketTools packetTools = new PacketTools();
      packetTools.ComposePacket(command, payload);
      this.WriteGenericCommandPacket(packetTools.Packet, (int) packetTools.PacketLength);
    }

    public void WriteGenericCommandPacket(ReceiverCommands command, ushort payload)
    {
      PacketTools packetTools = new PacketTools();
      packetTools.ComposePacket(command, payload);
      this.WriteGenericCommandPacket(packetTools.Packet, (int) packetTools.PacketLength);
    }

    public void WriteGenericCommandPacket(ReceiverCommands command, int payload)
    {
      PacketTools packetTools = new PacketTools();
      packetTools.ComposePacket(command, payload);
      this.WriteGenericCommandPacket(packetTools.Packet, (int) packetTools.PacketLength);
    }

    public void WriteGenericCommandPacket(ReceiverCommands command, uint payload)
    {
      PacketTools packetTools = new PacketTools();
      packetTools.ComposePacket(command, payload);
      this.WriteGenericCommandPacket(packetTools.Packet, (int) packetTools.PacketLength);
    }

    public void WriteGenericCommandPacket(ReceiverCommands command, long payload)
    {
      PacketTools packetTools = new PacketTools();
      packetTools.ComposePacket(command, payload);
      this.WriteGenericCommandPacket(packetTools.Packet, (int) packetTools.PacketLength);
    }

    public void WriteGenericCommandPacket(ReceiverCommands command, ulong payload)
    {
      PacketTools packetTools = new PacketTools();
      packetTools.ComposePacket(command, payload);
      this.WriteGenericCommandPacket(packetTools.Packet, (int) packetTools.PacketLength);
    }

    public void WriteGenericCommandPacket(ReceiverCommands command, byte payload1, uint payload2)
    {
      PacketTools packetTools = new PacketTools();
      packetTools.ComposePacket(command, payload1, payload2);
      this.WriteGenericCommandPacket(packetTools.Packet, (int) packetTools.PacketLength);
    }

    public void WriteGenericCommandPacket(ReceiverCommands command, byte payload1, uint payload2, byte payload3)
    {
      PacketTools packetTools = new PacketTools();
      packetTools.ComposePacket(command, payload1, payload2, payload3);
      this.WriteGenericCommandPacket(packetTools.Packet, (int) packetTools.PacketLength);
    }

    public void WriteGenericCommandPacket(ReceiverCommands command, byte[] payload)
    {
      if (payload != null && payload.Length > 1584)
        throw new ArgumentException("Invalid payload size. Data is limited to " + (ushort) 1584.ToString());
      PacketTools packetTools = new PacketTools();
      if (payload == null)
        packetTools.ComposePacket(command);
      else
        packetTools.ComposePacket(command, payload, payload.Length);
      this.WriteGenericCommandPacket(packetTools.Packet, (int) packetTools.PacketLength);
    }

    public void WriteGenericCommandPacket(byte[] packet, int packetLength)
    {
      if (packet == null)
        throw new ArgumentNullException("packet");
      if (packetLength < 6 || packetLength > 1590)
        throw new ArgumentException("Invalid packet length.");
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new DexComException("Invalid port or port closed!");
      this.m_port.FlushPort();
      this.m_port.WriteBytes(packet, packetLength);
    }

    public byte[] ReadGenericCommandPacket(TimeSpan maxWait)
    {
      byte commandId;
      return this.ReadGenericCommandPacket(maxWait, out commandId);
    }

    public byte[] ReadGenericCommandPacket(TimeSpan maxWait, out byte commandId)
    {
      if (this.m_port == null || !this.m_port.IsOpen)
        throw new DexComException("Invalid port or port closed!");
      byte[] numArray1 = (byte[]) null;
      byte[] buf = new byte[65541];
      int num1 = 0;
      commandId = (byte) 0;
      DateTime now = DateTime.Now;
      DexComException dexComException = (DexComException) null;
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
              commandId = numArray2[3];
              ushort num3 = BitConverter.ToUInt16(numArray2, 1);
              if ((int) num3 > 6)
              {
                ushort num4 = (ushort) ((uint) num3 - 6U);
                numArray1 = new byte[(int) num4];
                byte[] numArray3 = this.m_port.ReadBytes((int) num4);
                Array.Copy((Array) numArray3, 0, (Array) buf, num2, (int) num4);
                Array.Copy((Array) numArray3, 0, (Array) numArray1, 0, (int) num4);
                num2 += (int) num4;
              }
              else
                numArray1 = new byte[0];
              byte[] numArray4 = this.m_port.ReadBytes(2);
              ushort num5 = BitConverter.ToUInt16(numArray4, 0);
              ushort num6 = Crc.CalculateCrc16(buf, 0, num2);
              Array.Copy((Array) numArray4, 0, (Array) buf, num2, 2);
              num1 = num2 + 2;
              dexComException = (int) num5 == (int) num6 ? (DexComException) null : new DexComException("Failed CRC check in packet.");
              break;
            }
            else
            {
              dexComException = new DexComException("Unknown data read.  Failed to read start of packet.");
              break;
            }
          }
          catch (DexComException ex)
          {
            dexComException = new DexComException(ProgramContext.TryResourceLookup("Exception_FailedReadingPacket", "Failed to read contents of generic packets.", new object[0]), (Exception) ex);
            break;
          }
        }
        catch (TimedOutException ex)
        {
          dexComException = new DexComException(ProgramContext.TryResourceLookup("Exception_TimedOutWaitingForReceiverResponse", "Timed out waiting for response from receiver.", new object[0]), (Exception) ex);
        }
      }
      if (dexComException != null)
        throw dexComException;
      else
        return numArray1;
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
  }
}

// Type: Dexcom.R2Receiver.R2ReceiverTools
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using Dexcom.Common;
using Dexcom.Common.Data;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Dexcom.R2Receiver
{
  public abstract class R2ReceiverTools
  {
    public static ushort Pack32BitsInto16Bits(uint inputValue, bool allowPrecisionLoss)
    {
      uint num1 = 1048448U;
      if (allowPrecisionLoss)
        num1 = 1048575U;
      uint num2 = inputValue;
      if (num2 > num1)
        throw new ApplicationException("Unable to pack value into 16 bit number.  Input value exceeds maximum.");
      uint num3 = 0U;
      while (num2 > 8191U)
      {
        if (!allowPrecisionLoss && ((int) num2 & 1) != 0)
          throw new ApplicationException("Unable to pack value into 16 bit number.  Input value would lose precision.");
        num2 >>= 1;
        ++num3;
      }
      if (num3 > 7U)
        throw new ApplicationException("Unable to pack value into 16 bit number.  Exponent would be larger than 7.");
      uint num4 = num3 << 13;
      return Convert.ToUInt16(num2 | num4);
    }

    public static System.Type GetObjectTypeFromRecordType(R2RecordType recordType)
    {
      System.Type type = (System.Type) null;
      switch (recordType)
      {
        case R2RecordType.Reserved0x02:
          type = typeof (R2Reserved0x02Record);
          break;
        case R2RecordType.Log:
          type = typeof (R2LogRecord);
          break;
        case R2RecordType.Meter:
          type = typeof (R2MeterRecord);
          break;
        case R2RecordType.Sensor:
          type = typeof (R2SensorRecord);
          break;
        case R2RecordType.MatchedPair:
          type = typeof (R2MatchedPairRecord);
          break;
        case R2RecordType.Settings:
          type = typeof (R2SettingsRecord);
          break;
        case R2RecordType.STS_Calibration1:
          type = typeof (R2STSCalibration1Record);
          break;
        case R2RecordType.Sensor2:
          type = typeof (R2Sensor2Record);
          break;
        case R2RecordType.Settings2:
          type = typeof (R2Settings2Record);
          break;
        case R2RecordType.STS_Calibration2:
          type = typeof (R2STSCalibration2Record);
          break;
        case R2RecordType.Reserved0x0E:
          type = typeof (R2Reserved0x0ERecord);
          break;
        case R2RecordType.Reserved0x0F:
          type = typeof (R2Reserved0x0FRecord);
          break;
        case R2RecordType.STS_Calibration3:
          type = typeof (R2STSCalibration3Record);
          break;
        case R2RecordType.Meter2:
          type = typeof (R2Meter2Record);
          break;
        case R2RecordType.STS_Calibration4:
          type = typeof (R2STSCalibration4Record);
          break;
        case R2RecordType.Settings3:
          type = typeof (R2Settings3Record);
          break;
        case R2RecordType.Event:
          type = typeof (R2EventRecord);
          break;
        case R2RecordType.Meter3:
          type = typeof (R2Meter2Record);
          break;
        case R2RecordType.Sensor3:
          type = typeof (R2Sensor2Record);
          break;
        case R2RecordType.STS_Calibration5:
          type = typeof (R2STSCalibration5Record);
          break;
        case R2RecordType.Settings4:
          type = typeof (R2Settings4Record);
          break;
        case R2RecordType.Settings5:
          type = typeof (R2Settings5Record);
          break;
        case R2RecordType.Settings6:
          type = typeof (R2Settings6Record);
          break;
        case R2RecordType.ErrorLog:
          type = typeof (R2ErrorLogRecord);
          break;
        case R2RecordType.ErrorLog2:
          type = typeof (R2ErrorLog2Record);
          break;
      }
      return type;
    }

    public static int GetRecordSize(R2RecordType recordType)
    {
      switch (recordType)
      {
        case R2RecordType.Reserved0x02:
          return Marshal.SizeOf(typeof (R2Reserved0x02Record));
        case R2RecordType.Log:
          return Marshal.SizeOf(typeof (R2LogRecord));
        case R2RecordType.Meter:
          return Marshal.SizeOf(typeof (R2MeterRecord));
        case R2RecordType.Sensor:
          return Marshal.SizeOf(typeof (R2SensorRecord));
        case R2RecordType.MatchedPair:
          return Marshal.SizeOf(typeof (R2MatchedPairRecord));
        case R2RecordType.Settings:
          return Marshal.SizeOf(typeof (R2SettingsRecord));
        case R2RecordType.STS_Calibration1:
          return Marshal.SizeOf(typeof (R2STSCalibration1Record));
        case R2RecordType.Sensor2:
          return Marshal.SizeOf(typeof (R2Sensor2Record));
        case R2RecordType.Settings2:
          return Marshal.SizeOf(typeof (R2Settings2Record));
        case R2RecordType.STS_Calibration2:
          return Marshal.SizeOf(typeof (R2STSCalibration2Record));
        case R2RecordType.Reserved0x0E:
          return Marshal.SizeOf(typeof (R2Reserved0x0ERecord));
        case R2RecordType.Reserved0x0F:
          return Marshal.SizeOf(typeof (R2Reserved0x0FRecord));
        case R2RecordType.STS_Calibration3:
          return Marshal.SizeOf(typeof (R2STSCalibration3Record));
        case R2RecordType.Meter2:
          return Marshal.SizeOf(typeof (R2Meter2Record));
        case R2RecordType.STS_Calibration4:
          return Marshal.SizeOf(typeof (R2STSCalibration4Record));
        case R2RecordType.Settings3:
          return Marshal.SizeOf(typeof (R2Settings3Record));
        case R2RecordType.Event:
          return Marshal.SizeOf(typeof (R2EventRecord));
        case R2RecordType.Meter3:
          return Marshal.SizeOf(typeof (R2Meter2Record));
        case R2RecordType.Sensor3:
          return Marshal.SizeOf(typeof (R2Sensor2Record));
        case R2RecordType.STS_Calibration5:
          return Marshal.SizeOf(typeof (R2STSCalibration5Record));
        case R2RecordType.Settings4:
          return Marshal.SizeOf(typeof (R2Settings4Record));
        case R2RecordType.Settings5:
          return Marshal.SizeOf(typeof (R2Settings5Record));
        case R2RecordType.Settings6:
          return Marshal.SizeOf(typeof (R2Settings6Record));
        case R2RecordType.ErrorLog:
          return Marshal.SizeOf(typeof (R2ErrorLogRecord));
        case R2RecordType.ErrorLog2:
          return Marshal.SizeOf(typeof (R2ErrorLog2Record));
        default:
          throw new ApplicationException("Unknown record type. Size of record can not be calculated.");
      }
    }

    public static object DatabaseRecordFactory(R2RecordType recordType, byte[] data, int offset)
    {
      System.Type typeFromRecordType = R2ReceiverTools.GetObjectTypeFromRecordType(recordType);
      if (!(typeFromRecordType != (System.Type) null))
        throw new ApplicationException(string.Format("Unknown record type {0:X}. Offset = 0x{1:X}", (object) recordType, (object) offset));
      object obj = DataTools.ConvertBytesToObject(data, offset, typeFromRecordType);
      IGenericR2Record genericR2Record = obj as IGenericR2Record;
      if (genericR2Record == null)
        throw new ApplicationException(string.Format("Could not create record from data. Offset = 0x{0:X}", (object) offset));
      if (!genericR2Record.IsMatchingHeaderFooterType)
        throw new ApplicationException(string.Format("Failed to match record type in header/footer! {0} record, Offset = 0x{1:X}", (object) typeFromRecordType.Name, (object) offset));
      if (genericR2Record.RecordType != R2RecordType.ErrorLog && (int) Crc.CalculateCrc16(data, offset, offset + Marshal.OffsetOf(typeFromRecordType, "m_footer").ToInt32()) != (int) genericR2Record.RecordedCrc)
        throw new ApplicationException(string.Format("Bad CRC in record {0}, Offset = 0x{1:X}", (object) typeFromRecordType.Name, (object) offset));
      else
        return obj;
    }

    public static XmlDocument LoadDownloadFromFile(string strFilePath)
    {
      string str = strFilePath.Trim();
      if (str.Length <= 0)
        throw new ApplicationException("File path argument is empty!");
      FileInfo fileInfo = new FileInfo(str);
      if (!fileInfo.Exists)
        throw new ApplicationException("File not found!");
      int count = Convert.ToInt32(fileInfo.Length);
      byte[] buffer = new byte[count];
      int num = 0;
      using (FileStream fileStream = File.OpenRead(str))
        num = fileStream.Read(buffer, 0, count);
      if (num != buffer.Length)
        throw new ApplicationException("Failed to read contents of file.");
      XmlDocument xDocument = new XmlDocument();
      xDocument.PreserveWhitespace = true;
      try
      {
        using (MemoryStream memoryStream = new MemoryStream(buffer))
          xDocument.Load((Stream) memoryStream);
      }
      catch (XmlException ex)
      {
        throw new ApplicationException("Failed to load R2 Download file/data. XML error: " + ex.Message);
      }
      if (!CommonTools.VerifyDocumentSignature(xDocument))
        throw new ApplicationException("Contents of R2 Download file have been modified. Signature verification failed!");
      else
        return xDocument;
    }

    public static XmlDocument LoadDownloadFromData(byte[] downloadData)
    {
      XmlDocument xDocument = new XmlDocument();
      xDocument.PreserveWhitespace = true;
      try
      {
        using (MemoryStream memoryStream = new MemoryStream(downloadData))
          xDocument.Load((Stream) memoryStream);
      }
      catch (XmlException ex)
      {
        throw new ApplicationException("Failed to load R2 Download file/data. XML error: " + ex.Message);
      }
      if (!CommonTools.VerifyDocumentSignature(xDocument))
        throw new ApplicationException("Contents of R2 Download file have been modified. Signature verification failed!");
      else
        return xDocument;
    }

    public static byte[] ExtractErrorLogFromDownloadFile(string strFilePath)
    {
      return R2ReceiverTools.DoExtractReceiverDataFromDownloadFile(R2ReceiverTools.LoadDownloadFromFile(strFilePath), "ErrorLog");
    }

    public static byte[] ExtractErrorLogFromDownloadFile(XmlDocument xDownload)
    {
      return R2ReceiverTools.DoExtractReceiverDataFromDownloadFile(xDownload, "ErrorLog");
    }

    public static byte[] ExtractEventLogFromDownloadFile(string strFilePath)
    {
      return R2ReceiverTools.DoExtractReceiverDataFromDownloadFile(R2ReceiverTools.LoadDownloadFromFile(strFilePath), "EventLog");
    }

    public static HardwareConfiguration ExtractHardwareConfigurationFromDownloadFile(string filePath)
    {
      return R2ReceiverTools.ExtractHardwareConfigurationFromDownloadFile(R2ReceiverTools.LoadDownloadFromFile(filePath));
    }

    public static HardwareConfiguration ExtractHardwareConfigurationFromDownloadFile(XmlDocument xDownload)
    {
      return xDownload.SelectSingleNode("/ReceiverDownload/ReceiverData/HardwareConfigData") == null ? new HardwareConfiguration(new XR2HardwareConfig((XmlElement) xDownload.SelectSingleNode("/ReceiverDownload/ReceiverData/HardwareConfig"))) : R2ReceiverTools.CreateHardwareConfigurationFromBinary(R2ReceiverTools.DoExtractReceiverDataFromDownloadFile(xDownload, "HardwareConfigData"));
    }

    public static XmlElement ExtractFirmwareHeaderFromDownloadFile(XmlDocument xDownload)
    {
      return (XmlElement) xDownload.SelectSingleNode("/ReceiverDownload/ReceiverData/FirmwareHeader");
    }

    public static byte[] ExtractEventLogFromDownloadFile(XmlDocument xDownload)
    {
      return R2ReceiverTools.DoExtractReceiverDataFromDownloadFile(xDownload, "EventLog");
    }

    public static byte[] ExtractDatabaseRecordsFromDownloadFile(string strFilePath)
    {
      return R2ReceiverTools.DoExtractReceiverDataFromDownloadFile(R2ReceiverTools.LoadDownloadFromFile(strFilePath), "DatabaseRecords");
    }

    public static byte[] ExtractDatabaseRecordsFromDownloadFile(XmlDocument xDownload)
    {
      return R2ReceiverTools.DoExtractReceiverDataFromDownloadFile(xDownload, "DatabaseRecords");
    }

    public static HardwareConfiguration CreateHardwareConfigurationFromBinary(byte[] response)
    {
      if (response.Length == 128)
      {
        R2HWConfig r2HWConfig = (R2HWConfig) DataTools.ConvertBytesToObject(response, 0, typeof (R2HWConfig));
        if (!r2HWConfig.IsValidCrc())
          throw new ApplicationException("HardwareConfiguration (V1) does not have a valid CRC!");
        else
          return new HardwareConfiguration(r2HWConfig);
      }
      else
      {
        if (response.Length != 1024)
          throw new ApplicationException("Binary data is not a known size for a HardwareConfiguration.  Data may be an invalid or incompatible configuration.");
        R2HWConfigHeader r2HwConfigHeader = (R2HWConfigHeader) DataTools.ConvertBytesToObject(response, 0, typeof (R2HWConfigHeader));
        if ((int) r2HwConfigHeader.m_configVersion == 2)
        {
          R2HWConfigV2 r2HWConfig = (R2HWConfigV2) DataTools.ConvertBytesToObject(response, 0, typeof (R2HWConfigV2));
          if (!r2HWConfig.IsValidCrc())
            throw new ApplicationException("HardwareConfiguration (V2) does not have a valid CRC!");
          else
            return new HardwareConfiguration(r2HWConfig);
        }
        else if ((int) r2HwConfigHeader.m_configVersion == 3)
        {
          R2HWConfigV3 r2HWConfig = (R2HWConfigV3) DataTools.ConvertBytesToObject(response, 0, typeof (R2HWConfigV3));
          if (!r2HWConfig.IsValidCrc())
            throw new ApplicationException("HardwareConfiguration (V3) does not have a valid CRC!");
          else
            return new HardwareConfiguration(r2HWConfig);
        }
        else if ((int) r2HwConfigHeader.m_configVersion == 4)
        {
          R2HWConfigV4 r2HWConfig = (R2HWConfigV4) DataTools.ConvertBytesToObject(response, 0, typeof (R2HWConfigV4));
          if (!r2HWConfig.IsValidCrc())
            throw new ApplicationException("HardwareConfiguration (V4) does not have a valid CRC!");
          else
            return new HardwareConfiguration(r2HWConfig);
        }
        else if ((int) r2HwConfigHeader.m_configVersion == 5)
        {
          R2HWConfigV5 r2HWConfig = (R2HWConfigV5) DataTools.ConvertBytesToObject(response, 0, typeof (R2HWConfigV5));
          if (!r2HWConfig.IsValidCrc())
            throw new ApplicationException("HardwareConfiguration (V5) does not have a valid CRC!");
          else
            return new HardwareConfiguration(r2HWConfig);
        }
        else if ((int) r2HwConfigHeader.m_configVersion == 6)
        {
          R2HWConfigV6 r2HWConfig = (R2HWConfigV6) DataTools.ConvertBytesToObject(response, 0, typeof (R2HWConfigV6));
          if (!r2HWConfig.IsValidCrc())
            throw new ApplicationException("HardwareConfiguration (V6) does not have a valid CRC!");
          else
            return new HardwareConfiguration(r2HWConfig);
        }
        else
        {
          if ((int) r2HwConfigHeader.m_configVersion != 7)
            throw new ApplicationException("Binary data is not a known version for a HardwareConfiguration.  Data may be an invalid or incompatible configuration.");
          R2HWConfigV7 r2HWConfig = (R2HWConfigV7) DataTools.ConvertBytesToObject(response, 0, typeof (R2HWConfigV7));
          if (!r2HWConfig.IsValidCrc())
            throw new ApplicationException("HardwareConfiguration (V7) does not have a valid CRC!");
          else
            return new HardwareConfiguration(r2HWConfig);
        }
      }
    }

    public static string ConvertReceiverNumberToSerialNumberString(uint receiverNumber)
    {
      return string.Format("{0:000000}", (object) receiverNumber);
    }

    public static string GetUniqueReceiverIdentifierFromDownloadFile(XmlDocument xDownload)
    {
      string str = (string) null;
      XObject xobject = new XObject((XmlElement) xDownload.SelectSingleNode("/ReceiverDownload/ReceiverData/HardwareConfig"));
      if (xobject.IsNotNull())
      {
        uint receiverNumber = 0U;
        Guid guid = Guid.Empty;
        if (xobject.HasAttribute("ReceiverId"))
          receiverNumber = xobject.GetAttribute<uint>("ReceiverId");
        if (xobject.HasAttribute("ReceiverInstanceId"))
          guid = xobject.GetAttribute<Guid>("ReceiverInstanceId");
        str = string.Format("{0}_{1}", (object) R2ReceiverTools.ConvertReceiverNumberToSerialNumberString(receiverNumber), (object) CommonTools.ConvertToString(guid));
      }
      return str;
    }

    public static void GetUniqueReceiverIdentifierFromDownloadFile(XmlDocument xDownload, out uint receiverNumber, out Guid receiverInstance)
    {
      receiverNumber = 0U;
      receiverInstance = Guid.Empty;
      XObject xobject = new XObject((XmlElement) xDownload.SelectSingleNode("/ReceiverDownload/ReceiverData/HardwareConfig"));
      if (!xobject.IsNotNull())
        return;
      uint num = 0U;
      Guid guid = Guid.Empty;
      if (xobject.HasAttribute("ReceiverId"))
        num = xobject.GetAttribute<uint>("ReceiverId");
      if (xobject.HasAttribute("ReceiverInstanceId"))
        guid = xobject.GetAttribute<Guid>("ReceiverInstanceId");
      receiverNumber = num;
      receiverInstance = guid;
    }

    public static XmlDocument MergeDownloadIntoReceiver(XmlDocument xDownload, XmlDocument xReceiver)
    {
      XmlDocument xmlDocument;
      if (xReceiver != null)
      {
        Trace.Assert(xDownload != null);
        XObject xobject1 = new XObject((XmlElement) xDownload.SelectSingleNode("/ReceiverDownload"));
        XObject xobject2 = new XObject((XmlElement) xReceiver.SelectSingleNode("/ReceiverDownload"));
        XObject xobject3 = new XObject((XmlElement) xReceiver.SelectSingleNode("/ReceiverDownload/ReceiverData/DownloadHistory"));
        string fromDownloadFile1 = R2ReceiverTools.GetUniqueReceiverIdentifierFromDownloadFile(xDownload);
        string fromDownloadFile2 = R2ReceiverTools.GetUniqueReceiverIdentifierFromDownloadFile(xReceiver);
        if (xobject1.Id == xobject2.Id)
          throw new ApplicationException("Failed to merge download with receiver: download and receiver have the same download guid.");
        if (xobject1.HasAttribute("IsMergedReceiver"))
          throw new ApplicationException("Failed to merge download with receiver: download appears to be a receiver.");
        if (!xobject2.HasAttribute("IsMergedReceiver"))
          throw new ApplicationException("Failed to merge download with receiver: receiver appears to be a download.");
        if (xobject3.IsNull())
          throw new ApplicationException("Failed to merge download with receiver: receiver does not have download history element.");
        if (fromDownloadFile1 != fromDownloadFile2)
          throw new ApplicationException("Failed to merge download with receiver: download and receiver have different identifiers.");
        string xpath = string.Format("/ReceiverDownload/ReceiverData/DownloadHistory/Download[@Id='{0}']", (object) CommonTools.ConvertToString(xobject1.Id));
        if (new XObject((XmlElement) xReceiver.SelectSingleNode(xpath)).IsNotNull())
          return xReceiver;
        byte[] numArray = R2ReceiverTools.MergeDownloadDatabaseWithReceiverDatabase(R2ReceiverTools.ExtractDatabaseRecordsFromDownloadFile(xDownload), R2ReceiverTools.ExtractDatabaseRecordsFromDownloadFile(xReceiver));
        R2BlockInfoParser r2BlockInfoParser = new R2BlockInfoParser();
        r2BlockInfoParser.Parse(numArray);
        DateTime attribute1 = xobject1.GetAttribute<DateTime>("DateTimeNowUTC");
        DateTime attribute2 = xobject2.GetAttribute<DateTime>("DateTimeNowUTC");
        xmlDocument = new XmlDocument();
        if (attribute1 > attribute2)
        {
          XmlNode newChild = xmlDocument.ImportNode((XmlNode) xDownload.DocumentElement, true);
          xmlDocument.AppendChild(newChild);
        }
        else
        {
          XmlNode newChild = xmlDocument.ImportNode((XmlNode) xReceiver.DocumentElement, true);
          xmlDocument.AppendChild(newChild);
        }
        XObject xobject4 = new XObject((XmlElement) xmlDocument.SelectSingleNode("/ReceiverDownload"));
        xobject4.Id = xobject2.Id;
        xobject4.SetAttribute("IsMergedReceiver", true);
        xobject4.SetAttribute("DateTimeModified", DateTimeOffset.Now);
        xobject4.SetAttribute("MergedId", Guid.NewGuid());
        XmlNode oldChild1 = xmlDocument.SelectSingleNode("/ReceiverDownload/ReceiverData/DatabaseRecords");
        XmlNode oldChild2 = xmlDocument.SelectSingleNode("/ReceiverDownload/ReceiverData/DatabaseBlockInfo");
        XmlNode xmlNode = xmlDocument.SelectSingleNode("/ReceiverDownload/ReceiverData");
        XmlNode newChild1 = xmlDocument.SelectSingleNode("/ReceiverDownload/ReceiverData/DownloadHistory");
        if (newChild1 == null)
        {
          newChild1 = xmlDocument.ImportNode((XmlNode) xobject3.Element, true);
          xmlNode.AppendChild(newChild1);
        }
        XObject xobject5 = new XObject((XmlElement) newChild1);
        int intValue = xobject5.GetAttribute<int>("Count") + 1;
        xobject5.SetAttribute("Count", intValue);
        XObject xChildObject = new XObject("Download", xmlDocument);
        xChildObject.Id = xobject1.Id;
        xChildObject.SetAttribute("DateTimeNowLocal", xobject1.GetAttribute("DateTimeNowLocal"));
        xChildObject.SetAttribute("DateTimeNowUTC", xobject1.GetAttribute("DateTimeNowUTC"));
        xobject5.AppendChild(xChildObject);
        if (true)
        {
          XmlElement compressedBinaryElement = DataTools.CreateCompressedBinaryElement(numArray, "DatabaseRecords", xmlDocument);
          xmlNode.ReplaceChild((XmlNode) compressedBinaryElement, oldChild1);
        }
        else
        {
          XObject xobject6 = new XObject("DatabaseRecords", xmlDocument);
          xmlNode.ReplaceChild((XmlNode) xobject6.Element, oldChild1);
          xobject6.SetAttribute("Size", numArray.Length);
          xobject6.SetAttribute("CompressedSize", 0);
          xobject6.SetAttribute("IsCompressed", false);
          xobject6.Element.InnerText = Convert.ToBase64String(numArray);
        }
        XObject xobject7 = new XObject("DatabaseBlockInfo", xmlDocument);
        xmlNode.ReplaceChild((XmlNode) xobject7.Element, oldChild2);
        foreach (R2BlockInfo r2BlockInfo in r2BlockInfoParser.BlockInfoArray)
          xobject7.Element.AppendChild((XmlNode) r2BlockInfo.GetXml(xmlDocument));
        XmlNode oldChild3 = xobject4.Element.SelectSingleNode("ExpandedDatabase");
        if (oldChild3 != null)
          xobject4.Element.RemoveChild(oldChild3);
      }
      else
      {
        xmlDocument = new XmlDocument();
        XmlNode newChild = xmlDocument.ImportNode((XmlNode) xDownload.DocumentElement, true);
        xmlDocument.AppendChild(newChild);
        XObject xobject1 = new XObject((XmlElement) xmlDocument.SelectSingleNode("/ReceiverDownload"));
        Guid id = xobject1.Id;
        xobject1.Id = Guid.NewGuid();
        if (xobject1.HasAttribute("IsMergedReceiver"))
          throw new ApplicationException("Failed to merge download with receiver: download appears to be a receiver.");
        xobject1.SetAttribute("IsMergedReceiver", true);
        xobject1.SetAttribute("DateTimeModified", DateTimeOffset.Now);
        xobject1.SetAttribute("MergedId", Guid.NewGuid());
        XObject xobject2 = new XObject((XmlElement) xmlDocument.SelectSingleNode("/ReceiverDownload/ReceiverData"));
        XObject xChildObject1 = new XObject("DownloadHistory", xmlDocument);
        xChildObject1.SetAttribute("Count", 1);
        xobject2.AppendChild(xChildObject1);
        XObject xChildObject2 = new XObject("Download", xmlDocument);
        xChildObject2.Id = id;
        xChildObject2.SetAttribute("DateTimeNowLocal", xobject1.GetAttribute("DateTimeNowLocal"));
        xChildObject2.SetAttribute("DateTimeNowUTC", xobject1.GetAttribute("DateTimeNowUTC"));
        xChildObject1.AppendChild(xChildObject2);
        byte[] numArray = R2ReceiverTools.SortDatabaseBlocksByBlockNumber(R2ReceiverTools.ExtractDatabaseRecordsFromDownloadFile(xmlDocument));
        R2BlockInfoParser r2BlockInfoParser = new R2BlockInfoParser();
        r2BlockInfoParser.Parse(numArray);
        XmlNode oldChild1 = xmlDocument.SelectSingleNode("/ReceiverDownload/ReceiverData/DatabaseRecords");
        XmlNode oldChild2 = xmlDocument.SelectSingleNode("/ReceiverDownload/ReceiverData/DatabaseBlockInfo");
        XmlNode xmlNode = xmlDocument.SelectSingleNode("/ReceiverDownload/ReceiverData");
        if (true)
        {
          XmlElement compressedBinaryElement = DataTools.CreateCompressedBinaryElement(numArray, "DatabaseRecords", xmlDocument);
          xmlNode.ReplaceChild((XmlNode) compressedBinaryElement, oldChild1);
        }
        else
        {
          XObject xobject3 = new XObject("DatabaseRecords", xmlDocument);
          xmlNode.ReplaceChild((XmlNode) xobject3.Element, oldChild1);
          xobject3.SetAttribute("Size", numArray.Length);
          xobject3.SetAttribute("CompressedSize", 0);
          xobject3.SetAttribute("IsCompressed", false);
          xobject3.Element.InnerText = Convert.ToBase64String(numArray);
        }
        XObject xobject4 = new XObject("DatabaseBlockInfo", xmlDocument);
        xmlNode.ReplaceChild((XmlNode) xobject4.Element, oldChild2);
        foreach (R2BlockInfo r2BlockInfo in r2BlockInfoParser.BlockInfoArray)
          xobject4.Element.AppendChild((XmlNode) r2BlockInfo.GetXml(xmlDocument));
      }
      XmlDocument xDocument = new XmlDocument();
      xDocument.PreserveWhitespace = true;
      xDocument.LoadXml(CommonTools.FormatXml(xmlDocument.OuterXml));
      XmlDeclaration xmlDeclaration = xDocument.CreateXmlDeclaration("1.0", Encoding.UTF8.HeaderName, (string) null);
      xDocument.InsertBefore((XmlNode) xmlDeclaration, (XmlNode) xDocument.DocumentElement);
      CommonTools.ReplaceDocumentSignature(xDocument);
      return xDocument;
    }

    public static byte[] MergeDownloadDatabaseWithReceiverDatabase(byte[] downloadDatabase, byte[] receiverDatabase)
    {
      R2BlockInfoParser r2BlockInfoParser1 = new R2BlockInfoParser();
      r2BlockInfoParser1.Parse(downloadDatabase);
      R2BlockInfo[] blockInfoArray1 = r2BlockInfoParser1.BlockInfoArray;
      R2BlockInfoParser r2BlockInfoParser2 = new R2BlockInfoParser();
      r2BlockInfoParser2.Parse(receiverDatabase);
      R2BlockInfo[] blockInfoArray2 = r2BlockInfoParser2.BlockInfoArray;
      SortedList<uint, R2BlockInfo> sortedList = new SortedList<uint, R2BlockInfo>();
      foreach (R2BlockInfo r2BlockInfo in blockInfoArray2)
      {
        r2BlockInfo.Tag = (object) receiverDatabase;
        sortedList.Add(r2BlockInfo.Number, r2BlockInfo);
      }
      foreach (R2BlockInfo r2BlockInfo1 in blockInfoArray1)
      {
        r2BlockInfo1.Tag = (object) downloadDatabase;
        R2BlockInfo r2BlockInfo2 = (R2BlockInfo) null;
        if (sortedList.TryGetValue(r2BlockInfo1.Number, out r2BlockInfo2))
        {
          if ((int) r2BlockInfo1.Crc != (int) r2BlockInfo2.Crc && r2BlockInfo1.LastRecordTimeStampUtc > r2BlockInfo2.LastRecordTimeStampUtc)
            sortedList[r2BlockInfo1.Number] = r2BlockInfo1;
        }
        else
          sortedList.Add(r2BlockInfo1.Number, r2BlockInfo1);
      }
      byte[] databaseBlocks = new byte[sortedList.Count * R2ReceiverValues.BlockSize];
      int num = 0;
      foreach (KeyValuePair<uint, R2BlockInfo> keyValuePair in sortedList)
      {
        Array.Copy((Array) keyValuePair.Value.Tag, keyValuePair.Value.Offset, (Array) databaseBlocks, num * R2ReceiverValues.BlockSize, R2ReceiverValues.BlockSize);
        ++num;
      }
      return R2ReceiverTools.SortDatabaseBlocksByBlockNumber(databaseBlocks);
    }

    public static List<Guid> GetDownloadHistoryIdList(XmlDocument xReceiver)
    {
      List<Guid> list = new List<Guid>();
      foreach (XmlElement element in xReceiver.SelectNodes("/ReceiverDownload/ReceiverData/DownloadHistory/Download"))
      {
        XObject xobject = new XObject(element);
        list.Add(xobject.Id);
      }
      return list;
    }

    public static List<XObject> GetDownloadHistoryList(XmlDocument xReceiver)
    {
      List<XObject> list = new List<XObject>();
      foreach (XmlElement element in xReceiver.SelectNodes("/ReceiverDownload/ReceiverData/DownloadHistory/Download"))
      {
        XObject xobject = new XObject(element);
        list.Add(xobject);
      }
      return list;
    }

    public static XmlDocument ExpandReceiverDownload(XmlDocument xDownloadDoc)
    {
      XmlDocument xmlDocument = new XmlDocument();
      if (xDownloadDoc != null && xDownloadDoc.DocumentElement != null)
      {
        xmlDocument.PreserveWhitespace = true;
        xmlDocument.LoadXml(xDownloadDoc.OuterXml);
        R2DatabaseRecordsParser databaseRecordsParser = new R2DatabaseRecordsParser();
        byte[] fromDownloadFile1 = R2ReceiverTools.ExtractDatabaseRecordsFromDownloadFile(xDownloadDoc);
        byte[] fromDownloadFile2 = R2ReceiverTools.ExtractErrorLogFromDownloadFile(xDownloadDoc);
        databaseRecordsParser.Parse(fromDownloadFile1);
        databaseRecordsParser.ParseErrorLog(fromDownloadFile2);
        databaseRecordsParser.AdjustUserTimesOnRecordsBeforeFirstSettingsRecord();
        XObject xobject = new XObject("ExpandedDatabase", xmlDocument);
        xobject.SetAttribute("DateTimeCreated", DateTimeOffset.Now);
        xobject.AppendChild(R2ReceiverTools.ExpandRecords(Enumerable.Cast<IGenericRecord>((IEnumerable) databaseRecordsParser.DatabaseRecords.Sensor2Records), "Sensor2Records", xmlDocument));
        xobject.AppendChild(R2ReceiverTools.ExpandRecords(Enumerable.Cast<IGenericRecord>((IEnumerable) databaseRecordsParser.DatabaseRecords.SensorRecords), "SensorRecords", xmlDocument));
        xobject.AppendChild(R2ReceiverTools.ExpandRecords(Enumerable.Cast<IGenericRecord>((IEnumerable) databaseRecordsParser.DatabaseRecords.SettingsRecords), "SettingsRecords", xmlDocument));
        xobject.AppendChild(R2ReceiverTools.ExpandRecords(Enumerable.Cast<IGenericRecord>((IEnumerable) databaseRecordsParser.DatabaseRecords.MeterRecords), "MeterRecords", xmlDocument));
        xobject.AppendChild(R2ReceiverTools.ExpandRecords(Enumerable.Cast<IGenericRecord>((IEnumerable) databaseRecordsParser.DatabaseRecords.EventRecords), "EventRecords", xmlDocument));
        xobject.AppendChild(R2ReceiverTools.ExpandRecords(Enumerable.Cast<IGenericRecord>((IEnumerable) databaseRecordsParser.DatabaseRecords.LogRecords), "LogRecords", xmlDocument));
        xobject.AppendChild(R2ReceiverTools.ExpandRecords(Enumerable.Cast<IGenericRecord>((IEnumerable) databaseRecordsParser.DatabaseRecords.STSCalibrationRecords), "CalSetRecords", xmlDocument));
        xobject.AppendChild(R2ReceiverTools.ExpandRecords(Enumerable.Cast<IGenericRecord>((IEnumerable) databaseRecordsParser.DatabaseRecords.MatchedPairRecords), "MatchedPairRecords", xmlDocument));
        xobject.AppendChild(R2ReceiverTools.ExpandRecords(Enumerable.Cast<IGenericRecord>((IEnumerable) databaseRecordsParser.DatabaseRecords.ErrorLogRecords), "ErrorRecords", xmlDocument));
        XmlElement xmlElement = xmlDocument.DocumentElement.SelectSingleNode("ExpandedDatabase") as XmlElement;
        if (xmlElement == null)
          xmlDocument.DocumentElement.AppendChild((XmlNode) xobject.Element);
        else
          xmlDocument.DocumentElement.ReplaceChild((XmlNode) xobject.Element, (XmlNode) xmlElement);
      }
      return xmlDocument;
    }

    public static XObject ExpandRecords(IEnumerable<IGenericRecord> records, string sectionName, XmlDocument xOwnerDocument)
    {
      XObject xobject = new XObject(sectionName, xOwnerDocument);
      foreach (IGenericRecord genericRecord in records)
        xobject.Element.AppendChild((XmlNode) genericRecord.ToXml(xOwnerDocument));
      return xobject;
    }

    private static byte[] DoExtractReceiverDataFromDownloadFile(XmlDocument xDownload, string sectionName)
    {
      XObject xobject = new XObject((XmlElement) xDownload.SelectSingleNode("/ReceiverDownload/ReceiverData/" + sectionName));
      if (!xobject.IsNotNull())
        throw new ApplicationException("Name of section (element) not found in ReceiverData.");
      if (DataTools.IsCompressedElement(xobject.Element))
        return DataTools.DecompressElement(xobject.Element);
      else
        return Convert.FromBase64String(xobject.Element.InnerText);
    }

    public static ulong ConvertDateTimeToReceiverTime(DateTime pcDateTime)
    {
      return (ulong) (pcDateTime - R2ReceiverValues.R2BaseDateTime).TotalMilliseconds;
    }

    public static string RevisionToString(uint revision)
    {
      return CommonTools.RevisionToString(revision);
    }

    public static uint StringToRevision(string revision)
    {
      return CommonTools.StringToRevision(revision);
    }

    public static uint ConvertGtxTransmitterCodeToNumber(string code)
    {
      if (code == null)
        throw new ArgumentNullException("code");
      if (code.Length != 5)
        throw new ArgumentException("Transmitter code must be 5 alpha-numeric characters!");
      code = code.ToUpperInvariant();
      if (code.IndexOfAny(new char[4]
      {
        'I',
        'O',
        'V',
        'Z'
      }) >= 0)
        throw new ArgumentOutOfRangeException("code", (object) code, "Transmitter code must not contain the letters 'I', 'O', 'V' or 'Z'");
      StringBuilder stringBuilder = new StringBuilder(code);
      uint num1 = 0U;
      int num2 = 0;
      while (stringBuilder.Length > 0)
      {
        int num3 = R2ReceiverValues.TransmitterIdValidChars.IndexOf(stringBuilder[stringBuilder.Length - 1]);
        Trace.Assert(num3 >= 0, "Transmitter code contains an invalid character not previously detected!");
        if (num2 == 4 && num3 <= 15)
          throw new ArgumentException("Transmitter code must start with 'G' or greater!");
        num1 += (uint) (num3 << 5 * num2);
        ++num2;
        stringBuilder.Remove(stringBuilder.Length - 1, 1);
      }
      return num1 & 16777215U;
    }

    public static uint ConvertGen3TransmitterCodeToNumber(string code)
    {
      if (code == null)
        throw new ArgumentNullException("code");
      if (code.Length != 5)
        throw new ArgumentException("Transmitter code must be 5 alpha-numeric characters!");
      code = code.ToUpperInvariant();
      if (code.IndexOfAny(new char[4]
      {
        'I',
        'O',
        'V',
        'Z'
      }) >= 0)
        throw new ArgumentOutOfRangeException("code", (object) code, "Transmitter code must not contain the letters 'I', 'O', 'V' or 'Z'");
      StringBuilder stringBuilder = new StringBuilder(code);
      uint num1 = 0U;
      int num2 = 0;
      while (stringBuilder.Length > 0)
      {
        int num3 = R2ReceiverValues.TransmitterIdValidChars.IndexOf(stringBuilder[stringBuilder.Length - 1]);
        Trace.Assert(num3 >= 0, "Transmitter code contains an invalid character not previously detected!");
        if (num2 == 4 && num3 > 15)
          throw new ArgumentException("Transmitter code must start with 'F' or less!");
        num1 += (uint) (num3 << 5 * num2);
        ++num2;
        stringBuilder.Remove(stringBuilder.Length - 1, 1);
      }
      return num1 & 16777215U;
    }

    public static string ConvertGtxTransmitterNumberToCode(uint number)
    {
      if (number > 16777215U)
        throw new ApplicationException("Transmitter number too large!");
      number += 16777216U;
      StringBuilder stringBuilder = new StringBuilder();
      for (int index1 = 0; index1 < 5; ++index1)
      {
        int index2 = (int) number & 31;
        stringBuilder.Insert(0, R2ReceiverValues.TransmitterIdValidChars[index2]);
        number >>= 5;
      }
      return ((object) stringBuilder).ToString();
    }

    public static string ConvertGen3TransmitterNumberToCode(uint number)
    {
      if (number > 16777215U)
        throw new ApplicationException("Transmitter number too large!");
      StringBuilder stringBuilder = new StringBuilder();
      for (int index1 = 0; index1 < 5; ++index1)
      {
        int index2 = (int) number & 31;
        stringBuilder.Insert(0, R2ReceiverValues.TransmitterIdValidChars[index2]);
        number >>= 5;
      }
      return ((object) stringBuilder).ToString();
    }

    public static string ConvertTransmitterNumberToDisplayableCode(uint number)
    {
      if (number <= (uint) ushort.MaxValue)
        return number.ToString();
      if (number >= 16777216U)
        return R2ReceiverTools.ConvertGtxTransmitterNumberToCode(number & 16777215U);
      else
        return R2ReceiverTools.ConvertGen3TransmitterNumberToCode(number);
    }

    public static byte[] SortDatabaseBlocksByBlockNumber(byte[] databaseBlocks)
    {
      if (databaseBlocks == null || databaseBlocks.Length == 0)
        return databaseBlocks;
      if (databaseBlocks.Length % R2ReceiverValues.BlockSize != 0)
        throw new ArgumentException("Array of database block is not a multiple of a block size.", "databaseBlocks");
      int num = databaseBlocks.Length / R2ReceiverValues.BlockSize;
      SortedList<uint, int> sortedList = new SortedList<uint, int>();
      for (int index = 0; index < num; ++index)
      {
        int startOffset = index * R2ReceiverValues.BlockSize;
        object obj = DataTools.ConvertBytesToObject(databaseBlocks, startOffset, typeof (R2BlockHeader));
        if (obj == null)
          throw new ApplicationException("Data does not appear to be an R2 database!");
        R2BlockHeader r2BlockHeader = (R2BlockHeader) obj;
        sortedList.Add(r2BlockHeader.m_number, startOffset);
      }
      byte[] numArray = new byte[databaseBlocks.Length];
      int destinationIndex = 0;
      foreach (KeyValuePair<uint, int> keyValuePair in sortedList)
      {
        Array.Copy((Array) databaseBlocks, keyValuePair.Value, (Array) numArray, destinationIndex, R2ReceiverValues.BlockSize);
        destinationIndex += R2ReceiverValues.BlockSize;
      }
      return numArray;
    }

    public static bool GetVirtualComDriverInfo(out bool isInstalled, out string driverVersion, out bool isNewDriver)
    {
      bool flag = false;
      isInstalled = false;
      driverVersion = string.Empty;
      isNewDriver = false;
      string name1 = "SYSTEM\\CurrentControlSet\\Control\\Class\\{4D36E978-E325-11CE-BFC1-08002BE10318}";
      using (RegistryKey registryKey1 = Registry.LocalMachine.OpenSubKey(name1))
      {
        if (registryKey1 != null)
        {
          string[] subKeyNames = registryKey1.GetSubKeyNames();
          if (subKeyNames != null)
          {
            foreach (string name2 in subKeyNames)
            {
              try
              {
                using (RegistryKey registryKey2 = registryKey1.OpenSubKey(name2))
                {
                  if (registryKey2 != null)
                  {
                    flag = true;
                    string str1 = registryKey2.GetValue("DriverDesc") as string;
                    if (!string.IsNullOrEmpty(str1))
                    {
                      if (str1.StartsWith("Silicon Labs CP210x"))
                      {
                        isInstalled = true;
                        isNewDriver = true;
                        string str2 = registryKey2.GetValue("DriverVersion") as string;
                        if (!string.IsNullOrEmpty(str2))
                        {
                          driverVersion = str2;
                          break;
                        }
                      }
                    }
                  }
                }
              }
              catch (SecurityException ex)
              {
              }
            }
          }
        }
      }
      string name3 = "SYSTEM\\CurrentControlSet\\Control\\Class\\{36FC9E60-C465-11CF-8056-444553540000}";
      using (RegistryKey registryKey1 = Registry.LocalMachine.OpenSubKey(name3))
      {
        if (registryKey1 != null)
        {
          string[] subKeyNames = registryKey1.GetSubKeyNames();
          if (subKeyNames != null)
          {
            foreach (string name2 in subKeyNames)
            {
              try
              {
                using (RegistryKey registryKey2 = registryKey1.OpenSubKey(name2))
                {
                  if (registryKey2 != null)
                  {
                    flag = true;
                    string str1 = registryKey2.GetValue("DriverDesc") as string;
                    if (!string.IsNullOrEmpty(str1))
                    {
                      if (str1.StartsWith("CP210"))
                      {
                        isInstalled = true;
                        if (str1.StartsWith("CP210x"))
                          isNewDriver = true;
                        string str2 = registryKey2.GetValue("DriverVersion") as string;
                        if (!string.IsNullOrEmpty(str2))
                        {
                          if (!string.IsNullOrEmpty(driverVersion))
                          {
                            // ISSUE: explicit reference operation
                            // ISSUE: variable of a reference type
                            string local = driverVersion;
                            // ISSUE: explicit reference operation
                            string str3 = local + " ; ";
                            // ISSUE: explicit reference operation
                            local = str3;
                          }
                          // ISSUE: explicit reference operation
                          // ISSUE: variable of a reference type
                          string local1 = driverVersion;
                          // ISSUE: explicit reference operation
                          string str4 = local1 + str2;
                          // ISSUE: explicit reference operation
                          local1 = str4;
                          break;
                        }
                      }
                    }
                  }
                }
              }
              catch (SecurityException ex)
              {
              }
            }
          }
        }
      }
      return flag;
    }

    public static string GetVirtualComDriverUninstallString(out string displayName)
    {
      string str = string.Empty;
      displayName = string.Empty;
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
              if (name2.StartsWith("SLABCOMM"))
              {
                using (RegistryKey registryKey2 = registryKey1.OpenSubKey(name2))
                {
                  if (registryKey2 != null)
                  {
                    str = registryKey2.GetValue("UninstallString") as string;
                    displayName = registryKey2.GetValue("DisplayName") as string;
                    break;
                  }
                }
              }
            }
          }
        }
      }
      return str;
    }

    public static byte[] GetVirtualComDriver630Zip()
    {
      Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Dexcom.R2Receiver.SiLabs 6.30 VCP Driver.zip");
      byte[] buffer = (byte[]) null;
      if (manifestResourceStream == null)
        throw new ApplicationException("Assembly resource missing 'SiLabs 6.30 VCP Driver.zip' content.");
      using (manifestResourceStream)
      {
        buffer = new byte[manifestResourceStream.Length];
        manifestResourceStream.Read(buffer, 0, buffer.Length);
      }
      return buffer;
    }

    public static void UnzipVirtualComDriver(DirectoryInfo targetFolder)
    {
      if (!targetFolder.Exists)
        targetFolder.Create();
      using (MemoryStream memoryStream = new MemoryStream(R2ReceiverTools.GetVirtualComDriver630Zip()))
      {
        ZipFile zipFile = new ZipFile((Stream) memoryStream);
        try
        {
          foreach (ZipEntry entry in zipFile)
          {
            if (!entry.IsDirectory)
            {
              string name = entry.Name;
              byte[] buffer = new byte[entry.Size];
              Stream inputStream = zipFile.GetInputStream(entry);
              int offset = inputStream.Read(buffer, 0, buffer.Length);
              while (offset < buffer.Length)
                offset += inputStream.Read(buffer, offset, buffer.Length - offset);
              string path = Path.Combine(targetFolder.FullName, name);
              DirectoryInfo directoryInfo = new DirectoryInfo(Path.GetDirectoryName(path));
              if (!directoryInfo.Exists)
                directoryInfo.Create();
              using (FileStream fileStream = File.Create(path))
                fileStream.Write(buffer, 0, buffer.Length);
            }
          }
        }
        finally
        {
          if (zipFile != null)
            zipFile.Close();
        }
      }
    }

    public static void RunVirtualComDriverUpdateCheck()
    {
      bool isInstalled = false;
      string driverVersion = string.Empty;
      bool isNewDriver = false;
      string str1 = Path.Combine(Path.GetDirectoryName(Path.GetTempPath()), "SiLabs 6.30 VCP Driver");
      string fileName = Path.Combine(str1, "CP210xVCPInstaller.exe");
      bool flag1 = CommonTools.IsRoleMember(CommonTools.GetBuiltInAdminstratorsGroupName());
      if (R2ReceiverTools.GetVirtualComDriverInfo(out isInstalled, out driverVersion, out isNewDriver) && (!isInstalled || !isNewDriver))
        R2ReceiverTools.UnzipVirtualComDriver(new DirectoryInfo(str1));
      FileInfo fileInfo1 = new FileInfo(fileName);
      if (!R2ReceiverTools.GetVirtualComDriverInfo(out isInstalled, out driverVersion, out isNewDriver))
        return;
      if (isInstalled && !isNewDriver)
      {
        DialogResult dialogResult = DialogResult.None;
        if (flag1 && fileInfo1.Exists)
          dialogResult = MessageBox.Show(ProgramContext.TryResourceLookup("Program_UninstallOldDriver", "This application requires a virtual USB/COM driver to communicate with the DexCom R2 Receiver. There is a previous version of the driver installed. Please remove any DexCom R2 Receivers attached to the computer and then press OK to uninstall the old driver and begin the new driver installation application.", new object[0]), ProgramContext.TryResourceLookup("Program_ReceiverDriverUpdateRecommended", "DexCom R2 Receiver Driver Update Recommended", new object[0]), MessageBoxButtons.OKCancel);
        else if (fileInfo1.Exists)
        {
          int num1 = (int) MessageBox.Show(ProgramContext.TryResourceLookup("Program_UninstallOldDriverAdminRightsRequired", "This application requires a virtual USB/COM driver to communicate with the DexCom R2 Receiver. There is a previous version of the driver installed. To uninstall the old driver, please log into this computer with Adminstrator rights and run this application again.", new object[0]) + Environment.NewLine + ProgramContext.TryResourceLookup("Program_WindowsVistaRunAsAdministrator", "On Windows Vista or Windows 7, please run this application as Administrator", new object[0]), ProgramContext.TryResourceLookup("Program_ReceiverDriverUpdateRecommended", "DexCom R2 Receiver Driver Update Recommended", new object[0]), MessageBoxButtons.OK);
        }
        else
        {
          int num2 = (int) MessageBox.Show(ProgramContext.TryResourceLookup("Program_ContactDexComSupportToUninstallOldDriver", "This application requires a virtual USB/COM driver to communicate with the DexCom R2 Receiver. There is a previous version of the driver installed. To uninstall the old driver, please contact DexCom Support for assistance.", new object[0]), ProgramContext.TryResourceLookup("Program_ReceiverDriverUpdateRecommended", "DexCom R2 Receiver Driver Update Recommended", new object[0]), MessageBoxButtons.OK);
        }
        if (dialogResult == DialogResult.OK && flag1 && fileInfo1.Exists)
        {
          string displayName = string.Empty;
          string str2 = R2ReceiverTools.GetVirtualComDriverUninstallString(out displayName);
          if (displayName == string.Empty && str2 == string.Empty)
          {
            FileInfo fileInfo2 = new FileInfo("C:\\Windows\\System32\\uninstall.ini");
            if (fileInfo2.Exists)
            {
              bool flag2 = false;
              using (StreamReader streamReader = fileInfo2.OpenText())
              {
                for (string str3 = streamReader.ReadLine(); str3 != null; str3 = streamReader.ReadLine())
                {
                  if (str3.StartsWith("SLABCOMM"))
                  {
                    flag2 = true;
                    break;
                  }
                }
              }
              if (flag2)
              {
                displayName = "CP2101";
                str2 = "C:\\Windows\\System32\\uninstall.exe C:\\Windows\\System32\\uninstall.ini";
              }
            }
          }
          if (displayName.StartsWith("CP2101") && !string.IsNullOrEmpty(str2))
          {
            Process process = new Process();
            string[] strArray = str2.Split(" ".ToCharArray(), 2);
            if (strArray != null && strArray.Length == 2)
            {
              process.StartInfo.FileName = strArray[0];
              process.StartInfo.Arguments = strArray[1];
              if (process.Start())
              {
                process.WaitForExit();
                R2ReceiverTools.GetVirtualComDriverInfo(out isInstalled, out driverVersion, out isNewDriver);
              }
            }
          }
          if (isInstalled)
          {
            int num3 = (int) MessageBox.Show(ProgramContext.TryResourceLookup("Program_DriverUninstallFailedOrCancelled", "Operation to uninstall prior virtual USB/COM driver failed or was cancelled.", new object[0]), ProgramContext.TryResourceLookup("Program_ReceiverDriverUninstall", "DexCom R2 Receiver Driver Uninstall", new object[0]), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          }
        }
      }
      if (isInstalled)
        return;
      DialogResult dialogResult1 = DialogResult.None;
      if (flag1 && fileInfo1.Exists)
        dialogResult1 = MessageBox.Show(ProgramContext.TryResourceLookup("Program_ReceiverDriverInstallation", "This application requires a virtual USB/COM driver to communicate with the DexCom R2 Receiver. Please remove any DexCom R2 Receivers attached to the computer and then press OK to begin the driver installation application.", new object[0]), ProgramContext.TryResourceLookup("Program_ReceiverDriverRequired", "DexCom R2 Receiver Driver Required", new object[0]), MessageBoxButtons.OKCancel);
      else if (fileInfo1.Exists)
      {
        int num4 = (int) MessageBox.Show(ProgramContext.TryResourceLookup("Program_InstallDriverRunAsAdministrator", "This application requires a virtual USB/COM driver to communicate with the DexCom R2 Receiver. To install the driver, please log into this computer with Adminstrator rights and run this application again.", new object[0]) + Environment.NewLine + ProgramContext.TryResourceLookup("Program_WindowsVistaRunAsAdministrator", "On Windows Vista or Windows 7, please run this application as Administrator", new object[0]), ProgramContext.TryResourceLookup("Program_ReceiverDriverRequired", "DexCom R2 Receiver Driver Required", new object[0]), MessageBoxButtons.OK);
      }
      else
      {
        int num5 = (int) MessageBox.Show(ProgramContext.TryResourceLookup("Program_ContactDexComSupportToInstallDriver", "This application requires a virtual USB/COM driver to communicate with the DexCom R2 Receiver. To install the driver, please contact DexCom Support for assistance.", new object[0]), ProgramContext.TryResourceLookup("Program_ReceiverDriverRequired", "DexCom R2 Receiver Driver Required", new object[0]), MessageBoxButtons.OK);
      }
      if (dialogResult1 != DialogResult.OK || !flag1 || !fileInfo1.Exists)
        return;
      Process process1 = new Process();
      process1.StartInfo.FileName = fileName;
      if (!process1.Start())
        return;
      process1.WaitForExit();
      int num6 = (int) MessageBox.Show(ProgramContext.TryResourceLookup("Program_FinishDriverInstallation", "To finish the driver installation, please attach a DexCom R2 Receiver to the computer using the supplied USB/COM cable.", new object[0]), ProgramContext.TryResourceLookup("Program_ReceiverDriverRequired", "DexCom R2 Receiver Driver Required", new object[0]), MessageBoxButtons.OK);
    }

    private static XmlDocument DoDownloadReceiverBiosMode()
    {
      XmlDocument xmlDocument = (XmlDocument) null;
      using (API api = new API())
      {
        if (!api.ConnectToBios(true))
          throw new ApplicationException(ProgramContext.TryResourceLookup("Exception_UnableToLocateReceiver", "Unable to locate receiver.", new object[0]));
        XObject xobject = new XObject("ReceiverDownload");
        DateTime now = DateTime.Now;
        uint num1 = 0U;
        SettingsRecord settingsRecord = (SettingsRecord) null;
        uint uintValue1 = 0U;
        uint uintValue2 = 0U;
        uint num2 = api.ReadBiosHeader();
        bool boolValue1 = api.IsEventLogEmpty();
        bool boolValue2 = api.IsErrorLogEmpty();
        uint num3 = api.ReadRTC();
        DateTime dtValue1 = R2ReceiverValues.R2BaseDateTime.AddSeconds((double) num3);
        DateTime dtValue2 = dtValue1;
        api.ReadFirmwareHeader();
        XmlElement xmlElement1 = api.ReadExtendedFirmwareHeader();
        XmlElement xmlElement2 = api.ReadExtendedBiosHeader();
        byte[] sourceData1 = api.ReadHardwareConfigurationBinary();
        HardwareConfiguration hardwareConfiguration = api.CachedHardwareConfiguration;
        int num4 = (int) hardwareConfiguration.ReceiverId;
        byte[] sourceData2 = (byte[]) null;
        byte[] sourceData3 = (byte[]) null;
        byte[] numArray1 = (byte[]) null;
        bool boolValue3 = false;
        R2ErrorLogInfo r2ErrorLogInfo = new R2ErrorLogInfo();
        object obj1 = DataTools.ConvertBytesToObject(api.ReadBlock(R2ReceiverValues.DatabaseStartAddress, 1024U), 0, typeof (R2BlockHeader));
        if (obj1 == null)
          throw new ApplicationException("Data does not appear to be an R2 database!");
        R2BlockHeader r2BlockHeader = (R2BlockHeader) obj1;
        if ((int) r2BlockHeader.m_headerTag != (int) R2ReceiverValues.BlockHeaderTag)
          throw new ApplicationException("Data does not appear to be an R2 database!");
        if ((int) r2BlockHeader.m_number != 0)
        {
          numArray1 = R2ReceiverTools.SortDatabaseBlocksByBlockNumber(api.ReadDatabase());
          uintValue2 = (uint) (numArray1.Length / R2ReceiverValues.BlockSize);
        }
        else
        {
          uint size = Convert.ToUInt32(R2ReceiverValues.BlockSize);
          byte[] numArray2 = new byte[/*(IntPtr) */(32U * size)];
          int length = 0;
          for (uint index = 0U; index < 32U; ++index)
          {
            uint num5 = index * size;
            uint address = R2ReceiverValues.DatabaseStartAddress + num5;
            byte[] bytes = api.ReadBlock(address, size);
            object obj2 = DataTools.ConvertBytesToObject(bytes, 0, typeof (R2BlockHeader));
            if (obj2 == null)
              throw new ApplicationException("Data does not appear to be an R2 database!");
            r2BlockHeader = (R2BlockHeader) obj2;
            if ((int) r2BlockHeader.m_headerTag != (int) R2ReceiverValues.BlockHeaderTag)
              throw new ApplicationException("Data does not appear to be an R2 database!");
            if ((int) r2BlockHeader.m_status == (int) R2ReceiverValues.BlockStatusUsed || (int) r2BlockHeader.m_status == (int) R2ReceiverValues.BlockStatusReadyToErase)
            {
              Array.Copy((Array) bytes, 0L, (Array) numArray2, (long) num5, (long) bytes.Length);
              length += (int) size;
            }
            else
              break;
          }
          if (length > 0)
          {
            numArray1 = new byte[length];
            Array.Copy((Array) numArray2, (Array) numArray1, length);
            uintValue2 = (uint) (numArray1.Length / R2ReceiverValues.BlockSize);
          }
        }
        if (!boolValue1)
          sourceData3 = api.ReadEventLog();
        if (!boolValue2)
          sourceData2 = api.ReadErrorLog();
        api.Disconnect();
        xobject.Id = Guid.NewGuid();
        xobject.SetAttribute("DateTimeNowLocal", now);
        xobject.SetAttribute("DateTimeNowUTC", now.ToUniversalTime());
        xobject.SetAttribute("ApplicationName", AppDomain.CurrentDomain.FriendlyName);
        xobject.SetAttribute("ApplicationBase", AppDomain.CurrentDomain.SetupInformation.ApplicationBase);
        xobject.SetAttribute("AssemblyFullName", Assembly.GetExecutingAssembly().FullName);
        xobject.SetAttribute("AssemblyVersion", ((object) Assembly.GetExecutingAssembly().GetName().Version).ToString());
        XApplicationInfo xapplicationInfo = DataTools.GetXApplicationInfo(xobject.Element.OwnerDocument);
        xobject.AppendChild((XObject) xapplicationInfo);
        XComputerInfo xcomputerInfo = DataTools.GetXComputerInfo(xobject.Element.OwnerDocument);
        if (string.Compare(Environment.UserDomainName, "DEXCOM", StringComparison.InvariantCultureIgnoreCase) != 0)
          xcomputerInfo.Deidentify();
        xobject.AppendChild((XObject) xcomputerInfo);
        XObject xChildObject1 = new XObject(xobject.CreateElement("ReceiverData"));
        xobject.AppendChild(xChildObject1);
        xChildObject1.SetAttribute("DatabaseRevision", num1.ToString("X"));
        xChildObject1.SetAttribute("IsEventLogEmpty", boolValue1);
        xChildObject1.SetAttribute("IsErrorLogEmpty", boolValue2);
        xChildObject1.SetAttribute("InternalTime", dtValue2);
        xChildObject1.SetAttribute("RTCTime", dtValue1);
        xChildObject1.SetAttribute("BiosHeader", num2.ToString("X"));
        xChildObject1.SetAttribute("IsFirmwareMode", false);
        xChildObject1.SetAttribute("LastFixedBlockInDatabase", uintValue1);
        xChildObject1.SetAttribute("NumberOfNewBlocksInDatabase", uintValue2);
        if (boolValue3)
        {
          xChildObject1.SetAttribute("HasErrorLogInfo", boolValue3);
          xChildObject1.SetAttribute("DateTimeLastError", r2ErrorLogInfo.DateTimeLastError);
          xChildObject1.SetAttribute("NumberOfErrors", r2ErrorLogInfo.NumberOfErrors);
        }
        XmlNode newChild1 = xobject.Element.OwnerDocument.ImportNode((XmlNode) xmlElement1, true);
        xChildObject1.Element.AppendChild(newChild1);
        XmlNode newChild2 = xobject.Element.OwnerDocument.ImportNode((XmlNode) xmlElement2, true);
        xChildObject1.Element.AppendChild(newChild2);
        XR2HardwareConfig xr2HardwareConfig = hardwareConfiguration.GetXR2HardwareConfig();
        XmlNode newChild3 = xobject.Element.OwnerDocument.ImportNode((XmlNode) xr2HardwareConfig.Element, true);
        xChildObject1.Element.AppendChild(newChild3);
        XmlElement compressedBinaryElement = DataTools.CreateCompressedBinaryElement(sourceData1, "HardwareConfigData", xobject.Element.OwnerDocument);
        xChildObject1.Element.AppendChild((XmlNode) compressedBinaryElement);
        XObject xChildObject2 = new XObject(DataTools.CreateCompressedBinaryElement(sourceData2, "ErrorLog", xobject.Element.OwnerDocument));
        XObject xChildObject3 = new XObject(DataTools.CreateCompressedBinaryElement(sourceData3, "EventLog", xobject.Element.OwnerDocument));
        XObject xChildObject4 = new XObject(DataTools.CreateCompressedBinaryElement(numArray1, "DatabaseRecords", xobject.Element.OwnerDocument));
        XObject xChildObject5 = new XObject(xobject.CreateElement("DatabaseBlockInfo"));
        if (numArray1 != null)
        {
          try
          {
            R2BlockInfoParser r2BlockInfoParser = new R2BlockInfoParser();
            r2BlockInfoParser.Parse(numArray1);
            R2BlockInfo[] blockInfoArray = r2BlockInfoParser.BlockInfoArray;
            if (blockInfoArray != null)
            {
              foreach (R2BlockInfo r2BlockInfo in blockInfoArray)
              {
                if ((int) r2BlockInfo.Status == (int) R2ReceiverValues.BlockStatusUsed || (int) r2BlockInfo.Status == (int) R2ReceiverValues.BlockStatusReadyToErase)
                  xChildObject5.Element.AppendChild((XmlNode) r2BlockInfo.GetXml(xobject.Element.OwnerDocument));
              }
            }
            if (settingsRecord == null)
              settingsRecord = r2BlockInfoParser.LatestSettingsRecord;
          }
          catch (Exception ex)
          {
          }
        }
        XObject xChildObject6 = new XObject(xobject.CreateElement("LastSettingsRecord"));
        if (settingsRecord != null)
        {
          foreach (XmlAttribute xmlAttribute in (XmlNamedNodeMap) settingsRecord.Xml.Attributes)
            xChildObject6.SetAttribute(xmlAttribute.Name, xmlAttribute.Value);
        }
        xChildObject1.AppendChild(xChildObject6);
        xChildObject1.AppendChild(xChildObject2);
        xChildObject1.AppendChild(xChildObject3);
        xChildObject1.AppendChild(xChildObject4);
        xChildObject1.AppendChild(xChildObject5);
        xmlDocument = new XmlDocument();
        xmlDocument.PreserveWhitespace = true;
        xmlDocument.LoadXml(CommonTools.FormatXml(xobject.Xml));
        XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", Encoding.UTF8.HeaderName, (string) null);
        xmlDocument.InsertBefore((XmlNode) xmlDeclaration, (XmlNode) xmlDocument.DocumentElement);
      }
      return xmlDocument;
    }

      //AJH This looks veeeeery interesting
    private static XmlDocument DoDownloadReceiver(API api, BackgroundWorker backgroundWorker, XObject xPriorDownloadInfo)
    {
      XmlDocument xmlDocument = (XmlDocument) null;
      XObject xobject1 = new XObject("DownloadProgressInfo");
      if (backgroundWorker == null || !backgroundWorker.CancellationPending)
      {
        uint num1 = 0U;
        uint num2 = 0U;
        DateTime dateTime = R2ReceiverValues.R2BaseDateTime;
        if (xPriorDownloadInfo != null && xPriorDownloadInfo.IsNotNull())
        {
          num1 = xPriorDownloadInfo.GetAttribute<uint>("PriorLastFixedBlock");
          num2 = xPriorDownloadInfo.GetAttribute<uint>("PriorNumberOfErrors");
          dateTime = xPriorDownloadInfo.GetAttribute<DateTime>("PriorTimeLastError");
        }
        xobject1.SetAttribute("DownloadState", "Connecting");
        if (backgroundWorker == null || !backgroundWorker.CancellationPending)
        {
          if (backgroundWorker != null)
            backgroundWorker.ReportProgress(0, (object) xobject1);
            // AJH: api.ConnectToFirmware(true): opens connection to receiver??
          if (!api.ConnectToFirmware(true))
            throw new ApplicationException(ProgramContext.TryResourceLookup("Exception_UnableToLocateReceiver", "Unable to locate receiver.", new object[0]));
          if (backgroundWorker == null || !backgroundWorker.CancellationPending)
          {
            XObject xobject2 = new XObject("ReceiverDownload");
            DateTime now = DateTime.Now;
            //AJH: api.ReadDatabaseRevision() 
            uint num3 = api.ReadDatabaseRevision();
            SettingsRecord settingsRecord = api.ReadSettings();
            ulong num4 = api.ReadInternalTime();
            uint newLastFixedRecord = 0U;
            uint numberOfBlocks = 0U;
            //AJH: api.ReadDatabaseInformationOnly(...): gets number of blocks, newLastFixedRecord (I think)
            api.ReadDatabaseInformationOnly(0U, out newLastFixedRecord, out numberOfBlocks);
            uint startBlock = num1 <= newLastFixedRecord ? num1 : 0U;
            xobject1.SetAttribute("DownloadState", "ReadingSettings");
            if (backgroundWorker == null || !backgroundWorker.CancellationPending)
            {
              if (backgroundWorker != null)
                backgroundWorker.ReportProgress(0, (object) xobject1);
              uint num5 = api.ReadBiosHeader();
              bool boolValue1 = api.IsEventLogEmpty();
              bool boolValue2 = api.IsErrorLogEmpty();
              uint num6 = api.ReadRTC();
              DateTime dtValue1 = R2ReceiverValues.R2BaseDateTime.AddSeconds((double) num6);
              DateTime dtValue2 = R2ReceiverValues.R2BaseDateTime.AddMilliseconds((double) num4);
              api.ReadFirmwareHeader();
              XmlElement xmlElement1 = api.ReadExtendedFirmwareHeader();
              XmlElement xmlElement2 = api.ReadExtendedBiosHeader();
              byte[] sourceData1 = api.ReadHardwareConfigurationBinary();
              HardwareConfiguration hardwareConfiguration = api.CachedHardwareConfiguration;
              if (backgroundWorker == null || !backgroundWorker.CancellationPending)
              {
                byte[] sourceData2 = (byte[]) null;
                byte[] sourceData3 = (byte[]) null;
                bool boolValue3 = false;
                R2ErrorLogInfo errorLogInfo = new R2ErrorLogInfo();
                bool flag = true;
                try
                {
                  boolValue3 = api.ReadErrorLogInfo(out errorLogInfo);
                }
                catch
                {
                }
                if (boolValue3)
                  flag = (errorLogInfo.DateTimeLastError != dateTime || (int) errorLogInfo.NumberOfErrors != (int) num2) && !boolValue2;
                bool connected = false;
                TimeSpan tsValue = CommonTools.LocalClockOffset(out connected);
                XObject xChildObject1 = new XObject("TimeInfo", xobject2.Element.OwnerDocument);
                xChildObject1.SetAttribute("ReceiverSkewOffset", settingsRecord.SkewOffset);
                xChildObject1.SetAttribute("ReceiverUserOffset", settingsRecord.UserOffset);
                xChildObject1.SetAttribute("ComputerDisplayTime0", DateTime.Now);
                xChildObject1.SetAttribute("ReceiverDisplayTime", R2ReceiverValues.R2BaseDateTime.AddMilliseconds((double) api.ReadInternalTime()) + settingsRecord.SkewOffset + settingsRecord.UserOffset);
                xChildObject1.SetAttribute("ComputerDisplayTime1", DateTime.Now);
                xChildObject1.SetAttribute("ComputerUtcTime0", DateTime.UtcNow);
                xChildObject1.SetAttribute("ReceiverUtcTime", R2ReceiverValues.R2BaseDateTime.AddSeconds((double) api.ReadRTC()));
                xChildObject1.SetAttribute("ComputerUtcTime1", DateTime.UtcNow);
                xChildObject1.SetAttribute("LocalClockOffset", tsValue);
                xobject1.SetAttribute("DownloadState", "ReadingDatabase");
                if (backgroundWorker == null || !backgroundWorker.CancellationPending)
                {
                  if (backgroundWorker != null)
                    backgroundWorker.ReportProgress(0, (object) xobject1);
                  byte[] numArray = R2ReceiverTools.SortDatabaseBlocksByBlockNumber(api.ReadDatabase(startBlock, backgroundWorker));
                  if (!boolValue1 && flag)
                  {
                    xobject1.SetAttribute("DownloadState", "ReadingEventLog");
                    if (backgroundWorker == null || !backgroundWorker.CancellationPending)
                    {
                      if (backgroundWorker != null)
                        backgroundWorker.ReportProgress(0, (object) xobject1);
                      sourceData3 = api.ReadEventLog(backgroundWorker);
                    }
                    else
                      goto label_68;
                  }
                  if (!boolValue2 && flag)
                  {
                    xobject1.SetAttribute("DownloadState", "ReadingErrorLog");
                    if (backgroundWorker == null || !backgroundWorker.CancellationPending)
                    {
                      if (backgroundWorker != null)
                        backgroundWorker.ReportProgress(0, (object) xobject1);
                      sourceData2 = api.ReadErrorLog(backgroundWorker);
                    }
                    else
                      goto label_68;
                  }
                  api.Disconnect();
                  xobject1.SetAttribute("DownloadState", "ValidatingDatabase");
                  if (backgroundWorker == null || !backgroundWorker.CancellationPending)
                  {
                    if (backgroundWorker != null)
                      backgroundWorker.ReportProgress(0, (object) xobject1);
                    try
                    {
                      new R2DatabaseRecordsParser().Parse(numArray);
                    }
                    catch (Exception ex)
                    {
                      throw new DexComException("Improper Receiver Database, download halted.", ex);
                    }
                    xobject1.SetAttribute("DownloadState", "CreatingXmlDownload");
                    if (backgroundWorker == null || !backgroundWorker.CancellationPending)
                    {
                      if (backgroundWorker != null)
                        backgroundWorker.ReportProgress(0, (object) xobject1);
                      xobject2.Id = Guid.NewGuid();
                      xobject2.SetAttribute("DateTimeNowLocal", now);
                      xobject2.SetAttribute("DateTimeNowUTC", now.ToUniversalTime());
                      xobject2.SetAttribute("ApplicationName", AppDomain.CurrentDomain.FriendlyName);
                      xobject2.SetAttribute("ApplicationBase", AppDomain.CurrentDomain.SetupInformation.ApplicationBase);
                      xobject2.SetAttribute("AssemblyFullName", Assembly.GetExecutingAssembly().FullName);
                      xobject2.SetAttribute("AssemblyVersion", ((object) Assembly.GetExecutingAssembly().GetName().Version).ToString());
                      if (xChildObject1 != null)
                        xobject2.AppendChild(xChildObject1);
                      XApplicationInfo xapplicationInfo = DataTools.GetXApplicationInfo(xobject2.Element.OwnerDocument);
                      xobject2.AppendChild((XObject) xapplicationInfo);
                      XComputerInfo xcomputerInfo = DataTools.GetXComputerInfo(xobject2.Element.OwnerDocument);
                      if (string.Compare(Environment.UserDomainName, "DEXCOM", StringComparison.InvariantCultureIgnoreCase) != 0)
                        xcomputerInfo.Deidentify();
                      xobject2.AppendChild((XObject) xcomputerInfo);
                      XObject xChildObject2 = new XObject(xobject2.CreateElement("ReceiverData"));
                      xobject2.AppendChild(xChildObject2);
                      xChildObject2.SetAttribute("DatabaseRevision", num3.ToString("X"));
                      xChildObject2.SetAttribute("IsEventLogEmpty", boolValue1);
                      xChildObject2.SetAttribute("IsErrorLogEmpty", boolValue2);
                      xChildObject2.SetAttribute("InternalTime", dtValue2);
                      xChildObject2.SetAttribute("RTCTime", dtValue1);
                      xChildObject2.SetAttribute("BiosHeader", num5.ToString("X"));
                      xChildObject2.SetAttribute("IsFirmwareMode", true);
                      xChildObject2.SetAttribute("LastFixedBlockInDatabase", newLastFixedRecord);
                      xChildObject2.SetAttribute("NumberOfNewBlocksInDatabase", numberOfBlocks);
                      if (boolValue3)
                      {
                        xChildObject2.SetAttribute("HasErrorLogInfo", boolValue3);
                        xChildObject2.SetAttribute("DateTimeLastError", errorLogInfo.DateTimeLastError);
                        xChildObject2.SetAttribute("NumberOfErrors", errorLogInfo.NumberOfErrors);
                      }
                      XmlNode newChild1 = xobject2.Element.OwnerDocument.ImportNode((XmlNode) xmlElement1, true);
                      xChildObject2.Element.AppendChild(newChild1);
                      XmlNode newChild2 = xobject2.Element.OwnerDocument.ImportNode((XmlNode) xmlElement2, true);
                      xChildObject2.Element.AppendChild(newChild2);
                      XR2HardwareConfig xr2HardwareConfig = hardwareConfiguration.GetXR2HardwareConfig();
                      XmlNode newChild3 = xobject2.Element.OwnerDocument.ImportNode((XmlNode) xr2HardwareConfig.Element, true);
                      xChildObject2.Element.AppendChild(newChild3);
                      XmlElement compressedBinaryElement = DataTools.CreateCompressedBinaryElement(sourceData1, "HardwareConfigData", xobject2.Element.OwnerDocument);
                      xChildObject2.Element.AppendChild((XmlNode) compressedBinaryElement);
                      XObject xChildObject3 = new XObject(DataTools.CreateCompressedBinaryElement(sourceData2, "ErrorLog", xobject2.Element.OwnerDocument));
                      XObject xChildObject4 = new XObject(DataTools.CreateCompressedBinaryElement(sourceData3, "EventLog", xobject2.Element.OwnerDocument));
                      XObject xChildObject5 = new XObject(DataTools.CreateCompressedBinaryElement(numArray, "DatabaseRecords", xobject2.Element.OwnerDocument));
                      XObject xChildObject6 = new XObject(xobject2.CreateElement("DatabaseBlockInfo"));
                      if (numArray != null)
                      {
                        try
                        {
                          R2BlockInfoParser r2BlockInfoParser = new R2BlockInfoParser();
                          r2BlockInfoParser.Parse(numArray);
                          R2BlockInfo[] blockInfoArray = r2BlockInfoParser.BlockInfoArray;
                          if (blockInfoArray != null)
                          {
                            foreach (R2BlockInfo r2BlockInfo in blockInfoArray)
                            {
                              if ((int) r2BlockInfo.Status == (int) R2ReceiverValues.BlockStatusUsed || (int) r2BlockInfo.Status == (int) R2ReceiverValues.BlockStatusReadyToErase)
                                xChildObject6.Element.AppendChild((XmlNode) r2BlockInfo.GetXml(xobject2.Element.OwnerDocument));
                            }
                          }
                          if (settingsRecord == null)
                            settingsRecord = r2BlockInfoParser.LatestSettingsRecord;
                        }
                        catch (Exception ex)
                        {
                        }
                      }
                      XObject xChildObject7 = new XObject(xobject2.CreateElement("LastSettingsRecord"));
                      if (settingsRecord != null)
                      {
                        foreach (XmlAttribute xmlAttribute in (XmlNamedNodeMap) settingsRecord.Xml.Attributes)
                          xChildObject7.SetAttribute(xmlAttribute.Name, xmlAttribute.Value);
                      }
                      xChildObject2.AppendChild(xChildObject7);
                      xChildObject2.AppendChild(xChildObject3);
                      xChildObject2.AppendChild(xChildObject4);
                      xChildObject2.AppendChild(xChildObject5);
                      xChildObject2.AppendChild(xChildObject6);
                      if (xPriorDownloadInfo != null && xPriorDownloadInfo.IsNotNull())
                      {
                        xPriorDownloadInfo.SetAttribute("PriorLastFixedBlock", newLastFixedRecord);
                        if (boolValue3)
                        {
                          xPriorDownloadInfo.SetAttribute("PriorNumberOfErrors", errorLogInfo.NumberOfErrors);
                          xPriorDownloadInfo.SetAttribute("PriorTimeLastError", errorLogInfo.DateTimeLastError);
                        }
                        else
                        {
                          xPriorDownloadInfo.SetAttribute("PriorNumberOfErrors", 0);
                          xPriorDownloadInfo.SetAttribute("PriorTimeLastError", R2ReceiverValues.R2BaseDateTime);
                        }
                      }
                      xmlDocument = new XmlDocument();
                      xmlDocument.PreserveWhitespace = true;
                      xmlDocument.LoadXml(CommonTools.FormatXml(xobject2.Xml));
                      XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", Encoding.UTF8.HeaderName, (string) null);
                      xmlDocument.InsertBefore((XmlNode) xmlDeclaration, (XmlNode) xmlDocument.DocumentElement);
                    }
                  }
                }
              }
            }
          }
        }
      }
label_68:
      return xmlDocument;
    }

    public static XmlDocument DownloadReceiver(bool isFirmwareMode)
    {
      XmlDocument xDocument = !isFirmwareMode ? R2ReceiverTools.DoDownloadReceiverBiosMode() : R2ReceiverTools.DoDownloadReceiver((API) null, (BackgroundWorker) null, (XObject) null);
      if (xDocument != null)
        CommonTools.AddDocumentSignature(xDocument);
      return xDocument;
    }

    public static XmlDocument DownloadReceiver(API api, BackgroundWorker backgroundWorker, XObject xPriorDownloadInfo)
    {
      XmlDocument xDocument = R2ReceiverTools.DoDownloadReceiver(api, backgroundWorker, xPriorDownloadInfo);
      if (xDocument != null)
        CommonTools.AddDocumentSignature(xDocument);
      return xDocument;
    }

    public static XUpdateQueryInfo ReadUpdateQueryInformationFromReceiver()
    {
      XUpdateQueryInfo xupdateQueryInfo = (XUpdateQueryInfo) null;
      using (API api = new API())
      {
        if (!api.ConnectToFirmware(true))
          throw new ApplicationException(ProgramContext.TryResourceLookup("Exception_UnableToLocateReceiver", "Unable to locate receiver.", new object[0]));
        xupdateQueryInfo = R2ReceiverTools.ReadUpdateQueryInformationFromReceiver(api);
        api.Disconnect();
      }
      return xupdateQueryInfo;
    }

    public static XUpdateQueryInfo ReadUpdateQueryInformationFromReceiver(API api)
    {
      XUpdateQueryInfo xupdateQueryInfo = new XUpdateQueryInfo();
      DateTime now = DateTime.Now;
      XObject xobject1 = new XObject(api.ReadExtendedBiosHeader());
      XObject xobject2 = new XObject(api.ReadExtendedFirmwareHeader());
      HardwareConfiguration hardwareConfiguration = api.CachedHardwareConfiguration;
      xupdateQueryInfo.DateTimeLocal = now;
      xupdateQueryInfo.DateTimeUTC = now.ToUniversalTime();
      xupdateQueryInfo.ReceiverNumber = hardwareConfiguration.ReceiverId;
      xupdateQueryInfo.ReceiverInstanceId = hardwareConfiguration.ReceiverInstanceId;
      xupdateQueryInfo.ImageInstanceId = hardwareConfiguration.ImageInstanceId;
      xupdateQueryInfo.ImageExtension = hardwareConfiguration.ConfigurationVersion >= 4U ? hardwareConfiguration.ImageExtension : string.Empty;
      xupdateQueryInfo.ImageSubExtension = hardwareConfiguration.ConfigurationVersion >= 6U ? hardwareConfiguration.ImageSubExtension : string.Empty;
      xupdateQueryInfo.HardwareProductNumber = hardwareConfiguration.HardwareProductNumber;
      xupdateQueryInfo.HardwareProductRevision = hardwareConfiguration.HardwareProductRevision;
      xupdateQueryInfo.FirmwareFlags = ((object) hardwareConfiguration.FirmwareFlags).ToString();
      xupdateQueryInfo.HardwareFlags = ((object) hardwareConfiguration.GetXR2HardwareConfig().HardwareFlags).ToString();
      xupdateQueryInfo.BiosRevision = xobject1.GetAttribute("BiosRevision");
      xupdateQueryInfo.BiosProductString = xobject1.GetAttribute("ProductString");
      xupdateQueryInfo.FirmwareRevision = xobject2.GetAttribute("Revision");
      xupdateQueryInfo.FirmwareProductString = xobject2.GetAttribute("ProductString");
      xupdateQueryInfo.FirmwareBuildDate = xobject2.GetAttribute("BuildDateAndTime");
      xupdateQueryInfo.DatabaseRevision = xobject2.GetAttribute("DatabaseRevision");
      return xupdateQueryInfo;
    }

    [DllImport("Kernel32.DLL", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern uint SetThreadExecutionState(uint state);

    [SecuritySafeCritical]
    public static void PreventStandby()
    {
      int num = (int) R2ReceiverTools.SetThreadExecutionState(1U);
    }

    public static List<SensorSession> CreateSensorSessionList(R2DatabaseRecords databaseRecords)
    {
      List<SensorSession> list = new List<SensorSession>();
      int index = 0;
      List<STSCalibrationRecord> calibrationRecords = databaseRecords.STSCalibrationRecords;
      while (index < calibrationRecords.Count)
      {
        SensorSession sensorSession1 = (SensorSession) null;
        STSCalibrationRecord calibrationRecord1 = (STSCalibrationRecord) null;
        for (; index < calibrationRecords.Count; ++index)
        {
          STSCalibrationRecord calibrationRecord2 = calibrationRecords[index];
          if (calibrationRecord2.SensorInsertionTime == R2ReceiverValues.EmptySensorInsertionTime)
          {
            calibrationRecord1 = calibrationRecord2;
          }
          else
          {
            sensorSession1 = new SensorSession();
            sensorSession1.StartCalibrationRecord = calibrationRecord2;
            break;
          }
        }
        if (sensorSession1 != null)
        {
          STSCalibrationRecord calibrationRecord2 = sensorSession1.StartCalibrationRecord;
          DateTime sensorInsertionTime = sensorSession1.StartCalibrationRecord.SensorInsertionTime;
          for (; index < calibrationRecords.Count; ++index)
          {
            STSCalibrationRecord calibrationRecord3 = calibrationRecords[index];
            SensorSession sensorSession2;
            if (calibrationRecord3.SensorInsertionTime == sensorInsertionTime)
            {
              calibrationRecord2 = calibrationRecord3;
              if (sensorSession1.FirstInCalRecord == null && (int) calibrationRecord3.AlgorithmCalibrationStateCode == 1)
                sensorSession1.FirstInCalRecord = calibrationRecord3;
              if (index == calibrationRecords.Count - 1)
              {
                sensorSession1.StopCalibrationRecord = calibrationRecord3;
                sensorSession1.FinishCalibrationRecord = calibrationRecord3;
                sensorSession1.DidSessionFinish = false;
                list.Add(sensorSession1);
                sensorSession2 = (SensorSession) null;
                ++index;
                break;
              }
            }
            else
            {
              if (calibrationRecord3.SensorInsertionTime == R2ReceiverValues.EmptySensorInsertionTime)
                calibrationRecord2 = calibrationRecord3;
              sensorSession1.StopCalibrationRecord = calibrationRecord3;
              sensorSession1.FinishCalibrationRecord = calibrationRecord2;
              sensorSession1.DidSessionFinish = true;
              list.Add(sensorSession1);
              sensorSession2 = (SensorSession) null;
              break;
            }
          }
        }
      }
      return list;
    }

    public static void CalculateSensorFailures(List<SensorSession> sensorSessions, List<IGenericRecord> allRecords)
    {
      foreach (SensorSession sensorSession in sensorSessions)
      {
        int indexAllRecords1 = (sensorSession.StartCalibrationRecord.Tag as RecordIndex).IndexAllRecords;
        int indexAllRecords2 = (sensorSession.FinishCalibrationRecord.Tag as RecordIndex).IndexAllRecords;
        for (int index = indexAllRecords1 + 1; index < indexAllRecords2; ++index)
        {
          if (allRecords[index].RecordType == R2RecordType.Log)
          {
            LogRecord logRecord = allRecords[index] as LogRecord;
            if ((int) logRecord.EventTypeCode == 1 && ((int) logRecord.Data1 == 18 || (int) logRecord.Data1 == 464))
            {
              sensorSession.FirstSensorFailureTime = logRecord.TimeStampUtc;
              break;
            }
          }
        }
      }
    }

    public static void FixupSensorSessionTimes(List<SensorSession> sensorSessions, DateTime firstMeterDisplayTime, DateTime firstGlucoseDisplayTime, DateTime lastMeterDisplayTime, DateTime lastGlucoseDisplayTime, DateTime firstMeterInternalTime, DateTime firstGlucoseInternalTime, DateTime lastMeterInternalTime, DateTime lastGlucoseInternalTime)
    {
      for (int index = sensorSessions.Count - 1; index >= 0; --index)
      {
        SensorSession sensorSession = sensorSessions[index];
        if (index == sensorSessions.Count - 1)
        {
          if (!sensorSession.DidSessionFinish)
          {
            DateTime dateTime = sensorSession.FinishDisplayTime;
            if (lastMeterDisplayTime > dateTime)
            {
              dateTime = lastMeterDisplayTime;
              DateTimePair dateTimePair = new DateTimePair()
              {
                DisplayTime = lastMeterDisplayTime,
                InternalTime = lastMeterInternalTime
              };
              sensorSession.Tag = (object) new KeyValuePair<bool, DateTimePair>(false, dateTimePair);
            }
            if (lastGlucoseDisplayTime > dateTime)
            {
              DateTimePair dateTimePair = new DateTimePair()
              {
                DisplayTime = lastGlucoseDisplayTime,
                InternalTime = lastGlucoseInternalTime
              };
              sensorSession.Tag = (object) new KeyValuePair<bool, DateTimePair>(false, dateTimePair);
            }
          }
        }
        else if (index == 0 && (int) sensorSession.FinishCalibrationRecord.AlgorithmCalibrationStateCode == 1 && firstGlucoseDisplayTime < sensorSession.StartDisplayTime)
        {
          DateTimePair dateTimePair = new DateTimePair()
          {
            DisplayTime = firstGlucoseDisplayTime,
            InternalTime = firstGlucoseInternalTime
          };
          sensorSession.Tag = (object) new KeyValuePair<bool, DateTimePair>(false, dateTimePair);
        }
      }
    }

    public static void CalculatePacketCapture(List<SensorSession> sensorSessions, R2DatabaseRecords databaseRecords)
    {
      foreach (SensorSession sensorSession in sensorSessions)
      {
        int num = 0;
        DateTime startInternalTime = sensorSession.StartInternalTime;
        DateTime finishInternalTime = sensorSession.FinishInternalTime;
        if (databaseRecords.Sensor2Records.Count > 0)
        {
          foreach (Sensor2Record sensor2Record in databaseRecords.Sensor2Records)
          {
            if (sensor2Record.ReceivedTimeUtc >= startInternalTime)
            {
              if (sensor2Record.ReceivedTimeUtc <= finishInternalTime)
                ++num;
              else
                break;
            }
          }
        }
        sensorSession.CountCapturedSensorRecords = num;
      }
    }

    public static void CalculatePriorCalibrationForMeters(R2DatabaseRecords databaseRecords)
    {
      int num = 0;
      DateTime dateTime = DateTime.MinValue;
      foreach (MeterRecord meterRecord in databaseRecords.MeterRecords)
      {
        if ((int) meterRecord.MeterFlagCode != 65151 && (int) meterRecord.GlucoseValue > 0)
        {
          Trace.Assert(meterRecord.GlucoseTimeStamp > dateTime, "Failed assertion that meter time stamps are strictly increasing.");
          dateTime = meterRecord.GlucoseTimeStamp;
          for (int index = num; index < databaseRecords.STSCalibrationRecords.Count; ++index)
          {
            STSCalibrationRecord calibrationRecord1 = databaseRecords.STSCalibrationRecords[index];
            STSCalibrationRecord calibrationRecord2 = (STSCalibrationRecord) null;
            if (index + 1 < databaseRecords.STSCalibrationRecords.Count)
              calibrationRecord2 = databaseRecords.STSCalibrationRecords[index + 1];
            if (dateTime >= calibrationRecord1.TimeStampUtc)
            {
              if (calibrationRecord2 == null)
              {
                num = index;
                meterRecord.PriorCalibrationRecord = calibrationRecord1;
                break;
              }
              else if (dateTime < calibrationRecord2.TimeStampUtc)
              {
                num = index;
                meterRecord.PriorCalibrationRecord = calibrationRecord1;
                break;
              }
            }
            else
              break;
          }
        }
      }
    }

    private static bool DoesRequireEgvAdjustments(XmlElement xFirmwareHeader)
    {
      bool flag = false;
      if (xFirmwareHeader != null)
      {
        XObject xobject = new XObject(xFirmwareHeader);
        string attribute = xobject.GetAttribute("ProductString");
        if (!string.IsNullOrEmpty(attribute) && attribute.Contains("SW8286") && xobject.GetAttribute<uint>("RevisionNumber") >= 117441306U)
          flag = true;
      }
      return flag;
    }

    public static List<KeyValuePair<MeterRecord, Sensor2Record>> CalculateMatchedPairsByLookaheadForExtendedSensors(R2DatabaseRecords records, DateTime minTimeUtc, DateTime maxTimeUtc, ushort minSMBG, ushort maxSMBG, bool requiresEgvCalculation)
    {
      List<KeyValuePair<MeterRecord, Sensor2Record>> list = new List<KeyValuePair<MeterRecord, Sensor2Record>>();
      bool flag = R2ReceiverTools.DoesRequireEgvAdjustments(records.ReceiverFirmwareHeader);
      foreach (MeterRecord key in records.MeterRecords)
      {
        if ((int) key.MeterFlagCode != 65151 && (int) key.GlucoseValue >= (int) minSMBG && ((int) key.GlucoseValue <= (int) maxSMBG && key.GlucoseTimeStamp >= minTimeUtc) && key.GlucoseTimeStamp <= maxTimeUtc)
        {
          TimeSpan timeSpan1 = TimeSpan.FromMinutes(5.0);
          TimeSpan timeSpan2 = TimeSpan.FromMinutes(2.5);
          DateTime dateTime1 = key.GlucoseTimeStamp + timeSpan1 - timeSpan2;
          DateTime dateTime2 = key.GlucoseTimeStamp + timeSpan1 + timeSpan2;
          foreach (Sensor2Record sensor2Record in records.Sensor2Records)
          {
            if (sensor2Record.ReceivedTimeUtc >= dateTime1)
            {
              if (sensor2Record.ReceivedTimeUtc < dateTime2)
              {
                if (requiresEgvCalculation)
                {
                  if ((int) sensor2Record.GlucoseValue < 39)
                  {
                    if ((int) sensor2Record.GlucoseValue != 0)
                      break;
                  }
                  STSCalibrationRecord calibrationRecord = key.PriorCalibrationRecord;
                  if (calibrationRecord != null)
                  {
                    if (calibrationRecord.Slope != 0.0)
                    {
                      if ((int) calibrationRecord.AlgorithmCalibrationStateCode == 1)
                      {
                        double num1 = (double) sensor2Record.RawCounts;
                        if (!sensor2Record.IsCleanEstimatedGlucoseValue)
                          num1 = (double) (sensor2Record.FilteredCounts * 2U);
                        double num2 = (num1 - calibrationRecord.Intercept) / calibrationRecord.Slope;
                        if (flag && calibrationRecord is STSCalibration5Record)
                          num2 = (num2 <= 100.0 ? (calibrationRecord as STSCalibration5Record).SlopeAdjust * 100.0 - 100.0 + num2 : num2 * (calibrationRecord as STSCalibration5Record).SlopeAdjust) - 5.0;
                        if (num2 > 401.0)
                          num2 = 401.0;
                        if (num2 < 39.0)
                          num2 = 39.0;
                        short num3 = Convert.ToInt16(Math.Round(num2, MidpointRounding.AwayFromZero));
                        sensor2Record.MatchedPairEGV = new short?(num3);
                        list.Add(new KeyValuePair<MeterRecord, Sensor2Record>(key, sensor2Record));
                        break;
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
                else if ((int) sensor2Record.GlucoseValue >= 39)
                {
                  list.Add(new KeyValuePair<MeterRecord, Sensor2Record>(key, sensor2Record));
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
      return list;
    }

    public static List<MatchedPair> FindMatchedPairsForSensorSession(SensorSession session, ushort minSMBG, ushort maxSMBG, bool usePriorEgv)
    {
      List<MatchedPair> list = new List<MatchedPair>();
      if (session != null && session.SessionMeterRecords.Count > 0 && (usePriorEgv || session.SessionCalSetRecords.Count > 0) && session.SessionSensorRecords.Count > 0)
      {
        foreach (MeterRecord meterRecord in session.SessionMeterRecords)
        {
          MatchedPair matchedPair = new MatchedPair();
          if ((int) meterRecord.MeterValue >= (int) minSMBG && (int) meterRecord.MeterValue <= (int) maxSMBG)
          {
            matchedPair.Meter = meterRecord;
            if (usePriorEgv)
            {
              TimeSpan timeSpan = TimeSpan.FromMinutes(5.0);
              DateTime dateTime = meterRecord.MeterTime - timeSpan;
              DateTime meterTime = meterRecord.MeterTime;
              foreach (Sensor2Record sensor2Record in session.SessionSensorRecords)
              {
                if ((int) sensor2Record.GlucoseValue >= 39 && sensor2Record.SystemTime >= dateTime)
                {
                  if (sensor2Record.SystemTime <= meterTime)
                    matchedPair.Sensor = sensor2Record;
                  else
                    break;
                }
              }
            }
            if (matchedPair.Sensor != null && usePriorEgv)
            {
              matchedPair.CalculatedGlucoseValue = (ushort) matchedPair.Sensor.GlucoseValue;
              matchedPair.AccuracyRegion = R2ReceiverTools.CalculateClarkeErrorGridRegion(matchedPair);
              matchedPair.Is2020 = R2ReceiverTools.DoesMatchedPairFallWithinRange(matchedPair, 20, 20);
              list.Add(matchedPair);
            }
          }
        }
      }
      return list;
    }

    public static bool DoesPointExistInRegionE(MatchedPair matchedPair)
    {
      return ReceiverDataTools.DoesPointExistInRegionE(matchedPair.MeterValue, matchedPair.CalculatedGlucoseValue);
    }

    public static bool DoesPointExistInRegionD(MatchedPair matchedPair)
    {
      return ReceiverDataTools.DoesPointExistInRegionD(matchedPair.MeterValue, matchedPair.CalculatedGlucoseValue);
    }

    public static bool DoesPointExistInRegionC(MatchedPair matchedPair)
    {
      return ReceiverDataTools.DoesPointExistInRegionC(matchedPair.MeterValue, matchedPair.CalculatedGlucoseValue);
    }

    public static bool DoesPointExistInRegionB(MatchedPair matchedPair)
    {
      return ReceiverDataTools.DoesPointExistInRegionB(matchedPair.MeterValue, matchedPair.CalculatedGlucoseValue);
    }

    public static bool DoesPointExistInRegionA(MatchedPair matchedPair)
    {
      return !R2ReceiverTools.DoesPointExistInRegionB(matchedPair) && !R2ReceiverTools.DoesPointExistInRegionC(matchedPair) && (!R2ReceiverTools.DoesPointExistInRegionD(matchedPair) && !R2ReceiverTools.DoesPointExistInRegionE(matchedPair));
    }

    public static string CalculateClarkeErrorGridRegion(MatchedPair matchedPair)
    {
      return ReceiverDataTools.CalculateClarkeErrorGridRegion(matchedPair.MeterValue, matchedPair.CalculatedGlucoseValue);
    }

    public static bool DoesMatchedPairFallWithinRange(MatchedPair matchedPair, int thresholdPercent, int thresholdValue)
    {
      return ReceiverDataTools.DoesMatchedPairFallWithinRange(matchedPair.MeterValue, matchedPair.CalculatedGlucoseValue, thresholdPercent, thresholdValue);
    }

    public static List<double> CalculateArdListForMatchedPairs(List<MatchedPair> matchedPairs)
    {
      List<double> list = new List<double>();
      foreach (MatchedPair matchedPair in matchedPairs)
      {
        ushort calculatedGlucoseValue = matchedPair.CalculatedGlucoseValue;
        if ((int) calculatedGlucoseValue >= 40 && (int) calculatedGlucoseValue <= 400)
        {
          double num = Math.Abs(R2ReceiverTools.CalculateRDForMatchedPair(matchedPair));
          list.Add(num);
        }
      }
      return list;
    }

    public static List<double> CalculateRDListForMatchedPairs(List<MatchedPair> matchedPairs)
    {
      List<double> list = new List<double>();
      foreach (MatchedPair matchedPair in matchedPairs)
      {
        ushort calculatedGlucoseValue = matchedPair.CalculatedGlucoseValue;
        if ((int) calculatedGlucoseValue >= 40 && (int) calculatedGlucoseValue <= 400)
        {
          double num = R2ReceiverTools.CalculateRDForMatchedPair(matchedPair);
          list.Add(num);
        }
      }
      return list;
    }

    public static double CalculateArdForMatchedPair(MatchedPair matchedPair)
    {
      return Math.Abs(((double) matchedPair.CalculatedGlucoseValue - (double) matchedPair.MeterValue) / (double) matchedPair.MeterValue);
    }

    public static double CalculateRDForMatchedPair(MatchedPair matchedPair)
    {
      return ((double) matchedPair.CalculatedGlucoseValue - (double) matchedPair.MeterValue) / (double) matchedPair.MeterValue;
    }
  }
}

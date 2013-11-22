// Type: Dexcom.ReceiverApi.ReceiverTools
// Assembly: Dexcom.ReceiverCommercialApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 3F623F65-908D-40A7-87D3-892C5D1FFA2E
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.ReceiverCommercialApi.dll

using Dexcom.Common;
using Dexcom.Common.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;

namespace Dexcom.ReceiverApi
{
  public class ReceiverTools
  {
    public static object DatabaseRecordFactory(ReceiverRecordType recordType, int revision, byte[] data, int offset)
    {
      Type typeFromRecordType = ReceiverTools.GetReceiverStructTypeFromRecordType(recordType, revision);
      if (!(typeFromRecordType != (Type) null))
        throw new DexComException(string.Format("Unknown record type {0:X}. Offset = 0x{1:X}", (object) recordType, (object) offset));
      object obj = DataTools.ConvertBytesToObject(data, offset, typeFromRecordType);
      IGenericReceiverRecord genericReceiverRecord = obj as IGenericReceiverRecord;
      if (genericReceiverRecord == null)
        throw new DexComException(string.Format("Could not create record from data. Offset = 0x{0:X}", (object) offset));
      if ((int) Crc.CalculateCrc16(data, offset, offset + Marshal.OffsetOf(typeFromRecordType, "m_crc").ToInt32()) != (int) genericReceiverRecord.RecordedCrc)
        throw new DexComException(string.Format("Bad CRC in record {0}, Offset = 0x{1:X}", (object) typeFromRecordType.Name, (object) offset));
      else
        return obj;
    }

    public static Type GetReceiverStructTypeFromRecordType(ReceiverRecordType recordType, int revision)
    {
      Type type = (Type) null;
      switch (recordType)
      {
        case ReceiverRecordType.ManufacturingData:
          if (revision == 1)
          {
            type = typeof (ReceiverManufacturingParameterRecord);
            break;
          }
          else
            break;
        case ReceiverRecordType.PCSoftwareParameter:
          if (revision == 1)
          {
            type = typeof (ReceiverPCParameterRecord);
            break;
          }
          else
            break;
        case ReceiverRecordType.EGVData:
          switch (revision)
          {
            case 1:
              type = typeof (ReceiverEGVRecord);
              break;
            case 2:
              type = typeof (ReceiverEGVRecord);
              break;
          }
        case ReceiverRecordType.InsertionTime:
          if (revision == 1)
          {
            type = typeof (ReceiverInsertionTimeRecord);
            break;
          }
          else
            break;
        case ReceiverRecordType.MeterData:
          if (revision == 1)
          {
            type = typeof (ReceiverMeterRecord);
            break;
          }
          else
            break;
        case ReceiverRecordType.UserEventData:
          if (revision == 1)
          {
            type = typeof (ReceiverUserEventRecord);
            break;
          }
          else
            break;
        default:
          throw new DexComException("Unknow record type requested in GetReceiverStructTypeFromRecordType.");
      }
      if (type == (Type) null)
        throw new DexComException(string.Format("Revision {0} of {1} record is not supported by this version of software.", (object) revision, (object) ((object) recordType).ToString()));
      else
        return type;
    }

    public static Type GetReceiverClassTypeFromRecordType(ReceiverRecordType recordType)
    {
      switch (recordType)
      {
        case ReceiverRecordType.ManufacturingData:
          return typeof (ManufacturingParameterRecord);
        case ReceiverRecordType.PCSoftwareParameter:
          return typeof (PCParameterRecord);
        case ReceiverRecordType.EGVData:
          return typeof (EGVRecord);
        case ReceiverRecordType.InsertionTime:
          return typeof (InsertionTimeRecord);
        case ReceiverRecordType.MeterData:
          return typeof (MeterRecord);
        case ReceiverRecordType.UserEventData:
          return typeof (EventRecord);
        default:
          throw new DexComException("Unknow record type requested in GetReceiverClassTypeFromRecordType.");
      }
    }

    public static int GetLatestSupportedRecordRevision(ReceiverRecordType recordType)
    {
      switch (recordType)
      {
        case ReceiverRecordType.ManufacturingData:
          return 1;
        case ReceiverRecordType.PCSoftwareParameter:
          return 1;
        case ReceiverRecordType.EGVData:
          return 2;
        case ReceiverRecordType.InsertionTime:
          return 1;
        case ReceiverRecordType.MeterData:
          return 1;
        case ReceiverRecordType.UserEventData:
          return 1;
        default:
          throw new DexComException("Unknow record type requested in GetLatestSupportedRecordRevision.");
      }
    }

    public static bool IsSupportedRecordRevision(ReceiverRecordType recordType, int revision)
    {
      switch (recordType)
      {
        case ReceiverRecordType.ManufacturingData:
          return revision >= 1 && revision <= 1;
        case ReceiverRecordType.PCSoftwareParameter:
          return revision >= 1 && revision <= 1;
        case ReceiverRecordType.EGVData:
          return revision >= 1 && revision <= 2;
        case ReceiverRecordType.InsertionTime:
          return revision >= 1 && revision <= 1;
        case ReceiverRecordType.MeterData:
          return revision >= 1 && revision <= 1;
        case ReceiverRecordType.UserEventData:
          return revision >= 1 && revision <= 1;
        default:
          throw new DexComException("Unknow record type requested in IsSupportedRecordRevision.");
      }
    }

    public static int GetRecordSize(ReceiverRecordType recordType, int revision)
    {
      int num = -1;
      switch (recordType)
      {
        case ReceiverRecordType.ManufacturingData:
          if (revision == 1)
          {
            num = Marshal.SizeOf(typeof (ReceiverManufacturingParameterRecord));
            break;
          }
          else
            break;
        case ReceiverRecordType.PCSoftwareParameter:
          if (revision == 1)
          {
            num = Marshal.SizeOf(typeof (ReceiverPCParameterRecord));
            break;
          }
          else
            break;
        case ReceiverRecordType.EGVData:
          switch (revision)
          {
            case 1:
              num = Marshal.SizeOf(typeof (ReceiverEGVRecord));
              break;
            case 2:
              num = Marshal.SizeOf(typeof (ReceiverEGVRecord));
              break;
          }
        case ReceiverRecordType.InsertionTime:
          if (revision == 1)
          {
            num = Marshal.SizeOf(typeof (ReceiverInsertionTimeRecord));
            break;
          }
          else
            break;
        case ReceiverRecordType.MeterData:
          if (revision == 1)
          {
            num = Marshal.SizeOf(typeof (ReceiverMeterRecord));
            break;
          }
          else
            break;
        case ReceiverRecordType.UserEventData:
          if (revision == 1)
          {
            num = Marshal.SizeOf(typeof (ReceiverUserEventRecord));
            break;
          }
          else
            break;
        default:
          throw new DexComException("Unknow record type requested in GetRecordSize.");
      }
      if (num == -1)
        throw new DexComException(string.Format("Revision {0} of {1} record is not supported by this version of software.", (object) revision, (object) ((object) recordType).ToString()));
      else
        return num;
    }

    public static bool DoesReceiverRecordTypeMatchFlags(ReceiverRecordType recordType, ReceiverRecordTypeFlags flags)
    {
      ReceiverRecordTypeFlags receiverRecordType = ReceiverTools.GetReceiverRecordTypeFlagFromReceiverRecordType(recordType);
      return (flags & receiverRecordType) == receiverRecordType;
    }

    public static ReceiverRecordTypeFlags GetReceiverRecordTypeFlagFromReceiverRecordType(ReceiverRecordType recordType)
    {
      switch (recordType)
      {
        case ReceiverRecordType.ManufacturingData:
          return ReceiverRecordTypeFlags.ManufacturingData;
        case ReceiverRecordType.FirmwareParameterData:
          return ReceiverRecordTypeFlags.FirmwareParameterData;
        case ReceiverRecordType.PCSoftwareParameter:
          return ReceiverRecordTypeFlags.PCSoftwareParameter;
        case ReceiverRecordType.SensorData:
          return ReceiverRecordTypeFlags.SensorData;
        case ReceiverRecordType.EGVData:
          return ReceiverRecordTypeFlags.EGVData;
        case ReceiverRecordType.CalSet:
          return ReceiverRecordTypeFlags.CalSet;
        case ReceiverRecordType.Aberration:
          return ReceiverRecordTypeFlags.Aberration;
        case ReceiverRecordType.InsertionTime:
          return ReceiverRecordTypeFlags.InsertionTime;
        case ReceiverRecordType.ReceiverLogData:
          return ReceiverRecordTypeFlags.ReceiverLogData;
        case ReceiverRecordType.ReceiverErrorData:
          return ReceiverRecordTypeFlags.ReceiverErrorData;
        case ReceiverRecordType.MeterData:
          return ReceiverRecordTypeFlags.MeterData;
        case ReceiverRecordType.UserEventData:
          return ReceiverRecordTypeFlags.UserEventData;
        case ReceiverRecordType.UserSettingData:
          return ReceiverRecordTypeFlags.UserSettingData;
        default:
          throw new DexComException("Unknow record type requested in GetReceiverRecordTypeFlagFromReceiverRecordType.");
      }
    }

    public static List<T> ParsePage<T>(DatabasePage page) where T : IGenericRecord
    {
      return ReceiverTools.ParsePage<T>(page.PageHeader, page.PageData);
    }

    public static List<T> ParsePage<T>(DatabasePageHeader header, byte[] data) where T : IGenericRecord
    {
      return ReceiverTools.ParsePage<T>(header.RecordType, header.PageNumber, header.FirstRecordIndex, header.NumberOfRecords, header.Revision, data);
    }

    public static List<T> ParsePage<T>(XPageHeader header, byte[] data) where T : IGenericRecord
    {
      return ReceiverTools.ParsePage<T>((ReceiverRecordType) header.RecordTypeId, header.PageNumber, header.FirstRecordIndex, header.NumberOfRecords, header.RecordRevision, data);
    }

    public static List<T> ParsePage<T>(ReceiverRecordType recordType, uint pageNumber, uint firstRecordIndex, uint numberOfRecords, byte recordRevision, byte[] data) where T : IGenericRecord
    {
      List<T> list = new List<T>();
      if ((int) firstRecordIndex != -1)
      {
        int recordSize = ReceiverTools.GetRecordSize(recordType, (int) recordRevision);
        int num1 = 0;
        try
        {
          for (uint index = 0U; index < numberOfRecords; ++index)
          {
            T obj = (T) Activator.CreateInstance(ReceiverTools.GetReceiverClassTypeFromRecordType(recordType), (object) data, (object) num1, (object) recordRevision);
            uint num2 = firstRecordIndex + index;
            obj.RecordNumber = num2;
            obj.PageNumber = pageNumber;
            list.Add(obj);
            num1 += recordSize;
          }
        }
        catch (TargetInvocationException ex)
        {
          DexComException dexComException = ex.InnerException as DexComException;
          if (dexComException != null)
            throw dexComException;
          throw;
        }
      }
      return list;
    }

    public static string ConvertGen4TransmitterNumberToCode(uint number)
    {
      if (number > 16777215U)
        throw new DexComException("Transmitter number too large!");
      StringBuilder stringBuilder = new StringBuilder();
      for (int index1 = 0; index1 < 5; ++index1)
      {
        int index2 = (int) number & 31;
        stringBuilder.Insert(0, ReceiverValues.TransmitterIdValidChars[index2]);
        number >>= 5;
      }
      return ((object) stringBuilder).ToString();
    }

    public static uint ConvertGen4TransmitterCodeToNumber(string code)
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
      uint num = 0U;
      for (int index = 0; index < code.Length; ++index)
        num = (num << 5) + (uint) ReceiverValues.TransmitterIdValidChars.IndexOf(code[index]);
      return num;
    }

    public static bool IsValidGen4TransmitterCode(string transmitterCode)
    {
      bool flag = !string.IsNullOrEmpty(transmitterCode) && transmitterCode.Length == 5 && (transmitterCode.StartsWith("6") || transmitterCode.StartsWith("7"));
      if (flag)
      {
        if (transmitterCode.ToUpperInvariant().IndexOfAny(new char[4]
        {
          'I',
          'O',
          'V',
          'Z'
        }) >= 0)
          flag = false;
      }
      return flag;
    }

    public static uint ConvertDateTimeToReceiverTime(DateTime input)
    {
      if (input < ReceiverValues.ReceiverBaseDateTime)
        throw new DexComException("Input date is less than the Receiver base date time");
      else
        return Convert.ToUInt32((input - ReceiverValues.ReceiverBaseDateTime).TotalSeconds);
    }

    public static DateTime ConvertReceiverTimeToDateTime(uint input)
    {
      return ReceiverValues.ReceiverBaseDateTime.AddSeconds((double) input);
    }

    public static string PrintPacket(byte[] bytesToPrint)
    {
      if (bytesToPrint == null)
        return string.Empty;
      string str = string.Empty;
      for (int index = 0; index < bytesToPrint.Length; ++index)
        str = str + string.Format("{0:X2} ", (object) Convert.ToInt32(bytesToPrint[index]));
      return str;
    }

    public static ReceiverCommands GetReceiverCommandFromByte(byte commandByte)
    {
      ReceiverCommands receiverCommands = ReceiverCommands.Null;
      if (Enum.IsDefined(typeof (ReceiverCommands), (object) commandByte))
        receiverCommands = (ReceiverCommands) commandByte;
      return receiverCommands;
    }

    public static string GetReceiverCommandStringFromByte(byte command)
    {
      if (Enum.IsDefined(typeof (ReceiverCommands), (object) command))
        return ((object) (ReceiverCommands) command).ToString();
      else
        return string.Format("UNKNOWN byte {0}(0x{1:X2})", (object) command, (object) command);
    }

    public static XmlDocument ExpandReceiverDownload(XmlDocument xDownload)
    {
      XmlDocument xmlDocument = new XmlDocument();
      if (xDownload != null && xDownload.DocumentElement != null)
      {
        xmlDocument.LoadXml(xDownload.OuterXml);
        ReceiverDatabaseRecordsParser databaseRecordsParser = new ReceiverDatabaseRecordsParser();
        databaseRecordsParser.Parse(xmlDocument);
        XObject xobject = new XObject("ExpandedDatabase", xmlDocument);
        xobject.SetAttribute("DateTimeCreated", DateTimeOffset.Now);
        xobject.AppendChild(ReceiverTools.ExpandRecords(Enumerable.Cast<IGenericRecord>((IEnumerable) databaseRecordsParser.ManufacturingRecords), "ManufacturingRecords", xmlDocument));
        xobject.AppendChild(ReceiverTools.ExpandRecords(Enumerable.Cast<IGenericRecord>((IEnumerable) databaseRecordsParser.PCParameterRecords), "PCParameterRecords", xmlDocument));
        xobject.AppendChild(ReceiverTools.ExpandRecords(Enumerable.Cast<IGenericRecord>((IEnumerable) databaseRecordsParser.EgvRecords), "EgvRecords", xmlDocument));
        xobject.AppendChild(ReceiverTools.ExpandRecords(Enumerable.Cast<IGenericRecord>((IEnumerable) databaseRecordsParser.InsertionTimeRecords), "InsertionTimeRecords", xmlDocument));
        xobject.AppendChild(ReceiverTools.ExpandRecords(Enumerable.Cast<IGenericRecord>((IEnumerable) databaseRecordsParser.MeterRecords), "MeterRecords", xmlDocument));
        xobject.AppendChild(ReceiverTools.ExpandRecords(Enumerable.Cast<IGenericRecord>((IEnumerable) databaseRecordsParser.EventRecords), "EventRecords", xmlDocument));
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

    public static byte[] GetDexCom64BitVCP1002DriverExecutable()
    {
      return ReceiverTools.GetResourceStreamFromThisAssembly("Dexcom.ReceiverApi.DexCom64VCP1002.exe");
    }

    public static byte[] GetDexCom32BitVCP1002DriverExecutable()
    {
      return ReceiverTools.GetResourceStreamFromThisAssembly("Dexcom.ReceiverApi.DexCom32VCP1002.exe");
    }

    public static byte[] GetResourceStreamFromThisAssembly(string resourceName)
    {
      Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
      byte[] buffer = (byte[]) null;
      if (manifestResourceStream == null)
        throw new DexComException(string.Format("Assembly resource missing '{0}' content.", (object) resourceName));
      using (manifestResourceStream)
      {
        buffer = new byte[manifestResourceStream.Length];
        manifestResourceStream.Read(buffer, 0, buffer.Length);
      }
      return buffer;
    }

    public static bool IsDexComVCP1002DriverInstalled()
    {
      return GlobalReceiverRegistryTools.IsDexComVcp1002DriverInstalled();
    }
  }
}

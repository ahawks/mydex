// Type: Dexcom.Common.Data.ReceiverDataTools
// Assembly: Dexcom.Common.Data, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 64B9BFC3-C891-4315-B216-574914861A80
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.Data.dll

using Dexcom.Common;
using System;
using System.IO;
using System.Xml;

namespace Dexcom.Common.Data
{
  public static class ReceiverDataTools
  {
    public static ReceiverDataTools.ReceiverFileType GetReceiverFileType(XmlDocument xDocument)
    {
      ReceiverDataTools.ReceiverFileType receiverFileType = ReceiverDataTools.ReceiverFileType.Unknown;
      if (xDocument != null)
      {
        XObject xobject = new XObject(xDocument.DocumentElement);
        if (xobject.IsNotNull())
        {
          string name = xobject.Element.Name;
          if (name == "ReceiverDownload")
          {
            if (xobject.HasAttribute("DownloadType"))
              receiverFileType = !(xobject.GetAttribute<string>("DownloadType") == "Merged") ? ReceiverDataTools.ReceiverFileType.ReceiverDownload : ReceiverDataTools.ReceiverFileType.Receiver;
            else if (xobject.HasAttribute("IsMergedReceiver"))
              receiverFileType = ReceiverDataTools.ReceiverFileType.R2Receiver;
            else if (xobject.HasXPathAttribute("ReceiverData/FirmwareHeader/@Revision"))
              receiverFileType = ReceiverDataTools.ReceiverFileType.R2Download;
          }
          else
            receiverFileType = !(name == "PatientFile") ? ReceiverDataTools.ReceiverFileType.Unknown : ReceiverDataTools.ReceiverFileType.Patient;
        }
      }
      return receiverFileType;
    }

    public static XmlDocument ExtractReceiverFileFromStudioPatientFile(XmlDocument xPatientDoc)
    {
      XmlDocument xmlDocument1 = (XmlDocument) null;
      string xpath = string.Format("DeviceData/ReceiverFiles/{0}", (object) "FileInfo");
      XFileInfo xFileInfo = new XFileInfo(xPatientDoc.DocumentElement.SelectSingleNode(xpath) as XmlElement);
      if (xFileInfo.IsNotNull())
      {
        byte[] fileContents = DataTools.ExtractFileContents(xFileInfo);
        XmlDocument xmlDocument2 = new XmlDocument();
        xmlDocument2.PreserveWhitespace = true;
        using (MemoryStream memoryStream = new MemoryStream(fileContents))
        {
          xmlDocument2.Load((Stream) memoryStream);
          xmlDocument1 = xmlDocument2;
        }
      }
      return xmlDocument1;
    }

    public static int CountDownloadFilesInStudioPatientFile(XmlDocument xPatientDoc)
    {
      string xpath = string.Format("DeviceData/DownloadFiles/{0}", (object) "FileInfo");
      return xPatientDoc.DocumentElement.SelectNodes(xpath).Count;
    }

    public static XmlDocument ExtractDownloadFileFromStudioPatientFile(XmlDocument xPatientDoc, int downloadIndex)
    {
      XmlDocument xmlDocument1 = (XmlDocument) null;
      string xpath = string.Format("DeviceData/DownloadFiles/{1}[{0}]", (object) (downloadIndex + 1), (object) "FileInfo");
      XFileInfo xFileInfo = new XFileInfo(xPatientDoc.DocumentElement.SelectSingleNode(xpath) as XmlElement);
      if (xFileInfo.IsNotNull())
      {
        byte[] fileContents = DataTools.ExtractFileContents(xFileInfo);
        XmlDocument xmlDocument2 = new XmlDocument();
        xmlDocument2.PreserveWhitespace = true;
        using (MemoryStream memoryStream = new MemoryStream(fileContents))
        {
          xmlDocument2.Load((Stream) memoryStream);
          xmlDocument1 = xmlDocument2;
        }
      }
      return xmlDocument1;
    }

    public static byte[] ExtractR2ErrorLogFromStudioPatientFile(XmlDocument xPatient)
    {
      byte[] numArray = (byte[]) null;
      XmlDocument studioPatientFile1 = ReceiverDataTools.ExtractReceiverFileFromStudioPatientFile(xPatient);
      if (studioPatientFile1 != null && ReceiverDataTools.GetReceiverFileType(studioPatientFile1) == ReceiverDataTools.ReceiverFileType.R2Receiver)
      {
        numArray = ReceiverDataTools.ExtractR2ReceiverDataFromDownloadFile(studioPatientFile1, "ErrorLog");
        if (numArray != null && numArray.Length == 0)
          numArray = (byte[]) null;
        XObject xobject1 = new XObject(studioPatientFile1.DocumentElement);
        if (numArray == null && xobject1.HasXPathAttribute("ReceiverData/@HasErrorLogInfo") && xobject1.GetXPathAttribute<uint>("ReceiverData/@NumberOfErrors") > 0U)
        {
          int num = ReceiverDataTools.CountDownloadFilesInStudioPatientFile(xPatient);
          if (num > 0)
          {
            for (int downloadIndex = num - 1; downloadIndex >= 0; --downloadIndex)
            {
              XmlDocument studioPatientFile2 = ReceiverDataTools.ExtractDownloadFileFromStudioPatientFile(xPatient, downloadIndex);
              if (studioPatientFile2 != null)
              {
                XObject xobject2 = new XObject(studioPatientFile2.DocumentElement);
                numArray = ReceiverDataTools.ExtractR2ReceiverDataFromDownloadFile(studioPatientFile2, "ErrorLog");
                if (numArray != null && numArray.Length == 0)
                  numArray = (byte[]) null;
                if (numArray != null)
                  break;
              }
            }
          }
        }
      }
      return numArray;
    }

    public static byte[] ExtractR2ReceiverDataFromDownloadFile(XmlDocument xDownload, string sectionName)
    {
      XObject xobject = new XObject((XmlElement) xDownload.SelectSingleNode("/ReceiverDownload/ReceiverData/" + sectionName));
      if (!xobject.IsNotNull())
        throw new ApplicationException("Name of section (element) not found in ReceiverData.");
      if (DataTools.IsCompressedElement(xobject.Element))
        return DataTools.DecompressElement(xobject.Element);
      else
        return Convert.FromBase64String(xobject.Element.InnerText);
    }

    public static string ConvertR2ReceiverNumberToSerialNumberString(uint receiverNumber)
    {
      return string.Format("{0:000000}", (object) receiverNumber);
    }

    public static XReceiverRecord CreateXReceiverRecordFromXmlDocument(XmlDocument xReceiverDoc)
    {
      ReceiverDataTools.ReceiverFileType receiverFileType = ReceiverDataTools.GetReceiverFileType(xReceiverDoc);
      bool flag1 = receiverFileType == ReceiverDataTools.ReceiverFileType.R2Download || receiverFileType == ReceiverDataTools.ReceiverFileType.R2Receiver;
      bool flag2 = receiverFileType == ReceiverDataTools.ReceiverFileType.ReceiverDownload || receiverFileType == ReceiverDataTools.ReceiverFileType.Receiver;
      XReceiverRecord xreceiverRecord = (XReceiverRecord) null;
      if (flag2)
        xreceiverRecord = ReceiverDataTools.DoCreateG4XReceiverRecordFromXmlDocument(xReceiverDoc);
      if (flag1)
        xreceiverRecord = ReceiverDataTools.DoCreateR2XReceiverRecordFromXmlDocument(xReceiverDoc);
      return xreceiverRecord;
    }

    private static XReceiverRecord DoCreateG4XReceiverRecordFromXmlDocument(XmlDocument xReceiverDoc)
    {
      ReceiverDataTools.ReceiverFileType receiverFileType = ReceiverDataTools.GetReceiverFileType(xReceiverDoc);
      XObject xobject = new XObject(xReceiverDoc.DocumentElement);
      XReceiverRecord xreceiverRecord = new XReceiverRecord();
      xreceiverRecord.ReceiverFileType = receiverFileType;
      if (xobject.HasAttribute("MergedId"))
        xreceiverRecord.MergedId = xobject.GetAttribute<Guid>("MergedId");
      xreceiverRecord.DownloadId = xobject.Id;
      if (xreceiverRecord.MergedId != Guid.Empty)
        xreceiverRecord.Id = xreceiverRecord.MergedId;
      else
        xreceiverRecord.Id = xreceiverRecord.DownloadId;
      xreceiverRecord.DateTimeCreated = xobject.GetAttribute<DateTimeOffset>("DateTimeCreated");
      if (xobject.HasAttribute("ReceiverId"))
        xreceiverRecord.ReceiverDatabaseId = xobject.GetAttribute<Guid>("ReceiverId");
      if (xobject.HasXPathAttribute("/ReceiverDownload/Database/@PageCount"))
        xreceiverRecord.PageCount = xobject.GetXPathAttribute<int>("/ReceiverDownload/Database/@PageCount");
      if (xobject.HasXPathAttribute("/ReceiverDownload/DownloadHistory/@Count"))
        xreceiverRecord.DownloadCount = xobject.GetXPathAttribute<int>("/ReceiverDownload/DownloadHistory/@Count");
      xreceiverRecord.SerialNumber = xobject.GetAttribute<string>("SerialNumber");
      if (xobject.HasXPathAttribute("/ReceiverDownload/ReceiverSettings/@TransmitterId"))
        xreceiverRecord.TransmitterId = xobject.GetXPathAttribute<string>("/ReceiverDownload/ReceiverSettings/@TransmitterId");
      xreceiverRecord.DownloadType = xobject.GetAttribute<string>("DownloadType");
      if (xobject.HasXPathAttribute("/ReceiverDownload/ApplicationInfo/@ProductName"))
        xreceiverRecord.PCProductName = xobject.GetXPathAttribute<string>("/ReceiverDownload/ApplicationInfo/@ProductName");
      if (xobject.HasXPathAttribute("/ReceiverDownload/ApplicationInfo/@ProductVersion"))
        xreceiverRecord.PCProductVersion = xobject.GetXPathAttribute<string>("/ReceiverDownload/ApplicationInfo/@ProductVersion");
      if (xobject.HasXPathAttribute("/ReceiverDownload/FirmwareHeader/@ApiVersion"))
        xreceiverRecord.ApiVersion = xobject.GetXPathAttribute<string>("/ReceiverDownload/FirmwareHeader/@ApiVersion");
      if (xobject.HasXPathAttribute("/ReceiverDownload/FirmwareHeader/@ProductId"))
        xreceiverRecord.ProductId = xobject.GetXPathAttribute<string>("/ReceiverDownload/FirmwareHeader/@ProductId");
      if (xobject.HasXPathAttribute("/ReceiverDownload/FirmwareHeader/@ProductName"))
        xreceiverRecord.ProductName = xobject.GetXPathAttribute<string>("/ReceiverDownload/FirmwareHeader/@ProductName");
      if (xobject.HasXPathAttribute("/ReceiverDownload/FirmwareHeader/@SoftwareNumber"))
        xreceiverRecord.SoftwareNumber = xobject.GetXPathAttribute<string>("/ReceiverDownload/FirmwareHeader/@SoftwareNumber");
      if (xobject.HasXPathAttribute("/ReceiverDownload/FirmwareHeader/@FirmwareVersion"))
        xreceiverRecord.FirmwareVersion = xobject.GetXPathAttribute<string>("/ReceiverDownload/FirmwareHeader/@FirmwareVersion");
      if (xobject.HasXPathAttribute("/ReceiverDownload/FirmwareHeader/@PortVersion"))
        xreceiverRecord.PortVersion = xobject.GetXPathAttribute<string>("/ReceiverDownload/FirmwareHeader/@PortVersion");
      if (xobject.HasXPathAttribute("/ReceiverDownload/FirmwareHeader/@RFVersion"))
        xreceiverRecord.RFVersion = xobject.GetXPathAttribute<string>("/ReceiverDownload/FirmwareHeader/@RFVersion");
      if (xobject.HasXPathAttribute("/ReceiverDownload/FirmwareHeader/@DexBootVersion"))
        xreceiverRecord.DexBootVersion = xobject.GetXPathAttribute<string>("/ReceiverDownload/FirmwareHeader/@DexBootVersion");
      return xreceiverRecord;
    }

    private static XReceiverRecord DoCreateR2XReceiverRecordFromXmlDocument(XmlDocument xReceiverDoc)
    {
      ReceiverDataTools.ReceiverFileType receiverFileType = ReceiverDataTools.GetReceiverFileType(xReceiverDoc);
      XObject xobject = new XObject(xReceiverDoc.DocumentElement);
      XReceiverRecord xreceiverRecord = new XReceiverRecord();
      xreceiverRecord.ReceiverFileType = receiverFileType;
      if (xobject.HasAttribute("MergedId"))
        xreceiverRecord.MergedId = xobject.GetAttribute<Guid>("MergedId");
      xreceiverRecord.DownloadId = xobject.Id;
      if (xreceiverRecord.MergedId != Guid.Empty)
        xreceiverRecord.Id = xreceiverRecord.MergedId;
      else
        xreceiverRecord.Id = xreceiverRecord.DownloadId;
      DateTime attribute1 = xobject.GetAttribute<DateTime>("DateTimeNowLocal");
      DateTime attribute2 = xobject.GetAttribute<DateTime>("DateTimeNowUTC");
      xreceiverRecord.DateTimeCreated = new DateTimeOffset(attribute1, attribute1 - attribute2);
      if (xobject.HasXPathAttribute("/ReceiverDownload/ReceiverData/HardwareConfig/@ReceiverInstanceId"))
        xreceiverRecord.ReceiverDatabaseId = xobject.GetXPathAttribute<Guid>("/ReceiverDownload/ReceiverData/HardwareConfig/@ReceiverInstanceId");
      XmlNodeList xmlNodeList = xobject.Element.SelectNodes("/ReceiverDownload/ReceiverData/DatabaseBlockInfo/BlockInfo");
      xreceiverRecord.PageCount = xmlNodeList.Count;
      if (xobject.HasXPathAttribute("/ReceiverDownload/ReceiverData/DownloadHistory/@Count"))
        xreceiverRecord.DownloadCount = xobject.GetXPathAttribute<int>("/ReceiverDownload/ReceiverData/DownloadHistory/@Count");
      if (xobject.HasXPathAttribute("/ReceiverDownload/ReceiverData/HardwareConfig/@ReceiverId"))
      {
        uint xpathAttribute = xobject.GetXPathAttribute<uint>("/ReceiverDownload/ReceiverData/HardwareConfig/@ReceiverId");
        xreceiverRecord.SerialNumber = ReceiverDataTools.ConvertR2ReceiverNumberToSerialNumberString(xpathAttribute);
      }
      if (xobject.HasXPathAttribute("/ReceiverDownload/ReceiverData/LastSettingsRecord/@TransmitterCode"))
        xreceiverRecord.TransmitterId = xobject.GetXPathAttribute<string>("/ReceiverDownload/ReceiverData/LastSettingsRecord/@TransmitterCode");
      if (receiverFileType == ReceiverDataTools.ReceiverFileType.R2Receiver)
        xreceiverRecord.DownloadType = "Merged";
      if (receiverFileType == ReceiverDataTools.ReceiverFileType.R2Download)
        xreceiverRecord.DownloadType = "Incremental";
      if (xobject.HasXPathAttribute("/ReceiverDownload/ApplicationInfo/@ProductName"))
        xreceiverRecord.PCProductName = xobject.GetXPathAttribute<string>("/ReceiverDownload/ApplicationInfo/@ProductName");
      if (xobject.HasXPathAttribute("/ReceiverDownload/ApplicationInfo/@ProductVersion"))
        xreceiverRecord.PCProductVersion = xobject.GetXPathAttribute<string>("/ReceiverDownload/ApplicationInfo/@ProductVersion");
      if (xobject.HasXPathAttribute("/ReceiverDownload/ReceiverData/FirmwareHeader/@DatabaseRevision"))
        xreceiverRecord.ApiVersion = xobject.GetXPathAttribute<string>("/ReceiverDownload/ReceiverData/FirmwareHeader/@DatabaseRevision");
      if (xobject.HasXPathAttribute("/ReceiverDownload/ReceiverData/FirmwareHeader/@ProductString"))
        xreceiverRecord.SoftwareNumber = xobject.GetXPathAttribute<string>("/ReceiverDownload/ReceiverData/FirmwareHeader/@ProductString");
      if (xobject.HasXPathAttribute("/ReceiverDownload/ReceiverData/FirmwareHeader/@Revision"))
        xreceiverRecord.FirmwareVersion = xobject.GetXPathAttribute<string>("/ReceiverDownload/ReceiverData/FirmwareHeader/@Revision");
      return xreceiverRecord;
    }

    public static bool DoesPointExistInRegionE(ushort meterValue, ushort egv)
    {
      return (int) meterValue > 180 && (int) egv <= 70 || (int) meterValue <= 70 && (int) egv >= 180;
    }

    public static bool DoesPointExistInRegionD(ushort meterValue, ushort egv)
    {
      return (int) meterValue >= 240 && (int) egv > 70 && (int) egv < 180 || (int) meterValue <= 70 && (int) egv > 70 && ((int) egv < 180 && (double) egv >= 1.2 * (double) meterValue);
    }

    public static bool DoesPointExistInRegionC(ushort meterValue, ushort egv)
    {
      return (int) meterValue <= 180 && (double) egv <= 1.4 * (double) meterValue - 182.0 || (int) meterValue > 70 && (double) egv >= 1.03 * (double) meterValue + 107.9;
    }

    public static bool DoesPointExistInRegionB(ushort meterValue, ushort egv)
    {
      return !ReceiverDataTools.DoesPointExistInRegionC(meterValue, egv) && !ReceiverDataTools.DoesPointExistInRegionD(meterValue, egv) && !ReceiverDataTools.DoesPointExistInRegionE(meterValue, egv) && ((int) meterValue > 70 && (double) egv <= 0.8 * (double) meterValue || (int) meterValue > 70 && (double) egv >= 1.2 * (double) meterValue);
    }

    public static bool DoesPointExistInRegionA(ushort meterValue, ushort egv)
    {
      return !ReceiverDataTools.DoesPointExistInRegionB(meterValue, egv) && !ReceiverDataTools.DoesPointExistInRegionC(meterValue, egv) && (!ReceiverDataTools.DoesPointExistInRegionD(meterValue, egv) && !ReceiverDataTools.DoesPointExistInRegionE(meterValue, egv));
    }

    public static string CalculateClarkeErrorGridRegion(ushort meterValue, ushort egv)
    {
      string str = string.Empty;
      return !ReceiverDataTools.DoesPointExistInRegionE(meterValue, egv) ? (!ReceiverDataTools.DoesPointExistInRegionD(meterValue, egv) ? (!ReceiverDataTools.DoesPointExistInRegionC(meterValue, egv) ? (!ReceiverDataTools.DoesPointExistInRegionB(meterValue, egv) ? "A" : "B") : "C") : "D") : "E";
    }

    public static bool DoesMatchedPairFallWithinRange(ushort meterValue, ushort egv, int thresholdPercent, int thresholdValue)
    {
      if ((int) meterValue <= 80)
        return Math.Abs((int) egv - (int) meterValue) <= thresholdValue;
      else
        return Math.Abs(((int) egv - (int) meterValue) * 100 / (int) meterValue) <= thresholdPercent;
    }

    public static ushort CalculateGlucoseFromCounts(double slope, double intercept, double slope_adjust, double counts)
    {
      double num1 = (counts - intercept) / slope;
      double num2 = (num1 <= 100.0 ? slope_adjust * 100.0 - 100.0 + num1 : num1 * slope_adjust) - 5.0;
      if (num2 > 401.0)
        num2 = 401.0;
      if (num2 < 39.0)
        num2 = 39.0;
      return Convert.ToUInt16(Math.Round(num2, MidpointRounding.AwayFromZero));
    }

    public enum ReceiverFileType
    {
      Unknown,
      ReceiverDownload,
      Receiver,
      Patient,
      R2Download,
      R2Receiver,
      R2Patient,
    }
  }
}

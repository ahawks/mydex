// Type: Dexcom.ReceiverApi.DownloadReceiverHelper
// Assembly: Dexcom.ReceiverCommercialApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 3F623F65-908D-40A7-87D3-892C5D1FFA2E
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.ReceiverCommercialApi.dll

using Dexcom.Common;
using Dexcom.Common.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml;

namespace Dexcom.ReceiverApi
{
  public static class DownloadReceiverHelper
  {
    public static List<KeyValuePair<ReceiverRecordType, DatabasePageHeader>> GetPartitionPageHeadersFromReceiver()
    {
      using (ReceiverApi api = new ReceiverApi())
      {
        api.Connect();
        api.VerifyApiVersion();
        return DownloadReceiverHelper.DoGetPartitionPageHeadersFromReceiver(api, ReceiverValues.MatchAnythingReceiverRecordTypeFlags, (BackgroundWorker) null);
      }
    }

    public static List<KeyValuePair<ReceiverRecordType, DatabasePageHeader>> GetPartitionPageHeadersFromReceiver(BackgroundWorker backgroundWorker)
    {
      using (ReceiverApi api = new ReceiverApi())
      {
        api.Connect();
        api.VerifyApiVersion();
        return DownloadReceiverHelper.DoGetPartitionPageHeadersFromReceiver(api, ReceiverValues.MatchAnythingReceiverRecordTypeFlags, backgroundWorker);
      }
    }

    public static List<KeyValuePair<ReceiverRecordType, DatabasePageHeader>> GetPartitionPageHeadersFromReceiver(ReceiverApi api, ReceiverRecordTypeFlags recordsFilter, BackgroundWorker backgroundWorker)
    {
      return DownloadReceiverHelper.DoGetPartitionPageHeadersFromReceiver(api, recordsFilter, backgroundWorker);
    }

    private static List<KeyValuePair<ReceiverRecordType, DatabasePageHeader>> DoGetPartitionPageHeadersFromReceiver(ReceiverApi api, ReceiverRecordTypeFlags recordsFilter, BackgroundWorker backgroundWorker)
    {
      List<KeyValuePair<ReceiverRecordType, DatabasePageHeader>> list = new List<KeyValuePair<ReceiverRecordType, DatabasePageHeader>>();
      foreach (XPartition xpartition in api.ReadDatabasePartitionInfo().Partitions)
      {
        if (backgroundWorker != null)
        {
          if (backgroundWorker.CancellationPending)
            break;
        }
        ReceiverRecordType receiverRecordType = DownloadReceiverHelper.DoFetchAndVerifyRecordType(xpartition.Name, xpartition.Id);
        if (ReceiverTools.DoesReceiverRecordTypeMatchFlags(receiverRecordType, recordsFilter))
        {
          DatabasePageRange databasePageRange = api.ReadDatabasePageRange(receiverRecordType);
          if ((int) databasePageRange.LastPage != -1)
          {
            for (uint pageNumber = databasePageRange.FirstPage; pageNumber <= databasePageRange.LastPage; ++pageNumber)
            {
              DatabasePageHeader databasePageHeader = api.ReadDatabasePageHeader(receiverRecordType, pageNumber);
              list.Add(new KeyValuePair<ReceiverRecordType, DatabasePageHeader>(receiverRecordType, databasePageHeader));
            }
          }
        }
      }
      return list;
    }

    public static List<KeyValuePair<ReceiverRecordType, XPageHeader>> GetPageHeadersFromDownload(XmlElement xDownload)
    {
      List<KeyValuePair<ReceiverRecordType, XPageHeader>> list = new List<KeyValuePair<ReceiverRecordType, XPageHeader>>();
      XmlElement sourceElement = xDownload.SelectSingleNode("/ReceiverDownload/Database") as XmlElement;
      if (sourceElement != null)
      {
        if (DataTools.IsCompressedElement(sourceElement))
          sourceElement = DataTools.DecompressElementWithHint(sourceElement) as XmlElement;
        foreach (XmlElement element in sourceElement.SelectNodes("Pages/Page/PageHeader"))
        {
          XPageHeader xpageHeader = new XPageHeader(element);
          ReceiverRecordType key = DownloadReceiverHelper.DoFetchAndVerifyRecordType(xpageHeader.GetAttribute<string>("RecordType"), xpageHeader.GetAttribute<byte>("RecordTypeId"));
          list.Add(new KeyValuePair<ReceiverRecordType, XPageHeader>(key, xpageHeader));
        }
      }
      return list;
    }

    private static ReceiverRecordType DoFetchAndVerifyRecordType(string recordTypeString, byte recordTypeId)
    {
      ReceiverRecordType receiverRecordType = (ReceiverRecordType) recordTypeId;
      bool flag1 = Enum.IsDefined(typeof (ReceiverRecordType), (object) recordTypeString);
      bool flag2 = Enum.IsDefined(typeof (ReceiverRecordType), (object) recordTypeId);
      if (flag2 || flag1)
      {
        if (flag2 && flag1)
        {
          if (recordTypeString != ((object) receiverRecordType).ToString())
            throw new DexComException(string.Format("Encountered mismatched ReceiverRecordType: Partition Name='{0}' != Internal Name='{1}'.", (object) recordTypeString, (object) ((object) receiverRecordType).ToString()));
        }
        else
        {
          if (flag1)
            throw new DexComException(string.Format("Encountered unknown ReceiverRecordType id='{0}' in database partition.", (object) recordTypeId));
          if (flag2)
            throw new DexComException(string.Format("Encountered unknown ReceiverRecordType name='{0}' in database partition.", (object) recordTypeString));
          Trace.Assert(false);
        }
      }
      return receiverRecordType;
    }

    private static int DoCountDatabasePagesInReceiver(ReceiverApi api, ReceiverRecordTypeFlags recordsFilter, BackgroundWorker backgroundWorker)
    {
      int num = 0;
      foreach (XPartition xpartition in api.ReadDatabasePartitionInfo().Partitions)
      {
        if (backgroundWorker != null)
        {
          if (backgroundWorker.CancellationPending)
            break;
        }
        ReceiverRecordType recordType = DownloadReceiverHelper.DoFetchAndVerifyRecordType(xpartition.Name, xpartition.Id);
        if (ReceiverTools.DoesReceiverRecordTypeMatchFlags(recordType, recordsFilter))
        {
          DatabasePageRange databasePageRange = api.ReadDatabasePageRange(recordType);
          if ((int) databasePageRange.LastPage != -1)
            num += (int) databasePageRange.LastPage - (int) databasePageRange.FirstPage + 1;
        }
      }
      return num;
    }

    public static XmlDocument DownloadReceiver()
    {
      using (ReceiverApi api = new ReceiverApi())
      {
        api.Connect();
        api.VerifyApiVersion();
        return DownloadReceiverHelper.DoDownloadReceiver(api, ReceiverValues.MatchAnythingReceiverRecordTypeFlags, (List<KeyValuePair<ReceiverRecordType, XPageHeader>>) null, (BackgroundWorker) null, false);
      }
    }

    public static XmlDocument DownloadReceiver(ReceiverRecordTypeFlags recordsFilter, List<KeyValuePair<ReceiverRecordType, XPageHeader>> priorPageHeaders, BackgroundWorker backgroundWorker)
    {
      XmlDocument xmlDocument = (XmlDocument) null;
      using (ReceiverApi api = new ReceiverApi())
      {
        if (!backgroundWorker.CancellationPending)
        {
          api.Connect();
          api.VerifyApiVersion();
          xmlDocument = DownloadReceiverHelper.DoDownloadReceiver(api, recordsFilter, priorPageHeaders, backgroundWorker, false);
        }
      }
      return xmlDocument;
    }

    public static XmlDocument DownloadReceiver(ReceiverApi api, ReceiverRecordTypeFlags recordsFilter, List<KeyValuePair<ReceiverRecordType, XPageHeader>> priorPageHeaders, BackgroundWorker backgroundWorker)
    {
      return DownloadReceiverHelper.DoDownloadReceiver(api, recordsFilter, priorPageHeaders, backgroundWorker, false);
    }

    public static XmlDocument PeekAtReceiver()
    {
      using (ReceiverApi api = new ReceiverApi())
      {
        api.Connect();
        api.VerifyApiVersion();
        return DownloadReceiverHelper.DoDownloadReceiver(api, ReceiverValues.MatchAnythingReceiverRecordTypeFlags, (List<KeyValuePair<ReceiverRecordType, XPageHeader>>) null, (BackgroundWorker) null, true);
      }
    }

    public static XmlDocument PeekAtReceiver(ReceiverApi api)
    {
      return DownloadReceiverHelper.DoDownloadReceiver(api, ReceiverValues.MatchAnythingReceiverRecordTypeFlags, (List<KeyValuePair<ReceiverRecordType, XPageHeader>>) null, (BackgroundWorker) null, true);
    }

    private static XmlDocument DoDownloadReceiver(ReceiverApi api, ReceiverRecordTypeFlags recordsFilter, List<KeyValuePair<ReceiverRecordType, XPageHeader>> priorPageHeaders, BackgroundWorker backgroundWorker, bool skipDatabase)
    {
      XmlDocument xmlDocument = (XmlDocument) null;
      if (backgroundWorker == null || !backgroundWorker.CancellationPending)
      {
        DateTime now = DateTime.Now;
        XmlDocument applicationXmlDocument = DataTools.CreateApplicationXmlDocument("ReceiverDownload", false);
        if (string.Compare(Environment.UserDomainName, "DEXCOM", StringComparison.InvariantCultureIgnoreCase) != 0)
          DataTools.DeidentifyComputerInfo(applicationXmlDocument.DocumentElement);
        XObject xobject1 = new XObject(applicationXmlDocument.DocumentElement);
        xobject1.SetAttribute("SerialNumber", string.Empty);
        xobject1.SetAttribute("ReceiverId", CommonValues.NoneId);
        xobject1.SetAttribute("DownloadType", priorPageHeaders == null || priorPageHeaders.Count <= 0 ? "Normal" : "Incremental");
        double num1 = 723.0;
        XFirmwareHeader xfirmwareHeader = api.ReadFirmwareHeader();
        xobject1.Element.AppendChild(applicationXmlDocument.ImportNode((XmlNode) xfirmwareHeader.Element, true));
        if (backgroundWorker == null || !backgroundWorker.CancellationPending)
        {
          if (backgroundWorker != null)
            backgroundWorker.ReportProgress(Convert.ToInt32(1.0 / num1 * 100.0 * 10.0));
          if (api.HasManufacturingParameters())
          {
            ManufacturingParameterRecord manufacturingParameterRecord = api.ReadManufacturingParameters();
            xobject1.Element.AppendChild(applicationXmlDocument.ImportNode((XmlNode) manufacturingParameterRecord.ToXml(), true));
            xobject1.SetAttribute("SerialNumber", manufacturingParameterRecord.Parameters.SerialNumber);
          }
          if (backgroundWorker == null || !backgroundWorker.CancellationPending)
          {
            if (backgroundWorker != null)
              backgroundWorker.ReportProgress(Convert.ToInt32(2.0 / num1 * 100.0 * 10.0));
            if (api.HasPcParameters())
            {
              PCParameterRecord pcParameterRecord = api.ReadPcParameters();
              xobject1.Element.AppendChild(applicationXmlDocument.ImportNode((XmlNode) pcParameterRecord.ToXml(), true));
              xobject1.SetAttribute("ReceiverId", pcParameterRecord.Parameters.ReceiverId);
            }
            if (backgroundWorker == null || !backgroundWorker.CancellationPending)
            {
              if (backgroundWorker != null)
                backgroundWorker.ReportProgress(Convert.ToInt32(3.0 / num1 * 100.0 * 10.0));
              XFirmwareSettings xfirmwareSettings = api.ReadFirmwareSettings();
              xobject1.Element.AppendChild(applicationXmlDocument.ImportNode((XmlNode) xfirmwareSettings.Element, true));
              if (backgroundWorker == null || !backgroundWorker.CancellationPending)
              {
                if (backgroundWorker != null)
                  backgroundWorker.ReportProgress(Convert.ToInt32(4.0 / num1 * 100.0 * 10.0));
                XObject xobject2 = api.ReadLatestSettingsAsXml();
                xobject1.Element.AppendChild(applicationXmlDocument.ImportNode((XmlNode) xobject2.Element, true));
                if (backgroundWorker == null || !backgroundWorker.CancellationPending)
                {
                  if (backgroundWorker != null)
                    backgroundWorker.ReportProgress(Convert.ToInt32(5.0 / num1 * 100.0 * 10.0));
                  if (!skipDatabase && (recordsFilter & ReceiverRecordTypeFlags.ProcessorErrors) == ReceiverRecordTypeFlags.ProcessorErrors)
                  {
                    XObject xChildObject1 = new XObject("ProcessorErrorPages", applicationXmlDocument);
                    xobject1.AppendChild(xChildObject1);
                    byte[] numArray = api.ReadFlashPage(1272U);
                    if (!Array.TrueForAll<byte>(numArray, (Predicate<byte>) (val => (int) val == (int) byte.MaxValue)))
                    {
                      XObject xChildObject2 = new XObject(DataTools.CreateCompressedBinaryElement(numArray, "ProcessorErrorPage", applicationXmlDocument));
                      xChildObject1.AppendChild(xChildObject2);
                    }
                  }
                  XObject xChildObject3 = (XObject) null;
                  if (!skipDatabase)
                  {
                    xChildObject3 = new XObject("Database", applicationXmlDocument);
                    xobject1.AppendChild(xChildObject3);
                    XPartitionInfo xpartitionInfo = api.ReadDatabasePartitionInfo();
                    xChildObject3.Element.AppendChild(applicationXmlDocument.ImportNode((XmlNode) xpartitionInfo.Element, true));
                    if (backgroundWorker == null || !backgroundWorker.CancellationPending)
                    {
                      if (backgroundWorker != null)
                        backgroundWorker.ReportProgress(Convert.ToInt32(6.0 / num1 * 100.0 * 10.0));
                      XObject xChildObject1 = new XObject("Partitions", applicationXmlDocument);
                      xChildObject3.AppendChild(xChildObject1);
                      XObject xChildObject2 = new XObject("Pages", applicationXmlDocument);
                      xChildObject3.AppendChild(xChildObject2);
                      int num2 = DownloadReceiverHelper.DoCountDatabasePagesInReceiver(api, recordsFilter, backgroundWorker);
                      int num3 = 6;
                      int num4 = num2 + (num3 + 1);
                      if (backgroundWorker != null)
                      {
                        int percentProgress = Convert.ToInt32((double) num3 / (double) num4 * 100.0 * 10.0);
                        backgroundWorker.ReportProgress(percentProgress);
                      }
                      IEnumerator enumerator = xpartitionInfo.Partitions.GetEnumerator();
                      try
                      {
label_54:
                        while (enumerator.MoveNext())
                        {
                          XPartition xpartition = (XPartition) enumerator.Current;
                          if (backgroundWorker != null)
                          {
                            if (backgroundWorker.CancellationPending)
                              break;
                          }
                          byte id = xpartition.Id;
                          ReceiverRecordType record_type = (ReceiverRecordType) id;
                          if (ReceiverTools.DoesReceiverRecordTypeMatchFlags(record_type, recordsFilter))
                          {
                            DatabasePageRange databasePageRange = api.ReadDatabasePageRange(record_type);
                            XObject xChildObject4 = new XObject("Partition", applicationXmlDocument);
                            xChildObject1.AppendChild(xChildObject4);
                            xChildObject4.SetAttribute("RecordType", ((object) record_type).ToString());
                            xChildObject4.SetAttribute("RecordTypeId", id);
                            xChildObject4.SetAttribute("RecordRevision", xpartition.RecordRevision);
                            xChildObject4.SetAttribute("RecordLength", xpartition.RecordLength);
                            xChildObject4.SetAttribute("FirstPage", databasePageRange.FirstPage);
                            xChildObject4.SetAttribute("LastPage", databasePageRange.LastPage);
                            xChildObject4.SetAttribute("IsEmpty", (int) databasePageRange.LastPage == -1);
                            if ((int) databasePageRange.LastPage != -1)
                            {
                              uint first_page = databasePageRange.FirstPage;
                              uint last_page = databasePageRange.LastPage;
                              if (priorPageHeaders != null && priorPageHeaders.Count > 0)
                              {
                                XPageHeader xpageHeader = Enumerable.LastOrDefault<XPageHeader>(Enumerable.Select<KeyValuePair<ReceiverRecordType, XPageHeader>, XPageHeader>((IEnumerable<KeyValuePair<ReceiverRecordType, XPageHeader>>) Enumerable.OrderBy<KeyValuePair<ReceiverRecordType, XPageHeader>, uint>(Enumerable.Where<KeyValuePair<ReceiverRecordType, XPageHeader>>((IEnumerable<KeyValuePair<ReceiverRecordType, XPageHeader>>) priorPageHeaders, (Func<KeyValuePair<ReceiverRecordType, XPageHeader>, bool>) (entry =>
                                {
                                  if (entry.Key == record_type && entry.Value.PageNumber >= first_page)
                                    return entry.Value.PageNumber <= last_page;
                                  else
                                    return false;
                                })), (Func<KeyValuePair<ReceiverRecordType, XPageHeader>, uint>) (entry => entry.Value.PageNumber)), (Func<KeyValuePair<ReceiverRecordType, XPageHeader>, XPageHeader>) (entry => entry.Value)));
                                if (xpageHeader != null)
                                {
                                  uint pageNumber = xpageHeader.PageNumber;
                                  DatabasePageHeader databasePageHeader = api.ReadDatabasePageHeader(record_type, pageNumber);
                                  if ((int) xpageHeader.FirstRecordIndex == (int) databasePageHeader.FirstRecordIndex && (int) xpageHeader.NumberOfRecords == (int) databasePageHeader.NumberOfRecords)
                                  {
                                    first_page = pageNumber + 1U;
                                    if (first_page > last_page)
                                      continue;
                                  }
                                  else
                                    first_page = pageNumber;
                                }
                              }
                              uint pageNumber1 = first_page;
                              while (true)
                              {
                                if (pageNumber1 <= last_page && (backgroundWorker == null || !backgroundWorker.CancellationPending))
                                {
                                  int num5 = num3 + 1;
                                  int percentProgress = Convert.ToInt32((double) num5 / (double) num4 * 100.0 * 10.0);
                                  if (backgroundWorker != null)
                                    backgroundWorker.ReportProgress(percentProgress);
                                  uint num6 = (uint) ((int) last_page - (int) pageNumber1 + 1);
                                  if (num6 > 4U)
                                    num6 = 4U;
                                  foreach (DatabasePage databasePage in api.ReadDatabasePages(record_type, pageNumber1, (byte) num6))
                                  {
                                    XPage xpage = new XPage(applicationXmlDocument);
                                    xChildObject2.AppendChild((XObject) xpage);
                                    xpage.PageHeader.PageNumber = databasePage.PageHeader.PageNumber;
                                    xpage.PageHeader.RecordType = ((object) databasePage.PageHeader.RecordType).ToString();
                                    xpage.PageHeader.RecordTypeId = (int) databasePage.PageHeader.RecordType;
                                    xpage.PageHeader.RecordRevision = databasePage.PageHeader.Revision;
                                    xpage.PageHeader.FirstRecordIndex = databasePage.PageHeader.FirstRecordIndex;
                                    xpage.PageHeader.NumberOfRecords = databasePage.PageHeader.NumberOfRecords;
                                    xpage.PageHeader.Crc = databasePage.PageHeader.Crc;
                                    xpage.PageData = databasePage.PageData;
                                  }
                                  num3 = num5 + ((int) num6 - 1);
                                  pageNumber1 = pageNumber1 + (num6 - 1U) + 1U;
                                }
                                else
                                  goto label_54;
                              }
                            }
                          }
                        }
                      }
                      finally
                      {
                        IDisposable disposable = enumerator as IDisposable;
                        if (disposable != null)
                          disposable.Dispose();
                      }
                    }
                    else
                      goto label_64;
                  }
                  if (backgroundWorker == null || !backgroundWorker.CancellationPending)
                  {
                    int intValue = 0;
                    if (xChildObject3 != null)
                    {
                      intValue = xChildObject3.Element.SelectNodes("Pages/Page").Count;
                      xChildObject3.SetAttribute("PageCount", intValue);
                    }
                    if (xChildObject3 != null)
                    {
                      XmlElement compressedXmlElement = DataTools.CreateCompressedXmlElement(xChildObject3.Element, xChildObject3.Element.Name, applicationXmlDocument);
                      compressedXmlElement.SetAttribute("PageCount", intValue.ToString());
                      xobject1.Element.ReplaceChild((XmlNode) compressedXmlElement, (XmlNode) xChildObject3.Element);
                    }
                    xobject1.SetAttribute("ElapsedTime", DateTime.Now - now);
                    xmlDocument = applicationXmlDocument;
                  }
                }
              }
            }
          }
        }
      }
label_64:
      return xmlDocument;
    }

    public static void SimulateDownloadReceiver(ReceiverApi api, ReceiverRecordTypeFlags recordsFilter)
    {
      api.ReadFirmwareHeader();
      if (api.HasManufacturingParameters())
        api.ReadManufacturingParameters();
      if (api.HasPcParameters())
        api.ReadPcParameters();
      api.ReadLatestSettingsAsXml();
      XPartitionInfo xpartitionInfo = api.ReadDatabasePartitionInfo();
      DownloadReceiverHelper.DoCountDatabasePagesInReceiver(api, recordsFilter, (BackgroundWorker) null);
      DatabasePageRange databasePageRange = api.ReadDatabasePageRange(ReceiverRecordType.UserSettingData);
      foreach (XPartition xpartition in xpartitionInfo.Partitions)
      {
        ReceiverRecordType recordType = (ReceiverRecordType) xpartition.Id;
        if (ReceiverTools.DoesReceiverRecordTypeMatchFlags(recordType, recordsFilter))
        {
          api.ReadDatabasePageRange(recordType);
          int num1;
          switch (recordType)
          {
            case ReceiverRecordType.ManufacturingData:
              num1 = 2;
              break;
            case ReceiverRecordType.FirmwareParameterData:
              num1 = 2;
              break;
            case ReceiverRecordType.PCSoftwareParameter:
              num1 = 2;
              break;
            case ReceiverRecordType.SensorData:
              num1 = 347;
              break;
            case ReceiverRecordType.EGVData:
              num1 = 231;
              break;
            case ReceiverRecordType.CalSet:
              num1 = 29;
              break;
            case ReceiverRecordType.Aberration:
              num1 = 2;
              break;
            case ReceiverRecordType.InsertionTime:
              num1 = 2;
              break;
            case ReceiverRecordType.ReceiverLogData:
              num1 = 49;
              break;
            case ReceiverRecordType.ReceiverErrorData:
              num1 = 6;
              break;
            case ReceiverRecordType.MeterData:
              num1 = 4;
              break;
            case ReceiverRecordType.UserEventData:
              num1 = 25;
              break;
            case ReceiverRecordType.UserSettingData:
              num1 = 31;
              break;
            default:
              throw new DexComException("Unknown record type.");
          }
          List<DatabasePage> list1 = new List<DatabasePage>();
          api.ReadDatabasePageHeader(ReceiverRecordType.UserSettingData, databasePageRange.LastPage);
          for (uint index = 0U; (long) index < (long) num1; ++index)
          {
            int num2 = num1 - (int) index;
            if (num2 == 1)
            {
              DatabasePage databasePage = api.ReadDatabasePage(ReceiverRecordType.UserSettingData, databasePageRange.LastPage);
              list1.Add(databasePage);
            }
            else
            {
              if (num2 > 4)
                num2 = 4;
              if ((long) databasePageRange.LastPage > (long) num2)
              {
                List<DatabasePage> list2 = api.ReadDatabasePages(ReceiverRecordType.UserSettingData, (uint) ((int) databasePageRange.LastPage - num2 + 1), (byte) num2);
                list1.AddRange((IEnumerable<DatabasePage>) list2);
                index += (uint) (num2 - 1);
              }
              else
              {
                DatabasePage databasePage = api.ReadDatabasePage(ReceiverRecordType.UserSettingData, databasePageRange.LastPage);
                list1.Add(databasePage);
              }
            }
          }
          Trace.Assert(list1.Count == num1);
        }
      }
    }

    public static string MakeUniqueReceiverIdentifier(string serialNumber, Guid receiverInstance)
    {
      return string.Format("{0}_{1}", (object) serialNumber, (object) CommonTools.ConvertToString(receiverInstance));
    }

    public static string GetUniqueReceiverIdentifierFromDownloadFile(XmlDocument xDownload)
    {
      XObject xobject = new XObject(xDownload.DocumentElement);
      string serialNumber = string.Empty;
      Guid receiverInstance = Guid.Empty;
      if (xobject.HasAttribute("SerialNumber"))
        serialNumber = xobject.GetAttribute<string>("SerialNumber");
      if (xobject.HasAttribute("ReceiverId"))
        receiverInstance = xobject.GetAttribute<Guid>("ReceiverId");
      return DownloadReceiverHelper.MakeUniqueReceiverIdentifier(serialNumber, receiverInstance);
    }

    public static void GetUniqueReceiverIdentifierFromDownloadFile(XmlDocument xDownload, out string serialNumber, out Guid receiverInstance)
    {
      serialNumber = string.Empty;
      receiverInstance = Guid.Empty;
      XObject xobject = new XObject(xDownload.DocumentElement);
      string str = string.Empty;
      Guid guid = Guid.Empty;
      if (xobject.HasAttribute("SerialNumber"))
        str = xobject.GetAttribute<string>("SerialNumber");
      if (xobject.HasAttribute("ReceiverId"))
        guid = xobject.GetAttribute<Guid>("ReceiverId");
      serialNumber = str;
      receiverInstance = guid;
    }

    public static int GetPageCount(XmlDocument xDownload)
    {
      return new XObject(xDownload.DocumentElement).GetXPathAttribute<int>("/ReceiverDownload/Database/@PageCount");
    }

    public static string GetBlindMode(XmlDocument xDownload)
    {
      string str = string.Empty;
      return new XObject(xDownload.DocumentElement).GetXPathAttribute<string>("/ReceiverDownload/ReceiverSettings/@BlindMode");
    }

    public static List<Guid> GetDownloadHistoryIdList(XmlDocument xReceiver)
    {
      List<Guid> list = new List<Guid>();
      foreach (XmlElement element in xReceiver.SelectNodes("/ReceiverDownload/DownloadHistory/Download"))
      {
        XObject xobject = new XObject(element);
        list.Add(xobject.Id);
      }
      return list;
    }

    public static List<XObject> GetDownloadHistoryList(XmlDocument xReceiver)
    {
      List<XObject> list = new List<XObject>();
      foreach (XmlElement element in xReceiver.SelectNodes("/ReceiverDownload/DownloadHistory/Download"))
      {
        XObject xobject = new XObject(element);
        list.Add(xobject);
      }
      return list;
    }

    public static string GetDownloadType(XmlDocument xReceiver)
    {
      string str = string.Empty;
      return new XObject(xReceiver.SelectSingleNode("/ReceiverDownload") as XmlElement).GetAttribute<string>("DownloadType");
    }

    public static Guid GetMergedId(XmlDocument xReceiver)
    {
      Guid guid = Guid.Empty;
      return new XObject(xReceiver.SelectSingleNode("/ReceiverDownload") as XmlElement).GetAttribute<Guid>("MergedId");
    }

    public static XmlDocument MergeDownloadIntoReceiver(XmlDocument xDownload, XmlDocument xReceiver)
    {
      XmlDocument ownerDocument;
      if (xReceiver != null)
      {
        Trace.Assert(xDownload != null);
        XObject xobject1 = new XObject(xDownload.SelectSingleNode("/ReceiverDownload") as XmlElement);
        XObject xobject2 = new XObject(xReceiver.SelectSingleNode("/ReceiverDownload") as XmlElement);
        XObject xobject3 = new XObject(xReceiver.SelectSingleNode("/ReceiverDownload/DownloadHistory") as XmlElement);
        string fromDownloadFile1 = DownloadReceiverHelper.GetUniqueReceiverIdentifierFromDownloadFile(xDownload);
        string fromDownloadFile2 = DownloadReceiverHelper.GetUniqueReceiverIdentifierFromDownloadFile(xReceiver);
        string attribute1 = xobject1.GetAttribute<string>("DownloadType");
        string attribute2 = xobject2.GetAttribute<string>("DownloadType");
        if (xobject1.Id == xobject2.Id)
          throw new DexComException("Failed to merge download with receiver: download and receiver have the same download guid.");
        if (attribute1 != "Normal" && attribute1 != "Incremental")
          throw new DexComException("Failed to merge download with receiver: download appears to be a receiver.");
        if (attribute2 != "Merged")
          throw new DexComException("Failed to merge download with receiver: receiver appears to be a download.");
        if (xobject3.IsNull())
          throw new DexComException("Failed to merge download with receiver: receiver does not have download history element.");
        if (fromDownloadFile1 != fromDownloadFile2)
          throw new DexComException("Failed to merge download with receiver: download and receiver have different identifiers.");
        string xpath1 = string.Format("/ReceiverDownload/DownloadHistory/Download[@Id='{0}']", (object) CommonTools.ConvertToString(xobject1.Id));
        if (new XObject(xReceiver.SelectSingleNode(xpath1) as XmlElement).IsNotNull())
          return xReceiver;
        DateTimeOffset attribute3 = xobject1.GetAttribute<DateTimeOffset>("DateTimeCreated");
        DateTimeOffset attribute4 = xobject2.GetAttribute<DateTimeOffset>("DateTimeCreated");
        ownerDocument = new XmlDocument();
        XmlElement sourceElement;
        XmlElement xmlElement1;
        if (attribute3 > attribute4)
        {
          XmlNode newChild = ownerDocument.ImportNode((XmlNode) xDownload.DocumentElement, true);
          ownerDocument.AppendChild(newChild);
          sourceElement = xReceiver.SelectSingleNode("/ReceiverDownload/Database") as XmlElement;
          xmlElement1 = ownerDocument.SelectSingleNode("/ReceiverDownload/Database") as XmlElement;
        }
        else
        {
          XmlNode newChild = ownerDocument.ImportNode((XmlNode) xReceiver.DocumentElement, true);
          ownerDocument.AppendChild(newChild);
          sourceElement = xDownload.SelectSingleNode("/ReceiverDownload/Database") as XmlElement;
          xmlElement1 = ownerDocument.SelectSingleNode("/ReceiverDownload/Database") as XmlElement;
        }
        if (DataTools.IsCompressedElement(xmlElement1))
          xmlElement1 = DataTools.DecompressElementWithHint(xmlElement1) as XmlElement;
        if (DataTools.IsCompressedElement(sourceElement))
          sourceElement = DataTools.DecompressElementWithHint(sourceElement) as XmlElement;
        XmlElement xmlElement2 = xmlElement1.SelectSingleNode("Pages") as XmlElement;
        XmlNodeList xmlNodeList1 = xmlElement1.SelectNodes("Pages/Page");
        XmlNodeList xmlNodeList2 = sourceElement.SelectNodes("Pages/Page");
        List<XPage> list = new List<XPage>(Enumerable.Select<XmlElement, XPage>(Enumerable.Cast<XmlElement>((IEnumerable) xmlNodeList1), (Func<XmlElement, XPage>) (node => new XPage(node))));
        using (List<XPage>.Enumerator enumerator = new List<XPage>(Enumerable.Select<XmlElement, XPage>(Enumerable.Cast<XmlElement>((IEnumerable) xmlNodeList2), (Func<XmlElement, XPage>) (node => new XPage(node)))).GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            XPage x_source_page = enumerator.Current;
            XPage xpage = Enumerable.FirstOrDefault<XPage>((IEnumerable<XPage>) list, (Func<XPage, bool>) (x_target_page =>
            {
              if ((int) x_target_page.PageHeader.PageNumber == (int) x_source_page.PageHeader.PageNumber && x_target_page.PageHeader.RecordTypeId == x_source_page.PageHeader.RecordTypeId)
                return (int) x_target_page.PageHeader.FirstRecordIndex == (int) x_source_page.PageHeader.FirstRecordIndex;
              else
                return false;
            }));
            if (xpage != null)
            {
              if (xpage.PageHeader.NumberOfRecords < x_source_page.PageHeader.NumberOfRecords)
                xmlElement2.ReplaceChild(xmlElement1.OwnerDocument.ImportNode((XmlNode) x_source_page.Element, true), (XmlNode) xpage.Element);
            }
            else
              xmlElement2.AppendChild(xmlElement1.OwnerDocument.ImportNode((XmlNode) x_source_page.Element, true));
          }
        }
        XmlElement xmlElement3;
        XmlElement xmlElement4;
        if (attribute3 > attribute4)
        {
          xmlElement3 = xReceiver.SelectSingleNode("/ReceiverDownload/ProcessorErrorPages") as XmlElement;
          xmlElement4 = ownerDocument.SelectSingleNode("/ReceiverDownload/ProcessorErrorPages") as XmlElement;
        }
        else
        {
          xmlElement3 = xDownload.SelectSingleNode("/ReceiverDownload/ProcessorErrorPages") as XmlElement;
          xmlElement4 = ownerDocument.SelectSingleNode("/ReceiverDownload/ProcessorErrorPages") as XmlElement;
        }
        XmlNodeList xmlNodeList3 = (XmlNodeList) null;
        if (xmlElement3 != null)
          xmlNodeList3 = xmlElement3.SelectNodes("ProcessorErrorPage");
        if (xmlElement4 == null)
        {
          XObject xobject4 = new XObject("ProcessorErrorPages", ownerDocument);
          xmlElement4 = ownerDocument.DocumentElement.AppendChild((XmlNode) xobject4.Element) as XmlElement;
        }
        if (xmlElement4 != null && xmlNodeList3 != null)
        {
          foreach (XmlElement xmlElement5 in xmlNodeList3)
          {
            string xpath2 = string.Format("ProcessorErrorPage[@Size='{0}' and @Crc32='{1}']", (object) xmlElement5.GetAttribute("Size"), (object) xmlElement5.GetAttribute("Crc32"));
            if (xmlElement4.SelectSingleNode(xpath2) == null)
              xmlElement4.AppendChild(ownerDocument.ImportNode((XmlNode) xmlElement5, true));
          }
        }
        XmlDeclaration xmlDeclaration = ownerDocument.CreateXmlDeclaration("1.0", Encoding.UTF8.HeaderName, (string) null);
        ownerDocument.InsertBefore((XmlNode) xmlDeclaration, (XmlNode) ownerDocument.DocumentElement);
        XObject xobject5 = new XObject(ownerDocument.SelectSingleNode("/ReceiverDownload") as XmlElement);
        xobject5.Id = xobject2.Id;
        xobject5.SetAttribute("DownloadType", "Merged");
        xobject5.SetAttribute("DateTimeModified", DateTimeOffset.Now);
        xobject5.SetAttribute("MergedId", Guid.NewGuid());
        XmlNode newChild1 = ownerDocument.SelectSingleNode("/ReceiverDownload/DownloadHistory");
        if (newChild1 == null)
        {
          newChild1 = ownerDocument.ImportNode((XmlNode) xobject3.Element, true);
          ownerDocument.DocumentElement.AppendChild(newChild1);
        }
        XObject xobject6 = new XObject(newChild1 as XmlElement);
        int intValue = xobject6.GetAttribute<int>("Count") + 1;
        xobject6.SetAttribute("Count", intValue);
        XObject xChildObject = new XObject("Download", ownerDocument);
        xChildObject.Id = xobject1.Id;
        xChildObject.SetAttribute("DateTimeCreated", xobject1.GetAttribute("DateTimeCreated"));
        xChildObject.SetAttribute("DateTimeCreatedLocal", xobject1.GetAttribute("DateTimeCreatedLocal"));
        xChildObject.SetAttribute("DateTimeCreatedUtc", xobject1.GetAttribute("DateTimeCreatedUtc"));
        xChildObject.SetAttribute("DownloadType", xobject1.GetAttribute("DownloadType"));
        xobject6.AppendChild(xChildObject);
        XmlNodeList xmlNodeList4 = xmlElement1.SelectNodes("Pages/Page");
        xmlElement1.SetAttribute("PageCount", xmlNodeList4.Count.ToString());
        XmlElement xmlElement6 = xobject5.Element.SelectSingleNode("/ReceiverDownload/Database") as XmlElement;
        XmlElement xmlElement7 = xobject5.Element.SelectSingleNode("/ReceiverDownload") as XmlElement;
        if (true)
        {
          XmlElement compressedXmlElement = DataTools.CreateCompressedXmlElement(xmlElement1, xmlElement1.Name, ownerDocument);
          compressedXmlElement.SetAttribute("PageCount", xmlNodeList4.Count.ToString());
          xmlElement7.ReplaceChild((XmlNode) compressedXmlElement, (XmlNode) xmlElement6);
        }
        else
        {
          XmlElement xmlElement5 = ownerDocument.ImportNode((XmlNode) xmlElement1, true) as XmlElement;
          xmlElement7.ReplaceChild((XmlNode) xmlElement5, (XmlNode) xmlElement6);
        }
        XmlNode oldChild = xobject5.Element.SelectSingleNode("/ReceiverDownload/ExpandedDatabase");
        if (oldChild != null)
          xmlElement7.RemoveChild(oldChild);
      }
      else
      {
        ownerDocument = new XmlDocument();
        XmlNode newChild = ownerDocument.ImportNode((XmlNode) xDownload.DocumentElement, true);
        ownerDocument.AppendChild(newChild);
        XObject xobject = new XObject(ownerDocument.SelectSingleNode("/ReceiverDownload") as XmlElement);
        Guid id = xobject.Id;
        xobject.Id = Guid.NewGuid();
        if (xobject.GetAttribute<string>("DownloadType") == "Merged")
          throw new DexComException("Failed to merge download with receiver: download appears to be a receiver.");
        XObject xChildObject1 = new XObject("DownloadHistory", ownerDocument);
        xChildObject1.SetAttribute("Count", 1);
        xobject.AppendChild(xChildObject1);
        XObject xChildObject2 = new XObject("Download", ownerDocument);
        xChildObject2.Id = id;
        xChildObject2.SetAttribute("DateTimeCreated", xobject.GetAttribute("DateTimeCreated"));
        xChildObject2.SetAttribute("DateTimeCreatedLocal", xobject.GetAttribute("DateTimeCreatedLocal"));
        xChildObject2.SetAttribute("DateTimeCreatedUtc", xobject.GetAttribute("DateTimeCreatedUtc"));
        xChildObject2.SetAttribute("DownloadType", xobject.GetAttribute("DownloadType"));
        xChildObject1.AppendChild(xChildObject2);
        xobject.SetAttribute("DownloadType", "Merged");
        xobject.SetAttribute("DateTimeModified", DateTimeOffset.Now);
        xobject.SetAttribute("MergedId", Guid.NewGuid());
      }
      return ownerDocument;
    }

    public static XmlDocument MergeReceiverIntoReceiver(XmlDocument xSourceReceiver, XmlDocument xTargetReceiver)
    {
      XmlDocument ownerDocument;
      if (xTargetReceiver != null)
      {
        Trace.Assert(xSourceReceiver != null);
        XObject xobject1 = new XObject(xSourceReceiver.SelectSingleNode("/ReceiverDownload") as XmlElement);
        XObject xobject2 = new XObject(xTargetReceiver.SelectSingleNode("/ReceiverDownload") as XmlElement);
        XObject xobject3 = new XObject(xSourceReceiver.SelectSingleNode("/ReceiverDownload/DownloadHistory") as XmlElement);
        XObject xobject4 = new XObject(xTargetReceiver.SelectSingleNode("/ReceiverDownload/DownloadHistory") as XmlElement);
        string fromDownloadFile1 = DownloadReceiverHelper.GetUniqueReceiverIdentifierFromDownloadFile(xSourceReceiver);
        string fromDownloadFile2 = DownloadReceiverHelper.GetUniqueReceiverIdentifierFromDownloadFile(xTargetReceiver);
        string downloadType1 = DownloadReceiverHelper.GetDownloadType(xSourceReceiver);
        string downloadType2 = DownloadReceiverHelper.GetDownloadType(xTargetReceiver);
        Guid mergedId1 = DownloadReceiverHelper.GetMergedId(xSourceReceiver);
        Guid mergedId2 = DownloadReceiverHelper.GetMergedId(xTargetReceiver);
        if (xobject1.Id == xobject2.Id)
          throw new DexComException("Failed to merge receiver files: source and target have the same id/guid.");
        if (downloadType1 != "Merged")
          throw new DexComException("Failed to merge receiver files: source does not appear to be a receiver.");
        if (downloadType2 != "Merged")
          throw new DexComException("Failed to merge receiver files: target does not appear to be a receiver.");
        if (mergedId1 == mergedId2)
          throw new DexComException("Failed to merge receiver files: source and target have the same merged id/guid.");
        if (xobject3.IsNull())
          throw new DexComException("Failed to merge receiver files: source receiver does not have download history element.");
        if (xobject4.IsNull())
          throw new DexComException("Failed to merge receiver files: target receiver does not have download history element.");
        if (fromDownloadFile1 != fromDownloadFile2)
          throw new DexComException("Failed to merge receiver files: source and target are for different receiver identifiers.");
        DateTimeOffset attribute1 = xobject1.GetAttribute<DateTimeOffset>("DateTimeCreated");
        DateTimeOffset attribute2 = xobject2.GetAttribute<DateTimeOffset>("DateTimeCreated");
        ownerDocument = new XmlDocument();
        XmlElement sourceElement;
        XmlElement xmlElement1;
        if (attribute1 > attribute2)
        {
          XmlNode newChild = ownerDocument.ImportNode((XmlNode) xSourceReceiver.DocumentElement, true);
          ownerDocument.AppendChild(newChild);
          sourceElement = xTargetReceiver.SelectSingleNode("/ReceiverDownload/Database") as XmlElement;
          xmlElement1 = ownerDocument.SelectSingleNode("/ReceiverDownload/Database") as XmlElement;
        }
        else
        {
          XmlNode newChild = ownerDocument.ImportNode((XmlNode) xTargetReceiver.DocumentElement, true);
          ownerDocument.AppendChild(newChild);
          sourceElement = xSourceReceiver.SelectSingleNode("/ReceiverDownload/Database") as XmlElement;
          xmlElement1 = ownerDocument.SelectSingleNode("/ReceiverDownload/Database") as XmlElement;
        }
        if (DataTools.IsCompressedElement(xmlElement1))
          xmlElement1 = DataTools.DecompressElementWithHint(xmlElement1) as XmlElement;
        if (DataTools.IsCompressedElement(sourceElement))
          sourceElement = DataTools.DecompressElementWithHint(sourceElement) as XmlElement;
        XmlElement xmlElement2 = xmlElement1.SelectSingleNode("Pages") as XmlElement;
        XmlNodeList xmlNodeList1 = xmlElement1.SelectNodes("Pages/Page");
        XmlNodeList xmlNodeList2 = sourceElement.SelectNodes("Pages/Page");
        List<XPage> list1 = new List<XPage>(Enumerable.Select<XmlElement, XPage>(Enumerable.Cast<XmlElement>((IEnumerable) xmlNodeList1), (Func<XmlElement, XPage>) (node => new XPage(node))));
        using (List<XPage>.Enumerator enumerator = new List<XPage>(Enumerable.Select<XmlElement, XPage>(Enumerable.Cast<XmlElement>((IEnumerable) xmlNodeList2), (Func<XmlElement, XPage>) (node => new XPage(node)))).GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            XPage x_source_page = enumerator.Current;
            XPage xpage = Enumerable.FirstOrDefault<XPage>((IEnumerable<XPage>) list1, (Func<XPage, bool>) (x_target_page =>
            {
              if ((int) x_target_page.PageHeader.PageNumber == (int) x_source_page.PageHeader.PageNumber && x_target_page.PageHeader.RecordTypeId == x_source_page.PageHeader.RecordTypeId)
                return (int) x_target_page.PageHeader.FirstRecordIndex == (int) x_source_page.PageHeader.FirstRecordIndex;
              else
                return false;
            }));
            if (xpage != null)
            {
              if (xpage.PageHeader.NumberOfRecords < x_source_page.PageHeader.NumberOfRecords)
                xmlElement2.ReplaceChild(xmlElement1.OwnerDocument.ImportNode((XmlNode) x_source_page.Element, true), (XmlNode) xpage.Element);
            }
            else
              xmlElement2.AppendChild(xmlElement1.OwnerDocument.ImportNode((XmlNode) x_source_page.Element, true));
          }
        }
        XmlElement xmlElement3;
        XmlElement xmlElement4;
        if (attribute1 > attribute2)
        {
          xmlElement3 = xTargetReceiver.SelectSingleNode("/ReceiverDownload/ProcessorErrorPages") as XmlElement;
          xmlElement4 = ownerDocument.SelectSingleNode("/ReceiverDownload/ProcessorErrorPages") as XmlElement;
        }
        else
        {
          xmlElement3 = xSourceReceiver.SelectSingleNode("/ReceiverDownload/ProcessorErrorPages") as XmlElement;
          xmlElement4 = ownerDocument.SelectSingleNode("/ReceiverDownload/ProcessorErrorPages") as XmlElement;
        }
        XmlNodeList xmlNodeList3 = (XmlNodeList) null;
        if (xmlElement3 != null)
          xmlNodeList3 = xmlElement3.SelectNodes("ProcessorErrorPage");
        if (xmlElement4 == null)
        {
          XObject xobject5 = new XObject("ProcessorErrorPages", ownerDocument);
          xmlElement4 = ownerDocument.DocumentElement.AppendChild((XmlNode) xobject5.Element) as XmlElement;
        }
        if (xmlElement4 != null && xmlNodeList3 != null)
        {
          foreach (XmlElement xmlElement5 in xmlNodeList3)
          {
            string xpath = string.Format("ProcessorErrorPage[@Size='{0}' and @Crc32='{1}']", (object) xmlElement5.GetAttribute("Size"), (object) xmlElement5.GetAttribute("Crc32"));
            if (xmlElement4.SelectSingleNode(xpath) == null)
              xmlElement4.AppendChild(ownerDocument.ImportNode((XmlNode) xmlElement5, true));
          }
        }
        XmlDeclaration xmlDeclaration = ownerDocument.CreateXmlDeclaration("1.0", Encoding.UTF8.HeaderName, (string) null);
        ownerDocument.InsertBefore((XmlNode) xmlDeclaration, (XmlNode) ownerDocument.DocumentElement);
        XObject xobject6 = new XObject(ownerDocument.SelectSingleNode("/ReceiverDownload") as XmlElement);
        xobject6.Id = xobject2.Id;
        xobject6.SetAttribute("DownloadType", "Merged");
        xobject6.SetAttribute("DateTimeModified", DateTimeOffset.Now);
        xobject6.SetAttribute("MergedId", Guid.NewGuid());
        XmlNode oldChild1 = ownerDocument.SelectSingleNode("/ReceiverDownload/DownloadHistory");
        XObject xobject7 = new XObject("DownloadHistory", ownerDocument);
        List<XObject> downloadHistoryList1 = DownloadReceiverHelper.GetDownloadHistoryList(xSourceReceiver);
        List<XObject> downloadHistoryList2 = DownloadReceiverHelper.GetDownloadHistoryList(xTargetReceiver);
        List<XObject> list2 = new List<XObject>();
        list2.AddRange((IEnumerable<XObject>) downloadHistoryList1);
        list2.AddRange((IEnumerable<XObject>) downloadHistoryList2);
        list2.Sort((Comparison<XObject>) ((first, second) => first.GetAttribute<DateTimeOffset>("DateTimeCreated").CompareTo(second.GetAttribute<DateTimeOffset>("DateTimeCreated"))));
        List<Guid> list3 = new List<Guid>();
        using (List<XObject>.Enumerator enumerator = list2.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            XObject download = enumerator.Current;
            if (!list3.Exists((Predicate<Guid>) (item => item == download.Id)))
            {
              list3.Add(download.Id);
              xobject7.Element.AppendChild(ownerDocument.ImportNode((XmlNode) download.Element, true));
            }
          }
        }
        xobject7.SetAttribute("Count", list3.Count);
        ownerDocument.DocumentElement.ReplaceChild((XmlNode) xobject7.Element, oldChild1);
        XmlNodeList xmlNodeList4 = xmlElement1.SelectNodes("Pages/Page");
        xmlElement1.SetAttribute("PageCount", xmlNodeList4.Count.ToString());
        XmlElement xmlElement6 = xobject6.Element.SelectSingleNode("/ReceiverDownload/Database") as XmlElement;
        XmlElement xmlElement7 = xobject6.Element.SelectSingleNode("/ReceiverDownload") as XmlElement;
        if (true)
        {
          XmlElement compressedXmlElement = DataTools.CreateCompressedXmlElement(xmlElement1, xmlElement1.Name, ownerDocument);
          compressedXmlElement.SetAttribute("PageCount", xmlNodeList4.Count.ToString());
          xmlElement7.ReplaceChild((XmlNode) compressedXmlElement, (XmlNode) xmlElement6);
        }
        else
        {
          XmlElement xmlElement5 = ownerDocument.ImportNode((XmlNode) xmlElement1, true) as XmlElement;
          xmlElement7.ReplaceChild((XmlNode) xmlElement5, (XmlNode) xmlElement6);
        }
        XmlNode oldChild2 = xobject6.Element.SelectSingleNode("/ReceiverDownload/ExpandedDatabase");
        if (oldChild2 != null)
          xmlElement7.RemoveChild(oldChild2);
      }
      else
      {
        Trace.Assert(xSourceReceiver != null);
        ownerDocument = xSourceReceiver;
      }
      return ownerDocument;
    }
  }
}

// Type: Dexcom.ReceiverApi.ReceiverDatabaseRecordsParser
// Assembly: Dexcom.ReceiverCommercialApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 3F623F65-908D-40A7-87D3-892C5D1FFA2E
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.ReceiverCommercialApi.dll

using Dexcom.Common;
using Dexcom.Common.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Dexcom.ReceiverApi
{
  public class ReceiverDatabaseRecordsParser
  {
    public string SerialNumber { get; private set; }

    public XFirmwareHeader FirmwareHeader { get; private set; }

    public List<ManufacturingParameterRecord> ManufacturingRecords { get; private set; }

    public List<PCParameterRecord> PCParameterRecords { get; private set; }

    public List<EGVRecord> EgvRecords { get; private set; }

    public List<InsertionTimeRecord> InsertionTimeRecords { get; private set; }

    public List<MeterRecord> MeterRecords { get; private set; }

    public List<EventRecord> EventRecords { get; private set; }

    public ReceiverRecordTypeFlags RecordsFilter { get; set; }

    public ReceiverDatabaseRecordsParser()
    {
      this.RecordsFilter = ReceiverValues.AllKnownReceiverRecords;
      this.ManufacturingRecords = new List<ManufacturingParameterRecord>();
      this.PCParameterRecords = new List<PCParameterRecord>();
      this.EgvRecords = new List<EGVRecord>();
      this.InsertionTimeRecords = new List<InsertionTimeRecord>();
      this.MeterRecords = new List<MeterRecord>();
      this.EventRecords = new List<EventRecord>();
    }

    public void Parse(XmlDocument xDownloadDoc)
    {
      if (xDownloadDoc == null)
        return;
      XmlElement xmlElement = xDownloadDoc.SelectSingleNode("/ReceiverDownload") as XmlElement;
      if (xmlElement == null)
        throw new DexComException("Contents do not appear to be a compatible DexCom Receiver Download.");
      XmlElement sourceElement = xmlElement.SelectSingleNode("Database") as XmlElement;
      if (DataTools.IsCompressedElement(sourceElement))
        sourceElement = DataTools.DecompressElementWithHint(sourceElement) as XmlElement;
      this.SerialNumber = xmlElement.GetAttribute("SerialNumber");
      this.FirmwareHeader = new XFirmwareHeader(xmlElement.SelectSingleNode("FirmwareHeader") as XmlElement);
      this.DoParsePages(new List<XPage>(Enumerable.Select<XmlElement, XPage>(Enumerable.Cast<XmlElement>((IEnumerable) sourceElement.SelectNodes("Pages/Page")), (Func<XmlElement, XPage>) (node => new XPage(node)))));
      this.DoSortRecords();
    }

    private void DoSortRecords()
    {
      foreach (IAsyncResult asyncResult in new List<IAsyncResult>()
      {
        this.DoAsyncSortRecordsByRecordNumber<ManufacturingParameterRecord>(this.ManufacturingRecords),
        this.DoAsyncSortRecordsByRecordNumber<PCParameterRecord>(this.PCParameterRecords),
        this.DoAsyncSortRecordsByRecordNumber<EGVRecord>(this.EgvRecords),
        this.DoAsyncSortRecordsByRecordNumber<InsertionTimeRecord>(this.InsertionTimeRecords),
        this.DoAsyncSortRecordsByRecordNumber<MeterRecord>(this.MeterRecords),
        this.DoAsyncSortRecordsByRecordNumber<EventRecord>(this.EventRecords)
      })
      {
        if (asyncResult != null)
          asyncResult.AsyncWaitHandle.WaitOne();
      }
    }

    private IAsyncResult DoAsyncSortRecordsByRecordNumber<T>(List<T> list) where T : IGenericRecord
    {
      return new Action<Comparison<T>>(list.Sort).BeginInvoke(new Comparison<T>(this.DoCompareRecordByRecordNumber<T>), (AsyncCallback) null, (object) null);
    }

    private int DoCompareRecordByRecordNumber<T>(T first, T second) where T : IGenericRecord
    {
      int num = 0;
      return num == 0 ? first.RecordNumber.CompareTo(second.RecordNumber) : num;
    }

    private void DoParsePages(List<XPage> xPages)
    {
      for (int index = 0; index < xPages.Count; ++index)
      {
        if (index <= xPages.Count - 8)
        {
          Func<XPage, object> func1 = new Func<XPage, object>(this.DoParsePageToList);
          IAsyncResult result1 = func1.BeginInvoke(xPages[index], (AsyncCallback) null, (object) null);
          Func<XPage, object> func2 = new Func<XPage, object>(this.DoParsePageToList);
          IAsyncResult result2 = func2.BeginInvoke(xPages[index + 1], (AsyncCallback) null, (object) null);
          Func<XPage, object> func3 = new Func<XPage, object>(this.DoParsePageToList);
          IAsyncResult result3 = func3.BeginInvoke(xPages[index + 2], (AsyncCallback) null, (object) null);
          Func<XPage, object> func4 = new Func<XPage, object>(this.DoParsePageToList);
          IAsyncResult result4 = func4.BeginInvoke(xPages[index + 3], (AsyncCallback) null, (object) null);
          Func<XPage, object> func5 = new Func<XPage, object>(this.DoParsePageToList);
          IAsyncResult result5 = func5.BeginInvoke(xPages[index + 4], (AsyncCallback) null, (object) null);
          Func<XPage, object> func6 = new Func<XPage, object>(this.DoParsePageToList);
          IAsyncResult result6 = func6.BeginInvoke(xPages[index + 5], (AsyncCallback) null, (object) null);
          Func<XPage, object> func7 = new Func<XPage, object>(this.DoParsePageToList);
          IAsyncResult result7 = func7.BeginInvoke(xPages[index + 6], (AsyncCallback) null, (object) null);
          Func<XPage, object> func8 = new Func<XPage, object>(this.DoParsePageToList);
          IAsyncResult result8 = func8.BeginInvoke(xPages[index + 7], (AsyncCallback) null, (object) null);
          object list1 = func1.EndInvoke(result1);
          object list2 = func2.EndInvoke(result2);
          object list3 = func3.EndInvoke(result3);
          object list4 = func4.EndInvoke(result4);
          object list5 = func5.EndInvoke(result5);
          object list6 = func6.EndInvoke(result6);
          object list7 = func7.EndInvoke(result7);
          object list8 = func8.EndInvoke(result8);
          this.DoAssignListObjectToRecords(list1, (ReceiverRecordType) xPages[index].PageHeader.RecordTypeId);
          this.DoAssignListObjectToRecords(list2, (ReceiverRecordType) xPages[index + 1].PageHeader.RecordTypeId);
          this.DoAssignListObjectToRecords(list3, (ReceiverRecordType) xPages[index + 2].PageHeader.RecordTypeId);
          this.DoAssignListObjectToRecords(list4, (ReceiverRecordType) xPages[index + 3].PageHeader.RecordTypeId);
          this.DoAssignListObjectToRecords(list5, (ReceiverRecordType) xPages[index + 4].PageHeader.RecordTypeId);
          this.DoAssignListObjectToRecords(list6, (ReceiverRecordType) xPages[index + 5].PageHeader.RecordTypeId);
          this.DoAssignListObjectToRecords(list7, (ReceiverRecordType) xPages[index + 6].PageHeader.RecordTypeId);
          this.DoAssignListObjectToRecords(list8, (ReceiverRecordType) xPages[index + 7].PageHeader.RecordTypeId);
          index += 7;
        }
        else
          this.DoAssignListObjectToRecords(this.DoParsePageToList(xPages[index]), (ReceiverRecordType) xPages[index].PageHeader.RecordTypeId);
      }
    }

    private void DoAssignListObjectToRecords(object list, ReceiverRecordType recordType)
    {
      switch (recordType)
      {
        case ReceiverRecordType.ManufacturingData:
          this.ManufacturingRecords.AddRange((IEnumerable<ManufacturingParameterRecord>) list);
          break;
        case ReceiverRecordType.PCSoftwareParameter:
          this.PCParameterRecords.AddRange((IEnumerable<PCParameterRecord>) list);
          break;
        case ReceiverRecordType.EGVData:
          using (List<EGVRecord>.Enumerator enumerator = ((List<EGVRecord>) list).GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              EGVRecord current = enumerator.Current;
              if (!current.IsDisplayOnly)
                this.EgvRecords.Add(current);
            }
            break;
          }
        case ReceiverRecordType.InsertionTime:
          this.InsertionTimeRecords.AddRange((IEnumerable<InsertionTimeRecord>) list);
          break;
        case ReceiverRecordType.MeterData:
          this.MeterRecords.AddRange((IEnumerable<MeterRecord>) list);
          break;
        case ReceiverRecordType.UserEventData:
          this.EventRecords.AddRange((IEnumerable<EventRecord>) list);
          break;
      }
    }

    private object DoParsePageToList(XPage xPage)
    {
      object obj = (object) null;
      switch ((ReceiverRecordType) xPage.PageHeader.RecordTypeId)
      {
        case ReceiverRecordType.ManufacturingData:
          obj = (object) ReceiverTools.ParsePage<ManufacturingParameterRecord>(xPage.PageHeader, xPage.PageData);
          break;
        case ReceiverRecordType.PCSoftwareParameter:
          obj = (object) ReceiverTools.ParsePage<PCParameterRecord>(xPage.PageHeader, xPage.PageData);
          break;
        case ReceiverRecordType.EGVData:
          obj = (object) ReceiverTools.ParsePage<EGVRecord>(xPage.PageHeader, xPage.PageData);
          break;
        case ReceiverRecordType.InsertionTime:
          obj = (object) ReceiverTools.ParsePage<InsertionTimeRecord>(xPage.PageHeader, xPage.PageData);
          break;
        case ReceiverRecordType.MeterData:
          obj = (object) ReceiverTools.ParsePage<MeterRecord>(xPage.PageHeader, xPage.PageData);
          break;
        case ReceiverRecordType.UserEventData:
          obj = (object) ReceiverTools.ParsePage<EventRecord>(xPage.PageHeader, xPage.PageData);
          break;
      }
      return obj;
    }
  }
}

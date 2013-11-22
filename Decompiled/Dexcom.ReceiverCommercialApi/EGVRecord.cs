// Type: Dexcom.ReceiverApi.EGVRecord
// Assembly: Dexcom.ReceiverCommercialApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 3F623F65-908D-40A7-87D3-892C5D1FFA2E
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.ReceiverCommercialApi.dll

using Dexcom.Common;
using System;
using System.Xml;

namespace Dexcom.ReceiverApi
{
  public class EGVRecord : IGenericRecord
  {
    private ReceiverEGVRecord m_internalRecord;

    public int RecordRevision { get; private set; }

    public ushort GlucoseValue
    {
      get
      {
        return this.m_internalRecord.GlucoseValue;
      }
    }

    public TrendArrow TrendArrow
    {
      get
      {
        return this.m_internalRecord.TrendArrow;
      }
    }

    public NoiseMode NoiseMode
    {
      get
      {
        return this.m_internalRecord.NoiseMode;
      }
    }

    public bool IsImmediateMatch
    {
      get
      {
        return this.m_internalRecord.IsImmediateMatch;
      }
    }

    public bool IsDisplayOnly
    {
      get
      {
        return this.m_internalRecord.IsDisplayOnly;
      }
    }

    public TimeSpan DeltaTime { get; set; }

    public int GapCount { get; set; }

    public string SpecialValue
    {
      get
      {
        string str = string.Empty;
        if (Enum.IsDefined(typeof (SpecialGlucoseValues), (object) this.GlucoseValue))
          str = ((object) (SpecialGlucoseValues) this.GlucoseValue).ToString();
        return str;
      }
    }

    public DateTime SystemTime
    {
      get
      {
        return this.m_internalRecord.SystemTimeStamp;
      }
    }

    public DateTime DisplayTime
    {
      get
      {
        return this.m_internalRecord.DisplayTimeStamp;
      }
    }

    public ReceiverRecordType RecordType
    {
      get
      {
        return this.m_internalRecord.RecordType;
      }
    }

    public uint RecordNumber { get; set; }

    public uint PageNumber { get; set; }

    public object Tag { get; set; }

    public EGVRecord(byte[] data, int offset)
      : this(data, offset, ReceiverTools.GetLatestSupportedRecordRevision(ReceiverRecordType.EGVData))
    {
    }

    public EGVRecord(byte[] data, int offset, int recordRevision)
    {
      this.m_internalRecord = (ReceiverEGVRecord) ReceiverTools.DatabaseRecordFactory(this.m_internalRecord.RecordType, recordRevision, data, offset);
      this.RecordRevision = recordRevision;
    }

    public XmlElement ToXml()
    {
      return this.ToXml(new XmlDocument());
    }

    public XmlElement ToXml(XmlDocument xOwner)
    {
      XObject xobject = new XObject("EGVRecord", xOwner);
      xobject.SetAttribute("RecordNumber", this.RecordNumber);
      xobject.SetAttribute("RecordRevision", this.RecordRevision);
      xobject.SetAttribute("SystemTime", this.SystemTime);
      xobject.SetAttribute("DisplayTime", this.DisplayTime);
      xobject.SetAttribute("TrendArrow", ((object) this.TrendArrow).ToString());
      xobject.SetAttribute("NoiseMode", ((object) this.NoiseMode).ToString());
      xobject.SetAttribute("GlucoseValue", this.GlucoseValue);
      xobject.SetAttribute("IsImmediateMatch", this.IsImmediateMatch);
      xobject.SetAttribute("IsDisplayOnly", this.IsDisplayOnly);
      xobject.SetAttribute("GapCount", this.GapCount);
      xobject.SetAttribute("DeltaTime", this.DeltaTime);
      return xobject.Element;
    }
  }
}

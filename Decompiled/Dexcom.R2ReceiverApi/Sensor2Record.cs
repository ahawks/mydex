// Type: Dexcom.R2Receiver.Sensor2Record
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using Dexcom.Common;
using System;
using System.Runtime.InteropServices;
using System.Xml;

namespace Dexcom.R2Receiver
{
  public class Sensor2Record : IGenericRecord
  {
    private TimeSpan m_calculatedSkewOffset = TimeSpan.Zero;
    private TimeSpan m_calculatedUserOffset = TimeSpan.Zero;
    private short? m_matchedPairEGV = new short?();
    private string m_calibrationState = string.Empty;
    private DateTime m_priorReceivedTimeUtc = CommonValues.EmptyDateTime;
    private TimeSpan m_gapTimeSpan = TimeSpan.Zero;
    private R2Sensor2Record m_record;
    private double m_calibrationSlope;
    private double m_calibrationIntercept;
    private DateTime m_calibrationSensorInsertionTime;
    private byte m_calibrationStateCode;
    private int m_calibrationRecordNumber;
    private DateTime m_calibrationTimeStampUtc;
    private uint m_priorTransmitterId;
    private int m_gapCount;
    private object m_tag;

    public object Tag
    {
      get
      {
        return this.m_tag;
      }
      set
      {
        this.m_tag = value;
      }
    }

    public R2RecordType RecordType
    {
      get
      {
        return this.m_record.RecordType;
      }
    }

    public XmlElement Xml
    {
      get
      {
        return this.ToXml();
      }
    }

    public DateTime SystemTime
    {
      get
      {
        return this.TimeStampUtc;
      }
    }

    public DateTime DisplayTime
    {
      get
      {
        return this.UserTimeStamp;
      }
    }

    public int RecordNumber
    {
      get
      {
        return this.m_record.m_key.m_recordNumber;
      }
    }

    public DateTime TimeStampUtc
    {
      get
      {
        return this.m_record.RecordTimeStamp;
      }
    }

    public DateTime CorrectedTimeStampUtc
    {
      get
      {
        return this.m_record.RecordTimeStamp + this.m_calculatedSkewOffset;
      }
    }

    public DateTime UserTimeStamp
    {
      get
      {
        return this.m_record.RecordTimeStamp + this.m_calculatedSkewOffset + this.m_calculatedUserOffset;
      }
    }

    public ushort RawCountsX
    {
      get
      {
        return this.m_record.m_rawCounts;
      }
    }

    public uint RawCounts
    {
      get
      {
        return ((uint) this.m_record.m_rawCounts & 8191U) << ((int) this.m_record.m_rawCounts >> 13 & 7);
      }
    }

    public ushort FilteredCountsX
    {
      get
      {
        return this.m_record.m_filteredCounts;
      }
    }

    public uint FilteredCounts
    {
      get
      {
        return ((uint) this.m_record.m_filteredCounts & 8191U) << ((int) this.m_record.m_filteredCounts >> 13 & 7);
      }
    }

    public DateTime ReceivedTimeUtc
    {
      get
      {
        return R2ReceiverValues.R2BaseDateTime.AddMilliseconds((double) this.m_record.m_receivedTime);
      }
      set
      {
        this.m_record.m_receivedTime = (ulong) (value - R2ReceiverValues.R2BaseDateTime).TotalMilliseconds;
      }
    }

    public uint TransmitterId
    {
      get
      {
        return this.m_record.m_transmitterId;
      }
    }

    public string TransmitterCode
    {
      get
      {
        return R2ReceiverTools.ConvertTransmitterNumberToDisplayableCode(this.m_record.m_transmitterId);
      }
    }

    public uint TransmitterNumber
    {
      get
      {
        return this.m_record.m_transmitterId & 16777215U;
      }
    }

    public short GlucoseValue
    {
      get
      {
        if (this.m_record.RecordType == R2RecordType.Sensor3)
          return (short) ((int) this.m_record.m_footer.m_updateableData & 4095);
        else
          return (short) this.m_record.m_footer.m_updateableData;
      }
      set
      {
        this.m_record.m_footer.m_updateableData = (ushort) value;
      }
    }

    [ColumnInfo(DefaultWidth = 50, Ordinal = 10, Visible = true)]
    public bool IsCleanEstimatedGlucoseValue
    {
      get
      {
        if (this.m_record.RecordType == R2RecordType.Sensor3)
          return ((int) this.m_record.m_footer.m_updateableData & 32768) != 0;
        else
          return true;
      }
    }

    public DateTime PriorReceivedTimeUtc
    {
      get
      {
        return this.m_priorReceivedTimeUtc;
      }
    }

    public uint PriorTransmitterId
    {
      get
      {
        return this.m_priorTransmitterId;
      }
    }

    public string PriorTransmitterCode
    {
      get
      {
        return R2ReceiverTools.ConvertTransmitterNumberToDisplayableCode(this.m_priorTransmitterId);
      }
    }

    public int GapCount
    {
      get
      {
        return this.m_gapCount;
      }
      set
      {
        this.m_gapCount = value;
      }
    }

    public TimeSpan GapTimeSpan
    {
      get
      {
        return this.m_gapTimeSpan;
      }
      set
      {
        this.m_gapTimeSpan = value;
      }
    }

    public short RawEstimatedGlucoseValue
    {
      get
      {
        return (short) this.m_record.m_footer.m_updateableData;
      }
      set
      {
        this.m_record.m_footer.m_updateableData = (ushort) value;
      }
    }

    public uint BlockNumber
    {
      get
      {
        return this.m_record.m_key.m_block;
      }
    }

    public string CalibrationState
    {
      get
      {
        return this.m_calibrationState;
      }
    }

    public byte CalibrationStateCode
    {
      get
      {
        return this.m_calibrationStateCode;
      }
    }

    public DateTime CalibrationSensorInsertionTime
    {
      get
      {
        return this.m_calibrationSensorInsertionTime;
      }
    }

    public double CalibrationSlope
    {
      get
      {
        return this.m_calibrationSlope;
      }
    }

    public double CalibrationIntercept
    {
      get
      {
        return this.m_calibrationIntercept;
      }
    }

    public int CalibrationRecordNumber
    {
      get
      {
        return this.m_calibrationRecordNumber;
      }
    }

    public DateTime CalibrationTimeStampUtc
    {
      get
      {
        return this.m_calibrationTimeStampUtc;
      }
    }

    public short? MatchedPairEGV
    {
      get
      {
        return this.m_matchedPairEGV;
      }
      set
      {
        this.m_matchedPairEGV = value;
      }
    }

    public Sensor2Record(byte[] data, int offset)
    {
      this.m_record = (R2Sensor2Record) R2ReceiverTools.DatabaseRecordFactory((R2RecordType) data[offset], data, offset);
      if ((int) this.m_record.m_footer.m_updateableData == (int) ushort.MaxValue && (int) this.m_record.m_footer.m_updateableCrc == (int) ushort.MaxValue)
        return;
      int start = offset + Marshal.OffsetOf(this.m_record.GetType(), "m_footer").ToInt32() + Marshal.OffsetOf(this.m_record.m_footer.GetType(), "m_updateableData").ToInt32();
      int end = offset + Marshal.OffsetOf(this.m_record.GetType(), "m_footer").ToInt32() + Marshal.OffsetOf(this.m_record.m_footer.GetType(), "m_updateableCrc").ToInt32();
      if ((int) Crc.CalculateCrc16(data, start, end) != (int) this.m_record.m_footer.m_updateableCrc)
        throw new ApplicationException(string.Format("Bad updateable CRC in record {0}, Offset =  0x{1:X}", (object) this.m_record.GetType().Name, (object) offset));
    }

    public Sensor2Record(DateTime packetTime, short glucoseValue)
    {
      this.ReceivedTimeUtc = packetTime;
      this.GlucoseValue = glucoseValue;
    }

    public void SetCalculatedTimeOffsets(TimeSpan skewOffset, TimeSpan userOffset)
    {
      this.m_calculatedSkewOffset = skewOffset;
      this.m_calculatedUserOffset = userOffset;
    }

    public void SetPriorSensorInformation(Sensor2Record priorRecord)
    {
      if (priorRecord == null)
        return;
      this.m_priorReceivedTimeUtc = priorRecord.ReceivedTimeUtc;
      this.m_priorTransmitterId = priorRecord.TransmitterId;
      this.m_gapTimeSpan = this.ReceivedTimeUtc - priorRecord.ReceivedTimeUtc;
      if (!(this.m_gapTimeSpan > new TimeSpan(0, 0, 0, 0, R2ReceiverValues.TransmitterMinimumGapMsec)))
        return;
      this.m_gapCount = (int) ((this.m_gapTimeSpan.TotalMilliseconds - (double) R2ReceiverValues.TransmitterIntervalMsec) / (double) R2ReceiverValues.TransmitterIntervalMsec + 0.5);
    }

    public void SetPriorCalibrationInformation(STSCalibrationRecord priorRecord)
    {
      if (priorRecord == null)
        return;
      this.m_calibrationSlope = priorRecord.Slope;
      this.m_calibrationIntercept = priorRecord.Intercept;
      this.m_calibrationSensorInsertionTime = priorRecord.SensorInsertionTime;
      this.m_calibrationState = priorRecord.AlgorithmCalibrationState;
      this.m_calibrationStateCode = priorRecord.AlgorithmCalibrationStateCode;
      this.m_calibrationRecordNumber = priorRecord.RecordNumber;
      this.m_calibrationTimeStampUtc = priorRecord.TimeStampUtc;
    }

    public int GetInternalRecordSize()
    {
      return this.m_record.RecordSize;
    }

    public R2Sensor2Record GetInternalRecord()
    {
      return this.m_record;
    }

    public XmlElement ToXml()
    {
      return this.ToXml(new XmlDocument());
    }

    public XmlElement ToXml(XmlDocument xOwner)
    {
      XObject xobject = new XObject("Sensor2Record", xOwner);
      xobject.SetAttribute("RecordNumber", this.RecordNumber);
      xobject.SetAttribute("TimeStampUtc", this.TimeStampUtc);
      xobject.SetAttribute("CorrectedTimeStampUtc", this.CorrectedTimeStampUtc);
      xobject.SetAttribute("UserTimeStamp", this.UserTimeStamp);
      xobject.SetAttribute("RawCountsX", string.Format("0x{0:X}", (object) this.RawCountsX));
      xobject.SetAttribute("RawCounts", this.RawCounts);
      xobject.SetAttribute("FilteredCountsX", string.Format("0x{0:X}", (object) this.FilteredCountsX));
      xobject.SetAttribute("FilteredCounts", this.FilteredCounts);
      xobject.SetAttribute("ReceivedTimeUtc", this.ReceivedTimeUtc);
      xobject.SetAttribute("TransmitterId", this.TransmitterId);
      xobject.SetAttribute("TransmitterCode", this.TransmitterCode);
      xobject.SetAttribute("GlucoseValue", this.GlucoseValue);
      xobject.SetAttribute("RawEstimatedGlucoseValue", this.RawEstimatedGlucoseValue);
      xobject.SetAttribute("IsCleanEstimatedGlucoseValue", this.IsCleanEstimatedGlucoseValue);
      xobject.SetAttribute("PriorReceivedTimeUtc", this.PriorReceivedTimeUtc);
      xobject.SetAttribute("PriorTransmitterId", this.PriorTransmitterId);
      xobject.SetAttribute("PriorTransmitterCode", this.PriorTransmitterCode);
      xobject.SetAttribute("GapCount", this.GapCount);
      xobject.SetAttribute("GapTimeSpan", this.GapTimeSpan);
      xobject.SetAttribute("CalibrationState", this.CalibrationState);
      xobject.SetAttribute("CalibrationStateCode", this.CalibrationStateCode);
      xobject.SetAttribute("CalibrationSensorInsertionTime", this.CalibrationSensorInsertionTime);
      xobject.SetAttribute("CalibrationSlope", this.CalibrationSlope);
      xobject.SetAttribute("CalibrationIntercept", this.CalibrationIntercept);
      xobject.SetAttribute("CalibrationRecordNumber", this.CalibrationRecordNumber);
      xobject.SetAttribute("CalibrationTimeStampUtc", this.CalibrationTimeStampUtc);
      return xobject.Element;
    }
  }
}

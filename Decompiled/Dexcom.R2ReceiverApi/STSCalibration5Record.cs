// Type: Dexcom.R2Receiver.STSCalibration5Record
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using Dexcom.Common;
using System;
using System.Xml;

namespace Dexcom.R2Receiver
{
  public class STSCalibration5Record : STSCalibrationRecord, IGenericRecord
  {
    private R2STSCalibration5Record m_record;

    public override XmlElement Xml
    {
      get
      {
        return this.ToXml();
      }
    }

    [ColumnInfo(DefaultWidth = 50, Ordinal = 0, Visible = true)]
    public override int RecordNumber
    {
      get
      {
        return this.m_record.m_key.m_recordNumber;
      }
    }

    [ColumnInfo(DefaultWidth = 50, Ordinal = 0, Visible = false)]
    public override uint BlockNumber
    {
      get
      {
        return this.m_record.m_key.m_block;
      }
    }

    [ColumnInfo(DefaultWidth = 160, FormatString = "yyyy'-'MM'-'dd' 'hh':'mm':'ss'.'fff tt", Ordinal = 1, Visible = true)]
    public override DateTime TimeStampUtc
    {
      get
      {
        return this.m_record.RecordTimeStamp;
      }
    }

    [ColumnInfo(DefaultWidth = 4, FormatString = "yyyy'-'MM'-'dd' 'hh':'mm':'ss'.'fff tt", Ordinal = 2, Visible = true)]
    public override DateTime CorrectedTimeStampUtc
    {
      get
      {
        return this.m_record.RecordTimeStamp + this.m_calculatedSkewOffset;
      }
    }

    [ColumnInfo(DefaultWidth = 160, FormatString = "yyyy'-'MM'-'dd' 'hh':'mm':'ss'.'fff tt", Ordinal = 3, Visible = true)]
    public override DateTime UserTimeStamp
    {
      get
      {
        return this.m_record.RecordTimeStamp + this.m_calculatedSkewOffset + this.m_calculatedUserOffset;
      }
    }

    [ColumnInfo(DefaultWidth = 100, Ordinal = 4, Visible = true)]
    public override string AlgorithmCalibrationState
    {
      get
      {
        if (Enum.IsDefined(typeof (R2AlgorithmState), (object) this.m_record.m_algorithmCalibrationState))
          return ((object) (R2AlgorithmState) this.m_record.m_algorithmCalibrationState).ToString();
        else
          return "Unknown State " + this.m_record.m_algorithmCalibrationState.ToString();
      }
    }

    [ColumnInfo(DefaultWidth = 50, Ordinal = 5, Visible = true)]
    public override byte AlgorithmCalibrationStateCode
    {
      get
      {
        return this.m_record.m_algorithmCalibrationState;
      }
    }

    [ColumnInfo(DefaultWidth = 50, Ordinal = 6, Visible = true)]
    public override byte CalSetPoints
    {
      get
      {
        return this.m_record.m_calSetPoints;
      }
    }

    [ColumnInfo(DefaultWidth = 160, FormatString = "yyyy'-'MM'-'dd' 'hh':'mm':'ss'.'fff tt", Ordinal = 11, Visible = true)]
    public override DateTime SensorInsertionTime
    {
      get
      {
        return R2ReceiverValues.R2BaseDateTime.AddMilliseconds((double) this.m_record.m_sensorInsertionTime);
      }
    }

    [ColumnInfo(DefaultWidth = 75, Ordinal = 12, Visible = true)]
    public override double Slope
    {
      get
      {
        return this.m_record.m_slope;
      }
    }

    [ColumnInfo(DefaultWidth = 75, Ordinal = 13, Visible = true)]
    public override double Intercept
    {
      get
      {
        return this.m_record.m_intercept;
      }
    }

    [ColumnInfo(DefaultWidth = 75, Ordinal = 14, Visible = true)]
    public override ushort SensorLife
    {
      get
      {
        return (ushort) 7;
      }
    }

    [ColumnInfo(DefaultWidth = 75, Ordinal = 20, Visible = true)]
    public double SlopeAdjust
    {
      get
      {
        return this.m_record.m_slopeAdjust;
      }
    }

    [ColumnInfo(DefaultWidth = 50, Ordinal = 21, Visible = true)]
    public byte CountsAberrantPoints
    {
      get
      {
        return this.m_record.m_countsAberrantPoints;
      }
    }

    [ColumnInfo(DefaultWidth = 50, Ordinal = 22, Visible = true)]
    public byte AbsoluteAberrantPoints
    {
      get
      {
        return this.m_record.m_absoluteAberrantPoints;
      }
    }

    [ColumnInfo(DefaultWidth = 50, Ordinal = 23, Visible = true)]
    public byte PowerAberrantPoints
    {
      get
      {
        return this.m_record.m_powerAberrantPoints;
      }
    }

    [ColumnInfo(DefaultWidth = 100, Ordinal = 24, Visible = true)]
    public string CalSetPointDisplay0
    {
      get
      {
        string str = string.Empty;
        if ((int) this.CalSetPoints >= 1)
        {
          R2MatchedPairData r2MatchedPairData = this.m_record.m_calibrationSet0;
          str = str + string.Format("SensorCount='{0}' SmbgGlucose='{1}' SmbgTimeStampUtc='{2}'", (object) r2MatchedPairData.m_sensorCount, (object) r2MatchedPairData.m_smbgGlucose, (object) R2ReceiverValues.R2BaseDateTime.AddMilliseconds((double) r2MatchedPairData.m_smbgTimeStampUtc).ToString("yyyy'-'MM'-'dd' 'hh':'mm':'ss'.'fff tt"));
        }
        return str;
      }
    }

    [ColumnInfo(DefaultWidth = 100, Ordinal = 25, Visible = true)]
    public string CalSetPointDisplay1
    {
      get
      {
        string str = string.Empty;
        if ((int) this.CalSetPoints >= 2)
        {
          R2MatchedPairData r2MatchedPairData = this.m_record.m_calibrationSet1;
          str = str + string.Format("SensorCount='{0}' SmbgGlucose='{1}' SmbgTimeStampUtc='{2}'", (object) r2MatchedPairData.m_sensorCount, (object) r2MatchedPairData.m_smbgGlucose, (object) R2ReceiverValues.R2BaseDateTime.AddMilliseconds((double) r2MatchedPairData.m_smbgTimeStampUtc).ToString("yyyy'-'MM'-'dd' 'hh':'mm':'ss'.'fff tt"));
        }
        return str;
      }
    }

    [ColumnInfo(DefaultWidth = 100, Ordinal = 26, Visible = true)]
    public string CalSetPointDisplay2
    {
      get
      {
        string str = string.Empty;
        if ((int) this.CalSetPoints >= 3)
        {
          R2MatchedPairData r2MatchedPairData = this.m_record.m_calibrationSet2;
          str = str + string.Format("SensorCount='{0}' SmbgGlucose='{1}' SmbgTimeStampUtc='{2}'", (object) r2MatchedPairData.m_sensorCount, (object) r2MatchedPairData.m_smbgGlucose, (object) R2ReceiverValues.R2BaseDateTime.AddMilliseconds((double) r2MatchedPairData.m_smbgTimeStampUtc).ToString("yyyy'-'MM'-'dd' 'hh':'mm':'ss'.'fff tt"));
        }
        return str;
      }
    }

    [ColumnInfo(DefaultWidth = 100, Ordinal = 27, Visible = true)]
    public string CalSetPointDisplay3
    {
      get
      {
        string str = string.Empty;
        if ((int) this.CalSetPoints >= 4)
        {
          R2MatchedPairData r2MatchedPairData = this.m_record.m_calibrationSet3;
          str = str + string.Format("SensorCount='{0}' SmbgGlucose='{1}' SmbgTimeStampUtc='{2}'", (object) r2MatchedPairData.m_sensorCount, (object) r2MatchedPairData.m_smbgGlucose, (object) R2ReceiverValues.R2BaseDateTime.AddMilliseconds((double) r2MatchedPairData.m_smbgTimeStampUtc).ToString("yyyy'-'MM'-'dd' 'hh':'mm':'ss'.'fff tt"));
        }
        return str;
      }
    }

    [ColumnInfo(DefaultWidth = 100, Ordinal = 28, Visible = true)]
    public string CalSetPointDisplay4
    {
      get
      {
        string str = string.Empty;
        if ((int) this.CalSetPoints >= 5)
        {
          R2MatchedPairData r2MatchedPairData = this.m_record.m_calibrationSet4;
          str = str + string.Format("SensorCount='{0}' SmbgGlucose='{1}' SmbgTimeStampUtc='{2}'", (object) r2MatchedPairData.m_sensorCount, (object) r2MatchedPairData.m_smbgGlucose, (object) R2ReceiverValues.R2BaseDateTime.AddMilliseconds((double) r2MatchedPairData.m_smbgTimeStampUtc).ToString("yyyy'-'MM'-'dd' 'hh':'mm':'ss'.'fff tt"));
        }
        return str;
      }
    }

    [ColumnInfo(DefaultWidth = 100, Ordinal = 29, Visible = true)]
    public string CalSetPointDisplay5
    {
      get
      {
        string str = string.Empty;
        if ((int) this.CalSetPoints >= 6)
        {
          R2MatchedPairData r2MatchedPairData = this.m_record.m_calibrationSet5;
          str = str + string.Format("SensorCount='{0}' SmbgGlucose='{1}' SmbgTimeStampUtc='{2}'", (object) r2MatchedPairData.m_sensorCount, (object) r2MatchedPairData.m_smbgGlucose, (object) R2ReceiverValues.R2BaseDateTime.AddMilliseconds((double) r2MatchedPairData.m_smbgTimeStampUtc).ToString("yyyy'-'MM'-'dd' 'hh':'mm':'ss'.'fff tt"));
        }
        return str;
      }
    }

    public STSCalibration5Record(byte[] data, int offset)
    {
      this.m_recordType = (R2RecordType) data[offset];
      this.m_record = (R2STSCalibration5Record) R2ReceiverTools.DatabaseRecordFactory(this.m_recordType, data, offset);
    }

    public override object GetInternalRecord()
    {
      return (object) this.m_record;
    }

    public override XmlElement ToXml()
    {
      return this.ToXml(new XmlDocument());
    }

    public override XmlElement ToXml(XmlDocument xOwner)
    {
      XObject xobject = new XObject("STSCalibrationRecord", xOwner);
      xobject.SetAttribute("RecordType", ((object) this.RecordType).ToString());
      xobject.SetAttribute("RecordNumber", this.RecordNumber);
      xobject.SetAttribute("TimeStampUtc", this.TimeStampUtc);
      xobject.SetAttribute("CorrectedTimeStampUtc", this.CorrectedTimeStampUtc);
      xobject.SetAttribute("UserTimeStamp", this.UserTimeStamp);
      xobject.SetAttribute("AlgorithmCalibrationState", this.AlgorithmCalibrationState);
      xobject.SetAttribute("AlgorithmCalibrationStateCode", this.AlgorithmCalibrationStateCode);
      xobject.SetAttribute("CalSetPoints", this.CalSetPoints);
      xobject.SetAttribute("SensorInsertionTime", this.SensorInsertionTime);
      xobject.SetAttribute("Slope", this.Slope);
      xobject.SetAttribute("Intercept", this.Intercept);
      xobject.SetAttribute("SensorLife", this.SensorLife);
      xobject.SetAttribute("SlopeAdjust", this.SlopeAdjust);
      xobject.SetAttribute("CountsAberrantPoints", this.CountsAberrantPoints);
      xobject.SetAttribute("AbsoluteAberrantPoints", this.AbsoluteAberrantPoints);
      xobject.SetAttribute("PowerAberrantPoints", this.PowerAberrantPoints);
      xobject.SetAttribute("CalSetPointDisplay0", this.CalSetPointDisplay0);
      xobject.SetAttribute("CalSetPointDisplay1", this.CalSetPointDisplay1);
      xobject.SetAttribute("CalSetPointDisplay2", this.CalSetPointDisplay2);
      xobject.SetAttribute("CalSetPointDisplay3", this.CalSetPointDisplay3);
      xobject.SetAttribute("CalSetPointDisplay4", this.CalSetPointDisplay4);
      xobject.SetAttribute("CalSetPointDisplay5", this.CalSetPointDisplay5);
      return xobject.Element;
    }
  }
}

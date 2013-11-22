// Type: Dexcom.R2Receiver.SensorSession
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using Dexcom.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Dexcom.R2Receiver
{
  public class SensorSession
  {
    private DateTime m_firstSensorFailureTime = CommonValues.EmptyDateTime;
    private STSCalibrationRecord m_startCalRecord;
    private STSCalibrationRecord m_finishCalRecord;
    private STSCalibrationRecord m_stopCalRecord;
    private STSCalibrationRecord m_firstInCalRecord;
    private bool m_didSessionFinish;
    private int m_countSensorRecords;
    private object m_tag;

    public List<MeterRecord> SessionMeterRecords { get; set; }

    public List<Sensor2Record> SessionSensorRecords { get; set; }

    public List<STSCalibrationRecord> SessionCalSetRecords { get; set; }

    public MatchedPairStat MatchedPairStats { get; set; }

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

    public DateTime FirstSensorFailureTime
    {
      get
      {
        return this.m_firstSensorFailureTime;
      }
      set
      {
        this.m_firstSensorFailureTime = value;
      }
    }

    public STSCalibrationRecord StartCalibrationRecord
    {
      get
      {
        return this.m_startCalRecord;
      }
      set
      {
        this.m_startCalRecord = value;
      }
    }

    public DateTime StartSystemTime
    {
      get
      {
        return this.StartInternalTime;
      }
    }

    public DateTime FinishSystemTime
    {
      get
      {
        return this.FinishInternalTime;
      }
    }

    public DateTime StartInternalTime
    {
      get
      {
        DateTime dateTime = CommonValues.EmptyDateTime;
        if (this.m_startCalRecord != null)
          dateTime = this.m_startCalRecord.TimeStampUtc;
        return dateTime;
      }
    }

    public DateTime StartDisplayTime
    {
      get
      {
        DateTime dateTime = CommonValues.EmptyDateTime;
        if (this.m_startCalRecord != null)
          dateTime = this.m_startCalRecord.UserTimeStamp;
        return dateTime;
      }
    }

    public STSCalibrationRecord FinishCalibrationRecord
    {
      get
      {
        return this.m_finishCalRecord;
      }
      set
      {
        this.m_finishCalRecord = value;
      }
    }

    public DateTime FinishInternalTime
    {
      get
      {
        DateTime dateTime = CommonValues.EmptyDateTime;
        if (this.SensorReinserted)
          dateTime = this.m_stopCalRecord.TimeStampUtc;
        else if (this.m_finishCalRecord != null)
          dateTime = this.m_finishCalRecord.TimeStampUtc;
        return dateTime;
      }
    }

    public DateTime FinishDisplayTime
    {
      get
      {
        DateTime dateTime = CommonValues.EmptyDateTime;
        if (this.SensorReinserted)
          dateTime = this.m_stopCalRecord.UserTimeStamp;
        else if (this.m_finishCalRecord != null)
          dateTime = this.m_finishCalRecord.UserTimeStamp;
        return dateTime;
      }
    }

    public STSCalibrationRecord StopCalibrationRecord
    {
      get
      {
        return this.m_stopCalRecord;
      }
      set
      {
        this.m_stopCalRecord = value;
      }
    }

    public STSCalibrationRecord FirstInCalRecord
    {
      get
      {
        return this.m_firstInCalRecord;
      }
      set
      {
        this.m_firstInCalRecord = value;
      }
    }

    public bool HasSessionStarted
    {
      get
      {
        return this.StartInternalTime != CommonValues.EmptyDateTime;
      }
    }

    public bool HasSessionFinished
    {
      get
      {
        return this.FinishInternalTime != CommonValues.EmptyDateTime;
      }
    }

    public bool SensorReinserted
    {
      get
      {
        bool flag = false;
        if (this.m_stopCalRecord != null && this.m_stopCalRecord.SensorInsertionTime != R2ReceiverValues.EmptySensorInsertionTime && this.DidSessionFinish)
        {
          Trace.Assert(this.m_finishCalRecord != this.m_stopCalRecord, "Reinserted sensor should imply Finish record != Stop record");
          flag = true;
        }
        return flag;
      }
    }

    public bool SensorRemoved
    {
      get
      {
        bool flag = false;
        if (this.m_stopCalRecord != null && this.m_stopCalRecord.SensorInsertionTime == R2ReceiverValues.EmptySensorInsertionTime)
          flag = true;
        return flag;
      }
    }

    public bool DidSessionFinish
    {
      get
      {
        return this.m_didSessionFinish;
      }
      set
      {
        this.m_didSessionFinish = value;
      }
    }

    public TimeSpan SensorLife
    {
      get
      {
        TimeSpan timeSpan = TimeSpan.Zero;
        if (this.m_startCalRecord != null)
          timeSpan = this.m_startCalRecord.RecordType != R2RecordType.STS_Calibration1 ? TimeSpan.FromDays((double) this.m_startCalRecord.SensorLife) : TimeSpan.FromDays(3.0);
        return timeSpan;
      }
    }

    public TimeSpan Duration
    {
      get
      {
        DateTime startInternalTime = this.StartInternalTime;
        DateTime finishInternalTime = this.FinishInternalTime;
        if (startInternalTime != CommonValues.EmptyDateTime && finishInternalTime != CommonValues.EmptyDateTime)
          return finishInternalTime - startInternalTime;
        else
          return TimeSpan.Zero;
      }
    }

    public TimeSpan DurationFromSensorInsertion
    {
      get
      {
        DateTime dateTime = this.StartInternalTime;
        DateTime finishInternalTime = this.FinishInternalTime;
        if (this.m_startCalRecord != null)
          dateTime = this.m_startCalRecord.SensorInsertionTime;
        if (dateTime != CommonValues.EmptyDateTime && finishInternalTime != CommonValues.EmptyDateTime)
          return finishInternalTime - dateTime;
        else
          return TimeSpan.Zero;
      }
    }

    public bool IdealSession
    {
      get
      {
        return this.DidSessionFinish && !this.SensorReinserted && (this.SensorRemoved && !(this.Duration == TimeSpan.Zero)) && (!(this.SensorLife == TimeSpan.Zero) && !(this.Duration < this.SensorLife));
      }
    }

    public DateTime FirstInCalibrationDateTime
    {
      get
      {
        if (this.m_firstInCalRecord != null)
          return this.m_firstInCalRecord.TimeStampUtc;
        else
          return CommonValues.EmptyDateTime;
      }
    }

    public TimeSpan FirstInCalibrationTimeSpan
    {
      get
      {
        if (this.m_startCalRecord != null && this.m_firstInCalRecord != null)
          return this.m_firstInCalRecord.TimeStampUtc - this.m_startCalRecord.SensorInsertionTime;
        else
          return TimeSpan.Zero;
      }
    }

    public int CountCapturedSensorRecords
    {
      get
      {
        return this.m_countSensorRecords;
      }
      set
      {
        this.m_countSensorRecords = value;
      }
    }

    public int CountExpectedSensorRecords
    {
      get
      {
        int num = 0;
        if (this.Duration != TimeSpan.Zero)
          num = (int) (this.Duration.TotalMinutes / 5.0);
        return num;
      }
    }

    public double PercentageCapture
    {
      get
      {
        double num = 0.0;
        if (this.CountExpectedSensorRecords > 0)
        {
          num = Math.Round((double) this.CountCapturedSensorRecords / (double) this.CountExpectedSensorRecords * 100.0, 2);
          if (num > 100.0)
            num = 100.0;
        }
        return num;
      }
    }

    public SensorSession()
    {
      this.SessionMeterRecords = new List<MeterRecord>();
      this.SessionSensorRecords = new List<Sensor2Record>();
      this.SessionCalSetRecords = new List<STSCalibrationRecord>();
    }
  }
}

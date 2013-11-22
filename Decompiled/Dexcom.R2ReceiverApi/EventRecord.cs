// Type: Dexcom.R2Receiver.EventRecord
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using Dexcom.Common;
using System;
using System.Xml;

namespace Dexcom.R2Receiver
{
  public class EventRecord : IGenericRecord
  {
    private TimeSpan m_calculatedSkewOffset = TimeSpan.Zero;
    private TimeSpan m_calculatedUserOffset = TimeSpan.Zero;
    private R2EventRecord m_record;
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

    public byte EventTypeCode
    {
      get
      {
        return this.m_record.m_eventType;
      }
    }

    public string EventType
    {
      get
      {
        if (Enum.IsDefined(typeof (R2EventRecordType), (object) this.m_record.m_eventType))
          return ((object) (R2EventRecordType) this.m_record.m_eventType).ToString();
        else
          return "Unknown Event " + this.m_record.m_eventType.ToString();
      }
    }

    public byte Value0
    {
      get
      {
        return this.m_record.m_eventValue0;
      }
    }

    public ushort Value1
    {
      get
      {
        return this.m_record.m_eventValue1;
      }
    }

    public uint Value2
    {
      get
      {
        return this.m_record.m_eventValue2;
      }
    }

    public DateTime EventTimeStamp
    {
      get
      {
        return R2ReceiverValues.R2BaseDateTime.AddMilliseconds((double) this.m_record.m_timeStamp);
      }
    }

    public string DataDisplay
    {
      get
      {
        string str1 = "Unknown";
        if (Enum.IsDefined(typeof (R2EventRecordType), (object) this.m_record.m_eventType))
        {
          switch (this.m_record.m_eventType)
          {
            case (byte) 0:
              str1 = string.Format("Carbs: {0} grams", (object) this.Value1);
              break;
            case (byte) 1:
              str1 = string.Format("Insulin: {0} units", (object) (Convert.ToDouble(this.Value1) / 100.0));
              break;
            case (byte) 2:
              string str2 = "Unknown";
              if (Enum.IsDefined(typeof (R2ExerciseType), (object) this.Value1))
                str2 = ((object) (R2ExerciseType) this.Value1).ToString();
              if ((int) this.Value2 != 0)
              {
                TimeSpan timeSpan = TimeSpan.FromMilliseconds((double) this.Value2);
                str1 = string.Format("Exercise: {0}, {1}", (object) str2, (object) string.Format("{0} minutes", (object) timeSpan.TotalMinutes));
              }
              else
              {
                str1 = string.Format("Exercise: {0}", (object) str2);
              }
              break;
            case (byte) 3:
              str1 = "Health Incident : ";
              if (Enum.IsDefined(typeof (R2HealthType), (object) this.Value1))
              {
                switch (this.Value1)
                {
                  case (ushort) 1:
                    str1 = str1 + "Illness";
                    break;
                  case (ushort) 2:
                    str1 = str1 + "Stress";
                    break;
                  case (ushort) 3:
                    str1 = str1 + "High Symptoms";
                    break;
                  case (ushort) 4:
                    str1 = str1 + "Low Symptoms";
                    break;
                  case (ushort) 5:
                    str1 = str1 + "Cycle";
                    break;
                  case (ushort) 6:
                    str1 = str1 + "Alcohol";
                    break;
                  default:
                    str1 = str1 + "Unknown";
                    break;
                }
              }
              break;
          }
        }
        return str1;
      }
    }

    public string EventTypeExtended
    {
      get
      {
        string str1 = "Unknown";
        if (Enum.IsDefined(typeof (R2EventRecordType), (object) this.m_record.m_eventType))
        {
          switch (this.m_record.m_eventType)
          {
            case (byte) 0:
              str1 = "Carbs";
              break;
            case (byte) 1:
              str1 = "Insulin";
              break;
            case (byte) 2:
              string str2 = "Unknown";
              if (Enum.IsDefined(typeof (R2ExerciseType), (object) this.Value1))
                str2 = ((object) (R2ExerciseType) this.Value1).ToString();
              str1 = string.Format("Exercise {0}", (object) str2);
              break;
            case (byte) 3:
              string str3 = "Unknown";
              if (Enum.IsDefined(typeof (R2HealthType), (object) this.Value1))
                str3 = ((object) (R2HealthType) this.Value1).ToString();
              str1 = string.Format("Health {0}", (object) str3);
              break;
            default:
              str1 = "Unknown";
              break;
          }
        }
        return str1;
      }
    }

    public string Data
    {
      get
      {
        string str = string.Empty;
        if (Enum.IsDefined(typeof (R2EventRecordType), (object) this.m_record.m_eventType))
        {
          switch (this.m_record.m_eventType)
          {
            case (byte) 0:
              str = this.Value1.ToString();
              break;
            case (byte) 1:
              str = this.Value1.ToString();
              break;
            case (byte) 2:
              str = (this.Value2 / 60000U).ToString();
              break;
            case (byte) 3:
              str = string.Empty;
              break;
          }
        }
        return str;
      }
    }

    [ColumnInfo(DefaultWidth = 50, Ordinal = 0, Visible = false)]
    public uint BlockNumber
    {
      get
      {
        return this.m_record.m_key.m_block;
      }
    }

    public EventRecord(byte[] data, int offset)
    {
      this.m_record = (R2EventRecord) R2ReceiverTools.DatabaseRecordFactory(this.m_record.RecordType, data, offset);
    }

    public void SetCalculatedTimeOffsets(TimeSpan skewOffset, TimeSpan userOffset)
    {
      this.m_calculatedSkewOffset = skewOffset;
      this.m_calculatedUserOffset = userOffset;
    }

    public int GetInternalRecordSize()
    {
      return this.m_record.RecordSize;
    }

    public R2EventRecord GetInternalRecord()
    {
      return this.m_record;
    }

    public XmlElement ToXml()
    {
      return this.ToXml(new XmlDocument());
    }

    public XmlElement ToXml(XmlDocument xOwner)
    {
      XObject xobject = new XObject("EventRecord", xOwner);
      xobject.SetAttribute("RecordNumber", this.RecordNumber);
      xobject.SetAttribute("TimeStampUtc", this.TimeStampUtc);
      xobject.SetAttribute("CorrectedTimeStampUtc", this.CorrectedTimeStampUtc);
      xobject.SetAttribute("UserTimeStamp", this.UserTimeStamp);
      xobject.SetAttribute("EventTypeCode", this.EventTypeCode);
      xobject.SetAttribute("EventType", this.EventType);
      xobject.SetAttribute("Value0", this.Value0);
      xobject.SetAttribute("Value1", this.Value1);
      xobject.SetAttribute("Value2", this.Value2);
      xobject.SetAttribute("EventTimeStamp", this.EventTimeStamp);
      xobject.SetAttribute("DataDisplay", this.DataDisplay);
      return xobject.Element;
    }
  }
}

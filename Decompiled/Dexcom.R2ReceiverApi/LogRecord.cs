// Type: Dexcom.R2Receiver.LogRecord
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using Dexcom.Common;
using System;
using System.Xml;

namespace Dexcom.R2Receiver
{
  public class LogRecord : IGenericRecord
  {
    private TimeSpan m_calculatedSkewOffset = TimeSpan.Zero;
    private TimeSpan m_calculatedUserOffset = TimeSpan.Zero;
    private R2LogRecord m_record;
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
        if (Enum.IsDefined(typeof (R2LogEventType), (object) this.m_record.m_eventType))
          return ((object) (R2LogEventType) this.m_record.m_eventType).ToString();
        else
          return "Unknown Event " + this.m_record.m_eventType.ToString();
      }
    }

    public byte Data0
    {
      get
      {
        return this.m_record.m_data0;
      }
    }

    public ushort Data1
    {
      get
      {
        return this.m_record.m_data1;
      }
    }

    public ushort Data2
    {
      get
      {
        return this.m_record.m_data2;
      }
    }

    public string DataDisplay
    {
      get
      {
        string str1 = string.Empty;
        if (Enum.IsDefined(typeof (R2LogEventType), (object) this.m_record.m_eventType))
        {
          switch (this.m_record.m_eventType)
          {
            case (byte) 1:
              str1 = !Enum.IsDefined(typeof (R2ScreenType), (object) this.Data1) ? "Unknown Screen" : ((object) (R2ScreenType) this.Data1).ToString();
              break;
            case (byte) 2:
              str1 = string.Format("Total Recs={0}, New Recs={1}", (object) this.Data1, (object) this.Data2);
              break;
            case (byte) 3:
              string str2 = string.Empty;
              string str3;
              switch (this.Data1)
              {
                case (ushort) 0:
                  str3 = "Retry Failure";
                  break;
                case (ushort) 1:
                  str3 = "Parse Failure";
                  break;
                case (ushort) 2:
                  str3 = "Invalid Response";
                  break;
                case (ushort) 3:
                  str3 = "Display Time Out Of Range";
                  break;
                case (ushort) 4:
                  str3 = "Checksum Failure";
                  break;
                default:
                  str3 = "Unknown";
                  break;
              }
              str1 = string.Format("Code={0}, Meaning={1}, Line={2}", (object) this.Data1, (object) str3, (object) this.Data2);
              break;
            case (byte) 6:
              str1 = string.Format("CRC=0x{0:X}, TransmitterId={1}, Data=0x{2:X}", (object) this.Data0, (object) this.Data1, (object) this.Data2);
              break;
            case (byte) 7:
              str1 = string.Format("CRC=0x{0:X}, TransmitterId={1}, Data=0x{2:X}", (object) this.Data0, (object) this.Data1, (object) this.Data2);
              break;
            case (byte) 8:
              str1 = string.Format("BytesAvailable=0x{0:X}", (object) (((int) this.Data1 << 16) + (int) this.Data2));
              break;
            case (byte) 10:
              str1 = string.Format("StartReg={0}, RegA={1}, RegB={2}, RegC={3}, RegD={4}", (object) this.Data0, (object) (((int) this.Data1 & 65280) >> 16), (object) ((int) this.Data1 & (int) byte.MaxValue), (object) (((int) this.Data2 & 65280) >> 16), (object) ((int) this.Data2 & (int) byte.MaxValue));
              break;
            case (byte) 11:
              string str4 = string.Empty;
              string str5 = string.Empty;
              string str6 = "Unknown Meter Event";
              switch (this.Data1)
              {
                case (ushort) 0:
                  str6 = "Mark Bad Time";
                  str5 = "ReasonCode";
                  str4 = "Unknown Reason Code";
                  if (((int) this.Data2 & 1) != 0)
                    str4 = "Meter time out of range.";
                  if (((int) this.Data2 & 2) != 0)
                    str4 = "New record incorrect time.";
                  if (((int) this.Data2 & 4) != 0)
                    str4 = "New meter.";
                  if (((int) this.Data2 & 8) != 0)
                    str4 = "Duplicate time stamp.";
                  if (((int) this.Data2 & 16) != 0)
                  {
                    str4 = "User dismissed new meter.";
                    break;
                  }
                  else
                    break;
                case (ushort) 1:
                  str6 = "Unknown Meter Type";
                  break;
                case (ushort) 2:
                  str6 = "Meter Disconnected";
                  break;
                case (ushort) 3:
                  str6 = "Timeout";
                  str5 = "Line";
                  str4 = this.Data2.ToString();
                  break;
                case (ushort) 4:
                  str6 = "Cancel New Meter";
                  break;
                case (ushort) 5:
                  str6 = "Meter Time";
                  break;
              }
              str1 = !(str4 != string.Empty) ? string.Format("Event={0}", (object) str6) : string.Format("Event={0}, {1}={2}", (object) str6, (object) str5, (object) str4);
              break;
            case (byte) 12:
              uint num = ((uint) this.Data2 << 16) + (uint) this.Data1;
              str1 = string.Format("0x{0:X} ({1})", (object) num, (object) num);
              break;
            case (byte) 13:
              str1 = !Enum.IsDefined(typeof (RFWindowMode), (object) this.Data1) ? "Unknown RF Window Mode" : ((object) (RFWindowMode) this.Data1).ToString();
              break;
            case (byte) 14:
              str1 = string.Format("CRC=0x{0:X}, TransmitterCode={1}", (object) this.Data0, (object) R2ReceiverTools.ConvertTransmitterNumberToDisplayableCode((uint) this.Data2 << 16 | (uint) this.Data1));
              break;
            case (byte) 15:
              str1 = string.Format("Intermediate Glucose={0}", (object) this.Data1);
              break;
            case (byte) 16:
              str1 = !Enum.IsDefined(typeof (R2AlgoNote), (object) this.Data1) ? "Unknown Algo Note" : string.Format("{0}, Records={1}", (object) ((object) (R2AlgoNote) this.Data1).ToString(), (object) this.Data2);
              break;
            case (byte) 18:
              str1 = !Enum.IsDefined(typeof (R2ArrowDisplayState), (object) this.Data1) ? "Unknown Arrow Displayed" : ((object) (R2ArrowDisplayState) this.Data1).ToString();
              break;
          }
        }
        return str1;
      }
    }

    public uint BlockNumber
    {
      get
      {
        return this.m_record.m_key.m_block;
      }
    }

    public LogRecord(byte[] data, int offset)
    {
      this.m_record = (R2LogRecord) R2ReceiverTools.DatabaseRecordFactory(this.m_record.RecordType, data, offset);
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

    public R2LogRecord GetInternalRecord()
    {
      return this.m_record;
    }

    public XmlElement ToXml()
    {
      return this.ToXml(new XmlDocument());
    }

    public XmlElement ToXml(XmlDocument xOwner)
    {
      XObject xobject = new XObject("LogRecord", xOwner);
      xobject.SetAttribute("RecordNumber", this.RecordNumber);
      xobject.SetAttribute("TimeStampUtc", this.TimeStampUtc);
      xobject.SetAttribute("CorrectedTimeStampUtc", this.CorrectedTimeStampUtc);
      xobject.SetAttribute("UserTimeStamp", this.UserTimeStamp);
      xobject.SetAttribute("EventTypeCode", this.EventTypeCode);
      xobject.SetAttribute("EventType", this.EventType);
      xobject.SetAttribute("Data0", this.Data0);
      xobject.SetAttribute("Data1", this.Data1);
      xobject.SetAttribute("Data2", this.Data2);
      xobject.SetAttribute("DataDisplay", this.DataDisplay);
      return xobject.Element;
    }
  }
}

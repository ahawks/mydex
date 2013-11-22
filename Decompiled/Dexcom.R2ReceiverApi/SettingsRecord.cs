// Type: Dexcom.R2Receiver.SettingsRecord
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using Dexcom.Common;
using System;
using System.Xml;

namespace Dexcom.R2Receiver
{
  public class SettingsRecord : IGenericRecord
  {
    private TimeSpan m_calculatedSkewOffset = TimeSpan.Zero;
    private TimeSpan m_calculatedUserOffset = TimeSpan.Zero;
    private R2Settings2Record m_record;
    private R2Settings6Record m_record6;
    private byte m_record3_IsVibeOnly;
    private R2RecordType m_recordType;
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
        return this.m_recordType;
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

    public TimeSpan SkewOffset
    {
      get
      {
        return TimeSpan.FromMilliseconds((double) this.m_record.m_skewOffset);
      }
      set
      {
        this.m_record.m_skewOffset = this.m_record6.m_skewOffset = Convert.ToInt64(value.TotalMilliseconds);
      }
    }

    public TimeSpan UserOffset
    {
      get
      {
        return TimeSpan.FromMilliseconds((double) this.m_record.m_externalOffset);
      }
      set
      {
        this.m_record.m_externalOffset = this.m_record6.m_externalOffset = Convert.ToInt64(value.TotalMilliseconds);
      }
    }

    public uint LowGlucoseThreshold
    {
      get
      {
        return this.m_record.m_lowGlucoseThreshold;
      }
      set
      {
        this.m_record.m_lowGlucoseThreshold = value;
        this.m_record6.m_lowGlucoseThreshold = Convert.ToUInt16(value);
      }
    }

    public uint HighGlucoseThreshold
    {
      get
      {
        return this.m_record.m_highGlucoseThreshold;
      }
      set
      {
        this.m_record.m_highGlucoseThreshold = value;
        this.m_record6.m_highGlucoseThreshold = Convert.ToUInt16(value);
      }
    }

    public uint TransmitterSerialNumber
    {
      get
      {
        return this.m_record.m_transmitterSerialNumber;
      }
      set
      {
        this.m_record.m_transmitterSerialNumber = this.m_record6.m_transmitterSerialNumber = value;
      }
    }

    public string TransmitterCode
    {
      get
      {
        return R2ReceiverTools.ConvertTransmitterNumberToDisplayableCode(this.m_record.m_transmitterSerialNumber);
      }
    }

    public string MeterSerialNumber
    {
      get
      {
        return this.m_record.m_meterSerialNumber;
      }
    }

    public string GlucoseUnits
    {
      get
      {
        return (int) this.m_record.m_glucoseUnits != 0 ? "mmol/L" : "mg/dL";
      }
      set
      {
        if (value.ToUpper().Equals("MG/DL"))
          this.m_record.m_glucoseUnits = (byte) 0;
        else
          this.m_record.m_glucoseUnits = (byte) 1;
      }
    }

    public string ClockMode
    {
      get
      {
        return (int) this.m_record.m_clockMode != 0 ? "24hr" : "AM/PM";
      }
      set
      {
        if (value.ToUpper().Equals("AM/PM"))
          this.m_record.m_clockMode = (byte) 0;
        else
          this.m_record.m_clockMode = (byte) 1;
      }
    }

    public string DisplayMode
    {
      get
      {
        return (int) this.m_record.m_displayMode != 0 ? "Unblinded" : "Blinded";
      }
      set
      {
        if (value.Equals("Blinded"))
          this.m_record.m_displayMode = this.m_record6.m_displayMode = (byte) 0;
        else
          this.m_record.m_displayMode = this.m_record6.m_displayMode = (byte) 1;
      }
    }

    public byte GlucoseUnitsCode
    {
      get
      {
        return this.m_record.m_glucoseUnits;
      }
      set
      {
        this.m_record.m_glucoseUnits = value;
      }
    }

    public byte ClockModeCode
    {
      get
      {
        return this.m_record.m_clockMode;
      }
      set
      {
        this.m_record.m_clockMode = value;
      }
    }

    public byte DisplayModeCode
    {
      get
      {
        return this.m_record.m_displayMode;
      }
      set
      {
        this.m_record.m_displayMode = this.m_record6.m_displayMode = value;
      }
    }

    public uint RecordsInLastMeterTransfer
    {
      get
      {
        return this.m_record.m_recordsInLastMeterTransfer;
      }
    }

    public DateTime LastMeterTransferTime
    {
      get
      {
        return R2ReceiverValues.R2BaseDateTime.AddMilliseconds((double) this.m_record.m_lastMeterTransferTime);
      }
    }

    public DateTime LTSImplantTime
    {
      get
      {
        return R2ReceiverValues.R2BaseDateTime.AddMilliseconds((double) this.m_record.m_longTermSensorImplantTime);
      }
      set
      {
        this.m_record.m_longTermSensorImplantTime = this.m_record6.m_longTermSensorImplantTime = Convert.ToUInt64((value - R2ReceiverValues.R2BaseDateTime).TotalMilliseconds);
      }
    }

    public byte NumberOf7DayLicenses
    {
      get
      {
        return this.m_record.m_numberOf7DayLicenses;
      }
      set
      {
        this.m_record.m_numberOf7DayLicenses = value;
      }
    }

    public byte NumberOf3DayLicenses
    {
      get
      {
        return this.m_record.m_numberOf3DayLicenses;
      }
      set
      {
        this.m_record.m_numberOf3DayLicenses = value;
      }
    }

    public bool BeepOnReceive
    {
      get
      {
        return (int) this.m_record6.m_beepOnReceive != 0;
      }
      set
      {
        this.m_record6.m_beepOnReceive = value ? (byte) 1 : (byte) 0;
      }
    }

    public bool BacklightEnabled
    {
      get
      {
        return (int) this.m_record6.m_backlightEnabled != 0;
      }
      set
      {
        this.m_record6.m_backlightEnabled = value ? (byte) 1 : (byte) 0;
      }
    }

    public byte MeterType
    {
      get
      {
        return this.m_record6.m_meterType;
      }
      set
      {
        this.m_record6.m_meterType = value;
      }
    }

    public bool IsVibrateOnly
    {
      get
      {
        if (this.m_recordType == R2RecordType.Settings4 || this.m_recordType == R2RecordType.Settings5)
          return false;
        else
          return (int) this.m_record3_IsVibeOnly != 0;
      }
      set
      {
        this.m_record3_IsVibeOnly = value ? (byte) 1 : (byte) 0;
      }
    }

    public ushort HighSnooze
    {
      get
      {
        return this.m_record6.m_highSnooze;
      }
      set
      {
        this.m_record6.m_highSnooze = value;
      }
    }

    public ushort LowSnooze
    {
      get
      {
        return this.m_record6.m_lowSnooze;
      }
      set
      {
        this.m_record6.m_lowSnooze = value;
      }
    }

    public string LowGlucoseAlertType
    {
      get
      {
        if (Enum.IsDefined(typeof (R2AlertType), (object) this.m_record6.m_lowGlucoseAlertType))
          return Enum.GetName(typeof (R2AlertType), (object) this.m_record6.m_lowGlucoseAlertType);
        else
          return "Unknown";
      }
    }

    public string OutOfRangeAlertType
    {
      get
      {
        if (Enum.IsDefined(typeof (R2AlertType), (object) this.m_record6.m_outOfRangeAlertType))
          return Enum.GetName(typeof (R2AlertType), (object) this.m_record6.m_outOfRangeAlertType);
        else
          return "Unknown";
      }
    }

    public string HighGlucoseAlertType
    {
      get
      {
        if (Enum.IsDefined(typeof (R2AlertType), (object) this.m_record6.m_highGlucoseAlertType))
          return Enum.GetName(typeof (R2AlertType), (object) this.m_record6.m_highGlucoseAlertType);
        else
          return "Unknown";
      }
    }

    public string OtherAlertType
    {
      get
      {
        if (Enum.IsDefined(typeof (R2AlertType), (object) this.m_record6.m_otherAlertType))
          return Enum.GetName(typeof (R2AlertType), (object) this.m_record6.m_otherAlertType);
        else
          return "Unknown";
      }
    }

    public ushort OutOfRangeMinutes
    {
      get
      {
        return this.m_record6.m_outOfRangeMinutes;
      }
      set
      {
        this.m_record6.m_outOfRangeMinutes = value;
      }
    }

    public byte UpRate
    {
      get
      {
        return this.m_record6.m_upRate;
      }
      set
      {
        this.m_record6.m_upRate = value;
      }
    }

    public string UpRateAlertType
    {
      get
      {
        if (Enum.IsDefined(typeof (R2AlertType), (object) this.m_record6.m_upRateAlertType))
          return Enum.GetName(typeof (R2AlertType), (object) this.m_record6.m_upRateAlertType);
        else
          return "Unknown";
      }
    }

    public byte DownRate
    {
      get
      {
        return this.m_record6.m_downRate;
      }
      set
      {
        this.m_record6.m_downRate = value;
      }
    }

    public string DownRateAlertType
    {
      get
      {
        if (Enum.IsDefined(typeof (R2AlertType), (object) this.m_record6.m_downRateAlertType))
          return Enum.GetName(typeof (R2AlertType), (object) this.m_record6.m_downRateAlertType);
        else
          return "Unknown";
      }
    }

    public DateTime RtcResetTime
    {
      get
      {
        return R2ReceiverValues.R2BaseDateTime.AddMilliseconds((double) this.m_record6.m_rtcResetTime);
      }
    }

    public uint BlockNumber
    {
      get
      {
        return this.m_record.m_key.m_block;
      }
    }

    public SettingsRecord(byte[] data, int offset)
    {
      this.m_recordType = (R2RecordType) data[offset];
      if (this.m_recordType == R2RecordType.Settings6)
        this.SetR2Record((R2Settings6Record) R2ReceiverTools.DatabaseRecordFactory(this.m_recordType, data, offset));
      else if (this.m_recordType == R2RecordType.Settings5)
        this.SetR2Record((R2Settings5Record) R2ReceiverTools.DatabaseRecordFactory(this.m_recordType, data, offset));
      else if (this.m_recordType == R2RecordType.Settings4)
        this.SetR2Record((R2Settings4Record) R2ReceiverTools.DatabaseRecordFactory(this.m_recordType, data, offset));
      else if (this.m_recordType == R2RecordType.Settings3)
        this.SetR2Record((R2Settings3Record) R2ReceiverTools.DatabaseRecordFactory(this.m_recordType, data, offset));
      else if (this.m_recordType == R2RecordType.Settings2)
        this.SetR2Record((R2Settings2Record) R2ReceiverTools.DatabaseRecordFactory(this.m_recordType, data, offset));
      else
        this.SetR2Record((R2SettingsRecord) R2ReceiverTools.DatabaseRecordFactory(this.m_recordType, data, offset));
      this.m_calculatedSkewOffset = this.SkewOffset;
      this.m_calculatedUserOffset = this.UserOffset;
    }

    public SettingsRecord()
    {
      this.m_recordType = R2RecordType.Settings6;
      this.SetToDefaults();
    }

    public void SetR2Record(R2Settings6Record record)
    {
      this.m_recordType = R2RecordType.Settings6;
      this.m_record6 = record;
      this.m_record.m_header = record.m_header;
      this.m_record.m_key = record.m_key;
      this.m_record.m_skewOffset = record.m_skewOffset;
      this.m_record.m_externalOffset = record.m_externalOffset;
      this.m_record.m_lowGlucoseThreshold = (uint) record.m_lowGlucoseThreshold;
      this.m_record.m_highGlucoseThreshold = (uint) record.m_highGlucoseThreshold;
      this.m_record.m_transmitterSerialNumber = record.m_transmitterSerialNumber;
      this.m_record.m_meterSerialNumber = record.m_meterSerialNumber;
      this.m_record.m_glucoseUnits = (byte) 0;
      this.m_record.m_clockMode = (byte) 0;
      this.m_record.m_displayMode = record.m_displayMode;
      this.m_record.m_recordsInLastMeterTransfer = record.m_recordsInLastMeterTransfer;
      this.m_record.m_lastMeterTransferTime = record.m_lastMeterTransferTime;
      this.m_record.m_longTermSensorImplantTime = record.m_longTermSensorImplantTime;
      this.m_record.m_numberOf7DayLicenses = (byte) 0;
      this.m_record.m_numberOf3DayLicenses = (byte) 0;
      this.m_record.m_footer = record.m_footer;
    }

    public void SetR2Record(R2Settings5Record record)
    {
      this.m_recordType = R2RecordType.Settings5;
      this.m_record.m_header = record.m_header;
      this.m_record.m_key = record.m_key;
      this.m_record.m_skewOffset = record.m_skewOffset;
      this.m_record.m_externalOffset = record.m_externalOffset;
      this.m_record.m_lowGlucoseThreshold = (uint) record.m_lowGlucoseThreshold;
      this.m_record.m_highGlucoseThreshold = (uint) record.m_highGlucoseThreshold;
      this.m_record.m_transmitterSerialNumber = record.m_transmitterSerialNumber;
      this.m_record.m_meterSerialNumber = record.m_meterSerialNumber;
      this.m_record.m_glucoseUnits = (byte) 0;
      this.m_record.m_clockMode = (byte) 0;
      this.m_record.m_displayMode = record.m_displayMode;
      this.m_record.m_recordsInLastMeterTransfer = record.m_recordsInLastMeterTransfer;
      this.m_record.m_lastMeterTransferTime = record.m_lastMeterTransferTime;
      this.m_record.m_longTermSensorImplantTime = record.m_longTermSensorImplantTime;
      this.m_record.m_numberOf7DayLicenses = (byte) 0;
      this.m_record.m_numberOf3DayLicenses = (byte) 0;
      this.m_record.m_footer = record.m_footer;
      this.m_record6.m_header = record.m_header;
      this.m_record6.m_key = record.m_key;
      this.m_record6.m_skewOffset = record.m_skewOffset;
      this.m_record6.m_externalOffset = record.m_externalOffset;
      this.m_record6.m_lowGlucoseThreshold = record.m_lowGlucoseThreshold;
      this.m_record6.m_lowGlucoseAlertType = record.m_lowGlucoseAlertType;
      this.m_record6.m_outOfRangeAlertType = record.m_outOfRangeAlertType;
      this.m_record6.m_highGlucoseThreshold = record.m_highGlucoseThreshold;
      this.m_record6.m_highGlucoseAlertType = record.m_highGlucoseAlertType;
      this.m_record6.m_otherAlertType = record.m_otherAlertType;
      this.m_record6.m_transmitterSerialNumber = record.m_transmitterSerialNumber;
      this.m_record6.m_meterSerialNumber = record.m_meterSerialNumber;
      this.m_record6.m_beepOnReceive = record.m_beepOnReceive;
      this.m_record6.m_backlightEnabled = record.m_backlightEnabled;
      this.m_record6.m_displayMode = record.m_displayMode;
      this.m_record6.m_upRateAlertType = record.m_upRateAlertType;
      this.m_record6.m_recordsInLastMeterTransfer = record.m_recordsInLastMeterTransfer;
      this.m_record6.m_highSnooze = record.m_highSnooze;
      this.m_record6.m_meterType = record.m_meterType;
      this.m_record6.m_downRateAlertType = record.m_downRateAlertType;
      this.m_record6.m_lowSnooze = record.m_lowSnooze;
      this.m_record6.m_outOfRangeMinutes = record.m_outOfRangeMinutes;
      this.m_record6.m_upRate = record.m_upRate;
      this.m_record6.m_downRate = record.m_downRate;
      this.m_record6.m_reserved = record.m_reserved;
      this.m_record6.m_longTermSensorImplantTime = record.m_longTermSensorImplantTime;
      this.m_record6.m_lastMeterTransferTime = record.m_lastMeterTransferTime;
      this.m_record6.m_rtcResetTime = 0UL;
      this.m_record6.m_footer = record.m_footer;
    }

    public void SetR2Record(R2Settings4Record record)
    {
      this.m_recordType = R2RecordType.Settings4;
      this.m_record.m_header = record.m_header;
      this.m_record.m_key = record.m_key;
      this.m_record.m_skewOffset = record.m_skewOffset;
      this.m_record.m_externalOffset = record.m_externalOffset;
      this.m_record.m_lowGlucoseThreshold = (uint) record.m_lowGlucoseThreshold;
      this.m_record.m_highGlucoseThreshold = (uint) record.m_highGlucoseThreshold;
      this.m_record.m_transmitterSerialNumber = record.m_transmitterSerialNumber;
      this.m_record.m_meterSerialNumber = record.m_meterSerialNumber;
      this.m_record.m_glucoseUnits = (byte) 0;
      this.m_record.m_clockMode = (byte) 0;
      this.m_record.m_displayMode = record.m_displayMode;
      this.m_record.m_recordsInLastMeterTransfer = record.m_recordsInLastMeterTransfer;
      this.m_record.m_lastMeterTransferTime = record.m_lastMeterTransferTime;
      this.m_record.m_longTermSensorImplantTime = record.m_longTermSensorImplantTime;
      this.m_record.m_numberOf7DayLicenses = (byte) 0;
      this.m_record.m_numberOf3DayLicenses = (byte) 0;
      this.m_record.m_footer = record.m_footer;
      this.m_record6.m_header = record.m_header;
      this.m_record6.m_key = record.m_key;
      this.m_record6.m_skewOffset = record.m_skewOffset;
      this.m_record6.m_externalOffset = record.m_externalOffset;
      this.m_record6.m_lowGlucoseThreshold = Convert.ToUInt16(record.m_lowGlucoseThreshold);
      this.m_record6.m_lowGlucoseAlertType = (byte) 1;
      this.m_record6.m_outOfRangeAlertType = (byte) 3;
      this.m_record6.m_highGlucoseThreshold = Convert.ToUInt16(record.m_highGlucoseThreshold);
      this.m_record6.m_highGlucoseAlertType = (byte) 1;
      this.m_record6.m_otherAlertType = (byte) 1;
      this.m_record6.m_transmitterSerialNumber = record.m_transmitterSerialNumber;
      this.m_record6.m_meterSerialNumber = record.m_meterSerialNumber;
      this.m_record6.m_beepOnReceive = record.m_beepOnReceive;
      this.m_record6.m_backlightEnabled = record.m_backlightEnabled;
      this.m_record6.m_displayMode = record.m_displayMode;
      this.m_record6.m_upRateAlertType = (byte) 3;
      this.m_record6.m_recordsInLastMeterTransfer = record.m_recordsInLastMeterTransfer;
      this.m_record6.m_highSnooze = record.m_highSnooze;
      this.m_record6.m_meterType = record.m_meterType;
      this.m_record6.m_downRateAlertType = (byte) 3;
      this.m_record6.m_lowSnooze = record.m_lowSnooze;
      this.m_record6.m_outOfRangeMinutes = (ushort) 30;
      this.m_record6.m_upRate = (byte) 0;
      this.m_record6.m_downRate = (byte) 0;
      this.m_record6.m_lastMeterTransferTime = record.m_lastMeterTransferTime;
      this.m_record6.m_longTermSensorImplantTime = record.m_longTermSensorImplantTime;
      this.m_record6.m_rtcResetTime = 0UL;
      this.m_record6.m_footer = record.m_footer;
    }

    public void SetR2Record(R2Settings3Record record)
    {
      this.m_recordType = R2RecordType.Settings3;
      this.m_record3_IsVibeOnly = record.m_isVibeOnly;
      this.m_record.m_header = record.m_header;
      this.m_record.m_key = record.m_key;
      this.m_record.m_skewOffset = record.m_skewOffset;
      this.m_record.m_externalOffset = record.m_externalOffset;
      this.m_record.m_lowGlucoseThreshold = record.m_lowGlucoseThreshold;
      this.m_record.m_highGlucoseThreshold = record.m_highGlucoseThreshold;
      this.m_record.m_transmitterSerialNumber = record.m_transmitterSerialNumber;
      this.m_record.m_meterSerialNumber = record.m_meterSerialNumber;
      this.m_record.m_glucoseUnits = (byte) 0;
      this.m_record.m_clockMode = (byte) 0;
      this.m_record.m_displayMode = record.m_displayMode;
      this.m_record.m_recordsInLastMeterTransfer = record.m_recordsInLastMeterTransfer;
      this.m_record.m_lastMeterTransferTime = record.m_lastMeterTransferTime;
      this.m_record.m_longTermSensorImplantTime = record.m_longTermSensorImplantTime;
      this.m_record.m_numberOf7DayLicenses = (byte) 0;
      this.m_record.m_numberOf3DayLicenses = (byte) 0;
      this.m_record.m_footer = record.m_footer;
      this.m_record6.m_header = record.m_header;
      this.m_record6.m_key = record.m_key;
      this.m_record6.m_skewOffset = record.m_skewOffset;
      this.m_record6.m_externalOffset = record.m_externalOffset;
      this.m_record6.m_lowGlucoseThreshold = Convert.ToUInt16(record.m_lowGlucoseThreshold);
      this.m_record6.m_lowGlucoseAlertType = (byte) 1;
      this.m_record6.m_outOfRangeAlertType = (byte) 3;
      this.m_record6.m_highGlucoseThreshold = Convert.ToUInt16(record.m_highGlucoseThreshold);
      this.m_record6.m_highGlucoseAlertType = (byte) 1;
      this.m_record6.m_otherAlertType = (byte) 1;
      this.m_record6.m_transmitterSerialNumber = record.m_transmitterSerialNumber;
      this.m_record6.m_meterSerialNumber = record.m_meterSerialNumber;
      this.m_record6.m_beepOnReceive = record.m_beepOnReceive;
      this.m_record6.m_backlightEnabled = record.m_backlightEnabled;
      this.m_record6.m_displayMode = record.m_displayMode;
      this.m_record6.m_upRateAlertType = (byte) 3;
      this.m_record6.m_recordsInLastMeterTransfer = record.m_recordsInLastMeterTransfer;
      this.m_record6.m_highSnooze = record.m_highSnooze;
      this.m_record6.m_meterType = record.m_meterType;
      this.m_record6.m_downRateAlertType = (byte) 3;
      this.m_record6.m_lowSnooze = record.m_lowSnooze;
      this.m_record6.m_outOfRangeMinutes = (ushort) 30;
      this.m_record6.m_upRate = (byte) 0;
      this.m_record6.m_downRate = (byte) 0;
      this.m_record6.m_lastMeterTransferTime = record.m_lastMeterTransferTime;
      this.m_record6.m_longTermSensorImplantTime = record.m_longTermSensorImplantTime;
      this.m_record6.m_rtcResetTime = 0UL;
      this.m_record6.m_footer = record.m_footer;
    }

    public void SetR2Record(R2Settings2Record record)
    {
      this.m_recordType = R2RecordType.Settings2;
      this.m_record = record;
      this.m_record6.m_header = record.m_header;
      this.m_record6.m_key = record.m_key;
      this.m_record6.m_skewOffset = this.m_record.m_skewOffset;
      this.m_record6.m_externalOffset = this.m_record.m_externalOffset;
      this.m_record6.m_lowGlucoseThreshold = Convert.ToUInt16(this.m_record.m_lowGlucoseThreshold);
      this.m_record6.m_lowGlucoseAlertType = (byte) 1;
      this.m_record6.m_outOfRangeAlertType = (byte) 3;
      this.m_record6.m_highGlucoseThreshold = Convert.ToUInt16(this.m_record.m_highGlucoseThreshold);
      this.m_record6.m_highGlucoseAlertType = (byte) 1;
      this.m_record6.m_otherAlertType = (byte) 1;
      this.m_record6.m_transmitterSerialNumber = this.m_record.m_transmitterSerialNumber;
      this.m_record6.m_meterSerialNumber = this.m_record.m_meterSerialNumber;
      this.m_record6.m_beepOnReceive = (byte) 0;
      this.m_record6.m_backlightEnabled = (byte) 0;
      this.m_record6.m_displayMode = this.m_record.m_displayMode;
      this.m_record6.m_upRateAlertType = (byte) 3;
      this.m_record6.m_recordsInLastMeterTransfer = this.m_record.m_recordsInLastMeterTransfer;
      this.m_record6.m_highSnooze = (ushort) byte.MaxValue;
      this.m_record6.m_meterType = (byte) 1;
      this.m_record6.m_downRateAlertType = (byte) 3;
      this.m_record6.m_lowSnooze = (ushort) byte.MaxValue;
      this.m_record6.m_outOfRangeMinutes = (ushort) 30;
      this.m_record6.m_upRate = (byte) 0;
      this.m_record6.m_downRate = (byte) 0;
      this.m_record6.m_lastMeterTransferTime = this.m_record.m_lastMeterTransferTime;
      this.m_record6.m_longTermSensorImplantTime = this.m_record.m_longTermSensorImplantTime;
      this.m_record6.m_rtcResetTime = 0UL;
      this.m_record6.m_footer = record.m_footer;
    }

    public void SetR2Record(R2SettingsRecord record)
    {
      this.m_recordType = R2RecordType.Settings;
      this.m_record.m_header = record.m_header;
      this.m_record.m_key = record.m_key;
      this.m_record.m_skewOffset = record.m_skewOffset;
      this.m_record.m_externalOffset = record.m_externalOffset;
      this.m_record.m_lowGlucoseThreshold = record.m_lowGlucoseThreshold;
      this.m_record.m_highGlucoseThreshold = record.m_highGlucoseThreshold;
      this.m_record.m_transmitterSerialNumber = record.m_transmitterSerialNumber;
      this.m_record.m_meterSerialNumber = record.m_meterSerialNumber;
      this.m_record.m_glucoseUnits = (byte) 0;
      this.m_record.m_clockMode = (byte) 0;
      this.m_record.m_displayMode = record.m_displayMode;
      this.m_record.m_recordsInLastMeterTransfer = record.m_recordsInLastMeterTransfer;
      this.m_record.m_lastMeterTransferTime = record.m_lastMeterTransferTime;
      this.m_record.m_longTermSensorImplantTime = record.m_longTermSensorImplantTime;
      this.m_record.m_numberOf7DayLicenses = (byte) 0;
      this.m_record.m_numberOf3DayLicenses = (byte) 0;
      this.m_record.m_footer = record.m_footer;
      this.m_record6.m_header = record.m_header;
      this.m_record6.m_key = record.m_key;
      this.m_record6.m_skewOffset = record.m_skewOffset;
      this.m_record6.m_externalOffset = record.m_externalOffset;
      this.m_record6.m_lowGlucoseThreshold = Convert.ToUInt16(record.m_lowGlucoseThreshold);
      this.m_record6.m_lowGlucoseAlertType = (byte) 1;
      this.m_record6.m_outOfRangeAlertType = (byte) 3;
      this.m_record6.m_highGlucoseThreshold = Convert.ToUInt16(record.m_highGlucoseThreshold);
      this.m_record6.m_highGlucoseAlertType = (byte) 1;
      this.m_record6.m_otherAlertType = (byte) 1;
      this.m_record6.m_transmitterSerialNumber = record.m_transmitterSerialNumber;
      this.m_record6.m_meterSerialNumber = record.m_meterSerialNumber;
      this.m_record6.m_beepOnReceive = (byte) 0;
      this.m_record6.m_backlightEnabled = (byte) 0;
      this.m_record6.m_displayMode = record.m_displayMode;
      this.m_record6.m_upRateAlertType = (byte) 3;
      this.m_record6.m_recordsInLastMeterTransfer = record.m_recordsInLastMeterTransfer;
      this.m_record6.m_highSnooze = (ushort) byte.MaxValue;
      this.m_record6.m_meterType = (byte) 1;
      this.m_record6.m_downRateAlertType = (byte) 3;
      this.m_record6.m_lowSnooze = (ushort) byte.MaxValue;
      this.m_record6.m_outOfRangeMinutes = (ushort) 30;
      this.m_record6.m_upRate = (byte) 0;
      this.m_record6.m_downRate = (byte) 0;
      this.m_record6.m_lastMeterTransferTime = record.m_lastMeterTransferTime;
      this.m_record6.m_longTermSensorImplantTime = record.m_longTermSensorImplantTime;
      this.m_record6.m_rtcResetTime = 0UL;
      this.m_record6.m_footer = record.m_footer;
    }

    public void SetToDefaults()
    {
      this.m_record.m_skewOffset = 0L;
      this.m_record.m_externalOffset = 0L;
      this.m_record.m_lowGlucoseThreshold = 80U;
      this.m_record.m_highGlucoseThreshold = 200U;
      this.m_record.m_transmitterSerialNumber = 0U;
      this.m_record.m_meterSerialNumber = "";
      this.m_record.m_glucoseUnits = (byte) 0;
      this.m_record.m_clockMode = (byte) 0;
      this.m_record.m_displayMode = (byte) 1;
      this.m_record.m_recordsInLastMeterTransfer = 0U;
      this.m_record.m_lastMeterTransferTime = 0UL;
      this.m_record.m_longTermSensorImplantTime = ulong.MaxValue;
      this.m_record.m_numberOf7DayLicenses = (byte) 0;
      this.m_record.m_numberOf3DayLicenses = (byte) 0;
      this.m_record6.m_skewOffset = 0L;
      this.m_record6.m_externalOffset = 0L;
      this.m_record6.m_lowGlucoseThreshold = (ushort) 80;
      this.m_record6.m_lowGlucoseAlertType = (byte) 1;
      this.m_record6.m_outOfRangeAlertType = (byte) 3;
      this.m_record6.m_highGlucoseThreshold = (ushort) 200;
      this.m_record6.m_highGlucoseAlertType = (byte) 1;
      this.m_record6.m_otherAlertType = (byte) 1;
      this.m_record6.m_transmitterSerialNumber = 0U;
      this.m_record6.m_meterSerialNumber = "";
      this.m_record6.m_beepOnReceive = (byte) 0;
      this.m_record6.m_backlightEnabled = (byte) 0;
      this.m_record6.m_displayMode = (byte) 1;
      this.m_record6.m_upRateAlertType = (byte) 3;
      this.m_record6.m_recordsInLastMeterTransfer = 0U;
      this.m_record6.m_highSnooze = (ushort) byte.MaxValue;
      this.m_record6.m_meterType = (byte) 1;
      this.m_record6.m_downRateAlertType = (byte) 3;
      this.m_record6.m_lowSnooze = (ushort) byte.MaxValue;
      this.m_record6.m_outOfRangeMinutes = (ushort) 30;
      this.m_record6.m_upRate = (byte) 0;
      this.m_record6.m_downRate = (byte) 0;
      this.m_record6.m_lastMeterTransferTime = 0UL;
      this.m_record6.m_longTermSensorImplantTime = ulong.MaxValue;
      this.m_record6.m_rtcResetTime = 0UL;
    }

    public void SetCalculatedTimeOffsets(TimeSpan skewOffset, TimeSpan userOffset)
    {
      this.m_calculatedSkewOffset = skewOffset;
      this.m_calculatedUserOffset = userOffset;
    }

    public R2Settings6Record GetInternalRecordAsSetttings6Record()
    {
      return this.m_record6;
    }

    public R2Settings5Record GetInternalRecordAsSetttings5Record()
    {
      return new R2Settings5Record()
      {
        m_header = this.m_record6.m_header,
        m_key = this.m_record6.m_key,
        m_skewOffset = this.m_record6.m_skewOffset,
        m_externalOffset = this.m_record6.m_externalOffset,
        m_lowGlucoseThreshold = this.m_record6.m_lowGlucoseThreshold,
        m_lowGlucoseAlertType = this.m_record6.m_lowGlucoseAlertType,
        m_outOfRangeAlertType = this.m_record6.m_outOfRangeAlertType,
        m_highGlucoseThreshold = this.m_record6.m_highGlucoseThreshold,
        m_highGlucoseAlertType = this.m_record6.m_highGlucoseAlertType,
        m_otherAlertType = this.m_record6.m_otherAlertType,
        m_transmitterSerialNumber = this.m_record6.m_transmitterSerialNumber,
        m_meterSerialNumber = this.m_record6.m_meterSerialNumber,
        m_beepOnReceive = this.m_record6.m_beepOnReceive,
        m_backlightEnabled = this.m_record6.m_backlightEnabled,
        m_displayMode = this.m_record6.m_displayMode,
        m_upRateAlertType = this.m_record6.m_upRateAlertType,
        m_recordsInLastMeterTransfer = this.m_record6.m_recordsInLastMeterTransfer,
        m_highSnooze = this.m_record6.m_highSnooze,
        m_meterType = this.m_record6.m_meterType,
        m_downRateAlertType = this.m_record6.m_downRateAlertType,
        m_lowSnooze = this.m_record6.m_lowSnooze,
        m_outOfRangeMinutes = this.m_record6.m_outOfRangeMinutes,
        m_upRate = this.m_record6.m_upRate,
        m_downRate = this.m_record6.m_downRate,
        m_reserved = this.m_record6.m_reserved,
        m_longTermSensorImplantTime = this.m_record6.m_longTermSensorImplantTime,
        m_lastMeterTransferTime = this.m_record6.m_lastMeterTransferTime,
        m_footer = this.m_record6.m_footer
      };
    }

    public R2Settings4Record GetInternalRecordAsSettings4Record()
    {
      return new R2Settings4Record()
      {
        m_header = this.m_record6.m_header,
        m_key = this.m_record6.m_key,
        m_skewOffset = this.m_record6.m_skewOffset,
        m_externalOffset = this.m_record6.m_externalOffset,
        m_lowGlucoseThreshold = this.m_record6.m_lowGlucoseThreshold,
        m_lowGlucoseAlertType = this.m_record6.m_lowGlucoseAlertType,
        m_outOfRangeAlertType = this.m_record6.m_outOfRangeAlertType,
        m_highGlucoseThreshold = this.m_record6.m_highGlucoseThreshold,
        m_highGlucoseAlertType = this.m_record6.m_highGlucoseAlertType,
        m_otherAlertType = this.m_record6.m_otherAlertType,
        m_transmitterSerialNumber = this.m_record6.m_transmitterSerialNumber,
        m_meterSerialNumber = this.m_record6.m_meterSerialNumber,
        m_beepOnReceive = this.m_record6.m_beepOnReceive,
        m_backlightEnabled = this.m_record6.m_backlightEnabled,
        m_displayMode = this.m_record6.m_displayMode,
        m_unused = (byte) 0,
        m_recordsInLastMeterTransfer = this.m_record6.m_recordsInLastMeterTransfer,
        m_highSnooze = this.m_record6.m_highSnooze,
        m_meterType = this.m_record6.m_meterType,
        m_reserved = (byte) 0,
        m_lowSnooze = this.m_record6.m_lowSnooze,
        m_outOfRangeMinutes = this.m_record6.m_outOfRangeMinutes,
        m_longTermSensorImplantTime = this.m_record6.m_longTermSensorImplantTime,
        m_lastMeterTransferTime = this.m_record6.m_lastMeterTransferTime,
        m_footer = this.m_record6.m_footer
      };
    }

    public R2Settings3Record GetInternalRecordAsSettings3Record()
    {
      return new R2Settings3Record()
      {
        m_header = this.m_record6.m_header,
        m_key = this.m_record6.m_key,
        m_skewOffset = this.m_record6.m_skewOffset,
        m_externalOffset = this.m_record6.m_externalOffset,
        m_lowGlucoseThreshold = (uint) this.m_record6.m_lowGlucoseThreshold,
        m_highGlucoseThreshold = (uint) this.m_record6.m_highGlucoseThreshold,
        m_transmitterSerialNumber = this.m_record6.m_transmitterSerialNumber,
        m_meterSerialNumber = this.m_record6.m_meterSerialNumber,
        m_beepOnReceive = this.m_record6.m_beepOnReceive,
        m_backlightEnabled = this.m_record6.m_backlightEnabled,
        m_displayMode = this.m_record6.m_displayMode,
        m_unused = (byte) 0,
        m_recordsInLastMeterTransfer = this.m_record6.m_recordsInLastMeterTransfer,
        m_highSnooze = this.m_record6.m_highSnooze,
        m_meterType = this.m_record6.m_meterType,
        m_isVibeOnly = this.m_record3_IsVibeOnly,
        m_lowSnooze = this.m_record6.m_lowSnooze,
        m_lastMeterTransferTime = this.m_record6.m_lastMeterTransferTime,
        m_longTermSensorImplantTime = this.m_record6.m_longTermSensorImplantTime,
        m_footer = this.m_record6.m_footer
      };
    }

    public R2Settings2Record GetInternalRecordAsSettings2Record()
    {
      return this.m_record;
    }

    public R2SettingsRecord GetInternalRecordAsSettings1Record()
    {
      R2SettingsRecord r2SettingsRecord = new R2SettingsRecord();
      r2SettingsRecord.m_header = this.m_record.m_header;
      r2SettingsRecord.m_header.m_type = (byte) 8;
      r2SettingsRecord.m_key = this.m_record.m_key;
      r2SettingsRecord.m_clockMode = this.m_record.m_clockMode;
      r2SettingsRecord.m_displayMode = this.m_record.m_displayMode;
      r2SettingsRecord.m_externalOffset = this.m_record.m_externalOffset;
      r2SettingsRecord.m_glucoseUnits = this.m_record.m_glucoseUnits;
      r2SettingsRecord.m_highGlucoseThreshold = this.m_record.m_highGlucoseThreshold;
      r2SettingsRecord.m_lastMeterTransferTime = this.m_record.m_lastMeterTransferTime;
      r2SettingsRecord.m_longTermSensorImplantTime = this.m_record.m_longTermSensorImplantTime;
      r2SettingsRecord.m_lowGlucoseThreshold = this.m_record.m_lowGlucoseThreshold;
      r2SettingsRecord.m_meterSerialNumber = this.m_record.m_meterSerialNumber;
      r2SettingsRecord.m_recordsInLastMeterTransfer = this.m_record.m_recordsInLastMeterTransfer;
      r2SettingsRecord.m_skewOffset = this.m_record.m_skewOffset;
      r2SettingsRecord.m_transmitterSerialNumber = this.m_record.m_transmitterSerialNumber;
      r2SettingsRecord.m_footer = this.m_record.m_footer;
      r2SettingsRecord.m_footer.m_type = this.m_record.m_header.m_type;
      return r2SettingsRecord;
    }

    public int GetInternalRecordSize()
    {
      return R2ReceiverTools.GetRecordSize(this.m_recordType);
    }

    public XmlElement ToXml()
    {
      return this.ToXml(new XmlDocument());
    }

    public XmlElement ToXml(XmlDocument xOwner)
    {
      XObject xobject = new XObject("SettingsRecord", xOwner);
      xobject.SetAttribute("RecordType", ((object) this.m_recordType).ToString());
      xobject.SetAttribute("RecordNumber", this.RecordNumber);
      xobject.SetAttribute("TimeStampUtc", this.TimeStampUtc);
      xobject.SetAttribute("CorrectedTimeStampUtc", this.CorrectedTimeStampUtc);
      xobject.SetAttribute("UserTimeStamp", this.UserTimeStamp);
      xobject.SetAttribute("SkewOffset", this.SkewOffset);
      xobject.SetAttribute("UserOffset", this.UserOffset);
      xobject.SetAttribute("LowGlucoseThreshold", this.LowGlucoseThreshold);
      xobject.SetAttribute("HighGlucoseThreshold", this.HighGlucoseThreshold);
      xobject.SetAttribute("TransmitterSerialNumber", this.TransmitterSerialNumber);
      xobject.SetAttribute("TransmitterCode", this.TransmitterCode);
      xobject.SetAttribute("MeterSerialNumber", this.MeterSerialNumber);
      xobject.SetAttribute("GlucoseUnits", this.GlucoseUnits);
      xobject.SetAttribute("ClockMode", this.ClockMode);
      xobject.SetAttribute("DisplayMode", this.DisplayMode);
      xobject.SetAttribute("GlucoseUnitsCode", this.GlucoseUnitsCode);
      xobject.SetAttribute("ClockModeCode", this.ClockModeCode);
      xobject.SetAttribute("DisplayModeCode", this.DisplayModeCode);
      xobject.SetAttribute("RecordsInLastMeterTransfer", this.RecordsInLastMeterTransfer);
      xobject.SetAttribute("LastMeterTransferTime", this.LastMeterTransferTime);
      xobject.SetAttribute("LTSImplantTime", this.LTSImplantTime);
      xobject.SetAttribute("NumberOf7DayLicenses", this.NumberOf7DayLicenses);
      xobject.SetAttribute("NumberOf3DayLicenses", this.NumberOf3DayLicenses);
      xobject.SetAttribute("BeepOnReceive", this.BeepOnReceive);
      xobject.SetAttribute("BacklightEnabled", this.BacklightEnabled);
      xobject.SetAttribute("HighSnooze", this.HighSnooze);
      xobject.SetAttribute("LowSnooze", this.LowSnooze);
      xobject.SetAttribute("IsVibrateOnly", this.IsVibrateOnly);
      xobject.SetAttribute("LowGlucoseAlertType", this.LowGlucoseAlertType);
      xobject.SetAttribute("OutOfRangeAlertType", this.OutOfRangeAlertType);
      xobject.SetAttribute("HighGlucoseAlertType", this.HighGlucoseAlertType);
      xobject.SetAttribute("OtherAlertType", this.OtherAlertType);
      xobject.SetAttribute("OutOfRangeMinutes", this.OutOfRangeMinutes);
      xobject.SetAttribute("UpRate", this.UpRate);
      xobject.SetAttribute("UpRateAlertType", this.UpRateAlertType);
      xobject.SetAttribute("DownRate", this.DownRate);
      xobject.SetAttribute("DownRateAlertType", this.DownRateAlertType);
      xobject.SetAttribute("RtcResetTime", this.RtcResetTime);
      return xobject.Element;
    }
  }
}

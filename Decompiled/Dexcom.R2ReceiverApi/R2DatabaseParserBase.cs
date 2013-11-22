// Type: Dexcom.R2Receiver.R2DatabaseParserBase
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using Dexcom.Common;
using Dexcom.Common.Data;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Dexcom.R2Receiver
{
  public abstract class R2DatabaseParserBase
  {
    private SettingsRecord m_latestSettingsRecord = new SettingsRecord();
    private TimeSpan m_latestSkewOffset = TimeSpan.Zero;
    private TimeSpan m_latestUserOffset = TimeSpan.Zero;
    private int m_priorRecordOffset;
    private bool m_isStopParser;
    private int m_numBlocksParsed;
    private int m_numRecordsParsed;

    public int NumBlocksParsed
    {
      get
      {
        return this.m_numBlocksParsed;
      }
    }

    public int NumRecordsParsed
    {
      get
      {
        return this.m_numRecordsParsed;
      }
    }

    public SettingsRecord LatestSettingsRecord
    {
      get
      {
        return this.m_latestSettingsRecord;
      }
    }

    public TimeSpan LatestSkewOffset
    {
      get
      {
        return this.m_latestSkewOffset;
      }
      set
      {
        this.m_latestSkewOffset = value;
      }
    }

    public TimeSpan LatestUserOffset
    {
      get
      {
        return this.m_latestUserOffset;
      }
      set
      {
        this.m_latestUserOffset = value;
      }
    }

    public int PriorRecordOffset
    {
      get
      {
        return this.m_priorRecordOffset;
      }
    }

    public bool StopParser
    {
      get
      {
        lock (this)
          return this.m_isStopParser;
      }
      set
      {
        lock (this)
          this.m_isStopParser = value;
      }
    }

    public event ProgressEventHandler ParserProgress;

    protected virtual void BlockPrefixHandler(byte[] data, int offset)
    {
    }

    protected virtual void EndOfBlockEmptyHandler(byte[] data, int offset)
    {
    }

    protected virtual void EndOfBlockRecordHandler(byte[] data, int offset)
    {
    }

    protected virtual void LogRecordHandler(byte[] data, int offset)
    {
    }

    protected virtual void EventRecordHandler(byte[] data, int offset)
    {
    }

    protected virtual void MeterRecordHandler(byte[] data, int offset)
    {
    }

    protected virtual void SensorRecordHandler(byte[] data, int offset)
    {
    }

    protected virtual void Sensor2RecordHandler(byte[] data, int offset)
    {
    }

    protected virtual void SettingsRecordHandler(byte[] data, int offset)
    {
    }

    protected virtual void MatchedPairRecordHandler(byte[] data, int offset)
    {
    }

    protected virtual void LTSCalibrationRecordHandler(byte[] data, int offset)
    {
    }

    protected virtual void STSCalibrationRecordHandler(byte[] data, int offset)
    {
    }

    public virtual void Parse(byte[] data)
    {
      if (data == null)
        throw new ArgumentNullException("data");
      if (data.Length == 0)
        throw new ApplicationException("R2 Database is empty.");
      try
      {
        int index = 0;
        int num1 = 0;
        bool flag1 = false;
        this.StopParser = false;
        this.m_numBlocksParsed = 0;
        this.m_numRecordsParsed = 0;
        EventUtils.FireEventAsync((Delegate) this.ParserProgress, (object) this, (object) new ProgressEventArgs(ProgressEventArgs.ProgressState.Start, index, data.Length));
        while (!flag1 && !this.StopParser)
        {
          EventUtils.FireEventAsync((Delegate) this.ParserProgress, (object) this, (object) new ProgressEventArgs(ProgressEventArgs.ProgressState.Run, index, data.Length));
          object obj = DataTools.ConvertBytesToObject(data, index, typeof (R2DatabaseBlockPrefix));
          if (obj == null)
            throw new ApplicationException("Data does not appear to be an R2 database!");
          R2BlockHeader r2BlockHeader = ((R2DatabaseBlockPrefix) obj).m_blockHeader;
          if ((int) r2BlockHeader.m_headerTag != (int) R2ReceiverValues.BlockHeaderTag)
            throw new ApplicationException("Data does not appear to be an R2 database!");
          this.BlockPrefixHandler(data, index);
          ++this.m_numBlocksParsed;
          bool flag2;
          if ((int) r2BlockHeader.m_status == (int) R2ReceiverValues.BlockStatusUsed || (int) r2BlockHeader.m_status == (int) R2ReceiverValues.BlockStatusReadyToErase)
          {
            index += Marshal.SizeOf(r2BlockHeader.GetType());
            this.m_priorRecordOffset = 0;
            bool flag3 = false;
            int num2 = 0;
            while (!flag3 && !this.StopParser)
            {
              if (index - num1 >= R2ReceiverValues.BlockSize)
              {
                Trace.Assert(index - num1 == R2ReceiverValues.BlockSize, "Parsed too far past end of block.");
                this.EndOfBlockEmptyHandler(data, index);
                break;
              }
              else
              {
                byte num3 = data[index];
                if (Enum.IsDefined(typeof (R2RecordType), (object) num3))
                {
                  R2RecordType recordType = (R2RecordType) num3;
                  switch (recordType)
                  {
                    case R2RecordType.EndOfBlock:
                      this.EndOfBlockRecordHandler(data, index);
                      ++index;
                      flag3 = true;
                      continue;
                    case R2RecordType.Corrupted:
                      flag2 = true;
                      throw new ApplicationException(string.Format("Found unexpected record type {1} in database at offset 0x{0:X}", (object) index, (object) ((object) recordType).ToString()));
                    case R2RecordType.Reserved0x02:
                    case R2RecordType.Reserved0x0E:
                    case R2RecordType.Reserved0x0F:
                      ++this.m_numRecordsParsed;
                      this.LTSCalibrationRecordHandler(data, index);
                      this.m_priorRecordOffset = index;
                      index += R2ReceiverTools.GetRecordSize(recordType);
                      continue;
                    case R2RecordType.Log:
                      ++this.m_numRecordsParsed;
                      this.LogRecordHandler(data, index);
                      this.m_priorRecordOffset = index;
                      index += R2ReceiverTools.GetRecordSize(recordType);
                      continue;
                    case R2RecordType.Meter:
                    case R2RecordType.Meter2:
                    case R2RecordType.Meter3:
                      ++this.m_numRecordsParsed;
                      this.MeterRecordHandler(data, index);
                      this.m_priorRecordOffset = index;
                      index += R2ReceiverTools.GetRecordSize(recordType);
                      continue;
                    case R2RecordType.BadTimeMeter:
                      flag2 = true;
                      throw new ApplicationException(string.Format("Found illegal record type {1} in database at offset 0x{0:X}", (object) index, (object) ((object) recordType).ToString()));
                    case R2RecordType.Sensor:
                      ++this.m_numRecordsParsed;
                      this.SensorRecordHandler(data, index);
                      this.m_priorRecordOffset = index;
                      index += R2ReceiverTools.GetRecordSize(recordType);
                      continue;
                    case R2RecordType.MatchedPair:
                      ++this.m_numRecordsParsed;
                      this.MatchedPairRecordHandler(data, index);
                      this.m_priorRecordOffset = index;
                      index += R2ReceiverTools.GetRecordSize(recordType);
                      continue;
                    case R2RecordType.Settings:
                    case R2RecordType.Settings2:
                    case R2RecordType.Settings3:
                    case R2RecordType.Settings4:
                    case R2RecordType.Settings5:
                    case R2RecordType.Settings6:
                      ++this.m_numRecordsParsed;
                      SettingsRecord settingsRecord = new SettingsRecord(data, index);
                      this.m_latestSettingsRecord = settingsRecord;
                      this.m_latestSkewOffset = settingsRecord.SkewOffset;
                      this.m_latestUserOffset = settingsRecord.UserOffset;
                      this.SettingsRecordHandler(data, index);
                      this.m_priorRecordOffset = index;
                      index += R2ReceiverTools.GetRecordSize(recordType);
                      continue;
                    case R2RecordType.STS_Calibration1:
                    case R2RecordType.STS_Calibration2:
                    case R2RecordType.STS_Calibration3:
                    case R2RecordType.STS_Calibration4:
                    case R2RecordType.STS_Calibration5:
                      ++this.m_numRecordsParsed;
                      this.STSCalibrationRecordHandler(data, index);
                      this.m_priorRecordOffset = index;
                      index += R2ReceiverTools.GetRecordSize(recordType);
                      continue;
                    case R2RecordType.Sensor2:
                    case R2RecordType.Sensor3:
                      ++this.m_numRecordsParsed;
                      this.Sensor2RecordHandler(data, index);
                      this.m_priorRecordOffset = index;
                      index += R2ReceiverTools.GetRecordSize(recordType);
                      continue;
                    case R2RecordType.Event:
                      ++this.m_numRecordsParsed;
                      this.EventRecordHandler(data, index);
                      this.m_priorRecordOffset = index;
                      index += R2ReceiverTools.GetRecordSize(recordType);
                      continue;
                    case R2RecordType.Unassigned:
                      ++num2;
                      if (num2 > 128)
                      {
                        flag3 = true;
                        this.EndOfBlockEmptyHandler(data, index);
                      }
                      ++index;
                      continue;
                    default:
                      flag2 = true;
                      throw new ApplicationException(string.Format("Found unexpected/unknown record type in database at offset 0x{0:X}", (object) index));
                  }
                }
                else
                {
                  flag2 = true;
                  throw new ApplicationException(string.Format("Unknown record type 0x{0:X} found at offset = 0x{1:X}", (object) num3, (object) index));
                }
              }
            }
          }
          else if ((int) r2BlockHeader.m_status != (int) R2ReceiverValues.BlockStatusReadyToErase && (int) r2BlockHeader.m_status != (int) R2ReceiverValues.BlockStatusErased)
          {
            flag2 = true;
            throw new ApplicationException(string.Format("Unknown block type found at offset = 0x{0:X}", (object) index));
          }
          if (!flag1)
          {
            num1 += R2ReceiverValues.BlockSize;
            if (num1 >= data.Length)
              flag1 = true;
            index = num1;
          }
        }
      }
      finally
      {
        EventUtils.FireEventAsync((Delegate) this.ParserProgress, (object) this, (object) new ProgressEventArgs(ProgressEventArgs.ProgressState.Done, data.Length, data.Length));
      }
    }
  }
}

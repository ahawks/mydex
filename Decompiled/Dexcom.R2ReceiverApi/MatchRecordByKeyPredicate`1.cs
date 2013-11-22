// Type: Dexcom.R2Receiver.MatchRecordByKeyPredicate`1
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using System;
using System.Collections.Generic;

namespace Dexcom.R2Receiver
{
  public class MatchRecordByKeyPredicate<T> : IComparer<T> where T : class, IGenericRecord
  {
    private int m_recordNumber;
    private DateTime m_recordTimeStampUtc;

    public MatchRecordByKeyPredicate(int recordNumber, DateTime recordTimeStampUtc)
    {
      this.m_recordNumber = recordNumber;
      this.m_recordTimeStampUtc = recordTimeStampUtc;
    }

    public bool Match(T record)
    {
      if (record.RecordNumber == this.m_recordNumber)
        return record.TimeStampUtc == this.m_recordTimeStampUtc;
      else
        return false;
    }

    public int Compare(T x, T y)
    {
      if ((object) x == (object) y)
        return 0;
      if ((object) y != null && (object) x == null)
        return -1;
      int num = this.m_recordNumber;
      DateTime dateTime = this.m_recordTimeStampUtc;
      if ((object) y != null)
      {
        num = y.RecordNumber;
        dateTime = y.TimeStampUtc;
      }
      if (x.TimeStampUtc < dateTime)
        return -1;
      if (x.TimeStampUtc > dateTime)
        return 1;
      if (x.RecordNumber < num)
        return -1;
      return x.RecordNumber > num ? 1 : 0;
    }
  }
}

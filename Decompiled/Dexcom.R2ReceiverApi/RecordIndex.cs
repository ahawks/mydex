// Type: Dexcom.R2Receiver.RecordIndex
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

namespace Dexcom.R2Receiver
{
  public class RecordIndex
  {
    private int m_indexAllRecords = -1;
    private int m_indexSameRecords = -1;

    public int IndexAllRecords
    {
      get
      {
        return this.m_indexAllRecords;
      }
      set
      {
        this.m_indexAllRecords = value;
      }
    }

    public int IndexSameRecords
    {
      get
      {
        return this.m_indexSameRecords;
      }
      set
      {
        this.m_indexSameRecords = value;
      }
    }
  }
}

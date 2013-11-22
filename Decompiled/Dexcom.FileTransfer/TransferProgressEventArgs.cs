// Type: Dexcom.FileTransfer.TransferProgressEventArgs
// Assembly: Dexcom.FileTransfer, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 38934138-A845-4F5C-AA0D-8047C5BBDF07
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.FileTransfer.dll

using System;

namespace Dexcom.FileTransfer
{
  public class TransferProgressEventArgs : EventArgs
  {
    private int m_currentCount;
    private int m_totalCount;
    private TransferState m_currentState;

    public TransferState State
    {
      get
      {
        return this.m_currentState;
      }
      set
      {
        this.m_currentState = value;
      }
    }

    public int Count
    {
      get
      {
        return this.m_currentCount;
      }
      set
      {
        this.m_currentCount = value;
      }
    }

    public int TotalCount
    {
      get
      {
        return this.m_totalCount;
      }
      set
      {
        this.m_totalCount = value;
      }
    }

    public int Percentage
    {
      get
      {
        int num = 0;
        if (this.m_totalCount > 0)
          num = Convert.ToInt32((double) this.m_currentCount / (double) this.m_totalCount * 100.0);
        return num;
      }
    }

    public TransferProgressEventArgs(TransferState currentState, int currentCount, int totalCount)
    {
      this.m_currentState = currentState;
      this.m_currentCount = currentCount;
      this.m_totalCount = totalCount;
    }
  }
}

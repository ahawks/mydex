// Type: Dexcom.Common.ProgressEventArgs
// Assembly: Dexcom.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B95A59FD-1598-4EA8-B1F5-D812FE5A6802
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.dll

using System;

namespace Dexcom.Common
{
  public class ProgressEventArgs : EventArgs
  {
    private int m_currentCount;
    private int m_totalCount;
    private ProgressEventArgs.ProgressState m_currentState;

    public ProgressEventArgs.ProgressState State
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

    public ProgressEventArgs(ProgressEventArgs.ProgressState currentState, int currentCount, int totalCount)
    {
      this.m_currentState = currentState;
      this.m_currentCount = currentCount;
      this.m_totalCount = totalCount;
    }

    public enum ProgressState
    {
      Unknown,
      Start,
      Run,
      Done,
      Canceled,
      Aborted,
    }
  }
}

// Type: Dexcom.Common.PerformanceTimer
// Assembly: Dexcom.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B95A59FD-1598-4EA8-B1F5-D812FE5A6802
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.dll

using System.Runtime.InteropServices;
using System.Security;

namespace Dexcom.Common
{
  [SecuritySafeCritical]
  public class PerformanceTimer
  {
    private static long m_frequency;
    private long m_startTime;
    private long m_totalTime;
    private int m_totalHits;
    private int m_timerId;

    static PerformanceTimer()
    {
      PerformanceTimer.QueryPerformanceFrequency(out PerformanceTimer.m_frequency);
    }

    public PerformanceTimer()
    {
      this.Reset();
    }

    public PerformanceTimer(int timerId)
    {
      this.m_timerId = timerId;
      this.Reset();
    }

    [DllImport("Kernel32.dll")]
    private static bool QueryPerformanceCounter(out long lpPerformanceCount);

    [DllImport("Kernel32.dll")]
    private static bool QueryPerformanceFrequency(out long lpFrequency);

    public void Reset()
    {
      this.m_startTime = 0L;
      this.m_totalTime = 0L;
      this.m_totalHits = 0;
    }

    public void Start()
    {
      PerformanceTimer.QueryPerformanceCounter(out this.m_startTime);
    }

    public void Stop()
    {
      long lpPerformanceCount;
      PerformanceTimer.QueryPerformanceCounter(out lpPerformanceCount);
      this.m_totalTime += lpPerformanceCount - this.m_startTime;
      ++this.m_totalHits;
    }

    public double GetLapTime()
    {
      long lpPerformanceCount;
      PerformanceTimer.QueryPerformanceCounter(out lpPerformanceCount);
      long num1 = lpPerformanceCount - this.m_startTime;
      double num2 = 0.0;
      if (num1 > 0L && PerformanceTimer.m_frequency > 0L)
        num2 = (double) (ulong) ((double) num1 * 1000000.0 / (double) PerformanceTimer.m_frequency) / 1000.0;
      return num2;
    }

    public double GetElapsedTime()
    {
      double num = 0.0;
      if (this.m_totalTime > 0L && this.m_totalHits > 0 && PerformanceTimer.m_frequency > 0L)
        num = (double) (ulong) ((double) this.m_totalTime * 1000000.0 / (double) PerformanceTimer.m_frequency) / 1000.0;
      return num;
    }

    public double GetAverageTime()
    {
      double num = 0.0;
      if (this.m_totalTime > 0L && this.m_totalHits > 0 && PerformanceTimer.m_frequency > 0L)
        num = (double) (ulong) ((double) this.m_totalTime * 1000000.0 / (double) PerformanceTimer.m_frequency) / 1000.0 / (double) this.m_totalHits;
      return num;
    }

    public override string ToString()
    {
      double num1 = 0.0;
      double num2 = 0.0;
      if (this.m_totalTime > 0L && this.m_totalHits > 0 && PerformanceTimer.m_frequency > 0L)
      {
        num1 = (double) (ulong) ((double) this.m_totalTime * 1000000.0 / (double) PerformanceTimer.m_frequency) / 1000.0;
        num2 = num1 / (double) this.m_totalHits;
      }
      string str1 = string.Empty;
      string str2;
      if (this.m_totalHits > 1)
        str2 = string.Format("Timer{0:00}: Total={1:#.000}ms, Hits={2}, Avg={3:#.000}", (object) this.m_timerId, (object) num1, (object) this.m_totalHits, (object) num2);
      else
        str2 = string.Format("Timer{0:00}: Total={1:#.000}ms", (object) this.m_timerId, (object) num1);
      return str2;
    }
  }
}

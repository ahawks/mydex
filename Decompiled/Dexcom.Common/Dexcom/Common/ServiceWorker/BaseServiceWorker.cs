// Type: Dexcom.Common.ServiceWorker.BaseServiceWorker
// Assembly: Dexcom.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B95A59FD-1598-4EA8-B1F5-D812FE5A6802
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.dll

using System.Xml;

namespace Dexcom.Common.ServiceWorker
{
  public abstract class BaseServiceWorker : IServiceWorker
  {
    private XmlElement m_xServiceWorker;
    private bool m_isPaused;
    private bool m_isRunning;

    public XmlElement ServiceWorkerXml
    {
      get
      {
        lock (this)
          return this.m_xServiceWorker;
      }
      set
      {
        lock (this)
          this.m_xServiceWorker = value;
      }
    }

    public bool IsPaused
    {
      get
      {
        lock (this)
          return this.m_isPaused;
      }
      set
      {
        lock (this)
          this.m_isPaused = value;
      }
    }

    public bool IsRunning
    {
      get
      {
        lock (this)
          return this.m_isRunning;
      }
      set
      {
        lock (this)
          this.m_isRunning = value;
      }
    }

    public abstract void Execute();

    public virtual void Startup()
    {
      this.IsRunning = true;
    }

    public virtual void Shutdown()
    {
      this.IsRunning = false;
    }

    public virtual void Pause()
    {
      this.IsPaused = true;
    }

    public virtual void Continue()
    {
      this.IsPaused = false;
    }
  }
}

// Type: Dexcom.Common.Data.Context
// Assembly: Dexcom.Common.Data, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 64B9BFC3-C891-4315-B216-574914861A80
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.Data.dll

using System;
using System.Runtime.Serialization;

namespace Dexcom.Common.Data
{
  [DataContract]
  public class Context
  {
    private Guid m_systemId = Guid.Empty;
    private Guid m_sessionId = Guid.Empty;
    private Guid m_userId = Guid.Empty;

    [DataMember]
    public Guid SystemId
    {
      get
      {
        return this.m_systemId;
      }
      set
      {
        this.m_systemId = value;
      }
    }

    [DataMember]
    public Guid SessionId
    {
      get
      {
        return this.m_sessionId;
      }
      set
      {
        this.m_sessionId = value;
      }
    }

    [DataMember]
    public Guid UserId
    {
      get
      {
        return this.m_userId;
      }
      set
      {
        this.m_userId = value;
      }
    }

    public Context()
    {
    }

    public Context(Context context)
    {
      this.m_systemId = context.m_systemId;
      this.m_sessionId = context.m_sessionId;
      this.m_userId = context.m_userId;
    }

    public Context(XOnlineContext context)
    {
      this.m_systemId = context.SystemId;
      this.m_sessionId = context.SessionId;
      this.m_userId = context.UserId;
    }

    public static explicit operator XOnlineContext(Context context)
    {
      return new XOnlineContext()
      {
        SystemId = context.SystemId,
        SessionId = context.SessionId,
        UserId = context.UserId
      };
    }

    public static explicit operator Context(XOnlineContext context)
    {
      return new Context(context);
    }

    public void Empty()
    {
      this.m_systemId = Guid.Empty;
      this.m_sessionId = Guid.Empty;
      this.m_userId = Guid.Empty;
    }

    public bool IsValid()
    {
      bool flag = false;
      if (this.m_systemId != Guid.Empty && this.m_sessionId != Guid.Empty && this.m_userId != Guid.Empty)
        flag = true;
      return flag;
    }

    public override string ToString()
    {
      return new XOnlineContext()
      {
        SystemId = this.m_systemId,
        SessionId = this.m_sessionId,
        UserId = this.m_userId
      }.FullOuterXml();
    }
  }
}

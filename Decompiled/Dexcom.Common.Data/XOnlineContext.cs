// Type: Dexcom.Common.Data.XOnlineContext
// Assembly: Dexcom.Common.Data, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 64B9BFC3-C891-4315-B216-574914861A80
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.Data.dll

using Dexcom.Common;
using System;
using System.Runtime.Serialization;
using System.Security;
using System.Xml;
using System.Xml.Serialization;

namespace Dexcom.Common.Data
{
  [Serializable]
  public class XOnlineContext : XObject, ISerializable
  {
    private Guid m_systemId = Guid.Empty;
    private Guid m_sessionId = Guid.Empty;
    private Guid m_userId = Guid.Empty;
    public const string Tag = "OnlineContext";

    [XmlAttribute]
    public Guid SystemId
    {
      get
      {
        return this.m_systemId;
      }
      set
      {
        this.m_systemId = value;
        this.SetAttribute("SystemId", this.m_systemId);
      }
    }

    [XmlAttribute]
    public Guid SessionId
    {
      get
      {
        return this.m_sessionId;
      }
      set
      {
        this.m_sessionId = value;
        this.SetAttribute("SessionId", this.m_sessionId);
      }
    }

    [XmlAttribute]
    public Guid UserId
    {
      get
      {
        return this.m_userId;
      }
      set
      {
        this.m_userId = value;
        this.SetAttribute("UserId", this.m_userId);
      }
    }

    public XOnlineContext()
      : this(new XmlDocument())
    {
    }

    public XOnlineContext(XmlDocument ownerDocument)
      : base("OnlineContext", ownerDocument)
    {
      this.SystemId = Guid.Empty;
      this.SessionId = Guid.Empty;
      this.UserId = Guid.Empty;
    }

    public XOnlineContext(XmlElement element)
      : base(element)
    {
      this.DoSyncCachedMembers();
    }

    protected XOnlineContext(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this.DoSyncCachedMembers();
    }

    private void DoSyncCachedMembers()
    {
      this.SystemId = this.GetAttributeAsGuid("SystemId");
      this.SessionId = this.GetAttributeAsGuid("SessionId");
      this.UserId = this.GetAttributeAsGuid("UserId");
    }

    [SecurityCritical]
    public new void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      base.GetObjectData(info, context);
    }
  }
}

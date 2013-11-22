// Type: Dexcom.Common.OnlineException
// Assembly: Dexcom.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B95A59FD-1598-4EA8-B1F5-D812FE5A6802
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.dll

using System;
using System.Runtime.Serialization;
using System.Security;
using System.Xml;

namespace Dexcom.Common
{
  [Serializable]
  public class OnlineException : DexComException, ISerializable
  {
    private ExceptionInfo m_info;

    public ExceptionInfo Info
    {
      get
      {
        return this.m_info;
      }
    }

    public OnlineException(ExceptionType eType)
    {
      this.m_info = new ExceptionInfo(eType);
    }

    public OnlineException(ExceptionType eType, string message)
      : base(message)
    {
      this.m_info = new ExceptionInfo(eType);
    }

    public OnlineException(ExceptionType eType, string message, Exception innerException)
      : base(message, innerException)
    {
      this.m_info = new ExceptionInfo(eType);
    }

    protected OnlineException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this.m_info = (ExceptionInfo) info.GetValue("OnlineException.m_info", typeof (ExceptionInfo));
    }

    public override string ToString()
    {
      return this.ToXml().OuterXml;
    }

    public virtual XmlElement ToXml()
    {
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.LoadXml("<OnlineException Id='' Type='' TypeString='' Category='' CategoryString='' Severity='' SeverityString='' DateThrown='' Message='' />");
      xmlDocument.DocumentElement.SetAttribute("Id", CommonTools.ConvertToString(this.m_info.Id));
      xmlDocument.DocumentElement.SetAttribute("Type", ((int) this.m_info.Type).ToString());
      xmlDocument.DocumentElement.SetAttribute("TypeString", ((object) this.m_info.Type).ToString());
      xmlDocument.DocumentElement.SetAttribute("Category", ((int) this.m_info.Category).ToString());
      xmlDocument.DocumentElement.SetAttribute("CategoryString", ((object) this.m_info.Category).ToString());
      xmlDocument.DocumentElement.SetAttribute("Severity", ((int) this.m_info.Severity).ToString());
      xmlDocument.DocumentElement.SetAttribute("SeverityString", ((object) this.m_info.Severity).ToString());
      xmlDocument.DocumentElement.SetAttribute("DateThrown", CommonTools.ConvertToString(this.m_info.DateThrown));
      xmlDocument.DocumentElement.SetAttribute("Message", this.Message);
      xmlDocument.DocumentElement.SetAttribute("FullText", base.ToString());
      xmlDocument.DocumentElement.SetAttribute("HostName", this.m_info.HostName);
      xmlDocument.DocumentElement.SetAttribute("HostIP", this.m_info.HostIP);
      return xmlDocument.DocumentElement;
    }

    public virtual OnlineException CopyForClient()
    {
      return new OnlineException(this.m_info.Type, this.Message)
      {
        m_info = {
          Id = this.m_info.Id,
          Category = this.m_info.Category,
          Severity = this.m_info.Severity,
          DateThrown = this.m_info.DateThrown,
          HostName = string.Empty,
          HostIP = string.Empty
        }
      };
    }

    [SecurityCritical]
    public new void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      base.GetObjectData(info, context);
      info.AddValue("OnlineException.m_info", (object) this.m_info);
    }
  }
}

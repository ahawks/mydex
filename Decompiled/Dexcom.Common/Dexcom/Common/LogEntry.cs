// Type: Dexcom.Common.LogEntry
// Assembly: Dexcom.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B95A59FD-1598-4EA8-B1F5-D812FE5A6802
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.dll

using System;
using System.Xml;

namespace Dexcom.Common
{
  public class LogEntry
  {
    private string m_message = string.Empty;
    private Guid m_id = Guid.NewGuid();
    private DateTime m_dateCreated = DateTime.Now;
    private string m_fileName = string.Empty;
    private string m_methodName = string.Empty;
    private LogEntryCategory m_category;
    private uint m_code;
    private int m_lineNumber;

    public XmlElement Content { get; set; }

    public string FileName
    {
      get
      {
        return this.m_fileName;
      }
      set
      {
        this.m_fileName = value;
      }
    }

    public int LineNumber
    {
      get
      {
        return this.m_lineNumber;
      }
      set
      {
        this.m_lineNumber = value;
      }
    }

    public string MethodName
    {
      get
      {
        return this.m_methodName;
      }
      set
      {
        this.m_methodName = value;
      }
    }

    public LogEntryCategory Category
    {
      get
      {
        return this.m_category;
      }
      set
      {
        this.m_category = value;
      }
    }

    public uint Code
    {
      get
      {
        return this.m_code;
      }
      set
      {
        this.m_code = value;
      }
    }

    public string Message
    {
      get
      {
        return this.m_message;
      }
      set
      {
        this.m_message = value;
      }
    }

    public LogEntry()
    {
    }

    public LogEntry(string message)
    {
      this.m_category = LogEntryCategory.Information;
      this.m_message = message;
    }

    public LogEntry(LogEntryCategory category, string message)
    {
      this.m_category = category;
      this.m_message = message;
    }

    public LogEntry(LogEntryCategory category, string message, uint code)
    {
      this.m_category = category;
      this.m_message = message;
      this.m_code = code;
    }

    public XmlElement ToXml()
    {
      return this.ToXml(new XmlDocument());
    }

    public XmlElement ToXml(XmlDocument ownerDoc)
    {
      XObject xobject = new XObject("LogEntry", ownerDoc);
      xobject.Id = this.m_id;
      xobject.SetAttribute("DateTimeCreatedLocal", this.m_dateCreated);
      xobject.SetAttribute("DateTimeCreatedUtc", this.m_dateCreated.ToUniversalTime());
      xobject.SetAttribute("Category", ((object) this.m_category).ToString());
      xobject.SetAttribute("Code", this.m_code);
      xobject.SetAttribute("Message", this.m_message);
      xobject.SetAttribute("File", this.m_fileName);
      xobject.SetAttribute("LineNumber", this.m_lineNumber);
      xobject.SetAttribute("Method", this.m_methodName);
      if (this.Content != null)
      {
        XObject xChildObject = new XObject("Content", ownerDoc);
        xChildObject.Element.AppendChild(ownerDoc.ImportNode((XmlNode) this.Content, true));
        xobject.AppendChild(xChildObject);
      }
      return xobject.Element;
    }
  }
}

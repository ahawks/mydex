// Type: Dexcom.Client.Common.AppLogger
// Assembly: Dexcom.Client.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B0B1988C-106E-413B-9428-09D6C6A8ED63
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Client.Common.dll

using Dexcom.Common;
using Dexcom.Common.Data;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Dexcom.Client.Common
{
  public static class AppLogger
  {
    private static object m_lock = new object();
    private static XmlDocument m_xAppLogDoc = (XmlDocument) null;
    private static XmlElement m_xLogEntries = (XmlElement) null;
    private static string m_dateTimeNameFormat = "yyyy'-'MM'-'dd";
    private static string m_strAppLogFolderPath = string.Empty;
    private static string m_strAppLogFilePath = string.Empty;

    public static bool IsInitialized { get; private set; }

    public static string AppLogFolderPath
    {
      get
      {
        return AppLogger.m_strAppLogFolderPath;
      }
    }

    public static string AppLogFilePath
    {
      get
      {
        return AppLogger.m_strAppLogFilePath;
      }
    }

    private static string DateTimeNameFormat
    {
      get
      {
        return AppLogger.m_dateTimeNameFormat;
      }
    }

    public static event AppLogger.AppLogEntryHandler AppLogEntry;

    static AppLogger()
    {
    }

    public static void AddAppLogEntry(LogEntry logEntry)
    {
      if (string.IsNullOrEmpty(logEntry.FileName))
      {
        StackFrame[] frames = new StackTrace(1, true).GetFrames();
        for (int index = 0; index < 2 && index < frames.Length; ++index)
        {
          StackFrame stackFrame = frames[index];
          MethodBase method = stackFrame.GetMethod();
          if (method != (MethodBase) null)
          {
            LogEntry logEntry1 = logEntry;
            string str = logEntry1.MethodName + ";" + method.Name;
            logEntry1.MethodName = str;
          }
          LogEntry logEntry2 = logEntry;
          string str1 = logEntry2.FileName + ";" + stackFrame.GetFileName();
          logEntry2.FileName = str1;
          int fileLineNumber = stackFrame.GetFileLineNumber();
          logEntry.LineNumber = fileLineNumber != 0 ? fileLineNumber : logEntry.LineNumber;
        }
      }
      EventUtils.FireEventAsync((Delegate) AppLogger.AppLogEntry, new object[1]
      {
        (object) logEntry.ToXml()
      });
    }

    public static void AddAppLogEntry(string message)
    {
      StackFrame[] frames = new StackTrace(1, true).GetFrames();
      LogEntry logEntry1 = new LogEntry(message);
      for (int index = 0; index < 2 && index < frames.Length; ++index)
      {
        StackFrame stackFrame = frames[index];
        MethodBase method = stackFrame.GetMethod();
        if (method != (MethodBase) null)
        {
          LogEntry logEntry2 = logEntry1;
          string str = logEntry2.MethodName + ";" + method.Name;
          logEntry2.MethodName = str;
        }
        LogEntry logEntry3 = logEntry1;
        string str1 = logEntry3.FileName + ";" + stackFrame.GetFileName();
        logEntry3.FileName = str1;
        int fileLineNumber = stackFrame.GetFileLineNumber();
        logEntry1.LineNumber = fileLineNumber != 0 ? fileLineNumber : logEntry1.LineNumber;
      }
      EventUtils.FireEventAsync((Delegate) AppLogger.AppLogEntry, new object[1]
      {
        (object) logEntry1.ToXml()
      });
    }

    public static void AddAppLogEntrySync(LogEntry logEntry)
    {
      if (string.IsNullOrEmpty(logEntry.FileName))
      {
        StackFrame[] frames = new StackTrace(1, true).GetFrames();
        for (int index = 0; index < 2 && index < frames.Length; ++index)
        {
          StackFrame stackFrame = frames[index];
          MethodBase method = stackFrame.GetMethod();
          if (method != (MethodBase) null)
          {
            LogEntry logEntry1 = logEntry;
            string str = logEntry1.MethodName + ";" + method.Name;
            logEntry1.MethodName = str;
          }
          LogEntry logEntry2 = logEntry;
          string str1 = logEntry2.FileName + ";" + stackFrame.GetFileName();
          logEntry2.FileName = str1;
          int fileLineNumber = stackFrame.GetFileLineNumber();
          logEntry.LineNumber = fileLineNumber != 0 ? fileLineNumber : logEntry.LineNumber;
        }
      }
      EventUtils.FireEvent((Delegate) AppLogger.AppLogEntry, new object[1]
      {
        (object) logEntry.ToXml()
      });
    }

    public static void AddAppLogEntrySync(string message)
    {
      StackFrame[] frames = new StackTrace(1, true).GetFrames();
      LogEntry logEntry1 = new LogEntry(message);
      for (int index = 0; index < 2 && index < frames.Length; ++index)
      {
        StackFrame stackFrame = frames[index];
        MethodBase method = stackFrame.GetMethod();
        if (method != (MethodBase) null)
        {
          LogEntry logEntry2 = logEntry1;
          string str = logEntry2.MethodName + ";" + method.Name;
          logEntry2.MethodName = str;
        }
        LogEntry logEntry3 = logEntry1;
        string str1 = logEntry3.FileName + ";" + stackFrame.GetFileName();
        logEntry3.FileName = str1;
        int fileLineNumber = stackFrame.GetFileLineNumber();
        logEntry1.LineNumber = fileLineNumber != 0 ? fileLineNumber : logEntry1.LineNumber;
      }
      EventUtils.FireEvent((Delegate) AppLogger.AppLogEntry, new object[1]
      {
        (object) logEntry1.ToXml()
      });
    }

    private static void OnAppLogEntry(XmlElement xLogEntry)
    {
      try
      {
        lock (AppLogger.m_lock)
        {
          if (!AppLogger.IsInitialized || AppLogger.m_xLogEntries == null || (AppLogger.m_xAppLogDoc == null || xLogEntry == null))
            return;
          AppLogger.m_xLogEntries.AppendChild(AppLogger.m_xAppLogDoc.ImportNode((XmlNode) xLogEntry, true));
          AppLogger.m_xAppLogDoc.Save(AppLogger.AppLogFilePath);
          if (!(AppLogger.AppLogFilePath != AppLogger.DoGetCurrentAppLogFilePath()))
            return;
          AppLogger.m_strAppLogFilePath = AppLogger.DoGetCurrentAppLogFilePath();
          AppLogger.DoCreateAppLog(AppLogger.AppLogFilePath);
          AppLogger.DoStartAppLog(AppLogger.AppLogFilePath);
        }
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(((object) ex).ToString());
      }
    }

    public static void Initialize(string appLogFolderPath, string dateTimeNameFormat)
    {
      AppLogger.m_dateTimeNameFormat = dateTimeNameFormat;
      AppLogger.Initialize(appLogFolderPath);
    }

    public static void Initialize(string appLogFolderPath)
    {
      AppLogger.m_strAppLogFolderPath = appLogFolderPath;
      DirectoryInfo directoryInfo = new DirectoryInfo(AppLogger.m_strAppLogFolderPath);
      if (!directoryInfo.Exists)
        directoryInfo.Create();
      AppLogger.AppLogEntry += new AppLogger.AppLogEntryHandler(AppLogger.OnAppLogEntry);
      AppLogger.m_strAppLogFilePath = AppLogger.DoGetCurrentAppLogFilePath();
      AppLogger.DoCreateAppLog(AppLogger.AppLogFilePath);
      AppLogger.DoStartAppLog(AppLogger.AppLogFilePath);
      AppLogger.IsInitialized = true;
    }

    public static XmlNodeList GetMessagesForCurrentLogSession()
    {
      lock (AppLogger.m_lock)
        return AppLogger.m_xLogEntries.SelectNodes("LogEntry");
    }

    private static string DoGetCurrentAppLogFilePath()
    {
      return Path.Combine(AppLogger.AppLogFolderPath, string.Format("App Log {0}.xml", (object) DateTime.Now.ToString(AppLogger.DateTimeNameFormat)));
    }

    private static void DoCreateAppLog(string strAppLogFilePath)
    {
      FileInfo fileInfo = new FileInfo(strAppLogFilePath);
      if (fileInfo.Exists)
      {
        XmlDocument xmlDocument = new XmlDocument();
        try
        {
          xmlDocument.Load(fileInfo.FullName);
        }
        catch
        {
          string destFileName = Path.Combine(AppLogger.AppLogFolderPath, string.Format("{0}-{1}.xml", (object) Path.GetFileNameWithoutExtension(fileInfo.FullName), (object) CommonTools.ConvertToString(Guid.NewGuid())));
          fileInfo.MoveTo(destFileName);
          fileInfo = new FileInfo(strAppLogFilePath);
        }
      }
      if (fileInfo.Exists)
        return;
      XmlDocument ownerDocument = new XmlDocument();
      XObject xobject = new XObject("AppLogs", ownerDocument);
      ownerDocument.AppendChild((XmlNode) xobject.Element);
      xobject.Id = Guid.NewGuid();
      DateTime now = DateTime.Now;
      xobject.SetAttribute("DateTimeCreatedLocal", now);
      xobject.SetAttribute("DateTimeCreatedUtc", now.ToUniversalTime());
      XApplicationInfo xapplicationInfo = DataTools.GetXApplicationInfo(ownerDocument);
      xobject.AppendChild((XObject) xapplicationInfo);
      XComputerInfo xcomputerInfo = DataTools.GetXComputerInfo(ownerDocument);
      xobject.AppendChild((XObject) xcomputerInfo);
      XmlDocument xmlDocument1 = new XmlDocument();
      xmlDocument1.PreserveWhitespace = true;
      xmlDocument1.LoadXml(CommonTools.FormatXml(ownerDocument.DocumentElement.OuterXml));
      XmlDeclaration xmlDeclaration = xmlDocument1.CreateXmlDeclaration("1.0", Encoding.UTF8.HeaderName, (string) null);
      xmlDocument1.InsertBefore((XmlNode) xmlDeclaration, (XmlNode) xmlDocument1.DocumentElement);
      xmlDocument1.Save(strAppLogFilePath);
    }

    private static void DoStartAppLog(string strAppLogFilePath)
    {
      if (!new FileInfo(strAppLogFilePath).Exists)
        throw new DexComException("Failed to create app log file.");
      XmlDocument ownerDocument = new XmlDocument();
      ownerDocument.Load(strAppLogFilePath);
      XObject xobject = new XObject("AppLog", ownerDocument);
      ownerDocument.DocumentElement.AppendChild((XmlNode) xobject.Element);
      xobject.Id = Guid.NewGuid();
      DateTime now = DateTime.Now;
      xobject.SetAttribute("DateTimeCreatedLocal", now);
      xobject.SetAttribute("DateTimeCreatedUtc", now.ToUniversalTime());
      XApplicationInfo xapplicationInfo = DataTools.GetXApplicationInfo(ownerDocument);
      xobject.AppendChild((XObject) xapplicationInfo);
      XComputerInfo xcomputerInfo = DataTools.GetXComputerInfo(ownerDocument);
      xobject.AppendChild((XObject) xcomputerInfo);
      XObject xChildObject = new XObject("LogEntries", ownerDocument);
      xobject.AppendChild(xChildObject);
      ownerDocument.Save(strAppLogFilePath);
      AppLogger.m_xAppLogDoc = ownerDocument;
      AppLogger.m_xLogEntries = xChildObject.Element;
    }

    public delegate void AppLogEntryHandler(XmlElement xLogEntry);
  }
}

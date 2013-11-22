// Type: Dexcom.Common.IProgramContext
// Assembly: Dexcom.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B95A59FD-1598-4EA8-B1F5-D812FE5A6802
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.dll

using System;

namespace Dexcom.Common
{
  public interface IProgramContext
  {
    string ApplicationName { get; }

    string ApplicationTitle { get; }

    string TemporaryFolderPath { get; }

    event EventHandler CultureChangedEvent;

    void AddSessionLogEntry(LogEntry logEntry);

    void AddSessionLogEntry(string message);

    void AddSessionLogEntrySync(LogEntry logEntry);

    void AddSessionLogEntrySync(string message);

    T Property<T>(string propertyName);

    bool HasProperty(string propertyName);

    string ResourceLookup(string resourceKey);

    string ResourceLookup(string resourceKey, params object[] args);
  }
}

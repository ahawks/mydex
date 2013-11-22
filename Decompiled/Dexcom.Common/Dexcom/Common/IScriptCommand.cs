// Type: Dexcom.Common.IScriptCommand
// Assembly: Dexcom.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B95A59FD-1598-4EA8-B1F5-D812FE5A6802
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.dll

using System.Xml;

namespace Dexcom.Common
{
  public interface IScriptCommand
  {
    bool Passed { get; }

    event CommandProgressHandler CommandProgress;

    void Verify(XmlElement commandXml);

    void Initialize(XmlElement commandXml);

    void Execute();

    XmlElement Results();
  }
}

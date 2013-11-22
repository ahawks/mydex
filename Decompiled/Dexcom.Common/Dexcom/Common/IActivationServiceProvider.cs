// Type: Dexcom.Common.IActivationServiceProvider
// Assembly: Dexcom.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B95A59FD-1598-4EA8-B1F5-D812FE5A6802
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.dll

using System.Collections.Generic;

namespace Dexcom.Common
{
  public interface IActivationServiceProvider
  {
    string GetRequestKey();

    List<string> GetRequestKeys();

    string GetActivationCode(string RequestKey);

    bool IsValidActivationCode(string activationCode);
  }
}

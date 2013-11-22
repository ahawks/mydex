// Type: Dexcom.Common.ProgramContext
// Assembly: Dexcom.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B95A59FD-1598-4EA8-B1F5-D812FE5A6802
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.dll

namespace Dexcom.Common
{
  public static class ProgramContext
  {
    private static IProgramContext m_programContext;

    public static IProgramContext Current
    {
      get
      {
        return ProgramContext.m_programContext;
      }
      set
      {
        ProgramContext.m_programContext = value;
      }
    }

    static ProgramContext()
    {
    }

    public static string TryResourceLookup(string resourceKey, string fallbackValue, params object[] args)
    {
      string str = fallbackValue;
      if (ProgramContext.Current != null)
      {
        try
        {
          str = ProgramContext.Current.ResourceLookup(resourceKey, args);
        }
        catch
        {
        }
      }
      return str;
    }
  }
}

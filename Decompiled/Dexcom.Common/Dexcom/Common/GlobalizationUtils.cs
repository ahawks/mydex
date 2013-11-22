// Type: Dexcom.Common.GlobalizationUtils
// Assembly: Dexcom.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B95A59FD-1598-4EA8-B1F5-D812FE5A6802
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.dll

using System;
using System.Collections.Generic;

namespace Dexcom.Common
{
  public class GlobalizationUtils
  {
    public static List<DayOfWeek> GetSystemDayOfWeek(List<DaysOfWeek> daysOfWeekList)
    {
      List<DayOfWeek> list = new List<DayOfWeek>();
      foreach (DaysOfWeek daysOfWeek in daysOfWeekList)
      {
        if (Enum.IsDefined(typeof (DayOfWeek), (object) ((object) daysOfWeek).ToString()))
          list.Add((DayOfWeek) Enum.Parse(typeof (DayOfWeek), ((object) daysOfWeek).ToString()));
      }
      return list;
    }
  }
}

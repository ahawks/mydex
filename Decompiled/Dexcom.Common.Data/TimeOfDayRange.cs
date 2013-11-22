// Type: Dexcom.Common.Data.TimeOfDayRange
// Assembly: Dexcom.Common.Data, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 64B9BFC3-C891-4315-B216-574914861A80
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.Data.dll

using Dexcom.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Xml;

namespace Dexcom.Common.Data
{
  public class TimeOfDayRange : ICloneable, IComparable
  {
    public static readonly TimeSpan Midnight = new TimeSpan(863999999999L);
    public static readonly Guid DefaultNighttimeRangeId = new Guid("{00000009-0000-0000-0000-000000000000}");
    private Guid m_id = Guid.NewGuid();
    private TimeSpan m_minTime = TimeSpan.MaxValue;
    private TimeSpan m_maxTime = TimeSpan.MaxValue;
    private MinRangeOperator m_minOperator = MinRangeOperator.GreaterThanOrEqual;
    private string m_name = string.Empty;
    private MaxRangeOperator m_maxOperator;
    private bool m_spansMidnight;
    private TimeOfDayRange m_beforeMidnightRange;
    private TimeOfDayRange m_afterMidnightRange;
    private int m_countMatches;

    public Guid Id
    {
      get
      {
        return this.m_id;
      }
      set
      {
        this.m_id = value;
      }
    }

    public string Name
    {
      get
      {
        return this.m_name;
      }
      set
      {
        this.m_name = value;
      }
    }

    public int CountMatches
    {
      get
      {
        return this.m_countMatches;
      }
      set
      {
        this.m_countMatches = value;
      }
    }

    public TimeSpan MinTime
    {
      get
      {
        return this.m_minTime;
      }
      private set
      {
        if (value >= TimeSpan.Zero && value <= TimeOfDayRange.Midnight)
        {
          this.m_minTime = value;
          this.DoCalculateSpansMidnight();
        }
        else
        {
          if (value != TimeSpan.MaxValue)
            throw new ArgumentOutOfRangeException("MinTime", "Time must be within a 24 hour period.");
          this.m_maxTime = value;
        }
      }
    }

    public TimeSpan MaxTime
    {
      get
      {
        return this.m_maxTime;
      }
      private set
      {
        if (value >= TimeSpan.Zero && value <= TimeOfDayRange.Midnight)
        {
          this.m_maxTime = value;
          this.DoCalculateSpansMidnight();
        }
        else
        {
          if (value != TimeSpan.MaxValue)
            throw new ArgumentOutOfRangeException("MaxTime", "Time must be within a 24 hour period.");
          this.m_maxTime = value;
        }
      }
    }

    public MinRangeOperator MinOperator
    {
      get
      {
        return this.m_minOperator;
      }
      private set
      {
        this.m_minOperator = value;
        this.DoCalculateSpansMidnight();
      }
    }

    public MaxRangeOperator MaxOperator
    {
      get
      {
        return this.m_maxOperator;
      }
      private set
      {
        this.m_maxOperator = value;
        this.DoCalculateSpansMidnight();
      }
    }

    public bool SpansMidnight
    {
      get
      {
        return this.m_spansMidnight;
      }
    }

    public TimeOfDayRange BeforeMidnightRange
    {
      get
      {
        return this.m_beforeMidnightRange;
      }
    }

    public TimeOfDayRange AfterMidnightRange
    {
      get
      {
        return this.m_afterMidnightRange;
      }
    }

    public TimeSpan Duration
    {
      get
      {
        TimeSpan timeSpan1 = TimeSpan.Zero;
        if (this.SpansMidnight)
          timeSpan1 = timeSpan1 + this.BeforeMidnightRange.Duration + this.AfterMidnightRange.Duration;
        else if (this.MinTime != TimeSpan.MaxValue || this.MaxTime != TimeSpan.MaxValue)
        {
          TimeSpan timeSpan2 = TimeSpan.Zero;
          TimeSpan timeSpan3 = TimeSpan.Zero;
          if (this.MinTime != TimeSpan.MaxValue)
          {
            timeSpan2 = this.MinTime;
            if (this.MinOperator == MinRangeOperator.GreaterThan)
              timeSpan2 += new TimeSpan(1L);
          }
          TimeSpan timeSpan4 = !(this.MaxTime != TimeSpan.MaxValue) ? TimeOfDayRange.Midnight : this.MaxTime;
          if (timeSpan4 == TimeOfDayRange.Midnight)
            timeSpan4 += new TimeSpan(1L);
          if (this.MaxOperator == MaxRangeOperator.LessThan)
            timeSpan4 -= new TimeSpan(1L);
          if (timeSpan4 > timeSpan2)
            timeSpan1 = timeSpan4 - timeSpan2;
        }
        return timeSpan1;
      }
    }

    public string ShortDisplayWithoutName
    {
      get
      {
        string str1 = string.Empty;
        if (this.MinTime == TimeSpan.MaxValue && this.MaxTime == TimeSpan.MaxValue)
          str1 = ProgramContext.TryResourceLookup("Common_Empty", "Empty", new object[0]);
        else if (this.MinTime != TimeSpan.MaxValue)
        {
          string str2 = str1 + TimeOfDayRange.ConvertToTimeString(this.MinTime);
          string str3 = "-";
          if (this.MaxTime == TimeSpan.MaxValue || this.MaxTime == TimeOfDayRange.Midnight)
            str1 = str2 + " " + str3 + " " + TimeOfDayRange.ConvertToTimeString(TimeOfDayRange.Midnight);
          else
            str1 = str2 + " " + str3 + " " + TimeOfDayRange.ConvertToTimeString(this.MaxTime);
        }
        else if (this.MaxTime != TimeSpan.MaxValue)
        {
          string str2 = "-";
          str1 = TimeOfDayRange.ConvertToTimeString(TimeOfDayRange.Midnight) + " " + str2 + " " + TimeOfDayRange.ConvertToTimeString(this.MaxTime);
        }
        return str1;
      }
    }

    public string ShortDisplay
    {
      get
      {
        string str = this.ShortDisplayWithoutName;
        if (!string.IsNullOrEmpty(str))
        {
          if (!string.IsNullOrEmpty(this.m_name))
            str = string.Format("{0} ({1})", (object) this.m_name, (object) str);
        }
        else if (!string.IsNullOrEmpty(this.m_name))
          str = str + this.m_name;
        return str;
      }
    }

    public string DisplayWithoutName
    {
      get
      {
        string str1 = string.Empty;
        if (this.MinTime == TimeSpan.MaxValue && this.MaxTime == TimeSpan.MaxValue)
          str1 = ProgramContext.TryResourceLookup("Common_Empty", "Empty", new object[0]);
        else if (this.MinTime != TimeSpan.MaxValue)
        {
          str1 = this.MinOperator != MinRangeOperator.GreaterThanOrEqual ? str1 + "> " + TimeOfDayRange.ConvertToTimeString(this.MinTime) : str1 + ">= " + TimeOfDayRange.ConvertToTimeString(this.MinTime);
          if (this.MaxTime != TimeSpan.MaxValue)
          {
            string str2 = ProgramContext.TryResourceLookup("Common_and", "and", new object[0]);
            if (this.MaxOperator == MaxRangeOperator.LessThanOrEqual)
              str1 = str1 + " " + str2 + " <= " + TimeOfDayRange.ConvertToTimeString(this.MaxTime);
            else
              str1 = str1 + " " + str2 + " < " + TimeOfDayRange.ConvertToTimeString(this.MaxTime);
          }
        }
        else if (this.MaxTime != TimeSpan.MaxValue)
          str1 = this.MaxOperator != MaxRangeOperator.LessThanOrEqual ? str1 + "< " + TimeOfDayRange.ConvertToTimeString(this.MaxTime) : str1 + "<= " + TimeOfDayRange.ConvertToTimeString(this.MaxTime);
        return str1;
      }
    }

    public string Display
    {
      get
      {
        string str = this.DisplayWithoutName;
        if (!string.IsNullOrEmpty(str))
        {
          if (!string.IsNullOrEmpty(this.m_name))
            str = string.Format("{0} ({1})", (object) this.m_name, (object) str);
        }
        else if (!string.IsNullOrEmpty(this.m_name))
          str = str + this.m_name;
        return str;
      }
    }

    static TimeOfDayRange()
    {
    }

    public TimeOfDayRange()
    {
    }

    public TimeOfDayRange(string name, MinRangeOperator minOp, TimeSpan minTime, MaxRangeOperator maxOp, TimeSpan maxTime)
    {
      this.Name = name;
      this.MinOperator = minOp;
      this.MaxOperator = maxOp;
      this.MinTime = minTime;
      this.MaxTime = maxTime;
    }

    public TimeOfDayRange(XTimeOfDayRange xTimeOfDayRange)
    {
      this.Id = xTimeOfDayRange.Id;
      this.Name = xTimeOfDayRange.Name;
      this.MinOperator = xTimeOfDayRange.MinOperator;
      this.MinTime = xTimeOfDayRange.MinTime;
      this.MaxOperator = xTimeOfDayRange.MaxOperator;
      this.MaxTime = xTimeOfDayRange.MaxTime;
    }

    public TimeOfDayRange Intersect(TimeOfDayRange other)
    {
      TimeOfDayRange timeOfDayRange = new TimeOfDayRange();
      timeOfDayRange.Name = "Intersect";
      DateTime dateTime1 = new DateTime(2010, 1, 1);
      DateTime dateTime2 = dateTime1;
      DateTime dateTime3 = dateTime2 + TimeSpan.FromDays(1.0);
      DateTime dateTime4 = dateTime1;
      DateTime dateTime5 = dateTime4 + TimeSpan.FromDays(1.0);
      if (this.SpansMidnight)
      {
        if (this.BeforeMidnightRange.MinTime != TimeSpan.MaxValue)
          dateTime2 += this.BeforeMidnightRange.MinTime;
        if (this.AfterMidnightRange.MaxTime != TimeSpan.MaxValue)
          dateTime3 += this.AfterMidnightRange.MaxTime;
      }
      else
      {
        if (this.MinTime != TimeSpan.MaxValue)
          dateTime2 += this.MinTime;
        if (this.MaxTime != TimeSpan.MaxValue)
          dateTime3 = dateTime1 + this.MaxTime;
      }
      if (other.SpansMidnight)
      {
        if (other.BeforeMidnightRange.MinTime != TimeSpan.MaxValue)
          dateTime4 += other.BeforeMidnightRange.MinTime;
        if (other.AfterMidnightRange.MaxTime != TimeSpan.MaxValue)
          dateTime5 += other.AfterMidnightRange.MaxTime;
      }
      else
      {
        if (other.MinTime != TimeSpan.MaxValue)
          dateTime4 += other.MinTime;
        if (other.MaxTime != TimeSpan.MaxValue)
          dateTime5 = dateTime1 + other.MaxTime;
      }
      DateTime dateTime6 = dateTime2.CompareTo(dateTime4) > 0 ? dateTime2 : dateTime4;
      DateTime dateTime7 = dateTime3.CompareTo(dateTime5) < 0 ? dateTime3 : dateTime5;
      if (dateTime6 < dateTime7)
        timeOfDayRange = new TimeOfDayRange("Intersect", MinRangeOperator.GreaterThanOrEqual, dateTime6.TimeOfDay, MaxRangeOperator.LessThanOrEqual, dateTime7.TimeOfDay);
      else if (this.SpansMidnight != other.SpansMidnight)
      {
        TimeSpan timeSpan = TimeSpan.FromDays(1.0);
        if (!this.SpansMidnight)
        {
          dateTime2 += timeSpan;
          dateTime3 += timeSpan;
        }
        else
        {
          dateTime4 += timeSpan;
          dateTime5 += timeSpan;
        }
        DateTime dateTime8 = dateTime2.CompareTo(dateTime4) > 0 ? dateTime2 : dateTime4;
        DateTime dateTime9 = dateTime3.CompareTo(dateTime5) < 0 ? dateTime3 : dateTime5;
        if (dateTime8 < dateTime9)
          timeOfDayRange = new TimeOfDayRange("Intersect", MinRangeOperator.GreaterThanOrEqual, dateTime8.TimeOfDay, MaxRangeOperator.LessThanOrEqual, dateTime9.TimeOfDay);
      }
      return timeOfDayRange;
    }

    public bool IsTimeInRange(TimeSpan timestamp)
    {
      bool flag = false;
      if (timestamp >= TimeSpan.Zero && timestamp <= TimeSpan.FromDays(1.0))
      {
        if (this.SpansMidnight)
          flag = timestamp == TimeOfDayRange.Midnight || timestamp == TimeSpan.FromDays(1.0) || timestamp == TimeSpan.Zero || (this.BeforeMidnightRange.IsTimeInRange(timestamp) || this.AfterMidnightRange.IsTimeInRange(timestamp));
        else if (this.MinTime != TimeSpan.MaxValue || this.MaxTime != TimeSpan.MaxValue)
        {
          if (timestamp == TimeSpan.FromDays(1.0))
            timestamp = TimeOfDayRange.Midnight;
          flag = !(this.MinTime == TimeSpan.MaxValue) ? (!(this.MaxTime == TimeSpan.MaxValue) ? (this.MinOperator == MinRangeOperator.GreaterThan ? timestamp > this.MinTime : timestamp >= this.MinTime) && (this.MaxOperator == MaxRangeOperator.LessThan ? timestamp < this.MaxTime : timestamp <= this.MaxTime) : (this.MinOperator == MinRangeOperator.GreaterThan ? timestamp > this.MinTime : timestamp >= this.MinTime)) : (this.MaxOperator == MaxRangeOperator.LessThan ? timestamp < this.MaxTime : timestamp <= this.MaxTime);
        }
        else
          flag = false;
      }
      return flag;
    }

    private void DoCalculateSpansMidnight()
    {
      this.m_spansMidnight = this.MinTime != TimeSpan.MaxValue && this.MaxTime != TimeSpan.MaxValue && this.MinTime > this.MaxTime;
      if (this.m_spansMidnight)
      {
        this.m_beforeMidnightRange = new TimeOfDayRange("BeforeMidnight", this.MinOperator, this.MinTime, MaxRangeOperator.LessThanOrEqual, TimeOfDayRange.Midnight);
        this.m_afterMidnightRange = new TimeOfDayRange("AfterMidnight", MinRangeOperator.GreaterThanOrEqual, TimeSpan.Zero, this.MaxOperator, this.MaxTime);
        Trace.Assert(!this.m_beforeMidnightRange.SpansMidnight, "Before Midnight Range can't itself span midnight!");
        Trace.Assert(!this.m_afterMidnightRange.SpansMidnight, "After Midnight Range can't itself span midnight!");
      }
      else
      {
        this.m_beforeMidnightRange = (TimeOfDayRange) null;
        this.m_afterMidnightRange = (TimeOfDayRange) null;
      }
    }

    public static string ConvertToTimeString(TimeSpan time)
    {
      string str = string.Empty;
      if (time == TimeOfDayRange.Midnight)
        time = TimeSpan.Zero;
      return new DateTime(time.Ticks).ToShortTimeString();
    }

    public static string ConvertToHourOfTheDayString(TimeSpan time)
    {
      string str = string.Empty;
      if (time == TimeOfDayRange.Midnight)
        time = TimeSpan.Zero;
      string format = DateTimeFormatInfo.CurrentInfo.ShortTimePattern.Replace(":mm", "").Replace(".mm", "") + " ";
      return new DateTime(time.Ticks).ToString(format).Replace(" ", "").ToLower();
    }

    public override string ToString()
    {
      return this.Display;
    }

    public bool IsMatch(DateTime compareTo)
    {
      return this.IsMatch(compareTo.TimeOfDay);
    }

    public bool IsMatch(TimeSpan compareTo)
    {
      bool flag = true;
      if (this.MinTime == TimeSpan.MaxValue && this.MaxTime == TimeSpan.MaxValue)
        flag = false;
      if (this.SpansMidnight)
      {
        flag = this.m_beforeMidnightRange.IsMatch(compareTo) || this.m_afterMidnightRange.IsMatch(compareTo);
      }
      else
      {
        if (this.MinTime != TimeSpan.MaxValue)
          flag = this.MinOperator != MinRangeOperator.GreaterThanOrEqual ? flag && compareTo > this.MinTime : flag && compareTo >= this.MinTime;
        if (flag && this.MaxTime != TimeSpan.MaxValue)
          flag = this.MaxOperator != MaxRangeOperator.LessThanOrEqual ? flag && compareTo < this.MaxTime : flag && compareTo <= this.MaxTime;
      }
      return flag;
    }

    public XmlElement ToXml()
    {
      return this.ToXml(new XmlDocument());
    }

    public XmlElement ToXml(XmlDocument ownerDocument)
    {
      XObject xobject = new XObject("TimeOfDayRange", ownerDocument);
      xobject.Id = this.Id;
      xobject.Name = this.Name;
      xobject.SetAttribute("MinOperator", ((object) this.MinOperator).ToString());
      xobject.SetAttribute("MinTime", this.MinTime);
      xobject.SetAttribute("MaxOperator", ((object) this.MaxOperator).ToString());
      xobject.SetAttribute("MaxTime", this.MaxTime);
      return xobject.Element;
    }

    public static List<TimeOfDayRange> GenerateDefaultTimesOfDayList()
    {
      List<TimeOfDayRange> list = new List<TimeOfDayRange>();
      list.Add(new TimeOfDayRange(ProgramContext.TryResourceLookup("Common_EarlyMorning", "Early Morning", new object[0]), MinRangeOperator.GreaterThanOrEqual, new TimeSpan(0, 0, 0), MaxRangeOperator.LessThan, new TimeSpan(6, 0, 0))
      {
        Id = new Guid("{00000001-0000-0000-0000-000000000000}")
      });
      list.Add(new TimeOfDayRange(ProgramContext.TryResourceLookup("Common_BeforeBreakfast", "Before Breakfast", new object[0]), MinRangeOperator.GreaterThanOrEqual, new TimeSpan(6, 0, 0), MaxRangeOperator.LessThan, new TimeSpan(8, 0, 0))
      {
        Id = new Guid("{00000002-0000-0000-0000-000000000000}")
      });
      list.Add(new TimeOfDayRange(ProgramContext.TryResourceLookup("Common_AfterBreakfast", "After Breakfast", new object[0]), MinRangeOperator.GreaterThanOrEqual, new TimeSpan(8, 0, 0), MaxRangeOperator.LessThan, new TimeSpan(11, 0, 0))
      {
        Id = new Guid("{00000003-0000-0000-0000-000000000000}")
      });
      list.Add(new TimeOfDayRange(ProgramContext.TryResourceLookup("Common_BeforeLunch", "Before Lunch", new object[0]), MinRangeOperator.GreaterThanOrEqual, new TimeSpan(11, 0, 0), MaxRangeOperator.LessThan, new TimeSpan(13, 0, 0))
      {
        Id = new Guid("{00000004-0000-0000-0000-000000000000}")
      });
      list.Add(new TimeOfDayRange(ProgramContext.TryResourceLookup("Common_AfterLunch", "After Lunch", new object[0]), MinRangeOperator.GreaterThanOrEqual, new TimeSpan(13, 0, 0), MaxRangeOperator.LessThan, new TimeSpan(16, 0, 0))
      {
        Id = new Guid("{00000005-0000-0000-0000-000000000000}")
      });
      list.Add(new TimeOfDayRange(ProgramContext.TryResourceLookup("Common_BeforeDinner", "Before Dinner", new object[0]), MinRangeOperator.GreaterThanOrEqual, new TimeSpan(16, 0, 0), MaxRangeOperator.LessThan, new TimeSpan(18, 0, 0))
      {
        Id = new Guid("{00000006-0000-0000-0000-000000000000}")
      });
      list.Add(new TimeOfDayRange(ProgramContext.TryResourceLookup("Common_AfterDinner", "After Dinner", new object[0]), MinRangeOperator.GreaterThanOrEqual, new TimeSpan(18, 0, 0), MaxRangeOperator.LessThan, new TimeSpan(21, 0, 0))
      {
        Id = new Guid("{00000007-0000-0000-0000-000000000000}")
      });
      list.Add(new TimeOfDayRange(ProgramContext.TryResourceLookup("Common_LateEvening", "Late Evening", new object[0]), MinRangeOperator.GreaterThanOrEqual, new TimeSpan(21, 0, 0), MaxRangeOperator.LessThan, TimeOfDayRange.Midnight)
      {
        Id = new Guid("{00000008-0000-0000-0000-000000000000}")
      });
      TimeOfDayRange timeOfDayRange = TimeOfDayRange.GenerateDefaultNighttimeRange();
      list.Insert(0, timeOfDayRange);
      return list;
    }

    public static TimeOfDayRange GenerateDefaultNighttimeRange()
    {
      return new TimeOfDayRange(ProgramContext.TryResourceLookup("Common_Nighttime", "Nighttime", new object[0]), MinRangeOperator.GreaterThanOrEqual, TimeSpan.FromHours(22.0), MaxRangeOperator.LessThanOrEqual, TimeSpan.FromHours(6.0))
      {
        Id = TimeOfDayRange.DefaultNighttimeRangeId
      };
    }

    public object Clone()
    {
      return this.MemberwiseClone();
    }

    public int CompareTo(object obj)
    {
      TimeOfDayRange timeOfDayRange = obj as TimeOfDayRange;
      if (timeOfDayRange == null)
        return 1;
      if (timeOfDayRange == this)
        return 0;
      if (this.MinTime < timeOfDayRange.MinTime)
        return -1;
      if (this.MinTime > timeOfDayRange.MinTime)
        return 1;
      if (this.MaxTime < timeOfDayRange.MaxTime)
        return -1;
      if (this.MaxTime > timeOfDayRange.MaxTime)
        return 1;
      if (this.MaxOperator == timeOfDayRange.MaxOperator)
        return 0;
      return this.MaxOperator == MaxRangeOperator.LessThan ? -1 : 1;
    }
  }
}

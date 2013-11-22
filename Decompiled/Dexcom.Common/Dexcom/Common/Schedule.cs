// Type: Dexcom.Common.Schedule
// Assembly: Dexcom.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B95A59FD-1598-4EA8-B1F5-D812FE5A6802
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.dll

using System;
using System.Xml;

namespace Dexcom.Common
{
  public class Schedule
  {
    public static readonly Schedule.DaysOfWeek EveryDay = Schedule.DaysOfWeek.Sunday | Schedule.DaysOfWeek.Monday | Schedule.DaysOfWeek.Tuesday | Schedule.DaysOfWeek.Wednesday | Schedule.DaysOfWeek.Thursday | Schedule.DaysOfWeek.Friday | Schedule.DaysOfWeek.Saturday;
    public static readonly Schedule.DaysOfWeek WeekDays = Schedule.DaysOfWeek.Monday | Schedule.DaysOfWeek.Tuesday | Schedule.DaysOfWeek.Wednesday | Schedule.DaysOfWeek.Thursday | Schedule.DaysOfWeek.Friday;
    public static readonly Schedule.DaysOfWeek WeekEnds = Schedule.DaysOfWeek.Sunday | Schedule.DaysOfWeek.Saturday;
    public static readonly Schedule.MonthsOfYear AllYear = Schedule.MonthsOfYear.January | Schedule.MonthsOfYear.February | Schedule.MonthsOfYear.March | Schedule.MonthsOfYear.April | Schedule.MonthsOfYear.May | Schedule.MonthsOfYear.June | Schedule.MonthsOfYear.July | Schedule.MonthsOfYear.August | Schedule.MonthsOfYear.September | Schedule.MonthsOfYear.October | Schedule.MonthsOfYear.November | Schedule.MonthsOfYear.December;
    private DateTime m_dateScheduleStart = DateTime.Today;
    private DateTime m_dateScheduleStop = CommonValues.EmptyDateTime;
    private TimeSpan m_timeStart = DateTime.Now.TimeOfDay;
    private int m_intervalCount = 1;
    private TimeSpan m_intervalTimeSpan = CommonValues.EmptyTimeSpan;
    private Schedule.DaysOfWeek m_daysOfTheWeek = Schedule.EveryDay;
    private Schedule.MonthsOfYear m_monthsOfTheYear = Schedule.AllYear;
    private bool m_useDayNumber = true;
    private int m_dayNumberInMonth = 1;
    private Schedule.DayOfMonth m_dayOfTheMonth = Schedule.DayOfMonth.First;
    private Schedule.DaysOfWeek m_dayOfTheWeek = Schedule.DaysOfWeek.Sunday;
    private Schedule.ScheduleType m_typeOfSchedule;
    private TimeSpan[] m_timesOfTheDay;

    public Schedule.ScheduleType TypeOfSchedule
    {
      get
      {
        return this.m_typeOfSchedule;
      }
      set
      {
        this.m_typeOfSchedule = value;
      }
    }

    public DateTime ScheduleStart
    {
      get
      {
        return this.m_dateScheduleStart;
      }
      set
      {
        this.m_dateScheduleStart = value.Date;
      }
    }

    public DateTime ScheduleStop
    {
      get
      {
        return this.m_dateScheduleStop;
      }
      set
      {
        this.m_dateScheduleStop = value;
      }
    }

    public TimeSpan TimeStart
    {
      get
      {
        return this.m_timeStart;
      }
      set
      {
        this.m_timeStart = value;
      }
    }

    public int IntervalCount
    {
      get
      {
        return this.m_intervalCount;
      }
      set
      {
        this.m_intervalCount = value > 0 ? value : 1;
      }
    }

    public TimeSpan IntervalDuration
    {
      get
      {
        return this.m_intervalTimeSpan;
      }
      set
      {
        this.m_intervalTimeSpan = value;
      }
    }

    public TimeSpan[] TimesOfTheDay
    {
      get
      {
        if (this.m_timesOfTheDay == null)
          return (TimeSpan[]) null;
        else
          return (TimeSpan[]) this.m_timesOfTheDay.Clone();
      }
      set
      {
        this.m_timesOfTheDay = (TimeSpan[]) value.Clone();
        Array.Sort<TimeSpan>(this.m_timesOfTheDay);
      }
    }

    public Schedule.DaysOfWeek DaysOfTheWeek
    {
      get
      {
        return this.m_daysOfTheWeek;
      }
      set
      {
        this.m_daysOfTheWeek = value;
      }
    }

    public Schedule.MonthsOfYear MonthsOfTheYear
    {
      get
      {
        return this.m_monthsOfTheYear;
      }
      set
      {
        this.m_monthsOfTheYear = value;
      }
    }

    public bool UseDayNumber
    {
      get
      {
        return this.m_useDayNumber;
      }
      set
      {
        this.m_useDayNumber = value;
      }
    }

    public int DayNumberInMonth
    {
      get
      {
        return this.m_dayNumberInMonth;
      }
      set
      {
        this.m_dayNumberInMonth = value;
      }
    }

    public Schedule.DayOfMonth DayOfTheMonth
    {
      get
      {
        return this.m_dayOfTheMonth;
      }
      set
      {
        this.m_dayOfTheMonth = value;
      }
    }

    public Schedule.DaysOfWeek DayOfTheWeek
    {
      get
      {
        return this.m_dayOfTheWeek;
      }
      set
      {
        this.m_dayOfTheWeek = value;
      }
    }

    static Schedule()
    {
    }

    public Schedule()
    {
    }

    public Schedule(XmlElement xSchedule)
    {
      XObject xobject1 = new XObject(xSchedule);
      this.TypeOfSchedule = (Schedule.ScheduleType) xobject1.GetAttributeAsEnum("Type", typeof (Schedule.ScheduleType));
      this.ScheduleStart = xobject1.GetAttributeAsDateTime("DateScheduleStart");
      this.ScheduleStop = xobject1.GetAttributeAsDateTime("DateScheduleStop");
      if (this.ScheduleStart == CommonValues.EmptyDateTime)
        this.ScheduleStart = DateTime.Today;
      XObject xobject2 = new XObject((XmlElement) xobject1.Element.SelectSingleNode(((object) this.TypeOfSchedule).ToString()));
      if (!xobject2.IsNotNull())
        throw new OnlineException(ExceptionType.System, "Schedule missing configured element with settings for schedule type.");
      this.TimeStart = xobject2.GetAttributeAsTimeSpan("TimeStart");
      if (this.TypeOfSchedule == Schedule.ScheduleType.OneShot)
        return;
      if (this.TypeOfSchedule == Schedule.ScheduleType.Interval)
        this.IntervalDuration = xobject2.GetAttributeAsTimeSpan("Duration");
      else if (this.TypeOfSchedule == Schedule.ScheduleType.Daily)
      {
        this.IntervalCount = xobject2.GetAttributeAsInt("Interval");
        XmlElement xmlElement = (XmlElement) xobject2.Element.SelectSingleNode("TimesOfTheDay");
        if (xmlElement != null)
        {
          XmlNodeList xmlNodeList = xmlElement.SelectNodes("Time");
          TimeSpan[] timeSpanArray = new TimeSpan[xmlNodeList.Count + 1];
          timeSpanArray[0] = this.m_timeStart;
          for (int index = 0; index < xmlNodeList.Count; ++index)
          {
            XObject xobject3 = new XObject((XmlElement) xmlNodeList[index]);
            timeSpanArray[index + 1] = xobject3.GetAttributeAsTimeSpan("TimeStart");
          }
          this.TimesOfTheDay = timeSpanArray;
        }
        else
        {
          this.m_timesOfTheDay = new TimeSpan[1];
          this.m_timesOfTheDay[0] = this.m_timeStart;
        }
      }
      else if (this.TypeOfSchedule == Schedule.ScheduleType.Weekly)
      {
        this.IntervalCount = xobject2.GetAttributeAsInt("Interval");
        if (xobject2.GetAttributeAsString("DaysOfTheWeek").Length == 0)
          this.m_daysOfTheWeek = Schedule.EveryDay;
        else
          this.m_daysOfTheWeek = (Schedule.DaysOfWeek) xobject2.GetAttributeAsEnum("DaysOfTheWeek", typeof (Schedule.DaysOfWeek));
      }
      else
      {
        if (this.TypeOfSchedule != Schedule.ScheduleType.Monthly)
          return;
        this.m_monthsOfTheYear = xobject2.GetAttributeAsString("MonthsOfTheYear").Length != 0 ? (Schedule.MonthsOfYear) xobject2.GetAttributeAsEnum("MonthsOfTheYear", typeof (Schedule.MonthsOfYear)) : Schedule.AllYear;
        XObject xobject3 = new XObject((XmlElement) xobject2.Element.SelectSingleNode("DayOfTheMonth"));
        if (!xobject3.IsNotNull())
          throw new OnlineException(ExceptionType.System, "Schedule missing configured element with settings for schedule type.");
        this.DayNumberInMonth = xobject3.GetAttributeAsInt("DayNumber");
        if (this.DayNumberInMonth > 0 && this.DayNumberInMonth <= 31)
          return;
        this.UseDayNumber = false;
        this.DayOfTheMonth = (Schedule.DayOfMonth) xobject3.GetAttributeAsEnum("OnThe", typeof (Schedule.DayOfMonth));
        this.DayOfTheWeek = (Schedule.DaysOfWeek) xobject3.GetAttributeAsEnum("DayOfTheWeek", typeof (Schedule.DaysOfWeek));
      }
    }

    private int DoCalculateDayNumberInMonth(int year, int month)
    {
      DateTime dateTime = new DateTime(year, month, 1);
      int num1 = DateTime.DaysInMonth(year, month);
      int num2 = 0;
      if (this.m_useDayNumber)
      {
        num2 = this.m_dayNumberInMonth > num1 ? num1 : this.m_dayNumberInMonth;
      }
      else
      {
        int num3 = 0;
        int num4 = 0;
        int num5 = 0;
        int num6 = 0;
        int num7 = 0;
        for (int index = 0; index < num1; ++index)
        {
          if ((this.m_dayOfTheWeek & (Schedule.DaysOfWeek) (1 << (int) (dateTime.AddDays((double) index).DayOfWeek & (DayOfWeek) 31))) != Schedule.DaysOfWeek.Unknown)
          {
            if (index < 7)
              num3 = index + 1;
            else if (index < 14)
              num4 = index + 1;
            else if (index < 21)
              num5 = index + 1;
            else if (index < 28)
              num7 = num6 = index + 1;
            else if (index < 31)
              num7 = index + 1;
          }
        }
        switch (this.m_dayOfTheMonth)
        {
          case Schedule.DayOfMonth.First:
            num2 = num3;
            break;
          case Schedule.DayOfMonth.Second:
            num2 = num4;
            break;
          case Schedule.DayOfMonth.Third:
            num2 = num5;
            break;
          case Schedule.DayOfMonth.Fourth:
            num2 = num6;
            break;
          case Schedule.DayOfMonth.Last:
            num2 = num7;
            break;
        }
      }
      return num2;
    }

    public bool CalculateOffsetToNextInterval(out TimeSpan offsetToNextInterval)
    {
      return this.CalculateOffsetToNextInterval(DateTime.Now, out offsetToNextInterval);
    }

    public bool CalculateOffsetToNextInterval(DateTime startingDateTime, out TimeSpan offsetToNextInterval)
    {
      offsetToNextInterval = CommonValues.EmptyTimeSpan;
      bool flag1 = false;
      if (startingDateTime >= this.m_dateScheduleStart + this.m_timeStart)
      {
        if (this.m_dateScheduleStop != CommonValues.EmptyDateTime)
        {
          if (startingDateTime < this.m_dateScheduleStop)
            flag1 = true;
          else
            offsetToNextInterval = this.m_dateScheduleStop - startingDateTime;
        }
        else
          flag1 = true;
      }
      else
        offsetToNextInterval = this.m_dateScheduleStart + this.m_timeStart - startingDateTime;
      if (flag1)
      {
        switch (this.m_typeOfSchedule)
        {
          case Schedule.ScheduleType.Interval:
            offsetToNextInterval = this.m_intervalTimeSpan;
            break;
          case Schedule.ScheduleType.OneShot:
            offsetToNextInterval = CommonValues.EmptyTimeSpan;
            break;
          case Schedule.ScheduleType.Daily:
            if (this.m_timesOfTheDay.Length == 0)
              throw new OnlineException(ExceptionType.System, "Daily Schedule has no times configured!");
            int days1 = (startingDateTime - this.m_dateScheduleStart).Days;
            int num1 = 0;
            TimeSpan timeSpan1 = CommonValues.EmptyTimeSpan;
            TimeSpan timeSpan2 = startingDateTime.TimeOfDay;
            while (days1 % this.m_intervalCount != 0)
            {
              ++days1;
              ++num1;
              timeSpan2 = this.m_timesOfTheDay[0];
            }
            if (timeSpan2 > this.m_timesOfTheDay[this.m_timesOfTheDay.Length - 1])
            {
              timeSpan2 = this.m_timesOfTheDay[0];
              int num2 = days1 + 1;
              ++num1;
              while (num2 % this.m_intervalCount != 0)
              {
                ++num2;
                ++num1;
              }
            }
            if (num1 > 0)
            {
              timeSpan1 = startingDateTime.AddDays(1.0).Date - startingDateTime + timeSpan2;
              --num1;
            }
            else
            {
              foreach (TimeSpan timeSpan3 in this.m_timesOfTheDay)
              {
                if (timeSpan3 > timeSpan2)
                {
                  timeSpan1 = timeSpan3 - timeSpan2;
                  break;
                }
              }
            }
            offsetToNextInterval = new TimeSpan(timeSpan1.Days + num1, timeSpan1.Hours, timeSpan1.Minutes, timeSpan1.Seconds, timeSpan1.Milliseconds + 1);
            break;
          case Schedule.ScheduleType.Weekly:
            DateTime dateTime1 = this.m_dateScheduleStart.AddDays((double) -(int) this.m_dateScheduleStart.DayOfWeek);
            int days2 = (startingDateTime - dateTime1).Days;
            int num3 = days2 / 7;
            int num4 = 0;
            TimeSpan timeSpan4 = CommonValues.EmptyTimeSpan;
            if (startingDateTime.TimeOfDay > this.m_timeStart)
            {
              ++days2;
              num3 = days2 / 7;
              ++num4;
            }
            for (bool flag2 = (this.m_daysOfTheWeek & (Schedule.DaysOfWeek) (1 << (int) (startingDateTime.AddDays((double) num4).DayOfWeek & (DayOfWeek) 31))) != Schedule.DaysOfWeek.Unknown; num3 % this.m_intervalCount != 0 || !flag2; flag2 = (this.m_daysOfTheWeek & (Schedule.DaysOfWeek) (1 << (int) (startingDateTime.AddDays((double) num4).DayOfWeek & (DayOfWeek) 31))) != Schedule.DaysOfWeek.Unknown)
            {
              ++days2;
              num3 = days2 / 7;
              ++num4;
            }
            TimeSpan timeSpan5;
            if (num4 > 0)
            {
              timeSpan5 = startingDateTime.AddDays(1.0).Date - startingDateTime + this.m_timeStart;
              --num4;
            }
            else
              timeSpan5 = this.m_timeStart - startingDateTime.TimeOfDay;
            offsetToNextInterval = new TimeSpan(timeSpan5.Days + num4, timeSpan5.Hours, timeSpan5.Minutes, timeSpan5.Seconds, timeSpan5.Milliseconds + 1);
            break;
          case Schedule.ScheduleType.Monthly:
            int months = 0;
            DateTime dateTime2 = new DateTime(startingDateTime.Year, startingDateTime.Month, 1);
            for (bool flag2 = (this.m_monthsOfTheYear & (Schedule.MonthsOfYear) (1 << dateTime2.AddMonths(months).Month - 1)) != Schedule.MonthsOfYear.Unknown; !flag2; flag2 = (this.m_monthsOfTheYear & (Schedule.MonthsOfYear) (1 << dateTime2.AddMonths(months).Month - 1)) != Schedule.MonthsOfYear.Unknown)
              ++months;
            int day1 = this.DoCalculateDayNumberInMonth(dateTime2.AddMonths(months).Year, dateTime2.AddMonths(months).Month);
            bool flag3 = false;
            if (months == 0)
            {
              if (startingDateTime.Day == day1)
              {
                if (startingDateTime.TimeOfDay <= this.m_timeStart)
                {
                  offsetToNextInterval = this.m_timeStart - startingDateTime.TimeOfDay + new TimeSpan(0, 0, 0, 0, 1);
                  flag3 = true;
                }
              }
              else if (startingDateTime.Day < day1)
              {
                offsetToNextInterval = new DateTime(startingDateTime.Year, startingDateTime.Month, day1) + this.m_timeStart - startingDateTime + new TimeSpan(0, 0, 0, 0, 1);
                flag3 = true;
              }
              if (!flag3)
                ++months;
            }
            if (!flag3)
            {
              for (bool flag2 = (this.m_monthsOfTheYear & (Schedule.MonthsOfYear) (1 << dateTime2.AddMonths(months).Month - 1)) != Schedule.MonthsOfYear.Unknown; !flag2; flag2 = (this.m_monthsOfTheYear & (Schedule.MonthsOfYear) (1 << dateTime2.AddMonths(months).Month - 1)) != Schedule.MonthsOfYear.Unknown)
                ++months;
              dateTime2 = dateTime2.AddMonths(months);
              int day2 = this.DoCalculateDayNumberInMonth(dateTime2.Year, dateTime2.Month);
              offsetToNextInterval = new DateTime(dateTime2.Year, dateTime2.Month, day2) + this.m_timeStart - startingDateTime + new TimeSpan(0, 0, 0, 0, 1);
              break;
            }
            else
              break;
          default:
            throw new OnlineException(ExceptionType.System, "Unknown Schedule Type!");
        }
      }
      if (flag1 && this.m_dateScheduleStop != CommonValues.EmptyDateTime && startingDateTime + offsetToNextInterval > this.m_dateScheduleStop)
      {
        offsetToNextInterval = CommonValues.EmptyTimeSpan;
        flag1 = false;
      }
      return flag1;
    }

    public enum ScheduleType
    {
      Interval,
      OneShot,
      Daily,
      Weekly,
      Monthly,
    }

    [System.Flags]
    public enum DaysOfWeek
    {
      Unknown = 0,
      Sunday = 1,
      Monday = 2,
      Tuesday = 4,
      Wednesday = 8,
      Thursday = 16,
      Friday = 32,
      Saturday = 64,
    }

    public enum DayOfMonth
    {
      Unknown,
      First,
      Second,
      Third,
      Fourth,
      Last,
    }

    [System.Flags]
    public enum MonthsOfYear
    {
      Unknown = 0,
      January = 1,
      February = 2,
      March = 4,
      April = 8,
      May = 16,
      June = 32,
      July = 64,
      August = 128,
      September = 256,
      October = 512,
      November = 1024,
      December = 2048,
    }
  }
}

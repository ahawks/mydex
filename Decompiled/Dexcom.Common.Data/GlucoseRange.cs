// Type: Dexcom.Common.Data.GlucoseRange
// Assembly: Dexcom.Common.Data, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 64B9BFC3-C891-4315-B216-574914861A80
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.Data.dll

using Dexcom.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;

namespace Dexcom.Common.Data
{
  public class GlucoseRange : ICloneable, IComparable
  {
    private Guid m_id = Guid.NewGuid();
    private double m_minValue = double.NaN;
    private double m_maxValue = double.NaN;
    private MinRangeOperator m_minOperator = MinRangeOperator.GreaterThanOrEqual;
    private MaxRangeOperator m_maxOperator = MaxRangeOperator.LessThanOrEqual;
    private string m_name = string.Empty;
    private int m_countMatches;

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

    public double MinValue
    {
      get
      {
        return this.m_minValue;
      }
      set
      {
        this.m_minValue = value;
      }
    }

    public double MinValueInclusive
    {
      get
      {
        double num = this.m_minValue;
        if (!double.IsNaN(this.m_minValue) && this.m_minOperator == MinRangeOperator.GreaterThan)
          ++num;
        return num;
      }
    }

    public double MaxValue
    {
      get
      {
        return this.m_maxValue;
      }
      set
      {
        this.m_maxValue = value;
      }
    }

    public double MaxValueInclusive
    {
      get
      {
        double num = this.m_maxValue;
        if (!double.IsNaN(this.m_maxValue) && this.m_maxOperator == MaxRangeOperator.LessThan)
          --num;
        return num;
      }
    }

    public MinRangeOperator MinOperator
    {
      get
      {
        return this.m_minOperator;
      }
      set
      {
        this.m_minOperator = value;
      }
    }

    public MaxRangeOperator MaxOperator
    {
      get
      {
        return this.m_maxOperator;
      }
      set
      {
        this.m_maxOperator = value;
      }
    }

    public string DisplayWithoutName
    {
      get
      {
        bool convertToMmol = false;
        try
        {
          if (ProgramContext.Current != null)
          {
            if (ProgramContext.Current.HasProperty("IsDisplayMMolUnits"))
              convertToMmol = ProgramContext.Current.Property<bool>("IsDisplayMMolUnits");
          }
        }
        catch
        {
        }
        string str1 = string.Empty;
        if (double.IsNaN(this.MinValue) && double.IsNaN(this.MaxValue))
          str1 = ProgramContext.TryResourceLookup("Common_Empty", "Empty", new object[0]);
        else if (!double.IsNaN(this.MinValue))
        {
          string str2 = CommonTools.FormatGlucoseValue(this.MinValue, false, convertToMmol, convertToMmol ? 2 : 0);
          str1 = this.MinOperator != MinRangeOperator.GreaterThanOrEqual ? str1 + "> " + str2 : str1 + ">= " + str2;
          if (!double.IsNaN(this.MaxValue))
          {
            string str3 = ProgramContext.TryResourceLookup("Common_and", "and", new object[0]);
            string str4 = CommonTools.FormatGlucoseValue(this.MaxValue, false, convertToMmol, convertToMmol ? 2 : 0);
            if (this.MaxOperator == MaxRangeOperator.LessThanOrEqual)
              str1 = str1 + " " + str3 + " <= " + str4;
            else
              str1 = str1 + " " + str3 + " < " + str4;
          }
        }
        else if (!double.IsNaN(this.MaxValue))
        {
          string str2 = CommonTools.FormatGlucoseValue(this.MaxValue, false, convertToMmol, convertToMmol ? 2 : 0);
          str1 = this.MaxOperator != MaxRangeOperator.LessThanOrEqual ? str1 + "< " + str2 : str1 + "<= " + str2;
        }
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

    public string ShortDisplayWithoutName
    {
      get
      {
        bool convertToMmol = false;
        try
        {
          if (ProgramContext.Current != null)
          {
            if (ProgramContext.Current.HasProperty("IsDisplayMMolUnits"))
              convertToMmol = ProgramContext.Current.Property<bool>("IsDisplayMMolUnits");
          }
        }
        catch
        {
        }
        string str1 = string.Empty;
        if (double.IsNaN(this.MinValue) && double.IsNaN(this.MaxValue))
          str1 = ProgramContext.TryResourceLookup("Common_Empty", "Empty", new object[0]);
        else if (!double.IsNaN(this.MinValue))
        {
          if (!double.IsNaN(this.MaxValue))
          {
            string str2 = this.MinOperator != MinRangeOperator.GreaterThanOrEqual ? str1 + CommonTools.FormatGlucoseValue(this.MinValue + 1.0, false, convertToMmol, convertToMmol ? 1 : 0) : str1 + CommonTools.FormatGlucoseValue(this.MinValue, false, convertToMmol, convertToMmol ? 1 : 0);
            str1 = this.MaxOperator != MaxRangeOperator.LessThanOrEqual ? str2 + " - " + CommonTools.FormatGlucoseValue(this.MaxValue - 1.0, false, convertToMmol, convertToMmol ? 1 : 0) : str2 + " - " + CommonTools.FormatGlucoseValue(this.MaxValue, false, convertToMmol, convertToMmol ? 1 : 0);
          }
          else
            str1 = this.MinOperator != MinRangeOperator.GreaterThanOrEqual ? "> " + CommonTools.FormatGlucoseValue(this.MinValue, false, convertToMmol, convertToMmol ? 1 : 0) : "> " + CommonTools.FormatGlucoseValue(this.MinValue - 1.0, false, convertToMmol, convertToMmol ? 1 : 0);
        }
        else if (!double.IsNaN(this.MaxValue))
          str1 = this.MaxOperator != MaxRangeOperator.LessThanOrEqual ? str1 + "< " + CommonTools.FormatGlucoseValue(this.MaxValue, false, convertToMmol, convertToMmol ? 1 : 0) : str1 + "< " + CommonTools.FormatGlucoseValue(this.MaxValue + 1.0, false, convertToMmol, convertToMmol ? 1 : 0);
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

    public GlucoseRange()
    {
    }

    public GlucoseRange(string name)
    {
      this.Name = name;
    }

    public GlucoseRange(string name, MinRangeOperator minOp, double minValue, MaxRangeOperator maxOp, double maxValue)
    {
      this.Name = name;
      this.MinOperator = minOp;
      this.MinValue = minValue;
      this.MaxOperator = maxOp;
      this.MaxValue = maxValue;
    }

    public GlucoseRange(XGlucoseRange xGlucoseRange)
    {
      this.Id = xGlucoseRange.Id;
      this.Name = xGlucoseRange.Name;
      this.MinOperator = xGlucoseRange.MinOperator;
      this.MinValue = xGlucoseRange.MinValue;
      this.MaxOperator = xGlucoseRange.MaxOperator;
      this.MaxValue = xGlucoseRange.MaxValue;
    }

    public override string ToString()
    {
      return this.Display;
    }

    public bool IsMatch(double glucoseValue)
    {
      bool flag = true;
      if (double.IsNaN(this.MinValue) && double.IsNaN(this.MaxValue))
        flag = false;
      else if (!double.IsNaN(this.MinValue))
        flag = this.MinOperator != MinRangeOperator.GreaterThanOrEqual ? flag && glucoseValue > this.MinValue : flag && glucoseValue >= this.MinValue;
      if (flag && !double.IsNaN(this.MaxValue))
        flag = this.MaxOperator != MaxRangeOperator.LessThanOrEqual ? flag && glucoseValue < this.MaxValue : flag && glucoseValue <= this.MaxValue;
      return flag;
    }

    public XmlElement ToXml()
    {
      return this.ToXml(new XmlDocument());
    }

    public XmlElement ToXml(XmlDocument ownerDocument)
    {
      return new XGlucoseRange(ownerDocument)
      {
        Id = this.Id,
        Name = this.Name,
        MinOperator = this.MinOperator,
        MinValue = this.MinValue,
        MaxOperator = this.MaxOperator,
        MaxValue = this.MaxValue
      }.Element;
    }

    public static List<GlucoseRange> GenerateDefaultGlucoseRangeList()
    {
      return new List<GlucoseRange>()
      {
        new GlucoseRange(ProgramContext.TryResourceLookup("Common_Hypoglycemia", "Hypoglycemia", new object[0]), MinRangeOperator.GreaterThanOrEqual, double.NaN, MaxRangeOperator.LessThan, 55.0)
        {
          Id = new Guid("{00000001-0000-0000-0000-000000000000}")
        },
        new GlucoseRange(ProgramContext.TryResourceLookup("Common_Low", "Low", new object[0]), MinRangeOperator.GreaterThanOrEqual, 55.0, MaxRangeOperator.LessThan, 80.0)
        {
          Id = new Guid("{00000002-0000-0000-0000-000000000000}")
        },
        new GlucoseRange(ProgramContext.TryResourceLookup("Common_Target", "Target", new object[0]), MinRangeOperator.GreaterThanOrEqual, 80.0, MaxRangeOperator.LessThanOrEqual, 130.0)
        {
          Id = new Guid("{00000003-0000-0000-0000-000000000000}")
        },
        new GlucoseRange(ProgramContext.TryResourceLookup("Common_High", "High", new object[0]), MinRangeOperator.GreaterThan, 130.0, MaxRangeOperator.LessThanOrEqual, 240.0)
        {
          Id = new Guid("{00000004-0000-0000-0000-000000000000}")
        },
        new GlucoseRange(ProgramContext.TryResourceLookup("Common_Hyperglycemia", "Hyperglycemia", new object[0]), MinRangeOperator.GreaterThan, 240.0, MaxRangeOperator.LessThanOrEqual, double.NaN)
        {
          Id = new Guid("{00000005-0000-0000-0000-000000000000}")
        }
      };
    }

    public static List<GlucoseRange> GenerateDefaultHistogramGlucoseRangeList()
    {
      return new List<GlucoseRange>()
      {
        new GlucoseRange(string.Empty, MinRangeOperator.GreaterThanOrEqual, 39.0, MaxRangeOperator.LessThan, 45.0),
        new GlucoseRange(string.Empty, MinRangeOperator.GreaterThanOrEqual, 45.0, MaxRangeOperator.LessThan, 90.0),
        new GlucoseRange(string.Empty, MinRangeOperator.GreaterThanOrEqual, 90.0, MaxRangeOperator.LessThan, 135.0),
        new GlucoseRange(string.Empty, MinRangeOperator.GreaterThanOrEqual, 135.0, MaxRangeOperator.LessThan, 180.0),
        new GlucoseRange(string.Empty, MinRangeOperator.GreaterThanOrEqual, 180.0, MaxRangeOperator.LessThan, 225.0),
        new GlucoseRange(string.Empty, MinRangeOperator.GreaterThanOrEqual, 225.0, MaxRangeOperator.LessThan, 270.0),
        new GlucoseRange(string.Empty, MinRangeOperator.GreaterThanOrEqual, 270.0, MaxRangeOperator.LessThan, 315.0),
        new GlucoseRange(string.Empty, MinRangeOperator.GreaterThanOrEqual, 315.0, MaxRangeOperator.LessThan, 360.0),
        new GlucoseRange(string.Empty, MinRangeOperator.GreaterThanOrEqual, 360.0, MaxRangeOperator.LessThanOrEqual, 401.0)
      };
    }

    public static List<GlucoseRange> GenerateFineHistogramGlucoseRangeList()
    {
      return new List<GlucoseRange>()
      {
        new GlucoseRange(string.Empty, MinRangeOperator.GreaterThanOrEqual, 39.0, MaxRangeOperator.LessThan, 56.0),
        new GlucoseRange(string.Empty, MinRangeOperator.GreaterThanOrEqual, 56.0, MaxRangeOperator.LessThan, 71.0),
        new GlucoseRange(string.Empty, MinRangeOperator.GreaterThanOrEqual, 71.0, MaxRangeOperator.LessThan, 101.0),
        new GlucoseRange(string.Empty, MinRangeOperator.GreaterThanOrEqual, 101.0, MaxRangeOperator.LessThan, 131.0),
        new GlucoseRange(string.Empty, MinRangeOperator.GreaterThanOrEqual, 131.0, MaxRangeOperator.LessThan, 161.0),
        new GlucoseRange(string.Empty, MinRangeOperator.GreaterThanOrEqual, 161.0, MaxRangeOperator.LessThan, 191.0),
        new GlucoseRange(string.Empty, MinRangeOperator.GreaterThanOrEqual, 191.0, MaxRangeOperator.LessThan, 221.0),
        new GlucoseRange(string.Empty, MinRangeOperator.GreaterThanOrEqual, 221.0, MaxRangeOperator.LessThan, 251.0),
        new GlucoseRange(string.Empty, MinRangeOperator.GreaterThanOrEqual, 251.0, MaxRangeOperator.LessThan, 281.0),
        new GlucoseRange(string.Empty, MinRangeOperator.GreaterThanOrEqual, 281.0, MaxRangeOperator.LessThan, 311.0),
        new GlucoseRange(string.Empty, MinRangeOperator.GreaterThanOrEqual, 311.0, MaxRangeOperator.LessThan, 341.0),
        new GlucoseRange(string.Empty, MinRangeOperator.GreaterThanOrEqual, 341.0, MaxRangeOperator.LessThan, 371.0),
        new GlucoseRange(string.Empty, MinRangeOperator.GreaterThanOrEqual, 371.0, MaxRangeOperator.LessThanOrEqual, 401.0)
      };
    }

    public static List<GlucoseRange> GenerateGlucoseTargetLowHighListFromTarget(double lowValue, double highValue)
    {
      Trace.Assert(lowValue <= highValue);
      return GlucoseRange.GenerateGlucoseTargetLowHighListFromTarget(new GlucoseRange(ProgramContext.TryResourceLookup("Common_Target", "Target", new object[0]), MinRangeOperator.GreaterThanOrEqual, lowValue, MaxRangeOperator.LessThanOrEqual, highValue));
    }

    public static List<GlucoseRange> GenerateGlucoseTargetLowHighListFromTarget(GlucoseRange target)
    {
      List<GlucoseRange> list = new List<GlucoseRange>();
      double minValue = target.MinValue;
      double maxValue = target.MaxValue;
      bool flag1 = false;
      bool flag2 = false;
      if (minValue < 39.0 || minValue == 39.0 && target.MinOperator == MinRangeOperator.GreaterThanOrEqual)
        flag2 = true;
      if (double.IsNaN(maxValue) || maxValue > 401.0 || maxValue == 401.0 && target.MaxOperator == MaxRangeOperator.LessThanOrEqual)
        flag1 = true;
      if (flag2 && flag1)
      {
        list.Add(new GlucoseRange(ProgramContext.TryResourceLookup("Common_Target", "Target", new object[0]), MinRangeOperator.GreaterThanOrEqual, 39.0, MaxRangeOperator.LessThanOrEqual, 401.0));
        list.Add((GlucoseRange) null);
        list.Add((GlucoseRange) null);
      }
      else if (flag2)
      {
        list.Add(new GlucoseRange(ProgramContext.TryResourceLookup("Common_Target", "Target", new object[0]), MinRangeOperator.GreaterThanOrEqual, 39.0, target.MaxOperator, maxValue));
        list.Add((GlucoseRange) null);
        if (target.MaxOperator == MaxRangeOperator.LessThan)
          list.Add(new GlucoseRange(ProgramContext.TryResourceLookup("Common_High", "High", new object[0]), MinRangeOperator.GreaterThanOrEqual, maxValue, MaxRangeOperator.LessThanOrEqual, 401.0));
        else
          list.Add(new GlucoseRange(ProgramContext.TryResourceLookup("Common_High", "High", new object[0]), MinRangeOperator.GreaterThan, maxValue, MaxRangeOperator.LessThanOrEqual, 401.0));
      }
      else if (flag1)
      {
        list.Add(new GlucoseRange(ProgramContext.TryResourceLookup("Common_Target", "Target", new object[0]), target.MinOperator, minValue, MaxRangeOperator.LessThanOrEqual, 401.0));
        if (target.MinOperator == MinRangeOperator.GreaterThan)
          list.Add(new GlucoseRange(ProgramContext.TryResourceLookup("Common_Low", "Low", new object[0]), MinRangeOperator.GreaterThanOrEqual, 39.0, MaxRangeOperator.LessThanOrEqual, minValue));
        else
          list.Add(new GlucoseRange(ProgramContext.TryResourceLookup("Common_Low", "Low", new object[0]), MinRangeOperator.GreaterThanOrEqual, 39.0, MaxRangeOperator.LessThan, minValue));
        list.Add((GlucoseRange) null);
      }
      else
      {
        list.Add(new GlucoseRange(ProgramContext.TryResourceLookup("Common_Target", "Target", new object[0]), target.MinOperator, minValue, target.MaxOperator, maxValue));
        if (target.MinOperator == MinRangeOperator.GreaterThan)
          list.Add(new GlucoseRange(ProgramContext.TryResourceLookup("Common_Low", "Low", new object[0]), MinRangeOperator.GreaterThanOrEqual, 39.0, MaxRangeOperator.LessThanOrEqual, minValue));
        else
          list.Add(new GlucoseRange(ProgramContext.TryResourceLookup("Common_Low", "Low", new object[0]), MinRangeOperator.GreaterThanOrEqual, 39.0, MaxRangeOperator.LessThan, minValue));
        if (target.MaxOperator == MaxRangeOperator.LessThan)
          list.Add(new GlucoseRange(ProgramContext.TryResourceLookup("Common_High", "High", new object[0]), MinRangeOperator.GreaterThanOrEqual, maxValue, MaxRangeOperator.LessThanOrEqual, 401.0));
        else
          list.Add(new GlucoseRange(ProgramContext.TryResourceLookup("Common_High", "High", new object[0]), MinRangeOperator.GreaterThan, maxValue, MaxRangeOperator.LessThanOrEqual, 401.0));
      }
      return list;
    }

    public object Clone()
    {
      return this.MemberwiseClone();
    }

    public int CompareTo(object obj)
    {
      GlucoseRange glucoseRange = obj as GlucoseRange;
      if (glucoseRange == null)
        return 1;
      if (glucoseRange == this)
        return 0;
      if (this.MinValue < glucoseRange.MinValue)
        return -1;
      if (this.MinValue > glucoseRange.MinValue)
        return 1;
      if (this.MaxValue < glucoseRange.MaxValue)
        return -1;
      if (this.MaxValue > glucoseRange.MaxValue)
        return 1;
      if (this.MaxOperator == glucoseRange.MaxOperator)
        return 0;
      return this.MaxOperator == MaxRangeOperator.LessThan ? -1 : 1;
    }
  }
}

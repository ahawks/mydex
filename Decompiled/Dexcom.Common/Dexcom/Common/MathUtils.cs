// Type: Dexcom.Common.MathUtils
// Assembly: Dexcom.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B95A59FD-1598-4EA8-B1F5-D812FE5A6802
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Dexcom.Common
{
  public static class MathUtils
  {
    public static double Summation(List<double> values)
    {
      double sum = 0.0;
      if (values.Count > 0)
        values.ForEach((Action<double>) (val => sum += val));
      return sum;
    }

    public static double Range(List<double> values)
    {
      double min = 0.0;
      double max = 0.0;
      if (values.Count > 1)
      {
        min = values[0];
        max = values[0];
        values.ForEach((Action<double>) (val =>
        {
          min = Math.Min(min, val);
          max = Math.Max(max, val);
        }));
      }
      return max - min;
    }

    public static double Average(List<double> values)
    {
      double num1 = MathUtils.Summation(values);
      double num2 = 0.0;
      if (values.Count > 0)
        num2 = num1 / (double) values.Count;
      return num2;
    }

    public static List<double> Multiply(List<double> aValues, double multiplier)
    {
      List<double> list = new List<double>(aValues.Count);
      foreach (double num in aValues)
        list.Add(num * multiplier);
      return list;
    }

    public static List<double> Multiply(List<double> aValues, List<double> bValues)
    {
      Trace.Assert(aValues.Count == bValues.Count);
      List<double> list = new List<double>(aValues.Count);
      for (int index = 0; index < aValues.Count; ++index)
        list.Add(aValues[index] * bValues[index]);
      return list;
    }

    public static List<double> Add(List<double> aValues, double addend)
    {
      List<double> list = new List<double>(aValues.Count);
      foreach (double num in aValues)
        list.Add(num + addend);
      return list;
    }

    public static List<double> Add(List<double> aValues, List<double> bValues)
    {
      Trace.Assert(aValues.Count == bValues.Count);
      List<double> list = new List<double>(aValues.Count);
      for (int index = 0; index < aValues.Count; ++index)
        list.Add(aValues[index] + bValues[index]);
      return list;
    }

    public static List<double> Divide(List<double> aValues, double divisor)
    {
      Trace.Assert(divisor != 0.0);
      List<double> list = new List<double>(aValues.Count);
      foreach (double num in aValues)
        list.Add(num / divisor);
      return list;
    }

    public static List<double> Divide(List<double> aValues, List<double> bValues)
    {
      Trace.Assert(aValues.Count == bValues.Count);
      List<double> list = new List<double>(aValues.Count);
      for (int index = 0; index < aValues.Count; ++index)
        list.Add(aValues[index] / bValues[index]);
      return list;
    }

    public static List<double> Subtract(List<double> aValues, double subtrahend)
    {
      List<double> list = new List<double>(aValues.Count);
      foreach (double num in aValues)
        list.Add(num - subtrahend);
      return list;
    }

    public static List<double> Subtract(List<double> aValues, List<double> bValues)
    {
      Trace.Assert(aValues.Count == bValues.Count);
      List<double> list = new List<double>(aValues.Count);
      for (int index = 0; index < aValues.Count; ++index)
        list.Add(aValues[index] - bValues[index]);
      return list;
    }

    public static List<double> Deviation(List<double> values)
    {
      double subtrahend = MathUtils.Average(values);
      return MathUtils.Subtract(values, subtrahend);
    }

    public static List<double> DeviationSquared(List<double> values)
    {
      double avg = MathUtils.Average(values);
      List<double> deviation_squared = new List<double>(values.Count);
      values.ForEach((Action<double>) (val =>
      {
        double local_0 = val - avg;
        deviation_squared.Add(local_0 * local_0);
      }));
      return deviation_squared;
    }

    public static double StandardDeviation(List<double> values)
    {
      double num = 0.0;
      if (values.Count > 1)
        num = Math.Sqrt(MathUtils.Summation(MathUtils.DeviationSquared(values)) / (double) (values.Count - 1));
      return num;
    }

    public static double Slope(List<double> yValues, List<double> xValues)
    {
      List<double> list = MathUtils.Deviation(xValues);
      List<double> bValues = MathUtils.Deviation(yValues);
      return MathUtils.Summation(MathUtils.Multiply(list, bValues)) / MathUtils.Summation(MathUtils.Multiply(list, list));
    }

    public static double Slope(List<double> yValues, List<double> xValues, out double intercept)
    {
      List<double> list = MathUtils.Deviation(xValues);
      List<double> bValues = MathUtils.Deviation(yValues);
      double num1 = MathUtils.Summation(MathUtils.Multiply(list, bValues)) / MathUtils.Summation(MathUtils.Multiply(list, list));
      double num2 = MathUtils.Average(xValues);
      double num3 = MathUtils.Average(yValues);
      intercept = num3 - num1 * num2;
      return num1;
    }

    public static double Intercept(List<double> yValues, List<double> xValues)
    {
      List<double> list = MathUtils.Deviation(xValues);
      List<double> bValues = MathUtils.Deviation(yValues);
      double num1 = MathUtils.Summation(MathUtils.Multiply(list, bValues)) / MathUtils.Summation(MathUtils.Multiply(list, list));
      double num2 = MathUtils.Average(xValues);
      return MathUtils.Average(yValues) - num1 * num2;
    }

    public static double RValue(List<double> yValues, List<double> xValues)
    {
      List<double> list1 = MathUtils.Deviation(xValues);
      List<double> list2 = MathUtils.Deviation(yValues);
      return MathUtils.Summation(MathUtils.Multiply(list1, list2)) / Math.Sqrt(MathUtils.Summation(MathUtils.Multiply(list1, list1)) * MathUtils.Summation(MathUtils.Multiply(list2, list2)));
    }

    public static List<double> Quartiles(List<double> values)
    {
      List<double> list = new List<double>((IEnumerable<double>) new double[5]);
      List<int> K = new List<int>((IEnumerable<int>) new int[5]
      {
        0,
        25,
        50,
        75,
        100
      });
      if (values.Count > 0)
        list = MathUtils.Quantile(values, K, 100);
      return list;
    }

    public static double Quantile(List<double> values, int k, int q)
    {
      double num1 = 0.0;
      if (values.Count == 1)
        num1 = values[0];
      else if (values.Count > 1)
      {
        List<double> list = new List<double>((IEnumerable<double>) values);
        list.Sort();
        int count = list.Count;
        double num2 = (double) k / (double) q;
        double d = (double) (count - 1) * num2;
        int index = (int) Math.Floor(d);
        double num3 = d - (double) index;
        if (index >= count - 1)
        {
          num1 = list[count - 1];
        }
        else
        {
          double num4 = list[index];
          double num5 = list[index + 1];
          num1 = num4 + num3 * (num5 - num4);
        }
      }
      return num1;
    }

    public static List<double> Quantile(List<double> values, List<int> K, int q)
    {
      List<double> list1 = new List<double>();
      if (values.Count == 1)
      {
        foreach (int num in K)
          list1.Add(values[0]);
      }
      else if (values.Count > 1)
      {
        List<double> list2 = new List<double>((IEnumerable<double>) values);
        list2.Sort();
        int count = list2.Count;
        foreach (double num1 in K)
        {
          double num2 = num1 / (double) q;
          double d = (double) (count - 1) * num2;
          int index = (int) Math.Floor(d);
          double num3 = d - (double) index;
          if (index >= count - 1)
          {
            list1.Add(list2[count - 1]);
          }
          else
          {
            double num4 = list2[index];
            double num5 = list2[index + 1];
            list1.Add(num4 + num3 * (num5 - num4));
          }
        }
      }
      return list1;
    }

    public static double Percentile(List<double> values, int percentile)
    {
      return MathUtils.Quantile(values, percentile, 100);
    }

    public static List<double> PercentilesBy5(List<double> values)
    {
      List<int> K = new List<int>((IEnumerable<int>) new int[21]
      {
        0,
        5,
        10,
        15,
        20,
        25,
        30,
        35,
        40,
        45,
        50,
        55,
        60,
        65,
        70,
        75,
        80,
        85,
        90,
        95,
        100
      });
      List<double> list = new List<double>((IEnumerable<double>) new double[21]);
      if (values.Count > 0)
        list = MathUtils.Quantile(values, K, 100);
      return list;
    }

    public static double Variance(List<double> values)
    {
      double num = 0.0;
      if (values.Count > 1)
        num = MathUtils.Summation(MathUtils.DeviationSquared(values)) / (double) (values.Count - 1);
      return num;
    }
  }
}

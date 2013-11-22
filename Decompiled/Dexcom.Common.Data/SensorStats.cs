// Type: Dexcom.Common.Data.SensorStats
// Assembly: Dexcom.Common.Data, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 64B9BFC3-C891-4315-B216-574914861A80
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.Data.dll

using Dexcom.Common;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Dexcom.Common.Data
{
  public class SensorStats
  {
    private List<double> m_glucoseValues = new List<double>();
    private List<double> m_weightedValues = new List<double>();
    private DateTime m_firstValueDateTime = CommonValues.EmptyDateTime;
    private DateTime m_lastValueDateTime = CommonValues.EmptyDateTime;
    private int m_count;
    private int m_totalCount;
    private double m_firstValue;
    private double m_lastValue;
    private double m_minValue;
    private double m_maxValue;
    private double m_mean;
    private double m_median;
    private double m_quartile25;
    private double m_quartile75;
    private double m_percentile5;
    private double m_percentile10;
    private double m_percentile90;
    private double m_percentile95;
    private double m_standardDeviation;
    private double m_interQuartileRange;
    private double m_coefficientOfVariation;
    private double m_standardErrorOfMean;
    private double m_estimatedStandardDeviation;
    private double m_weightedMean;
    private double m_weightedSum;

    public int Count
    {
      get
      {
        return this.m_count;
      }
    }

    public int WeightedCount
    {
      get
      {
        return this.m_weightedValues.Count;
      }
    }

    public int RunningCount
    {
      get
      {
        return this.m_glucoseValues.Count;
      }
    }

    public int TotalCount
    {
      get
      {
        return this.m_totalCount;
      }
      set
      {
        this.m_totalCount = value;
      }
    }

    public double FirstValue
    {
      get
      {
        return this.m_firstValue;
      }
      set
      {
        this.m_firstValue = value;
      }
    }

    public double LastValue
    {
      get
      {
        return this.m_lastValue;
      }
      set
      {
        this.m_lastValue = value;
      }
    }

    public DateTime FirstValueDateTime
    {
      get
      {
        return this.m_firstValueDateTime;
      }
      set
      {
        this.m_firstValueDateTime = value;
      }
    }

    public DateTime LastValueDateTime
    {
      get
      {
        return this.m_lastValueDateTime;
      }
      set
      {
        this.m_lastValueDateTime = value;
      }
    }

    public double WeightedMean
    {
      get
      {
        return this.m_weightedMean;
      }
      set
      {
        this.m_weightedMean = value;
      }
    }

    public double WeightedSum
    {
      get
      {
        return this.m_weightedSum;
      }
      set
      {
        this.m_weightedSum = value;
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

    public double Mean
    {
      get
      {
        return this.m_mean;
      }
      set
      {
        this.m_mean = value;
      }
    }

    public double Median
    {
      get
      {
        return this.m_median;
      }
      set
      {
        this.m_median = value;
      }
    }

    public double Quartile25
    {
      get
      {
        return this.m_quartile25;
      }
      set
      {
        this.m_quartile25 = value;
      }
    }

    public double Quartile75
    {
      get
      {
        return this.m_quartile75;
      }
      set
      {
        this.m_quartile75 = value;
      }
    }

    public double Percentile5
    {
      get
      {
        return this.m_percentile5;
      }
      set
      {
        this.m_percentile5 = value;
      }
    }

    public double Percentile10
    {
      get
      {
        return this.m_percentile10;
      }
      set
      {
        this.m_percentile10 = value;
      }
    }

    public double Percentile90
    {
      get
      {
        return this.m_percentile90;
      }
      set
      {
        this.m_percentile90 = value;
      }
    }

    public double Percentile95
    {
      get
      {
        return this.m_percentile95;
      }
      set
      {
        this.m_percentile95 = value;
      }
    }

    public double StandardDeviation
    {
      get
      {
        return this.m_standardDeviation;
      }
      set
      {
        this.m_standardDeviation = value;
      }
    }

    public double InterQuartileRange
    {
      get
      {
        return this.m_interQuartileRange;
      }
      set
      {
        this.m_interQuartileRange = value;
      }
    }

    public double CoefficientOfVariation
    {
      get
      {
        return this.m_coefficientOfVariation;
      }
      set
      {
        this.m_coefficientOfVariation = value;
      }
    }

    public double StandardErrorOfMean
    {
      get
      {
        return this.m_standardErrorOfMean;
      }
      set
      {
        this.m_standardErrorOfMean = value;
      }
    }

    public double EstimatedStandardDeviation
    {
      get
      {
        return this.m_estimatedStandardDeviation;
      }
      set
      {
        this.m_estimatedStandardDeviation = value;
      }
    }

    public double PercentageOfTotal
    {
      get
      {
        if (this.m_totalCount <= 0)
          return 0.0;
        else
          return (double) this.m_count / (double) this.m_totalCount;
      }
    }

    public void Add(DateTime time, double glucoseValue)
    {
      this.Add(time, glucoseValue, new double?());
    }

    public void Add(DateTime time, double glucoseValue, double? weightedValue)
    {
      this.m_glucoseValues.Add(glucoseValue);
      if (weightedValue.HasValue)
        this.m_weightedValues.Add(weightedValue.Value);
      if (this.m_glucoseValues.Count == 1)
      {
        this.m_firstValueDateTime = time;
        this.m_firstValue = glucoseValue;
      }
      this.m_lastValueDateTime = time;
      this.m_lastValue = glucoseValue;
    }

    private void DoResetStats()
    {
      this.m_minValue = 0.0;
      this.m_maxValue = 0.0;
      this.m_mean = 0.0;
      this.m_median = 0.0;
      this.m_quartile25 = 0.0;
      this.m_quartile75 = 0.0;
      this.m_percentile5 = 0.0;
      this.m_percentile10 = 0.0;
      this.m_percentile90 = 0.0;
      this.m_percentile95 = 0.0;
      this.m_standardDeviation = 0.0;
      this.m_interQuartileRange = 0.0;
      this.m_coefficientOfVariation = 0.0;
      this.m_standardErrorOfMean = 0.0;
      this.m_estimatedStandardDeviation = 0.0;
      this.m_weightedMean = 0.0;
      this.m_weightedSum = 0.0;
    }

    public void Calculate()
    {
      this.DoResetStats();
      this.m_count = this.m_glucoseValues.Count;
      if (this.m_count > 0)
      {
        bool flag = false;
        try
        {
          if (ProgramContext.Current != null)
          {
            if (ProgramContext.Current.HasProperty("IsDisplayMMolUnits"))
              flag = ProgramContext.Current.Property<bool>("IsDisplayMMolUnits");
          }
        }
        catch
        {
        }
        if (flag)
          this.m_glucoseValues = MathUtils.Divide(this.m_glucoseValues, 18.02);
        List<double> list = MathUtils.PercentilesBy5(this.m_glucoseValues);
        this.m_minValue = list[0];
        this.m_percentile5 = list[1];
        this.m_percentile10 = list[2];
        this.m_quartile25 = list[5];
        this.m_median = list[10];
        this.m_quartile75 = list[15];
        this.m_percentile90 = list[18];
        this.m_percentile95 = list[19];
        this.m_maxValue = list[20];
        this.m_mean = MathUtils.Average(this.m_glucoseValues);
        this.m_standardDeviation = MathUtils.StandardDeviation(this.m_glucoseValues);
        this.m_interQuartileRange = this.m_quartile75 - this.m_quartile25;
        this.m_coefficientOfVariation = this.m_mean == 0.0 ? 0.0 : this.m_standardDeviation / this.m_mean;
        this.m_standardErrorOfMean = this.m_standardDeviation / Math.Sqrt((double) this.m_count);
        this.m_estimatedStandardDeviation = this.m_interQuartileRange / 1.34898;
      }
      if (this.WeightedCount <= 0)
        return;
      this.m_weightedMean = MathUtils.Average(this.m_weightedValues);
      this.m_weightedSum = MathUtils.Summation(this.m_weightedValues);
    }

    public XmlElement ToXml()
    {
      return this.ToXml(new XmlDocument());
    }

    public XmlElement ToXml(XmlDocument ownerDocument)
    {
      XObject xobject = new XObject("SensorStats", ownerDocument);
      xobject.SetAttribute("Count", this.Count);
      xobject.SetAttribute("FirstValue", this.FirstValue);
      xobject.SetAttribute("FirstValueDateTime", this.FirstValueDateTime);
      xobject.SetAttribute("LastValue", this.LastValue);
      xobject.SetAttribute("LastValueDateTime", this.LastValueDateTime);
      xobject.SetAttribute("MinValue", this.MinValue);
      xobject.SetAttribute("Percentile5", this.Percentile5);
      xobject.SetAttribute("Percentile10", this.Percentile10);
      xobject.SetAttribute("Quartile25", this.Quartile25);
      xobject.SetAttribute("Mean", this.Mean);
      xobject.SetAttribute("Median", this.Median);
      xobject.SetAttribute("Quartile75", this.Quartile75);
      xobject.SetAttribute("Percentile90", this.Percentile90);
      xobject.SetAttribute("Percentile95", this.Percentile95);
      xobject.SetAttribute("MaxValue", this.MaxValue);
      xobject.SetAttribute("StandardDeviation", this.StandardDeviation);
      xobject.SetAttribute("PercentageOfTotal", this.PercentageOfTotal);
      xobject.SetAttribute("InterQuartileRange", this.InterQuartileRange);
      xobject.SetAttribute("CoefficientOfVariation", this.CoefficientOfVariation);
      xobject.SetAttribute("StandardErrorOfMean", this.StandardErrorOfMean);
      xobject.SetAttribute("EstimatedStandardDeviation", this.EstimatedStandardDeviation);
      xobject.SetAttribute("WeightedCount", this.WeightedCount);
      xobject.SetAttribute("WeightedSum", this.WeightedSum);
      xobject.SetAttribute("WeightedMean", this.WeightedMean);
      XObject xChildObject1 = new XObject("GlucoseValues", ownerDocument);
      xobject.AppendChild(xChildObject1);
      foreach (double doubleValue in this.m_glucoseValues)
      {
        XObject xChildObject2 = new XObject("EGV", ownerDocument);
        xChildObject2.SetAttribute("V", doubleValue);
        xChildObject1.AppendChild(xChildObject2);
      }
      XObject xChildObject3 = new XObject("WeightedValues", ownerDocument);
      xobject.AppendChild(xChildObject3);
      foreach (double doubleValue in this.m_weightedValues)
      {
        XObject xChildObject2 = new XObject("WV", ownerDocument);
        xChildObject2.SetAttribute("V", doubleValue);
        xChildObject3.AppendChild(xChildObject2);
      }
      return xobject.Element;
    }

    public override string ToString()
    {
      return this.ToXml().OuterXml;
    }
  }
}

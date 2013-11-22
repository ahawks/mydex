// Type: Dexcom.R2Receiver.MatchedPairStat
// Assembly: Dexcom.R2ReceiverApi, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: CFBF21F8-8DCC-4821-9279-3746E4DC5499
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.R2ReceiverApi.dll

using Dexcom.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;

namespace Dexcom.R2Receiver
{
  public class MatchedPairStat
  {
    private List<MatchedPair> m_matchedPairList = new List<MatchedPair>();
    private int m_totalCount;
    private int m_totalCountA;
    private int m_totalCountB;
    private int m_totalCountC;
    private int m_totalCountD;
    private int m_totalCountE;
    private int m_totalCount2020;
    private int m_totalCount3030;
    private int m_totalCount4040;
    private double m_mard;
    private double m_stdDevArd;
    private double m_medianArd;
    private double m_minArd;
    private double m_maxArd;
    private double m_meanRD;
    private double m_stdDevRD;
    private double m_medianRD;
    private double m_minRD;
    private double m_maxRD;

    public string Name { get; set; }

    public int MinSMBG { get; set; }

    public int MaxSMBG { get; set; }

    public int MinEGV { get; set; }

    public int MaxEGV { get; set; }

    public int TotalCount
    {
      get
      {
        return this.m_totalCount;
      }
    }

    public int TotalCountA
    {
      get
      {
        return this.m_totalCountA;
      }
    }

    public int TotalCountB
    {
      get
      {
        return this.m_totalCountB;
      }
    }

    public int TotalCountC
    {
      get
      {
        return this.m_totalCountC;
      }
    }

    public int TotalCountD
    {
      get
      {
        return this.m_totalCountD;
      }
    }

    public int TotalCountE
    {
      get
      {
        return this.m_totalCountE;
      }
    }

    public int TotalCount2020
    {
      get
      {
        return this.m_totalCount2020;
      }
    }

    public int TotalCount3030
    {
      get
      {
        return this.m_totalCount3030;
      }
    }

    public int TotalCount4040
    {
      get
      {
        return this.m_totalCount4040;
      }
    }

    public int TotalCountAplusB
    {
      get
      {
        return this.m_totalCountA + this.m_totalCountB;
      }
    }

    public double PercentA
    {
      get
      {
        if (this.m_totalCount <= 0)
          return 0.0;
        else
          return Math.Round(Convert.ToDouble(this.m_totalCountA) * 100.0 / Convert.ToDouble(this.m_totalCount), 2, MidpointRounding.AwayFromZero);
      }
    }

    public double PercentB
    {
      get
      {
        if (this.m_totalCount <= 0)
          return 0.0;
        else
          return Math.Round(Convert.ToDouble(this.m_totalCountB) * 100.0 / Convert.ToDouble(this.m_totalCount), 2, MidpointRounding.AwayFromZero);
      }
    }

    public double PercentC
    {
      get
      {
        if (this.m_totalCount <= 0)
          return 0.0;
        else
          return Math.Round(Convert.ToDouble(this.m_totalCountC) * 100.0 / Convert.ToDouble(this.m_totalCount), 2, MidpointRounding.AwayFromZero);
      }
    }

    public double PercentD
    {
      get
      {
        if (this.m_totalCount <= 0)
          return 0.0;
        else
          return Math.Round(Convert.ToDouble(this.m_totalCountD) * 100.0 / Convert.ToDouble(this.m_totalCount), 2, MidpointRounding.AwayFromZero);
      }
    }

    public double PercentE
    {
      get
      {
        if (this.m_totalCount <= 0)
          return 0.0;
        else
          return Math.Round(Convert.ToDouble(this.m_totalCountE) * 100.0 / Convert.ToDouble(this.m_totalCount), 2, MidpointRounding.AwayFromZero);
      }
    }

    public double Percent2020
    {
      get
      {
        if (this.m_totalCount <= 0)
          return 0.0;
        else
          return Math.Round(Convert.ToDouble(this.m_totalCount2020) * 100.0 / Convert.ToDouble(this.m_totalCount), 2, MidpointRounding.AwayFromZero);
      }
    }

    public double Percent3030
    {
      get
      {
        if (this.m_totalCount <= 0)
          return 0.0;
        else
          return Math.Round(Convert.ToDouble(this.m_totalCount3030) * 100.0 / Convert.ToDouble(this.m_totalCount), 2, MidpointRounding.AwayFromZero);
      }
    }

    public double Percent4040
    {
      get
      {
        if (this.m_totalCount <= 0)
          return 0.0;
        else
          return Math.Round(Convert.ToDouble(this.m_totalCount4040) * 100.0 / Convert.ToDouble(this.m_totalCount), 2, MidpointRounding.AwayFromZero);
      }
    }

    public double PercentAplusB
    {
      get
      {
        if (this.m_totalCount <= 0)
          return 0.0;
        else
          return Math.Round(Convert.ToDouble(this.m_totalCountA + this.m_totalCountB) * 100.0 / Convert.ToDouble(this.m_totalCount), 2, MidpointRounding.AwayFromZero);
      }
    }

    public double Mard
    {
      get
      {
        return Math.Round(this.m_mard * 100.0, 2, MidpointRounding.AwayFromZero);
      }
    }

    public double StdDevArd
    {
      get
      {
        return Math.Round(this.m_stdDevArd * 100.0, 2, MidpointRounding.AwayFromZero);
      }
    }

    public double MedianArd
    {
      get
      {
        return Math.Round(this.m_medianArd * 100.0, 2, MidpointRounding.AwayFromZero);
      }
    }

    public double MinArd
    {
      get
      {
        return Math.Round(this.m_minArd * 100.0, 2, MidpointRounding.AwayFromZero);
      }
    }

    public double MaxArd
    {
      get
      {
        return Math.Round(this.m_maxArd * 100.0, 2, MidpointRounding.AwayFromZero);
      }
    }

    public double MeanRD
    {
      get
      {
        return Math.Round(this.m_meanRD * 100.0, 2, MidpointRounding.AwayFromZero);
      }
    }

    public double StdDevRD
    {
      get
      {
        return Math.Round(this.m_stdDevRD * 100.0, 2, MidpointRounding.AwayFromZero);
      }
    }

    public double MedianRD
    {
      get
      {
        return Math.Round(this.m_medianRD * 100.0, 2, MidpointRounding.AwayFromZero);
      }
    }

    public double MinRD
    {
      get
      {
        return Math.Round(this.m_minRD * 100.0, 2, MidpointRounding.AwayFromZero);
      }
    }

    public double MaxRD
    {
      get
      {
        return Math.Round(this.m_maxRD * 100.0, 2, MidpointRounding.AwayFromZero);
      }
    }

    public List<MatchedPair> MatchedPairList
    {
      get
      {
        return this.m_matchedPairList;
      }
    }

    public MatchedPairStat()
    {
      this.Name = string.Empty;
      this.MinSMBG = int.MinValue;
      this.MaxSMBG = int.MaxValue;
      this.MinEGV = int.MinValue;
      this.MaxEGV = int.MaxValue;
    }

    public void Evaluate(MatchedPair matchedPair)
    {
      if (!this.DoDoesMatchCriterion(matchedPair))
        return;
      this.m_matchedPairList.Add(matchedPair);
      ++this.m_totalCount;
      if (R2ReceiverTools.DoesPointExistInRegionE(matchedPair))
        ++this.m_totalCountE;
      else if (R2ReceiverTools.DoesPointExistInRegionD(matchedPair))
        ++this.m_totalCountD;
      else if (R2ReceiverTools.DoesPointExistInRegionC(matchedPair))
        ++this.m_totalCountC;
      else if (R2ReceiverTools.DoesPointExistInRegionB(matchedPair))
        ++this.m_totalCountB;
      else
        ++this.m_totalCountA;
      if (R2ReceiverTools.DoesMatchedPairFallWithinRange(matchedPair, 20, 20))
      {
        ++this.m_totalCount2020;
        ++this.m_totalCount3030;
        ++this.m_totalCount4040;
      }
      else if (R2ReceiverTools.DoesMatchedPairFallWithinRange(matchedPair, 30, 30))
      {
        ++this.m_totalCount3030;
        ++this.m_totalCount4040;
      }
      else
      {
        if (!R2ReceiverTools.DoesMatchedPairFallWithinRange(matchedPair, 40, 40))
          return;
        ++this.m_totalCount4040;
      }
    }

    public void Calculate()
    {
      this.DoCalculateArdStats();
      this.DoCalculateRDStats();
    }

    private void DoCalculateArdStats()
    {
      List<double> values = new List<double>();
      if (this.m_matchedPairList.Count > 0)
        values.AddRange((IEnumerable<double>) R2ReceiverTools.CalculateArdListForMatchedPairs(this.m_matchedPairList));
      this.m_mard = MathUtils.Average(values);
      this.m_stdDevArd = MathUtils.StandardDeviation(values);
      List<double> list = MathUtils.Quartiles(values);
      this.m_minArd = list[0];
      this.m_medianArd = list[2];
      this.m_maxArd = list[4];
    }

    private void DoCalculateRDStats()
    {
      List<double> values = new List<double>();
      if (this.m_matchedPairList.Count > 0)
        values.AddRange((IEnumerable<double>) R2ReceiverTools.CalculateRDListForMatchedPairs(this.m_matchedPairList));
      this.m_meanRD = MathUtils.Average(values);
      this.m_stdDevRD = MathUtils.StandardDeviation(values);
      List<double> list = MathUtils.Quartiles(values);
      this.m_minRD = list[0];
      this.m_medianRD = list[2];
      this.m_maxRD = list[4];
    }

    private bool DoDoesMatchCriterion(MatchedPair matchedPair)
    {
      return this.DoDoesMatchCriterion(matchedPair.MeterValue, matchedPair.CalculatedGlucoseValue);
    }

    private bool DoDoesMatchCriterion(ushort meterValue, ushort egv)
    {
      return (int) meterValue >= this.MinSMBG && (int) meterValue <= this.MaxSMBG && ((int) egv >= this.MinEGV && (int) egv <= this.MaxEGV);
    }

    public XmlElement GetXml()
    {
      return this.GetXml(new XmlDocument());
    }

    public XmlElement GetXml(XmlDocument xDoc)
    {
      XObject xobject = new XObject(this.GetType().Name, xDoc);
      foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(this.GetType()))
      {
        switch (propertyDescriptor.Name)
        {
          case "MatchedPairList":
            continue;
          default:
            xobject.SetAttribute(propertyDescriptor.Name, propertyDescriptor.GetValue((object) this), propertyDescriptor.PropertyType);
            continue;
        }
      }
      return xobject.Element;
    }
  }
}

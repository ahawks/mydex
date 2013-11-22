// Type: Dexcom.Common.Data.EventData
// Assembly: Dexcom.Common.Data, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 64B9BFC3-C891-4315-B216-574914861A80
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.Data.dll

using Dexcom.Common;
using System;
using System.Xml;

namespace Dexcom.Common.Data
{
  public class EventData : IComparable
  {
    public Guid DeviceId;
    public uint RecordNumber;
    public DateTime DisplayTime;
    public DateTime InternalTime;
    public DateTime EventTime;
    public EventData.EventType TypeOfEvent;
    public int Value;

    public XmlElement Xml
    {
      get
      {
        return this.ToXml();
      }
    }

    public override string ToString()
    {
      return this.ToXml().OuterXml;
    }

    public XmlElement ToXml()
    {
      return this.ToXml(new XmlDocument());
    }

    public XmlElement ToXml(XmlDocument ownerDocument)
    {
      return new XEventData(ownerDocument)
      {
        DeviceId = this.DeviceId,
        RecordNumber = this.RecordNumber,
        DisplayTime = (this.DisplayTime == DateTime.MinValue ? CommonValues.EmptyDateTime : this.DisplayTime),
        InternalTime = (this.InternalTime == DateTime.MinValue ? CommonValues.EmptyDateTime : this.InternalTime),
        EventTime = (this.EventTime == DateTime.MinValue ? CommonValues.EmptyDateTime : this.EventTime),
        TypeOfEvent = this.TypeOfEvent,
        Value = this.Value
      }.Element;
    }

    public int CompareTo(object obj)
    {
      int num1 = 0;
      if (!(obj is EventData))
        throw new ArgumentException("obj not EventData");
      EventData eventData = (EventData) obj;
      int num2 = num1 == 0 ? this.RecordNumber.CompareTo(eventData.RecordNumber) : num1;
      int num3 = num2 == 0 ? this.InternalTime.CompareTo(eventData.InternalTime) : num2;
      int num4 = num3 == 0 ? this.DisplayTime.CompareTo(eventData.DisplayTime) : num3;
      int num5 = num4 == 0 ? this.EventTime.CompareTo(eventData.EventTime) : num4;
      int num6 = num5 == 0 ? this.TypeOfEvent.CompareTo((object) eventData.TypeOfEvent) : num5;
      return num6 == 0 ? this.Value.CompareTo(eventData.Value) : num6;
    }

    public enum EventType : byte
    {
      Unknown,
      Carbs,
      Insulin,
      ExerciseLight,
      ExerciseMedium,
      ExerciseHeavy,
      HealthAlcohol,
      HealthCycle,
      HealthHighSymptoms,
      HealthIllness,
      HealthLowSymptoms,
      HealthStress,
    }
  }
}

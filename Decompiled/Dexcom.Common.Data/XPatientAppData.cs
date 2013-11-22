// Type: Dexcom.Common.Data.XPatientAppData
// Assembly: Dexcom.Common.Data, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: 64B9BFC3-C891-4315-B216-574914861A80
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.Data.dll

using Dexcom.Common;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Security;
using System.Xml;
using System.Xml.Serialization;

namespace Dexcom.Common.Data
{
  [Serializable]
  public class XPatientAppData : XObject, ISerializable
  {
    public const string Tag = "PatientAppData";
    public const string BlindTransitionDataCollectionTag = "BlindTransitionDataCollection";
    public const string EventDataCollectionTag = "EventDataCollection";
    public const string GlucoseDataCollectionTag = "GlucoseDataCollection";
    public const string GlucoseDataArrayTag = "GlucoseDataArray";
    public const string MeterDataCollectionTag = "MeterDataCollection";
    public const string SensorSessionDataCollectionTag = "SensorSessionDataCollection";
    public const string TimeOffsetDataCollectionTag = "TimeOffsetDataCollection";

    [TypeConverter(typeof (OnlineGuidConverter))]
    [ColumnInfo(Visible = false)]
    [XmlAttribute]
    public new Guid Id
    {
      get
      {
        return this.GetAttributeAsGuid("Id");
      }
      set
      {
        this.SetAttribute("Id", value);
      }
    }

    [ColumnInfo(Visible = false)]
    [XmlAttribute]
    public new string Name
    {
      get
      {
        return this.GetAttribute("Name");
      }
      set
      {
        this.SetAttribute("Name", value.Trim());
      }
    }

    [XmlAttribute]
    [ColumnInfo(Visible = false)]
    public new string Description
    {
      get
      {
        return this.GetAttribute("Description");
      }
      set
      {
        this.SetAttribute("Description", value.Trim());
      }
    }

    [XmlAttribute]
    [ColumnInfo(Visible = false)]
    public DateTimeOffset DateCreated
    {
      get
      {
        return this.GetAttributeAsDateTimeOffset("DateCreated");
      }
      set
      {
        this.SetAttribute("DateCreated", value);
      }
    }

    [XmlAttribute]
    [ColumnInfo(Visible = false)]
    public DateTimeOffset DateModified
    {
      get
      {
        return this.GetAttributeAsDateTimeOffset("DateModified");
      }
      set
      {
        this.SetAttribute("DateModified", value);
      }
    }

    public XCollection<XBlindTransitionData> BlindTransitionDataCollection
    {
      get
      {
        XmlElement xmlElement = this.Element.SelectSingleNode("BlindTransitionDataCollection") as XmlElement;
        if (DataTools.IsCompressedElement(xmlElement))
          xmlElement = (XmlElement) DataTools.DecompressElementWithHint(xmlElement);
        return new XCollection<XBlindTransitionData>(xmlElement);
      }
      set
      {
        XmlElement xmlElement1 = this.Element.SelectSingleNode("BlindTransitionDataCollection") as XmlElement;
        XmlElement xmlElement2;
        if (true)
        {
          xmlElement2 = DataTools.CreateCompressedXmlElement(value.Element, "BlindTransitionDataCollection", this.Element.OwnerDocument);
          xmlElement2.SetAttribute("ItemCount", value.Count.ToString());
        }
        else
        {
          xmlElement2 = this.Element.OwnerDocument.ImportNode((XmlNode) value.Element, true) as XmlElement;
          xmlElement2.SetAttribute("ItemCount", value.Count.ToString());
        }
        if (xmlElement1 == null)
          this.Element.AppendChild((XmlNode) xmlElement2);
        else
          this.Element.ReplaceChild((XmlNode) xmlElement2, (XmlNode) xmlElement1);
      }
    }

    public XCollection<XEventData> EventDataCollection
    {
      get
      {
        XmlElement xmlElement = this.Element.SelectSingleNode("EventDataCollection") as XmlElement;
        if (DataTools.IsCompressedElement(xmlElement))
          xmlElement = (XmlElement) DataTools.DecompressElementWithHint(xmlElement);
        return new XCollection<XEventData>(xmlElement);
      }
      set
      {
        XmlElement xmlElement1 = this.Element.SelectSingleNode("EventDataCollection") as XmlElement;
        XmlElement xmlElement2;
        if (false)
        {
          xmlElement2 = DataTools.CreateCompressedXmlElement(value.Element, "EventDataCollection", this.Element.OwnerDocument);
          xmlElement2.SetAttribute("ItemCount", value.Count.ToString());
        }
        else
        {
          xmlElement2 = this.Element.OwnerDocument.ImportNode((XmlNode) value.Element, true) as XmlElement;
          xmlElement2.SetAttribute("ItemCount", value.Count.ToString());
        }
        if (xmlElement1 == null)
          this.Element.AppendChild((XmlNode) xmlElement2);
        else
          this.Element.ReplaceChild((XmlNode) xmlElement2, (XmlNode) xmlElement1);
      }
    }

    public XCollection<XGlucoseData> GlucoseDataCollection
    {
      get
      {
        XmlElement xmlElement = this.Element.SelectSingleNode("GlucoseDataCollection") as XmlElement;
        if (DataTools.IsCompressedElement(xmlElement))
          xmlElement = (XmlElement) DataTools.DecompressElementWithHint(xmlElement);
        return new XCollection<XGlucoseData>(xmlElement);
      }
      set
      {
        XmlElement xmlElement1 = this.Element.SelectSingleNode("GlucoseDataCollection") as XmlElement;
        XmlElement xmlElement2;
        if (false)
        {
          xmlElement2 = DataTools.CreateCompressedXmlElement(value.Element, "GlucoseDataCollection", this.Element.OwnerDocument);
          xmlElement2.SetAttribute("ItemCount", value.Count.ToString());
        }
        else
        {
          xmlElement2 = this.Element.OwnerDocument.ImportNode((XmlNode) value.Element, true) as XmlElement;
          xmlElement2.SetAttribute("ItemCount", value.Count.ToString());
        }
        if (xmlElement1 == null)
          this.Element.AppendChild((XmlNode) xmlElement2);
        else
          this.Element.ReplaceChild((XmlNode) xmlElement2, (XmlNode) xmlElement1);
      }
    }

    public XObject GlucoseDataArrayElement
    {
      get
      {
        return new XObject(this.Element.SelectSingleNode("GlucoseDataArray") as XmlElement);
      }
    }

    public byte[] GlucoseDataStructArray
    {
      get
      {
        byte[] numArray = (byte[]) null;
        XmlElement sourceElement = this.Element.SelectSingleNode("GlucoseDataArray") as XmlElement;
        if (DataTools.IsCompressedElement(sourceElement))
          numArray = DataTools.DecompressElement(sourceElement);
        return numArray;
      }
      set
      {
        XmlElement xmlElement1 = this.Element.SelectSingleNode("GlucoseDataArray") as XmlElement;
        XmlElement xmlElement2 = value.Length <= 0 ? new XObject("GlucoseDataArray", this.Element.OwnerDocument).Element : DataTools.CreateCompressedElement(value, "GlucoseDataArray", this.Element.OwnerDocument, DataTools.CompressedHintType.Binary);
        if (xmlElement1 == null)
          this.Element.AppendChild((XmlNode) xmlElement2);
        else
          this.Element.ReplaceChild((XmlNode) xmlElement2, (XmlNode) xmlElement1);
      }
    }

    public XCollection<XMeterData> MeterDataCollection
    {
      get
      {
        XmlElement xmlElement = this.Element.SelectSingleNode("MeterDataCollection") as XmlElement;
        if (DataTools.IsCompressedElement(xmlElement))
          xmlElement = (XmlElement) DataTools.DecompressElementWithHint(xmlElement);
        return new XCollection<XMeterData>(xmlElement);
      }
      set
      {
        XmlElement xmlElement1 = this.Element.SelectSingleNode("MeterDataCollection") as XmlElement;
        XmlElement xmlElement2;
        if (false)
        {
          xmlElement2 = DataTools.CreateCompressedXmlElement(value.Element, "MeterDataCollection", this.Element.OwnerDocument);
          xmlElement2.SetAttribute("ItemCount", value.Count.ToString());
        }
        else
        {
          xmlElement2 = this.Element.OwnerDocument.ImportNode((XmlNode) value.Element, true) as XmlElement;
          xmlElement2.SetAttribute("ItemCount", value.Count.ToString());
        }
        if (xmlElement1 == null)
          this.Element.AppendChild((XmlNode) xmlElement2);
        else
          this.Element.ReplaceChild((XmlNode) xmlElement2, (XmlNode) xmlElement1);
      }
    }

    public XCollection<XSensorSessionData> SensorSessionDataCollection
    {
      get
      {
        XmlElement xmlElement = this.Element.SelectSingleNode("SensorSessionDataCollection") as XmlElement;
        if (DataTools.IsCompressedElement(xmlElement))
          xmlElement = (XmlElement) DataTools.DecompressElementWithHint(xmlElement);
        return new XCollection<XSensorSessionData>(xmlElement);
      }
      set
      {
        XmlElement xmlElement1 = this.Element.SelectSingleNode("SensorSessionDataCollection") as XmlElement;
        XmlElement xmlElement2;
        if (false)
        {
          xmlElement2 = DataTools.CreateCompressedXmlElement(value.Element, "SensorSessionDataCollection", this.Element.OwnerDocument);
          xmlElement2.SetAttribute("ItemCount", value.Count.ToString());
        }
        else
        {
          xmlElement2 = this.Element.OwnerDocument.ImportNode((XmlNode) value.Element, true) as XmlElement;
          xmlElement2.SetAttribute("ItemCount", value.Count.ToString());
        }
        if (xmlElement1 == null)
          this.Element.AppendChild((XmlNode) xmlElement2);
        else
          this.Element.ReplaceChild((XmlNode) xmlElement2, (XmlNode) xmlElement1);
      }
    }

    public XCollection<XTimeOffsetData> TimeOffsetDataCollection
    {
      get
      {
        XmlElement xmlElement = this.Element.SelectSingleNode("TimeOffsetDataCollection") as XmlElement;
        if (DataTools.IsCompressedElement(xmlElement))
          xmlElement = (XmlElement) DataTools.DecompressElementWithHint(xmlElement);
        return new XCollection<XTimeOffsetData>(xmlElement);
      }
      set
      {
        XmlElement xmlElement1 = this.Element.SelectSingleNode("TimeOffsetDataCollection") as XmlElement;
        XmlElement xmlElement2;
        if (false)
        {
          xmlElement2 = DataTools.CreateCompressedXmlElement(value.Element, "TimeOffsetDataCollection", this.Element.OwnerDocument);
          xmlElement2.SetAttribute("ItemCount", value.Count.ToString());
        }
        else
        {
          xmlElement2 = this.Element.OwnerDocument.ImportNode((XmlNode) value.Element, true) as XmlElement;
          xmlElement2.SetAttribute("ItemCount", value.Count.ToString());
        }
        if (xmlElement1 == null)
          this.Element.AppendChild((XmlNode) xmlElement2);
        else
          this.Element.ReplaceChild((XmlNode) xmlElement2, (XmlNode) xmlElement1);
      }
    }

    public XPatientAppData()
      : this(new XmlDocument())
    {
    }

    public XPatientAppData(XmlDocument ownerDocument)
      : base("PatientAppData", ownerDocument)
    {
      DateTimeOffset now = DateTimeOffset.Now;
      this.Id = Guid.NewGuid();
      this.Name = string.Empty;
      this.DateCreated = now;
      this.DateModified = now;
      this.Element.AppendChild((XmlNode) new XObject("BlindTransitionDataCollection", ownerDocument).Element);
      this.Element.AppendChild((XmlNode) new XObject("EventDataCollection", ownerDocument).Element);
      this.Element.AppendChild((XmlNode) new XObject("GlucoseDataCollection", ownerDocument).Element);
      this.Element.AppendChild((XmlNode) new XObject("GlucoseDataArray", ownerDocument).Element);
      this.Element.AppendChild((XmlNode) new XObject("MeterDataCollection", ownerDocument).Element);
      this.Element.AppendChild((XmlNode) new XObject("SensorSessionDataCollection", ownerDocument).Element);
      this.Element.AppendChild((XmlNode) new XObject("TimeOffsetDataCollection", ownerDocument).Element);
    }

    public XPatientAppData(XmlElement element)
      : base(element)
    {
    }

    protected XPatientAppData(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    [SecurityCritical]
    public new void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      base.GetObjectData(info, context);
    }
  }
}

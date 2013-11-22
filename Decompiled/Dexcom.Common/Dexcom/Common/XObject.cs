// Type: Dexcom.Common.XObject
// Assembly: Dexcom.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B95A59FD-1598-4EA8-B1F5-D812FE5A6802
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.dll

using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security;
using System.Xml;
using System.Xml.Serialization;

namespace Dexcom.Common
{
  [Serializable]
  public class XObject : ISerializable
  {
    private XmlElement m_element;

    public virtual string TagName
    {
      get
      {
        if (this.m_element != null)
          return this.m_element.Name;
        else
          return string.Empty;
      }
    }

    public virtual XmlElement Element
    {
      get
      {
        return this.m_element;
      }
    }

    public virtual string Xml
    {
      get
      {
        if (this.m_element != null)
          return this.m_element.OuterXml;
        else
          return string.Empty;
      }
    }

    [ColumnInfo]
    [XmlAttribute]
    [TypeConverter(typeof (OnlineGuidConverter))]
    public Guid Id
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

    [ColumnInfo]
    [XmlAttribute]
    public string Name
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

    [ColumnInfo]
    [XmlAttribute]
    public string Description
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

    protected XObject()
    {
    }

    public XObject(string strName)
    {
      if (strName == null)
        throw new ArgumentNullException("Invalid null argument: strName");
      if (strName == string.Empty)
        throw new ArgumentException("Invalid empty argument: strName");
      this.m_element = new XmlDocument().CreateElement(strName);
    }

    public XObject(XmlElement element)
    {
      this.m_element = element;
    }

    public XObject(string strName, XmlDocument ownerDocument)
    {
      if (strName == null)
        throw new ArgumentNullException("Invalid null argument: strName");
      if (strName == string.Empty)
        throw new ArgumentException("Invalid empty argument: strName");
      if (ownerDocument == null)
        throw new ArgumentNullException("Invalid null argument: ownerDocument");
      this.m_element = ownerDocument.CreateElement(strName);
    }

    protected XObject(SerializationInfo info, StreamingContext context)
    {
      string @string = info.GetString("XObject.m_element");
      if (@string == null || !(@string != string.Empty))
        return;
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.LoadXml(@string);
      this.m_element = xmlDocument.DocumentElement;
    }

    public void AppendChild(XObject xChildObject)
    {
      if (this.IsNull())
        throw new DexComException("XObject's element is null.");
      if (xChildObject.IsNull())
        throw new DexComException("xChildObject's element is null.");
      if (!this.IsNotNull() || !xChildObject.IsNotNull())
        return;
      this.m_element.AppendChild((XmlNode) xChildObject.Element);
    }

    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      string str = this.FullOuterXml();
      info.AddValue("XObject.m_element", (object) str);
    }

    public string FullOuterXml()
    {
      string str = string.Empty;
      if (this.m_element != null)
        str = this.m_element.OwnerDocument.DocumentElement != this.m_element ? this.m_element.OuterXml : this.m_element.OwnerDocument.OuterXml;
      return str;
    }

    public XmlElement CreateElement(string strName)
    {
      XmlElement xmlElement = (XmlElement) null;
      if (this.m_element != null)
      {
        XmlDocument ownerDocument = this.m_element.OwnerDocument;
        if (ownerDocument != null)
          xmlElement = ownerDocument.CreateElement(strName);
      }
      if (xmlElement == null)
        xmlElement = new XmlDocument().CreateElement(strName);
      return xmlElement;
    }

    public bool IsNull()
    {
      return this.m_element == null;
    }

    public bool IsNotNull()
    {
      return this.m_element != null;
    }

    public bool HasAttribute(string strName)
    {
      if (this.IsNull())
        throw new DexComException("XObject's element is null.");
      bool flag = false;
      if (this.m_element != null)
        flag = this.m_element.HasAttribute(strName);
      return flag;
    }

    public string GetAttribute(string strName)
    {
      if (this.IsNull())
        throw new DexComException("XObject's element is null.");
      string str = (string) null;
      if (this.m_element != null)
      {
        XmlAttribute attributeNode = this.m_element.GetAttributeNode(strName);
        if (attributeNode != null)
          str = attributeNode.Value;
      }
      return str;
    }

    public string GetAttributeAsString(string strName)
    {
      string attribute = this.GetAttribute(strName);
      if (attribute == null)
        throw new FormatException(string.Format("Attribute '{0}' not found.", (object) strName));
      else
        return attribute;
    }

    public void GetAttribute(string strName, out string attributeValue)
    {
      attributeValue = this.GetAttributeAsString(strName);
    }

    public bool GetAttributeAsBool(string strName)
    {
      string attribute = this.GetAttribute(strName);
      if (attribute == "1")
        return true;
      if (attribute == "0" || attribute == string.Empty)
        return false;
      if (attribute == null)
        throw new FormatException(string.Format("Bool attribute '{0}' not found.", (object) strName));
      else
        throw new FormatException(string.Format("Bool attribute '{0}' value not formatted correctly.", (object) strName));
    }

    public void GetAttribute(string strName, out bool attributeValue)
    {
      attributeValue = this.GetAttributeAsBool(strName);
    }

    public int GetAttributeAsInt(string strName)
    {
      string attribute = this.GetAttribute(strName);
      int num;
      if (attribute == string.Empty)
      {
        num = 0;
      }
      else
      {
        if (attribute == null)
          throw new FormatException(string.Format("Int attribute '{0}' not found.", (object) strName));
        num = int.Parse(attribute);
      }
      return num;
    }

    public void GetAttribute(string strName, out int attributeValue)
    {
      attributeValue = this.GetAttributeAsInt(strName);
    }

    public uint GetAttributeAsUInt(string strName)
    {
      string attribute = this.GetAttribute(strName);
      uint num;
      if (attribute == string.Empty)
      {
        num = 0U;
      }
      else
      {
        if (attribute == null)
          throw new FormatException(string.Format("UInt attribute '{0}' not found.", (object) strName));
        num = uint.Parse(attribute);
      }
      return num;
    }

    public void GetAttribute(string strName, out uint attributeValue)
    {
      attributeValue = this.GetAttributeAsUInt(strName);
    }

    public ushort GetAttributeAsUShort(string strName)
    {
      string attribute = this.GetAttribute(strName);
      ushort num;
      if (attribute == string.Empty)
      {
        num = (ushort) 0;
      }
      else
      {
        if (attribute == null)
          throw new FormatException(string.Format("UShort attribute '{0}' not found.", (object) strName));
        num = ushort.Parse(attribute);
      }
      return num;
    }

    public void GetAttribute(string strName, out short attributeValue)
    {
      attributeValue = this.GetAttributeAsShort(strName);
    }

    public short GetAttributeAsShort(string strName)
    {
      string attribute = this.GetAttribute(strName);
      short num;
      if (attribute == string.Empty)
      {
        num = (short) 0;
      }
      else
      {
        if (attribute == null)
          throw new FormatException(string.Format("Short attribute '{0}' not found.", (object) strName));
        num = short.Parse(attribute);
      }
      return num;
    }

    public void GetAttribute(string strName, out ushort attributeValue)
    {
      attributeValue = this.GetAttributeAsUShort(strName);
    }

    public byte GetAttributeAsByte(string strName)
    {
      string attribute = this.GetAttribute(strName);
      byte num;
      if (attribute == string.Empty)
      {
        num = (byte) 0;
      }
      else
      {
        if (attribute == null)
          throw new FormatException(string.Format("Byte attribute '{0}' not found.", (object) strName));
        num = byte.Parse(attribute);
      }
      return num;
    }

    public void GetAttribute(string strName, out byte attributeValue)
    {
      attributeValue = this.GetAttributeAsByte(strName);
    }

    public long GetAttributeAsLong(string strName)
    {
      string attribute = this.GetAttribute(strName);
      long num;
      if (attribute == string.Empty)
      {
        num = 0L;
      }
      else
      {
        if (attribute == null)
          throw new FormatException(string.Format("Long attribute '{0}' not found.", (object) strName));
        num = long.Parse(attribute);
      }
      return num;
    }

    public void GetAttribute(string strName, out long attributeValue)
    {
      attributeValue = this.GetAttributeAsLong(strName);
    }

    public ulong GetAttributeAsULong(string strName)
    {
      string attribute = this.GetAttribute(strName);
      ulong num;
      if (attribute == string.Empty)
      {
        num = 0UL;
      }
      else
      {
        if (attribute == null)
          throw new FormatException(string.Format("ULong attribute '{0}' not found.", (object) strName));
        num = ulong.Parse(attribute);
      }
      return num;
    }

    public void GetAttribute(string strName, out ulong attributeValue)
    {
      attributeValue = this.GetAttributeAsULong(strName);
    }

    public double GetAttributeAsDouble(string strName)
    {
      string attribute = this.GetAttribute(strName);
      double num;
      if (attribute == string.Empty)
      {
        num = 0.0;
      }
      else
      {
        if (attribute == null)
          throw new FormatException(string.Format("Double attribute '{0}' not found.", (object) strName));
        num = double.Parse(attribute, (IFormatProvider) CultureInfo.InvariantCulture);
      }
      return num;
    }

    public void GetAttribute(string strName, out double attributeValue)
    {
      attributeValue = this.GetAttributeAsDouble(strName);
    }

    public Guid GetAttributeAsGuid(string strName)
    {
      Guid guid = Guid.Empty;
      string attribute = this.GetAttribute(strName);
      if (attribute == null)
        throw new FormatException(string.Format("Guid attribute '{0}' not found.", (object) strName));
      string g = attribute.Trim();
      if (!(g != string.Empty))
        return Guid.Empty;
      try
      {
        return new Guid(g);
      }
      catch
      {
        throw new FormatException(string.Format("Guid attribute '{0}' not formatted correctly.", (object) strName));
      }
    }

    public void GetAttribute(string strName, out Guid attributeValue)
    {
      attributeValue = this.GetAttributeAsGuid(strName);
    }

    public DateTime GetAttributeAsDateTime(string strName)
    {
      DateTime dateTime = CommonValues.EmptyDateTime;
      string attribute = this.GetAttribute(strName);
      if (attribute == null)
        throw new FormatException(string.Format("DateTime attribute '{0}' not found.", (object) strName));
      string s = attribute.Trim();
      if (s == "MaxValue")
        return DateTime.MaxValue;
      if (s == "MinValue")
        return DateTime.MinValue;
      if (!(s != string.Empty))
        return CommonValues.EmptyDateTime;
      try
      {
        return DateTime.Parse(s, (IFormatProvider) CultureInfo.InvariantCulture);
      }
      catch
      {
        throw new FormatException(string.Format("DateTime attribute '{0}' not formatted correctly.", (object) strName));
      }
    }

    public DateTimeOffset GetAttributeAsDateTimeOffset(string strName)
    {
      DateTimeOffset dateTimeOffset = CommonValues.EmptyDateTimeOffset;
      string attribute = this.GetAttribute(strName);
      if (attribute == null)
        throw new FormatException(string.Format("DateTimeOffset attribute '{0}' not found.", (object) strName));
      string input = attribute.Trim();
      if (input == "MaxValue")
        return DateTimeOffset.MaxValue;
      if (input == "MinValue")
        return DateTimeOffset.MinValue;
      if (!(input != string.Empty))
        return CommonValues.EmptyDateTimeOffset;
      try
      {
        return DateTimeOffset.Parse(input, (IFormatProvider) CultureInfo.InvariantCulture);
      }
      catch
      {
        throw new FormatException(string.Format("DateTimeOffset attribute '{0}' not formatted correctly.", (object) strName));
      }
    }

    public void GetAttribute(string strName, out DateTime attributeValue)
    {
      attributeValue = this.GetAttributeAsDateTime(strName);
    }

    public void GetAttribute(string strName, out DateTimeOffset attributeValue)
    {
      attributeValue = this.GetAttributeAsDateTimeOffset(strName);
    }

    public TimeSpan GetAttributeAsTimeSpan(string strName)
    {
      TimeSpan timeSpanValue = TimeSpan.Zero;
      string attribute = this.GetAttribute(strName);
      if (attribute == null)
        throw new FormatException(string.Format("TimeSpan attribute '{0}' not found.", (object) strName));
      if (!CommonTools.ParseTimeSpan(attribute.Trim(), out timeSpanValue))
        throw new FormatException(string.Format("TimeSpan attribute '{0}' not formatted correctly.", (object) strName));
      else
        return timeSpanValue;
    }

    public void GetAttribute(string strName, out TimeSpan attributeValue)
    {
      attributeValue = this.GetAttributeAsTimeSpan(strName);
    }

    public object GetAttributeAsEnum(string strName, Type enumType)
    {
      string attributeAsString = this.GetAttributeAsString(strName);
      return Enum.Parse(enumType, attributeAsString);
    }

    public object GetAttribute(string strName, Type hintType)
    {
      switch (Type.GetTypeCode(hintType))
      {
        case TypeCode.Boolean:
          return (object) (bool) (this.GetAttribute<bool>(strName) ? 1 : 0);
        case TypeCode.Byte:
          return (object) this.GetAttribute<byte>(strName);
        case TypeCode.Int16:
          return (object) this.GetAttribute<short>(strName);
        case TypeCode.UInt16:
          return (object) this.GetAttribute<ushort>(strName);
        case TypeCode.Int32:
          return (object) this.GetAttribute<int>(strName);
        case TypeCode.UInt32:
          return (object) this.GetAttribute<uint>(strName);
        case TypeCode.Int64:
          return (object) this.GetAttribute<long>(strName);
        case TypeCode.UInt64:
          return (object) this.GetAttribute<ulong>(strName);
        case TypeCode.Double:
          return (object) this.GetAttribute<double>(strName);
        case TypeCode.DateTime:
          return (object) this.GetAttribute<DateTime>(strName);
        case TypeCode.String:
          return (object) this.GetAttribute<string>(strName);
        default:
          if (hintType == typeof (Guid))
            return (object) this.GetAttribute<Guid>(strName);
          if (hintType == typeof (TimeSpan))
            return (object) this.GetAttributeAsTimeSpan(strName);
          if (hintType == typeof (DateTimeOffset))
            return (object) this.GetAttributeAsDateTimeOffset(strName);
          else
            throw new FormatException(string.Format("Attempt to get attribute '{0}' with unknown hint type!", (object) strName));
      }
    }

    public T GetAttribute<T>(string strName)
    {
      string attributeAsString = this.GetAttributeAsString(strName);
      Type type = typeof (T);
      TypeCode typeCode = Type.GetTypeCode(type);
      if (string.IsNullOrEmpty(attributeAsString))
      {
        if (typeCode == TypeCode.DateTime)
          return (T) (ValueType) CommonValues.EmptyDateTime;
        if (type == typeof (DateTimeOffset))
          return (T) (ValueType) CommonValues.EmptyDateTimeOffset;
        if (typeCode == TypeCode.String)
          return (T) string.Empty;
        else
          return default (T);
      }
      else
      {
        if (type == typeof (TimeSpan))
          return (T) (ValueType) this.GetAttributeAsTimeSpan(strName);
        if (type == typeof (DateTimeOffset))
          return (T) (ValueType) this.GetAttributeAsDateTimeOffset(strName);
        switch (typeCode)
        {
          case TypeCode.Boolean:
            if (attributeAsString == "1")
              return (T) (ValueType) true;
            if (attributeAsString == "0")
              return (T) (ValueType) false;
            else
              throw new FormatException(string.Format("Bool attribute '{0}' value not formatted correctly.", (object) strName));
          case TypeCode.DateTime:
            return (T) (ValueType) this.GetAttributeAsDateTime(strName);
          case TypeCode.String:
            return (T) attributeAsString;
          default:
            return (T) TypeDescriptor.GetConverter(type).ConvertFromString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, attributeAsString);
        }
      }
    }

    public T GetXPathAttribute<T>(string xPath)
    {
      if (this.IsNull())
        throw new DexComException("XObject's element is null.");
      string str = (string) null;
      if (this.m_element != null)
      {
        XmlAttribute xmlAttribute = this.m_element.SelectSingleNode(xPath) as XmlAttribute;
        if (xmlAttribute != null)
          str = xmlAttribute.Value;
      }
      if (str == null)
        throw new FormatException(string.Format("XPath Attribute '{0}' not found.", (object) xPath));
      Type type = typeof (T);
      TypeCode typeCode = Type.GetTypeCode(type);
      if (string.IsNullOrEmpty(str))
      {
        if (typeCode == TypeCode.DateTime)
          return (T) (ValueType) CommonValues.EmptyDateTime;
        if (type == typeof (DateTimeOffset))
          return (T) (ValueType) CommonValues.EmptyDateTimeOffset;
        if (typeCode == TypeCode.String)
          return (T) string.Empty;
        else
          return default (T);
      }
      else if (type == typeof (TimeSpan))
      {
        TimeSpan timeSpanValue = TimeSpan.Zero;
        if (!CommonTools.ParseTimeSpan(str, out timeSpanValue))
          throw new FormatException(string.Format("TimeSpan attribute '{0}' not formatted correctly.", (object) xPath));
        else
          return (T) (ValueType) timeSpanValue;
      }
      else
      {
        switch (Type.GetTypeCode(type))
        {
          case TypeCode.Boolean:
            if (str == "1")
              return (T) (ValueType) true;
            if (str == "0")
              return (T) (ValueType) false;
            else
              throw new FormatException(string.Format("Bool attribute '{0}' value not formatted correctly.", (object) xPath));
          case TypeCode.String:
            return (T) str;
          default:
            return (T) TypeDescriptor.GetConverter(type).ConvertFromString(str);
        }
      }
    }

    public bool HasXPathAttribute(string xPath)
    {
      if (this.IsNull())
        throw new DexComException("XObject's element is null.");
      else
        return this.m_element.SelectSingleNode(xPath) is XmlAttribute;
    }

    public void SetAttribute(string strName, object attrValue, Type hintType)
    {
      if (attrValue == null || attrValue is DBNull)
      {
        switch (Type.GetTypeCode(hintType))
        {
          case TypeCode.Boolean:
            this.SetAttribute(strName, false);
            break;
          case TypeCode.Byte:
            this.SetAttribute(strName, (byte) 0);
            break;
          case TypeCode.Int16:
            this.SetAttribute(strName, (short) 0);
            break;
          case TypeCode.UInt16:
            this.SetAttribute(strName, (ushort) 0);
            break;
          case TypeCode.Int32:
            this.SetAttribute(strName, 0);
            break;
          case TypeCode.UInt32:
            this.SetAttribute(strName, 0U);
            break;
          case TypeCode.Int64:
            this.SetAttribute(strName, 0L);
            break;
          case TypeCode.UInt64:
            this.SetAttribute(strName, 0UL);
            break;
          case TypeCode.Double:
            this.SetAttribute(strName, 0.0);
            break;
          case TypeCode.DateTime:
            this.SetAttribute(strName, CommonValues.EmptyDateTime);
            break;
          case TypeCode.String:
            this.SetAttribute(strName, string.Empty);
            break;
          default:
            if (hintType == typeof (Guid))
            {
              this.SetAttribute(strName, Guid.Empty);
              break;
            }
            else if (hintType == typeof (TimeSpan))
            {
              this.SetAttribute(strName, TimeSpan.Zero);
              break;
            }
            else
            {
              if (!(hintType == typeof (DateTimeOffset)))
                throw new FormatException(string.Format("Attempt to set attribute '{0}' with null value and unknown hint type!", (object) strName));
              this.SetAttribute(strName, CommonValues.EmptyDateTimeOffset);
              break;
            }
        }
      }
      else if (attrValue.GetType() == hintType)
      {
        switch (Type.GetTypeCode(attrValue.GetType()))
        {
          case TypeCode.Boolean:
            this.SetAttribute(strName, (bool) attrValue);
            break;
          case TypeCode.Byte:
            this.SetAttribute(strName, (byte) attrValue);
            break;
          case TypeCode.Int16:
            this.SetAttribute(strName, (short) attrValue);
            break;
          case TypeCode.UInt16:
            this.SetAttribute(strName, (ushort) attrValue);
            break;
          case TypeCode.Int32:
            this.SetAttribute(strName, (int) attrValue);
            break;
          case TypeCode.UInt32:
            this.SetAttribute(strName, (uint) attrValue);
            break;
          case TypeCode.Int64:
            this.SetAttribute(strName, (long) attrValue);
            break;
          case TypeCode.UInt64:
            this.SetAttribute(strName, (ulong) attrValue);
            break;
          case TypeCode.Double:
            this.SetAttribute(strName, (double) attrValue);
            break;
          case TypeCode.DateTime:
            this.SetAttribute(strName, (DateTime) attrValue);
            break;
          case TypeCode.String:
            this.SetAttribute(strName, (string) attrValue);
            break;
          default:
            if (hintType == typeof (Guid))
            {
              this.SetAttribute(strName, (Guid) attrValue);
              break;
            }
            else if (hintType == typeof (TimeSpan))
            {
              this.SetAttribute(strName, (TimeSpan) attrValue);
              break;
            }
            else
            {
              if (!(hintType == typeof (DateTimeOffset)))
                throw new FormatException(string.Format("Attempt to set attribute '{0}' with generic value and unknown hint type!", (object) strName));
              this.SetAttribute(strName, (DateTimeOffset) attrValue);
              break;
            }
        }
      }
      else if (hintType == typeof (bool) && attrValue is int)
        this.SetAttribute(strName, (int) attrValue != 0);
      else if (hintType == typeof (TimeSpan) && attrValue is long)
      {
        this.SetAttribute(strName, new TimeSpan((long) attrValue));
      }
      else
      {
        if (!(hintType == typeof (TimeSpan)) || !(attrValue is double))
          throw new FormatException(string.Format("Attempt to set attribute '{0}' with generic value incompatible with hint type!", (object) strName));
        this.SetAttribute(strName, DateTime.FromOADate((double) attrValue) - CommonValues.EmptyDateTime);
      }
    }

    public void SetAttribute(string strName, string strValue)
    {
      if (this.IsNull())
        throw new DexComException("XObject's element is null.");
      if (this.m_element == null)
        return;
      this.m_element.SetAttribute(strName, strValue);
    }

    public void SetAttribute(string strName, bool boolValue)
    {
      this.SetAttribute(strName, boolValue ? "1" : "0");
    }

    public void SetAttribute(string strName, byte byteValue)
    {
      this.SetAttribute(strName, byteValue.ToString());
    }

    public void SetAttribute(string strName, short shortValue)
    {
      this.SetAttribute(strName, shortValue.ToString());
    }

    public void SetAttribute(string strName, ushort ushortValue)
    {
      this.SetAttribute(strName, ushortValue.ToString());
    }

    public void SetAttribute(string strName, int intValue)
    {
      this.SetAttribute(strName, intValue.ToString());
    }

    public void SetAttribute(string strName, uint uintValue)
    {
      this.SetAttribute(strName, uintValue.ToString());
    }

    public void SetAttribute(string strName, long longValue)
    {
      this.SetAttribute(strName, longValue.ToString());
    }

    public void SetAttribute(string strName, ulong ulongValue)
    {
      this.SetAttribute(strName, ulongValue.ToString());
    }

    public void SetAttribute(string strName, double doubleValue)
    {
      this.SetAttribute(strName, doubleValue.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    }

    public void SetAttribute(string strName, DateTime dtValue)
    {
      string strValue = string.Empty;
      if (!(dtValue == CommonValues.EmptyDateTime))
        strValue = !(dtValue == DateTime.MinValue) ? (!(dtValue == DateTime.MaxValue) ? CommonTools.ConvertToString(dtValue) : "MaxValue") : "MinValue";
      this.SetAttribute(strName, strValue);
    }

    public void SetAttribute(string strName, DateTimeOffset dtoValue)
    {
      string strValue = string.Empty;
      if (!(dtoValue == CommonValues.EmptyDateTimeOffset))
        strValue = !(dtoValue == DateTimeOffset.MinValue) ? (!(dtoValue == DateTimeOffset.MaxValue) ? CommonTools.ConvertToString(dtoValue) : "MaxValue") : "MinValue";
      this.SetAttribute(strName, strValue);
    }

    public void SetAttribute(string strName, TimeSpan tsValue)
    {
      string strValue = string.Empty;
      if (tsValue != TimeSpan.Zero)
        strValue = tsValue.ToString();
      this.SetAttribute(strName, strValue);
    }

    public void SetAttribute(string strName, Guid guidValue)
    {
      string strValue = string.Empty;
      if (guidValue != Guid.Empty)
        strValue = CommonTools.ConvertToString(guidValue);
      this.SetAttribute(strName, strValue);
    }
  }
}

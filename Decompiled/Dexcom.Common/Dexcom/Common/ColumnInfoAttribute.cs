// Type: Dexcom.Common.ColumnInfoAttribute
// Assembly: Dexcom.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B95A59FD-1598-4EA8-B1F5-D812FE5A6802
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Common.dll

using System;
using System.Collections;
using System.ComponentModel;

namespace Dexcom.Common
{
  [AttributeUsage(AttributeTargets.Property)]
  public sealed class ColumnInfoAttribute : Attribute, IComparable
  {
    private int m_defaultWidth = 100;
    private string m_displayName = string.Empty;
    private string m_displayKey = string.Empty;
    private string m_description = string.Empty;
    private string m_formatString = string.Empty;
    private int m_ordinal = -1;
    private bool m_isVisible = true;
    private string m_name = string.Empty;
    private int m_index = -1;
    private Type m_columnType;
    private PropertyDescriptor m_propertyDescriptor;

    public int DefaultWidth
    {
      get
      {
        return this.m_defaultWidth;
      }
      set
      {
        this.m_defaultWidth = value;
      }
    }

    public string DisplayName
    {
      get
      {
        return this.m_displayName;
      }
      set
      {
        if (value != null)
          this.m_displayName = value.Trim();
        else
          this.m_displayName = string.Empty;
      }
    }

    public string DisplayKey
    {
      get
      {
        return this.m_displayKey;
      }
      set
      {
        if (value != null)
          this.m_displayKey = value.Trim();
        else
          this.m_displayKey = string.Empty;
      }
    }

    public string Description
    {
      get
      {
        return this.m_description;
      }
      set
      {
        if (value != null)
          this.m_description = value.Trim();
        else
          this.m_description = string.Empty;
      }
    }

    public string FormatString
    {
      get
      {
        return this.m_formatString;
      }
      set
      {
        if (value != null)
          this.m_formatString = value.Trim();
        else
          this.m_formatString = string.Empty;
      }
    }

    public int Ordinal
    {
      get
      {
        return this.m_ordinal;
      }
      set
      {
        this.m_ordinal = value;
      }
    }

    public bool Visible
    {
      get
      {
        return this.m_isVisible;
      }
      set
      {
        this.m_isVisible = value;
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
        if (value != null)
          this.m_name = value.Trim();
        else
          this.m_name = string.Empty;
      }
    }

    public Type ColumnType
    {
      get
      {
        return this.m_columnType;
      }
      set
      {
        this.m_columnType = value;
      }
    }

    public int Index
    {
      get
      {
        return this.m_index;
      }
      set
      {
        this.m_index = value;
      }
    }

    public PropertyDescriptor PropertyDescriptor
    {
      get
      {
        return this.m_propertyDescriptor;
      }
      set
      {
        this.m_propertyDescriptor = value;
      }
    }

    public ColumnInfoAttribute()
    {
    }

    public ColumnInfoAttribute(int ordinal, bool isVisible, Type columnType, string name, string displayName)
    {
      this.m_ordinal = ordinal;
      this.m_index = ordinal;
      this.m_isVisible = isVisible;
      this.m_columnType = columnType;
      this.m_name = name;
      this.m_displayName = displayName;
    }

    public ColumnInfoAttribute(int ordinal, bool isVisible, Type columnType, string name, string displayName, string formatString, int displayWidth)
    {
      this.m_ordinal = ordinal;
      this.m_index = ordinal;
      this.m_isVisible = isVisible;
      this.m_columnType = columnType;
      this.m_name = name;
      this.m_displayName = displayName;
      this.m_formatString = formatString;
      this.m_defaultWidth = displayWidth;
    }

    public int CompareTo(object obj)
    {
      ColumnInfoAttribute columnInfoAttribute = obj as ColumnInfoAttribute;
      if (columnInfoAttribute == null)
        throw new ArgumentException("obj not ColumnInfoAttribute");
      int num = this.Ordinal.CompareTo(columnInfoAttribute.Ordinal);
      if (num == 0)
        num = this.Name.CompareTo(columnInfoAttribute.Name);
      return num;
    }

    public static ColumnInfoAttribute[] GetColumnInfo(Type type)
    {
      ArrayList arrayList = new ArrayList();
      foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(type))
      {
        ColumnInfoAttribute columnInfoAttribute = propertyDescriptor.Attributes[typeof (ColumnInfoAttribute)] as ColumnInfoAttribute;
        if (columnInfoAttribute != null)
        {
          if (columnInfoAttribute.Name == string.Empty)
            columnInfoAttribute.Name = propertyDescriptor.Name;
          if (columnInfoAttribute.DisplayName == string.Empty)
            columnInfoAttribute.DisplayName = columnInfoAttribute.Name;
          if (columnInfoAttribute.ColumnType == (Type) null)
            columnInfoAttribute.ColumnType = propertyDescriptor.PropertyType;
          columnInfoAttribute.PropertyDescriptor = propertyDescriptor;
          arrayList.Add((object) columnInfoAttribute);
        }
      }
      arrayList.Sort();
      for (int index = 0; index < arrayList.Count; ++index)
        ((ColumnInfoAttribute) arrayList[index]).Index = index;
      return (ColumnInfoAttribute[]) arrayList.ToArray(typeof (ColumnInfoAttribute));
    }
  }
}

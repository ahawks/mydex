// Type: Dexcom.Client.Common.ComboBoxItem
// Assembly: Dexcom.Client.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B0B1988C-106E-413B-9428-09D6C6A8ED63
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Client.Common.dll

using System;

namespace Dexcom.Client.Common
{
  [Serializable]
  public class ComboBoxItem : IComparable
  {
    private string m_strDisplayMember;
    private object m_item;

    public string DisplayMember
    {
      get
      {
        return this.m_strDisplayMember;
      }
      set
      {
        this.m_strDisplayMember = value;
      }
    }

    public object Item
    {
      get
      {
        return this.m_item;
      }
      set
      {
        this.m_item = value;
      }
    }

    public ComboBoxItem()
    {
      this.m_strDisplayMember = "";
      this.m_item = (object) null;
    }

    public ComboBoxItem(string strDisplayMember, object item)
    {
      this.m_strDisplayMember = strDisplayMember;
      this.m_item = item;
    }

    public override string ToString()
    {
      return this.DisplayMember;
    }

    public int CompareTo(object obj)
    {
      ComboBoxItem comboBoxItem = obj as ComboBoxItem;
      if (comboBoxItem != null)
        return this.DisplayMember.CompareTo(comboBoxItem.DisplayMember);
      else
        throw new ArgumentException("obj not ComboBoxItem");
    }
  }
}

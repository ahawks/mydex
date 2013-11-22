// Type: Dexcom.Client.Common.ColumnMapItem
// Assembly: Dexcom.Client.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B0B1988C-106E-413B-9428-09D6C6A8ED63
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Client.Common.dll

namespace Dexcom.Client.Common
{
  public class ColumnMapItem
  {
    public static readonly int ColumnWidthAdjustToContents = -1;
    public static readonly int ColumnWidthAdjustToHeader = -2;
    private string m_strSourceName = string.Empty;
    private string m_strDisplayName = string.Empty;
    private int m_defaultWidth = ColumnMapItem.ColumnWidthAdjustToHeader;
    private int m_currentWidth = ColumnMapItem.ColumnWidthAdjustToHeader;

    public string SourceName
    {
      get
      {
        return this.m_strSourceName;
      }
    }

    public string DisplayName
    {
      get
      {
        return this.m_strDisplayName;
      }
    }

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

    public int CurrentWidth
    {
      get
      {
        return this.m_currentWidth;
      }
      set
      {
        this.m_currentWidth = value;
      }
    }

    static ColumnMapItem()
    {
    }

    public ColumnMapItem()
    {
    }

    public ColumnMapItem(string strSourceName, string strDisplayName)
    {
      this.m_strSourceName = strSourceName;
      this.m_strDisplayName = strDisplayName;
    }

    public ColumnMapItem(string strSourceName, string strDisplayName, int defaultWidth, int currentWidth)
    {
      this.m_strSourceName = strSourceName;
      this.m_strDisplayName = strDisplayName;
      this.m_defaultWidth = defaultWidth;
      this.m_currentWidth = currentWidth;
    }
  }
}

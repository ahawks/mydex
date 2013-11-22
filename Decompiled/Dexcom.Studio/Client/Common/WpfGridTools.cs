// Type: Dexcom.Client.Common.WpfGridTools
// Assembly: Dexcom.Studio, Version=12.0.3.43, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: DEB7F911-78A1-4A44-A206-F03A1B17E3DE
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Studio.exe

using Dexcom.Common;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Xml;

namespace Dexcom.Client.Common
{
  public static class WpfGridTools
  {
    public static XObject FetchGridInfo(DataGrid grid, XmlDocument ownerDocument)
    {
      XObject xobject = new XObject("GridInfo", ownerDocument);
      xobject.SetAttribute("Name", grid.Name);
      XObject xChildObject1 = new XObject("Columns", ownerDocument);
      xChildObject1.SetAttribute("Count", grid.Columns.Count);
      xobject.AppendChild(xChildObject1);
      if (grid.Columns.Count > 0)
        xChildObject1.SetAttribute("HasColumnDisplayIndexes", grid.Columns[0].DisplayIndex != -1);
      foreach (DataGridColumn dataGridColumn in (Collection<DataGridColumn>) grid.Columns)
      {
        XObject xChildObject2 = new XObject("Column", ownerDocument);
        xChildObject1.AppendChild(xChildObject2);
        xChildObject2.SetAttribute("Index", grid.Columns.IndexOf(dataGridColumn));
        xChildObject2.SetAttribute("DisplayIndex", dataGridColumn.DisplayIndex);
      }
      return xobject;
    }

    public static void ResetColumnOrder(DataGrid grid)
    {
      List<DataGridColumn> list = new List<DataGridColumn>(grid.Columns.Count);
      foreach (DataGridColumn dataGridColumn in (Collection<DataGridColumn>) grid.Columns)
        list.Add(dataGridColumn);
      foreach (DataGridColumn dataGridColumn in list)
      {
        grid.Columns.Remove(dataGridColumn);
        dataGridColumn.DisplayIndex = list.IndexOf(dataGridColumn);
      }
      foreach (DataGridColumn dataGridColumn in list)
        grid.Columns.Add(dataGridColumn);
    }

    public static void RestoreGridInfo(DataGrid grid, XObject xWindowSettings)
    {
      XObject xobject1 = new XObject(xWindowSettings.Element.SelectSingleNode(string.Format("//GridInfo[@Name='{0}']", (object) grid.Name)) as XmlElement);
      if (!xobject1.IsNotNull() || !(xobject1.Name == grid.Name) || (!xobject1.GetXPathAttribute<bool>("Columns/@HasColumnDisplayIndexes") || xobject1.GetXPathAttribute<int>("Columns/@Count") != grid.Columns.Count))
        return;
      foreach (XmlElement element in xobject1.Element.SelectNodes("Columns/Column"))
      {
        XObject xobject2 = new XObject(element);
        grid.Columns[xobject2.GetAttribute<int>("Index")].DisplayIndex = xobject2.GetAttribute<int>("DisplayIndex");
      }
    }
  }
}

// Type: Dexcom.Client.Common.FormUtils
// Assembly: Dexcom.Client.Common, Version=4.0.0.1, Culture=neutral, PublicKeyToken=71077f6d94a459dc
// MVID: B0B1988C-106E-413B-9428-09D6C6A8ED63
// Assembly location: C:\Program Files (x86)\Dexcom\Dexcom Studio 12.0.3.43\Dexcom.Client.Common.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace Dexcom.Client.Common
{
  public class FormUtils
  {
    public static Control FindFocusedControl(Control.ControlCollection controls)
    {
      Control control1 = (Control) null;
      foreach (Control control2 in (ArrangedElementCollection) controls)
      {
        if (control2.Focused)
        {
          control1 = control2;
          break;
        }
        else
        {
          control1 = FormUtils.FindFocusedControl(control2.Controls);
          if (control1 != null)
            break;
        }
      }
      return control1;
    }

    public static void EnableVisualStylesOnControls(Control.ControlCollection controls)
    {
      if (controls == null)
        return;
      foreach (Control control in (ArrangedElementCollection) controls)
      {
        if (control is ButtonBase)
        {
          ButtonBase buttonBase = control as ButtonBase;
          if (buttonBase.FlatStyle == FlatStyle.Standard)
            buttonBase.FlatStyle = FlatStyle.System;
        }
        else if (control is Label)
        {
          Label label = control as Label;
          if (label.FlatStyle == FlatStyle.Standard)
            label.FlatStyle = FlatStyle.System;
        }
        else if (control is GroupBox)
        {
          GroupBox groupBox = control as GroupBox;
          if (groupBox.FlatStyle == FlatStyle.Standard)
            groupBox.FlatStyle = FlatStyle.System;
          FormUtils.EnableVisualStylesOnControls(control.Controls);
        }
        else
          FormUtils.EnableVisualStylesOnControls(control.Controls);
      }
    }

    public static void AddDataGridColumnStyle(DataGridTableStyle tableStyle, string strHeaderText, string strMappingName, bool isReadOnly, int width, string strFormat)
    {
      DataGridTextBoxColumn gridTextBoxColumn = new DataGridTextBoxColumn();
      gridTextBoxColumn.HeaderText = strHeaderText;
      gridTextBoxColumn.MappingName = strMappingName;
      gridTextBoxColumn.ReadOnly = isReadOnly;
      gridTextBoxColumn.Width = width;
      gridTextBoxColumn.Format = strFormat;
      tableStyle.GridColumnStyles.Add((DataGridColumnStyle) gridTextBoxColumn);
    }

    public static void AddDataGridColumnStyle(DataGridTableStyle tableStyle, string strHeaderText, string strMappingName, bool isReadOnly, int width, HorizontalAlignment alignment)
    {
      DataGridBoolColumn dataGridBoolColumn = new DataGridBoolColumn();
      dataGridBoolColumn.HeaderText = strHeaderText;
      dataGridBoolColumn.MappingName = strMappingName;
      dataGridBoolColumn.ReadOnly = isReadOnly;
      dataGridBoolColumn.Width = width;
      dataGridBoolColumn.Alignment = HorizontalAlignment.Center;
      dataGridBoolColumn.TrueValue = (object) "1";
      dataGridBoolColumn.FalseValue = (object) "0";
      tableStyle.GridColumnStyles.Add((DataGridColumnStyle) dataGridBoolColumn);
    }

    public static ArrayList GetSelectedRows(DataGrid dataGrid)
    {
      ArrayList arrayList = new ArrayList();
      if (dataGrid.DataSource != null)
      {
        CurrencyManager currencyManager = (CurrencyManager) dataGrid.FindForm().BindingContext[dataGrid.DataSource, dataGrid.DataMember];
        for (int row = 0; row < currencyManager.List.Count; ++row)
        {
          if (dataGrid.IsSelected(row))
            arrayList.Add((object) row);
        }
      }
      return arrayList;
    }

    public static ArrayList GetSelectedRows(DataGrid dataGrid, string strColumnName)
    {
      ArrayList arrayList = new ArrayList();
      if (dataGrid.DataSource != null)
      {
        CurrencyManager currencyManager = (CurrencyManager) dataGrid.FindForm().BindingContext[dataGrid.DataSource, dataGrid.DataMember];
        for (int row1 = 0; row1 < currencyManager.List.Count; ++row1)
        {
          if (dataGrid.IsSelected(row1))
          {
            DataRow row2 = ((DataRowView) currencyManager.List[row1]).Row;
            int ordinal = row2.Table.Columns[strColumnName].Ordinal;
            arrayList.Add(row2.ItemArray[ordinal]);
          }
        }
      }
      return arrayList;
    }

    public static int FindRowByUniqueId(DataGrid dataGrid, Guid uniqueId)
    {
      if (dataGrid.DataSource != null)
      {
        CurrencyManager currencyManager = (CurrencyManager) dataGrid.FindForm().BindingContext[dataGrid.DataSource, dataGrid.DataMember];
        for (int index = 0; index < currencyManager.List.Count; ++index)
        {
          DataRow row = ((DataRowView) currencyManager.List[index]).Row;
          int ordinal = row.Table.Columns["Id"].Ordinal;
          if (ordinal >= 0 && new Guid(row.ItemArray[ordinal].ToString()) == uniqueId)
            return index;
        }
      }
      return -1;
    }

    public static void AutoSizeDataGridColumns(DataGrid dataGrid)
    {
      FormUtils.AutoSizeDataGridColumns(dataGrid, dataGrid.DataMember);
    }

    public static void AutoSizeDataGridColumns(DataGrid dataGrid, string tableStyleName)
    {
      string index1 = tableStyleName;
      if ((index1 == null || index1 == string.Empty) && (dataGrid.TableStyles != null && dataGrid.TableStyles.Count > 0))
        index1 = dataGrid.TableStyles[0].MappingName;
      if (dataGrid.TableStyles == null || dataGrid.TableStyles[index1] == null)
        return;
      GridColumnStylesCollection gridColumnStyles = dataGrid.TableStyles[index1].GridColumnStyles;
      int count = gridColumnStyles.Count;
      MethodInfo method = typeof (DataGrid).GetMethod("ColAutoResize", BindingFlags.Instance | BindingFlags.NonPublic);
      for (int index2 = 0; index2 < count; ++index2)
      {
        if (gridColumnStyles[index2].Width != 0 && gridColumnStyles[index2].PropertyDescriptor != null)
          method.Invoke((object) dataGrid, new object[1]
          {
            (object) index2
          });
      }
    }

    public static List<T> GetSelectedRows<T>(DataGridView dataGridView, string columnName)
    {
      List<T> list = new List<T>();
      foreach (DataGridViewRow dataGridViewRow in (BaseCollection) dataGridView.SelectedRows)
      {
        DataRowView dataRowView = dataGridViewRow.DataBoundItem as DataRowView;
        if (dataRowView != null)
        {
          DataRow row = dataRowView.Row;
          if (row != null)
          {
            DataColumn dataColumn = row.Table.Columns[columnName];
            if (dataColumn != null)
            {
              int ordinal = dataColumn.Ordinal;
              object obj = row.ItemArray[ordinal];
              if (obj == null || obj == DBNull.Value)
                obj = System.Type.GetTypeCode(typeof (T)) != TypeCode.String ? (object) default (T) : (object) string.Empty;
              list.Add((T) obj);
            }
          }
        }
      }
      return list;
    }

    public static List<int> GetSelectedRows(DataGridView dataGridView)
    {
      List<int> list = new List<int>();
      foreach (DataGridViewRow dataGridViewRow in (BaseCollection) dataGridView.SelectedRows)
        list.Add(dataGridViewRow.Index);
      return list;
    }

    public static int FindRowByUniqueId(DataGridView dataGridView, Guid uniqueId)
    {
      int num = -1;
      if (dataGridView != null)
      {
        foreach (DataGridViewRow dataGridViewRow in (IEnumerable) dataGridView.Rows)
        {
          DataRowView dataRowView = dataGridViewRow.DataBoundItem as DataRowView;
          if (dataRowView != null)
          {
            DataRow row = dataRowView.Row;
            if (row != null)
            {
              DataColumn dataColumn = row.Table.Columns["Id"];
              if (dataColumn != null)
              {
                int ordinal = dataColumn.Ordinal;
                object obj = row.ItemArray[ordinal];
                if (obj != null && obj != DBNull.Value && (obj is Guid && uniqueId == (Guid) obj))
                {
                  num = dataGridViewRow.Index;
                  break;
                }
              }
            }
          }
        }
      }
      return num;
    }

    public static void EnsureRowDisplayed(DataGridView dataGridView, int rowIndex)
    {
      if (dataGridView == null || rowIndex < 0 || (rowIndex >= dataGridView.Rows.Count || (dataGridView.Rows.GetRowState(rowIndex) & DataGridViewElementStates.Displayed) == DataGridViewElementStates.Displayed))
        return;
      int rowCount = dataGridView.Rows.GetRowCount(DataGridViewElementStates.Displayed);
      int num = rowIndex - rowCount + 1;
      if (num < 0)
        num = 0;
      dataGridView.FirstDisplayedScrollingRowIndex = num;
    }

    public static void SelectSingleRow(DataGridView dataGridView, int rowIndex)
    {
      if (dataGridView == null)
        return;
      foreach (DataGridViewBand dataGridViewBand in (BaseCollection) dataGridView.SelectedRows)
        dataGridViewBand.Selected = false;
      if (rowIndex < 0 || rowIndex >= dataGridView.Rows.Count)
        return;
      dataGridView.Rows[rowIndex].Selected = true;
    }
  }
}

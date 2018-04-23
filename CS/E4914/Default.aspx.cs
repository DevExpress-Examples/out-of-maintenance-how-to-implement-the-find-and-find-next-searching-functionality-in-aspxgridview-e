using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using System.Collections.ObjectModel;

namespace E4914 {
    public partial class Default : System.Web.UI.Page {
        protected void dvProducts_Init(object sender, EventArgs e) {
            ASPxGridView gridView = sender as ASPxGridView;
            gridView.JSProperties["cpFindText"] = String.Empty;
            gridView.JSProperties["cpFindPositionRow"] = 0;
            gridView.JSProperties["cpFindPositionColumn"] = 0;
            gridView.JSProperties["cpFindSomehting"] = true;
            gridView.JSProperties["cpFind"] = false;
        }
        protected void dvProducts_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e) {
            ASPxGridView gridView = sender as ASPxGridView;
            string[] parameters = e.Parameters.Split('|');
            if (parameters[0] == "FIND") {
                string text = parameters[1];
                int rowIndex = int.Parse(parameters[2]);
                int colIndex = int.Parse(parameters[3]);

                Find(gridView, text, rowIndex, colIndex);
                gridView.JSProperties["cpFind"] = true;
            }
        }
        
        void Find(ASPxGridView gridView, string text, int rowIndex, int colIndex) {
            ReadOnlyCollection<GridViewDataColumn> groupedColumns = gridView.GetGroupedColumns();
            for (int row = rowIndex; row < gridView.VisibleRowCount; row++) {
                for (int col = (row == rowIndex) ? colIndex : 0; col < gridView.Columns.Count; col++) {
                    GridViewDataColumn dataColumn = gridView.Columns[col] as GridViewDataColumn;
                    if (dataColumn != null && dataColumn.Visible) {
                        object cellValue = gridView.GetRowValues(row, dataColumn.FieldName);
                        if (cellValue != null && cellValue.ToString().Contains(text)) {
                            if ((gridView.GetRowLevel(row) == groupedColumns.Count && !groupedColumns.Contains(dataColumn))  //Is data row cell
                                || (gridView.IsGroupRow(row) && gridView.GetRowLevel(row) == dataColumn.GroupIndex)) { //Is group row of the current dataColumn
                                gridView.MakeRowVisible(gridView.GetRowValues(row, gridView.KeyFieldName));
                                gridView.JSProperties["cpFindText"] = text;
                                gridView.JSProperties["cpFindPositionRow"] = row;
                                gridView.JSProperties["cpFindPositionColumn"] = col + 1;
                                gridView.JSProperties["cpFindSomehting"] = true;
                                return;
                            }
                        }
                    }
                }
            }
            gridView.JSProperties["cpFindPositionRow"] = 0;
            gridView.JSProperties["cpFindPositionColumn"] = 0;
            gridView.JSProperties["cpFindSomehting"] = false;
        }

        protected void dvProducts_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e) {
            ASPxGridView gridView = sender as ASPxGridView;
            if (e.VisibleIndex == (int) gridView.JSProperties["cpFindPositionRow"] && e.DataColumn.Index == ((int) gridView.JSProperties["cpFindPositionColumn"]) - 1) {
                string text = (string) gridView.JSProperties["cpFindText"];
                if (!String.IsNullOrEmpty(text))
                    e.Cell.Text = e.CellValue.ToString().Replace(text, String.Format("<b class='highlight'>{0}</b>", text));
            }
        }

        protected void dvProducts_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e) {
            ASPxGridView gridView = sender as ASPxGridView;
            if (e.RowType == GridViewRowType.Group && e.VisibleIndex == (int) gridView.JSProperties["cpFindPositionRow"]) {
                string text = (string) gridView.JSProperties["cpFindText"];
                if (!String.IsNullOrEmpty(text))
                    foreach (TableCell cell in e.Row.Cells) {
                        if (!String.IsNullOrEmpty(cell.Text))
                            cell.Text = cell.Text.Replace(text, String.Format("<b class='highlight'>{0}</b>", text));
                    }
            }
        }
    }
}
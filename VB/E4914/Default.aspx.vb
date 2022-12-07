Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.Web
Imports System.Collections.ObjectModel

Namespace E4914
	Partial Public Class [Default]
		Inherits System.Web.UI.Page
		Protected Sub dvProducts_Init(ByVal sender As Object, ByVal e As EventArgs)
			Dim gridView As ASPxGridView = TryCast(sender, ASPxGridView)
			gridView.JSProperties("cpFindText") = String.Empty
			gridView.JSProperties("cpFindPositionRow") = 0
			gridView.JSProperties("cpFindPositionColumn") = 0
			gridView.JSProperties("cpFindSomehting") = True
			gridView.JSProperties("cpFind") = False
		End Sub
		Protected Sub dvProducts_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
			Dim gridView As ASPxGridView = TryCast(sender, ASPxGridView)
			Dim parameters() As String = e.Parameters.Split("|"c)
			If parameters(0) = "FIND" Then
				Dim text As String = parameters(1)
				Dim rowIndex As Integer = Integer.Parse(parameters(2))
				Dim colIndex As Integer = Integer.Parse(parameters(3))

				Find(gridView, text, rowIndex, colIndex)
				gridView.JSProperties("cpFind") = True
			End If
		End Sub

		Private Sub Find(ByVal gridView As ASPxGridView, ByVal text As String, ByVal rowIndex As Integer, ByVal colIndex As Integer)
			Dim groupedColumns As ReadOnlyCollection(Of GridViewDataColumn) = gridView.GetGroupedColumns()
			For row As Integer = rowIndex To gridView.VisibleRowCount - 1
				For col As Integer = If((row = rowIndex), colIndex, 0) To gridView.Columns.Count - 1
					Dim dataColumn As GridViewDataColumn = TryCast(gridView.Columns(col), GridViewDataColumn)
					If dataColumn IsNot Nothing AndAlso dataColumn.Visible Then
						Dim cellValue As Object = gridView.GetRowValues(row, dataColumn.FieldName)
						If cellValue IsNot Nothing AndAlso cellValue.ToString().Contains(text) Then
							If (gridView.GetRowLevel(row) = groupedColumns.Count AndAlso (Not groupedColumns.Contains(dataColumn))) OrElse (gridView.IsGroupRow(row) AndAlso gridView.GetRowLevel(row) = dataColumn.GroupIndex) Then 'Is group row of the current dataColumn
								gridView.MakeRowVisible(gridView.GetRowValues(row, gridView.KeyFieldName))
								gridView.JSProperties("cpFindText") = text
								gridView.JSProperties("cpFindPositionRow") = row
								gridView.JSProperties("cpFindPositionColumn") = col + 1
								gridView.JSProperties("cpFindSomehting") = True
								Return
							End If
						End If
					End If
				Next col
			Next row
			gridView.JSProperties("cpFindPositionRow") = 0
			gridView.JSProperties("cpFindPositionColumn") = 0
			gridView.JSProperties("cpFindSomehting") = False
		End Sub

		Protected Sub dvProducts_HtmlDataCellPrepared(ByVal sender As Object, ByVal e As ASPxGridViewTableDataCellEventArgs)
			Dim gridView As ASPxGridView = TryCast(sender, ASPxGridView)
			If e.VisibleIndex = CInt(Fix(gridView.JSProperties("cpFindPositionRow"))) AndAlso e.DataColumn.Index = (CInt(Fix(gridView.JSProperties("cpFindPositionColumn")))) - 1 Then
				Dim text As String = CStr(gridView.JSProperties("cpFindText"))
				If (Not String.IsNullOrEmpty(text)) Then
					e.Cell.Text = e.CellValue.ToString().Replace(text, String.Format("<b class='highlight'>{0}</b>", text))
				End If
			End If
		End Sub

		Protected Sub dvProducts_HtmlRowPrepared(ByVal sender As Object, ByVal e As ASPxGridViewTableRowEventArgs)
			Dim gridView As ASPxGridView = TryCast(sender, ASPxGridView)
			If e.RowType = GridViewRowType.Group AndAlso e.VisibleIndex = CInt(Fix(gridView.JSProperties("cpFindPositionRow"))) Then
				Dim text As String = CStr(gridView.JSProperties("cpFindText"))
				If (Not String.IsNullOrEmpty(text)) Then
					For Each cell As TableCell In e.Row.Cells
						If (Not String.IsNullOrEmpty(cell.Text)) Then
							cell.Text = cell.Text.Replace(text, String.Format("<b class='highlight'>{0}</b>", text))
						End If
					Next cell
				End If
			End If
		End Sub
	End Class
End Namespace
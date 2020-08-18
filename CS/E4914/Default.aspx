<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="E4914.Default" %>

<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .highlight 
        {
            background-color: Yellow;
        }
    </style>
    <script type="text/javascript">
        function FindNext(grid, text) {
            var oldText = grid.cpFindText;
            var row = (oldText == text) ? grid.cpFindPositionRow : 0;
            var col = (oldText == text) ? grid.cpFindPositionColumn : 0;
            grid.PerformCallback("FIND|" + text + "|" + row + "|" + col);
        }

        function btFind_Click() {
            FindNext(gridView, textBox.GetText());
        }

        function gridView_EndCallback(s, e) {
            if (s.cpFind) {
                if (!s.cpFindSomehting) {
                    alert("Nothing found.");
                    button.SetText("Find");
                }
                else {
                    button.SetText("Find Next");
                }
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>    
        <table>
            <tr>
                <td>
                    <dx:ASPxTextBox ID="txSearch" runat="server" ClientInstanceName="textBox" Width="100%" Text="Paul">
                    </dx:ASPxTextBox>
                </td>
                <td>
                    <dx:ASPxButton ID="btFind" runat="server" ClientInstanceName="button" Text="Find" AutoPostBack="false">
                        <ClientSideEvents Click="btFind_Click" />
                    </dx:ASPxButton>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <dx:ASPxGridView ID="dvProducts" runat="server" ClientInstanceName="gridView" 
                        AutoGenerateColumns="False" DataSourceID="dsProducts"
                        KeyFieldName="CustomerID" oncustomcallback="dvProducts_CustomCallback" 
                        onhtmldatacellprepared="dvProducts_HtmlDataCellPrepared" 
                        oninit="dvProducts_Init" onhtmlrowprepared="dvProducts_HtmlRowPrepared">
                        <ClientSideEvents EndCallback="gridView_EndCallback" />
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="CustomerID" ReadOnly="True" 
                                VisibleIndex="0">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="CompanyName" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ContactName" VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ContactTitle" VisibleIndex="3" GroupIndex="0">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Address" VisibleIndex="4">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="City" VisibleIndex="5">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Country" VisibleIndex="6">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <Settings ShowGroupPanel="true" />
                        <SettingsBehavior AutoExpandAllGroups="True" />
                    </dx:ASPxGridView>
                    <asp:AccessDataSource ID="dsProducts" runat="server" DataFile="~/App_Data/nwind.mdb"                        
                        
                        SelectCommand="SELECT [CustomerID], [CompanyName], [ContactName], [ContactTitle], [Address], [City], [Country] FROM [Customers]">
                    </asp:AccessDataSource>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

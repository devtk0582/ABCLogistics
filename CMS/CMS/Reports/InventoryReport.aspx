<%@ Page Title="Inventory Reports" Language="C#" MasterPageFile="~/MasterPages/ABCLogisticsCMS.Master" AutoEventWireup="true" CodeBehind="InventoryReport.aspx.cs" Inherits="CMS.Reports.InventoryReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="upAdminView" runat="server">
        <ContentTemplate>
        <table width="100%" cellspacing="1" cellpadding="1" style="padding: 5px 10px 5px 10px;">
        <tr>
            <td align="center">
                        <div style="width: 100%; text-align: center; margin-top: 15px;">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                <ProgressTemplate>
                                    <div class="updateprogress">
                                        Please Wait...<br />
                                        <img src="../Images/progress.gif" alt="" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                    </td>
        </tr>
        <tr valign="top">
            <td align="left">
                 <asp:Label ID="lblTitle" runat="server" Text="Inventory Report" CssClass="titleStyle"></asp:Label>
            </td>
        </tr>
                <tr valign="top">
            <td>
            <asp:Label ID="lblNoProduct" Font-Bold="true" runat="server" Font-Names="Verdana"
                                   Font-Size="Small" ForeColor="Red">
                        </asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" cellpadding="0" cellspacing="0"  class="tblLabelStyle">
                <tr>
                    <td runat="server" id="SalesRepProducts" >
                            <table width="100%" cellpadding="5" cellspacing="0">
                            <tr>
                                <td colspan="2">
                                     <asp:Label ID="lblErr" Visible="false" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                                <tr  valign="middle" style="background-image: url('../Images/categoryHeader.JPG'); background-repeat: repeat-x;
                                 width: 100%; padding: 10px 0px 10px 0px;">
                                    <td align="left" width="15%">
                                        <asp:Label ID="Label4" runat="server" Text="Filter By Product:"></asp:Label>
                                    </td>
                                    <td align="left" width="85%">
                                        <asp:DropDownList Width="200px" ID="ddlProducts" runat="server" 
                                            AutoPostBack="true" onselectedindexchanged="ddlProducts_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td colspan="2" width="100%" style="padding: 10px 0 5px 0; height:350px;" >
                                        <div class="divlbs" style="width:100%;">
                                            <asp:GridView ID="gvInventories" runat="server" AutoGenerateColumns="False" BorderColor="Red"
                                                BorderStyle="Dashed" BorderWidth="0px" AllowSorting="True" AllowPaging="True" PageSize="15"
                                                EnableSortingAndPagingCallbacks="True" CellPadding="3" GridLines="Both"
                                                Height="100%" Width="100%" OnSorting="gvInventories_Sorting" OnRowCommand="gvInventories_RowCommand"
                                                OnPageIndexChanging="gvInventories_PageIndexChanging" OnRowDataBound="gvInventories_RowDataBound">
                                                 <HeaderStyle CssClass="gvHeaderStyle" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Item/SKU"  SortExpression="SKU" >
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkSKU"  CommandName="ViewSKU" CommandArgument= '<%#bind("ProductID")%>' 
                                                                            Text='<%#Eval("SKU").ToString()%>'   runat="server"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="20%" Font-Size="11px" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <%--  <asp:BoundField DataField="ProductName" SortExpression="ProductName" HeaderText="Product">
                                                        <ItemStyle Width="15%" Font-Size="11px" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>--%>

                                                    <asp:BoundField DataField="ProductDesc" SortExpression="ProductDesc" HeaderText="Product Desc">
                                                    <ItemStyle Width="25%" Font-Size="11px" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>

<%--                                                    <asp:TemplateField HeaderText="Product" SortExpression="ProductName" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:LinkButton CommandName="ViewProduct" CommandArgument='<%#Eval("ProductID").ToString()%>'
                                                                ID="lnkName" Text='<%#Eval("ProductName").ToString()%>' runat="server"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="20%" />
                                                    </asp:TemplateField>--%>
                                                   
                                                    <asp:BoundField DataField="SizeName" SortExpression="SizeName" HeaderText="Size">
                                                        <ItemStyle Width="15%" Font-Size="11px" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>

                                                   

                                                    <asp:BoundField DataField="ColorName" SortExpression="ColorName" HeaderText="Color">
                                                        <ItemStyle Width="15%" Font-Size="11px" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Qty" SortExpression="Qty" HeaderText="Qty Stock">
                                                        <ItemStyle Width="15%" Font-Size="11px" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    
                                                </Columns>
                                                <PagerSettings Mode="NumericFirstLast" FirstPageText="<<" LastPageText=">>" NextPageText=">" PreviousPageText="<" />
                                        <PagerStyle CssClass="gvPagerStyle"/>
                                        <RowStyle CssClass="gvRowStyle"/>
                                        <AlternatingRowStyle CssClass="gvAltRowStyle" />
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                    </td>
                </tr>
            </table>
            </td>
        </tr>
        </table>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

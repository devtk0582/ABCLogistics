<%@ Page Title="Inventory Manager" Language="C#" MasterPageFile="~/MasterPages/ABCLogisticsCMS.Master"
    AutoEventWireup="true" CodeBehind="InventoryManager.aspx.cs" Inherits="CMS.Catalog.InventoryManager" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="upAdminView" runat="server">
        <ContentTemplate>
            <table width="100%" cellspacing="1" cellpadding="1" style="padding: 5px 10px 5px 10px; height: 500px;">
                <tr>
                    <td align="center" colspan="2">
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
                        <asp:Label ID="lblTitle" runat="server" Text="Inventory Manager" CssClass="titleStyle"></asp:Label>
                    </td>
                                                                <td align="right">
                                                               <asp:LinkButton ID="lbSwitchRole" runat="server" 
                        Text="Log In As Sales Representative" CssClass="addButtonStyle" 
                        Font-Underline="false" onclick="lbSwitchRole_Click" Visible="false"></asp:LinkButton>
                                            </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblErr" Visible="false" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr valign="top">
                    <td runat="server" id="AdminProducts" colspan="2">
                        <table width="100%" class="tblLabelStyle">
                            <tr>
                                <td>
                                    <asp:Label ID="lblNoProduct" Font-Bold="true" runat="server" Font-Names="Verdana"
                                        Font-Size="Small" ForeColor="Red">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr valign="middle" style="background-image: url('../Images/categoryHeader.JPG'); background-repeat: repeat-x;
                                 width: 100%; padding: 10px 0px 10px 0px;">
                                <td>
                                    <table width="100%">
                                        <tr valign="middle" style="padding: 10px 0px 10px 0px;">
                                            <td style="width: 70px;">
                                                <asp:Label ID="Label1" runat="server" Text="Filter by:" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td style="width: 50px;">
                                                <asp:Label ID="Label2" runat="server" Text="Category:"></asp:Label>
                                            </td>
                                            <td style="width: 50px;">
                                                <asp:DropDownList ID="ddlCategoryName" runat="server" Style="width: 150px; margin-left: 5px;
                                                    margin-right: 15px;">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 50px;">
                                                <asp:Label ID="Label3" runat="server" Text="Manufacturer:"></asp:Label>
                                            </td>
                                            <td style="width: 50px;">
                                                <asp:DropDownList ID="ddlManufacture" runat="server" Style="width: 150px; margin-left: 5px;
                                                    margin-right: 15px;">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="filterProduct_Click" CssClass="addButtonStyle">Go</asp:LinkButton>
                                            </td>

                                            <td align="right">
                                                <%--<asp:LinkButton ID="lnlBtnAddNewProduct" runat="server" CssClass="addButtonStyle"
                                                    OnClick="AddNewProduct_Click">Add New Product
                                                </asp:LinkButton>--%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td colspan="2" style="padding: 5px 0 5px 0; height: 350px;">
                                    <div class="divlbs">
                                    <asp:GridView ID="gridViewInventory" runat="server" AutoGenerateColumns="false" GridLines="Both"
                                        BorderWidth="0" AllowSorting="true" OnSorting="GridView_Sorting" AllowPaging="True" PageSize="20"
                                        Width="100%" OnRowCommand="GridView_OnRowCommand" OnPageIndexChanging="gridViewInventory_PageIndexChanging">
                                        <HeaderStyle CssClass="gvHeaderStyle" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Product" SortExpression="Title">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkBtnDesc" runat="server" CommandName="editProduct" CommandArgument='<%# bind("inv_template_id") %>'
                                                        Text='<%# bind("Title") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                                 <ItemStyle Width="30%"/> 
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mfr" SortExpression="Mfr">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblManufacturer" runat="server" Text='<%# bind("Mfr") %>'></asp:Label>
                                                </ItemTemplate>
                                                 <ItemStyle Width="20%"/> 
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item/SKU" SortExpression="SKU">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSKU" runat="server" Text='<%# bind("SKU") %>'></asp:Label>
                                                </ItemTemplate>
                                                 <ItemStyle Width="30%"/> 
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Live" SortExpression="IsLive">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLive" runat="server" Text='<%# bind("IsLive") %>'></asp:Label>
                                                </ItemTemplate>
                                                 <ItemStyle Width="10%"/>  
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Feature" SortExpression="IsFeature">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFeature" runat="server" Text='<%# bind("IsFeature") %>'></asp:Label>
                                                </ItemTemplate>
                                                 <ItemStyle Width="10%"/>  
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerSettings Mode="NumericFirstLast" FirstPageText="<<" LastPageText=">>" NextPageText=">"
                                            PreviousPageText="<" />
                                        <PagerStyle CssClass="gvPagerStyle" />
                                        <RowStyle CssClass="gvRowStyle" />
                                        <AlternatingRowStyle CssClass="gvAltRowStyle" />
                                    </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr valign="top">
                    <td runat="server" id="SalesRepProducts" colspan="2">
                        <table width="100%" cellpadding="5" cellspacing="0" class="tblLabelStyle">
                            <tr style="padding: 5px 0px 5px 0px;background:#FFF380 url('../Images/gridViewHeader.jpg') repeat-x top;">
                                <td align="left" width="15%">
                                    <asp:Label ID="Label4" runat="server" Text="Filter By Product:"></asp:Label>
                                </td>
                                <td align="left" width="85%">
                                    <asp:DropDownList Width="200px" ID="ddlProducts" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlProducts_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td colspan="2" style="padding: 5px 0 5px 0; height: 350px;">
                                    <div class="divlbs">
                                        <asp:GridView ID="gvInventories" runat="server" AutoGenerateColumns="False" BorderWidth="0px" AllowSorting="True" AllowPaging="True"
                                            EnableSortingAndPagingCallbacks="True"  GridLines="Both" Width="100%"  PageSize="15"
                                            OnSorting="gvInventories_Sorting" OnRowCommand="gvInventories_RowCommand" OnPageIndexChanging="gvInventories_PageIndexChanging"
                                            OnRowDataBound="gvInventories_RowDataBound">
                                            <HeaderStyle CssClass="gvHeaderStyle" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Item/SKU" SortExpression="ProductName" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:LinkButton CommandName="ViewProduct" CommandArgument='<%#Eval("ProductID").ToString()%>'
                                                            ID="lnkName" Text='<%#Eval("ProductName").ToString()%>' runat="server"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20%" />
                                                </asp:TemplateField>


                                                 <asp:BoundField DataField="ProductDesc" SortExpression="ProductDesc" HeaderText="Product Desc">
                                                    <ItemStyle Width="25%" Font-Size="11px" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>


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
                                                <asp:TemplateField HeaderText="Qty Req">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hfInvID" runat="server" Value='<%#Eval("InvID").ToString()%>' />
                                                        <asp:TextBox ID="txtQty" Columns="6" MaxLength="8" runat="server" Text="0"></asp:TextBox>
                                                        <asp:Label ID="lblQty" runat="server" Text='<%#Eval("Qty").ToString()%>' Visible="false"></asp:Label>
                                                         <asp:Label ID="lblSizeID" runat="server" Text='<%#Eval("Size_ID").ToString()%>' Visible="false"></asp:Label>
                                                          <asp:Label ID="lblColorID" runat="server" Text='<%#Eval("Color_Id").ToString()%>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" Font-Size="11px" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerSettings Mode="NumericFirstLast" FirstPageText="<<" LastPageText=">>" NextPageText=">"
                                                PreviousPageText="<" />
                                            <PagerStyle CssClass="gvPagerStyle" />
                                            <RowStyle CssClass="gvRowStyle" />
                                            <AlternatingRowStyle CssClass="gvAltRowStyle" />
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="right">
                                    <asp:LinkButton ID="lnkbAddtoCart" runat="server" Text="ADD TO CART" CssClass="addButtonStyle"
                                       OnClick="lnkbAddtoCart_Click"></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

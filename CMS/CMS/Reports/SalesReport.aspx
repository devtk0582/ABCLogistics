<%@ Page Title="Sales Report" Language="C#" MasterPageFile="~/MasterPages/ABCLogisticsCMS.Master" AutoEventWireup="true" CodeBehind="SalesReport.aspx.cs" Inherits="CMS.Reports.SalesReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:UpdatePanel ID="upOrderView" runat="server">
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
                    <td colspan="2" align="left">
                        <asp:Label ID="lblTitle" runat="server" Text="Sales Report" CssClass="titleStyle"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" height="15px">
                        <asp:Label ID="lblErr" Visible="false" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                
                <tr valign="top" >
                    <td>
                        <table width="100%" cellpadding="5" cellspacing="0" class="tblLabelStyle">
                            <tr valign="middle" style="background-image: url('../Images/categoryHeader.JPG'); background-repeat: repeat-x;
                                 width: 100%; padding: 10px 0px 10px 0px;">
                                <td align="left" width="10%">
                                    <asp:Label ID="lblfilter" runat="server" Text="Filter:"></asp:Label>
                                </td>
                                <td align="left" width="90%" valign="middle">
                                
                                    <asp:DropDownList Width="120px" ID="ddlSalesReport" runat="server" 
                                        onselectedindexchanged="ddlSalesReport_SelectedIndexChanged" AutoPostBack="true" >
                                     
                                        <asp:ListItem Text="By Customer" Value="Customer" Selected="True" ></asp:ListItem>
                                        <asp:ListItem Text="By Month" Value="Month"></asp:ListItem>
                                        <asp:ListItem Text="By Status" Value="Status"></asp:ListItem>
                                        <asp:ListItem Text="By Product" Value="Products"></asp:ListItem>
                                        <%--<asp:ListItem Text="Product Views" Value="Products Views"></asp:ListItem>--%>
                                       
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td colspan="2" style="padding: 10px 0 5px 0; height: 100%;">
                                    <div class="divlbs">
                                        <asp:GridView ID="gvSalesReport" runat="server"  AutoGenerateColumns="true" BorderColor="Red"
                                            BorderStyle="Dashed" BorderWidth="0px" AllowSorting="True" 
                                            AllowPaging="True"  PageSize="20" 
                                            AlternatingRowStyle-CssClass="gvAltRowStyle" 
                                            RowStyle-CssClass="gvRowStyle" HeaderStyle-CssClass="gvHeaderStyle" 
                                            EnableSortingAndPagingCallbacks="True" CellPadding="3" GridLines="Both" 
                                            Width="100%" onrowcommand="gvSalesReport_RowCommand" 
                                            onrowdatabound="gvSalesReport_RowDataBound" 
                                            onpageindexchanging="gvSalesReport_PageIndexChanging" 
                                            onsorting="gvSalesReport_Sorting">
                                            <HeaderStyle CssClass="gvHeaderStyle" />
                                            <%--<Columns>
                                                <asp:TemplateField HeaderText="Order#" SortExpression="ordernum" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderid" runat="server" Visible="false" Text='<%# bind("ordernum") %>'></asp:Label>
                                                        <asp:Label ID="lblCustomer" runat="server" Visible="false" Text='<%# bind("customer") %>'></asp:Label>
                                                        <asp:LinkButton ID="lblOrder_ID" CommandName="OrderDetail" Visible="true" CommandArgument='<%# bind("order_Id") %>' runat="server" Text='<%# bind("ordernum") %>'></asp:LinkButton>
                                                       
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="User" SortExpression="who_placed_order" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUser" runat="server" Text='<%# bind("who_placed_order") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="30%" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Status" SortExpression="status" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# bind("status") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Order Date" SortExpression="order_date" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderDate" runat="server" Text='<%# bind("order_date") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20%" />
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Total" SortExpression="Total" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotal" runat="server" Text='<%# bind("Total") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Ship Date" SortExpression="Ship_date" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblShipDate" runat="server" Text='<%# bind("Ship_date") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20%" />
                                                </asp:TemplateField>


                                            </Columns>--%>
                                            <PagerSettings Mode="NumericFirstLast" FirstPageText="<<" LastPageText=">>" NextPageText=">"
                                                PreviousPageText="<" />
                                            <PagerStyle CssClass="gvPagerStyle" />
                                           
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                          
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

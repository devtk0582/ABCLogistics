<%@ Page Title="Orders" Language="C#" MasterPageFile="~/MasterPages/ABCLogisticsCMS.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="CMS.Orders.Orders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="upOrderView" runat="server">
        <ContentTemplate>
            <table width="100%" cellspacing="1" cellpadding="1" style="padding: 5px 10px 5px 10px;">
                <tr>
                    <td align="center" colspan="2">
                        <div style="width: 100%; text-align: center; margin-top: 15px;">
                          
                        </div>
                    </td>
                </tr>
                <tr valign="top">
                    <td colspan="2" align="left">
                        <asp:Label ID="lblTitle" runat="server" Text="Orders" CssClass="titleStyle"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblErr" Visible="false" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                
                <tr valign="top">
                    <td>
                        <table width="100%" cellpadding="3" cellspacing="0" class="tblLabelStyle">
                            <tr id="trSerchBar" runat="server"  valign="middle" style="background-image: url('../Images/categoryHeader.JPG'); background-repeat: repeat-x;
                                 width: 100%; padding: 10px 0px 10px 0px;">
                                <td align="left" width="15%">
                                    <asp:Label ID="lblfilc" runat="server" Text="Filter By Product:"></asp:Label>
                                </td>
                                <td align="left" width="85%" valign="middle">
                                <asp:Label ID="lblSts" runat="server" Text="Status:"></asp:Label>&nbsp;&nbsp;
                                    <asp:DropDownList Width="150px" ID="ddlStatus" runat="server">
                                        <asp:ListItem Text="Any" Value="Any" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="New" Value="New"></asp:ListItem>
                                        <asp:ListItem Text="Processing" Value="Processing"></asp:ListItem>
                                        <asp:ListItem Text="Shipped" Value="Shipped"></asp:ListItem>
                                        <asp:ListItem Text="Delivered" Value="Delivered"></asp:ListItem>
                                        <asp:ListItem Text="Cancelled" Value="Cancelled"></asp:ListItem>
                                        <%--<asp:ListItem Text="Closed" Value="Closed"></asp:ListItem>--%>
                                    </asp:DropDownList>&nbsp;&nbsp;
                                     <asp:Label ID="lblOrd" runat="server" Text="Order#:"></asp:Label>&nbsp;&nbsp;
                                     <asp:TextBox ID="txtOrderID" runat="server" Width="40px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                      <asp:LinkButton ID="lbGoSerch" runat="server" Font-Underline="false" 
                                           Text="GO"  
                                           CssClass="addButtonStyle" onclick="lbGoSerch_Click"></asp:LinkButton>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td colspan="2" style="padding: 10px 0 5px 0; height:350px;" >
                                    <div class="divlbs">
                                        <asp:GridView ID="gvOrders" runat="server"  AutoGenerateColumns="False" BorderColor="Red"
                                            BorderStyle="Dashed" BorderWidth="0px" AllowSorting="True" 
                                            AllowPaging="True"  PageSize="20"
                                            EnableSortingAndPagingCallbacks="True" CellPadding="3" GridLines="Both" Width="100%"
                                             OnSorting="gvOrders_Sorting"  EmptyDataText="No Record Found."
                                            OnPageIndexChanging="gvOrders_PageIndexChanging" 
                                            onrowcommand="gvOrders_RowCommand" onrowdatabound="gvOrders_RowDataBound" 
                                            >
                                            <HeaderStyle CssClass="gvHeaderStyle" />
                                            <Columns>
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
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

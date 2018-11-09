<%@ Page Title="Customer List - CMS Master" Language="C#" MasterPageFile="~/CMSMaster.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="CMSMaster.Home" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="rpCustomersUpdatePanel" runat="server">
        <ContentTemplate>
            <table width="100%" cellspacing="1" cellpadding="1" style="font-size: 11px; margin-top: 10px;">
                <tr>
                    <td>
                        <table width="100%">
                            <tr>
                                <td align="left" width="20%">
                                    <asp:Label ID="lblTitle" runat="server" Text="Customers" Font-Bold="True" Font-Size="12"
                                        ForeColor="White" Font-Names="Georgia"></asp:Label>
                                </td>
                                <td align="right" width="80%">
                                    <div class="divlbs" style="float: right">
                                        <asp:Label ID="Label1" runat="server" Text="Search Customer: "></asp:Label>
                                        <asp:TextBox ID="txtSearchCustomer" runat="server" Text="" OnTextChanged="txtSearchCustomer_TextChanged"
                                            AutoPostBack="true" CssClass="input_text"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="txtSearchCustomer_AutoCompleteExtender" runat="server"
                                            Enabled="True" ServicePath="~/AutoCompleteService.asmx" ServiceMethod="GetCustomers"
                                            MinimumPrefixLength="1" TargetControlID="txtSearchCustomer">
                                        </cc1:AutoCompleteExtender>
                                        <%--                                        <cc1:textboxwatermarkextender id="txtWESearch" runat="server" targetcontrolid="txtSearchCustomer"
                                            watermarktext="Search Here">
                        </cc1:textboxwatermarkextender>--%>
                                        &nbsp;&nbsp;
                                        <asp:LinkButton ID="lbShowAll" runat="server" ForeColor="White" BackColor="#668FCC"
                                            Text="Show All" CssClass="lbStyle" OnClick="lbShowAll_Click"></asp:LinkButton>
<%--                                        &nbsp; &nbsp; &nbsp;
                                        <asp:LinkButton CssClass="lbStyle" ID="lbAddCustomer" runat="server" Text="Add Customer" BackColor="#668FCC"
                                            ForeColor="White" OnClick="lbAddCustomer_Click"></asp:LinkButton>--%>
                                    </div>
                                </td>
                            </tr>
                                                        <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblErrUser" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div class="divlbs" style="text-align: left; font-size: 11px">
                                        <asp:Label ID="lblGVMsg" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                                        <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="False" BorderColor="Red"
                                            BorderStyle="Dashed" BorderWidth="0px" AllowSorting="true" AllowPaging="true" 
                                            EnableSortingAndPagingCallbacks="true" CellPadding="3" GridLines="Horizontal"
                                            Height="100%" Width="100%" OnSorting="gvCustomers_Sorting" OnRowCommand="gvCustomers_RowCommand"
                                            OnPageIndexChanging="gvCustomers_PageIndexChanging" OnRowCreated="gvCustomers_RowCreated">
                                            <HeaderStyle CssClass="pageDrkTD" BackColor="#6690C0" ForeColor="white" Height="25px"
                                                HorizontalAlign="Left" VerticalAlign="Bottom" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Customer Name" SortExpression="CustName" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hfDBName" runat="server" Value='<%#Eval("Databasename").ToString() %>' />
                                                        <asp:LinkButton CommandName="ShowDetails" CommandArgument='<%#Eval("GteDatabases_Id").ToString()%>'
                                                            ID="lnkCustName" Text='<%#Eval("CustName").ToString()%>' runat="server"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20%" />
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderStyle-HorizontalAlign="Left" DataField="Address1" ItemStyle-Width="23%"
                                                    HeaderText="Address" SortExpression="Address1" />
                                                <asp:BoundField HeaderStyle-HorizontalAlign="Left" DataField="City" ItemStyle-Width="10%"
                                                    HeaderText="City" SortExpression="City" />
                                                <asp:BoundField HeaderStyle-HorizontalAlign="Left" DataField="State" ItemStyle-Width="10%"
                                                    HeaderText="State" SortExpression="State" />
                                                <asp:BoundField HeaderStyle-HorizontalAlign="Left" DataField="ZIP" ItemStyle-Width="10%"
                                                    HeaderText="Zip" SortExpression="ZIP" />
                                                <asp:BoundField HeaderStyle-HorizontalAlign="Left" DataField="CountryName" ItemStyle-Width="20%"
                                                    SortExpression="CountryName" HeaderText="Country" />
                                            </Columns>
                                            <RowStyle CssClass="pageLitTD" />
                                            <AlternatingRowStyle CssClass="pageLtrTD" />
                                            <PagerSettings Mode="NumericFirstLast" FirstPageImageUrl="images/paging-first.gif"
                                                LastPageImageUrl="images/paging-last.gif" />
                                            <PagerStyle BackColor="LightBlue" Font-Names="Verdana" ForeColor="#660066" HorizontalAlign="Center"
                                                VerticalAlign="Bottom" Font-Size="Medium" />
                                            <SortedAscendingHeaderStyle CssClass="GVSortingAsc" />
                                            <SortedDescendingHeaderStyle CssClass="GVSortingDesc" />
                                        </asp:GridView>
                                </td>
                            </tr>

                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

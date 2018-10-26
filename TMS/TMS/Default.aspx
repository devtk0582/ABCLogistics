<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="TMS.Default" %>
<%@ Register Src="~/UserControls/ContentManager.ascx" TagName="ContentManager" TagPrefix="UC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
<div style="margin: 120px 5px 5px 5px;">
  <div style="background-color: #3580B7; height: 15px; margin-bottom: 5px;">
    </div>
    <div>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="75%">
                    <table width="100%">
                        <tr>
                            <td>
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="content_manager_container" valign="top">
                                            <UC:ContentManager runat="server" id="cmLeft"></UC:ContentManager>
                                        </td>
                                        <td class="content_manager_container" valign="top">
                                            <UC:ContentManager runat="server" id="cmRight"></UC:ContentManager>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="top">
                                <table width="100%" cellpadding="2" cellspacing="2">
                                    <tr>
                                        <td valign="top" align="center" class="dashboard_container">
                                            <asp:Repeater ID="rptCustomerService" runat="server">
                                                <HeaderTemplate>
                                                    <table width="100%" cellpadding="2" cellspacing="2">
                                                        <tr>
                                                            <td align="center" colspan="2" class="dashboard_header">
                                                                Customer Service
                                                            </td>
                                                        </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td align="left" width="70%">
                                                            <%# Eval("Item").ToString() %>
                                                        </td>
                                                        <td align="right" width="30%">
                                                            <%# Eval("Value").ToString() %>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </td>
                                        <td valign="top" align="center" class="dashboard_container" >
                                            <asp:Repeater ID="rptOperationalQuotes" runat="server">
                                                <HeaderTemplate>
                                                    <table width="100%" cellpadding="2" cellspacing="2">
                                                        <tr>
                                                            <td align="center" colspan="2" class="dashboard_header">
                                                                Operational Quotes
                                                            </td>
                                                        </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td align="left" width="70%">
                                                            <%# Eval("Item").ToString() %>
                                                        </td>
                                                        <td align="right" width="30%">
                                                            <%# Eval("Value").ToString() %>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </td>
                                        <td valign="top" align="center" class="dashboard_container">
                                            <asp:Repeater ID="rptInboundShipments" runat="server">
                                                <HeaderTemplate>
                                                    <table width="100%" cellpadding="2" cellspacing="2">
                                                        <tr>
                                                            <td align="center" colspan="2" class="dashboard_header">
                                                                Inbound Shipments
                                                            </td>
                                                        </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td align="left" width="70%">
                                                            <%# Eval("Item").ToString() %>
                                                        </td>
                                                        <td align="right" width="30%">
                                                            <%# Eval("Value").ToString() %>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top" width="25%">
                    <asp:Repeater ID="rptSideMenus" runat="server">
                        <HeaderTemplate>
                            <table cellpadding="0" cellspacing="0" class="side_menus_table">
                                <tr class="side_menus_table_gray_bg">
                                    <td align="left" valign="middle">
                                        <div style="display: inline-block; margin-right: 10px">
                                            <asp:TextBox ID="txtReviewBy" runat="server"></asp:TextBox>
                                        </div>
                                        <div style="display: inline-block;">
                                            Review By
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="separator">
                                    </td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="side_menus_table_gray_bg">
                                <td align="left" valign="middle">
                                    <div style="display: inline-block;margin-right: 10px">
                                        <asp:ImageButton ID="ibtnSideMenu" runat="server" ImageUrl='<%# Eval("MenuImage").ToString() %>' PostBackUrl='<%# Eval("MenuUrl").ToString() %>' Height="25" Width="25" />
                                    </div>
                                    <div style="display: inline-block;">
                                        <asp:HyperLink ID="hlSideMenu" runat="server" NavigateUrl='<%# Eval("MenuUrl").ToString() %>' Text='<%# Eval("MenuName").ToString() %>'></asp:HyperLink>
                                    </div>
                                    </td>
                                <%--<td valign="middle" align="right" width="70%" style="padding-right: 5px;">
                                    
                                </td>--%>
                            </tr>
                            <tr>
                                <td class="separator">
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </td>
            </tr>
        </table>
    </div>
</div>
  
</asp:Content>

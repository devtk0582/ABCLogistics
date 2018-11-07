﻿<%@ Page Title="Shipping Methods" Language="C#" MasterPageFile="~/MasterPages/ABCLogisticsCMS.Master" AutoEventWireup="true" CodeBehind="ShippingMethods.aspx.cs" Inherits="CMS.Settings.ShippingMethods" %>
<%@ Register TagPrefix="UC" TagName="AddShippingMethod" Src="~/UserControls/AddShippingMethod.ascx" %>
<%@ Register TagPrefix="UC" TagName="EditShippingMethod" Src="~/UserControls/EditShippingMethod.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="upShippingMethods" runat="server">
        <ContentTemplate>
            <table width="100%" cellspacing="1" cellpadding="1" >
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
                <tr>
                    <td align="left">
                        <asp:Label ID="lblTitle" runat="server" Text="Shipping Methods" CssClass="titleStyle"></asp:Label>
                    </td>
                    <td align="right">
                    <div class="divlbs">
                        <asp:LinkButton ID="lbAddShippingMethod" runat="server" Text="Add Shipping Method" 
                           CssClass="addButtonStyle"  onclick="lbAddShippingMethod_Click"></asp:LinkButton>
                    </div>
                    </td>
                </tr>
                <tr valign="top">
                                 <td colspan="2" style="height: 350px; padding-top: 20px;">
                                <asp:HiddenField ID="hfCurrentID" runat="server" />
                                <div class="divlbs">
                                    <asp:GridView ID="gvShippingMethods" runat="server" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True"
                                        EnableSortingAndPagingCallbacks="True" CellPadding="3" GridLines="Both" BorderWidth="0"
                                        Width="100%" OnSorting="gvShippingMethods_Sorting" OnRowCommand="gvShippingMethods_RowCommand"
                                        OnPageIndexChanging="gvShippingMethods_PageIndexChanging" OnRowDataBound="gvShippingMethods_RowDataBound">
                                        <HeaderStyle CssClass="gvHeaderStyle" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Name" SortExpression="ShipMethod_Name" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton CommandName="EditMethod" CommandArgument='<%#Eval("ShipMethod_ID").ToString()%>'
                                                        ID="lnkName" Text='<%#Eval("ShipMethod_Name").ToString()%>' runat="server"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="40%" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="License" SortExpression="License"
                                                 HeaderText="License">
                                                <ItemStyle Width="15%" Font-Size="11px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AccountNumber" SortExpression="AccountNumber"
                                                 HeaderText="Account Number">
                                                <ItemStyle Width="20%" Font-Size="11px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ShipMethod_Cost" SortExpression="ShipMethod_Cost"
                                                 HeaderText="Cost">
                                                <ItemStyle Width="15%" Font-Size="11px" />
                                                
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Status" SortExpression="Is_Active">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hfStatus" runat="server" Value='<%#Eval("Is_Active").ToString()%>' />
                                                    <asp:LinkButton CommandName="ToggleMethod" CommandArgument='<%#Eval("ShipMethod_ID").ToString()%>'
                                                        ID="lnkToggle" Text='Delete' runat="server"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="10%"/>
                                               
                                            </asp:TemplateField>
                                        </Columns>
                                        
                                        <PagerSettings Mode="NumericFirstLast" FirstPageText="<<" LastPageText=">>" NextPageText=">" PreviousPageText="<" />
                                        <PagerStyle CssClass="gvPagerStyle"/>
                                        <RowStyle CssClass="gvRowStyle"/>
                                        <AlternatingRowStyle CssClass="gvAltRowStyle" />
                                    </asp:GridView>
                                   
                                    </div>
                                </td>

                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblErr" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
            <UC:AddShippingMethod ID="ucAddShippingMethod" runat="server" OnSaveButtonClicked="ucAddShippingMethod_SaveButtonClicked"></UC:AddShippingMethod>
            <UC:EditShippingMethod ID="ucEditShippingMethod" runat="server" OnSaveButtonClicked="ucEditShippingMethod_SaveButtonClicked"></UC:EditShippingMethod>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<%@ Page Title="Shipping Classes" Language="C#" MasterPageFile="~/MasterPages/ABCLogisticsCMS.Master" AutoEventWireup="true" CodeBehind="ShippingClasses.aspx.cs" Inherits="CMS.Settings.ShippingClasses" %>
<%@ Register TagPrefix="UC" TagName="AddShippingClass" Src="~/UserControls/AddShippingClass.ascx" %>
<%@ Register TagPrefix="UC" TagName="EditShippingClass" Src="~/UserControls/EditShippingClass.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="upShippingClasses" runat="server">
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
                        <asp:Label ID="lblTitle" runat="server" Text="Shipping Classes" CssClass="titleStyle"></asp:Label>
                    </td>
                    <td align="right">
                    <div class="divlbs">
                        <asp:LinkButton ID="lbAddShippingClass" runat="server" Text="Add Shipping Class" CssClass="addButtonStyle" onclick="lbAddShippingClass_Click"></asp:LinkButton>
                    </div>
                    </td>
                </tr>
                <tr valign="top">

                                <td colspan="2" style="height: 350px; padding-top: 20px;">
                                <asp:HiddenField ID="hfCurrentID" runat="server" />
                                <div class="divlbs">
                                    <asp:GridView ID="gvShippingClasss" runat="server" AutoGenerateColumns="False" BorderWidth="0px" AllowSorting="True" AllowPaging="True"
                                        EnableSortingAndPagingCallbacks="True" CellPadding="3" GridLines="Both"
                                        Width="100%" OnSorting="gvShippingClasss_Sorting" OnRowCommand="gvShippingClasss_RowCommand"
                                        OnPageIndexChanging="gvShippingClasss_PageIndexChanging" OnRowDataBound="gvShippingClasss_RowDataBound">
                                       <HeaderStyle CssClass="gvHeaderStyle" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Name" SortExpression="ShipClass">
                                                <ItemTemplate>
                                                    <asp:LinkButton CommandName="EditShippingClass" CommandArgument='<%#Eval("ShippingClassID").ToString()%>'
                                                        ID="lnkName" Text='<%#Eval("ShipClass").ToString()%>' runat="server"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="80%" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" SortExpression="Is_Active">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hfStatus" runat="server" Value='<%#Eval("Is_Active").ToString()%>' />
                                                    <asp:LinkButton CommandName="ToggleShippingClass" CommandArgument='<%#Eval("ShippingClassID").ToString()%>'
                                                        ID="lnkToggle" Text='Delete' runat="server"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="20%"/>
                                                <HeaderStyle HorizontalAlign="Left" />
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
            <UC:AddShippingClass ID="ucAddShippingClass" runat="server" OnSaveButtonClicked="ucAddShippingClass_SaveButtonClicked"></UC:AddShippingClass>
            <UC:EditShippingClass ID="ucEditShippingClass" runat="server" OnSaveButtonClicked="ucEditShippingClass_SaveButtonClicked"></UC:EditShippingClass>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


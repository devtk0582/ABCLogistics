<%@ Page Title="Billing Codes" Language="C#" MasterPageFile="~/MasterPages/ABCLogisticsCMS.Master" AutoEventWireup="true" CodeBehind="BillingCodes.aspx.cs" Inherits="CMS.Settings.BillingCodes" %>
<%@ Register TagPrefix="UC" TagName="AddBillingCode" Src="~/UserControls/AddBillingCode.ascx" %>
<%@ Register TagPrefix="UC" TagName="EditBillingCode" Src="~/UserControls/EditBillingCode.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="upBillingCodes" runat="server">
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
                    <td align="left" >
                        <asp:Label ID="lblTitle" runat="server" Text="Billing Codes" CssClass="titleStyle"></asp:Label>
                    </td>
                     <td align="right">
                    <div class="divlbs">
                        <asp:LinkButton ID="lbAddBillingCode" runat="server" Text="Add Billing Code" 
                             CssClass="addButtonStyle" onclick="lbAddBillingCode_Click"
                         ></asp:LinkButton>
                    </div>
                    </td>
                </tr>
                 <tr>
                    <td colspan="2">
                        <asp:Label ID="lblErr" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr valign="top">
                                <td colspan="2" style="height: 350px; padding-top: 20px;">
                                <asp:HiddenField ID="hfCurrentID" runat="server" />
                                <div class="divlbs">
                                    <asp:GridView ID="gvBillingCodes" runat="server" AutoGenerateColumns="False" BorderWidth="0px" AllowSorting="True" AllowPaging="True"
                                        EnableSortingAndPagingCallbacks="True" CellPadding="3" GridLines="Both"
                                        Width="100%" OnSorting="gvBillingCodes_Sorting" OnRowCommand="gvBillingCodes_RowCommand"
                                        OnPageIndexChanging="gvBillingCodes_PageIndexChanging" OnRowDataBound="gvBillingCodes_RowDataBound">
                                        <HeaderStyle CssClass="gvHeaderStyle" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Name" SortExpression="BillingCode_Name">
                                                <ItemTemplate>
                                                    <asp:LinkButton CommandName="EditBillingCode" CommandArgument='<%#Eval("BillingCode_ID").ToString()%>'
                                                        ID="lnkName" Text='<%#Eval("BillingCode_Name").ToString()%>' runat="server"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="20%" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="BillingCode_Desc"
                                                 HeaderText="Description" SortExpression="BillingCode_Desc">
                                                <ItemStyle Width="60%"/>
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Status" SortExpression="Is_Active">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hfStatus" runat="server" Value='<%#Eval("Is_Active").ToString()%>' />
                                                    <asp:LinkButton CommandName="ToggleBillingCode" CommandArgument='<%#Eval("BillingCode_ID").ToString()%>'
                                                        ID="lnkToggle" Text='Delete' runat="server"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="20%" />
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

            </table>
            <UC:AddBillingCode ID="ucAddBillingCode" runat="server" OnSaveButtonClicked="ucAddBillingCode_SaveButtonClicked"></UC:AddBillingCode>
            <UC:EditBillingCode ID="ucEditBillingCode" runat="server" OnSaveButtonClicked="ucEditBillingCode_SaveButtonClicked"></UC:EditBillingCode>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

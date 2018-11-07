<%@ Page Title="Packagings" Language="C#" MasterPageFile="~/MasterPages/ABCLogisticsCMS.Master" AutoEventWireup="true" CodeBehind="Packagings.aspx.cs" Inherits="CMS.Settings.Packagings" %>
<%@ Register TagPrefix="UC" TagName="AddPackaging" Src="~/UserControls/AddPackaging.ascx" %>
<%@ Register TagPrefix="UC" TagName="EditPackaging" Src="~/UserControls/EditPackaging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="upPackagings" runat="server">
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
                        <asp:Label ID="lblTitle" runat="server" Text="Packagings" CssClass="titleStyle"></asp:Label>
                    </td>
                    <td align="right">
                    <div class="divlbs">
                        <asp:LinkButton ID="lbAddPackaging" runat="server" Text="Add Packaging" 
                            CssClass="addButtonStyle" onclick="lbAddPackaging_Click"></asp:LinkButton>
                    </div>
                    </td>
                </tr>
                <tr valign="top">

                                <td colspan="2" style="height: 350px; padding-top: 20px;">
                                <asp:HiddenField ID="hfCurrentID" runat="server" />
                                <div class="divlbs">
                                    <asp:GridView ID="gvPackagings" runat="server" AutoGenerateColumns="False" BorderWidth="0px" AllowSorting="True" AllowPaging="True"
                                        EnableSortingAndPagingCallbacks="True" CellPadding="3" GridLines="Both"
                                        Width="100%" OnSorting="gvPackagings_Sorting" OnRowCommand="gvPackagings_RowCommand"
                                        OnPageIndexChanging="gvPackagings_PageIndexChanging" OnRowDataBound="gvPackagings_RowDataBound">
                                       <HeaderStyle CssClass="gvHeaderStyle" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Name" SortExpression="Package_Name">
                                                <ItemTemplate>
                                                    <asp:LinkButton CommandName="EditPackaging" CommandArgument='<%#Eval("Packaging_ID").ToString()%>'
                                                        ID="lnkName" Text='<%#Eval("Package_Name").ToString()%>' runat="server"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="80%" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" SortExpression="Is_Active">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hfStatus" runat="server" Value='<%#Eval("Is_Active").ToString()%>' />
                                                    <asp:LinkButton CommandName="TogglePackaging" CommandArgument='<%#Eval("Packaging_ID").ToString()%>'
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
            <UC:AddPackaging ID="ucAddPackaging" runat="server" OnSaveButtonClicked="ucAddPackaging_SaveButtonClicked"></UC:AddPackaging>
            <UC:EditPackaging ID="ucEditPackaging" runat="server" OnSaveButtonClicked="ucEditPackaging_SaveButtonClicked"></UC:EditPackaging>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

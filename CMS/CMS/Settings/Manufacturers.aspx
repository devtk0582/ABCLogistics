<%@ Page Title="Manufacturers" Language="C#" MasterPageFile="~/MasterPages/ABCLogisticsCMS.Master" AutoEventWireup="true" CodeBehind="Manufacturers.aspx.cs" Inherits="CMS.Catalog.Manufacturers" %>
<%@ Register TagPrefix="UC" TagName="AddManufacturer" Src="~/UserControls/AddManufacturer.ascx" %>
<%@ Register TagPrefix="UC" TagName="EditManufacturer" Src="~/UserControls/EditManufacturer.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="upManufacturers" runat="server">
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
                        <asp:Label ID="lblTitle" runat="server" Text="Manufacturers" CssClass="titleStyle"></asp:Label>
                    </td>
                    <td align="right">
                    <div class="divlbs">
                        <asp:LinkButton ID="lbAddManufacturer" runat="server" Text="Add Manufacturer" 
                          CssClass="addButtonStyle"  onclick="lbAddManufacturer_Click"></asp:LinkButton>
                    </div>
                    </td>
                </tr>
                <tr valign="top">

                                <td colspan="2" style="height: 350px; padding-top: 20px;">
                                <asp:HiddenField ID="hfCurrentID" runat="server" />
                                <div class="divlbs">
                                    <asp:GridView ID="gvManufacturers" runat="server" AutoGenerateColumns="False" BorderWidth="0px" AllowSorting="True" AllowPaging="True"
                                        EnableSortingAndPagingCallbacks="True" CellPadding="3" GridLines="Both"
                                        Width="100%" OnSorting="gvManufacturers_Sorting" OnRowCommand="gvManufacturers_RowCommand"
                                        OnPageIndexChanging="gvManufacturers_PageIndexChanging" OnRowDataBound="gvManufacturers_RowDataBound">
                                       <HeaderStyle CssClass="gvHeaderStyle" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Name" SortExpression="Manufactures_Desc">
                                                <ItemTemplate>
                                                    <asp:LinkButton CommandName="EditManufacturer" CommandArgument='<%#Eval("Manufactures_Id").ToString()%>'
                                                        ID="lnkName" Text='<%#Eval("Manufactures_Desc").ToString()%>' runat="server"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="80%" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" SortExpression="Is_Active">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hfStatus" runat="server" Value='<%#Eval("Is_Active").ToString()%>' />
                                                    <asp:LinkButton CommandName="ToggleManufacturer" CommandArgument='<%#Eval("Manufactures_Id").ToString()%>'
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
            <UC:AddManufacturer ID="ucAddManufacturer" runat="server" OnSaveButtonClicked="ucAddManufacturer_SaveButtonClicked"></UC:AddManufacturer>
            <UC:EditManufacturer ID="ucEditManufacturer" runat="server" OnSaveButtonClicked="ucEditManufacturer_SaveButtonClicked"></UC:EditManufacturer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

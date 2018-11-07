<%@ Page Title="Sizes" Language="C#" MasterPageFile="~/MasterPages/ABCLogisticsCMS.Master"
    AutoEventWireup="true" CodeBehind="Sizes.aspx.cs" Inherits="CMS.Catalog.Sizes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%" cellspacing="1" cellpadding="1">
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
                        <asp:Label ID="lblSizes" CssClass="titleStyle" runat="server" Text="Sizes"></asp:Label>
                    </td>
                    <td align="right">
                        <asp:LinkButton ID="lnlBtnAddNewSize" runat="server" CssClass="addButtonStyle" OnClick="addNewSize_Click">Add New Size</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblErr" Visible="false" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr valign="top">
                    <td colspan="2">
                        <asp:Label ID="lblErrProduct" CssClass="errMessage" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr valign="top">
                    <td colspan="2" style="height: 350px; padding-top: 20px;">
                    <asp:HiddenField ID="hfCurrentSizeID" runat="server" />
                    <div class="divlbs">
                        <asp:GridView ID="gridViewSizes" runat="server" AutoGenerateColumns="false" GridLines="Both"
                           BorderWidth="0"  AllowSorting="true" OnSorting="GridView_Sorting" Width="100%" DataKeyNames="Size_ID"
                            OnRowCommand="GridView_OnRowCommand" AllowPaging="true" OnPageIndexChanging="gridViewSizes_OnPageIndexChanging"
                            PageSize="15" OnRowDataBound="GridView_OnRowDataBound" EnableSortingAndPagingCallbacks="True">
                            <HeaderStyle CssClass="gvHeaderStyle" />
                            <Columns>
                                <asp:TemplateField HeaderText="Code" SortExpression="Size_Code">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkBtnSizeCode" runat="server" CommandName="Popup" CommandArgument='<%# bind("Size_ID") %>'
                                            Text='<%# bind("Size_Code") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Width="45%"/>  
                                </asp:TemplateField>                        
                                <asp:TemplateField HeaderText="Name" SortExpression="Size_Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsizeName" runat="server" Text='<%# bind("Size_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="45%"/>  
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" SortExpression="Is_Active">
                                    <ItemTemplate>                                      
                                        <asp:HiddenField ID="hfStatus" runat="server"  Value='<%#Eval("Is_Active").ToString()%>'/>
                                        <asp:LinkButton ID="lnkToggle" CommandName="ToggleSize" CommandArgument='<%#Eval("Size_ID").ToString()%>'
                                                        runat="server" Text='Delete'></asp:LinkButton>
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
                <tr>
                    <td>
                        <input id="dummy2" type="button" style="display: none" runat="server" />
                        <asp:ModalPopupExtender ID="ModlPopup2" runat="server" PopupControlID="pnl2ModlPopup"
                            TargetControlID="dummy2" BackgroundCssClass="popupBG " DropShadow="true">
                        </asp:ModalPopupExtender>
                        <asp:Panel ID="pnl2ModlPopup" runat="server">
                            <table class="PopupFormBG" width="60%">
                                <tr style="height: 30px;">
                                    <td class="PopupFormTitleStyle" colspan="2" align="left">
                                        <asp:Label ID="lblNewSize" runat="server" Text="Add New Size"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" >
                                        <asp:Label ID="lblErrMessage" CssClass="errMessage" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;" width="40%" align="left">
                                        Code:
                                    </td>
                                    <td style="font-weight: bold;" width="60%" align="left">
                                        <asp:TextBox ID="txtbxSizeCode" runat="server" MaxLength="7" Columns="7"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;" width="40%" align="left">
                                        Name:
                                    </td>
                                    <td style="font-weight: bold;" width="60%" align="left">
                                        <asp:TextBox ID="txtbxSizeName" runat="server" MaxLength="20"></asp:TextBox>
                                    </td>
                                </tr>
                             <%--   <tr>
                                    <td style="font-weight: bold;" width="40%" align="left">
                                        Active:
                                    </td>
                                    <td style="font-weight: bold;" width="60%" align="left">
                                        <asp:CheckBox ID="chkbxSizeIsActive" runat="server" />
                                    </td>
                                </tr>--%>
                                <tr style="height: 50px;">
                        <td colspan="2" align="right">
                            <table width="30%">
                                <tr>
                                    <td align="center">
                                        <asp:LinkButton ID="LnkBtnInsertNewColor" runat="server" CssClass="addButtonStyle"
                                                OnClick="InsertNewSize_Click">Save
                                            </asp:LinkButton>
                                    </td>
                                    <td align="center">
                                        <asp:LinkButton ID="LinkButton3" CssClass="addButtonStyle" runat="server" OnClick="CancelInsertNewColor_Click">Cancel
                                            </asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td>
                        <input id="dummy" type="button" style="display: none" runat="server" />
                        <asp:ModalPopupExtender ID="ModlPopup" runat="server" PopupControlID="pnlModlPopup"
                            TargetControlID="dummy" BackgroundCssClass="popupBG" DropShadow="true">
                        </asp:ModalPopupExtender>
                        <asp:Panel ID="pnlModlPopup" runat="server">
                            <asp:HiddenField ID="HiddenField" runat="server" />
                            <table class="PopupFormBG" width="60%">
                                <tr style="height: 30px;">
                                    <td colspan="2" class="PopupFormTitleStyle" align="left">
                                        <asp:Label ID="lblEditSize" runat="server" Text="Update Size"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblErrMessageUpdate" runat="server" CssClass="errMessage"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;" width="40%" align="left">
                                        Code:
                                    </td>
                                    <td style="font-weight: bold;" width="60%" align="left">
                                        <asp:TextBox ID="txtSizeCode" runat="server" Text='<%# Bind("Size_Code") %>'
                                            MaxLength="7" Columns="7"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;" width="40%" align="left">
                                        Name:
                                    </td>
                                    <td style="font-weight: bold;" width="60%" align="left">
                                        <asp:TextBox ID="txtSizeName" runat="server" Text='<%# Bind("Size_Name") %>'
                                            MaxLength="20"></asp:TextBox>
                                    </td>
                                </tr>
                            <%--    <tr>
                                    <td style="font-weight: bold;" width="40%" align="left">
                                       Active:
                                    </td>
                                    <td style="font-weight: bold;" width="60%" align="left">
                                        <asp:CheckBox ID="chkBxActive" runat="server" />
                                    </td>
                                </tr>--%>
                                <tr style="height: 50px;">
                        <td colspan="2" align="right">
                            <table width="30%">
                                <tr>
                                    <td align="center">
                                                       <asp:LinkButton ID="lnkBtnUpdate" runat="server" CssClass="addButtonStyle" OnClick="UpdateSize_Click">Update</asp:LinkButton>
                                    </td>
                         <%--           <td align="center">
                                     <asp:LinkButton ID="lnkBtnDelete" CssClass="addButtonStyle" OnClick="DeleteSize_Click"
                                                runat="server">Delete</asp:LinkButton>
                                    </td>--%>
                                    <td align="center">
                                         <asp:LinkButton ID="lnkBtnCancel" runat="server" CssClass="addButtonStyle" OnClick="Cancel_Click">Cancel</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

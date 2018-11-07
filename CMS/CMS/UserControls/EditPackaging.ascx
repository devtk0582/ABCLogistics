<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditPackaging.ascx.cs" Inherits="CMS.UserControls.EditPackaging" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<input id="dummy" runat="server" style="display: none" type="button" />
<ajaxToolkit:ModalPopupExtender ID="mpePopup" runat="server" 
    BackgroundCssClass="popupBG" DropShadow="true" PopupControlID="pnlPopUp" 
    TargetControlID="dummy" />
<asp:Panel ID="pnlPopUp" runat="server" CssClass="modalPopup" ScrollBars="Auto">
    <center>
        <asp:UpdatePanel ID="upEditPackaging" runat="server" Height="350px" 
            UpdateMode="Conditional" Visible="false" Width="850px">
            <ContentTemplate>
                <asp:HiddenField ID="hfPackagingID" runat="server" />
                <table class="PopupFormBG" width="60%">
                    <tr style="height: 30px;" class="PopupFormTitleStyle">
                        <td align="left">
                            <asp:Label ID="lblTitle" runat="server" 
                                Text="Edit Packaging"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <table width="100%" class="tblLabelStyle">
                                <tr>
                                    <td align="left" colspan="2">
                                        <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="font-weight: bold;" width="50%">
                                        Packaging Name<font color="red">*</font>:
                                    </td>
                                    <td align="left" style="font-weight: bold;" width="50%">
                                        <asp:TextBox ID="txtPackagingName" runat="server" Columns="20" 
                                            CssClass="textbox" MaxLength="20" TabIndex="1"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="height: 50px;">
                        <td align="right">
                            <table width="30%">
                                <tr>
                                    <td align="center">
                                        <asp:LinkButton ID="lbSave" runat="server" Text="Update"
                                            CssClass="addButtonStyle" OnClick="lbSave_Click"></asp:LinkButton>
                                    </td>
                                    <td align="center">
                                        <asp:LinkButton ID="lbCancel" runat="server" Text="Cancel"
                                            CssClass="addButtonStyle" OnClick="lbCancel_Click"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </center>
</asp:Panel>


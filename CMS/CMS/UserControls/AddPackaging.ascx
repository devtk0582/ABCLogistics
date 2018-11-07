<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddPackaging.ascx.cs" Inherits="CMS.UserControls.AddPackaging" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<input id="dummy" type="button" style="display: none" runat="server" />
<ajaxToolkit:ModalPopupExtender runat="server" ID="mpePopup" TargetControlID="dummy"
    PopupControlID="pnlPopUp" BackgroundCssClass="popupBG" DropShadow="true" />
<asp:Panel ID="pnlPopUp" runat="server" CssClass="modalPopup" ScrollBars="Auto">
    <center>
        <asp:UpdatePanel ID="upAddPackaging" runat="server" UpdateMode="Conditional" Visible="false"
            Width="850px" Height="350px">
            <ContentTemplate>
                <table class="PopupFormBG" width="60%">
                    <tr style="height: 30px;" class="PopupFormTitleStyle">
                        <td align="left">
                            <asp:Label ID="lblTitle" runat="server" Text="Add Packaging"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <table width="100%" class="tblLabelStyle">
                                <tr>
                                    <td align="left"  colspan="2">
                                        <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;" width="50%" align="left">
                                        Packaging Name<font color="red">*</font>:
                                    </td>
                                    <td style="font-weight: bold;" width="50%" align="left">
                                        <asp:TextBox ID="txtPackagingName" TabIndex="1" runat="server"
                                            CssClass="textbox" MaxLength="20" Columns="20"></asp:TextBox>
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
                                        <asp:LinkButton ID="lbSave" runat="server" Text="Add"
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

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditShippingMethod.ascx.cs" Inherits="CMS.UserControls.EditShippingMethod" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<input id="dummy" type="button" style="display: none" runat="server" />
<ajaxToolkit:ModalPopupExtender runat="server" ID="mpePopup" TargetControlID="dummy"
    PopupControlID="pnlPopUp" BackgroundCssClass="popupBG" DropShadow="true" />
<asp:Panel ID="pnlPopUp" runat="server" CssClass="modalPopup" ScrollBars="Auto">
    <center>
        <asp:UpdatePanel ID="upEditShipMethod" runat="server" UpdateMode="Conditional" Visible="false"
            Width="850px" Height="350px">
            <ContentTemplate>
                <asp:HiddenField ID="hfMethodID" runat="server" /> 
                <table class="PopupFormBG" width="60%">
                    <tr style="height: 30px;" class="PopupFormTitleStyle">
                        <td align="left">
                            <asp:Label ID="lblTitle" runat="server" Text="Edit Shipping Method" ></asp:Label>
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
                                    <td style="font-weight: bold;" width="60%" align="left">
                                        Shipping Method Name<font color="red">*</font>:
                                    </td>
                                    <td style="font-weight: bold;" width="40%" align="left">
                                        <asp:TextBox Columns="20" ID="txtShippingMethodName" TabIndex="1" runat="server"
                                            CssClass="textbox" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;" align="left">
                                        Cost:
                                    </td>
                                    <td style="font-weight: bold;" align="left">
                                        <asp:TextBox ID="txtCost" TabIndex="2" runat="server" CssClass="textbox" Columns="10"></asp:TextBox>
                      <%--                  <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Invalid Cost" EnableClientScript="false"
                                            ControlToValidate="txtCost" Type="Double" ForeColor="Red"></asp:CompareValidator>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;" align="left">
                                        License Number:
                                    </td>
                                    <td style="font-weight: bold;" align="left">
                                        <asp:TextBox ID="txtLicense" TabIndex="3" runat="server" CssClass="textbox" Columns="20" MaxLength="20"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;" align="left">
                                        User Name:
                                    </td>
                                    <td style="font-weight: bold;" align="left">
                                        <asp:TextBox ID="txtUserName" TabIndex="4" runat="server" CssClass="textbox" Columns="20" MaxLength="20"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;" align="left">
                                        Password:
                                    </td>
                                    <td style="font-weight: bold;" align="left">
                                        <asp:TextBox ID="txtPassword" TabIndex="5" runat="server" CssClass="textbox" Columns="20" MaxLength="20"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;" align="left">
                                        Account Number:
                                    </td>
                                    <td style="font-weight: bold;" align="left">
                                        <asp:TextBox ID="txtAccountNumber" TabIndex="6" runat="server" CssClass="textbox" Columns="20" MaxLength="20"></asp:TextBox>
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

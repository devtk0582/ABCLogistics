<%@ Page Title="Customer Details - CMS Master" Language="C#" MasterPageFile="~/CMSMaster.Master" AutoEventWireup="true"
    CodeBehind="CustomerDetails.aspx.cs" Inherits="CMSMaster.CustomerDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                        <table width="100%" border="0" cellpadding="3" cellspacing="0" bgcolor="f4f5f7" style="text-align: left;
                            font-weight: normal; padding: 5px 10px 5px 10px; font-size: 11px;" >
                            <tr>
                                <td width="100%" colspan="4">
                                    <asp:Label ID="lblErrMessage" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Customer No:
                                </td>
                                <td>
                                    <asp:TextBox CssClass="input_text" Width="40px" ID="txtCustomerNo" MaxLength="3"
                                        runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    Database Name:<span class="errText">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDBName" CssClass="input_text" MaxLength="50" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Customer Since:<span class="errText">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCustomerSince" CssClass="input_text" Width="50px" MaxLength="4"
                                        runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    Active:
                                </td>
                                <td class="cbStyle">
                                    <asp:CheckBox ID="chkActive" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Customer Name:<span class="errText">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCustomerName" CssClass="input_text" MaxLength="50" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    Date Entered:<span class="errText">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDateEntered" CssClass="input_text" MaxLength="30" Width="80px"
                                        runat="server"></asp:TextBox>
                                    <asp:ImageButton ImageAlign="Middle" runat="Server" ID="imgDate" ImageUrl="~/images/Calendar_scheduleHS.png"
                                        AlternateText="Click to show calendar" />
                                    <ajaxToolkit:CalendarExtender ID="calendarButtonExtender1" runat="server" TargetControlID="txtDateEntered"
                                        PopupButtonID="imgDate" Format="MM/dd/yyyy" CssClass="cal_Theme1" />
                                    <%--                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" TargetControlID="txtDateEntered"
                                        Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                        OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                        ErrorTooltipEnabled="True" />
                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator5" runat="server" ControlExtender="MaskedEditExtender5"
                                        ControlToValidate="txtDateEntered" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                        Display="Dynamic" TooltipMessage="Input a date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                        ValidationGroup="MKE" />--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Address:<span class="errText">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAddress1" CssClass="input_text" Width="180px" MaxLength="200"
                                        runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    Address2:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAddress2" Width="180px" CssClass="input_text" MaxLength="200"
                                        runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Phone 1:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPhone1" CssClass="input_text" Width="90px" MaxLength="20" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    Phone 2:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPhone2" CssClass="input_text" MaxLength="20" Width="90px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Main Contact:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMainContact" CssClass="input_text" MaxLength="30" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    Secondary Contact:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSecondaryContact" CssClass="input_text" MaxLength="30" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Primary E-Mail:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPrimaryEmail" CssClass="input_text" MaxLength="30" Width="220px"
                                        runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    Secondary Email:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSecondaryemail" CssClass="input_text" Width="220px" MaxLength="30"
                                        runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Country:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCountry" CssClass="DropDown_Style" AutoPostBack="true" Width="144px"
                                        runat="server" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    State:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlState" CssClass="DropDown_Style" AutoPostBack="true" Width="144px"
                                        runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    City:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCity" CssClass="DropDown_Style" Width="144px" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    Zip:<span class="errText">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtZip" CssClass="input_text" MaxLength="10" Width="50px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    ABCLogistics Sales Rep Email:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtABCLogisticsSalRepEmail" CssClass="input_text" Width="220px" MaxLength="50"
                                        runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    CSR Contact Email:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCSRConEmail" Width="220px" CssClass="input_text" MaxLength="50"
                                        runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    Notes:
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtNotes" CssClass="input_text" Width="600px" Rows="4" MaxLength="500"
                                        TextMode="MultiLine" Columns="3" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="right" style="text-align: right">
                                    <asp:LinkButton ID="btnSave" Text="Save" CssClass="button1" ForeColor="white" 
                                        runat="server" onclick="btnSave_Click" />
                                    <asp:LinkButton ID="btnCancel" Text="Cancel" CssClass="button1" ForeColor="white"
                                        runat="server" onclick="btnCancel_Click" />
                                </td>
                            </tr>
                        </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

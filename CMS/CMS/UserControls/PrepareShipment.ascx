<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PrepareShipment.ascx.cs"
    Inherits="CMS.UserControls.PrepareShipment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControls/ctlConfirm.ascx" TagName="EmptyCart" TagPrefix="uc" %>
<input id="dummy" type="button" style="display: none" runat="server" />
<ajaxToolkit:ModalPopupExtender ID="MPEPrepare" runat="server" PopupControlID="pnlPrepare"
    TargetControlID="dummy" BackgroundCssClass="popupBG">
</ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="pnlPrepare" runat="server">
    <div class="PopupFormBG" style="width: 600px; background-color: white; padding-bottom: 1px;">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr style="height: 30px;">
                <td class="PopupFormTitleStyle">
                    <asp:Label ID="lblTitle" runat="server" Text="Prepare Shipment"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblErr" runat="server" CssClass="errMessage"></asp:Label>
                </td>
            </tr>
            <tr style="padding-top: 10px;">
                <td>
                    <table width="98%" cellpadding="0" cellspacing="0" align="center" class="tblLabelStyle">
                        <tr>
                            <td>
                                <span><b>Please check all shipment details and click PRINT LABEL to print label.</b>
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td height="10px">
                                &nbsp;
                                <asp:HiddenField ID="hfOrderId" runat="server" />
                                <asp:HiddenField ID="hfLicense" runat="server" />
                                <asp:HiddenField ID="hfUserName" runat="server" />
                                <asp:HiddenField ID="hfPassword" runat="server" />
                                <asp:HiddenField ID="hfAccountNumber" runat="server" />
                            </td>
                        </tr>
                        <tr width="100%">
                            <td width="100%">
                                <table border="0" cellpadding="1" cellspacing="0" width="100%">
                                    <tr width="100%">
                                        <td width="50%" align="left" valign="top">
                                            <table width="100%" cellpadding="0" cellspacing="0" border="0" style="vertical-align: top;"
                                                class="tblLabelStyle">
                                                <tr>
                                                    <td valign="top" colspan="2">
                                                        <span><b>Ship From Address</b></span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span>Name</span>
                                                    </td>
                                                    <td>
                                                        <span>
                                                            <asp:Label ID="lblFromName" runat="server" Text=""></asp:Label>
                                                        </span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span>Street</span>
                                                    </td>
                                                    <td>
                                                        <span>
                                                            <asp:Label ID="lblFromStreet" runat="server" Text=""></asp:Label>
                                                        </span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span>City</span>
                                                    </td>
                                                    <td>
                                                        <span>
                                                            <asp:Label ID="lblFromCity" runat="server" Text=""></asp:Label>
                                                        </span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span>State</span>
                                                    </td>
                                                    <td>
                                                        <asp:HiddenField ID="hfFromStateUPSCode" runat="server" /> 
                                                        <span>
                                                            <asp:Label ID="lblFromState" runat="server" Text=""></asp:Label>
                                                        </span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span>Country</span>
                                                    </td>
                                                    <td>
                                                        <asp:HiddenField ID="hfFromCountryUPSCode" runat="server" />
                                                        <span>
                                                            <asp:Label ID="lblFromCountry" runat="server" Text=""></asp:Label>
                                                        </span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span>Zip</span>
                                                    </td>
                                                    <td>
                                                        <span>
                                                            <asp:Label ID="lblFromZip" runat="server" Text=""></asp:Label>
                                                        </span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span>Phone Number</span>
                                                    </td>
                                                    <td>
                                                        <span>
                                                            <asp:Label ID="lblFromPhone" runat="server" Text=""></asp:Label>
                                                        </span>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td valign="top">
                                            <table width="100%" cellpadding="0" cellspacing="0" border="0" style="vertical-align: top;"
                                                class="tblLabelStyle">
                                                <tr>
                                                    <td valign="top" colspan="2">
                                                        <span><b>Ship To Address</b> </span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span>Name</span>
                                                    </td>
                                                    <td>
                                                        <span>
                                                            <asp:Label ID="lblToName" runat="server" Text=""></asp:Label>
                                                        </span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span>Street</span>
                                                    </td>
                                                    <td>
                                                        <span>
                                                            <asp:Label ID="lblToStreet" runat="server" Text=""></asp:Label>
                                                        </span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span>City</span>
                                                    </td>
                                                    <td>
                                                        <span>
                                                            <asp:Label ID="lblToCity" runat="server" Text=""></asp:Label>
                                                        </span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span>State</span>
                                                    </td>
                                                    <td>
                                                        <asp:HiddenField ID="hfToStateUPSCode" runat="server" />
                                                        <span>
                                                            <asp:Label ID="lblToState" runat="server" Text=""></asp:Label>
                                                        </span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span>Country</span>
                                                    </td>
                                                    <td>
                                                        <asp:HiddenField ID="hfToCountryUPSCode" runat="server" />
                                                        <span>
                                                            <asp:Label ID="lblToCountry" runat="server" Text=""></asp:Label>
                                                        </span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span>Zip</span>
                                                    </td>
                                                    <td>
                                                        <span>
                                                            <asp:Label ID="lblToZip" runat="server" Text=""></asp:Label>
                                                        </span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span>Phone Number</span>
                                                    </td>
                                                    <td>
                                                        <span>
                                                            <asp:Label ID="lblToPhone" runat="server" Text=""></asp:Label>
                                                        </span>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <table width="100%">
                                    <tr>
                                        <td align="left">
                                            <span><b>Total Weight</b></span>
                                        </td>
                                        <td align="left">
                                            <span>
                                                <asp:Label ID="lblWeight" runat="server"></asp:Label>
                                            </span>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="5px">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%" style="text-align: left">
                                    <tr>
                                        <td class="tableLayout">
                                            <span>Shipping Service</span>
                                        </td>
                                        <td>
                                            <span>
                                                <asp:Label ID="lblShipService" runat="server"></asp:Label>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span>Address Validation</span>
                                        </td>
                                        <td>
                                            <span>
                                                <asp:Label ID="lblAddressValidation" runat="server"></asp:Label>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span>Shipment Charge Type</span>
                                        </td>
                                        <td>
                                            <span>
                                                <asp:Label ID="lblChargeType" runat="server"></asp:Label>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span>Measurement Type</span>
                                        </td>
                                        <td>
                                            <span>
                                                <asp:Label ID="lblMeasurementType" runat="server"></asp:Label>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span>Packaging Type</span>
                                        </td>
                                        <td>
                                            <span>
                                                <asp:Label ID="lblPackagingType" runat="server"></asp:Label>
                                            </span>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                       <tr style="height: 50px;">
                        <td align="right">
                            <table width="30%">
                                <tr>
                                    <td align="center">
                                        <asp:LinkButton ID="lbPrintLabel" runat="server" Text="Print Label"
                                            CssClass="addButtonStyle" OnClick="lbPrintLabel_Click"></asp:LinkButton>
                                    </td>
                                    <td align="center">
                                        <asp:LinkButton ID="lbOK" runat="server" Text="OK"
                                            CssClass="addButtonStyle" OnClick="lbOK_Click"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Panel>
<uc:EmptyCart ID="ctlEmptyCart" runat="server" />


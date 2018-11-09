<%@ Page Title="Customer UI Settings - CMS Master" Language="C#" MasterPageFile="~/CMSMaster.Master" AutoEventWireup="true"
    CodeBehind="CustomerFonts.aspx.cs" Inherits="CMSMaster.CustomerFonts" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function headerBGColorChanged() {
            var headerFont = $get("<%=lblHeaderFont.ClientID %>");
            headerFont.style.backgroundColor = $get("<%=txtHeaderBGColor.ClientID %>").value;
        }

        function headerForeColorChanged() {
            var headerFont = $get("<%=lblHeaderFont.ClientID %>");
            headerFont.style.color = $get("<%=txtHeaderFontColor.ClientID %>").value;
        }

        function footerBGColorChanged() {
            var footerFont = $get("<%=lblFooterFont.ClientID %>");
            footerFont.style.backgroundColor = $get("<%=txtFooterBGColor.ClientID %>").value;

        }

        function footerForeColorChanged() {
            var footerFont = $get("<%=lblFooterFont.ClientID %>");
            footerFont.style.color = $get("<%=txtFooterFontColor.ClientID %>").value;
        }

        function sidebarBGColorChanged(type) {
            var sidebarFont = $get("<%=lblSideBarFont.ClientID %>");
            sidebarFont.style.backgroundColor = $get("<%=txtSideBarBGColor.ClientID %>").value;
        }

        function sidebarForeColorChanged() {
            var sidebarFont = $get("<%=lblSideBarFont.ClientID %>");
            sidebarFont.style.color = $get("<%=txtSideBarFontColor.ClientID %>").value;
        }

        function headerFontSizeChanged() {
            if ($get("<%=rblHeaderFontSizeByUnits.ClientID %>").checked == true) {
                var headerFont = $get("<%=lblHeaderFont.ClientID %>");
                headerFont.style.fontSize = $get("<%=txtHeaderFontSize.ClientID %>").value;
            }
        }

        function footerFontSizeChanged() {
            if ($get("<%=rblFooterFontSizeByUnits.ClientID %>").checked == true) {
                var headerFont = $get("<%=lblFooterFont.ClientID %>");
                headerFont.style.fontSize = $get("<%=txtFooterFontSize.ClientID %>").value;
            }
        }

        function sidebarFontSizeChanged() {
            if ($get("<%=rblSideBarFontSizeByUnits.ClientID %>").checked == true) {
                var headerFont = $get("<%=lblSideBarFont.ClientID %>");
                headerFont.style.fontSize = $get("<%=txtSideBarFontSize.ClientID %>").value;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%" border="0" cellpadding="3" cellspacing="0" class="panelStyle"
                bgcolor="f4f5f7" style="text-align: left; font-weight: normal; padding: 5px;
                font-size: 11px;">
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblTitle" runat="server" Text="Customer UI Settings" Font-Bold="True"
                            Font-Size="Medium" ForeColor="#0099CC"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblErr" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <asp:Panel ID="plHeader" Width="100%" runat="server" GroupingText="Header Font">
                            <table width="100%" style="margin-left: 5px">
                                <tr>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td width="30%">
                                                    <asp:Label ID="Label9" runat="server" Text="Header Background Color:"></asp:Label>
                                                </td>
                                                <td width="70%" align="left">
                                                    <asp:TextBox ID="txtHeaderBGColor" runat="server" OnTextChanged="txtHeaderBGColor_TextChanged"
                                                        AutoPostBack="true" CssClass="input_text"></asp:TextBox>
                                                    <asp:ColorPickerExtender ID="txtHeaderBGColor_ColorPickerExtender" runat="server"
                                                        Enabled="True" TargetControlID="txtHeaderBGColor" SampleControlID="lblHeaderBG"
                                                        OnClientColorSelectionChanged="headerBGColorChanged">
                                                    </asp:ColorPickerExtender>
                                                    &nbsp;
                                                    <asp:Label ID="lblHeaderBG" runat="server" Height="20" Width="20"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="30%" align="left">
                                                    <asp:Label ID="Label2" runat="server" Text="Font Name:"></asp:Label>
                                                </td>
                                                <td width="70%" align="left">
                                                    <asp:DropDownList ID="ddlHeaderFontNames" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlHeaderFontNames_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="30%" align="left">
                                                    <asp:Label ID="Label3" runat="server" Text="Font Size:"></asp:Label>
                                                </td>
                                                <td width="70%" align="left">
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="20%" class="rbStyle">
                                                                <asp:RadioButton ID="rblHeaderFontSizeByName" runat="server" Text="By Name" GroupName="HeaderFontSize" />
                                                            </td>
                                                            <td width="80%">
                                                                <asp:DropDownList ID="ddlHeaderFontSizes" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlHeaderFontSizes_SelectedIndexChanged"
                                                                    Style="height: 22px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="20%" class="rbStyle">
                                                                <asp:RadioButton ID="rblHeaderFontSizeByUnits" runat="server" Text="By Units" GroupName="HeaderFontSize"
                                                                    Checked="true" />
                                                            </td>
                                                            <td width="80%">
                                                                <asp:TextBox ID="txtHeaderFontSize" runat="server" Columns="10" onblur="Javascript:headerFontSizeChanged();" CssClass="input_text"></asp:TextBox>
                                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtHeaderFontSize"
                                                                    Operator="DataTypeCheck" Type="Integer" ErrorMessage="Value Must Be Integer"></asp:CompareValidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="30%" align="left">
                                                    <asp:Label ID="Label4" runat="server" Text="Font Color:"></asp:Label>
                                                </td>
                                                <td width="70%">
                                                    <asp:TextBox ID="txtHeaderFontColor" runat="server" OnTextChanged="txtHeaderFontColor_TextChanged"
                                                        AutoPostBack="true" CssClass="input_text"></asp:TextBox>
                                                    <asp:ColorPickerExtender ID="txtHeaderFontColor_ColorPickerExtender" runat="server"
                                                        Enabled="True" TargetControlID="txtHeaderFontColor" SampleControlID="lblHeaderFontColor"
                                                        OnClientColorSelectionChanged='headerForeColorChanged'>
                                                    </asp:ColorPickerExtender>
                                                    &nbsp;
                                                    <asp:Label ID="lblHeaderFontColor" runat="server" Height="20" Width="20"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                    <td align="left" width="100px">
                        <asp:Label ID="lblHeaderFont" runat="server" Text="Text"></asp:Label>
                    </td>
                </tr>
                </table> </asp:Panel> </td> </tr>
                <%--<tr>
                    <td colspan="2" align="left">
                        <asp:LinkButton ID="lbSL" runat="server">Get Color</asp:LinkButton>

                        <asp:ModalPopupExtender ID="lbSL_ModalPopupExtender" runat="server" 
                            PopupControlID="plSL" BackgroundCssClass="popupSLBG" Enabled="True" TargetControlID="lbSL">
                        </asp:ModalPopupExtender>
                        <asp:Panel ID="plSL" runat="server">
                         <div id="silverlightControlHost">
    <center>
        <object data="data:application/x-silverlight-2," type="application/x-silverlight-2" width="600" height="400">
		  <param name="source" value="ClientBin/CMSMaster_SL.xap"/>
		  <param name="onError" value="onSilverlightError" />
		  <param name="background" value="white" />
		  <param name="minRuntimeVersion" value="4.0.50826.0" />
		  <param name="autoUpgrade" value="true" />
		  <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=4.0.50826.0" style="text-decoration:none">
 			  <img src="http://go.microsoft.com/fwlink/?LinkId=161376" alt="Get Microsoft Silverlight" style="border-style:none"/>
		  </a>
	    </object><iframe id="_sl_historyFrame" style="visibility:hidden;height:0px;width:0px;border:0px"></iframe>
        </center>
        </div>
                        </asp:Panel>
                       
                    </td>
                </tr>--%>
                <tr>
                    <td valign="top">
                        <asp:Panel ID="Panel1" Width="100%" runat="server" GroupingText="Footer Font">
                            <table width="100%" style="margin-left: 5px">
                                <tr>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td width="30%">
                                                    <asp:Label ID="Label14" runat="server" Text="Footer Background Color:"></asp:Label>
                                                </td>
                                                <td width="70%" align="left">
                                                    <asp:TextBox ID="txtFooterBGColor" runat="server" OnTextChanged="txtFooterBGColor_TextChanged"
                                                        AutoPostBack="true" CssClass="input_text"></asp:TextBox>
                                                    <asp:ColorPickerExtender ID="txtFooterBGColor_ColorPickerExtender" runat="server"
                                                        Enabled="True" TargetControlID="txtFooterBGColor" SampleControlID="lblFooterBG"
                                                        OnClientColorSelectionChanged="footerBGColorChanged">
                                                    </asp:ColorPickerExtender>
                                                    &nbsp;
                                                    <asp:Label ID="lblFooterBG" runat="server" Height="20" Width="20"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="30%" align="left">
                                                    <asp:Label ID="Label6" runat="server" Text="Font Name:"></asp:Label>
                                                </td>
                                                <td width="70%" align="left">
                                                    <asp:DropDownList ID="ddlFooterFontNames" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFooterFontNames_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="30%" align="left">
                                                    <asp:Label ID="Label7" runat="server" Text="Font Size:"></asp:Label>
                                                </td>
                                                <td width="70%" align="left">
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="20%" class="rbStyle">
                                                                <asp:RadioButton ID="rblFooterFontSizeByName" runat="server" Text="By Name" GroupName="FooterFontSize" />
                                                            </td>
                                                            <td width="80%">
                                                                <asp:DropDownList ID="ddlFooterFontSizes" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFooterFontSizes_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="20%" class="rbStyle">
                                                                <asp:RadioButton ID="rblFooterFontSizeByUnits" runat="server" Text="By Units" GroupName="FooterFontSize"
                                                                    Checked="true" />
                                                            </td>
                                                            <td width="80%">
                                                                <asp:TextBox ID="txtFooterFontSize" runat="server" Columns="10" onblur="Javascript:footerFontSizeChanged();" CssClass="input_text"></asp:TextBox>
                                                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtFooterFontSize"
                                                                    Operator="DataTypeCheck" Type="Integer" ErrorMessage="Value Must Be Integer"></asp:CompareValidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="30%" align="left">
                                                    <asp:Label ID="Label8" runat="server" Text="Font Color:"></asp:Label>
                                                </td>
                                                <td width="70%">
                                                    <asp:TextBox ID="txtFooterFontColor" runat="server" OnTextChanged="txtFooterFontColor_TextChanged"
                                                        AutoPostBack="true" CssClass="input_text"></asp:TextBox>
                                                    <asp:ColorPickerExtender ID="txtFooterFontColor_ColorPickerExtender" runat="server"
                                                        Enabled="True" TargetControlID="txtFooterFontColor" SampleControlID="lblFooterFontColor"
                                                        OnClientColorSelectionChanged='footerForeColorChanged'>
                                                    </asp:ColorPickerExtender>
                                                    &nbsp;
                                                    <asp:Label ID="lblFooterFontColor" runat="server" Height="20" Width="20"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td align="left" width="100px">
                                        <asp:Label ID="lblFooterFont" runat="server" Text="Text"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <asp:Panel ID="Panel2" Width="100%" runat="server" GroupingText="Side Bar Font">
                            <table width="100%" style="margin-left: 5px">
                                <tr>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td width="30%">
                                                    <asp:Label ID="Label19" runat="server" Text="Sidebar Background Color:"></asp:Label>
                                                </td>
                                                <td width="70%" align="left">
                                                    <asp:TextBox ID="txtSideBarBGColor" runat="server" OnTextChanged="txtSideBarBGColor_TextChanged"
                                                        AutoPostBack="true" CssClass="input_text"></asp:TextBox>
                                                    <asp:ColorPickerExtender ID="txtSideBarBGColor_ColorPickerExtender" runat="server"
                                                        Enabled="True" TargetControlID="txtSideBarBGColor" SampleControlID="lblSideBarBG"
                                                        OnClientColorSelectionChanged='sidebarBGColorChanged'>
                                                    </asp:ColorPickerExtender>
                                                    &nbsp;
                                                    <asp:Label ID="lblSideBarBG" runat="server" Height="20px" Width="20px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="30%" align="left">
                                                    <asp:Label ID="Label16" runat="server" Text="Font Name:"></asp:Label>
                                                </td>
                                                <td width="70%" align="left">
                                                    <asp:DropDownList ID="ddlSideBarFontNames" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSideBarFontNames_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="30%" align="left">
                                                    <asp:Label ID="Label17" runat="server" Text="Font Size:"></asp:Label>
                                                </td>
                                                <td width="70%" align="left">
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="20%" class="rbStyle">
                                                                <asp:RadioButton ID="rblSideBarFontSizeByName" runat="server" Text="By Name" GroupName="SideBarFontSize" />
                                                            </td>
                                                            <td width="80%">
                                                                <asp:DropDownList ID="ddlSideBarFontSizes" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSideBarFontSizes_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="20%" class="rbStyle">
                                                                <asp:RadioButton ID="rblSideBarFontSizeByUnits" runat="server" Text="By Units" GroupName="SideBarFontSize"
                                                                    Checked="true" />
                                                            </td>
                                                            <td width="80%">
                                                                <asp:TextBox ID="txtSideBarFontSize" runat="server" Columns="10" onblur="Javascript:sidebarFontSizeChanged();" CssClass="input_text"></asp:TextBox>
                                                                <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtSideBarFontSize"
                                                                    Operator="DataTypeCheck" Type="Integer" ErrorMessage="Value Must Be Integer"></asp:CompareValidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="30%" align="left">
                                                    <asp:Label ID="Label18" runat="server" Text="Font Color:"></asp:Label>
                                                </td>
                                                <td width="70%">
                                                    <asp:TextBox ID="txtSideBarFontColor" runat="server" OnTextChanged="txtSideBarFontColor_TextChanged"
                                                        AutoPostBack="true" CssClass="input_text"></asp:TextBox>
                                                    <asp:ColorPickerExtender ID="txtSideBarFontColor_ColorPickerExtender" runat="server"
                                                        Enabled="True" TargetControlID="txtSideBarFontColor" SampleControlID="lblSideBarFontColor"
                                                        OnClientColorSelectionChanged="sidebarForeColorChanged">
                                                    </asp:ColorPickerExtender>
                                                    &nbsp;
                                                    <asp:Label ID="lblSideBarFontColor" runat="server" Height="20" Width="20"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td align="left" width="100px">
                                        <asp:Label ID="lblSideBarFont" runat="server" Text="Text"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <%--                <tr>
                    <td>
                        <asp:Label ID="Label10" runat="server" Text="Main Font:"></asp:Label>
                    </td>
                </tr>--%>
                <%--<tr>
                    <td valign="top">
                        <table width="100%" style="margin-left: 30px">
                            <tr>
                                <td width="20%" align="left">
                                    <asp:Label ID="Label11" runat="server" Text="Font Name:"></asp:Label>
                                </td>
                                <td width="80%" align="left">
                                    <asp:DropDownList ID="ddlMainFontNames" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMainFontNames_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="20%" align="left">
                                    <asp:Label ID="Label12" runat="server" Text="Font Size:"></asp:Label>
                                </td>
                                <td width="80%" align="left">
                                    <asp:DropDownList ID="ddlMainFontSizes" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMainFontSizes_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="20%" align="left">
                                    <asp:Label ID="Label13" runat="server" Text="Font Color:"></asp:Label>
                                    &nbsp;
                                    <asp:Label ID="lblMainFontColor" runat="server" Height="20" Width="20"></asp:Label>
                                </td>
                                <td width="80%">
                                    <asp:TextBox ID="txtMainFontColor" runat="server" OnTextChanged="txtMainFontColor_TextChanged"></asp:TextBox>
                                    <asp:ColorPickerExtender ID="txtMainFontColor_ColorPickerExtender" runat="server"
                                        Enabled="True" TargetControlID="txtMainFontColor" SampleControlID="lblMainFontColor">
                                    </asp:ColorPickerExtender>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblMainFont" runat="server" Text="Text" Font-Size="XX-Large"></asp:Label>
                    </td>
                </tr>--%>
                <tr>
                    <td colspan="2" align="right">
                        <asp:LinkButton ID="btnSave" Text="Save" CssClass="button1" ForeColor="white" runat="server"
                            OnClick="btnSave_Click" />
                        <asp:LinkButton ID="btnCancel" Text="Cancel" CssClass="button1" ForeColor="white"
                            runat="server" onclick="btnCancel_Click" />
                        <asp:LinkButton ID="lbPreview" Text="Preview" CssClass="button1" ForeColor="white"
                            runat="server" OnClick="lbPreview_Click" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

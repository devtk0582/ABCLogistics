<%@ Page Title="Check Out" Language="C#" MasterPageFile="~/MasterPages/ABCLogisticsCMS.Master" AutoEventWireup="true"
    CodeBehind="CheckOut.aspx.cs" Inherits="CMS.Catalog.CheckOut" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%--<%@ Register Src="~/UserControls/ctlConfirm.ascx" TagName="EmptyCart" TagPrefix="uc1" %>--%>
<%@ Register Src="../UserControls/PrepareShipment.ascx" TagName="PrepareShipment" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="upCheckOut" runat="server">
        <ContentTemplate>
        <table width="100%" cellspacing="1" cellpadding="1" style="padding: 5px 10px 5px 10px;">
         <tr>
                    <td align="center">
                        <div style="width: 100%; text-align: center; margin-top: 15px;">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" >
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
                        &nbsp;&nbsp;&nbsp;<asp:Label ID="lblTitle" runat="server" Text="Check Out" CssClass="titleStyle"></asp:Label>
                    </td>
                    </tr>


        <tr><td valign="top" width="100%" align="left">
          <table width="100%" border="0" cellpadding="0" cellspacing="0" >
                <tr id="tr_CheckOut" runat="server" visible="false">
                    <td>
                        <table width="100%" cellpadding="0" cellspacing="0" >
                            <tr>
                                <td>
                                    <table width="98%" cellpadding="0" cellspacing="0" align="center" class="tblLabelStyle">
                                        
                                        <tr>
                                            <td align="left" style="background-image: url('../Images/categoryHeader.JPG'); background-repeat: repeat-x;
                                                height: 30px; width: 100%;">
                                               &nbsp;<asp:Label ID="lblheaderMsg" runat="server" Font-Bold="true" Text="Please enter shipping information for your order below"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" width="100%">
                                                <table width="100%" cellpadding="0" cellspacing="0" class="tblLabelStyle">
                                                    <tr>
                                                        <td width="70%" valign="top" align="left">
                                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                       <span><b>Your personal details</b></span> 
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblPhone" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td width="30%" align="left">
                                                            <table width="100%" cellpadding="0" cellspacing="0" class="formbg">
                                                                <tr>
                                                                    <td align="left">
                                                                        <table width="100%" cellpadding="5" cellspacing="0" class="tblLabelStyle">
                                                                            <tr>
                                                                                <td align="left" width="100%">
                                                                                    
                                                                                     <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:LinkButton ID="lnkycart" runat="server" Text="Your Cart:"></asp:LinkButton>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td width="100%">
                                                                                                        <asp:Panel ID="pnlViewCart" ScrollBars="Vertical" runat="server" Width="100%" Height="100px"
                                                                                                            BackColor="#F2F2F2">
                                                                                                            <asp:GridView ID="gv_ViewCart" runat="server" AutoGenerateColumns="false" GridLines="Both"
                                                                                                                CssClass="myGridView" RowStyle-CssClass="rowStyle" AlternatingRowStyle-CssClass="altRowStyle"
                                                                                                                ShowFooter="true" HeaderStyle-CssClass="headerStyle" AllowSorting="false" AllowPaging="false"
                                                                                                                Width="100%" Height="100%" OnRowDataBound="gv_ViewCart_RowDataBound">
                                                                                                                <Columns>
                                                                                                                    <asp:TemplateField HeaderText="Quantity" SortExpression="qty" ControlStyle-Font-Size="9pt"
                                                                                                                        ItemStyle-Height="13px">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lblqty1" runat="server" Text='<%# bind("qty") %>'></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                        <FooterTemplate>
                                                                                                                        </FooterTemplate>
                                                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Item/SKU" SortExpression="part_num" ControlStyle-Font-Size="9pt"
                                                                                                                        ItemStyle-Height="13px">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lblpart_num1" runat="server" Text='<%# bind("part_num") %>'></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                        <FooterTemplate>
                                                                                                                            <asp:Label ID="lblTAmount1" runat="server" Text="Total Amount:" Font-Bold="true"
                                                                                                                                Font-Size="10pt"></asp:Label></FooterTemplate>
                                                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Each" SortExpression="List_price" Visible="false"
                                                                                                                        ControlStyle-Font-Size="9pt" ItemStyle-Height="13px">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lblEach1" runat="server" Text='<%#bind("List_price") %>'></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                        <FooterTemplate>
                                                                                                                            <asp:Label ID="lblTAmount12" runat="server" Text="Total Amount:" Font-Bold="true"
                                                                                                                                Font-Size="10pt"></asp:Label></FooterTemplate>
                                                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                                                        <FooterStyle HorizontalAlign="Right" />
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Total" SortExpression="List_price" ControlStyle-Font-Size="9pt"
                                                                                                                        ItemStyle-Height="13px">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lblList_price1" runat="server" Text='<%#bind("List_price")*bind("qty") %>'></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                        <FooterTemplate>
                                                                                                                            <asp:Label ID="lblTotalAmount1" runat="server" Text="" Font-Bold="true" Font-Size="10pt"></asp:Label>
                                                                                                                        </FooterTemplate>
                                                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                                                    </asp:TemplateField>
                                                                                                                </Columns>
                                                                                                            </asp:GridView>
                                                                                                        </asp:Panel>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td height="2px">
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                       
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
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
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblMend" runat="server" Text="Fields marked with an * are required"
                                                    ForeColor="Black"></asp:Label>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="background-image: url('../Images/categoryHeader.JPG'); background-repeat: repeat-x;
                                                height: 30px; width: 100%; padding-left: 5px;">
                                                <asp:Label ID="lblspadd" runat="server" Text="Shipping Address" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="100%">
                                                <table width="100%" cellpadding="5" cellspacing="0" border="0" class="tblLabelStyle" >
                                                    <tr>
                                                        <td width="20%" align="left">
                                                          <span>  <b>Choose&nbsp;a&nbsp;previous&nbsp;address:*</b></span>
                                                        </td>
                                                        <td width="80%" align="left">
                                                            <asp:DropDownList ID="ddlAddress" runat="server" Width="250px" AutoPostBack="true" CssClass="DropDown_Style"
                                                                OnSelectedIndexChanged="ddlAddress_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="left" width="100%">
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" class="tblLabelStyle">
                                                                <tr>
                                                                    <td width="100%" align="left" style="text-align: left;">
                                                                        <div align="left" style="width: 100%; text-align: left;">
                                                                            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                                                                                <tr>
                                                                                    <td colspan="4">
                                                                                        <asp:Label ID="lblvaliderror" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td width="23%" align="left" style="text-align: left; height: 19px; vertical-align: middle;">
                                                                                      <span>Company :</span>  
                                                                                    </td>
                                                                                    <td width="30%" align="left" style="text-align: left; height: 19px; vertical-align: middle;">
                                                                                         <span><asp:TextBox ID="txtCompany" runat="server" MaxLength="100" Width="180px" AutoPostBack="false" Font-Size="12px"></asp:TextBox></span>
                                                                                        <%-- <cc1:AutoCompleteExtender ID="ACECompany" CompletionListHighlightedItemCssClass="SearchListItemHover"
                                                                    CompletionListItemCssClass="SearchList" runat="server" TargetControlID="txtCompany"
                                                                    OnClientItemSelected="AddressToSelect" ServicePath="~/AutoComplete.asmx" ServiceMethod="GetAddressInfo"
                                                                    MinimumPrefixLength="1" CompletionInterval="1000" EnableCaching="true" CompletionSetCount="5">
                                                                </cc1:AutoCompleteExtender>--%>
                                                                                    </td>
                                                                                    <td width="8%">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                    <td width="39%">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td width="23%" align="left" style="text-align: left; height: 19px; vertical-align: middle;">
                                                                                      <span>   Street Address :</span>
                                                                                    </td>
                                                                                    <td width="30%" align="left" style="text-align: left; height: 19px; vertical-align: middle;">
                                                                                       <span>  <asp:TextBox ID="txtStreetAdd1" runat="server" Width="310px" Font-Size="12px"></asp:TextBox></span>
                                                                                    </td>
                                                                                    <td width="8%">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                    <td width="39%">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td width="23%" align="left" style="text-align: left; height: 19px; vertical-align: middle;">
                                                                                       <span>  Address line 2 :</span>
                                                                                    </td>
                                                                                    <td width="30%" align="left" style="text-align: left; height: 19px; vertical-align: middle;">
                                                                                      <span>   <asp:TextBox ID="txtAdd2" runat="server" Width="310px" Font-Size="12px"></asp:TextBox></span>
                                                                                    </td>
                                                                                    <td width="8%">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                    <td width="39%">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td width="23%" align="left" style="text-align: left; height: 19px; vertical-align: middle;">
                                                                                      <span>   Country :</span>
                                                                                    </td>
                                                                                    <td width="30%" align="left" style="text-align: left; height: 19px; vertical-align: middle;">
                                                                                        <span> <asp:DropDownList ID="ddlCountry" Width="143px" AutoPostBack="true" runat="server" CssClass="DropDown_Style"
                                                                                            OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                                                                        </asp:DropDownList></span>
                                                                                    </td>
                                                                                    <td width="8%" align="right" style="text-align: right; height: 19px; vertical-align: middle;">
                                                                                        <span> State :</span>
                                                                                    </td>
                                                                                    <td width="39%" align="left" style="text-align: left; height: 19px; vertical-align: middle;">
                                                                                        <span> <asp:DropDownList ID="ddlState" Width="140px" AutoPostBack="true" runat="server" CssClass="DropDown_Style"
                                                                                            OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                                                                        </asp:DropDownList></span>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td width="23%" style="text-align: left; height: 19px; vertical-align: middle;">
                                                                                       <span>  City :</span>
                                                                                    </td>
                                                                                    <td width="30%" align="left" style="text-align: left; height: 19px; vertical-align: middle;">
                                                                                        <span> <asp:DropDownList ID="ddlCity" runat="server" Width="140px" CssClass="DropDown_Style">
                                                                                        </asp:DropDownList></span>
                                                                                    </td>
                                                                                    <td width="8%" align="right" style="text-align: right; height: 19px; vertical-align: middle;">
                                                                                       <span>  Zip :</span>
                                                                                    </td>
                                                                                    <td width="39%" align="left" style="text-align: left; height: 19px; vertical-align: middle;">
                                                                                       <span>  <asp:TextBox ID="txtZip" runat="server" Width="80px" MaxLength="20" Font-Size="12px"></asp:TextBox></span>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td width="23%" align="left" style="text-align: left; height: 19px; vertical-align: middle;">
                                                                                      <span>   Email : </span>
                                                                                    </td>
                                                                                    <td width="30%" align="left" style="text-align: left; height: 19px; vertical-align: middle;">
                                                                                       <span>  <asp:TextBox ID="txtEmail" runat="server" Width="240px" 
                                                                                            Font-Size="12px"></asp:TextBox></span>
                                                                                    </td>
                                                                                    <td width="8%" align="right" style="text-align: right; height: 19px; vertical-align: middle;">
                                                                                       <span>  Phone :</span>
                                                                                    </td>
                                                                                    <td width="39%" align="left" style="text-align: left; height: 19px; vertical-align: middle;">
                                                                                       <span>  <asp:TextBox ID="txtPhone" runat="server" Width="90px" Font-Size="12px"></asp:TextBox></span>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                           <span>  <b>Comments&nbsp;on&nbsp;your&nbsp;order:</b></span>
                                                        </td>
                                                        <td width="80%" align="left">
                                                           <span>  <asp:TextBox ID="txtNotes" runat="server" Font-Size="12px" TextMode="MultiLine" Height="80px" Width="500px"></asp:TextBox></span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2"  width="100%">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>

                <tr id="tr_PickShip" runat="server" visible="false">
                    <td>
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <table width="98%" cellpadding="0" cellspacing="0" align="center" class="tblLabelStyle">
                                        
                                        <tr>
                                            <td style="background-image: url('../Images/categoryHeader.JPG'); background-repeat: repeat-x;
                                                height: 25px; width: 100%; padding-left: 5px;">
                                               <span> <b>Billing Code</b></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="5px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr width="100%">
                                            <td width="100%">
                                                <table border="0" cellpadding="1" cellspacing="0" width="100%">
                                                    <tr width="100%">
                                                        <td width="20%" align="left">
                                                          <span>  Billing&nbsp;Code: </span>
                                                        </td>
                                                        <td width="80%" align="left">
                                                         <span>   <asp:DropDownList ID="ddlBillingCode" runat="server" Width="250px" CssClass="DropDown_Style">
                                                            </asp:DropDownList></span>
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
                                            <td style="background-image: url('../Images/categoryHeader.JPG'); background-repeat: repeat-x;
                                                height: 25px; width: 100%; padding-left: 5px;">
                                                <asp:Label ID="Label1" runat="server" Text="Shipping Method" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                               <span> <b>NOTE: Orders placed before 3pm EST will be processed same day. Orders placed after
                                                    3pm EST will be processed the next business day. </b></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="2px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr width="100%">
                                            <td width="100%">
                                                <table width="100%" cellpadding="1" cellspacing="0" border="0" class="tblLabelStyle">
                                                    <tr width="100%">
                                                        <td width="20%" align="left">
                                                          <span>  Shipping&nbsp;Method: </span>
                                                        </td>
                                                        <td width="80%" align="left">
                                                          <span>  <asp:DropDownList ID="ddlShippingMethod" runat="server" Width="250px" CssClass="DropDown_Style">
                                                            </asp:DropDownList> </span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="left">
                                                         <span>   In&nbsp;Hands&nbsp;By: </span>
                                                        </td>
                                                        <td width="80%" align="left">
                                                          <span>  <asp:TextBox ID="txtInHandsBy" runat="server" Width="75px" Font-Size="12px"></asp:TextBox> </span>
                                                            <asp:ImageButton ImageAlign="Middle" runat="Server" ID="imgDate" ImageUrl="~/images/Calendar_scheduleHS.png"
                                                                AlternateText="Click to show calendar" />
                                                            <ajaxToolkit:CalendarExtender ID="calendarButtonExtender1" runat="server" TargetControlID="txtInHandsBy" 
                                                                OnClientShowing="showDate" PopupButtonID="imgDate" Format="MM/dd/yyyy" CssClass="cal_Theme1" />
                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" TargetControlID="txtInHandsBy"
                                                                Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                                OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                                                ErrorTooltipEnabled="True" />
                                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator5" runat="server" ControlExtender="MaskedEditExtender5"
                                                                ControlToValidate="txtInHandsBy" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                                Display="Dynamic" TooltipMessage="Input a date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                                ValidationGroup="MKE" />
                                                                &nbsp;&nbsp;<asp:Label ID="lblErrorHandby" runat="server" ForeColor="Red" Visible="false"></asp:Label>
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
                                            <td style="background-image: url('../Images/categoryHeader.JPG'); background-repeat: repeat-x;
                                                height: 25px; width: 100%; padding-left: 5px;">
                                                <asp:Label ID="Label2" runat="server" Text="Packaging" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="5px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr width="100%">
                                            <td width="100%">
                                                <table width="100%" cellpadding="1" cellspacing="0" border="0" class="tblLabelStyle">
                                                    <tr width="100%">
                                                        <td width="20%" align="left">
                                                          <span>  Packaging: </span>
                                                        </td>
                                                        <td width="80%" align="left">
                                                          <span>  <asp:DropDownList ID="ddlpackaging" runat="server" Width="250px" CssClass="DropDown_Style">
                                                            </asp:DropDownList> </span>
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
                                            <td style="background-image: url('../Images/categoryHeader.JPG'); background-repeat: repeat-x;
                                                height: 25px; width: 100%; padding-left: 5px;">
                                                <asp:Label ID="Label3" runat="server" Text="Enclosure" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr width="100%">
                                            <td width="100%">
                                                <table width="100%" cellpadding="5" cellspacing="0" border="0">
                                                    <tr width="100%">
                                                        <td width="20%" align="left">
                                                          <span>  Enclose: </span>
                                                        </td>
                                                        <td width="80%" align="left">
                                                            <asp:RadioButtonList ID="rblEnclose" runat="server" OnSelectedIndexChanged="cbl_SelectedIndexChanged"
                                                                AutoPostBack="true">
                                                                <asp:ListItem Value="S">Packing Slip</asp:ListItem>
                                                                <asp:ListItem Value="C">Gift Card</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <tr width="100%" id="trGift" visible="false" runat="server">
                                                        <td colspan="2" align="left">
                                                            <div align="left" style="width: 100%; text-align: left;">
                                                                <table border="0" cellpadding="2" cellspacing="1" width="100%" class="tblLabelStyle">
                                                                    <tr>
                                                                        <td colspan="2" style="background-image: url('../Images/categoryHeader.JPG'); background-repeat: repeat-x;
                                                                            height: 25px; width: 100%; padding-left: 5px;">
                                                                          <span>  <b>Greeting</b> </span>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="20%" align="left">
                                                                          <span>  Dear: </span>
                                                                        </td>
                                                                        <td width="80%" align="left">
                                                                          <span>  <asp:TextBox ID="txtDear" runat="server" Width="210px" Font-Size="12px"></asp:TextBox> </span>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                         <span>   Giftcard Text: </span>
                                                                        </td>
                                                                        <td>
                                                                         <span>   <asp:TextBox ID="txtGiftcard" runat="server" TextMode="MultiLine" Height="80px" Width="210px" Font-Size="12px"></asp:TextBox> </span>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2" style="background-image: url('../Images/categoryHeader.JPG'); background-repeat: repeat-x;
                                                                            height: 25px; width: 100%; padding-left: 5px;">
                                                                           <span> <b>Signature </b> </span>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                          <span>  Sincerely: </span>
                                                                        </td>
                                                                        <td>
                                                                          <span>  <asp:TextBox ID="txtSincerely" runat="server" Width="210px" Font-Size="12px"></asp:TextBox></span>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                           <span> Your Name: </span>
                                                                        </td>
                                                                        <td>
                                                                           <span> <asp:TextBox ID="txtYourName" runat="server" Width="210px" Font-Size="12px"></asp:TextBox> </span>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                         <span>   Title/Position: </span>
                                                                        </td>
                                                                        <td>
                                                                          <span>  <asp:TextBox ID="txtTitle" runat="server" Width="210px" Font-Size="12px"></asp:TextBox> </span>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>

                <tr id="tr_Confirm" runat="server" visible="false">
                    <td>
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <table width="98%" cellpadding="0" cellspacing="0" align="center" class="tblLabelStyle">
                                       
                                        <tr>
                                            <td style="background-image: url('../Images/categoryHeader.JPG'); background-repeat: repeat-x;
                                                height: 25px; width: 100%; padding-left: 5px;">
                                               <span> <b>Please check all order details and click CONFIRM ORDER to send your order for processing.</b> </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="10px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                             <span>   <asp:Label ID="lblUser" runat="server" Width="350px" Text="" Font-Bold="true"></asp:Label> </span>
                                            </td>
                                        </tr>
                                        <tr width="100%">
                                            <td width="100%">
                                                <table border="0" cellpadding="1" cellspacing="0" width="100%">
                                                    <tr width="100%">
                                                        <td width="50%" align="left" valign="top">
                                                            <table width="100%" cellpadding="0" cellspacing="0" border="0" style="vertical-align: top;" class="tblLabelStyle">
                                                                <tr>
                                                                    <td valign="top">
                                                                     <span>   <b>Billing Address</b> </span>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                       <span> <asp:Label ID="lblBillingAddress" runat="server" Width="350px" Text=""></asp:Label> </span>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td valign="top">
                                                            <table width="100%" cellpadding="0" cellspacing="0" border="0" style="vertical-align: top;" class="tblLabelStyle">
                                                                <tr>
                                                                    <td valign="top">
                                                                      <span>  <b>Shipping Address</b> </span>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                       <span> <asp:Label ID="lblShippingAddress" runat="server" Width="350px" Text=""></asp:Label> </span>
                                                                    </td>
                                                                </tr>
                                                            </table>
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
                                                <asp:GridView ID="gv_ViewCartCheckout" runat="server" AutoGenerateColumns="false"
                                                    GridLines="Both" CssClass="myGridView" RowStyle-CssClass="rowStyle" AlternatingRowStyle-CssClass="altRowStyle"
                                                    ShowFooter="true" HeaderStyle-CssClass="headerStyle" AllowSorting="true" AllowPaging="false"
                                                    Width="100%" Height="100%" OnRowDataBound="gv_ViewCartCheckout_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Item/SKU"  ControlStyle-Font-Size="9pt"
                                                            ItemStyle-Height="13px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblpart_num2" runat="server" Text='<%# bind("part_num") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                            </FooterTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>


                                                         <asp:TemplateField HeaderText="Product Desc"  ControlStyle-Font-Size="9pt"
                                                            ItemStyle-Height="13px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProductDesc" runat="server" Text='<%# bind("ProductDesc") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                            </FooterTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderText="Quantity"  ControlStyle-Font-Size="9pt"
                                                            ItemStyle-Height="13px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblqty2" runat="server" Text='<%# bind("qty") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                            </FooterTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Each"  ControlStyle-Font-Size="9pt"
                                                            ItemStyle-Height="13px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEach2" runat="server" Text='<%#bind("List_price") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTAmount2" runat="server" Text="Total Amount:" Font-Bold="true"
                                                                    Font-Size="10pt"></asp:Label></FooterTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total"  ControlStyle-Font-Size="9pt"
                                                            ItemStyle-Height="13px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblList_price2" runat="server" Text='<%#bind("List_price")*bind("qty") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTotalAmount2" runat="server" Text="" Font-Bold="true" Font-Size="10pt"></asp:Label>
                                                            </FooterTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                              <span>  <asp:Label ID="lblInHandBy1" runat="server" Width="350px" Text=""></asp:Label> </span>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                             <span>   <asp:Label ID="lblpackaging1" runat="server" Width="350px" Text=""></asp:Label> </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                              <span>  <asp:Label ID="lblEnclosure1" runat="server" Width="350px" Text=""></asp:Label> </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                              <span>  <asp:Label ID="lblamount1" runat="server" Width="350px" Text=""></asp:Label> </span>
                                            </td>
                                        </tr>
                                    </table>
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

                <tr>
                    <td align="right" style="text-align: right;">
                        <asp:LinkButton ID="lnkBack" runat="server" Text="Back" CssClass="addButtonStyle"
                            OnClick="lnkBack_Click"></asp:LinkButton>&nbsp;
                        <asp:LinkButton ID="lnkNext" runat="server" Text="Continue" CssClass="addButtonStyle"
                            OnClick="lnkNext_Click"></asp:LinkButton>
                    </td>
                </tr>

                <%--<tr>
                    <td>
                        <uc1:EmptyCart ID="ctlEmptyCart" OnEmptyCartBG="ctlEmptyCart_OnEmptyCartBG" runat="server" />
                        
                    </td>
                </tr>--%>
                <tr>
                    <td>
                        <uc2:PrepareShipment ID="ctlPrepareShipment" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="100%" cellpadding="0" cellspacing="0" runat="server" id="tblEmptyCart"
                            visible="false">
                            <tr>
                                <td>
                                    <table width="30%" cellpadding="5" cellspacing="5" align="center" class="tblLabelStyle"
>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                              <span>  <b>Your cart is empty. </b> </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>
                                                    <asp:LinkButton ID="lnkEmptCarbtn" runat="server" Text="Continue Shopping" CssClass="addButtonStyle"
                                                        Width="130px" OnClick="lnkEmptCarbtn_Click"></asp:LinkButton>
                                                </b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="40px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>

            </table>

        </td></tr>

      </table> 
      
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

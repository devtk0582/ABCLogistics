<%@ Page Title="Order Details" Language="C#" AutoEventWireup="true" CodeBehind="OrderDetail.aspx.cs" MasterPageFile="~/MasterPages/ABCLogisticsCMS.Master" Inherits="CMS.Orders.OrderDetail" %>
   <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
   <%@ Register Src="~/UserControls/ctlErrormsg.ascx" TagName="Errormsg" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="upOrderView" runat="server">
        <ContentTemplate>
     <table width="100%" cellpadding="0" cellspacing="0" >
        <tr>
            <td>
                <table  width="98%"  cellpadding="0" cellspacing="0" align="center">
                    <tr>
                        <td height="30px">
                        <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                        <td style="text-align :left;width:50% ">
                         <asp:Label ID="lblErrorMsg" CssClass="errMessage" runat="server" Visible="false"  Text=""></asp:Label>
                        </td>
                        <td align="right"  height="30px"  style="text-align :right; width:50%; padding-right:0px;" >
                           &nbsp;<asp:LinkButton ID="lbtnPrintInovoice" runat="server" 
                                                     CssClass="addButtonStyle" onclick="lbtnPrintInovoice_Click" >Print Invoice
                                            </asp:LinkButton>
                        </td>
                        </tr></table>
                          
                        </td>
                    </tr>
                    <tr >
                        <td align="left" style="background-image: url('../Images/categoryHeader.JPG'); background-repeat: repeat-x;
                                    height: 30px; width: 100%; ">
                           &nbsp;<asp:Label ID="lblheaderMsg"  runat="server" Font-Bold="true"  Text="ORDER DETAIL"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <table width="100%" cellpadding="0" cellspacing="0"  class="tblLabelStyle">
                                <tr>
                                    <td width="50%" valign="top" >
                                        <table width="100%" cellpadding="2" cellspacing="2">
                                            <tr>
                                                <td width="35%" >
                                                   <span> Order Number: </span>
                                                </td>
                                                <td>
                                                  <span>  <asp:Label ID="lblOrderNum" runat="server" Text=""></asp:Label></span>
                                                </td>
                                            </tr>
                                              <tr>
                                               <td>
                                                 <span>  Submitted on: </span>
                                                </td>

                                                <td>
                                        
                                                   <span>  <asp:Label ID="lblSubmittedOn" runat="server" Text=""></asp:Label></span>
                                                </td>
                                            </tr>
                                            <tr>
                                             <td>
                                            <span> Order Status:</span>
                                                </td>
                                                <td>
                                                     <span><asp:Label ID="lblOrderStatus" runat="server" Text=""></asp:Label></span>
                                                </td>
                                            </tr>
                                             <tr>
                                              <td>
                                                  <span>  Customer: </span>
                                                </td>
                                                <td>
                                                    <span> <asp:Label ID="lblCustomer" runat="server" Text=""></asp:Label>
                                                     <asp:Label ID="lblCustMail" runat="server" Text=""></asp:Label></span>
                                                </td>
                                            </tr>
                                              <tr>
                                              <td>
                                                <span>  Number of Items: </span>
                                                </td>
                                                <td>
                                                    <span> <asp:Label ID="lblNoOfItems" runat="server" Text=""></asp:Label></span>
                                                   
                                                </td>
                                            </tr>
                                              <tr>
                                              <td>
                                               <span> Requested Ship Method: </span>
                                                </td>
                                                <td>
                                                  <span>   <asp:Label ID="lblReqShipMethod" runat="server" Text=""></asp:Label></span>
                                                
                                                </td>
                                            </tr>
                                            <tr>
                                              <td>
                                           <span> Tracking#: </span>
                                                </td>
                                                <td>
                                                  <span>   <asp:Label ID="lblTrackingNo" runat="server" Text=""></asp:Label></span>
                                                
                                                </td>
                                            </tr>
                                              <tr>
                                              <td>
                                               <span> Quote Amount: </span>
                                                </td>
                                                <td>
                                                  <span>   <asp:Label ID="lblQuoteAmt" runat="server" Text=""></asp:Label></span>
                                                   
                                                </td>
                                            </tr>
                                            <tr>
                                              <td>
                                            <span>Total Discount: </span>
                                                </td>
                                                <td>
                                                  <span>   <asp:Label ID="lblTtlDisc" runat="server" Text=""></asp:Label></span>
                                                   
                                                </td>
                                            </tr>
                                            <tr>
                                              <td>
                                           <span>Billed to date: </span>
                                                </td>
                                                <td>
                                                   <span>  <asp:Label ID="lblBillingDate" runat="server" Text=""></asp:Label></span>
                                                   
                                                </td>
                                            </tr>
                                            <tr>
                                              <td>
                                            <span> Payment Method: </span>
                                                </td>
                                                <td>
                                                    <span> <asp:Label ID="lblPaymentMethod" runat="server" Text=""></asp:Label></span>
                                                   
                                                </td>
                                            </tr>
                                              <tr>
                                              <td valign="top">
                                       <span> Billing to:</span>
                                                </td>
                                                <td>
                                                <table width="100%">
                                                <tr>
                                                <td>
                                                 <span> <asp:Label ID="lblBillingName" runat="server" Text=""></asp:Label></span>
                                                </td></tr>
                                                <tr>
                                                
                                                <td>
                                                 <span> <asp:Label ID="lblBillAdd1" runat="server" Text=""></asp:Label></span>
                                                </td></tr>
                                                    <tr>
                                                
                                                <td>
                                                  <asp:Label ID="lblBillAdd2" runat="server" Text=""></asp:Label>
                                                </td></tr>
                                                   <tr>
                                                <td>
                                                 <span>  <asp:Label ID="lblBillCity" runat="server" Text=""></asp:Label></span>
                                                </td></tr>
                                                <tr>
                                                <td>
                                                  <span> <asp:Label ID="lblBillStateZip" runat="server" Text=""></asp:Label></span>
                                                </td></tr>
                                                <tr>
                                                
                                                <td>
                                                <span> <asp:Label ID="lblCountry" runat="server" Text=""></asp:Label></span>
                                                </td>
                                                </tr>
                                                </table>
                                                   
                                                   
                                                  
                                                    
                                                   
                                                </td>
                                            </tr>
                                              <tr>
                                              <td valign="top">
                                         <span> Shipping to: </span>
                                                </td>
                                                <td>
                                                  <table width="100%" class="tblLabelStyle">
                                                <tr>
                                                <td>
                                                <span>  <asp:Label ID="lblShipName" runat="server" Text=""></asp:Label></span>
                                                </td></tr>
                                                <tr>
                                                
                                                <td>
                                                <span>  <asp:Label ID="lblShipAdd1" runat="server" Text=""></asp:Label></span>
                                                </td></tr>
                                                <tr>
                                                <td>
                                                 <span>  <asp:Label ID="lblShipAdd2" runat="server" Text=""></asp:Label></span>
                                                </td></tr>
                                                    <tr>
                                                <td>
                                                  <span> <asp:Label ID="lblShippCity" runat="server" Text=""></asp:Label></span>
                                                </td></tr>
                                                <tr>
                                                
                                                <td>
                                                <span> <asp:Label ID="lblShipStateZip" runat="server" Text=""></asp:Label></span>
                                                </td>
                                                </tr>
                                                   <tr>
                                                
                                                <td>
                                                <span> <asp:Label ID="lblShipCountry" runat="server" Text=""></asp:Label></span>
                                                </td>
                                                </tr>
                                                </table>
                                                   
                                                </td>
                                            </tr>
                                           
                                        </table>
                                    </td>
                                    <td width="50%"  valign="top">
                                    <div style="background-color:#F2F2F2;"  >
                                       
                                                    <table width="100%" cellpadding="2" cellspacing="2" style="border:1 solid black" runat="server" id="tblOrderupdate">
                                                        <tr>
                                                             <td align="left" style="background-image: url('../Images/categoryHeader.JPG'); background-repeat: repeat-x;
                                    height: 30px; width: 100%;">
                            <asp:Label ID="Label1" runat="server" Font-Bold="true"  Text="Order Detail"></asp:Label>
                        </td>
                                                            
                                                        </tr>
                                                     
                                                     
                                                         <tr>
                                                            <td >
                                                            <table width="100%" cellpadding="5" cellspacing="0" border="0" class="tblLabelStyle">
                                <tr>
                                
                                    <td width="30%" align="left">
                                     <span>  Status:</span>
                                    </td>
                                    <td align="left">
                                              <asp:DropDownList ID="ddlStatus" CssClass="DropDown_Style" runat="server" 
                                                  onselectedindexchanged="ddlStatus_SelectedIndexChanged" AutoPostBack="true"  >
                                               <%--  <asp:ListItem Text="Select" Value="-1" Selected="True"></asp:ListItem>--%>
                                        <%--<asp:ListItem Text="New" Value="N"></asp:ListItem>
                                        <asp:ListItem Text="Processing" Value="P"></asp:ListItem>
                                        <asp:ListItem Text="Shipped" Value="S"></asp:ListItem>
                                        <asp:ListItem Text="Delivered" Value="D"></asp:ListItem>
                                        <asp:ListItem Text="Cancelled" Value="C"></asp:ListItem>--%>
                        </asp:DropDownList>
                       <%--  <asp:Label ID="lblStatus" runat="server" Width="140px" Text=""></asp:Label>--%>
                                    </td>
                                  
                                </tr>
                                   <tr>
                                   <td  style="text-align:left;" >
                                 <span> Comments: </span>
                                    </td>
                                    <td  align="left">
                                   <span>   <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" CssClass="input_text" Width="90%" ></asp:TextBox></span>
                                    </td>
                                  
                                </tr>
                                   <tr id="trShipMethod" runat="server" visible="false" >
                                    <td  style="text-align:left;" >
                      <span>Ship Method: </span>
                                    </td>
                                    <td  align="left">
                                       <asp:DropDownList ID="ddlShippMethod" CssClass="DropDown_Style" runat="server"  >
                                        
                        </asp:DropDownList>
                                    </td>
                                </tr>
                                
                                 
                                 <tr id="trShipDate" runat="server" visible="false" >
                                    <td  style="text-align:left;" >
                                  <span>  <asp:Label ID="lblShipDate" runat="server"   Text=""></asp:Label></span>
                          
                                    </td>
                                    <td  align="left">
                                  <span>   <asp:TextBox ID="txtInHandsBy" runat="server" CssClass="input_text" Width="75px"></asp:TextBox></span>
                          <asp:ImageButton ImageAlign="Middle" runat="Server" ID="imgDate" ImageUrl="~/images/Calendar_scheduleHS.png"
                                AlternateText="Click to show calendar" />
                            <ajaxToolkit:CalendarExtender ID="calendarButtonExtender1" runat="server" TargetControlID="txtInHandsBy" OnClientShowing="showDate" 
                                PopupButtonID="imgDate" Format="MM/dd/yyyy" CssClass="cal_Theme1"/>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" TargetControlID="txtInHandsBy"
                                Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                ErrorTooltipEnabled="True" />
                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator5" runat="server" ControlExtender="MaskedEditExtender5"
                                ControlToValidate="txtInHandsBy" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                Display="Dynamic" TooltipMessage="Input a date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                ValidationGroup="MKE" /> 
                                    </td>
                                </tr>
                                     <tr id="trTracking" runat="server" visible="false" >
                                    <td  style="text-align:left;" >
                            <span>Tracking#: </span>
                                    </td>
                                    <td  align="left">
                                    <span>  <asp:TextBox ID="txtTrackingNo" runat="server"  CssClass="input_text" ></asp:TextBox> </span>
                                    </td>
                                </tr>
                                  <tr id="trNotifyCustomer" runat="server" visible="false" >
                                    <td  style="text-align:left;" >
                                 <span> Notify Customer? </span>
                                    </td>
                                    <td  align="left">
                                  <asp:CheckBox ID="chkNotify" runat="server" />
                                    </td>
                                </tr>
                                 <tr>
                                    <td  colspan="2" style="text-align:right;" >
                                <asp:LinkButton ID="lbtnUpdate" runat="server" 
                                                     CssClass="addButtonStyle" onclick="lbtnUpdate_Click" >Update
                                            </asp:LinkButton>
                                          <%--  <asp:LinkButton ID="lbtnCancel" runat="server" 
                                                     CssClass="addButtonStyle" >CANCEL ORDER
                                            </asp:LinkButton>--%>
                                    </td>
                              
                                </tr>
                       
                              
                      
                                
                            </table>
                                                            </td>
                                                        </tr>
                                                        <tr><td height="1px"></td></tr>
                                                           <tr>
                                                           
                                                        <td width="100%">
                                                        
                                                            </td>
                                                        </tr>
                                                    </table>
                                                     <asp:Panel ID="pnlViewCart" ScrollBars="Auto"  runat="server" Width="100%"  BackColor="#F2F2F2">
                                                            <asp:GridView ID="gvNotes" runat="server"  AutoGenerateColumns="false" GridLines="Both"
                                                                CssClass="myGridView" RowStyle-CssClass="rowStyle" 
                                                                AlternatingRowStyle-CssClass="altRowStyle" ShowFooter="true"
                                                                HeaderStyle-CssClass="HeaderStyle" AllowSorting="true" 
                                                                 Width="100%" Height="100%"
                                                                 >
                                                              <Columns>
                                                               <asp:TemplateField HeaderText="Date"  ControlStyle-Font-Size="9pt"
                                                                    ItemStyle-Height="13px">
                                                                    <ItemTemplate>
                                                                            <asp:Label ID="lblNoteDate" runat="server" Text='<%# Bind("[Action Date]") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                      <FooterTemplate></FooterTemplate>
                                                                     <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="Action Type" ControlStyle-Font-Size="9pt"
                                                                    ItemStyle-Height="13px">
                                                                    <ItemTemplate>
                                                                            <asp:Label ID="lblNotify" runat="server" Text='<%# Bind("[Action Type]") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    
                                                                     <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>

                                                                 <asp:TemplateField HeaderText="Comments"   ControlStyle-Font-Size="9pt"
                                                                    ItemStyle-Height="13px">
                                                                    <ItemTemplate>
                                                                            <asp:Label ID="lblStatus" runat="server" Text='<%#Bind("Comment") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                     
                                                                     <HeaderStyle HorizontalAlign="Left" />
                                                                     <FooterStyle HorizontalAlign="Right"/>
                                                                </asp:TemplateField>

                                                                  
                                                            </Columns> 
                                                            </asp:GridView>
                                                            </asp:Panel>
                                         </div>       
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
                        <td align="left" style="background-image: url('../Images/categoryHeader.JPG'); background-repeat: repeat-x;
                                    height: 30px; width: 100%;">
                             &nbsp;<asp:Label ID="lblOrderItemsHead" runat="server" Font-Bold="true"  Text="ORDER ITEMS "></asp:Label>
                        </td>
                    </tr>
                     <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                      <tr valign="top">
                                <td colspan="2" style="padding: 5px 0 5px 0; ">
                                    <div class="divlbs">
                                        <asp:GridView ID="gvItems" runat="server" AutoGenerateColumns="False" BorderColor="Red"
                                            BorderStyle="Dashed" BorderWidth="0px" 
                                            CellPadding="3" EmptyDataText="No Record Found" GridLines="Both" 
                                            Width="100%" onrowdatabound="gvItems_RowDataBound"   >
                                            <HeaderStyle CssClass="gvHeaderStyle" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Product"  HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProduct" runat="server" Text='<%# bind("ProductDesc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="50%" />
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Color"  HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblColor" runat="server" Text='<%# bind("Color_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="15%" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Size"  HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSize" runat="server" Text='<%# bind("Size_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="15%" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Quantity"  HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQuantity" runat="server" Text='<%# bind("Qty") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%"  />
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Each"  HeaderStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEach" runat="server" Text='<%# bind("Price") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%" HorizontalAlign="Right"/>
                                                    <HeaderStyle  HorizontalAlign="Right" Width="5%"/>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Total" SortExpression="ShipDate" HeaderStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotal" runat="server" Text='<%# bind("TotalPrice") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%" HorizontalAlign="Right" />
                                                    <HeaderStyle  HorizontalAlign="Right" Width="5%"/>
                                                </asp:TemplateField>


                                            </Columns>
                                           <%-- <PagerSettings Mode="NumericFirstLast" FirstPageText="<<" LastPageText=">>" NextPageText=">"
                                                PreviousPageText="<" />--%>
                                            <PagerStyle CssClass="gvPagerStyle" />
                                            <RowStyle CssClass="gvRowStyle" />
                                            <AlternatingRowStyle CssClass="gvAltRowStyle" />
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>


                   
                    <tr valign="top" >
                        <td style="text-align:right;">
                            <table width="100%" cellpadding="5" cellspacing="0" border="0"  class="tblLabelStyle">
                                <tr>
                                  
                                    <td width="80%" align="right">
                                      <span>  Subtotal: </span>
                                    </td>
                                    <td width="20%" align="right">
                                       <span>   <asp:Label ID="lblSubTotal" runat="server" Font-Bold="true"  Text=""></asp:Label> </span>
                                    </td>
                                </tr>
                                   <tr>
                                
                                    <td align="right">
                                    <span>  Shipping:   <asp:Label ID="lblShipping" runat="server"  Text=""></asp:Label> </span>
                                    </td>
                                    <td  align="right">
                                   
                                        <span>  <asp:Label ID="lblShippingCost" runat="server" Font-Bold="true"  Text=""></asp:Label> </span>
                                    </td>
                                </tr>
                                   <tr>
                                  
                                    <td align="right">
                                      <span>  Total: </span>
                                    </td>
                                    <td  align="right">
                                        <span>  <asp:Label ID="lblTotalCost" runat="server" Font-Bold="true"  Text=""></asp:Label> </span>
                                    </td>
                                </tr>
                       
                              
                      
                                
                            </table>
                        </td>
                    </tr>
                    

                      <tr valign="top" >
                        <td style="text-align:left;" valign="top" >
                            <table width="100%" cellpadding="5" cellspacing="0" border="0"  class="tblLabelStyle">
                                <tr>
                                
                                    <td width="9%" align="left">
                                     <span>   Billing Code: </span>
                                    </td>
                                    <td width="40%" align="left">
                                         <span> <asp:Label ID="lblBillingCode" runat="server" Font-Bold="true"  Text=""></asp:Label> </span>
                                    </td>
                                       <td width="49%" >
                                      
                                    </td>
                                </tr>
                                   <tr>
                                   <td  align="left" >
                                    <span>  In Hand By: </span>
                                    </td>
                                    <td  align="left">
                                     <span> <asp:Label ID="lblInHandBy" Font-Bold="true"  runat="server"  Text=""></asp:Label> </span>
                                    </td>
                                      <td  >
                                      
                                    </td>
                                </tr>
                                   <tr>
                                  
                                    <td  align="left">
                                      <span>  Packaging: </span>
                                    </td>
                                    <td  align="left">
                                       <span>   <asp:Label ID="lblPackaging" runat="server" Font-Bold="true"  Text=""></asp:Label> </span>
                                    </td>
                                     <td  >
                                      
                                    </td>
                                </tr>
                       
                              
                      
                                
                            </table>
                        </td>
                    </tr>
                     <tr>
                        <td>
                           <uc1:Errormsg ID="ctlErrormsg" OnEmptyCartBG="ctlEmptyCart_OnEmptyCartBG" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

         </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
        

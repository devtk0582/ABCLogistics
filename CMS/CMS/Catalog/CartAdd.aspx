<%@ Page Title="View Cart" Language="C#" MasterPageFile="~/MasterPages/ABCLogisticsCMS.Master" AutoEventWireup="true"
    CodeBehind="CartAdd.aspx.cs" Inherits="CMS.Catalog.CartAdd" %>
    <%@ Register Src="~/UserControls/ctlEmptyCart.ascx" TagName="EmptyCart" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script language="javascript" type="text/javascript">

        function VisibleBtn(txtQty) {
            var btnUpdate = document.getElementById(txtQty);
            btnUpdate.style.visibility = "visible";
        }
    </script>

    <asp:UpdatePanel ID="upCart" runat="server">
        <ContentTemplate>
     <table width="100%" cellspacing="1" cellpadding="1" style="padding: 5px 10px 5px 10px; height: 500px;">
     <tr><td style="width: 100%; text-align: center; margin-top: 15px;">&nbsp;</td></tr>
                  <tr valign="top">
                    <td align="left">
                        <asp:Label ID="lblTitle" runat="server" Text="View Cart" CssClass="titleStyle"></asp:Label>
                    </td>
                    </tr>
                    <tr><td valign="top">
                <table width="100%" cellpadding="0" cellspacing="0" runat="server" id="tblAddCart" class="tblLabelStyle">
                
                <tr>
                    <td width="100%" align="center"> 
                        <asp:GridView ID="gvCart" runat="server" AutoGenerateColumns="false" GridLines="Both" BorderWidth="0px"
                                        CssClass="myGridView" RowStyle-CssClass="rowStyle" AlternatingRowStyle-CssClass="altRowStyle"
                                        HeaderStyle-CssClass="headerStyle" AllowSorting="true"
                                        AllowPaging="false" PageSize="20" Width="100%" ShowFooter="true"
                                        OnRowDataBound="gvCart_RowDataBound" OnPageIndexChanging="gvCart_OnPageIndexChanging">
                            <Columns>
                            <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                       
                                            <asp:Label ID="lblProdID" runat="server" Text='<%# bind("ProductID") %>'></asp:Label><br />
                                            <asp:Label ID="lblSizeID" runat="server" Text='<%# bind("Size_ID") %>'></asp:Label><br />
                                            <asp:Label ID="lblColorID" runat="server" Text='<%# bind("Color_id") %>'></asp:Label>
                                       
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Item/SKU" ControlStyle-Font-Size="9pt" ItemStyle-Height="13px" >
                                    <ItemTemplate>
                                            <asp:Label ID="lblManufacturer" runat="server" Text='<%# bind("ProductName") %>'></asp:Label>
                                    </ItemTemplate>
                                     <HeaderStyle HorizontalAlign="Left" />
                                     <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Product Desc" ControlStyle-Font-Size="9pt" ItemStyle-Height="13px">
                                    <ItemTemplate>
                                            <asp:Label ID="lblProductDesc" runat="server" Text='<%# bind("ProductDesc") %>'></asp:Label>
                                    </ItemTemplate>
                                     <HeaderStyle HorizontalAlign="Left" />
                                     <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Size" ControlStyle-Font-Size="9pt" ItemStyle-Height="13px">
                                    <ItemTemplate>
                                            <asp:Label ID="lblSizeName" runat="server" Text='<%# bind("Size_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                     <HeaderStyle HorizontalAlign="Left" />
                                     <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Color" ControlStyle-Font-Size="9pt" ItemStyle-Height="13px">
                                    <ItemTemplate>
                                            <asp:Label ID="lblColorName" runat="server" Text='<%# bind("Color_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                     <HeaderStyle HorizontalAlign="Left" />
                                     <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity" ControlStyle-Font-Size="9pt" ItemStyle-Height="13px">
                                    <ItemTemplate>
                                            <asp:TextBox ID="txtQty" runat="server" Width="30px" MaxLength="6" Text='<%# bind("Qty") %>'></asp:TextBox><br />
                                            <asp:Label ID="lblTQty" runat="server" Text='<%# bind("Qty") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                     <HeaderStyle HorizontalAlign="Left" />
                                     <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Each" ControlStyle-Font-Size="9pt" ItemStyle-Height="13px">
                                    <ItemTemplate>
                                           $<asp:Label ID="lblPrice" runat="server" Text='<%# bind("Price") %>'></asp:Label>
                                        
                                    </ItemTemplate>
                                    <FooterTemplate>
                                            <asp:Label ID="lblFoterText" runat="server" Font-Bold="true" Text="SubTotal"></asp:Label>
                                    </FooterTemplate>
                                     <HeaderStyle HorizontalAlign="Left" />
                                     <ItemStyle HorizontalAlign="Left" />
                                     <FooterStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total" ControlStyle-Font-Size="9pt" ItemStyle-Height="13px">
                                    <ItemTemplate>
                                           $<asp:Label ID="lblPrice2" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                          $<asp:Label ID="lblFoterAmt" runat="server" Font-Bold="true" Text=""></asp:Label>
                                    </FooterTemplate>
                                     <HeaderStyle HorizontalAlign="Left" />
                                     <ItemStyle HorizontalAlign="Left" />
                                      <FooterStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                            </Columns>
                             <PagerSettings Mode="NumericFirstLast" FirstPageText="<<" LastPageText=">>" NextPageText=">"
                                                PreviousPageText="<" />
                             <PagerStyle CssClass="gvPagerStyle" />
                                        
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" width="100%" class="tblLabelStyle">
                            <tr>
                                <td colspan="2">
                                    &nbsp; 
                                </td>
                            </tr>
                            <tr>
                                <td width="65%" align="left">
                                </td>
                                <td width="35%" align="right">
                                    <asp:LinkButton ID="lnkQtyUpdate" Style="visibility: hidden" runat="server" Text="Update"
                                        CssClass="addButtonStyle"  onclick="lnkQtyUpdate_Click"></asp:LinkButton>&nbsp;
                                </td>
                            </tr>

                             <tr>
                                <td colspan="2" align="left" >
                                  &nbsp;&nbsp;<asp:Label ID="ErrorMsg" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="65%" align="left">
                                    &nbsp; &nbsp;<asp:LinkButton ID="lnkEmptyCart" runat="server" Text="[Empty Cart]" 
                                        ForeColor="Black" Width="80px" onclick="lnkEmptyCart_Click"></asp:LinkButton><br />
                                    &nbsp;<asp:Label ID="lblcarttext" runat="server" Text="* Note: set Quantity to 0 to remove an item from your cart."></asp:Label>
                                </td>
                                <td width="35%" align="left">
                                </td>
                            </tr>
                             <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td width="65%" align="left">
                                </td>
                                <td width="35%" align="right">
                                <asp:LinkButton ID="lnlchkOutNow" runat="server" Text="Check Out Now"
                                        CssClass="addButtonStyle"  onclick="lnlchkOutNow_Click"></asp:LinkButton>&nbsp;
                                </td>
                            </tr>
                           


                            <tr>
                                <td colspan="2" height="100px">
                                  <%-- <uc1:EmptyCart ID="ctlEmptyCart"   OnEmptyCartBG ="ctlEmptyCart_OnEmptyCartBG"  runat="server" />--%>
                                  <uc1:EmptyCart ID="ctlEmptyCart"  runat="server" />
                                </td>
                            </tr>


                        </table>
                    </td>
                </tr>
            </table>
           

             <table width="100%" cellpadding="0" cellspacing="0" runat="server" id="tblEmptyCart" visible="false" class="tblLabelStyle">
             <tr><td>
             <table width="30%" cellpadding="5" cellspacing="5" align="center">
             
             <tr><td>&nbsp;</td></tr>
              <tr><td align="center"><b>Your cart is empty. </b></td></tr>
             
              <tr><td align="center"><asp:LinkButton ID="lnkEmptCarbtn" runat="server" 
                      Text="Continue Shopping" CssClass="addButtonStyle" 
                      onclick="lnkEmptCarbtn_Click"></asp:LinkButton> </td></tr>
               <tr><td height="40px">&nbsp;</td></tr>
              </table></td></tr>
             </table>

      </td></tr>
      </table> 
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

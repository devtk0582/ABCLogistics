<%@ Page Title="Products" Language="C#" MasterPageFile="~/MasterPages/ABCLogisticsCMS.Master" AutoEventWireup="true"
    CodeBehind="ProductSalesRep.aspx.cs" Inherits="CMS.Catalog.ProductSalesRep" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="upProd" runat="server">
        <ContentTemplate>
      
               <table width="100%" cellpadding="0" cellspacing="0" class="tblLabelStyle">
                <tr>
                    <td colspan="3" height="25px">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="center">
                       
                    </td>
                </tr>
                <tr>
                    <td width="58%" align="left">
                   
                        <asp:ListView runat="server" ID="ProductsDetails" GroupItemCount="1">
                            <LayoutTemplate>
                                <table cellpadding="2" runat="server" id="tblProducts" style="height: 220px" width="100%">
                                    <tr runat="server" id="groupPlaceholder">
                                    </tr>
                                </table>
                            </LayoutTemplate>
                            <GroupTemplate>
                                <tr runat="server" id="productRow" style="height: 80px">
                                    <td runat="server" id="itemPlaceholder">
                                    </td>
                                </tr>
                            </GroupTemplate>
                            <ItemTemplate>
                                <td id="Td1" valign="top" align="center" style="width: 100;" runat="server">
                                    <asp:Image runat="server" ID="imgproduct" ImageUrl='<%#"~/ShowProductImage.ashx?id=" + Eval("ImageID").ToString()%>'
                                        Height="250" Width="300" />
                                </td>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table  cellpadding="5" cellspacing="5" width="100%">
                                    <tr>
                                        
                                        <td align="center">
                                            <img src="../Images/ImgNotFound.jpg" height="250" width="300" />
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </td>
                    <td width="40%" align="left" valign="top">
                    <div style="margin-top: 20px; height:300px;">
                        <table width="70%" cellpadding="5" cellspacing="2" align="center" bgcolor="#FFEEAB" class="tblLabelStyle" >

                        
                            <tr>
                                <td style="margin-top: 20px;">
                                  <span>  &nbsp;<asp:Label ID="lblProducerName" runat="server" Font-Bold="true"></asp:Label><br />
                                    &nbsp;<asp:Label ID="lblProductDesc" runat="server" Font-Bold="true"></asp:Label><br />
                                    &nbsp;<asp:Label ID="lblProductName" runat="server" Font-Bold="true"></asp:Label></span>
                                </td>
                            </tr>
                           
                            <tr>
                                <td>
                                     &nbsp;<asp:DropDownList ID="ddlSize" runat="server"  AutoPostBack="true" CssClass="DropDown_Style"
                                        OnSelectedIndexChanged="ddlSize_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;<asp:DropDownList ID="ddlColor" runat="server"  AutoPostBack="true" CssClass="DropDown_Style"
                                        OnSelectedIndexChanged="ddlColor_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                  <span>  &nbsp;each: $
                                    <asp:Label ID="lblPrice" runat="server" Font-Bold="true"></asp:Label></span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                   <span>  &nbsp;Quntity:
                                      <asp:TextBox ID="txtDftQty" runat="server" Text="1" Width="30px" Font-Size="12px"></asp:TextBox> </span>
                                </td>
                            </tr>
                            <tr>
                                <td height="50px">
                                  <span>   <asp:Label ID="ErrorMsg" runat="server" ForeColor="Red" Visible="false" Font-Bold="true"></asp:Label></span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lnkbAddtoCart" runat="server" Text="ADD TO CART" CssClass="addButtonStyle"
                                         onclick="lnkbAddtoCart_Click"></asp:LinkButton>&nbsp;&nbsp;
                                    <asp:LinkButton ID="lnkbViewCart" runat="server" Text="VIEW CART" CssClass="addButtonStyle"
                                         onclick="lnkbViewCart_Click"></asp:LinkButton>
                                </td>
                            </tr>
                             <tr><td colspan="2" height="25px"><br /></td></tr>
                        </table>
                    </div>
                    </td>
                    <td width="2%">&nbsp;</td>
                </tr>
                <tr>
                    <td width="58%" align="left">
                        <asp:Repeater ID="rptProductImg" runat="server" OnItemCommand="rptProductImg_ItemCommand">
                            <HeaderTemplate>
                                <table width="50%" align="center">
                                    <tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <td align="center" style="float:left;">
                                    <asp:ImageButton ID="ProdImage" runat="server" ImageUrl='<%#"~/ShowProductImage.ashx?id=" + Eval("ImageID").ToString()%>'
                                        CommandArgument='<% #Eval("ImageID")%>' CommandName="Click" Height="60" Width="60" />
                                </td>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tr> </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </td>
                    <td width="42%" align="left">
                    &nbsp;
                    </td>
                    <td width="2%">&nbsp;</td>
                </tr>

               <tr><td colspan="3">&nbsp;</td></tr>
              
            </table>

        
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

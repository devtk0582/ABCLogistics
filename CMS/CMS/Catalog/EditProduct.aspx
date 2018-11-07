<%@ Page Title="Add / Edit Product" Language="C#" MasterPageFile="~/MasterPages/ABCLogisticsCMS.Master"
    AutoEventWireup="true" CodeBehind="EditProduct.aspx.cs" Inherits="CMS.Catalog.EditProdcut" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkUploadImage" />
        </Triggers>
        <ContentTemplate>
            <table width="100%" cellspacing="1" cellpadding="1" style="padding: 5px 10px 5px 10px;
                height: 100%;">
                <tr>
                    <td align="center">
                        <div style="width: 100%; text-align: center; margin-top: 15px;">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
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
                <tr valign="top" style="padding-bottom: 5px;">
                    <td>
                        <asp:Label ID="lblInventoryManager" CssClass="titleStyle" runat="server" Text="Inventory Manager"></asp:Label>
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <asp:HiddenField ID="hfInventoryID" runat="server" />
                        <table class="categoryLayout">
                            <tr>
                                <td style="width: 35px;">
                                    <asp:Label ID="lblSKU" CssClass="categoryFontLayout" runat="server" Text="SKU:"></asp:Label>
                                </td>
                                <td style="width: 200px;">
                                    <asp:Label ID="lblSKUData" CssClass="categoryFontLayout" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblCategory" CssClass="categoryFontLayout" runat="server"></asp:Label>
                                </td>
                                <td align="right" style="padding-left: 3px;">
                                    <table>
                                        <tr>
                                            <td align="center">
                                                <%--<asp:LinkButton ID="lnkbtnRemove" CssClass="addButtonStyle" OnClick="RemoveProduct_Click"
                                                    runat="server" OnClientClick="return confirm('Are you sure you want to delete this product?');">Remove</asp:LinkButton>--%>
                                            </td>
                                            <td align="center">
                                                <asp:LinkButton ID="lnkbtnEdit" CssClass="addButtonStyle" runat="server" OnClick="EditProduct_Click"
                                                    Visible="false">Update</asp:LinkButton>
                                            </td>
                                            <td align="center">
                                                <asp:LinkButton ID="lnkAddNewProduct" OnClick="AddProduct_Click" CssClass="addButtonStyle"
                                                    runat="server">Add</asp:LinkButton>
                                            </td>
                                            <td align="center">
                                                <asp:LinkButton ID="lnkbtnCancel" CssClass="addButtonStyle" OnClick="CancelEditProduct_Click"
                                                    runat="server">Cancel</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <asp:Label ID="lblErr" Visible="false" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                        <asp:Label ID="lblErrProduct" CssClass="errMessage" runat="server"></asp:Label>
                        <table style="width: 100%; padding: 0px 0px 0px 0px;"  class="tblLabelStyle">
                            <tr>
                                <%--Left panel for Product--%>
                                <td style="width: 65%; padding-left:0px;">
                                    <table style="width: 90%;">
                                        <tr>
                                            <td style="width: 80px; text-align: left;">
                                                <asp:Label ID="lblPrdTitle" runat="server" Text="Product Title: "></asp:Label>
                                            </td>
                                            <td style="width: 90px;">
                                                <asp:TextBox ID="txtbxPrdTitle" runat="server" MaxLength="50"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblSKUProd" runat="server" Text="SKU: "></asp:Label>
                                            </td>
                                            <td style="width: 90px;">
                                                <asp:TextBox ID="txtbxSKU" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 90%;">
                                        <tr>
                                            <td style="margin-left: 2px;">
                                                <asp:Label ID="lblDesc" runat="server" Text="Description:"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="margin-left: 2px;">
                                                <asp:TextBox ID="txtbxDesc" CssClass="desctxtbx" runat="server" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="hfCategory" runat="server" Value='<%# bind("Category_Id") %>' />
                                    <table style="width: 90%;">
                                        <tr>
                                            <td style="width: 60px; margin-left: 2px;">
                                                <asp:Label ID="lblCat" runat="server" Text="Category:"></asp:Label>
                                            </td>
                                            <td style="width: 120px;">
                                                <asp:DropDownList ID="ddlCategory" Width="120px" runat="server" AutoPostBack="true"
                                                    OnSelectedIndexChanged="PopulateSubcat_SelectChange">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 30px;">
                                                <asp:Image ID="imgArrow" runat="server" CssClass="arrowImg" ImageUrl="~/Images/arrow.jpg" />
                                            </td>
                                            <td style="width: 80px;">
                                                <asp:Label ID="Label1" runat="server" Text="Subcategory:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlSubcategory" Width="120px" AutoPostBack="true" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 90%;">
                                        <tr>
                                            <td style="width: 15px;">
                                                <asp:Label ID="lblLive" runat="server" Text="Live:"></asp:Label>
                                            </td>
                                            <td style="width: 65px;">
                                                <asp:CheckBox ID="chkBxLive" runat="server" />
                                            </td>
                                            <td style="width: 50px;">
                                                <asp:Label ID="LabelFeature" runat="server" Text="Featured:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkBxFeatured" CssClass="checkBx" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <%--Right panel for Manufacturer--%>
                                <td style="width: 50%">
                                    <table style="width: 50%;">
                                        <tr style="height: 30px;">
                                            <td>
                                                <asp:Label ID="lblManuftr" CssClass="pdtDesptRlbl" runat="server" Text="Manufacturer:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlMfr" runat="server" Width="120px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr style="height: 30px;">
                                            <td>
                                                <asp:Label ID="lblPrice" CssClass="pdtDesptRlbl" runat="server" Text="Price : "></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPrice" runat="server" Width="115px" MaxLength="6"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr style="height: 30px;">
                                            <td>
                                                <asp:Label ID="lblListPrice" CssClass="pdtDesptRlbl" runat="server" Text="List Price:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtListPrice" runat="server" Width="115px" MaxLength="6"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr style="height: 30px;">
                                            <td>
                                                <asp:Label ID="lblWeight" CssClass="pdtDesptRlbl" runat="server" Text="Weight(LB):"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtWeight" runat="server" Width="115px"  MaxLength="6"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr style="height: 30px;">
                                            <td>
                                                <asp:Label ID="lblShipClass" CssClass="pdtDesptRlbl" runat="server" Text="Shipping Class:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlShipClass" runat="server" Width="120px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <table width="100%" class="tblLabelStyle">
                            <tr>
                                <td>
                                    <%--User to add new inventory button--%>
                                    <asp:Panel ID="PanelAddNewInventoryButton" runat="server" Width="100%">
                                        <table class="categoryLayout">
                                            <tr>
                                                <td style="width: 80%;">
                                                    <asp:Label ID="Label4" runat="server" Text="INVENTORY" CssClass="inventorylbl"></asp:Label>
                                                </td>
                                                <td align="right">
                                                    <%--<asp:LinkButton ID="LinkButton2" runat="server" OnClick="OpenAddInventoryPanel_Click"
                                                        CssClass="addButtonStyle">Add New Inventory</asp:LinkButton>--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblErrInventory" runat="server" CssClass="errMessage"></asp:Label>
                                </td>
                            </tr>
                          <%--  <tr>
                                <td>
                                    <asp:Panel ID="panelAddnewInventory" runat="server" Visible="false">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label5" runat="server" Text="Size:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlnewInventoriesSize" runat="server" Style="width: 100px;">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label6" runat="server" Text="Color:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlnewInventoriesColor" runat="server" Style="width: 100px;">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label13" runat="server" Text="QTY"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtnewInventoriesQTY" MaxLength="6" runat="server" Style="width: 40px;"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lnkOpenWindow" CssClass="addButtonStyle" runat="server" OnClick="AddNewInventory_Click">Add</asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>--%>
                            <tr>
                                <td>
                                    <asp:Panel Height="100px" ID="panelDisplayInventory" runat="server" ScrollBars="Auto">
                                        <asp:UpdatePanel ID="UpdatePanelDisplayInventory" runat="server">
                                            <ContentTemplate>
                                                <asp:Repeater ID="repeaterInventories" runat="server" OnItemDataBound="repeater_OnItemDataBound"
                                                    OnItemCommand="repeaterInventories_OnItemCommand">
                                                    <HeaderTemplate>
                                                        <table width="100%"  cellpadding="3" cellspacing ="0">
                                                          
                                                    </HeaderTemplate>
                                                     <ItemTemplate>
                                                    <tr class="gvRowStyle" >
                                                        <td align="left" width="10%">
                                                            <asp:Label ID="lblInvSKU" runat="server" Text='<%# bind("SKU") %>' Style="padding: 0px 0px 0px 0px;"></asp:Label>
                                                        </td>
                                                        <td align="left" style="text-align: left;" width="10%">
                                                            <asp:HiddenField ID="hfInventoriesSize" runat="server" Value='<%# bind("Size_ID") %>' />
                                                            <asp:HiddenField ID="hfInventoriesColor" runat="server" Value='<%# bind("Color_Id") %>' />
                                                            <asp:HiddenField ID="hfInventoriesExt2ID" runat="server" Value='<%# bind("inv_Template_ext2_ID") %>' />
                                                            <asp:HiddenField ID="hfInventoriesTemplateID" runat="server" Value='<%# bind("inv_template_id") %>' />
                                                            <asp:Label ID="lblInventorySize" runat="server" Font-Bold="true" Text="Size:" Style="padding: 0px 0px 0px 0px;"></asp:Label>
                                                        </td>
                                                        <td align="left" style="text-align: left;" width="10%">
                                                            <asp:Label ID="lblSize" runat="server" Text='<%# bind("Size_Name") %>'></asp:Label>
                                                        </td>
                                                        <td align="left" style="text-align: left;" width="10%">
                                                            <asp:Label ID="lblInventoryColor" runat="server" Font-Bold="true" Text="Color:" Style="padding: 0px 0px 0px 0px;"></asp:Label>
                                                        </td>
                                                        <td align="left" style="text-align: left;" width="10%">
                                                            <asp:Label ID="lblColor" runat="server" Text='<%# bind("Color_Name") %>'></asp:Label>
                                                        </td>
                                                        <td align="left" style="text-align: left;" width="10%">
                                                            <asp:Label ID="lblInventoryQuantity" runat="server" Font-Bold="true" Text="Quantity:"
                                                                Style="padding: 0px 0px 0px 0px;"></asp:Label>
                                                        </td>
                                                        <td align="left" style="text-align: left;" width="10%">
                                                            <asp:Label ID="Label1" runat="server" Text='<%# bind("Qty") %>'></asp:Label>
                                                            <asp:HiddenField ID="hfQty" Value='<%# Eval("Qty") %>' runat="server" />
                                                        </td>
                                                        <td align="left" style="text-align: left;" width="30%">
                                                       
                                                            <asp:LinkButton ID="lnkInventoriesRemove" CssClass="addButtonStyle" runat="server"
                                                                Visible="false">Remove</asp:LinkButton>
                                                      
                                                       
                                                            <asp:Label ID="lblErrMessage" runat="server" CssClass="errMessage"></asp:Label>
                                                            <asp:Label ID="lblMessage" Font-Bold="true" runat="server" Font-Names="Verdana" Font-Size="Small"
                                                                ForeColor="Black">
                                                            </asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>

                                               
                                                    <FooterTemplate>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <table width="100%" class="tblLabelStyle">
                            <tr>
                                <td>
                                    <asp:Panel ID="panelAddNewImage" runat="server">
                                        <table class="categoryLayout">
                                            <tr>
                                                <td style="width: 80%;">
                                                    <asp:Label ID="lblAddNewImage" runat="server" Text="ADD IMAGE" CssClass="inventorylbl"></asp:Label>
                                                </td>
                                                <td align="right">
                                                    <asp:LinkButton ID="lnkAddNewImage" runat="server" OnClick="OpenAddImagePanel_Click"
                                                        CssClass="addButtonStyle">Add New Image</asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                        <%--Popup window for add image--%>
                                        <input id="popupAddImage" type="button" style="display: none" runat="server" />
                                        <asp:ModalPopupExtender ID="MdlPopupImage" runat="server" TargetControlID="popupAddImage"
                                            PopupControlID="pnlPopupImage" BackgroundCssClass="popupBG" DropShadow="true">
                                        </asp:ModalPopupExtender>
                                        <asp:Panel ID="pnlPopupImage" runat="server">
                                            <table class="PopupFormBG" width="80%">
                                                <tr style="height: 30px;">
                                                    <td colspan="2" class="PopupFormTitleStyle">
                                                        <asp:Label ID="lblNewImage" runat="server" Font-Bold="True"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="lblErrorImage" runat="server" CssClass="errMessage"></asp:Label>
                                                    </td>
                                                </tr>
                                                <%--                        <tr>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="File Name:" Width="100px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNewFileName" runat="server"></asp:TextBox>
                            </td>
                        </tr>--%>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label10" runat="server">Color:</asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlNewColor" runat="server" Style="width: 120px;">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblImage" runat="server" Text="Image:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:FileUpload ID="fileUploadImage" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label2" runat="server" Text="Default Image:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkDefaultImage" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr style="height: 50px;">
                                                    <td align="right" colspan="2">
                                                        <table width="30%">
                                                            <tr>
                                                                <td align="center">
                                                                    <asp:LinkButton ID="lnkUploadImage" runat="server" CssClass="addButtonStyle" OnClick="SaveImage_Click">Save</asp:LinkButton>
                                                                </td>
                                                                <td align="center">
                                                                    <asp:LinkButton ID="lnkCanelUploadImage" CssClass="addButtonStyle" onClick="CancelImage_Click"  runat="server">Cancel</asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel Height="100px" ID="panelDisplayImages" runat="server" ScrollBars="Auto">
                                        <asp:Label ID="lblErrImage" runat="server" CssClass="errMessage"></asp:Label>
                                        <asp:HiddenField ID="hfInventoriesID" runat="server" Value='<%# Eval("inv_template_id") %>' />
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:Repeater ID="repeaterInventoriesImages" runat="server" OnItemCommand="repeaterImage_OnItemCommand">
                                                    <HeaderTemplate>
                                                        <table>
                                                            <tr>
                                                                <%--       <td style="width:150px;">                                            
                                            <asp:Label ID="lblInventoryFileName" runat="server" Text="File Name:"></asp:Label> 
                                        </td>--%>
                                                                <td style="width: 100px;">
                                                                    <asp:Label ID="lblInventoriesColor" runat="server" Text="Color"></asp:Label>
                                                                </td>
                                                                <td style="width: 100px;">
                                                                    <asp:Label ID="lblInventoriesImage" runat="server" Text="Image"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <tr>
                                                                        <asp:HiddenField ID="hfImageInveTemplateID" runat="server" Value='<%# bind("inv_template_id") %>' />
                                                                        <asp:HiddenField ID="hfInventoriesImageID" runat="server" Value='<%# Eval("inv_template_image_id") %>' />
                                                                    </tr>
                                                                </td>
                                                                <%--<td style="width:150px;">
                                            <asp:Label ID="lblInventoriesFileName" runat="server" Text='<%# Eval("inv_template_name").ToString() %>'></asp:Label>
                                        </td> --%>
                                                                <td style="width: 100px;">
                                                                    <asp:Label ID="lblInvenotiresColor2" runat="server" Text='<%# Eval("Color_Name") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 100px;">
                                                                    <asp:Image ID="imgInventoryImage" runat="server" ImageUrl='<%#"~/ShowProductImage.ashx?id=" + Eval("inv_template_image_id").ToString()%>'
                                                                        Height="90" Width="75" />
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="lnkImageEdit" CssClass="addButtonStyle" CommandName="editImages"
                                                                        runat="server" CommandArgument='<%# bind("inv_template_image_id") %>'>Update</asp:LinkButton>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="lnkRemoveImage" CssClass="addButtonStyle" runat="server" CommandName="removeImage"
                                                                        CommandArgument='<%# bind("inv_template_image_id") %>'>Remove</asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

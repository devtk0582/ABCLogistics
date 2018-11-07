<%@ Page Title="Products" Language="C#" MasterPageFile="~/MasterPages/ABCLogisticsCMS.Master" AutoEventWireup="true"
    CodeBehind="Products.aspx.cs" Inherits="CMS.Catalog.Products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="background-color: #C3CBD6; margin-top: 20px;">
        <table width="100%" class="tblLabelStyle">
            <tr>
                <td align="left">
                    <table width="100%">
                        <tr style="height: 20px;">
                            <td width="60">
                                <asp:Label ID="Label1" runat="server" Text="Search:" ForeColor="Black"></asp:Label>
                            </td>
                            <td width="180">
                                <asp:DropDownList ID="ddlCategories" runat="server" Width="160">
                                </asp:DropDownList>
                            </td>
                            <td width="280">
                                <asp:TextBox ID="txtSearch" runat="server" Width="250"></asp:TextBox>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbSearch" runat="server" Font-Underline="false" Text="GO"
                                    CssClass="addButtonStyle" OnClick="lbSearch_Click"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
                <td align="right">
                    <asp:LinkButton ID="lbSwitchRole" runat="server" 
                        Text="Log In As Sales Representative" CssClass="addButtonStyle" 
                        Font-Underline="false" onclick="lbSwitchRole_Click" Visible="false"></asp:LinkButton> 
                </td>
                <%--<td align="right">
                    <asp:LinkButton ID="lbAddProduct" runat="server" Text="Add Product" CssClass="addButtonStyle"
                        Font-Underline="false" Visible="false" onclick="lbAddProduct_Click"></asp:LinkButton>
                </td>--%>
            </tr>
        </table>
    </div>
    <div>
        <table width="100%" class="tblLabelStyle">
            <tr>
                <td width="20%" valign="top">
                    <asp:Panel ID="plCategories" Height="500px" runat="server" ScrollBars="Auto">
                        <table width="100%" runat="server" id="tblSideBar">
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblSideBar" runat="server" Font-Bold="true"></asp:Label>
                                <div style="height: 5px;">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:LinkButton ID="lbShowAllCategories" runat="server" Text="All Categories" Font-Bold="true"
                                    Font-Underline="false" OnClick="lbShowAllCategories_Click"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Repeater ID="rptCategories" runat="server" OnItemDataBound="rptCategories_ItemDataBound"
                                    OnItemCommand="rptCategories_ItemCommand">
                                    <HeaderTemplate>
                                        <table width="100%">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td align="left">
                                                <asp:HiddenField ID="hfCategoryID" runat="server" Value='<%#Eval("Category_Id").ToString()%>' />
                                                <asp:LinkButton ID="lbCategory" runat="server" Font-Bold="true" Text='<%#Eval("Category_Name").ToString()%>'
                                                    CommandArgument='<%#Eval("Category_Id").ToString()%>' CommandName="Main" Font-Underline="false"></asp:LinkButton>
                                            </td>
                                        </tr>
                                        <tr style="margin-left: 10px">
                                            <td>
                                                <asp:Repeater ID="rptSubcategories" runat="server" OnItemCommand="rptSubcategories_ItemCommand">
                                                    <HeaderTemplate>
                                                        <table width="100%">
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:HiddenField ID="hfSubcategoryID" runat="server" Value='<%#Eval("Subcategory_Id").ToString()%>' />
                                                                <asp:LinkButton ID="lbSubcategory" runat="server" Text='<%#Eval("Subcategory_Name").ToString()%>'
                                                                    CommandArgument='<%#Eval("Subcategory_Id").ToString()%>' CommandName="Sub" Font-Underline="false"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                    </table>
                    </asp:Panel>       
                </td>
                <td width="80%" valign="top">
                    <table width="100%" cellpadding="0" cellspacing="0" class="tblLabelStyle">
                        <tr valign="top">
                            <td align="left" colspan="3" style="padding: 5px 0px 5px 10px;">
                                <asp:Label ID="lblNavBar" runat="server" Font-Bold="true" Font-Size="12px" ForeColor="#668FCC"></asp:Label>
                            </td>
                        </tr>
                        <tr valign="middle" style="padding: 10px 5px 10px 5px;background:#FFF380 url('../Images/gridViewHeader.jpg') repeat-x top;">
                            <td align="left" width="50px" style="padding: 5px 0px 5px 10px;">
                               <asp:Label ID="Label4" runat="server" Text="Sort By:"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlSoryBy" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSoryBy_SelectedIndexChanged">
                                    <asp:ListItem Text="Manufacturer" Value="MAN"></asp:ListItem>
                                    <asp:ListItem Text="Price High To Low" Value="PHL"></asp:ListItem>
                                    <asp:ListItem Text="Price Low To High" Value="PLH"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                <asp:DataPager runat="server" ID="dpProducts" PageSize="12" PagedControlID="ProductsListView">
                                    <Fields>
                                        <asp:NumericPagerField ButtonCount="3" PreviousPageText="<--" NextPageText="-->"
                                            NumericButtonCssClass="numeric_button" CurrentPageLabelCssClass="current_page" />
                                    </Fields>
                                </asp:DataPager>
                            </td>
                        </tr>
                <tr style="height:10px;">
                    <td colspan="3">
                        <asp:Label ID="lblErr" Visible="false" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                        <tr valign="top">
                            <td colspan="3" width="100%">
                                <asp:Label ID="lblProductMsg" runat="server"></asp:Label>
                               <asp:ListView runat="server" ID="ProductsListView" GroupItemCount="4" OnPagePropertiesChanging="ProductsListView_PagePropertiesChanging"
                                    OnItemCommand="ProductsListView_ItemCommand">
                                    <LayoutTemplate>
                                        <table cellpadding="2" runat="server" id="tblProducts" style="height: 320px" width="100%">
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
                                        <td id="Td1" valign="top" align="center" style="width: 100" runat="server">
                                            <asp:ImageButton ID="ProductImage" runat="server" ImageUrl='<%#"~/ShowProductImage.ashx?id=" + Eval("ImageID").ToString()%>'
                                                CommandArgument='<%#Eval("ProductID")%>' CommandName="View" Height="100" Width="80" />
                                            <br />
                                            <asp:LinkButton ID="lbViewProduct" runat="server" CommandArgument='<% #Eval("ProductID")%>' CommandName="View" Text='<% #Eval("ProductName")%>'></asp:LinkButton>
                                            <br />
                                            <asp:Label ID="Label5" runat="server" Text='<%# String.Format("{0:C}", Eval("Price"))%>'></asp:Label>
                                        </td>
                                    </ItemTemplate>
                                </asp:ListView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

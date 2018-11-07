<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SBACMS.Master" AutoEventWireup="true" CodeBehind="Catalog1.aspx.cs" Inherits="CMS.Catalog1" %>
<%@ Register TagPrefix="UC" TagName="UserControl" Src="~/UserControls/SizesAddNewSizeControl.ascx" %>  

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server" >
    <div>

   <%--<div class="sub-menu-link">
        <div class="sub-menu-container">
            <div class="sub-menu">Products</div>
            <div class="sub-menu">Inventory Manager</div>
            <div class="sub-menu">Categories</div>
            <div class="sub-menu">Colors</div>
            <div class="sub-menu">Sizes</div>
       </div>
    </div>--%>

   <div class="searchLayout">
        <div class="searchHeader">
            <div style="float:left; padding:5px 0px 5px 0px;">
                <asp:Label ID="Label1" runat="server" Text="Search:" ></asp:Label>
            </div>
            <div style="float:left; padding:5px 10px 5px 10px;">
                <asp:DropDownList ID="ddlSearch" runat="server" CssClass="searchDDL">
                </asp:DropDownList>
            </div>
            <div style="float:left; padding:5px 0px 5px 0px;">
                <asp:TextBox ID="tbSearch" runat="server" CssClass="searchTB"></asp:TextBox>
            </div>
            <div style="float:left; padding:1px 0px 1px 5px;">            
                <asp:ImageButton ID="ImageButton1" runat="server" CssClass="searchButton" 
                    ImageUrl="~/Images/buttonSearch.png" />
            </div>
            <div style="float:right; padding:5px 0px 5px 0px;">
                <asp:Button ID="Button1" runat="server" Text="Add New Product" OnClick="AddNewProduct_Click" CssClass="addNewProdtBtn" />
            </div>
        </div>    
    </div>

    <div class="selectedCategory">
        <p>This part will display the category selected by user!!! I gona use USER CONTROL HERE!!!</p>
    </div>
    
   <div class="content">
        <div class="sideMenu">
            <div class="sideMenuHeaderLayout" align="center">
                <asp:Label ID="lblBrwsCat" CssClass="sideMenuHeader" runat="server" Text="Browse Categories"></asp:Label>
            </div>
            
            <div class="categories" style="padding-Top:5px;">                             
                <asp:LinkButton ID="lnkbtnMen" CssClass="mainCategory" runat="server">Men</asp:LinkButton>
                <div><asp:LinkButton ID="lnkbtnMenPolo" CssClass="subCategory" runat="server">Polo</asp:LinkButton></div>
                <div><asp:LinkButton ID="lnkbtnMenShirts" CssClass="subCategory" runat="server">Shirts</asp:LinkButton></div>
                <div><asp:LinkButton ID="lnkbtnMenPants" CssClass="subCategory" runat="server">Pants</asp:LinkButton></div>
                <div><asp:LinkButton ID="lnkbtnMenShoes" CssClass="subCategory" runat="server">Shoes</asp:LinkButton></div>
                <div><asp:LinkButton ID="lnkbtnMenSuits" CssClass="subCategory" runat="server">Suits</asp:LinkButton></div>
                <div><asp:LinkButton ID="lnkbtnMenTies" CssClass="subCategory" runat="server">Ties</asp:LinkButton></div>
                <div><asp:LinkButton ID="lnkbtnMenAccessories" CssClass="subCategory" runat="server">Accessories</asp:LinkButton></div>
            </div>

            <div class="categories">
                <asp:LinkButton ID="lnkbtnWomen" CssClass="mainCategory" runat="server">Women</asp:LinkButton>
                <div><asp:LinkButton ID="lnkbtnWomenTops" CssClass="subCategory" runat="server">Tops</asp:LinkButton></div>
                <div><asp:LinkButton ID="lnkbtnWomenJeans" CssClass="subCategory" runat="server">Jeans</asp:LinkButton></div>
                <div><asp:LinkButton ID="lnkbtnWomenBoots" CssClass="subCategory" runat="server">Boots</asp:LinkButton></div>
                <div><asp:LinkButton ID="lnkbtnWomenShoes" CssClass="subCategory" runat="server">Shoes</asp:LinkButton></div>
                <div><asp:LinkButton ID="lnkbtnWomenCoat" CssClass="subCategory" runat="server">Coats & Outerwear</asp:LinkButton></div>
                <div><asp:LinkButton ID="lnkbtnWomenDresses" CssClass="subCategory" runat="server">Dresses</asp:LinkButton></div>
                <div><asp:LinkButton ID="lnkbtnWomenSweaters" CssClass="subCategory" runat="server">Sweaters</asp:LinkButton></div>
                <div><asp:LinkButton ID="lnkbtnWomenSkirts" CssClass="subCategory" runat="server">Skirts</asp:LinkButton></div>
            </div>

            <div class="categories">
                <asp:LinkButton ID="lnkbtnKid" CssClass="mainCategory" runat="server">Kid</asp:LinkButton>
                <div><asp:LinkButton ID="lnkbtnKidPolo" CssClass="subCategory" runat="server">Polo</asp:LinkButton></div>
                <div><asp:LinkButton ID="lnkbtnKidShirts" CssClass="subCategory" runat="server">Shirts</asp:LinkButton></div>
                <div><asp:LinkButton ID="lnkbtnKidPants" CssClass="subCategory" runat="server">Pants</asp:LinkButton></div>
                <div><asp:LinkButton ID="lnkbtnKidShoes" CssClass="subCategory" runat="server">Shoes</asp:LinkButton></div>
                <div><asp:LinkButton ID="lnkbtnKidJeans" CssClass="subCategory" runat="server">Jeans</asp:LinkButton></div>
            </div>
        </div>

        <div class="displayContent">
            <div class="categoryLayout">
                <div class="categoryInfo" >
                    <asp:Label ID="lblSKU" CssClass="categoryFontLayout" runat="server" Text="SKU:"></asp:Label>
                </div>
                <div class="categoryInfo" >
                    <asp:Label ID="lblSKUData" CssClass="categoryFontLayout" runat="server" Text="This is a label for SKU!!!"></asp:Label>
                </div>
                <div class="categoryInfo" style="margin-left:20px;" >
                    <asp:Label ID="lblCategory" CssClass="categoryFontLayout" runat="server" Text="Catgory selected!!!"></asp:Label>
                </div>
                <div class="categoryInfoButton">
                    <asp:LinkButton ID="lnkbtnRemove" CssClass="categoryBtn" runat="server">Remove</asp:LinkButton>
                </div>
                <div class="categoryInfoButton">
                    <asp:LinkButton ID="lnkbtnEdit" CssClass="categoryBtn" runat="server">Edit</asp:LinkButton>                    
                </div>
            </div>

            <div class="pdtDescription">             
                <div class="pdtDesptLpanel">
                    <div class="pdtDestLLayout">
                        <div class="prdTitleLayout">
                            <asp:Label ID="lblPrdTitle" runat="server" Text="Product Title:"></asp:Label>
                        </div>
                        <div class="prdTitleLayout">
                            <asp:TextBox ID="txtbxPrdTitle" runat="server" CssClass="prodTxtBox"></asp:TextBox>
                        </div>

                        <div style="float:right">
                            <asp:TextBox ID="txtbxSKU" runat="server" CssClass="prodTxtBox"></asp:TextBox>
                            
                        </div >
                        <div style="float:right">
                            <asp:Label ID="lblSKUProd" runat="server" Text="SKU:"></asp:Label>
                        </div>
                    </div>
                    <div class="pdtDesc">
                        <div>
                            <asp:Label ID="lblDesc" runat="server" Text="Description:"></asp:Label>
                        </div>
                        <div>
                            <asp:TextBox ID="txtbxDescXXX" CssClass="desctxtbx" runat="server" 
                                TextMode="MultiLine" ></asp:TextBox>
                        </div>
                    </div>   
                    <div class="pdtDestLLayout">
                        <div class="prdTitleLayout">
                            <asp:Label ID="lblCat" runat="server" Text="Category:"></asp:Label>
                        </div>
                        <div class="prdTitleLayout">
                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="catddl">
                            </asp:DropDownList>
                        </div>
                        <div class="prdTitleLayout">
                            <asp:Image ID="imgArrow" runat="server" CssClass="arrowImg" 
                                ImageUrl="~/Images/arrow.jpg" />
                        </div>
                        <div class="prdTitleLayout">
                            <asp:DropDownList ID="ddlSubCategory" runat="server" CssClass="catddl">
                            </asp:DropDownList>
                        </div>
                        <div class="prdTitleLayout">
                            <asp:ImageButton ID="ImgClose" runat="server" 
                                ImageUrl="~/Images/close.GIF" CssClass="closeImgBtn"  />
                        </div>
                        <div class="prdTitleLayout">
                            <asp:LinkButton ID="lnkBtnAddSubCat" runat="server" CssClass="addSubCatLnkBtn">Add Sub Category</asp:LinkButton>
                        </div>
                    </div>
    
                     <div class="pdtDestLLayout">
                        <div class="prdTitleLayout">
                            <asp:Label ID="lblLive" runat="server" Text="Live:"></asp:Label>
                        </div>
                        <div class="prdTitleLayout">
                            <asp:CheckBox ID="chkBxLive" CssClass="checkBx" runat="server" />
                        </div>
                        <div class="prdTitleLayout">
                            <asp:Label ID="Label5" runat="server" Text="Featured:"></asp:Label>
                        </div>
                        <div class="prdTitleLayout">
                            <asp:CheckBox ID="chkBxFeatured" CssClass="checkBx" runat="server" />
                        </div>
                        <div class="prdTitleLayout">
                            <asp:Label ID="Label6" runat="server" Text="Featured Category:"></asp:Label>
                        </div>
                        <div>
                            <asp:DropDownList ID="ddlFeatureCat" runat="server"  CssClass="catddl">
                            </asp:DropDownList>
                        </div>
                     </div>
             
                </div>
                <div class="pdtDesptRpanel">
                    <div class="pdtDesptRLayout">
                        <div class="lblTxtLayout">
                            <asp:Label ID="lblManuftr"  CssClass="pdtDesptRlbl" runat="server" Text="Manufacturer:"></asp:Label>
                        </div>                        
                        <div class="lblTxtLayout">
                            <asp:TextBox ID="txtManuftr" runat="server" CssClass="pdtDesptRtxtbx"></asp:TextBox>
                        </div>
                    </div>
                    <div class="pdtDesptRLayout">
                        <div class="lblTxtLayout">
                            <asp:Label ID="lblPrice" CssClass="pdtDesptRlbl" runat="server" Text="Price:"></asp:Label>
                        </div>
                        <div class="lblTxtLayout">
                            <asp:TextBox ID="txtPrice" runat="server" CssClass="pdtDesptRtxtbx"></asp:TextBox>
                        </div>
                    </div>                  
                    <div class="pdtDesptRLayout">
                        <div class="lblTxtLayout">
                            <asp:Label ID="lblListPrice" CssClass="pdtDesptRlbl" runat="server" Text="List Price:"></asp:Label>
                        </div>
                        <div class="lblTxtLayout">
                            <asp:TextBox ID="txtListPrice" runat="server" CssClass="pdtDesptRtxtbx"></asp:TextBox>
                        </div>
                    </div>
                    <div class="pdtDesptRLayout">
                        <div class="lblTxtLayout">
                            <asp:Label ID="lblWeight" CssClass="pdtDesptRlbl" runat="server" Text="Weight(LB):" ></asp:Label>
                        </div>
                        <div class="lblTxtLayout">
                            <asp:TextBox ID="txtWeight" runat="server" CssClass="pdtDesptRtxtbx"></asp:TextBox>
                        </div>
                    </div>
                    <div class="pdtDesptRLayout">
                        <div class="lblTxtLayout">
                            <asp:Label ID="lblShipClass" CssClass="pdtDesptRlbl" runat="server" Text="Shipping Class:" ></asp:Label>
                        </div>
                        <div class="lblTxtLayout">
                            <asp:DropDownList ID="ddlShipClass" runat="server" width="110px">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>

            <div class="inventory">
                <div class="invtLayout">
                    <asp:Label ID="Label7" runat="server" Text="INVENTORY" CssClass="inventorylbl"></asp:Label>
                </div>
                <div class="invtLayoutDivider">
                </div>

                <div class="inventoryDisplay">
                
                
                </div>
            </div>
            


            <div class="categoryLayout">
                <div class="categoryInfo" >
                    <asp:Label ID="Label2" CssClass="categoryFontLayout" runat="server" Text="SKU:"></asp:Label>
                </div>
                <div class="categoryInfo" >
                    <asp:Label ID="Label3" CssClass="categoryFontLayout" runat="server" Text="This is a label for SKU!!!"></asp:Label>
                </div>
                <div class="categoryInfo" style="margin-left:20px;" >
                    <asp:Label ID="Label4" CssClass="categoryFontLayout" runat="server" Text="Catgory selected!!!"></asp:Label>
                </div>
            </div>
      
        
        </div>
    </div>

    <UC:UserControl runat="server" ID="userControlAddNewProduct" />

</div>
</asp:Content>




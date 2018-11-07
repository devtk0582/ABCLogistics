<%@ Page Title="Categories" Language="C#" MasterPageFile="~/MasterPages/ABCLogisticsCMS.Master" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="CMS.Catalog.Categories" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
     <table width="100%" cellspacing="1" cellpadding="1">
        <tr>
            <td align="center" colspan="2">
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
        <tr valign="top">
            <td align="left">
                <asp:Label ID="lblCategories" CssClass="titleStyle" runat="server" Text="Categories"></asp:Label>
            </td>
            <td align="right">
                <asp:LinkButton ID="lnlBtnAddNewCategory" runat="server" CssClass="addButtonStyle" OnClick="addNewCategory_Click">Add Category</asp:LinkButton>
            </td>
        </tr>
        <tr>
                    <td colspan="2">
                        <asp:Label ID="lblErr" Visible="false" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr valign="top">
            <td colspan="2">
            <asp:Label ID="lblErrCategories" CssClass="errMessage" runat="server" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr valign="top">
            <td colspan="2" style="height: 350px; padding-top: 20px;">
            <asp:HiddenField ID="hfCurrentID" runat="server" />
            <div class="divlbs">
            <asp:GridView ID="gridViewCategories" runat="server" AutoGenerateColumns="false" AllowSorting="true" GridLines="Both"
                                  Onsorting="GridView_Sorting" DataKeyNames="Category_ID" Width="100%" EnableSortingAndPagingCallbacks="True"
                                  OnRowCommand="GridView_OnRowCommand" BorderWidth="0"  OnRowDataBound="GridView_OnRowDataBound"
                                  OnPageIndexChanging="gridViewCategories_OnPageIndexChanging"  PageSize="15" AllowPaging="true">
                    <HeaderStyle CssClass="gvHeaderStyle" />
                    <Columns>
                        <asp:TemplateField HeaderText="Category" SortExpression="Category_Name">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkBtnCategoryName" runat="server" Text='<%# bind("Category_Name") %>'
                                                CommandName="PopupSubcategories" CommandArgument='<%# bind("Category_ID") %>'></asp:LinkButton>
                            </ItemTemplate>     
                            <ItemStyle Width="70%"/>                    
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkBtnCategoryEdit" runat="server" CommandName="PopupCategory" 
                                                CommandArgument='<%# bind("Category_ID") %>'>Edit</asp:LinkButton>
                            </ItemTemplate>   
                            <ItemStyle Width="20%"/>                      
                        </asp:TemplateField>
                        <asp:templatefield HeaderText="Status" SortExpression="Is_Active">  
                            <itemtemplate> 
                                <asp:HiddenField ID="hfStatus" runat="server"  Value='<%#Eval("Is_Active").ToString()%>'/>
                                <asp:LinkButton ID="lnkToggle" runat="server" CommandName="ToggleCategory" CommandArgument='<%#Eval("Category_ID").ToString()%>'
                                                 Text='Delete'></asp:LinkButton>
                             
                            </itemtemplate>      
                            <ItemStyle Width="10%"/>                    
                        </asp:templatefield>                  
                    </Columns>
                    <PagerSettings Mode="NumericFirstLast" FirstPageText="<<" LastPageText=">>" NextPageText=">" PreviousPageText="<" />
                                        <PagerStyle CssClass="gvPagerStyle"/>
                                        <RowStyle CssClass="gvRowStyle"/>
                                        <AlternatingRowStyle CssClass="gvAltRowStyle" />
                    </asp:GridView>
                    </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table style="width:100%;" class="tblLabelStyle">
        <tr>
            <td>
            <%--add Category popup window--%>
            <input id="popCategories" type="button" style="display: none" runat="server" />
                <asp:ModalPopupExtender ID="ModlPopupCategories" runat="server" PopupControlID="pnlCatModlPopup"
                                         TargetControlID="popCategories" BackgroundCssClass="popupBG" DropShadow="true">
                </asp:ModalPopupExtender>
                <asp:Panel ID="pnlCatModlPopup" runat="server">
                    <table class="PopupFormBG" width="60%">
                        <tr style="height: 30px;">
                            <td class="PopupFormTitleStyle" colspan="2" align="left">                    
                                <asp:Label ID="lblCatHeadaer" runat="server" Text="Add New Category"></asp:Label>
                            </td>                
                        </tr> 
                        <tr>           
                            <td colspan="3" >
                                <asp:Label ID="lblErrMessage"  CssClass="errMessage" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold;" width="50%" align="left">
                                Category Name:
                            </td>
                            <td style="font-weight: bold;" width="50%" align="left">
                                <asp:TextBox ID="txtbxCatName"  runat="server" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>                   
                        <tr style="height: 50px;">
                        <td align="right" colspan="2">
                            <table width="30%">
                                <tr>
                                
                                    <td align="center">
                                         <asp:LinkButton ID="LnkBtnSaveNewCat" runat="server" 
                                        CssClass="addButtonStyle" OnClick="InsertNewCategory_Click"  >Save
                                    </asp:LinkButton>
                                    </td>
                                    <td align="center">
                                <asp:LinkButton ID="lnkBtnCancelCat" runat="server" 
                                        CssClass="addButtonStyle" OnClick="CancelInsertCat_Click" >Cancel
                                    </asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        </tr>
                    </table>
                 </asp:Panel>            
            </td>
            <td>
                <%--Update category popupwindow--%>
                <input id="popUpdateCat" type="button" style="display: none" runat="server" />
                    <asp:ModalPopupExtender ID="ModlPopupUpdatecat" runat="server" PopupControlID="pnlCatUpdateModlPopup" 
                                            DropShadow="true" TargetControlID="popUpdateCat" BackgroundCssClass="popupBG">
                    </asp:ModalPopupExtender>
                        <asp:Panel ID="pnlCatUpdateModlPopup" runat="server">
                        <asp:HiddenField ID="hfCategoryID" runat="server" />
                             <table class="PopupFormBG" width="60%">
                                <tr style="height: 30px;">
                                    <td colspan="2" class="PopupFormTitleStyle">                     
                                        <asp:Label ID="lblEditCategory" runat="server" Text="Update Category"></asp:Label>
                                    </td>                
                                </tr> 
                                <tr>           
                                    <td colspan="2" >
                                        <asp:Label ID="lblErrMessageUpdate" runat="server" CssClass="errMessage"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;" align="left" width="50%">
                                        Category Name: 
                                    </td>
                                    <td style="font-weight: bold;" align="left" width="50%">
                                        <asp:TextBox ID="txtUpdateCat" runat="server" Text='<%# Bind("Category_Name") %>' MaxLength="20">
                                        </asp:TextBox> 
                                    </td>
                                </tr>
                          <%--      <tr>                            
                                    <td style="font-weight: bold;" align="left">
                                        Active:
                                    </td>
                                    <td style="font-weight: bold;" align="left">
                                        <asp:CheckBox ID="chkBxActive" runat="server" />
                                    </td>
                                </tr>--%>
                                <tr style="height: 50px;">
                        <td align="right" colspan="2">
                            <table width="30%">
                                <tr>
                                    
                                   
                                    <td align="center">
                                    <asp:LinkButton ID="lnkBtnUpdate" runat="server" CssClass="addButtonStyle" OnClick="UpdateCategory_Click">Update</asp:LinkButton>  
                                    </td>
                                 <%--    <td align="center">
                                        <asp:LinkButton ID="lnkBtnDelete"  CssClass="addButtonStyle" runat="server" OnClick="DeleteCategory_Click">Delete</asp:LinkButton>
                                    </td>--%>
                                    <td align="center">
                                         <asp:LinkButton ID="lnkBtnCancel" runat="server"  CssClass="addButtonStyle" OnClick="CancelCategory_Click">Cancel</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                            </table>  
                        </asp:Panel>
            </td>
        </tr>
        <tr>
        <%--subcat popup window--%>
            <td colspan="2">


            <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
            <ContentTemplate>
            
                <asp:Panel ID="plSubCategory" runat="server" overflow="auto">            
                <asp:HiddenField ID="hfSubCatID" runat="server" />              
                <asp:HiddenField ID="hfCatID" runat="server" />
                <%--changes starts here--%>
                <div style=" background-color:White; padding:1px 1px 20px 1px" >
                        <div >
                           <table class="PopupFormBG" width="500px">
                                <tr>
                                    <td colspan="2" class="PopupFormTitleStyle" >                    
                                        <asp:Label ID="lblCategory" runat="server" Text="Subcategory" Font-Bold="True"></asp:Label>
                                        
                                    </td>                
                                </tr> 
                                <tr>    
                                    <td colspan="2">
                                        <asp:Label ID="lblErrMessageSubEdit" runat="server" CssClass="errMessage"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                     <td style="font-weight: bold;" width="30%" align="left">
                                        Name:
                                    </td>
                                     <td style="font-weight: bold;" width="70%" align="left">
                                        <asp:TextBox ID="txtSubCatName" runat="server" MaxLength="20"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                     <td style="font-weight: bold;" width="30%" align="left">
                                        Description:
                                    </td>
                                     <td style="font-weight: bold;" width="70%" align="left">
                                        <asp:TextBox ID="txtSubDescription" runat="server" Height="50px" Width="300px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                 <tr>
                                     <td style="font-weight: bold;" width="30%" align="left">
                                        Shipping Class:
                                    </td>
                                     <td style="font-weight: bold;" width="70%" align="left">
                                        <%--<asp:TextBox ID="txtSubCatShipClass" runat="server" MaxLength="20"></asp:TextBox>--%>
                                         <asp:DropDownList ID="ddlShipClass" runat="server" Width="120px">
                                                </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                     <td style="font-weight: bold;" width="30%" align="left">
                                       Featur Rank:
                                    </td>
                                     <td style="font-weight: bold;" width="70%" align="left">
                                        <%--<asp:TextBox ID="txtSubCatFeatureRank" runat="server" MaxLength="20"></asp:TextBox>--%>
                                        <asp:DropDownList ID="ddlSubCatFRank" runat="server">
                                        <asp:ListItem Text="Select" Value="Select" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                     <td style="font-weight: bold;" width="30%" align="left">
                                        Taxable:
                                    </td>
                                     <td style="font-weight: bold;" width="70%" align="left">
                                        <asp:CheckBox ID="chkTax" runat="server" />
                                    </td>
                                </tr> 
                   <tr style="height: 50px;">
                        <td align="right" colspan="2">
                            <table width="30%">
                                <tr>
                                
                                    <td align="center">
                                                                                <asp:LinkButton ID="lnkSubCatUpdate" runat="server" 
                                                     CssClass="addButtonStyle" OnClick="UpdateSubcategory_Click">Update
                                            </asp:LinkButton>
                                    </td>
                              <%--      <td align="center">
                                        <asp:LinkButton ID="lnkSubCatDelete" runat="server" 
                                                CssClass="addButtonStyle" OnClick="DeleteSubcategory_Click">Delete
                                            </asp:LinkButton>
                                    </td>--%>
                                    <td align="center" >
                                    <asp:LinkButton ID="lnkBtnInsertNewSubCategories" runat="server" 
                                                    CssClass="addButtonStyle" OnClick="InsertNewSubCategories_Click">Add
                                            </asp:LinkButton>
                                    </td>
                                    
                                                                        <td align="center">
                                        <asp:LinkButton ID="lnkSubCatCancel" runat="server" 
                                                    CssClass="addButtonStyle" OnClick="CancelSubCat_Click">Cancel
                                            </asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>                              
                                <tr>    
                                    <td colspan="2">
                                    </td>
                                </tr>
                            </table>
                        
                        </div>

                    <%--GridView for subcategory--%>
                    <asp:HiddenField ID="hfCurrentSubcatID" runat="server" />
                    <div  id="subcatgridView" class="divlbs" style="overflow:auto; height:200px; width:500px; overflow-x: hidden" >   
                    <asp:Label ID="lblnoSubcatFound" CssClass="errMessage" runat="server"></asp:Label>    
                    <asp:GridView ID="gridViewSubCategories" runat="server" AutoGenerateColumns="false"
                               AllowSorting="true" GridLines="Both" EnableSortingAndPagingCallbacks="True"
                              Onsorting="GridView_Sorting" DataKeyNames="Category_ID" Width="500px" 
                              OnRowCommand="GridView_OnRowCommand_Subcategory" OnRowDataBound="SubcatGridView_OnRowDataBound">
                        <HeaderStyle CssClass="gvHeaderStyle"  />
                        <Columns>
                            <asp:templatefield HeaderText="Name"  ControlStyle-Font-Size="9pt">  
                                <itemtemplate> 
                                    <asp:LinkButton ID="lnkBtnSubCategoryName" runat="server" Text='<%# bind("Subcategory_Name") %>'
                                                    CommandName="PopupSubCatUpdate" CommandArgument='<%# bind("Subcategory_Id") %>'></asp:LinkButton>          
                                </itemtemplate>                          
                             </asp:templatefield>  
                             <asp:templatefield HeaderText="Ship Class"  ControlStyle-Font-Size="9pt">  
                                <itemtemplate> 
                                    <asp:label ID="lblSubShipClass" runat="server" text='<%# bind("Shipping_Class") %>'></asp:label>
                                </itemtemplate>                          
                             </asp:templatefield>  
                             <asp:templatefield HeaderText="Tax"  ControlStyle-Font-Size="9pt">  
                                <itemtemplate> 
                                    <asp:label ID="lblSubIsTax" runat="server" text='<%# bind("Is_Taxable") %>'></asp:label>
                                </itemtemplate>                          
                             </asp:templatefield>                               
                             <asp:templatefield HeaderText="Feature"  ControlStyle-Font-Size="9pt">  
                                <itemtemplate> 
                                    <asp:label ID="lblSubFeature" runat="server" text='<%# bind("Feature_Rank") %>'></asp:label>
                                </itemtemplate>                          
                             </asp:templatefield> 
                             <asp:templatefield HeaderText="Status"  ControlStyle-Font-Size="9pt">  
                                <itemtemplate> 
                                    <asp:HiddenField ID="hfStatusSubcat" runat="server"  Value='<%#Eval("Is_Active").ToString()%>'/>
                                    <asp:LinkButton ID="lnkToggleSubcat" CommandName="ToggleSubcat" CommandArgument='<%#Eval("Subcategory_ID").ToString()%>'
                                                        runat="server" Text='Delete'></asp:LinkButton>
                                    
                                </itemtemplate>                          
                             </asp:templatefield> 
                        </Columns>
                        <PagerSettings Mode="NumericFirstLast" FirstPageText="<<" LastPageText=">>" NextPageText=">" PreviousPageText="<" />
                                        <PagerStyle CssClass="gvPagerStyle"/>
                                        <RowStyle CssClass="gvRowStyle"/>
                                        <AlternatingRowStyle CssClass="gvAltRowStyle" />
                    </asp:GridView>
                
                    </div>  
                 </div>
                 </asp:Panel>

                <%--to here--%>
                
                <input id="popupSubCat" type="button" style="display: none" runat="server" />
                <asp:ModalPopupExtender ID="modPopupSubCat" runat="server" PopupControlID="plSubCategory"
                                         TargetControlID="popupSubCat" BackgroundCssClass="popupBG" DropShadow="true">
                </asp:ModalPopupExtender>
           
         </ContentTemplate>
         </asp:UpdatePanel>
            </td>
        </tr>
    </table>
            </td>
        </tr>
     </table>
    


    </ContentTemplate>
    </asp:UpdatePanel>
    <%--Panel ends here--%>


 

</asp:Content>

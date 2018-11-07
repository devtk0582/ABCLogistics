<%@ Page Title="Users" Language="C#" MasterPageFile="~/MasterPages/ABCLogisticsCMS.Master" AutoEventWireup="true"
    CodeBehind="Users.aspx.cs" Inherits="CMS.Settings.Users" %>

<%@ Register src="../UserControls/AddUser.ascx" tagname="AddUser" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="upAddUsers" runat="server">
        <ContentTemplate>
            <table width="100%" cellspacing="1" cellpadding="1" >
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
              
                <tr>
                    <td>
                        <table width="100%" >
                            <tr>
                             <td align="left" >
                              <asp:Label ID="lblTitle" runat="server" Text="Users" CssClass="titleStyle"
                             ></asp:Label>
                            </td>
                                <td align="right">
                                    <div class="divlbs" style="float: right">
                                        <asp:LinkButton   CssClass="addButtonStyle" ID="lbAddUser" runat="server" Text="Add User"
                                              onclick="lbAddUser_Click" 
                                              ></asp:LinkButton>
                                    </div>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td colspan="2" style="height: 350px; padding-top: 20px;">
                                    <asp:HiddenField ID="hfCurrentID" runat="server" />
                                    <div class="divlbs">
                                        <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="False" BorderWidth="0px" AllowSorting="True" AllowPaging="True"
                                            EnableSortingAndPagingCallbacks="True" CellPadding="3" GridLines="Both"
                                            Width="100%"  
                                           OnRowCommand="gvUsers_RowCommand"  
                                           OnPageIndexChanging="gvUsers_PageIndexChanging"
                                           OnSorting="gvUsers_Sorting"
                                           >
                                          <HeaderStyle CssClass="gvHeaderStyle" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Login Name" SortExpression="user_email">
                                                    <ItemTemplate>
                                                        <asp:LinkButton CommandName="EditMethod" CommandArgument='<%#Eval("User_id").ToString()%>'
                                                            ID="lnkuser_email" Text='<%#Eval("user_email").ToString()%>' runat="server"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="40%" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="user_first" SortExpression="user_first" HeaderText="First Name">
                                                    <ItemStyle Width="15%"  />
                                                  
                                                </asp:BoundField>
                                                  <asp:BoundField DataField="user_last" SortExpression="user_last" HeaderText="Last Name">
                                                    <ItemStyle Width="15%"  />
                                                   
                                                </asp:BoundField>
                                                 <asp:BoundField DataField="Role_Description" SortExpression="Role_Description" HeaderText="Role Name">
                                                    <ItemStyle Width="15%"   />
                                                    
                                                </asp:BoundField>                                              
                                            </Columns>
                                            <PagerSettings Mode="NumericFirstLast" FirstPageText="<<" LastPageText=">>" NextPageText=">" PreviousPageText="<" />
                                        <PagerStyle CssClass="gvPagerStyle"/>
                                        <RowStyle CssClass="gvRowStyle"/>
                                        <AlternatingRowStyle CssClass="gvAltRowStyle" />
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblErr" runat="server" ForeColor="Red"></asp:Label>
                      
                    </td>
                </tr>
            </table>
              <uc1:AddUser ID="AddUser1" runat="server"  OnSaveButtonClicked="ucAddUser_SaveButtonClicked" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

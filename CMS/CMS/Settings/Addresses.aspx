<%@ Page Title="Addresses" Language="C#" MasterPageFile="~/MasterPages/ABCLogisticsCMS.Master" AutoEventWireup="true"
    CodeBehind="Addresses.aspx.cs" Inherits="CMS.Settings.Addresses" %>

<%@ Register Src="../UserControls/AddAddresses.ascx" TagName="AddAddresses" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script language="javascript" type="text/ecmascript">
    function ValidDelete() {
        var DeleCon = confirm('Are you sure , you want delete this record?');
        if (DeleCon == true) {
            return true;
        }
        else {
            return false;
        }
    }

    
</script>
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
                    <td>
                        <asp:Label ID="lblErr" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                            <tr>
                             <td align="left" >
                               <asp:Label ID="lblTitle" runat="server" Text="Address" CssClass="titleStyle"
                             ></asp:Label>
                             </td>
                                <td align="right">
                                    <div class="divlbs"  >
                                        <asp:LinkButton CssClass="addButtonStyle" ID="lbAddAddresses" runat="server" Text="Add Address"
                                              OnClick="lbAddAddresses_Click"   ></asp:LinkButton>
                                    </div>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td colspan="2" style="height: 350px; padding-top: 20px;">
                                    <asp:HiddenField ID="hfCurrentID" runat="server" />
                                    <div class="divlbs">
                                        <asp:GridView ID="gvAddresses" runat="server" AutoGenerateColumns="False" 
                                            BorderWidth="0px" AllowSorting="True" AllowPaging="True"
                                            EnableSortingAndPagingCallbacks="True" CellPadding="3" GridLines="Both"
                                            Height="100%" Width="100%"
                                             OnRowCommand="gvAddresses_RowCommand"
                                             OnPageIndexChanging="gvAddresses_PageIndexChanging"
                                             OnSorting="gvAddresses_Sorting" onrowdeleting="gvAddresses_RowDeleting" onrowdatabound="gvAddresses_RowDataBound"  
                                            >
                                           <HeaderStyle CssClass="gvHeaderStyle" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Company" SortExpression="company_name" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:LinkButton CommandName="EditMethod" CommandArgument='<%#Eval("address_id").ToString()%>'
                                                            ID="lnkcompany_name" Text='<%#Eval("company_name").ToString()%>' runat="server"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20%" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="addr" SortExpression="addr" HeaderText="Address">
                                                    <ItemStyle Width="20%"   />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="City" SortExpression="City" HeaderText="City">
                                                    <ItemStyle Width="15%"   />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="State" SortExpression="State" HeaderText="State">
                                                    <ItemStyle Width="15%"  />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="postal_code" SortExpression="postal_code" HeaderText="Zip">
                                                    <ItemStyle Width="15%"   />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CountryName" SortExpression="CountryName" HeaderText="Country">
                                                    <ItemStyle Width="15%"   />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:LinkButton CommandName="Delete" CommandArgument='<%#Eval("address_id").ToString()%>'
                                                            ID="lnkdelete" Text='Delete' runat="server"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20%" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerSettings Mode="NumericFirstLast" FirstPageText="<<" LastPageText=">>" NextPageText=">"
                                                PreviousPageText="<" />
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
                
            </table>
            <uc1:AddAddresses ID="AddAddresses1" OnSaveButtonClicked="ucAddaddresses_SaveButtonClicked" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

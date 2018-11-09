<%@ Page Title="Customer Logos - CMS Master" Language="C#" MasterPageFile="~/CMSMaster.Master" AutoEventWireup="true" CodeBehind="CustomerLogos.aspx.cs" Inherits="CMSMaster.CustomerLogos" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    function ClearForm() {
        $get("<%=fuHeader.ClientID %>").outerHTML = $get("<%=fuHeader.ClientID %>").outerHTML;
        $get("<%=fuFooter.ClientID %>").outerHTML = $get("<%=fuFooter.ClientID %>").outerHTML;
        $get("<%=fuSideBar.ClientID %>").outerHTML = $get("<%=fuSideBar.ClientID %>").outerHTML;
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" border="0" cellpadding="8" cellspacing="0" bgcolor="f4f5f7" style="text-align: left; font-weight: normal; padding: 5px; font-size: 11px; ">
        <tr>
            <td colspan="2">
                <asp:Label ID="lblTitle" runat="server" Text="Customer Logos" Font-Bold="True" 
                    Font-Size="Medium" ForeColor="#0099CC"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblErr" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr style="height: 75px">
            <td width="20%" valign="top">
                <asp:Label ID="Label1" runat="server" Text="Header Logo:"></asp:Label>
            </td>
            <td width="80%" valign="top">
                <asp:Image ID="imgHeader" runat="server" Width="160" Height="65" ImageUrl="~/ShowImage.ashx?Type=Header" />
            &nbsp;&nbsp;&nbsp;
                <asp:FileUpload ID="fuHeader" runat="server" BackColor="#FFFFCC" CssClass="input_text" />
             &nbsp;&nbsp;&nbsp;
                <asp:ImageButton ID="ibtnDelHeaderLogo" runat="server" 
                    ImageUrl="~/images/delete_16x16.gif" onclick="ibtnDelHeaderLogo_Click" />
            </td>
        </tr>
         <tr style="height: 75px">
            <td width="20%" valign="top">
                <asp:Label ID="Label2" runat="server" Text="Footer Logo:"></asp:Label>
            </td>
            <td width="80%" valign="top">
                <asp:Image ID="imgFooter" runat="server" Width="160" Height="65" ImageUrl="~/ShowImage.ashx?Type=Footer" />
             &nbsp;&nbsp;&nbsp;
                <asp:FileUpload ID="fuFooter" runat="server" BackColor="#FFFFCC" CssClass="input_text"  />
                &nbsp;&nbsp;&nbsp;
                <asp:ImageButton ID="ibtnDelFooterLogo" runat="server" 
                    ImageUrl="~/images/delete_16x16.gif" onclick="ibtnDelFooterLogo_Click" />
            </td>
        </tr>
        <tr style="height: 75px">
            <td width="20%" valign="top">
                <asp:Label ID="Label3" runat="server" Text="Side Bar Logo:"></asp:Label>
            </td>
            <td width="80%" valign="top">
                <asp:Image ID="imgSideBar" runat="server" Width="160" Height="40" ImageUrl="~/ShowImage.ashx?Type=SideBar" />
             &nbsp;&nbsp;&nbsp;
                <asp:FileUpload ID="fuSideBar" runat="server" BackColor="#FFFFCC" CssClass="input_text" />
            &nbsp;&nbsp;&nbsp;
                <asp:ImageButton ID="ibtnDelSideBarLogo" runat="server" 
                    ImageUrl="~/images/delete_16x16.gif" onclick="ibtnDelSideBarLogo_Click" 
                    style="height: 16px" />
            </td>
             
        </tr>
        <tr>
            <td colspan="4" align="right">
                <asp:LinkButton ID="btnSave" Text="Save" cssclass="button1" ForeColor="white"
                                        runat="server" onclick="btnSave_Click" />
                                    <asp:LinkButton ID="btnCancel" Text="Cancel" 
                    cssclass="button1" ForeColor="white"
                                        runat="server" OnClientClick="ClearForm();" />
 <asp:LinkButton ID="lbPreview" Text="Preview" CssClass="button1" ForeColor="white" runat="server" onclick="lbPreview_Click" />
            </td>
        </tr>
    </table>
</asp:Content>

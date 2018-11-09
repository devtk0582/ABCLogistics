<%@ Page Title="Customer Messages - CMS Master" Language="C#" MasterPageFile="~/CMSMaster.Master" AutoEventWireup="true" CodeBehind="CustomerMessages.aspx.cs" Inherits="CMSMaster.CustomerMessages" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" border="0" cellpadding="5" cellspacing="0" bgcolor="f4f5f7" style="text-align: left; font-weight: normal; padding: 5px; font-size: 11px;">
        <tr>
            <td colspan="3">
                <asp:Label ID="lblTitle" runat="server" Text="Customer Messages" Font-Bold="True" 
                    Font-Size="12" ForeColor="#0099CC"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblErr" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%" align="left" valign="top">
                <asp:Label ID="Label1" runat="server" Text="Header Message:"></asp:Label>
            </td>
            <td width="80%" align="left" valign="top">
                <asp:TextBox ID="txtHeaderMsg" runat="server" TextMode="MultiLine" Rows="2" Text="ABCLogistics CMS" Columns="30" MaxLength="30" CssClass="input_text"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td width="20%" align="left" valign="top">
                <asp:Label ID="Label2" runat="server" Text="Footer Message:"></asp:Label>
            </td>
            <td width="80%" align="left" valign="top">
               <asp:TextBox ID="txtFooterMsg" runat="server" TextMode="MultiLine" Rows="2" Text="Copy Right @ 2012 Service By Air" Columns="100" MaxLength="100" CssClass="input_text"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="20%" align="left" valign="top">
                <asp:Label ID="Label3" runat="server" Text="Side Menu Message:"></asp:Label>
            </td>
            <td width="80%" align="left" valign="top">
               <asp:TextBox ID="txtSideBarMsg" runat="server" TextMode="MultiLine" Rows="2" Text="Service By Air" 
                    Columns="30" MaxLength="30" CssClass="input_text"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="20%" align="left" valign="top">
                <asp:Label ID="Label4" runat="server" Text="Welcome Message:"></asp:Label>
            </td>
            <td width="80%" align="left" valign="top">
               <asp:TextBox ID="txtWelcomeMsg" runat="server" Text="Welcome to Service By Air" TextMode="MultiLine" Rows="5" Columns="80" CssClass="input_text"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <asp:LinkButton ID="btnSave" Text="Save" cssclass="button1" ForeColor="white"
                                        runat="server" onclick="btnSave_Click" />
                                    <asp:LinkButton ID="btnCancel" Text="Cancel" 
                    cssclass="button1" ForeColor="white"
                                        runat="server" onclick="btnCancel_Click" />
 <asp:LinkButton ID="lbPreview" Text="Preview" CssClass="button1" ForeColor="white" runat="server" onclick="lbPreview_Click" />
            </td>
        </tr>
    </table>
</asp:Content>

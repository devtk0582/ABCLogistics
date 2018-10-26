<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContentManager.ascx.cs" Inherits="TMS.UserControls.ContentManager" %>
<table width="100%">
    <tr>
        <td>
            <asp:Label ID="lblHeader" runat="server" Text="" Font-Bold="true" Font-Size="Large"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Literal ID="ltlContent" runat="server"></asp:Literal>
        </td>
    </tr>
</table>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="TMS.ResetPassword" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Reset Password- TMS</title>
    <link rel="stylesheet" href="Styles/Site.css" type="text/css" />
    <link rel="stylesheet" href="Styles/tms.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" align="center" border="0">
                        <tr>
                            <td width="100%" align="center">
                                <table width="100%" border="0" align="center">
                                    <tr>
                                        <td width="20%" align="left">
                                            <img alt="logo" src="Images/logo.gif" width="239" height="89" />
                                        </td>
                                        <td width="80%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" align="left">
                                        </td>
                                        <td width="80%" bgcolor="#17375E" style="height: 2px;">
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table height="350" width="600" align="center" border="0" bgcolor="#bfbfbf">
                                    <tr>
                                        <td width="100%" align="center">
                                            <table border="0" align="center" cellpadding="2" cellspacing="1" id="login_table">
                                                <tr valign="middle" style="padding: 10px;">
                                                        <td align="left">
                                                            <asp:Label ID="lblMsg" runat="server" ForeColor="Black"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="padding: 25px; height: 40px;">
                                                        <td align="right">
                                                            <asp:LinkButton CssClass="login_button" ID="lbRedirectToLogin" runat="server" ForeColor="White"
                                                                Text="Log In From Here" PostBackUrl="~/log-in" />
                                                        </td>
                                                    </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="">
                                    <tr>
                                        <td bgcolor="#17375E" style="height: 5px;">
                                        </td>
                                    </tr>
                                    <%--  <tr>
                                        <td bgcolor="#FFFFFF" height="40px" align="left">
                                            <span class="Footer-text">Copyright &copy;
                                                <%: System.DateTime.Today.Year  %>
                                                Powered by SummitWorks Technologies Inc.</span>
                                        </td>
                                    </tr>--%>
                                </table>
                            </td>
                        </tr>
                    </table>
    </div>
    </form>
</body>
</html>

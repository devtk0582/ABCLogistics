<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CMS.Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>E Commerce Website</title>
    <link rel="stylesheet" href="Styles/others.css" type="text/css" />
    <link href="Styles/ABCLogisticsCMS.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnLogin">
    <center>
        <div class="stylesheet">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr id="trHeader" runat="server">
                    <td colspan="3">
                        <table width="100%">
                            <tr>
                                <td align="left" width="180">
                                    <asp:Image ID="imgHeader" runat="server" Width="160" Height="65" />
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblHeader" runat="server" Text="Header Text" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr valign="top" style="height: 350px;">
                    <td id="tdSideBarLeft" runat="server" valign="top" width="8%">
                    </td>
                    <td width="84%">
                        <table style="height: 100%" width="100%">
                            <tr style="height: 100px">
                                <td valign="middle">
                                    <asp:Literal ID="lblWelcome" runat="server" Text="Welcome Message"></asp:Literal>
                                </td>
                            </tr>
                            <tr style="background-color: White; height: 250px">
                                <td>
                                
                                    <table border="1" frame="border" align="center" bgcolor="white" style="font-size: 12px">
                                        <tr>
                                            <td>
                                                    <table border="0" align="center" cellpadding="1" cellspacing="1" style="width: 317px;
                                                        font-size: 12px;" bgcolor="#EEFFEE">
                                                        <tr>
                                                            <td align="right">
                                                                &nbsp;
                                                            </td>
                                                            <td align="right" class="style8">
                                                                &nbsp;
                                                            </td>
                                                            <td class="style7">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style15">
                                                            </td>
                                                            <td>
                                                                <span class="style18">User Name:</span>
                                                            </td>
                                                            <td class="style17">
                                                                <span class="style12"></span>
                                                                <asp:TextBox ID="txtUserID" CssClass="style13" MaxLength="50" runat="server" Width="150px"
                                                                    Font-Names="Verdana" Font-Size="12px"></asp:TextBox>
                                                                <span class="style12"></span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style6">
                                                            </td>
                                                            <td>
                                                                <span class="style18">Password:</span>
                                                            </td>
                                                            <td class="style17">
                                                                <span class="style12"></span>
                                                                <asp:TextBox ID="txtPWD" CssClass="style13" MaxLength="20" TextMode="Password" runat="server"
                                                                    Width="150px" Font-Names="Verdana" Font-Size="12px"></asp:TextBox>
                                                                <span class="style12"></span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td class="style8">
                                                                &nbsp;
                                                            </td>
                                                            <td class="style7">
                                                                <asp:Label ID="lblErrMessage" Font-Bold="true" runat="server" Font-Names="Verdana"
                                                                    Font-Size="Small" ForeColor="Red"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td class="style8">
                                                                &nbsp;
                                                            </td>
                                                            <td class="style7">
                                                                <table width="100%" border="0" cellspacing="0">
                                                                    <tr>
                                                                        <td class="style10">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td width="93%">
                                                                            <asp:LinkButton ID="btnLogin" runat="server"  CssClass="addButtonStyle"
                                                                                 Text="Login" OnClick="btnLogin_Click" />&nbsp;&nbsp;
                                                                            <asp:LinkButton ID="btnReset" runat="server" 
                                                                                CssClass="addButtonStyle" Text="Refresh" OnClick="btnReset_Click" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td class="style8">
                                                                &nbsp;
                                                            </td>
                                                            <td class="style7">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                               
                                            </td>
                                        </tr>
                                    </table>
                                     
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td id="tdSideBarRight" runat="server" valign="top" width="8%">
                    </td>
                </tr>
                <tr id="trFooter" runat="server">
                    <td colspan="3">
                        <table width="100%">
                            <tr>
                                <td align="left" width="180">
                                    <asp:Image ID="imgFooter" runat="server" Width="160" Height="40" />
                                </td>
                                <td align="center">
                                    <asp:Label ID="lblFooter" runat="server" Text="Footer Text" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </center>
    </form>
</body>
</html>

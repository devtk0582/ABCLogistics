<%@ Page Title="Preview Page" Language="C#" AutoEventWireup="true" CodeBehind="TestPage.aspx.cs" Inherits="CMSMaster.TestPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link rel="stylesheet" href="css/preview.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
        <table width="80%">
            <tr>
                <td>
                    <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">Back To CMS</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td>
                <table width="100%"  cellpadding="0" cellspacing="0">
                <tr runat="server" id="trHeader">
                    <td colspan="2">
                        <table width="100%">
                            <tr>
                                <td align="left">
                                    <asp:Image ID="imgHeader" runat="server" Width="168" Height="65"/>
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblHeader" runat="server" Text="Header Text"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                                <tr style="height: 18px;">
                    <td runat="server" id="tdMainMenu" style="background-color: #003F75;" width="100%" colspan="2">
                        <ul id="menu">
                            <li runat="server" id="liLink">
                                <asp:LinkButton ID="lb" runat="server" Text="Menu1"></asp:LinkButton>
                            </li>
                            <li>
                                <a>Menu2</a>
                            </li>
                            <li>
                                <a>Menu3</a>
                            </li>
                            <li>
                                <a>Menu4</a>
                            </li>
                        </ul>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div runat="server" id="divSep" style="height: 5px; background-color: #BDD1E1;" >
                        </div>
                    </td>
                </tr>
                <tr style="height: 25px">
                    <td runat="server" id="tdSubMenu" style="background-color: #88B6DF;" width="100%" colspan="2">
                         <ul id="menu1">
                            <li>
                                <a>SubMenu1</a>
                            </li>
                            <li>
                                <a>SubMenu2</a>
                            </li>
                            <li>
                                <a>SubMenu3</a>
                            </li>
                            <li>
                                <a>SubMenu4</a>
                            </li>
                            <li>
                                <a>SubMenu5</a>
                            </li>
                        </ul>
                    </td>
                </tr>
                <tr>
                    <td width="20%" valign="top" runat="server" id="tdSideBar">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Image ID="imgSideBar" runat="server" Width="160" Height="65" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblSideBar" runat="server" Text="Side Text"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="80%">
                        <table width="100%" style="height: 300px;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblWelcome" runat="server" Text="Welcome Message"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server" id="trFooter">
                <td colspan="2">
                    <table width="100%">
                        <tr>
                                                <td align="left">
                        <asp:Image ID="imgFooter" runat="server" Width="160" Height="40" />
                    </td>
                    <td align="right">
                        <asp:Label ID="lblFooter" runat="server" Text="Footer Text"></asp:Label>
                    </td>
                        </tr>
                    </table>
                </td>
                </tr>
            </table>
                </td>
            </tr>
        </table>
            
        </center>
    </div>
    </form>
</body>
</html>

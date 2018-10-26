<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs"
    Inherits="TMS.LogIn" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>ABCLogistics GLOBAL - TMS : Login </title>
    <link href="Styles/tms.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.checkbox.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function OpenGoogleAuth(w, h) {
            var left = (screen.width / 2) - (w / 2);
            var top = (screen.height / 2) - (h / 2);
            window.open('google-authentication', '', 'width=600,height=450,titlebar=yes,top=' + top + ', left=' + left);
            return false;
        }

        function RedirectToHome() {
            window.location = "home";
        }

        $(document).ready(function () {
            $("#<%=cbRememberMe.ClientID %>").checkbox({ cls: 'jquery-safari-checkbox' });
        });
    </script>
</head>
<body>
    <center>
        <div style="padding-top: 50px;">
            <div id="wrapper">
                <div class="main_body">
                    <form id="mainForm" defaultbutton="btnLogin" runat="server">
                    <asp:ScriptManager ID="ScriptManager" runat="server">
                    </asp:ScriptManager>
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
                                                <tr>
                                                    <td align="center" style="background-color: #17375E;" class="login_header">
                                                        <span>TMS Log In</span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" class="login_table_row">
                                                        <asp:Label ID="lblErrMessage" Font-Bold="true" runat="server" Font-Names="Verdana" Font-Size="12px" ForeColor="#F08080"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" class="login_table_row">
                                                        User ID:
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="login_table_row">
                                                        <asp:TextBox ID="txtUserID" MaxLength="50" Width="100%" runat="server" Font-Names="Verdana"
                                                            Font-Size="12px" placeholder="Please enter your email address"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" class="login_table_row">
                                                        Password:
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="login_table_row">
                                                        <asp:TextBox ID="txtPWD" MaxLength="50" TextMode="Password" runat="server" Width="100%"
                                                            Font-Names="Verdana" Font-Size="12px" placeholder="Please enter your password"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" class="login_table_row inputTransStyle">
                                                        <asp:CheckBox ID="cbRememberMe" runat="server" Text=" Remember Me" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" class="login_table_row">
                                                        <asp:LinkButton ID="btnLogin" runat="server" Text="Sign In" OnClick="btnLogin_Click" CssClass="login_button" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" style="padding: 10px;">
                                                        <asp:LinkButton ID="lnkGoogleOpenIDLogin" runat="server" Text="ABCLogistics Employee" CssClass="login_button" OnClientClick="return OpenGoogleAuth(600,450);"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:LinkButton ID="lnkForgotPasword" ForeColor="#003366" Font-Bold="True" runat="server"
                                                            Text="Forgot Password?" Font-Names="Verdana" Font-Size="11px" 
                                                            onclick="lnkForgotPasword_Click"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="white" height="20px" align="center">
                                <input id="popupForgotPwd" type="button" style="display: none" runat="server" />
                                <cc1:ModalPopupExtender ID="mpeForgotPW" runat="server" TargetControlID="popupForgotPwd"
                                    CancelControlID="lbCancel" PopupControlID="pnlPopupForgotPwd" BackgroundCssClass="popupBG"
                                    DropShadow="true">
                                </cc1:ModalPopupExtender>
                                <asp:Panel ID="pnlPopupForgotPwd" runat="server" CssClass="modalPopupfp" >
                                    <table width="100%" cellpadding="3" cellspacing="2" style="font-family: Verdana; background-color: White">
                                        <tr>
                                            <td colspan="2" align="left" bgcolor="#17375E" style="padding-left: 5px;">
                                                <span style="font-weight: bold; color: White;">Forgot Password</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="left" style="padding-left: 5px;">
                                                <asp:Label ID="lblValidate" runat="server" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" width="25%" style="padding-left: 5px;">
                                                            Email:
                                                        </td>
                                                        <td align="left" width="75%" style="padding-right: 5px;">
                                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="input_text" MaxLength="50" Width="100%"></asp:TextBox>
                                                        </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="right">
                                                <asp:LinkButton ID="lbSubmit" runat="server" Text="Submit" 
                                                    CssClass="login_button" onclick="lbSubmit_Click"></asp:LinkButton>
                                                &nbsp;&nbsp;
                                                <asp:LinkButton ID="lbCancel" runat="server" Text="Cancel" CssClass="login_button"></asp:LinkButton>
                                                &nbsp;&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#17375E" style="height: 5px;">
                                        </td>
                        </tr>
                    </table>
                    </form>
                </div>
            </div>
        </div>
    </center>
</body>
</html>

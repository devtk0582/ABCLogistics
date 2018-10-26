<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoogleAuth.aspx.cs" Inherits="TMS.GoogleAuth" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>ABCLogistics Global Google Authentication</title>
    <script src="Scripts/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function AuthReturn() {
            window.opener.RedirectToHome();
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="background-color: Gray; text-align: center; vertical-align: middle;color: White;">
        <asp:Label ID="lblMsg" runat="server" Text="Google OpenID authentication is in process..."></asp:Label>
    </div>
    </form>
</body>
</html>

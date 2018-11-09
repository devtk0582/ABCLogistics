<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CMSMaster._Default" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

 <html xmlns="http://www.w3.org/1999/xhtml">
  <head>
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <title>Log In - CMS Master, SERVICE BY AIR, INC.</title>
   <link rel="stylesheet" href="css/MainSite.css" type="text/css" />
   <link rel="stylesheet" href="css/MainOthers.css" type="text/css" />
  <style type="text/css">
.stylesheet {
	font-family: Verdana, Geneva, sans-serif;
	font-size: 14px;
	font-style: normal;
	line-height: normal;
	font-weight: normal;
	font-variant: normal;
	text-align: left;
	border: thin solid #CCC;
	list-style-type: square;
	z-index: auto;
	width: 85%;
	color: #00F;
}
  .stylesheet table tr td table tr td table tr td table tr td {
	color: #FFF;
	font-size: 12px;
	text-align: center;
	font-weight: bold;
}
  #apDiv1 {
	position:absolute;
	left:575px;
	top:144px;
	width:300px;
	height:141px;
	z-index:1;
}
  .stylesheet table tr td table tr td table tr td {
	color: #000;
	font-size: 14px;
}
  .stylesheet table tr td table tr td #form3 {
	font-size: 12px;
}
  .stylesheet table tr td table tr td {
	font-size: 11px;
	color: #066;
	font-weight: bold;
}
            
      .style6
      {
          text-align: right;
          height: 21px;
      }
      .style7
      {
          width: 461px;
      }
      .style8
      {
          width: 219px;
      }
      .style10
      {
          width: 7%;
      }
      .style12
      {
          font-size: xx-small;
          text-align: right;
      }
      .style13
      {
          border: 1px solid #c0c0c0;
          padding: 2px;
          font-family: Verdana, Geneva, sans-serif;
          font-size: xx-small;
          color: #000000;
          background-color: #ffffff;
      }
      .style15
      {
          text-align: right;
          font-size: xx-small;
          height: 21px;
      }
      .style17
      {
          width: 461px;
          height: 21px;
      }
      .style18
      {
          font-size: xx-small;
          text-align: right;
          color: #006666;
      }
  </style>
  <script type="text/javascript">
      function MM_swapImgRestore() { //v3.0
          var i, x, a = document.MM_sr; for (i = 0; a && i < a.length && (x = a[i]) && x.oSrc; i++) x.src = x.oSrc;
      }
      function MM_preloadImages() { //v3.0
          var d = document; if (d.images) {
              if (!d.MM_p) d.MM_p = new Array();
              var i, j = d.MM_p.length, a = MM_preloadImages.arguments; for (i = 0; i < a.length; i++)
                  if (a[i].indexOf("#") != 0) { d.MM_p[j] = new Image; d.MM_p[j++].src = a[i]; }
          }
      }

      function MM_findObj(n, d) { //v4.01
          var p, i, x; if (!d) d = document; if ((p = n.indexOf("?")) > 0 && parent.frames.length) {
              d = parent.frames[n.substring(p + 1)].document; n = n.substring(0, p);
          }
          if (!(x = d[n]) && d.all) x = d.all[n]; for (i = 0; !x && i < d.forms.length; i++) x = d.forms[i][n];
          for (i = 0; !x && d.layers && i < d.layers.length; i++) x = MM_findObj(n, d.layers[i].document);
          if (!x && d.getElementById) x = d.getElementById(n); return x;
      }

      function MM_swapImage() { //v3.0
          var i, j = 0, x, a = MM_swapImage.arguments; document.MM_sr = new Array; for (i = 0; i < (a.length - 2); i += 3)
              if ((x = MM_findObj(a[i])) != null) { document.MM_sr[j++] = x; if (!x.oSrc) x.oSrc = x.src; x.src = a[i + 2]; }
      }
  </script>
  </head>
    
  <body onload="MM_preloadImages('images/login1.jpg','images/login_bt1.jpg','images/refresh_bt1.jpg','images/sign_up.jpg')">
  <center>
  <div class="stylesheet" >
 <form id="form4" defaultbutton="btnLogin"  runat="server">
    <table width="100%"  border="0" >
      <tr>
        <td >
              <asp:ScriptManager ID="ScriptManager1" runat="server">
              </asp:ScriptManager>
<%--              <cc1:ModalPopupExtender ID="mpeForgotPW" runat="server"  TargetControlID="lnkForgotPasword"
                         PopupControlID="pnlForgotPW"   Drag="true"  BackgroundCssClass="popupBG"                       
                    
    DropShadow="true"  >
              </cc1:ModalPopupExtender>--%>
            </td>
      </tr>
      <tr>
        <td height="215" >
        <table width="100%" border="0">
           <tr>
              <td width="76%"></td>
              <td width="24%" align="right"><img src="images/logo.gif" width="168" height="63" /></td>
            </tr>
          </table>
        <table height="317" width="100%" align="center" border="0" background="images/cms2.gif">
        <tr>
        <td>
            <table border="1" align="center" bgcolor="White" frame="border" 
                style="font-size: xx-small">
            <tr>
            <td>
            <table border="0" align="center" cellpadding="1" 
                cellspacing="1" style="width: 317px; font-size: xx-small;" bgcolor="White">
                   
          <tr>
            <td align="right">&nbsp;</td>
            <td align="right" class="style8"><a href="#" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image5','','images/sign_up.jpg',1)"><img src="images/sign_up1.jpg" name="Image5" width="64" height="16" border="0" id="Image5" /></a></td>
            <td class="style7">&nbsp;</td>
          </tr>
          <tr>
            <td class="style15"></td>
            <td><span class="style18">User Name:</span></td>
            <td class="style17"><span class="style12"><%--<form id="form1" name="form1" method="post" action="">--%>
           <%--   <label for="textfield4"></label>
              <input type="text" name="textfield" id="textfield4" />--%></span>
                <asp:TextBox ID="txtUserID"
                  CssClass="style13" MaxLength="50" runat="server" Width="150px" Font-Names="Verdana"
                  Font-Size="X-Small"></asp:TextBox>
                <span class="style12">
                </span>
            </td>
          </tr>
          <tr>
            <td class="style6"></td>
            <td><span class="style18">Password:</span></td>
            <td class="style17"><span class="style12">
                </span>
                <asp:TextBox ID="txtPWD" CssClass="style13" MaxLength="20" TextMode="Password"
                    runat="server" Width="150px" Font-Names="Verdana" Font-Size="X-Small"></asp:TextBox>
                <span class="style12">
                </span>
            </td>
          </tr>
           <tr>
            <td align="right">&nbsp;</td>
            <td align="right" class="style8">&nbsp;</td>
            <td class="style7">
              &nbsp;<input type="checkbox" name="checkbox" id="cbRememberMe" runat="Server" />
                <span class="style18">Remember Me</span>
          <%--  </form>--%>
            </td>
          </tr>
          <tr>
            <td>&nbsp;</td>
            <td class="style8">&nbsp;</td>
            <td class="style7"><table width="100%" border="0" cellspacing="0">
              <tr>
                <td class="style10">&nbsp;</td>
                <td width="93%">
                    <asp:LinkButton   onmouseout="MM_swapImgRestore()"
                        onmouseover="MM_swapImage('Image3','','images/login_bt1.jpg',1)" ID="btnLogin"
                        runat="server" ForeColor="White" CssClass="button1" Text="Login" 
                        onclick="btnLogin_Click"  />
                    <asp:LinkButton onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image4','','images/refresh_bt1.jpg',1)"
                        ID="btnReset" runat="server" ForeColor="White" CssClass="button1" Text="Refresh" OnClick="btnReset_Click" />
                   
               </td>
              </tr>
            </table></td>
          </tr>
          <tr>
            <td>&nbsp;</td>
            <td class="style8">&nbsp;</td>
            <td class="style7">
                <asp:Label ID="lblErrMessage" Font-Bold="true" runat="server" Font-Names="Verdana" 
                    Font-Size="Small" ForeColor="Red"></asp:Label>
              </td>
          </tr>
          <tr>
            <td>&nbsp;</td>
            <td class="style8">&nbsp;</td>
            <td class="style7"><asp:LinkButton ID="lnkForgotPasword" ForeColor="#003366" Font-Bold="True" runat="server"
                    Text="Forgot My User Name/Password" Font-Names="Verdana" Font-Size="X-Small"></asp:LinkButton></td>
          </tr>
         
       
         
          </table>
        </td>
        </tr>
        </table>
        </td>
        </tr>
        </table>

            <%--<div>
                <asp:Panel ID="pnlforgotPWD" runat="server" Width="400" hight="300" BorderColor="#6690C0"
                    BorderStyle="Solid" BorderWidth="1px" BackColor="White">
                    <asp:UpdatePanel ID="udpInner" runat="Server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:PostBackTrigger ControlID="imgRetrival" />
                            
                        </Triggers>
                        <ContentTemplate>
                            <table width="100%">
                               
                                <tr>
                                    <td colspan="2" background="images/tdbg.png" spry:repeat="x" style="font-family: Verdana;
                                        font-size: medium; color: #FFFFFF; height: 30px;" align="center">
                                        Forgot Password
                                    </td>
                                </tr>
                                 <tr>
                                     <td colspan="2" >
                                        <asp:Label ID="lblpopupErrorMsg" ForeColor="Red" runat="server"></asp:Label>
                                     </td>
                                </tr>
                                <tr>
                                    <td align="left" style="font-family: Verdana; font-size: x-small" class="style1">
                                        Email :
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtEmail" CssClass="input_text" runat="server" Font-Names="Verdana"
                                            Font-Size="X-Small" MaxLength="50" Width="150px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="font-family: Verdana; font-size: x-small" class="style1">
                                        Security Question1 :
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlSecQuestion1" CssClass="DropDown_Style" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="font-family: Verdana; font-size: x-small" class="style1">
                                        Answer :
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtAnswer1" CssClass="input_text" runat="server" Font-Names="Verdana"
                                            Font-Size="X-Small" MaxLength="50" TextMode="MultiLine" Width="150px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="font-family: Verdana; font-size: x-small" class="style1">
                                        Security Question2 :
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlSecQuestion2" CssClass="DropDown_Style" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="font-family: Verdana; font-size: x-small" class="style1">
                                        Answer :
                                    </td>
                                    <td align="left">
                                        <asp:TextBox CssClass="input_text" ID="txtAnswer2" runat="server" Font-Names="Verdana"
                                            Font-Size="X-Small" MaxLength="50" TextMode="MultiLine" Width="150px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="right">
                                       
                                        <asp:LinkButton ID="imgRetrival" runat="server" ForeColor="White" 
                                            CssClass="button1" Text="Retrieve Password" onclick="imgRetrival_Click" />
                                        <asp:LinkButton ID="imgCancel" runat="server" ForeColor="White" CssClass="button1"
                                            Text="Cancel" onclick="imgCancel_Click" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="imgRetrival" />
                        </Triggers>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>--%>

            
        <table width="100%" border="0">
          <tr>
            <td width="24%"><img src="images/cms.gif" width="233" height="68" /></td>
            <td width="58%">&nbsp;</td>
            <td width="18%" align="right" valign="bottom" id = "tdMassage" runat ="server"  >
</td>
          </tr>
        </table></td>
      </tr>
      <tr>
        <td background="images/tdbg.png" spry:repeat="x" height="140">&nbsp;</td>
      </tr>
    </table>
    <%--<div>
                <asp:Panel ID="pnlForgotPW" runat="server" Width="400" BorderColor="#6690C0"
                    BorderStyle="Solid" BorderWidth="1px" BackColor="White">
                    <asp:UpdatePanel ID="udpInner" runat="Server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table width="100%">
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblpopupErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center" style="background-color: #005DCE;">
                                        <asp:Label ID="Label5" runat="server" Text="Forgot Password" Font-Names="Verdana"
                                         Font-Size="Medium" ForeColor="#FFFFFF" Height="30px"></asp:Label>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="Label4" runat="server"  Font-Names="Verdana" Font-Size="Small"
                                         ForeColor="Maroon"><font color="red">*</font>Email:</asp:Label>
                                    </td>
                                      <td align="left">
                                        <asp:TextBox ID="txtEmail" CssClass="input_text" runat="server" Font-Names="Verdana"
                                            Font-Size="Small" MaxLength="40" Width="150px" Columns="40"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="right" colspan="2">
                                        <asp:LinkButton ID="lbRetrival" runat="server" CssClass="button1" 
                                            ForeColor="White" onclick="lbRetrival_Click" Text="Retrieve" />
                                        &nbsp;
                                        <asp:LinkButton ID="lbCancel" runat="server" CssClass="button1" 
                                            ForeColor="White" onclick="lbCancel_Click" Text="Cancel" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>--%>
    </form> 
  </div>
  </center>
  </body>
</html>
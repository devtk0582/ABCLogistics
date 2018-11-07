<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctlEmptyCart.ascx.cs" Inherits="CMS.UserControls.ctlEmptyCart" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<input id="targeAddNewSize" type="button" style="display: none" runat="server" />
<asp:ModalPopupExtender ID="ModlPopupEC" runat="server" PopupControlID="panelEC" 
                        TargetControlID="targeAddNewSize" BackgroundCssClass="popupBG">
</asp:ModalPopupExtender>

<asp:Panel ID="panelEC" runat="server">
    <div class="PopupFormBG" style="width:400px; background-color:Silver; padding-bottom:1px;">
        <div  class="PopupFormTitleStyle" style="height:30px; vertical-align:middle;" align="left">
        <div style="padding-top:5px;">
            &nbsp;<asp:Label ID="lblHeader" runat="server" Font-Size="11pt" Font-Bold="true" Text ="Empty Cart"></asp:Label>
        </div>
        </div>
        <div class="windowBody"  style=" width:auto; background-color:White; padding:10px 10px 10px 10px;">
           
          <div class="tblLabelStyle"><asp:Label ID="lblErrorMag" runat="server" >This Will Remove all the items from your cart. Are you Sure?</asp:Label></div>
          
          <div>&nbsp;</div>
            <div style="padding:3px 0px 3px 0px;">
              
                
                <div style="float:right;">
                    <asp:LinkButton ID="lnkBtnCancel" runat="server"  CssClass="popupButton" >Cancel</asp:LinkButton>
                </div> 
                <div style="float:right; padding-right:10px;">
                    <asp:LinkButton ID="lnkBtnok"  CssClass="popupButton"   runat="server" 
                        onclick="lnkBtnok_Click">OK</asp:LinkButton>
                </div>
            </div>
             <div>&nbsp;</div>
        </div>

</div>

</asp:Panel>
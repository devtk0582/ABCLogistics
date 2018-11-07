<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctlConfirm.ascx.cs" Inherits="CMS.UserControls.ctlConfirm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<input id="targeConfirm" type="button" style="display: none" runat="server" />
<asp:ModalPopupExtender ID="MPEConfirm" runat="server" PopupControlID="pnlConfirm" 
                        TargetControlID="targeConfirm" BackgroundCssClass="popupBG">
</asp:ModalPopupExtender>

<asp:Panel ID="pnlConfirm" runat="server">
    <div class="PopupFormBG" style="width:600px; background-color:Silver; padding-bottom:1px;">
        <div  class="PopupFormTitleStyle" style="height:30px; vertical-align:middle;" align="left">
        <div style="padding-top:5px;">
            &nbsp;<asp:Label ID="lblHeader" runat="server" Font-Size="11pt" Font-Bold="true" Text ="Success!"></asp:Label>
        </div>
        </div>
        <div class="windowBody"  style=" width:auto; background-color:White; padding:10px 10px 10px 10px;">
           
          <div><asp:Label ID="lblErrorMagConfirm" runat="server" Font-Bold="true" ></asp:Label></div>
          
          <div>&nbsp;</div>
          <div>
            <iframe width="100%" height="600px" name="preview" id="iframePreview" runat="server"></iframe>
          </div>
            <div style="padding:3px 0px 3px 0px;">
              
                
                
                <div style="float:right; padding-right:10px;">
                    <asp:LinkButton ID="lnkBtnok"  CssClass="popupButton"   runat="server" 
                        onclick="lnkBtnok_Click">OK</asp:LinkButton>
                </div>
            </div>
             <div>&nbsp;</div>
        </div>

</div>

</asp:Panel>

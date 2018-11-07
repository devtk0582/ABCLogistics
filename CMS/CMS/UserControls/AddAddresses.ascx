<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddAddresses.ascx.cs"
    Inherits="CMS.UserControls.AddAddresses" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<input id="dummy" type="button" style="display: none" runat="server" />
<ajaxToolkit:ModalPopupExtender runat="server" ID="mpePopup" TargetControlID="dummy"
    PopupControlID="pnlPopUp" BackgroundCssClass="popupBG" DropShadow="true" />
<asp:Panel ID="pnlPopUp" runat="server" CssClass="modalPopup" ScrollBars="Auto">
    <center>

     <script language="javascript" type="text/ecmascript">
         function onlyNumbers(evt) {
             var e = event || evt; // for trans-browser compatibility    
             var charCode = e.which || e.keyCode;

             if ((charCode > 31 && (charCode < 46 || charCode > 57)) || charCode == 47)
                 return false;

             return true;
         }
        </script>

        <asp:UpdatePanel ID="upAddUser" runat="server" UpdateMode="Conditional" Visible="false"
            Width="850px" Height="350px">
            <ContentTemplate>
                <table class="PopupFormBG" width="80%">
                   <tr style="height: 30px;">
                        <td align="left" class="PopupFormTitleStyle">
                            <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <table width="100%" class="tblLabelStyle">
                                <tr>
                                    <td colspan="4" align="left">
                                        <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;" width="30%" align="left">
                                        Name<font color="red">*</font>:
                                    </td>
                                    <td style="font-weight: bold;" width="20%" align="left">
                                        <asp:TextBox Columns="20" ID="txtAddName" TabIndex="1" runat="server" CssClass="newSizeTxtBx"
                                            MaxLength="20"></asp:TextBox>
                                    </td>
                                    <td style="font-weight: bold;" width="30%" align="left">
                                        &nbsp;
                                        <asp:Label ID="lblAddID" runat="server" Visible="false" ></asp:Label>
                                    </td>
                                    <td style="font-weight: bold;" width="20%" align="left">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;" width="30%" align="left">
                                        Address<font color="red">*</font>:
                                    </td>
                                    <td style="font-weight: bold;" width="20%" align="left">
                                         <asp:TextBox Columns="30" ID="txtAdd1" TabIndex="2" runat="server" CssClass="newSizeTxtBx"
                                            MaxLength="70"></asp:TextBox>
                                    </td>
                                    <td style="font-weight: bold;" width="30%" align="left">
                                        Address 2:
                                    </td>
                                    <td style="font-weight: bold;" width="20%" align="left">
                                        <asp:TextBox Columns="30" ID="txtAdd2" TabIndex="3" runat="server" CssClass="newSizeTxtBx"
                                            MaxLength="70"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;" width="30%" align="left">
                                        Country<font color="red">*</font>:
                                    </td>
                                    <td style="font-weight: bold;" width="20%" align="left">
                                        <asp:DropDownList ID="ddlCountry" Width="143px" TabIndex="4"  
                                            AutoPostBack="true" CssClass="DropDown_Style"
                                            runat="server" onselectedindexchanged="ddlCountry_SelectedIndexChanged1">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="font-weight: bold;" width="30%" align="left">
                                        State<font color="red">*</font>:
                                    </td>
                                    <td style="font-weight: bold;" width="20%" align="left">
                                          <asp:DropDownList ID="ddlState" width="143px" AutoPostBack="true" TabIndex="5" 
                                              CssClass="DropDown_Style" runat="server" 
                                              onselectedindexchanged="ddlState_SelectedIndexChanged1">
                                                                                                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;" width="30%" align="left">
                                        City<font color="red">*</font>:
                                    </td>
                                    <td style="font-weight: bold;" width="20%" align="left">
                                        
                                           <asp:DropDownList ID="ddlCity" CssClass="DropDown_Style" width="143px" runat="server">
                                                                                                                        </asp:DropDownList>
                                            
                                    </td>
                                    <td style="font-weight: bold;" width="30%" align="left">
                                        Zip<font color="red">*</font>:
                                    </td>
                                    <td style="font-weight: bold;" width="20%" align="left">
                                       <asp:TextBox Columns="30" ID="txtZip" TabIndex="7" runat="server" CssClass="newSizeTxtBx"
                                             MaxLength="10" Width="94px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;" width="30%" align="left">
                                        Phone:
                                    </td>
                                    <td colspan="3" style="font-weight: bold;" width="70%" align="left">
                                        <asp:TextBox Columns="30" ID="txtPhone" TabIndex="8" runat="server" CssClass="newSizeTxtBx"
                                            MaxLength="20"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="height: 50px;">
                        <td align="right">
                            <table  class="tblLabelStyle">
                                <tr>
                                    <td align="center">
                                        <asp:LinkButton ID="lbSave" runat="server" Text="Add"
                                             CssClass="addButtonStyle"
                                            onclick="lbSave_Click" ></asp:LinkButton>
                                    </td>
                                    <td align="center">
                                        <asp:LinkButton ID="lbCancel" runat="server" Text="Cancel" 
                                             CssClass="addButtonStyle"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </center>
</asp:Panel>

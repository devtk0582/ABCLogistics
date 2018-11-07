<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddUser.ascx.cs" Inherits="CMS.UserControls.AddUser" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<input id="dummy" type="button" style="display: none" runat="server" />
<ajaxToolkit:ModalPopupExtender runat="server" ID="mpePopup" TargetControlID="dummy"
    PopupControlID="pnlPopUp" BackgroundCssClass="popupBG" DropShadow="true" />
<asp:Panel ID="pnlPopUp" runat="server" CssClass="modalPopup" ScrollBars="Auto">
    <center>
        <asp:UpdatePanel ID="upAddUser" runat="server" UpdateMode="Conditional"  Visible="false"
            Width="850px" Height="350px">
            <ContentTemplate>
            <asp:Panel ID="pnlScrol" ScrollBars="Vertical" Height="650px" runat="server">
                <table class="PopupFormBG" width="80%">
                 <tr>
                    <td align="center">
                        <div style="width: 100%; text-align: center;">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                <ProgressTemplate>
                                    <div class="updateprogress">
                                        Please Wait...<br />
                                        <img src="../Images/progress.gif" alt="" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                    </td>
                </tr>
                    <tr style="height: 30px;" >
                        <td align="left" class="PopupFormTitleStyle">
                            <asp:Label ID="lblTitle" runat="server" Text=""
                                 ></asp:Label>
                        </td>
                    </tr>
                   
                    <tr style="height: 100px;">
                        <td align="center">
                            <table width="100%" class="tblLabelStyle">
                                <tr>
                                    <td colspan="4" align="left">
                                        <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;" width="30%" align="left">
                                        First Name<font color="red">*</font>:
                                    </td>
                                    <td style="font-weight: bold;" width="20%" align="left">
                                        <asp:TextBox Columns="30" ID="txtFirstName" TabIndex="1" runat="server"
                                            CssClass="textbox" MaxLength="30"></asp:TextBox>
                                    </td>
                                       <td style="font-weight: bold;" width="30%" align="left">
                                        Last Name<font color="red">*</font>:
                                    </td>
                                     <td style="font-weight: bold;" width="20%" align="left">
                                        <asp:TextBox Columns="30" ID="txtLastName" TabIndex="2" runat="server"
                                            CssClass="textbox" MaxLength="30"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                  <td style="font-weight: bold;" width="30%" align="left">
                                        Role<font color="red">*</font>:
                                    </td>
                                    <td style="font-weight: bold;" width="20%" align="left">
                                       <asp:DropDownList ID="ddlRoles" TabIndex="3" runat="server">
                                              <asp:ListItem Value="-1" >Select</asp:ListItem>
                                              <asp:ListItem Value="57">CMS Admin User</asp:ListItem>
                                              <asp:ListItem Value="58">CMS Sales Rep</asp:ListItem>
                                       </asp:DropDownList>
                                       
                                    </td>
                                       <td style="font-weight: bold;" width="30%" align="left">
                                        E-mail<font color="red">*</font>:
                                    </td>
                                     <td style="font-weight: bold;" width="20%" align="left">
                                        <asp:TextBox Columns="30" ID="txtEmail" TabIndex="4" runat="server"
                                            CssClass="textbox" MaxLength="70"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;" width="30%" align="left">
                                        Phone1:
                                    </td>
                                    <td style="font-weight: bold;" width="20%" align="left">
                                        <asp:TextBox Columns="30" ID="txtPhone1" TabIndex="5" runat="server"
                                            CssClass="textbox" MaxLength="50"></asp:TextBox>
                                    </td>
                                       <td style="font-weight: bold;" width="30%" align="left">
                                         Phone2:
                                    </td>
                                     <td style="font-weight: bold;" width="20%" align="left">
                                        <asp:TextBox Columns="30" ID="txtPhone2" TabIndex="6" runat="server"
                                            CssClass="textbox" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                 <tr>
                                    <td style="font-weight: bold;" width="30%" align="left">
                                       Password<font color="red">*</font>:
                                    </td>
                                    <td style="font-weight: bold;" width="20%" align="left">
                                        <asp:TextBox Columns="30" ID="txtPassword" TabIndex="7" runat="server"
                                            CssClass="textbox" TextMode="Password" MaxLength="8"></asp:TextBox>
                                            <asp:Label ID="lblPassword" runat="server" Visible="false" ></asp:Label>
                                            <asp:Label ID="lblPasswordRename" runat="server" Visible="false" ></asp:Label>
                                    </td>
                                       <td style="font-weight: bold;" width="30%" align="left">
                                         Active:
                                    </td>
                                     <td style="font-weight: bold;" width="20%" align="left">
                                       <asp:CheckBox ID="chkActive" TabIndex="8" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                   <td style="font-weight: bold;" width="30%" align="left">
                                       Security Question 1:
                                    </td>
                                    <td style="font-weight: bold;" width="20%" align="left">
                                    <asp:DropDownList ID="ddlSecQuestion1" TabIndex="9" runat="server"></asp:DropDownList>
                                    </td>
                                     <td style="font-weight: bold;" width="30%" align="left">
                                       Security Answer 1:
                                    </td>
                                     <td style="font-weight: bold;" width="20%" align="left">
                                     <asp:TextBox Columns="30" ID="txtSeQuAns1" TabIndex="10" runat="server"
                                            CssClass="textbox" TextMode="MultiLine"   MaxLength="200"></asp:TextBox>
                                    </td>
                                </tr>
                                
                                 <tr>
                                  <td style="font-weight: bold;" width="30%" align="left">
                                       Security Question 2:
                                    </td>
                                     <td style="font-weight: bold;" width="20%" align="left">
                                    <asp:DropDownList ID="ddlSecQuestion2" TabIndex="11" runat="server"></asp:DropDownList>
                                    </td>
                                      <td style="font-weight: bold;" width="30%" align="left">
                                       Security Answer 2:
                                    </td>
                                   <td style="font-weight: bold;" width="20%" align="left">
                                     <asp:TextBox Columns="30" ID="txtSeQuAns2" TabIndex="12" runat="server"
                                            CssClass="textbox" TextMode="MultiLine"   MaxLength="200"></asp:TextBox>
                                    </td>
                                </tr>
                                 
                            </table>
                            <table width="100%">
                           <tr style="height: 30px;" >
                        <td colspan="4" align="left" class="PopupFormTitleStyle">
                            <asp:Label ID="lblPrimarttitle" runat="server" Text="Primary Address"
                                 ></asp:Label>
                        </td>
                    </tr>
                            <tr>
                             <td style="font-weight: bold;" width="30%" align="left">
                             Choose a previous address:
                             </td>
                                 <td colspan="3" align="left" >
                                  <asp:DropDownList ID="ddlAddress" runat="server" Width="250px" 
                                            AutoPostBack="true" onselectedindexchanged="ddlAddress_SelectedIndexChanged" >
                                        </asp:DropDownList></td>
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
                                        Country:
                                    </td>
                                    <td style="font-weight: bold;" width="20%" align="left">
                                        <asp:DropDownList ID="ddlCountry" Width="143px" TabIndex="4"  
                                            AutoPostBack="true" CssClass="DropDown_Style"
                                            runat="server" onselectedindexchanged="ddlCountry_SelectedIndexChanged1">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="font-weight: bold;" width="30%" align="left">
                                        State:
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
                                        City:
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
                                <tr style="height: 30px;" >
                        <td colspan="4" align="left" class="PopupFormTitleStyle">
                            <asp:Label ID="lblSecondaryAddTitle" runat="server" Text="Secondary Address"
                                 ></asp:Label>
                        </td>
                    </tr>

                       <tr>
                         <td style="font-weight: bold;" width="30%" align="left">
                         Choose a previous address:
                         </td>
                                 <td colspan="3" align="left" >
                                  <asp:DropDownList ID="ddlAddress1" runat="server" Width="250px" 
                                            AutoPostBack="true" onselectedindexchanged="ddlAddress1_SelectedIndexChanged" >
                                        </asp:DropDownList></td>
                            </tr>
                             <tr>
                                    <td style="font-weight: bold;" width="30%" align="left">
                                        Name<font color="red">*</font>:
                                    </td>
                                    <td style="font-weight: bold;" width="20%" align="left">
                                        <asp:TextBox Columns="20" ID="txtAddNamesec" TabIndex="1" runat="server" CssClass="newSizeTxtBx"
                                            MaxLength="20"></asp:TextBox>
                                    </td>
                                    <td style="font-weight: bold;" width="30%" align="left">
                                        &nbsp;
                                        <asp:Label ID="Label2" runat="server" Visible="false" ></asp:Label>
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
                                         <asp:TextBox Columns="30" ID="txtAddSecon" TabIndex="2" runat="server" CssClass="newSizeTxtBx"
                                            MaxLength="70"></asp:TextBox>
                                    </td>
                                    <td style="font-weight: bold;" width="30%" align="left">
                                        Address 2:
                                    </td>
                                    <td style="font-weight: bold;" width="20%" align="left">
                                        <asp:TextBox Columns="30" ID="txtAdd2Sec" TabIndex="3" runat="server" CssClass="newSizeTxtBx"
                                            MaxLength="70"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;" width="30%" align="left">
                                        Country:
                                    </td>
                                    <td style="font-weight: bold;" width="20%" align="left">
                                        <asp:DropDownList ID="ddlCountry1" Width="143px" TabIndex="4"  
                                            AutoPostBack="true" CssClass="DropDown_Style"
                                            runat="server" onselectedindexchanged="ddlCountry1_SelectedIndexChanged1">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="font-weight: bold;" width="30%" align="left">
                                        State:
                                    </td>
                                    <td style="font-weight: bold;" width="20%" align="left">
                                          <asp:DropDownList ID="ddlState1" width="143px" AutoPostBack="true" TabIndex="5" 
                                              CssClass="DropDown_Style" runat="server" 
                                              onselectedindexchanged="ddlState1_SelectedIndexChanged1">
                                                                                                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;" width="30%" align="left">
                                        City:
                                    </td>
                                    <td style="font-weight: bold;" width="20%" align="left">
                                        
                                           <asp:DropDownList ID="ddlCity1" CssClass="DropDown_Style" width="143px" runat="server">
                                                                                                                        </asp:DropDownList>
                                            
                                    </td>
                                    <td style="font-weight: bold;" width="30%" align="left">
                                        Zip<font color="red">*</font>:
                                    </td>
                                    <td style="font-weight: bold;" width="20%" align="left">
                                       <asp:TextBox Columns="30" ID="txtZipSec" TabIndex="7" runat="server" CssClass="newSizeTxtBx"
                                             MaxLength="10" Width="94px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;" width="30%" align="left">
                                        Phone:
                                    </td>
                                    <td colspan="3" style="font-weight: bold;" width="70%" align="left">
                                        <asp:TextBox Columns="30" ID="txtPhoneSec" TabIndex="8" runat="server" CssClass="newSizeTxtBx"
                                            MaxLength="20"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="height: 50px;">
                        <td align="right">
                            <table  >
                                <tr>
                                    <td align="center">
                                        <asp:LinkButton ID="lbSave" runat="server" Text="Add" CssClass="addButtonStyle" OnClick="lbSave_Click"></asp:LinkButton>
                                    </td>
                                    <td align="center">
                                        <asp:LinkButton ID="lbCancel" runat="server" Text="Cancel" CssClass="addButtonStyle" OnClick="lbCancel_Click"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

                </asp:Panel>
            </ContentTemplate>

        </asp:UpdatePanel>
    </center>
</asp:Panel>

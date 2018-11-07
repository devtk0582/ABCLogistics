<%@ Page Title="Colors" Language="C#" MasterPageFile="~/MasterPages/ABCLogisticsCMS.Master" AutoEventWireup="true" CodeBehind="Colors.aspx.cs" Inherits="CMS.Catalog.Colors" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updatePanel" runat="server">
    <ContentTemplate>
     <table width="100%" cellspacing="1" cellpadding="1" >
        <tr>
            <td align="center" colspan="2">
                        <div style="width: 100%; text-align: center; margin-top: 15px;">
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
        <tr valign="top">
        <td align="left">
                <asp:Label ID="lblColors" CssClass="titleStyle" runat="server" Text="Colors"></asp:Label>
            </td>
            <td align="right" >
                <asp:LinkButton ID="lnlBtnAddNewColor" runat="server" CssClass="addButtonStyle" OnClick="AddNewColor_Click">Add New Color</asp:LinkButton>
            </td>
        </tr>
        <tr>
                    <td colspan="2">
                        <asp:Label ID="lblErr" Visible="false" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr valign="top">
            <td colspan="2">
            <asp:Label ID="lblErrColor" runat="server" CssClass="errMessage" Visible="false"></asp:Label>
            </td>
        </tr>
                <tr>
            <td colspan="2" style="padding-top: 20px;">
                <div style="overflow:auto; height:350px; overflow-x: hidden"  align="center" class="divlbs">
                    <asp:DataList ID="dlColors" runat="server" RepeatColumns="5"   OnItemDataBound="DataList_OnItemDataBound"
                                       RepeatDirection="Horizontal"  OnItemCommand="DataList_OnItemCommand" Width="100%" >                        
                        <ItemTemplate>
                            <table id="TableItemTemplate" runat="server" class="itemStyle" style="width:90%; height:100%; table-layout:fixed;"  cellpadding="1" cellspacing="2" >
                                <tr id="TableRowItem" style="height:20px;">
                                    <td id="tcItemData" class="itemRowStyle" style="width:85px; height:20px; overflow:hidden; text-align:left; padding-left:4px; " runat="server">
                                        <asp:Label ID="Label1" runat="server" Text="Color Name:" ></asp:Label>                                         
                                     </td>
                                     <td id="Td1" class="itemRowStyle" style="width:100px; height:20px; overflow:hidden; text-align:left;" runat="server">
                                        <asp:LinkButton ID="lnkbtnColorName" runat="server" CommandName="PopupColor" 
                                                                 CommandArgument='<%# bind("Color_Id") %>' Text= ' <%# bind("Color_Name") %>'> </asp:LinkButton>
                                     </td>
                                 </tr>
                                 <tr id="Tr1" style="height:20px;">
                                    <td id="Td2" class="itemRowStyle" style="width:85px; height:20px; overflow:hidden; text-align:left; padding-left:4px; " runat="server">
                                        <asp:Label ID="Label2" runat="server" Text="Code: "></asp:Label>
                                    </td>
                                    <td id="Td3" class="itemRowStyle" style="width:100px; height:20px; text-align:left;" runat="server">
                                        <asp:Label ID="lblColorCode" runat="server" Text=' <%# bind("Color_Code") %>'></asp:Label>
                                    </td>
                                 </tr>
                                 <tr id="Tr2" style="height:20px;">
                                    <td id="Td4" class="itemRowStyle" style="width:85px; height:20px; overflow:hidden; text-align:left; padding-left:4px; " runat="server">
                                        <asp:Label ID="Label3" runat="server" Text="Value:"></asp:Label>
                                    </td>
                                    <td id="Td5" class="itemRowStyle" style="width:100px; height:20px; overflow:hidden; text-align:left;" runat="server">
                                        <asp:Label ID="lblColorValue" runat="server" Text=' <%# bind("Color_Val") %>'></asp:Label>
                                    </td>
                                 </tr>
                            </table>
                        </ItemTemplate>    
                    </asp:DataList>            
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <input id="dummy3" type="button" style="display: none" runat="server" />
                <asp:ModalPopupExtender ID="ModlPopupColor" runat="server" PopupControlID="pnlClrModlPopup" 
                                        TargetControlID="dummy3" BackgroundCssClass="popupBG" DropShadow="true">
                </asp:ModalPopupExtender>
                <asp:Panel ID="pnlClrModlPopup" runat="server">
                <table class="PopupFormBG" width="60%">
                <tr style="height: 30px;">
                        <td colspan="2" align="left" class="PopupFormTitleStyle">
                           <asp:Label ID="lblNewSize" runat="server" Text="Add New Color"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblErrMessage" CssClass="errMessage" runat="server"></asp:Label>
                        </td>
                    </tr>                   
                    <tr>
                        <td style="font-weight: bold;" width="40%" align="left">
                            Color Name:
                        </td>
                        <td style="font-weight: bold;" width="60%" align="left">
                            <asp:TextBox ID="txtBxNewColorName"  runat="server" MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                       <td style="font-weight: bold;" width="40%" align="left">
                            Code:
                        </td>
                        <td style="font-weight: bold;" width="60%" align="left">
                            <asp:TextBox ID="txtBxNewColorCode" runat="server" MaxLength="5" Columns="5"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold;" width="40%" align="left">
                            Value:
                        </td>
                        <td style="font-weight: bold;" width="60%" align="left">
                            <asp:TextBox ID="txtBxNewColorvalue"  runat="server" Columns="6" MaxLength="6" ></asp:TextBox>
                            <asp:ColorPickerExtender ID="ColorPickerExtender1" runat="server"
                                      targetcontrolid="txtBxNewColorvalue" PopupButtonID="txtBxNewColorvalue"
                                      PopupPosition="Right" SampleControlID="txtBxNewColorvalue">
                            </asp:ColorPickerExtender>
                        </td>
                    </tr>     
                    <tr style="height: 50px;">
                        <td align="right" colspan="2">
                            <table width="30%">
                                <tr>
                                    <td align="center">
                                        <asp:LinkButton ID="lnkBtnNewColor" CssClass="addButtonStyle" runat="server" OnClick="InsertNewColor_Click">Save</asp:LinkButton>
                                    </td>
                                    <td align="center">
                                       <asp:LinkButton ID="lnkBtnNewColorCancel" CssClass="addButtonStyle" OnClick="CancelInsertNewColor_Click" runat="server">Cancel</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>            
                </table>
                </asp:Panel>
            </td>
            <td>
                <input id="dummy4" type="button" style="display: none" runat="server" />
                    <asp:ModalPopupExtender ID="MdlPopupColorEdit" PopupControlID="pnlModlPopupEditColor" 
                                            runat="server" TargetControlID="dummy4" BackgroundCssClass="popupBG" 
                                            DropShadow="true">
                    </asp:ModalPopupExtender>
                        <asp:Panel ID="pnlModlPopupEditColor" runat="server">   
                        <asp:HiddenField ID="hfColor" runat="server" />                             
                            <table class="PopupFormBG" width="60%">
                            <tr style="height: 30px;">
                        <td colspan="2" align="left" class="PopupFormTitleStyle">
                            <asp:Label ID="Label4" runat="server" Text="Update Color"></asp:Label>
                        </td>
                    </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblErrMessageUpdate" CssClass="errMessage" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                   <td style="font-weight: bold;" width="40%" align="left">                                           
                                        Color Name:
                                    </td>
                                    <td style="font-weight: bold;" width="60%" align="left">
                                        <asp:TextBox ID="txtBxColorName" runat="server" MaxLength="20"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;" width="40%" align="left">
                                        Code:
                                    </td>
                                    <td style="font-weight: bold;" width="60%" align="left">
                                        <asp:TextBox ID="txtBxColorCode" runat="server" MaxLength="5" Columns="5"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;" width="40%" align="left">
                                        Value:
                                    </td>
                                    <td style="font-weight: bold;" width="60%" align="left">
                                        <asp:TextBox ID="txtBxColorValue" runat="server" MaxLength="6" Columns="6"></asp:TextBox>
                                        <asp:ColorPickerExtender ID="ColorPickerExtender2" runat="server" PopupPosition="Right"
                                                                    SampleControlID="txtBxColorValue" targetcontrolid="txtBxColorValue" >
                                        </asp:ColorPickerExtender>
                                    </td>
                                </tr>
                                <tr style="height: 50px;">
                        <td colspan="2" align="right">
                            <table width="30%" class="tblLabelStyle">
                                <tr>
                                    <td align="center">
                                                                                   <asp:LinkButton ID="lnkUpdateColor" CssClass="addButtonStyle" 
                                                        OnClick="UpdateColor_Click" runat="server">Update </asp:LinkButton>   
                                    </td>
                                    <td align="center">
                                         <asp:LinkButton ID="lnkDeleteColor" CssClass="addButtonStyle" 
                                                        OnClick="DeleteColor_Click" runat="server">Delete </asp:LinkButton>
                                    </td>
                                    <td align="center">
                                         <asp:LinkButton ID="lnkCancelColor" CssClass="addButtonStyle"
                                                        OnClick="CancelUpdate_Click" runat="server">Cancel </asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>                     
                            </table>
                        </asp:Panel>
            </td>
        </tr>
        </table>

    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

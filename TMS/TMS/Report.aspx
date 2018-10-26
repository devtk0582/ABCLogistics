<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Report.aspx.cs" Inherits="TMS.Report" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link type="text/css" rel="stylesheet" href="Styles/demo_table.css" />
    <link type="text/css" rel="stylesheet" href="Styles/jquery-ui.min.css" />
    <link type="text/css" rel="stylesheet" href="Styles/custom_reporting.css" />
    <link type="text/css" rel="stylesheet" href="Styles/TableTools.css" />
    <script src="Scripts/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
    <script src="Scripts/TableTools.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $("#column_orig, #column_dest").sortable({
                connectWith: ".connectedSortable"
            }).disableSelection();

            $("#divColumnFilter").dialog({
                autoOpen: false,
                modal: true,
                hide: {
                    effect: "Fade",
                    duration: 1000
                },
                height: 600,
                width: 800,
                buttons: {
                    "Update": function () {
                        var origColumnData = $('#column_orig').sortable('toArray');
                        var destColumnData = $('#column_dest').sortable('toArray');

                        $.ajax({
                            data: JSON.stringify({ origColumns: origColumnData, destColumns: destColumnData }),
                            type: 'POST',
                            url: 'Report.aspx/SaveUserReportColumns',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.d == "SUCCESS") {
                                    $.ajax({
                                        type: "POST",
                                        url: "Report.aspx/GetUserReportColumns",
                                        data: "{}",
                                        contentType: "application/json; charset=utf-8",
                                        dataType: "json",
                                        success: function (data) {
                                            if (data != null && data.d != null) {
                                                $("#divColumnFilter").dialog("close");

                                                var dataTables = $.fn.dataTable.fnTables(true);
                                                if (dataTables.length > 0) {
                                                    $('#reportTable').dataTable().fnDestroy();
                                                    $('#reportTable').empty();
                                                }
                                                buildReport(data.d);
                                            }
                                        }
                                    });
                                }
                            }
                        });
                    },

                    Cancel: function () {
                        $(this).dialog("close");
                    }
                }
            });

            $("#btnColumnFilter").click(function () {
                $("#column_orig").empty();
                $("#column_dest").empty();

                $.ajax({
                    type: "POST",
                    url: "Report.aspx/GetUserReportColumnsMain",
                    data: "{}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data != null) {
                            if (data.d.ColumnOrig != null && data.d.ColumnOrig.length > 0) {
                                $.each(data.d.ColumnOrig, function (index, value) {
                                    $("<li/>", {
                                        id: "column-" + data.d.ColumnOrig[index].ColumnCode.toLowerCase(),
                                        class: "ui-state-default",
                                        text: data.d.ColumnOrig[index].ColumnName
                                    }).appendTo("#column_orig");
                                });
                            }

                            if (data.d.ColumnDest != null && data.d.ColumnDest.length > 0) {
                                $.each(data.d.ColumnDest, function (index, value) {
                                    $("<li/>", {
                                        id: "column-" + data.d.ColumnDest[index].ColumnCode.toLowerCase(),
                                        class: "ui-state-default",
                                        text: data.d.ColumnDest[index].ColumnName
                                    }).appendTo("#column_dest");
                                });
                            }
                            $("#divColumnFilter").dialog("open");
                        }
                    }
                });

            });

            $("#btnSearch").click(function () {
                $('#reportTable').dataTable().fnDraw();
            });

            $.ajax({
                type: "POST",
                url: "Report.aspx/GetUserReportColumns",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result != null && result.d != null && result.d.length > 0) {
                        buildReport(result.d);
                    }
                }
            });
        });

        function buildReport(data) {

            var columnDefTable = [];
            for (var i = 0; i < data.length; i++) {
                columnDefTable.push({
                    "sTitle": data[i].ColumnName,
                    "aTargets": [i]
                });
            };
            oTable = $('#reportTable').dataTable({
                "sDom": 'Tr<"H"lf><"dynamic"t><"F"ip>',
                "sPaginationType": "full_numbers",
                "aoColumnDefs": columnDefTable,
                "iDisplayLength": 10,
                "bProcessing": true,
                "bServerSide": true,
                "bLengthChange": true,
                "bScrollCollapse": true,
                "sScrollX": "100%",
                //"sScrollXInner": "100%",
                //"bFilter": true,
                "bSort": true,
                "bDestroy": true,
                "bFilter": false,
                "oTableTools": {
                    "aButtons": ["csv", "xls", "pdf",
                    {
                        "sExtends": "xls",
                        "sUrl": "ExcelReportGenerator.ashx",
                        "sAction": "text",
                        "sTag": "default",
                        "sFieldBoundary": "",
                        "sFieldSeperator": "\t",
                        "sNewLine": "<br>",
                        "sToolTip": "",
                        "sButtonClass": "DTTT_button_text",
                        "sButtonClassHover": "DTTT_button_text_hover",
                        "sButtonText": "Export All",
                        "mColumns": "all",
                        "bHeader": true,
                        "bFooter": true,
                        "sDiv": "",
                        "fnMouseover": null,
                        "fnMouseout": null,
                        "fnClick": function (nButton, oConfig) {
                            $("#iframeReport").remove();
                            var iframe = document.createElement('iframe');
                            iframe.setAttribute("id", "iframeReport");
                            iframe.style.height = "0px";
                            iframe.style.width = "0px";
                            document.body.appendChild(iframe);
                            var nContentWindow = iframe.contentWindow; 
                            nContentWindow.document.open(); 
                            nContentWindow.document.close();

                            var nForm = nContentWindow.document.createElement('form'); 
                            nForm.setAttribute('method', 'post');

                            var aoPost = [
                            { "name" : "search_ship_date_from", "value": $("#<%=txtFromShipmentDate.ClientID %>").val()},
                            { "name" : "search_ship_date_to", "value" : $("#<%=txtToShipmentDate.ClientID %>").val()},
                            { "name" : "search_quote", "value" : $("#<%=cbQuote.ClientID %>").is(":checked")},
                            { "name" : "search_service", "value" : $("#<%=ddlServices.ClientID %> :selected").text()},
                            { "name" : "search_terminal", "value" : $("#<%=txtTerminal.ClientID %>").val() }
                            ];
                            for (var i = 0; i < aoPost.length; i++) {
                                nInput = nContentWindow.document.createElement('input');
                                nInput.setAttribute('name', aoPost[i].name);
                                nInput.setAttribute('type', 'text');
                                nInput.value = aoPost[i].value;
                                nForm.appendChild(nInput); 
                            }

                            nForm.setAttribute('action', oConfig.sUrl);
                            nContentWindow.document.body.appendChild(nForm);
                            nForm.submit();
                            
                        },
                        "fnSelect": null,
                        "fnComplete": null,
                        "fnInit": null
                    }
                    ],
                    "sSwfPath": "SWF/copy_csv_xls_pdf.swf"
                },
                "oLanguage": {
                    "sProcessing": "Loading..."
                },
                "sServerMethod": "POST",
                "sAjaxSource": "ReportGenerator.ashx",
                "fnServerParams": function (aoData) {
                    aoData.push({ "name": "search_ship_date_from", "value": $("#<%=txtFromShipmentDate.ClientID %>").val() });
                    aoData.push({ "name": "search_ship_date_to", "value": $("#<%=txtToShipmentDate.ClientID %>").val() });
                    aoData.push({ "name": "search_quote", "value": $("#<%=cbQuote.ClientID %>").is(":checked") });
                    aoData.push({ "name": "search_service", "value": $("#<%=ddlServices.ClientID %> :selected").text() });
                    aoData.push({ "name": "search_terminal", "value": $("#<%=txtTerminal.ClientID %>").val() });
                }
            });
        }

        function SearchData() {
            $('#reportTable').dataTable().fnDraw();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div style="background-color: #3580B7; height: 15px;">
    </div>
    <div id="container" style="margin: 30px 5px 5px 5px">
        <table width="100%" cellpadding="3" cellspacing="2">
            <tr>
                <td>
                    <div>
                        <table id="filterTbl">
                            <tr>
                                <td>
                                    From:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFromShipmentDate" runat="server"></asp:TextBox>
                                    &nbsp; 
                                    <asp:ImageButton ID="ibtnFromShipmentDate" runat="server" ImageAlign="Middle" AlternateText="Click to show calendar" ImageUrl="~/Images/calendar_button.png"  />
                                    <asp:MaskedEditExtender ID="txtFromShipmentDate_MaskedEditExtender" runat="server" 
                                        Mask="99/99/9999" Enabled="True" 
                                        TargetControlID="txtFromShipmentDate" MaskType="Date">
                                    </asp:MaskedEditExtender>
                                    <asp:CalendarExtender ID="txtFromShipmentDate_CalendarExtender" runat="server" 
                                        Enabled="True" TargetControlID="txtFromShipmentDate" Format="MM/dd/yyyy" PopupButtonID="ibtnFromShipmentDate">
                                    </asp:CalendarExtender>

                                </td>
                                <td>
                                    To:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtToShipmentDate" runat="server"></asp:TextBox>
                                    &nbsp; 
                                    <asp:ImageButton ID="ibtnToShipmentDate" runat="server" ImageAlign="Middle" AlternateText="Click to show calendar" ImageUrl="~/Images/calendar_button.png"  />
                                    <asp:MaskedEditExtender ID="txtToShipmentDate_MaskedEditExtender" runat="server" 
                                        Mask="99/99/9999" Enabled="True" 
                                        TargetControlID="txtToShipmentDate" MaskType="Date">
                                    </asp:MaskedEditExtender>
                                    <asp:CalendarExtender ID="txtToShipmentDate_CalendarExtender" runat="server" 
                                        Enabled="True" TargetControlID="txtToShipmentDate" Format="MM/dd/yyyy" PopupButtonID="ibtnToShipmentDate">
                                    </asp:CalendarExtender>
                                </td>
                                <td>
                                    Quote: 
                                </td>
                                <td>
                                    <asp:CheckBox ID="cbQuote" runat="server" />
                                </td>
                                <td>
                                    Service Level:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlServices" runat="server">
                                        <asp:ListItem Text="NONE" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="DIRECT IATA" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="STANDARD CONSOLIDATION" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="NEXT BUSINESS DAY AM" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="ECONOMY 3-5 BUSINESS DAY" Value="4"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    Terminal:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTerminal" runat="server"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <input id="btnSearch" type="button" value="Search" />&nbsp;&nbsp;&nbsp;
                                    <input id="btnColumnFilter" type="button" value="Column Filter" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="dynamic">
                        <table cellpadding="0" cellspacing="0" border="0" class="display" id="reportTable">
                            <tbody>
                                <tr>
                                    <td class="dataTables_empty">
                                        No Data Available
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <div id="divColumnFilter" >
            <table width="100%">
                <tr>
                    <td width="50%" valign="top">
                        <div style="border: 1px solid black; width: 350px; min-height: 200px; max-height: 450px;
                            display: inline-block; background-color: Gray; overflow: auto;">
                            <ul id="column_orig" class="connectedSortable">
                            </ul>
                        </div>
                    </td>
                    <td width="50%" valign="top">
                        <div style="border: 1px solid black; width: 350px; min-height: 200px; max-height: 450px;
                            display: inline-block; background-color: Gray; overflow: auto;">
                            <ul id="column_dest" class="connectedSortable">
                            </ul>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

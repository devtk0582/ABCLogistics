using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using ABCLogisticsBusinessLogic;

namespace TMS
{
    /// <summary>
    /// Summary description for ExcelReportGenerator
    /// </summary>
    public class ExcelReportGenerator : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Session["GlobalUserID"] != null)
            {
                int userId = Convert.ToInt32(context.Session["GlobalUserID"]);
                string searchFromShipDate = context.Request.Params["search_ship_date_from"];
                string searchToShipDate = context.Request.Params["search_ship_date_to"];
                string searchQuote = context.Request.Params["search_quote"];
                string searchService = context.Request.Params["search_service"];
                string searchTerminal = context.Request.Params["search_terminal"];

                GlobalReportBLL reportBLL = new GlobalReportBLL();

                List<ReportColumn> userColumns = reportBLL.GetUserMasterReportColumns(userId);

                if (userColumns.Count > 0)
                {
                    List<ShipmentReportView> shipments = reportBLL.GetUserExcelReportData(userId, searchFromShipDate, searchToShipDate, searchQuote, searchService, searchTerminal);

                    if (shipments.Count > 0)
                    {
                        using (ExcelPackage excelPackage = new ExcelPackage())
                        {
                            var workSheet = excelPackage.Workbook.Worksheets.Add("ShipmentReport");
                            workSheet.View.ShowGridLines = true;

                            int rowIndex = 1;
                            for (int i = 0; i < userColumns.Count; i++)
                            {
                                workSheet.Column(i + 1).Width = 50;
                                ExcelRange headerCell = workSheet.Cells[rowIndex, i + 1];
                                headerCell.Value = userColumns[i].ColumnName;
                                headerCell.Style.Font.Bold = true;
                                headerCell.Style.Font.Color.SetColor(Color.FloralWhite);
                                headerCell.Style.Font.Size = 10;
                                headerCell.Style.Font.Name = "Verdana";
                                headerCell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                headerCell.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));
                                headerCell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                headerCell.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                headerCell.Style.WrapText = true;
                            }
                            

                            rowIndex++;

                            foreach (ShipmentReportView shipment in shipments)
                            {
                                try
                                {
                                    for (int i = 0; i < userColumns.Count; i++)
                                    {
                                        workSheet.Cells[rowIndex, i + 1].Value = GetReportColumnValue(userColumns[i].ColumnCode, shipment);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    (new GlobalErrorLogsBLL()).LogGlobalError(ex, "TMS - ExcelReportGenerator - Shipments");
                                }
                                rowIndex++;
                            }

                            excelPackage.Workbook.Properties.Author = "ABCLogistics";
                            excelPackage.Workbook.Properties.Title = "Shipment Report";

                            context.Response.Clear();
                            context.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            context.Response.AddHeader("content-disposition", "attachment;  filename=ShipmentReport_" + DateTime.Now.ToString("MMddyyyyHHmm") + ".xlsx");
                            context.Response.BinaryWrite(excelPackage.GetAsByteArray());
                            context.Response.End();
                        }
                    }
                }
            }
        }

        private string GetReportColumnValue(string columnName, ShipmentReportView shipmentView)
        {
            string value = string.Empty;
            switch (columnName)
            {
                case "HAWB":
                    value = shipmentView.HAWB;
                    break;
                case "ABCLogisticsTRACKING":
                    value = shipmentView.ABCLogisticsTracking;
                    break;
                case "SHIPPERNAME":
                    value = shipmentView.ShipperName;
                    break;
                case "SHIPPERADDRESS":
                    value = shipmentView.ShipperAddress;
                    break;
                case "SHIPPERCITY":
                    value = shipmentView.ShipperCity;
                    break;
                case "SHIPPERSTATE":
                    value = shipmentView.ShipperState;
                    break;
                case "RECEIVERNAME":
                    value = shipmentView.ReceiverName;
                    break;
                case "RECEIVERADDRESS":
                    value = shipmentView.ReceiverAddress;
                    break;
                case "RECEIVERCITY":
                    value = shipmentView.ReceiverCity;
                    break;
                case "RECEIVERSTATE":
                    value = shipmentView.ReceiverState;
                    break;
                case "SERVICEDESC":
                    value = shipmentView.ServiceDesc;
                    break;
                case "TERMINAL":
                    value = shipmentView.Terminal;
                    break;
                case "CREATEDATE":
                    value = shipmentView.CreateDate.HasValue ? shipmentView.CreateDate.Value.ToShortDateString() : "";
                    break;
                case "ISQUOTE":
                    value = shipmentView.IsQuote.HasValue && shipmentView.IsQuote.Value ? "YES" : "NO";
                    break;
                case "PUAREA":
                    value = shipmentView.PUArea;
                    break;
                case "DELAREA":
                    value = shipmentView.DELArea;
                    break;
                case "FUELSURCHARGE":
                    value = shipmentView.NoFuelSurcharge.HasValue && !shipmentView.NoFuelSurcharge.Value ? "YES" : "NO";
                    break;
            }
            return value;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
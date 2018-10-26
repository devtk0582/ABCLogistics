using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using System.Text;
using ABCLogisticsBusinessLogic;

namespace TMS
{
    /// <summary>
    /// Summary description for ReportGenerator
    /// </summary>
    public class ReportGenerator : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Session["GlobalUserID"] != null)
            {
                int userId = Convert.ToInt32(context.Session["GlobalUserID"]);
                int echo = Int32.Parse(context.Request.Params["sEcho"]);
                int displayLength = Int32.Parse(context.Request.Params["iDisplayLength"]);
                int displayStart = Int32.Parse(context.Request.Params["iDisplayStart"]);
                string search = context.Request.Params["sSearch"];



                GlobalReportBLL reportBLL = new GlobalReportBLL();

                List<UserReportColumn> userColumns = reportBLL.GetUserReportColumns(userId);

                List<ShipmentReportView> shipments = reportBLL.GetUserReportData(userId, userColumns, context.Request.Params, displayStart, displayLength);

                string outputJson = string.Empty;
                int totalRecords = reportBLL.GetUserReportDataCount(userId, context.Request.Params);
                int totalDisplayRecords = totalRecords;

                if (shipments.Count > 0)
                {
                    StringBuilder sbOutputJson = new StringBuilder();
                    foreach (ShipmentReportView shipment in shipments)
                    {
                        sbOutputJson.Append("[");
                        for (int i = 0; i < userColumns.Count; i++)
                        {
                            if (i != userColumns.Count - 1)
                                sbOutputJson.Append("\"" + GetReportColumnValue(userColumns[i].ColumnCode, shipment) + "\",");
                            else
                                sbOutputJson.Append("\"" + GetReportColumnValue(userColumns[i].ColumnCode, shipment) + "\"");
                        }
                        sbOutputJson.Append("],");
                    }

                    outputJson = sbOutputJson.ToString();
                    outputJson = outputJson.Remove(outputJson.Length - 1);

                    sbOutputJson.Clear();
                    sbOutputJson.Append("{");
                    sbOutputJson.Append("\"sEcho\": ");
                    sbOutputJson.Append(echo);
                    sbOutputJson.Append(",");
                    sbOutputJson.Append("\"iTotalRecords\": ");
                    sbOutputJson.Append(totalRecords);
                    sbOutputJson.Append(",");
                    sbOutputJson.Append("\"iTotalDisplayRecords\": ");
                    sbOutputJson.Append(totalDisplayRecords);
                    sbOutputJson.Append(",");
                    sbOutputJson.Append("\"aaData\": [");
                    sbOutputJson.Append(outputJson);
                    sbOutputJson.Append("]}");
                    outputJson = sbOutputJson.ToString();
                }

                context.Response.Clear();
                context.Response.ClearHeaders();
                context.Response.ClearContent();
                context.Response.Write(outputJson);
                context.Response.Flush();
                context.Response.End();
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
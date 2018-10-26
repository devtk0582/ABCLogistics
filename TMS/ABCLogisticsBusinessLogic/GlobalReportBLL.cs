using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ABCLogisticsBusinessLogic
{
    public class GlobalReportBLL
    {
        private ABCLogisticsEntities entities;

        public GlobalReportBLL()
        {
            entities = new ABCLogisticsEntities();
        }

        public GlobalReportBLL(string connectionString)
        {
            entities = new ABCLogisticsEntities(connectionString);
        }

        public List<ReportColumn> GetMasterReportColumns()
        {
            return (from column in entities.ReportColumns
                    select column).ToList();
        }

        public ReportColumnMain GetUserReportColumnsMain(int userId)
        {
            List<ReportColumn> columnDest = (from column in entities.UserReportColumns
                                             join masterColumn in entities.ReportColumns on column.ColumnCode equals masterColumn.ColumnCode
                                            where column.UserID == userId
                                            orderby column.SortNum
                                            select masterColumn).ToList();

            List<ReportColumn> columnOrig = entities.Global_GetUserMasterReportColumns(userId).ToList();

            ReportColumnMain mainReportColumn = new ReportColumnMain()
            {
                ColumnOrig = columnOrig,
                ColumnDest = columnDest
            };

            return mainReportColumn;
        }

        public void SaveUserReportColumns(string[] origColumns, string[] destColumns, int userId)
        {
            for (int i = 0; i < destColumns.Length; i++)
            {
                string columnCode = destColumns[i].Split('-')[1].ToUpper();
                var userReportColumn = (from o in entities.UserReportColumns
                                        where o.ColumnCode == columnCode && o.UserID == userId
                                        select o).SingleOrDefault();

                if (userReportColumn == null)
                {
                    UserReportColumn newColumn = new UserReportColumn()
                    {
                        UserID = userId,
                        ColumnCode = columnCode,
                        SortNum = i + 1
                    };
                    entities.UserReportColumns.AddObject(newColumn);
                }
                else
                {
                    userReportColumn.SortNum = i + 1;
                }
            }

            foreach (string column in origColumns)
            {
                string columnCode = column.Split('-')[1].ToUpper();
                var removeColumn = (from o in entities.UserReportColumns
                                    where o.ColumnCode == columnCode && o.UserID == userId
                                   select o).SingleOrDefault();

                if (removeColumn != null)
                {
                    entities.UserReportColumns.DeleteObject(removeColumn);
                }
            }

            entities.SaveChanges();
        }

        public List<ShipmentReportView> GetUserReportData(int userId, List<UserReportColumn> userColumns, System.Collections.Specialized.NameValueCollection requestParams, int skipCount, int takeCount)
        {
            string searchFromShipDate = requestParams["search_ship_date_from"];
            string searchToShipDate = requestParams["search_ship_date_to"];
            string searchQuote = requestParams["search_quote"];
            string searchService = requestParams["search_service"];
            string searchTerminal = requestParams["search_terminal"];

            var reportQuery = (from shipment in entities.ShipmentReportViews
                               select shipment);

            if (!string.IsNullOrEmpty(searchFromShipDate))
            {
                DateTime fromShipDate = DateTime.Parse(searchFromShipDate);
                reportQuery = reportQuery.Where(o => o.CreateDate >= fromShipDate);
            }
            if (!string.IsNullOrEmpty(searchToShipDate))
            {
                DateTime toShipDate = DateTime.Parse(searchToShipDate);
                reportQuery = reportQuery.Where(o => o.CreateDate <= toShipDate);
            }
            if (!string.IsNullOrEmpty(searchQuote))
                reportQuery = reportQuery.Where(o => o.IsQuote == (searchQuote == "true" ? true : false));
            if (!string.IsNullOrEmpty(searchService) && searchService != "NONE")
                reportQuery = reportQuery.Where(o => o.ServiceDesc == searchService);
            if (!string.IsNullOrEmpty(searchTerminal))
                reportQuery = reportQuery.Where(o => o.Terminal.Contains(searchTerminal.ToUpper()));


            for (int i = 0; i < userColumns.Count; i++)
            {
                if (requestParams["bSortable_" + i] == "true" && !string.IsNullOrEmpty(requestParams["iSortCol_0"]) && Convert.ToInt32(requestParams["iSortCol_0"]) == i)
                {
                    switch (userColumns[i].ColumnCode)
                    {
                        case "HAWB":
                            if (requestParams["sSortDir_0"] == "asc")
                                reportQuery = reportQuery.OrderBy(o => o.HAWB);
                            else
                                reportQuery = reportQuery.OrderByDescending(o => o.HAWB);
                            break;
                        case "ABCLogisticsTRACKING":
                            if (requestParams["sSortDir_0"] == "asc")
                                reportQuery = reportQuery.OrderBy(o => o.ABCLogisticsTracking);
                            else
                                reportQuery = reportQuery.OrderByDescending(o => o.ABCLogisticsTracking);
                            break;
                        case "SHIPPERNAME":
                            if (requestParams["sSortDir_0"] == "asc")
                                reportQuery = reportQuery.OrderBy(o => o.ShipperName);
                            else
                                reportQuery = reportQuery.OrderByDescending(o => o.ShipperName);
                            break;
                        case "SHIPPERADDRESS":
                            if (requestParams["sSortDir_0"] == "asc")
                                reportQuery = reportQuery.OrderBy(o => o.ShipperAddress);
                            else
                                reportQuery = reportQuery.OrderByDescending(o => o.ShipperAddress);
                            break;
                        case "SHIPPERCITY":
                            if (requestParams["sSortDir_0"] == "asc")
                                reportQuery = reportQuery.OrderBy(o => o.ShipperCity);
                            else
                                reportQuery = reportQuery.OrderByDescending(o => o.ShipperCity);
                            break;
                        case "SHIPPERSTATE":
                            if (requestParams["sSortDir_0"] == "asc")
                                reportQuery = reportQuery.OrderBy(o => o.ShipperState);
                            else
                                reportQuery = reportQuery.OrderByDescending(o => o.ShipperState);
                            break;
                        case "RECEIVERNAME":
                            if (requestParams["sSortDir_0"] == "asc")
                                reportQuery = reportQuery.OrderBy(o => o.ReceiverName);
                            else
                                reportQuery = reportQuery.OrderByDescending(o => o.ReceiverName);
                            break;
                        case "RECEIVERADDRESS":
                            if (requestParams["sSortDir_0"] == "asc")
                                reportQuery = reportQuery.OrderBy(o => o.ReceiverAddress);
                            else
                                reportQuery = reportQuery.OrderByDescending(o => o.ReceiverAddress);
                            break;
                        case "RECEIVERCITY":
                            if (requestParams["sSortDir_0"] == "asc")
                                reportQuery = reportQuery.OrderBy(o => o.ReceiverCity);
                            else
                                reportQuery = reportQuery.OrderByDescending(o => o.ReceiverCity);
                            break;
                        case "RECEIVERSTATE":
                            if (requestParams["sSortDir_0"] == "asc")
                                reportQuery = reportQuery.OrderBy(o => o.ReceiverState);
                            else
                                reportQuery = reportQuery.OrderByDescending(o => o.ReceiverState);
                            break;
                        case "SERVICEDESC":
                            if (requestParams["sSortDir_0"] == "asc")
                                reportQuery = reportQuery.OrderBy(o => o.ServiceDesc);
                            else
                                reportQuery = reportQuery.OrderByDescending(o => o.ServiceDesc);
                            break;
                        case "TERMINAL":
                            if (requestParams["sSortDir_0"] == "asc")
                                reportQuery = reportQuery.OrderBy(o => o.Terminal);
                            else
                                reportQuery = reportQuery.OrderByDescending(o => o.Terminal);
                            break;
                        case "CREATEDATE":
                            if (requestParams["sSortDir_0"] == "asc")
                                reportQuery = reportQuery.OrderBy(o => o.CreateDate);
                            else
                                reportQuery = reportQuery.OrderByDescending(o => o.CreateDate);
                            break;
                        case "ISQUOTE":
                            if (requestParams["sSortDir_0"] == "asc")
                                reportQuery = reportQuery.OrderBy(o => o.IsQuote);
                            else
                                reportQuery = reportQuery.OrderByDescending(o => o.IsQuote);
                            break;
                        case "PUAREA":
                            if (requestParams["sSortDir_0"] == "asc")
                                reportQuery = reportQuery.OrderBy(o => o.PUArea);
                            else
                                reportQuery = reportQuery.OrderByDescending(o => o.PUArea);
                            break;
                        case "DELAREA":
                            if (requestParams["sSortDir_0"] == "asc")
                                reportQuery = reportQuery.OrderBy(o => o.DELArea);
                            else
                                reportQuery = reportQuery.OrderByDescending(o => o.DELArea);
                            break;
                        case "FUELSURCHARGE":
                            if (requestParams["sSortDir_0"] == "asc")
                                reportQuery = reportQuery.OrderBy(o => o.NoFuelSurcharge);
                            else
                                reportQuery = reportQuery.OrderByDescending(o => o.NoFuelSurcharge);
                            break;
                    }
                }
            }

            return reportQuery.Skip(skipCount).Take(takeCount).ToList();
        }

        public List<ShipmentReportView> GetUserExcelReportData(int userId, string fromDate, string toDate, string isQuote, string service, string terminal)
        {
            var reportQuery = (from shipment in entities.ShipmentReportViews
                               select shipment);

            if (!string.IsNullOrEmpty(fromDate))
            {
                DateTime fromShipDate = DateTime.Parse(fromDate);
                reportQuery = reportQuery.Where(o => o.CreateDate >= fromShipDate);
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                DateTime toShipDate = DateTime.Parse(toDate);
                reportQuery = reportQuery.Where(o => o.CreateDate <= toShipDate);
            }
            if (!string.IsNullOrEmpty(isQuote))
                reportQuery = reportQuery.Where(o => o.IsQuote == (isQuote == "true" ? true : false));
            if (!string.IsNullOrEmpty(service) && service != "NONE")
                reportQuery = reportQuery.Where(o => o.ServiceDesc == service);
            if (!string.IsNullOrEmpty(terminal))
                reportQuery = reportQuery.Where(o => o.Terminal.Contains(terminal.ToUpper()));

            return reportQuery.ToList();
        }

        public int GetUserReportDataCount(int userId, System.Collections.Specialized.NameValueCollection requestParams)
        {
            string searchFromShipDate = requestParams["search_ship_date_from"];
            string searchToShipDate = requestParams["search_ship_date_to"];
            string searchQuote = requestParams["search_quote"];
            string searchService = requestParams["search_service"];
            string searchTerminal = requestParams["search_terminal"];

            var reportQuery = (from shipment in entities.ShipmentReportViews
                               select shipment);

            if (!string.IsNullOrEmpty(searchFromShipDate))
            {
                DateTime fromShipDate = DateTime.Parse(searchFromShipDate);
                reportQuery = reportQuery.Where(o => o.CreateDate >= fromShipDate);
            }
            if (!string.IsNullOrEmpty(searchToShipDate))
            {
                DateTime toShipDate = DateTime.Parse(searchToShipDate);
                reportQuery = reportQuery.Where(o => o.CreateDate <= toShipDate);
            }
            if (!string.IsNullOrEmpty(searchQuote))
                reportQuery = reportQuery.Where(o => o.IsQuote == (searchQuote == "true" ? true : false));
            if (!string.IsNullOrEmpty(searchService) && searchService != "NONE")
                reportQuery = reportQuery.Where(o => o.ServiceDesc == searchService);
            if (!string.IsNullOrEmpty(searchTerminal))
                reportQuery = reportQuery.Where(o => o.Terminal.Contains(searchTerminal.ToUpper()));

            return reportQuery.Count();
        }
       
        public List<UserReportColumn> GetUserReportColumns(int userId)
        {
            return (from column in entities.UserReportColumns
                    where column.UserID == userId
                    orderby column.SortNum
                    select column).ToList();
        }

        public List<ReportColumn> GetUserMasterReportColumns(int userId)
        {
            return (from column in entities.UserReportColumns
                    join masterColumn in entities.ReportColumns on column.ColumnCode equals masterColumn.ColumnCode
                    where column.UserID == userId
                    orderby column.SortNum
                    select masterColumn).ToList();
        }
    }
}

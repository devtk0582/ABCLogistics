using ABCLogisticsBusinessLogic;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;

namespace TMS
{
    public partial class Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((AjaxControlToolkit.AlwaysVisibleControlExtender)Page.Master.FindControl("HeaderAlwaysVisibleControlExtender")).Enabled = false;
            }
        }

        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public static ReportColumnMain GetUserReportColumnsMain()
        {
            if (HttpContext.Current.Session["GlobalUserID"] != null)
            {
                int userId = Convert.ToInt32(HttpContext.Current.Session["GlobalUserID"]);

                return (new GlobalReportBLL()).GetUserReportColumnsMain(userId);
            }
            else
            {
                return null;
            }
        }

        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public static List<ReportColumn> GetUserReportColumns()
        {
            if (HttpContext.Current.Session["GlobalUserID"] != null)
            {
                int userId = Convert.ToInt32(HttpContext.Current.Session["GlobalUserID"]);

                return (new GlobalReportBLL()).GetUserMasterReportColumns(userId);
            }
            else
            {
                return null;
            }
        }

        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public static string SaveUserReportColumns(string[] origColumns, string[] destColumns)
        {
            if (HttpContext.Current.Session["GlobalUserID"] != null)
            {
                (new GlobalReportBLL()).SaveUserReportColumns(origColumns, destColumns, Convert.ToInt32(HttpContext.Current.Session["GlobalUserID"]));

                return "SUCCESS";
            }
            else
                return "";
        }
    }
}
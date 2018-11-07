using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using CMS.DAL;

namespace CMS
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup

        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            //if ((System.Web.HttpContext.Current != null) && (System.Web.HttpContext.Current.Session != null))
            //{
            //    Error_Log objErrorlog = new Error_Log();

            //    System.Web.HttpContext.Current.Session["ErrorLogID"] = objErrorlog.LogErrorIntoDB(Server.GetLastError());
            //}

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
            if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.User.Identity.Name))
            { FormsAuthentication.SignOut(); }
        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.
            if (System.Web.HttpContext.Current != null)
            { FormsAuthentication.SignOut(); }
        }

    }
}

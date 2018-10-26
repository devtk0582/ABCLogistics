using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TMSBusinessLogic;

namespace TMS
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindContentManager();
                BindDashboard();
                BindSideMenus();
            }
        }

        private void BindContentManager()
        {
            cmLeft.BindContent("Content Manager 1", @"this is a <a href='http://www.abclogistics.com' target='_blank'>test</a>");
            cmRight.BindContent("Content Manager 2", @"<p>Test Content Manager</p>");
        }

        private void BindDashboard()
        {
            rptCustomerService.DataSource = (new CustomerServiceBLL()).GetCustomerServiceDashboard();
            rptCustomerService.DataBind();

            rptOperationalQuotes.DataSource = (new OperationalQuotesBLL()).GetOperationalQuotesDashboard();
            rptOperationalQuotes.DataBind();

            rptInboundShipments.DataSource = (new InboundShipmentsBLL()).GetInboundShipmentsDashboard();
            rptInboundShipments.DataBind();
        }

        private void BindSideMenus()
        {
            rptSideMenus.DataSource = (new MenusBLL()).GetSideMenus();
            rptSideMenus.DataBind();
        }
    }
}
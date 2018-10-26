using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMSBusinessLogic
{
    public class CustomerServiceBLL
    {
        public List<DashboardView> GetCustomerServiceDashboard()
        {
            List<DashboardView> views = new List<DashboardView>();
            views.Add(new DashboardView()
            {
                Item = "Attention Required",
                Value = "125"
            });
            views.Add(new DashboardView()
            {
                Item = "Status Today",
                Value = "25"
            });
            views.Add(new DashboardView()
            {
                Item = "In Progress",
                Value = "125"
            });
            views.Add(new DashboardView()
            {
                Item = "Service Delay",
                Value = "0"
            });
            views.Add(new DashboardView()
            {
                Item = "All Shipments",
                Value = "425"
            });

            return views;

        }
    }
}

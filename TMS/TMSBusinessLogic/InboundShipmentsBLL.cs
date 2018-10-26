using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMSBusinessLogic
{
    public class InboundShipmentsBLL
    {
        public List<DashboardView> GetInboundShipmentsDashboard()
        {
            List<DashboardView> views = new List<DashboardView>();
            views.Add(new DashboardView()
            {
                Item = "Must Be Delivered Today",
                Value = "25"
            });
            views.Add(new DashboardView()
            {
                Item = "Must Be Delivered Tomorrow",
                Value = "25"
            });
            views.Add(new DashboardView()
            {
                Item = "Must Be Delivered This Week",
                Value = "150"
            });
            views.Add(new DashboardView()
            {
                Item = "All Inbound Shipments",
                Value = "200"
            });

            return views;

        }
    }
}

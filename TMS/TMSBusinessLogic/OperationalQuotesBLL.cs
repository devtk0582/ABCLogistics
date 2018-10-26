using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMSBusinessLogic
{
    public class OperationalQuotesBLL
    {
        public List<DashboardView> GetOperationalQuotesDashboard()
        {
            List<DashboardView> views = new List<DashboardView>();
            views.Add(new DashboardView()
            {
                Item = "Expires Today",
                Value = "25"
            });
            views.Add(new DashboardView()
            {
                Item = "Expires Within Next 2 Days",
                Value = "50"
            });
            views.Add(new DashboardView()
            {
                Item = "Expires Within Next 7 Days",
                Value = "75"
            });
            views.Add(new DashboardView()
            {
                Item = "All Open Quotes",
                Value = "100"
            });

            return views;

        }
    }
}

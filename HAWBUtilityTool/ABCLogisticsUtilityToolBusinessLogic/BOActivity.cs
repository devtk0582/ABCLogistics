using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Reflection;

namespace ABCLogisticsUtilityToolBusinessLogic
{
    public class BOActivity
    {
        private ILog logger;

        public BOActivity()
        {
            logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }

        public List<string> RemoveCustomerReportShipments(string customerNumber, string strHawbNumbers)
        {
            List<string> successHAWBs = new List<string>();
            try
            {
                string[] hawbList = strHawbNumbers.Split(';');
                using (ABCLogisticsShippingDBEntities context = new ABCLogisticsShippingDBEntities())
                {
                    foreach (string hawb in hawbList)
                    {
                        if (hawb.Trim() != string.Empty)
                        {
                            var activity = (from a in context.activities
                                            where a.hbillno == hawb.Trim() && a.accountno == customerNumber
                                            select a).SingleOrDefault();

                            if (activity != null)
                            {
                                context.activities.DeleteObject(activity);
                                context.SaveChanges();
                                successHAWBs.Add(hawb.Trim());
                                logger.Info(hawb.Trim() + " has been removed");
                            }
                            else
                                logger.Info(hawb.Trim() + " has not been found");
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                if (logger != null) logger.Error(ex.ToString());
            }
            return successHAWBs;
        }
    }
}

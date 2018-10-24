using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Reflection;
using ABCLogisticsUtilityToolBusinessLogic;

namespace ABCLogisticsUtilityToolBusinessLogic
{
    public class BOABCLogisticsGlobalShipment
    {
        private ILog logger;

        public BOABCLogisticsGlobalShipment()
        {
            logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }

        public Global_SearchFFMShipment_Result SearchFFMShipment(string hawb, int type)
        {
            Global_SearchFFMShipment_Result result = null;
            using (ABCLogisticsGlobalEntities context = new ABCLogisticsGlobalEntities())
            {
                result = context.Global_SearchFFMShipment(hawb, type).SingleOrDefault();
            }
            if (result != null && result.ShipmentID > 0)
                logger.Info("Shipment found for HAWB " + hawb + " and type " + type);
            return result;
        }

        public int RemoveFFMShipment(int shipmentId, int type)
        {
            int result = -1;

            ABCLogisticsGlobalEntities context = new ABCLogisticsGlobalEntities();
            try
            {
                var returnValue = context.Global_RemoveFFMShipment(shipmentId, type).SingleOrDefault();
                result = returnValue.HasValue ? returnValue.Value : 0;
                if (result > 0)
                    logger.Info("Shipment removed for shipment id " + shipmentId + " and type " + type);
                else
                    logger.Info("Shipment not removed. Error result: " + result);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ex.InnerException != null ? " " + ex.InnerException.Message : "");
            }
            finally
            {
                context.Dispose();
                context = null;
            }

            return result;
        }
    }
}

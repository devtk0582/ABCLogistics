using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ABCLogisticsBusinessLogic
{
    public class BaseBLL
    {
        protected ABCLogisticsEntities entities;

        public BaseBLL()
        {
            entities = new ABCLogisticsEntities();
        }

        public BaseBLL(string connectionString)
        {
            entities = new ABCLogisticsEntities(connectionString);
        }
    }
}

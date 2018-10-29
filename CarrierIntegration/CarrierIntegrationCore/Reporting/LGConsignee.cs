using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrierIntegrationCore.Reporting
{
    public class LGConsignee
    {
        public int ConsigneeID { get; set; }

        public string LOAD_ID { get; set; }

        public string ConsigneeCity { get; set; }

        public string ConsigneeState { get; set; }

        public string ConsigneeZip { get; set; }
    }
}

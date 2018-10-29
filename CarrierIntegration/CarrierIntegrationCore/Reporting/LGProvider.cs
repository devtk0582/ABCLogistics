using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrierIntegrationCore.Reporting
{
    public class LGProvider
    {
        public string Name { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public double SignatureRate { get; set; }

        public double SignaturePlusRate { get; set; }
    }
}

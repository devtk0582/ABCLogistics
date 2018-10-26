using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMSBusinessLogic
{
    public class TMSBaseBLL
    {
        protected TMSEntities entities;

        public TMSBaseBLL()
        {
            entities = new TMSEntities();
        }

        public TMSBaseBLL(string connectionString)
        {
            entities = new TMSEntities(connectionString);
        }
    }
}

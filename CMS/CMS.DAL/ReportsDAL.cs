﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ABCLogistics.DataAccess;

namespace CMS.DAL
{
    public class ReportsDAL
    {
        string CMSconnString = Convert.ToString(ConfigurationManager.AppSettings["CMSConnectionString"]);



    }
}

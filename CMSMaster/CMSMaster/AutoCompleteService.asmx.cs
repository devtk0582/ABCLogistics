using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using ABCLogistics.DataAccess;
using System.Collections;

namespace CMSMaster
{
    /// <summary>
    /// Summary description for AutoCompleteService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AutoCompleteService : System.Web.Services.WebService
    {

        [WebMethod]
        public string[] GetCustomers(string prefixText, int count, string contextKey)
        {
            string strConn = System.Configuration.ConfigurationSettings.AppSettings["CMSConnectionString"].ToString();

            try
            {
                if (count == 0)
                    count = 10;
                ArrayList items = new ArrayList(count);
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@in_GteDatabases_Id", SqlDbType.Int);
                parameters[1] = new SqlParameter("@in_CustName", SqlDbType.NVarChar);
                parameters[2] = new SqlParameter("@in_UserID", SqlDbType.NVarChar);
                parameters[3] = new SqlParameter("@in_Active", SqlDbType.Int);

                parameters[0].Value = 0;
                parameters[1].Value = prefixText;
                parameters[2].Value = contextKey;
                parameters[3].Value = 1;

                DataSet customers = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "Gte_GetGTEDatabasesSearch", parameters);
                DataView dvCustomers = customers.Tables[0].DefaultView;
                dvCustomers.RowFilter = "CMSCustomer=True";
                if (customers != null && dvCustomers.Table.Rows.Count > 0)
                {
                    foreach (DataRowView dr in dvCustomers)
                    {
                        if (dr["CustName"].ToString() != "")
                            items.Add(dr["CustName"].ToString());
                    }
                }
                return (string[])items.ToArray(typeof(string));

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

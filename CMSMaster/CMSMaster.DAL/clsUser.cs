using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ABCLogistics.DataAccess;

namespace CMSMaster.DAL
{
    public class clsUser
    {
        private string strConnection = System.Configuration.ConfigurationSettings.AppSettings["CMSConnectionString"].ToString();
        public DataSet AuthenticateUser(string username, string password)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            try
            {
                parameters[0] = new SqlParameter("@in_User_Email", SqlDbType.NVarChar);
                parameters[1] = new SqlParameter("@in_User_Pwd", SqlDbType.NVarChar);

                parameters[0].Value = username;
                parameters[1].Value = password;

                return SqlHelper.ExecuteDataset(strConnection, CommandType.StoredProcedure, "Usr_AuthenticateLogin", parameters);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}

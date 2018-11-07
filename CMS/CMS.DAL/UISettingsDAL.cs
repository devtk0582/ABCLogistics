using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using ABCLogistics.DataAccess;
using System.Data.SqlClient;

namespace CMS.DAL
{
    public class UISettingsDAL
    {
        string strConnection = ConfigurationManager.AppSettings["CMSConnectionString"].ToString();

        public DataSet GetCustomerMessages()
        {
            try
            {
                return SqlHelper.ExecuteDataset(strConnection, CommandType.StoredProcedure, "SP_CMS_GetCustomerMessages", null);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetCustomerMessages");
                throw new ApplicationException(strErrCode);
            }
        }

        public DataSet GetCustomerUIsettings()
        {
            try
            {
                return SqlHelper.ExecuteDataset(strConnection, CommandType.StoredProcedure, "SP_CMS_GetCustomerUISettings", null);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetCustomerUIsettings");
                throw new ApplicationException(strErrCode);
            }
        }

        public DataSet GetCustomerLogos()
        {
            try
            {
                return SqlHelper.ExecuteDataset(strConnection, CommandType.StoredProcedure, "SP_CMS_GetCustomerLogos", null);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetCustomerLogos");
                throw new ApplicationException(strErrCode);
            }
        }

        public byte[] GetCustomerLogo(int id)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            try
            {
                parameters[0] = new SqlParameter("@logoId", SqlDbType.Int);
                if (id != 0)
                    parameters[0].Value = id;
                else
                    parameters[0].Value = DBNull.Value;

                return (byte[])SqlHelper.ExecuteScalar(strConnection, CommandType.StoredProcedure, "SP_CMS_GetCustomerLogo", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetCustomerLogo", id.ToString());
                throw new ApplicationException(strErrCode);
            }
        }
    }
}

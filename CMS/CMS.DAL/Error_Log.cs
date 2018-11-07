using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ABCLogistics.DataAccess;
using System.Configuration;

namespace CMS.DAL
{
    public class Error_Log
    {
        #region SQL
        private const string SP_ERROR_LOG = "SP_CMS_Error_Log";

        private const string PARAM_ERROR_MESSAGE = "@in_message";
        private const string PARAM_ERROR_SOURCE = "@in_source";
        private const string PARAM_ERROR_STACK_TRACE = "@in_stack_trace";
        private const string PARAM_ERROR_MEMBER_NAME = "@in_member_name";
        private const string PARAM_ERROR_PARAM_VALUES = "@in_param_values";

        string c_strDBConnectionString = ConfigurationManager.AppSettings["CMSConnectionString"].ToString();
        #endregion

        #region Manage Data
        public string LogErrorIntoDB(Exception ex, params  string[] ParamValues)
        {
            if ((ex.Source != "mscorlib") && (ex.Message != "Thread was being aborted."))
            {
                Exception exBase = ex.GetBaseException();
                string strParamVals = string.Empty;
                for (int iCntr = 0; iCntr < ParamValues.Length; iCntr++)
                { strParamVals += ParamValues[iCntr] + Environment.NewLine; }

                SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter(PARAM_ERROR_MESSAGE, ((exBase.Message==null)||(exBase.Message==string.Empty))?null:exBase.Message),
                new SqlParameter(PARAM_ERROR_SOURCE, ((exBase.Source==null)||(exBase.Source==string.Empty))?null:exBase.Source),
                new SqlParameter(PARAM_ERROR_STACK_TRACE, ((exBase.StackTrace==null)||(exBase.StackTrace==string.Empty))?null:exBase.StackTrace),
                new SqlParameter(PARAM_ERROR_MEMBER_NAME, ((exBase.TargetSite.Name==null)||(exBase.TargetSite.Name==string.Empty))?null:exBase.TargetSite.Name),
                new SqlParameter(PARAM_ERROR_PARAM_VALUES, strParamVals),
                new SqlParameter("@Out_msg",System.Data.SqlDbType.BigInt)};

                param[0].SqlDbType = System.Data.SqlDbType.VarChar;
                param[1].SqlDbType = System.Data.SqlDbType.VarChar;
                param[2].SqlDbType = System.Data.SqlDbType.VarChar;
                param[3].SqlDbType = System.Data.SqlDbType.VarChar;
                param[4].SqlDbType = System.Data.SqlDbType.VarChar;
                param[5].Direction = System.Data.ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(c_strDBConnectionString, System.Data.CommandType.StoredProcedure, SP_ERROR_LOG, param);
                return param[5].Value.ToString();
            }
            else
            {
                return null;
            }
        }
        #endregion

    }
}

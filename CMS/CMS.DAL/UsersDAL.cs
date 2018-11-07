using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ABCLogistics.DataAccess;

namespace CMS.DAL
{
    public class UsersDAL
    {
        string strMasterConn = ConfigurationManager.AppSettings["GTEMasterConnectionString"].ToString();
        //string strConnection = ConfigurationManager.AppSettings["CMSConnectionString"].ToString();
        string c_strDBConnectionString = ConfigurationManager.AppSettings["CMSConnectionString"].ToString();
        public DataSet AuthenticateUser(string username, string password)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            try
            {
                parameters[0] = new SqlParameter("@in_User_Email", SqlDbType.NVarChar);
                parameters[0].Value = username;
                parameters[1] = new SqlParameter("@in_User_Pwd", SqlDbType.NVarChar);
                parameters[1].Value = password;
                return SqlHelper.ExecuteDataset(strMasterConn, CommandType.StoredProcedure, "Usr_AuthenticateLogin", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "AuthenticateUser", username, password);
                throw new ApplicationException(strErrCode);
            }
        }

        public DataSet GetRoleMenus(int roleId)
        {
            SqlParameter[] parameters = new SqlParameter[3];
            try
            {
                parameters[0] = new SqlParameter("@in_Role_Id", SqlDbType.Int);
                parameters[0].Value = roleId;
                parameters[1] = new SqlParameter("@in_Menu_Id", SqlDbType.Int);
                parameters[1].Value = 0;
                parameters[2] = new SqlParameter("@in_MenuType", SqlDbType.NVarChar);
                parameters[2].Value = "C";

                return SqlHelper.ExecuteDataset(strMasterConn, CommandType.StoredProcedure, "Rms_GetRolesMenuItems", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetRoleMenus", roleId.ToString());
                throw new ApplicationException(strErrCode);
            }
        }

        public DataSet GetRoleSubMenus(int roleId, int menuId)
        {
            SqlParameter[] parameters = new SqlParameter[3];
            try
            {
                parameters[0] = new SqlParameter("@in_Role_Id", SqlDbType.Int);
                parameters[0].Value = roleId;
                parameters[1] = new SqlParameter("@in_Menu_Id", SqlDbType.Int);
                parameters[1].Value = menuId;
                parameters[2] = new SqlParameter("@in_MenuType", SqlDbType.NVarChar);
                parameters[2].Value = "C";
                return SqlHelper.ExecuteDataset(strMasterConn, CommandType.StoredProcedure, "Rms_GetRolesMenuItems", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetRoleSubMenus", roleId.ToString(), menuId.ToString());
                throw new ApplicationException(strErrCode);
            }
        }


        // User List
        //Added 

        public DataSet GetUsersList(int iUserId, string strRoles)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            try
            {
                parameters[0] = new SqlParameter("@in_user_id", SqlDbType.Int);
                parameters[0].Value = iUserId;
                parameters[1] = new SqlParameter("@in_RolesText", SqlDbType.VarChar);
                parameters[1].Value = strRoles;
                return SqlHelper.ExecuteDataset(c_strDBConnectionString, CommandType.StoredProcedure, "sp_WMS_GetCustomerUser", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetUsersList", iUserId.ToString(), strRoles);
                throw new ApplicationException(strErrCode);
            }
        }

        public string UserUpdation_DAL(int iUser_Id, string strUser_First, string strUser_Last, string strUser_Email, string strUser_Pwd, string strPhone1, string strPhone2, bool bActive, string strSecurityQuestion1, string strSecurityAnswer1,
                    string strSecurityQuestion2, string strSecurityAnswer2, string strFlag, int iRole_id, string UserWhseData)
        {
            SqlParameter[] parameters = new SqlParameter[16];

            try
            {
                parameters[0] = new SqlParameter("@in_User_Id", SqlDbType.Int);
                parameters[1] = new SqlParameter("@in_User_First", SqlDbType.NVarChar);
                parameters[2] = new SqlParameter("@in_User_Last", SqlDbType.NVarChar);
                parameters[3] = new SqlParameter("@in_User_Email", SqlDbType.NVarChar);
                parameters[4] = new SqlParameter("@in_User_Pwd", SqlDbType.NVarChar);
                parameters[5] = new SqlParameter("@in_Phone1", SqlDbType.NVarChar);
                parameters[6] = new SqlParameter("@in_Phone2", SqlDbType.NVarChar);
                parameters[7] = new SqlParameter("@in_Active", SqlDbType.Bit);
                parameters[8] = new SqlParameter("@in_SecurityQuestion1", SqlDbType.NVarChar);
                parameters[9] = new SqlParameter("@in_SecurityAnswer1", SqlDbType.NVarChar);
                parameters[10] = new SqlParameter("@in_SecurityQuestion2", SqlDbType.NVarChar);
                parameters[11] = new SqlParameter("@in_SecurityAnswer2", SqlDbType.NVarChar);
                parameters[12] = new SqlParameter("@in_Flag", SqlDbType.Char);
                parameters[13] = new SqlParameter("@in_Role_id", SqlDbType.Int);
                parameters[14] = new SqlParameter("@In_Data", SqlDbType.NText);


                parameters[15] = new SqlParameter("@out_msg", SqlDbType.NVarChar, 200);
                parameters[15].Direction = ParameterDirection.Output;
                parameters[0].Value = iUser_Id;
                parameters[1].Value = strUser_First;
                parameters[2].Value = strUser_Last;
                parameters[3].Value = strUser_Email;
                parameters[4].Value = strUser_Pwd;
                parameters[5].Value = strPhone1;
                parameters[6].Value = strPhone2;
                parameters[7].Value = bActive;
                parameters[8].Value = strSecurityQuestion1;
                parameters[9].Value = strSecurityAnswer1;
                parameters[10].Value = strSecurityQuestion2;
                parameters[11].Value = strSecurityAnswer2;
                parameters[12].Value = strFlag;
                parameters[13].Value = iRole_id;
                parameters[14].Value = UserWhseData;

                SqlHelper.ExecuteNonQuery(strMasterConn, CommandType.StoredProcedure, "Usr_UpdateUser", parameters);

                return parameters[15].Value.ToString();
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "UserUpdation_DAL", iUser_Id.ToString(), strUser_First, strUser_Last
                    , strUser_Email, strUser_Pwd, strPhone1, strPhone2, bActive.ToString(), strSecurityQuestion1.ToString(), strSecurityAnswer1,
                    strSecurityQuestion2, strSecurityAnswer2, strFlag, iRole_id.ToString(), UserWhseData);
                throw new ApplicationException(strErrCode);
            }
        }


        public DataSet GetSecurityQuestions_DAL(int iQuestion_Id)
        {
            SqlParameter[] parameters = new SqlParameter[1];

            try
            {
                parameters[0] = new SqlParameter("@iQuestion_Id", SqlDbType.Int);
                parameters[0].Value = iQuestion_Id;

                return SqlHelper.ExecuteDataset(strMasterConn, CommandType.StoredProcedure, "SecurityQuestions_List", parameters);

            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetSecurityQuestions_DAL", iQuestion_Id.ToString());
                throw new ApplicationException(strErrCode);
            }
        }


        public DataSet GetCMSCusDatabaseId_DAL(String strDbName)
        {
            SqlParameter[] parameters = new SqlParameter[1];

            try
            {
                parameters[0] = new SqlParameter("@DbName", SqlDbType.NVarChar,50);
                parameters[0].Value = strDbName;

                return SqlHelper.ExecuteDataset(strMasterConn, CommandType.StoredProcedure, "GTE_CMS_GetCMSCustID", parameters);

            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GTE_CMS_GetCMSCustID", strDbName.ToString());
                throw new ApplicationException(strErrCode);
            }
        }


        
    }
}

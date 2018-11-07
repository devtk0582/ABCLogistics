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
    public class SettingsDAL
    {
        string strConnection = ConfigurationManager.AppSettings["CMSConnectionString"].ToString();
        string strGTEMasterConnectionString = ConfigurationManager.AppSettings["GTEMasterConnectionString"].ToString();

        public DataSet GetShippingMethods(int Active)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            try
            {
                parameters[0] = new SqlParameter("@Active", SqlDbType.Int);
                parameters[0].Value = Active;
                return SqlHelper.ExecuteDataset(strConnection, CommandType.StoredProcedure, "SP_CMS_GetShippingMethods", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetShippingMethods");
                throw new ApplicationException(strErrCode);
            }
        }

        public DataSet GetShippingServices()
        {
            try
            {
                return SqlHelper.ExecuteDataset(strConnection, CommandType.StoredProcedure, "SP_CMS_GetShippingServices", null);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetShippingServices");
                throw new ApplicationException(strErrCode);
            }
        }

        public DataSet GetPackagings()
        {
            try
            {                          
                return SqlHelper.ExecuteDataset(strConnection, CommandType.StoredProcedure, "SP_CMS_GetPackagings", null);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetPackagings");
                throw new ApplicationException(strErrCode);
            }
        }

        public DataSet GetPackagings(int ipackingid)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            try
            {
                parameters[0] = new SqlParameter("@in_Is_Active", SqlDbType.Int);
                parameters[0].Value = ipackingid;
                return SqlHelper.ExecuteDataset(strConnection, CommandType.StoredProcedure, "SP_CMS_GetPackagings", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetPackagings");
                throw new ApplicationException(strErrCode);
            }
        }

        public DataSet GetBillingCodes(int Active)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            try
            {
                parameters[0] = new SqlParameter("@Active", SqlDbType.Int);
                parameters[0].Value = Active;

                return SqlHelper.ExecuteDataset(strConnection, CommandType.StoredProcedure, "SP_CMS_GetBillingCodes", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetBillingCodes");
                throw new ApplicationException(strErrCode);
            }
        }

        public DataSet GetManufacturers(int Active)
        {
            SqlParameter[] parameters = new SqlParameter[1];

            try
            {
                parameters[0] = new SqlParameter("@Active", SqlDbType.Int);
                parameters[0].Value = Active;
                return SqlHelper.ExecuteDataset(strConnection, CommandType.StoredProcedure, "SP_CMS_GetManufacturers", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetManufacturers");
                throw new ApplicationException(strErrCode);
            }
        }

        public DataSet GetShippingClasses(int Active)
        {
            SqlParameter[] parameters = new SqlParameter[1];

            try
            {
                parameters[0] = new SqlParameter("@Active", SqlDbType.Int);
                parameters[0].Value = Active;
                return SqlHelper.ExecuteDataset(strConnection, CommandType.StoredProcedure, "SP_CMS_GetShippingClass", null);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetShippingClasses");
                throw new ApplicationException(strErrCode);
            }
        }

        public DataSet GetShippingMethodByID(int methodId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            try
            {
                parameters[0] = new SqlParameter("@ship_method_id", SqlDbType.Int);

                if (methodId != 0)
                    parameters[0].Value = methodId;
                else
                    parameters[0].Value = DBNull.Value;

                return SqlHelper.ExecuteDataset(strConnection, CommandType.StoredProcedure, "SP_CMS_GetShippingMethodByID", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetShippingMethodByID");
                throw new ApplicationException(strErrCode);
            }
        }

        public DataSet GetShippingServiceByID(int serviceId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            try
            {
                parameters[0] = new SqlParameter("@service_id", SqlDbType.Int);

                if (serviceId != 0)
                    parameters[0].Value = serviceId;
                else
                    parameters[0].Value = DBNull.Value;

                return SqlHelper.ExecuteDataset(strConnection, CommandType.StoredProcedure, "SP_CMS_GetShippingServiceByID", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetShippingServiceByID");
                throw new ApplicationException(strErrCode);
            }
        }

        public DataSet GetPackagingByID(int packagingId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            try
            {
                parameters[0] = new SqlParameter("@packaging_id", SqlDbType.Int);

                if (packagingId != 0)
                    parameters[0].Value = packagingId;
                else
                    parameters[0].Value = DBNull.Value;

                return SqlHelper.ExecuteDataset(strConnection, CommandType.StoredProcedure, "SP_CMS_GetPackagingByID", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetPackagingByID");
                throw new ApplicationException(strErrCode);
            }
        }

        public DataSet GetBillingCodeByID(int billingCodeId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            try
            {
                parameters[0] = new SqlParameter("@billing_code_id", SqlDbType.Int);

                if (billingCodeId != 0)
                    parameters[0].Value = billingCodeId;
                else
                    parameters[0].Value = DBNull.Value;

                return SqlHelper.ExecuteDataset(strConnection, CommandType.StoredProcedure, "SP_CMS_GetBillingCodeByID", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetBillingCodeByID");
                throw new ApplicationException(strErrCode);
            }
        }

        public DataSet GetManufacturerByID(int manuId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            try
            {
                parameters[0] = new SqlParameter("@manu_id", SqlDbType.Int);

                if (manuId != 0)
                    parameters[0].Value = manuId;
                else
                    parameters[0].Value = DBNull.Value;

                return SqlHelper.ExecuteDataset(strConnection, CommandType.StoredProcedure, "SP_CMS_GetManufacturerByID", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetManufacturerByID");
                throw new ApplicationException(strErrCode);
            }
        }

        public DataSet GetShippingClassByID(int shipClassId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            try
            {
                parameters[0] = new SqlParameter("@shipclass_id", SqlDbType.Int);

                if (shipClassId != 0)
                    parameters[0].Value = shipClassId;
                else
                    parameters[0].Value = DBNull.Value;

                return SqlHelper.ExecuteDataset(strConnection, CommandType.StoredProcedure, "SP_CMS_GetShippingClassByID", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetShippingClassByID");
                throw new ApplicationException(strErrCode);
            }
        }

        public int InsertShippingMethod(string name, double cost, string license, string username, string password, string accountNumber)
        {
            SqlParameter[] parameters = new SqlParameter[6];
            try
            {
                parameters[0] = new SqlParameter("@ship_method_name", SqlDbType.VarChar);
                parameters[1] = new SqlParameter("@ship_method_cost ", SqlDbType.Float);
                parameters[2] = new SqlParameter("@license", SqlDbType.VarChar);
                parameters[3] = new SqlParameter("@username", SqlDbType.VarChar);
                parameters[4] = new SqlParameter("@password", SqlDbType.VarChar);
                parameters[5] = new SqlParameter("@account_number", SqlDbType.VarChar);

                parameters[0].Value = name;
                parameters[1].Value = cost;
                parameters[2].Value = license;
                parameters[3].Value = username;
                parameters[4].Value = password;
                parameters[5].Value = accountNumber;

                return SqlHelper.ExecuteNonQuery(strConnection, CommandType.StoredProcedure, "SP_CMS_InsertShippingMethod", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "InsertShippingMethod", name, cost.ToString());
                throw new ApplicationException(strErrCode);
            }
        }

        public int UpdateShippingMethod(int id, string name, double cost, string license, string username, string password, string accountNumber)
        {
            SqlParameter[] parameters = new SqlParameter[7];
            try
            {
                parameters[0] = new SqlParameter("@ship_method_id", SqlDbType.Int);
                parameters[1] = new SqlParameter("@ship_method_name", SqlDbType.VarChar);
                parameters[2] = new SqlParameter("@ship_method_cost ", SqlDbType.Float);
                parameters[3] = new SqlParameter("@license", SqlDbType.VarChar);
                parameters[4] = new SqlParameter("@username", SqlDbType.VarChar);
                parameters[5] = new SqlParameter("@password", SqlDbType.VarChar);
                parameters[6] = new SqlParameter("@account_number", SqlDbType.VarChar);

                if (id != 0)
                    parameters[0].Value = id;
                else
                    parameters[0].Value = DBNull.Value;

                parameters[1].Value = name;
                parameters[2].Value = cost;
                parameters[3].Value = license;
                parameters[4].Value = username;
                parameters[5].Value = password;
                parameters[6].Value = accountNumber;

                return SqlHelper.ExecuteNonQuery(strConnection, CommandType.StoredProcedure, "SP_CMS_UpdateShippingMethod", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "UpdateShippingMethod", id.ToString(), name, cost.ToString());
                throw new ApplicationException(strErrCode);
            }
        }

        public int ToggleShippingMethod(int id, bool isActive)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            try
            {
                parameters[0] = new SqlParameter("@ship_method_id", SqlDbType.Int);
                parameters[1] = new SqlParameter("@is_active", SqlDbType.Bit);
                
                if (id != 0)
                    parameters[0].Value = id;
                else
                    parameters[0].Value = DBNull.Value;
                parameters[1].Value = isActive;

                return SqlHelper.ExecuteNonQuery(strConnection, CommandType.StoredProcedure, "SP_CMS_ToggleShippingMethod", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "ToggleShippingMethod");
                throw new ApplicationException(strErrCode);
            }
        }

        public int InsertPackaging(string name)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            try
            {
                parameters[0] = new SqlParameter("@packaging_name", SqlDbType.VarChar);

                parameters[0].Value = name;

                return SqlHelper.ExecuteNonQuery(strConnection, CommandType.StoredProcedure, "SP_CMS_InsertPackaging", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "InsertPackaging");
                throw new ApplicationException(strErrCode);
            }
        }

        public int UpdatePackaging(int id, string name)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            try
            {
                parameters[0] = new SqlParameter("@packaging_id", SqlDbType.Int);
                parameters[1] = new SqlParameter("@packaging_name", SqlDbType.VarChar);
                
                if (id != 0)
                    parameters[0].Value = id;
                else
                    parameters[0].Value = DBNull.Value;
                parameters[1].Value = name;

                return SqlHelper.ExecuteNonQuery(strConnection, CommandType.StoredProcedure, "SP_CMS_UpdatePackaging", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "UpdatePackaging");
                throw new ApplicationException(strErrCode);
            }
        }

        public int TogglePackaging(int id, bool isActive)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            try
            {
                parameters[0] = new SqlParameter("@packaging_id", SqlDbType.Int);
                parameters[1] = new SqlParameter("@is_active", SqlDbType.Bit);

                if (id != 0)
                    parameters[0].Value = id;
                else
                    parameters[0].Value = DBNull.Value;
                parameters[1].Value = isActive;

                return SqlHelper.ExecuteNonQuery(strConnection, CommandType.StoredProcedure, "SP_CMS_TogglePackaging", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "TogglePackaging");
                throw new ApplicationException(strErrCode);
            }
        }

        public int InsertBillingCode(string name, string desc)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            try
            {
                parameters[0] = new SqlParameter("@billing_code_name", SqlDbType.VarChar);
                parameters[1] = new SqlParameter("@billing_code_desc", SqlDbType.VarChar);

                parameters[0].Value = name;
                parameters[1].Value = desc;

                return SqlHelper.ExecuteNonQuery(strConnection, CommandType.StoredProcedure, "SP_CMS_InsertBillingCode", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "InsertBillingCode");
                throw new ApplicationException(strErrCode);
            }
        }

        public int UpdateBillingCode(int id, string name, string desc)
        {
            SqlParameter[] parameters = new SqlParameter[3];
            try
            {
                parameters[0] = new SqlParameter("@billing_code_id", SqlDbType.Int);
                parameters[1] = new SqlParameter("@billing_code_name", SqlDbType.VarChar);
                parameters[2] = new SqlParameter("@billing_code_desc ", SqlDbType.VarChar);
                if (id != 0)
                    parameters[0].Value = id;
                else
                    parameters[0].Value = DBNull.Value;
                parameters[1].Value = name;
                parameters[2].Value = desc;

                return SqlHelper.ExecuteNonQuery(strConnection, CommandType.StoredProcedure, "SP_CMS_UpdateBillingCode", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "UpdateBillingCode");
                throw new ApplicationException(strErrCode);
            }
        }

        public int ToggleBillingCode(int id, bool isActive)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            try
            {
                parameters[0] = new SqlParameter("@billing_code_id", SqlDbType.Int);
                parameters[1] = new SqlParameter("@is_active", SqlDbType.Bit);

                if (id != 0)
                    parameters[0].Value = id;
                else
                    parameters[0].Value = DBNull.Value;
                parameters[1].Value = isActive;

                return SqlHelper.ExecuteNonQuery(strConnection, CommandType.StoredProcedure, "SP_CMS_ToggleBillingCode", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "ToggleBillingCode");
                throw new ApplicationException(strErrCode);
            }
        }

        public int InsertManufacturer(string name)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            try
            {
                parameters[0] = new SqlParameter("@name", SqlDbType.VarChar);

                parameters[0].Value = name;

                return SqlHelper.ExecuteNonQuery(strConnection, CommandType.StoredProcedure, "SP_CMS_InsertManufacturer", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "InsertManufacturer");
                throw new ApplicationException(strErrCode);
            }
        }

        public int UpdateManufacturer(int id, string name)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            try
            {
                parameters[0] = new SqlParameter("@manu_id", SqlDbType.Int);
                parameters[1] = new SqlParameter("@manu_name", SqlDbType.VarChar);

                if (id != 0)
                    parameters[0].Value = id;
                else
                    parameters[0].Value = DBNull.Value;
                parameters[1].Value = name;

                return SqlHelper.ExecuteNonQuery(strConnection, CommandType.StoredProcedure, "SP_CMS_UpdateManufacturer", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "UpdateManufacturer");
                throw new ApplicationException(strErrCode);
            }
        }

        public int ToggleManufacturer(int id, bool isActive)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            try
            {
                parameters[0] = new SqlParameter("@manu_id", SqlDbType.Int);
                parameters[1] = new SqlParameter("@is_active", SqlDbType.Bit);

                if (id != 0)
                    parameters[0].Value = id;
                else
                    parameters[0].Value = DBNull.Value;
                parameters[1].Value = isActive;

                return SqlHelper.ExecuteNonQuery(strConnection, CommandType.StoredProcedure, "SP_CMS_ToggleManufacturer", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "ToggleManufacturer");
                throw new ApplicationException(strErrCode);
            }
        }

        public int InsertShippingClass(string name)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            try
            {
                parameters[0] = new SqlParameter("@name", SqlDbType.VarChar);

                parameters[0].Value = name;

                return SqlHelper.ExecuteNonQuery(strConnection, CommandType.StoredProcedure, "SP_CMS_InsertShippingClass", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "InsertShippingClass");
                throw new ApplicationException(strErrCode);
            }
        }

        public int UpdateShippingClass(int id, string name)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            try
            {
                parameters[0] = new SqlParameter("@shipclass_id", SqlDbType.Int);
                parameters[1] = new SqlParameter("@shipclass_name", SqlDbType.VarChar);

                if (id != 0)
                    parameters[0].Value = id;
                else
                    parameters[0].Value = DBNull.Value;
                parameters[1].Value = name;

                return SqlHelper.ExecuteNonQuery(strConnection, CommandType.StoredProcedure, "SP_CMS_UpdateShippingClass", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "UpdateShippingClass");
                throw new ApplicationException(strErrCode);
            }
        }

        public int ToggleShippingClass(int id, bool isActive)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            try
            {
                parameters[0] = new SqlParameter("@shipclass_id", SqlDbType.Int);
                parameters[1] = new SqlParameter("@is_active", SqlDbType.Bit);

                if (id != 0)
                    parameters[0].Value = id;
                else
                    parameters[0].Value = DBNull.Value;
                parameters[1].Value = isActive;

                return SqlHelper.ExecuteNonQuery(strConnection, CommandType.StoredProcedure, "SP_CMS_ToggleShippingClass", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "ToggleShippingClass");
                throw new ApplicationException(strErrCode);
            }
        }



        public DataSet GetAddressBook_DAL(int iaddress_id, string strcompany_name, int iSartRow, int iEndrow)
        {
            SqlParameter[] parameters = new SqlParameter[4];
            try
            {
                parameters[0] = new SqlParameter("@in_address_id", SqlDbType.Int);
                if (iaddress_id != 0)
                    parameters[0].Value = iaddress_id;
                else
                    parameters[0].Value = DBNull.Value;
                parameters[1] = new SqlParameter("@in_company_name", SqlDbType.VarChar);
                parameters[1].Value = strcompany_name;
                parameters[2] = new SqlParameter("@StartRow", SqlDbType.Int);
                parameters[2].Value = iSartRow;
                parameters[3] = new SqlParameter("@EndRow", SqlDbType.Int);
                parameters[3].Value = iEndrow;
                return SqlHelper.ExecuteDataset(strConnection, CommandType.StoredProcedure, "sp_WMS_Add_Getaddress_table", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetAddressBook_DAL");
                throw new ApplicationException(strErrCode);
            }
        }


        public DataSet GetCountry_DAL(int iCountryId)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            try
            {
                parameters[0] = new SqlParameter("@in_CountryId", SqlDbType.Int);
                parameters[0].Value = iCountryId;
                return SqlHelper.ExecuteDataset(strGTEMasterConnectionString, CommandType.StoredProcedure, "Cou_GetCountry", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetCountry_DAL");
                throw new ApplicationException(strErrCode);
            }
        }

        public DataSet GetState_DAL(int iCountryId)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            try
            {
                parameters[0] = new SqlParameter("@in_CountryId", SqlDbType.Int);
                parameters[0].Value = iCountryId;
                return SqlHelper.ExecuteDataset(strGTEMasterConnectionString, CommandType.StoredProcedure, "Cou_GetState", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetState_DAL");
                throw new ApplicationException(strErrCode);
            }
        }

        public DataSet GetCity_DAL(int iStateId)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            try
            {
                parameters[0] = new SqlParameter("@in_StateId", SqlDbType.Int);
                parameters[0].Value = iStateId;
                return SqlHelper.ExecuteDataset(strGTEMasterConnectionString, CommandType.StoredProcedure, "Cou_GetCity", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetCity_DAL");
                throw new ApplicationException(strErrCode);
            }
        }


        public string UpdateCMSAddressBook_DAL(string str_AddressData, string in_Flag)
        {
            SqlParameter[] parameters = new SqlParameter[3];
            string strRet = "0";
            try
            {
                parameters[0] = new SqlParameter("@In_Data", SqlDbType.NVarChar);
                parameters[0].Value = str_AddressData;
                parameters[1] = new SqlParameter("@in_Flag", SqlDbType.NVarChar);
                parameters[1].Value = in_Flag;
                parameters[2] = new SqlParameter("@out_msg", SqlDbType.NVarChar, 200);
                parameters[2].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(strConnection, CommandType.StoredProcedure, "SP_CMS_Add_UpdateAddress", parameters);
                strRet =   parameters[2].Value.ToString();;
            }
            catch (Exception ex)
            {
                strRet = "0";
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "UpdateCMSAddressBook_DAL");
                throw new ApplicationException(strErrCode);
            }
            return strRet;
        }
        public string UpdateAddressBook_DAL(string str_AddressData, string in_Flag)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            string strRet = "0";
            try
            {
                parameters[0] = new SqlParameter("@In_Data", SqlDbType.NVarChar);
                parameters[0].Value = str_AddressData;
                parameters[1] = new SqlParameter("@in_Flag", SqlDbType.NVarChar);
                parameters[1].Value = in_Flag;

               

                SqlHelper.ExecuteNonQuery(strConnection, CommandType.StoredProcedure, "SP_WMS_Add_UpdateAddress", parameters);
                strRet = "1";
            }
            catch (Exception ex)
            {
                strRet = "0";
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "UpdateAddressBook_DAL");
                throw new ApplicationException(strErrCode);
            }
            return strRet;
        }

        public string UpdateUserAddressBook_DAL(int iprimaryaddress_id, int isecondaryaddress_id,int iUserid,string strInserUpdateFlag,string strPriSecFlag)
        {
            SqlParameter[] parameters = new SqlParameter[5];
            string strRet = "0";
            try
            {
                parameters[0] = new SqlParameter("@In_primaryaddress_id", SqlDbType.Int);
                parameters[0].Value = iprimaryaddress_id;
                parameters[1] = new SqlParameter("@in_secondaryaddress_id", SqlDbType.Int);
                parameters[1].Value = isecondaryaddress_id;
                parameters[2] = new SqlParameter("@in_userid", SqlDbType.Int);
                parameters[2].Value = iUserid ;
                parameters[3] = new SqlParameter("@in_Flag", SqlDbType.VarChar);
                parameters[3].Value = strInserUpdateFlag;
                parameters[4] = new SqlParameter("@in_primaryorSecomdary", SqlDbType.VarChar);
                parameters[4].Value = strPriSecFlag ;
                SqlHelper.ExecuteNonQuery(strConnection, CommandType.StoredProcedure, "SP_CMS_UserAddress", parameters);
                strRet = "1";
            }
            catch (Exception ex)
            {
                strRet = "0";
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "UpdateUserAddressBook_DAL");
                throw new ApplicationException(strErrCode);
            }
            return strRet;
        }


        public DataSet GetUserAddress(int iUserid)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            try
            {
                parameters[0] = new SqlParameter("@in_userid", SqlDbType.Int);
                parameters[0].Value = iUserid;
                return SqlHelper.ExecuteDataset(strConnection, CommandType.StoredProcedure, "SP_CMS_GetUserAddress", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetUserAddress", iUserid.ToString());
                throw new ApplicationException(strErrCode);
            }
        }


        public string DeleteAddressBook_DAL(int iaddress_id)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            string strRet = "";
            try
            {
                parameters[0] = new SqlParameter("@in_address_id", SqlDbType.Int);
                parameters[0].Value = iaddress_id;
                parameters[1] = new SqlParameter("@out_msg", SqlDbType.VarChar ,200);
                parameters[1].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(strConnection, CommandType.StoredProcedure, "SP_WMS_Add_DeleteAddress", parameters);
                strRet = parameters[1].Value.ToString() ;
            }
            catch (Exception ex)
            {
                strRet = "Errror";
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "DeleteAddressBook_DAL", iaddress_id.ToString());
               
            }
            return strRet;
        }



    }
}

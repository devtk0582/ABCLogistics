using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ABCLogistics.DataAccess;

namespace CMSMaster.DAL
{
    public class clsCustomer
    {
        private string strConnection = System.Configuration.ConfigurationSettings.AppSettings["CMSConnectionString"].ToString();
        private string strCustomerConnection = System.Configuration.ConfigurationSettings.AppSettings["CMSCustomerConnectionString"].ToString();

        public clsCustomer(string dbName)
        {
            strCustomerConnection = strCustomerConnection.Replace("?", dbName);
        }
        
        public DataSet GetCustomers(int databaseId, string customerName, string userId)
        {
            SqlParameter[] parameters = new SqlParameter[3];
            try
            {
                parameters[0] = new SqlParameter("@in_GteDatabases_Id", SqlDbType.Int);
                if (databaseId != 0)
                    parameters[0].Value = databaseId;
                else
                    parameters[0].Value = DBNull.Value;
                parameters[1] = new SqlParameter("@in_CustName", SqlDbType.NVarChar);
                parameters[1].Value = customerName;
                parameters[2] = new SqlParameter("@in_UserID", SqlDbType.Int);
                parameters[2].Value = int.Parse(userId);
                return SqlHelper.ExecuteDataset(strConnection, CommandType.StoredProcedure, "Gte_GetGTEDatabases", parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetCountries(int countryId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            try
            {
                parameters[0] = new SqlParameter("@in_CountryId", SqlDbType.Int);
                parameters[0].Value = countryId;
                return SqlHelper.ExecuteDataset(strConnection, CommandType.StoredProcedure, "Cou_GetCountry", parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetStates(int countryId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            try
            {
                parameters[0] = new SqlParameter("@in_CountryId", SqlDbType.Int);
                parameters[0].Value = countryId;
                return SqlHelper.ExecuteDataset(strConnection, CommandType.StoredProcedure, "Cou_GetState", parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetCities(int stateId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            try
            {
                parameters[0] = new SqlParameter("@in_StateId", SqlDbType.Int);
                parameters[0].Value = stateId;
                return SqlHelper.ExecuteDataset(strConnection, CommandType.StoredProcedure, "Cou_GetCity", parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertCustomerMessages(int custId, string custHeaderMsg, string custFooterMsg, string custSideMsg, string custWelcomeMsg)
        {
            SqlParameter[] parameters = new SqlParameter[5];
            try
            {
                parameters[0] = new SqlParameter("@cust_Id", SqlDbType.Int);
                if (custId != 0)
                    parameters[0].Value = custId;
                else
                    parameters[0].Value = DBNull.Value;
                parameters[1] = new SqlParameter("@cust_header_msg", SqlDbType.VarChar);
                parameters[1].Value = custHeaderMsg;
                parameters[2] = new SqlParameter("@cust_footer_msg", SqlDbType.VarChar);
                parameters[2].Value = custFooterMsg;
                parameters[3] = new SqlParameter("@cust_side_msg", SqlDbType.VarChar);
                parameters[3].Value = custSideMsg;
                parameters[4] = new SqlParameter("@cust_welcome_msg", SqlDbType.VarChar);
                parameters[4].Value = custWelcomeMsg;
                return SqlHelper.ExecuteNonQuery(strCustomerConnection, CommandType.StoredProcedure, "SP_CMS_InsertCustomerMessages", parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataSet GetCustomerMessages(int custId)
        {
            try
            {
                return SqlHelper.ExecuteDataset(strCustomerConnection, CommandType.StoredProcedure, "SP_CMS_GetCustomerMessages", null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertCustomerUISettings(int custId, string headerColor, string headerFontName, string headerFontSize, string headerForeColor,
            string footerColor, string footerFontName, string footerFontSize, string footerForeColor,
            string sideColor, string sideFontName, string sideFontSize, string sideForeColor)
        {
            SqlParameter[] parameters = new SqlParameter[13];
            try
            {
                parameters[0] = new SqlParameter("@cust_Id", SqlDbType.Int);
                if (custId != 0)
                    parameters[0].Value = custId;
                else
                    parameters[0].Value = DBNull.Value;
                parameters[1] = new SqlParameter("@cust_header_font_name", SqlDbType.VarChar);
                parameters[1].Value = headerFontName;
                parameters[2] = new SqlParameter("@cust_header_color", SqlDbType.VarChar);
                parameters[2].Value = headerColor;
                parameters[3] = new SqlParameter("@cust_header_font_size", SqlDbType.VarChar);
                parameters[3].Value = headerFontSize;
                parameters[4] = new SqlParameter("@cust_header_font_color", SqlDbType.VarChar);
                parameters[4].Value = headerForeColor;

                parameters[5] = new SqlParameter("@cust_footer_font_name", SqlDbType.VarChar);
                parameters[5].Value = footerFontName;
                parameters[6] = new SqlParameter("@cust_footer_color", SqlDbType.VarChar);
                parameters[6].Value = footerColor;
                parameters[7] = new SqlParameter("@cust_footer_font_size", SqlDbType.VarChar);
                parameters[7].Value = footerFontSize;
                parameters[8] = new SqlParameter("@cust_footer_font_color", SqlDbType.VarChar);
                parameters[8].Value = footerForeColor;

                parameters[9] = new SqlParameter("@cust_sidebar_font_name", SqlDbType.VarChar);
                parameters[9].Value = sideFontName;
                parameters[10] = new SqlParameter("@cust_sidebar_color", SqlDbType.VarChar);
                parameters[10].Value = sideColor;
                parameters[11] = new SqlParameter("@cust_sidebar_font_size", SqlDbType.VarChar);
                parameters[11].Value = sideFontSize;
                parameters[12] = new SqlParameter("@cust_sidebar_font_color", SqlDbType.VarChar);
                parameters[12].Value = sideForeColor;

                return SqlHelper.ExecuteNonQuery(strCustomerConnection, CommandType.StoredProcedure, "SP_CMS_InsertCustomerUISettings", parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataSet GetCustomerUIsettings(int custId)
        {
            try
            {
                return SqlHelper.ExecuteDataset(strCustomerConnection, CommandType.StoredProcedure, "SP_CMS_GetCustomerUISettings", null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertCustomerLogos(int custId, string custName, byte[] headerLogo, byte[] footerLogo, byte[] sidebarLogo)
        {
            SqlParameter[] parameters = new SqlParameter[5];
            try
            {
                parameters[0] = new SqlParameter("@cust_Id", SqlDbType.Int);
                if (custId != 0)
                    parameters[0].Value = custId;
                else
                    parameters[0].Value = DBNull.Value;

                parameters[1] = new SqlParameter("@cust_name", SqlDbType.VarChar);
                parameters[1].Value = custName;

                parameters[2] = new SqlParameter("@cust_header_logo", SqlDbType.Image);
                if (headerLogo != null)
                    parameters[2].Value = headerLogo;
                else
                    parameters[2].Value = DBNull.Value;
                parameters[3] = new SqlParameter("@cust_footer_logo", SqlDbType.Image);
                if (footerLogo != null)
                    parameters[3].Value = footerLogo;
                else
                    parameters[3].Value = DBNull.Value;
                parameters[4] = new SqlParameter("@cust_other_logo", SqlDbType.Image);
                if (sidebarLogo != null)
                    parameters[4].Value = sidebarLogo;
                else
                    parameters[4].Value = DBNull.Value;
                return SqlHelper.ExecuteNonQuery(strCustomerConnection, CommandType.StoredProcedure, "SP_CMS_InsertCustomerLogos", parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataSet GetCustomerLogos()
        {
            try
            {
                return SqlHelper.ExecuteDataset(strCustomerConnection, CommandType.StoredProcedure, "SP_CMS_GetCustomerLogo", null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public byte[] GetCustomerLogo(int id)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            try
            {
                parameters[0] = new SqlParameter("@logoId", SqlDbType.Int);
                parameters[0].Value = id;

                return (byte[])SqlHelper.ExecuteScalar(strCustomerConnection, CommandType.StoredProcedure, "SP_CMS_GetCustomerLogo", parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteCustomerLogo(int id)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            try
            {
                parameters[0] = new SqlParameter("@logoId", SqlDbType.Int);
                if (id != 0)
                    parameters[0].Value = id;
                else
                    parameters[0].Value = DBNull.Value;

                return SqlHelper.ExecuteNonQuery(strCustomerConnection, CommandType.StoredProcedure, "SP_CMS_DeleteCustomerLogo", parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string UpdateCustomerInfo(int customerId, string customerName, string dbName, bool isActive, string dateEntered, string customerNum, string address1, string address2, string zip, int cityId, int stateId, int countryId, int customerSince,
            string primaryContact, string primaryEmail, string primaryPhone, string secondaryContact, string secondaryEmail, string secondaryPhone, string notes, string saleRepEmail, string CSRConEmail)
        {
            SqlParameter[] parameters = new SqlParameter[22];
            try
            {
                parameters[0] = new SqlParameter("@in_GteDatabases_Id", SqlDbType.Int);
                parameters[1] = new SqlParameter("@in_CustName", SqlDbType.NVarChar);
                parameters[2] = new SqlParameter("@in_Databasename", SqlDbType.NVarChar);
                parameters[3] = new SqlParameter("@in_Active", SqlDbType.Bit);
                parameters[4] = new SqlParameter("@in_DateEntered", SqlDbType.SmallDateTime);
                parameters[5] = new SqlParameter("@in_CustNumber", SqlDbType.NVarChar);
                parameters[6] = new SqlParameter("@in_Address1", SqlDbType.NVarChar);
                parameters[7] = new SqlParameter("@in_Address2", SqlDbType.NVarChar);
                parameters[8] = new SqlParameter("@in_Zip", SqlDbType.Int);
                parameters[9] = new SqlParameter("@in_City_Id", SqlDbType.Int);
                parameters[10] = new SqlParameter("@in_State_Id", SqlDbType.Int);
                parameters[11] = new SqlParameter("@in_Country_Id", SqlDbType.Int);
                parameters[12] = new SqlParameter("@in_Cust_Since", SqlDbType.Int);
                parameters[13] = new SqlParameter("@in_Primary_ContactName", SqlDbType.NVarChar);
                parameters[14] = new SqlParameter("@in_Primary_Email", SqlDbType.NVarChar);
                parameters[15] = new SqlParameter("@in_Primary_Phone", SqlDbType.NVarChar);
                parameters[16] = new SqlParameter("@in_Secondary_ContactName", SqlDbType.NVarChar);
                parameters[17] = new SqlParameter("@In_Secondary_Email", SqlDbType.NVarChar);
                parameters[18] = new SqlParameter("@In_Secondary_Phone", SqlDbType.NVarChar);
                parameters[19] = new SqlParameter("@In_Notes", SqlDbType.NVarChar);
                parameters[20] = new SqlParameter("@in_ABCLogisticsSalRep_Email", SqlDbType.NVarChar);
                parameters[21] = new SqlParameter("@in_CSRCon_Email", SqlDbType.NVarChar);
                
                parameters[0].Value = customerId;
                parameters[1].Value = customerName;
                parameters[2].Value = dbName;
                parameters[3].Value = isActive;
                parameters[4].Value = dateEntered;
                parameters[5].Value = customerNum;
                parameters[6].Value = address1;
                parameters[7].Value = address2;
                parameters[8].Value = zip;
                parameters[9].Value = cityId;
                parameters[10].Value = stateId;
                parameters[11].Value = countryId;
                parameters[12].Value = customerSince;
                parameters[13].Value = primaryContact;
                parameters[14].Value = primaryEmail;
                parameters[15].Value = primaryPhone;
                parameters[16].Value = secondaryContact;
                parameters[17].Value = secondaryEmail;
                parameters[18].Value = secondaryPhone;
                parameters[19].Value = notes;
                parameters[20].Value = saleRepEmail;
                parameters[21].Value = CSRConEmail;


                return SqlHelper.ExecuteNonQuery(strConnection, CommandType.StoredProcedure, "Gte_UpdateGTEDatabases", parameters).ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

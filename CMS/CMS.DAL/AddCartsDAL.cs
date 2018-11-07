using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CMS.DAL;
using ABCLogistics.DataAccess;
using System.Data.SqlClient;

namespace CMS.DAL
{


    public class AddCartsDAL
    {
        string CMSconnString = Convert.ToString(ConfigurationManager.AppSettings["CMSConnectionString"]);
        string CMSGteMasterconnString = Convert.ToString(ConfigurationManager.AppSettings["GTEMasterConnectionString"]);


        //public AddCartsDAL()
        //{
           
        //}
        //public AddCartsDAL (string strDBName) 
        //{
        //    CMSconnString = CMSconnString.Replace("?", strDBName);
        //}
 


        public DataSet GetProductImage(int ProductID, int ColorID, int SizeID, int ImagID)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@ProductID", SqlDbType.Int);
                parameters[0].Value = ProductID;
                parameters[1] = new SqlParameter("@ColorID", SqlDbType.Int);
                parameters[1].Value = ColorID;
                parameters[2] = new SqlParameter("@SizeID", SqlDbType.Int);
                parameters[2].Value = SizeID;
                parameters[3] = new SqlParameter("@ImagID", SqlDbType.Int);
                parameters[3].Value = ImagID;

                return SqlHelper.ExecuteDataset(CMSconnString, CommandType.StoredProcedure, "SP_CMS_GetProductImageRep", parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetProductDetails(int ProductID)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@ProductID", SqlDbType.Int);
                parameters[0].Value = ProductID;

                return SqlHelper.ExecuteDataset(CMSconnString, CommandType.StoredProcedure, "SP_CMS_GetProductDetails", parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetProductSize(int ProductID)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@ProductID", SqlDbType.Int);
                parameters[0].Value = ProductID;

                return SqlHelper.ExecuteDataset(CMSconnString, CommandType.StoredProcedure, "SP_CMS_GetProductSize", parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetProductColor(int ProductID, int SizeID)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@ProductID", SqlDbType.Int);
                parameters[0].Value = ProductID;
                parameters[1] = new SqlParameter("@SizeID", SqlDbType.Int);
                parameters[1].Value = SizeID;

                return SqlHelper.ExecuteDataset(CMSconnString, CommandType.StoredProcedure, "SP_CMS_GetProductColor", parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet ChkAvaQty(int ProductID, int ColorID, int SizeID)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@ProductID", SqlDbType.Int);
                parameters[0].Value = ProductID;
                parameters[1] = new SqlParameter("@ColorID", SqlDbType.Int);
                parameters[1].Value = ColorID;
                parameters[2] = new SqlParameter("@SizeID", SqlDbType.Int);
                parameters[2].Value = SizeID;

                return SqlHelper.ExecuteDataset(CMSconnString, CommandType.StoredProcedure, "SP_CMS_ChkAvaQty", parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Function to lock items
        /// </summary>
        /// <param name="strsessionId"></param>
        /// <param name="strPartNum"></param>
        /// <param name="strrev"></param>
        /// <param name="strSQL"></param>
        /// <param name="iqty_req"></param>
        /// <param name="strUserId"></param>
        /// <param name="strWhseNum"></param>
        /// <param name="strOrderType"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public int AddTempParts_DAL(string strsessionId, string strPartNum, int iqty_req, string strUserId, string strWhseNum, int ColorID, int SizeID)
        {
            SqlParameter[] parameters = new SqlParameter[7];

           
            try
            {
                parameters[0] = new SqlParameter("@PartNum", SqlDbType.NVarChar, 40);
               // parameters[1] = new SqlParameter("@rev", SqlDbType.NVarChar, 40);
                parameters[1] = new SqlParameter("@WhseNum", SqlDbType.Int);
               // parameters[3] = new SqlParameter("@SQL", SqlDbType.NVarChar, 1000);
                parameters[2] = new SqlParameter("@qty_req", SqlDbType.Int);
                parameters[3] = new SqlParameter("@sessionId", SqlDbType.NVarChar, 40);
                parameters[4] = new SqlParameter("@UserId", SqlDbType.Int);
                parameters[5] = new SqlParameter("@ColouId", SqlDbType.Int);
                parameters[6] = new SqlParameter("@SizeId", SqlDbType.Int);

                parameters[0].Value = strPartNum;
                //parameters[1].Value = strrev;
                parameters[1].Value = strWhseNum;
                //parameters[3].Value = string.Empty;
                parameters[2].Value = iqty_req;
                parameters[3].Value = strsessionId;
                parameters[4].Value = strUserId;
                parameters[5].Value = ColorID;
                parameters[6].Value = SizeID;

                return SqlHelper.ExecuteNonQuery(CMSconnString, CommandType.StoredProcedure, "SP_CMS_AddTempParts", parameters);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataSet GetTempParts(string strsessionId, string strOrderType)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 6000;
           
            try
            {
                parameters[0] = new SqlParameter("@sessionId", SqlDbType.NVarChar, 40);
                parameters[0].Value = strsessionId;
                parameters[1] = new SqlParameter("@OrderType", SqlDbType.Char, 1);
                parameters[1].Value = strOrderType;
                //return SqlHelper.ExecuteDataset(CMSconnString, CommandType.StoredProcedure, "SP_CMS_GetTempParts", parameters);


                using (SqlConnection connection = new SqlConnection(CMSconnString))
                {
                    connection.Open();

                    cmd = new SqlCommand("SP_CMS_GetTempParts", connection);
                    cmd.CommandType = CommandType.StoredProcedure;


                    foreach (SqlParameter p in parameters)
                    {
                        if (((p != null)))
                        {
                            // Check for derived output value with no value assigned
                           
                            cmd.Parameters.Add(p);
                        }
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    return ds;


                }
                

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        public int RemoveTempParts(string strsessionId, string strPartNum, int iqty_req, string strUserId, int ColorID, int SizeID)
        {
            SqlParameter[] parameters = new SqlParameter[6];
            
            try
            {
                parameters[0] = new SqlParameter("@PartNum", SqlDbType.NVarChar, 40);
                parameters[1] = new SqlParameter("@qty_req", SqlDbType.Int);
                parameters[2] = new SqlParameter("@sessionId", SqlDbType.NVarChar, 40);
                parameters[3] = new SqlParameter("@UserId", SqlDbType.Int);
                parameters[4] = new SqlParameter("@ColouId", SqlDbType.Int);
                parameters[5] = new SqlParameter("@SizeId", SqlDbType.Int);

                parameters[0].Value = strPartNum;
                parameters[1].Value = iqty_req;
                parameters[2].Value = strsessionId;
                parameters[3].Value = strUserId;
                parameters[4].Value = ColorID;
                parameters[5].Value = SizeID;
                return SqlHelper.ExecuteNonQuery(CMSconnString, CommandType.StoredProcedure, "SP_CMS_RemoveTempParts", parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteTempPartsDocuments(string strSessionId, int iPOId)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            try
            {
                parameters[0] = new SqlParameter("@SessionId", SqlDbType.NVarChar, 40);
                parameters[1] = new SqlParameter("@POid", SqlDbType.Int);
                parameters[0].Value = strSessionId;
                parameters[1].Value = iPOId;
                return SqlHelper.ExecuteNonQuery(CMSconnString, CommandType.StoredProcedure, "SP_WMS_DeleteTempPartsDocuments", parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertChkoutParts(string strSessionId, int iUserID)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            try
            {
                parameters[0] = new SqlParameter("@SessionId", SqlDbType.NVarChar, 40);
                parameters[1] = new SqlParameter("@userId", SqlDbType.BigInt);
                parameters[0].Value = strSessionId;
                parameters[1].Value = iUserID;
                return SqlHelper.ExecuteNonQuery(CMSconnString, CommandType.StoredProcedure, "SP_CMS_InstChkoutParts", parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetDefaultCMSWarehouses()
        {
            SqlParameter[] parameters = new SqlParameter[1];
            try
            {
                parameters[0] = new SqlParameter("@CMSDefault", SqlDbType.NVarChar);
                parameters[0].Value = 1;


                return SqlHelper.ExecuteDataset(CMSconnString, CommandType.StoredProcedure, "SP_CMS_GetDefaultWarehouses", parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while retrieving Warehouses  info - " + ex.Message.ToString());
            }
        }

        public DataSet GetViewCartDetail_DAL(string strSessionId, int iUserId)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            try
            {
                parameters[0] = new SqlParameter("@SessionId", SqlDbType.NVarChar, 100);
                parameters[0].Value = strSessionId;
                parameters[1] = new SqlParameter("@UserId", SqlDbType.Int);
                parameters[1].Value = iUserId;
                return SqlHelper.ExecuteDataset(CMSconnString, CommandType.StoredProcedure, "SP_CMS_GetViewCartDetail", parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetOrder_DAL(string strWhereClause, int iUserId, string DbName, string strGroupBy)
        {
            SqlParameter[] parameters = new SqlParameter[4];
            try
            {
                parameters[0] = new SqlParameter("@Where", SqlDbType.NVarChar);
                parameters[0].Value = strWhereClause;
                parameters[1] = new SqlParameter("@UserId", SqlDbType.Int);
                parameters[1].Value = iUserId;
                parameters[2] = new SqlParameter("@DbName", SqlDbType.VarChar);
                parameters[2].Value = DbName;
                parameters[3] = new SqlParameter("@GroupBy", SqlDbType.VarChar);
                parameters[3].Value = strGroupBy;
                return SqlHelper.ExecuteDataset(CMSGteMasterconnString, CommandType.StoredProcedure, "CMS_OrderStatus", parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet ChkMenuPCLSessionExist_DAL(string strSessionId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            try
            {
                parameters[0] = new SqlParameter("@SessionId", SqlDbType.NVarChar, 40);
                parameters[0].Value = strSessionId;

                return SqlHelper.ExecuteDataset(CMSconnString, CommandType.StoredProcedure, "SP_CMS_ChkMenuPCLSessionExist", parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DelChkoutParts_DAL(string strSessionId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            try
            {
                parameters[0] = new SqlParameter("@SessionId", SqlDbType.NVarChar,40);
                parameters[0].Value = strSessionId;

                return SqlHelper.ExecuteNonQuery(CMSconnString, CommandType.StoredProcedure, "SP_CMS_DelChkoutParts", parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string  InsertAddress_DAL(string CompanyNname, string StreetAdd, string Address2, int CountryId, int StateId, int CityId, string PostalCode, string Email, string Phone)
        {
            SqlParameter[] parameters = new SqlParameter[10];
            try
            {
                parameters[0] = new SqlParameter("@Company_name", SqlDbType.VarChar, 100);
                parameters[0].Value = CompanyNname;
                parameters[1] = new SqlParameter("@StreetAdd", SqlDbType.VarChar,75);
                parameters[1].Value = StreetAdd;
                parameters[2] = new SqlParameter("@Address2", SqlDbType.VarChar, 75);
                parameters[2].Value = Address2;
                parameters[3] = new SqlParameter("@Country_id", SqlDbType.Int);
                parameters[3].Value = CountryId;
                parameters[4] = new SqlParameter("@State_Id", SqlDbType.Int);
                parameters[4].Value = StateId;
                parameters[5] = new SqlParameter("@City_Id", SqlDbType.Int);
                parameters[5].Value = CityId;
                parameters[6] = new SqlParameter("@Postal_code", SqlDbType.VarChar,20);
                parameters[6].Value = PostalCode;
                parameters[7] = new SqlParameter("@Customer_email", SqlDbType.VarChar,50);
                parameters[7].Value = Email;
                parameters[8] = new SqlParameter("@Phone", SqlDbType.VarChar, 50);
                parameters[8].Value = Phone;
                parameters[9] = new SqlParameter("@AddId", SqlDbType.Int);
                parameters[9].Direction = ParameterDirection.Output;

                 SqlHelper.ExecuteNonQuery(CMSconnString, CommandType.StoredProcedure, "SP_CMS_UpdateAddress", parameters);

                 return parameters[9].Value.ToString();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

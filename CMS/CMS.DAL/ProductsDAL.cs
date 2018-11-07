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
    public class ProductsDAL
    {
        string strConnection = ConfigurationManager.AppSettings["CMSConnectionString"].ToString();
        public DataSet GetProducts(int subcategoryId, bool isSub, string orderBy)
        {
            SqlParameter[] parameters = new SqlParameter[3];
            try
            {
                parameters[0] = new SqlParameter("@subcategory_id", SqlDbType.Int);
                if (subcategoryId != 0)
                    parameters[0].Value = subcategoryId;
                else
                    parameters[0].Value = DBNull.Value;

                parameters[1] = new SqlParameter("@is_sub", SqlDbType.Bit);
                parameters[1].Value = isSub;
                parameters[2] = new SqlParameter("@orderBy", SqlDbType.VarChar);
                parameters[2].Value = orderBy;

                return SqlHelper.ExecuteDataset(strConnection, CommandType.StoredProcedure, "SP_CMS_GetProducts", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetProducts", subcategoryId.ToString(), isSub.ToString(), orderBy);
                throw new ApplicationException(strErrCode); 
            }
        }

        public byte[] GetProductImage(int imageId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            try
            {
                parameters[0] = new SqlParameter("@image_id", SqlDbType.Int);
                parameters[0].Value = imageId;
                
                return (byte[])SqlHelper.ExecuteScalar(strConnection, CommandType.StoredProcedure, "SP_CMS_GetProductImage", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetProductImage", imageId.ToString());
                throw new ApplicationException(strErrCode); 
            }
        }

        public DataSet GetProductSearch(int subcategoryId, string searchValue, string orderBy)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@subcategory_id", SqlDbType.Int);
                parameters[1] = new SqlParameter("@searchValue", SqlDbType.VarChar);
                parameters[2] = new SqlParameter("@orderBy", SqlDbType.VarChar);
                if (subcategoryId != 0)
                    parameters[0].Value = subcategoryId;
                else
                    parameters[0].Value = DBNull.Value;

                parameters[1].Value = searchValue;
                parameters[2].Value = orderBy;
                return SqlHelper.ExecuteDataset(strConnection, CommandType.StoredProcedure, "SP_CMS_GetProductsSearch", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetProductSearch", subcategoryId.ToString(),  searchValue,  orderBy);
                throw new ApplicationException(strErrCode); 
            }
        }

        public DataSet GetDDLProducts()
        {
            try
            {
                return SqlHelper.ExecuteDataset(strConnection, CommandType.StoredProcedure, "SP_CMS_GetDDLProducts", null);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetDDLProducts");
                throw new ApplicationException(strErrCode); 
            }
        }
        

    }
}

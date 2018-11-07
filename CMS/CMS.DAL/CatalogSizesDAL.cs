using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ABCLogistics.DataAccess;

namespace CMS.DAL
{
    public class CatalogSizesDAL
    {
        string CMSconnString = Convert.ToString(ConfigurationManager.AppSettings["CMSConnectionString"]);

        /*
         *Insert new Size.
         */
        public int InsertNewSize_DAL(string newSizeCode, string newSizeName)
        {
            SqlParameter[] parameters = new SqlParameter[3];
            try
            {
                parameters[0] = new SqlParameter("@newSizeCode", SqlDbType.NVarChar);
                if (newSizeCode.Trim() != string.Empty)
                    parameters[0].Value = newSizeCode.Trim();
                parameters[1] = new SqlParameter("@newSizeName", SqlDbType.NVarChar);
                if (newSizeName.Trim() != string.Empty)
                    parameters[1].Value = newSizeName.Trim();
                parameters[2] = new SqlParameter("@Out", SqlDbType.Int);
                parameters[2].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(CMSconnString, CommandType.StoredProcedure, "SP_CMS_InsertNewSize", parameters);
                return Convert.ToInt32( parameters[2].Value.ToString());
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "InsertNewSize_DAL", newSizeCode, newSizeName);
                throw new ApplicationException(strErrCode);
            }

        }//End of InsertNewSize_DAL method....   

        /*
         * Update the size
         */
        public int UpdateSize_DAL(int sizeID, string newSizeCode, string newSizeName)
        {
            SqlParameter[] parameters = new SqlParameter[3];
            try
            {
                parameters[0] = new SqlParameter("@updateSizeID", SqlDbType.Int);
                parameters[1] = new SqlParameter("@updateSizeCode", SqlDbType.NVarChar);
                parameters[2] = new SqlParameter("@updateSizeName", SqlDbType.NVarChar);


                if (sizeID != 0)
                    parameters[0].Value = sizeID;
                if (newSizeCode.Trim() != string.Empty)
                    parameters[1].Value = newSizeCode.Trim();
                if (newSizeName.Trim() != string.Empty)
                    parameters[2].Value = newSizeName.Trim();


                return SqlHelper.ExecuteNonQuery(CMSconnString, CommandType.StoredProcedure, "SP_CMS_UpdateSize", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "UpdateSize_DAL", sizeID.ToString(), newSizeCode,
                                                               newSizeName);
                throw new ApplicationException(strErrCode);
            }
        }//End of UpdateSize_DAL...

        /*
         * Get all the sizes
         */
        public DataSet GetSizes_DAL(int Active)
        {
            SqlParameter[] parameters = new SqlParameter[1];

            try
            {
                parameters[0] = new SqlParameter("@Active", SqlDbType.Int);
                parameters[0].Value = Active;
                return SqlHelper.ExecuteDataset(CMSconnString, CommandType.StoredProcedure, "SP_CMS_GetSizes", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetSizes_DAL");
                throw new ApplicationException(strErrCode);
            }
        }// End of GetSizes_DAL method...

        /*
         * Delete the size by size ID
         */
        public string DeleteSize_DAL(int id, bool isActive)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            try
            {
                parameters[0] = new SqlParameter("@deleteSizeID", SqlDbType.Int);
                if (id != 0)
                    parameters[0].Value = id;
                else
                    parameters[0].Value = DBNull.Value;
                parameters[1] = new SqlParameter("@is_active", SqlDbType.Bit);
                parameters[1].Value = isActive;
                return SqlHelper.ExecuteScalar(CMSconnString, CommandType.StoredProcedure, "SP_CMS_DeleteSize", parameters).ToString();
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "DeleteSize_DAL", id.ToString(), isActive.ToString());
                throw new ApplicationException(strErrCode);
            }
        }// End of DeleteSize_DAL method...


        /*
         * Get size by sizeID
         */
        public DataSet GetSizeBySizeID_DAL(int id)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            try
            {
                parameters[0] = new SqlParameter("@newSizeID ", SqlDbType.Int);

                if (id != 0)
                    parameters[0].Value = id;

                return SqlHelper.ExecuteDataset(CMSconnString, CommandType.StoredProcedure, "SP_CMS_GetSizeBySizeID", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetSizeBySizeID_DAL", id.ToString());
                throw new ApplicationException(strErrCode);
            }
        }//End of GetSizeBySizeID_DAL method...
    }
}

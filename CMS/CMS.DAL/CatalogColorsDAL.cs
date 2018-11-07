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
    public class CatalogColorsDAL
    {
        string CMSconnString = Convert.ToString(ConfigurationManager.AppSettings["CMSConnectionString"]);

        public DataSet GetColors_DAL()
        {
            try
            {
                return SqlHelper.ExecuteDataset(CMSconnString, CommandType.StoredProcedure, "SP_CMS_GetColors", null);
            }
            catch (Exception ex)
            {   string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetColors_DAL");
                throw new ApplicationException(strErrCode);
            }
        }// End of GetSizes_DAL method...

        /*
         * Delete the color by color ID
         */
        public string DeleteColor_DAL(int colorID)
        {
            SqlParameter[] parameters = new SqlParameter[1];

            try
            {
                parameters[0] = new SqlParameter("@deleteColorID ", SqlDbType.Int);
                if (colorID != 0)
                    parameters[0].Value = colorID;
          
                return SqlHelper.ExecuteScalar(CMSconnString, CommandType.StoredProcedure, "SP_CMS_DeleteColor", parameters).ToString();
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "DeleteColor_DAL", colorID.ToString());
                throw new ApplicationException(strErrCode);
            }
        }// End of DeleteColor_DAL method...

        /*
         * Insert new color
         */
        public int InsertNewColor_DAL(string name, string code,string value)
        {
            SqlParameter[] parameters = new SqlParameter[4];

            try
            {
                parameters[0] = new SqlParameter("@newColorName", SqlDbType.NVarChar);
                parameters[1] = new SqlParameter("@newColorCode", SqlDbType.NVarChar);
                parameters[2] = new SqlParameter("@newColorVal ", SqlDbType.NVarChar);

                if (name.Trim() != string.Empty)
                    parameters[0].Value = name.Trim();
                if (code.Trim() != string.Empty)
                    parameters[1].Value = code.Trim();
                if (value.Trim() != string.Empty)
                    parameters[2].Value = value.Trim();
                parameters[3] = new SqlParameter("@Out", SqlDbType.Int);
                parameters[3].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(CMSconnString, CommandType.StoredProcedure, "SP_CMS_InsertNewColor", parameters);
                return Convert.ToInt32(parameters[3].Value.ToString());
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "InsertNewColor_DAL", name, code, value);
                throw new ApplicationException(strErrCode);
            }       
        }//End of InsertNewColor_DAL...

        /*
         * Update the color
         */
        public int UpdateColor_DAL(int ID, string name, string code, string value)
        {
            SqlParameter[] parameters = new SqlParameter[4];

            try
            {
                parameters[0] = new SqlParameter("@newColorID", SqlDbType.Int);
                parameters[1] = new SqlParameter("@newColorName", SqlDbType.NVarChar);
                parameters[2] = new SqlParameter("@newColorCode", SqlDbType.NVarChar);
                parameters[3] = new SqlParameter("@newColorValue ", SqlDbType.NVarChar);

                if (ID != 0)
                    parameters[0].Value = ID;
                if (name.Trim() != string.Empty)
                    parameters[1].Value = name.Trim();
                if (code.Trim() != string.Empty)
                    parameters[2].Value = code.Trim();
                if (value.Trim() != string.Empty)
                    parameters[3].Value = value.Trim();
                return SqlHelper.ExecuteNonQuery(CMSconnString, CommandType.StoredProcedure, "SP_CMS_UpdateColor", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "UpdateColor_DAL", ID.ToString(), name, code,value);
                throw new ApplicationException(strErrCode);
            }      
        }//End of UpdateSize_DAL...

        /*
         * Get Color by ColorID
         */
        public DataSet GetColorByColorID_DAL(int id)
        {
            SqlParameter[] parameters = new SqlParameter[1];

            try
            {
                parameters[0] = new SqlParameter("@newColorID ", SqlDbType.Int);

                if (id != 0)
                    parameters[0].Value = id;

                return SqlHelper.ExecuteDataset(CMSconnString, CommandType.StoredProcedure, "SP_CMS_GetColorByColorID", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetColorByColorID_DAL", id.ToString());
                throw new ApplicationException(strErrCode);
            }      
        }//End of GetColorByColorID_DAL...
    }
}

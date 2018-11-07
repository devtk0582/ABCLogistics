using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CMS.DAL;
using ABCLogistics.DataAccess;
namespace CMS.DAL
{
    public class CatalogCategoriesDAL
    {
        string CMSconnString = Convert.ToString(ConfigurationManager.AppSettings["CMSConnectionString"]);

        public DataSet GetCategories_DAL()
        { 
            try
            {
                return SqlHelper.ExecuteDataset(CMSconnString, CommandType.StoredProcedure, "SP_CMS_GetCategories", null);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetCategories_DAL");
                throw new ApplicationException(strErrCode);
            }
        }// End of GetCategories_DAL method...


        /*
         * Get sub categories by parent category
         */
        public DataSet GetCategoryByCategory_DAL(int id)
        {
            SqlParameter[] parameters = new SqlParameter[1];

            try
            {
                parameters[0] = new SqlParameter("@newCategoryID ", SqlDbType.Int);

                if (id != 0)
                    parameters[0].Value = id;

                return SqlHelper.ExecuteDataset(CMSconnString, CommandType.StoredProcedure, "SP_CMS_GetCategoryByCategoryID", parameters);

            }
            catch (Exception ex)
            {   
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetCategoryByCategory_DAL", id.ToString());
                throw new ApplicationException(strErrCode);
            }   
        }//GetCategoryByCategory_DAL

        /*
         *Insert new Category.
         */
        public int  InsertNewCategory_DAL(string newCatName)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            try
            {
                parameters[0] = new SqlParameter("@newCategoryName", SqlDbType.NVarChar);
             
                if (newCatName.Trim() != string.Empty)
                    parameters[0].Value = newCatName.Trim();
                

                return SqlHelper.ExecuteNonQuery(CMSconnString, CommandType.StoredProcedure, "SP_CMS_InsertNewCategory", parameters);
            }
            catch (Exception ex)
            {                
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "InsertNewCategory_DAL", newCatName);
                throw new ApplicationException(strErrCode);
            }
        }//End of InsertNewSize_DAL method....   

        /*
         * Update Category
         */
        public int UpdateCategory_DAL(int newID, string newName)
        {
            SqlParameter[] parameters = new SqlParameter[2];

             try
            {
                parameters[0] = new SqlParameter("@updateCategoryID", SqlDbType.Int);
                parameters[1] = new SqlParameter("@updateName", SqlDbType.NVarChar);
               

                if (newID != 0)
                    parameters[0].Value = newID;
                if (newName.Trim() != string.Empty)
                    parameters[1].Value = newName.Trim();
           

                return SqlHelper.ExecuteNonQuery(CMSconnString, CommandType.StoredProcedure, "SP_CMS_UpdateCategory", parameters);
            }
            catch (Exception ex)
            {                
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "UpdateCategory_DAL", newID.ToString(), newName);
                throw new ApplicationException(strErrCode);
            }
        }//End of UpdateSize_DAL...

        /*
         * Delete the category by category ID
         */
        public string DeleteCategory_DAL(int id, bool isActive)
        {
            SqlParameter[] parameters = new SqlParameter[2];

            try
            {
                parameters[0] = new SqlParameter("@deleteCatID ", SqlDbType.BigInt);
                if (id != 0)
                    parameters[0].Value = id;
                else
                    parameters[0].Value = DBNull.Value;
                parameters[1] = new SqlParameter("@is_active", SqlDbType.Bit);
                parameters[1].Value = isActive;
                return SqlHelper.ExecuteScalar(CMSconnString, CommandType.StoredProcedure, "SP_CMS_DeleteCategory", parameters).ToString();
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "DeleteCategory_DAL", id.ToString(), isActive.ToString());
                throw new ApplicationException(strErrCode);
            }
        }// End of DeleteSize_DAL method...


        /*
         * Get subcategory by Category Name
         */
        public DataSet GetSubcategoryByCategoryName_DAL(string name)
        {
            SqlParameter[] parameters = new SqlParameter[1];

            try
            {
                parameters[0] = new SqlParameter("@categoryName ", SqlDbType.BigInt);

                if (name.Trim() != string.Empty)
                    parameters[0].Value = Int64.Parse(name);
                else 
                    parameters[0].Value = DBNull.Value;

                return SqlHelper.ExecuteDataset(CMSconnString, CommandType.StoredProcedure, "SP_CMS_GetSubcategoriesByCatgoryName", parameters);
            }
            catch (Exception ex)
            {                
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetSubcategoryByCategoryName_DAL", name);
                throw new ApplicationException(strErrCode);
            }    
        }

        /*
         *Insert new Size.
         */
        public int InsertNewSubcategory_DAL(string name, string desc, string shipClass, bool tax,
                                                int featureRank, int catID)
        {
            SqlParameter[] parameters = new SqlParameter[6];
               
            try
            {
                parameters[0] = new SqlParameter("@newSubcategoryName", SqlDbType.NVarChar);
                parameters[1] = new SqlParameter("@newSubcategoryDesc", SqlDbType.NVarChar);
                parameters[2] = new SqlParameter("@newShippingClass ", SqlDbType.NVarChar);
                parameters[3] = new SqlParameter("@newIsTaxable", SqlDbType.Bit);
                parameters[4] = new SqlParameter("@newFeatureRank", SqlDbType.BigInt);   
                parameters[5] = new SqlParameter("@newCategoryID ", SqlDbType.BigInt);

                if (name.Trim() != string.Empty)
                    parameters[0].Value = name.Trim();          
                if (desc.Trim() != string.Empty)
                    parameters[1].Value = desc.Trim();
                if (shipClass.Trim() != string.Empty)
                    parameters[2].Value = shipClass.Trim();
                parameters[3].Value = tax;
                if (featureRank != 0)
                    parameters[4].Value = featureRank;          
                if (catID != 0)
                    parameters[5].Value = catID;

                return SqlHelper.ExecuteNonQuery(CMSconnString, CommandType.StoredProcedure, "SP_CMS_InsertNewSubcategories", parameters);
      
            }
            catch (Exception ex)
            {                
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "InsertNewSubcategory_DAL", name, desc, shipClass, tax.ToString(),
                                                             featureRank.ToString(), catID.ToString());
                throw new ApplicationException(strErrCode);
            }      
        }//End of InsertNewSize_DAL method....   

        /*
         *Update subcategory.
         */
        public int UpdateSubcategory_DAL(string name, string desc, string shipClass, bool tax,
                                                int featureRank,int catID,int subCatID)
        {
            SqlParameter[] parameters = new SqlParameter[7];
                           
            try
            {
                parameters[0] = new SqlParameter("@newSubcategoryName", SqlDbType.NVarChar);        
                parameters[1] = new SqlParameter("@newSubcategoryDesc", SqlDbType.NVarChar);
                parameters[2] = new SqlParameter("@newShippingClass ", SqlDbType.NVarChar);
                parameters[3] = new SqlParameter("@newIsTaxable", SqlDbType.Bit);
                parameters[4] = new SqlParameter("@newFeatureRank", SqlDbType.BigInt);        
                parameters[5] = new SqlParameter("@newCategoryID ", SqlDbType.BigInt);
                parameters[6] = new SqlParameter("@newSubcategoryID ", SqlDbType.BigInt);

                if (name.Trim() != string.Empty)
                    parameters[0].Value = name.Trim();
                if (desc.Trim() != string.Empty)
                    parameters[1].Value = desc.Trim();
                if (shipClass.Trim() != string.Empty)
                    parameters[2].Value = shipClass.Trim();
                parameters[3].Value = tax;
                if (featureRank != 0)
                    parameters[4].Value = featureRank;
                if (catID != 0)
                    parameters[5].Value = catID;
                if (subCatID != 0)
                    parameters[6].Value = subCatID;

                return SqlHelper.ExecuteNonQuery(CMSconnString, CommandType.StoredProcedure, "SP_CMS_UpdateSubcategories", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "UpdateSubcategory_DAL", name,  desc, shipClass, tax.ToString(),
                                                 featureRank.ToString(),   catID.ToString(), subCatID.ToString());
                throw new ApplicationException(strErrCode);
            }      
        }//End of InsertNewSize_DAL method....   

        /*
         * Get subcategory by subcategory ID
         */
        public DataSet GetSubcategoryBySubcategoryID_DAL(int id)
        {
            SqlParameter[] parameters = new SqlParameter[1];

            try
            {
                parameters[0] = new SqlParameter("@subcategoryID ", SqlDbType.BigInt);

                if (id != 0)
                    parameters[0].Value = id;

                return SqlHelper.ExecuteDataset(CMSconnString, CommandType.StoredProcedure, "SP_CMS_GetSubCategoryBySubcategoryID", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetSubcategoryBySubcategoryID_DAL", id.ToString());
                throw new ApplicationException(strErrCode);
            }    
        }//End of GetSubcategoryBySubcategoryID_DAL...


        /*
         * Delete the Subcategory by Subcategory ID
         */
        public string DeleteSubcategory_DAL(int id, bool isActive)
        {
            SqlParameter[] parameters = new SqlParameter[2];

            try
            {
                parameters[0] = new SqlParameter("@subCatID ", SqlDbType.BigInt);
                if (id != 0)
                    parameters[0].Value = id;
                else
                    parameters[0].Value = DBNull.Value;
                parameters[1] = new SqlParameter("@is_active", SqlDbType.Bit);
                parameters[1].Value = isActive;
                return SqlHelper.ExecuteScalar(CMSconnString, CommandType.StoredProcedure, "SP_CMS_DeleteSubCategory", parameters).ToString();
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "DeleteSubcategory_DAL", id.ToString());
                throw new ApplicationException(strErrCode);
            }
        }// End of DeleteSize_DAL method...

        public DataSet GetCategories()
        {
            try
            {
                return SqlHelper.ExecuteDataset(CMSconnString, CommandType.StoredProcedure, "SP_CMS_GetProductCategories", null);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetCategories");
                throw new ApplicationException(strErrCode);
            }
        }

        public DataSet GetSubCategories(int categoryId)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@categoryId", SqlDbType.Int);
                if (categoryId != 0)
                    parameters[0].Value = categoryId;
                else
                    parameters[0].Value = DBNull.Value;
                return SqlHelper.ExecuteDataset(CMSconnString, CommandType.StoredProcedure, "SP_CMS_GetProductSubcategories", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetSubCategories", categoryId.ToString());
                throw new ApplicationException(strErrCode);
            }
        }

        public DataSet GetCategoriesSearch()
        {
            try
            {
                return SqlHelper.ExecuteDataset(CMSconnString, CommandType.StoredProcedure, "SP_CMS_GetCategoriesSearch", null);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetCategoriesSearch");
                throw new ApplicationException(strErrCode);
            }
        }

        public string GetCategoryNameBySubCategory(int subcategoryId)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@subcategoryId", SqlDbType.Int);
                if (subcategoryId != 0)
                    parameters[0].Value = subcategoryId;
                else
                    parameters[0].Value = DBNull.Value;
                return SqlHelper.ExecuteScalar(CMSconnString, CommandType.StoredProcedure, "SP_CMS_GetProductCategoryBySubCategory", parameters).ToString();
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetCategoryNameBySubCategory", subcategoryId.ToString());
                throw new ApplicationException(strErrCode);
            }
        }

    }
}

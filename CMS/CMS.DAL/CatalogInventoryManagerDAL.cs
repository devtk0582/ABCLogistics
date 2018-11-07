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
    public class CatalogInventoryManagerDAL
    {
        string CMSconnString = Convert.ToString(ConfigurationManager.AppSettings["CMSConnectionString"]);

        /*
         * Get category name for DDL 
         */
        public DataSet GetCategoryName_DAL()
        {
            try
            {
                return SqlHelper.ExecuteDataset(CMSconnString, CommandType.StoredProcedure, "SP_CMS_GetCategoryName", null);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetCategoryName_DAL");
                throw new ApplicationException(strErrCode);

            }
        }// End of GetCategoryName_DAL method...

        public DataSet GetManufactureDescription_DAL()
        {
            try
            {
                return SqlHelper.ExecuteDataset(CMSconnString, CommandType.StoredProcedure, "SP_CMS_GetManufacturers", null);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetManufactureDescription_DAL");
                throw new ApplicationException(strErrCode);
            }
        }// End of GetCategoryName_DAL method...

        //Get the products.
        public DataSet GetInvenotry_DAL(int categoryID, int mfrID)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            
            try
            {
                parameters[0] = new SqlParameter("@in_Category_ID ", SqlDbType.Int);
                if (categoryID != 0)
                    parameters[0].Value = categoryID;
                parameters[1] = new SqlParameter("@in_Manufectured_Id ", SqlDbType.Int);
                if (mfrID != 0)
                    parameters[1].Value = mfrID;
                                
                return SqlHelper.ExecuteDataset(CMSconnString, CommandType.StoredProcedure, "SP_CMS_GetInventory", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetInvenotry_DAL", categoryID.ToString(),mfrID.ToString());
                throw new ApplicationException(strErrCode);
            } 
        }

        /*
         * Get Product by inventoryID ID
         */
        public DataSet GetProductByInventoryID_DAL(int id)
        {
            SqlParameter[] parameters = new SqlParameter[1];

            try
            {
                parameters[0] = new SqlParameter("@newInventoryID ", SqlDbType.Int);

                if (id != 0)
                    parameters[0].Value = id;

                return SqlHelper.ExecuteDataset(CMSconnString, CommandType.StoredProcedure, "SP_CMS_GetProductByInventoryID", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetProductByInventoryID_DAL", id.ToString());
                throw new ApplicationException(strErrCode);
            }
        }//End of GetProductByInventoryID_DAL...

        /*
         * Get Category by Category Name
         */
        public DataSet GetCategoryByCategoryName_DAL(string name)
        {
            SqlParameter[] parameters = new SqlParameter[1];

            try
            {
                parameters[0] = new SqlParameter("@newCatName ", SqlDbType.NVarChar);

                if (name.Trim() != string.Empty)
                    parameters[0].Value = name.Trim();

                return SqlHelper.ExecuteDataset(CMSconnString, CommandType.StoredProcedure, "SP_CMS_GetCategoryNameByCategoryName", parameters);                
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetCategoryByCategoryName_DAL", name);
                throw new ApplicationException(strErrCode);
            }
        }

        public DataSet GetSubCategoryBySubcategoryName_DAL(string name, char U)
        {
            SqlParameter[] parameters = new SqlParameter[2];

            try
            {
                parameters[0] = new SqlParameter("@newSubcatName ", SqlDbType.NVarChar);
                parameters[1] = new SqlParameter("@in_Flag ", SqlDbType.Char);
                if (name.Trim() != string.Empty)
                    parameters[0].Value = name.Trim();
                parameters[1].Value = U;
                return SqlHelper.ExecuteDataset(CMSconnString, CommandType.StoredProcedure, "SP_CMS_GetSubCategoryBySubcategoryName", parameters);

            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetSubCategoryBySubcategoryName_DAL",name, U.ToString());
                throw new ApplicationException(strErrCode);
            } 
        }

        public DataSet GetInvenotries_DAL(int id)
        {
            SqlParameter[] parameters = new SqlParameter[1];

            try
            {
                parameters[0] = new SqlParameter("@newInvID ", SqlDbType.Int);
                if (id != 0)
                    parameters[0].Value = id;
                return SqlHelper.ExecuteDataset(CMSconnString, CommandType.StoredProcedure, "SP_CMS_GetInventories", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetInvenotries_DAL", id.ToString());
                throw new ApplicationException(strErrCode);
            }
        }

        public DataSet GetInventoriesImagesbyInvtID_DAL(int id)
        {
            SqlParameter[] parameters = new SqlParameter[1];

            try
            {
                parameters[0] = new SqlParameter("@newInvID", SqlDbType.Int);
                if (id != 0)
                    parameters[0].Value = id;
                return SqlHelper.ExecuteDataset(CMSconnString, CommandType.StoredProcedure, "SP_CMS_GetInventoriesImagesbyInvtID", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetInventoriesImagesbyInvtID_DAL", id.ToString());
                throw new ApplicationException(strErrCode);
            }
        }

        public DataSet GetSubcatgories_DAL()
        {
            try
            {
                return SqlHelper.ExecuteDataset(CMSconnString, CommandType.StoredProcedure, "SP_CMS_GetCategoriesSubCatShippingClass", null);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetSubcatgories_DAL");
                throw new ApplicationException(strErrCode);
            }
        }

        /*
         * d the inventories
         */
        public int UpdateInventories_DAL(int invtID, int newInvTemplateID, int sizeID, int colorID, string newSKU, 
                                         int newQty, int inwhsenum, int userid)
        {
            SqlParameter[] parameters = new SqlParameter[8];

            try
            {
                parameters[0] = new SqlParameter("@invtExtID", SqlDbType.Int);
                parameters[1] = new SqlParameter("@newInvTemplateID", SqlDbType.Int);
                parameters[2] = new SqlParameter("@newSizeID", SqlDbType.Int);
                parameters[3] = new SqlParameter("@newColorID ", SqlDbType.Int);
                parameters[4] = new SqlParameter("@newSKUNumber", SqlDbType.NVarChar);
                parameters[5] = new SqlParameter("@newQty", SqlDbType.Int);
                parameters[6] = new SqlParameter("@inwhsenum", SqlDbType.Int);
                parameters[7] = new SqlParameter("@userid", SqlDbType.Int);
                if (invtID != 0)
                    parameters[0].Value = invtID;

                if (newInvTemplateID != 0)
                    parameters[1].Value = newInvTemplateID;

                if (sizeID != 0)
                    parameters[2].Value = sizeID;

                if (colorID != 0)
                    parameters[3].Value = colorID;

                if (newSKU.Trim() != string.Empty)
                    parameters[4].Value = newSKU.Trim();

                parameters[5].Value = newQty;
                    
                parameters[6].Value = inwhsenum;
                parameters[7].Value = userid;

                return SqlHelper.ExecuteNonQuery(CMSconnString, CommandType.StoredProcedure, "SP_CMS_UpdateInventories", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "UpdateInventories_DAL",invtID.ToString(), newInvTemplateID.ToString(), 
                                                                sizeID.ToString(),colorID.ToString(), newSKU, newQty.ToString(),inwhsenum.ToString(), userid.ToString());

                throw new ApplicationException(strErrCode);
            }
        }//End of UpdateInventories_DAL...

        /*
         * Add new Product
         */
        public string AddNewProductToCustInvTemp_DAL(int invtID, string newPartNum, string newDesc1, double newWeight, double newPrice,
                                       char I)
        {
            SqlParameter[] parameters = new SqlParameter[49];

            try
            {
                parameters[0] = new SqlParameter("@in_InvTemp_Id", SqlDbType.Int);
                if (invtID != 0)
                    parameters[0].Value = invtID;                    
                parameters[1] = new SqlParameter("@in_PartNo",SqlDbType.NVarChar);
                    if (newPartNum.Trim() != string.Empty)
                        parameters[1].Value = newPartNum.Trim();
                parameters[2] = new SqlParameter("@in_Manufacturer",SqlDbType.NVarChar);
                    parameters[2].Value = null; 
                parameters[3] = new SqlParameter("@in_Rev",SqlDbType.NVarChar);
                    parameters[3].Value = null;
                parameters[4] = new SqlParameter("@in_BulkItem",SqlDbType.Bit);
                    parameters[4].Value = 0;
                parameters[5] = new SqlParameter("@in_ModelN0",SqlDbType.NVarChar);
                    parameters[5].Value = null;
                parameters[6] = new SqlParameter("@in_Description1",SqlDbType.NVarChar);
                    if (newDesc1.Trim() != string.Empty)
                        parameters[6].Value = newDesc1.Trim();                
                parameters[7] = new SqlParameter("@in_Description2",SqlDbType.NVarChar);
                    parameters[7].Value = null;
                parameters[8] = new SqlParameter("@in_UOM",SqlDbType.NVarChar);
                    parameters[8].Value = null;
                parameters[9] = new SqlParameter("@in_Hieght",SqlDbType.Float);
                    parameters[9].Value = 0;
                parameters[10] = new SqlParameter("@in_Weight",SqlDbType.Float);
                    if (newWeight != 0)
                    parameters[10].Value = newWeight;
                parameters[11] = new SqlParameter("@in_Width",SqlDbType.Float);
                    parameters[11].Value = 0;
                parameters[12] = new SqlParameter("@in_Length",SqlDbType.Float);
                    parameters[12].Value = 0;
                parameters[13] = new SqlParameter("@in_Warrentyable", SqlDbType.Bit);
                    parameters[13].Value = 0;
                parameters[14] = new SqlParameter("@in_CountryOfOrigin",SqlDbType.NVarChar);
                    parameters[14].Value = null;
                parameters[15] = new SqlParameter("@in_HarmonizedCode",SqlDbType.NVarChar);
                    parameters[15].Value = null;
                parameters[16] = new SqlParameter("@in_FamilyCode",SqlDbType.NVarChar);
                    parameters[16].Value = null;
                parameters[17] = new SqlParameter("@in_ECCN_Code",SqlDbType.NVarChar);
                    parameters[17].Value = null;    
                parameters[18] = new SqlParameter("@in_MFG_PartNo1",SqlDbType.NVarChar);
                    parameters[18].Value = null;
                parameters[19] = new SqlParameter("@in_MFG_PartNo2",SqlDbType.NVarChar);
                    parameters[19].Value = null;
                parameters[20] = new SqlParameter("@In_ReqSerialNo",SqlDbType.Bit);
                    parameters[20].Value = 0;
                parameters[21] = new SqlParameter("@In_ReqLotNo", SqlDbType.Bit);
                    parameters[21].Value = 0;
                parameters[22] = new SqlParameter("@In_Oversize", SqlDbType.Bit);
                    parameters[22].Value = 0;
                parameters[23] = new SqlParameter("@In_Crated", SqlDbType.Bit);
                    parameters[23].Value = 0;
                parameters[24] = new SqlParameter("@In_Heavy", SqlDbType.Bit);
                    parameters[24].Value = 0;
                parameters[25] = new SqlParameter("@In_Hazmat", SqlDbType.Bit);
                    parameters[25].Value = 0;
                parameters[26] = new SqlParameter("@In_Price",  SqlDbType.Float);
                    if (newPrice != 0)
                        parameters[26].Value = newPrice;                
                parameters[27] = new SqlParameter("@In_SKU",  SqlDbType.NVarChar);
                    parameters[27].Value = null;
                parameters[28] = new SqlParameter("@In_StyleNumber",  SqlDbType.NVarChar);
                    parameters[28].Value = null;
                parameters[29] = new SqlParameter("@In_UPC",  SqlDbType.NVarChar);
                    parameters[29].Value = null;
                parameters[30] = new SqlParameter("@In_NMFCDescription",  SqlDbType.NVarChar);
                    parameters[30].Value = null;
                parameters[31] = new SqlParameter("@In_NMFCNumber",  SqlDbType.NVarChar);
                    parameters[31].Value = null;
                parameters[32] = new SqlParameter("@In_FreightClass",  SqlDbType.Char);
                    parameters[32].Value = null;
                parameters[33] = new SqlParameter("@In_CasesPerLayer",  SqlDbType.Int);
                    parameters[33].Value = 0;
                parameters[34] = new SqlParameter("@In_LayersPerPallet",  SqlDbType.Int);
                    parameters[34].Value = 0;
                parameters[35] = new SqlParameter("@In_EachesPerPallet",  SqlDbType.Int);
                    parameters[35].Value = 0;
                parameters[36] = new SqlParameter("@In_MinShipQty",  SqlDbType.Int);
                    parameters[36].Value = 0;
                parameters[37] = new SqlParameter("@In_WhPalletStackHeight",  SqlDbType.Decimal);
                    parameters[37].Value = 0;
                parameters[38] = new SqlParameter("@In_ShelfLife",  SqlDbType.Decimal);
                    parameters[38].Value = 0;
                parameters[39] = new SqlParameter("@In_CubicDensity",  SqlDbType.Decimal);
                    parameters[39].Value = 0;
                parameters[40] = new SqlParameter("@In_TotalPalletHeight",  SqlDbType.Decimal);
                    parameters[40].Value = 0;
                parameters[41] = new SqlParameter("@In_CoPackerPlantText",   SqlDbType.NVarChar);
                    parameters[41].Value = null;
                parameters[42] = new SqlParameter("@In_ProductRotation",   SqlDbType.NVarChar);
                    parameters[42].Value = null;
                parameters[43] = new SqlParameter("@In_TempControl",   SqlDbType.Bit);
                    parameters[43].Value = 0;
                parameters[44] = new SqlParameter("@In_EachesPerCase",  SqlDbType.Decimal);
                    parameters[44].Value = 0;
                parameters[45] = new SqlParameter("@In_ContainerType",  SqlDbType.Int);
                    parameters[45].Value = 0;
                parameters[46] = new SqlParameter("@In_TotalPartsQty",  SqlDbType.Int);
                    parameters[46].Value = 0;
                parameters[47] = new SqlParameter("@in_Flag",   SqlDbType.Char);
                    parameters[47].Value = I;
                parameters[48] = new SqlParameter("@out_msg",   SqlDbType.NVarChar,200);
                parameters[48].Direction = ParameterDirection.Output;

                 SqlHelper.ExecuteNonQuery(CMSconnString, CommandType.StoredProcedure, "SP_WMS_UpdateCustInvTemp", parameters);
               return parameters[48].Value.ToString();

            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "AddNewProductToCustInvTemp_DAL", invtID.ToString(),
                                                                newPartNum, newDesc1, newWeight.ToString(), newPrice.ToString(), I.ToString());
                throw new ApplicationException(strErrCode);
            }
        }//End of AddNewProductToCustInvTemp_DAL...

        /*
         *Add new/edit Prodcut
         */
        public int AddNewProduct_DAL(int newIntID, int subcat, int newCat, int newMfr, double newListPrice,
                                     int newShipClass, Char I,bool newIsFeatureProd,  bool newIsLive,
                                     string newDescription)
        {
                                    
            SqlParameter[] parameters = new SqlParameter[10];
            try
            {
                parameters[0] = new SqlParameter("@in_inv_template_id", SqlDbType.Int);
                if (newIntID != 0)
                    parameters[0].Value = newIntID;
                parameters[1] = new SqlParameter("@in_SubCategory_Id", SqlDbType.Int);
                if (subcat != 0)
                    parameters[1].Value = subcat;
                parameters[2] = new SqlParameter("@in_Category_ID", SqlDbType.Int);
                if (newCat != 0)
                    parameters[2].Value = newCat;
                parameters[3] = new SqlParameter("@in_Manufectured_Id", SqlDbType.Int);
                if (newMfr != 0)
                    parameters[3].Value = newMfr;
                parameters[4] = new SqlParameter("@in_List_price", SqlDbType.Float);
                if (newListPrice != 0)
                    parameters[4].Value = newListPrice;
                parameters[5] = new SqlParameter("@in_Shipping_ID", SqlDbType.Int);
                if (newShipClass != 0)
                    parameters[5].Value = newShipClass;
                parameters[6] = new SqlParameter("@in_Flag", SqlDbType.Char);
                parameters[6].Value = I;
                parameters[7] = new SqlParameter("@in_Is_FeatureProduct", SqlDbType.Bit);
                parameters[7].Value = newIsFeatureProd;
                parameters[8] = new SqlParameter("@in_IsLive", SqlDbType.Bit);
                parameters[8].Value = newIsLive;
                parameters[9] = new SqlParameter("@in_Description", SqlDbType.NVarChar);
                if (newDescription.Trim() != string.Empty)
                    parameters[9].Value = newDescription.Trim(); 
                
                return SqlHelper.ExecuteNonQuery(CMSconnString, CommandType.StoredProcedure, "SP_CM_UpdateProductInfo", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "AddNewProduct_DAL", newIntID.ToString(), subcat.ToString(),
                                                               newCat.ToString(), newMfr.ToString(),newListPrice.ToString(), newShipClass.ToString(), I.ToString(),
                                                               newIsFeatureProd.ToString(),  newIsLive.ToString(),newDescription);
                
                throw new ApplicationException(strErrCode);
            }

        }//End of AddNewProduct_DAL method....   


        public DataSet GetManufacturer_DAL(int Active )
        {
            SqlParameter[] parameters = new SqlParameter[1];

            try
            {
                parameters[0] = new SqlParameter("@Active", SqlDbType.Int);
                parameters[0].Value = Active;
                return SqlHelper.ExecuteDataset(CMSconnString, CommandType.StoredProcedure, "SP_CMS_GetManufacturers", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetManufacturer_DAL");
                throw new ApplicationException(strErrCode);
            }
        }

        /*
         *Insert new Inventory.
         */
        public int InsertNewInventory_DAL(int newInvtID, int newSizeID, int newColorID,string newSKU,int newQty,
                                           int inwhsenum, int userid)
        {
            SqlParameter[] parameters = new SqlParameter[7];
            try
            {
                parameters[0] = new SqlParameter("@in_inv_template_id", SqlDbType.Int);
                if (newInvtID != 0)
                    parameters[0].Value = newInvtID;
                parameters[1] = new SqlParameter("@in_Size_ID", SqlDbType.Int);
                if (newSizeID != 0)
                    parameters[1].Value = newSizeID;
                parameters[2] = new SqlParameter("@in_Color_id", SqlDbType.Int);
                if (newColorID != 0)
                    parameters[2].Value = newColorID;
                parameters[3] = new SqlParameter("@in_SKUNumber", SqlDbType.NVarChar);
                if (newSKU.Trim() != string.Empty)
                    parameters[3].Value = newSKU.Trim();
                parameters[4] = new SqlParameter("@in_Qty", SqlDbType.Int);
                    parameters[4].Value = newQty;
                parameters[5] = new SqlParameter("@inwhsenum", SqlDbType.Int);
                parameters[5].Value = inwhsenum;
                parameters[6] = new SqlParameter("@userid", SqlDbType.Int);
                parameters[6].Value = userid;
                return SqlHelper.ExecuteNonQuery(CMSconnString, CommandType.StoredProcedure, "SP_CM_AddCM_inv_Template_ext2", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "InsertNewInventory_DAL", newInvtID.ToString(), newSizeID.ToString(),
                                                               newColorID.ToString(),newSKU,newQty.ToString(), inwhsenum.ToString(), userid.ToString());

                throw new ApplicationException(strErrCode);
            }

        }//End of InsertNewSize_DAL method....   



        /*
         *Save new image to DB.
         */
        public int SaveImage_DAL(string newImageName, int newColorID, byte[] newImage, int newInvTemplateID, int imageID, bool defaultImage)
        {
            SqlParameter[] parameters = new SqlParameter[6];
            try
            {
                parameters[0] = new SqlParameter("@upInvTemplateName", SqlDbType.NVarChar);
                if (newImageName.Trim() != string.Empty)
                    parameters[0].Value = newImageName.Trim();
                parameters[1] = new SqlParameter("@upColorID", SqlDbType.Int);
                if (newColorID != 0)
                    parameters[1].Value = newColorID;
                parameters[2] = new SqlParameter("@upImage", SqlDbType.Image);
                parameters[2].Value = newImage;
                parameters[3] = new SqlParameter("@upInvTemplateID", SqlDbType.Int);
                if (newInvTemplateID != 0)
                    parameters[3].Value = newInvTemplateID;
                parameters[4] = new SqlParameter("@inv_template_image_id", SqlDbType.Int);
                    parameters[4].Value = imageID;
                parameters[5] = new SqlParameter("@upDefaultImage", SqlDbType.Bit);
                parameters[5].Value = defaultImage;
                return SqlHelper.ExecuteNonQuery(CMSconnString, CommandType.StoredProcedure, "SP_CMS_SaveNewImage", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "SaveImage_DAL", newImageName.ToString(), newColorID.ToString(),
                                                                 newImage.ToString(), newInvTemplateID.ToString(), imageID.ToString(), defaultImage.ToString());
                throw new ApplicationException(strErrCode);
            }

        }//End of InsertNewSize_DAL method.... 

        /*
         * Get Image by Image ID
         */
        public DataSet GetImageByImage_DAL(int id)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            try
            {
                parameters[0] = new SqlParameter("@imageID", SqlDbType.Int);

                if (id != 0)
                    parameters[0].Value = id;

                return SqlHelper.ExecuteDataset(CMSconnString, CommandType.StoredProcedure, "SP_CMS_GetImagebyImageID", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetImageByImage_DAL", id.ToString());
                throw new ApplicationException(strErrCode);
            }
        }//End of GetSizeBySizeID_DAL method...
        
        public DataSet GetProductInventory(int productId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            try
            {
                parameters[0] = new SqlParameter("@productId", SqlDbType.Int);

                if (productId != 0)
                    parameters[0].Value = productId;

                return SqlHelper.ExecuteDataset(CMSconnString, CommandType.StoredProcedure, "SP_CMS_GetProductInventories", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetProductInventory", productId.ToString());
                throw new ApplicationException(strErrCode);
            }
        }

        public int DeleteProductImagebyImageID_DAL(int id)
        {
            SqlParameter[] parameters = new SqlParameter[1];

            try
            {
                parameters[0] = new SqlParameter("@del_template_image_id ", SqlDbType.BigInt);
                if (id != 0)
                    parameters[0].Value = id;

                return SqlHelper.ExecuteNonQuery(CMSconnString, CommandType.StoredProcedure, "SP_CMS_DeleteProductImagebyImageID", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "DeleteProductImagebyImageID_DAL", id.ToString());
                throw new ApplicationException(strErrCode);
            }
        }// End of DeleteProductImagebyImageID_DAL method. 


        public DataSet GetShippingClass_DAL(int Active)
        {
            SqlParameter[] parameters = new SqlParameter[1];

            try
            {
                parameters[0] = new SqlParameter("@Active", SqlDbType.Int);
                parameters[0].Value = Active;
                return SqlHelper.ExecuteDataset(CMSconnString, CommandType.StoredProcedure, "SP_CMS_GetShippingClass", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "GetShippingClass_DAL");
                throw new ApplicationException(strErrCode);
            }
        }

        public int RemoveProduct_DAL(int id)
        {
            SqlParameter[] parameters = new SqlParameter[2];

            try
            {
                parameters[0] = new SqlParameter("@in_inv_template_id ", SqlDbType.BigInt);
                if (id != 0)
                    parameters[0].Value = id;
                parameters[1] = new SqlParameter("@in_Status_Code ", SqlDbType.Int);
                parameters[1].Value = 1;

                return SqlHelper.ExecuteNonQuery(CMSconnString, CommandType.StoredProcedure, "SP_CMS_DeleteProducts", parameters);
            }
            catch (Exception ex)
            {
                string strErrCode = "~DAL-EXCEPTION~" + "," + (new Error_Log()).LogErrorIntoDB(ex, "RemoveProduct_DAL", id.ToString());
                throw new ApplicationException(strErrCode);
            }
        }// End of RemoveProduct_DAL method. 

    }
}

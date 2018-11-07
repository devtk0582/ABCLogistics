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


    public class OrderEntryDAL
    {
        string CMSconnString = Convert.ToString(ConfigurationManager.AppSettings["CMSConnectionString"]);
        string strGTEMasterConnectionString = ConfigurationManager.AppSettings["GTEMasterConnectionString"].ToString();
       
        public void  DBName (string strDBName) 
        {
            CMSconnString = CMSconnString.Replace("?", strDBName);
        }


      
        public DataSet GetPOGlobalOrderType_DAL(int iGlobalOrderType_Id, int iOT_OrderType_Id)
        {
            SqlParameter[] parameters = new SqlParameter[3];

            try
            {
                parameters[0] = new SqlParameter("@GlobalOrderTypeiD", SqlDbType.Int);
                parameters[0].Value = iGlobalOrderType_Id;
                parameters[1] = new SqlParameter("@in_OrderType_ID", SqlDbType.Int);
                parameters[1].Value = iOT_OrderType_Id;
                return SqlHelper.ExecuteDataset(CMSconnString, CommandType.StoredProcedure, "sp_WMSPO_GetGlobalOrderTypes", parameters);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public DataSet GetPOGlobalService_DAL(int iGlobalService_Id)
        {
            SqlParameter[] parameters = new SqlParameter[2];

            try
            {
                parameters[0] = new SqlParameter("@GlobalServiceTypeId", SqlDbType.Int);
                parameters[0].Value = iGlobalService_Id;

                return SqlHelper.ExecuteDataset(CMSconnString, CommandType.StoredProcedure, "sp_WMSPO_GetGlobalServicType", parameters);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



     /// <summary>
/// Function to place order
/// </summary>
/// <param name="strsessionId"></param>
/// <param name="iUserID"></param>
/// <param name="iServiceCode"></param>
/// <param name="iCustOrderType"></param>
/// <param name="iOrderType"></param>
/// <param name="strreference1"></param>
/// <param name="strreference2"></param>
/// <param name="strreference3"></param>
/// <param name="strsitecontactemail"></param>
/// <param name="strrequiredDelivery"></param>
/// <param name="streta"></param>
/// <param name="strNotes"></param>
/// <param name="bUrgent"></param>
/// <param name="iWhseNum"></param>
/// <param name="strplacedBy"></param>
/// <param name="iCarrier"></param>
/// <param name="strName"></param>
/// <param name="strAddr"></param>
/// <param name="strCity"></param>
/// <param name="strState"></param>
/// <param name="strCountry"></param>
/// <param name="strAddr2"></param>
/// <param name="strPostal_Code"></param>
/// <param name="strPhone1"></param>
/// <param name="strPhone2"></param>
/// <param name="strFax"></param>
/// <param name="strEmail"></param>
/// <param name="strPrimanycontact"></param>
/// <param name="iwhseNumTransfer"></param>
/// <param name="strCustomerPO"></param>
/// <param name="iaddress_id"></param>
/// <param name="strCustEmail"></param>
/// <param name="strCountfreq"></param>
/// <param name="iDay"></param>
/// <param name="strStartDate"></param>
/// <param name="strEndDate"></param>
/// <param name="strCountEmail"></param>
/// <param name="iWeek"></param>
/// <returns></returns>
/// <remarks></remarks>
public string PlaceOrder_DAL(string strsessionId, int iUserID,string streta, string strNotes,string strplacedBy, 
     string strName, string strAddr, string strCity, string strState,string strCountry, string strAddr2, string strPostal_Code, string strPhone1, 
    string strPhone2, string strFax, string strEmail, string strPrimanycontact 
    )
{
    DataSet ds;
    int iCustOrderType, iServiceCode, iWhseNum;
    iCustOrderType = 0;
    iServiceCode = 0;
    iWhseNum = 0;
	SqlParameter[] parameters = new SqlParameter[48];


	try {
		parameters[0] = new SqlParameter("@SessionID", SqlDbType.NVarChar, 50);
		parameters[1] = new SqlParameter("@UserID", SqlDbType.Int);
		parameters[2] = new SqlParameter("@ServiceCode", SqlDbType.Int);
		parameters[3] = new SqlParameter("@Cust_Order_Type", SqlDbType.Int);
		parameters[4] = new SqlParameter("@OrderType", SqlDbType.Int);
		parameters[5] = new SqlParameter("@reference1", SqlDbType.NVarChar, 30);
		parameters[6] = new SqlParameter("@reference2", SqlDbType.NVarChar, 30);
		parameters[7] = new SqlParameter("@reference3", SqlDbType.NVarChar, 30);
		parameters[8] = new SqlParameter("@site_contact_email", SqlDbType.NVarChar, 50);
		parameters[9] = new SqlParameter("@requiredDelivery", SqlDbType.SmallDateTime);
		parameters[10] = new SqlParameter("@eta", SqlDbType.SmallDateTime);
		parameters[11] = new SqlParameter("@Urgent", SqlDbType.Bit);
		parameters[12] = new SqlParameter("@Notes", SqlDbType.NVarChar, 1024);
		parameters[13] = new SqlParameter("@placedBy", SqlDbType.NVarChar, 25);
		parameters[14] = new SqlParameter("@WhseNum", SqlDbType.Int);
		parameters[15] = new SqlParameter("@Carrier", SqlDbType.Int);
		parameters[16] = new SqlParameter("@Name", SqlDbType.NVarChar, 30);
		parameters[17] = new SqlParameter("@Addr", SqlDbType.NVarChar, 200);
		parameters[18] = new SqlParameter("@Addr2", SqlDbType.NVarChar, 200);
		parameters[19] = new SqlParameter("@Postal_Code", SqlDbType.NVarChar, 20);
		parameters[20] = new SqlParameter("@City", SqlDbType.NVarChar, 50);
		parameters[21] = new SqlParameter("@State", SqlDbType.NVarChar, 50);
		parameters[22] = new SqlParameter("@Country", SqlDbType.NVarChar, 50);
		parameters[23] = new SqlParameter("@Phone1", SqlDbType.NVarChar, 15);
		parameters[24] = new SqlParameter("@Phone2", SqlDbType.NVarChar, 15);
		parameters[25] = new SqlParameter("@Fax", SqlDbType.NVarChar, 15);
		parameters[26] = new SqlParameter("@Email", SqlDbType.NVarChar, 30);
		parameters[27] = new SqlParameter("@Primany_contact", SqlDbType.NVarChar, 30);
		parameters[28] = new SqlParameter("@CustomerPO", SqlDbType.NVarChar, 30);
		parameters[29] = new SqlParameter("@CustEmail", SqlDbType.NVarChar, 30);
		parameters[30] = new SqlParameter("@address_id", SqlDbType.Int);
		parameters[31] = new SqlParameter("@whseNumTransfer", SqlDbType.Int);
	    parameters[32] = new SqlParameter("@Countfreq", SqlDbType.NVarChar, 1);
		parameters[33] = new SqlParameter("@Day", SqlDbType.Int);
		parameters[34] = new SqlParameter("@StartDate", SqlDbType.SmallDateTime);
		parameters[35] = new SqlParameter("@EndDate", SqlDbType.SmallDateTime);
		parameters[36] = new SqlParameter("@CountEmail", SqlDbType.NVarChar, 100);
		parameters[37] = new SqlParameter("@Week", SqlDbType.Int);
		parameters[38] = new SqlParameter("@placeMethod", SqlDbType.Int);
		parameters[39] = new SqlParameter("@POID", SqlDbType.Int);
		parameters[40] = new SqlParameter("@ContainerNum", SqlDbType.NVarChar, 30);
		parameters[41] = new SqlParameter("@SealNum", SqlDbType.NVarChar, 30);
		parameters[42] = new SqlParameter("@Carriers", SqlDbType.NVarChar, 30);
		parameters[43] = new SqlParameter("@Vessel", SqlDbType.NVarChar, 30);
		parameters[44] = new SqlParameter("@EstDepartureDt", SqlDbType.SmallDateTime);
		parameters[45] = new SqlParameter("@EstArrivalDt", SqlDbType.SmallDateTime);
		parameters[46] = new SqlParameter("@ActArrivalDt", SqlDbType.SmallDateTime);
		parameters[47] = new SqlParameter("@ActDepartureDt", SqlDbType.SmallDateTime);

		parameters[0].Value = strsessionId;
		parameters[1].Value = iUserID;

        ds = GetPOGlobalOrderType_DAL(1, 1);

        if( ds != null) 
        {
            if (ds.Tables.Count > 0)
            {
                iCustOrderType = Convert.ToInt32(ds.Tables[0].Rows[0]["dict_ordertype_id"]); 
            }
        }
	

        ds = GetPOGlobalService_DAL(1);

        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                iServiceCode = Convert.ToInt32(ds.Tables[0].Rows[0]["dict_servicetype_id"]);
            }
        }

        parameters[2].Value = iServiceCode;
		parameters[3].Value = iCustOrderType;
		parameters[4].Value = 1;
		parameters[5].Value = string.Empty;
        parameters[6].Value = string.Empty;
        parameters[7].Value = string.Empty;
        parameters[8].Value = string.Empty;

		
		parameters[9].Value = DBNull.Value;
		

		if (streta.Trim() == string.Empty) {
			parameters[10].Value = DBNull.Value;
		} else {
			parameters[10].Value = streta;
		}

		parameters[11].Value = 0;
		parameters[12].Value = strNotes;
		parameters[13].Value = strplacedBy;

        ds = (new AddCartsDAL()).GetDefaultCMSWarehouses();
        
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                iWhseNum = Convert.ToInt32(ds.Tables[0].Rows[0]["whse_id"]);
            }
        }
        
		parameters[14].Value = iWhseNum;
		parameters[15].Value = 0;
		parameters[16].Value = strName;
		parameters[17].Value = strAddr;
		parameters[18].Value = strAddr2;
		parameters[19].Value = strPostal_Code;
		parameters[20].Value = strCity;
		parameters[21].Value = strState;
		parameters[22].Value = strCountry;
		parameters[23].Value = strPhone1;
		parameters[24].Value = strPhone2;
		parameters[25].Value = strFax;
		parameters[26].Value = strEmail;
		parameters[27].Value = strPrimanycontact;
        parameters[28].Value = "Not Applicable";
        parameters[29].Value = string.Empty; 
		parameters[30].Value = 0;
		parameters[31].Value = 0;
        parameters[32].Value = string.Empty;
		parameters[33].Value = 0;

		parameters[34].Value = DBNull.Value;
	

		
		parameters[35].Value = DBNull.Value;


        parameters[36].Value = string.Empty;
		parameters[37].Value = 0;
		parameters[38].Value = 3;
		parameters[39].Value = 0;
        parameters[40].Value = string.Empty;
        parameters[41].Value = string.Empty;
        parameters[42].Value = string.Empty;
        parameters[43].Value = string.Empty;
		
		parameters[44].Value = DBNull.Value;
	
	
		parameters[45].Value = DBNull.Value;
	

		parameters[46].Value = DBNull.Value;
		

		parameters[47].Value = DBNull.Value;
	


        return SqlHelper.ExecuteScalar(CMSconnString, CommandType.StoredProcedure, "sp_WMS_PlaceOrder", parameters).ToString() ;

	} catch (Exception ex) {
		throw ex;
	}
}

public string AddGiftCartDetail_DAL(string strSessionId, string strShippingAdd, string strCheckOutComm, Int32 iBillingCodeId, Int32 iShipMethodId,
    string strETA, string strCheckOutGeeting, string strftcard, string strSignHeader, string strSignName, string strSignTitle, Int32 iPackagingId, Int32 iOrderId,
   string strEnclosureType)
{
    
    SqlParameter[] parameters = new SqlParameter[15];


    try
    {
        parameters[0] = new SqlParameter("@in_SessionId", SqlDbType.NVarChar,100);
        parameters[1] = new SqlParameter("@in_ShippingAdd", SqlDbType.NVarChar,500);
        parameters[2] = new SqlParameter("@in_CheckOutComm", SqlDbType.NVarChar,500);
        parameters[3] = new SqlParameter("@in_BillingCodeId", SqlDbType.BigInt);
        parameters[4] = new SqlParameter("@in_ShipMethodId", SqlDbType.BigInt);
        parameters[5] = new SqlParameter("@in_ETA", SqlDbType.DateTime);
        parameters[6] = new SqlParameter("@in_CheckOutGreeting", SqlDbType.NVarChar,50);
        parameters[7] = new SqlParameter("@in_ftcartText", SqlDbType.NVarChar,300);
        parameters[8] = new SqlParameter("@in_SignatureHeader", SqlDbType.NVarChar,20);
        parameters[9] = new SqlParameter("@in_SignatureName", SqlDbType.NVarChar,30);
        parameters[10] = new SqlParameter("@in_SignatureTitle", SqlDbType.NVarChar,30);
        parameters[11] = new SqlParameter("@in_PackagingId", SqlDbType.BigInt);
        parameters[12] = new SqlParameter("@in_OrderId", SqlDbType.Int);
        parameters[13] = new SqlParameter("@in_EnclosureType", SqlDbType.Char,2);
        parameters[14] = new SqlParameter("@in_Outmsg", SqlDbType.Int);
               

        parameters[0].Value = strSessionId;
        parameters[1].Value = strShippingAdd;
        parameters[2].Value = strCheckOutComm;
        parameters[3].Value = iBillingCodeId;
        parameters[4].Value = iShipMethodId;
        if (strETA.Trim() == string.Empty)
        {
            parameters[5].Value = DBNull.Value;
        }
        else
        {
            parameters[5].Value = strETA;
        }

        parameters[6].Value = strCheckOutGeeting;
        parameters[7].Value = strftcard;
        parameters[8].Value = strSignHeader;
        parameters[9].Value = strSignName;
        parameters[10].Value = strSignTitle;
        parameters[11].Value = iPackagingId;
        parameters[12].Value = iOrderId;
        parameters[13].Value = strEnclosureType;
        parameters[14].Direction = ParameterDirection.Output;


         SqlHelper.ExecuteScalar(CMSconnString, CommandType.StoredProcedure, "SP_CMS_InsertGiftCartDetail", parameters);
         return parameters[14].Value.ToString();
    }
    catch (Exception ex)
    {
        throw ex;
    }
}
 
 public DataSet GetGTEDatabasesSearch_DAL(int iGteDatabases_Id , string  strCustName , string strUserID , int bActive ) 
    {
     SqlParameter[] parameters = new SqlParameter[4];
           
            try 
	            {	        
		            parameters[0] = new SqlParameter("@in_GteDatabases_Id", SqlDbType.Int);
                    parameters[0].Value = iGteDatabases_Id;
                    parameters[1] = new SqlParameter("@in_CustName", SqlDbType.NVarChar);
                    parameters[1].Value = strCustName;
                    parameters[2] = new SqlParameter("@in_UserID", SqlDbType.Int);
                    parameters[2].Value = Convert.ToInt32 (strUserID);
                    parameters[3] = new SqlParameter("@in_Active", SqlDbType.Int);
                    parameters[3].Value = bActive;
                    return SqlHelper.ExecuteDataset(strGTEMasterConnectionString, CommandType.StoredProcedure, "Gte_GetGTEDatabasesSearch", parameters);
	            }
	            catch (Exception)
	            {
		
		            throw;
	            }


     
           
      }
 public DataSet GetOrderItems_DAL(int OrderId, int iUserID)
 {
     SqlParameter[] parameters = new SqlParameter[3];

     try
     {
         parameters[0] = new SqlParameter("@OrderId", SqlDbType.Int);
         parameters[0].Value = OrderId;
         parameters[1] = new SqlParameter("@UserId", SqlDbType.Int);
         parameters[1].Value = Convert.ToInt32(iUserID);

         return SqlHelper.ExecuteDataset(CMSconnString, CommandType.StoredProcedure, "SP_CMS_GetOrderItems", parameters);
     }
     catch (Exception)
     {

         throw;
     }




 }

        public DataSet GetOrderInfo_DAL(int OrderId,  int iUserID)
 {
     SqlParameter[] parameters = new SqlParameter[3];

     try
     {
         parameters[0] = new SqlParameter("@OrderId", SqlDbType.Int);
         parameters[0].Value = OrderId;
         parameters[1] = new SqlParameter("@UserId", SqlDbType.Int);
         parameters[1].Value = Convert.ToInt32(iUserID);

         return SqlHelper.ExecuteDataset(CMSconnString, CommandType.StoredProcedure, "SP_CMS_GetOrderInfo", parameters);
     }
     catch (Exception)
     {

         throw;
     }




 }

        public DataSet GetOrderNotes_DAL(int iorderid, int iiNumofRec)
        {
            SqlParameter[] parameters = new SqlParameter[3];

            try
            {
                parameters[0] = new SqlParameter("@OrderId", SqlDbType.Int);
                parameters[0].Value = iorderid;
                parameters[1] = new SqlParameter("@NumOfRecord", SqlDbType.Int);
                parameters[1].Value = iiNumofRec;
                return SqlHelper.ExecuteDataset(CMSconnString, CommandType.StoredProcedure, "sp_WMS_OrderNotes", parameters);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int   InsertNotes_DAL(int  iUserId,int iOderNum,int DUAid,string strnotes,int iOrderType)
        {
            SqlParameter[] parameters = new SqlParameter[5];

            try
            {
                parameters[0] = new SqlParameter("@User_id", SqlDbType.Int);
                parameters[0].Value = iUserId;
                parameters[1] = new SqlParameter("@OrderNum", SqlDbType.Int);
                parameters[1].Value = iOderNum;
                parameters[2] = new SqlParameter("@D_UserActionId", SqlDbType.Int);
                parameters[2].Value = DUAid;
                parameters[3] = new SqlParameter("@Notes", SqlDbType.NVarChar);
                parameters[3].Value = strnotes;
                parameters[4] = new SqlParameter("@OrderTye", SqlDbType.Int);
                parameters[4].Value = iOrderType;
               
                return SqlHelper.ExecuteNonQuery(CMSconnString, CommandType.StoredProcedure, "sp_WMS_AddNotes", parameters);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertShippNotes_DAL(int iUserId, int iOderNum, int DUAid, string strnotes, int iShipMethodId,string strTracking)
        {
            SqlParameter[] parameters = new SqlParameter[6];
            
            try
            {
                parameters[0] = new SqlParameter("@User_id", SqlDbType.Int);
                parameters[0].Value = iUserId;
                parameters[1] = new SqlParameter("@OrderNum", SqlDbType.Int);
                parameters[1].Value = iOderNum;
                parameters[2] = new SqlParameter("@D_UserActionId", SqlDbType.Int);
                parameters[2].Value = DUAid;
                parameters[3] = new SqlParameter("@Notes", SqlDbType.NVarChar);
                parameters[3].Value = strnotes;
                parameters[4] = new SqlParameter("@ShipMethodid", SqlDbType.Int);
                parameters[4].Value = iShipMethodId;
                parameters[5] = new SqlParameter("@Tracking", SqlDbType.NVarChar);
                parameters[5].Value = strTracking;
                return SqlHelper.ExecuteNonQuery(CMSconnString, CommandType.StoredProcedure, "sp_CMS_ShipConfirm", parameters);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string InsertNotesPOD_DAL(int iUserId,int iOrderType,int iOrdernum,int iD_UserActionId,string strNote,string strDADate,string  strDATime,string strDDDate,string strDDTime,string strwaittime,string strSignedby, string strTollCharges , string strTotalWait , string strADDriven , string strTUValue , string strInboundpart )
        {
            SqlParameter[] parameters = new SqlParameter[16];
            SqlCommand command;
            string strPod = string.Empty;
            try
            {
                parameters[0] = new SqlParameter("@User_id", SqlDbType.Int);
                parameters[0].Value = iUserId;

                parameters[1] = new SqlParameter("@OrderType", SqlDbType.Int);
                parameters[1].Value = iOrderType;

                parameters[2] = new SqlParameter("@OrderNum", SqlDbType.Int);
                parameters[2].Value = iOrdernum;

                parameters[3] = new SqlParameter("@D_UserActionId", SqlDbType.Int);
                parameters[3].Value = iD_UserActionId;

                parameters[4] = new SqlParameter("@Notes", SqlDbType.NVarChar, 1000);
                parameters[4].Value = strNote;

                parameters[5] = new SqlParameter("@DArrivaldate", SqlDbType.SmallDateTime);
                if (strDADate.Trim() == String.Empty)
	            {
		            parameters[5].Value = DBNull.Value;
	            }
                else
                {
                    parameters[5].Value = strDADate;
                }

                parameters[6] = new SqlParameter("@DArrivalTime", SqlDbType.SmallDateTime);
                if ( strDATime.Trim() == String.Empty)
	            {
		            parameters[6].Value = DBNull.Value;
	            }
                else
                {
                    parameters[6].Value = strDATime;
                }

                parameters[7] = new SqlParameter("@DDeparturedate", SqlDbType.SmallDateTime);
                if (strDDDate.Trim() == String.Empty)
	            {
		             parameters[7].Value = DBNull.Value;
	            }
                else
                {
                    parameters[7].Value = strDDDate;
                }

                parameters[8] = new SqlParameter("@DDepartureTime", SqlDbType.SmallDateTime);
                 if (strDDTime.Trim() == String.Empty) 
                    parameters[8].Value = DBNull.Value;
                else
                    parameters[8].Value = strDDTime;
               

                parameters[9] = new SqlParameter("@WaitTime", SqlDbType.Decimal);
                 if (strwaittime.Trim() == String.Empty) 
                    parameters[9].Value = DBNull.Value;
                else
                    parameters[9].Value = strwaittime;
               

                parameters[10] = new SqlParameter("@Signedby", SqlDbType.NVarChar, 50);
                 if (strSignedby.Trim() == String.Empty) 
                    parameters[10].Value = DBNull.Value;
                 else
                    parameters[10].Value = strSignedby;
               

                parameters[11] = new SqlParameter("@TollCharges", SqlDbType.Decimal);
                if (strTollCharges.Trim() == String.Empty) 
                    parameters[11].Value = DBNull.Value;
                else
                    parameters[11].Value = strTollCharges;
               


                parameters[12] = new SqlParameter("@TotalWeight", SqlDbType.Decimal);
                if (strTotalWait.Trim() == String.Empty)
                    parameters[12].Value = DBNull.Value;
                else
                    parameters[12].Value = strTotalWait;
                

                parameters[13] = new SqlParameter("@ActualDistanceDriven", SqlDbType.Decimal);
                if (strADDriven.Trim() == String.Empty)
                    parameters[13].Value = DBNull.Value;
                else
                    parameters[13].Value = strADDriven;
                

                parameters[14] = new SqlParameter("@TotalUnitValue", SqlDbType.Decimal);
                 if (strTUValue.Trim() == String.Empty)
                    parameters[14].Value = DBNull.Value;
                else
                    parameters[14].Value = strTUValue;
                
                
                parameters[15] = new SqlParameter("@InboundXML", SqlDbType.NText);
                parameters[15].Value = strInboundpart;

               using (SqlConnection connection = new SqlConnection(CMSconnString)) 
               {
	           connection.Open();

	           command = new SqlCommand("sp_WMS_AddPOD", connection);

	          command.CommandType = CommandType.StoredProcedure;

	            if (((parameters != null)))
                {
		            //SqlParameter p = default(SqlParameter);
                    foreach (SqlParameter p in parameters) 
                    {
			            if (((p != null))) {
				            // Check for derived output value with no value assigned
				            if ((p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Input) && p.Value == null)
                            {
					            p.Value = DBNull.Value;
				            }
				            command.Parameters.Add(p);
			            }
		            }
	            }
	            command.CommandTimeout = 6000;

	            strPod =Convert.ToString( command.ExecuteScalar());
	            connection.Close();

                }

                return strPod;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string UploadShippingLabel_DAL(string docDesc, byte[] docBody, string docContentType, string docFileName,
            string docContent, int docLength, int orderId, int userId)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[9];
                parameters[0] = new SqlParameter("@doc_type", SqlDbType.TinyInt);
                parameters[0].Value = 1;
                parameters[1] = new SqlParameter("@doc_desc", SqlDbType.VarChar);
                parameters[1].Value = docDesc;
                parameters[2] = new SqlParameter("@doc_body", SqlDbType.VarBinary);
                parameters[2].Value = docBody;
                parameters[3] = new SqlParameter("@doc_content_type", SqlDbType.VarChar);
                parameters[3].Value = docContentType;
                parameters[4] = new SqlParameter("@doc_file_name", SqlDbType.VarChar);
                parameters[4].Value = docFileName;
                parameters[5] = new SqlParameter("@doc_content", SqlDbType.VarChar);
                parameters[5].Value = docContent;
                parameters[6] = new SqlParameter("@doc_length", SqlDbType.Int);
                parameters[6].Value = docLength;
                parameters[7] = new SqlParameter("@order_id", SqlDbType.Int);
                parameters[7].Value = orderId;
                parameters[8] = new SqlParameter("@user_id", SqlDbType.Int);
                parameters[8].Value = userId;

                SqlHelper.ExecuteNonQuery(CMSconnString, CommandType.StoredProcedure, "SP_CMS_UploadShippingLabel", parameters);

                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetGlobalWarehouseLabelAddress_DAL(int orderId)
        {
            SqlParameter[] parameters = new SqlParameter[1];

            try
            {
                parameters[0] = new SqlParameter("@order_id", SqlDbType.Int);
                if (orderId != 0)
                    parameters[0].Value = orderId;
                else
                    parameters[0].Value = DBNull.Value;

                return SqlHelper.ExecuteDataset(CMSconnString, CommandType.StoredProcedure, "SP_CMS_GetGlobalWarehouseLabelAddress", parameters);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertOrderShipment_DAL(int InOrderNum, string InFromName, string InFromAttentionName, string InFromStree1, string InFromStreet2,
                   int InFromCity, int InFromState, int InFromCountry, string InFromZip, string InFromPhone, string InToName, string InToAttentionName,
                   string InToStreet1, string InToStreet2, int InToCity, int InToState, int InToCountry, string InToZip, string InToPhone, int InShipService,
                   string InAddressValidation, string InChargeType, string InUnitType, string InPackagingType)
        {
            SqlParameter[] parameters = new SqlParameter[24];

            try
            {
                parameters[0] = new SqlParameter("@InOrderNum", SqlDbType.Int);
                parameters[0].Value = InOrderNum;
                parameters[1] = new SqlParameter("@InFromName", SqlDbType.VarChar);
                parameters[1].Value = InFromName;
                parameters[2] = new SqlParameter("@InFromAttentionName", SqlDbType.VarChar);
                parameters[2].Value = InFromAttentionName;
                parameters[3] = new SqlParameter("@InFromStree1", SqlDbType.VarChar);
                parameters[3].Value = InFromStree1;
                parameters[4] = new SqlParameter("@InFromStreet2", SqlDbType.VarChar);
                parameters[4].Value = InFromStreet2;
                parameters[5] = new SqlParameter("@InFromCity", SqlDbType.Int);
                parameters[5].Value = InFromCity;
                parameters[6] = new SqlParameter("@InFromState", SqlDbType.Int);
                parameters[6].Value = InFromState;
                parameters[7] = new SqlParameter("@InFromCountry", SqlDbType.Int);
                parameters[7].Value = InFromCountry;
                parameters[8] = new SqlParameter("@InFromZip", SqlDbType.VarChar);
                parameters[8].Value = InFromZip;
                parameters[9] = new SqlParameter("@InFromPhone", SqlDbType.VarChar);
                parameters[9].Value = InFromPhone;
                parameters[10] = new SqlParameter("@InToName", SqlDbType.VarChar);
                parameters[10].Value = InToName;
                parameters[11] = new SqlParameter("@InToAttentionName", SqlDbType.VarChar);
                parameters[11].Value = InToAttentionName;
                parameters[12] = new SqlParameter("@InToStreet1", SqlDbType.VarChar);
                parameters[12].Value = InToStreet1;
                parameters[13] = new SqlParameter("@InToStreet2", SqlDbType.VarChar);
                parameters[13].Value = InToStreet2;
                parameters[14] = new SqlParameter("@InToCity", SqlDbType.Int);
                parameters[14].Value = InToCity;
                parameters[15] = new SqlParameter("@InToState", SqlDbType.Int);
                parameters[15].Value = InToState;
                parameters[16] = new SqlParameter("@InToCountry", SqlDbType.Int);
                parameters[16].Value = InToCountry;
                parameters[17] = new SqlParameter("@InToZip", SqlDbType.VarChar);
                parameters[17].Value = InToZip;
                parameters[18] = new SqlParameter("@InToPhone", SqlDbType.VarChar);
                parameters[18].Value = InToPhone;
                parameters[19] = new SqlParameter("@InShipService", SqlDbType.Int);
                parameters[19].Value = InShipService;
                parameters[20] = new SqlParameter("@InAddressValidation", SqlDbType.VarChar);
                parameters[20].Value = InAddressValidation;
                parameters[21] = new SqlParameter("@InChargeType", SqlDbType.VarChar);
                parameters[21].Value = InChargeType;
                parameters[22] = new SqlParameter("@InUnitType", SqlDbType.VarChar);
                parameters[22].Value = InUnitType;
                parameters[23] = new SqlParameter("@InPackagingType", SqlDbType.VarChar);
                parameters[23].Value = InPackagingType;

                return SqlHelper.ExecuteNonQuery(CMSconnString, CommandType.StoredProcedure, "SP_CMS_InsertOrderShipment", parameters);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetOrderShipmentInformation(int orderId)
        {
            SqlParameter[] parameters = new SqlParameter[1];

            try
            {
                parameters[0] = new SqlParameter("@InOrderNum", SqlDbType.Int);
                if (orderId != 0)
                    parameters[0].Value = orderId;
                else
                    parameters[0].Value = DBNull.Value;

                return SqlHelper.ExecuteDataset(CMSconnString, CommandType.StoredProcedure, "SP_CMS_GetOrderShipmentByOrderNum", parameters);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}


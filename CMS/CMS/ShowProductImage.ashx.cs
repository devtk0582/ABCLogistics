using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using ABCLogistics.DataAccess;
using System.Data.SqlClient;

namespace CMS
{
    /// <summary>
    /// Summary description for ShowProductImage
    /// </summary>
    public class ShowProductImage : IHttpHandler
    {

        string strConn = ConfigurationManager.AppSettings["CMSConnectionString"].ToString();
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                if (context.Request.QueryString["id"] != null && context.Request.QueryString["id"].ToString() != "")
                {
                    string imageId = context.Request.QueryString["id"].ToString();
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@image_id", SqlDbType.Int);
                    parameters[0].Value = int.Parse(imageId);
                    byte[] image = (byte[])SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "SP_CMS_GetProductImage", parameters);

                    if (image != null && image.Length > 0)
                    {
                        context.Response.BinaryWrite(image);
                    }
                    else
                    {
                        context.Response.Write("");
                    }
                    context.Response.End();
                }
                else
                {
                    context.Response.WriteFile("~/Images/no-image.png");
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("");
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
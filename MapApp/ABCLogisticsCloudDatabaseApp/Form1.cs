using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ABCLogisticsCloudDatabaseApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ABCLogisticsGlobalGeoWebServiceReference.GeoWebService service = new ABCLogisticsGlobalGeoWebServiceReference.GeoWebService();
            using(FFMCloudDataClassesDataContext context = new FFMCloudDataClassesDataContext())
            {
                var providers = (from o in context.FFM_Providers
                                 where o.Latitude == null
                                 select o).ToList();

                int ProviderCount = providers.Count, SuccessCount = 0;
                
                var globalContext = new ABCLogisticsGlobalDataClassesDataContext();

                
                foreach (FFM_Provider provider in providers)
                {
                    string zipcode = (from o in globalContext.Accounts
                                      where o.AccountID == provider.AccountID.Value
                                      select o.Postal).SingleOrDefault();

                    if(!string.IsNullOrEmpty(zipcode))
                    {
                        ABCLogisticsGlobalGeoWebServiceReference.GeoLocation loc = service.GeocodeZipCode(zipcode);

                        if (loc != null)
                        {
                            provider.Latitude = loc.Latitude;
                            provider.Longitude = loc.Longitude;
                            context.SubmitChanges();
                            SuccessCount++;
                        }
                    }
                    
                }
                textBox.Text = ProviderCount.ToString() + " " + SuccessCount.ToString();
            }

            
            
        }
    }

}

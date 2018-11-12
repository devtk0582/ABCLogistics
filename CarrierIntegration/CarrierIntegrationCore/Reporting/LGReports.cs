using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace CarrierIntegrationCore.Reporting
{
    public class LGReports
    {
        public void GenerateReport(List<LGConsignee> consigneeList, List<LGProvider> providers)
        {
            string fileName = "RFQ_" + DateTime.Now.ToString("MMddyyyyHHmmss") + ".xlsx";
            FileInfo outputFile = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files/" + fileName));
            using (ExcelPackage excelPackage = new ExcelPackage(outputFile))
            {
                var workSheet = excelPackage.Workbook.Worksheets.Add("Report");
                workSheet.View.ShowGridLines = true;
                workSheet.Column(1).Width = 15;
                workSheet.Column(2).Width = 20;
                workSheet.Column(3).Width = 15;
                workSheet.Column(4).Width = 15;
                workSheet.Column(5).Width = 20;
                workSheet.Column(6).Width = 20;
                workSheet.Column(7).Width = 20;
                workSheet.Column(8).Width = 15;
                workSheet.Column(9).Width = 15;
                workSheet.Column(10).Width = 15;
                workSheet.Column(11).Width = 25;
                workSheet.Column(12).Width = 15;
                workSheet.Column(13).Width = 15;

                int rowIndex = 1;

                workSheet.Cells[rowIndex, 1].Value = "LOAD_ID";
                workSheet.Cells[rowIndex, 2].Value = "Consignee City";
                workSheet.Cells[rowIndex, 3].Value = "Consignee State";
                workSheet.Cells[rowIndex, 4].Value = "Consignee Zip";
                workSheet.Cells[rowIndex, 5].Value = "SP Name";
                workSheet.Cells[rowIndex, 6].Value = "SP Address 1";
                workSheet.Cells[rowIndex, 7].Value = "SP Address 2";
                workSheet.Cells[rowIndex, 8].Value = "SP City";
                workSheet.Cells[rowIndex, 9].Value = "SP State";
                workSheet.Cells[rowIndex, 10].Value = "SP Zip";
                workSheet.Cells[rowIndex, 11].Value = "Distance - SP to Consignee";
                workSheet.Cells[rowIndex, 12].Value = "Signature";
                workSheet.Cells[rowIndex, 13].Value = "Signature Plus";
                workSheet.Row(rowIndex).Style.Font.Bold = true;
                workSheet.Row(rowIndex).Style.Font.Size = 10;
                workSheet.Row(rowIndex).Style.Font.Name = "Verdana";
                workSheet.Row(rowIndex).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                workSheet.Row(rowIndex).Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                workSheet.Row(rowIndex).Style.WrapText = true;
                rowIndex++;

                foreach (LGConsignee consignee in consigneeList)
                {
                    var providerInfo = providers.Where(p => p.Zip == consignee.ConsigneeZip).ToList();

                    if (providerInfo != null && providerInfo.Count > 0)
                    {
                        for (int count = 0; count < providerInfo.Count; count++)
                        {
                            if (count == 0)
                            {
                                workSheet.Cells[rowIndex, 1].Value = consignee.LOAD_ID;
                                workSheet.Cells[rowIndex, 2].Value = consignee.ConsigneeCity;
                                workSheet.Cells[rowIndex, 3].Value = consignee.ConsigneeState;
                                workSheet.Cells[rowIndex, 4].Value = consignee.ConsigneeZip;
                            }
                            else
                            {
                                workSheet.Cells[rowIndex, 1].Value = "";
                                workSheet.Cells[rowIndex, 2].Value = "";
                                workSheet.Cells[rowIndex, 3].Value = "";
                                workSheet.Cells[rowIndex, 4].Value = "";
                            }

                            workSheet.Cells[rowIndex, 5].Value = providerInfo[count].Name;
                            workSheet.Cells[rowIndex, 6].Value = providerInfo[count].Address1;
                            workSheet.Cells[rowIndex, 7].Value = providerInfo[count].Address2;
                            workSheet.Cells[rowIndex, 8].Value = providerInfo[count].City;
                            workSheet.Cells[rowIndex, 9].Value = providerInfo[count].State;
                            workSheet.Cells[rowIndex, 10].Value = providerInfo[count].Zip;
                            workSheet.Cells[rowIndex, 11].Value = GetDistance(consignee.ConsigneeZip, providerInfo[count].Zip).ToString();
                            workSheet.Cells[rowIndex, 12].Value = providerInfo[count].SignatureRate.ToString();
                            workSheet.Cells[rowIndex, 13].Value = providerInfo[count].SignaturePlusRate.ToString();

                            rowIndex++;
                        }
                    }
                    else
                    {
                        workSheet.Cells[rowIndex, 1].Value = consignee.LOAD_ID;
                        workSheet.Cells[rowIndex, 2].Value = consignee.ConsigneeCity;
                        workSheet.Cells[rowIndex, 3].Value = consignee.ConsigneeState;
                        workSheet.Cells[rowIndex, 4].Value = consignee.ConsigneeZip;
                        workSheet.Cells[rowIndex, 5].Value = "";
                        workSheet.Cells[rowIndex, 6].Value = "";
                        workSheet.Cells[rowIndex, 7].Value = "";
                        workSheet.Cells[rowIndex, 8].Value = "";
                        workSheet.Cells[rowIndex, 9].Value = "";
                        workSheet.Cells[rowIndex, 10].Value = "";
                        workSheet.Cells[rowIndex, 11].Value = "";
                        workSheet.Cells[rowIndex, 12].Value = "";
                        workSheet.Cells[rowIndex, 13].Value = "";
                        rowIndex++;
                    }
                }

                excelPackage.Workbook.Properties.Author = "ABC Logistics";
                excelPackage.Save();
            }
        }

        private decimal GetDistance(string originZip, string destZip)
        {
            decimal distance = 0;
            string requestUrl = @"http://zipcodedistanceapi.redline13.com/rest/key/distance.xml/" + originZip.Trim() + "/" + destZip.Trim() + "/mile";

            System.Net.HttpWebRequest request;
            Uri uri = new Uri(requestUrl);
            request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(uri);
            XDocument xmlDoc;
            System.Net.HttpWebResponse response = null;
            try
            {
                response = (System.Net.HttpWebResponse)request.GetResponse();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    XmlReader responseStream = XmlReader.Create(request.GetResponse().GetResponseStream());
                    xmlDoc = XDocument.Load(responseStream);
                    if (responseStream != null)
                        responseStream.Close();
                    if (xmlDoc.Root.Element("distance") != null)
                        distance = decimal.Parse(xmlDoc.Root.Element("distance").Value);
                    else
                    {
                        MessageBox.Show(xmlDoc.Root.Element("error_code").Value + ": " + xmlDoc.Root.Element("error_msg").Value);
                    }
                }
                else
                {
                    MessageBox.Show("No Response");
                }
            }
            catch (Exception ex)
            {
                if (response != null)
                {
                    response.Close();
                }
                response = null;
                request = null;
                distance = -1;
            }

            return distance;
        }
    }
}

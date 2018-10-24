using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ABCLogisticsUtilityToolBusinessLogic;

namespace ABCLogisticsUtilityTools
{
    public partial class frmMain : Form
    {
        public int CurrentShipmentID { get; set; }

        public frmMain()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCustomerNumber.Text.Trim() != string.Empty && txtHAWBNumbers.Text.Trim() != string.Empty)
                {
                    List<string> successHAWB = (new BOActivity()).RemoveCustomerReportShipments(txtCustomerNumber.Text.Trim(), txtHAWBNumbers.Text.Trim());
                    if (successHAWB.Count > 0)
                    {
                        lblResult.Text = "Following HAWB numbers are removed from ABCLogistics Online Report successfully: ";
                        for (int i = 0; i < successHAWB.Count; i++)
                        {
                            if (i == successHAWB.Count - 1)
                                lblResult.Text += successHAWB[i];
                            else
                                lblResult.Text += successHAWB[i] + ", ";
                        }
                    }
                    else
                    {
                        lblResult.Text = "Error processing request. Please see logs for more details";
                    }
                }
                else
                {
                    lblResult.Text = "Please enter HAWB numbers";
                }
            }
            catch (Exception ex)
            {
                lblResult.Text = ex.Message;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //Initialize Remove FFM Shipment Panel

            try
            {
                Dictionary<string, int> ffmTypes = new Dictionary<string, int>();
                ffmTypes.Add("First Miles Only", 1);
                ffmTypes.Add("Final Miles Only", 2);
                cbRFSType.DataSource = new BindingSource(ffmTypes, null);
                cbRFSType.DisplayMember = "Key";
                cbRFSType.ValueMember = "Value";
                cbRFSType.SelectedIndex = 0;
                CurrentShipmentID = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnRFSSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lblRFSStatus.Text = "";
                lblRFSContract.Text = "";
                lblRFSTerminal.Text = "";
                CurrentShipmentID = 0;

                if (txtRFSHAWB.Text.Trim() != "")
                {
                    var shipment = (new BOABCLogisticsGlobalShipment()).SearchFFMShipment(txtRFSHAWB.Text.Trim(), Convert.ToInt32(cbRFSType.SelectedValue));
                    if (shipment != null)
                    {
                        txtRFSInfo.Text += "Shipment is found for HAWB " + txtRFSHAWB.Text.Trim() + Environment.NewLine;
                        lblRFSContract.Text = shipment.ContractID.HasValue ? "Created" : "Not Created";
                        lblRFSStatus.Text = shipment.StatusDescription ?? "None";
                        lblRFSTerminal.Text = shipment.Terminal ?? "None";
                        CurrentShipmentID = shipment.ShipmentID;
                    }
                    else
                    {
                        MessageBox.Show("No shipment is found for this HAWB");
                    }
                }
                else
                {
                    MessageBox.Show("HAWB number is required");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnRFSRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRFSHAWB.Text.Trim() != "")
                {
                    if (MessageBox.Show("Are you sure to remove all the records related to this HAWB number " + txtRFSHAWB.Text.Trim() + "? This operation is not reversible and will be effective immediately.", "Warning for removing shipment", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                    {
                        int result = (new BOABCLogisticsGlobalShipment()).RemoveFFMShipment(CurrentShipmentID, Convert.ToInt32(cbRFSType.SelectedValue));
                        if (result > 0)
                        {
                            string successMsg = "HAWB " + txtRFSHAWB.Text.Trim() + " has been removed from ABCLogistics Global and FFM system. Please review.";
                            MessageBox.Show(successMsg);
                            txtRFSInfo.Text += successMsg + Environment.NewLine;
                            txtRFSHAWB.Text = "";
                            cbRFSType.SelectedIndex = 0;
                            lblRFSContract.Text = "";
                            lblRFSStatus.Text = "";
                            lblRFSTerminal.Text = "";
                        }
                        else
                        {
                            string failMsg = "Failed to remove the shipment with HAWB number " + txtRFSHAWB.Text.Trim();
                            MessageBox.Show(failMsg);
                            txtRFSInfo.Text += failMsg + Environment.NewLine;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


    }
}

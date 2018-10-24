namespace ABCLogisticsUtilityTools
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpRemoveReportShipmentPS = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCustomerNumber = new System.Windows.Forms.TextBox();
            this.lblResult = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblPSRMessage = new System.Windows.Forms.Label();
            this.txtHAWBNumbers = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.tpRemoveFFMShipment = new System.Windows.Forms.TabPage();
            this.cbRFSType = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lblRFSContract = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblRFSTerminal = new System.Windows.Forms.Label();
            this.lblRFSStatus = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtRFSInfo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnRFSSearch = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtRFSHAWB = new System.Windows.Forms.TextBox();
            this.btnRFSRemove = new System.Windows.Forms.Button();
            this.tcMain.SuspendLayout();
            this.tpRemoveReportShipmentPS.SuspendLayout();
            this.tpRemoveFFMShipment.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcMain
            // 
            this.tcMain.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tcMain.Controls.Add(this.tpRemoveReportShipmentPS);
            this.tcMain.Controls.Add(this.tpRemoveFFMShipment);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Margin = new System.Windows.Forms.Padding(4);
            this.tcMain.Multiline = true;
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(976, 400);
            this.tcMain.TabIndex = 0;
            // 
            // tpRemoveReportShipmentPS
            // 
            this.tpRemoveReportShipmentPS.BackColor = System.Drawing.SystemColors.Info;
            this.tpRemoveReportShipmentPS.Controls.Add(this.label3);
            this.tpRemoveReportShipmentPS.Controls.Add(this.label2);
            this.tpRemoveReportShipmentPS.Controls.Add(this.txtCustomerNumber);
            this.tpRemoveReportShipmentPS.Controls.Add(this.lblResult);
            this.tpRemoveReportShipmentPS.Controls.Add(this.label1);
            this.tpRemoveReportShipmentPS.Controls.Add(this.lblPSRMessage);
            this.tpRemoveReportShipmentPS.Controls.Add(this.txtHAWBNumbers);
            this.tpRemoveReportShipmentPS.Controls.Add(this.btnSubmit);
            this.tpRemoveReportShipmentPS.Location = new System.Drawing.Point(4, 4);
            this.tpRemoveReportShipmentPS.Margin = new System.Windows.Forms.Padding(4);
            this.tpRemoveReportShipmentPS.Name = "tpRemoveReportShipmentPS";
            this.tpRemoveReportShipmentPS.Padding = new System.Windows.Forms.Padding(4);
            this.tpRemoveReportShipmentPS.Size = new System.Drawing.Size(968, 367);
            this.tpRemoveReportShipmentPS.TabIndex = 0;
            this.tpRemoveReportShipmentPS.Text = "Pure Storage Report";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(82, 197);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "HAWB:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(82, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Customer Number:";
            // 
            // txtCustomerNumber
            // 
            this.txtCustomerNumber.Location = new System.Drawing.Point(249, 150);
            this.txtCustomerNumber.Margin = new System.Windows.Forms.Padding(4);
            this.txtCustomerNumber.Name = "txtCustomerNumber";
            this.txtCustomerNumber.Size = new System.Drawing.Size(150, 26);
            this.txtCustomerNumber.TabIndex = 5;
            // 
            // lblResult
            // 
            this.lblResult.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lblResult.Location = new System.Drawing.Point(91, 252);
            this.lblResult.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(672, 96);
            this.lblResult.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(80, 36);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(380, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Remove Shipment From Online Report";
            // 
            // lblPSRMessage
            // 
            this.lblPSRMessage.AutoSize = true;
            this.lblPSRMessage.Location = new System.Drawing.Point(82, 115);
            this.lblPSRMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPSRMessage.Name = "lblPSRMessage";
            this.lblPSRMessage.Size = new System.Drawing.Size(878, 20);
            this.lblPSRMessage.TabIndex = 2;
            this.lblPSRMessage.Text = "Please enter the customer number and the HAWB numbers separated by semicolon (;) " +
    "below and click on \"Submit\". ";
            // 
            // txtHAWBNumbers
            // 
            this.txtHAWBNumbers.Location = new System.Drawing.Point(249, 197);
            this.txtHAWBNumbers.Margin = new System.Windows.Forms.Padding(4);
            this.txtHAWBNumbers.Name = "txtHAWBNumbers";
            this.txtHAWBNumbers.Size = new System.Drawing.Size(514, 26);
            this.txtHAWBNumbers.TabIndex = 1;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(811, 311);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(4);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(145, 37);
            this.btnSubmit.TabIndex = 0;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // tpRemoveFFMShipment
            // 
            this.tpRemoveFFMShipment.BackColor = System.Drawing.SystemColors.Info;
            this.tpRemoveFFMShipment.Controls.Add(this.cbRFSType);
            this.tpRemoveFFMShipment.Controls.Add(this.label9);
            this.tpRemoveFFMShipment.Controls.Add(this.lblRFSContract);
            this.tpRemoveFFMShipment.Controls.Add(this.label10);
            this.tpRemoveFFMShipment.Controls.Add(this.lblRFSTerminal);
            this.tpRemoveFFMShipment.Controls.Add(this.lblRFSStatus);
            this.tpRemoveFFMShipment.Controls.Add(this.label6);
            this.tpRemoveFFMShipment.Controls.Add(this.txtRFSInfo);
            this.tpRemoveFFMShipment.Controls.Add(this.label5);
            this.tpRemoveFFMShipment.Controls.Add(this.btnRFSSearch);
            this.tpRemoveFFMShipment.Controls.Add(this.label4);
            this.tpRemoveFFMShipment.Controls.Add(this.label7);
            this.tpRemoveFFMShipment.Controls.Add(this.label8);
            this.tpRemoveFFMShipment.Controls.Add(this.txtRFSHAWB);
            this.tpRemoveFFMShipment.Controls.Add(this.btnRFSRemove);
            this.tpRemoveFFMShipment.Location = new System.Drawing.Point(4, 4);
            this.tpRemoveFFMShipment.Margin = new System.Windows.Forms.Padding(4);
            this.tpRemoveFFMShipment.Name = "tpRemoveFFMShipment";
            this.tpRemoveFFMShipment.Padding = new System.Windows.Forms.Padding(4);
            this.tpRemoveFFMShipment.Size = new System.Drawing.Size(968, 367);
            this.tpRemoveFFMShipment.TabIndex = 1;
            this.tpRemoveFFMShipment.Text = "Remove FFM Shipment";
            // 
            // cbRFSType
            // 
            this.cbRFSType.FormattingEnabled = true;
            this.cbRFSType.Location = new System.Drawing.Point(324, 139);
            this.cbRFSType.Name = "cbRFSType";
            this.cbRFSType.Size = new System.Drawing.Size(181, 28);
            this.cbRFSType.TabIndex = 25;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(268, 142);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 20);
            this.label9.TabIndex = 24;
            this.label9.Text = "Type:";
            // 
            // lblRFSContract
            // 
            this.lblRFSContract.AutoSize = true;
            this.lblRFSContract.Location = new System.Drawing.Point(144, 186);
            this.lblRFSContract.Name = "lblRFSContract";
            this.lblRFSContract.Size = new System.Drawing.Size(48, 20);
            this.lblRFSContract.TabIndex = 23;
            this.lblRFSContract.Text = "None";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(46, 186);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(78, 20);
            this.label10.TabIndex = 22;
            this.label10.Text = "Contract:";
            // 
            // lblRFSTerminal
            // 
            this.lblRFSTerminal.AutoSize = true;
            this.lblRFSTerminal.Location = new System.Drawing.Point(144, 268);
            this.lblRFSTerminal.Name = "lblRFSTerminal";
            this.lblRFSTerminal.Size = new System.Drawing.Size(48, 20);
            this.lblRFSTerminal.TabIndex = 21;
            this.lblRFSTerminal.Text = "None";
            // 
            // lblRFSStatus
            // 
            this.lblRFSStatus.AutoSize = true;
            this.lblRFSStatus.Location = new System.Drawing.Point(144, 223);
            this.lblRFSStatus.Name = "lblRFSStatus";
            this.lblRFSStatus.Size = new System.Drawing.Size(48, 20);
            this.lblRFSStatus.TabIndex = 20;
            this.lblRFSStatus.Text = "None";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(46, 268);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 20);
            this.label6.TabIndex = 19;
            this.label6.Text = "Terminal:";
            // 
            // txtRFSInfo
            // 
            this.txtRFSInfo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtRFSInfo.Location = new System.Drawing.Point(651, 142);
            this.txtRFSInfo.Multiline = true;
            this.txtRFSInfo.Name = "txtRFSInfo";
            this.txtRFSInfo.ReadOnly = true;
            this.txtRFSInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRFSInfo.Size = new System.Drawing.Size(297, 165);
            this.txtRFSInfo.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(46, 223);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 20);
            this.label5.TabIndex = 17;
            this.label5.Text = "Status:";
            // 
            // btnRFSSearch
            // 
            this.btnRFSSearch.Location = new System.Drawing.Point(538, 133);
            this.btnRFSSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnRFSSearch.Name = "btnRFSSearch";
            this.btnRFSSearch.Size = new System.Drawing.Size(93, 35);
            this.btnRFSSearch.TabIndex = 16;
            this.btnRFSSearch.Text = "Search";
            this.btnRFSSearch.UseVisualStyleBackColor = true;
            this.btnRFSSearch.Click += new System.EventHandler(this.btnRFSSearch_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(46, 142);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 20);
            this.label4.TabIndex = 15;
            this.label4.Text = "HAWB:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(44, 27);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(370, 25);
            this.label7.TabIndex = 11;
            this.label7.Text = "Remove Shipment From FFM System";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(46, 81);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(658, 20);
            this.label8.TabIndex = 10;
            this.label8.Text = "Please search for the HAWB number and click on \"Remove Shipment\" once confirmed.";
            // 
            // txtRFSHAWB
            // 
            this.txtRFSHAWB.Location = new System.Drawing.Point(148, 142);
            this.txtRFSHAWB.Margin = new System.Windows.Forms.Padding(4);
            this.txtRFSHAWB.Name = "txtRFSHAWB";
            this.txtRFSHAWB.Size = new System.Drawing.Size(113, 26);
            this.txtRFSHAWB.TabIndex = 9;
            // 
            // btnRFSRemove
            // 
            this.btnRFSRemove.Location = new System.Drawing.Point(795, 314);
            this.btnRFSRemove.Margin = new System.Windows.Forms.Padding(4);
            this.btnRFSRemove.Name = "btnRFSRemove";
            this.btnRFSRemove.Size = new System.Drawing.Size(164, 29);
            this.btnRFSRemove.TabIndex = 8;
            this.btnRFSRemove.Text = "Remove Shipment";
            this.btnRFSRemove.UseVisualStyleBackColor = true;
            this.btnRFSRemove.Click += new System.EventHandler(this.btnRFSRemove_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 400);
            this.Controls.Add(this.tcMain);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMain";
            this.Text = "ABCLogistics Utility Tool";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tcMain.ResumeLayout(false);
            this.tpRemoveReportShipmentPS.ResumeLayout(false);
            this.tpRemoveReportShipmentPS.PerformLayout();
            this.tpRemoveFFMShipment.ResumeLayout(false);
            this.tpRemoveFFMShipment.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpRemoveReportShipmentPS;
        private System.Windows.Forms.TabPage tpRemoveFFMShipment;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPSRMessage;
        private System.Windows.Forms.TextBox txtHAWBNumbers;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCustomerNumber;
        private System.Windows.Forms.Label lblRFSTerminal;
        private System.Windows.Forms.Label lblRFSStatus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtRFSInfo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnRFSSearch;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtRFSHAWB;
        private System.Windows.Forms.Button btnRFSRemove;
        private System.Windows.Forms.ComboBox cbRFSType;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblRFSContract;
        private System.Windows.Forms.Label label10;
    }
}


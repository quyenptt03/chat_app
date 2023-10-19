namespace Server
{
    partial class frmServer
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
            this.lvDSClient = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvOnlClient = new System.Windows.Forms.ListView();
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblConnectedClients = new System.Windows.Forms.Label();
            this.btnSendImage = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.lsvMain = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // lvDSClient
            // 
            this.lvDSClient.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.lvDSClient.HideSelection = false;
            this.lvDSClient.Location = new System.Drawing.Point(25, 267);
            this.lvDSClient.Name = "lvDSClient";
            this.lvDSClient.Size = new System.Drawing.Size(283, 229);
            this.lvDSClient.TabIndex = 20;
            this.lvDSClient.UseCompatibleStateImageBehavior = false;
            this.lvDSClient.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 200;
            // 
            // lvOnlClient
            // 
            this.lvOnlClient.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name});
            this.lvOnlClient.HideSelection = false;
            this.lvOnlClient.Location = new System.Drawing.Point(25, 45);
            this.lvOnlClient.Name = "lvOnlClient";
            this.lvOnlClient.Size = new System.Drawing.Size(283, 180);
            this.lvOnlClient.TabIndex = 21;
            this.lvOnlClient.UseCompatibleStateImageBehavior = false;
            this.lvOnlClient.View = System.Windows.Forms.View.Details;
            // 
            // name
            // 
            this.name.Text = "Name";
            this.name.Width = 200;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 237);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 16);
            this.label2.TabIndex = 17;
            this.label2.Text = "Danh sách client";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 16);
            this.label1.TabIndex = 18;
            this.label1.Text = "Danh sách client đang online";
            // 
            // lblConnectedClients
            // 
            this.lblConnectedClients.AutoSize = true;
            this.lblConnectedClients.Location = new System.Drawing.Point(25, 514);
            this.lblConnectedClients.Name = "lblConnectedClients";
            this.lblConnectedClients.Size = new System.Drawing.Size(125, 16);
            this.lblConnectedClients.TabIndex = 19;
            this.lblConnectedClients.Text = "Tổng client online: 0";
            // 
            // btnSendImage
            // 
            this.btnSendImage.Location = new System.Drawing.Point(778, 441);
            this.btnSendImage.Margin = new System.Windows.Forms.Padding(4);
            this.btnSendImage.Name = "btnSendImage";
            this.btnSendImage.Size = new System.Drawing.Size(136, 55);
            this.btnSendImage.TabIndex = 16;
            this.btnSendImage.Text = "Send Image";
            this.btnSendImage.UseVisualStyleBackColor = true;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(922, 441);
            this.btnSend.Margin = new System.Windows.Forms.Padding(4);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(136, 55);
            this.btnSend.TabIndex = 15;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(319, 441);
            this.txtInput.Margin = new System.Windows.Forms.Padding(4);
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(449, 54);
            this.txtInput.TabIndex = 14;
            // 
            // lsvMain
            // 
            this.lsvMain.HideSelection = false;
            this.lsvMain.Location = new System.Drawing.Point(320, 45);
            this.lsvMain.Margin = new System.Windows.Forms.Padding(4);
            this.lsvMain.Name = "lsvMain";
            this.lsvMain.Size = new System.Drawing.Size(737, 388);
            this.lsvMain.TabIndex = 13;
            this.lsvMain.UseCompatibleStateImageBehavior = false;
            this.lsvMain.View = System.Windows.Forms.View.List;
            // 
            // frmServer
            // 
            this.AcceptButton = this.btnSend;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1086, 550);
            this.Controls.Add(this.lvDSClient);
            this.Controls.Add(this.lvOnlClient);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblConnectedClients);
            this.Controls.Add(this.btnSendImage);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.lsvMain);
            this.Name = "frmServer";
            this.Text = "Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmServer_FormClosing);
            this.Load += new System.EventHandler(this.frmServer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvDSClient;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ListView lvOnlClient;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblConnectedClients;
        private System.Windows.Forms.Button btnSendImage;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.ListView lsvMain;
    }
}


namespace Client
{
    partial class frmClient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmClient));
            this.tabPanel = new System.Windows.Forms.TabControl();
            this.serverTab = new System.Windows.Forms.TabPage();
            this.btnSendDocx = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.lsvMain = new System.Windows.Forms.ListView();
            this.clientTab = new System.Windows.Forms.TabPage();
            this.fpnPrivateMessList = new System.Windows.Forms.FlowLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnClientSend = new System.Windows.Forms.Button();
            this.txtClientInput = new System.Windows.Forms.TextBox();
            this.lvClientMain = new System.Windows.Forms.ListView();
            this.groupTab = new System.Windows.Forms.TabPage();
            this.fpnListGroupChat = new System.Windows.Forms.FlowLayoutPanel();
            this.button2 = new System.Windows.Forms.Button();
            this.btnGroupSend = new System.Windows.Forms.Button();
            this.txtGroupInput = new System.Windows.Forms.TextBox();
            this.lvGroupMain = new System.Windows.Forms.ListView();
            this.lblUsername = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
<<<<<<< HEAD
            this.lbgroubmember = new System.Windows.Forms.Label();
=======
            this.btnAddGroup = new System.Windows.Forms.Button();
>>>>>>> 80eac65439f495f379c526056fae3adba1d0a84e
            this.tabPanel.SuspendLayout();
            this.serverTab.SuspendLayout();
            this.clientTab.SuspendLayout();
            this.groupTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabPanel
            // 
            this.tabPanel.Controls.Add(this.serverTab);
            this.tabPanel.Controls.Add(this.clientTab);
            this.tabPanel.Controls.Add(this.groupTab);
            this.tabPanel.Location = new System.Drawing.Point(219, 2);
            this.tabPanel.Name = "tabPanel";
            this.tabPanel.SelectedIndex = 0;
            this.tabPanel.Size = new System.Drawing.Size(983, 623);
            this.tabPanel.TabIndex = 0;
            // 
            // serverTab
            // 
            this.serverTab.Controls.Add(this.btnSendDocx);
            this.serverTab.Controls.Add(this.btnSend);
            this.serverTab.Controls.Add(this.txtInput);
            this.serverTab.Controls.Add(this.lsvMain);
            this.serverTab.Location = new System.Drawing.Point(4, 25);
            this.serverTab.Name = "serverTab";
            this.serverTab.Padding = new System.Windows.Forms.Padding(3);
            this.serverTab.Size = new System.Drawing.Size(975, 594);
            this.serverTab.TabIndex = 0;
            this.serverTab.Text = "Server";
            this.serverTab.UseVisualStyleBackColor = true;
            // 
            // btnSendDocx
            // 
            this.btnSendDocx.Location = new System.Drawing.Point(685, 502);
            this.btnSendDocx.Margin = new System.Windows.Forms.Padding(4);
            this.btnSendDocx.Name = "btnSendDocx";
            this.btnSendDocx.Size = new System.Drawing.Size(138, 55);
            this.btnSendDocx.TabIndex = 16;
            this.btnSendDocx.Text = "Send Docx";
            this.btnSendDocx.UseVisualStyleBackColor = true;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(827, 502);
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
            this.txtInput.Location = new System.Drawing.Point(37, 502);
            this.txtInput.Margin = new System.Windows.Forms.Padding(4);
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(640, 54);
            this.txtInput.TabIndex = 14;
            // 
            // lsvMain
            // 
            this.lsvMain.HideSelection = false;
            this.lsvMain.Location = new System.Drawing.Point(37, 33);
            this.lsvMain.Margin = new System.Windows.Forms.Padding(4);
            this.lsvMain.Name = "lsvMain";
            this.lsvMain.Size = new System.Drawing.Size(928, 461);
            this.lsvMain.TabIndex = 13;
            this.lsvMain.UseCompatibleStateImageBehavior = false;
            this.lsvMain.View = System.Windows.Forms.View.List;
            // 
            // clientTab
            // 
            this.clientTab.Controls.Add(this.fpnPrivateMessList);
            this.clientTab.Controls.Add(this.button1);
            this.clientTab.Controls.Add(this.btnClientSend);
            this.clientTab.Controls.Add(this.txtClientInput);
            this.clientTab.Controls.Add(this.lvClientMain);
            this.clientTab.Location = new System.Drawing.Point(4, 25);
            this.clientTab.Name = "clientTab";
            this.clientTab.Padding = new System.Windows.Forms.Padding(3);
            this.clientTab.Size = new System.Drawing.Size(975, 594);
            this.clientTab.TabIndex = 1;
            this.clientTab.Text = "Client";
            this.clientTab.UseVisualStyleBackColor = true;
            // 
            // fpnPrivateMessList
            // 
            this.fpnPrivateMessList.AutoScroll = true;
            this.fpnPrivateMessList.Location = new System.Drawing.Point(6, 35);
            this.fpnPrivateMessList.Name = "fpnPrivateMessList";
            this.fpnPrivateMessList.Size = new System.Drawing.Size(238, 523);
            this.fpnPrivateMessList.TabIndex = 21;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(671, 504);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(138, 55);
            this.button1.TabIndex = 20;
            this.button1.Text = "Send Docx";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnClientSend
            // 
            this.btnClientSend.Location = new System.Drawing.Point(813, 504);
            this.btnClientSend.Margin = new System.Windows.Forms.Padding(4);
            this.btnClientSend.Name = "btnClientSend";
            this.btnClientSend.Size = new System.Drawing.Size(136, 55);
            this.btnClientSend.TabIndex = 19;
            this.btnClientSend.Text = "Send";
            this.btnClientSend.UseVisualStyleBackColor = true;
            this.btnClientSend.Click += new System.EventHandler(this.btnClientSend_Click);
            // 
            // txtClientInput
            // 
            this.txtClientInput.Location = new System.Drawing.Point(251, 504);
            this.txtClientInput.Margin = new System.Windows.Forms.Padding(4);
            this.txtClientInput.Multiline = true;
            this.txtClientInput.Name = "txtClientInput";
            this.txtClientInput.Size = new System.Drawing.Size(412, 54);
            this.txtClientInput.TabIndex = 18;
            // 
            // lvClientMain
            // 
            this.lvClientMain.HideSelection = false;
            this.lvClientMain.Location = new System.Drawing.Point(251, 35);
            this.lvClientMain.Margin = new System.Windows.Forms.Padding(4);
            this.lvClientMain.Name = "lvClientMain";
            this.lvClientMain.Size = new System.Drawing.Size(700, 461);
            this.lvClientMain.TabIndex = 17;
            this.lvClientMain.UseCompatibleStateImageBehavior = false;
            this.lvClientMain.View = System.Windows.Forms.View.List;
            // 
            // groupTab
            // 
            this.groupTab.Controls.Add(this.btnAddGroup);
            this.groupTab.Controls.Add(this.lbgroubmember);
            this.groupTab.Controls.Add(this.fpnListGroupChat);
            this.groupTab.Controls.Add(this.button2);
            this.groupTab.Controls.Add(this.btnGroupSend);
            this.groupTab.Controls.Add(this.txtGroupInput);
            this.groupTab.Controls.Add(this.lvGroupMain);
            this.groupTab.Location = new System.Drawing.Point(4, 25);
            this.groupTab.Name = "groupTab";
            this.groupTab.Size = new System.Drawing.Size(975, 594);
            this.groupTab.TabIndex = 2;
            this.groupTab.Text = "Group";
            this.groupTab.UseVisualStyleBackColor = true;
            // 
            // fpnListGroupChat
            // 
            this.fpnListGroupChat.AutoScroll = true;
            this.fpnListGroupChat.Location = new System.Drawing.Point(11, 35);
            this.fpnListGroupChat.Name = "fpnListGroupChat";
            this.fpnListGroupChat.Size = new System.Drawing.Size(238, 461);
            this.fpnListGroupChat.TabIndex = 26;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(676, 504);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(138, 55);
            this.button2.TabIndex = 25;
            this.button2.Text = "Send Docx";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btnGroupSend
            // 
            this.btnGroupSend.Location = new System.Drawing.Point(818, 504);
            this.btnGroupSend.Margin = new System.Windows.Forms.Padding(4);
            this.btnGroupSend.Name = "btnGroupSend";
            this.btnGroupSend.Size = new System.Drawing.Size(136, 55);
            this.btnGroupSend.TabIndex = 24;
            this.btnGroupSend.Text = "Send";
            this.btnGroupSend.UseVisualStyleBackColor = true;
            // 
            // txtGroupInput
            // 
            this.txtGroupInput.Location = new System.Drawing.Point(256, 504);
            this.txtGroupInput.Margin = new System.Windows.Forms.Padding(4);
            this.txtGroupInput.Multiline = true;
            this.txtGroupInput.Name = "txtGroupInput";
            this.txtGroupInput.Size = new System.Drawing.Size(412, 54);
            this.txtGroupInput.TabIndex = 23;
            // 
            // lvGroupMain
            // 
            this.lvGroupMain.HideSelection = false;
            this.lvGroupMain.Location = new System.Drawing.Point(256, 35);
            this.lvGroupMain.Margin = new System.Windows.Forms.Padding(4);
            this.lvGroupMain.Name = "lvGroupMain";
            this.lvGroupMain.Size = new System.Drawing.Size(700, 461);
            this.lvGroupMain.TabIndex = 22;
            this.lvGroupMain.UseCompatibleStateImageBehavior = false;
            this.lvGroupMain.View = System.Windows.Forms.View.List;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.lblUsername.Location = new System.Drawing.Point(86, 46);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(100, 22);
            this.lblUsername.TabIndex = 1;
            this.lblUsername.Text = "Username";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(19, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(55, 55);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
<<<<<<< HEAD
            // lbgroubmember
            // 
            this.lbgroubmember.AutoSize = true;
            this.lbgroubmember.Location = new System.Drawing.Point(20, 16);
            this.lbgroubmember.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbgroubmember.Name = "lbgroubmember";
            this.lbgroubmember.Size = new System.Drawing.Size(73, 16);
            this.lbgroubmember.TabIndex = 27;
            this.lbgroubmember.Text = "Thành viên";
=======
            // btnAddGroup
            // 
            this.btnAddGroup.Location = new System.Drawing.Point(11, 505);
            this.btnAddGroup.Name = "btnAddGroup";
            this.btnAddGroup.Size = new System.Drawing.Size(238, 54);
            this.btnAddGroup.TabIndex = 2;
            this.btnAddGroup.Text = "Add Group";
            this.btnAddGroup.UseVisualStyleBackColor = true;
            this.btnAddGroup.Click += new System.EventHandler(this.btnAddGroup_Click);
>>>>>>> 80eac65439f495f379c526056fae3adba1d0a84e
            // 
            // frmClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1223, 637);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.tabPanel);
            this.Name = "frmClient";
            this.Text = "Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmClient_FormClosing);
            this.Load += new System.EventHandler(this.frmClient_Load);
            this.tabPanel.ResumeLayout(false);
            this.serverTab.ResumeLayout(false);
            this.serverTab.PerformLayout();
            this.clientTab.ResumeLayout(false);
            this.clientTab.PerformLayout();
            this.groupTab.ResumeLayout(false);
            this.groupTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabPanel;
        private System.Windows.Forms.TabPage serverTab;
        private System.Windows.Forms.Button btnSendDocx;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.ListView lsvMain;
        private System.Windows.Forms.TabPage clientTab;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabPage groupTab;
        private System.Windows.Forms.FlowLayoutPanel fpnPrivateMessList;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnClientSend;
        private System.Windows.Forms.TextBox txtClientInput;
        private System.Windows.Forms.ListView lvClientMain;
        private System.Windows.Forms.FlowLayoutPanel fpnListGroupChat;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnGroupSend;
        private System.Windows.Forms.TextBox txtGroupInput;
        private System.Windows.Forms.ListView lvGroupMain;
        private System.Windows.Forms.Label lbgroubmember;
        private System.Windows.Forms.Button btnAddGroup;
    }
}


namespace Client
{
    partial class frmGroup
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
            this.btnAddGroup = new System.Windows.Forms.Button();
            this.txtAddGroup = new System.Windows.Forms.TextBox();
            this.fpnPrivateMessList = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // btnAddGroup
            // 
            this.btnAddGroup.Location = new System.Drawing.Point(238, 386);
            this.btnAddGroup.Name = "btnAddGroup";
            this.btnAddGroup.Size = new System.Drawing.Size(98, 52);
            this.btnAddGroup.TabIndex = 0;
            this.btnAddGroup.Text = "Add Group";
            this.btnAddGroup.UseVisualStyleBackColor = true;
            this.btnAddGroup.Click += new System.EventHandler(this.btnAddGroup_Click);
            // 
            // txtAddGroup
            // 
            this.txtAddGroup.Location = new System.Drawing.Point(12, 386);
            this.txtAddGroup.Multiline = true;
            this.txtAddGroup.Name = "txtAddGroup";
            this.txtAddGroup.Size = new System.Drawing.Size(220, 52);
            this.txtAddGroup.TabIndex = 1;
            // 
            // fpnPrivateMessList
            // 
            this.fpnPrivateMessList.AutoScroll = true;
            this.fpnPrivateMessList.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.fpnPrivateMessList.Location = new System.Drawing.Point(12, 12);
            this.fpnPrivateMessList.Name = "fpnPrivateMessList";
            this.fpnPrivateMessList.Size = new System.Drawing.Size(314, 359);
            this.fpnPrivateMessList.TabIndex = 22;
            // 
            // frmGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 450);
            this.Controls.Add(this.fpnPrivateMessList);
            this.Controls.Add(this.txtAddGroup);
            this.Controls.Add(this.btnAddGroup);
            this.Name = "frmGroup";
            this.Text = "frmGroup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddGroup;
        private System.Windows.Forms.TextBox txtAddGroup;
        private System.Windows.Forms.FlowLayoutPanel fpnPrivateMessList;
    }
}
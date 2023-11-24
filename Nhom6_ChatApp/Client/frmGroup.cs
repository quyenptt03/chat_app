using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class frmGroup : Form
    {

        string clientPartnerName;
        public string NameMember { get; private set; }
        public frmGroup(DataTable dt)
        {
            InitializeComponent();
            LoadPrivateMessageListToPanel(dt);
        }
        private void LoadPrivateMessageListToPanel(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                Button button = new Button();
                button.Text = row["chatPartnerUsername"].ToString();
                button.Click += privateMessageButton_Click;
                button.Width = fpnPrivateMessList.Width - 10;
                button.Height = 50;

                fpnPrivateMessList.Controls.Add(button);
            }
        }
        private void privateMessageButton_Click(object sender, EventArgs e)
        {
            if (sender is Button clickedButton)
            {
                clientPartnerName = clickedButton.Text;
                txtAddGroup.Text += clientPartnerName + ',';
                clientPartnerName = txtAddGroup.Text;

            }

        }

        private void btnAddGroup_Click(object sender, EventArgs e)
        {
            NameMember = clientPartnerName.ToString();
            this.Close();

        }
    }
}

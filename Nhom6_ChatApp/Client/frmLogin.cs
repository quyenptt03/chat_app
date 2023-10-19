using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class frmLogin : Form
    {

        Socket client;
        Account clientAccount = new Account();
        public frmLogin(Socket sock)
        {
            InitializeComponent();
            client = sock;
        }

        // gom mảnh
        byte[] Serialize(object data)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(stream, data);

            return stream.ToArray();
        }

        // phân mảnh
        object Deserialize(byte[] data)
        {
            MemoryStream stream = new MemoryStream(data);
            BinaryFormatter formatter = new BinaryFormatter();

            return formatter.Deserialize(stream);
        }

        private bool Login(string username, string password)
        {
            QueryAction act = new QueryAction(QueryActionType.Login, username, password);
            MessageData data = new MessageData() { Type = MessageType.Query, Data = act, QA_Content=QueryActionType.Login };

            client.Send(Serialize(data));
            byte[] buffer = new byte[1024 * 5000];
            client.Receive(buffer);

            DataTable result = (DataTable)Deserialize(buffer);
            if (result.Rows.Count == 1)
            {
                clientAccount.ID = Convert.ToInt32(result.Rows[0]["ID"]);
                clientAccount.Username = result.Rows[0]["username"].ToString();
                return true;
            }

            return false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPwd.Text;


            if (username == "" || password == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông báo");
                return;
            }

            bool isLoginSuccess = Login(username, password);
            if (!isLoginSuccess)
            {
                MessageBox.Show("Sai tên tài khoản hoặc mật khẩu", "Thông tin không hợp lệ");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Tag = clientAccount;
            this.Close();
            this.Dispose();
        }
    }
}

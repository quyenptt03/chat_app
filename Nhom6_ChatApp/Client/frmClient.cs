using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Client
{
    public partial class frmClient : Form
    {
        IPEndPoint IP;
        Socket client;
        Account clientAccount = new Account();
        string clientPartnerName;
        string chatRoomID;
        public frmClient()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            Connect();
        }

        // Kết nối đến server
        void Connect()
        {
            IP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2010);
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                client.Connect(IP);


                if (client.Connected)
                {
                    frmLogin frmLogin = new frmLogin(client);
                    var result = frmLogin.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        clientAccount = (Account)frmLogin.Tag;
                        this.Text = clientAccount.Username;
                        lblUsername.Text = clientAccount.Username;
                        queryToGetData(QueryActionType.GetPrivateChatsByUsername);
                        queryToGetData(QueryActionType.GetGroupChatsByUsername);

                    }
                }
            }
            catch
            {
                MessageBox.Show("Không thể kết nối đến server", "Lỗi");
                return;
            }

            Thread listen = new Thread(Receive);
            listen.IsBackground = true;
            listen.Start();
        }


        // Đóng kết nối socket

        void CloseConnection()
        {
            client.Close();
        }

        // Gửi tin nhắn đến server
        void Send(MessageType type, object data, string receiver = "Server")
        {
            MessageData sendData = new MessageData();
            sendData.Sender = clientAccount.Username;
            sendData.Data = data;
            sendData.Type = type;

            if (type == MessageType.Query)
            {
                sendData.Receiver = receiver;
            }
            else if (type == MessageType.Server)
            {
                sendData.Receiver = receiver;
            }
            else if (type == MessageType.Client)
            {
                sendData.Receiver = receiver;
            }
            else if (type == MessageType.Group)
            {
                sendData.Receiver = receiver;//ID nhom "thuy,quyen"
            }

            client.Send(Serialize(sendData));
        }

        // Lắng nghe phản hồi từ phía server
        void Receive()
        {
            try
            {
                while (true)
                {
                    byte[] buffer = new byte[1024 * 5000];
                    client.Receive(buffer);
                    MessageData receiveData = (MessageData)Deserialize(buffer);
                    string mess;

                    if (receiveData.Type == MessageType.Query)
                    {
                        handleReceiveQuery(receiveData);
                    }
                    else if (receiveData.Type == MessageType.Server)
                    {
                        mess = (string)receiveData.Data;
                        AddMessage($"{receiveData.Sender}: {mess}", lsvMain);
                    }
                    else if (receiveData.Type == MessageType.Client)
                    {
                        mess = (string)receiveData.Data;
                        AddMessage($"{receiveData.Sender}: {mess}", lvClientMain);
                    }
                    else if (receiveData.Type == MessageType.Group)
                    {
                        mess = (string)receiveData.Data;
                        AddMessage($"{receiveData.Sender}: {mess}", lvGroupMain);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Có lỗi xảy ra trong quá trình nhận phản hồi từ server. Đóng kết nối", e.Message);
                CloseConnection();
            }
        }


        // Thêm tin nhắn vào màn hình
        void AddMessage(string s, ListView lv)
        {
            ListViewItem mess = new ListViewItem() { Text = s };
            lv.Items.Add(mess);
            txtInput.Clear();
        }

        // Phân mảnh dữ liệu, tạo thành mảng byte để gửi đi
        byte[] Serialize(object data)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(stream, data);

            return stream.ToArray();
        }

        // Gom mảnh dữ liệu, tạo thành đối tượng ban đầu
        object Deserialize(byte[] data)
        {
            MemoryStream stream = new MemoryStream(data);
            BinaryFormatter formatter = new BinaryFormatter();

            return formatter.Deserialize(stream);
        }

        #region handle receive data
        private void handleReceiveQuery(MessageData recvData)
        {
            switch (recvData.QA_Content)
            {
                case QueryActionType.GetPrivateChatsByUsername:
                    LoadPrivateMessageListToPanel((DataTable)recvData.Data);
                    break;

                case QueryActionType.GetGroupChatsByUsername:
                    LoadGroupChatToPanel((DataTable)recvData.Data);
                    break;

                case QueryActionType.GetMessagesPrivateChat:
                    LoadMessagesToListview((DataTable)recvData.Data, lvClientMain);
                    break;

                case QueryActionType.GetRoomMembersByID:
                    ConvertDataTableToString((DataTable)recvData.Data, lvGroupMain);
                    break;

                case QueryActionType.GetMessagesRoomChat:
                    LoadMessagesToListview((DataTable)recvData.Data, lvGroupMain);
                    break;
            }
        }



        #endregion
        // gửi truy vấn để lấy dữ liệu

        private void queryToGetData(QueryActionType type, params object[] args)
        {
            QueryAction act = new QueryAction(type, clientAccount.Username);
            if (args.Length > 0)
            {
                act.ID = Convert.ToInt32(args[0]);
            }
            Send(MessageType.Query, act);

        }

        // load danh sách tin nhắn giữa 2 client vào panel
        private void LoadPrivateMessageListToPanel(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                Button button = new Button();
                button.Text = row["chatPartnerUsername"].ToString();
                button.Name = row["privateMessageID"].ToString();
                button.Click += privateMessageButton_Click;
                button.Width = fpnPrivateMessList.Width - 10;
                button.Height = 50;

                fpnPrivateMessList.Controls.Add(button);
            }
        }

        // load danh sách group chat vào panel
        private void LoadGroupChatToPanel(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                Button button = new Button();
                button.Text = row["roomChatName"].ToString();
                button.Name = row["roomChatID"].ToString();
                button.Click += groupMessageButton_Click;
                button.Width = fpnPrivateMessList.Width - 10;
                button.Height = 50;

                fpnListGroupChat.Controls.Add(button);
            }
        }

        // load danh sách tin nhắn vào listview
        private void LoadMessagesToListview(DataTable dt, ListView lv)
        {
            //lv.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                string mess = $"{row["senderName"]}:  {row["content"]}";
                AddMessage(mess, lv);
            }
        }

        // hàm xử lí sự kiện khi nhấn vào private message
        private void privateMessageButton_Click(object sender, EventArgs e)
        {
            if (sender is Button clickedButton)
            {
                clientPartnerName = clickedButton.Text;
                string chatID = clickedButton.Name;
                queryToGetData(QueryActionType.GetMessagesPrivateChat, chatID);

            }

        }
        // hàm xử lí sự kiện khi nhấn vào groub message
        private void groupMessageButton_Click(object sender, EventArgs e)
        {
            if (sender is Button clickedButton)
            {
                chatRoomID = clickedButton.Name;
                queryToGetData(QueryActionType.GetRoomMembersByID, chatRoomID);
                queryToGetData(QueryActionType.GetMessagesRoomChat, chatRoomID);

            }
        }
        private void frmClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            //DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (result == DialogResult.Yes)
            CloseConnection();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string message = txtInput.Text;
            Send(MessageType.Server, message);
            AddMessage("Me: " + message, lsvMain);
        }
        private void btnClientSend_Click(object sender, EventArgs e)
        {
            string message = txtClientInput.Text;
            Send(MessageType.Client, message, clientPartnerName);
            AddMessage("Me: " + message, lvClientMain);

        }

    

        private void ConvertDataTableToString(DataTable dt, ListView lv)
        {

            StringBuilder result = new StringBuilder();

            foreach (DataRow row in dt.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    result.Append(item.ToString() + ",");
                }
            }
            result.Length -= 1;
            lbgroubmember.Text = "Thành viên: "+ result.ToString();
            clientPartnerName = result.ToString();
        }

        private void btnGroupSend_Click(object sender, EventArgs e)
        {
            string message =  txtGroupInput.Text;
            Send(MessageType.Group,  message, chatRoomID + "," + clientPartnerName);
            AddMessage("Me: " + message, lvGroupMain);
        }
    }
}
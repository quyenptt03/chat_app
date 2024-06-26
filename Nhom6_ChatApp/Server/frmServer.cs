﻿using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Reflection;
using System.Xml.Linq;

namespace Server
{
    public partial class frmServer : Form
    {
        string connectionString = "server=.; database = AccountManagement; Integrated Security = true;";
        IPEndPoint IP;
        Socket server;
        List<Socket> clientList = new List<Socket>();
        //danh sách tất cả các client
        List<Account> clientAccountList = new List<Account>();
        Account clientAccount = new Account();
        string members;
        public frmServer()
        {
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;

            Start();
        }


        // Kết nối đến server
        void Start()
        {
            IP = new IPEndPoint(IPAddress.Any, 2010);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientList = new List<Socket>();

            server.Bind(IP);

            Thread listen = new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        server.Listen(100);
                        Socket client = server.Accept();

                        clientList.Add(client);
                        clientAccount = new Account();

                        Thread receive = new Thread(Receive);
                        receive.IsBackground = true;
                        receive.Start(client);

                        AddMessage(client.RemoteEndPoint.ToString() + ": " + "Đã kết nối");
                        getConnectedClientCount();
                    }
                }
                catch
                {
                    IP = new IPEndPoint(IPAddress.Any, 2010);
                    server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                }
            });

            listen.IsBackground = true;
            listen.Start();
        }


        // Đóng kết nối socket
        void CloseConnection()
        {
            server.Close();
        }

        // Gửi tin nhắn cho client
        void Send(Socket client, MessageData sendData)
        {
            client.Send(Serialize(sendData));
        }

        // Lắng nghe phản hồi từ phía server
        void Receive(object obj)
        {
            Socket client = obj as Socket;

            try
            {
                while (true)
                {
                    byte[] buffer = new byte[1024 * 5000];
                    client.Receive(buffer);
                    MessageData recvData = (MessageData)Deserialize(buffer);

                    if (recvData.Type == MessageType.Query)
                    {
                        ReceiveQueryAction(recvData,client);
                    }
                    else if (recvData.Type == MessageType.Server)
                    {
                        handleMessageToServer(recvData, client);
                    }
                    else if (recvData.Type == MessageType.Client)
                    {
                        handleMessageToClient(recvData);
                    }
                    else if (recvData.Type == MessageType.Group)
                    {
                        handleMessageToGroup(recvData);
                    }
                    else if (recvData.Type == MessageType.AddGroup)
                    {
                        getIdbyUsernameMemberGroup(recvData);
                    }

                }
            }
            catch
            {
                AddMessage(client.RemoteEndPoint.ToString() + ": Đã đóng kết nối");
                clientList.Remove(client);
                updateClientAccount(client);
                LoadListClients();
                getConnectedClientCount();
                LoadListOnlineClients();
                client.Close();
            }
        }

       
        // Thêm tin nhắn vào màn hình
        void AddMessage(string message)
        {
            lsvMain.Items.Add(new ListViewItem() { Text = message });
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
        #region xử lí nhận dữ liệu từ client
        // xử lý truy vấn database
        private void ReceiveQueryAction(MessageData recvData, Socket client)
        {
            QueryAction data = (QueryAction)recvData.Data;
            MessageData sendData;
            sendData = new MessageData();
            sendData.Type = MessageType.Query;
            sendData.Sender = "Server";

            switch (data.Type)
            {
                case QueryActionType.Login:
                    getClientLogin(data.Username, data.Password, client);
                    break;
                case QueryActionType.GetPrivateChatsByUsername:
                    sendData.Data = getPrivateChatsByUsername(data.Username);
                    sendData.QA_Content = QueryActionType.GetPrivateChatsByUsername;
                    Send(client, sendData);
                    break;
                case QueryActionType.GetGroupChatsByUsername:
                    sendData.Data = getGroupChatsByUsername(data.Username);
                    sendData.QA_Content = QueryActionType.GetGroupChatsByUsername;
                    Send(client, sendData);
                    break;
                case QueryActionType.GetMessagesPrivateChat:
                    sendData.Data = getMessagesPrivateMessage(data.ID);
                    sendData.QA_Content = QueryActionType.GetMessagesPrivateChat;
                    Send(client, sendData);
                   // DeleteMessagesPrivateMessage(data.ID);
                    break;
                case QueryActionType.GetRoomMembersByID:
                    sendData.Data = getRoomMembersByID(data.ID);
                    sendData.QA_Content = QueryActionType.GetRoomMembersByID;
                    Send(client, sendData);
                    break;
            }
        }
        // xử lí tin nhắn gửi tới server
        private void handleMessageToServer(MessageData recvData, Socket client)
        {
            string sender = recvData.Sender;
            string message = recvData.Data.ToString();
            AddMessage($"{sender}: {message}");
        }

        // xử lí tin nhắn gửi tới client
        private void handleMessageToClient(MessageData recvData)
        {          
            foreach (Account clientAcc in clientAccountList)
            {
                if(clientAcc.Username == recvData.Receiver && clientAcc.Status == 0)
                {   DataTable dt = getIDPrivateMessage((String)recvData.Sender, (String)recvData.Receiver);
                    DataTable IDUser = GetUserIDByUsername((String)recvData.Sender);
                    if (dt.Rows.Count > 0 && IDUser.Rows.Count>0)
                    {
                        int ID= Convert.ToInt32(IDUser.Rows[0]["ID"]);
                        int privateMessageID = Convert.ToInt32(dt.Rows[0]["ID"]);
                        SaveMessage(privateMessageID, ID, (String)recvData.Data);
                        break;
                    } 
                   
                }
                if (clientAcc.Username == recvData.Receiver && clientAcc.Status == 1)
                {
                    Send(clientAcc.ClientSocket, recvData);
                }
               
            }
        }
        // Save message room off 
        private void SaveMessageRoom(int roomID, int senderID, string content)
        {

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            // Lấy thời gian hiện tại
            DateTime currentTime = DateTime.Now;

            string sqlStr = "insert Message(roomID, senderID, content, timestamp) values(@roomID, @senderID, @content, @timestamp)";

            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                // Thêm các tham số vào truy vấn SQL
                cmd.Parameters.AddWithValue("@roomID", roomID);
                cmd.Parameters.AddWithValue("@senderID", senderID);
                cmd.Parameters.AddWithValue("@content", content);
                cmd.Parameters.AddWithValue("@timestamp", currentTime);

                // Thực thi truy vấn
                cmd.ExecuteNonQuery();
            }

            conn.Close();

        }
        //Lấy Id của các thành viên để tạo nhóm
        private void getIdbyUsernameMemberGroup(MessageData recvData)
        {
            string member = "";
            string[] recvNameList;

            if (recvData.Data is string dataString)
            {
                recvNameList = dataString.Split(',');
                foreach (Account clientAcc in clientAccountList)
                {
                    if (recvNameList.Contains(clientAcc.Username))
                    {
                            member += clientAcc.ID.ToString()+',';
                    }
                }
            }
            DataTable IDUserSender = GetUserIDByUsername((String)recvData.Sender);
            member += IDUserSender.Rows[0]["ID"].ToString() + ',';
            CreateGroupWithMember(member.Trim());
        }
        // xử lí tin nhắn tới group chat
        private void handleMessageToGroup(MessageData recvData)
        {
            string[] recvNameList = recvData.Receiver.Split(',');
            foreach (Account clientAcc in clientAccountList)
            {
                if (recvNameList.Contains(clientAcc.Username) && clientAcc.Username != recvData.Sender && clientAcc.Status == 1)
                if (recvNameList.Contains(clientAcc.Username) && clientAcc.Username != recvData.Sender && clientAcc.Status == 0)
                {
                    DataTable IDUser = GetUserIDByUsername((String)recvData.Sender);
                    if (IDUser.Rows.Count > 0)
                    {
                        int ID = Convert.ToInt32(IDUser.Rows[0]["ID"]);
                        SaveMessageRoom(int.Parse(recvNameList[0]), ID, (String)recvData.Data); 
                    }

                }
                 if (recvNameList.Contains(clientAcc.Username) && clientAcc.Username != recvData.Sender && clientAcc.Status == 1)
                {
                    Send(clientAcc.ClientSocket, recvData);
                }
            }
        }
        #endregion

        #region query to sql database
        // Lấy danh sách tài khoản từ csdl
        private List<Account> getAllClientsFromDB()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Account";
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            adapter.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                Account acc = new Account();
                acc.ID = Convert.ToInt32(dr["ID"]);
                acc.Username = dr["username"].ToString();

                clientAccountList.Add(acc);
            }
            return clientAccountList;
        }

        // Login
        private void getClientLogin(string username, string password, Socket sock)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Account WHERE username = @username AND password = @password";

            cmd.Parameters.Add("@username", SqlDbType.NVarChar, 100).Value = username;
            cmd.Parameters.Add("@password", SqlDbType.NVarChar, 100).Value = password;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                //clientAccount = new Account();
                //            clientAccount.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                //            clientAccount.Username = dt.Rows[0]["username"].ToString();
                //clientAccount.ClientSocket = sock;
                //clientAccountList.Add(clientAccount);
                Account clientAcc = clientAccountList.Where(client => client.Username == dt.Rows[0]["username"].ToString()).First();
                clientAcc.ClientSocket = sock;
                clientAcc.Status = 1;
                LoadListOnlineClients();
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    DataRow dr = dt.NewRow();
                    dt.Rows.Add(dr);
                }
            }

            sock.Send(Serialize(dt));


            conn.Close();
            conn.Dispose();
            adapter.Dispose();
        }

        // lấy danh sách private chat từ username
        private DataTable getPrivateChatsByUsername(string username)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "exec GetPrivateMessagesByUsername @username";

            cmd.Parameters.Add("@username", SqlDbType.NVarChar, 100);
            cmd.Parameters["@username"].Value = username;
            
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);


            conn.Close();
            conn.Dispose();
            adapter.Dispose();

            return dt;
        }

        // lấy danh sách group chat từ username----------------
        private DataTable getGroupChatsByUsername(string username)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "exec GetGroupChatsByUsername1 @username";

            cmd.Parameters.Add("@username", SqlDbType.NVarChar, 100);
            cmd.Parameters["@username"].Value = username.Trim();

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);


            conn.Close();
            conn.Dispose();
            adapter.Dispose();

            return dt;
        }

        // lấy danh sách tin nhắn trong private message từ ID
        private DataTable getMessagesPrivateMessage(int ID)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "exec GetMessageInPrivateMessage @ID";

            cmd.Parameters.Add("@ID", SqlDbType.NVarChar, 100);
            cmd.Parameters["@ID"].Value = ID;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);


            conn.Close();
            conn.Dispose();
            adapter.Dispose();

            return dt;
        }
        // lấy danh sách tin nhắn trong room message từ ID
        private DataTable getMessagesRoomMessage(int ID)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "exec GetMessageRoomMessage @ID";

            cmd.Parameters.Add("@ID", SqlDbType.NVarChar, 100);
            cmd.Parameters["@ID"].Value = ID;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);


            conn.Close();
            conn.Dispose();
            adapter.Dispose();

            return dt;
        }

        // Save message off 
        private void SaveMessage(int privateMessageID, int senderID, string content)
        {
   
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            // Lấy thời gian hiện tại
            DateTime currentTime = DateTime.Now;

            string sqlStr = "insert Message(privateMessageID, senderID, content, timestamp) values(@privateMessageID, @senderID, @content, @timestamp)";

            using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
            {
                // Thêm các tham số vào truy vấn SQL
                cmd.Parameters.AddWithValue("@privateMessageID", privateMessageID);
                cmd.Parameters.AddWithValue("@senderID", senderID);
                cmd.Parameters.AddWithValue("@content", content);
                cmd.Parameters.AddWithValue("@timestamp", currentTime);

                // Thực thi truy vấn
                cmd.ExecuteNonQuery();
            }

            conn.Close();

        }
        // Lấy danh sách thành viên trong 1 nhóm từ ID nhóm 
        private DataTable getRoomMembersByID(int roomID)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "exec GetRoomMembersByID @roomID";

            cmd.Parameters.Add("@roomID", SqlDbType.NVarChar, 100);
            cmd.Parameters["@roomID"].Value = roomID;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);


            conn.Close();
            conn.Dispose();
            adapter.Dispose();

            return dt;
        }
        //Lấy ID nhóm chat giữa 2 client
        private DataTable getIDPrivateMessage(string username1, string username2)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "exec GetPrivateMessageID1 @username1 , @username2";

            cmd.Parameters.Add("@username1", SqlDbType.NVarChar, 100);
            cmd.Parameters["@username1"].Value = username1;
            cmd.Parameters.Add("@username2", SqlDbType.NVarChar, 100);
            cmd.Parameters["@username2"].Value = username2;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);


            conn.Close();
            conn.Dispose();
            adapter.Dispose();

            return dt;
        }


        // Xóa tin nhắn trong private message từ ID
        private DataTable DeleteMessagesPrivateMessage(int ID)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "exec DeleteMessageInPrivateMessage @ID";

            cmd.Parameters.Add("@ID", SqlDbType.NVarChar, 100);
            cmd.Parameters["@ID"].Value = ID;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);


            conn.Close();
            conn.Dispose();
            adapter.Dispose();

            return dt;
        }

        private DataTable GetUserIDByUsername(string username)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "exec GetUserIDByUsername @username";

            cmd.Parameters.Add("@username", SqlDbType.NVarChar, 100);
            cmd.Parameters["@username"].Value = username;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);


            conn.Close();
            conn.Dispose();
            adapter.Dispose();

            return dt;
        }

        //Tạo nhóm
        private void CreateGroupWithMember(string members)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "exec CreateGroupWithMember @members";

            cmd.Parameters.Add("@members", SqlDbType.NVarChar, 100);
            cmd.Parameters["@members"].Value = members;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);


            conn.Close();
            conn.Dispose();
            adapter.Dispose();
        }
        #endregion



        // số lượng client kết nối vào server
        private void getConnectedClientCount()
        {
            lblConnectedClients.Text = $"Tổng client online: {clientList.Count}";
        }

        // update socket của client thành null
        private void updateClientAccount(Socket socket)
        {
            foreach (Account clientAccount in clientAccountList)
            {
                if (clientAccount.ClientSocket == socket)
                {
                    clientAccount.ClientSocket = null;
                }
            }
        }

        // Thêm client vào listview
        private void AddClientLV(Account acc, ListView lv)
        {
            ListViewItem item = new ListViewItem(acc.Username);
            lv.Items.Add(item);
        }
        private void RemoveClientLV(Account acc, ListView lv)
        {
            ListViewItem item = new ListViewItem(acc.Username);
            lv.Items.Remove(item);
        }

        // Load danh sách client từ db vào listview
        private void LoadListClientLV()
        {
            lvDSClient.Items.Clear();
            List<Account> listAcc = getAllClientsFromDB();
            if (listAcc.Count > 0)
            {
                foreach (Account acc in listAcc)
                {
                    AddClientLV(acc, lvDSClient);
                }
            }
        }
        public void LoadListClients()
        {
            foreach (Account clientAcc in clientAccountList)
            {
                if (!clientList.Any(clientSocket => clientSocket == clientAcc.ClientSocket))
                {
                    clientAcc.Status = 0;
                }
            }
        }
        //Load danh sách client đang onlinet

        private void LoadListOnlineClients()
        {
            lvOnlClient.Items.Clear();
            foreach (Account clientAccount in clientAccountList)
            {
                //if(clientAccount.ClientSocket != null) 
                if (clientAccount.Status == 1)
                    AddClientLV(clientAccount, lvOnlClient);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            MessageData sendData = new MessageData();
            sendData.Type = MessageType.Server;
            sendData.Data = txtInput.Text;
            sendData.Sender = "Server";
            foreach (Socket client in clientList)
            {
                Send(client, sendData);
            }

            AddMessage("Server: " + txtInput.Text);
            txtInput.Clear();
        }

        private void frmServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseConnection();
        }

        private void frmServer_Load(object sender, EventArgs e)
        {
            LoadListClientLV();
        }
    }
}

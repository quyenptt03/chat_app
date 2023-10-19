using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Account
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Socket ClientSocket { get; set; }
        public int Status { get; set; }//0: offline, 1: online

        public Account() { }
        public Account(int id, string username, string password, Socket clientSock, int status)
        {
            this.ID = id;
            this.Username = username;
            this.Password = password;
            this.ClientSocket = clientSock;
            this.Status = status;
        }

        //public Account(byte[] data)
        //{
        //    int place = 0;
        //    ID = BitConverter.ToInt32(data, place);
        //    place += 4;
        //    UsernameSize = BitConverter.ToInt32(data, place);
        //    place += 4;
        //    Username = Encoding.ASCII.GetString(data, place, UsernameSize);
        //    place += UsernameSize;
        //    PasswordSize = BitConverter.ToInt32(data, place);
        //    place += 4;
        //    Password = Encoding.ASCII.GetString(data, place, PasswordSize);
        //    place += PasswordSize;
        //}

        //public byte[] GetBytes()
        //{
        //    byte[] data = new byte[1024];
        //    int place = 0;

        //    Buffer.BlockCopy(BitConverter.GetBytes(ID), 0, data, place, 4);
        //    place += 4;
        //    Buffer.BlockCopy(BitConverter.GetBytes(Username.Length), 0, data, place, 4);
        //    place += 4;
        //    Buffer.BlockCopy(Encoding.ASCII.GetBytes(Username), 0, data, place, Username.Length);
        //    place += Username.Length;
        //    Buffer.BlockCopy(BitConverter.GetBytes(Password.Length), 0, data, place, 4);
        //    place += 4;
        //    Buffer.BlockCopy(Encoding.ASCII.GetBytes(Password), 0, data, place, Password.Length);
        //    place += Password.Length;

        //    size = place;
        //    return data;
        //}

    }
}

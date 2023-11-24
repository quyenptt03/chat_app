using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [Serializable]
    public enum QueryActionType
    {
        Login,
        GetPrivateChatsByUsername,
        GetGroupChatsByUsername,
        GetMessagesPrivateChat,
        GetRoomMembersByID,
        GetMessagesRoomChat,
        
    }

    [Serializable]
    public class QueryAction
    {
        public QueryActionType Type { get; set; }
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public QueryAction() { }
        public QueryAction(QueryActionType type, string username, string pass)
        {
            this.Type = type;
            this.Username = username;
            this.Password = pass;
        }

        public QueryAction(QueryActionType type, string username)
        {
            this.Type=type;
            this.Username = username;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [Serializable]
    public enum MessageType
    {
        Query,
        Server,
        Client,
        Group,
        AddGroup,
    }

    [Serializable]
    public class MessageData
    {
        public MessageType Type;
        public object Data { get; set; }
        public QueryActionType QA_Content { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public MessageData()
        {

        }

        public MessageData(MessageType type, object data, string sender, string receiver, QueryActionType qa_content = 0)
        {
            Type = type;
            Data = data;
            Sender = sender;
            Receiver = receiver;
            QA_Content = qa_content;
        }
    }
}

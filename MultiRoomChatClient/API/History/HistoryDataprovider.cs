using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiRoomChatClient
{
    public class HistoryDataprovider
    {
        private string folder;

        public HistoryDataprovider(string Folder)
        {
            this.folder = Folder + "/";
            Directory.CreateDirectory(folder);
        }

        public void AppendMessage(string room, ChatMessage message)
        {
            File.AppendAllLines(folder + room, new string[] { JsonConvert.SerializeObject(message) });
        }

        public void AppendSequence(string room, ChatMessage[] messages)
        {

            string[] text = new string[messages.Length];
            for(int i = 0; i< text.Length; i++)
            {
                text[i] = JsonConvert.SerializeObject(messages[i]);
            }
            File.AppendAllLines(folder + room, text);
        }

        public ChatMessage[] GetHistory(string roomName, ChatMessage last = null)
        {
            if (!File.Exists(folder + roomName))
            {
                return new LinkedList<ChatMessage>().ToArray();
            }

            string[] list = File.ReadAllLines(folder + roomName);
            LinkedList<ChatMessage> messages = new LinkedList<ChatMessage>();
            int length = list.Length;
            for (int i = 0; i < length; i++)
            {
                ChatMessage msg = JsonConvert.DeserializeObject<ChatMessage>(list[length - 1 - i]);
                if(last != null && last.TimeStamp == msg.TimeStamp)
                {
                    break;
                }
                messages.AddFirst(msg);
            }
            return messages.ToArray();
        }

    }
}

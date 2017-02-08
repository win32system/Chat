using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiRoomChatClient
{
    public partial class MessageListBox : UserControl
    {
        public MessageListBox()
        {
            InitializeComponent();
        }

        public MessageListBox(RoomObjExt room)
        {
            InitializeComponent();

        }
    }
}

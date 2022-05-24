using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Media;
using System.Windows.Input;
using System.Threading;

namespace client
{
    public class Config
    {
        public static string BASE_URL = "http://localhost";
        public static string AUTH_PATH = Environment.CurrentDirectory + @"\auth.json";
    }

    public partial class client : Form
    {
        private static ChatAPI.UserAuth auth;

        private string selectedChat;
        private string lastMsgId;

        public client()
        {
            InitializeComponent();
        }

        private void displayMessage(Color clientColor, string clientName, string text)
        {
            rtbMesssages.SelectionColor = clientColor;
            rtbMesssages.SelectionFont = new Font(rtbMesssages.Font, FontStyle.Bold);
           
            rtbMesssages.AppendText($"{clientName}: ");
            rtbMesssages.SelectionColor = rtbMesssages.ForeColor;
            rtbMesssages.SelectionFont = rtbMesssages.Font;

            rtbMesssages.AppendText($"{text}\r\n{String.Concat(Enumerable.Repeat("-", (int)Math.Floor(rtbMesssages.Width / (rtbMesssages.Font.Size / 2))))}\r\n");
        }

        private void client_Load(object sender, EventArgs e)
        {
            try
            {
                auth = ChatAPI.LoadAuth(Config.AUTH_PATH);
            }
            catch
            {
                LoginForm form = new LoginForm();

                if (form.ShowDialog() == DialogResult.OK)
                    client_Load(sender, e);
                else
                    MessageBox.Show("You're not signed in.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                form.Dispose();
                Close();
                return;
            }

            button1.Enabled = rtbMessageBox.Enabled = false;
            rtbMessageBox.Enabled = button1.Enabled;

            messageChecker.Enabled = true;
            refreshChats(sender, e);
            checkMessages(sender, e);
        }

        private void addChatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateChatForm form = new CreateChatForm();

            if (form.ShowDialog() == DialogResult.OK)
                refreshChats(sender, e);

            form.Dispose();
        }

        private void joinChatToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void chatSelector_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string chat = e.Node.FullPath.Split('\\')[0];
            string[] parts = chat.Split('@');

            selectedChat = parts[parts.Length - 1];

            chatSelector.CollapseAll();
            e.Node.Expand();

            checkMessages(sender, e);
        }

        private void checkMessages(object sender, EventArgs e)
        {
            if (selectedChat == null)
                return;

            List<ChatAPI.Message> messages = ChatAPI.GetMessages(Config.BASE_URL, selectedChat, auth.user_id, auth.token);

            if (messages.Count <= 0)
            {
                rtbMesssages.Text = "";
                lastMsgId = null;
                return;
            }

            if (messages[messages.Count - 1].id == lastMsgId)
                return;

            if (messages[messages.Count - 1].user_id != auth.user_id)
                SystemSounds.Hand.Play();

            rtbMesssages.Text = "";

            foreach (ChatAPI.Message message in messages)
                displayMessage(Color.Red, message.username, Encoding.UTF8.GetString(Convert.FromBase64String(message.text)));

            lastMsgId = messages[messages.Count - 1].id;
        }

        private void sendMessage(object sender, EventArgs e)
        {
            if (rtbMessageBox.Text.Length <= 0)
                return;

            ChatAPI.SendMessage(Config.BASE_URL, rtbMessageBox.Text, selectedChat, auth.user_id, auth.token);
            checkMessages(sender, e);
            rtbMessageBox.Text = "";
        }

        private void rtbMessageBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && e.Control)
            {
                sendMessage(sender, e);
                e.Handled = false;
                rtbMessageBox.Clear();
            }
        }

        private void refreshChats(object sender, EventArgs e)
        {
            chatSelector.Nodes.Clear();
            List<ChatAPI.Chat> chats = ChatAPI.GetChats(Config.BASE_URL, auth.user_id, auth.token);

            foreach (ChatAPI.Chat chat in chats)
            {
                TreeNode[] users = new TreeNode[chat.users.Length];

                for (int i = 0; i < chat.users.Length; i++)
                    users[i] = new TreeNode(chat.users[i].username + "#" + chat.users[i].id, new TreeNode[0]);

                chatSelector.Nodes.Add(new TreeNode(chat.name + "@" + chat.chat_id, users));
            }

            if (chatSelector.Nodes.Count <= 0)
                return;

            chatSelector.Nodes[0].Expand();
            selectedChat = chats[0].chat_id;

            button1.Enabled = !(selectedChat == null);
            rtbMessageBox.Enabled = button1.Enabled;
        }
    }
}

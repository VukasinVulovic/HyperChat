using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace client
{
    public partial class CreateChatForm : Form
    {
        public CreateChatForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(tbName.Text, @"^[a-zA-Z0-9_]{1,20}$"))
            {
                errorProvider.SetError(tbName, "Name must include only the characters of the alphabet, number and underscore and be between 1 and 20 cahracters.");
                return;
            }
            else
                errorProvider.SetError(tbName, null);

            ChatAPI.UserAuth auth;

            try
            {
                auth = ChatAPI.LoadAuth(Config.AUTH_PATH);
            }
            catch
            {
                MessageBox.Show("You're not signed in.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
                return;
            }

            if (!ChatAPI.CreateChat(Config.BASE_URL, tbName.Text, auth.user_id, auth.token))
            {
                MessageBox.Show("Error while creating chat, name could be taken.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                MessageBox.Show("Created chat uccessfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
                return;
            }

        }
    }
}

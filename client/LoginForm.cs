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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private bool validateValues()
        {
            if (!Regex.IsMatch(tbUsername.Text, @"^[a-zA-Z0-9_]{1,20}$"))
            {
                errorProvider.SetError(tbUsername, "Username must include only the characters of the alphabet, number and underscore and be between 1 and 20 cahracters.");
                return false;
            }
            else
                errorProvider.SetError(tbUsername, null);

            if (!Regex.IsMatch(tbPassword.Text, @"^(?=.*?[a-z])(?=.*?[0-9]).{8,20}$"))
            {
                errorProvider.SetError(tbPassword, "Username must be between 8 and 20 cahracters and include only the characters of the alphabet, special characters and have at least three numbers.");
                return false;
            }
            else
                errorProvider.SetError(tbPassword, null);

            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (validateValues())
            {
                ChatAPI.UserAuth auth = ChatAPI.LoginUser(Config.BASE_URL, tbUsername.Text, tbPassword.Text);

                if (auth == null)
                {
                    MessageBox.Show("Error while logging in user, username or password could be bad.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    MessageBox.Show("User login successfull.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    ChatAPI.SaveAuth(Config.AUTH_PATH, auth);
                    return;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (validateValues())
            {
                ChatAPI.UserAuth auth = ChatAPI.RegisterUser(Config.BASE_URL, tbUsername.Text, tbPassword.Text);

                if (auth == null)
                {
                    MessageBox.Show("Error while registering user, username could be taken.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    MessageBox.Show("User creation successfull.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    ChatAPI.SaveAuth(Config.AUTH_PATH, auth);
                    return;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tpotifiy
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            Bitmap background = Properties.Resources.BackGround;

            this.BackgroundImage = background;

            this.BackgroundImageLayout = ImageLayout.Stretch;

            Color panelBackground = Color.FromArgb(100, Color.DarkGreen);

            panel1.BackColor = panelBackground;

            Color panelBackgroundDark = Color.FromArgb(100, Color.Black);

            panel2.BackColor = panelBackgroundDark;

            textBoxUname.Parent = panel2;
            textBoxPword.Parent = panel2;

            textBoxUname.ForeColor = Color.LightGreen;
            textBoxUname.AutoSize = false;
            textBoxUname.Height = 23;

            textBoxPword.ForeColor = Color.LightGreen;
            textBoxPword.AutoSize = false;
            textBoxPword.Height = 23;

            labelUsername.BackColor = Color.Transparent;
            labelUsername.Parent = panel2; 

            labelPassword.BackColor = Color.Transparent;
            labelPassword.Parent = panel2;

            labelForgot.BackColor = Color.Transparent;
            labelForgot.Parent = panel2;

            linkLabelPassword.BackColor = Color.Transparent;
            linkLabelPassword.Parent = panel2;
        }
                
        private void button1_Click(object sender, EventArgs e)
        {
            string _uname = textBoxUname.Text;
            string _pword = textBoxPword.Text;
            string hashedPassword = Utilities.ComputeSha256Hash(_pword);

            if (Utilities.Login(_uname, hashedPassword))
            {
                MessageBox.Show("Login Success!", "Success", MessageBoxButtons.OK);

                MainForm mainform = new MainForm();
                mainform.Show();

                this.Hide();
            }
            else
            {
                textBoxUname.Text = "";
                textBoxPword.Text = "";
                MessageBox.Show("Wrong Username or Password!", "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SignupForm signupform = new SignupForm();
            signupform.Show();

            this.Hide();
        }

        private void linkLabelPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ForgotPasswordForm forgotpasswordform = new ForgotPasswordForm();
            forgotpasswordform.Show();

            this.Hide();
        }
    }
}

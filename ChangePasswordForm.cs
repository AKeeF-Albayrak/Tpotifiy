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
    public partial class ChangePasswordForm : Form
    {
        public ChangePasswordForm()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;

            this.StartPosition = FormStartPosition.CenterScreen;

            Bitmap background = Properties.Resources.BackGround;

            this.BackgroundImage = background;

            this.BackgroundImageLayout = ImageLayout.Stretch;

            Color panelBackgroundDark = Color.FromArgb(100, Color.Black);

            panel1.BackColor = panelBackgroundDark;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != textBox2.Text)
            {
                //password does not match
            }
            else if (Utilities.checkPassword(Utilities.ComputeSha256Hash(textBox1.Text)))
            {
                //same as old password
            }
            else if (textBox1.Text == textBox2.Text)
            {
                Utilities.changePassword(Utilities.ComputeSha256Hash(textBox1.Text));
                //password changed

                LoginForm loginform = new LoginForm();
                loginform.Show();

                this.Hide();
            }
        }
    }
}
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
    public partial class ForgotPasswordForm : Form
    {
        string randomcode = Utilities.GenerateRandomCode();
        int x = 0;

        public ForgotPasswordForm()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;

            this.StartPosition = FormStartPosition.CenterScreen;

            Bitmap background = Properties.Resources.BackGround;

            this.BackgroundImage = background;

            this.BackgroundImageLayout = ImageLayout.Stretch;

            Color panelBackgroundDark = Color.FromArgb(100, Color.Black);

            panel1.BackColor = panelBackgroundDark;

            panel2.BackColor = panelBackgroundDark;
        }

        private void buttonSendCode_Click(object sender, EventArgs e)
        {
            if (Utilities.checkMail(textBox1.Text))
            {
                if (x == 0)
                {
                    Utilities.SendEmail(textBox1.Text, "Forgot Your Password", randomcode);
                    x++;
                }
                else
                {
                    MessageBox.Show("The code has already been sent", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("There is no such user in our system!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (x == 1)
            {
                if (textBox3.Text == randomcode)
                {
                    ChangePasswordForm changePasswordForm = new ChangePasswordForm();
                    changePasswordForm.Show();

                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Wrong Code!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

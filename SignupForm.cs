using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Tpotifiy
{
    public partial class SignupForm : Form
    {
        private bool isDigit(string s) 
        {
            foreach (char c in s)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        public SignupForm()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void SignupForm_Load(object sender, EventArgs e)
        {
            Bitmap background = Properties.Resources.BackGround;

            this.BackgroundImage = background;

            this.BackgroundImageLayout = ImageLayout.Stretch;

            Color panelBackground = Color.FromArgb(100, Color.DarkGreen);

            panel1.BackColor = panelBackground;
        }

        private void pictureBoxMale_Click(object sender, EventArgs e)
        {
            checkBoxMale.Checked = true;
            checkBoxFemale.Checked = false;
        }

        private void pictureBoxFemale_Click(object sender, EventArgs e)
        {
            checkBoxFemale.Checked = true;
            checkBoxMale.Checked = false;
        }

        private void checkBoxFemale_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxMale.Checked = !checkBoxFemale.Checked;
        }

        private void checkBoxMale_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxFemale.Checked = !checkBoxMale.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBoxName.Text;
            string surname = textBoxSurname.Text;
            string username = textBoxUsername.Text;
            string mail = textBoxMail.Text;
            string password1 = textBoxPWord1.Text;
            string password2= textBoxPWord2.Text;
            string phone_number = textBoxPNumber.Text;
            string birth_date= dateTimePickerBirthDate.Value.ToShortDateString();
            bool gender = true;

            if (checkBoxFemale.Checked)
            {
                gender = false;
            }

            textBoxName.Text = "";
            textBoxSurname.Text = "";
            textBoxUsername.Text = "";
            textBoxMail.Text = "";
            textBoxPNumber.Text = "";
            textBoxPWord1.Text = "";
            textBoxPWord2.Text = "";
            checkBoxMale.Checked = true;
            checkBoxFemale.Checked = false;

            if (phone_number.Length != 11)
            {
                label13.Text = "Phone Number must be 11 characters!";
                label13.ForeColor = System.Drawing.Color.Red;

                label11.Text = "";
                label12.Text = "";
                label14.Text = "";
            }
            else if(!isDigit(phone_number))
            {
                label13.Text = "Invalid phone number!";
                label13.ForeColor = System.Drawing.Color.Red;
            }
            else if (!mail.EndsWith("@gmail.com"))
            {
                label14.Text = "The email address must end with '@gmail.com'!";
                label14.ForeColor = System.Drawing.Color.Red;

                label11.Text = "";
                label12.Text = "";
                label13.Text = "";
            }
            else if (password1 != password2)
            {
                label12.Text = "Password length must be between 5-30 characters!";
                label12.ForeColor = System.Drawing.Color.Red;

                label13.Text = "";
                label12.Text = "";
                label14.Text = "";
            }
            else if (password1.Length < 5 || password1.Length > 30)
            {
                label12.Text = "Password length must be between 5-30 characters!";
                label12.ForeColor = System.Drawing.Color.Red;

                label11.Text = "Password length must be between 5-30 characters!";
                label11.ForeColor = System.Drawing.Color.Red;

                label13.Text = "";
                label14.Text = "";
            }
            else
            {
                Utilities.SignUp(name, surname, username, mail, password1, phone_number, birth_date, gender);

                label11.Text = "";
                label12.Text = "";
                label13.Text = "";
                label14.Text = "";
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginForm mainForm = new LoginForm();
            mainForm.Show();

            this.Close();
        }
    }
}

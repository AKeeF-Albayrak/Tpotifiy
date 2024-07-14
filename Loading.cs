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
    public partial class Loading : Form
    {
        private Timer timer;
        public Loading()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;

            this.StartPosition = FormStartPosition.CenterScreen;

            timer = new Timer();
            timer.Interval = 100;//6700
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();

            LoginForm mainForm = new LoginForm();
            mainForm.Show();

            this.Close();
        }

    }
}

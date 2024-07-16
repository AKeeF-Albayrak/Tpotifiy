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
    public partial class AudioPlayerForm : Form
    {
        public AudioPlayerForm()
        {
            InitializeComponent();
            InitializeTrackBar();
            InitializeProgressBar();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Session.isPlaying = !(Session.isPlaying);

            if (Session.isPlaying)
            {
                Utilities.PlaySong("C:\\Users\\akifa\\OneDrive\\Desktop\\Tpotifiy\\Resources\\Rick Astley - Never Gonna Give You Up (Official Music Video).mp3",progressBar1,labelTimePassed);
                pictureBox1.Image = Image.FromFile(@"C:\Users\akifa\OneDrive\Desktop\Tpotifiy\Resources\pause.png");
            }
            else
            {
                Utilities.StopSong();
                pictureBox1.Image = Image.FromFile(@"C:\Users\akifa\OneDrive\Desktop\Tpotifiy\Resources\play_button.png");
            }
        }

        private void InitializeTrackBar()
        {
            // TrackBar özelliklerini ayarla
            trackBarVolume.Minimum = 0;
            trackBarVolume.Maximum = 100;
            trackBarVolume.TickFrequency = 10;
            trackBarVolume.Value = 50; // Başlangıçta %50 ses seviyesi

            // TrackBar scroll event'ini ekle
            trackBarVolume.Scroll += trackBarVolume_Scroll;
        }

        private void trackBarVolume_Scroll(object sender, EventArgs e)
        {
            float volume = trackBarVolume.Value / 100f;
            Utilities.SetVolume(volume);
            labelVolume.Text = $"Volume: {trackBarVolume.Value}%";
        }

        private void InitializeProgressBar()
        {
            progressBar1.MouseClick += progressBar1_MouseClick;
        }

        private void progressBar1_MouseClick(object sender, MouseEventArgs e)
        {
            // Calculate the percentage of the click position
            float clickPosition = (float)e.X / progressBar1.Width;

            // Calculate the new position in the song in seconds
            int newPosition = (int)(clickPosition * progressBar1.Maximum);

            // Update the song position
            Utilities.SeekTo(newPosition);
        }
    }
}

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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            this.IsMdiContainer = true;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ShowSearchForm();
            ShowAlbumForm();
            ShowPlayerForm();
        }

        private void ShowSearchForm()
        {
            SearchForm searchForm = new SearchForm();

            searchForm.TopLevel = false;
            searchForm.FormBorderStyle = FormBorderStyle.None;
            searchForm.Dock = DockStyle.Fill;

            panelSearch.Controls.Clear();
            panelSearch.Controls.Add(searchForm);
            searchForm.Show();
        }

        private void ShowAlbumForm()
        {
            AlbumForm albumForm = new AlbumForm();

            albumForm.TopLevel = false;
            albumForm.FormBorderStyle = FormBorderStyle.None;
            albumForm.Dock = DockStyle.Fill;

            panelAlbums.Controls.Clear();
            panelAlbums.Controls.Add(albumForm);
            albumForm.Show();
        }

        private void ShowPlayerForm()
        {
            AudioPlayerForm audioPlayerForm = new AudioPlayerForm();


            audioPlayerForm.TopLevel = false;
            audioPlayerForm.FormBorderStyle = FormBorderStyle.None;
            audioPlayerForm.Dock = DockStyle.Fill;

            panelPlayer.Controls.Clear();
            panelPlayer.Controls.Add(audioPlayerForm);
            audioPlayerForm.Show();
        }
    }
}

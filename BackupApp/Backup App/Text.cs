using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Backup_App
{
    public partial class Text : Form
    {
        public Text()
        {
            InitializeComponent();
        }

        public void setCoordinates(Point p)
        {
            this.Location = p;
        }

        string y = "";
        public void setBack(string backFrom)
        {
            y = backFrom;
            if (backFrom == "help")
            {
                label1.Text = "<- Back to Help";
            }
            else
            {
                label1.Text = "<- Back to Origin";
            }
        }

        private void helpLabel_Click(object sender, EventArgs e)
        {
            Help he = new Help();
            he.StartPosition = FormStartPosition.Manual;
            he.Location = new Point(this.Location.X, this.Location.Y);
            he.Show();
            this.Close();
        }

        private void backupLabel_Click(object sender, EventArgs e)
        {
            BackupHelp bH = new BackupHelp();
            bH.StartPosition = FormStartPosition.Manual;
            bH.Location = new Point(this.Location.X, this.Location.Y);
            bH.Show();
            this.Close();
        }

        private void termLabel_Click(object sender, EventArgs e)
        {
            Terms t = new Terms();
            t.StartPosition = FormStartPosition.Manual;
            t.Location = new Point(this.Location.X, this.Location.Y);
            t.Show();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (y == "help")
            {
                BackupHelp h = new BackupHelp();
                h.StartPosition = FormStartPosition.Manual;
                h.Location = new Point(this.Location.X, this.Location.Y);
                h.Show();
                this.Close();
            }
            else
            {
                Multiple m = new Multiple();
                m.StartPosition = FormStartPosition.Manual;
                m.Location = new Point(this.Location.X, this.Location.Y);
                m.Show();
                this.Close();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}

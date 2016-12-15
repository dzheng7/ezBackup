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
    public partial class Destination : Form
    {
        public Destination()
        {
            InitializeComponent();
        }

        public void setCoordinates(Point p)
        {
            this.Location = p;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            BackupHelp bH = new BackupHelp();
            bH.StartPosition = FormStartPosition.Manual;
            bH.Location = new Point(this.Location.X, this.Location.Y);
            bH.Show();
            this.Close();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
        private void helpLabel_Click(object sender, EventArgs e)
        {
            Help h = new Help();
            h.StartPosition = FormStartPosition.Manual;
            h.Location = new Point(this.Location.X, this.Location.Y);
            h.Show();
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
    }
}

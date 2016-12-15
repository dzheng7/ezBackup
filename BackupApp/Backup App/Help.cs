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
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();
        }

        public void setCoordinates(Point p)
        {
            this.Location = p;
        }

        private void backupLabel_Click_1(object sender, EventArgs e)
        {
            BackupHelp bH = new BackupHelp();
            bH.setCoordinates(new Point(this.Location.X, this.Location.Y));
            bH.Show();
            this.Close();
        }


        private void helpLabel_Click(object sender, EventArgs e)
        {
            Help h = new Help();
            h.StartPosition = FormStartPosition.Manual;
            h.Location = new Point(this.Location.X, this.Location.Y);
            h.Show();
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

        private void Help_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(this.Width + ", " + this.Height);
            this.Width = 350;
            this.Height = 525;
        }

        private void Help_SizeChanged(object sender, EventArgs e)
        {
            //this.Width = 350;
            //this.Height = 525;
        }

        private void helpLabel_Click_1(object sender, EventArgs e)
        {
            Help h = new Help();
            h.StartPosition = FormStartPosition.Manual;
            h.Location = new Point(this.Location.X, this.Location.Y);
            h.setCoordinates(new Point(this.Location.X, this.Location.Y));
            h.Show();
            this.Close();
        }

        private void backupLabel_Click(object sender, EventArgs e)
        {
            BackupHelp bH = new BackupHelp();
            bH.StartPosition = FormStartPosition.Manual;
            bH.Location = new Point(this.Location.X, this.Location.Y);
            bH.setCoordinates(new Point(this.Location.X, this.Location.Y));
            bH.Show();
            this.Close();
        }

        private void termLabel_Click_1(object sender, EventArgs e)
        {
            Terms t = new Terms();
            t.StartPosition = FormStartPosition.Manual;
            t.Location = new Point(this.Location.X, this.Location.Y);
            t.setCoordinates(new Point(this.Location.X, this.Location.Y));
            t.Show();
            this.Close();
        }
    }
}

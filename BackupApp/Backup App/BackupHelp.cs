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
    public partial class BackupHelp : Form
    {
        public BackupHelp()
        {
            InitializeComponent();
        }

        public void setCoordinates(Point p)
        {
            this.Location = p;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Origin o = new Origin();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = this.Location;
            o.Show();
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Destination d = new Destination();
            d.StartPosition = FormStartPosition.Manual;
            d.Location = this.Location;
            d.Show();
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {
            Multiple m = new Multiple();
            m.StartPosition = FormStartPosition.Manual;
            m.Location = new Point(this.Location.X, this.Location.Y);
            m.setBack("help");
            m.Show();
            this.Close();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Origin o = new Origin();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = new Point(this.Location.X, this.Location.Y);
            o.Show();
            this.Close();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Text t = new Text();
            t.StartPosition = FormStartPosition.Manual;
            t.Location = new Point(this.Location.X, this.Location.Y);
            t.setBack("help");
            t.Show();
            this.Close();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            IndivSelect i = new IndivSelect();
            i.StartPosition = FormStartPosition.Manual;
            i.Location = new Point(this.Location.X, this.Location.Y);
            i.setBack("help");
            i.Show();
            this.Close();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Destination d = new Destination();
            d.StartPosition = FormStartPosition.Manual;
            d.Location = new Point(this.Location.X, this.Location.Y);
            d.Show();
            this.Close();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            Help h = new Help();
            h.StartPosition = FormStartPosition.Manual;
            h.Location = this.Location;
            h.Show();
            this.Close();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            BackupHelp h = new BackupHelp();
            h.StartPosition = FormStartPosition.Manual;
            h.Location = this.Location;
            h.Show();
            this.Close();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            Terms t = new Terms();
            t.StartPosition = FormStartPosition.Manual;
            t.Location = this.Location;
            t.Show();
            this.Close();
        }
        
    }
}

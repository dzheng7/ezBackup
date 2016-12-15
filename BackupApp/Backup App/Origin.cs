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
    public partial class Origin : Form
    {
        public Origin()
        {
            InitializeComponent();
        }

        private void Origin_Load(object sender, EventArgs e)
        {
            
        }

        public void setCoordinates(Point p)
        {
            this.Location = p;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            /*if (y == "help")
            {
                //Help j = new Help();
                //j.setCoordinates(new Point(this.Location.X, this.Location.Y));
                //j.Show();
                //this.Close();
            }
            else
            {*/
                BackupHelp bH = new BackupHelp();
                bH.StartPosition = FormStartPosition.Manual;
                bH.Location = new Point(this.Location.X, this.Location.Y);
                bH.Show();
                this.Close();
            //}
        }

        private void label6_Click_1(object sender, EventArgs e)
        {
            Multiple m = new Multiple();
            m.StartPosition = FormStartPosition.Manual;
            m.Location = new Point(this.Location.X, this.Location.Y);
            m.Show();
            this.Close();
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

        private void label13_Click(object sender, EventArgs e)
        {

        }


    }
}

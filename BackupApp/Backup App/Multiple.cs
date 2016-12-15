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
    public partial class Multiple : Form
    {
        public Multiple()
        {
            InitializeComponent();
        }

        public void setCoordinates(Point p)
        {
            this.Location = p;
            //this
        }

        string h = "";
        public void setBack(string backFrom)
        {
            h = backFrom;
            if (backFrom == "help")
            {
                label1.Text = "<- Back to Help";
            }
            else
            {
                label1.Text = "<- Back to Origin";
            }
        }

        private void helpLabel_Click_1(object sender, EventArgs e)
        {
            Help he = new Help();
            he.StartPosition = FormStartPosition.Manual;
            he.Location = new Point(this.Location.X, this.Location.Y);
            he.Show();
            this.Close();
        }

        private void backupLabel_Click_1(object sender, EventArgs e)
        {
            BackupHelp bH = new BackupHelp();
            bH.StartPosition = FormStartPosition.Manual;
            bH.Location = new Point(this.Location.X, this.Location.Y);
            bH.Show();
            this.Close();
        }

        private void termLabel_Click_1(object sender, EventArgs e)
        {
            Terms t = new Terms();
            t.StartPosition = FormStartPosition.Manual;
            t.Location = new Point(this.Location.X, this.Location.Y);
            t.Show();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (h == "help")
            {
                BackupHelp gh = new BackupHelp();
                gh.StartPosition = FormStartPosition.Manual;
                gh.Location = new Point(this.Location.X, this.Location.Y);
                gh.Show();
                this.Close();
            }
            else
            {
                Origin o = new Origin();
                o.StartPosition = FormStartPosition.Manual;
                o.Location = new Point(this.Location.X, this.Location.Y);
                o.Show();
                this.Close();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Text t = new Text();
            t.StartPosition = FormStartPosition.Manual;
            t.Location = new Point(this.Location.X, this.Location.Y);
            t.Show();
            this.Close();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            IndivSelect iS = new IndivSelect();
            iS.StartPosition = FormStartPosition.Manual;
            iS.Location = new Point(this.Location.X, this.Location.Y);
            iS.Show();
            this.Close();
        }

    }
}

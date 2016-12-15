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
    public partial class Terms : Form
    {
        public Terms()
        {
            InitializeComponent();
        }

        public void setCoordinates(Point p)
        {
            this.Location = p;
        }

        private void Terms_Load(object sender, EventArgs e)
        {
            /*textBox1.Text = "Use this application to backup your files \n\n"
                + "Origin: the place(s) that you want to backup files from\n"
                + "Single Folder- Backup files from only one folder\n"
                + "Multiple Folders- Backup files from more than one folder"
                    + " via a text file containing the file paths\n\n"
                + "Destination: the folder you want to place your files in";*/
            /*textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText("Information:");*/
            /*textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText("Use this application to backup your files to a folder!");
            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText("Key: ");
            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText("     Origin-the place(s) that you want to backup files from");
            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText("          Select an option, then your choice of folder(s)");
            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText("          Single Folder: Backup files from a selected folder");
            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText("          Multiple Folders: Backup many folders"
                + " - you choose how");
            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText("               Use Text File: Insert the paths of the folders"
                + " you want to backup into the file");
            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText("               Select Individually: Choose folders one-by-one");
            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText("     Destination-the folder you want to place your files in");
            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText("Other: ");
            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText("     See Folders: See which folders you selected to be backed up");
            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText("     The text file should separate the folder paths"
                + " by line breaks");
            textBox1.Width = this.Width;
            textBox1.Height = this.Height;*/
        }

        private void Terms_SizeChanged_1(object sender, EventArgs e)
        {
            //this.Width = 500;
            //this.Height = 800;
        }
        Help h = new Help();
        BackupHelp b = new BackupHelp();
        private void helpLabel_Click(object sender, EventArgs e)
        {
            h.StartPosition = FormStartPosition.Manual;
            h.Location = new Point(this.Location.X, this.Location.Y);
            h.Show();
            this.Close();
        }

        private void backupLabel_Click(object sender, EventArgs e)
        {
            b.StartPosition = FormStartPosition.Manual;
            b.Location = new Point(this.Location.X, this.Location.Y);
            b.Show();
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

        private void Terms_Load_1(object sender, EventArgs e)
        {
            //MessageBox.Show(this.Width + ", " + this.Height);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

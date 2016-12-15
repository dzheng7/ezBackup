using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;

namespace Backup_App
{
    public partial class Confirmation : Form
    {
        public Confirmation()
        {
            InitializeComponent();
        }
        bool ready = false;
        bool clicked = false;
        private void Confirmation_Load(object sender, EventArgs e)
        {
            flowLayoutPanel2.Left = (this.Width - flowLayoutPanel2.Width) /2;
            textBox1.Left = (this.Width - textBox1.Width) / 2;
        }
        private void Cofirmation_Resize(object sender, EventArgs e)
        {
            flowLayoutPanel2.Location = new Point((this.Width - flowLayoutPanel2.Width) / 2, flowLayoutPanel1.Height /2);
            textBox1.Location = new Point((this.Width - textBox1.Width) / 2, (this.Height - flowLayoutPanel1.Height) / 2);
        }

        private void yesButton_Click(object sender, EventArgs e)
        {
            ready = true;
            clicked = true;
            this.Close();
        }

        private void noButton_Click(object sender, EventArgs e)
        {
            clicked = true;
            this.Close();
        }
        public bool getReady()
        {
            return ready;
        }
        public bool getClicked()
        {
            return clicked;
        }
    }
}

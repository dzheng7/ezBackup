namespace Backup_App
{
    partial class Start
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.startButton = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(338, 298);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 34);
            this.startButton.TabIndex = 12;
            this.startButton.Text = "Begin!";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(37, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(111, 82);
            this.label9.TabIndex = 20;
            this.label9.Text = "ez";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Green;
            this.label10.Location = new System.Drawing.Point(128, 18);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(275, 82);
            this.label10.TabIndex = 21;
            this.label10.Text = "Backup";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label10);
            this.panel1.ForeColor = System.Drawing.SystemColors.Desktop;
            this.panel1.Location = new System.Drawing.Point(153, 102);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(434, 121);
            this.panel1.TabIndex = 22;
            // 
            // Start
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(753, 344);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.startButton);
            this.Name = "Start";
            this.Text = "Begin!";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel1;
    }
}
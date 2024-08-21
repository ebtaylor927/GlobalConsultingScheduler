using System.Windows.Forms;
using System;

namespace GlobalConsultingScheduler
{
    partial class UserSchedule
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
        private Button btnHome;
        private DataGridView dgvUserSchedule;

        private void InitializeComponent()
        {
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "User Schedule";

            this.dgvUserSchedule = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserSchedule)).BeginInit();
            this.SuspendLayout();

            this.dgvUserSchedule.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUserSchedule.Location = new System.Drawing.Point(10, 50);
            this.dgvUserSchedule.Name = "dgvUserSchedule";
            this.dgvUserSchedule.Size = new System.Drawing.Size(1300, 350);
            this.Controls.Add(this.dgvUserSchedule);

            this.btnHome = new Button();
            this.btnHome.Text = "Home";
            this.btnHome.Location = new System.Drawing.Point(10, 10);
            this.btnHome.Size = new System.Drawing.Size(75, 30);
            this.btnHome.Click += new EventHandler(this.BtnHome_Click);
            this.Controls.Add(this.btnHome);

            ((System.ComponentModel.ISupportInitialize)(this.dgvUserSchedule)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}
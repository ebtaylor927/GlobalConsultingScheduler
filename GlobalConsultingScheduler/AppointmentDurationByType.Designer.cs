using System.Windows.Forms;

namespace GlobalConsultingScheduler
{
    partial class AppointmentDurationByType
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
        private DataGridView dgvAppointmentDurations;

        private void InitializeComponent()
        {
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 450);
            this.Text = "AppointmentDurationByType";

            this.btnHome = new System.Windows.Forms.Button();
            this.btnHome.Text = "Home";
            this.btnHome.Location = new System.Drawing.Point(10, 10);
            this.btnHome.Size = new System.Drawing.Size(100, 30);
            this.btnHome.Click += BtnHome_Click;
            this.Controls.Add(this.btnHome);

            this.dgvAppointmentDurations = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointmentDurations)).BeginInit();
            this.dgvAppointmentDurations.Location = new System.Drawing.Point(10, 50);
            this.dgvAppointmentDurations.Size = new System.Drawing.Size(850, 390);
            this.Controls.Add(this.dgvAppointmentDurations);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointmentDurations)).EndInit();
        }


        #endregion
    }
}
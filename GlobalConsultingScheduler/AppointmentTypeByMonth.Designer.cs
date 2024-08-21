using System.Windows.Forms;

namespace GlobalConsultingScheduler
{
    partial class AppointmentTypeByMonth
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
        private DataGridView dgvReport;

        private void InitializeComponent()
        {
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Appointment Types by Month";

            this.btnHome = new Button();
            this.btnHome.Location = new System.Drawing.Point(680, 10);
            this.btnHome.Size = new System.Drawing.Size(100, 30);
            this.btnHome.Text = "Home";
            this.btnHome.Click += BtnHome_Click;
            this.Controls.Add(this.btnHome);

            this.dgvReport = new DataGridView();
            this.dgvReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReport.Location = new System.Drawing.Point(10, 50);
            this.dgvReport.Name = "dgvReport";
            this.dgvReport.Size = new System.Drawing.Size(780, 390);
            this.Controls.Add(this.dgvReport);
        }

        #endregion
    }
}
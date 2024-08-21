using System;

namespace GlobalConsultingScheduler
{
    partial class AppointmentsPage
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
        private System.Windows.Forms.Button btnAddAppointment;
        private System.Windows.Forms.DataGridView dgvAppointments;
        private System.Windows.Forms.Button btnHome;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 450);
            this.Text = "AppointmentsPage";

            this.btnHome = new System.Windows.Forms.Button();
            this.btnHome.Location = new System.Drawing.Point(680, 10);
            this.btnHome.Size = new System.Drawing.Size(110, 30);
            this.btnHome.Text = "Home";
            this.btnHome.UseVisualStyleBackColor = true;
            this.Controls.Add(this.btnHome);
            this.btnHome.Click += new EventHandler(this.BtnHome_Click);

            this.btnAddAppointment = new System.Windows.Forms.Button();
            this.btnAddAppointment.Location = new System.Drawing.Point(10, 10);
            this.btnAddAppointment.Size = new System.Drawing.Size(120, 30);
            this.btnAddAppointment.Text = "Add Appointment";
            this.btnAddAppointment.BackColor = System.Drawing.Color.Green;
            this.btnAddAppointment.ForeColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnAddAppointment);

            this.dgvAppointments = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointments)).BeginInit();
            this.SuspendLayout();
            
            this.dgvAppointments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAppointments.Location = new System.Drawing.Point(10, 50);
            this.dgvAppointments.Name = "dgvAppointments";
            this.dgvAppointments.Size = new System.Drawing.Size(1350, 390);
            this.dgvAppointments.TabIndex = 1;
            this.Controls.Add(this.dgvAppointments);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointments)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}
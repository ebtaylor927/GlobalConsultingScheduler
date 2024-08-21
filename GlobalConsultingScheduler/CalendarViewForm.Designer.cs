using System.Windows.Forms;
using System;

namespace GlobalConsultingScheduler
{
    partial class CalendarViewForm
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
        private DataGridView dgvAppointments;
        private MonthCalendar monthCalendar;

        private void InitializeComponent()
        {
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1320, 600);
            this.Text = "Calendar View";

            this.monthCalendar = new MonthCalendar();
            this.monthCalendar.Location = new System.Drawing.Point(10, 10);
            this.Controls.Add(this.monthCalendar);
            this.monthCalendar.DateChanged += new DateRangeEventHandler(this.MonthCalendar_DateChanged);

            this.dgvAppointments = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointments)).BeginInit();
            this.dgvAppointments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAppointments.Location = new System.Drawing.Point(10, 200);
            this.dgvAppointments.Name = "dgvAppointments";
            this.dgvAppointments.Size = new System.Drawing.Size(1300, 390);
            this.dgvAppointments.TabIndex = 1;
            this.Controls.Add(this.dgvAppointments);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointments)).EndInit();

            // Home Button
            this.btnHome = new Button();
            this.btnHome.Location = new System.Drawing.Point(660, 10);
            this.btnHome.Size = new System.Drawing.Size(120, 30);
            this.btnHome.Text = "Home";
            this.btnHome.UseVisualStyleBackColor = true;
            this.btnHome.Click += new EventHandler(this.BtnHome_Click);
            this.Controls.Add(this.btnHome);
        }


        #endregion
    }
}
namespace GlobalConsultingScheduler
{
    partial class MainForm
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
        private System.Windows.Forms.Label lblSelectOperation;
        private System.Windows.Forms.Button btnManageCustomer;
        private System.Windows.Forms.Button btnManageAppointments;
        private System.Windows.Forms.Label lblViewReports;
        private System.Windows.Forms.Button btnAppointmentTypesByMonth;
        private System.Windows.Forms.Button btnScheduleForUser;
        private System.Windows.Forms.Button btnThirdReport;
        private System.Windows.Forms.Button btnCalendarView;


        private void InitializeComponent()
        {
            this.lblSelectOperation = new System.Windows.Forms.Label();
            this.btnManageCustomer = new System.Windows.Forms.Button();
            this.btnManageAppointments = new System.Windows.Forms.Button();
            this.lblViewReports = new System.Windows.Forms.Label();
            this.btnAppointmentTypesByMonth = new System.Windows.Forms.Button();
            this.btnScheduleForUser = new System.Windows.Forms.Button();
            this.btnThirdReport = new System.Windows.Forms.Button();

            // 
            // lblSelectOperation
            // 
            this.lblSelectOperation.AutoSize = true;
            this.lblSelectOperation.Location = new System.Drawing.Point(50, 20);
            this.lblSelectOperation.Name = "lblSelectOperation";
            this.lblSelectOperation.Size = new System.Drawing.Size(100, 13);
            this.lblSelectOperation.TabIndex = 0;
            this.lblSelectOperation.Text = "Select an Operation";

            // 
            // btnManageCustomer
            // 
            this.btnManageCustomer.BackColor = System.Drawing.Color.Green;
            this.btnManageCustomer.ForeColor = System.Drawing.Color.White;
            this.btnManageCustomer.Location = new System.Drawing.Point(50, 60);
            this.btnManageCustomer.Name = "btnManageCustomer";
            this.btnManageCustomer.Size = new System.Drawing.Size(120, 23);
            this.btnManageCustomer.TabIndex = 1;
            this.btnManageCustomer.Text = "Manage Customer";
            this.btnManageCustomer.UseVisualStyleBackColor = false;

            // 
            // btnManageAppointments
            // 
            this.btnManageAppointments.BackColor = System.Drawing.Color.Blue;
            this.btnManageAppointments.ForeColor = System.Drawing.Color.White;
            this.btnManageAppointments.Location = new System.Drawing.Point(180, 60);
            this.btnManageAppointments.Name = "btnManageAppointments";
            this.btnManageAppointments.Size = new System.Drawing.Size(140, 23);
            this.btnManageAppointments.TabIndex = 2;
            this.btnManageAppointments.Text = "Manage Appointments";
            this.btnManageAppointments.UseVisualStyleBackColor = false;

            this.btnCalendarView = new System.Windows.Forms.Button();
            this.btnCalendarView.BackColor = System.Drawing.Color.Orange;
            this.btnCalendarView.ForeColor = System.Drawing.Color.White;
            this.btnCalendarView.Location = new System.Drawing.Point(330, 60); // Adjust the location as needed
            this.btnCalendarView.Name = "btnCalendarView";
            this.btnCalendarView.Size = new System.Drawing.Size(140, 23); // Adjust the size as needed
            this.btnCalendarView.TabIndex = 7;
            this.btnCalendarView.Text = "Calendar View";
            this.btnCalendarView.UseVisualStyleBackColor = false;
            this.btnCalendarView.Click += new System.EventHandler(this.BtnCalendarView_Click);
            this.Controls.Add(this.btnCalendarView);

            // 
            // lblViewReports
            // 
            this.lblViewReports.AutoSize = true;
            this.lblViewReports.Location = new System.Drawing.Point(50, 100);
            this.lblViewReports.Name = "lblViewReports";
            this.lblViewReports.Size = new System.Drawing.Size(70, 13);
            this.lblViewReports.TabIndex = 3;
            this.lblViewReports.Text = "View Reports";

            // 
            // btnAppointmentTypesByMonth
            // 
            this.btnAppointmentTypesByMonth.Location = new System.Drawing.Point(50, 140);
            this.btnAppointmentTypesByMonth.Name = "btnAppointmentTypesByMonth";
            this.btnAppointmentTypesByMonth.Size = new System.Drawing.Size(220, 23);
            this.btnAppointmentTypesByMonth.TabIndex = 4;
            this.btnAppointmentTypesByMonth.Text = "Number of Appointment Types by Month";
            this.btnAppointmentTypesByMonth.UseVisualStyleBackColor = true;
            this.btnAppointmentTypesByMonth.Click += new System.EventHandler(this.BtnAppointmentTypesByMonth_Click);

            // 
            // btnScheduleForUser
            // 
            this.btnScheduleForUser.Location = new System.Drawing.Point(280, 140);
            this.btnScheduleForUser.Name = "btnScheduleForUser";
            this.btnScheduleForUser.Size = new System.Drawing.Size(140, 23);
            this.btnScheduleForUser.TabIndex = 5;
            this.btnScheduleForUser.Text = "Schedule for Each User";
            this.btnScheduleForUser.UseVisualStyleBackColor = true;
            this.btnScheduleForUser.Click += new System.EventHandler(this.BtnScheduleForUser_Click);

            // 
            // btnThirdReport
            // 
            this.btnThirdReport.Location = new System.Drawing.Point(430, 140);
            this.btnThirdReport.Name = "btnThirdReport";
            this.btnThirdReport.Size = new System.Drawing.Size(190, 23);
            this.btnThirdReport.TabIndex = 6;
            this.btnThirdReport.Text = "Appointments Duration by Type";
            this.btnThirdReport.UseVisualStyleBackColor = true;
            this.btnThirdReport.Click += new System.EventHandler(this.BtnThirdReport_Click);

            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblSelectOperation);
            this.Controls.Add(this.btnManageCustomer);
            this.Controls.Add(this.btnManageAppointments);
            this.Controls.Add(this.lblViewReports);
            this.Controls.Add(this.btnAppointmentTypesByMonth);
            this.Controls.Add(this.btnScheduleForUser);
            this.Controls.Add(this.btnThirdReport);
            this.Text = "Main Form";
        }

        #endregion
    }
}
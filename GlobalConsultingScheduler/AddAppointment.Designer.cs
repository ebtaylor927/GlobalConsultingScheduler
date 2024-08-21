using System;
using System.Windows.Forms;

namespace GlobalConsultingScheduler
{
    partial class AddAppointment
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
        /// 
        private System.Windows.Forms.TextBox txtCustomerId;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.TextBox txtContact;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Button btnSave;
        private void InitializeComponent()
        {
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Text = "Add Appointment";

            // Customer ID
            Label lblCustomerId = new Label();
            lblCustomerId.Text = "Customer ID:";
            lblCustomerId.Location = new System.Drawing.Point(50, 50);
            lblCustomerId.AutoSize = true;
            this.Controls.Add(lblCustomerId);

            this.txtCustomerId = new System.Windows.Forms.TextBox();
            this.txtCustomerId.Location = new System.Drawing.Point(200, 50);
            this.Controls.Add(this.txtCustomerId);

            // User ID
            Label lblUserId = new Label();
            lblUserId.Text = "User ID:";
            lblUserId.Location = new System.Drawing.Point(50, 90);
            lblUserId.AutoSize = true;
            this.Controls.Add(lblUserId);

            this.txtUserId = new System.Windows.Forms.TextBox();
            this.txtUserId.Location = new System.Drawing.Point(200, 90);
            this.Controls.Add(this.txtUserId);

            // Title
            Label lblTitle = new Label();
            lblTitle.Text = "Title:";
            lblTitle.Location = new System.Drawing.Point(50, 130);
            lblTitle.AutoSize = true;
            this.Controls.Add(lblTitle);

            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtTitle.Location = new System.Drawing.Point(200, 130);
            this.Controls.Add(this.txtTitle);

            // Description
            Label lblDescription = new Label();
            lblDescription.Text = "Description:";
            lblDescription.Location = new System.Drawing.Point(50, 170);
            lblDescription.AutoSize = true;
            this.Controls.Add(lblDescription);

            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtDescription.Location = new System.Drawing.Point(200, 170);
            this.txtDescription.Size = new System.Drawing.Size(200, 60);
            this.txtDescription.Multiline = true;
            this.Controls.Add(this.txtDescription);

            // Location
            Label lblLocation = new Label();
            lblLocation.Text = "Location:";
            lblLocation.Location = new System.Drawing.Point(50, 240);
            lblLocation.AutoSize = true;
            this.Controls.Add(lblLocation);

            this.txtLocation = new System.Windows.Forms.TextBox();
            this.txtLocation.Location = new System.Drawing.Point(200, 240);
            this.Controls.Add(this.txtLocation);

            // Contact
            Label lblContact = new Label();
            lblContact.Text = "Contact:";
            lblContact.Location = new System.Drawing.Point(50, 280);
            lblContact.AutoSize = true;
            this.Controls.Add(lblContact);

            this.txtContact = new System.Windows.Forms.TextBox();
            this.txtContact.Location = new System.Drawing.Point(200, 280);
            this.Controls.Add(this.txtContact);

            // Type
            Label lblType = new Label();
            lblType.Text = "Type:";
            lblType.Location = new System.Drawing.Point(50, 320);
            lblType.AutoSize = true;
            this.Controls.Add(lblType);

            this.txtType = new System.Windows.Forms.TextBox();
            this.txtType.Location = new System.Drawing.Point(200, 320);
            this.Controls.Add(this.txtType);

            // URL
            Label lblUrl = new Label();
            lblUrl.Text = "URL:";
            lblUrl.Location = new System.Drawing.Point(50, 360);
            lblUrl.AutoSize = true;
            this.Controls.Add(lblUrl);

            this.txtUrl = new System.Windows.Forms.TextBox();
            this.txtUrl.Location = new System.Drawing.Point(200, 360);
            this.Controls.Add(this.txtUrl);

            // Start Time
            Label lblStart = new Label();
            lblStart.Text = "Start Time:";
            lblStart.Location = new System.Drawing.Point(50, 400);
            lblStart.AutoSize = true;
            this.Controls.Add(lblStart);

            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.dtpStart.Location = new System.Drawing.Point(200, 400);
            this.dtpStart.Format = DateTimePickerFormat.Custom;
            this.dtpStart.CustomFormat = "yyyy-MM-dd HH:mm";
            this.Controls.Add(this.dtpStart);

            // End Time
            Label lblEnd = new Label();
            lblEnd.Text = "End Time:";
            lblEnd.Location = new System.Drawing.Point(50, 440);
            lblEnd.AutoSize = true;
            this.Controls.Add(lblEnd);

            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpEnd.Location = new System.Drawing.Point(200, 440);
            this.dtpEnd.Format = DateTimePickerFormat.Custom;
            this.dtpEnd.CustomFormat = "yyyy-MM-dd HH:mm";
            this.Controls.Add(this.dtpEnd);

            this.btnSave = new System.Windows.Forms.Button();
            this.btnSave.Text = "Save";
            this.btnSave.Location = new System.Drawing.Point(200, 480);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.Controls.Add(this.btnSave);

        }


        #endregion
    }
}
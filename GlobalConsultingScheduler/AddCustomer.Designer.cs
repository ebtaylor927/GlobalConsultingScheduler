using System;
using System.Windows.Forms;


namespace GlobalConsultingScheduler
{
    partial class AddCustomer
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
        // UI components declaration
        private System.Windows.Forms.Label lblAddCustomer;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtAddress2;
        private System.Windows.Forms.TextBox txtPostalCode;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.TextBox txtCountry;
        private System.Windows.Forms.Button btnSave;

        private void InitializeComponent()
        {
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Add Customer";

            this.lblAddCustomer = new System.Windows.Forms.Label();
            this.lblAddCustomer.Location = new System.Drawing.Point(50, 20);
            this.lblAddCustomer.Size = new System.Drawing.Size(200, 30);
            this.lblAddCustomer.Text = "Add Customer";
            this.Controls.Add(this.lblAddCustomer);

            // Name
            Label lblName = new Label();
            lblName.Text = "Name:";
            lblName.Location = new System.Drawing.Point(50, 60);
            lblName.AutoSize = true;
            this.Controls.Add(lblName);

            this.txtName = new System.Windows.Forms.TextBox();
            this.txtName.Location = new System.Drawing.Point(150, 60);
            this.txtName.Size = new System.Drawing.Size(200, 20);
            this.Controls.Add(this.txtName);

            // Address
            Label lblAddress = new Label();
            lblAddress.Text = "Address:";
            lblAddress.Location = new System.Drawing.Point(50, 100);
            lblAddress.AutoSize = true;
            this.Controls.Add(lblAddress);

            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtAddress.Location = new System.Drawing.Point(150, 100);
            this.txtAddress.Size = new System.Drawing.Size(200, 20);
            this.Controls.Add(this.txtAddress);

            // Address2
            Label lblAddress2 = new Label();
            lblAddress2.Text = "Address 2:";
            lblAddress2.Location = new System.Drawing.Point(50, 140);
            lblAddress2.AutoSize = true;
            this.Controls.Add(lblAddress2);

            this.txtAddress2 = new System.Windows.Forms.TextBox();
            this.txtAddress2.Location = new System.Drawing.Point(150, 140);
            this.txtAddress2.Size = new System.Drawing.Size(200, 20);
            this.Controls.Add(this.txtAddress2);

            // PostalCode
            Label lblPostalCode = new Label();
            lblPostalCode.Text = "Postal Code:";
            lblPostalCode.Location = new System.Drawing.Point(50, 180);
            lblPostalCode.AutoSize = true;
            this.Controls.Add(lblPostalCode);

            this.txtPostalCode = new System.Windows.Forms.TextBox();
            this.txtPostalCode.Location = new System.Drawing.Point(150, 180);
            this.txtPostalCode.Size = new System.Drawing.Size(200, 20);
            this.Controls.Add(this.txtPostalCode);

            // Phone
            Label lblPhone = new Label();
            lblPhone.Text = "Phone:";
            lblPhone.Location = new System.Drawing.Point(50, 220);
            lblPhone.AutoSize = true;
            this.Controls.Add(lblPhone);

            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtPhone.Location = new System.Drawing.Point(150, 220);
            this.txtPhone.Size = new System.Drawing.Size(200, 20);
            this.txtPhone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPhone_KeyPress);
            this.Controls.Add(this.txtPhone);

            // City
            Label lblCity = new Label();
            lblCity.Text = "City:";
            lblCity.Location = new System.Drawing.Point(50, 260);
            lblCity.AutoSize = true;
            this.Controls.Add(lblCity);

            this.txtCity = new System.Windows.Forms.TextBox();
            this.txtCity.Location = new System.Drawing.Point(150, 260);
            this.txtCity.Size = new System.Drawing.Size(200, 20);
            this.Controls.Add(this.txtCity);

            // Country
            Label lblCountry = new Label();
            lblCountry.Text = "Country:";
            lblCountry.Location = new System.Drawing.Point(50, 300);
            lblCountry.AutoSize = true;
            this.Controls.Add(lblCountry);

            this.txtCountry = new System.Windows.Forms.TextBox();
            this.txtCountry.Location = new System.Drawing.Point(150, 300);
            this.txtCountry.Size = new System.Drawing.Size(200, 20);
            this.Controls.Add(this.txtCountry);

            // Save Button
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSave.Location = new System.Drawing.Point(50, 340);
            this.btnSave.Text = "Save";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.Controls.Add(this.btnSave);
        }



        #endregion
    }
}
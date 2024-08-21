using System;

namespace GlobalConsultingScheduler
{
    partial class CustomerPage
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
        private System.Windows.Forms.Button btnAddCustomer;
        private System.Windows.Forms.DataGridView dgvCustomers;
        private System.Windows.Forms.Button btnHome;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 450);
            this.Text = "CustomerPage";

            this.btnHome = new System.Windows.Forms.Button();
            this.btnHome.Location = new System.Drawing.Point(680, 10);
            this.btnHome.Size = new System.Drawing.Size(110, 30);
            this.btnHome.Text = "Home";
            this.btnHome.UseVisualStyleBackColor = true;
            this.Controls.Add(this.btnHome);
            this.btnHome.Click += new EventHandler(this.BtnHome_Click);

            this.btnAddCustomer = new System.Windows.Forms.Button();
            this.btnAddCustomer.Location = new System.Drawing.Point(10, 10);
            this.btnAddCustomer.Size = new System.Drawing.Size(120, 30);
            this.btnAddCustomer.Text = "Add Customer";
            this.btnAddCustomer.BackColor = System.Drawing.Color.Green;
            this.btnAddCustomer.ForeColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnAddCustomer);

            this.dgvCustomers = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).BeginInit();
            this.SuspendLayout();
            
            this.dgvCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCustomers.Location = new System.Drawing.Point(10, 50);
            this.dgvCustomers.Name = "dgvCustomers";
            this.dgvCustomers.Size = new System.Drawing.Size(780, 390);
            this.dgvCustomers.TabIndex = 1;
            this.Controls.Add(this.dgvCustomers);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).EndInit();
            this.ResumeLayout(false);
        }


        #endregion
    }
}
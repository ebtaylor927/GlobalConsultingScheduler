using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GlobalConsultingScheduler
{
    public partial class CustomerPage : Form
    {
        public CustomerPage()
        {
            InitializeComponent();
            InitializeCustomerDataGridView();
            dgvCustomers.CellContentClick += new DataGridViewCellEventHandler(dgvCustomers_CellContentClick);
            dgvCustomers.Width = 950;
            this.btnAddCustomer.Click += new EventHandler(this.btnAddCustomer_Click);

        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            AddCustomer addCustomerForm = new AddCustomer();
            addCustomerForm.ShowDialog();
            dgvCustomers.DataSource = GetData();
        }


        private void InitializeCustomerDataGridView()
        {
            dgvCustomers.AutoGenerateColumns = true;

            dgvCustomers.DataSource = GetData();
            dgvCustomers.Columns["addressId"].Visible = false;
            dgvCustomers.Columns["cityId"].Visible = false;
            dgvCustomers.Columns["countryId"].Visible = false;


            // Add Update and Delete buttons
            AddActionButton("Update", Color.Green, Color.White);
            AddActionButton("Delete", Color.Red, Color.White);
        }

        private DataTable GetData()
        {
            DataTable dataTable = new DataTable();
            string connectionString = DatabaseConfig.ConnectionString;

            // SQL query to fetch customer data along with address, city, and country
            string query = @"
            SELECT 
                c.customerId AS 'Customer ID', 
                c.customerName AS 'Name', 
                a.address AS 'Address', 
                ci.city AS 'City', 
                co.country AS 'Country', 
                a.phone AS 'Phone', 
                c.active AS 'Active',
                c.addressId, ci.cityId, co.countryId
            FROM customer c
            JOIN address a ON c.addressId = a.addressId
            JOIN city ci ON a.cityId = ci.cityId
            JOIN country co ON ci.countryId = co.countryId;";


            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    conn.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching data: " + ex.Message);
                }
            }

            return dataTable;
        }
        private void dgvCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                // Check if it's the Update button
                if (senderGrid.Columns[e.ColumnIndex].Name == "UpdateColumn")
                {
                    // Execute the update logic
                    UpdateCustomerRecord(e.RowIndex);
                }
                else if (senderGrid.Columns[e.ColumnIndex].Name == "DeleteColumn")
                {
                    // Execute the delete logic
                    int customerId = Convert.ToInt32(senderGrid.Rows[e.RowIndex].Cells["Customer ID"].Value);
                    DeleteCustomerRecord(customerId);

                    // Refresh the DataGridView to reflect the deletion
                    dgvCustomers.DataSource = GetData();
                }
            }
        }

        private void UpdateCustomerRecord(int rowIndex)
        {
            var row = dgvCustomers.Rows[rowIndex];
            int customerId = Convert.ToInt32(row.Cells["Customer ID"].Value);
            string name = Convert.ToString(row.Cells["Name"].Value).Trim();
            string address = Convert.ToString(row.Cells["Address"].Value).Trim();
            string city = Convert.ToString(row.Cells["City"].Value).Trim();
            string country = Convert.ToString(row.Cells["Country"].Value).Trim();
            string phone = Convert.ToString(row.Cells["Phone"].Value).Trim();
            bool active = Convert.ToBoolean(row.Cells["Active"].Value);

            // Additional ID fields for linking
            int addressId = Convert.ToInt32(row.Cells["addressId"].Value);
            int cityId = Convert.ToInt32(row.Cells["cityId"].Value);
            int countryId = Convert.ToInt32(row.Cells["countryId"].Value);

            // Perform validation before updating
            if (!ValidateCustomerInput(name, address, phone))
            {
                return;
            }

            // Proceed with updates if validation succeeds
            bool success = true;
            success &= UpdateCustomer(customerId, name, active);
            success &= UpdateAddress(addressId, address, phone);
            success &= UpdateCity(cityId, city);
            success &= UpdateCountry(countryId, country);

            if (success)
            {
                MessageBox.Show("All changes saved successfully.", "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("One or more changes failed to save.", "Update Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateCustomerInput(string name, string address, string phone)
        {
            // Check for null or whitespace in required fields
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Name, Address, and Phone number fields must not be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            name = name.Trim();
            address = address.Trim();
            phone = phone.Trim();

            // Validate phone number format (digits and dashes only)
            if (!System.Text.RegularExpressions.Regex.IsMatch(phone, @"^[0-9-]+$"))
            {
                MessageBox.Show("Phone number must contain only digits and dashes.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true; // All validations passed
        }

        private bool ExecuteNonQuery(string query, Dictionary<string, object> parameters)
        {
            string connectionString = DatabaseConfig.ConnectionString;
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        foreach (var param in parameters)
                        {
                            cmd.Parameters.AddWithValue(param.Key, param.Value);
                        }
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private bool UpdateCustomer(int customerId, string name, bool active)
        {
            DateTime utcNow = DateTime.UtcNow; // Current UTC time

            string query = @"
        UPDATE customer 
        SET 
            customerName = @name, 
            active = @active,
            lastUpdate = @utcNow, 
            lastUpdateBy = @username 
        WHERE customerId = @customerId";
            return ExecuteNonQuery(query, new Dictionary<string, object>
    {
        {"@customerId", customerId},
        {"@name", name},
        {"@active", active},
        {"@utcNow", utcNow},
        {"@username", SessionManager.LoggedInUser}
    });
        }

        private bool UpdateAddress(int addressId, string address, string phone)
        {
            DateTime utcNow = DateTime.UtcNow; // Current UTC time

            string query = @"
        UPDATE address 
        SET 
            address = @address, 
            phone = @phone,
            lastUpdate = @utcNow, 
            lastUpdateBy = @username 
        WHERE addressId = @addressId";
            return ExecuteNonQuery(query, new Dictionary<string, object>
    {
        {"@addressId", addressId},
        {"@address", address},
        {"@phone", phone},
        {"@utcNow", utcNow},
        {"@username", SessionManager.LoggedInUser}
    });
        }

        private bool UpdateCity(int cityId, string city)
        {
            DateTime utcNow = DateTime.UtcNow; // Current UTC time

            string query = @"
        UPDATE city 
        SET 
            city = @city,
            lastUpdate = @utcNow, 
            lastUpdateBy = @username 
        WHERE cityId = @cityId";
            return ExecuteNonQuery(query, new Dictionary<string, object>
    {
        {"@cityId", cityId},
        {"@city", city},
        {"@utcNow", utcNow},
        {"@username", SessionManager.LoggedInUser}
    });
        }

        private bool UpdateCountry(int countryId, string country)
        {
            DateTime utcNow = DateTime.UtcNow; // Current UTC time

            string query = @"
        UPDATE country 
        SET 
            country = @country,
            lastUpdate = @utcNow, 
            lastUpdateBy = @username 
        WHERE countryId = @countryId";
            return ExecuteNonQuery(query, new Dictionary<string, object>
    {
        {"@countryId", countryId},
        {"@country", country},
        {"@utcNow", utcNow},
        {"@username", SessionManager.LoggedInUser}
    });
        }


        private void DeleteCustomerRecord(int customerId)
        {
            string connectionString = DatabaseConfig.ConnectionString;
            string query = "DELETE FROM customer WHERE customerId = @customerId";

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@customerId", customerId);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Customer record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No record found to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Failed to delete the customer record. There may be related records in other tables that prevent deletion.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while deleting the customer record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void BtnHome_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void AddActionButton(string buttonText, Color backColor, Color foreColor)
        {
            DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
            btnColumn.Name = buttonText + "Column";
            btnColumn.HeaderText = "";
            btnColumn.Text = buttonText;
            btnColumn.UseColumnTextForButtonValue = true;
            btnColumn.FlatStyle = FlatStyle.Flat;
            btnColumn.CellTemplate.Style.BackColor = backColor;
            btnColumn.CellTemplate.Style.ForeColor = foreColor;
            dgvCustomers.Columns.Add(btnColumn);
        }
    }
}

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
    public partial class AddCustomer : Form

    {
        private string connectionString = DatabaseConfig.ConnectionString;
        public AddCustomer()
        {
            InitializeComponent();
        }
        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string address = txtAddress.Text.Trim();
            string address2 = txtAddress2.Text.Trim();
            string postalCode = txtPostalCode.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string city = txtCity.Text.Trim();
            string country = txtCountry.Text.Trim();

            // Validation
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(city) || string.IsNullOrEmpty(country))
            {
                MessageBox.Show("Name, Address, Phone, City, and Country are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate phone number format (only digits and dashes)
            if (!System.Text.RegularExpressions.Regex.IsMatch(phone, @"^[0-9-]+$"))
            {
                MessageBox.Show("Phone number must contain only digits and dashes.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            try
            {
                int countryId = SaveOrGetCountryId(country, SessionManager.LoggedInUser);
                int cityId = SaveOrGetCityId(city, countryId, SessionManager.LoggedInUser);
                int addressId = SaveAddress(address, address2, cityId, postalCode, phone, SessionManager.LoggedInUser);
                bool success = SaveCustomer(name, addressId, true, SessionManager.LoggedInUser);

                if (success)
                {
                    MessageBox.Show("Customer added successfully.");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to add customer.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
        private int SaveOrGetCountryId(string country, string username)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                // Check if country exists
                using (var cmd = new MySqlCommand("SELECT countryId FROM country WHERE country = @country", conn))
                {
                    cmd.Parameters.AddWithValue("@country", country);
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                }

                // Get current UTC time
                DateTime utcNow = DateTime.UtcNow;

                // Insert new country with UTC times for createDate and lastUpdate
                using (var cmd = new MySqlCommand("INSERT INTO country (country, createDate, createdBy, lastUpdate, lastUpdateBy) VALUES (@country, @utcNow, @username, @utcNow, @username); SELECT LAST_INSERT_ID();", conn))
                {
                    cmd.Parameters.AddWithValue("@country", country);
                    cmd.Parameters.AddWithValue("@utcNow", utcNow);
                    cmd.Parameters.AddWithValue("@username", username);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        private int SaveOrGetCityId(string city, int countryId, string username)
        {
            DateTime utcNow = DateTime.UtcNow;

            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                // Check if city exists
                using (var cmd = new MySqlCommand("SELECT cityId FROM city WHERE city = @city AND countryId = @countryId", conn))
                {
                    cmd.Parameters.AddWithValue("@city", city);
                    cmd.Parameters.AddWithValue("@countryId", countryId);
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                }

                // Insert new city with UTC times
                using (var cmd = new MySqlCommand("INSERT INTO city (city, countryId, createDate, createdBy, lastUpdate, lastUpdateBy) VALUES (@city, @countryId, @utcNow, @username, @utcNow, @username); SELECT LAST_INSERT_ID();", conn))
                {
                    cmd.Parameters.AddWithValue("@city", city);
                    cmd.Parameters.AddWithValue("@countryId", countryId);
                    cmd.Parameters.AddWithValue("@utcNow", utcNow);
                    cmd.Parameters.AddWithValue("@username", username);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        private int SaveAddress(string address, string address2, int cityId, string postalCode, string phone, string username)
        {
            DateTime utcNow = DateTime.UtcNow;

            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("INSERT INTO address (address, address2, cityId, postalCode, phone, createDate, createdBy, lastUpdate, lastUpdateBy) VALUES (@address, @address2, @cityId, @postalCode, @phone, @utcNow, @username, @utcNow, @username); SELECT LAST_INSERT_ID();", conn))
                {
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@address2", address2);
                    cmd.Parameters.AddWithValue("@cityId", cityId);
                    cmd.Parameters.AddWithValue("@postalCode", postalCode);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@utcNow", utcNow);
                    cmd.Parameters.AddWithValue("@username", username);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        private bool SaveCustomer(string name, int addressId, bool isActive, string username)
        {
            DateTime utcNow = DateTime.UtcNow;

            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("INSERT INTO customer (customerName, addressId, active, createDate, createdBy, lastUpdate, lastUpdateBy) VALUES (@name, @addressId, @isActive, @utcNow, @username, @utcNow, @username)", conn))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@addressId", addressId);
                    cmd.Parameters.AddWithValue("@isActive", isActive);
                    cmd.Parameters.AddWithValue("@utcNow", utcNow);
                    cmd.Parameters.AddWithValue("@username", username);
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }


    }
}

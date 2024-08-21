using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GlobalConsultingScheduler
{
    public partial class AddAppointment : Form
    {
        private string connectionString = DatabaseConfig.ConnectionString;
        public AddAppointment()
        {
            InitializeComponent();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text.Trim()) || string.IsNullOrWhiteSpace(txtCustomerId.Text.Trim()) || string.IsNullOrWhiteSpace(txtUserId.Text.Trim()) || string.IsNullOrWhiteSpace(txtType.Text.Trim()))
            {
                MessageBox.Show("Customer ID, User ID, Title, and Type are required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int customerId = Convert.ToInt32(txtCustomerId.Text.Trim());
            int userId = Convert.ToInt32(txtUserId.Text.Trim());
            string title = txtTitle.Text.Trim();
            string description = txtDescription.Text.Trim();
            string location = txtLocation.Text.Trim();
            string contact = txtContact.Text.Trim();
            string type = txtType.Text.Trim();
            string url = txtUrl.Text.Trim();
            DateTime start = dtpStart.Value;
            DateTime end = dtpEnd.Value;

            // Convert `start` and `end` to EST and validate business hours (9:00 AM to 5:00 PM, Monday to Friday)
            TimeZoneInfo estZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime startEst = TimeZoneInfo.ConvertTime(start, estZone);
            DateTime endEst = TimeZoneInfo.ConvertTime(end, estZone);

            // Validation logic
            if (!IsWithinBusinessHours(startEst, endEst))
            {
                MessageBox.Show("Appointment times must be within business hours (9:00 AM to 5:00 PM EST, Monday to Friday).");
                return;
            }

            
            DateTime startUtc = TimeZoneInfo.ConvertTimeToUtc(start, TimeZoneInfo.Local);
            DateTime endUtc = TimeZoneInfo.ConvertTimeToUtc(end, TimeZoneInfo.Local);

            if (HasOverlappingAppointments(0, customerId, startUtc, endUtc))
            {
                MessageBox.Show("This appointment overlaps with another appointment.");
                return;
            }

            // Current UTC time for createDate and lastUpdate
            DateTime utcNow = DateTime.UtcNow;

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                    INSERT INTO appointment 
                    (customerId, userId, title, description, location, contact, type, url, start, end, createDate, createdBy, lastUpdate, lastUpdateBy)
                    VALUES 
                    (@customerId, @userId, @title, @description, @location, @contact, @type, @url, @start, @end, @createDate, @loggedInUser, @lastUpdate, @loggedInUser);
                ";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        // Bind parameters
                        cmd.Parameters.AddWithValue("@customerId", customerId);
                        cmd.Parameters.AddWithValue("@userId", userId);
                        cmd.Parameters.AddWithValue("@title", title);
                        cmd.Parameters.AddWithValue("@description", description);
                        cmd.Parameters.AddWithValue("@location", location);
                        cmd.Parameters.AddWithValue("@contact", contact);
                        cmd.Parameters.AddWithValue("@type", type);
                        cmd.Parameters.AddWithValue("@url", url);
                        cmd.Parameters.AddWithValue("@start", startUtc);
                        cmd.Parameters.AddWithValue("@end", endUtc);
                        cmd.Parameters.AddWithValue("@createDate", utcNow);
                        cmd.Parameters.AddWithValue("@lastUpdate", utcNow);
                        cmd.Parameters.AddWithValue("@loggedInUser", SessionManager.LoggedInUser);

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Appointment added successfully.");
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed to add appointment.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool IsWithinBusinessHours(DateTime start, DateTime end)
        {
            DayOfWeek day = start.DayOfWeek;
            bool isWeekday = day >= DayOfWeek.Monday && day <= DayOfWeek.Friday;
            bool isWithinHours = start.TimeOfDay >= new TimeSpan(9, 0, 0) && end.TimeOfDay <= new TimeSpan(17, 0, 0);
            return isWeekday && isWithinHours;
        }

        private bool HasOverlappingAppointments(int appointmentId, int customerId, DateTime start, DateTime end)
        {

            string connectionString = DatabaseConfig.ConnectionString;
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                SELECT COUNT(*)
                FROM appointment
                WHERE customerId = @customerId AND appointmentId != @appointmentId AND
                ((start <= @start AND end > @start) OR
                (start < @end AND end >= @end) OR
                (start >= @start AND end <= @end))";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@customerId", customerId);
                    cmd.Parameters.AddWithValue("@appointmentId", appointmentId);
                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@end", end);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

    }
}

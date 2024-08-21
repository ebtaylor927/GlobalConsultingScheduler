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
    public partial class AppointmentsPage : Form
    {
        public AppointmentsPage()
        {
            InitializeComponent();
            InitializeAppointmentDataGridView();
            dgvAppointments.CellContentClick += new DataGridViewCellEventHandler(dgvAppointments_CellContentClick);
            this.btnAddAppointment.Click += new EventHandler(this.btnAddAppointment_Click);
        }
        private void btnAddAppointment_Click(object sender, EventArgs e)
        {
            AddAppointment addAppointmentForm = new AddAppointment();
            addAppointmentForm.ShowDialog();
            dgvAppointments.DataSource = GetData();
        }
        private void BtnHome_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void InitializeAppointmentDataGridView()
        {
            // Configure DataGridView to automatically generate columns
            dgvAppointments.AutoGenerateColumns = true;

            // Assuming GetData method fetches data as DataTable
            dgvAppointments.DataSource = GetData();

            // Add Update and Delete buttons
            AddActionButton("Update", Color.Green, Color.White);
            AddActionButton("Delete", Color.Red, Color.White);
        }
        private DataTable GetData()
        {
            DataTable dataTable = new DataTable();
            string connectionString = DatabaseConfig.ConnectionString;

            string query = @"
    SELECT 
        appointmentId, customerId, userId, title, description, 
        location, contact, type, url, start, end 
    FROM appointment;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    conn.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dataTable);

                    // Convert start and end times from UTC to local time
                    foreach (DataRow row in dataTable.Rows)
                    {
                        row["start"] = TimeZoneInfo.ConvertTimeFromUtc((DateTime)row["start"], TimeZoneInfo.Local);
                        row["end"] = TimeZoneInfo.ConvertTimeFromUtc((DateTime)row["end"], TimeZoneInfo.Local);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching data: " + ex.Message);
                }
            }

            return dataTable;
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
            dgvAppointments.Columns.Add(btnColumn);
        }
        private void dgvAppointments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                // Handle the Update button click
                if (senderGrid.Columns[e.ColumnIndex].Name == "UpdateColumn")
                {
                    UpdateAppointmentRecord(e.RowIndex);
                }
                // Handle the Delete button click
                else if (senderGrid.Columns[e.ColumnIndex].Name == "DeleteColumn")
                {
                    // Extract the appointmentId from the row where the Delete button was clicked
                    int appointmentId = Convert.ToInt32(senderGrid.Rows[e.RowIndex].Cells["appointmentId"].Value);
                    DeleteAppointment(appointmentId);
                }
            }
        }

        private void UpdateAppointmentRecord(int rowIndex)
        {
            string connectionString = DatabaseConfig.ConnectionString;

            var row = dgvAppointments.Rows[rowIndex];
            int appointmentId = Convert.ToInt32(row.Cells["appointmentId"].Value);
            int customerId = Convert.ToInt32(row.Cells["customerId"].Value);
            int userId = Convert.ToInt32(row.Cells["userId"].Value);
            string title = Convert.ToString(row.Cells["title"].Value);
            string description = Convert.ToString(row.Cells["description"].Value);
            string location = Convert.ToString(row.Cells["location"].Value);
            string contact = Convert.ToString(row.Cells["contact"].Value);
            string type = Convert.ToString(row.Cells["type"].Value);
            string url = Convert.ToString(row.Cells["url"].Value);
            DateTime startLocal = Convert.ToDateTime(row.Cells["start"].Value);
            DateTime endLocal = Convert.ToDateTime(row.Cells["end"].Value);

            // Assuming the local times are in Eastern Standard Time
            TimeZoneInfo estZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime startEst = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(startLocal, estZone.Id);
            DateTime endEst = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(endLocal, estZone.Id);

            // Validate business hours and overlapping appointments
            if (!IsWithinBusinessHours(startEst, endEst))
            {
                MessageBox.Show("Appointment times must be within business hours (9:00 AM to 5:00 PM EST, Monday to Friday).");
                return;
            }
            // Convert `start` and `end` from EST to UTC for storing
            DateTime startUtc = TimeZoneInfo.ConvertTimeToUtc(startLocal, TimeZoneInfo.Local);
            DateTime endUtc = TimeZoneInfo.ConvertTimeToUtc(endLocal, TimeZoneInfo.Local);

            if (HasOverlappingAppointments(appointmentId, customerId, startUtc, endUtc))
            {
                MessageBox.Show("This appointment overlaps with another appointment.");
                return;
            }

            

            // Update operation
            string updateQuery = @"UPDATE appointment SET 
                            customerId = @customerId, userId = @userId, title = @title, 
                            description = @description, location = @location, contact = @contact, 
                            type = @type, url = @url, start = @startUtc, end = @endUtc, 
                            lastUpdate = @lastUpdateUtc, lastUpdateBy = @lastUpdateBy 
                           WHERE appointmentId = @appointmentId";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                    {
                        // Assigning parameter values
                        cmd.Parameters.AddWithValue("@appointmentId", appointmentId);
                        cmd.Parameters.AddWithValue("@customerId", customerId);
                        cmd.Parameters.AddWithValue("@userId", userId);
                        cmd.Parameters.AddWithValue("@title", title);
                        cmd.Parameters.AddWithValue("@description", description);
                        cmd.Parameters.AddWithValue("@location", location);
                        cmd.Parameters.AddWithValue("@contact", contact);
                        cmd.Parameters.AddWithValue("@type", type);
                        cmd.Parameters.AddWithValue("@url", url);
                        cmd.Parameters.AddWithValue("@startUtc", startUtc);
                        cmd.Parameters.AddWithValue("@endUtc", endUtc);
                        cmd.Parameters.AddWithValue("@lastUpdateUtc", DateTime.UtcNow);
                        cmd.Parameters.AddWithValue("@lastUpdateBy", SessionManager.LoggedInUser);

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Appointment updated successfully.");
                            dgvAppointments.DataSource = GetData(); // Refresh the data grid view
                        }
                        else
                        {
                            MessageBox.Show("Failed to update appointment.");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Database error occurred: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
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
                // The SQL query checks for any appointments that overlap with the given time range
                // It excludes the current appointment being edited, if it exists (appointmentId > 0)
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
                    return count > 0; // Returns true if there are overlapping appointments
                }
            }
        }
        private void DeleteAppointment(int appointmentId)
        {
            string connectionString = DatabaseConfig.ConnectionString;
            string query = "DELETE FROM appointment WHERE appointmentId = @appointmentId";

            using (var conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@appointmentId", appointmentId);
                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Appointment deleted successfully.");
                            // Refresh the DataGridView to reflect the deletion
                            dgvAppointments.DataSource = GetData();
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete the appointment.");
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    // Here you can handle specific database exceptions if needed
                    MessageBox.Show($"A database error occurred: {ex.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
        }





    }
}

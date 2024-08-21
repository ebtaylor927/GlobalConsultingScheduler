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
    public partial class CalendarViewForm : Form
    {
        private string connectionString = DatabaseConfig.ConnectionString;
        public CalendarViewForm()
        {
            InitializeComponent();
        }
        private void BtnHome_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void MonthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            LoadAppointmentsForDate(e.Start);
        }

        private void LoadAppointmentsForDate(DateTime date)
        {
            DataTable dataTable = new DataTable();
            string query = @"
                SELECT 
                    appointmentId, customerId, userId, title, description, 
                    location, contact, type, url, start, end 
                FROM appointment
                WHERE DATE(start) = @date;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd"));
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    conn.Open();
                    adapter.Fill(dataTable);

                    dgvAppointments.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching appointments: " + ex.Message);
                }
            }
        }
    }
}

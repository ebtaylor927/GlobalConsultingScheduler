using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GlobalConsultingScheduler
{
    public partial class AppointmentTypeByMonth : Form
    {
        public AppointmentTypeByMonth()
        {
            InitializeComponent();
            LoadReportData();
        }
        private void BtnHome_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void LoadReportData()
        {
            string connectionString = DatabaseConfig.ConnectionString;
            var appointments = new List<AppointmentReportItem>();
            using (var conn = new MySqlConnection(connectionString))
            {
                try
                {
                    string query = @"
            SELECT 
                MONTH(start) AS 'MonthNumber',
                MONTHNAME(start) AS 'Month', 
                type, 
                COUNT(*) AS 'Total' 
            FROM appointment 
            GROUP BY MONTH(start), type 
            ORDER BY MONTH(start), type;";

                    var cmd = new MySqlCommand(query, conn);
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            appointments.Add(new AppointmentReportItem
                            {
                                Month = reader["Month"].ToString(),
                                Type = reader["Type"].ToString(),
                                Total = Convert.ToInt32(reader["Total"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading report data: " + ex.Message);
                    return;
                }
            }

            // Use the MonthNumber for ordering. Assuming appointments list has the month numbers.
            dgvReport.DataSource = appointments
                .OrderBy(a => DateTime.ParseExact(a.Month, "MMMM", CultureInfo.CurrentCulture).Month)
                .ThenBy(a => a.Type)
                .ToList();

            // Adjust DataGridView columns as needed
        }



    }
    public class AppointmentReportItem
    {
        public string Month { get; set; }
        public string Type { get; set; }
        public int Total { get; set; }
    }

}

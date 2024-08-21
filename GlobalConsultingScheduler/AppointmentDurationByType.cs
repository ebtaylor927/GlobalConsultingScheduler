using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GlobalConsultingScheduler
{
    public partial class AppointmentDurationByType : Form
    {
        public AppointmentDurationByType()
        {
            InitializeComponent();
            LoadAppointmentDurationsByType();
        }

        private void BtnHome_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadAppointmentDurationsByType()
        {
            string connectionString = DatabaseConfig.ConnectionString;
            List<AppointmentDurationType> durations = new List<AppointmentDurationType>();

            string query = @"
                SELECT 
                    type, 
                    SUM(TIMESTAMPDIFF(MINUTE, start, end)) AS TotalDuration
                FROM 
                    appointment
                GROUP BY 
                    type
                ORDER BY 
                    type;";

            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var duration = new AppointmentDurationType
                            {
                                Type = reader.GetString("type"),
                                TotalDuration = reader.GetInt32("TotalDuration")
                            };
                            durations.Add(duration);
                        }
                    }
                }
            }

            // Use a lambda expression to format or further process durations
            var formattedDurations = durations.Select(d => new
            {
                Type = d.Type,
                TotalDurationInHours = $"{d.TotalDuration / 60}h {d.TotalDuration % 60}m"
            }).ToList();

            dgvAppointmentDurations.DataSource = formattedDurations;
        }
    }

    public class AppointmentDurationType
    {
        public string Type { get; set; }
        public int TotalDuration { get; set; } // in minutes
    }
}

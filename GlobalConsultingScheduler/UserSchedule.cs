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
    public partial class UserSchedule : Form
    {
        public UserSchedule()
        {
            InitializeComponent();
            this.Load += UserSchedule_Load;
        }
        private void BtnHome_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void UserSchedule_Load(object sender, EventArgs e)
        {
            LoadUserSchedules();
        }

        private void LoadUserSchedules()
        {
            List<UserAppointment> userAppointments = FetchUserAppointments();
            var sortedAppointments = userAppointments.OrderBy(appointment => appointment.UserName).ThenBy(appointment => appointment.Start).ToList();

            dgvUserSchedule.DataSource = sortedAppointments;

            dgvUserSchedule.Columns["Start"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dgvUserSchedule.Columns["End"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
        }

        private List<UserAppointment> FetchUserAppointments()
        {
            var appointments = new List<UserAppointment>();
            string connectionString = DatabaseConfig.ConnectionString;

            string query = @"
            SELECT 
                u.userName AS UserName, 
                a.title AS Title, 
                a.description AS Description, 
                a.type AS Type, 
                a.start AS Start, 
                a.end AS End
            FROM 
                user u
                INNER JOIN appointment a ON u.userId = a.userId
            ORDER BY 
                u.userName, a.start;";

            using (var conn = new MySqlConnection(connectionString))
            {
                var cmd = new MySqlCommand(query, conn);
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Convert from UTC to local time here
                            var startUtc = Convert.ToDateTime(reader["Start"]);
                            var endUtc = Convert.ToDateTime(reader["End"]);
                            var startLocal = TimeZoneInfo.ConvertTimeFromUtc(startUtc, TimeZoneInfo.Local);
                            var endLocal = TimeZoneInfo.ConvertTimeFromUtc(endUtc, TimeZoneInfo.Local);

                            appointments.Add(new UserAppointment
                            {
                                UserName = reader["UserName"].ToString(),
                                Title = reader["Title"].ToString(),
                                Description = reader["Description"].ToString(),
                                Type = reader["Type"].ToString(),
                                Start = startLocal,
                                End = endLocal
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error fetching user schedules: {ex.Message}");
                }
            }

            return appointments;
        }


    }
    public class UserAppointment
    {
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }

}

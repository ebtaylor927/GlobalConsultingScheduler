using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Resources;
using MySql.Data.MySqlClient;
using System.Net;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.IO;

namespace GlobalConsultingScheduler
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            LocalizeUI();
            this.Load += LoginForm_Load;
            btnLogin.Click += new EventHandler(btnLogin_Click);
        }
        private void LocalizeUI()
        {
            var culture = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            var rm = new ResourceManager("GlobalConsultingScheduler.Messages", typeof(LoginForm).Assembly);

            this.Text = rm.GetString("LoginTitle", CultureInfo.CurrentUICulture);
            lblUsername.Text = rm.GetString("Username", CultureInfo.CurrentUICulture);
            lblPassword.Text = rm.GetString("Password", CultureInfo.CurrentUICulture);
            btnLogin.Text = rm.GetString("LoginButtonText", CultureInfo.CurrentUICulture);
        }
        private void DisplayUserLocation()
        {
            using (var client = new WebClient())
            {
                try
                {
                    string jsonResponse = client.DownloadString("https://ipinfo.io/json");
                    var serializer = new JavaScriptSerializer();
                    var jsonContent = serializer.Deserialize<Dictionary<string, string>>(jsonResponse);

                    if (jsonContent != null && jsonContent.ContainsKey("city") && jsonContent.ContainsKey("country"))
                    {
                        string location = $"Your location: {jsonContent["city"]}, {jsonContent["country"]}";
                        this.Invoke(new Action(() => lblLocation.Text = location));
                    }
                }
                catch (Exception ex)
                {
                    this.Invoke(new Action(() => lblLocation.Text = "Location unavailable"));
                }
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
       
            DisplayUserLocation();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string connectionString = DatabaseConfig.ConnectionString;
            int userId = 0;

            string query = "SELECT userId FROM user WHERE userName=@username AND password=@password";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Use parameters to prevent SQL injection
                        cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                        cmd.Parameters.AddWithValue("@password", txtPassword.Text);

                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            // Login is successful
                            userId = Convert.ToInt32(result);
                            SessionManager.LoggedInUser = txtUsername.Text;

                            // Record the login history
                            RecordLoginHistory(txtUsername.Text);

                            // Check for upcoming appointments
                            CheckForUpcomingAppointments(userId);

                            this.Hide();

                            MainForm mainForm = new MainForm();
                            mainForm.FormClosed += (senders, args) => Application.Exit();
                            
                            mainForm.Show();
                        }
                        else
                        {
                            // Login failed
                            DisplayLoginError();
                        }
                    }
                }
                catch (Exception ex)
                {
                    DisplayLoginError();
                }
            }
        }

        private void CheckForUpcomingAppointments(int userId)
        {
            string connectionString = DatabaseConfig.ConnectionString;
            DateTime nowUtc = DateTime.UtcNow;
            DateTime windowEnd = nowUtc.AddMinutes(15);

            string query = @"
        SELECT title, start 
        FROM appointment 
        WHERE userId = @userId 
        AND start BETWEEN @now AND @windowEnd;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@now", nowUtc);
                    cmd.Parameters.AddWithValue("@windowEnd", windowEnd);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            DateTime appointmentStart = (DateTime)reader["start"];
                            string title = reader["title"].ToString();

                            // Convert the appointment start time from UTC to local time
                            DateTime localStart = TimeZoneInfo.ConvertTimeFromUtc(appointmentStart, TimeZoneInfo.Local);

                            MessageBox.Show($"You have an upcoming appointment titled '{title}' at {localStart.ToString("g")}.", "Upcoming Appointment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        private void DisplayLoginError()
        {
            var rm = new ResourceManager("GlobalConsultingScheduler.Messages", typeof(LoginForm).Assembly);
            lblError.Text = rm.GetString("LoginError", CultureInfo.CurrentUICulture);
        }
        private void RecordLoginHistory(string username)
        {
            try
            {
                string projectRootPath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;

                string logFilePath = Path.Combine(projectRootPath, "Login_History.txt");

                // Prepare the log message
                string logMessage = $"Login Time: {DateTime.Now}, Username: {username}\n";

                // Append the log message to the file
                File.AppendAllText(logFilePath, logMessage);
            }
            catch (Exception ex)
            {
                // Optionally handle any exceptions
                Console.WriteLine($"An error occurred while writing to the login history file: {ex.Message}");
            }
        }




    }
    public static class SessionManager
    {
        public static string LoggedInUser { get; set; }
    }
}

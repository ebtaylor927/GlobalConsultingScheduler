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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            AttachEventHandlers();
        }
        private void AttachEventHandlers()
        {
            this.btnManageCustomer.Click += new EventHandler(this.BtnManageCustomer_Click);
            this.btnManageAppointments.Click += new EventHandler(this.BtnManageAppointments_Click);
        }

        private void BtnManageCustomer_Click(object sender, EventArgs e)
        {
            CustomerPage customerPage = new CustomerPage();
            this.Hide();
            customerPage.ShowDialog();
            this.Show();
        }
        private void BtnManageAppointments_Click(object sender, EventArgs e)
        {
            AppointmentsPage appointmentPage = new AppointmentsPage();
            this.Hide();
            appointmentPage.ShowDialog();
            this.Show();
        }
        private void BtnCalendarView_Click(object sender, EventArgs e)
        {
            CalendarViewForm calendarViewForm = new CalendarViewForm();
            this.Hide();
            calendarViewForm.ShowDialog();
            this.Show();
        }
        private void BtnAppointmentTypesByMonth_Click(object sender, EventArgs e)
        {
            AppointmentTypeByMonth appointmentTypeByMonth = new AppointmentTypeByMonth();
            this.Hide();
            appointmentTypeByMonth.ShowDialog();
            this.Show();
        }
        private void BtnScheduleForUser_Click(object sender, EventArgs e)
        {
            UserSchedule userSchedule = new UserSchedule();
            this.Hide();
            userSchedule.ShowDialog();
            this.Show();
        }
        private void BtnThirdReport_Click(object sender, EventArgs e)
        {
            AppointmentDurationByType appointmentDurationByType = new AppointmentDurationByType();
            this.Hide();
            appointmentDurationByType.ShowDialog();
            this.Show();
        }

    }
}

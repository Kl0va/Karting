using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karting
{
    public partial class RacerMenu : Form
    {
        public string Mail;
        public RacerMenu(string Email)
        {
            InitializeComponent();
            timer1.Start();
            Mail = Email;
        }

        private void Back_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime NowDate = DateTime.Now;
            DateTime StartRace = DateTime.Parse("2022-06-20 00:00");
            TimeSpan Span = StartRace.Subtract(NowDate);
            Timer.Text = $@"До начала события осталось {StartRace.Year - NowDate.Year} лет, {StartRace.Month - NowDate.Month} месяцев, {StartRace.Day - NowDate.Day} дней, {Span.ToString("hh")} часов, {Span.ToString("mm")} минут, {Span.ToString("ss")} секунд";
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Контакты \nТелефон: +7 999 999 99 99\nEmail: yugkyug@kartskills.org","Контакты");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RaceRegistration raceRegistration = new RaceRegistration(Mail);
            raceRegistration.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EditProfile editProfile = new EditProfile(Mail);
            editProfile.Show();
        }
    }
}

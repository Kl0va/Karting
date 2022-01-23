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
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
            TimerTim.Start();
        }

        private void TimerTim_Tick(object sender, EventArgs e)
        {
            DateTime NowDate = DateTime.Now;
            DateTime StartRace = DateTime.Parse("2022-06-20 00:00");
            TimeSpan Span = StartRace.Subtract(NowDate);
            Timer.Text = $@"До начала события осталось {StartRace.Year-NowDate.Year} лет, {StartRace.Month-NowDate.Month} месяцев, {StartRace.DayOfWeek-NowDate.DayOfWeek} дней, {Span.ToString("hh")} часов, {Span.ToString("mm")} минут, {Span.ToString("ss")} секунд";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Sponsorship sponsorship = new Sponsorship();
            sponsorship.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Details details = new Details();
            details.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}

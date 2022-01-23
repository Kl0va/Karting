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
    public partial class Examination : Form
    {
        public Examination()
        {
            InitializeComponent();
            timer1.Start();
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
            Timer.Text = $@"До начала события осталось {StartRace.Year - NowDate.Year} лет, {StartRace.Month - NowDate.Month} месяцев, {StartRace.DayOfWeek - NowDate.DayOfWeek} дней, {Span.ToString("hh")} часов, {Span.ToString("mm")} минут, {Span.ToString("ss")} секунд";
        }
    }
}

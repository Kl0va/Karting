﻿using System;
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
    public partial class Details : Form
    {
        public Details()
        {
            InitializeComponent();
            TimerTim.Start();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TimerTim_Tick(object sender, EventArgs e)
        {
            DateTime NowDate = DateTime.Now;
            DateTime StartRace = DateTime.Parse("2022-06-20 00:00");
            TimeSpan Span = StartRace.Subtract(NowDate);
            Timer.Text = $@"До начала события осталось {StartRace.Year - NowDate.Year} лет, {StartRace.Month - NowDate.Month} месяцев, {StartRace.DayOfWeek - NowDate.DayOfWeek} дней, {Span.ToString("hh")} часов, {Span.ToString("mm")} минут, {Span.ToString("ss")} секунд";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ListOfCharities listOfCharities = new ListOfCharities();
            listOfCharities.Show();
        }
    }
}
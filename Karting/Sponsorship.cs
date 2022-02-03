using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karting
{
    public partial class Sponsorship : Form
    {
        public static string ConnStr = @"Data Source=DESKTOP-4LPER1V\KLOVA;Initial Catalog=Karting;Persist Security Info=True;User ID=sa;Password=V783278k";
        public Sponsorship()
        {
            InitializeComponent();
            TimerTim.Start();
        }

        private void TimerTim_Tick(object sender, EventArgs e)
        {
            DateTime NowDate = DateTime.Now;
            DateTime StartRace = DateTime.Parse("2022-06-20 00:00");
            TimeSpan Span = StartRace.Subtract(NowDate);
            Timer.Text = $@"До начала события осталось {StartRace.Year - NowDate.Year} лет, {StartRace.Month - NowDate.Month} месяцев, {StartRace.DayOfWeek - NowDate.DayOfWeek} дней, {Span.ToString("hh")} часов, {Span.ToString("mm")} минут, {Span.ToString("ss")} секунд";
        }

        private void Back_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int summ = int.Parse(Money.Text);
                summ -= 10;
                if (summ > 0)
                {
                    MoneyLabel.Text = $@"{summ.ToString()}$";
                    Money.Text = summ.ToString();
                }
            }
            catch
            {

            }
        }

        private void Money_TextChanged(object sender, EventArgs e)
        {
            MoneyLabel.Text = $@"{Money.Text}$";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int summ = int.Parse(Money.Text);
                summ += 10;
                MoneyLabel.Text = $@"{summ.ToString()}$";
                Money.Text = summ.ToString();
            }
            catch
            {

            }
        }

        private void Pay_Click(object sender, EventArgs e)
        {
            if(SponsorName.Text != "" && CardName.Text != "" && CardNum.Text.Length == 16 && Month.Text.Length == 2 && Year.Text.Length == 4 && int.Parse(Year.Text) >= DateTime.Now.Year && int.Parse(Month.Text) <= 12)
            {
                SponsorshipConfirmation sponsorship = new SponsorshipConfirmation(racerCombo.Text,FondName.Text,MoneyLabel.Text);
                sponsorship.Show();
                Close();
            }
        }

        private void Sponsorship_Load(object sender, EventArgs e)
        {
            SqlConnection Connection = new SqlConnection(ConnStr);
            Connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT CONCAT (First_Name,' ',Last_Name,' ',Result.BidNumber,' - ','(',Country.Country_Name,')') as FINUM , Registration.ID_Racer FROM[Result] INNER JOIN[Registration] ON[Registration].ID_Registration = [Result].ID_Registration INNER JOIN Racer ON Racer.ID_Racer = [Registration].[ID_Racer] INNER JOIN Country ON Country.ID_Country = Racer.ID_Country", Connection);
            DataSet racer = new DataSet();
            adapter.Fill(racer);
            racerCombo.DataSource = racer.Tables[0];
            racerCombo.DisplayMember = "FINUM";
            racerCombo.ValueMember = "Registration.ID_Racer";
        }

        private void racerCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConnStr);
            connection.Open();

            SqlDataAdapter adapter1 = new SqlDataAdapter("SELECT Racer.ID_Racer, Charity.Charity_Name FROM Registration INNER JOIN Racer ON Racer.[ID_Racer] = [Registration].[ID_Racer] INNER JOIN [Charity] ON [Charity].[ID_Сharity]= [Registration].[ID_Charity]", connection);
            DataSet dataset1 = new DataSet();
            adapter1.Fill(dataset1);

            FondName.Text = dataset1.Tables[0].Rows[racerCombo.SelectedIndex][1].ToString();
        }
    }
}

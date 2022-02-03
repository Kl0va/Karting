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
    public partial class RaceRegistration : Form
    {
        public int Total = 0;
        public string ConnStr = @"Data Source=DESKTOP-4LPER1V\KLOVA;Initial Catalog=Karting;Persist Security Info=True;User ID=sa;Password=V783278k";
        string Mail;
        public RaceRegistration(string Email)
        {
            InitializeComponent();
            timer1.Start();
            LoadData();
            Mail = Email;
        }

        private void LoadData()
        {
            SqlConnection sqlConnection = new SqlConnection(ConnStr);
            sqlConnection.Open();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT Charity_Name FROM Charity",sqlConnection);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
            Charity.ValueMember = "ID_Charity";
            Charity.DisplayMember = "Charity_Name";
            Charity.DataSource = dataSet.Tables[0];
            sqlConnection.Close();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TotalMomey.Text = Total.ToString();
            DateTime NowDate = DateTime.Now;
            DateTime StartRace = DateTime.Parse("2022-06-20 00:00");
            TimeSpan Span = StartRace.Subtract(NowDate);
            Timer.Text = $@"До начала события осталось {StartRace.Year - NowDate.Year} лет, {StartRace.Month - NowDate.Month} месяцев, {StartRace.Day - NowDate.Day} дней, {Span.ToString("hh")} часов, {Span.ToString("mm")} минут, {Span.ToString("ss")} секунд";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RacerMenu racerMenu = new RacerMenu(Mail);
            racerMenu.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (int.Parse(TotalMomey.Text) >= 25)
            {
                SqlConnection connection = new SqlConnection(ConnStr);
                connection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter($"SELECT ID_Racer from Racer WHERE Last_Name = '{Mail}'",connection);
                DataSet dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);

                string check = Charity.Text;
                Console.WriteLine(check);


                SqlDataAdapter sqlDataAdapter1 = new SqlDataAdapter($"SELECT ID_Сharity FROM Charity WHERE Charity_Name = '{check}'",connection);
                DataSet dataSet1 = new DataSet();
                sqlDataAdapter1.Fill(dataSet1);

                List<int> racerId = new List<int>();
                foreach(DataRow row in dataSet.Tables[0].Rows)
                {
                    racerId.Add(row.Field<int>("ID_Racer"));
                }

                List<int> charityId = new List<int>();
                foreach(DataRow row1 in dataSet1.Tables[0].Rows)
                {
                    charityId.Add(row1.Field<int>("ID_Сharity"));
                }

                SqlCommand sqlCommand = new SqlCommand($"INSERT INTO Registration(ID_Racer,Registration_Date,ID_Registration_Status,Cost,ID_Charity,SponsorshipTarget) VALUES({racerId[0]},'{DateTime.Now}',{1},{decimal.Parse(TotalMomey.Text)},{charityId[0]},{decimal.Parse(textBox1.Text)})",connection);
                sqlCommand.ExecuteNonQuery();
                connection.Close();

                ConfirmRegistration confirmRegistration = new ConfirmRegistration(Mail);
                confirmRegistration.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Что-то не так");
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked == true)
            {
                Total += 0;
            }
            else
            {
                Total += 0;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                Total += 30;
            }
            else
            {
                Total -= 30;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                Total += 50;
            }
            else
            {
                Total -= 50;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true)
            {
                Total += 25;
            }
            else
            {
                Total -= -25;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox2.Checked == true)
            {
                Total += 40;
            }
            else
            {
                Total -= 40;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox3.Checked == true)
            {
                Total += 65;
            }
            else
            {
                Total -= 65;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }   
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
    }
}

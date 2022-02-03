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
    public partial class Login : Form
    {
        public string ConnStr = @"Data Source=DESKTOP-4LPER1V\KLOVA;Initial Catalog=Karting;Persist Security Info=True;User ID=sa;Password=V783278k";
        public Login()
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
            Timer.Text = $@"До начала события осталось {StartRace.Year - NowDate.Year} лет, {StartRace.Month - NowDate.Month} месяцев, {StartRace.Day - NowDate.Day} дней, {Span.ToString("hh")} часов, {Span.ToString("mm")} минут, {Span.ToString("ss")} секунд";
        }

        private void Auth_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection Connection = new SqlConnection(this.ConnStr);
                Connection.Open();
                SqlDataAdapter AdapterEmail = new SqlDataAdapter($"SELECT [User].Email FROM [User] WHERE Email = '{Email.Text}'", Connection);
                SqlDataAdapter AdapterPassword = new SqlDataAdapter($"SELECT [User].Password FROM [User] WHERE Password = '{Password.Text}'", Connection);
                DataSet DataSetEmail = new DataSet();
                DataSet DataSetPassword = new DataSet();
                AdapterEmail.Fill(DataSetEmail);
                AdapterPassword.Fill(DataSetPassword);
                List<String> Mass = new List<String>();
                foreach (DataRow row in DataSetEmail.Tables[0].Rows)
                {
                    Mass.Add(row.Field<string>("Email"));
                }
                List<String> MassPassword = new List<String>();
                foreach (DataRow row in DataSetPassword.Tables[0].Rows)
                {
                    MassPassword.Add(row.Field<string>("Password"));
                }

                if (Mass[0] == Email.Text && MassPassword[0] == Password.Text)
                {
                    SqlDataAdapter sql = new SqlDataAdapter($"SELECT Last_Name FROM [USER] WHERE Email = '{Email.Text}'", Connection);
                    DataSet dataSet = new DataSet();
                    sql.Fill(dataSet);

                    SqlDataAdapter AdapterRole = new SqlDataAdapter($"SELECT [User].ID_Role FROM [User] WHERE Email = '{Email.Text}'", Connection);
                    DataSet DataSetRole = new DataSet();
                    AdapterRole.Fill(DataSetRole);
                    List<String> MassRole = new List<String>();
                    foreach (DataRow row in DataSetRole.Tables[0].Rows)
                    {
                        MassRole.Add(row.Field<string>("ID_Role"));
                    }

                    if (MassRole[0] == "R")
                    {
                        List<string> mass = new List<string>();
                        foreach(DataRow row in dataSet.Tables[0].Rows)
                        {
                            mass.Add(row.Field<string>("Last_Name"));
                        }
                        RacerMenu runnerMenu = new RacerMenu(mass[0]);
                        runnerMenu.Show();
                    }
                    if (MassRole[0] == "C")
                    {
                        CoordinatorMenu coordinatorMenu = new CoordinatorMenu();
                        coordinatorMenu.Show();
                    }
                    if (MassRole[0] == "A")
                    {
                        AdministratorMenu administratorMenu = new AdministratorMenu();
                        administratorMenu.Show();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Проверьте данные");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

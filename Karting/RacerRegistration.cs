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
    public partial class RacerRegistration : Form
    {
        public string ConnStr = @"Data Source=DESKTOP-4LPER1V\KLOVA;Initial Catalog=Karting;Persist Security Info=True;User ID=sa;Password=V783278k";
        public RacerRegistration()
        {
            InitializeComponent();
            LoadData();
            timer1.Start();
        }

        private void LoadData()
        {
            SqlConnection Connection = new SqlConnection(ConnStr);
            Connection.Open();
            SqlDataAdapter genderAdapter = new SqlDataAdapter("SELECT Gender_Name FROM Gender",Connection);
            DataSet dataSet = new DataSet();
            genderAdapter.Fill(dataSet);
            Gender.ValueMember = "ID_Gender";
            Gender.DisplayMember = "Gender_Name";
            Gender.DataSource = dataSet.Tables[0];

            SqlDataAdapter countryAdapter = new SqlDataAdapter("SELECT Country_Name FROM Country",Connection);
            DataSet countrySet = new DataSet();
            countryAdapter.Fill(countrySet);
            Country.ValueMember = "ID_Country";
            Country.DisplayMember = "Country_Name";
            Country.DataSource = countrySet.Tables[0];
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

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int dateTime = DateTime.Now.Year;
            int res = dateTime - dateOfBirth.Value.Year;
            if (Email.Text.Contains("@") && Email.Text != "" && Password.Text.Length >= 6 && Password.Text == RePassword.Text && FileName.Text != "" && res >=10 && Password.Text.Contains("!") && FirstName.Text != "" && SecondName.Text != "")
            {
                SqlConnection sqlConnection = new SqlConnection(ConnStr);
                sqlConnection.Open();
                SqlCommand createRacerCommand = new SqlCommand($"INSERT INTO [dbo].[User](Email,Password,First_Name,Last_Name,ID_Role) VALUES ('{Email.Text}','{Password.Text}','{FirstName.Text}','{SecondName.Text}','R')",sqlConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter($"SELECT ID_Country FROM Country WHERE Country_Name = '{Country.Text}'",sqlConnection);
                DataSet dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);
                List<string> test = new List<string>();
                foreach(DataRow data in dataSet.Tables[0].Rows)
                {
                    test.Add(data.Field<string>("ID_Country"));
                }
                string gender = "";
                if(Gender.SelectedIndex == 0)
                {
                    gender = "F";
                }
                else
                {
                    gender = "M";
                }
                SqlCommand sqlCommand = new SqlCommand($"INSERT INTO Racer(First_Name,Last_Name,Gender,DateOfBirth,ID_Country,Photo) VALUES ('{FirstName.Text}','{SecondName.Text}','{gender}','{dateOfBirth.Value}','{test[0]}','{FileName.Text}')",sqlConnection);
                sqlCommand.ExecuteNonQuery();
                createRacerCommand.ExecuteNonQuery();
                sqlConnection.Close();
                RaceRegistration raceRegistration = new RaceRegistration(SecondName.Text);
                raceRegistration.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Bitmap Image;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.PNG;*.JPEG;*.BMP)|*.PNG;*.JPEG;*.BMP";
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Image = new Bitmap(openFileDialog.FileName);
                racerPhoto.Image = Image;
                FileName.Text = openFileDialog.FileName;
            }
        }
    }
}

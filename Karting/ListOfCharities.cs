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
    public partial class ListOfCharities : Form
    {
        public string ConnStr = @"Data Source=DESKTOP-4LPER1V\KLOVA;Initial Catalog=Karting;Persist Security Info=True;User ID=sa;Password=V783278k";
        public ListOfCharities()
        {
            InitializeComponent();
            timer1.Start();
            LoadCharities();
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

        private static void LoadCharities()
        {
        }

        private void ListOfCharities_Load(object sender, EventArgs e)
        {
            SqlConnection Connection = new SqlConnection(this.ConnStr);
            Connection.Open();
            SqlDataAdapter AdapterCharityName = new SqlDataAdapter($"SELECT Charity_Name FROM Charity", Connection);
            DataSet DataSetName = new DataSet();
            AdapterCharityName.Fill(DataSetName);
            List<String> MassName = new List<String>();
            foreach (DataRow row in DataSetName.Tables[0].Rows)
            {
                MassName.Add(row.Field<string>("Charity_Name"));
            }

            SqlDataAdapter AdapterCharityDesc = new SqlDataAdapter($"SELECT Charity_Description FROM Charity", Connection);
            DataSet DataSetDesc = new DataSet();
            AdapterCharityDesc.Fill(DataSetDesc);
            List<String> MassDesc = new List<String>();
            foreach (DataRow row in DataSetDesc.Tables[0].Rows)
            {
                MassDesc.Add(row.Field<string>("Charity_Description"));
            }

            int Size = 50;
            for (int i = 0; i < MassName.ToArray().Length; i++)
            {
                Label LABEL = new Label();
                Label LABELDESC = new Label();

                LABELDESC.Size = new Size(300, 150);
                LABELDESC.Location = new Point(400, 100 + Size);
                LABELDESC.Text = MassDesc[i];

                LABEL.Size = new Size(150, 100);
                LABEL.Location = new Point(250, 100 + Size);
                LABEL.Text = MassName[i];
                this.Controls.Add(LABEL);
                this.Controls.Add(LABELDESC);
                Size += 200;
            }

            Connection.Close();
        }
    }
}

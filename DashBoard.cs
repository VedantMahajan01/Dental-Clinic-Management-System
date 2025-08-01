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

namespace DentalClinic
{
    public partial class DashBoard : Form
    {
        public DashBoard()
        {
            InitializeComponent();
        }

        ConnectionString MyConnection = new ConnectionString();
        private void DashBoard_Load(object sender, EventArgs e)
        {
        
   
            SqlConnection Con = MyConnection.GetCon();
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from Appointmenttb",Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            Pendinglbl.Text = dt.Rows[0][0].ToString();

            SqlDataAdapter sda1 = new SqlDataAdapter("select count(*) from Patient ", Con);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            Patientlbl.Text = dt1.Rows[0][0].ToString();

            SqlDataAdapter sda2 = new SqlDataAdapter("select count(*) from UserTbl", Con);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            Userlbl.Text = dt2.Rows[0][0].ToString();

            SqlDataAdapter sda3 = new SqlDataAdapter("select count(*) from TestTbl", Con);
            DataTable dt3 = new DataTable();
            sda3.Fill(dt3);
            Datelbl.Text = dt3.Rows[0][0].ToString();
            Con.Close();

            HistoryDGV.Visible = true;

            SqlConnection Conn = new SqlConnection();
            Conn.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\hp\OneDrive\Documents\DentalDb.mdf;Integrated Security=True;Connect Timeout=30";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Con;
            cmd.CommandText = "select * from UserTbl"; 
            SqlDataAdapter sdai = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sdai.Fill(ds);

            HistoryDGV.DataSource = ds.Tables[0];
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Appointment App = new Appointment();
            App.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            DashBoard dash = new DashBoard();
            dash.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Prescription presc = new Prescription();
            presc.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Treatment treat = new Treatment();
            treat.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Appointment App = new Appointment();
            App.Show();
            this.Hide();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            Tests tes = new Tests();
            tes.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Bill bil = new Bill();
            bil.Show();
            this.Show();
        }
    }
}

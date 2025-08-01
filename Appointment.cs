using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Windows.Markup;
using System.IO;

namespace DentalClinic
{
    public partial class Appointment : Form
    {
        public Appointment()
        {
            InitializeComponent();
        }
        ConnectionString Mycon = new ConnectionString();
        private void fillPatient()
        {
            SqlConnection Con = Mycon.GetCon();
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select PatName from Patient ", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("PatName", typeof(string));
            dt.Load(rdr);
            PatientCb.ValueMember = "PatName";
            PatientCb.DataSource = dt;
            Con.Close();
        }
        private void fillallergy()
        {
            SqlConnection Con = Mycon.GetCon();
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select PatAllergies from Patient", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Patientname", typeof(string));
            dt.Load(rdr);
            PatientCb.ValueMember = "Patientname";
            PatientCb.DataSource = dt;
            Con.Close();
        }
        private void Getallergy()
        {
            SqlConnection Con = Mycon.GetCon();
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Patient where PatName='"+ PatientCb.SelectedValue.ToString()+"'", Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                AllenameTb.Text = dr["PatAllergies"].ToString();
            }
            Con.Close();
        }
        void populate()
        {
            Myclass Pat = new Myclass();
            string query = "select * from Appointmenttb";
            DataSet ds = Pat.ShowPatient(query);
            AppointmentDGV.DataSource = ds.Tables[0];
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string query = "insert into Appointmenttb values('" + PatientCb.SelectedValue.ToString() + "','" + AllenameTb.Text + "', '" + Date.Value.Date.ToString() + "', '" + Time.Value.TimeOfDay.ToString() + "')";
            Myclass Pat = new Myclass();
            try
            {
                Pat.AddPatient(query);
                MessageBox.Show("Appointment Successfully Recorded");
                populate();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        int key = 0;
        private void AppointmentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            PatientCb.SelectedValue = AppointmentDGV.SelectedRows[0].Cells[1].Value.ToString();
            AllenameTb.Text = AppointmentDGV.SelectedRows[0].Cells[2].Value.ToString();
            string Pat = AppointmentDGV.SelectedRows[0].Cells[2].Value.ToString();

            if (Pat == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(AppointmentDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Myclass Pat = new Myclass();
            if (key == 0)
            {
                MessageBox.Show("Select The Appointment To Cancel");
            }
            else
            {
                try
                {
                    string query = "Delete from Appointmenttb where ApId=" + key + "";

                    Pat.DeletePatient(query);
                    MessageBox.Show("Appointment Successfully Cancelled");
                    populate();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Myclass Pat = new Myclass();
            if (key == 0)
            {
                MessageBox.Show("Select The Appointment");
            }
            else
            {
                try
                {
                    string query = "Update Appointmenttb set Patientname='" + PatientCb.SelectedValue.ToString() + "',Allergy='" + AllenameTb.Text + "', ApDate='" + Date.Value.Date + "',ApTime='"+Time.Value.TimeOfDay+"' where ApId=" + key + ";";
                    Pat.DeletePatient(query);
                    MessageBox.Show("Appointment Successfully Updated");
                    populate();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Patient Pat = new Patient();
            Pat.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Treatment treat = new Treatment();
            treat.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Prescription Presc = new Prescription();
            Presc.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            DashBoard dash = new DashBoard();
            dash.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Tests tes = new Tests();
            tes.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Bill bil = new Bill();
            bil.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();
        }

        private void Appointment_Load(object sender, EventArgs e)
        {
            populate();
            fillPatient();
        }

        private void PatientCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Getallergy();
        }
    }
}

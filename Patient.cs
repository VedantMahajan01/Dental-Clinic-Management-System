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
using System.Configuration;
using System.Text.RegularExpressions;

namespace DentalClinic
{
    public partial class Patient : Form
    {
        public Patient()
        {
            InitializeComponent();
        }

        private void PatSaveBtn_Click(object sender, EventArgs e)
        {
            Regex ex = new Regex("^[0-9]{10}");
            bool isinvalid = ex.IsMatch(PatPhoneTb.Text);
            if (isinvalid)
            {
                if (PatNameTb.Text == "" || PatPhoneTb.Text == "" || PAgeTb.Text == "" || AddressTb.Text == "" || PatAllerigiesTb.Text == "")
                {
                    MessageBox.Show("Messing Information");
                }
                else
                {

                    string query = "insert into Patient values('" + PatNameTb.Text + "','" + PatPhoneTb.Text + "','" + AddressTb.Text + "','" + PAgeTb.Text + "','" + DOBDate.Value.Date + "','" + GenCb.SelectedItem.ToString() + "','" + PatAllerigiesTb.Text + "')";
                    Myclass Pat = new Myclass();
                    try
                    {
                        Pat.AddPatient(query);
                        MessageBox.Show("Patient Sussessfully Added");
                        populate();
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }

                }    
            }
            else
            {
                MessageBox.Show("Enter Proper Mob. Number");
            }
        }

        void populate()
        {
            Myclass Pat = new Myclass();
            string query = "select * from Patient";

            DataSet ds = Pat.ShowPatient(query);
            PatientDGV.DataSource = ds.Tables[0];
        }
        private void Patient_Load(object sender, EventArgs e)
        {
            populate();
        }
        int key = 0;
        private void PatientDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PatNameTb.Text = PatientDGV.SelectedRows[0].Cells[1].Value.ToString();
            PatPhoneTb.Text = PatientDGV.SelectedRows[0].Cells[2].Value.ToString();
            AddressTb.Text = PatientDGV.SelectedRows[0].Cells[3].Value.ToString();
            PAgeTb.Text = PatientDGV.SelectedRows[0].Cells[4].Value.ToString();
            DOBDate.Text = PatientDGV.SelectedRows[0].Cells[5].Value.ToString();
            GenCb.SelectedItem = PatientDGV.SelectedRows[0].Cells[6].Value.ToString();
            PatAllerigiesTb.Text = PatientDGV.SelectedRows[0].Cells[7].Value.ToString();
            if (PatNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(PatientDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Myclass Pat = new Myclass();
            if(key == 0)
            {
                MessageBox.Show("Select the Patient");
            }
            else
            {
                try
                {
                    string query = "Delete from Patient where PatId=" + key + "";
                    Pat.DeletePatient(query);
                    MessageBox.Show("Patient Sussessfully Deleted");
                    populate();
                }
                catch(Exception Ex)
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
                MessageBox.Show("Select the Patient");
            }
            else
            {
                try
                {
                    string query = "Update Patient set PatName='" + PatNameTb.Text + "',PatPhone='" + PatPhoneTb.Text + "',PatAddress ='" + AddressTb.Text + "',PatDOB='" + DOBDate.Value.Date + "',PatGender='" + GenCb.SelectedItem.ToString() + "',PatAllergies='" + PatAllerigiesTb.Text + "' where PatId=" + key + "";
                    Pat.UpdatePatient(query);
                    MessageBox.Show("Patient Sussessfully Updated");
                    populate();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Show();
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

    }
}
   
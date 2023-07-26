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
namespace HospitalAppointmentSystem
{
    public partial class FrmPatientLogin : Form
    {
        public FrmPatientLogin()
        {
            InitializeComponent();
        }



        SqlConnect conn = new SqlConnect();


        private void LblSignUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmPatientSignUp frm = new FrmPatientSignUp();
            frm.Show();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Select * From Tbl_Patients Where PatientTC=@p1 and PatientPassword=@p2",conn.sqlConn());
            command.Parameters.AddWithValue("@p1", mskTC.Text);
            command.Parameters.AddWithValue("@p2", TxtPassword.Text);
            SqlDataReader reader = command.ExecuteReader();

            if ( reader.Read()) 
            {

                FrmPatientDetails fr = new FrmPatientDetails();
                fr.tc = mskTC.Text; // tc değişkenini detay sayfasına taşımak için kullanıyoruz.
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong TC & Password");
            }

        }
    }
}

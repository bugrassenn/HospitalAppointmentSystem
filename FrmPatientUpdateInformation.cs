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
    public partial class FrmPatientUpdateInformation : Form
    {
        public FrmPatientUpdateInformation()
        {
            InitializeComponent();
        }


        public string TCno;

        SqlConnect conn = new SqlConnect();
        private void FrmPatientUpdateInformation_Load(object sender, EventArgs e)
        {
            mskTC.Text = TCno;

            SqlCommand command = new SqlCommand("Select * From Tbl_Patients where PatientTC=@p1", conn.sqlConn());
            command.Parameters.AddWithValue("@p1", mskTC.Text);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read()) 
            {
                TxtFirstName.Text = reader[1].ToString();
                TxtLastName.Text = reader[2].ToString();
                mskPhone.Text = reader[4].ToString();
                TxtPassword.Text = reader[5].ToString();
                CmbGender.Text = reader[6].ToString();
            }
            conn.sqlConn().Close();

        }

        private void BtnUpdateInformation_Click(object sender, EventArgs e)
        {
            SqlCommand command  = new SqlCommand("update Tbl_Patients set PatientFirstName=@p1,PatientLastName=@p2,PatientPhone=@p3,PatientPassword=@p4,PatientGender=@p5 where PatientTc=@p6",conn.sqlConn());
            command.Parameters.AddWithValue("@p1",TxtFirstName.Text);
            command.Parameters.AddWithValue("@p2", TxtLastName.Text);
            command.Parameters.AddWithValue("@p3", mskPhone.Text);
            command.Parameters.AddWithValue("@p4", TxtPassword.Text);
            command.Parameters.AddWithValue("@p5", CmbGender.Text);
            command.Parameters.AddWithValue("@p6", mskTC.Text);
            command.ExecuteNonQuery();
            conn.sqlConn().Close() ;
            MessageBox.Show("Information is Update ! ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}

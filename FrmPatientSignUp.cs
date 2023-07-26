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
    public partial class FrmPatientSignUp : Form
    {
        public FrmPatientSignUp()
        {
            InitializeComponent();
        }

        SqlConnect conn = new SqlConnect();

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            
            SqlCommand command = new SqlCommand("insert into Tbl_Patients (PatientFirstName,PatientLastName,PatientTC,PatientPhone,PatientPassword,PatientGender) values (@p1,@p2,@p3,@p4,@p5,@p6)",conn.sqlConn());
            command.Parameters.AddWithValue("@p1",TxtFirstName.Text);
            command.Parameters.AddWithValue("@p2", TxtLastName.Text);
            command.Parameters.AddWithValue("@p3", mskTC.Text);
            command.Parameters.AddWithValue("@p4", mskPhone.Text);
            command.Parameters.AddWithValue("@p5", TxtPassword.Text);
            command.Parameters.AddWithValue("@p6", CmbGender.Text);
            command.ExecuteNonQuery();
            conn.sqlConn().Close();

            MessageBox.Show("Register Successfull Password: " + TxtPassword.Text ,"İnfo", MessageBoxButtons.OK,MessageBoxIcon.Information);

        }
    }
}

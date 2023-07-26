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
    public partial class FrmDoctorLogin : Form
    {
        public FrmDoctorLogin()
        {
            InitializeComponent();
        }


        SqlConnect conn = new SqlConnect();


        private void BtnLogin_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Select * From Tbl_Doctors where DoctorTC=@p1 and DoctorPassword=@p2", conn.sqlConn());
            cmd.Parameters.AddWithValue("@p1",mskTC.Text);
            cmd.Parameters.AddWithValue("@p2", TxtPassword.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                FrmDoctorDetails fr = new FrmDoctorDetails();
                fr.TC = mskTC.Text;
                fr.Show();
            }
            else
            {
                MessageBox.Show("Wrong TC & Password");
            }
            conn.sqlConn();

        }
    }
}

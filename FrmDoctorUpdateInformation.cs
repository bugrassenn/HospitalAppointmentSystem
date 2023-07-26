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
    public partial class FrmDoctorUpdateInformation : Form
    {
        public FrmDoctorUpdateInformation()
        {
            InitializeComponent();
        }

        SqlConnect conn = new SqlConnect();

        public string TCNO;

        private void FrmDoctorUpdateInformation_Load(object sender, EventArgs e)
        {
            mskTC.Text = TCNO;


            SqlCommand cmd = new SqlCommand("Select * From Tbl_Doctors where DoctorTC= @p1", conn.sqlConn());
            cmd.Parameters.AddWithValue("@p1", mskTC.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                TxtFirstName.Text = dr[1].ToString();
                TxtLastName.Text = dr[2].ToString();
                CmbBranch.Text= dr[3].ToString();
                TxtPassword.Text = dr[4].ToString();
            }
            conn.sqlConn().Close(); 

        }

        private void BtnUpdateInformation_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Update Tbl_Doctors set DoctorFirstName=@p1,DoctorLastName=@p2,DoctorBranch=@p3,DoctorPassword=@p4 where DoctorTC=@p5", conn.sqlConn());
            cmd.Parameters.AddWithValue("@p1", TxtFirstName.Text);
            cmd.Parameters.AddWithValue("@p2", TxtLastName.Text);
            cmd.Parameters.AddWithValue("@p3",CmbBranch.Text);
            cmd.Parameters.AddWithValue("@p4",TxtPassword.Text);
            cmd.Parameters.AddWithValue("@p5",mskTC.Text);
            cmd.ExecuteNonQuery();
            conn.sqlConn().Close();
            MessageBox.Show("Update Information");
       
        }
    }
}

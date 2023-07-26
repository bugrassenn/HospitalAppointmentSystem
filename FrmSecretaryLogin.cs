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
    public partial class FrmSecretaryLogin : Form
    {
        public FrmSecretaryLogin()
        {
            InitializeComponent();
        }



        SqlConnect conn = new SqlConnect();

        

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Select * From Tbl_Secretarys where SecretaryTC=@p1 and SecretaryPassword=@p2",conn.sqlConn());

            command.Parameters.AddWithValue("@p1", mskTC.Text );
            command.Parameters.AddWithValue("@p2", TxtPassword.Text );

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            { 
                FrmSecretaryDetail fr = new FrmSecretaryDetail();
                fr.TCsecretary = mskTC.Text;
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong TC & Password");
            }
            conn.sqlConn().Close();
        }
    }
}

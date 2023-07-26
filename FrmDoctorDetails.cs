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
    public partial class FrmDoctorDetails : Form
    {
        public FrmDoctorDetails()
        {
            InitializeComponent();
        }

        SqlConnect conn = new SqlConnect();

        public string TC;
        private void FrmDoctorDetails_Load(object sender, EventArgs e)
        {
            LblTC.Text = TC;

            // Doktor Ad Soyad çekme işlemi

            SqlCommand cmd = new SqlCommand("Select DoctorFirstName,DoctorLastName From Tbl_Doctors Where DoctorTC=@p1",conn.sqlConn());
            cmd.Parameters.AddWithValue("@p1", LblTC.Text);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                LblName.Text = dr[0] + " " + dr[1];
            }
            conn.sqlConn().Close();

            // Randevuları çekme

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Appointments where AppointmentDoctor = '"+ LblName.Text + "'",conn.sqlConn());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            FrmDoctorUpdateInformation  fr = new FrmDoctorUpdateInformation();
            fr.TCNO = LblTC.Text;
            fr.Show();
        }

        private void BtnAnnouncement_Click(object sender, EventArgs e)
        {
            FrmAnnouncements fr = new FrmAnnouncements();
            fr.Show();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int select = dataGridView1.SelectedCells[0].RowIndex;

            RchComplaint.Text = dataGridView1.Rows[select].Cells[7].Value.ToString(); 
        }
    }
}

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
    public partial class FrmPatientDetails : Form
    {
        public FrmPatientDetails()
        {
            InitializeComponent();
        }

        public string tc; // tc yi login sayfasından almak için oluşturuyoruz.


        SqlConnect conn = new SqlConnect();

        private void FrmPatientDetails_Load(object sender, EventArgs e)
        {
            LblTC.Text = tc;
            // Adsoyad Kısmını Databaseden çekme 
            SqlCommand command = new SqlCommand("Select PatientFirstName,PatientLastName From Tbl_Patients Where PatientTc = @p1", conn.sqlConn());
            command.Parameters.AddWithValue("@p1", LblTC.Text);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                LblName.Text = reader[0] + " " + reader[1];
            }
            conn.sqlConn().Close();


            // Randevu Geçmişini Databaseden çekme

            DataTable dataTable = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("Select * From Tbl_Appointments where PatientTC="+tc,conn.sqlConn());
            adapter.Fill(dataTable); //datatable içini doldur adapterden gelen veri ile
            dataGridView1.DataSource = dataTable;


            // Branşları databaseden çekme

            SqlCommand command2 = new SqlCommand("Select BranchName From Tbl_Branchs", conn.sqlConn());
            SqlDataReader reader2 = command2.ExecuteReader();

            while (reader2.Read())
            {
                CmbBranch.Items.Add(reader2[0]);
            }
            conn.sqlConn().Close();

        }

        private void CmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Branş Seçildiği zaman doktorlar gelmesi için branş kısmına click olduğunda
            CmbDoctor.Items.Clear();

            SqlCommand command = new SqlCommand("Select DoctorFirstName,DoctorLastName  From Tbl_Doctors where DoctorBranch=@p1", conn.sqlConn());
            command.Parameters.AddWithValue("@p1",CmbBranch.Text);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read()) 
            {
                CmbDoctor.Items.Add(reader[0]+ " " + reader[1] );
            }
            conn.sqlConn().Close();
        }

        private void CmbDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            // doktoru seçtikten sonra randevuların gelmesi için

            DataTable dataTable = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("Select * From Tbl_Appointments where AppointmentBranch = '" + CmbBranch.Text + "'" +" and AppointmentDoctor='" + CmbDoctor.Text + "'and AppointmentStatus=0", conn.sqlConn());
            adapter.Fill(dataTable); //adapterden gelen değerleri datatable a yazdır
            dataGridView2.DataSource = dataTable;

        }

        private void LnkUpdateInformation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmPatientUpdateInformation fr = new FrmPatientUpdateInformation();
            fr.TCno = LblTC.Text;
            fr.Show();
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int select = dataGridView2.SelectedCells[0].RowIndex;
            TxtID.Text = dataGridView2.Rows[select].Cells[0].Value.ToString();
        }

        private void BtnMakeAppointment_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Update Tbl_Appointments Set AppointmentStatus=1, PatientTC=@p1, PatientComplaint=@p2 where Appointmentid=@p3",conn.sqlConn());
            cmd.Parameters.AddWithValue("@p1", LblTC.Text);
            cmd.Parameters.AddWithValue("@p2", RchComplaint.Text);
            cmd.Parameters.AddWithValue("@p3", TxtID.Text);
            cmd.ExecuteNonQuery();
            conn.sqlConn().Close();
            MessageBox.Show("Appointment made","Info", MessageBoxButtons.OK);
        }
    }
}

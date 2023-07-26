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
    public partial class FrmSecretaryDetail : Form
    {
        public FrmSecretaryDetail()
        {
            InitializeComponent();
        }

        public string TCsecretary;


        SqlConnect conn = new SqlConnect();
        private void FrmSecretaryDetail_Load(object sender, EventArgs e)
        {
            LblTC.Text = TCsecretary;

            /// Ad Soyad

            SqlCommand cmd = new SqlCommand("Select SecretaryName From Tbl_Secretarys where SecretaryTC =@p1",conn.sqlConn());
            cmd.Parameters.AddWithValue("@p1", LblTC.Text);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                LblName.Text = reader[0].ToString();
            }
            conn.sqlConn().Close();



            // Branşları datagride aktarma

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_Branchs", conn.sqlConn());
            da.Fill(dt);
            dataGridView1.DataSource = dt;


            // Doktorları listeye aktarma

            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Select  (DoctorFirstName + ' ' + DoctorLastName ) as 'Doctors', DoctorBranch  From Tbl_Doctors", conn.sqlConn());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;


            // Branşı combobox a aktarma

            SqlCommand cmd2 = new SqlCommand("Select BranchName From Tbl_Branchs", conn.sqlConn());
            SqlDataReader reader2 = cmd2.ExecuteReader();
           
            while (reader2.Read())
            {
                CmbBranch.Items.Add(reader2[0].ToString());
            }
            conn.sqlConn().Close(); 
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {

            // Yeni Randevu Ekleme

            SqlCommand cmd = new SqlCommand("insert into Tbl_Appointments (AppointmentDate,AppointmentHour,AppointmentBranch,AppointmentDoctor) values (@p1,@p2,@p3,@p4)",conn.sqlConn());
            cmd.Parameters.AddWithValue("@p1", mskDate.Text);
            cmd.Parameters.AddWithValue("@p2", mskHour.Text);
            cmd.Parameters.AddWithValue("@p3", CmbBranch.Text);
            cmd.Parameters.AddWithValue("@p4", CmbDoctor.Text);
            cmd.ExecuteNonQuery();
            conn.sqlConn().Close();
            MessageBox.Show("Appointment Created!");
        }

        private void CmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoctor.Items.Clear(); // 

            SqlCommand cmd = new SqlCommand("Select DoctorFirstName,DoctorLastName From Tbl_Doctors where DoctorBranch=@p1", conn.sqlConn());
            cmd.Parameters.AddWithValue("@p1", CmbBranch.Text);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                CmbDoctor.Items.Add(reader[0] + " " + reader[1]);
            }
            conn.sqlConn().Close(); 

        }

        private void BtnCreateAnnouncement_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("insert into Tbl_Announcements (Announcement) values (@d1)", conn.sqlConn());
            cmd.Parameters.AddWithValue("@d1", RchAnnouncement.Text);
            cmd.ExecuteNonQuery();
            conn.sqlConn().Close();
            MessageBox.Show("Announcement Created");
        }

        private void BtnDoctorPanel_Click(object sender, EventArgs e)
        {
            FrmDoctorPanel fr = new FrmDoctorPanel();
            fr.Show();
        }

        private void BtnBranchPanel_Click(object sender, EventArgs e)
        {
            FrmBranch fr = new FrmBranch();
            fr.Show();
        }

        private void BtnList_Click(object sender, EventArgs e)
        {
            FrmAppointmentList fr = new FrmAppointmentList();
            fr.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmAnnouncements fr = new FrmAnnouncements();
            fr.Show();
        }
    }
}

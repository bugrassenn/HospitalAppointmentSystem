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
    public partial class FrmDoctorPanel : Form
    {
        public FrmDoctorPanel()
        {
            InitializeComponent();
        }
        SqlConnect conn = new SqlConnect();
        private void FrmDoctorPanel_Load(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("Select  *  From Tbl_Doctors", conn.sqlConn());
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;

            // branşları comboboxa aktarma
            SqlCommand cmd2 = new SqlCommand("Select BranchName From Tbl_Branchs", conn.sqlConn());
            SqlDataReader reader2 = cmd2.ExecuteReader();

            while (reader2.Read())
            {
                CmbBranch.Items.Add(reader2[0].ToString());
            }
            conn.sqlConn().Close();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("insert into Tbl_Doctors (DoctorFirstName,DoctorLastName,DoctorBranch,DoctorTC,DoctorPassword) values (@p1,@p2,@p3,@p4,@p5)",conn.sqlConn());
            cmd.Parameters.AddWithValue("@p1", TxtFirstName.Text);
            cmd.Parameters.AddWithValue("@p2", TxtLastName.Text);
            cmd.Parameters.AddWithValue("@p3", CmbBranch.Text);
            cmd.Parameters.AddWithValue("@p4", mskTC.Text);
            cmd.Parameters.AddWithValue("@p5", TxtPassword.Text);
            cmd.ExecuteNonQuery();
            conn.sqlConn().Close();
            MessageBox.Show("Doctors Add!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // doktorun bilgilerini getirmesi için 

            int select = dataGridView1.SelectedCells[0].RowIndex;
            TxtFirstName.Text = dataGridView1.Rows[select].Cells[1].Value.ToString();
            TxtLastName.Text = dataGridView1.Rows[select].Cells[2].Value.ToString();
            CmbBranch.Text = dataGridView1.Rows[select].Cells[3].Value.ToString();
            mskTC.Text = dataGridView1.Rows[select].Cells[4].Value.ToString();
            TxtPassword.Text = dataGridView1.Rows[select].Cells[5].Value.ToString();

        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("delete from Tbl_Doctors Where DoctorTC=@p1",conn.sqlConn());
            cmd.Parameters.AddWithValue("@p1", mskTC.Text);
            cmd.ExecuteNonQuery();
            conn.sqlConn().Close();
            MessageBox.Show("Register Delete1");
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("update Tbl_Doctors set DoctorFirstName =@p1, DoctorLastName = @p2, DoctorBranch = @p3, DoctorPassword=@p5 where DoctorTC=@p4",conn.sqlConn() );
            cmd.Parameters.AddWithValue("@p1", TxtFirstName.Text);
            cmd.Parameters.AddWithValue("@p2", TxtLastName.Text);
            cmd.Parameters.AddWithValue("@p3", CmbBranch.Text);
            cmd.Parameters.AddWithValue("@p4", mskTC.Text);
            cmd.Parameters.AddWithValue("@p5", TxtPassword.Text);
            cmd.ExecuteNonQuery();
            conn.sqlConn().Close();
            MessageBox.Show("Doctor Update!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

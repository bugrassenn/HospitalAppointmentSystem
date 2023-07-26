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
    public partial class FrmBranch : Form
    {
        public FrmBranch()
        {
            InitializeComponent();
        }


        SqlConnect conn = new SqlConnect();
        private void FrmBranch_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Branchs",conn.sqlConn());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("insert into Tbl_Branchs (BranchName) values (@p1)",conn.sqlConn());
            cmd.Parameters.AddWithValue("@p1", TxtBranch.Text);
            cmd.ExecuteNonQuery();
            conn.sqlConn().Close();
            MessageBox.Show("Branc Add!");
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int select = dataGridView1.SelectedCells[0].RowIndex;
            TxtID.Text = dataGridView1.Rows[select].Cells[0].Value.ToString();
            TxtBranch.Text = dataGridView1.Rows[select].Cells[1].Value.ToString();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("delete from Tbl_Branchs where Branchid = @p1", conn.sqlConn());
            cmd.Parameters.AddWithValue("@p1", TxtID.Text);
            cmd.ExecuteNonQuery();
            conn.sqlConn().Close();
            MessageBox.Show("Branch Deleted!");
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("update Tbl_Branchs set BranchName=@p1 where Branchid=@p2", conn.sqlConn());
            cmd.Parameters.AddWithValue("@p1", TxtBranch.Text);
            cmd.Parameters.AddWithValue("@p2", TxtID.Text);
            cmd.ExecuteNonQuery();
            conn.sqlConn().Close();
            MessageBox.Show("Branch Update!");
        }
    }
}

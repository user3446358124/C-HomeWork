using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LMSProject
{
    public partial class ReturnBook : Form
    {
        TcpClient client;
        private BinaryReader br;
        private BinaryWriter bw;
        NetworkStream ns;
        public ReturnBook(TcpClient tcpclient)
        {
            InitializeComponent();
            client = tcpclient;
            ns = client.GetStream();
            br = new BinaryReader(ns);
            bw = new BinaryWriter(ns);
        }


        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void circularPicture1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void jFlatButton5_Click(object sender, EventArgs e)
        {
            txtBkName.Clear();
            txtBkIssueDate.Clear();
        }

        private void jFlatButton2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void btnStSearch_Click(object sender, EventArgs e)
        {
            if (txtStNumber.Text != "")
            {
                String CS = "data source=.; database = LMSDB; integrated security=SSPI";
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandText = " select *from tblIssueBooks where stNumber='" + txtStNumber.Text + "' and bkReturnDate is null";
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet dataSet = new DataSet();
                    da.Fill(dataSet);
                    
                    if (dataSet.Tables[0].Rows.Count != 0)
                    {
                        dataGridView1.DataSource = dataSet.Tables[0];
                    }
                    else
                    {
                        MessageBox.Show("Student Number is invalid or no book issued", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        string bkName;
        string bkIssueDate;
        Int64 rowId;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                rowId = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                bkName = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                bkIssueDate = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            }
            txtBkName.Text = bkName;
            txtBkIssueDate.Text = bkIssueDate;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void SendMessage(string message)
        {
            try
            {
                //将字符串写入网络流，此方法会自动附加字符串长度前缀
                bw.Write(message);
                bw.Flush();
            }
            catch
            {

            }
        }

        private void btnbkreturn_Click(object sender, EventArgs e)
        {
            String CS = "data source=.; database = LMSDB; integrated security=SSPI";
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "Update tblIssueBooks set bkReturnDate='"+ returnDate.Text +"' where stNumber='"+ txtStNumber.Text +"' and Id="+ rowId +" ";
                cmd.ExecuteNonQuery();
                MessageBox.Show(bkName+" Book is returned Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SendMessage("学生" + txtStNumber.Text + "于" + returnDate.Text + "还书成功");
            }

        }

        private void txtStNumber_TextChanged(object sender, EventArgs e)
        {
            if (txtStNumber.Text == "")
            {
                dataGridView1.DataSource = null;
                txtBkName.Clear();
                txtBkIssueDate.Clear();
            }
        }

        private void btnStRefresh_Click(object sender, EventArgs e)
        {
            txtStNumber.Clear();
        }
    }
}

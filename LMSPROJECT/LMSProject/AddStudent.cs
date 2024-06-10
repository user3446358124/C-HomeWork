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
    public partial class AddStudent : Form
    {
        
        TcpClient client;
        private BinaryReader br;
        private BinaryWriter bw;
        NetworkStream ns;
        public AddStudent(TcpClient tcpclient)
        {
            InitializeComponent();
            client = tcpclient;
            ns = client.GetStream();
            br = new BinaryReader(ns);
            bw = new BinaryWriter(ns);
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

        private void jFlatButton2_Click(object sender, EventArgs e)
        {

        }

        private void circularPicture1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void btnSaveStInfo_Click(object sender, EventArgs e)
        {
            String CS = "data source=.; database = LMSDB; integrated security=SSPI";
            using (SqlConnection con = new SqlConnection(CS))
            {
                if (txtstName.Text != "" && txtstNumber.Text != "" && txtstDepartment.Text != "" && txtstSemester.Text != "" && txtstContact.Text != "" && txtstEmail.Text != "")
                {
                    //Int64 contact = Int64.Parse(txtstContact.Text);
                    SqlCommand cmd = new SqlCommand("insert into tblStudentInfos(stName,stNumber,stDepartment,stSemester,stContact,stEmail) values('" + txtstName.Text + "', '" + txtstNumber.Text + "'," +
                   "'" + txtstDepartment.Text + "', '" + txtstSemester.Text + "', '" + Int64.Parse(txtstContact.Text) + "', '" + txtstEmail.Text + "') ", con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Students Infos saved.", " Success" + MessageBoxButtons.OK + MessageBoxIcon.Information);
                    SendMessage("名为" + txtstName.Text + "的学生信息保存成功");
                    txtstName.Clear();
                    txtstNumber.Clear();
                    txtstDepartment.Clear();  
                    txtstSemester.Clear();
                    txtstContact.Clear();
                    txtstEmail.Clear();
                }
                else
                {
                    MessageBox.Show("No Info entered.", "Error" + MessageBoxButtons.OK + MessageBoxIcon.Warning);
                }

            }
        }

        private void btnStInfoRefrsh_Click(object sender, EventArgs e)
        {
            txtstName.Clear();
            txtstNumber.Clear();
            txtstDepartment.Clear();
            txtstSemester.Clear();
            txtstEmail.Clear();
        }
    }
}

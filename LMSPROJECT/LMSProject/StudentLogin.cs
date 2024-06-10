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
    public partial class StudentLogin : Form
    {
        TcpClient client;
        private BinaryReader br;
        private BinaryWriter bw;
        NetworkStream ns;
        public StudentLogin(TcpClient tcpClient)
        {
            InitializeComponent();
            // 创建客户端，与服务端连接
            client = tcpClient;
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

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            String CS = "data source=.; database = LMSDB; integrated security=SSPI";
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("Select *from tblStudentInfos where stName='" + txtusername.Text + "' and stNumber ='" + txtpassword.Text + "' ", con);
                 con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                    StudentDashboard das = new StudentDashboard();
                    this.Hide();
                    das.Show();
                    SendMessage("学生" + txtusername.Text + "登录成功");
                }

                else
                {
                    MessageBox.Show("Wrong Username OR Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SendMessage("学生" + txtusername.Text + "登录失败");
                }
            }
        }

        private void txtusername_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtusername.Text == "StudentName")
            {
                txtusername.Clear();
            }
        }

        private void txtpassword_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtpassword.Text == "StudentNumber")
            {
                txtpassword.Clear();
                txtpassword.PasswordChar = '*';
            }
        }

        private void circularPicture1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}

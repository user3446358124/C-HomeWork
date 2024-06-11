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
using System.Configuration;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace LMSProject
{
    public partial class Login : Form
    {
        TcpClient client;
        private BinaryReader br;
        private BinaryWriter bw;
        NetworkStream ns;

        public Login(TcpClient tcpClient)
        {
            InitializeComponent();
            client = tcpClient;
            ns = client.GetStream();
            br = new BinaryReader(ns);
            bw = new BinaryWriter(ns);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
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

       

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            String CS = "data source=.; database = LMSDB; integrated security=SSPI";
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("Select *from tblLogin where UserName='" + txtusername.Text + "' and Password ='" + txtpassword.Text + "' ", con);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                    Dashboard das = new Dashboard(client);
                    this.Hide();
                    das.Show();
                    SendMessage("管理员" + txtusername.Text + "登录成功");
                }

                else
                {
                    MessageBox.Show("Wrong Username OR Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SendMessage("管理员" + txtusername.Text + "登录失败");
                }
            }
        }

        private void circularPicture1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void txtusername_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtusername.Text == "UserName")
            {
                txtusername.Clear();
            }
        }

        private void txtpassword_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtpassword.Text == "Password")
            {
                txtpassword.Clear();
                txtpassword.PasswordChar = '*';
            }
        }

        private void txtusername_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtpassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}

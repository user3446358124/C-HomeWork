using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LMSProject
{
    public partial class HomePage : Form
    {
        TcpClient client;
        private BinaryReader br;
        private BinaryWriter bw;
        NetworkStream ns;
        public HomePage()
        {
            InitializeComponent();
            // 创建客户端，与服务端连接
            client = new TcpClient();
            client.Connect("127.0.0.1", 3033);
            ns = client.GetStream();
            br = new BinaryReader(ns);
            bw = new BinaryWriter(ns);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
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

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendMessage("用户尝试以管理员身份登录");
            Login admin = new Login(client);
            admin.Show();
        }

        private void studentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendMessage("用户尝试以学生身份登录");
            StudentLogin stlogin = new StudentLogin(client);
            stlogin.Show();
        }

        private void booksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendMessage("用户尝试图书搜索");
            BookSearch bksearch = new BookSearch();
            bksearch.Show();
        }

        private void circularPicture1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void circularPicture1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}

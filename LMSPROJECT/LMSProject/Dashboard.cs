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
    public partial class Dashboard : Form
    {
        TcpClient client;
        private BinaryReader br;
        private BinaryWriter bw;
        NetworkStream ns;

        public Dashboard(TcpClient tcpclient)
        {
            InitializeComponent();
            client = tcpclient;
            ns = client.GetStream();
            br = new BinaryReader(ns);
            bw = new BinaryWriter(ns);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void returNBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendMessage("admin点击了ReturnBook模块");
            ReturnBook returnbook = new ReturnBook(client);
            returnbook.Show();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void completeBookDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendMessage("admin点击了BookDetail模块");
            CompleteBookDetail bookdetail = new CompleteBookDetail();
            bookdetail.Show();
        }

        private void circularPicture1_Click(object sender, EventArgs e)
        {
            //
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
        private void dashboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendMessage("admin点击了home模块");
            HomePage home = new HomePage();
            home.Show();
        }

        private void addNewBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendMessage("admin点击了AddBook模块");
            AddBook addbook = new AddBook(client);
            addbook.Show();
        }

        private void viewBooksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendMessage("admin点击了ViewBook模块");
            ViewBook viewbook = new ViewBook();
            viewbook.Show();
        }

        private void viewStudentInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendMessage("admin点击了ViewStudent模块");
            ViewStudent viewstudent = new ViewStudent();
            viewstudent.Show();
        }

        private void studentToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addNewStudentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendMessage("admin点击了AddStudent模块");
            AddStudent addStudent = new AddStudent(client);
            addStudent.Show();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendMessage("admin点击了logOut模块");
            this.Hide();
        }

        private void issueBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendMessage("admin点击了issueBook模块");
            IssueBook issuebook = new IssueBook(client);
            issuebook.Show();
        }

        private void circularProgressBar4_Click(object sender, EventArgs e)
        {

        }
    }
}

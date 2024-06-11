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
    public partial class AddBook : Form
    {
        TcpClient client;
        private BinaryReader br;
        private BinaryWriter bw;
        NetworkStream ns;
        public AddBook(TcpClient tcpclient)
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

        private void circularPicture1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnSaveBook_Click(object sender, EventArgs e)
        {
            String CS = "data source=.; database = LMSDB; integrated security=SSPI";
            using (SqlConnection con = new SqlConnection(CS))
            {
                if(txtbkName.Text!="" && txtbkAuthor.Text!="" && txtbkPublication.Text!="" && bkPurchaseDate.Text!="" && txtbkPrice.Text!="" && txtbkQuantity.Text!="")
                {
                    SqlCommand cmd = new SqlCommand("insert into tblBookInfos(bkName,bkAuthor,bkPublication,bkDate,bkPrice,bkQuanity) values('" + txtbkName.Text + "', '" + txtbkAuthor.Text + "'," +
                   "'" + txtbkPublication.Text + "', '" + bkPurchaseDate.Text + "', '" + int.Parse(txtbkPrice.Text) + "', '" + int.Parse(txtbkQuantity.Text) + "') ", con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Book Infos saved.", " Success" + MessageBoxButtons.OK + MessageBoxIcon.Information);
                    SendMessage("名为" + txtbkName.Text + "的书信息保存成功");
                    txtbkName.Clear();
                    txtbkAuthor.Clear();
                    txtbkPublication.Clear();
                    // bkPurchaseDate.Clear();
                    txtbkPrice.Clear();
                    txtbkQuantity.Clear();
                }
                else
                {
                    MessageBox.Show("No Info entered.", "Error" + MessageBoxButtons.OK + MessageBoxIcon.Warning);
                }
               
            }
        }

        private void btnBookCancel_Click(object sender, EventArgs e)
        {
            txtbkName.Clear();
            txtbkAuthor.Clear();
            txtbkPublication.Clear();
            // bkPurchaseDate.Clear();
            txtbkPrice.Clear();
            txtbkQuantity.Clear();
        }
    }
}

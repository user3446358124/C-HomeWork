using Microsoft.VisualBasic.ApplicationServices;
using System.Net;
using System.Net.Sockets;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Server
{
    public partial class Form1 : Form
    {
        TcpListener listener;
        public string userName;
        Thread thread;

        public BinaryReader br { get; private set; }
        public BinaryWriter bw { get; private set; }
        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            listener = new TcpListener(IPAddress.Loopback, 3033);
            listener.Start();

            this.Invoke((MethodInvoker)delegate
            {
                textBox1.AppendText("已开始监听" + Environment.NewLine);
            });
            Thread t = new Thread(RecConnect);
            t.IsBackground = true;
            t.Start();
        }

        private void RecConnect()
        {
            while (true)
            {   //接受传入的TCP连接请求，并返回一个TcpClient对象client
                TcpClient client = listener.AcceptTcpClient();
                NetworkStream networkStream = client.GetStream();
                br = new BinaryReader(networkStream);
                bw = new BinaryWriter(networkStream);
                Thread t = new Thread(ReceiveFromClient);
                t.IsBackground = true;
                t.Start();
            }
        }
        public void ReceiveFromClient()
        {
            while (true)
            {
                string receiveString = null;
                try
                {
                    receiveString = br.ReadString();
                }
                catch
                {
                    return;
                }
                this.Invoke((MethodInvoker)delegate
                {
                    textBox1.AppendText(receiveString+"\r\n");
                });
                
            }
        }
    }
}


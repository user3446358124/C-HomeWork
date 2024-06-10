# Library Management System

### 监听

在主页面通过构造方法创建客户端并连接服务端

```C#
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
```

由于仅有监听功能，客户端无需新建线程等待服务端消息，仅仅使用`SendMessage`函数与服务端通信即可。

对于服务端，由于服务端不得不与多台客户端间进行通讯，所以运用多线程技术成为必要条件，我们在服务端一旦开始监听后，一但接收到新的客户端连接，新建后台线程来完成通讯。

```C#
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
```

```C#
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
```

并将接收到的信息打印在`textBox`文本框中

```C#
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
```

需要注意的是，每次更新页面时，需要将客户端的`client`作为构造函数传入新页面，以保证连接不中断，如下为跳转`login`页面的操作与构造函数实现：

跳转:

```C#
 private void adminToolStripMenuItem_Click(object sender, EventArgs e)
 {
     SendMessage("用户尝试以管理员身份登录");
     Login admin = new Login(client);
     admin.Show();
 }
```

`login`构造函数实现与`SendMessage`函数继承

```C#
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
```

这样，在新页面中，我们仍然可通过`SendMessage`方法与服务端通信

```C#
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
```

至于其余模块的监听工作实现，原理仍如上所示。

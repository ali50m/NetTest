using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;

namespace WinFormTest
{
    public partial class Form1 : Form
    {
        WebSocket _webSocket;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = new String(textBox1.Text.Where(m => (int)m >= 65 && (int)m <= 90).ToArray());
        }
        private void Form1_Disposed(object sender, EventArgs e)
        {
            (_webSocket as IDisposable).Dispose();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //var conn = new HubConnection("http://localhost:8080/signalr", "param1=testParam");
            //var proxy = conn.CreateHubProxy("systemHub");
            //proxy.On("hello", message => Invoke(new Action(()=>listBox1.Items.Add(message))));
            //await conn.Start();

            //string url = "ws://localhost:24900/" + "test.ashx";

            //ClientWebSocket client = new ClientWebSocket();
            //await client.ConnectAsync(new Uri(url), new CancellationToken());
            //while (true)
            //{
            //    var array = new byte[4096];
            //    var result = await client.ReceiveAsync(new ArraySegment<byte>(array), new CancellationToken());
            //    var msg = Encoding.UTF8.GetString(array,0,result.Count);
            //    MessageBox.Show(msg);
            //}

            //await client.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes("my message")), System.Net.WebSockets.WebSocketMessageType.Text, true, new CancellationToken());

            _webSocket = new WebSocket("ws://localhost:8180/mybatis-spring/websocket");
            _webSocket.OnMessage += (o, args) => this.Invoke(new Action(()=>txtSocketMsg.AppendText(args.Data)));
            _webSocket.OnOpen += (o, args) => MessageBox.Show("连接WebSocket");
            _webSocket.OnClose += (o, args) => MessageBox.Show("断开WebSocket——"+args.Reason);
            _webSocket.OnError += (o, args) => MessageBox.Show("错误WebSocket——"+args.Message);
            _webSocket.Connect();
            //ws.Send("BALUS");
        }

        private void sqlConnTest_Click(object sender, EventArgs e)
        {
            using (var conn = new System.Data.SqlClient.SqlConnection(@"Data Source=localhost\sqlexpress;Initial Catalog=BIMWEB_Test;Integrated Security=True"))
            {
                conn.Open();
                var adapter = new System.Data.SqlClient.SqlDataAdapter("select * from Sys_UserInfo", conn);
                var dt = new DataTable();
                adapter.Fill(dt);

            }

            
        }

    }
}

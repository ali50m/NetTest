using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = new String( textBox1.Text.Where(m => (int)m >= 65 && (int)m <= 90).ToArray());
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            //var conn = new HubConnection("http://localhost:8080/signalr", "param1=testParam");
            //var proxy = conn.CreateHubProxy("systemHub");
            //proxy.On("hello", message => Invoke(new Action(()=>listBox1.Items.Add(message))));
            //await conn.Start();

           
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

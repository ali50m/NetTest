using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormTest
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = await GetString();
        }


        public async Task<string> GetString()
        {
            var a = await Task.Run(()=>{
                Thread.Sleep(5000);
                return "abc";
            });

           return a;
        }
    }
}

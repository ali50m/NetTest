using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;


namespace WinFormTest
{
    public partial class ApiInvoker : Form
    {
        public ApiInvoker()
        {
            InitializeComponent();
        }

        private async void btnInvoke_Click(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                var invokeData = bsInvokeData.Current as InvokeData;

                client.BaseAddress = new Uri(invokeData.BaseUrl);
                if (!String.IsNullOrEmpty(invokeData.Token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", invokeData.Token);
                }
                HttpResponseMessage message;
                HttpContent content;
                if (invokeData.UseJson)
                {
                    content = new StringContent(invokeData.RequestData);
                    content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                }
                else
                {
                    var nameValueCollection = HttpUtility.ParseQueryString(invokeData.RequestData);
                    content = new FormUrlEncodedContent(nameValueCollection.Cast<string>().Select(m => new KeyValuePair<string, string>(m, nameValueCollection[m])));
                }
                //if (!String.IsNullOrEmpty(invokeData.Token))
                //{
                //    content.Headers.Add("Authorization", "Bearer" + invokeData.Token);
                //}



                switch (invokeData.HttpMethod.ToLower())
                {
                    case "get":
                        message = await client.GetAsync(invokeData.Url);
                        break;
                    case "post":
                        message = await client.PostAsync(invokeData.Url, content);
                        break;
                    case "put":
                        message = await client.PutAsync(invokeData.Url, content);
                        break;
                    case "delete":
                        message = await client.DeleteAsync(invokeData.Url);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                invokeData.ResponseData = await message.Content.ReadAsStringAsync();
                bsInvokeData.ResetCurrentItem();

            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show("是否发送token请求", "", MessageBoxButtons.YesNo);

            bsInvokeData.Add(dialogResult == DialogResult.Yes ? new InvokeData("oauth/token", "grant_type=client_credentials&scope=trust&client_id=my-trusted-client&client_secret=secret", false, "post") : new InvokeData("api/persons", "", true, "get"));
        }


    }

    public class InvokeData
    {
        public InvokeData(string url, string requestData, bool userJson, string httpMethod)
        {
            Id = Guid.NewGuid();
            UseJson = userJson;
            BaseUrl = "http://localhost:8180/mybatis-spring/";
            Url = url;
            HttpMethod = httpMethod;
            RequestData = requestData;
        }
        public Guid Id { get; private set; }
        string _name;
        public string Name
        {
            get
            {
                if (String.IsNullOrEmpty(_name))
                {
                    return Url;
                }
                return _name;
            }
            set { _name = value; }
        }
        public string BaseUrl { get; set; }
        public string Url { get; set; }
        public string HttpMethod { get; set; }
        public string Token { get; set; }
        public bool UseJson { get; set; }
        public string RequestData { get; set; }
        public string ResponseData { get; set; }
    }
}

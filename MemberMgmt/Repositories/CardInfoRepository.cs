using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MemberMgmt.Models;
using Newtonsoft.Json.Linq;

namespace MemberMgmt.Repositories
{
    class CardInfoRepository : IRepositories.ICardInfoRepository
    {

        public async Task<Info> GetOne(string qrCode)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Properties.Settings.Default.ApiBaseUrl);
                var responseMessage = await client.PostAsJsonAsync("client/verify", "{'info':'" + qrCode + "'}");
                var info = await responseMessage.Content.ReadAsAsync<Info>();
                return info;
            }
        }

        public async Task<Info> GetOne(string memberName, string mobile)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Properties.Settings.Default.ApiBaseUrl);
                var responseMessage = await client.PostAsJsonAsync("client/search", "{'memberName':'" + memberName + "','memberPhone':'" + mobile + "'}");
                var info = await responseMessage.Content.ReadAsAsync<Info>();
                return info;
            }
        }

        public async Task<Info> EditState(int state)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Properties.Settings.Default.ApiBaseUrl);
                var responseMessage = await client.PostAsJsonAsync("card/edit/state", "{'state':'" + state + "'}");
                var info = await responseMessage.Content.ReadAsAsync<Info>();
                return info;
            }
        }

        public async Task<string> GetOrderState(string memberId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Properties.Settings.Default.ApiBaseUrl);
                var responseMessage = await client.PostAsJsonAsync("client/order/verify", "{'memberId':'" + memberId + "'}");
                var str = await responseMessage.Content.ReadAsStringAsync();
                return str;
            }
        }

        public async Task<string> Test(string input)
        {
            //client?info=1231
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Properties.Settings.Default.ApiBaseUrl);
                return await client.GetStringAsync("client?info="+input);
            }
        }
    }
}

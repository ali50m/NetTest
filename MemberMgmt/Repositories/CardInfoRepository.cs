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

        public async Task<Info> GetOne(string qrCode, bool withConsume)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Properties.Settings.Default.ApiBaseUrl);
                var keyValuePairs = new[]{
                    new KeyValuePair<string, string>("info",qrCode)
                };
                var content = new FormUrlEncodedContent(keyValuePairs);

                var responseMessage = await client.PostAsync(withConsume ? "client/verify" : "client/selectMember", content);
                //var info = await responseMessage.Content.ReadAsStringAsync();
                var info = await responseMessage.Content.ReadAsAsync<Info>();
                return info;
                //return new Info();
            }
        }

        public async Task<Info> GetOne(string memberName, string mobile)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Properties.Settings.Default.ApiBaseUrl);
                var keyValuePairs = new[]{
                    new KeyValuePair<string, string>("memberName",memberName),
                     new KeyValuePair<string, string>("memberPhone",mobile)
                };
                var content = new FormUrlEncodedContent(keyValuePairs);
                var responseMessage = await client.PostAsync("client/search", content);
                var info = await responseMessage.Content.ReadAsAsync<Info>();
                return info;
            }
        }

        public async Task<Info> EditState(string memberId, int state)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Properties.Settings.Default.ApiBaseUrl);
                var keyValuePairs = new[]{
                    new KeyValuePair<string, string>("memberId",memberId),
                    new KeyValuePair<string, string>("state",state.ToString())
                };
                var content = new FormUrlEncodedContent(keyValuePairs);
                var responseMessage = await client.PostAsync("card/edit/state", content);
                var info = await responseMessage.Content.ReadAsAsync<Info>();
                return info;
            }
        }

        public async Task<Info> GetOrderState(string memberId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Properties.Settings.Default.ApiBaseUrl);
                var keyValuePairs = new[]{
                    new KeyValuePair<string, string>("memberId",memberId)
                };
                var content = new FormUrlEncodedContent(keyValuePairs);
                var responseMessage = await client.PostAsync("client/order/verify", content);
                var info = await responseMessage.Content.ReadAsAsync<Info>();
                return info;
            }
        }

    }
}

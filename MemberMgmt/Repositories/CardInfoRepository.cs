using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MemberMgmt.Models;

namespace MemberMgmt.Repositories
{
    class CardInfoRepository : IRepositories.ICardInfoRepository
    {
        bool a;
        public async Task<CardInfo> GetOne()
        {
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("http://www.aaa.com/");
            //    var responseMessage = await client.GetAsync("api/card-infors");
            //    await responseMessage.Content.ReadAsAsync<CardInfo>();
            //}
            await Task.FromResult(0);
            a = !a;
            return a?new CardInfo {
                CardNum = "001",
                Name = "张三",
                IdCardNum="140112198106121715"

            }: new CardInfo
            {
                CardNum = "002",
                Name = "李四",
                IdCardNum = "140110196502011214"

            };
        }
    }
}

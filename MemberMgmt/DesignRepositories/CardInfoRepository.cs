using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemberMgmt.Models;

namespace MemberMgmt.DesignRepositories
{
    class CardInfoRepository : IRepositories.ICardInfoRepository
    {
        public async Task<Info> GetOne(string qrCode)
        {
            await Task.FromResult(0);
            return new Info
            {
                Card = new Card
                {
                    CardNum = "001",
                    Name = "张三",
                }

            };
        }


        public Task<Info> GetOne(string memberName, string mobile)
        {
            throw new NotImplementedException();
        }

        public Task<Info> EditState(int state)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetOrderState(string memberId)
        {
            throw new NotImplementedException();
        }

        public Task<string> Test(string input)
        {
            throw new NotImplementedException();
        }
    }
}

using MemberMgmt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberMgmt.IRepositories
{
    interface ICardInfoRepository
    {
        Task<Info> GetOne(string qrCode,bool withConsume);
        Task<Info> GetOne(string memberName,string mobile);
        Task<Info> EditState(int state);
        Task<String> GetOrderState(string memberId);
    }
}

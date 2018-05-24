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
        Task<Info> EditState(string memberId, int state);
        Task<Info> GetOrderState(string memberId);
    }
}

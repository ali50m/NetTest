using MemberMgmt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberMgmt.Services
{
    class CardInfoService
    {
        public CardInfoService(IRepositories.ICardInfoRepository repository)
        {
            _repository = repository;
        }
        IRepositories.ICardInfoRepository _repository;
        /// <summary>
        /// 用二维码字符串查询会员信息
        /// </summary>
        /// <param name="qrCode">二维码字符串</param>
        /// <param name="withConsume">是否消费</param>
        /// <returns></returns>
        internal async Task<Info> GetOne(string qrCode,bool withConsume)
        {
            return await _repository.GetOne(qrCode, withConsume);
        }
        /// <summary>
        /// 用会员名或手机号查会员信息
        /// </summary>
        /// <param name="memberName"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        internal async Task<Info> GetOne(string memberName, string mobile)
        {
            return await _repository.GetOne(memberName, mobile);
        }
        internal async Task<Info> EditState(int state)
        {
            return await _repository.EditState(state);
        }
        internal async Task<String> GetOrderState(string memberId)
        {
            return await _repository.GetOrderState(memberId);
        }


    }
}

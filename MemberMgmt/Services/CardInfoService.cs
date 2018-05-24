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
        internal async Task<Info> GetOne(string qrCode)
        {
            return await _repository.GetOne(qrCode);
        }
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

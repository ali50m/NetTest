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
        internal Models.CardInfo GetOne() {
            return _repository.GetOne();
        }
    }
}

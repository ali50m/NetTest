using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberMgmt.Models
{
    class Card
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int IsOpen { get; set; }
        public int UserDay { get; set; }
        public string Detail { get; set; }
        public decimal DefaultRewardMoney { get; set; }
        public string Info { get; set; }

        public String CardNum { get; set; }
        public MyMemberPossessCard MyMemberPossessCard { get; set; }

    }
    class MyMemberPossessCard{
        public int State { get; set; }
        public string BuyTime { get; set; }
        public string LoseTime { get; set; }
        public string CardNum { get; set; }
    }
}

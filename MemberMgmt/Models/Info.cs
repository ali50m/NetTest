using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberMgmt.Models
{
    class Info
    {
        public string Ref { get; set; }
        public List<Seat> Seats { get; set; }
        public Member Member { get; set; }
        public Card Card { get; set; }

    }
}

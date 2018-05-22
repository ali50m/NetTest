using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberMgmt.IRepositories
{
    interface IServerPing
    {
        Task<string> Ping();
    }
}

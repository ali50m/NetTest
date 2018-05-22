using MemberMgmt.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberMgmt.Repositories
{
    class ServerPing:IServerPing
    {
        public Task<string> Ping()
        {
            throw new NotImplementedException();
        }
    }
}

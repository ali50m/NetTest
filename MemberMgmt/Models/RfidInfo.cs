using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberMgmt.Models
{
    class RfidInfo
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string ID { get; set; }
        public string DataStr { get; set; }
        public byte[] Data { get; set; }
        public string MemberNo
        {
            get {
                return Encoding.Default.GetString(Data);
            }
            set
            {
                Data= Encoding.Default.GetBytes(value);
            }
        }
    }
}

using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.Position.Models
{
    public struct LsTagVideoTrace
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }
        public ulong tagid { get; set; }
        public string ip { get; set; }
        public int port { get; set; }
        public string user { get; set; }
        public string pwd { get; set; }
        public bool success { get; set; }
        public int type { get; set; }
        public int mode { get; set; }
    }
}

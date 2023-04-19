using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.Position.Models
{
    class LsBatteryCapInfo
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }
        public ulong tagid { get; set; }
        public int battcap { get; set; }
        public bool charging { get; set; }
        public override string ToString()
        {
            return String.Format("{0}: {1}:{2}",tagid,battcap,charging );
        }
    }
}

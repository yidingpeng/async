using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.Position.Models
{
    public struct LsRegInOutInfo
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }
        public ulong tagid { get; set; }
        public string tagname { get; set; }
        public ulong areaid { get; set; }
        public string areaname { get; set; }
        public int mapid { get; set; }
        public string mapname { get; set; }
        public int status { get; set; }
        public ulong timestamp { get; set; }
    }
}

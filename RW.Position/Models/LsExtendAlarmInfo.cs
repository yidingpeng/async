using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.Position.Models
{
    class LsExtendAlarmInfo
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }
        public int alarmtype { get; set; }
        public ulong alarmid { get; set; }
        public ulong tagid { get; set; }
        public ulong timestamp { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public int mapid { get; set; }
        public string alarmmsg { get; set; }
        public ulong fenceid { get; set; }
        public string fencename { get; set; }
        public string videoip { get; set; }
        public int videoport { get; set; }
        public int videochannel { get; set; }
    }
}

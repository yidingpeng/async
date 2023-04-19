using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.Position.Models
{
    class LsBaseStatus
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }
        public uint baseid { get; set; }
        public int status { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
        public int mapid { get; set; }
    }
}

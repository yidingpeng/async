using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.Position.Models
{
    public struct LsPersonSI
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }
        public ushort frameAll { get; set; }
        public ushort frameID { get; set; }
        public uint tag_counts { get; set; }
        public uint online_counts { get; set; }
        public List<MapTagsInfo> mpi { get; set; }
    }
}

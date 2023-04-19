using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.Position.Models
{
    class LsIOState
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }
        public ushort frameAll { get; set; }
        public ushort frameID { get; set; }
        public List<LsAreaInfo> areas { get; set; }
        public override string ToString()
        {
            return String.Format("{0}: {1}:{2}", frameAll, frameID, areas);
        }
    }
}

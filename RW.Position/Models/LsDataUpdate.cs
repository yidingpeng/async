using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.Position.Models
{
    public struct LsDataUpdate
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }
        public int updatetype { get; set; }
        public int op { get; set; }
    }
}

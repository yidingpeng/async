using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.Position.Models
{
    public class LsErrorInfo
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }
        public int errno { get; set; }

        public string errmsg { get; set; }
    }
}

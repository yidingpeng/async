using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.Position.Models
{
    public class ProccessSuccessFlag
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int id { get; set; }
        public string processingName { get; set; }
        public int processingValue { get; set; }

    }
}

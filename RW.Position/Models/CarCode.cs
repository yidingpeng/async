using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.Position.Models
{
    public class CarCode
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }
        public ulong tagid { get; set; }
        public string  vehicleCode { get; set; }
        public int accessState { get; set; }
    }
}

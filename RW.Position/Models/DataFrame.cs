using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.Position.Models
{
    public  class DataFrame
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }
        public byte[] data {get; set;}
        public string sendOrReceiveSign { get; set; }
        public int sendOrReceiveSignSequence { get; set; }
        public string dataName { get; set; }
    }
}

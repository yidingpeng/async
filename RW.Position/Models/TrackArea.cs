using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.Position.Models
{
    public class TrackArea
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int id { get; set; }
        public string Trackcode { get; set; }
        public int xmin { get; set; }
        public int xmax { get; set; }
        public int ymin { get; set; }
        public int ymax { get; set; }
    }
}

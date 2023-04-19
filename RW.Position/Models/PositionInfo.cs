using FreeSql.DataAnnotations;
using System;

namespace RW.Position.Models
{
    public class PositionInfo
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }
        public ulong tagid { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }
        public uint mapid { get; set; }
        public uint batt { get; set; }
        public bool sleep { get; set; }
        public bool charging { get; set; }
        public uint timestamp { get; set; }
        public int floor { get; set; }
        public int dim { get; set; }
        public override string ToString()
        {
            return String.Format("{0}: {1}:{2}: {3}:{4}: {5}:{6}: {7}: {8}: {9}: {10}", tagid, x,y,z,mapid,batt,sleep,charging,timestamp,floor,dim);
        }
    }
}

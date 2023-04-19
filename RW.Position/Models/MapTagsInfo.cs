using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.Position.Models
{
    public struct MapTagsInfo
    {
        public uint mapid { get; set; }
        public string mapname { get; set; }
        public List<ulong> tags { get; set; }
    }
}

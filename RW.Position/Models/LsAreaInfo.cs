using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.Position.Models
{
    public struct LsAreaInfo
    {
        public ulong areaid { get; set; }
        public string areaname { get; set; }
        public List<RegTagInfo> RegTagList { get; set; }
        public override string ToString()
        {
            return String.Format("{0}: {1}:{2}", areaid, areaname, RegTagList);
        }
    }
}

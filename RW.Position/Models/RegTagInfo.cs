using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.Position.Models
{
    public struct RegTagInfo
    {
        public ulong tagid { get; set; }
        public string tagname { get; set; }
        public string grpname { get; set; }
        public int state { get; set; }
        public ulong intime { get; set; }
        public ulong outtime { get; set; }
        public uint timelong { get; set; }
        public int resver { get; set; }
    }
}

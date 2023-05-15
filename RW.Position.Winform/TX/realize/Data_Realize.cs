using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RW.Position.TX.Communication;

namespace RW.Position.TX.realize
{
    public class Data_Realize : IData_Realize_interface
    {
        /// <summary>
        /// 状态
        /// </summary>
        public bool State { get; set; }
        /// <summary>
        /// 服务端Ip信息
        /// </summary>
        public string IP_info { get; set; }
        public List<BaseDataType> fulldata { get; set; }
        public List<BaseDataType> senddata { get; set; }

    
        public Data_Realize()
        {

        }

       

        public virtual void Fulldata(byte[] data)
        {

        }

        public virtual bool? Senddata(byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}

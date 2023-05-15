using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RW.Position.TX.realize
{

    public interface IData_Realize_interface
    {
        //private static object locked = new object();
        /// <summary>
        /// 连接状态
        /// </summary>
        bool State { get; set; }
        /// <summary>
        /// 接收的数据
        /// </summary>
        List<BaseDataType> fulldata { get; set; }

        /// <summary>
        /// 发送的数据
        /// </summary>
        List<BaseDataType> senddata { get; set; }

       
        /// <summary>
        /// 匹配 ip地址
        /// </summary>
        //string TCP_IP { get; set; }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data"></param>
        bool? Senddata(byte[] data);
        /// <summary>
        /// 填充数据
        /// </summary>
        /// <param name="data"></param>
        void Fulldata(byte[] data);
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RW.Position.TX.Communication
{
    interface ITCP_Server_interface
    {
        /// <summary>
        /// 服务端的IP地址，一般使用本机地址
        /// </summary>
        string IP { get; set; }
        /// <summary>
        /// 绑定的端口
        /// </summary>
        int Port { get; set; }


        /// <summary>
        /// 套接字
        /// </summary>
        Socket socket { get; set; }


        /// <summary>
        /// 接收数据事件
        /// </summary>
        event Action<Socket> TcpShowMsgEvent;
        /// <summary>
        /// 连接事件
        /// </summary>
        event Action<string, bool> TcpstateEvent;

        /// <summary>
        /// 初始化方法
        /// </summary>
        void initialize();
      
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RW.Position.TX.Communication
{
    interface ITCP_Client_interface
    {  /// <summary>
       /// 本机地址,需要Bind时才用到
       /// </summary>
        string IP { get; set; }
        /// <summary>
        /// 绑定的端口,需要Bind时才用到
        /// </summary>
        int Port { get; set; }

        /// <summary>
        /// 远程端口
        /// </summary>
        int TelePort { get; set; }

        /// <summary>
        /// 远程IP
        /// </summary>
        string TeleIP { get; set; }
        /// <summary>
        /// 套接字
        /// </summary>
        Socket socket { get; set; }


        /// <summary>
        /// 接收数据事件
        /// </summary>
        event Action<byte[]> TcpShowMsgEvent;
        /// <summary>
        /// 连接事件
        /// </summary>
        event Action<string, bool> TcpstateEvent;

        /// <summary>
        /// 初始化方法
        /// </summary>
        void initialize();
        /// <summary>
        /// 监听方法
        /// </summary>
        void monitor();
        /// <summary>
        /// 发送数据方法
        /// </summary>
        /// <param name="senddata"></param>
        bool? send(byte[] senddata);
    }
}

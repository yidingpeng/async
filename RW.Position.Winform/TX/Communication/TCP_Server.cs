using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RW.Position.TX.Communication
{
    class TCP_Server : ITCP_Server_interface
    {

        private static readonly TCP_Server instance = new TCP_Server();

        static TCP_Server() { }
        private TCP_Server() { initialize(); }

        public static TCP_Server Instance { get { return instance; } }
        public string IP { get { return GetIP(); } set { } }
        public int Port { get; set; } = 30000;//从配置文件中读取
        public Socket socket { get; set; }
     
 

        public event Action<Socket> TcpShowMsgEvent;
        public event Action<string, bool> TcpstateEvent;

        string GetIP()
        {
            IPHostEntry iPHostEntry = Dns.GetHostEntry(Dns.GetHostName());
            return iPHostEntry.AddressList[3].ToString();
        }
        public void initialize()
        {
            try
            {
                if (!PortInUse(Port))
                {
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                    IPAddress iP = IPAddress.Parse(IP);
                    IPEndPoint endPoint = new IPEndPoint(iP, Port);
                    socket.Bind(endPoint);
                    socket.Listen(5);
                    Thread th = new Thread(new ThreadStart(() =>
                    {
                        while (true)
                        {
                            Socket connectClient = null;
                            connectClient = socket.Accept();
                            if (connectClient != null)
                            {
                                TcpstateEvent?.Invoke(connectClient.RemoteEndPoint.ToString(),true);
                                TcpShowMsgEvent?.Invoke(connectClient);

                            }
                        }
                    }));
                    th.Start();
                    th.Name = "等待连接";
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 查询端口是否被占用
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        bool PortInUse(int port)
        {
            bool inUse = false;

            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();

            if (ipEndPoints.Where(s => s.Port == port).FirstOrDefault() != null)
            {
                inUse = true;
            }
            return inUse;
            //foreach (IPEndPoint endPoint in ipEndPoints)
            //{
            //    if (endPoint.Port == port)
            //    {
            //        inUse = true;
            //        break;
            //    }
            //}
            //return inUse;
        }
    }
}

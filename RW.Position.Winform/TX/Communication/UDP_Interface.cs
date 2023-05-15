
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


namespace MetorSignalSimulator.UI.Driver
{
    public class TCPStation
    {
        public TCPStation()
        {

        }

        /// <summary>
        /// 连接事件
        /// </summary>
        public Action<bool> TcpstateEvent;
        /// <summary>
        /// 接收数据事件
        /// </summary>
        public Action<byte[]> TcpShowMsgEvent;

        
        Socket socketSend = null;
        /// <summary>
        /// 连接服务
        /// </summary>
        public void ConnectTcp()
        {
            Thread c_thread = new Thread(Received);
            c_thread.IsBackground = true;
            c_thread.Start();
            ConetntTCP();
        }
        object locked = new object();
        private void ConetntTCP()
        {
            try
            {
                //创建客户端Socket，获得远程ip和端口号
                socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ip = IPAddress.Parse("");
                IPEndPoint point = new IPEndPoint(ip, int.Parse(""));
                socketSend.Bind(point);
                lianjieTCP(point);
                ShowNotice(true, "连接成功!");
                //开启新的线程，不停的接收服务器发来的消息

            }
            catch (Exception ex)
            {
                //socketSend.Close();
                ShowNotice(false, "连接失败！请检查服务端是否开启，IP或者端口号错误...");

            }
        }

        public void lianjieTCP(IPEndPoint point)
        {
            try
            {
                socketSend.Connect(point);
            }
            catch (Exception ex)
            {
                ShowNotice(false, "连接失败！请检查服务端是否开启，IP或者端口号错误...");
                Thread.Sleep(5000);
                lianjieTCP(point);
            }
        }
        public bool State
        {
            get { return _State; }
            set
            {
                if (_State != value && value == true)
                {
                    socketSend.Close();
                    Thread.Sleep(5000);
                    ConetntTCP();
                }
                _State = value;
            }
        }
        public bool _State
        {
            get;
            set;
        } = false;
        /// <summary>
        /// 数据接收
        /// </summary>
        /// <param name="str"></param>
        void ShowMsg(byte[] str)
        {
            TcpShowMsgEvent?.Invoke(str);
        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        public void CloseTcp()
        {
            socketSend.Close();
        }
        /// <summary>
        /// 数据发送
        /// </summary>
        /// <param name="senddata"></param>
        public void TCPSend(byte[] senddata)
        {
            try
            {
                socketSend.Send(senddata);
            }
            catch (Exception ex) { }
        }
        void ShowNotice(bool result, string str)
        {
            
            TcpstateEvent?.Invoke(true);
        }
        /// <summary>
        /// 接收服务端返回的消息
        /// </summary>
        void Received()
        {
            while (true)
            {
                try
                {

                    byte[] buffer = new byte[1024 * 1024 * 3];

                    if (socketSend.Available == 0 && socketSend.Poll(1000, SelectMode.SelectRead))
                    {

                    }
                    else
                    {
                        State = false;
                    }
                    //实际接收到的有效字节数
                    int len = socketSend.Receive(buffer);


                    if (len == 0)
                    {
                        continue;
                    }
                    ShowMsg(buffer.Take(len).ToArray());

                }
                catch (Exception EX)
                {
                    if (socketSend != null && socketSend.Available == 0 && socketSend.Poll(1000, SelectMode.SelectRead))
                    {
                        State = true;
                    }
                    else
                    {
                        State = false;
                    }
                }
            }
        }
    }


    public class TCPServer
    {
        public Action<Socket> initdata = null;
        public Socket server = null;

        public TCPServer()
        {

        }
        public void Init()
        {
            try
            {
                if (PortInUse(30000))
                {
                    return;
                }
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                IPAddress iP = IPAddress.Parse("10.0.10.14");
                IPEndPoint endPoint = new IPEndPoint(iP, 30000);
                server.Bind(endPoint);
                server.Listen(5);

                Thread th = new Thread(new ThreadStart(() =>
                {
                    while (true)
                    {
                        Socket connectClient = null;
                        connectClient = server.Accept();
                        if (connectClient != null)
                        {
                            initdata?.Invoke(connectClient);
                        }
                    }
                }));
                th.Start();
                th.Name = $"客户端数据";
                th.IsBackground = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool PortInUse(int port)
        {
            bool inUse = false;

            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();

            foreach (IPEndPoint endPoint in ipEndPoints)
            {
                if (endPoint.Port == port)
                {
                    inUse = true;
                    break;
                }
            }

            return inUse;
        }

    }
}

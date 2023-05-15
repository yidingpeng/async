using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RW.Position.TX.Communication
{
    public class TCP_Client : ITCP_Client_interface
    {

        public TCP_Client() {  }

        public string IP { get; set; }
        public int Port { get; set; }
        public Socket socket { get; set; }

        public int TelePort { get; set; } = 12345;
        public string TeleIP { get; set; } = "127.0.0.1";

        public event Action<byte[]> TcpShowMsgEvent;
        public event Action<string, bool> TcpstateEvent;

        public Func<byte[],bool?> Send;
        public void initialize()
        {
            Thread th_monitor = new Thread(monitor);
            th_monitor.Name = "监听TCPclient线程" + TeleIP + ":" + TelePort;
            th_monitor.IsBackground = true;
            th_monitor.Start();

            Send = send;

            Connet();
        }
        void Connet()
        {
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint point = new IPEndPoint(IPAddress.Parse(TeleIP), TelePort);
                socket.Connect(point);

                TcpstateEvent?.Invoke(TeleIP + "：" + TelePort, true);
            }
            catch (Exception ex)
            {
                Thread.Sleep(5000);
                //写日志
                Close();
                Connet();
            }
        }
        public void monitor()
        {
            while (true)
            {
                try
                {
                    if (socket == null)
                    {
                        continue;
                    }
                    byte[] buffer = new byte[1024 * 1024 * 3];
                    int len = socket.Receive(buffer);
                    if (len == 0)
                    {
                        continue;
                    }
                    TcpShowMsgEvent?.Invoke(buffer.Take(len).ToArray());
                    //ShowMsg(buffer.Take(len).ToArray());
                }
                catch (Exception EX)
                {
                    //写日志
                    Close();
                    Connet();
                }
            }
        }
        
        public bool? send(byte[] senddata)
        {
            try
            {
                socket.Send(senddata);
                return true;
            }
            catch (Exception ex)
            {
                Close(); 
                return false;
               
            }
        }
        static object locked = new object();
        void Close()
        {
            lock (locked)
            {
                socket?.Close();
                socket = null;
                TcpstateEvent?.Invoke(TeleIP + "：" + TelePort, false);
            }
        }

      
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RW.Position.TX.Communication;

namespace RW.Position.TX.realize
{
    public class TCP_Server_Data : Data_Realize
    {
        public TCP_Server_Data()
        {
            TCP_Server.Instance.TcpShowMsgEvent += TcpShowMsgEvent;
            TCP_Server.Instance.TcpstateEvent += Instance_TcpstateEvent;
        }
        public Func<byte[], bool> SendToClient;
        /// <summary>
        /// 连接事件
        /// </summary>
        /// <param name="str"></param>
        /// <param name="obj"></param>
        private void Instance_TcpstateEvent(string str, bool obj)
        {
            if (TCP_IP != str.Split(':')[0])
            {
                return;
            }
            State = obj;
        }

        public string TCP_IP = "192.168.0.17";
        /// <summary>
        /// 接受数据事件
        /// </summary>
        /// <param name="Client_Socket"></param>
        public void TcpShowMsgEvent(System.Net.Sockets.Socket Client_Socket)
        {
            //判断客户端Ip是否相符
            string infor = Client_Socket.RemoteEndPoint.ToString();
            if (TCP_IP != infor.Split(':')[0])
            {
                return;
            }
            IP_info = TCP_IP;

            //拿到套接字，读取数据，回复数据；
            Action Full = new Action(() =>
            {
                while (true)
                {
                    try
                    {
                        byte[] arrMsg = new byte[1024 * 1024];
                        int length = Client_Socket.Receive(arrMsg);
                        if (length > 0)
                        {
                            Fulldata(arrMsg.Take(length).ToArray());
                        }
                        else if (Client_Socket.Poll(1000, SelectMode.SelectRead))
                        {
                            Client_Socket.Close();
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        State = false;
                        Client_Socket.Close();
                        return;
                    }
                }
            });
            Full.BeginInvoke(null, null);


            SendToClient = (Souce) =>
            {
                try
                {
                    Client_Socket.Send(Souce);
                    return true;
                }
                catch (Exception)
                {
                    State = false;
                    Client_Socket.Close();
                    return false;
                }

            };


            Action Send = new Action(() =>
            {
                while (true)
                {
                    var data = new byte[] { };//组合数据发送

                    Senddata(data);
                    Thread.Sleep(100);
                }
            });
            Send.BeginInvoke(null, null);
        }

        public override void Fulldata(byte[] data)
        {
            //将数据填充至fulldata
            fulldata = null;
            //throw new NotImplementedException();
        }
        public override bool? Senddata(byte[] data)
        {
            return SendToClient?.Invoke(data);
        }

    }
}

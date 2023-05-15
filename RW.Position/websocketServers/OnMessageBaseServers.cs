using Newtonsoft.Json;
using RW.Position.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace RW.Position.websocketServers
{
    public class OnMessageBaseServers: WebSocketBehavior
    {
        private static List<LsBaseStatus> websocketData { get; set; }
        static int flag = 0;
        private readonly Socket _socket;
        public OnMessageBaseServers()
        {
            Thread th_monitor = new Thread(monitor);

            th_monitor.IsBackground = true;
            th_monitor.Start();

        }
        public OnMessageBaseServers( Socket socket) : this()
        {
            
            _socket = socket;
        }
        public void monitor()
        {
            while (true)
            {

                if (_socket == null)
                {
                    continue;
                }
                byte[] buffer = new byte[1024 * 1024 * 3];
                int len = _socket.Receive(buffer);
                if (len == 0)
                {
                    continue;
                }
                var s = (bool)Fulldata(buffer.Take(len).ToArray());
                //if (receive == true) MessageBox.Show(receive.ToString());
                Console.WriteLine(s + "2222");

            }
        }
        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="data"></param>
        public bool Fulldata(byte[] data)
        {
            return true;
            //int positonChangeCount = data.Count(b => b == positonChangeDataCheck);
            //int carTrackCount = data.Count(b => b == carTrackDataCheck);
            //int trackCarCount = data.Count(b => (b == trackCarDataCheck || b == 0x37 || b == 0x13));
            //int shoesDataCount = data.Count(b => (b == shoesDataCheck));
            //int shoesChangeDataCount = data.Count(b => b == shoesChangeDataCheck);
            //int syestemRequestAlarmDataCount = data.Count(b => b == syestemRequestAlarmDataCheck);

            // MessageBox.Show(positonChangeCount.ToString());
            //if (positonChangeCount > 0)
            //{
            //    if (positonChangeDataCheck == 0x23) successSend = 0;
            //    return true;
            //}
            //else if (trackCarCount > 0)
            //{
            //    if (data.Count(b => b == 0x13) > 0)
            //    {
            //        trackCarDataCheck = 0x23;
            //    }
            //    if (data.Count(b => b == 0x37) > 0)
            //    {
            //        trackCarDataCheck = 0x37;
            //    }
            //    return true;
            //}


            //else if (carTrackCount > 0)
            //{
            //    if ((carTrackDataCheck == 0x33) || (carTrackDataCheck == 0x10))
            //    {
            //        if (carTrackDataCheck == 0x10)
            //        {
            //            MessageBox.Show("数据发送完成，请点击发送停止按钮");
            //        }
            //        OnHandlerCarTrackRequstReceive();
            //        carTrackDataCheck = 0x10;

            //    }
            //    if (carTrackDataCheck == 0x23) successSend = 0;
            //    return true;
            //}
            //else if (shoesDataCount > 0)
            //{
            //    if (shoesDataCheck == 0x20)
            //        Send(ShoesRequestAnswerConfirm);

            //    return true;
            //}
            //else if (shoesChangeDataCount > 0)
            //{
            //    if (shoesChangeDataCheck == 0x23) { successSend = 0; return false; }
            //    return true;
            //}
            //else if (syestemRequestAlarmDataCount > 0)
            //{

            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }
        public void getBaseValue(object sender, events.LsEventArgs<List<LsBaseStatus>> e)
        {
             websocketData = e.Data;
            int flag = 1;
            //byte[] bytes = { 2 };
            //_socket.Send(bytes);
            //_socket.Close();
            //foreach (Models.LsBaseStatus item in e.Data)
            //{
            //    Console.WriteLine("事件触发"+DateTime.Now.ToString() + "  基站: {0}  状态: {1}  地图: {2}", item.baseid, item.status, item.mapid);
            //}
        }
        protected override void OnMessage(MessageEventArgs e)
        {
            // handle message received from client
            if (websocketData != null)
            {
                while (true)
                {
                    foreach (LsBaseStatus item in websocketData)
                    {
                        Console.WriteLine("事件触发" + DateTime.Now.ToString() + "  基站: {0}  状态: {1}  地图: {2}", item.baseid, item.status, item.mapid);
                    }
                    var jsonData = JsonConvert.SerializeObject(websocketData);
                    Send(jsonData);
                }
            }

        }
    }
}

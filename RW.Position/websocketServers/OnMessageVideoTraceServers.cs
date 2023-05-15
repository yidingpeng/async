using Newtonsoft.Json;
using RW.Position.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace RW.Position.websocketServers
{
    public class OnMessageVideoTraceServers : WebSocketBehavior
    {
        private static LsTagVideoTrace websocketData { get; set; }
        public void getVideoTraceValue(object sender, events.LsEventArgs<LsTagVideoTrace> e)
        {
            //Console.Write("事件触发报警id：{0}， 消息：{1}\n", e.Data.mode, e.Data.port);
        }
        protected override void OnMessage(MessageEventArgs e)
        {
            // handle message received from client
            if (ReferenceEquals(websocketData, null))
            {
                while (true)
                {


                 //   Console.WriteLine(DateTime.Now.ToString() + "  标签: {0}  区域名: {1}  状态: {2}  地图: {3}", websocketData.tagid, websocketData.areaname, websocketData.status, websocketData.mapid);

                    var jsonData = JsonConvert.SerializeObject(websocketData);
                    Send(jsonData);
                }
            }

        }
    }
}

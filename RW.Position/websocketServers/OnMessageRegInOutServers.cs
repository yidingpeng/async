using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.Server;
using RW.Position.Models;
using WebSocketSharp;
using Newtonsoft.Json;

namespace RW.Position.websocketServers
{
    public class OnMessageRegInOutServers : WebSocketBehavior
    {
        private static LsRegInOutInfo websocketData { get; set; }
        public void getRegInOutInfoValue(object sender, events.LsEventArgs<LsRegInOutInfo> e)
        {
            Console.WriteLine(DateTime.Now.ToString() + "  标签: {0}  区域名: {1}  状态: {2}  地图: {3}", e.Data.tagid, e.Data.areaname, e.Data.status, e.Data.mapid);
        }
        protected override void OnMessage(MessageEventArgs e)
        {
            // handle message received from client
            if (ReferenceEquals(websocketData, null))
            {
                while (true)
                {


                    Console.WriteLine(DateTime.Now.ToString() + "  标签: {0}  区域名: {1}  状态: {2}  地图: {3}", websocketData.tagid, websocketData.areaname, websocketData.status, websocketData.mapid);

                    var jsonData = JsonConvert.SerializeObject(websocketData);
                    Send(jsonData);
                }
            }

        }
    }
}

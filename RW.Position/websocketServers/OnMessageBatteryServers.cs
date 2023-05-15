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
    public class OnMessageBatteryServers : WebSocketBehavior
    {
        private static List<LsBatteryCapInfo> websocketData { get; set; }
        public void getBatteryValue(object sender, events.LsEventArgs<List<LsBatteryCapInfo>> e)
        {
            foreach (Models.LsBatteryCapInfo item in e.Data)
            {
                Console.WriteLine("事件触发"+DateTime.Now.ToString() + "  标签: {0}  电量: {1}  是否充电: {2}", item.tagid, item.battcap, item.charging);

            }
        }
        protected override void OnMessage(MessageEventArgs e)
        {
            // handle message received from client
            if (websocketData != null)
            {



                while (true)
                {
                    foreach (LsBatteryCapInfo item in websocketData)
                    {
                        Console.WriteLine("事件触发" + DateTime.Now.ToString() + "  标签: {0}  电量: {1}  是否充电: {2}", item.tagid, item.battcap, item.charging);

                    }
                    var jsonData = JsonConvert.SerializeObject(websocketData);
                    Send(jsonData);
                }
            }

        }
    }
}

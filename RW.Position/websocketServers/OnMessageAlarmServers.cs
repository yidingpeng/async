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
    public class OnMessageAlarmServers : WebSocketBehavior
    {
        private static List<LsExtendAlarmInfo> websocketData { get; set; }
        public void getAlarmValue(object sender, events.LsEventArgs<LsExtendAlarmInfo> e)
        {          
                Console.Write("事件触发报警id：{0}， 消息：{1}\n", e.Data.alarmid, e.Data.alarmmsg);           
        }
        protected override void OnMessage(MessageEventArgs e)
        {
            // handle message received from client
            if (websocketData != null)
            {
                while (true)
                {
                    foreach (LsExtendAlarmInfo item in websocketData)
                    {
                        Console.Write("事件触发报警id：{0}， 消息：{1}\n", item.alarmid, item.alarmmsg);
                    }
                    var jsonData = JsonConvert.SerializeObject(websocketData);
                    Send(jsonData);
                }
            }
        }
    }
}

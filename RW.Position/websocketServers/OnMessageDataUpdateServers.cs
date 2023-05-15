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
    public class OnMessageDataUpdateServers : WebSocketBehavior
    {
        private static LsDataUpdate websocketData { get; set; }

        public void getDataUpdateValue(object sender, events.LsEventArgs<LsDataUpdate> e)
        {
            Console.WriteLine("触发事件"+DateTime.Now.ToString() + "  类型: {0}  操作: {1} ", e.Data.updatetype, e.Data.op);
        }
        protected override void OnMessage(MessageEventArgs e)
        {
            // handle message received from client
            if (ReferenceEquals(websocketData, null))
            {
                while (true)
                {

                    Console.WriteLine("触发事件" + DateTime.Now.ToString() + "  类型: {0}  操作: {1} ", websocketData.updatetype, websocketData.op);


                    var jsonData = JsonConvert.SerializeObject(websocketData);
                    Send(jsonData);
                }
            }

        }
    }
}

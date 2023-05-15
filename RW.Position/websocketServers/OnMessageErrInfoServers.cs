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
    public class OnMessageErrInfoServers: WebSocketBehavior
    {
        private static LsErrorInfo websocketData { get; set; }
        public void getErrInfoValue(object sender, events.LsEventArgs<LsErrorInfo> e)
        {
            Console.WriteLine(DateTime.Now.ToString() + "  报错信息: {0}  ", e.Data.errmsg);
        }
        protected override void OnMessage(MessageEventArgs e)
        {
            // handle message received from client
            if (ReferenceEquals(websocketData, null))
            {
                while (true)
                {

                    Console.WriteLine(DateTime.Now.ToString() + "  报错信息: {0}  ", websocketData.errmsg);


                    var jsonData = JsonConvert.SerializeObject(websocketData);
                    Send(jsonData);
                }
            }

        }
    }
}

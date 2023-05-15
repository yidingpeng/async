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
    public class OnMessagePeopleSIServer :WebSocketBehavior
    {
        private static LsPersonSI websocketData { get; set; }
        public void getPeopleSIValue(object sender, events.LsEventArgs<LsPersonSI> e)
        {
            Console.WriteLine("事件触发总帧数：{0}，当前帧：{1}", e.Data.frameAll, e.Data.frameID);
            Console.WriteLine("标签总数:{0},标签在线数目:{1}", e.Data.tag_counts, e.Data.online_counts);
            foreach (MapTagsInfo rti in e.Data.mpi)
            {
                Console.WriteLine("地图ID:{0},地图名称:{1}", rti.mapid, rti.mapname);
                foreach (uint tagid in rti.tags)
                {
                    Console.Write("标签ID:{0}\t", tagid);
                }
            }
            Console.Write("\n");
        }
        protected override void OnMessage(MessageEventArgs e)
        {
            // handle message received from client
            if (ReferenceEquals(websocketData, null))
            {
                while (true)
                {

                    Console.WriteLine("事件触发总帧数：{0}，当前帧：{1}", websocketData.frameAll, websocketData.frameID);
                    Console.WriteLine("标签总数:{0},标签在线数目:{1}", websocketData.tag_counts, websocketData.online_counts);
                    foreach (MapTagsInfo rti in websocketData.mpi)
                    {
                        Console.WriteLine("地图ID:{0},地图名称:{1}", rti.mapid, rti.mapname);
                        foreach (uint tagid in rti.tags)
                        {
                            Console.Write("标签ID:{0}\t", tagid);
                        }
                    }
                    Console.Write("\n");


                    var jsonData = JsonConvert.SerializeObject(websocketData);
                    Send(jsonData);
                }
            }

        }
    }
}

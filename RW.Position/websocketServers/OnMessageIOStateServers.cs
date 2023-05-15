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
    public class OnMessageIOStateServers : WebSocketBehavior
    {
        private static LsIOState websocketData { get; set; }

        public void getIOStateValue(object sender, events.LsEventArgs<LsIOState> e)
        {

            Console.WriteLine("事件触发总帧数：{0}，当前帧：{1}", e.Data.frameAll, e.Data.frameID);
            foreach (LsAreaInfo item in e.Data.areas)
            {
                Console.WriteLine("区域ID: {0}  区域名称: {1} ", item.areaid, item.areaname);
                foreach (RegTagInfo rti in item.RegTagList)
                {
                    Console.WriteLine("tagID: {0}  tagname: {1} grpname: {2} intime :{3} outtime :{4},停留时间：{5},状态：{6}",
                        rti.tagid, rti.tagname, rti.grpname, rti.intime, rti.outtime, rti.timelong, rti.state == 0 ? "进区域" : "出区域");
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
                    foreach (LsAreaInfo item in websocketData.areas)
                    {
                        Console.WriteLine("区域ID: {0}  区域名称: {1} ", item.areaid, item.areaname);
                        foreach (RegTagInfo rti in item.RegTagList)
                        {
                            Console.WriteLine("tagID: {0}  tagname: {1} grpname: {2} intime :{3} outtime :{4},停留时间：{5},状态：{6}",
                                rti.tagid, rti.tagname, rti.grpname, rti.intime, rti.outtime, rti.timelong, rti.state == 0 ? "进区域" : "出区域");
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

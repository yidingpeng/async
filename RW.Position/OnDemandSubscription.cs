using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LsWebsocketClient;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Configuration;
using RW.Position.Common;
using RW.Position.Extensions;
using RW.Position.Models;
using LsBatteryCapInfo = RW.Position.Models.LsBatteryCapInfo;

namespace RW.Position
{
    public class OnDemandSubscription
    {
        private readonly LocalsenseInterface _client;
        private readonly IMapper _mapper;
        private readonly IFreeSql _freeSql;

        public OnDemandSubscription() {
            string ip=ConfigurationService.config["websocketConnectionStrings:ip"];
            int Port = int.Parse(ConfigurationService.config["websocketConnectionStrings:Port"]);
            string user = ConfigurationService.config["websocketConnectionStrings:user"];
            string passwd = ConfigurationService.config["websocketConnectionStrings:passwd"];
            var setting = new CommonSetting(ip,Port,user,passwd);
            _client = new LocalsenseInterface(setting.IP, setting.Port, setting.UserName, setting.Password, setting.Salt);
            ////标签数据位置等信息 1
            //_client.OnMessagePos += (sender, e) =>
            //{
            //    Console.WriteLine("111111111111111111111111111111111111111111111111111111");
            //    OnHandlerPos(e.Data);
            //};
            ////标签电量信息 1
            //_client.OnMessageBattCap += (sender, e) =>
            //{
            //    Console.WriteLine("222222222222222222222222222222222222222222222222222222222");
            //  //  Console.WriteLine((_mapper.Map<List<LsBatteryCapInfo>>(e.Data))[0]);
            //};
            ////基站信息 1
            //_client.OnMessageBaseStatus += (sender, e) =>
            //{
            //    Console.WriteLine("333333333333333333333333333333333333333333");
            //   // Console.WriteLine((_mapper.Map<List<Models.LsBaseStatus>>(e.Data))[0]);
            //  //  Console.WriteLine((_mapper.Map<List<Models.LsBaseStatus>>(e.Data))[1]);
            //};

            ////报警信息 1
            //_client.OnMessageAlarm += (sender, e) =>
            //{
            //    Console.WriteLine("4444444444444444444444444444444444444444444444444444444444444444");
            //  //  Console.WriteLine((_mapper.Map<Models.LsExtendAlarmInfo>(e.Data)));
            //};
            ////区域信息 0
            //_client.OnMessageIOState += (sender, e) =>
            //{
            //    Console.WriteLine("55555555555555555555555555555555555555555555555555555555555");
            //   // Console.WriteLine((_mapper.Map<Models.LsIOState>(e.Data)));
            //};
            //// 更新信息 0
            //_client.OnMessageDataUpdate += (sender, e) =>
            //{
            //    Console.WriteLine("666666666666666666666666666666666666666666666666666666666666");
            //   // Console.WriteLine((_mapper.Map<Models.LsIOState>(e.Data)));
            //};
            ////心率信息 0
            //_client.OnMessageHeartRate += (sender, e) =>
            //   {
            //       Console.WriteLine("77777777777777777777777777777777777777777777777777777777777");
            //      // Console.WriteLine((_mapper.Map<Models.LsIOState>(e.Data)));
            //   };
            ////地图在线标签信息  1
            //_client.OnMessagePeopleSI += (sender, e) =>
            //{
            //    Console.WriteLine("8888888888888888888888888888888888888888888888888888888888888");
            //   // Console.WriteLine((_mapper.Map<Models.LsIOState>(e.Data)));
            //};
           
            //摄像头信息  0
            _client.OnMessageVideoTrace += (sender, e) =>
            {
                Console.WriteLine("十十十十十十十十十十十十十十十十十十十十十十十十十十十十");
              //  Console.WriteLine((_mapper.Map<Models.LsIOState>(e.Data)));
            };
            //区域进出信息  0
            _client.OnMessageRegInOut += (sender, e) =>
            {
                Console.WriteLine("wgswgswgswgswgs");
               // Console.WriteLine((_mapper.Map<Models.LsIOState>(e.Data)));
            };
            //WGS信息 0
            _client.OnMessageWGS_Pos += (sender, e) =>
            {
                Console.WriteLine("1221212122221212212121121");
               // Console.WriteLine((_mapper.Map<Models.LsIOState>(e.Data)));
            };
            //Global信息 0
            _client.OnMessageGlobal_Pos += (sender, e) =>
            {
                Console.WriteLine("131313331313131313113111313");
               // Console.WriteLine((_mapper.Map<Models.LsIOState>(e.Data)));
            };
            // 报错信息 0
            _client.OnMessageErrInfo += (sender, e) =>
            {
                     Console.WriteLine("131313331313131313113111313");
                     // Console.WriteLine((_mapper.Map<Models.LsIOState>(e.Data)));
            };
        }

        public OnDemandSubscription(IMapper mapper, IFreeSql freeSql) : this()
        {
            _mapper = mapper;
            _freeSql = freeSql;
            
        }

        /// <summary>
        /// 标签信息回调
        /// </summary>
        /// <param name="data"></param>
        public void OnHandlerPos(List<LsPosInfo> data)
        {
            //Console.WriteLine(data);
            var addModel = _mapper.Map<List<PositionInfo>>(data);
     //       Console.WriteLine((addModel[0]).ToString());
//            _freeSql.Insert(addModel).ExecuteAffrows();

        }
    }
}

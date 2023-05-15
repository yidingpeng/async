using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using MapsterMapper;
using LsWebsocketClient;

using Microsoft.Extensions.Configuration;
using RW.Position.Common;
using RW.Position.events;
using RW.Position.WinForm.Extensions;
using RW.Position.websocketServers;


namespace RW.Position.WinForm
{
    public class OnDemandSubscription
    {
        private readonly LocalsenseInterface _client;
        private readonly IMapper _mapper;

  
        Publisher _publisher;
        websocketServers.OnMessagePOSServers _onMessagePOSServers;
        public OnDemandSubscription()
        {
            string ip = ConfigurationService.config["websocketConnectionStrings:ip"];
            int Port = int.Parse(ConfigurationService.config["websocketConnectionStrings:Port"]);
            string user = ConfigurationService.config["websocketConnectionStrings:user"];
            string passwd = ConfigurationService.config["websocketConnectionStrings:passwd"];
            var setting = new CommonSetting(ip, Port, user, passwd);
            _client = new LocalsenseInterface(setting.IP, setting.Port, setting.UserName, setting.Password, setting.Salt);
           
            //接收位置数据事件
            _client.OnMessagePos += (sender, e) =>
            {
                var addModel = _mapper.Map<List<Models.PositionInfo>>(e.Data);
                OnHandlerPos(e.Data);
                //foreach (Models.PositionInfo item in addModel)
                //{
                //    Console.WriteLine("WGS坐标 标签: {0}  坐标: x: {1}  y: {2} z: {3} regid: {4}  sleep: {5} batt: {6}",
                //      item.tagid, (item.x), (item.y), (item.z), item.mapid, item.sleep, item.batt);
                //}
            };

            //_client.OnMessageWGS_Pos += (sender, e) =>
            //    OnHandlerWGS_Pos(e.Data);

            //_client.OnMessageGlobal_Pos += (sender, e) =>
            //   OnHandlerGlobal_Pos(e.Data);
            //接收报警数据事件
            //_client.OnMessageAlarm += (sender, e) =>
            //    OnHandlerAlarm(e.Data);

            //接收标签电量数据事件
            //_client.OnMessageBattCap += (sender, e) =>
            //    OnHandlerBattery(e.Data);

            //接收基站数据事件
            _client.OnMessageBaseStatus += (sender, e) =>
                OnHandlerBase(e.Data);

            //_client.OnMessageHeartRate += (sender, e) =>
            //    OnHandlerHeartRate(e.Data);

            //_client.OnMessageDataUpdate += (sender, e) =>
            //    OnHandlerDataUpdate(e.Data);

            //_client.OnMessageVideoTrace += (sender, e) =>
            //    OnHandlerVideoTrace(e.Data);
            //
            // 接收考勤区域进出事件
            _client.OnMessageRegInOut += (sender, e) =>
                OnHandlerAttenceIO(e.Data);
            //接收区域统计信息事件
            //_client.OnMessageIOState += (sender, e) =>
            //    OnHandlerIOState(e.Data);
            //接收人员统计信息事件
            //_client.OnMessagePeopleSI += (sender, e) =>
            //    OnHandlerPeopleSI(e.Data);
            //接收连接报错信息事件
            //_client.OnMessageErrInfo += (sender, e) =>
            //OnHandlerErrInfo(e.Data);       
        }

        public OnDemandSubscription(IMapper mapper, OnMessagePOSServers onMessagePOSServers, Publisher publisher) : this()
        {
            _mapper = mapper;
            _onMessagePOSServers = onMessagePOSServers;
            _publisher = publisher;
        }

        ///接收位置数据 
        private  void OnHandlerPos(List<LsPosInfo> vpos)
        {
            var addModel = _mapper.Map<List<Models.PositionInfo>>(vpos);
            // 注册事件处理程序

            _publisher.EventOnHandlerPos +=  _onMessagePOSServers.getPOSValue;
            //触发事件，然后将自定义的对象作为事件触发类型传入
            _publisher.IssrueEventOnHandlerPos(addModel);

        }
        private void OnHandlerWGS_Pos(List<LsPosInfo> vpos)
        {
            foreach (LsPosInfo item in vpos)
            {
                Console.WriteLine("WGS坐标 标签: {0}  坐标: x: {1}  y: {2} z: {3} regid: {4}  sleep: {5} batt: {6}",
                    item.tagid, (item.x), (item.y), (item.z), item.mapid, item.sleep, item.batt);
            }
        }

        private void OnHandlerGlobal_Pos(List<LsPosInfo> vpos)
        {
            foreach (LsPosInfo item in vpos)
            {

                Console.WriteLine("全局坐标 标签: {0}  坐标: x: {1}  y: {2} z: {3} regid: {4}  sleep: {5} batt: {6}",
                    item.tagid, (item.x), (item.y), (item.z), item.mapid, item.sleep, item.batt);

            }
        }
        // 接收区域统计信息
        private void OnHandlerIOState(LsIOState lis)
        {
            var addModel = _mapper.Map<Models.LsIOState>(lis);
            // 注册事件处理程序
            _publisher.EventOnHandlerIOState += new websocketServers.OnMessageIOStateServers().getIOStateValue;
            //触发事件，然后将自定义的对象作为事件触发类型传入
            _publisher.IssrueEventOnHandlerIOState(addModel);
        }

        // 接收报警数据
        private void OnHandlerAlarm(LsExtendAlarmInfo alarm)
        {
            var addModel = _mapper.Map<Models.LsExtendAlarmInfo>(alarm);
            // 注册事件处理程序
            _publisher.EventOnHandlerAlarm += new websocketServers.OnMessageAlarmServers().getAlarmValue;
            //触发事件，然后将自定义的对象作为事件触发类型传入
            _publisher.IssrueEventOnHandlerAlarm(addModel);

        }

        //接受人员统计信息
        private void OnHandlerPeopleSI(LsPersonSI lpi)
        {
            var addModel = _mapper.Map<Models.LsPersonSI>(lpi);
            _publisher.EventOnHandlerPeopleSI += new websocketServers.OnMessagePeopleSIServer().getPeopleSIValue;
            //触发事件，然后将自定义的对象作为事件触发类型传入
            _publisher.IssrueEventOnHandlerPeopleSI(addModel);
        }




        // 接收电量数据
        private void OnHandlerBattery(List<LsBatteryCapInfo> data)
        {
            var addModel = _mapper.Map<List<Models.LsBatteryCapInfo>>(data);
            // 注册事件处理程序
            _publisher.EventOnHandlerBattery += new websocketServers.OnMessageBatteryServers().getBatteryValue;
            //触发事件，然后将自定义的对象作为事件触发类型传入
            _publisher.IssrueEventOnHandlerBattery(addModel);
        }

        // 接收基站数据
        private void OnHandlerBase(List<LsBaseStatus> data)
        {
            var addModel = _mapper.Map<List<Models.LsBaseStatus>>(data);
            // 注册事件处理程序
            _publisher.EventOnHandlerBase += new websocketServers.OnMessageBaseServers().getBaseValue;
            //触发事件，然后将自定义的对象作为事件触发类型传入
            _publisher.IssrueEventOnHandlerBase(addModel);
        }

        // 接收心率数据
        private void OnHandlerHeartRate(LsHeartRate data)
        {

            Console.WriteLine(DateTime.Now.ToString() + "  标签: {0}  心率: {1}  时间：{2} ", data.tagid, data.val, data.timestamp);


        }

        // 接收数据更新
        private void OnHandlerDataUpdate(LsDataUpdate data)
        {
            var addModel = _mapper.Map<Models.LsDataUpdate>(data);
            // 注册事件处理程序
            _publisher.EventOnHandlerDataUpdate += new websocketServers.OnMessageDataUpdateServers().getDataUpdateValue;
            //触发事件，然后将自定义的对象作为事件触发类型传入
            _publisher.IssrueEventOnHandlerDataUpdate(addModel);
        }

        // 接收视频联动
        private void OnHandlerVideoTrace(LsTagVideoTrace data)
        {
            Console.WriteLine(DateTime.Now.ToString() + "  标签: {0}  ip: {1}  port: {2}  success: {3}", data.tagid, data.ip, data.port, data.success);
        }

        // 接收考勤区域进出
        private void OnHandlerAttenceIO(LsRegInOutInfo data)
        {
            var addModel = _mapper.Map<Models.LsRegInOutInfo>(data);
            // 注册事件处理程序
            _publisher.EventOnHandlerRegInOutInfo += new websocketServers.OnMessageRegInOutServers().getRegInOutInfoValue;
            //触发事件，然后将自定义的对象作为事件触发类型传入
            _publisher.IssrueEventOnHandlerRegInOutInfo(addModel);
        }
        private void OnHandlerErrInfo(LsErrorInfo data)
        {
            Console.WriteLine(DateTime.Now.ToString() + "  报错信息: {0}  ", data.errmsg);
        }
    }
}

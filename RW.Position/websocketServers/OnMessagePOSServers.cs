using LsWebsocketClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.Server;
using RW.Position.websocketServers;
using RW.Position.Models;
using WebSocketSharp;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using System.Timers;
using MapsterMapper;
using System.Net.Sockets;
using System.Threading;

namespace RW.Position.websocketServers
{
    //事件订阅者
    public class OnMessagePOSServers: WebSocketBehavior
    {
        private readonly IFreeSql _freeSql;
        private readonly IMapper _mapper;
        private readonly Socket _socket;
        private static readonly object _lockObj = new object();
        private static readonly object _getPOSValue = new object();
        public int  _trackPort;
        //CRC校验中的生成多项式
        private const ushort Polynomial = 0x1021;
        public bool receive=false;//接收是否成功
    
        
        public OnMessagePOSServers(IFreeSql freeSql,IMapper mapper,Socket socket) : this()
        {
            _freeSql = freeSql;
            _mapper = mapper;
            _socket = socket;
            
        }
        public OnMessagePOSServers()
        {           
            Thread th_monitor = new Thread(monitor);       
            th_monitor.IsBackground = true;
            th_monitor.Start();          
        }
        /// <summary>
        /// //定义 订阅方法，当触发发布事件的时候就调用该方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //public    void getPOSValue(object sender, events.LsEventArgs<List<PositionInfo>> e)
        public  async Task getPOSValue( List<PositionInfo> e)
        {
            foreach (Models.PositionInfo item in e)
            {

                Console.WriteLine("wwwwwwwwwwwwwwwww标签: {0}  坐标: x: {1}  y: {2}  regid: {3}  sleep: {4} batt: {5} floor: {6} indicator:{7}",
                    item.tagid, item.x, item.y, item.mapid, item.sleep, item.batt, item.floor, item.dim);

            }
           
               Execute(e);//定位数据写入数据库

           
                var tracks = _freeSql.Select<TrackArea>().ToList();//股道区域
                foreach (PositionInfo addModel in e)
                {
                    var carCode = await _freeSql.Select<CarCode>().Where(t => t.tagid == addModel.tagid).ToOneAsync();//车辆编号
                    foreach (TrackArea track in tracks)
                    {
                        //车辆驶出
                        if ((addModel.x > track.xmax || addModel.x < track.xmin) && (addModel.y < track.ymax && addModel.y > track.ymin) && carCode.accessState != 1)
                        {
                            await _freeSql.Update<CarCode>(carCode.Id).Set(a => a.accessState, 1).ExecuteAffrowsAsync();
                            if (addModel.x > track.xmax) { _trackPort = 1; }
                            if (addModel.x < track.xmin) { _trackPort = 2; }
                            OnHandlerPosChange(_trackPort, track.Trackcode, carCode.vehicleCode, carCode.accessState);
                        }
                        //车辆驶入
                        if ((addModel.x < track.xmax && addModel.x > track.xmin) && (addModel.y < track.ymax && addModel.y > track.ymin) && carCode.accessState != 0)
                        {
                            int i =await _freeSql.Update<CarCode>(carCode.Id).Set(a => a.accessState, 0).ExecuteAffrowsAsync();
                            if ((addModel.x < track.xmax) && (addModel.x > (track.xmax - track.xmin) / 3)) { _trackPort = 1; }
                            if (addModel.x > track.xmin && (addModel.x < (track.xmax - track.xmin) / 3)) { _trackPort = 2; }
                            OnHandlerPosChange(_trackPort, track.Trackcode, carCode.vehicleCode, carCode.accessState);
                        }

                      

                    }


                   
                }
           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="trackPort">股道端</param>
        /// <param name="trackCode">股道编码</param>
        /// <param name="vehicleCode">车辆编码</param>
        /// <param name="accessState">车辆进或出</param>
        public   void OnHandlerPosChange(int trackPort,string trackCode,string vehicleCode,int accessState)
        {
            
            var _proccessSuccessFlag = _freeSql.Select<ProccessSuccessFlag>().
                Where(t => t.processingName.Equals("carTrackPositonChange")).ToOne();//当前是否有正在执行的请求

            _freeSql.Update<ProccessSuccessFlag>(_proccessSuccessFlag.id).Set(p => p.processingValue, 1).ExecuteAffrows();
            #region 将字符串转成字节数组 并低字节在前高字节在后
            byte[] trackCodebytes = stringTo16Bytes(trackCode);
            Array.Reverse(trackCodebytes);
            byte[] vehicleCodebytes = stringTo16Bytes(vehicleCode);
            Array.Reverse(vehicleCodebytes);
            #endregion
            //查询要发送的数据格式
            var _dataFrame = _freeSql.Select<DataFrame>().Where(t => t.dataName.Equals("carTrackPositonChange")
             && t.sendOrReceiveSign.Equals("send") && t.sendOrReceiveSignSequence == 1).ToOne();
            byte[] bytes = _mapper.Map<DataFrame>(_dataFrame).data;

            byte[] sendSerialNumber = bytes.Skip(2).Take(2).ToArray();
            #region 发送序号2字节（低字节在前，高字节在后）首字节首位须为0
            ushort bytetoshort = BitConverter.ToUInt16(sendSerialNumber, 0);
            bytetoshort++;


            bytetoshort &= 0xEFFF;

            byte[] newsendSerialNumber = BitConverter.GetBytes(bytetoshort);
            Array.Copy(newsendSerialNumber, 0, bytes, 2, 2);
            #endregion
            byte[] crc = BitConverter.GetBytes(ComputeChecksum(bytes.Skip(0).Take(bytes.Length - 2).ToArray()));//截取字节数组 skip开始位置 take 多少个            
            Array.Copy(crc, 0, bytes, bytes.Length - 2, 2);
            int i = _freeSql.Update<DataFrame>(_dataFrame.Id).Set(a => a.data, bytes).ExecuteAffrows();
            _socket.Send(bytes);
            while (true)//监听服务端是否发送数据
            {
                if (receive == true)
                {
                    _freeSql.Update<ProccessSuccessFlag>(_proccessSuccessFlag.id).Set(p => p.processingValue, 0).ExecuteAffrows();
                    receive = false;
                    
                    break;
                }
            }
        }
        /// <summary>
        /// 将定位数据写入到数据库
        /// </summary>
        public void Execute(List<PositionInfo> e)
        {
            {

                    foreach (PositionInfo ps in e)
                    {
                        var positionInfo = _mapper.Map<PositionInfo>(_freeSql.Select<PositionInfo>().Where(p => p.tagid == ps.tagid).ToOne());

                        if (positionInfo != null)
                        {
                            ps.id = positionInfo.id;
                            int i = _freeSql.Update<PositionInfo>().SetSource(ps).ExecuteAffrows();
                        }
                        else
                            _freeSql.Insert(ps).ExecuteAffrows();
                    }
            }
        }
        public void monitor()
        {
            while (true)
            {

                if (_socket == null)
                {
                    continue;
                }
                byte[] buffer = new byte[1024 * 1024 * 3];
                int len = _socket.Receive(buffer);
                if (len == 0)
                {
                    continue;
                }
                 receive= (bool)Fulldata(buffer.Take(len).ToArray());
                
                Console.WriteLine(receive+ "11111");

            }
        }
        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="data"></param>
        public bool Fulldata(byte[] data)
        {
            return true;
          
        }
        
       
      
       
        /// <summary>
        /// string转16进制字节数组
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public byte[] stringTo16Bytes(string s)
        {
            return Enumerable.Range(0, s.Length)
                 .Where(x => x % 2 == 0)
                 .Select(x => Convert.ToByte(s.Substring(x, 2), 16))
                 .ToArray();
        }
        /// <summary>
        /// crc校验
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ushort ComputeChecksum(byte[] data)
        {

            ushort crc = 0xFFFF;

            for (int i = 0; i < data.Length; i++)
            {
                crc ^= (ushort)(data[i] << 8);

                for (int j = 0; j < 8; j++)
                {
                    if ((crc & 0x8000) != 0)
                        crc = (ushort)((crc << 1) ^ 0x1021);
                    else
                        crc <<= 1;
                }
            }

            return crc;
        }
    }
}

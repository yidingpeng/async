using RW.Position.Extensions;
using RW.Position.websocketServers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using RW.Position.TX.realize;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using LsWebsocketClient;
using RW.Position.Common;
using MapsterMapper;
using RW.Position.WinForm.Extensions;
using ConfigurationService = RW.Position.WinForm.Extensions.ConfigurationService;
using RW.Position.Models;

namespace RW.Position.Winform
{
    public partial class SocketClient : Form
    {
        //标签启动类
        RW.Position.WinForm.OnDemandSubscription _onDemandSubscription;
        //标签服务类配置启动
        WebSocketServerConfig _webSocketServerConfig;
        private readonly IMapper _mapper;
        private readonly IFreeSql _freeSql;
        //Socket客户端对象
        public Socket socket { get; set; }
        //定位系统客户端
        private LocalsenseInterface _client { get; set; }
        //socket连接端口号
        public int TelePort { get; set; }
        //socket连接ip地址
        public string TeleIP { get; set; }
        //socket 发送请求数据委托
        public Func<byte[], bool?> Send;
        //接收服务端数据事件
        public event Func<byte[], bool> TcpShowMsgEvent;
        //CRC校验中的生成多项式
        private const ushort Polynomial = 0x1021;
        //当车辆股道位置变化时,数据校验标志
        private static byte positonChangeDataCheck;
        //调度系统（或定位系统）向定位系统（或调度系统）发起请求车辆所在股道,数据校验标志
        private static byte carTrackDataCheck = 0x33;
        //定位系统向调度系统发起请求股道所存放的车辆,数据校验标志
        private static byte trackCarDataCheck;
        //定位系统根据需要向车厂调度系统发起申请打开铁鞋柜,数据校验标志
        private static byte shoesDataCheck;
        //铁鞋状态变化时，定位系统向调度系统发送铁鞋状态位置,数据校验标志
        private static byte shoesChangeDataCheck;
        //定位系统根据需要，向调度系统发送数据报警数据帧,数据校验标志
        private static byte syestemRequestAlarmDataCheck;
        //传输是否正确
        private static bool receive;
        //发送中标志
        private int successSend = 0;

        //当车辆股道位置变化时,发送定位数据格式
        private byte[] PositonChangeRequestBytes = { 0x68, 0x0A, 0x00, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0x00, 0x00 };
        private byte[] PositonChangeRequestConfirmBytes = { 0x68, 0x0A, 0x00, 0x00, 0x00, 0x00, 0x0B, 0x00, 0x00, 0x00, 0x00, 0x00 };
        private byte[] PositonChangeDataBytes = { 0x68, 0x16, 0x00, 0x00, 0x00, 0x00, 0x09, 0x01, 0x03, 0x00, 0x00, 0x09, 0x95, 0x05, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        private byte[] PositonChangeDataConfirmBytes = { 0x68, 0x07, 0x00, 0x00, 0x00, 0x00, 0x10, 0x00, 0x00 };
        private byte[] PositonChangeDataStopBytes = { 0x68, 0xA, 0x00, 0x00, 0x00, 0x00, 0x13, 0x00, 0x00, 0x00, 0x00, 0x00 };
        private byte[] PositonChangeDataStopConfirmBytes = { 0x68, 0xA, 0x00, 0x00, 0x00, 0x00, 0x23, 0x00, 0x00, 0x00, 0x00, 0x00 };
        //调度系统（或定位系统）向定位系统（或调度系统）发起请求车辆所在股道位置数据格式
        private byte[] CarTrackRequstData = { 0x68, 0x0B, 0x00, 0x00, 0x00, 0x00, 0x33, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        private byte[] CarTrackPositinDataNone = { 0x68, 0x0B, 0x00, 0x00, 0x00, 0x00, 0x34, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };//无数据
        private byte[] CarTrackPositinData = { 0x68, 0x0F, 0x00, 0x00, 0x00, 0x00, 0x09, 0x00, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };//有数据
        private byte[] CarTrackPositinDataConfirm = { 0x68, 0x07, 0x00, 0x00, 0x00, 0x00, 0x10, 0x00, 0x00 };
        private byte[] CarTrackPositinDataStop = { 0x68, 0x0A, 0x00, 0x00, 0x00, 0x00, 0x13, 0x00, 0x00, 0x00, 0x00, 0x00 };
        private byte[] CarTrackPositinDataStopConfirm = { 0x68, 0x0A, 0x00, 0x00, 0x00, 0x00, 0x23, 0x00, 0x00, 0x00, 0x00, 0x00 };
        //定位系统向调度系统发起请求股道所存放的车辆数据格式
        private byte[] TrackCarRequstData = { 0x68, 0x0B, 0x00, 0x00, 0x00, 0x00, 0x36, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        private byte[] TrackCarPositinDataNone = { 0x68, 0x0B, 0x00, 0x00, 0x00, 0x00, 0x34, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };//无数据
        private byte[] TrackCarPositinData = { 0x68, 0x0F, 0x00, 0x00, 0x00, 0x00, 0x19, 0x00, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };//有数据
        private byte[] TrackCarPositinDataConfirm = { 0x68, 0x07, 0x00, 0x00, 0x00, 0x00, 0x10, 0x00, 0x00 };
        private byte[] TrackCarPositinDataStop = { 0x68, 0x0A, 0x00, 0x00, 0x00, 0x00, 0x13, 0x00, 0x00, 0x00, 0x00, 0x00 };
        private byte[] TrackCarPositinDataStopConfirm = { 0x68, 0x0A, 0x00, 0x00, 0x00, 0x00, 0x23, 0x00, 0x00, 0x00, 0x00, 0x00 };
        //定位系统根据需要向车厂调度系统发起申请打开铁鞋柜数据格式
        private byte[] ShoesRequestApplyOpen = { 0x68, 0x32, 0x00, 0x00, 0x00, 0x00, 0x18, 0x01, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };//待定
        private byte[] ShoesRequestApplyUseConfirm = { 0x68, 0x0A, 0x00, 0x00, 0x00, 0x00, 0x0B, 0x00, 0x00, 0x00, 0x00, 0x00 };
        private byte[] ShoesUseConfirmAgreeOrNot = { 0x68, 0x0F, 0x00, 0x00, 0x00, 0x00, 0x20, 0x01, 0x03, 0x00, 0x00, 0x00 ,
            0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00};
        private byte[] ShoesRequestAnswerConfirm = { 0x68, 0x0A, 0x00, 0x00, 0x00, 0x00, 0x0B, 0x00, 0x00, 0x00, 0x00, 0x00 };
        //铁鞋状态变化时，定位系统向调度系统发送铁鞋状态位置
        private byte[] ShoesChangeRequest = { 0x68, 0x0A, 0x00, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0x00, 0x00 };
        private byte[] ShoesChangeRequestConfirm = { 0x68, 0x0A, 0x00, 0x00, 0x00, 0x00, 0x0B, 0x00, 0x00, 0x00, 0x00, 0x00 };
        private byte[] ShoesChangePosionData = { 0x68, 0x0E, 0x00, 0x00, 0x00, 0x00, 0x99, 0x01, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        private byte[] ShoesChangeDataConfirm = { 0x68, 0x07, 0x00, 0x00, 0x00, 0x00, 0x10, 0x00, 0x00 };
        private byte[] ShoesChangeStopConfirm = { 0x68, 0x04, 0x00, 0x00, 0x00, 0x00, 0x13, 0x00, 0x00, 0x00, 0x00, 0x00 };
        //定位系统根据需要，向调度系统发送数据报警数据帧
        private byte[] SyestemRequestAlarmData = { 0x68, 0x18, 0x00, 0x00, 0x00, 0x00, 0x55, 0x01, 0x03, 0x00, 0x00, 0x00 ,0x00,0x00,0x00,
        0x00,0x00,0x00,0x00,0x00,0x00};//待定
        private byte[] SyestemRequestDataConfirm = { 0x68, 0x07, 0x00, 0x00, 0x00, 0x00, 0x10, 0x00, 0x00 };

        public SocketClient()
        {
            InitializeComponent();
            #region 初始化定位系统软件获取推送数据
            string ip = ConfigurationService.config["websocketConnectionStrings:ip"];
            int Port = int.Parse(ConfigurationService.config["websocketConnectionStrings:Port"]);
            string user = ConfigurationService.config["websocketConnectionStrings:user"];
            string passwd = ConfigurationService.config["websocketConnectionStrings:passwd"];
            var setting = new CommonSetting(ip, Port, user, passwd);
            _client = new LocalsenseInterface(setting.IP, setting.Port, setting.UserName, setting.Password, setting.Salt);
            #endregion
        }
        public SocketClient(RW.Position.WinForm.OnDemandSubscription onDemandSubscription, WebSocketServerConfig webSocketServerConfig, IMapper mapper, IFreeSql freeSql) : this()
        {
            _onDemandSubscription = onDemandSubscription;
            _webSocketServerConfig = webSocketServerConfig;
            _mapper = mapper;
            _freeSql = freeSql;
        }

        /// <summary>
        /// 连接按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonconnect_Click(object sender, EventArgs e)
        {
            var connectString = textBoxConnectstring.Text.Trim();
            if (!connectString.Equals(""))
            {
                string[] con = connectString.Split(':');
                if (con.Length > 1)
                {
                    TeleIP = con[0];
                    TelePort = int.Parse(con[1]);
                }

            }
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint point = new IPEndPoint(IPAddress.Parse(TeleIP), TelePort);
                socket.Connect(point);
                MessageBox.Show("连接成功");
                Send = send;
                TcpShowMsgEvent += Fulldata;
                #region 创建线程监听服务端发送过来的数据
                Thread th_monitor = new Thread(monitor);
                th_monitor.Name = "监听TCPclient线程" + TeleIP + ":" + TelePort;
                th_monitor.IsBackground = true;
                th_monitor.Start();


                #endregion
                //接收位置数据事件
                _client.OnMessagePos += (lsender, le) =>
                {
                    //var addModels = _mapper.Map<List<PositionInfo>>(le.Data);

                    //foreach (PositionInfo addModel in addModels)
                    //{
                    //    var track = _freeSql.Select<TrackArea>().Where(t => t.tagid == addModel.tagid).ToOne();

                    //    //车辆驶出
                    //    if ((addModel.x > track.xmax || addModel.x < track.xmin) && track.state != 1)
                    //    {
                    //        int i = _freeSql.Update<TrackArea>(track.id).Set(a => a.state, 1).ExecuteAffrows();
                    //        OnHandlerPosChange(addModel);
                    //    }
                    //    //车辆驶入
                    //    if ((addModel.x < track.xmax && addModel.x > track.xmin) && track.state != 0)
                    //    {
                    //        int i = _freeSql.Update<TrackArea>(track.id).Set(a => a.state, 0).ExecuteAffrows();
                    //        OnHandlerPosChange(addModel);
                    //    }
                    //}
                };
            }
            catch
            {
                MessageBox.Show("连接失败 请查看连接地址是否正确");
            }

        }
        /// <summary>
        /// 当车辆股道位置变化调用
        /// </summary>
        /// <param name="vpos"></param>
        private void OnHandlerPosChange(PositionInfo positioningLabelsData)
        {
            if (successSend < 1)
            {
                PositonChangeRequestBytes[11] = (byte)ComputeChecksum(PositonChangeRequestBytes.Skip(1).Take(PositonChangeRequestBytes.Length - 1).ToArray());//截取字节数组 skip开始位置 take 结束位置
                Send(PositonChangeRequestBytes);
                successSend = 1;
                positonChangeDataCheck = 0x0B;
                while (true)
                {
                    if (receive == true)
                    {
                        Send(PositonChangeDataBytes);
                        receive = false;
                        positonChangeDataCheck = 0x10;
                    }
                    if (positonChangeDataCheck == 0x23)
                    {
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("还有其他请求在执行,请稍等");
            }
        }
        /// <summary>
        /// 当车辆股道位置变化时,发送请求按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonrequest_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        ///当车辆股道位置变化时, 发送定位数据按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonData_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 当车辆股道位置变化时,数据发送停止按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDataStop_Click(object sender, EventArgs e)
        {
            if (successSend == 1 && positonChangeDataCheck == 0x10)
            {
                Send(PositonChangeDataStopBytes);
                positonChangeDataCheck = 0x23;
            }
            else
            {
                MessageBox.Show("连接已关闭");
            }
        }
        /// <summary>
        /// 调度系统（或定位系统）向定位系统（或调度系统）发起请求车辆所在股道,请求时触发函数
        /// </summary>
        private void OnHandlerCarTrackRequstReceive()
        {


            Send(CarTrackPositinData);
            successSend = 1;


        }
        /// <summary>
        ///  调度系统（或定位系统）向定位系统（或调度系统）发起请求车辆所在股道,数据发送停止按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCarTrackStopData_Click(object sender, EventArgs e)
        {
            if (successSend == 1 && carTrackDataCheck == 0x10)
            {
                Send(CarTrackPositinDataStop);
                carTrackDataCheck = 0x23;
            }
            else
            {
                MessageBox.Show("连接已关闭");
            }
        }
        /// <summary>
        /// 请求车辆所在股道位置,请求数据按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRequestData_Click(object sender, EventArgs e)
        {
            if (successSend < 1)
            {
                TrackCarRequstData[11] = (byte)ComputeChecksum(TrackCarRequstData.Skip(1).Take(TrackCarRequstData.Length - 1).ToArray());//截取字节数组 skip开始位置 take 结束位置
                Send(TrackCarRequstData);
                successSend = 1;
                trackCarDataCheck = 0x19;
                Task task = Task.Run(() =>
                {
                    while (true)
                    {
                        if (receive == true)
                        {
                            if (trackCarDataCheck == 0x19 || trackCarDataCheck == 0x37)
                            {
                                MessageBox.Show("调度系统已发送数据，请点击数据确认按钮");

                                receive = false;
                                trackCarDataCheck = 0x19;
                            }
                            if (trackCarDataCheck == 0x23)
                            {
                                MessageBox.Show("调度系统已停止发送数据，请点击停止确认按钮");
                                break;
                            }
                        }

                    }
                });

            }
            else
            {
                MessageBox.Show("还有其他请求在执行,请稍等");
            }
        }
        /// <summary>
        /// 请求车辆所在股道位置,数据确认按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDataConfirm_Click(object sender, EventArgs e)
        {
            Send(TrackCarPositinDataConfirm);
            receive = false;
            trackCarDataCheck = 0x19;
        }
        /// <summary>
        /// 请求车辆所在股道位置,停止确认按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStopConfirm_Click(object sender, EventArgs e)
        {
            TrackCarRequstData[11] = (byte)ComputeChecksum(TrackCarRequstData.Skip(1).Take(TrackCarRequstData.Length - 1).ToArray());//截取字节数组 skip开始位置 take 结束位置
            Send(TrackCarPositinDataStopConfirm);
            successSend = 0;
        }
        /// <summary>
        /// 定位系统根据需要向车厂调度系统发起申请打开铁鞋柜,发送打开申请按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOpenApply_Click(object sender, EventArgs e)
        {
            if (successSend < 1)
            {
                ShoesRequestApplyOpen[11] = (byte)ComputeChecksum(ShoesRequestApplyOpen.Skip(1).Take(ShoesRequestApplyOpen.Length - 1).ToArray());//截取字节数组 skip开始位置 take 结束位置
                Send(ShoesRequestApplyOpen);
                successSend = 1;
                shoesDataCheck = 0x0B;
                while (true)
                {
                    if (receive == true)
                    {

                        receive = false;
                        shoesDataCheck = 0x20;
                    }
                    if (shoesDataCheck == 0x20)
                    {
                        successSend = 0;
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("还有其他请求在执行,请稍等");
            }
        }
        /// <summary>
        /// 铁鞋状态变化时，定位系统向调度系统发送铁鞋状态位置,数据发送请求按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonShoesDataRequest_Click(object sender, EventArgs e)
        {
            if (successSend < 1)
            {
                ShoesChangeRequest[11] = (byte)ComputeChecksum(ShoesChangeRequest.Skip(1).Take(ShoesChangeRequest.Length - 1).ToArray());//截取字节数组 skip开始位置 take 结束位置
                Send(ShoesChangeRequest);
                successSend = 1;
                shoesChangeDataCheck = 0x0B;
                Task task = Task.Run(() =>
                {
                    while (true)
                    {
                        if (receive == true)
                        {
                            Send(ShoesChangePosionData);
                            receive = false;
                            shoesChangeDataCheck = 0x10;
                        }
                        if (shoesChangeDataCheck == 0x23)
                        {
                            receive = false;
                            break;
                        }
                    }
                });
            }
            else
            {
                MessageBox.Show("还有其他请求在执行,请稍等");
            }
        }
        /// <summary>
        ///  铁鞋状态变化时，定位系统向调度系统发送铁鞋状态位置,数据发送停止按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonShoesDataStop_Click(object sender, EventArgs e)
        {
            if (successSend == 1 && shoesChangeDataCheck == 0x10)
            {
                Send(ShoesChangeStopConfirm);
                shoesChangeDataCheck = 0x23;
            }
            else
            {
                MessageBox.Show("连接已关闭");
            }
        }
        /// <summary>
        /// 定位系统根据需要，向调度系统发送数据报警数据帧,发送报警数据按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRequestAlarmData_Click(object sender, EventArgs e)
        {
            if (successSend < 1)
            {
                byte[] bytes1 = BitConverter.GetBytes(ComputeChecksum(SyestemRequestAlarmData.Skip(0).Take(SyestemRequestAlarmData.Length - 2).ToArray()));//截取字节数组 skip开始位置 take 结束位置
                int i = SyestemRequestAlarmData.Length;
                Array.Copy(bytes1, 0, SyestemRequestAlarmData, SyestemRequestAlarmData.Length - 2, 2);
                Send(SyestemRequestAlarmData);
                successSend = 1;
                syestemRequestAlarmDataCheck = 0x10;
                Task task = Task.Run(() =>
                {
                    while (true)
                    {
                        if (receive == true && syestemRequestAlarmDataCheck == 0x10)
                        {

                            MessageBox.Show("调度系统已成功接收报警数据");
                            receive = false;
                            successSend = 0;
                            break;

                        }

                    }
                });
            }
            else
            {
                MessageBox.Show("还有其他请求在执行,请稍等");
            }
        }
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="senddata"></param>
        /// <returns></returns>

        public bool? send(byte[] senddata)
        {
            try
            {
                socket.Send(senddata);
                return true;
            }
            catch (Exception ex)
            {
                Close();
                return false;

            }
        }
        /// <summary>
        /// 循环监听服务端发来的信息
        /// </summary>
        public void monitor()
        {
            while (true)
            {

                if (socket == null)
                {
                    continue;
                }
                byte[] buffer = new byte[1024 * 1024 * 3];
                int len = socket.Receive(buffer);
                if (len == 0)
                {
                    continue;
                }
                receive = (bool)TcpShowMsgEvent?.Invoke(buffer.Take(len).ToArray());
                //if (receive == true) MessageBox.Show(receive.ToString());

            }
        }
        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="data"></param>
        public bool Fulldata(byte[] data)
        {
            int positonChangeCount = data.Count(b => b == positonChangeDataCheck);
            int carTrackCount = data.Count(b => b == carTrackDataCheck);
            int trackCarCount = data.Count(b => (b == trackCarDataCheck || b == 0x37 || b == 0x13));
            int shoesDataCount = data.Count(b => (b == shoesDataCheck));
            int shoesChangeDataCount = data.Count(b => b == shoesChangeDataCheck);
            int syestemRequestAlarmDataCount = data.Count(b => b == syestemRequestAlarmDataCheck);

            // MessageBox.Show(positonChangeCount.ToString());
            if (positonChangeCount > 0)
            {
                if (positonChangeDataCheck == 0x23) successSend = 0;
                return true;
            }
            else if (trackCarCount > 0)
            {
                if (data.Count(b => b == 0x13) > 0)
                {
                    trackCarDataCheck = 0x23;
                }
                if (data.Count(b => b == 0x37) > 0)
                {
                    trackCarDataCheck = 0x37;
                }
                return true;
            }


            else if (carTrackCount > 0)
            {
                if ((carTrackDataCheck == 0x33) || (carTrackDataCheck == 0x10))
                {
                    if (carTrackDataCheck == 0x10)
                    {
                        MessageBox.Show("数据发送完成，请点击发送停止按钮");
                    }
                    OnHandlerCarTrackRequstReceive();
                    carTrackDataCheck = 0x10;

                }
                if (carTrackDataCheck == 0x23) successSend = 0;
                return true;
            }
            else if (shoesDataCount > 0)
            {
                if (shoesDataCheck == 0x20)
                    Send(ShoesRequestAnswerConfirm);

                return true;
            }
            else if (shoesChangeDataCount > 0)
            {
                if (shoesChangeDataCheck == 0x23) { successSend = 0; return false; }
                return true;
            }
            else if (syestemRequestAlarmDataCount > 0)
            {

                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// crc校验码
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static ushort ComputeChecksum(byte[] bytes)
        {
            ushort crc = 0xffff;

            for (int i = 0; i < bytes.Length; ++i)
            {
                crc ^= (ushort)(bytes[i] << 8);

                for (int j = 0; j < 8; ++j)
                {
                    if ((crc & 0x8000) != 0)
                    {
                        crc = (ushort)((crc << 1) ^ Polynomial);
                    }
                    else
                    {
                        crc <<= 1;
                    }
                }
            }

            crc = (ushort)~crc;

            return crc;
        }


    }
}

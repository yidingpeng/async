using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RW.Position.TX.Communication;

namespace RW.Position.TX.realize
{
    public class TCP_Client_Data : Data_Realize
    {
        public Lazy<TCP_Client> tCP_Client = null;

        public string TeleIP { get; set; } = "127.0.0.1";

        public int TelePort { get; set; } = 12345;
        public TCP_Client_Data()
        {

        }


        public void Init()
        {
            tCP_Client = new Lazy<TCP_Client>();

            tCP_Client.Value.TcpstateEvent += TCP_Client_TcpstateEvent;
            tCP_Client.Value.TcpShowMsgEvent += Fulldata;
            tCP_Client.Value.TeleIP = TeleIP;
            
            tCP_Client.Value.TelePort = TelePort;
            tCP_Client.Value.initialize();
          
        }
        /// <summary>
        /// 连接事件
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        public void TCP_Client_TcpstateEvent(string arg1, bool arg2)
        {
            IP_info = arg1;
            State = arg2;
        }
        public override void Fulldata(byte[] data)
        {
           //将数据填充至fulldata
            fulldata = null;
            //throw new NotImplementedException();
            byte[] bytes = new byte[1];
            bytes[0] = 1;
            Senddata(bytes);
        }
        public override bool? Senddata(byte[] data)
        {
            return tCP_Client.Value?.Send(data);
        }
    }
}

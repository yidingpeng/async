
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.Position.events
{
    //发布者
    public class Publisher
    {   //定义事件
        public event EventHandler<LsEventArgs<List<Models.PositionInfo>>> EventOnHandlerPos;
        public event EventHandler<LsEventArgs<List<Models.LsBaseStatus>>> EventOnHandlerBase;
        public event EventHandler<LsEventArgs<List<Models.LsBatteryCapInfo>>> EventOnHandlerBattery;
        public event EventHandler<LsEventArgs<Models.LsExtendAlarmInfo>> EventOnHandlerAlarm;
        public event EventHandler<LsEventArgs<Models.LsPersonSI>> EventOnHandlerPeopleSI;
        public event EventHandler<LsEventArgs<Models.LsIOState>> EventOnHandlerIOState; 
        public event EventHandler<LsEventArgs<Models.LsDataUpdate>> EventOnHandlerDataUpdate;
        public event EventHandler<LsEventArgs<Models.LsRegInOutInfo>> EventOnHandlerRegInOutInfo;
        //调用此方法触发位置数据事件
        public void IssrueEventOnHandlerPos(List<Models.PositionInfo> data)
        {
            //创建自定义实例，因为这里需要将方法的参数赋值给对象的属性
            events.LsEventArgs<List<Models.PositionInfo>> lsEventArgs = new events.LsEventArgs<List<Models.PositionInfo>>(data);

            if (EventOnHandlerPos != null)
                //触发事件，然后将自定义的对象作为事件触发类型传入
                EventOnHandlerPos(this, lsEventArgs);
        }
        //调用此方法触发基站数据事件
        public void IssrueEventOnHandlerBase(List<Models.LsBaseStatus> data)
        {
            //创建自定义实例，因为这里需要将方法的参数赋值给对象的属性
            events.LsEventArgs<List<Models.LsBaseStatus>> lsEventArgs = new events.LsEventArgs<List<Models.LsBaseStatus>>(data);

            if (EventOnHandlerBase != null)
                //触发事件，然后将自定义的对象作为事件触发类型传入
                EventOnHandlerBase(this, lsEventArgs);
        }
        //调用此方法触发电量数据事件
        public void IssrueEventOnHandlerBattery(List<Models.LsBatteryCapInfo> data)
        {
            //创建自定义实例，因为这里需要将方法的参数赋值给对象的属性
            events.LsEventArgs<List<Models.LsBatteryCapInfo>> lsEventArgs = new events.LsEventArgs<List<Models.LsBatteryCapInfo>>(data);

            if (EventOnHandlerBattery != null)
                //触发事件，然后将自定义的对象作为事件触发类型传入
                EventOnHandlerBattery(this, lsEventArgs);
        }
        //调用此方法触发报警数据事件
        public void IssrueEventOnHandlerAlarm(Models.LsExtendAlarmInfo data)
        {
            //创建自定义实例，因为这里需要将方法的参数赋值给对象的属性
            events.LsEventArgs<Models.LsExtendAlarmInfo> lsEventArgs = new events.LsEventArgs<Models.LsExtendAlarmInfo>(data);

            if (EventOnHandlerAlarm != null)
                //触发事件，然后将自定义的对象作为事件触发类型传入
                EventOnHandlerAlarm(this, lsEventArgs);
        }
        //调用此方法触发电量数据事件
        public void IssrueEventOnHandlerPeopleSI(Models.LsPersonSI data)
        {
            //创建自定义实例，因为这里需要将方法的参数赋值给对象的属性
            events.LsEventArgs<Models.LsPersonSI> lsEventArgs = new events.LsEventArgs<Models.LsPersonSI>(data);

            if (EventOnHandlerPeopleSI != null)
                //触发事件，然后将自定义的对象作为事件触发类型传入
                EventOnHandlerPeopleSI(this, lsEventArgs);
        }
        //调用此方法触发电量数据事件
        public void IssrueEventOnHandlerIOState(Models.LsIOState data)
        {
            //创建自定义实例，因为这里需要将方法的参数赋值给对象的属性
            events.LsEventArgs<Models.LsIOState> lsEventArgs = new events.LsEventArgs<Models.LsIOState>(data);

            if (EventOnHandlerIOState != null)
                //触发事件，然后将自定义的对象作为事件触发类型传入
                EventOnHandlerIOState(this, lsEventArgs);
        }
        //调用此方法触发标签更新数据事件
        public void IssrueEventOnHandlerDataUpdate(Models.LsDataUpdate data)
        {
            //创建自定义实例，因为这里需要将方法的参数赋值给对象的属性
            events.LsEventArgs<Models.LsDataUpdate> lsEventArgs = new events.LsEventArgs<Models.LsDataUpdate>(data);

            if (EventOnHandlerDataUpdate != null)
                //触发事件，然后将自定义的对象作为事件触发类型传入
                EventOnHandlerDataUpdate(this, lsEventArgs);
        }
        //调用此方法触发考勤区域进出数据事件
        public void IssrueEventOnHandlerRegInOutInfo(Models.LsRegInOutInfo data)
        {
            //创建自定义实例，因为这里需要将方法的参数赋值给对象的属性
            events.LsEventArgs<Models.LsRegInOutInfo> lsEventArgs = new events.LsEventArgs<Models.LsRegInOutInfo>(data);

            if (EventOnHandlerRegInOutInfo != null)
                //触发事件，然后将自定义的对象作为事件触发类型传入
                EventOnHandlerRegInOutInfo(this, lsEventArgs);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.Position.events
{
    //事件参数类型
    public class LsEventArgs<T> : EventArgs
    {
        private T _data;

        public T Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
            }
        }

        public LsEventArgs(T data)
        {
            _data = data;
        }
    }
}

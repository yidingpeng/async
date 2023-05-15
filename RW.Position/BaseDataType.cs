using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2.TX
{
    /// <summary>
    /// 基本数据类型
    /// </summary>
    public class BaseDataType
    {
        /// <summary>
        /// 类型
        /// </summary>
        public byte dt_type { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public byte dt_number { get; set; }
        /// <summary>
        /// 数据名称
        /// </summary>
        public string dt_name { get; set; }
        /// <summary>
        /// 数据值
        /// </summary>
        public object dt_value
        {
            get { return _value; }
            set
            {
                if (Convert.ToInt16(_value) != Convert.ToInt16(value))
                {
                    _value = value;
                    ValueChange?.Invoke(this);
                }
                else
                    _value = value;
            }
        }
        private object _value;
        /// <summary>
        /// 数据大小
        /// </summary>
        [DefaultValue("B1")]
        public string data_type { get; set; } = "B1";

        /// <summary>
        /// 是否只读
        /// </summary>
        public bool read_only { get; set; } = true;

        /// <summary>
        /// 此通道ID的数据大小
        /// </summary>
        public string dt_size { get; set; } = "Byte";

        public Action<BaseDataType> ValueChange;
    }
}

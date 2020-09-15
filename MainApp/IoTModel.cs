using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApp
{
    public class Item
    {
        public string id { get; set; }
        public string judgement { get; set; }
        public string value { get; set; }
    }

    public class ItemDatas
    {
        public string batch { get; set; }
        public string finalResult { get; set; }
        public List<Item> items { get; set; }
        public string machine { get; set; }
        public string name { get; set; }
        public string sequenceCount { get; set; }
        public string user { get; set; }
        public string sn { get; set; }
    }

    public class IoTModel
    {
        /// <summary>
        /// 命令类型
        /// item_data 一个工件的数据
        /// write_file 写文件
        /// </summary>
        public string cmd { get; set; }

        /// <summary>
        /// cmd为item_data时才有效
        /// </summary>
        public ItemDatas data { get; set; }

        /// <summary>
        /// cmd为write_file时才有效
        /// </summary>
        public string path { get; set; }

        public string timestamp { get; set; }
    }
    
    public class IoTEventArg:EventArgs
    {
        public IoTModel Data { get; set; }
    }
}

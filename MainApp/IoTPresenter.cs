using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Newtonsoft.Json;

namespace MainApp
{
    public class IoTPresenter
    {
        Dispatch mDispatch = new Dispatch();

        public IoTPresenter()
        {
            mDispatch.SmartMessage += MDispatch_SmartMessage;
        }

        /// <summary>
        /// 接收信号处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MDispatch_SmartMessage(object sender, EventArgs e)
        {
            IoTEventArg arg = (IoTEventArg)e;
            if (arg.Data.cmd == null)
                return;
            mDispatch.DoWork(arg.Data);
        }

        /// <summary>
        /// 启动服务器
        /// </summary>
        /// <returns></returns>
        public int Start()
        {
            string strIP = System.Configuration.ConfigurationManager.AppSettings["IP"];
            int port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Port"]);
            return mDispatch.Start(strIP, port);
        }

        /// <summary>
        /// 关闭服务器
        /// </summary>
        public void Close()
        {
            mDispatch.Close();
        }

        /// <summary>
        /// 增加接收处理事件
        /// </summary>
        /// <param name="handler"></param>
        public void AddDispatchEvent(EventHandler handler)
        {
            mDispatch.SmartMessage += handler;
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MainApp
{
    public class Dispatch
    {
        public event EventHandler SmartMessage;
        SingleTcpServer mServer = new SingleTcpServer();
        List<IoTModel> listIoTModel = new List<IoTModel>(10);

        public Dispatch()
        {
            mServer.SetReceiveCallback(ReceiveMsgCallback);
        }

        /// <summary>
        /// 接收信息的回调函数
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="len"></param>
        void ReceiveMsgCallback(byte[] msg, int len)
        {
            string strJson = Encoding.UTF8.GetString(msg, 0, msg.Length);
            IoTEventArg arg = new IoTEventArg();
            arg.Data = JsonConvert.DeserializeObject<IoTModel>(strJson);
            EventHandler handler = SmartMessage;
            handler?.Invoke(this, arg);
        }

        /// <summary>
        /// 接收信号处理内容
        /// </summary>
        /// <param name="iot"></param>
        public void DoWork(IoTModel iot)
        {
            if (iot.cmd.Equals("item_data"))
            {
                listIoTModel.Add(iot);
                Console.WriteLine("handle item_data");
            }
            else if (iot.cmd.Equals("write_file"))
            {
                Console.WriteLine(String.Format("write file {0},{1}", iot.path, iot.timestamp));
                if (string.IsNullOrEmpty(iot.path) || listIoTModel.Count == 0)
                    return;
                if (!Directory.Exists(iot.path))
                    Directory.CreateDirectory(iot.path);
                WriteFile(iot.path, iot.timestamp);
                listIoTModel.Clear();
            }            
        }
        
        /// <summary>
        /// 写入结果到文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="timeStamp"></param>
        void WriteFile(string path, string timeStamp)
        {//SN=;WorkOrder=;Result=PASS;ErrorCode=0;KSN=N/A;TTime=2020/07/20_16:02:19
            string SN = "";
            string WorkOrder = "";
            int ErrorCode = 0;
            int index = 0;
            foreach (var kvp in listIoTModel)
            {                
                if (string.IsNullOrEmpty(SN))
                    SN = kvp.data.sn;
                else if (SN != kvp.data.sn)
                    ;//不同的SN如何处理  
                WorkOrder = kvp.data.batch;
                if (!kvp.data.finalResult.Equals("PASS"))
                    ErrorCode |= (1 << index ++);
            }
            string Result = ErrorCode > 0 ? "FAIL" : "PASS";
          
            string filePath = Path.GetDirectoryName(path) + "\\" + SN + "-" + Result + ".txt";
            string strValue = "SN=" + SN + ";WorkOrder=" + WorkOrder + ";Result=" + Result + ";ErrorCode=" + ErrorCode + ";KSN=N/A;" + "TTime=" + timeStamp;
            File.WriteAllText(filePath, strValue);
        }

        /// <summary>
        /// 打开服务器
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public int Start(String IP,int port)
        {
            return mServer.Open(IP, port);
        }

        /// <summary>
        /// 关闭服务器
        /// </summary>
        public void Close()
        {
            mServer.Close();
        }
    }
}

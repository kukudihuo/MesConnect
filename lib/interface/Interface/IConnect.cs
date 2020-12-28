using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interface
{
    /// <summary>
    /// 接收与发送后的回调函数委托定义
    /// </summary>
    /// <param name="rec"></param>
    /// <param name="len"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public delegate bool DoReceiveDelegate(byte[] rec, int len, string key = "");
    public interface IConnect
    {
        event DoReceiveDelegate DoReceiveFun;
        void SendCommand(string str, bool bWaitRec = true);

        void SendCommand(Byte[] buf, int len, bool bWaitRec = true);
    }
}

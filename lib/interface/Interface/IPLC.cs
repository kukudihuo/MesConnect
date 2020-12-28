using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interface
{
    public interface IPLC
    {
        /// <summary>
        /// 生成 读取plc D寄存器的指令
        /// </summary>
        /// <param name="Address">开始D</param>
        /// <param name="Length">长度</param>
        /// <param name="_StationNo">站号</param> 
        /// <returns></returns>
        byte[] ReadDataToPLC(short Address, short Length = 1, byte _StationNo = 1);

        /// <summary>
        /// 写入字符串到D中
        /// </summary>
        /// <param name="_StationNo">站号</param>
        /// <param name="values">数值</param>
        /// <param name="Address">D的地址</param>
        /// <returns></returns>
        byte[] WriteDateToPLC(short Address, string values, byte _StationNo = 1);

        /// <summary>
        /// 写入单字双字节到D中
        /// </summary>
        /// <param name="_StationNo">站号</param>
        /// <param name="values">数值</param>
        /// <param name="Address">D的地址</param>
        /// <returns></returns>
        byte[] WriteDateToPLC(short Address, short values, byte _StationNo = 1);

        /// <summary>
        /// 写入单字双字节数值到D中
        /// </summary>
        /// <param name="_StationNo">站号</param>
        /// <param name="values">数值</param>
        /// <param name="Address">D的地址</param>
        /// <returns></returns>
        byte[] WriteDateToPLC(short Address, List<short> values, byte _StationNo = 1);

        /// <summary>
        /// 写入双字四字节数值到D中
        /// </summary>
        /// <param name="_StationNo">站号</param>
        /// <param name="values">数值</param>
        /// <param name="Address">D的地址</param>
        /// <returns></returns>
        byte[] WriteDateToPLC32(short Address, List<int> values, byte _StationNo = 1);

        byte[] WriteDateToPLCFloat(short Address, List<float> values, byte _StationNo = 1);
        byte[] WriteDateToPLC32Float(short Address, List<float> values, byte _StationNo = 1);

        List<byte> PlcHead { get; set; }
        List<short> PlcInt16s{ get; set; }
        List<int> PlcInt32s{ get; set; }
        List<float> PlcFloat32s{ get; set; }
        List<byte> PlcBytes{ get; set; }
        string PlcString { get; set; }

        /// <summary>
        /// 回调函数，接收到数据以后自动被调用
        /// </summary>
        /// <param name="rec"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        bool DealData(byte[] rec, int len, string key);

        /// <summary>
        /// 判断收到的信息是不是符合写入D的要求
        /// </summary>
        /// <param name="recStr">收到的信息</param>
        /// <returns></returns>
        bool WriteDIsOk();

        /// <summary>
        /// 判断收到的信息是不是符合读取D的要求
        /// </summary>
        /// <param name="recStr">收到的信息</param>
        /// <returns></returns>
        bool ReadDIsOk();

        /// <summary>
        /// List<byte> 转 List<short>，起始位置RevModbusStart
        /// </summary>
        /// <param name="Values"></param>
        /// <param name="AddrCount"></param>
        /// <returns></returns>
        List<short> BytesToInt16s(List<byte> bytes, int RevModbusStart = 0);

        /// <summary>
        /// List<byte> 转 List<int>，起始位置RevModbusStart
        /// </summary>
        /// <param name="Values"></param>
        /// <param name="AddrCount"></param>
        /// <returns></returns>
        List<int> BytesToInt32s(List<byte> bytes, int RevModbusStart = 0);

        /// <summary>
        /// List<byte> 转 StringASCII，起始位置RevModbusStart
        /// </summary>
        /// <param name="Values"></param>
        /// <param name="AddrCount"></param>
        /// <returns></returns>
        string BytesToStringASCII(List<byte> bytes, int RevModbusStart = 0);

    }
}

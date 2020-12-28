using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using ClassLib_ParaFile;

namespace Interface
{
    public enum Run_Mode { Not_Run, Home_Run, Single_Run, Auto_Run, DebugSingle_Run, Debug_Run };
    public enum Stop_Mode { Not_Stop, PauseSenso_Stop, Pause_Stop, SoftAbrupt_Stop, Abrupt_Stop };
    public interface IRunMode
    {
        #region 基础流程状态切换
        /// <summary>
        /// 当前运行状态
        /// </summary>
        Run_Mode Run
        {
            get;
        }
        /// <summary>
        /// 当前停止状态
        /// </summary>
        Stop_Mode Stop
        {
            get;
        }

        /// <summary>
        /// 是否复位完成
        /// </summary>
        bool IsAxisReseted
        {
            get;
            set;
        }
        /// <summary>
        /// 是否回零中状态
        /// </summary>
        /// <returns></returns>
        bool GetHoming();
        /// <summary>
        /// 是否自动状态
        /// </summary>
        /// <returns></returns>
        bool GetAutoing();
        /// <summary>
        /// 自动或空跑运行状态
        /// </summary>
        /// <returns></returns>
        bool GetRunning();
        /// <summary>
        /// 空跑状态
        /// </summary>
        /// <returns></returns>
        bool GetDebuging();
        /// <summary>
        /// 单步或空跑单步状态
        /// </summary>
        /// <returns></returns>
        bool GetSingling();
        /// <summary>
        /// 光纤感应暂停
        /// </summary>
        /// <returns></returns>
        bool GetPauseSensoStop();
        /// <summary>
        /// 设置回零中状态
        /// </summary>
        void SetHoming();
        /// <summary>
        /// 设置空跑状态
        /// </summary>
        void SetDebuging();
        /// <summary>
        /// 设置运行状态
        /// </summary>
        void SetSingling();
        /// <summary>
        /// 设置单步空跑状态
        /// </summary>
        void SetDebugSingling();
        /// <summary>
        /// 设置有条件运行状态
        /// </summary>
        /// <param name="bRun"></param>
        /// <param name="bDebug"></param>
        /// <param name="bSingle"></param>
        void SetRunning(bool bRun, bool bDebug = false, bool bSingle = false);        
        /// <summary>
        /// 读取到停止状态时抛出异常
        /// </summary>
        /// <param name="bSoftStop">读软停或急停</param>
        /// <param name="SoftStopLog">抛出异常前写入的日志内容</param>
        /// <returns></returns>
        bool ThrowStop(bool bSoftStop = true, string SoftStopLog = "");
        /// <summary>
        /// 读取软停或急停状态，非停止状态时刷新急停状态
        /// </summary>
        /// <param name="bSoftStop"></param>
        /// <returns></returns>
        bool GetStop(bool bSoftStop = true);

        /// <summary>
        /// 设置状态为软停或清除软停
        /// </summary>
        /// <param name="bSoftStop">软停或清除软停</param>
        /// <param name="SoftStopLog">第一次设置为软停时显示的信息</param>
        /// <returns></returns>
        bool SetSoftStop(bool bSoftStop = true, string SoftStopLog = "程序员忘记修改的异常");
        /// <summary>
        /// 读取暂停状态
        /// </summary>
        /// <param name="bUpdate"></param>
        /// <param name="bPauseSenso"></param>
        /// <returns></returns>
        bool GetPause(bool bUpdate = true, bool bPauseSenso = false);
        /// <summary>
        /// 设置暂停状态
        /// </summary>
        /// <param name="bPause"></param>
        /// <param name="bPauseSenso"></param>
        void SetPause(bool bPause = true, bool bPauseSenso = false);

        /// <summary>
        /// 处理暂停
        /// </summary>
        /// <param name="_isPause"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        bool Process_Pause(ref bool _isPause, ref Stopwatch watcher, bool bWhile);
        bool Process_Pause(ref Stopwatch watcher, bool bWhile);
        bool Process_Pause(ref CQueryTime watcher, bool bWhile = false);

        #region 虚拟函数
        /// <summary>
        /// 设置为急停状态并停止所有轴，断使能，结束所有线程
        /// </summary>
        /// <param name="bStop"></param>
        void SetAbruptStop(bool bStop = true);
        //以下取消，防止调用错误
        ///// <summary>
        ///// 刷新急停状态
        ///// </summary>
        ///// <returns></returns>
        //bool UpDateStop();
        ///// <summary>
        ///// 更新暂停状态
        ///// </summary>
        ///// <param name="bPauseSenso"></param>
        ///// <returns></returns>
        //bool UpdatePause(bool bPauseSenso = false);
        ///// <summary>
        ///// 设置信号灯和蜂鸣器
        ///// </summary>
        //void SetLightColor();
        #endregion

        #endregion
    }
}

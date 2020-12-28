using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using ClassLib_ParaFile;
using ClassLib_StdThread;
using ClassLib_RunMode;
using Interface;

namespace ClassLib_Motion
{
    public class Motion:IMotion
    {//运动管家
        #region 定义变量
        //Motion() { }//不允许创建对象
        //public const bool HIGH_LEVEL = false;   //高低电平取反低有效
        //public const bool LOW_LEVEL = true;
        //public const short AXIS_IN_CARD = 8;
        //public const short HOME_IN_CARD = 8;
        //public const int THREAD_TIMEOUT = 99999999;
        //public const int THREAD_WARNING = 20000;
        //public const int CONVERY_TIMEOUT = 10000;
        //public const int IO_TIMEOUT = 5000;
        //public const int SUSPEND_TIMEOUT = 5000;
        //public const double ConstDebugVel = 50;
        //public const double ConstDebugAcc = 1000;
        //public const int SAFE_OFFSET = 5000;

        //public static short CARD_TOTAL = 1;
        //public static int EXT_CARD_TOTAL = 0;
        //public static int OXT_CARD_TOTAL = 0;
        //public static int Max_Axis = 8;
        //public static short IO_IN_CARD = 16;//16
        //public static short IO_IN_CARD_EXT = 16;//16
        //public static short IO_IN_CARD_OXT = 32;

        //public static double DebugVel = 50;
        //public static double AutoSpeedPer = 0.2;
        //public static double DebugSpeedPer = 0.1;
        //public static double VelStartPer = 0.1;

        public static bool IsPauseByDoor = false;
        public static bool IsPauseBySensor = false;

        public static bool IsAxisReseted
        {
            get { return Run.s_RunMode.IsAxisReseted;}
            set { Run.s_RunMode.IsAxisReseted = value; }
        }

        //需要在主控设置
        public static string sStartButton = ""; //启动
        public static string sResetButton = ""; //复位
        public static string sEmStop = "";//"急停EXI1_0"
        public static string sPauseButton = ""; //"EXI3_0=停止";
        public static string sPauseDoor = "";//"EXI4_0=门禁开关"
        public static string sPauseSensor = "";//"光栅"
        public static string sGreenLight = "";//EXO1_0=三色灯-绿灯
        public static string sYellowLight = "";//"EXO2_0=三色灯-黄灯"
        public static string sRedLight = "";//"EXO0_0=三色灯-红灯"
        public static string sAlarmBee = "";//"EXO3_0=三色灯-蜂鸣"
        #endregion
        #region 基础流程状态切换
        public static bool g_bGoHomeOk = false;
        //public static bool GetHoming() { return Run.GetHoming(); }
        //public static bool GetAutoing() { return Run.GetAutoing(); }
        //public static bool GetRunning() { return Run.GetRunning(); }//{ return g_RunMode > Run_Mode.Home_Run; }
        //public static bool GetDebuging() { return Run.GetDebuging(); }
        //public static bool GetSingling() { return Run.GetSingling(); }
        //public static bool GetPauseSensoStop() { return Run.GetPauseSensoStop(); }
        //public static void SetHoming() { Run.SetHoming(); Motion.IsAxisReseted = false; }
        //public static void SetDebuging() { Run.SetDebuging(); }
        //public static void SetSingling() { Run.SetSingling(); }
        //public static void SetDebugSingling() { Run.SetDebugSingling(); }
        //public static void SetRunning(bool bRun, bool bDebug = false, bool bSingle = false) { Run.SetRunning(bRun, bDebug, bSingle); }
        //public static bool ThrowStop(bool bSoftStop = true)
        //{
        //    if (GetStop(bSoftStop))
        //    {
        //        throw new Exception( "流程已停止运行");
        //    }
        //    return false;
        //}
        /// <summary>
        /// 读取到停止状态时抛出异常
        /// </summary>
        /// <param name="bSoftStop">读软停或急停</param>
        /// <param name="SoftStopLog">抛出异常前写入的日志内容</param>
        /// <returns></returns>
        //public static bool ThrowStop(bool bSoftStop = true, string SoftStopLog = "")
        //{
        //    return Run.ThrowStop(bSoftStop, SoftStopLog);
        //}
        /// <summary>
        /// 读取软停或急停状态，非停止状态时刷新急停状态
        /// </summary>
        /// <param name="bSoftStop"></param>
        /// <returns></returns>
        //public static bool GetStop(bool bSoftStop = true)
        //{
        //    return Run.GetStop(bSoftStop);
        //}
        /// <summary>
        /// 刷新急停状态
        /// </summary>
        /// <returns></returns>
        public static bool UpDateStop()
        {
            if (!string.IsNullOrEmpty(sEmStop))
            {
                if (GetSmoothIo(sEmStop, times:5, delayTime:20))// || GetIo("急停EXI1_1"))//操作台急停
                {
                    //g_StopMode = Stop_Mode.Abrupt_Stop;
                    SetAbruptStop();
                    return true;
                }
            }

            return false;
        }
        /// <summary>
        /// 设置为急停状态并停止所有轴，断使能，结束所有线程
        /// </summary>
        /// <param name="bStop"></param>
        public static void SetAbruptStop(bool bStop = true)
        {            
            if (bStop)
            {
                if (g_Motion != null)
                {
                    StopAll();
                    SeverOffAll();
                }
            }
            //Run.SetAbruptStop(bStop);//可能导致无限循环
        }

        /// <summary>
        /// 设置状态为软停或清除软停
        /// </summary>
        /// <param name="bSoftStop">软停或清除软停</param>
        /// <param name="SoftStopLog">第一次设置为软停时显示的信息</param>
        /// <returns></returns>
        //public static bool SetSoftStop(bool bSoftStop = true, string SoftStopLog = "程序员忘记修改的异常")
        //{
        //    return Run.SetSoftStop(bSoftStop, SoftStopLog);
        //}
        /// <summary>
        /// 读取暂停状态
        /// </summary>
        /// <param name="bUpdate"></param>
        /// <param name="bPauseSenso"></param>
        /// <returns></returns>
        //public static bool GetPause(bool bUpdate = true, bool bPauseSenso = false)
        //{
        //    return Run.GetPause(bUpdate, bPauseSenso);
        //}

        public static bool UpdatePause(bool bPauseSenso = false)
        {//重写代替virtual
            if ((!string.IsNullOrEmpty(sPauseButton) && GetSmoothIo(sPauseButton, times: 5, delayTime: 20)))
            {
                if ((int)Run.s_RunMode.Stop < (int)Stop_Mode.Pause_Stop)
                    Publics.sDisplay.DisplayText("暂停按钮按下");
                Run.SetPause();
                return true;
            }
            if (IsPauseByDoor && (!string.IsNullOrEmpty(sPauseDoor) && GetSmoothIo(sPauseDoor, times:5, delayTime:50)))
            {
                if ((int)Run.s_RunMode.Stop < (int)Stop_Mode.Pause_Stop)
                    Publics.sDisplay.DisplayText("安全门被打开");
                Run.SetPause();
                return true;            
            }
            if (bPauseSenso && IsPauseBySensor && (!string.IsNullOrEmpty(sPauseSensor)))
            {
                if (GetSmoothIo(sPauseSensor, times: 5, delayTime: 10))
                {
                    if ((int)Run.s_RunMode.Stop < (int)Stop_Mode.Pause_Stop)
                        Publics.sDisplay.DisplayText("安全光栅被感应");
                        //SetPause();
                    Run.SetPause(bPauseSenso: true); //设置为光栅暂停，不感应时自动退出暂停
                        return true;
                }
                else
                {
                    Run.SetPause(false, true);
                }
            }
            return false;
        }

        //public static void SetPause(bool bPause = true, bool bPauseSenso = false)
        //{
        //    Run.SetPause(bPause, bPauseSenso);
        //}

        /// <summary>
        /// 处理暂停
        /// </summary>
        /// <param name="_isPause"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        //public static bool Process_Pause(ref bool _isPause, ref Stopwatch watcher, bool bWhile)
        //{
        //    return Run.Process_Pause(ref _isPause, ref watcher, bWhile);
        //}
        //public static bool Process_Pause(ref Stopwatch watcher, bool bWhile)
        //{
        //    return Run.Process_Pause(ref watcher, bWhile);
        //}
        //public static bool Process_Pause(ref CQueryTime watcher, bool bWhile = false)
        //{
        //    return Run.Process_Pause(ref watcher, bWhile);
        //}   

        public static void SetLightColor()
        {//重写代替virtual
            if (!string.IsNullOrEmpty(sGreenLight))
            {
                bool bStoped = Run.GetStop();
                if (m_bAlarm && Run.GetRunning())
                {
                    StartAlarmThread();
                    SetDo(sGreenLight, false);
                    SetDo(sYellowLight, false);
                    SetDo(sRedLight, true);
                }
                else if (bStoped)
                {
                    SetDo(sGreenLight, false);
                    SetDo(sYellowLight, false);
                    SetDo(sRedLight, true);
                    SetDo(sAlarmBee, true);
                }
                else if (Run.GetPause(false))
                {
                    SetDo(sGreenLight, false);
                    SetDo(sYellowLight, true);
                    SetDo(sRedLight, false);
                    SetDo(sAlarmBee, false);
                }
                else if (!Run.GetRunning())
                {
                    SetDo(sGreenLight, false);
                    SetDo(sYellowLight, true);
                    SetDo(sRedLight, false);
                    SetDo(sAlarmBee, false);
                }
                else
                {
                    SetDo(sGreenLight, true);
                    SetDo(sYellowLight, false);
                    SetDo(sRedLight, false);
                    //SetDo(sAlarmBee, false);
                }

            }
        }     
        //public static bool GetReady() { return g_StopMode < Stop_Mode.SoftAbrupt_Stop; }
        //public static bool GetGoHome()
        //{
        //    if (!g_bGoHomeOk)
        //        MessageBox.Show("请先回零!", "注意!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        //    return g_bGoHomeOk;
        //}
       
        #endregion

        #region 报警
        public static bool m_bAlarm = false;//是否响铃
        public static bool m_bAlarmForlong = false;//是否持续响铃
        public static double m_dAlarmTimeSpan = 0.2;//响铃间隔
        public static bool m_bDoNotAlarm = false;//是否屏蔽响铃
        public static bool m_bAlarmOnce = false;//当次响铃只响三次
        //public static void SetAlarm(bool once = false)
        //{
        //    StartAlarmThread(once);
        //}
        public static void CloseAlarm()
        {
            m_bAlarm = false;
        }
        public static Control_Thread AlarmThread = new Control_Thread();
        public static void StartAlarmThread(bool once = false)
        {
            m_bAlarm = true;
            m_bAlarmOnce = once;
            m_bDoNotAlarm = false;
            if (!AlarmThread.IsRunning())
            {
                AlarmThread = new Control_Thread();
                AlarmThread.LoadEvent += new LoadEventHandler(AlarmThread_LoadEvent);
                AlarmThread.Start();
            }
        }

        static short AlarmThread_LoadEvent(object sender, EventArgs e)
        {
            return AlarmFunction();
        }
        public static short AlarmFunction()
        {
            try
            {
                if (m_bAlarm)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (!m_bDoNotAlarm)
                            SetDo(sAlarmBee, true);
                        Thread.Sleep((int)(m_dAlarmTimeSpan * 1000));
                        SetDo(sAlarmBee, false);
                        Thread.Sleep((int)(m_dAlarmTimeSpan * 1000));
                    }
                }
                if (!m_bAlarmForlong || m_bAlarmOnce)
                {
                    m_bAlarmOnce = false;
                    m_bAlarm = false;
                }
                Thread.Sleep((int)(m_dAlarmTimeSpan * 2000));
            }
            catch
            {
                return -1;
            }
            return 0;
        }
        #endregion

        #region 声明运控对象，在主控中创建
        protected static AbsMotion g_Motion = null;//= new GtsMotion();
        //public static OtherIOs otherIOs = null; //OtherIOs改为继承自AbsMotion，运控卡与IOC0640卡不混用

        public static bool CreateMotion(AbsMotion absMotion = null)
        {
            g_Motion = absMotion;
            return InitMotion();

        }
        /// <summary>
        ///释放运动控制模块 
        /// </summary>
        /// <returns></returns>
        public static short FreeMotion()
        {
            if (g_Motion != null)
            {
                g_Motion = null;
                Dispose();
            }
            //if (otherIOs != null)
            //{
            //    otherIOs = null;
            //}
            return 0;
        }
        #endregion

#region 通过字典智能获取轴和IO
        static public Dictionary<int, AbsAxis> mapIndex2Axis = new Dictionary<int, AbsAxis>(24);     //1 TO 左X轴AbsAxis 通过下标访问，具体的轴号可以变更而不需要修改程序
        static public Dictionary<string, AbsAxis> mapMark1Axis = new Dictionary<string, AbsAxis>(24);//Axis1 TO AbsAxis
        static public Dictionary<string, AbsAxis> mapMark2Axis = new Dictionary<string, AbsAxis>(24);//X1 TO AbsAxis
        static public Dictionary<string, AbsAxis> mapName2Axis = new Dictionary<string, AbsAxis>(24);//左X轴 TO AbsAxis
        static public Dictionary<string, string> mapIoChanged = new Dictionary<string, string>(24);     //需重新指向的IO
        static public Dictionary<string, AbsIo> mapMark2Io = new Dictionary<string, AbsIo>(100);     //EXI0_0 TO AbsIo
        static public Dictionary<string, string> mapName2Mark = new Dictionary<string, string>(100); //气源压力 TO EXI0_0 
        static public Dictionary<string, string> mapMark2Name = new Dictionary<string, string>(100);  //EXI0_0 TO 气源压力
        static public List<AbsCard> vCard = new List<AbsCard>(10);
        //public Dictionary<string, string> mapExt2Card;

        /// <summary>
        /// 释放所有Dictionary与List
        /// </summary>
        static public void Dispose()
        {
            try
            {
                foreach (AbsCard iter in vCard)
                {
                    iter.Dispose();
                }
                vCard.Clear();

                foreach (KeyValuePair<string, AbsIo> kvp in mapMark2Io)
                {
                    kvp.Value.Dispose();
                }
                mapMark2Io.Clear();

                foreach (KeyValuePair<int, AbsAxis> kvp in mapIndex2Axis)
                {
                    kvp.Value.Dispose();
                }
                mapIndex2Axis.Clear();
                mapMark2Axis.Clear();
                mapName2Axis.Clear();
                mapName2Mark.Clear();
                mapMark2Name.Clear();
                mapMark1Axis.Clear();
                mapIoChanged.Clear();
                //mapExt2Card.Clear();  
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 将Motion.ini载入到Dictionary与List中来
        /// </summary>
        static public bool InitMotion()
        {
            Dispose();

            if (g_Motion.InitMotion(new Motion()))
                return true;
            try
            {
                string IniFileName = Publics.GetMotionPath() + "Motion.ini";
                //获取主卡及扩展卡配置文件路径
                string sRead = ParaFileINI.ReadINI("CARD", "CardTotal", IniFileName);
                AbsMotion.CARD_TOTAL = Convert.ToInt16(sRead);
                sRead = ParaFileINI.ReadINI("CARD", "ExtCardTotal", IniFileName);
                AbsMotion.EXT_CARD_TOTAL = Convert.ToInt32(sRead);
                sRead = ParaFileINI.ReadINI("CARD", "OxtCardTotal", IniFileName);
                AbsMotion.OXT_CARD_TOTAL = Convert.ToInt32(sRead);
                sRead = ParaFileINI.ReadINI("AXIS", "AxisTotal", IniFileName);
                AbsMotion.Max_Axis = Convert.ToInt32(sRead);
                sRead = ParaFileINI.ReadINI("AXIS", "AxisInFirstCard", IniFileName, "8");
                AbsMotion.AXIS_IN_CARD = Convert.ToInt16(sRead);
                sRead = ParaFileINI.ReadINI("CARD", "IoInFirstCard", IniFileName, "16");
                AbsMotion.IO_IN_CARD = Convert.ToInt16(sRead);
                sRead = ParaFileINI.ReadINI("CARD", "IoInExtCard", IniFileName, "16");
                AbsMotion.IO_IN_CARD_EXT = Convert.ToInt16(sRead);

                if (AbsMotion.CARD_TOTAL == 0 || AbsMotion.Max_Axis == 0)
                    return false;
                //要复写与运控相关的回调函数
                ClassLib_RunMode.Run.s_RunMode = new RunModeMotion();      

                //Publics.Process_Pause = new Publics.CallBackProcessPause(Motion.Process_Pause);
                //Publics.GetStop = new Publics.CallBackGetStop(Motion.GetStop);
                //Publics.SetStop = new Publics.CallBackSetStop(Motion.SetSoftStop);
                //Publics.ThrowStop = new Publics.CallBackSetStop(Motion.ThrowStop);
                //Publics.GetRunning = new Publics.CallBackGetRunning(Motion.GetRunning);
                //Publics.GetDebuging = new Publics.CallBackGetRunning(Motion.GetDebuging);

                //if (Motion.CARD_TOTAL == 0 || Motion.Max_Axis == 0)
                //    return;

                //生成普通IO及HOME和LIMIT
                for (int iCard = 0; iCard < (AbsMotion.Max_Axis - 1) / AbsMotion.AXIS_IN_CARD + 1; iCard++)
                {
                    //new GtsCard(Convert.ToInt16(iCard), sCardFile, mdl, false, sExtCardFile)
                    vCard.Add(g_Motion.CreateCard(iCard, IniFileName));
                    for (int iPort = 0; iPort < AbsMotion.IO_IN_CARD; iPort++)
                    {
                        string str;
                        str = "EXI";
                        str += iPort.ToString();
                        str += "_";
                        str += iCard.ToString();
                        mapMark2Io[str] = g_Motion.CreateIo(str);

                        str = "EXO";
                        str += iPort.ToString();
                        str += "_";
                        str += iCard.ToString();
                        mapMark2Io[str] = g_Motion.CreateIo(str);
                    }
                    for (int iPort = 0; iPort < AbsMotion.AXIS_IN_CARD; iPort++)
                    {
                        string str = "HOME";
                        str += iPort.ToString();
                        str += "_";
                        str += iCard.ToString();
                        mapMark2Io[str] = g_Motion.CreateIo(str);

                        str = "LIMIT";
                        str += iPort.ToString();
                        str += "+_";//P
                        str += iCard.ToString();
                        mapMark2Io[str] = g_Motion.CreateIo(str);

                        str = "LIMIT";
                        str += iPort.ToString();
                        str += "-_";//N
                        str += iCard.ToString();
                        mapMark2Io[str] = g_Motion.CreateIo(str);

                        //驱动报警
                        //电机到位
                        //伺服驱动
                        //报警清除
                    }
                }
                //生成扩展卡IO
                for (int iCard = 0; iCard < AbsMotion.EXT_CARD_TOTAL; iCard++)
                {
                    for (int iPort = 0; iPort < AbsMotion.IO_IN_CARD_EXT; iPort++)
                    {
                        string str = "DXI";
                        str += iPort.ToString();
                        str += "_";
                        str += iCard.ToString();
                        mapMark2Io[str] = g_Motion.CreateIo(str);

                        str = "DXO";
                        str += iPort.ToString();
                        str += "_";
                        str += iCard.ToString();
                        mapMark2Io[str] = g_Motion.CreateIo(str);
                    }
                }

                //生成轴并配置轴号及轴名称，导程，细分
                for (int i = 1; i <= AbsMotion.Max_Axis; i++)
                {
                    string sValue = "Axis" + i;
                    string str = ParaFileINI.ReadINI("AXIS", sValue, IniFileName);
                    string[] sst = str.Trim().Split('_');
                    if (sst.Count() > 1)
                    {
                        Int16 iAxisNum = Convert.ToInt16(sst[0]);
                        string sName = sst[sst.Count() - 1];
                        sValue = "Pitch" + i;
                        str = ParaFileINI.ReadINI("AXIS", sValue, IniFileName);
                        double dPitch = Convert.ToDouble(str);
                        sValue = "Divide" + i;
                        str = ParaFileINI.ReadINI("AXIS", sValue, IniFileName);
                        double dDivide = Convert.ToDouble(str);
                        mapIndex2Axis[i] = g_Motion.CreateAxis((short)i, iAxisNum, sName, dPitch, dDivide);

                        //str = ParaFileINI.ReadINI("AXIS", "Vel" + i, IniFileName);
                        //mapIndex2Axis[i].ChangeVel(Convert.ToDouble(str));
                        //str = ParaFileINI.ReadINI("AXIS", "Acc" + i, IniFileName);
                        //mapIndex2Axis[i].ChangeAcc(Convert.ToDouble(str));

                        //ParaFileINI paraFile = new ParaFileINI(IniFileName, "RESET" + i);
                        //if (paraFile.m_bEx)
                        //{
                        //    paraFile.ReadINI("IsNegateResetDirection", ref mapIndex2Axis[i].m_ResetAxis.IsNegateResetDirection);
                        //    paraFile.ReadINI("ResetType", ref mapIndex2Axis[i].m_ResetAxis.ResetType);
                        //    paraFile.ReadINI("FastHomeSpeed", ref mapIndex2Axis[i].m_ResetAxis.FastHomeSpeed, 80);
                        //    paraFile.ReadINI("FastHomeAcc", ref mapIndex2Axis[i].m_ResetAxis.FastHomeAcc, 500);
                        //    paraFile.ReadINI("SlowHomeSpeed", ref mapIndex2Axis[i].m_ResetAxis.SlowHomeSpeed, 0.2);
                        //    paraFile.ReadINI("SlowHomeAcc", ref mapIndex2Axis[i].m_ResetAxis.SlowHomeAcc, 10);
                        //    paraFile.ReadINI("RollBackDistance_1", ref mapIndex2Axis[i].m_ResetAxis.RollBackDistance_1, 3.0);
                        //    paraFile.ReadINI("RollBackDistance_2", ref mapIndex2Axis[i].m_ResetAxis.RollBackDistance_2, 0.5);
                        //    paraFile.ReadINI("OffsetZero", ref mapIndex2Axis[i].m_ResetAxis.OffsetZero, 1);
                        //    paraFile.ReadINI("TimeOut", ref mapIndex2Axis[i].m_ResetAxis.TimeOut, 20);
                        //    paraFile.ReadINI("IsServo", ref mapIndex2Axis[i].m_IsServo, true);
                        //}
                        mapMark1Axis["Axis" + i.ToString()] = mapIndex2Axis[i];
                        mapMark2Axis[sst[1]] = mapIndex2Axis[i];
                        if (sst.Count() > 2)
                            mapName2Axis[sst[2]] = mapIndex2Axis[i];
                    }
                }
                ReadAxisIni();
                ReadResetIni();
                //Motion.AutoSpeedPer = Convert.ToDouble(ParaFileINI.ReadINI("AXIS", "AutoPer", IniFileName, "0.2"));
                //Motion.DebugSpeedPer = Convert.ToDouble(ParaFileINI.ReadINI("AXIS", "DebugPer", IniFileName, "0.1"));

                //配置IO名称
                ReadIoIni();
                //foreach (KeyValuePair<string, AbsIo> kvp in mapMark2Io)
                //{
                //    string str = ParaFileINI.ReadINI("IO", kvp.Key, IniFileName);
                //    if (str != string.Empty)
                //    {
                //        string[] sst = str.Trim().Split(' ');
                //        kvp.Value.m_sName = sst[0].Trim();
                //        if (!mapMark2Name.ContainsKey(kvp.Key))
                //            mapMark2Name.Add(kvp.Key, kvp.Value.m_sName);
                //        if (mapName2Mark.Count(x => x.Key == kvp.Value.m_sName) == 0)
                //            mapName2Mark.Add(kvp.Value.m_sName, kvp.Key);//注意如果IO显示内容重复时会抛出异常
                //    }
                //}
                ReadChangedIO();
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine(e.Message);
            }
            return true;
        }

        static string IniFileName = Publics.GetMotionPath() + "Motion.ini";
        /// <summary>
        /// 读入轴的速度加速度信息
        /// </summary>
        static public void ReadAxisIni()
        {
            //string IniFileName = Publics.GetMotionPath() + "Motion.ini";
            for (int i = 1; i <= AbsMotion.Max_Axis; i++)
            {
                string str = ParaFileINI.ReadINI("AXIS", "Vel" + i, IniFileName);
                mapIndex2Axis[i].ChangeVel(Convert.ToDouble(str));
                str = ParaFileINI.ReadINI("AXIS", "Acc" + i, IniFileName);
                mapIndex2Axis[i].ChangeAcc(Convert.ToDouble(str));
            }
            AbsMotion.AutoSpeedPer = Convert.ToDouble(ParaFileINI.ReadINI("AXIS", "AutoPer", IniFileName, "0.2"));
            AbsMotion.DebugSpeedPer = Convert.ToDouble(ParaFileINI.ReadINI("AXIS", "DebugPer", IniFileName, "0.1"));
        }

        /// <summary>
        /// 写入速度加速度 以及运行与调试速度百分比
        /// </summary>
        static public void WriteAxisIni()
        {
            //string IniFileName = Publics.GetMotionPath() + "Motion.ini";
            for (int i = 1; i <= AbsMotion.Max_Axis; i++)
            {
                if (mapIndex2Axis.Keys.Contains(i) == true)
                {
                    ParaFileINI.WriteINI("AXIS", "Vel" + i, mapIndex2Axis[i].ReadVel().ToString(), IniFileName);
                    ParaFileINI.WriteINI("AXIS", "Acc" + i, mapIndex2Axis[i].ReadAcc().ToString(), IniFileName);
                }
            }
            ParaFileINI.WriteINI("AXIS", "AutoPer", AbsMotion.AutoSpeedPer.ToString(), IniFileName);
            ParaFileINI.WriteINI("AXIS", "DebugPer", AbsMotion.DebugSpeedPer.ToString(), IniFileName);
        }

        /// <summary>
        /// 读取Motion.ini中的轴复位信息，当修改了Motion.ini中的复位信息后，可以直接读入而不重启程序
        /// </summary>
        static public void ReadResetIni()
        {
            string IniFileName = Publics.GetMotionPath() + "Motion.ini";
            for (int i = 1; i <= AbsMotion.Max_Axis; i++)
            {
                ParaFileINI paraFile = new ParaFileINI(IniFileName, "RESET" + i);
                if (paraFile.m_bEx)
                {
                    paraFile.ReadINI("IsNegateResetDirection", ref mapIndex2Axis[i].m_ResetAxis.IsNegateResetDirection);
                    paraFile.ReadINI("ResetType", ref mapIndex2Axis[i].m_ResetAxis.ResetType);
                    paraFile.ReadINI("FastHomeSpeed", ref mapIndex2Axis[i].m_ResetAxis.FastHomeSpeed, 80);
                    paraFile.ReadINI("FastHomeAcc", ref mapIndex2Axis[i].m_ResetAxis.FastHomeAcc, 500);
                    paraFile.ReadINI("SlowHomeSpeed", ref mapIndex2Axis[i].m_ResetAxis.SlowHomeSpeed, 0.2);
                    paraFile.ReadINI("SlowHomeAcc", ref mapIndex2Axis[i].m_ResetAxis.SlowHomeAcc, 10);
                    paraFile.ReadINI("RollBackDistance_1", ref mapIndex2Axis[i].m_ResetAxis.RollBackDistance_1, 3.0);
                    paraFile.ReadINI("RollBackDistance_2", ref mapIndex2Axis[i].m_ResetAxis.RollBackDistance_2, 0.5);
                    paraFile.ReadINI("OffsetZero", ref mapIndex2Axis[i].m_ResetAxis.OffsetZero, 1);
                    paraFile.ReadINI("TimeOut", ref mapIndex2Axis[i].m_ResetAxis.TimeOut, 20);
                    paraFile.ReadINI("IsServo", ref mapIndex2Axis[i].m_IsServo, true);
                }
            }
        }

        /// <summary>
        /// 写入轴复位信息到Motion.ini
        /// </summary>
        static public void WriteResetIni()
        {
            for (int i = 1; i <= AbsMotion.Max_Axis; i++)
            {
                //string IniFileName = Publics.GetMotionPath() + "Motion.ini";
                ParaFileINI paraFile = new ParaFileINI(IniFileName, "RESET" + i);
                if (paraFile.m_bEx)
                {
                    paraFile.WriteINI("IsNegateResetDirection", mapIndex2Axis[i].m_ResetAxis.IsNegateResetDirection);
                    paraFile.WriteINI("ResetType", mapIndex2Axis[i].m_ResetAxis.ResetType);
                    paraFile.WriteINI("FastHomeSpeed", mapIndex2Axis[i].m_ResetAxis.FastHomeSpeed);
                    paraFile.WriteINI("FastHomeAcc", mapIndex2Axis[i].m_ResetAxis.FastHomeAcc);
                    paraFile.WriteINI("SlowHomeSpeed", mapIndex2Axis[i].m_ResetAxis.SlowHomeSpeed);
                    paraFile.WriteINI("SlowHomeAcc", mapIndex2Axis[i].m_ResetAxis.SlowHomeAcc);
                    paraFile.WriteINI("RollBackDistance_1", mapIndex2Axis[i].m_ResetAxis.RollBackDistance_1);
                    paraFile.WriteINI("RollBackDistance_2", mapIndex2Axis[i].m_ResetAxis.RollBackDistance_2);
                    paraFile.WriteINI("OffsetZero", mapIndex2Axis[i].m_ResetAxis.OffsetZero);
                    paraFile.WriteINI("TimeOut", mapIndex2Axis[i].m_ResetAxis.TimeOut);
                    paraFile.WriteINI("IsServo", mapIndex2Axis[i].m_IsServo);
                }
            }
        }

        /// <summary>
        /// 读入IO信息
        /// </summary>
        static public void ReadIoIni()
        {
            foreach (KeyValuePair<string, AbsIo> kvp in mapMark2Io)
            {
                string str = ParaFileINI.ReadINI("IO", kvp.Key, IniFileName);
                if (str != string.Empty)
                {
                    string[] sst = str.Trim().Split(' ');
                    kvp.Value.m_sName = sst[0].Trim();
                    if (!mapMark2Name.ContainsKey(kvp.Key))
                        mapMark2Name.Add(kvp.Key, kvp.Value.m_sName);
                    else
                        mapMark2Name[kvp.Key] = kvp.Value.m_sName;
                    if (mapName2Mark.Count(x => x.Key == kvp.Value.m_sName) == 0)
                        mapName2Mark.Add(kvp.Value.m_sName, kvp.Key);//注意如果IO显示内容重复时会抛出异常
                    else
                        mapName2Mark[kvp.Value.m_sName] = kvp.Key;//注意如果IO显示内容重复时会抛出异常
                }
            }
        }

        /// <summary>
        /// 读取重新指向的IO，可以在修改Motion.ini后直接读入而不需重启软件
        /// </summary>
        static public void ReadChangedIO()
        {
            mapIoChanged.Clear();
            Dictionary<string, string> mapTmp = new Dictionary<string, string>(100);
            string IOChangeName = Publics.GetGtsPath() + "ChangedIO.ini";//Publics.GetMotionPath()
            foreach (KeyValuePair<string, AbsIo> kvp in mapMark2Io)
            {
                string str = ParaFileINI.ReadINI("IO", kvp.Key, IOChangeName);
                if (str != string.Empty && str != "0")
                {
                    string[] sst = str.Trim().Split(' ');
                    if (!mapIoChanged.ContainsKey(sst[0].Trim()))
                    {
                        mapTmp[sst[0]] = kvp.Value.m_sName;
                        mapIoChanged.Add(kvp.Key, sst[0].Trim());
                    }
                }
            }
            foreach (KeyValuePair<string, string> kvp in mapTmp)
            {
                mapMark2Io[kvp.Key].m_sName = kvp.Value;
                mapMark2Name[kvp.Key] = kvp.Value;
                mapName2Mark[kvp.Value] = kvp.Key;
            }
        }
        //public void ResetChangedIO()
        //{
        //    mapIoChanged.Clear();
        //    string IOChangeName = Publics.GetGtsPath() + "ChangedIO.ini";
        //    foreach (KeyValuePair<string, AbsIo> kvp in mapMark2Io)
        //    {
        //        string str = ParaFileINI.ReadINI("IO", kvp.Key, IOChangeName);
        //        //str = "LIMIT3-_0 飞达A剥料检测 X215";
        //        if (str != string.Empty && str != "0")
        //        {
        //            string[] sst = str.Trim().Split(' ');
        //            if (!mapIoChanged.ContainsKey(sst[0].Trim()))
        //            {
        //                string tempName = mapMark2Io[sst[0]].m_sName;
        //                mapMark2Io[sst[0]].m_sName = sst[1].Trim();
        //                kvp.Value.m_sName = tempName;
        //                mapIoChanged.Add(kvp.Key, sst[0].Trim());
        //                mapIoChanged.Add(sst[0].Trim(), kvp.Key);
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// 解码IO
        /// </summary>
        /// <param name="sio"></param>
        static public void Decode(ref string sio)
        {//解码，去除多余内容
            if (string.IsNullOrEmpty(sio))
                return;
            int iPos = sio.IndexOf("=");
            if (iPos >= 0)
            {//用来兼容ini内容
                sio = sio.Substring(0, iPos);
            }
            if (sio[0] != 'E' && sio[0] != 'D' && sio[0] != 'H' && sio[0] != 'L' && sio[0] != 'O')
            {//前面是注释，需要解码
                iPos = sio.IndexOf("EX");
                if (iPos < 0)
                {
                    iPos = sio.IndexOf("DX");
                    if (iPos < 0)
                    {
                        iPos = sio.IndexOf("HOME");
                        if (iPos < 0)
                        {
                            iPos = sio.IndexOf("LIMIT");
                            if (iPos < 0)
                            {
                                iPos = sio.IndexOf("OX");
                            }
                        }
                    }
                }
                if (iPos > 0)
                    sio = sio.Substring(iPos, sio.Length - iPos);
                else
                    sio = "";
            }

            if (sio.Length > 7 && sio[sio.Length - 3] == 'N')
                sio = sio.Remove(sio.Length - 3, 1).Insert(6, "-");
            else if (sio.Length > 7 && sio[sio.Length - 3] == 'P')
                sio = sio.Remove(sio.Length - 3, 1).Insert(6, "+");

            if (sio.Length > 0 && sio[sio.Length - 2] != '_')
                sio += "_0";//第一张卡缩写时补齐
        }

        /// <summary>
        /// 解码轴
        /// </summary>
        /// <param name="sAxis"></param>
        static public void DecodeAxis(ref string sAxis)
        {
            if (string.IsNullOrEmpty(sAxis))
                return;
            if (sAxis.Contains('='))
            {
                int iPos = sAxis.IndexOf("=");
                if (iPos >= 0)
                {//用来兼容ini内容
                    sAxis = sAxis.Substring(0, iPos);
                }
                sAxis = sAxis.Trim();
            }
        }

        #region 对卡的通用操作
        static public void ReOpen()
        {
            foreach (AbsCard iter in vCard)
            {
                iter.CloseCard();
                Thread.Sleep(200);
                iter.OpenCard();
            }
        }
        static public void SetAllIo(bool level = AbsMotion.HIGH_LEVEL)
        {//设置并自动获取所有IO
            foreach (KeyValuePair<string, AbsIo> kvp in mapMark2Io)
            {
                kvp.Value.SetDoBit(level);
            }
        }
        static public void GetAllIo(bool bBit = false)
        {//获取所有IO
            if (!bBit)
            {
                for (int iCard = 0; iCard < (AbsMotion.Max_Axis - 1) / AbsMotion.AXIS_IN_CARD + 1; iCard++)
                {
                    Motion.GetIo("EXI0_" + iCard.ToString());
                    Motion.GetIo("EXO0_" + iCard.ToString());
                    Motion.GetIo("HOME0_" + iCard.ToString());
                    Motion.GetIo("LIMIT0+_" + iCard.ToString());
                    Motion.GetIo("LIMIT0-_" + iCard.ToString());
                }
                for (int iCard = 0; iCard < AbsMotion.EXT_CARD_TOTAL; iCard++)
                {
                    Motion.GetIo("DXI0_" + iCard.ToString());
                    Motion.GetIo("DXO0_" + iCard.ToString());
                }
                ReadAllIo();
            }
            else
            {
                foreach (KeyValuePair<string, AbsIo> kvp in mapMark2Io)
                {
                    kvp.Value.GetIoBit();
                }
            }
        }
        static public void ReadAllIo()
        {//获取所有IO
            foreach (KeyValuePair<string, AbsIo> kvp in mapMark2Io)
            {
                kvp.Value.ReadBit();
            }
        }

        //流程调用获取IO和轴对象
        static public AbsIo GetDicIo(string sio)
        {
            string sioOrg = sio;
            Decode(ref sio);

            //当IO被重新指定时，并且在非界面控制时，需要指向新的IO
            if (sioOrg.Length > sio.Length + 1 && mapIoChanged.Count > 0 && mapIoChanged.ContainsKey(sio))
                sio = mapIoChanged[sio];

            if (mapMark2Io.ContainsKey(sio))
                return mapMark2Io[sio];
            else if (mapName2Mark.ContainsKey(sio) && mapMark2Io.ContainsKey(mapName2Mark[sio]))
                return mapMark2Io[mapName2Mark[sio]];  //可以通过字符串解码或二级map的方式来定义注释
            else
            {
                //MessageBox.Show("错误的IO指令访问：" + sio.ToString(), "严重错误！", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                return null;
            }
        }

        #region 获取指定轴
        /// <summary>
        /// 根据虚拟轴号获取轴
        /// </summary>
        /// <param name="iAxis"></param>
        /// <returns></returns>
        static public AbsAxis GetAxis(int iAxis)
        {
            if (mapIndex2Axis.ContainsKey(iAxis))
                return mapIndex2Axis[iAxis];
            else
            {
                //if (iAxis <= Motion.Max_Axis)
                //    MessageBox.Show("错误的Axis指令访问：" + iAxis.ToString(), "严重错误！", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                return null;
            }
        }

        //根据字符串获取轴
        static public AbsAxis GetAxis(string sAxis)
        {
            if (mapMark2Axis.ContainsKey(sAxis))
                return mapMark2Axis[sAxis];
            else if (mapName2Axis.ContainsKey(sAxis))
                return mapName2Axis[sAxis];
            DecodeAxis(ref sAxis);
            if (mapMark1Axis.ContainsKey(sAxis))
                return mapMark1Axis[sAxis];
            else
            {
                MessageBox.Show("错误的Axis指令访问：" + sAxis, "严重错误！", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                return null;
            }
        }

        /// <summary>
        /// 获取虚拟轴号
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public static int GetAxis(AbsAxis axis)
        {
            if (axis != null)
                return axis.m_VirtualIndex;
            else
                return -1;
        }

        /// <summary>
        /// 轴号是否无效
        /// </summary>
        /// <param name="iAxis"></param>
        /// <returns></returns>
        public static bool IsNullAxis(int iAxis)
        {
            if (iAxis < 1 || iAxis >= 100)
                return true;
            else
                return false;
        }
        #endregion
        // 清除各轴的报警和限位
        static public short ClrStsAll()
        {
            foreach (AbsCard iter in vCard)
            {
                iter.ClrStsAll();
            }
            return 0;
        }
        static public void ResetEventAll()
        {
            foreach (KeyValuePair<int, AbsAxis> kvp in mapIndex2Axis)
            {
                //kvp.Value.myThread->myEvent->ResetEvent();
            }
        }
        static public void StopAll()
        {
            foreach (KeyValuePair<int, AbsAxis> kvp in mapIndex2Axis)
            {
                kvp.Value.Stop();
            }
        }
        static public void SeverOffAll()
        {
            foreach (KeyValuePair<int, AbsAxis> kvp in mapIndex2Axis)
            {
                kvp.Value.AxisOff();
            }
        }
        #endregion
#endregion
        #region 读取及设置IO
        public static bool SetDo(string cIoName, bool bLevel)
        {
            //当硬急停时禁止设置DO 软急停不受限制
            if (Run.GetStop(false) && !ListEmStopCanDo.Contains(cIoName))
                return false;
            if (string.IsNullOrEmpty(cIoName))
                return bLevel; //空串容错
            Decode(ref cIoName);
            if (string.IsNullOrEmpty(cIoName))
                return bLevel; //空串容错
            if (bLevel)
                bLevel = AbsMotion.HIGH_LEVEL;
            else
                bLevel = AbsMotion.LOW_LEVEL;
            AbsIo io = GetDicIo(cIoName);
            if (io != null)
            {
                if (IsNegateIO(io))
                    bLevel = !bLevel;
                if (DIsNegateIO(io))
                    bLevel = !bLevel;
                bool bGet = io.SetDoBit(bLevel);

                if (IsNegateIO(io))
                    bGet = !bGet;
                if (DIsNegateIO(io))
                    bGet = !bGet;

                if (AbsMotion.HIGH_LEVEL == bGet)
                    return true;
                else
                    return false;
            }
            //else
            //{
            //    io = otherIOs.GetIo(cIoName);
            //    if (io != null)
            //    {                    
            //        if (OIsNegateIO(io))
            //            bLevel = !bLevel;
            //        bool bGet = io.SetDoBit(bLevel);
            //        if (OIsNegateIO(io))
            //            bGet = !bGet;
            //        if (HIGH_LEVEL == bGet)
            //            return true;
            //        else
            //            return false;
            //    }
            //    else
            //        return false;
            //}
            return false;
        }

        public static bool SetDo(string cIoName, bool bLevel, string cWaitIn, bool bWaitLevel, string erro, long lTimeOut = 5000, string select = ",是否重新等待\r\n重试--继续等待\r\n取消--结束工作")
        {
            bool rtn = false;
            Start:
            Run.ThrowStop();
                rtn = SetDo(cIoName, bLevel);            
            if (!WaitIo(cWaitIn, erro, bWaitLevel, lTimeOut, select, true))
                    goto Start;
            return true;
        }
        public static bool SetDos(string[] cIoName, bool[] bLevel)
        {
            int i = 0;
            bool rtn = false;
            foreach (var io in cIoName)
            {
                if (i < bLevel.Length)
                    rtn = (rtn || SetDo(io, bLevel[i]));
                else
                    rtn = (rtn || SetDo(io, bLevel[0]));
                i++;
            }
            return rtn;
        }

        public static bool SetDos(string[] cIoName, bool[] bLevel, string cWaitIn, bool bWaitLevel, string erro, long lTimeOut = 5000, string select = ",是否重新等待\r\n重试--继续等待\r\n取消--结束工作")
        {
            bool rtn = false;
        Start:
            Run.ThrowStop();
            rtn = SetDos(cIoName, bLevel);
            if (!WaitIo(cWaitIn, erro, bWaitLevel, lTimeOut, select, true))
                goto Start;
            return rtn;
        }

        public static bool GetIos(string[] cIoName, bool bTrue = true, bool bOnce = true)
        { 
            if (cIoName.Length > 0)
            {
                if (GetIo(cIoName[0]) != bTrue)
                {
                    for (int i = 1; i < cIoName.Length; i++)
                    {
                        if (bOnce)
                        {
                            if (ReadIo(cIoName[i]) == bTrue)
                                return true;
                        }
                        else
                        {
                            if (GetIo(cIoName[i]) == bTrue)
                                return true;
                        }
                    }
                }
                else
                    return true;
            }
            return false;
        }
 /// <summary>
        /// 等待Io信号变化并确认
        /// </summary>
        /// <param name="cIoName"></param>
        /// <param name="times"></param>
        /// <param name="delayTime"></param>
        /// <returns></returns>
        public static bool WaitIoChanged(string cIoName, int times = 3, int delayTime = 10)
        {
            bool bDefault = GetIo(cIoName);
StartWork:
            bool rtn = WaitIo(cIoName, !bDefault, AbsMotion.CONVERY_TIMEOUT, bException: false);
            if (rtn)
            {
                if (GetSmoothIo(cIoName, !bDefault, times, delayTime))
                    return true;
                else
                    goto StartWork;
            }
            return false;
        }

        /// <summary>
        /// 连续读到多次相同信号时才确认为此信号
        /// </summary>
        /// <param name="cIoName"></param>
        /// <param name="bDefault"></param>
        /// <param name="times"></param>
        /// <param name="delayTime"></param>
        /// <returns></returns>
        public static bool GetSmoothIo(string cIoName, bool bDefault = true, int times = 3, int delayTime = 10)
        {
            int i = 0;
            do
            {
                if (GetIo(cIoName) == bDefault)
                {
                    if (i < times - 1)
                        Thread.Sleep(delayTime);                
                }
                else
                    return false;
                i++;
            }
            while(i < times);
            return true;
        }
        public bool GetIoInObj(string cIoName)
        {
            return Motion.GetIo(cIoName);
        }
        public static bool GetIo(string cIoName)
        {
            return GetIo(cIoName, true);
        }
        public static bool GetIo(string cIoName, bool bDefault)
        {
            Decode(ref cIoName);
            if (string.IsNullOrEmpty(cIoName))
                return bDefault; //空串容错
            AbsIo io = GetDicIo(cIoName);
            if (io != null)
            {
                bool bGet = io.GetIoBit();
                if (IsNegateIO(io))
                    bGet = !bGet;
                if (DIsNegateIO(io))
                    bGet = !bGet;
                if (AbsMotion.HIGH_LEVEL == bGet)
                    return true;
                else
                    return false;
            }
            //else
            //{
            //    io = otherIOs.GetIo(cIoName);
            //    if (io != null)
            //    {                    
            //        bool bGet = io.GetIoBit();
            //        if (OIsNegateIO(io))
            //            bGet = !bGet;
            //        if (HIGH_LEVEL == bGet)
            //            return true;
            //        else
            //            return false;
            //    }
            //    else
            //        return false;
            //}
            return false;
        }
        public static bool ReadIo(string cIoName)
        {
            if(cIoName.Contains("XO"))
            { return ReadOutput(cIoName); }
            return ReadIo(cIoName, true);
        }

        public static bool ReadOutput(string cIoName)
        {
            Decode(ref cIoName);
            if (string.IsNullOrEmpty(cIoName))
                return false;
            AbsIo io = GetDicIo(cIoName);
            if (io != null)
            {
                return io.GetIoBit();
            }
            return false;
        }

        public static bool ReadIo(string cIoName, bool bDefault = true)
        {
            Decode(ref cIoName);
            if (string.IsNullOrEmpty(cIoName))
                return bDefault; //空串容错
            AbsIo io = GetDicIo(cIoName);
            if (io != null)
            {
                bool bGet = io.ReadBit();
                if (IsNegateIO(io))
                    bGet = !bGet;
                if (DIsNegateIO(io))
                    bGet = !bGet;
                if (AbsMotion.HIGH_LEVEL == bGet)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        static List<string> _ListIo = new List<string>(10);
        static List<string> _DListIo = new List<string>(10);
        static List<string> _OListIo = new List<string>(10);
        //static Predicate<string> t = new Predicate<string>(Match);   //定义一个比较委托

        /// <summary>
        /// 急停，暂停，门开关信号反转
        /// </summary>
        public static void AddNegateIO()
        {
            _ListIo.Add(sEmStop);
            //_ListIo.Add(sEmStop+"_1");
            //_ListIo.Add(sEmStop+"_2");
            _ListIo.Add(sPauseButton);
            _ListIo.Add(sPauseDoor);
            //_ListIo.Add("LIMIT7+_0=光栅信号");         
        }
        /// <summary>
        /// 扩展卡信号反转
        /// </summary>
        /// <param name="IOString"></param>
        public static void DAddNegateIO(String IOString)
        {
            _DListIo.Add(IOString);
        }

        public static bool DIsNegateIO(AbsIo io)
        {
            if (!io.GetMark().Contains("_"))//卡号不能省，否则会找错
                return false;
            return _DListIo.Exists(x => x.Contains(io.GetMark()));
        }

        /// <summary>
        /// 任意信号反转
        /// </summary>
        /// <param name="IOString"></param>
        public static void AddNegateIO(String IOString)
        {
            _ListIo.Add(IOString);
        }
        public static bool IsNegateIO(AbsIo io)
        {
            if (!io.GetMark().Contains("_"))//卡号不能省，否则会找错
                return false;
            return _ListIo.Exists(x => x.Contains(io.GetMark()));
        }
        /// <summary>
        /// 卡信号反转
        /// </summary>
        /// <param name="IOString"></param>
        public static void OAddNegateIO(String IOString)
        {
            AddNegateIO(IOString);
            //_OListIo.Add(IOString);
        }

        public static bool OIsNegateIO(AbsIo io)
        {
            return IsNegateIO(io);
            //if (!io.GetMark().Contains("_"))//卡号不能省，否则会找错
            //    return false;
            //return _OListIo.Exists(x => x.Contains(io.GetMark()));
        }

        /// <summary>
        /// 硬急停后仍可操作的DO
        /// </summary>
        public static List<string> ListEmStopCanDo = new List<string>(10);
        #endregion

        #region 等待IO及状态
        public static bool WaitIo(string DI, string erro, bool bTrue = true, long lTimeOut = AbsMotion.IO_TIMEOUT,
                                  string select = ",是否重新等待\r\n重试--继续等待\r\n取消--结束工作", 
                                  bool canIgnore = true, bool canAbort = false)
        {          
            if (DI == "")
            {
                //Thread.Sleep(100);
                return true;
            }

            //永久等待
            if (AbsMotion.THREAD_TIMEOUT == lTimeOut)
                return WaitIo(DI, bTrue, lTimeOut);

            DialogResult result;
            do
            {
                if (!WaitIo(DI, bTrue, lTimeOut, bException: false))
                {
                    StartAlarmThread();
                    Publics.sDisplay.DisplayTextBox_ShowAlram(DI+"等待超时");
                    select = " 超时,是否重新等待;重试--继续等待";
                    if (canIgnore)
                        select += "\r\n忽略--跳过等待";
                    if (canAbort)
                        select += "\r\n取消--结束工作";
                    result = ClassLib_MSG.MSG.Show(erro + select + " 超时,是否重新等待\r\n重试--继续等待\r\n取消--结束工作", "", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    CloseAlarm();
                    if (DialogResult.Retry == result)
                    {
                        continue;
                    }
                    else if (DialogResult.Ignore == result)
                    {
                        if (canIgnore)
                            return false;
                        else
                            continue;
                    }
                    else if (!canAbort)
                    {
                        continue;
                    }

                    Publics.SetStop();
                    Publics.sDisplay.DisplayText_Log(erro + "等待超时");
                    throw new Exception(erro + "等待超时");
                }
                else
                    break;
            } while (true);

            return true;
        }

        /// <summary>
        /// 等待IO cIoName状态被更改为bTrue
        /// </summary>
        /// <param name="cIoName">IO名称</param>
        /// <param name="bTrue">判断状态</param>
        /// <param name="lTimeOut">超时时间</param>
        /// <param name="DelayTime">扫描间隔</param>
        /// <param name="bException">是否显示异常处理窗口</param>
        /// <param name="bSoftStop">软停是否退出</param>
        /// <param name="canIgnore">可忽略，退出循环，返回false</param>
        /// <param name="canAbort">可中止，抛出异常</param>
        /// <returns></returns>
        public static bool WaitIo(string cIoName, bool bTrue = true, long lTimeOut = AbsMotion.IO_TIMEOUT, long DelayTime = 50, 
                                  bool bException = true, bool bSoftStop = true, bool canIgnore = true, bool canAbort = false, int iSmoothTimes = 0)
        {
            //try
            //{
            CQueryTime qtNow = new CQueryTime();
            bool bNeedShow = true;
            //while (bTrue != GetIo(cIoName))
            while (!GetSmoothIo(cIoName, bTrue,iSmoothTimes))
            {
                if (!Control_Thread.WaitOne(ref qtNow, ref bNeedShow, lTimeOut, cIoName + " IO状态", bException, DelayTime, bSoftStop, canIgnore: canIgnore, canAbort: canAbort))
                {
                    return false;
                }
                //else
                //{
                //    if (Control_Thread.WaitOneResult == 1)
                //    {
                //        Control_Thread.WaitOneResult = 0;
                //        return true;
                //    }
                //}
                   
                //if (qtNow.Now() > lTimeOut || GetStop(bSoftStop))
                //{
                //    if (bException)  //通常都需要为true
                //    {//通常退出自动流程以后需要抛出异常，但是皮带线程等需要继续运行
                //        if (GetStop(bSoftStop))
                //        {
                //            throw new Exception();
                //        }
                //        else
                //        {
                //            result = ClassLib_MSG.MSG.Show(cIoName + " IO状态报警！超时", "", MessageBoxButtons.RetryCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                //            if (DialogResult.Retry == result || DialogResult.Abort == result)
                //            {
                //                continue;
                //            }
                //            SetSoftStop();
                //            throw new Exception(cIoName + " IO状态报警！超时");
                //        }
                //    }
                //    return false;
                //}

                //if (qtNow.Now() > THREAD_WARNING && bNeedShow)
                //{
                //    bNeedShow = false;
                //    Publics.sDisplay.DisplayText_Log(cIoName + "_IO等待状态的时间超过" + (THREAD_WARNING / 1000).ToString() + "秒");
                //}
            }
            //}
            //catch
            //{
            //    return false;
            //}
            return true;
        }

        /// <summary>
        /// 等待IO集 cIoName状态被更改为bTrue
        /// </summary>
        /// <param name="cIoName"></param>
        /// <param name="bTrue"></param>
        /// <param name="lTimeOut"></param>
        /// <param name="DelayTime"></param>
        /// <param name="bException"></param>
        /// <param name="bSoftStop"></param>
        /// <param name="canIgnore"></param>
        /// <param name="canAbort"></param>
        /// <returns></returns>
        public static bool WaitIos(string[] cIoName, bool bTrue = true, long lTimeOut = AbsMotion.IO_TIMEOUT, long DelayTime = 50,
                                  bool bException = true, bool bSoftStop = true, bool canIgnore = false, bool canAbort = false)
        {
            CQueryTime qtNow = new CQueryTime();
            bool bNeedShow = true;
            while (bTrue != GetIos(cIoName))
            {
                if (!Control_Thread.WaitOne(ref qtNow, ref bNeedShow, lTimeOut, cIoName + " IO状态", bException, DelayTime, bSoftStop, canIgnore: canIgnore, canAbort: canAbort))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 等待bool变量sts状态被更改为bTrue，sTimeOutLog作为超时报警显示，需要在调用时被修改
        /// </summary>
        /// <param name="sts"></param>
        /// <param name="bTrue"></param>
        /// <param name="lTimeOut"></param>
        /// <param name="DelayTime"></param>
        /// <param name="bException"></param>
        /// <param name="bSoftStop"></param>
        /// <param name="sTimeOutLog"></param>
        /// <param name="canIgnore"></param>
        /// <param name="canAbort"></param>
        /// <returns></returns>
        public static bool WaitSts(ref bool sts, bool bTrue = true, long lTimeOut = AbsMotion.THREAD_TIMEOUT, long DelayTime = 50,
                                   bool bException = true, bool bSoftStop = true, string sTimeOutLog = "bool状态", bool canIgnore = false, bool canAbort = false)
        {
            CQueryTime qtNow = new CQueryTime();
            bool bNeedShow = true;
            while (bTrue != sts)
            {
                if (!Control_Thread.WaitOne(ref qtNow, ref bNeedShow, lTimeOut, sTimeOutLog, bException, DelayTime, bSoftStop, canIgnore: canIgnore, canAbort: canAbort))
                {
                    return false;
                }
                //CQueryTime.SystemDelayMs(DelayTime);
                //Process_Pause(ref qtNow);
                //if (qtNow.Now() > lTimeOut || GetStop(bSoftStop))
                //{
                //    if (bException)
                //    {
                //        if (qtNow.Now() > lTimeOut)
                //        {
                //            SetSoftStop();
                //            throw new Exception("等待状态超时报警！");
                //        }
                //        else
                //            throw new Exception();
                //    }
                //    return false;
                //}
                //if (qtNow.Now() > THREAD_WARNING && bNeedShow)
                //{
                //    bNeedShow = false;
                //    Publics.sDisplay.DisplayText_Log("sts等待状态的时间超过" + (THREAD_WARNING / 1000).ToString() + "秒");
                //}
            }
            return true;
        }

        /// <summary>
        /// 等待int变量sts状态被更改为iNeed，sTimeOutLog作为超时报警显示，需要在调用时被修改
        /// </summary>
        /// <param name="sts"></param>
        /// <param name="iNeed"></param>
        /// <param name="lTimeOut"></param>
        /// <param name="DelayTime"></param>
        /// <param name="bException"></param>
        /// <param name="bSoftStop"></param>
        /// <param name="sTimeOutLog"></param>
        /// <param name="canIgnore"></param>
        /// <param name="canAbort"></param>
        /// <returns></returns>
        public static bool WaitSts(ref int sts, int iNeed = 0, long lTimeOut = AbsMotion.THREAD_TIMEOUT, long DelayTime = 50,
                                   bool bException = true, bool bSoftStop = true, string sTimeOutLog = "int状态", bool canIgnore = false, bool canAbort = false)
        {
            CQueryTime qtNow = new CQueryTime();
            bool bNeedShow = true;
            while (iNeed != sts)
            {
                if (!Control_Thread.WaitOne(ref qtNow, ref bNeedShow, lTimeOut, sTimeOutLog, bException, DelayTime, bSoftStop, canIgnore: canIgnore, canAbort: canAbort))
                {
                    return false;
                }

                //CQueryTime.SystemDelayMs(DelayTime);
                //Process_Pause(ref qtNow);
                //if (qtNow.Now() > lTimeOut || GetStop(bSoftStop))
                //{
                //    if (bException)
                //    {
                //        if (qtNow.Now() > lTimeOut)
                //        {
                //            SetSoftStop();
                //            throw new Exception("等待状态超时报警！");
                //        }
                //        else
                //            throw new Exception();
                //    }
                //    return false;
                //}
                //if (qtNow.Now() > THREAD_WARNING && bNeedShow)
                //{
                //    bNeedShow = false; 
                //    Publics.sDisplay.DisplayText_Log("整型sts等待状态的时间超过" + (THREAD_WARNING / 1000).ToString() + "秒");
                //}
            }
            return true;
        }

        /// <summary>
        /// 等待object变量sts状态被更改为oNeed，sTimeOutLog作为超时报警显示，需要在调用时被修改
        /// </summary>
        /// <param name="sts"></param>
        /// <param name="oNeed"></param>
        /// <param name="lTimeOut"></param>
        /// <param name="DelayTime"></param>
        /// <param name="bException"></param>
        /// <param name="bSoftStop"></param>
        /// <param name="sTimeOutLog"></param>
        /// <param name="canIgnore"></param>
        /// <param name="canAbort"></param>
        /// <returns></returns>
        public static bool WaitSts(ref object sts, object oNeed, long lTimeOut = AbsMotion.THREAD_TIMEOUT, long DelayTime = 50,
                                   bool bException = true, bool bSoftStop = true, string sTimeOutLog = "object状态", bool canIgnore = false, bool canAbort = false)
        {
            CQueryTime qtNow = new CQueryTime();
            bool bNeedShow = true;
            while (oNeed != sts)
            {
                if (!Control_Thread.WaitOne(ref qtNow, ref bNeedShow, lTimeOut, sTimeOutLog, bException, DelayTime, bSoftStop, canIgnore: canIgnore, canAbort: canAbort))
                {
                    return false;
                }

                //CQueryTime.SystemDelayMs(DelayTime);
                //Process_Pause(ref qtNow);
                //if (qtNow.Now() > lTimeOut || GetStop(bSoftStop))
                //{
                //    if (bException)
                //    {
                //        if (qtNow.Now() > lTimeOut)
                //        {
                //            SetSoftStop();
                //            throw new Exception("等待状态超时报警！");
                //        }
                //        else
                //            throw new Exception();
                //    }
                //    return false;
                //}
                //if (qtNow.Now() > THREAD_WARNING && bNeedShow)
                //{
                //    bNeedShow = false;
                //    Publics.sDisplay.DisplayText_Log("object等待状态的时间超过" + (THREAD_WARNING / 1000).ToString() + "秒");
                //}
            }
            return true;
        }

        public delegate bool CallBackWaitSts(object sender);
        /// <summary>
        /// 等待回调函数StsFun（参数sender）的返回值为bTrue，sTimeOutLog作为超时报警显示，需要在调用时被修改
        /// </summary>
        /// <param name="StsFun"></param>
        /// <param name="sender"></param>
        /// <param name="bTrue"></param>
        /// <param name="lTimeOut"></param>
        /// <param name="DelayTime"></param>
        /// <param name="bException"></param>
        /// <param name="bSoftStop"></param>
        /// <param name="sTimeOutLog"></param>
        /// <param name="canIgnore"></param>
        /// <param name="canAbort"></param>
        /// <returns></returns>
        public static bool WaitSts(CallBackWaitSts StsFun, object sender, bool bTrue = true, long lTimeOut = AbsMotion.THREAD_TIMEOUT, long DelayTime = 50,
                                   bool bException = true, bool bSoftStop = true, string sTimeOutLog = "回调函数状态", bool canIgnore = false, bool canAbort = false)
        {
            CQueryTime qtNow = new CQueryTime();
            bool bNeedShow = true;
            while (bTrue != StsFun(sender))
            {
                if (!Control_Thread.WaitOne(ref qtNow, ref bNeedShow, lTimeOut, sTimeOutLog, bException, DelayTime, bSoftStop, canIgnore: canIgnore, canAbort: canAbort))
                {
                    return false;
                }

                //CQueryTime.SystemDelayMs(DelayTime);
                //Process_Pause(ref qtNow);
                //if (qtNow.Now() > lTimeOut || GetStop(bSoftStop))
                //{
                //    if (bException)
                //    {
                //        if (qtNow.Now() > lTimeOut)
                //        {
                //            SetSoftStop();
                //            throw new Exception("等待状态超时报警！");
                //        }
                //        else
                //            throw new Exception();
                //    }
                //    return false;
                //}
                //if (qtNow.Now() > THREAD_WARNING && bNeedShow)
                //{
                //    bNeedShow = false;
                //    Publics.sDisplay.DisplayText_Log("回调函数等待状态的时间超过" + (THREAD_WARNING / 1000).ToString() + "秒");           
                //}
            }
            return true;
        }
        #endregion
    }
}

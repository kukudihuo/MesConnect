﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using ClassLib_ParaFile;
using ClassLib_MSG;
using ClassLib_RunMode;

namespace ClassLib_Motion
{
    //规划位置速度加速度
    public struct TPrfPrm
    {
        public double pos; //实时更新，用于读,写无效
        public double vel; //实时更新，用于读,写无效
        public double acc; //实时更新，用于读,写无效
        public int mode;   //运动模式：点位运动，jog运动，插补运动等
    }
    //目标位置速度加速度
    public struct TDesPrm
    {
        public long pos;
        public double vel;
        public double acc;
        public int pos_flex;
        public int vel_low;
    }
    //编码位置速度
    public struct TEnsPrm
    {
        public double pos;
        public double vel;
        public double pospre;
    }
    //正负限位
    public enum LimitPN
    {
        ALL = -1,
        MC_LIMIT_POSITIVE = 0,
        MC_LIMIT_NEGATIVE = 1
    }

    /// <summary>
    /// 管理Card, Axis, Io
    /// </summary>
    abstract public class AbsMotion
    {        
        public AbsMotion()
        {
            //Motion.InitMotion();
        }
        ~AbsMotion()
        {
            //Motion.Dispose();
        }

        virtual public bool InitMotion()
        {
            return false;
        }
#region 虚拟函数需要重写
        public abstract AbsCard CreateCard(int iCard, string IniFileName);
        public abstract AbsIo CreateIo(string str);
        public abstract AbsAxis CreateAxis(short virtualIndex, short index, string name, double pitch, double divide);
#endregion
    }

    /// <summary>
    /// 卡信息父类
    /// </summary>
    abstract public class AbsCard
    {
        protected short _cardNum;
        public bool m_bCardOK = false;

        public AbsCard()
        { }
        ~AbsCard()
        {
            Dispose();
        }
        public void Dispose() { }

        //虚拟函数需要重写
        public abstract short OpenCard();
        public abstract short CloseCard();
        public abstract short ClrStsAll();
    }

    /// <summary>
    /// 轴信息父类
    /// </summary>
    abstract public class AbsAxis
    {
        protected short _cardNum; //卡号
        protected short _axis;    //卡上序号
        protected double _Pitch = 40;  //导程
        protected double _Divide = 40000; //齿轮比/细分
        protected double _Vel = 100;
        protected double _Acc = 1000;
        //protected Thread m_Thread = null; //线程

        public bool m_IsServo = true; //伺服或步进
        public bool m_bPauseBySenson = false;
        public bool m_bPauseByDoor = true;
        public bool m_bSetAutoVel = true;
        public bool m_bStopWhenPause = true;

        void ThreadExecute()
        { }
        public string m_sName = "";//轴名称
        public short m_index;      //物理轴号
        public short m_VirtualIndex;//虚拟轴号


        public bool m_bAbsPos = true;

        private bool _bDebugStop = true;
        public int m_dLoopTimes = 1;
        public int m_Sts = 0; //C#无成员初始化列表，可直接在声明时初始化
        //public long m_pos = 0;//保存最近一次的目标位置
        public long m_OrgPos = 0;
        public double m_SafePosMM = 0;

        private bool s_bFirstGohome = true;
        private static bool s_bMessageBoxVisibled = false;

        public TDesPrm m_DesPrm;   //目标，写  实时的目标位置与速度
        public TPrfPrm m_PrfPrm;   //读 获取实时的规划位置与速度
        public TEnsPrm m_EncPrm;   //编码，读 获取实时的编码位置

        public mc.TJogPrm m_JogPrm;
        public mc.TTrapPrm m_TrapPrm; //点位写 实时的规划位置

        public ResetAxis m_ResetAxis;
        private AbsAxis()
        { }
        protected AbsAxis(short virtualIndex, short index, string name, double pitch, double divide)
        {
            m_VirtualIndex = virtualIndex;
            m_index = index;
            m_sName = name;
            m_DesPrm.pos = 0;
            m_DesPrm.vel = 0;
            m_DesPrm.pos = 0;
            _Pitch = pitch;
            _Divide = divide;

            _cardNum = (short)((index - 1) / Motion.AXIS_IN_CARD);
            _axis = (short)((index - 1) % Motion.AXIS_IN_CARD + 1);

            //m_Thread = new Thread(new ThreadStart(ThreadExecute)); //线程
            //myThread = new TAxisThread(true, "", this);//线程

            AxisOn(false);

            m_ResetAxis = new ResetAxis(this);
        }
        ~AbsAxis()
        {
            Dispose();
        }
        public void Dispose() { }

        #region 设置读取当前速度加速度
        public void ChangeVel(double vel)
        {
            _Vel = vel;
        }
        public void ChangeAcc(double acc)
        {
            _Acc = acc;
        }
        public double ReadVel()
        {
            return _Vel;
        }
        public double ReadAcc()
        {
            return _Acc;
        }
        public short GetPrf(short count = 1)
        {//获取规划数据
            GetPrfPos(count);
            //GetPrfVel(count);
            //GetPrfAcc(count);
            //GetPrfMode(count); //暂时不需要 20180915
            return 0;
        }
        #endregion

        #region 轴往返运动调试
        public short DoDebug()
        {
            //double vel = MainFrm->EdtVel->Text.Trim().ToDouble();
            //double pos = MainFrm->EdtPos->Text.Trim().ToDouble();
            ////mc.TTrapPrm trapPrm = new mc.TTrapPrm();
            //mc.TTrapPrm trapPrm;
            //trapPrm.acc = MainFrm->EdtAcc->Text.Trim().ToDouble();
            //trapPrm.dec = MainFrm->EdtDec->Text.Trim().ToDouble();
            //trapPrm.velStart = 0;
            //trapPrm.smoothTime = MainFrm->EdtSmoothTime->Text.Trim().ToDouble();
            //double dLoopTimes = MainFrm->EdtLoopTimes->Text.Trim().ToDouble();
            //double dArriveDelay = MainFrm->EdtArriveDelay->Text.Trim().ToDouble();

            //运行前需要设置m_tmpDesPrm与m_tmpTrapPrm
            if (0 == m_DesPrm.vel)
                return -1;

            GetEncPos();
            _bDebugStop = false;
            int i = 0;
            do
            {
                int iSigned = Convert.ToBoolean(i % 2) ? -1 : 1;
                MoveRelativePulse((iSigned * m_DesPrm.pos), m_DesPrm.vel, m_DesPrm.acc, true);
                i++;
                //m_PrfPrm;   //读
                //m_DesPrm;   //目标，写
                //m_EncPrm;   //编码，读
                //Thread.Sleep(dArriveDelay);
            } while (i <= m_dLoopTimes && !_bDebugStop && !Run.GetStop());
            return 0;
        }
        public void StopDoDebug()
        {
            _bDebugStop = true;
        }
        public void Execute() { }
        #endregion

        #region 脉冲与mm的转换
        public double PulseToMm(double pulse)
        {//脉冲转距离
            try
            {
                return (pulse * _Pitch) / _Divide;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return 0;
            }
            //double size = pos;
            //if (AxisType.HOME_AXIS == m_iGoHomeMode)
            //{//W
            //    size = (pos * 360.0) / Motion.ROUND_PULSE;
            //}
            //else if (AxisType.FADER_AXIS == m_iGoHomeMode)
            //{//F 1脉冲等于8um 1mm等于125脉冲
            //    size = pos * 8;
            //}
            //else if (AxisType.DOUBLE_LIMIT_AXIS == m_iGoHomeMode)   //Z
            //    size = pos / 700.0;
            //else  //X Y
            //    size = pos / 1000.0;
            //return size;
        }
        public long MmToPulse(double mm)
        {//距离转脉冲
            try
            {
                return Convert.ToInt64((mm * _Divide) / _Pitch);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return 0;
            }
            //double pos = size;
            //if (AxisType.HOME_AXIS == m_iGoHomeMode)
            //{
            //    pos = (size * Motion.ROUND_PULSE) / 360.0;
            //}
            //else if (AxisType.FADER_AXIS == m_iGoHomeMode)
            //{//1脉冲等于8um 1mm等于125脉冲
            //    pos = size / 8;
            //}
            //else if (AxisType.DOUBLE_LIMIT_AXIS == m_iGoHomeMode)   //Z
            //    pos = size * 700.0;
            //else  //X Y
            //    pos = size * 1000.0;
            //return pos;
        }
        public double GetEncPosMm()
        {
            return PulseToMm(GetEncPos());
        }
        public double GetPrfPosMm()
        {
            return PulseToMm(GetPrfPos());
        }
        public double GetDesPosMm()
        {
            return PulseToMm(m_DesPrm.pos);
        }
        #endregion

        #region 绝对点位运动与相对点位运动及JOG运动
        public bool MoveAbsolute(double pos, double vel, double acc = Motion.ConstDebugAcc, bool bWaitArrive = false, short smoothTime = 25, double pos_flex = 0, double vel_low = 0)
        {
            pos = MmToPulse(pos);
            vel = VelToPulse(vel);
            acc = AccToPulse(acc, vel);
            if (pos_flex != 0 && vel_low != 0)
            {
                pos_flex = MmToPulse(pos_flex);
                vel_low = VelToPulse(vel_low);
            }
            return MoveAbsolutePulse(pos, vel, acc, bWaitArrive, smoothTime, pos_flex, vel_low);
        }
        public bool MoveRelative(double pos, double vel, double acc = Motion.ConstDebugAcc, bool bWaitArrive = false, bool bEncPos = false, short smoothTime = 25)
        {
            if (bEncPos)
                return MoveAbsolute(GetEncPosMm() + pos, vel, acc, bWaitArrive, smoothTime);
            else
                return MoveAbsolute(GetPrfPosMm() + pos, vel, acc, bWaitArrive, smoothTime);
        }
        public bool MoveAbsolute(double pos, bool bWaitArrive = false, short smoothTime = 25, double pos_flex = 0, double vel_low = 0)
        {
            return MoveAbsolute(pos, _Vel, _Acc, bWaitArrive, smoothTime, pos_flex, vel_low);
        }
        public bool MoveRelative(double pos, bool bWaitArrive = false, bool bEncPos = false, short smoothTime = 25)
        {
            return MoveRelative(pos, _Vel, _Acc, bWaitArrive, bEncPos, smoothTime);
        }
        private bool MoveRelativePulse(double pos, double vel, double acc, bool bWaitArrive = false, bool bEncPos = false, short smoothTime = 25)
        {
            if (bEncPos)
                return MoveAbsolutePulse(GetEncPos() + pos, vel, acc, bWaitArrive, smoothTime);
            else
                return MoveAbsolutePulse(GetPrfPos() + pos, vel, acc, bWaitArrive, smoothTime);
        }
        protected virtual bool PerMoveSetPara(ref double pos, ref double vel, ref double acc)
        {
            if (Run.GetStop())
            {
                if (Run.GetRunning())
                    throw new Exception(m_sName + "轴准备运行时报警！");//m_index.ToString()
                else
                    MSG.Show(m_sName + "轴准备运行时报警！");
            }
            if (Run.GetRunning())
            {
                //自动状态下准备运动时先判断是否暂定，如果暂停则循环等待
                CQueryTime qtNow = new CQueryTime();
                if (!Run.Process_Pause(ref qtNow, true))
                    return false;

                if (Math.Abs(Motion.AutoSpeedPer) > 1)
                    Motion.AutoSpeedPer = 1;
                else if (Motion.AutoSpeedPer == 0)
                    Motion.AutoSpeedPer = 0.1;
                if (m_bSetAutoVel)
                {
                    vel = Motion.AutoSpeedPer * vel;
                    acc = acc / Motion.AutoSpeedPer;
                }
            }
            else if (!Run.GetHoming())// if (Motion.GetSingling())
            {
                if (Math.Abs(Motion.DebugSpeedPer) > 1)
                    Motion.DebugSpeedPer = 1;
                else if (Motion.DebugSpeedPer == 0)
                    Motion.DebugSpeedPer = 0.1;
                if (m_bSetAutoVel)
                {
                    vel = Motion.DebugSpeedPer * vel;
                    acc = acc / Motion.DebugSpeedPer;
                }
            }
            else
            {
                if (vel > VelToPulse(100))
                    vel = VelToPulse(100);
                else if (vel < -VelToPulse(100))
                    vel = -VelToPulse(100);

                if (acc > 0 && acc < AccToPulse(500, vel))
                    acc = AccToPulse(500, vel);
                else if (acc < 0 && acc > -AccToPulse(500, vel))
                    acc = -AccToPulse(500, vel);
            }
            return true;
        }
        protected bool MoveAbsolutePulse(double pos, double vel, double acc, bool bWaitArrive = false, short smoothTime = 25, double pos_flex = 0, double vel_low = 0)
        {
            PerMoveSetPara(ref pos, ref vel, ref acc);

            short sRtn;
            //清除异常
            sRtn = ClrSts();
            // 伺服使能
            sRtn += AxisOn();

            //设为点位模式并设置运动参数
            SetTrapPara(acc, smoothTime, vel);

            if (pos_flex != 0 && vel_low != 0)
            {
                double acc_low = 0;
                PerMoveSetPara(ref pos_flex, ref vel_low, ref acc_low);
                m_DesPrm.pos_flex = (int)pos_flex;
                m_DesPrm.vel_low = (int)vel_low;
                m_PrfPrm.mode = 9;
            }
            // 设置AXIS轴的目标速度
            sRtn = SetVel(vel);
            // 设置AXIS轴的目标位置
            sRtn = SetPos(Convert.ToInt64(pos));
            //if (!WaitSafe384()) //运动前判断轴冲突，在这里取消，要放到流程中需要运动前执行
            //    throw new Exception(m_index.ToString() + "轴运行位置冲突！");
            // 启动AXIS轴的运动
            sRtn = Update();
            //m_DesPrm.acc = acc;

            if (bWaitArrive)
                WaitArrive();
            return true;
        }
        public bool MoveJog(double vel = 0, double acc = Motion.ConstDebugAcc, bool bWaitStop = false, double smooth = 0.9, bool bAnysStop = false)
        {
            if (Run.GetStop())
            {
                return false;
            }

            if (vel == 0)
            {
                vel = _Vel;
                acc = _Acc;
            }
            vel = VelToPulse(vel);
            acc = AccToPulse(acc, vel);

            short sRtn;
            //清除异常
            sRtn = ClrSts();
            // 伺服使能
            sRtn += AxisOn();

            //设为jog模式并设置运动参数
            sRtn += SetJogPara(acc, smooth);

            //if (vel > 100)
            //    vel = 100;
            sRtn += SetVel(vel);
            GetEncPos();
            sRtn = Update();

            //m_DesPrm.acc = acc;

            if (bWaitStop)
                return WaitStop();
            if (bAnysStop)
                return AnysWaitStopJog();
            return true;
        }
        #endregion

        #region 等待点位运动到位
        //等待精准到位
        public bool WaitEncArrive(double offset = 0.02, long lTimeOut = Motion.THREAD_TIMEOUT, long DelayTime = 20)
        {
            return WaitArrive(lTimeOut, DelayTime, true, offset);
        }
        //等待预到位
        public bool WaitInplaceArrive(double offset = 5, long lTimeOut = Motion.THREAD_TIMEOUT, long DelayTime = 20)
        {
            return WaitArrive(lTimeOut, DelayTime, true, offset, false);
        }
        //等待避让到位
        public bool WaitToSafeDistance(double startPos, double distance, int iWaitDir = 0, long lTimeOut = Motion.THREAD_TIMEOUT, long DelayTime = 20)
        {
            return WaitArrive(lTimeOut, DelayTime, true, 0.1, false, startPos, distance, iWaitDir);
        }
        public bool WaitArrive(long lTimeOut = Motion.THREAD_TIMEOUT, long DelayTime = 20, bool bPrfArrive = false, double dOffset = 0.1, bool bWaitStop = true, double startPos = 0, double distance = 0, int iWaitDir = 0, bool bLimitWarnning = true)
        {
            CQueryTime qtNow = new CQueryTime();
            //bool bStopBySenso = false, bStoped = false;
            bool bShowStatus = true;
            bool bMoving = true;
            if (bPrfArrive)
                ;//MainFrm->Memo1->Lines->Add("等待轴"+String(m_index)+"精准到位!");
            do
            {
                // 读取AXIS轴的状态
                GetSts();
                // 读取AXIS轴的规划位置
                GetPrf();
                //double Pos = GetPos();
                GetEncPos();
                //GetEncVel();

                //处理异常
                if (Run.GetStop() || GetAlarm())
                {
                    Stop(true);
                    return false;
                }

                //处理暂停和恢复
                //bStopBySenso = StopAndContinueByDoor(ref bStoped);
                //if (bStopBySenso)
                //    qtNow.Start(); //Motion.Process_Pause(ref qtNow);
                //if (bWaitStop)
                //    bMoving = bStopBySenso || Moving(false);
                //if (bPrfArrive)
                //{
                //    bMoving = bStopBySenso || Math.Abs(m_EncPrm.pos - m_DesPrm.pos) > MmToPulse(dOffset);
                //    if (!Moving(false) && Math.Abs(m_EncPrm.pos - m_DesPrm.pos) > MmToPulse(dOffset))
                //        //MessageBox.Show("轴到位异常");
                //        bMoving = false;
                //}

                //处理超时
                if (0 != DelayTime && bMoving)
                    CQueryTime.SystemDelayMs(Math.Abs(DelayTime));

                //处理暂停和恢复
                bool bEmeStop = false;
                if (startPos == 0 || distance == 0)
                    bEmeStop = false; //bEmeStop = true;//暂时不启用急停及恢复防止Z轴撞机
                if (!StopAndContinueByDoor(ref qtNow, bEmeStop))
                    return false;
                bMoving = Moving(false);
                if (bPrfArrive)
                {
                    if (bWaitStop)
                        bMoving |= Math.Abs(m_EncPrm.pos - m_DesPrm.pos) > MmToPulse(dOffset);
                    else if (startPos == 0 || distance == 0)
                        bMoving = Math.Abs(m_EncPrm.pos - m_DesPrm.pos) > MmToPulse(dOffset);
                    else
                    {
                        if (iWaitDir == 0)//不区分方向
                            bMoving = Math.Abs(MmToPulse(startPos) - m_PrfPrm.pos) < MmToPulse(distance);
                        else if (iWaitDir < 0)
                            bMoving = MmToPulse(startPos) - m_PrfPrm.pos < MmToPulse(distance);
                        else
                            bMoving = m_PrfPrm.pos - MmToPulse(startPos) < MmToPulse(distance);
                    }
                    //bool rtn1 = GetStop();
                    //bool rtn2 = bMoving;
                    //bool rtn3 = GetLimit();
                    if (GetStop() && bMoving && GetLimit(false))
                    {
                        if (bLimitWarnning)
                        {
                            MSG.Show(m_sName + "轴到达限位已停止！");
                        }
                        else
                        {
                            Publics.sDisplay.DisplayText_Log(m_sName + "轴到达限位已停止！");
                            break;
                        }
                    }
                    //显示等待轴精准到位超时
                    if (qtNow.Now() > Motion.THREAD_WARNING && bShowStatus)
                    {
                        Publics.sDisplay.DisplayText_Log(m_sName + "轴精准到位超时" + (Motion.THREAD_WARNING / 1000).ToString() + "秒");

                        //if (bShowStatus && 6 == m_index)
                        //     if (6 == m_index)
                        //    {//容错
                        //          SetPos(m_pos);
                        //        Update();//需要验证已经在目标位置时Update的结果为!Moving()，否则会死循环
                        //      GetSts();
                        //   }
                        bShowStatus = false;
                    }
                }
                if (qtNow.Now() > lTimeOut)
                    return false;//超时1          

            } while (bMoving);
            SetPos(Convert.ToInt64(GetEncPos()));
            return true;
        }

        public bool StopAndContinueByDoor(ref CQueryTime watcher, bool bEmeStop)
        {//门开关及停止按键整机暂停
            if (Run.GetRunning())
            {
                if (m_DesPrm.pos > 500000 && m_bPauseBySenson)
                    bEmeStop = true;
                if (Run.GetPause(bPauseSenso: m_bPauseBySenson))
                {
                    if (!bEmeStop && m_bPauseBySenson && Run.GetPauseSensoStop())
                    {
                        return true;
                    }

                    if (Moving())
                    {
                        if (bEmeStop)
                            Stop(true);
                    }

                    while (true)
                    {
                        if (Run.GetRunning())
                        {
                            if (Run.GetStop())
                                return false;

                            GetSts();
                            if (Run.GetPause(bPauseSenso: m_bPauseBySenson))
                            {
                                if (!bEmeStop && m_bPauseBySenson && Run.GetPauseSensoStop())
                                {
                                    return true;
                                }
                                if (Moving())
                                {
                                    if (bEmeStop)
                                        Stop(true);
                                }
                            }
                            else
                            {
                                if (!Moving())
                                {
                                    if (bEmeStop)
                                    {
                                        SetPos(m_DesPrm.pos);
                                        Update();//需要验证已经在目标位置时Update的结果为!Moving(false)，否则会死循环
                                    }
                                    GetSts();
                                    watcher.Start();
                                    break;
                                }
                            }
                        }
                        else
                            break;

                        CQueryTime.SystemDelayMs(100);
                    }
                }
            }
            return true;
        }

        //public bool StopAndContinueByDoor(ref bool bStoped)
        //{//门开关及停止按键整机暂停
        //    //if (!m_bPauseByDoor && !m_bPauseBySenson)
        //    //    return false;

        //    bool bStopBySenso = false, bContinueBySenso = false;
        //    //bool bStop = !ydGetIo(sStop);
        //    //if (sDoor)
        //    //    bStop |= !ydGetIo(sDoor);
        //    //ydSetSuspendSenso(bStop);
        //    bStopBySenso = Motion.GetRunning() && Motion.GetPause(m_bPauseBySenson);
        //    bContinueBySenso = Motion.GetRunning() && !Motion.GetPause(m_bPauseBySenson) && !Moving(false);
        //    if (bStopBySenso)
        //    {
        //        if (Moving(false))
        //        {
        //            //Stop(true);
        //            bStoped = true;
        //        }
        //    }
        //    else if (bContinueBySenso && bStoped)
        //    {
        //        //SetPos(m_DesPrm.pos);
        //        //Update();//需要验证已经在目标位置时Update的结果为!Moving(false)，否则会死循环
        //        //GetSts();

        //        bStoped = false;
        //        bStopBySenso = false;
        //    }
        //    return bStopBySenso;
        //}
        protected bool WaitStop(double timeOut = 20)
        {
            Stopwatch watcher = new Stopwatch();
            watcher.Reset();
            watcher.Start();
            do
            {
                // 读取轴状态
                GetSts();
                // 读取规划位置
                GetPrfPos();
                // 读取编码器位置
                GetEncPos();
                if (watcher.ElapsedMilliseconds >= (timeOut * 1000))
                    return false;
                //处理暂停
                Run.Process_Pause(ref watcher, true);
                // 等待运动停止
            } while (Moving(false));

            watcher.Stop();
            return true;
        }
        public bool AnysWaitStopJog()
        {
            Thread waitStop = new Thread(new ThreadStart(AnysWaitStopJogFunction));
            waitStop.Start();
            return true;
        }
        public void AnysWaitStopJogFunction()
        {
            bool moving = true;
            do
            {
                if (Run.GetStop())
                {
                    moving = false;
                }
                if (Run.GetPause())
                {
                    //Stop();
                    //while (Motion.GetPause())
                    //{
                    //    Thread.Sleep(100);
                    //}
                    moving = false;
                }
                if (!Moving())
                {
                    moving = false;
                }
            } while (moving);
            Stop();
        }
        //需要做成外部函数或回调函数，以适用于不同机型
        public bool WaitSafe(long pos, ulong lTimeOut = Motion.THREAD_TIMEOUT, long DelayTime = 20) { return true; }
        public bool WaitSafe384(long lTimeOut = Motion.THREAD_TIMEOUT, long DelayTime = 20)
        {//384 用于运动前等待是否安全位置，运行中检测到非安全位置时急停
            //if (_axis != X1_384)
            //    return true;
            long pos2;
            double posEnc2;
            CQueryTime qtNow = new CQueryTime();
            bool bwhile = true;
            bool bStoped = false;
            bool bSafe = false;
            do
            {
                if (Run.GetStop())
                {
                    //Stop();
                    return false;
                }
                bSafe = Motion.GetIo("DXI9_1=收ABB干涉信号");
                pos2 = m_DesPrm.pos;//GetPos();
                posEnc2 = GetEncPos();

                bwhile = true;
                //if (pos2 < xSafePos && posEnc2 < xSafePos)
                //if (pos2 < g_pXywz1->m_PosSuckUp.x + 5000 || bSafe)
                {//往安全区域运行或有安全信号时
                    bwhile = false;
                }
                if (qtNow.Now() > lTimeOut)
                {
                    return false;
                }
                if (bwhile)
                {//等待安全信号后运行
                    long lSafePos = 0;//g_pXywz1->m_PosSuckUp.x + 5000 
                    if (GetEncPos() > lSafePos && Moving(true) && !bSafe)
                    {//在WaitArrive中以防万一相撞时急停 异常或流程错误时才会启动急停
                        Stop(true);
                        bStoped = true;
                        Run.SetSoftStop();
                        throw new Exception("X轴运行中机器人位置冲突急停！");
                    }
                    CQueryTime.SystemDelayMs(DelayTime);
                }
            } while (bwhile);
            return true;
        }
        #endregion

        #region 回零
        public void Reset()
        {
            m_ResetAxis.GoHome();
        }
        //private void GoHomeError()
        //{
        //    if (!s_bMessageBoxVisibled)
        //    {////防止线程访问冲突
        //        s_bMessageBoxVisibled = true;
        //        MessageBox.Show((m_index + "轴回原点捕获HOME出错，请检查软硬件！"), "严重错误！", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        //        s_bMessageBoxVisibled = false;
        //        //throw Exception("回原点出错！");
        //        Motion.g_bGoHomeOk = false;
        //    }
        //}

        //手动回原点
        //public bool GoHome(long search_pos = -900000, bool bIndex = false, double vel = Motion.ConstDebugVel, double acc = 0.25, long offset = 1000, long search_index = -21000, long index_off = 1000)
        //{
        //    if (AxisType.LOAD_AXIS == m_iGoHomeMode || AxisType.FADER_AXIS == m_iGoHomeMode)
        //        return true;//步进电机用相对位置不需要回原点

        //    short sRtn, capture;
        //    long status;
        //    int pos;

        //    ////static bool s_bFirstGohome = true;
        //    ////清除异常状态
        //    //sRtn = ClrSts();
        //    //// 驱动器使能
        //    //sRtn = AxisOn();
        //    //// 切换到点位运动模式
        //    //sRtn = PrfTrap();
        //    //// 读取点位模式运动参数
        //    //mc.TTrapPrm trapPrm;
        //    //sRtn = GetTrapPrm(out trapPrm);
        //    //trapPrm.acc = acc;
        //    //trapPrm.dec = acc;
        //    //// 临时设置点位模式运动参数
        //    //sRtn = SetTrapPrm(ref trapPrm);
        //    //// 临时设置点位模式目标速度，即回原点速度
        //    //sRtn = SetVel(vel);
        //    // 位置清零
        //    MoveJog(vel, acc, true);
        //    sRtn = ZeroPos();

        //    if (AxisType.LIMIT_AXIS == m_iGoHomeMode)
        //    {//Y轴回零采取单限位信号的方式
        //        return GoHomeByLimit();
        //    }
        //    else if (AxisType.DOUBLE_LIMIT_AXIS == m_iGoHomeMode)
        //    {//Z轴回零采取双限位信号的方式
        //        return ZGoHome(search_pos);
        //    }
        //    else if (AxisType.HOME_AXIS == m_iGoHomeMode)
        //    {//W轴回零采用原点信号方式
        //        bIndex = false;
        //        search_pos = Convert.ToInt32(-2.3 * _Divide);
        //        vel = _Divide / 1000.0;//1;//0.5;
        //        acc = _Divide / 20000.0;//0.1;
        //        offset = -(3 * Convert.ToInt64(_Divide)) / 360; //2.8125
        //        search_index = 1000;
        //        index_off = (3 * Convert.ToInt64(_Divide)) / 360;
        //        sRtn = SetVel(vel);
        //    }
        //    else if (AxisType.INDEX_AXIS == m_iGoHomeMode)
        //    {//X轴回零采取index方式
        //        //bIndex = true;
        //        bIndex = false;
        //        offset = -1000;
        //        index_off = -1000;
        //        //string strHome = "HOME"+m_toStr(_axis-1)+"_"+m_toStr(_cardNum);
        //        string strHome = "HOME" + (_axis - 1).ToString() + "_" + _cardNum.ToString();
        //        bool bHome = !Motion.GetIo(strHome);
        //        if (GetLimit(false, true))
        //        {//当前在HOME位置时需要先移出  当到负限位后需要移出
        //            // 运动到"捕获位置+偏移量"
        //            sRtn = SetPos(80000);
        //            // 在运动状态下更新目标位置
        //            sRtn = Update();
        //            do
        //            {
        //                bHome = !Motion.GetIo(strHome);
        //                if (bHome)
        //                    break;
        //            } while (Moving(true)); // 等待运动停止
        //        }
        //        //清除异常状态
        //        sRtn = ClrSts();
        //        if (bHome)
        //        {//当前在HOME位置时需要先移出
        //            // 运动到"捕获位置+偏移量"
        //            sRtn = SetPos(50000);
        //            // 在运动状态下更新目标位置
        //            sRtn = Update();
        //            do
        //            {
        //                //清除异常状态
        //                sRtn = ClrSts();
        //                // 读取轴状态
        //                GetSts();
        //                // 读取规划位置
        //                GetPrfPos();
        //                // 读取编码器位置
        //                GetEncPos();
        //                bHome = !Motion.GetIo(strHome);
        //                if (!bHome)
        //                {//退出HOME状态时延时后停止运动
        //                    //Stop();
        //                    Thread.Sleep(200);
        //                    break;
        //                }
        //                // 等待运动停止
        //            } while (Moving(false));
        //        }
        //    }
        //    else
        //        return false;

        //    // 设置点位模式目标位置，即原点搜索距离
        //    //清除异常状态
        //    sRtn = SetPos(search_pos);
        //    // 启动运动
        //    sRtn = Update();
        //    //修改为上升沿
        //    sRtn = SetCaptureSense(mc.CAPTURE_HOME, 1);
        //    // 启动Home捕获
        //    sRtn = SetCaptureMode(mc.CAPTURE_HOME);
        //    sRtn = ClrSts();
        //    do
        //    {
        //        // 读取轴状态
        //        GetSts();
        //        // 读取捕获状态
        //        sRtn = GetCaptureStatus(out capture, out pos);
        //        // 读取规划位置
        //        GetPrfPos();
        //        // 读取编码器位置
        //        GetEncPos();
        //        // 如果运动停止，返回出错信息
        //        if (!Moving(false))
        //        {
        //            if (GetLimit(false, true))
        //            {//当前在HOME位置时需要先移出
        //                // 运动到"捕获位置+偏移量"
        //                sRtn = SetPos(80000);
        //                // 在运动状态下更新目标位置
        //                sRtn = Update();
        //                do
        //                {
        //                } while (Moving(true)); // 等待运动停止
        //                //清除异常状态
        //                sRtn = ClrSts();
        //                sRtn = SetPos(search_pos);
        //                // 启动运动
        //                sRtn = Update();
        //                //修改为上升沿
        //                sRtn = SetCaptureSense(mc.CAPTURE_HOME, 1);
        //                // 启动Home捕获
        //                sRtn = SetCaptureMode(mc.CAPTURE_HOME);
        //                sRtn = ClrSts();
        //            }
        //            else if (AxisType.HOME_AXIS == m_iGoHomeMode && s_bFirstGohome)
        //            {//W轴回零失败重新再回零一次
        //                s_bFirstGohome = false;
        //                GoHome(search_pos);
        //                s_bFirstGohome = true;
        //                return true;
        //            }
        //            else
        //            {
        //                GoHomeError();
        //                return false;
        //            }
        //        }
        //        // 等待捕获触发
        //    } while (0 == capture);

        //    // 运动到"捕获位置+偏移量"
        //    sRtn = SetPos(pos + offset);
        //    //sRtn = SetPos(pos - offset);//折向为了慢速重新捕获
        //    vel = vel / 5;
        //    sRtn = SetVel(vel);
        //    // 在运动状态下更新目标位置
        //    sRtn = Update();
        //    WaitStop();
        //    // 检查是否到达"Home捕获位置+偏移量"
        //    //if( m_PrfPrm.pos != pos+offset )
        //    //{
        //    //    return 2;
        //    //}

        //    if (bIndex)
        //    {
        //        // 延时一段时间，等待电机停稳
        //        Thread.Sleep(200);
        //        bool bIndexOk = true;
        //        //修改为上升沿       
        //        sRtn = SetCaptureSense(mc.CAPTURE_HOME, 1);
        //        // 启动index捕获
        //        sRtn = SetCaptureMode(mc.CAPTURE_INDEX);
        //        //清除异常状态
        //        sRtn = ClrSts();
        //        // 设置当前位置+index搜索距离为目标位置
        //        sRtn = SetPos((long)(m_PrfPrm.pos + search_index));
        //        // 启动运动
        //        sRtn = Update();
        //        // 等待index捕获信号触发
        //        do
        //        {
        //            // 读取轴状态
        //            GetSts();
        //            // 读取捕获状态
        //            sRtn = GetCaptureStatus(out capture, out pos);
        //            // 读取规划位置
        //            GetPrfPos();
        //            // 读取编码器位置
        //            GetEncPos();
        //            // 电机已经停止，说明整个搜索过程中index信号一直没有触发
        //            if (!Moving(false))
        //            {
        //                bIndexOk = false;
        //                break;
        //                //return 1;
        //            }
        //            // 如果index信号已经触发，则退出循环，捕获位置已经在pos变量中保存
        //        } while (0 == capture);

        //        if (!bIndexOk)
        //        {
        //            Thread.Sleep(200);
        //            sRtn = ClrSts();
        //            // 设置当前位置+index搜索距离为目标位置
        //            sRtn = SetPos((long)(m_PrfPrm.pos - 2 * search_index));
        //            // 启动运动
        //            sRtn = Update();
        //            // 等待index捕获信号触发
        //            do
        //            {
        //                // 读取轴状态
        //                GetSts();
        //                // 读取捕获状态
        //                sRtn = GetCaptureStatus(out capture, out pos);
        //                // 读取规划位置
        //                GetPrfPos();
        //                // 读取编码器位置
        //                GetEncPos();
        //                // 电机已经停止，说明整个搜索过程中index信号一直没有触发
        //                if (!Moving(false))
        //                {
        //                    bIndexOk = false;
        //                    GoHomeError();
        //                    break;
        //                    //return 1;
        //                }
        //                // 如果index信号已经触发，则退出循环，捕获位置已经在pos变量中保存
        //            } while (0 == capture);
        //            if (capture > 0)
        //                bIndexOk = true;
        //        }
        //        if (bIndexOk)
        //        {
        //            // 设置捕获位置+index偏移量为目标位置
        //            sRtn = SetPos(pos + index_off);
        //            // 启动运动
        //            sRtn = Update();
        //            WaitStop();

        //            //if( m_PrfPrm.pos != pos+ index_off)
        //            //{
        //            //    return 2;
        //            //}
        //            // home+index捕获完毕
        //        }
        //    }
        //    else
        //    {
        //        if (vel > 1)
        //            vel = 1;
        //        sRtn = SetVel(vel);
        //        // 设置点位模式目标位置，即原点搜索距离
        //        sRtn = SetPos(Convert.ToInt32(m_PrfPrm.pos + -10 * offset));
        //        // 启动运动
        //        sRtn = Update();
        //        //修改为下降沿
        //        sRtn = SetCaptureSense(mc.CAPTURE_HOME, 0);
        //        // 启动Home捕获
        //        sRtn = SetCaptureMode(mc.CAPTURE_HOME);
        //        //清除异常状态
        //        sRtn = ClrSts();
        //        do
        //        {
        //            // 读取轴状态
        //            GetSts();
        //            // 读取捕获状态
        //            sRtn = GetCaptureStatus(out capture, out pos);
        //            // 读取规划位置
        //            GetPrfPos();
        //            // 读取编码器位置
        //            GetEncPos();
        //            // 如果运动停止，返回出错信息
        //            if (!Moving(false))
        //            {
        //                if (AxisType.HOME_AXIS == m_iGoHomeMode && s_bFirstGohome)
        //                {//W轴回零失败重新再回零一次
        //                    s_bFirstGohome = false;
        //                    GoHome(search_pos);
        //                    s_bFirstGohome = true;
        //                    return true;
        //                }
        //                else
        //                {
        //                    GoHomeError();
        //                }
        //                return false;
        //            }
        //            // 等待捕获触发
        //        } while (0 == capture);

        //        // 运动到"捕获位置+偏移量"
        //        sRtn = SetPos(pos - offset / 2);
        //        // 在运动状态下更新目标位置
        //        sRtn = Update();
        //        WaitStop();
        //        // 检查是否到达"Home捕获位置+偏移量"
        //        //if( m_PrfPrm.pos != pos+offset )
        //        //{
        //        //    return 2;
        //        //}
        //    }

        //    Thread.Sleep(200);
        //    // 位置清零
        //    sRtn = ZeroPos();
        //    // 读取规划位置
        //    GetPrfPos();
        //    // 读取编码器位置
        //    GetEncPos();
        //    return true;
        //}

        public bool ZGoHome(double vel = Motion.ConstDebugVel, double acc = 1000, double velLow = Motion.ConstDebugVel / 100, double accLow = 10, double offsetZero = 0, int timeOut = 10)
        {
            double offset = 1;
            short sRtn = LmtsOff();
            // 位置清零
            sRtn = ZeroPos();

            double lPos = 0, lPos1 = 0;
            String strLmtP = "LIMIT" + (_axis - 1).ToString() + "+_" + _cardNum.ToString();
            String strLmtN = "LIMIT" + (_axis - 1).ToString() + "-_" + _cardNum.ToString();
            bool bLmtP = !Motion.GetIo(strLmtP);
            bool bLmtN = !Motion.GetIo(strLmtN);
            //bool bLmtPN = false;

            //1 非同时感应到限位，先回到平衡位置
            if (!((bLmtP) && (bLmtN)))
            {
                double tmpVel = -Math.Abs(vel);
                if (bLmtP)//感应到正限位时往正方向运行
                    tmpVel = Math.Abs(vel);
                // 启动运动
                MoveJog(tmpVel, acc);

                Stopwatch watcher = new Stopwatch();
                watcher.Reset();
                watcher.Start();

                do
                {
                    bLmtP = !Motion.GetIo(strLmtP);
                    bLmtN = !Motion.GetIo(strLmtN);
                    if ((bLmtP) && (bLmtN)) //同时感应到，已经回到平衡位置
                        break;
                    if (watcher.ElapsedMilliseconds >= (timeOut * 1000))
                        return false;
                    //处理暂停
                    Run.Process_Pause(ref watcher, true);
                } while (Moving(true));
                watcher.Stop();
                Stop();
            }

            //2 从平衡位置错开+offset
            if ((bLmtP) && (bLmtN))
            {
                //bLmtPN = true;
                //正方向启动运动
                MoveJog(Math.Abs(vel), acc);
                do
                {
                    // 读取轴状态
                    GetSts();
                    // 读取规划位置
                    GetPrfPos();
                    // 读取编码器位置
                    GetEncPos();
                    bLmtP = !Motion.GetIo(strLmtP);
                    if (!bLmtP)
                    {//第一次脱离正限位继续运行offset距离
                        Stop();
                        MoveRelative(Math.Abs(offset), vel, acc, true);//等待到位
                        break;
                    }
                } while (Moving(false));
            }
            else
            {
                //GoHomeError();
                return false;
            }

            //3 开始精准定位         
            //bLmtP = !Motion.GetIo(strLmtP);
            //bLmtN = !Motion.GetIo(strLmtN);
            //if (bLmtN)
            {
                sRtn = ZeroPos();
                //负向慢速运动直到正限位，记录下位置    
                MoveRelative(-Math.Abs(10 * offset), velLow, accLow, bEncPos: false);
                //MoveJog(-Math.Abs(velLow));

                int iStep = 0;
                do
                {
                    // 读取轴状态
                    GetSts();
                    //读取规划位置
                    GetPrfPos();
                    // 读取编码器位置
                    GetEncPos();

                    if (iStep >= 0)
                    {
                        bLmtP = !Motion.GetIo(strLmtP);
                        bLmtN = !Motion.GetIo(strLmtN);
                        if (0 == iStep && bLmtP)
                        {//记录第一次慢速感应到正限位的位置1, 并快速运行到脱离负限位
                            iStep++;
                            lPos1 = Convert.ToInt32(m_EncPrm.pos);
                            MoveRelative(-Math.Abs(10 * offset), vel, acc, bEncPos: false);
                        }
                        else if (1 == iStep && !bLmtN)
                        {//脱离负限位后慢速往回运行+offset距离
                            iStep++;
                            MoveRelative(Math.Abs(offset), velLow, accLow, bEncPos: false);
                        }
                        else if (2 == iStep && bLmtN)
                        {//再次感应到负限位，此时记录下位置2，并计算出平均位置，再运行到平均位置
                            iStep = -1;
                            lPos = Convert.ToInt32((lPos1 + m_EncPrm.pos) / 2);
                            MoveAbsolute(PulseToMm(lPos) + offsetZero, vel, acc);
                        }
                    }
                } while (Moving(false));

                if (-1 != iStep)
                    return false;
            }
            //else
            //{
            //    return false;
            //}

            Thread.Sleep(500);
            // 位置清零
            sRtn = ZeroPos();
            // 读取规划位置
            GetPrfPos();
            // 读取编码器位置
            GetEncPos();
            return true;
        }

        public bool GoHomeZByLimit(double vel = Motion.ConstDebugVel, double acc = Motion.ConstDebugAcc, double offset = 5, double velLow = Motion.ConstDebugVel / 100, double accLow = 10, double offsetLow = 1, double offsetZero = 1, bool bRotation = false, bool bNegateResetDirection = false, int bTimeOut = 10)
        {
            int iOffsetDir = (bNegateResetDirection) ? -1 : 1;
            LmtsOn();  //enable吸头Z轴正负限位
            Thread.Sleep(5);
            //感应是否感应到负限位
            if (GetLimit(bNegateResetDirection, true))     //正在负限位
            {
                //1 往正限位方向快速走撞限位
                //if (!MoveJog(iOffsetDir * vel, acc, true))
                if (!MoveRelative(iOffsetDir * 100, vel, acc, true))
                {
                    MSG.Show("脱离限位运行超时,轴：" + m_sName);
                    LmtsOff();
                    return false;
                }
            }
            bool rtn = GoHomeByLimit(vel, acc, offset, velLow, accLow, offsetLow, offsetZero, bRotation, bNegateResetDirection, bTimeOut);
            LmtsOff();
            return rtn;
        }
        public bool GoHomeByLimit(double vel = Motion.ConstDebugVel, double acc = Motion.ConstDebugAcc, double offset = 5, double velLow = Motion.ConstDebugVel / 100, double accLow = 10, double offsetLow = 1, double offsetZero = 1, bool bRotation = false, bool bNegateResetDirection = false, int bTimeOut = 10)
        {
            int iOffsetDir = (bNegateResetDirection) ? -1 : 1;
            short sRtn = 0;
            ZeroPos();
            SetEncPos(0);
            try
            {
                if (bRotation)
                {//旋转轴
                    //int iTimes = 0;
                    //do
                    //{
                    //    LmtsOn(mc.MC_LIMIT_NEGATIVE);  //enable吸头R轴负限位 mc.MC_LIMIT_NEGATIVE
                    //    Thread.Sleep(5);
                    //    //感应是否感应到负限位，旋转80度
                    //    if (GetLimit(bNegateResetDirection, true))     //正在负限位
                    //    {
                    //        if (++iTimes > 5)
                    //            break;
                    //        LmtsOff(mc.MC_LIMIT_NEGATIVE);
                    //        //Thread.Sleep(10);
                    //        MoveRelative(-iOffsetDir * 80, vel, acc, true);
                    //    }
                    //    else
                    //        break;
                    //}while(true);
                    LmtsOn(LimitPN.MC_LIMIT_NEGATIVE);  //enable吸头R轴负限位 mc.MC_LIMIT_NEGATIVE
                    Thread.Sleep(5);
                    //感应是否感应到负限位（需要将HOME/负限位信号取反，脱离感应片时有信号），若脱离，正向旋转100度 
                    if (GetLimit(bNegateResetDirection, true))     //正在负限位
                    {
                        LmtsOff(LimitPN.MC_LIMIT_POSITIVE);//关闭正限位（恒昱只能正负都关闭）
                        //正方向旋转相对卡口度 等待停止
                        MoveRelative(iOffsetDir * 100, vel, acc, true);
                        LmtsOn(LimitPN.MC_LIMIT_NEGATIVE);//重新打开负限位
                    }
                }
                else
                {
                    //感应是否感应到负限位
                    int iTimes = 0;
                    do
                    {
                        if (GetLimit(bNegateResetDirection, true))     //正在负限位
                        {
                            //0 脱离负限位
                            //if (!MoveJog(iOffsetDir * vel, acc, true))
                            if (!MoveRelative(iOffsetDir * 5, vel, acc, true))
                            {
                                iTimes = 5;
                            }
                            iTimes++;
                        }
                        else
                            break;
                    } while (iTimes < 5);
                    if (iTimes >= 5)
                    {
                        MSG.Show("脱离限位超时,轴：" + m_sName);
                        return false;
                    }
                }
                //1 往负限位方向快速走撞限位
                if (!MoveJog(iOffsetDir * -vel, acc, true))
                //if (!MoveRelative(iOffsetDir * -9999, vel, acc, true))
                {
                    MSG.Show("第一次往限位方向运行超时,轴：" + m_sName);
                    return false;
                }
                //往回走距离1脱离感应片，然后重新用快速撞限位
                MoveRelative(iOffsetDir * offset, vel, acc, true);

                //2 重新用快速撞限位
                if (!MoveJog(iOffsetDir * -vel / 5, acc, true))
                //if (!MoveRelative(iOffsetDir * -9999, vel / 5, acc, true))
                {
                    MSG.Show("第二次往限位方向运行超时,轴：" + m_sName);
                    return false;
                }
                //往回走距离2脱离感应片，然后重新用慢速撞限位
                MoveRelative(iOffsetDir * offsetLow, vel, acc, true);

                //3 重新用慢速撞限位
                if (!MoveJog(iOffsetDir * -velLow, accLow, true))
                //if (!MoveRelative(iOffsetDir * -9999, velLow, accLow, true))
                {
                    MSG.Show("第三次往限位方向运行超时,轴：" + m_sName);
                    return false;
                }
                Thread.Sleep(200);
                if (offsetZero != 0)
                {//到限位后走偏移量作为零点
                    MoveRelative(iOffsetDir * offsetZero, vel, acc, true);
                    Thread.Sleep(1000);
                }

                //4 位置清零
                sRtn = ZeroPos();
                // 读取规划位置
                GetPrfPos();
                // 读取编码器位置
                GetEncPos();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (bRotation)
                {
                    LmtsOff(LimitPN.MC_LIMIT_NEGATIVE);  //取消吸头R轴负限位 mc.MC_LIMIT_NEGATIVE
                }
            }
            return true;
        }
        public virtual bool GoHomeByHome(double vel = Motion.ConstDebugVel, double acc = Motion.ConstDebugAcc, double offset = 5, double velLow = Motion.ConstDebugVel / 100, double accLow = 10, double offsetLow = 1, double offsetZero = 1, bool bRotation = false, bool bNegateResetDirection = false, int bTimeOut = 10)
        {
            return true;
        }
        public virtual bool GoHomeByIndex(double vel = Motion.ConstDebugVel, double acc = Motion.ConstDebugAcc, double offset = 5, double velLow = Motion.ConstDebugVel / 100, double accLow = 10, double offsetLow = 1, double offsetZero = 1, bool bRotation = false, bool bNegateResetDirection = false, int bTimeOut = 10)
        {
            return true;
        }
        
        #endregion

        #region 虚拟函数31个，需要重写
        public virtual double VelToPulse(double vel)
        {
            return MmToPulse(vel);// *0.001;    //mm/s 转换为pulse/ms 
        }
        public virtual double AccToPulse(double acc, double vel)
        {
            return Math.Abs(vel) / (MmToPulse(acc));
            // return (MmToPulse(acc) * 0.1) ; //mm/s2 转换为 pulse/ms2
        }
        //设为点位模式并设置运动参数
        abstract public short SetTrapPara(double acc, double smoothTime, double vel, bool bSave = true);
        //设为jog模式并设置运动参数
        abstract public short SetJogPara(double acc, double smooth, bool bSave = true);
        //是否正在运行中
        abstract public bool Moving(bool bGetSts = true);
        abstract public bool GetOnSts(bool bGetSts = false);
        abstract public bool GetAlarm(bool bGetSts = false);
        abstract public bool GetStop(bool bGetSts = false);
        abstract public bool GetLimit(bool bPlimit = true, bool bGetSts = false);

        //以下为固高卡功能的二次封装
        //状态
        abstract public int GetSts(short count = 1);
        abstract public short ClrSts(short count = 1);//多了个count参数，默认需要设为多少？

        //使能
        abstract public short AxisOn(bool bExp = true);
        abstract public short AxisOff();
        //限位开关
        abstract public short LmtsOn(LimitPN limitType = LimitPN.ALL);
        abstract public short LmtsOff(LimitPN limitType = LimitPN.ALL);
        //设置方向与脉冲
        abstract public short StepPulseDir(short pulse, short dir);

        //设置当前位置与速度
        abstract protected short SetPos(long pos, bool bSave = true);
        abstract protected short SetVel(double vel, bool bSave = true);
        abstract protected long GetPos(bool bSave = true);
        abstract protected double GetVel(bool bSave = true);

        //运动与停止
        abstract public short Update();
        abstract public short Stop(bool option = true);

        //规划位置与速度和模式
        abstract public short SetPrfPos(long prfPos);
        abstract public double GetPrfPos(short count = 1);
        abstract protected long GetPrfMode(short count = 1);

        //编码器位置与速度
        abstract public short SetEncPos(int encPos = 99999999);
        abstract public double GetEncPos(short count = 1);

        //位置清零
        abstract public short ZeroPos(short count = 1);

        //位置比较输出
        abstract public short SetCompareConfig(bool enable, short hcmp = 0);
        abstract public short ClearComparePoints(short hcmp = 0);
        abstract public short AddComparePoints(short hcmp, int pos, short dir = 0);
        #endregion
    }
    
    /// <summary>
    /// IO信息父类
    /// </summary>
    abstract public class AbsIo
    {
        //命名规则 EXI0_0 EXI1_0 EXI2_1 第一个数字是端口号，第二个数字是卡号
        public string m_sName = ""; //IO注释 默认即 _cMark
        protected ushort _Value;
        protected string _cMark; //IO标识 
        protected short _cardNum = 0;
        protected short _ioType;
        protected short _ioIndex = 0;
        protected bool _ioLevel = false;//unsigned short
        /// <summary>
        /// 是否为输出
        /// </summary>
        protected bool _bdo;
        protected short _mdl = -1; //扩展卡序号0-N，-1表示主卡
        public const short MC_GPI_ODL = 8;
        public const short MC_GPO_ODL = 10;

        public const short MC_GPI_MDL = 7;
        public const short MC_GPO_MDL = 9;
        public const short MC_GPI_MDL2 = 13;
        public const short MC_GPO_MDL2 = 14;
        public const short MC_GPI_MDL3 = 15;
        public const short MC_GPO_MDL3 = 16;
        public const short MC_GPI_MDL4 = 17;
        public const short MC_GPO_MDL4 = 18;
        protected static int[,] _ioValue = new int[Motion.CARD_TOTAL + Motion.EXT_CARD_TOTAL, MC_GPO_MDL4 + 1];
        protected static int[,] O_ioValue = new int[Motion.OXT_CARD_TOTAL, MC_GPO_MDL4 + 1];
        public short GetCardNum() { return _cardNum; }
        public short GetType() { return _ioType; }
        public bool IsDo() { return _bdo; }
        public bool IsExt() { return _mdl >= 0; }

        private AbsIo()
        { }
        public AbsIo(string cMark, string cName = "")
        {
            _cMark = cMark;
            if (cName == "")
                m_sName = _cMark;
            else
                m_sName = cName;

            Mark2Io(cMark);
            if (_ioType == mc.MC_GPO || _ioType == mc.MC_GPO_MDL || _ioType == MC_GPO_MDL2 ||
                _ioType == MC_GPO_MDL3 || _ioType == MC_GPO_MDL4 || _ioType == MC_GPO_ODL)
                _bdo = true;
            else
                _bdo = false;
        }
        ~AbsIo()
        {
            Dispose();
        }
        public void Dispose() { }

        public string GetMark()
        {
            return _cMark;
        }

        #region 预留的虚拟函数，通常不需要重写
        public virtual long ReadVaule()
        {
            if(_mdl < 10)
            return _ioValue[_cardNum, _ioType];
            else
                return O_ioValue[_cardNum, _ioType];
        }
        public virtual bool ReadBit()
        {
            if (_mdl < 0)
                _ioLevel = Convert.ToBoolean(_ioValue[_cardNum, _ioType] & (1 << (_ioIndex)));
            else if(_mdl < 10)
                _ioLevel = Convert.ToBoolean(_ioValue[_cardNum + Motion.CARD_TOTAL, _ioType] & (1 << (_ioIndex)));
            else
                //_ioLevel = Convert.ToBoolean(_ioValue[_cardNum + Motion.CARD_TOTAL + Motion.EXT_CARD_TOTAL, _ioType] & (1 << (_ioIndex)));
                _ioLevel = Convert.ToBoolean(O_ioValue[_cardNum, _ioType] & (1 << (_ioIndex)));
            return _ioLevel;
        }
        protected virtual void WriteVaule(int value = 0)
        {
            _ioValue[_cardNum, _ioType] = value;
        }
        public virtual bool GetIoBit()
        {
            if (_bdo)
            {
                _ioLevel = GetDoBit();
            }
            else
            {
                _ioLevel = GetDiBit();
            }
            return _ioLevel;
        }
        protected virtual bool Mark2Io(string cMark = "")
        {//字符串解码成底层指令
            //if (cMark.Length > 0)
            if (cMark != null && cMark != string.Empty)
            {
                string str = cMark;
                //if (bExtCard && int(str.find("DX")) >= 0)
                //{用来将DX转化为EX，适用于之前用扩展卡后来改成运控卡
                //    str = g_pYdMotion->mapExt2Card[str.c_str()];
                //}
                //str.UpperCase();
                if (str.IndexOf("EXI") >= 0)
                    _ioType = mc.MC_GPI;//4
                else if (str.IndexOf("DXI") >= 0)
                    _ioType = mc.MC_GPI_MDL;//7
                else if (str.IndexOf("EXO") >= 0)
                    _ioType = mc.MC_GPO;//12
                else if (str.IndexOf("DXO") >= 0)
                    _ioType = mc.MC_GPO_MDL;//9
                else if (str.IndexOf("HOME") >= 0)
                    _ioType = mc.MC_HOME;//3
                else if (str.IndexOf("LIMIT") >= 0)
                {
                    if (str[6] == '+')
                        _ioType = mc.MC_LIMIT_POSITIVE;//0
                    else
                        _ioType = mc.MC_LIMIT_NEGATIVE;//1
                }
                else if (str.IndexOf("OXI") >= 0)
                    _ioType = MC_GPI_ODL;
                else if (str.IndexOf("OXO") >= 0)
                    _ioType = MC_GPO_ODL;
                if (str.Length < 6)
                {//0
                    _cardNum = 0;
                }
                else
                {//0,1,2
                    _cardNum = Convert.ToInt16(str.Substring(str.Length - 1, 1));
                }
                if (str.IndexOf("EX") >= 0 || str.IndexOf("DX") >= 0 || str.IndexOf("OX") >= 0)
                {
                    int iPos = str.IndexOf("DX");
                    if (iPos >= 0)
                    {
                        if (str.Length < 6)
                        {//0
                            _mdl = 0;
                        }
                        else
                        {//0,1,2,3
                            _mdl = Convert.ToInt16(str.Substring(str.Length - 1, 1));
                            if (_mdl > 0)
                            {
                                if (_ioType == mc.MC_GPI_MDL)
                                {
                                    _ioType = (short)(MC_GPI_MDL2 + (_mdl - 1) * 2);
                                }
                                else if (_ioType == mc.MC_GPO_MDL)
                                {
                                    _ioType = (short)(MC_GPO_MDL2 + (_mdl - 1) * 2);
                                }
                            }
                        }
                    }
                    else if (str.IndexOf("OX") >= 0)
                    {
                        if (str.Length < 6)
                        {//0
                            _mdl = 0;
                        }
                        else
                        {//0,1,2,3
                            _mdl = Convert.ToInt16(str.Substring(str.Length - 1, 1));
                            if (_mdl > -1)
                            {
                                if (_ioType == MC_GPI_ODL)
                                {
                                    _ioType = (short)(MC_GPI_MDL3 + (_mdl - 1) * 2);
                                    _mdl = 10;
                                }
                                else if (_ioType == MC_GPO_ODL)
                                {
                                    _ioType = (short)(MC_GPO_MDL3 + (_mdl - 1) * 2);
                                    _mdl = 11;
                                }
                            }
                        }
                    }
                    else
                    {
                        _mdl = -1; //运控卡
                    }
                }

                //提取数字部分做为IO号
                //str = Regex.Replace(cMark, @"[^\d.\d\-\+]", "");
                //str = Regex.Replace(cMark, @"[^\d.\d]", "");
                //str = Regex.Replace(cMark, @"[^\d]", "");
                //_ioIndex = Convert.ToInt16(str);
                _ioIndex = Convert.ToInt16(Publics.GetFristNum(ref cMark));
                return true;
            }
            return false;
        }
        #endregion

        #region 虚拟函数需要重写
        abstract protected bool GetDoBit();
        abstract protected bool GetDiBit();
        abstract public bool SetDoBit(bool ioLevel);
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using ClassLib_ParaFile;
using ClassLib_MSG;

namespace ClassLib_Motion
{
    public enum AxisResetType
    {
        NONE = -1,         //不复位
        LIMIT = 0,         //限位（正或负）
        HOME_ROUND = 1,    //旋转原点
        DOUBLE_LIMIT_Z = 2,//双限位Z
        HOME_CATCH = 3,    //HOME信号捕获
        INDEX_CATCH = 4    //INDEX信号捕获
    }

    public class AbsResetAxis
    {
        //是否取反复位方向 默认：负向复位 取反：正向复位
        public bool IsNegateResetDirection = false;
        //复位类型0限位，1旋转HOME，2双限位Z，3HOME捕获，4INDEX捕获
        public int ResetType = 0;
        //快速到限位的速度
        public double FastHomeSpeed = AbsMotion.ConstDebugVel;//50
        //快速到限位的加速度
        public double FastHomeAcc = 1000;
        //第一次往回走的距离
        public double RollBackDistance_1 = 5;
        //慢速到限位的速度
        public double SlowHomeSpeed = AbsMotion.ConstDebugVel / 100;//0.5;
        //慢速到限位的加速度 
        public double SlowHomeAcc = 10;
        //第二次往回走的距离
        public double RollBackDistance_2 = 1;
        //原点偏移值
        public double OffsetZero = 1;
        //超时时间
        public int TimeOut = 20;

        public bool IsReseted = false;
        public bool IsReseting = false;
        protected AbsAxis _myAxis;
        public AbsResetAxis(AbsAxis myAxis)
        {
            _myAxis = myAxis;
        }
        protected AbsResetAxis() { }

        virtual public bool GoHome()
        {
            if ((int)AxisResetType.NONE != ResetType)
            {
                bool bRotation = ((ResetType == (int)AxisResetType.HOME_ROUND) ? true : false);
                try
                {
                    IsReseted = false;
                    IsReseting = true;

                    //_myAxis.
                    if ((int)AxisResetType.DOUBLE_LIMIT_Z == ResetType)
                        //IsReseted = _myAxis.ZGoHome(FastHomeSpeed, FastHomeAcc, SlowHomeSpeed, SlowHomeAcc);
                        IsReseted = GoHomeZByLimit(FastHomeSpeed, FastHomeAcc, RollBackDistance_1, SlowHomeSpeed, SlowHomeAcc, RollBackDistance_2, OffsetZero, bRotation, IsNegateResetDirection, TimeOut);
                    else if ((int)AxisResetType.HOME_CATCH == ResetType)
                        //HOME 捕获特殊复位方式，放在对应运控卡的子类中实现虚拟函数
                        IsReseted = _myAxis.GoHomeByHome(FastHomeSpeed, FastHomeAcc, RollBackDistance_1, SlowHomeSpeed, SlowHomeAcc, RollBackDistance_2, OffsetZero, bRotation, IsNegateResetDirection, TimeOut);
                    else if ((int)AxisResetType.INDEX_CATCH == ResetType)
                        //INDEX 捕获特殊复位方式，放在对应运控卡的子类中实现虚拟函数
                        IsReseted = _myAxis.GoHomeByIndex(FastHomeSpeed, FastHomeAcc, RollBackDistance_1, SlowHomeSpeed, SlowHomeAcc, RollBackDistance_2, OffsetZero, bRotation, IsNegateResetDirection, TimeOut);
                    else
                        IsReseted = GoHomeByLimit(FastHomeSpeed, FastHomeAcc, RollBackDistance_1, SlowHomeSpeed, SlowHomeAcc, RollBackDistance_2, OffsetZero, bRotation, IsNegateResetDirection, TimeOut);

                }
                catch (Exception ex)
                {
                    System.Diagnostics.Trace.WriteLine(ex.Message);
                    IsReseted = false;
                }
            }
            //else
            //    IsReseted = true;
            IsReseting = false;
            return IsReseted;
        }

        /// <summary>
        /// 早期的双限位Z复位，目前已不使用
        /// </summary>
        /// <param name="vel"></param>
        /// <param name="acc"></param>
        /// <param name="velLow"></param>
        /// <param name="accLow"></param>
        /// <param name="offsetZero"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        virtual public bool ZGoHome(double vel = AbsMotion.ConstDebugVel, double acc = 1000, double velLow = AbsMotion.ConstDebugVel / 100, double accLow = 10, double offsetZero = 0, int timeOut = 10)
        {
            double offset = 1;
            short sRtn = _myAxis.LmtsOff();
            // 位置清零
            sRtn = _myAxis.ZeroPos();

            double lPos = 0, lPos1 = 0;
            String strLmtP = "LIMIT" + (_myAxis.AxisInCard - 1).ToString() + "+_" + _myAxis.CardNum.ToString();
            String strLmtN = "LIMIT" + (_myAxis.AxisInCard - 1).ToString() + "-_" + _myAxis.CardNum.ToString();
            bool bLmtP = !AbsMotion.GetIo(strLmtP);
            bool bLmtN = !AbsMotion.GetIo(strLmtN);
            //bool bLmtPN = false;

            //1 非同时感应到限位，先回到平衡位置
            if (!((bLmtP) && (bLmtN)))
            {
                double tmpVel = -Math.Abs(vel);
                if (bLmtP)//感应到正限位时往正方向运行
                    tmpVel = Math.Abs(vel);
                // 启动运动
                _myAxis.MoveJog(tmpVel, acc);

                //Stopwatch watcher = new Stopwatch();
                //watcher.Reset();
                //watcher.Start();
                CQueryTime watcher = new CQueryTime();

                do
                {
                    bLmtP = !AbsMotion.GetIo(strLmtP);
                    bLmtN = !AbsMotion.GetIo(strLmtN);
                    if ((bLmtP) && (bLmtN)) //同时感应到，已经回到平衡位置
                        break;
                    //if (watcher.ElapsedMilliseconds >= (timeOut * 1000))
                    if (watcher.Now() >= (timeOut * 1000))
                        return false;
                    //处理暂停
                    Publics.Process_Pause(ref watcher, true);
                } while (_myAxis.Moving(true));
                //watcher.Stop();
                _myAxis.Stop();
            }

            //2 从平衡位置错开+offset
            if ((bLmtP) && (bLmtN))
            {
                //bLmtPN = true;
                //正方向启动运动
                _myAxis.MoveJog(Math.Abs(vel), acc);
                do
                {
                    // 读取轴状态
                    _myAxis.GetSts();
                    // 读取规划位置
                    _myAxis.GetPrfPos();
                    // 读取编码器位置
                    _myAxis.GetEncPos();
                    bLmtP = !AbsMotion.GetIo(strLmtP);
                    if (!bLmtP)
                    {//第一次脱离正限位继续运行offset距离
                        _myAxis.Stop();
                        _myAxis.MoveRelative(Math.Abs(offset), vel, acc, true);//等待到位
                        break;
                    }
                } while (_myAxis.Moving(false));
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
                sRtn = _myAxis.ZeroPos();
                //负向慢速运动直到正限位，记录下位置    
                _myAxis.MoveRelative(-Math.Abs(10 * offset), velLow, accLow, bEncPos: false);
                //MoveJog(-Math.Abs(velLow));

                int iStep = 0;
                do
                {
                    // 读取轴状态
                    _myAxis.GetSts();
                    //读取规划位置
                    _myAxis.GetPrfPos();
                    // 读取编码器位置
                    _myAxis.GetEncPos();

                    if (iStep >= 0)
                    {
                        bLmtP = !AbsMotion.GetIo(strLmtP);
                        bLmtN = !AbsMotion.GetIo(strLmtN);
                        if (0 == iStep && bLmtP)
                        {//记录第一次慢速感应到正限位的位置1, 并快速运行到脱离负限位
                            iStep++;
                            lPos1 = Convert.ToInt32(_myAxis.m_EncPrm.pos);
                            _myAxis.MoveRelative(-Math.Abs(10 * offset), vel, acc, bEncPos: false);
                        }
                        else if (1 == iStep && !bLmtN)
                        {//脱离负限位后慢速往回运行+offset距离
                            iStep++;
                            _myAxis.MoveRelative(Math.Abs(offset), velLow, accLow, bEncPos: false);
                        }
                        else if (2 == iStep && bLmtN)
                        {//再次感应到负限位，此时记录下位置2，并计算出平均位置，再运行到平均位置
                            iStep = -1;
                            lPos = Convert.ToInt32((lPos1 + _myAxis.m_EncPrm.pos) / 2);
                            _myAxis.MoveAbsolute(_myAxis.PulseToMm(lPos) + offsetZero, vel, acc);
                        }
                    }
                } while (_myAxis.Moving(false));

                if (-1 != iStep)
                    return false;
            }
            //else
            //{
            //    return false;
            //}

            Thread.Sleep(500);
            // 位置清零
            sRtn = _myAxis.ZeroPos();
            // 读取规划位置
            _myAxis.GetPrfPos();
            // 读取编码器位置
            _myAxis.GetEncPos();
            return true;
        }

        /// <summary>
        /// 双限位复位Z轴
        /// </summary>
        /// <param name="vel"></param>
        /// <param name="acc"></param>
        /// <param name="offset"></param>
        /// <param name="velLow"></param>
        /// <param name="accLow"></param>
        /// <param name="offsetLow"></param>
        /// <param name="offsetZero"></param>
        /// <param name="bRotation"></param>
        /// <param name="bNegateResetDirection"></param>
        /// <param name="bTimeOut"></param>
        /// <returns></returns>
        virtual public bool GoHomeZByLimit(double vel = AbsMotion.ConstDebugVel, double acc = AbsMotion.ConstDebugAcc, double offset = 5, double velLow = AbsMotion.ConstDebugVel / 100, double accLow = 10, double offsetLow = 1, double offsetZero = 1, bool bRotation = false, bool bNegateResetDirection = false, int bTimeOut = 10)
        {
            int iOffsetDir = (bNegateResetDirection) ? -1 : 1;
            _myAxis.LmtsOn();  //enable吸头Z轴正负限位
            Thread.Sleep(5);
            //感应是否感应到负限位
            if (_myAxis.GetLimit(bNegateResetDirection, true))     //正在负限位
            {
                //1 往正限位方向快速走撞限位
                //if (!MoveJog(iOffsetDir * vel, acc, true))
                if (!_myAxis.MoveRelative(iOffsetDir * 100, vel, acc, true))
                {
                    MSG.Show("脱离限位运行超时,轴：" + _myAxis.m_sName);
                    _myAxis.LmtsOff();
                    return false;
                }
            }
            bool rtn = GoHomeByLimit(vel, acc, offset, velLow, accLow, offsetLow, offsetZero, bRotation, bNegateResetDirection, bTimeOut);
            _myAxis.LmtsOff();
            return rtn;
        }

        /// <summary>
        /// 单限位复位XYRZ轴等
        /// </summary>
        /// <param name="vel"></param>
        /// <param name="acc"></param>
        /// <param name="offset"></param>
        /// <param name="velLow"></param>
        /// <param name="accLow"></param>
        /// <param name="offsetLow"></param>
        /// <param name="offsetZero"></param>
        /// <param name="bRotation"></param>
        /// <param name="bNegateResetDirection"></param>
        /// <param name="bTimeOut"></param>
        /// <returns></returns>
        virtual public bool GoHomeByLimit(double vel = AbsMotion.ConstDebugVel, double acc = AbsMotion.ConstDebugAcc, double offset = 5, double velLow = AbsMotion.ConstDebugVel / 100, double accLow = 10, double offsetLow = 1, double offsetZero = 1, bool bRotation = false, bool bNegateResetDirection = false, int bTimeOut = 10)
        {
            int iOffsetDir = (bNegateResetDirection) ? -1 : 1;
            short sRtn = 0;
            _myAxis.ZeroPos();
            _myAxis.SetEncPos(0);
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
                    _myAxis.LmtsOn(LimitPN.MC_LIMIT_NEGATIVE);  //enable吸头R轴负限位 mc.MC_LIMIT_NEGATIVE
                    Thread.Sleep(5);
                    //感应是否感应到负限位（需要将HOME/负限位信号取反，脱离感应片时有信号），若脱离，正向旋转100度 
                    if (_myAxis.GetLimit(bNegateResetDirection, true))     //正在负限位
                    {
                        _myAxis.LmtsOff(LimitPN.MC_LIMIT_POSITIVE);//关闭正限位（恒昱只能正负都关闭）
                        //正方向旋转相对卡口度 等待停止
                        _myAxis.MoveRelative(iOffsetDir * 100, vel, acc, true);
                        _myAxis.LmtsOn(LimitPN.MC_LIMIT_NEGATIVE);//重新打开负限位
                    }
                }
                else
                {
                    //感应是否感应到负限位
                    int iTimes = 0;
                    do
                    {
                        if (_myAxis.GetLimit(bNegateResetDirection, true))     //正在负限位
                        {
                            //0 脱离负限位
                            //if (!MoveJog(iOffsetDir * vel, acc, true))
                            if (!_myAxis.MoveRelative(iOffsetDir * 5, vel, acc, true))
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
                        MSG.Show("脱离限位超时,轴：" + _myAxis.m_sName);
                        return false;
                    }
                }
                //1 往负限位方向快速走撞限位
                if (!_myAxis.MoveJog(iOffsetDir * -vel, acc, true))
                //if (!MoveRelative(iOffsetDir * -9999, vel, acc, true))
                {
                    MSG.Show("第一次往限位方向运行超时,轴：" + _myAxis.m_sName);
                    return false;
                }
                //往回走距离1脱离感应片，然后重新用快速撞限位
                _myAxis.MoveRelative(iOffsetDir * offset, vel, acc, true);

                //2 重新用快速撞限位
                if (!_myAxis.MoveJog(iOffsetDir * -vel / 5, acc, true))
                //if (!MoveRelative(iOffsetDir * -9999, vel / 5, acc, true))
                {
                    MSG.Show("第二次往限位方向运行超时,轴：" + _myAxis.m_sName);
                    return false;
                }
                //往回走距离2脱离感应片，然后重新用慢速撞限位
                _myAxis.MoveRelative(iOffsetDir * offsetLow, vel, acc, true);

                //3 重新用慢速撞限位
                if (!_myAxis.MoveJog(iOffsetDir * -velLow, accLow, true))
                //if (!MoveRelative(iOffsetDir * -9999, velLow, accLow, true))
                {
                    MSG.Show("第三次往限位方向运行超时,轴：" + _myAxis.m_sName);
                    return false;
                }
                Thread.Sleep(200);
                if (offsetZero != 0)
                {//到限位后走偏移量作为零点
                    _myAxis.MoveRelative(iOffsetDir * offsetZero, vel, acc, true);
                    Thread.Sleep(1000);
                }

                //4 位置清零
                sRtn = _myAxis.ZeroPos();
                // 读取规划位置
                _myAxis.GetPrfPos();
                // 读取编码器位置
                _myAxis.GetEncPos();
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
                    _myAxis.LmtsOff(LimitPN.MC_LIMIT_NEGATIVE);  //取消吸头R轴负限位 mc.MC_LIMIT_NEGATIVE
                }
            }
            return true;
        }
    }
}

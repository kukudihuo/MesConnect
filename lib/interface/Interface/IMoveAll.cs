using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLib_ParaFile;

namespace Interface
{
    public enum ArriveMode
    {
        NoWaitMode = 0,
        WaitIncMode = 1,
        WaitStopMode = 2,
        WaitEncMode = 3
    }
    //public delegate double GetSafeZCallBack();


    public interface IMoveAll
    {
        //public string m_sName;
        //protected const double ConstZeroZOff = 0.6;
        //protected const double ConstIncOff = 0.5;
        //protected const double ConstEncOff = 0.05;
        ////public GetSafeZCallBack GetSafeZ = null;//获取安全高度


        //获取安全高度
        double GetSafeZnull();

        ////public int GetAxis(AbsAxis axis);

        //public virtual AbsAxis GetAxisX()
        //{
        //    return null;
        //}

        //public virtual AbsAxis GetAxisY()
        //{
        //    return null;
        //}

        //public virtual AbsAxis GetAxisW()
        //{
        //    return null;
        //}

        //public virtual AbsAxis GetAxisZ()
        //{
        //    return null;
        //}
        #region 运动控制与IO 1-9
        //protected PosXYWZ _desPos = new PosXYWZ(0);
        //protected PosXYWZ _currentPos = new PosXYWZ(0);
        
        /// <summary>
        ///1 点动模式下运动到任意位置绝对坐标
        /// </summary>
        /// <param name="pos">运动到目标位置</param>
        /// <param name="bMoveZ">Z是否运动</param>
        /// <param name="bMoveW">W是否运动</param>
        /// <param name="bMoveY">Y是否运动</param>
        /// <param name="bMoveX">X是否运动</param>
        /// <param name="arriveMode">等待模式</param>
        /// <param name="bWaitX">是否等待X停止</param>
        /// <param name="bWaitY">是否等待Y停止</param>
        /// <param name="bWaitW">是否等待W停止</param>
        /// <param name="bWaitZ">是否等待Z停止</param>
        /// <param name="off">等待到位偏差值</param>
        /// <param name="bSafeZ">是否Z先要运行到安全高度</param>
        /// <param name="pos_flex">降速起始点</param>
        /// <param name="vel_low_per">二段速度</param>
        /// <param name="dBackOffW">是否消除W反向间隙</param>
        /// <param name="dDelayMoveZ">Z下降前延时时间</param>
        /// <returns>是否正常运行结束</returns>
        bool MoveAll(PosXYWZ pos, bool bMoveZ = true, bool bMoveW = true, bool bMoveY = true, bool bMoveX = true,
                                            ArriveMode arriveMode = ArriveMode.WaitStopMode,
                                            bool bWaitX = true, bool bWaitY = true, bool bWaitW = true, bool bWaitZ = true,
                                            double off = 0.05, bool bSafeZ = true, double pos_flex = 0, double vel_low_per = 0, double dBackOffW = 0, double dDelayMoveZ = 0);
        //2 底层绝对点位运动
        //bool MovePos(PosXYWZ pos);

        //3 相对运动
        bool MoveAllRelative(PosXYWZ pos);

        //4 获取当前位置的绝对坐标
        PosXYWZ GetPos();

        //5 Z单独运动到目标位置
        //bool MoveZ(PosXYWZ pos, bool bReAbsorb = false, ArriveMode arriveMode = ArriveMode.WaitStopMode, double dOffsetZ = ConstIncOff, double pos_flex = 0, double vel_low = 0);
        //bool MoveZ(PosXYWZ pos, bool bReAbsorb, ArriveMode arriveMode, double dOffsetZ, double pos_flex, double vel_low);
        bool MoveZ(PosXYWZ pos, bool bReAbsorb = false, ArriveMode arriveMode = ArriveMode.WaitStopMode, double dOffsetZ = 0.5, double pos_flex = 0, double vel_low = 0);
        

        //6 Z单独回安全高度
        bool MoveSafeZ(bool bMoveOther = false);

        //7 获取目标位置
        PosXYWZ GetDesPos();

        //8 等待运行停止(机器人到位后返回)
        bool WaitArriveAll();

        //9 光源控制
        bool LightOn(bool bOn = true);

        /// <summary>
        /// 单动X轴
        /// </summary>
        /// <param name="pos">运行到目标位置</param>
        /// <param name="arriveMode">等待停止模式</param>
        /// <param name="off">等待误差</param>
        /// <param name="bSafeZ">Z是否先回安全位置</param>
        /// <returns>运行是否成功</returns>
        bool MoveX(PosXYWZ pos, ArriveMode arriveMode = ArriveMode.WaitStopMode, double off = 0.05, bool bSafeZ = true);
        /// <summary>
        /// 单动Y轴
        /// </summary>
        /// <param name="pos">运行到目标位置</param>
        /// <param name="arriveMode">等待停止模式</param>
        /// <param name="off">等待误差</param>
        /// <param name="bSafeZ">Z是否先回安全位置</param>
        /// <returns>运行是否成功</returns>
        bool MoveY(PosXYWZ pos, ArriveMode arriveMode = ArriveMode.WaitStopMode, double off = 0.05, bool bSafeZ = true);
        /// <summary>
        /// 单动W轴
        /// </summary>
        /// <param name="pos">运行到目标位置</param>
        /// <param name="arriveMode">等待停止模式</param>
        /// <param name="off">等待误差</param>
        /// <param name="bSafeZ">Z是否先回安全位置</param>
        /// <param name="dBackOffW">W轴是否消除反向间隙</param>
        /// <returns>运行是否成功</returns>
        bool MoveW(PosXYWZ pos, ArriveMode arriveMode = ArriveMode.WaitStopMode, double off = 0.05, bool bSafeZ = true, double dBackOffW = 0);
        /// <summary>
        /// 运行XY轴
        /// </summary>
        /// <param name="pos">运行到目标位置</param>
        /// <param name="arriveMode">等待停止模式</param>
        /// <param name="off">等待误差</param>
        /// <param name="bSafeZ">Z是否先回安全位置</param>       
        /// <returns>运行是否成功</returns>
        bool MoveXY(PosXYWZ pos, ArriveMode arriveMode = ArriveMode.WaitStopMode, double off = 0.05, bool bSafeZ = true);
        /// <summary>
        /// 运行XW轴
        /// </summary>
        /// <param name="pos">运行到目标位置</param>
        /// <param name="arriveMode">等待停止模式</param>
        /// <param name="off">等待误差</param>
        /// <param name="bSafeZ">Z是否先回安全位置</param>
        /// <param name="dBackOffW">W轴是否消除反向间隙</param>
        /// <returns>运行是否成功</returns>
        bool MoveXW(PosXYWZ pos, ArriveMode arriveMode = ArriveMode.WaitStopMode, double off = 0.05, bool bSafeZ = true, double dBackOffW = 0);
        /// <summary>
        /// 运行YW轴
        /// </summary>
        /// <param name="pos">运行到目标位置</param>
        /// <param name="arriveMode">等待停止模式</param>
        /// <param name="off">等待误差</param>
        /// <param name="bSafeZ">Z是否先回安全位置</param>
        /// <param name="dBackOffW">W轴是否消除反向间隙</param>
        /// <returns>运行是否成功</returns>
        bool MoveYW(PosXYWZ pos, ArriveMode arriveMode = ArriveMode.WaitStopMode, double off = 0.05, bool bSafeZ = true, double dBackOffW = 0);
        /// <summary>
        /// 运行XYW轴
        /// </summary>
        /// <param name="pos">运行到目标位置</param>
        /// <param name="arriveMode">等待停止模式</param>
        /// <param name="off">等待误差</param>
        /// <param name="bSafeZ">Z是否先回安全位置</param>
        /// <param name="dBackOffW">W轴是否消除反向间隙</param>
        /// <returns>运行是否成功</returns>
        bool MoveXYW(PosXYWZ pos, ArriveMode arriveMode = ArriveMode.WaitStopMode, double off = 0.05, bool bSafeZ = true, double dBackOffW = 0);

        bool WaitArriveAll(ArriveMode arriveMode, double off, bool bWaitX = true, bool bWaitY = true, bool bWaitW = true, bool bWaitZ = true);

        bool WaitIncArriveAll(double off);

        bool WaitEncArriveAll(double off);
        bool IsIncArriveAll(object off);

        bool IsEncArriveAll(object off);
        /// <summary>;
        /// 标定用 //消除反向间隙 未知方向的情况下
        /// </summary>
        bool MoveAllOffBack(PosXYWZ pos, double dOffBack = 0.5);
        /// <summary>
        /// 标定用 //消除反向间隙 已知方向的情况下
        /// </summary>
        bool MoveAllOffBack(PosXYWZ pos, double x, double y, double w, double dOffBack = 0.5, double dWaitTime = 0.2, double off = 0.05);

        ////7 获取安全高度
        //double GetSafeZ();
        #endregion 1-9
    }
}

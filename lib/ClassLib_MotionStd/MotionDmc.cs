using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ClassLib_ParaFile;

namespace ClassLib_Motion
{
    #region 包含一组类 DmcMotion DmcAxis DmcCard DmcIo

    /// <summary>
    /// 用于创建：DmcAxis DmcCard DmcIo
    /// </summary>
    public class DmcMotion : AbsMotion
    {
        public DmcMotion()
        {
        }
        ~DmcMotion()
        {
        }
        public override AbsCard CreateCard(int iCard, string IniFileName)
        {
            //获取主卡及扩展卡配置文件路径
            string sCardFile = ParaFileINI.ReadINI("CARD", "Card" + iCard, IniFileName);
            sCardFile = Publics.GetGtsPath() + sCardFile + ".ini";
            string sExtCardFile = ParaFileINI.ReadINI("CARD", "ExtCard", IniFileName);
            sExtCardFile = Publics.GetGtsPath() + sExtCardFile + ".ini";
            short mdl = Convert.ToInt16((0 == iCard && AbsMotion.EXT_CARD_TOTAL > 0) ? (AbsMotion.EXT_CARD_TOTAL - 1) : -1);
            return new DmcCard(Convert.ToInt16(iCard), sCardFile, mdl, false, sExtCardFile);
        }
        public override AbsIo CreateIo(string str)
        {
            return new DmcIo(str);
        }
        public override AbsAxis CreateAxis(short virtualIndex, short index, string name, double pitch, double divide)
        {
            return new DmcAxis(virtualIndex, index, name, pitch, divide);
        }
    }

    //初始化卡
    public class DmcCard : AbsCard
    {
        protected short _mdl;
        protected bool _bHaveFather;
        protected string _sFile;
        protected string _sExtFile = "";
        private static bool s_bFirst = true;

        public DmcCard(short cardNum, string sFile, short mdl = -1, bool bHaveFather = false, string sExtFile = "")
        {
            if (s_bFirst)
            {
                //short count = LTDMC.dmc8801_board_init();
                //short count = LTDMC.dmc_board_init();
                s_bFirst = false;
            }
            _cardNum = cardNum;
            _mdl = mdl;
            _bHaveFather = bHaveFather;
            _sFile = sFile;
            _sExtFile = sExtFile;

            if (!_bHaveFather)//构造函数中调用虚拟函数时虚拟效果不起作用
                OpenCard();
        }
        ~DmcCard()
        {
            Dispose();
        }
        new public void Dispose()
        {
            if (!_bHaveFather)//析构函数中调用虚拟函数时虚拟效果不起作用
                CloseCard();
        }

        public override short OpenCard()
        {
            try
            {
                //打开PCI卡
                //m_bCardOK = true;
                //PCI400_card_reset(Convert.ToUInt16(_cardNum));
                //PCI400_get_total_axes
                //short srn = LTDMC.dmc_board_init();
                short srn = LTDMC.dmc_board_init_onecard((ushort)_cardNum); 
                if (srn > 0)
                    m_bCardOK = true;
                srn = LTDMC.dmc_download_configfile((ushort)_cardNum, _sFile);
                if (_mdl >= 0)
                {//扩展卡
                    //for (ushort iCanid = 1; iCanid <= _mdl + 1; iCanid++)
                    //{
                    //    // dmc_get_can_state(WORD CardNo,WORD* NodeNum,WORD* state) 
                    //    LTDMC.dmc_set_can_state((ushort)_cardNum, iCanid, 1, 0);
                    //}
                    LTDMC.dmc_set_can_state((ushort)_cardNum, (ushort)(_mdl + 1), 1, 0);
                }
                //清除所有异常
                ClrStsAll();
                return srn;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
            return 0;
        }
        public override short CloseCard()
        {
            try
            {
                if (!s_bFirst)
                {
                    LTDMC.dmc_board_close();
                    s_bFirst = true;                   
                }
                return 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
            return 0;
        }
        public override short ClrStsAll()
        {
            return 0;//mc.GT_ClrSts(_cardNum, 1, Motion.AXIS_IN_CARD);
        }
    }

    //轴
    public class DmcAxis : AbsAxis
    {
        public enum AxisStatus
        {
            ALARM_STS = 0x01,       //报警
            LMT_P_STS = 0x02,       //正限位
            LMT_N_STS = 0x04,       //负限位
            EMG_STS = 0x08,         //急停位
            HOME_STS = 0x10,        //原点位
               
            S_LMT_P_STS = 0x40,     //软正限位
            S_LMT_N_STS = 0x80,     //软负限位
            ARRIVE_STS = 0x100,     //运行到位
            EZ_STS = 0x200,         //EZ
            RDY_STS = 0x400,        //伺服准备好
            DSTP_STS = 0x800,       //减速停止
        };

        public DmcAxis(short virtualIndex, short index, string name, double pitch, double divide)
            : base(virtualIndex, index, name, pitch, divide)
        {
            m_TrapPrm.velStart = 400;   //起始速度
            m_TrapPrm.smoothTime = (short)0.1; //S曲线 0-0.5s 0为T曲线

            m_JogPrm.acc = 1;
            m_JogPrm.dec = 1;
            m_JogPrm.smooth = 0.1; //要留意此参数

            m_ResetAxis = new ResetAxis(this);
        }
        ~DmcAxis()//bool disposing
        {
            Dispose();
        }
        //protected override void Dispose(bool disposing)//bool disposing
        new public void Dispose()//bool disposing
        {
            //新增退出程序后释放电机
            AxisOn(false);
            //线程
            ////delete myThread;
            //m_Thread = null;
        }

        //设置到位误差
        public void SetArriveOffset(int offPulse)
        {
            double factory = 0;
            int offCur = 0;
            LTDMC.dmc_get_factor_error((ushort)_cardNum, (ushort)_axis, ref factory, ref offCur);
            LTDMC.dmc_set_factor_error((ushort)_cardNum, (ushort)_axis, factory, offPulse);
        }
        //等待脉冲误差到位100ms
        public short CheckSuccessPulse()
        {
            return LTDMC.dmc_check_success_pulse((ushort)_cardNum, (ushort)_axis);
        }
        //等待编码误差到位100ms
        public short CheckSuccessEncoder()
        {
            return LTDMC.dmc_check_success_encoder((ushort)_cardNum, (ushort)_axis);
        }
        //插补运动
        //dmc_set_vector_profile_multicoor
        //dmc_line_multicoor
        //dmc_arc_move_multicoor 
        //dmc_check_done_multicoor
        //dmc_stop_multicoor 

        //PVT运动
        //dmc_PttTable
        //dmc_PtsTable
        //dmc_PvtTable
        //dmc_PvtsTable
        //dmc_PvtMove

        //位置比较输出
        //dmc_compare_set_config_extern    二维
        //dmc_compare_clear_points_extern  二维
        //dmc_compare_add_point_extern     二维
        //dmc_hcmp_set_mode                高速
        //dmc_hcmp_set_config              高速
        //dmc_hcmp_clear_points            高速
        //dmc_hcmp_add_point               高速

        #region 虚拟函数，需要重写

        #region 读入及设置轴各种状态
        //1 是否正在运行中
        override public bool Moving(bool bGetSts = true)
        {
            return !Convert.ToBoolean(LTDMC.dmc_check_done((ushort)_cardNum, (ushort)_axis));// || !Convert.ToBoolean((m_Sts & ((int)AxisStatus.ARRIVE_STS)));
        }
        //2 读取伺服使能
        override public bool GetOnSts(bool bGetSts = false)
        {
            if (bGetSts)
                GetSts();
            //PCI400_read_RDY_PIN
            return !Convert.ToBoolean(LTDMC.dmc_read_sevon_pin((ushort)_cardNum, (ushort)_axis));
        }
        //3 读取报警
        override public bool GetAlarm(bool bGetSts = false)
        {
            if (bGetSts)
                GetSts();
            return Convert.ToBoolean((m_Sts & ((int)AxisStatus.ALARM_STS)));
        }
        //4 读取停止信号
        override public bool GetStop(bool bGetSts = false)
        {
            return Convert.ToBoolean((m_Sts & ((int)AxisStatus.EMG_STS))) || Convert.ToBoolean((m_Sts & ((int)AxisStatus.DSTP_STS)));
        }
        //5 读取限位信号
        override public bool GetLimit(bool bPlimit = true, bool bGetSts = false)
        {
            if (bGetSts)
                GetSts();
            if (bPlimit)
                return Convert.ToBoolean((m_Sts & ((int)AxisStatus.LMT_P_STS)));
            else
                return Convert.ToBoolean((m_Sts & ((int)AxisStatus.LMT_N_STS)));
        }
        //6 读取所有状态
        override public int GetSts(short count = 1)
        {
            m_Sts = (int)LTDMC.dmc_axis_io_status((ushort)_cardNum, (ushort)_axis);
            return m_Sts;
        }
        //7 清除异常
        override public short ClrSts(short count = 1)//多了个count参数，默认需要设为多少？
        {//清除并更新状态，修改后必须更新才有效
            return LTDMC.nmc_clear_axis_errcode((ushort)_cardNum, (ushort)_axis);
        }
        //8 使能
        override public short AxisOn(bool bExp = true)
        {// 伺服使能
            GetSts();
            short sRtn = 0;
            if (!GetOnSts())
            {
                LTDMC.dmc_write_sevon_pin((ushort)_cardNum, (ushort)_axis, 0);
                Thread.Sleep(50);//需要延时等待伺服使能完成
            }
            if (bExp && GetAlarm())
            {
                string s = m_index.ToString() + "轴伺服报警！";//Convert.ToString(m_index);
                // throw new Exception(s);
            }
            return sRtn;
        }
        //9 关闭使能
        override public short AxisOff()
        {
            return (short)LTDMC.dmc_write_sevon_pin((ushort)_cardNum, (ushort)_axis, 1);
        }
        //10 设置方向与脉冲 暂时未用
        override public short StepPulseDir(short pulse, short dir)
        {
            ushort step = 0;
            if (dir == 1)
                step = 4;
            return (short)LTDMC.dmc_set_pulse_outmode((ushort)_cardNum, (ushort)_axis, step);
        }
        //11 限位开关打开
        override public short LmtsOn(LimitPN limitType = LimitPN.ALL)
        {
            ushort el_enable_cur = 1;
            ushort el_logic_cur = 0;
            ushort el_mode_cur = 0;
            LTDMC.dmc_get_el_mode((ushort)_cardNum, (ushort)_axis, ref el_enable_cur, ref el_logic_cur, ref el_mode_cur);

            if (LimitPN.MC_LIMIT_POSITIVE == limitType)
            {
                if (2 == el_enable_cur) 
                    el_enable_cur = 1;
                else if (0 == el_enable_cur) 
                    el_enable_cur = 3;                    
            }
            else if (LimitPN.MC_LIMIT_NEGATIVE == limitType)
            {
                if (3 == el_enable_cur)
                    el_enable_cur = 1;
                else if (0 == el_enable_cur)
                    el_enable_cur = 2;
            }
            else
                el_enable_cur = 1;

            return LTDMC.dmc_set_el_mode((ushort)_cardNum, (ushort)_axis, el_enable_cur, el_logic_cur, el_mode_cur);
        }
        //12 限位开关关闭
        override public short LmtsOff(LimitPN limitType = LimitPN.ALL)
        {
            ushort el_enable_cur = 0;
            ushort el_logic_cur = 0;
            ushort el_mode_cur = 0;
            LTDMC.dmc_get_el_mode((ushort)_cardNum, (ushort)_axis, ref el_enable_cur, ref el_logic_cur, ref el_mode_cur);

            if (LimitPN.MC_LIMIT_POSITIVE == limitType)
            {
                if (3 == el_enable_cur)
                    el_enable_cur = 0;
                else if (0 == el_enable_cur)
                    el_enable_cur = 2;
            }
            else if (LimitPN.MC_LIMIT_NEGATIVE == limitType)
            {
                if (2 == el_enable_cur)
                    el_enable_cur = 0;
                else if (0 == el_enable_cur)
                    el_enable_cur = 3;
            }
            else
                el_enable_cur = 0;

            return LTDMC.dmc_set_el_mode((ushort)_cardNum, (ushort)_axis, el_enable_cur, el_logic_cur, el_mode_cur);
        }
        #endregion

        #region 点位及连续运动
        //13 设为点位模式并设置运动参数加减速和模式
        override public short SetTrapPara(double acc, double smoothTime, double vel, bool bSave = true)
        {
            m_PrfPrm.mode = 0; //点位运动

            m_TrapPrm.acc = acc;
            m_TrapPrm.dec = acc;
            //if (smoothTime < 1)
            //    m_TrapPrm.smoothTime = smoothTime;
            // 设置点位运动参数
            if (bSave)
                m_DesPrm.acc = acc;
            return LTDMC.dmc_set_s_profile((ushort)_cardNum, (ushort)_axis, 0, m_TrapPrm.smoothTime);
        }
        //14 设为jog模式并设置运动参数加减速和模式
        override public short SetJogPara(double acc, double smooth, bool bSave = true)
        {
            //设为jog模式并设置运动参数
            SetTrapPara(acc, smooth,0, bSave);
            m_PrfPrm.mode = 1; //JOG运动
            return 0;
        }
        //15 设置运动目标位置
        override protected short SetPos(long pos, bool bSave = true)
        {
            m_DesPrm.pos = pos;
            return 0;
        }
        //16 设置速度
        override protected short SetVel(double vel, bool bSave = true)
        {
            //dmc_change_speed
            m_DesPrm.vel =vel;
            return LTDMC.dmc_set_profile((ushort)_cardNum, (ushort)_axis, m_TrapPrm.velStart, Math.Abs(m_DesPrm.vel), m_TrapPrm.acc, m_TrapPrm.acc, m_TrapPrm.velStart);
        }
        //17 读取运动目标位
        override protected long GetPos(bool bSave = true)
        {
            long Pos = LTDMC.dmc_get_position((ushort)_cardNum, (ushort)_axis);
            if (bSave)
                m_DesPrm.pos = Pos;
            return Pos;
        }
        //18 读取当前速度
        override protected double GetVel(bool bSave = true)
        {
            double vel = LTDMC.dmc_read_current_speed((ushort)_cardNum, (ushort)_axis);
            if (bSave)
                m_DesPrm.vel = vel;
            return vel;
        }
        //19 读取运动模式
        override protected long GetPrfMode(short count = 1)
        {
            return m_PrfPrm.mode;
            //return LTDMC.dmc_get_target_position((ushort)_cardNum, (ushort)_axis);
        }

        //20 运动与停止
        override public short Update()
        {// 启动AXIS轴的运动
            //PCI400_change_pmove_speed(0,Curr_Vel)；//执行改变速度指令 
            //LTDMC.dmc_pmov(_absAxis, (int)m_DesPrm.pos, 1);
            if (0 == m_PrfPrm.mode)
            {
                short sRtn;
                sRtn = LTDMC.dmc_update_target_position((ushort)_cardNum, (ushort)_axis, (int)m_DesPrm.pos, 0);
                if (!Moving())
                    sRtn = LTDMC.dmc_pmove((ushort)_cardNum, (ushort)_axis, (int)m_DesPrm.pos, 1);
            }
            else if (1 == m_PrfPrm.mode)
            {
                ushort dir = (ushort)(m_DesPrm.vel >= 0 ? 1 : 0);
                return LTDMC.dmc_vmove((ushort)_cardNum, (ushort)_axis, dir);
            }
            return 0;
        }
        //21 停止
        override public short Stop(bool option = true)
        {//option = false 平滑停止true紧急停止
            return LTDMC.dmc_stop((ushort)_cardNum, (ushort)_axis, Convert.ToUInt16(option));
        }

        //22 设置规划位置
        override public short SetPrfPos(long prfPos)
        {
            return LTDMC.dmc_set_position((ushort)_cardNum, (ushort)_axis, (int)prfPos);
        }
        //23 读取规划位置
        override public double GetPrfPos(short count = 1)
        {
            m_PrfPrm.pos = LTDMC.dmc_get_position((ushort)_cardNum, (ushort)_axis);
            return m_PrfPrm.pos;
        }

        //24 设置编码器位置
        override public short SetEncPos(int encPos)
        {
            m_EncPrm.pos = encPos;
            return LTDMC.dmc_set_encoder((ushort)_cardNum, (ushort)_axis, encPos);
        }
        //25 读取编码位置
        override public double GetEncPos(short count = 1)
        {
            m_EncPrm.pos = LTDMC.dmc_get_encoder((ushort)_cardNum, (ushort)_axis);
            return m_EncPrm.pos;
        }

        //26 位置清零
        override public short ZeroPos(short count = 1)
        {
            m_DesPrm.pos = 0;
            SetPrfPos(0);
            SetEncPos(0);
            return 0;
        }

        //间隙补偿
        //PCI400_set_backlash
        //PCI400_get_backlash

        //位置比较输出
        override public short SetCompareConfig(bool enable, short hcmp = 0)
        {
            if (enable)
            {
                LTDMC.dmc_hcmp_set_mode((ushort)_cardNum, (ushort)hcmp, 4);
                return LTDMC.dmc_hcmp_set_config((ushort)_cardNum, (ushort)hcmp, (ushort)_axis, 1, 0, 1000);
            }
            else
                return LTDMC.dmc_hcmp_set_mode((ushort)_cardNum, (ushort)hcmp, 0);
            //return (short)LTDMC.dmc_compare_set_config((ushort)_cardNum, (ushort)_axis, Convert.ToUInt16(enable), 1);
        }
        override public short ClearComparePoints(short hcmp = 0)
        {
            return LTDMC.dmc_hcmp_clear_points((ushort)_cardNum, (ushort)hcmp);
            //return (short)LTDMC.dmc_compare_clear_points((ushort)_cardNum, (ushort)_axis);
        }
        override public short AddComparePoints(short hcmp, int pos, short dir = 0)
        {
            return LTDMC.dmc_hcmp_add_point((ushort)_cardNum, (ushort)hcmp, pos);
            //return (short)LTDMC.dmc_compare_add_point((ushort)_cardNum, (ushort)_axis, pos, (ushort)dir, 6, (uint)hcmp);  //5 500us
        }
        #endregion
        #endregion
    }

    //输入输出
    public class DmcIo : AbsIo
    {
        public const short MC_SEVON = 8; //雷赛特有
        public DmcIo(string cMark, string cName = "")
            : base(cMark, cName)
        {
        }
        ~DmcIo()
        {
            Dispose();
        }
        new public void Dispose()
        {
        }

        //虚拟函数需要重写
        //读IO，写DO 
        override protected bool GetDoBit()
        {
            //一次读_ioType的一组IO，再通过_ioIndex取当前的IO值，所有的值再通过ReadAllIo读出来
            if (_mdl < 0)
            {//运控卡
                uint iRtn = LTDMC.dmc_read_outport((ushort)_cardNum, 0);
                s_ioValue[_cardNum, _ioType] = (int)(iRtn & 0xFFFF);
                s_ioValue[_cardNum, MC_SEVON] = (int)((iRtn >> 16) & 0xFF);
                s_ioValue[_cardNum, mc.MC_CLEAR] = (int)((iRtn >> 24) & 0xFF);
            }
            else
            {//扩展卡
                s_ioValue[_cardNum, _ioType] = (int)LTDMC.dmc_read_can_outport((ushort)_cardNum, (ushort)(_mdl + 1), 0);
            }
            _ioLevel = Convert.ToBoolean(s_ioValue[_cardNum, _ioType] & (1 << (_ioIndex)));
            return _ioLevel;
        }
        override protected bool GetDiBit()
        {
            //一次读_ioType的一组IO，再通过_ioIndex取当前的IO值，所有的值再通过ReadAllIo读出来
            if (_mdl < 0)
            {//运控卡       
                ushort uIoDmc = 1;
                if (_ioType == mc.MC_GPI || _ioType == mc.MC_LIMIT_POSITIVE || _ioType == mc.MC_LIMIT_NEGATIVE)
                    uIoDmc = 0;

                uint iRtn = LTDMC.dmc_read_inport((ushort)_cardNum, uIoDmc);

                if (0 == uIoDmc)
                {
                    s_ioValue[_cardNum, mc.MC_GPI] = (int)(iRtn & 0xFFFF);
                    s_ioValue[_cardNum, mc.MC_LIMIT_POSITIVE] = (int)((iRtn >> 16) & 0xFF);
                    s_ioValue[_cardNum, mc.MC_LIMIT_NEGATIVE] = (int)((iRtn >> 24) & 0xFF);
                }
                else
                {
                    s_ioValue[_cardNum, mc.MC_HOME] = (int)(iRtn & 0xFF);         //ORG 原点信号
                    s_ioValue[_cardNum, mc.MC_ALARM] = (int)((iRtn >> 8) & 0xFF);  //ALM 伺服报警
                    s_ioValue[_cardNum, mc.MC_ENABLE] = (int)((iRtn >> 16) & 0xFF); //RDY 伺服准备好    
                    s_ioValue[_cardNum, mc.MC_ARRIVE] = (int)((iRtn >> 24) & 0xFF); //INP 伺服定位完成 
                }
            }
            else
            {//扩展卡
                s_ioValue[_cardNum, _ioType] = (int)LTDMC.dmc_read_can_inport((ushort)_cardNum, (ushort)(_mdl + 1), 0);
            }
            _ioLevel = Convert.ToBoolean(s_ioValue[_cardNum, _ioType] & (1 << (_ioIndex)));
            //UpDateShow();
            return _ioLevel;
        }
        override public bool SetDoBit(bool ioLevel)
        {
            if (_bdo)
            {
                _ioLevel = ioLevel;
                if (_mdl < 0)//运控卡
                    LTDMC.dmc_write_outbit((ushort)_cardNum, (ushort)(_ioIndex), Convert.ToUInt16(_ioLevel));
                else//扩展卡
                    LTDMC.dmc_write_can_outbit((ushort)_cardNum, (ushort)(_mdl + 1), (ushort)(_ioIndex), Convert.ToUInt16(_ioLevel));
                GetDoBit();
            }
            return _ioLevel;
        }
    }
    #endregion
}
using BmDriverCLR;
using ClassLib_Motion;
using ClassLib_ParaFile;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ClassLib_MotionStd
{
    public class MotionBomming:AbsMotion
    {
        public MotionBomming()
        {

        }

        ~MotionBomming()
        {

        }

        public override AbsAxis CreateAxis(short virtualIndex, short index, string name, double pitch, double divide)
        {
            return new BommingAxis(virtualIndex, index, name, pitch, divide);
        }

        public override AbsCard CreateCard(int iCard,string IniFileName)
        {
            return new BommingCard(iCard,IniFileName);
        }

        public override AbsIo CreateIo(string str)
        {
            return new BommingIO(str);
        }
    }

    public class AxisStopModeConfig
    {
        public int Mode { get; set; }
        public int Distance { get; set; }
    }

    public class BommingMotionHelper
    {
        private readonly ILog log;
        BommingMotionHelper()
        {
            mMotionFactory = new MotionCotrolFactoryCli();
            mMotion = mMotionFactory.CreateMotionControl("BMMotion", "Smart");
            log = LogManager.GetLogger(typeof(BommingMotionHelper));
        }
        ~BommingMotionHelper() { }

        MotionCotrolFactoryCli mMotionFactory;
        MotionCtrolCli mMotion;
        Dictionary<int, AxisStopModeConfig> mAxisStopModeCfgs = new Dictionary<int, AxisStopModeConfig>();
        static readonly BommingMotionHelper mInstance = new BommingMotionHelper();
        bool mIsOpened = false;
        bool mThreadCond = true;
        public bool[] mIO = new bool[2];
        public bool[] mCommonIO = new bool[1];
        public bool mAlarm = false;
        public bool[] mLimit = new bool[8];
        public bool[] mAxisState = new bool[4];

        readonly int LIMIT_X_POSITIVE = 0;
        readonly int LIMIT_X_NEGATIVE = 1;
        readonly int LIMIT_Y_POSITIVE = 2;
        readonly int LIMIT_Y_NEGATIVE = 3;
        readonly int LIMIT_Z_POSITIVE = 4;
        readonly int LIMIT_Z_NEGATIVE = 5;
        readonly int LIMIT_G_POSITIVE = 6;
        readonly int LIMIT_G_NEGATIVE = 7;

        public static BommingMotionHelper GetInstance()
        {
            return mInstance;
        }

        public string IniFile { get; set; }
        public short OpenCard()
        {
            if(null != mMotion)
            {
                string sCardFile = ParaFileINI.ReadINI("CARD", "Card0", IniFile);
                sCardFile = Publics.GetGtsPath() + sCardFile + ".ini";
                
                string strConfigFile = ParaFileINI.ReadINI("Setting", "ConfigFile", sCardFile);
                string strMotionLineFile = ParaFileINI.ReadINI("Setting", "MotionLineFile", sCardFile);
                string strContent = File.ReadAllText(strConfigFile);
                mAxisStopModeCfgs = JsonConvert.DeserializeObject<Dictionary<int, AxisStopModeConfig>>(strContent);

                int ret = mMotion.Initialize(sCardFile);
                if(0 == ret)
                {
                    ret = mMotion.LoadMotionLinesData(strMotionLineFile);
                    if(0 == ret)
                    {
                        ret = mMotion.SetBreak(0, true);
                        //TODO 可能需要设置光电报警开关
                        //if(0 == ret) { ret = mMotion.GoHome(); }
                        if(null != mAxisStopModeCfgs)
                        {
                            foreach (var item in mAxisStopModeCfgs)
                            {
                                mMotion.SetStopMode(item.Key, (RefStopMode)item.Value.Mode, item.Value.Distance);
                            }
                        }
                        Thread t = new Thread(new ThreadStart(delegate ()
                        {
                            int count = 0;
                            bool bVal = false;
                            while(mThreadCond)
                            {
                                if (0 == count % 2)
                                {
                                    string strKey = mMotion.GetKeyDown(0);
                                    string[] keyState = strKey.Split(',');
                                    if (4 == keyState.Length)
                                    {
                                        mIO[0] = Convert.ToInt16(keyState[1]) == 1;
                                        mIO[1] = Convert.ToInt16(keyState[3]) == 1;
                                    }
                                }
                                //1：X轴正限位    2：X轴负限位
                                //3：Y轴正限位    4：Y轴负限位
                                //5：Z轴正限位    6：Z轴负限位
                                //7：光轴正限位   8：光轴负限位
                                int st = 0;
                                for(int i=0;i<8;i++)
                                {
                                    ret = mMotion.GetLightElectrictyLimit(i, ref st);
                                    if(0 == ret) { mLimit[i] = (st == 1); }
                                }

                                for(int i=0;i<4;i++)
                                {
                                    ret = mMotion.CheckInp(i, ref bVal);
                                    if(0 == ret) {
                                        mAxisState[i] = bVal;
                                        log.Info(String.Format("mAxisState[{0}] :{1}",i,bVal));
                                    }
                                }
                                ret = mMotion.GetInput(2, ref bVal);
                                if(0 == ret) { mCommonIO[0] = bVal; }
                                //for(int j=0;j<2;j++)
                                //{
                                //    DateTime tNow = DateTime.Now;
                                //    TimeSpan tSpan = tNow - mOutputTime[j];
                                //    if(tSpan.TotalMilliseconds >= 1000)
                                //    {
                                //        mOutput[j] = false;
                                //    }
                                //}
                               
                                count++;
                                Thread.Sleep(200);
                            }
                        }));
                        t.Start();
                        mIsOpened = true;
                    }
                    return (short)ret;
                }
                else
                {
                    MessageBox.Show("主板连接失败!");
                }
            }
            return 1;
        }

        public short CloseCard()
        {
            if(null != mMotion)
            {
                int ret = mMotion.SetBreak(0, false);
                mThreadCond = false;
                mIsOpened = false;
                if(0 == ret) { return (short)mMotion.DeInitialize(); }
            }
            return 1;
        }

        //public int SetStopMode(int axis)
        //{
        //    if(null != mMotion)
        //    {
        //        //return mMotion.SetStopMode(axis, mode, distance);
        //    }
        //    return 1;
        //}

        bool[] mOutput = new bool[2];
        DateTime[] mOutputTime = new DateTime[2];
        public bool SetIO(int bitNo,bool st)
        {
            if(null != mMotion && mIsOpened && 2 > bitNo)
            {
                int ret = 0;
                if(st)
                {
                    ret = mMotion.TrigCamera(bitNo);
                }
                mOutput[bitNo] = st;
                mOutputTime[bitNo] = DateTime.Now;
                return (0 == ret);
            }
            return false;
        }

        public bool GetDiBit(int bitNo)
        {
            if(0 == bitNo) { return mCommonIO[0]; }
            return false;
        }

        public int GetEncodePos(int axis,ref int pos)
        {
            if(null != mMotion && mIsOpened)
            {
                int ret = mMotion.GetEncoderPos(axis, ref pos);
                return ret;
            }
            return 1;
        }

        public int GetMotionPos(int axis, ref int pos)
        {
            if (null != mMotion && mIsOpened)
            {
                int ret = mMotion.GetMotionPos(axis);
                pos = ret;
                return ret;
            }
            return 1;
        }

        public bool GetDoBit(short bitNo)
        {
            if(2 > bitNo)
            {
                return mOutput[bitNo];
            }
            return false;
        }

        public bool Moving(short _axis)
        {
            if(4 > _axis)
            {
                log.Info(String.Format("Moving mAxisState[{0}]  :{1}", _axis, mAxisState[_axis]));
                return mAxisState[_axis];
            }
            return false;
        }

        public int Stop(int axis,bool option)
        {
            if (null != mMotion && mIsOpened)
            {
                if (option) { return mMotion.StopMoveInstant(axis); }
                return mMotion.StopMoving(axis);
            }
            return 1;
        }

        public long GetPos(short _axis)
        {
            if (null != mMotion && mIsOpened)
                return mMotion.GetMotionPos(_axis);
            return 1;
        }

        public short SetPos(short _axis, long pos)
        {
            if (null != mMotion && mIsOpened)
                return (short)mMotion.MoveTo(_axis,(int) pos, false);
            return 1;
        }

        public short SetVel(short _axis, double vel)
        {
            if (null != mMotion && mIsOpened)
                return (short)mMotion.SetVelocity(_axis, (float)vel);
            return 1;
        }

        public bool GetEMGState(short _axis)
        {
            bool res = false;
            if (null != mMotion && mIsOpened)
            {
                int ret = mMotion.CheckEMGState(_axis, ref res);
                if (0 != ret) { res = false; }
            }
            return res;
        }

        public bool MoveTo(int axis,double pos, double vel)
        {
            if (null != mMotion && mIsOpened)
            {
                int ret = mMotion.SetVelocity(axis, (float)vel);
                ret = mMotion.MoveTo(axis, (float)vel,(int)pos, true);
                return 0 == ret;
            }
            return false;
        }

        public bool Move(short _axis, double pos, double vel)
        {
            if (null != mMotion && mIsOpened)
            {
                int ret = mMotion.SetVelocity(_axis, (float)vel);
                ret = mMotion.Move(_axis, (float)vel, (int)pos, true);
                return 0 == ret;
            }
            return false;  
        }

        public enum BrightChannel
        {
            BC_RINGA,
            BC_RINGB,
            BC_RINGC,
            BC_RINGD,
            BC_ZERORING,
            BC_BACK,
            BC_COAXIAL,
            BC_HIGH
        }

        public int SetBright(BrightChannel channel,int value)
        {
            if (null != mMotion && mIsOpened)
            {
                Dictionary<BrightChannel, int> mapChannel = new Dictionary<BrightChannel, int>();
                mapChannel.Add(BrightChannel.BC_RINGA,1);
                mapChannel.Add(BrightChannel.BC_RINGB, 2);
                mapChannel.Add(BrightChannel.BC_RINGC, 3);
                mapChannel.Add(BrightChannel.BC_RINGD, 4);
                mapChannel.Add(BrightChannel.BC_ZERORING, 0);
                mapChannel.Add(BrightChannel.BC_BACK, 30);
                mapChannel.Add(BrightChannel.BC_COAXIAL, 31);
                mapChannel.Add(BrightChannel.BC_HIGH, 99);
                if (mapChannel.ContainsKey(channel))
                {
                    return mMotion.SetBright(mapChannel[channel], value);
                }
            }
            return 1;
        }

        internal bool GetKeyDown(int zin)
        {
            return mIO[zin];
        }

        internal void StartMoving(int axis, bool dir)
        {
            if(null != mMotion && mIsOpened)
            {
                mMotion.StartMoving(axis,20000, dir);
            }
        }
    }

    public class BommingCard:AbsCard
    {
        BommingMotionHelper mHelper = null;
        public BommingCard(int iCard,string IniFileName)
        {
            mHelper = BommingMotionHelper.GetInstance();
            mHelper.IniFile = IniFileName;
            OpenCard();
        }

        public override short CloseCard()
        {
            return mHelper.CloseCard();
        }

        public override short ClrStsAll()
        {
            return 1;
            //throw new NotImplementedException();
        }

        public override short OpenCard()
        {
            return mHelper.OpenCard();
        }
    }

    public class BommingAxis : AbsAxis
    {
        BommingMotionHelper mHelper = null;

        public BommingAxis(short virtualIndex, short index, string name, double pitch, double divide)
            : base(virtualIndex, index, name, pitch, divide)
        {
            mHelper = BommingMotionHelper.GetInstance();
        }

        public override short AddComparePoints(short hcmp, int pos, short dir = 0)
        {
            return 0;
        }

        public override short AxisOff()
        {
            return 0;
        }

        public override short AxisOn(bool bExp = true)
        {
            return 0;
        }

        public override short ClearComparePoints(short hcmp = 0)
        {
            return 0;
        }

        public override short ClrSts(short count = 1)
        {
            return 0;
        }

        public override bool GetAlarm(bool bGetSts = false)
        {
            return mHelper.mAlarm;
        }

        public override double GetEncPos(short count = 1)
        {
            int pos = 0;
            int ret = mHelper.GetEncodePos(_axis, ref pos);
            double dValue = pos;
            dValue = PulseToMm(dValue);
            return dValue;
        }

        public override bool GetLimit(bool bPlimit = true, bool bGetSts = false)
        {
            int index = 2*_axis;
            if (!bPlimit) index += 1;
            if(index < 8)
            {
                return mHelper.mLimit[index];
            }
            return false;
        }

        public override bool GetOnSts(bool bGetSts = false)
        {
            return false;
            //throw new NotImplementedException();
        }

        public override double GetPrfPos(short count = 1)
        {
            int pos = 0;
            int ret = mHelper.GetMotionPos(_axis, ref pos);
            double dValue = pos;
            dValue = PulseToMm(dValue);
            return dValue;
        }

        public override bool GetStop(bool bGetSts = false)
        {
            return mHelper.GetEMGState(_axis);
        }

        public override int GetSts(short count = 1)
        {
            //TODO 限位、报警、

            return 0;
        }

        public override short LmtsOff(LimitPN limitType = LimitPN.ALL)
        {
            return 0;
        }

        public override short LmtsOn(LimitPN limitType = LimitPN.ALL)
        {
            return 0;
        }

        public override bool Moving(bool bGetSts = true)
        {
            return mHelper.Moving(_axis);
        }

        public override short SetCompareConfig(bool enable, short hcmp = 0)
        {
            return 0;
        }

        public override short SetEncPos(int encPos = 99999999)
        {
            return 0;
        }

        public override short SetJogPara(double acc, double smooth, bool bSave = true)
        {
            return 0;
        }

        public override short SetPrfPos(long prfPos)
        {
            return 0;
        }

        public override short SetTrapPara(double acc, double smoothTime, double vel, bool bSave = true)
        {
            return 0;
        }

        public override short StepPulseDir(short pulse, short dir)
        {
            return 0;
        }

        public override short Stop(bool option = true)
        {
            return (short)mHelper.Stop(_axis,option);
        }

        public override short Update()
        {
            return 1;
            //throw new NotImplementedException();
        }

        public override short ZeroPos(short count = 1)
        {
            return 0;
        }

        protected override long GetPos(bool bSave = true)
        {
            long Pos = mHelper.GetPos(_axis);
            if (bSave)
                m_DesPrm.pos = Pos;
            return Pos;
        }

        protected override long GetPrfMode(short count = 1)
        {
            return 0;
        }

        protected override double GetVel(bool bSave = true)
        {
            return 0;
        }

        protected override short SetPos(long pos, bool bSave = true)
        {
            if(bSave) { m_DesPrm.pos = pos; }
            return mHelper.SetPos(_axis, pos);
        }

        protected override short SetVel(double vel, bool bSave = true)
        {
            if(bSave) { m_DesPrm.vel = vel; }
            return mHelper.SetVel(_axis, vel);
        }

        public override bool MoveAbsolute(double pos, double vel, double acc = AbsMotion.ConstDebugAcc, bool bWaitArrive = false, short smoothTime = 25, double pos_flex = 0, double vel_low = 0)
        {
            vel = 20000;
            pos = MmToPulse(pos);
            return mHelper.MoveTo(_axis,pos,vel);
        }

        public override bool MoveRelative(double pos, double vel, double acc = AbsMotion.ConstDebugAcc, bool bWaitArrive = false, bool bEncPos = false, short smoothTime = 25)
        {
            vel = 20000;
            pos = MmToPulse(pos);
            return mHelper.Move(_axis, pos, vel);
        }

        public override bool MoveJog(double vel = 0, double acc = AbsMotion.ConstDebugAcc, bool bWaitStop = false, double smooth = 0.9, bool bAnysStop = false)
        {
            bool dir = vel > 0;
            mHelper.StartMoving(_axis,dir);
            return true;
        }
    }

    public class BommingIO : AbsIo
    {
        BommingMotionHelper mHelper = null;
        public BommingIO(string cMark,string cName = "")
            :base(cMark,cName)
        {
            mHelper = BommingMotionHelper.GetInstance();
        }

        ~BommingIO() { }
        public override bool ReadBit()
        {
            return GetDiBit();
        }
        public override bool SetDoBit(bool ioLevel)
        {
            return mHelper.SetIO(_ioIndex, ioLevel);
        }

        enum ZYINPUT
        {
            ZYIN_MEASUREMENT,
            ZYIN_PRINT,
            ZYIN_EMEGY
        }
        protected override bool GetDiBit()
        {
            if ((int)ZYINPUT.ZYIN_MEASUREMENT == _ioIndex || (int)ZYINPUT.ZYIN_PRINT == _ioIndex)
            {
                return mHelper.GetKeyDown(_ioIndex);
            }
            else
            {
                return mHelper.GetDiBit(_ioIndex);
            }
        }

        protected override bool GetDoBit()
        {
            return mHelper.GetDoBit(_ioIndex);
        }
    }
}

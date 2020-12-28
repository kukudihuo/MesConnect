using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLib_ParaFile;
using csIOC0640;

namespace ClassLib_Motion
{
    public class IOsIOC0640 : OtherIOs
    {
        public IOsIOC0640()
            :base()
        {
        }
        ~IOsIOC0640()
        {
        }
        public override AbsCard CreateCard(int iCard, string IniFileName)
        {
            //获取主卡及扩展卡配置文件路径
            short mdl = Convert.ToInt16((0 == iCard && AbsMotion.EXT_CARD_TOTAL > 0) ? (AbsMotion.EXT_CARD_TOTAL - 1) : -1);
            return new IOC0640Card(Convert.ToInt16(iCard), mdl);
        }
        public override AbsIo CreateIo(string str)
        {
            return new IOC0640Io(str);
        }
    }

    //初始化卡
    public class IOC0640Card : AbsCard
    {
        protected short _mdl;
        protected bool _bHaveFather;
        protected string _sExtFile = "";
        private static bool s_bFirst = true;

        public IOC0640Card(short cardNum, short mdl = -1)
        {
            _cardNum = cardNum;
            _mdl = mdl;
            OpenCard();
        }
        ~IOC0640Card()
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
                m_bCardOK = true;
                short srn = (short)IOC0640.ioc_board_init();
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
                    IOC0640.ioc_board_close();
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

    //输入输出
    public class IOC0640Io : AbsIo
    {
        public IOC0640Io(string cMark, string cName = "")
            : base(cMark, cName)
        {
        }
        ~IOC0640Io()
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
            O_ioValue[_cardNum, _ioType] = IOC0640.ioc_read_outport((ushort)_cardNum, (ushort)(_mdl + 1));
            _ioLevel = Convert.ToBoolean(O_ioValue[_cardNum, _ioType] & (1 << (_ioIndex)));
            return _ioLevel;
        }
        override protected bool GetDiBit()
        {
            //一次读_ioType的一组IO，再通过_ioIndex取当前的IO值，所有的值再通过ReadAllIo读出来
            O_ioValue[_cardNum, _ioType] = IOC0640.ioc_read_inport((ushort)_cardNum, (ushort)(_mdl + 1));
            _ioLevel = Convert.ToBoolean(O_ioValue[_cardNum, _ioType] & (1 << (_ioIndex)));
            //UpDateShow();
            return _ioLevel;
        }
        override public bool SetDoBit(bool ioLevel)
        {
            if (_bdo)
            {
                _ioLevel = ioLevel;
                IOC0640.ioc_write_outbit((ushort)_cardNum, (ushort)(_ioIndex), Convert.ToUInt16(_ioLevel));
                GetDoBit();
                IOC0640.ioc_write_outport(1, 13, 0);
                int aa = IOC0640.ioc_read_outbit(1,13);
                int bb = IOC0640.ioc_read_outport(1, 13);
                O_ioValue[_cardNum, _ioType] = IOC0640.ioc_read_outport((ushort)_cardNum, (ushort)(_mdl + 1));
                IOC0640.ioc_write_outport(1, 13, 1);
                aa = IOC0640.ioc_read_outbit(1, 13);
                bb = IOC0640.ioc_read_outport(1, 13);
                O_ioValue[_cardNum, _ioType] = IOC0640.ioc_read_outport((ushort)_cardNum, (ushort)(_mdl + 1));
            }
            return _ioLevel;
        }
    }
}

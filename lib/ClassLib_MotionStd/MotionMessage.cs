using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClassLib_Motion
{
    /// <summary>
    /// 用于响应操作点击提示
    /// </summary>
    public class MessageMotion : AbsMotion
    {
        public MessageMotion()
        {
        }
        ~MessageMotion()
        {
        }

        public override AbsAxis CreateAxis(short virtualIndex, short index, string name, double pitch, double divide)
        {
            int iCard = (index - 1) / AbsMotion.AXIS_IN_CARD;
            return new MessageAxis(virtualIndex, index, name, pitch, divide);
        }

        public override AbsCard CreateCard(int iCard, string IniFileName)
        {
            return new MessageCard(Convert.ToInt16(iCard));
        }

        public override AbsIo CreateIo(string str)
        {
            int iCard = 0;
            string[] sst = str.Trim().Split('_');
            if (sst.Count() > 1)
                iCard = int.Parse(sst[1]);

            return new MessageIo(str);
        }
    }
    //初始化卡
    public class MessageCard : AbsCard
    {
        public MessageCard(short cardNum)
        {
            _cardNum = cardNum;
        }
        ~MessageCard()
        {
            Dispose();
        }
        public override short CloseCard()
        {
            return 0;//throw new NotImplementedException();
        }

        public override short ClrStsAll()
        {
            return 0;//throw new NotImplementedException();
        }

        public override short OpenCard()
        {
            return 0;//throw new NotImplementedException();
        }
    }

    //输入输出
    public class MessageIo : AbsIo
    {
        //TShape *pShape; //关联的显示控件，用于自动更新控件颜色
        public MessageIo(string cMark, string cName = "")
            : base(cMark, cName)
        {
        }
        ~MessageIo()
        {
            Dispose();
        }
        new public void Dispose()
        {
        }
        public override bool SetDoBit(bool ioLevel)
        {
            if (_mdl < 0)//运控卡
                MessageBox.Show("运动控制卡 " + _cMark.ToString() + " " + m_sName.ToString());
            else
                MessageBox.Show("扩展卡 " + _cMark.ToString() + " " + m_sName.ToString());

            return true;
        }

        protected override bool GetDiBit()
        {
            return false;
        }

        protected override bool GetDoBit()
        {
            return false;
        }
    }

    //轴
    public class MessageAxis : AbsAxis
    {
        public MessageAxis(short virtualIndex, short index, string name, double pitch, double divide)
            :  base(virtualIndex, index, name, pitch, divide)
        {
        }
        ~MessageAxis()//bool disposing
        {
            Dispose();
        }
        //protected override void Dispose(bool disposing)//bool disposing
        new public void Dispose()//bool disposing
        {
        }
        public override short AddComparePoints(short hcmp, int pos, short dir = 0)
        {
            return 0;//throw new NotImplementedException();
        }

        public override short AxisOff()
        {
            return 0;//throw new NotImplementedException();
        }

        public override short AxisOn(bool bExp = true)
        {
            return 0;
        }

        public override short ClearComparePoints(short hcmp = 0)
        {
            return 0;//throw new NotImplementedException();
        }

        public override short ClrSts(short count = 1)
        {
            return 0;
        }

        public override bool GetAlarm(bool bGetSts = false)
        {
            return false; //throw new NotImplementedException();
        }

        public override double GetEncPos(short count = 1)
        {
            return 6;
       }

        public override bool GetLimit(bool bPlimit = true, bool bGetSts = false)
        {
            return false;
        }

        public override bool GetOnSts(bool bGetSts = false)
        {
            return false;// throw new NotImplementedException();
        }

        public override double GetPrfPos(short count = 1)
        {
            return 0;
        }

        public override bool GetStop(bool bGetSts = false)
        {
            return false;// throw new NotImplementedException();
        }

        public override int GetSts(short count = 1)
        {
            return 0;// throw new NotImplementedException();
        }

        public override short LmtsOff(LimitPN limitType = LimitPN.ALL)
        {
            return 0;// throw new NotImplementedException();
        }

        public override short LmtsOn(LimitPN limitType = LimitPN.ALL)
        {
            return 0;
        }

        public override bool Moving(bool bGetSts = true)
        {
            return false;// throw new NotImplementedException();
        }

        public override short SetCompareConfig(bool enable, short hcmp = 0)
        {
            return 0;//  throw new NotImplementedException();
        }

        public override short SetEncPos(int encPos = 99999999)
        {
            return 0;// throw new NotImplementedException();
        }

        public override short SetJogPara(double acc, double smooth, bool bSave = true)
        {
            MessageBox.Show("Jog运动");
            return 0;
        }

        public override short SetPrfPos(long prfPos)
        {
            return 0;// throw new NotImplementedException();
        }

        public override short SetTrapPara(double acc, double smoothTime, double vel, bool bSave = true)
        {
            MessageBox.Show("点动");
            return 0;
        }

        public override short StepPulseDir(short pulse, short dir)
        {
            return 0;// throw new NotImplementedException();
        }

        public override short Stop(bool option = true)
        {
            return 0;
        }

        public override short Update()
        {
            return 0;
        }

        public override short ZeroPos(short count = 1)
        {
            MessageBox.Show("位置清零");
            return 1;
        }

        protected override long GetPos(bool bSave = true)
        {
            return 0;// throw new NotImplementedException();
        }

        protected override long GetPrfMode(short count = 1)
        {
            return 0;// throw new NotImplementedException();
        }

        protected override double GetVel(bool bSave = true)
        {
            return 0;// throw new NotImplementedException();
        }

        protected override short SetPos(long pos, bool bSave = true)
        {
            return 0;
        }

        protected override short SetVel(double vel, bool bSave = true)
        {
            return 0;
        }
    }
}

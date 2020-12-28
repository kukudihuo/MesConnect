using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClassLib_MotionUI
{
    public partial class Control_IO_Two : UserControl
    {
        public Control_IO_Two()
        {
            InitializeComponent();
        }

        [CategoryAttribute("自定义属性"), DescriptionAttribute("IO名称1：(0-15)输入EXI0,DXI0,LIMIT0+,LIMIT0-,HOME0,输出EXO0,DXO0;_后为0开始的卡号")]
        public string IO_Name1
        {
            get
            {
                return control_IO_One1.IO_Name;
            }
            set
            {
                control_IO_One1.IO_Name = value;
            }
        }

        [CategoryAttribute("自定义属性"), DescriptionAttribute("IO名称2：(0-15)输入EXI0,DXI0,LIMIT0+,LIMIT0-,HOME0,输出EXO0,DXO0;_后为0开始的卡号")]
        public string IO_Name2
        {
            get
            {
                return control_IO_One2.IO_Name;
            }
            set
            {
                control_IO_One2.IO_Name = value;
            }
        }

        [CategoryAttribute("自定义事件"), DescriptionAttribute("输出点击触发前事件")]
        public event DoBeginCheckdelegate DoBeginCheckEvent = null;

        private void control_IO_One1_DoCheckEvent(object sender, EventArgs e)
        {
            //control_IO_One2.TurnOverDo();
            control_IO_One2.io.SetDoBit(!control_IO_One1.io.GetIoBit());
        }

        private void control_IO_One2_DoCheckEvent(object sender, EventArgs e)
        {
            //control_IO_One1.TurnOverDo();
            control_IO_One1.io.SetDoBit(!control_IO_One2.io.GetIoBit());
        }

        /// <summary>
        /// 设置第一个输出的状态，并设置第二个输出为取反
        /// </summary>
        /// <param name="bTrue"></param>
        public void SetDoOne(bool bTrue)
        {
            control_IO_One1.io.SetDoBit(bTrue);
            control_IO_One2.io.SetDoBit(!bTrue);
        }

        private bool control_IO_One1_DoBeginCheckEvent(object sender, EventArgs e)
        {
            if (control_IO_One1.IO_Name.Contains("DXO") || control_IO_One1.IO_Name.Contains("EXO"))
            {
                if (DoBeginCheckEvent != null)
                {
                    return DoBeginCheckEvent(control_IO_One1.IO_Name, null);
                }
            }
            return true;
        }
    }
}

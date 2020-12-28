using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClassLib_Motion;
using ClassLib_RunMode;

namespace ClassLib_MotionUI
{
    public delegate bool DoBeginCheckdelegate(object sender, EventArgs e);
    public partial class Control_IO_One : UserControl
    {
        public AbsIo io = null;
        string _IO_Name = "EXI0_0";
        [CategoryAttribute("自定义属性"), DescriptionAttribute("IO名称：(0-15)输入EXI0,DXI0,LIMIT0+,LIMIT0-,HOME0,输出EXO0,DXO0;_后为0开始的卡号")]
        public string IO_Name
        {
            get
            {
                return _IO_Name;
            }
            set
            {
                _IO_Name = value;
                _IO_Name = _IO_Name.ToUpper();

                RefreshText(_IO_Name);
            }
        }

        [CategoryAttribute("自定义事件"), DescriptionAttribute("输出点击触发后事件")]
        public event System.EventHandler DoCheckEvent = null;

        [CategoryAttribute("自定义事件"), DescriptionAttribute("输出点击触发前事件")]
        public event DoBeginCheckdelegate DoBeginCheckEvent = null;

        void RefreshText(string ioName)
        {
            if (!string.IsNullOrEmpty(ioName))
            {
                int pos = ioName.IndexOf('=');
                if (pos <= 0)
                    pos = ioName.IndexOf(' ');
                if (pos > 0)
                {
                    Label_IO.Text = ioName.Substring(0, pos).Trim();

                    if (pos < ioName.Length)
                        Label_Name.Text = ioName.Substring(pos + 1);
                }
                else
                    Label_IO.Text = ioName;
                //io = Motion.g_Motion.GetIo(_IO_Name);
                //if (io != null)
                //{
                //    Label_IO.Text = io.m_sName;  
                //    Label_Name.Text = "";
                //}
            }
        }

        public Control_IO_One()
        {
            InitializeComponent();

            if (!m_ArrayList_Controls.Contains(this))
                m_ArrayList_Controls.Add(this);
        }
        public static System.Collections.ArrayList m_ArrayList_Controls = new System.Collections.ArrayList(20);
        private void Label_IO_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (MouseButtons.Left == e.Button && (_IO_Name.Contains("DXO") || _IO_Name.Contains("EXO")))
                {
                    ClickIO(sender, e);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }
        public void ClickIO(object sender, EventArgs e)
        {
                    if (DoBeginCheckEvent != null)
                    {
                        if (!DoBeginCheckEvent(_IO_Name, e))
                            return;
                    }
                    TurnOverDo();

                    if (DoCheckEvent != null)
                    {
                DoCheckEvent(_IO_Name, e);
                    }
                }
        public bool bIOSelected = false;
        private void 设置为快捷点击IOToolStripMenuItem_Click(object sender, EventArgs e)
            {
            SetIOSelect();
            }

        private void 清除快捷点击IOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearIOSelect();
        }
        private void SetIOSelect()
        {
            ClearIOSelect();
            this.bIOSelected = true;
        }
        public static void ClearIOSelect()
        {
            foreach(Control_IO_One kvp in m_ArrayList_Controls)
            {
                kvp.bIOSelected = false;
            }
        }
        public static Control_IO_One GetIOSelected()
        {
            foreach (Control_IO_One kvp in m_ArrayList_Controls)
            {
                if (kvp.bIOSelected)
                    return kvp;
            }
            return null;
        }
        public static void ChangeIO()
        {
            if (Run.GetRunning())
            {
                ClearIOSelect();
                return;
            }
            Control_IO_One control = GetIOSelected();
            if (control == null)
                return;
            control.ClickIO(null, null);
        }

        public void SetDo(bool bTrue)
        {
            Motion.SetDo(_IO_Name, bTrue);
        }
        public bool GetIo()
        {
            return Motion.GetIo(_IO_Name);
        }
        public void TurnOverDo()
        {
            bool bLevel = Motion.ReadIo(_IO_Name);//GetIo
            bLevel = !bLevel;
            Motion.SetDo(_IO_Name, bLevel);        
        }

        public override void Refresh()
        {
            if (io == null)
            {
                io = Motion.GetDicIo(_IO_Name);
                if (io != null)
                {
                    RefreshText(io.GetMark() + "=" + io.m_sName);
                    //Label_IO.Text = io.m_sName;
                }
            }
            if (Motion.ReadIo(IO_Name))
            {
                if (IO_Name.Contains("XO"))//EXO
                {
                    Label_IO.BackColor = System.Drawing.Color.Green;
                    Label_IO.ForeColor = System.Drawing.Color.White;
                }
                else
                {
                    Label_IO.BackColor = System.Drawing.Color.LimeGreen;
                    Label_IO.ForeColor = System.Drawing.Color.Blue;           
                    //Label_IO.BackColor = System.Drawing.Color.Red;
                    //Label_IO.Image = ClassLib_MotionUI.Properties.Resources.ball_green;
                }
            }
            else
            {
                Label_IO.BackColor = System.Drawing.Color.Gray;
                if (IO_Name.Contains("XO"))//EXO
                    ;//Label_IO.ForeColor = System.Drawing.Color.Black;
                else 
                {
                    //Label_IO.Image = ClassLib_MotionUI.Properties.Resources.ball_red;
                    Label_IO.BackColor = System.Drawing.Color.Red;
                    Label_IO.ForeColor = System.Drawing.Color.White;  
                }           
            }           
        }
    }
}


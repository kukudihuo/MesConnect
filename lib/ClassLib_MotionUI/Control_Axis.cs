using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using ClassLib_Motion;
using ClassLib_ParaFile;
using ClassLib_RunMode;

namespace ClassLib_MotionUI
{
    public partial class Control_Axis : UserControl
    {
        public static System.Collections.ArrayList m_ArrayList_Controls = new System.Collections.ArrayList(100);
        public bool BeAlarm = false;
        public static void GetAllControlsRecursion(System.Windows.Forms.Control.ControlCollection _Controls)
        {
            Publics.GetAllControlsRecursion(typeof(Control_Axis), _Controls, ref m_ArrayList_Controls);
        }
        public bool bSelectdeMove = false;
        public bool IsReseted = false;
        AbsAxis _Axis = null;
        public AbsAxis GetAixs() { return _Axis; }
        int _AxisNum = -1;
        [CategoryAttribute("自定义属性"), DescriptionAttribute("轴号：从1开始到100")]
        public int AxisNum //readonly
        {
            get
            {
                return _AxisNum;
            }
            set
            {
                _AxisNum = value;
                _Axis = Motion.GetAxis(_AxisNum);
                control_AxisShort.AxisNum = AxisNum;
                if (null != _Axis)
                    Label_AxisName.Text = _Axis.m_sName;
            }
        }
        [CategoryAttribute("自定义属性"), DescriptionAttribute("轴号：从1开始到100")]
        public AbsAxis Axis //readonly
        {
            get
            {
                return _Axis;
            }
        }
        [CategoryAttribute("自定义属性"), DescriptionAttribute("方向是否反向")]
        public bool DirNeget //readonly
        {
            get
            {
                return control_AxisShort.DirNeget;
            }
            set
            {
                control_AxisShort.DirNeget = value;
            }
        }
        [CategoryAttribute("自定义属性"), DescriptionAttribute("左按键显示内容")]
        public string LeftLabel //readonly
        {
            get
            {
                return control_AxisShort.LeftLabel;
            }
            set
            {
                control_AxisShort.LeftLabel = value;
            }
        }
        [CategoryAttribute("自定义属性"), DescriptionAttribute("右按键显示内容")]
        public string RightLabel //readonly
        {
            get
            {
                return control_AxisShort.RightLabel;
            }
            set
            {
                control_AxisShort.RightLabel = value;
            }
        }

        [CategoryAttribute("自定义属性"), DescriptionAttribute("是否运行复位")]
        public bool ResetEnabled
        {
            get
            {
                return Button_Reset.Enabled;
            }
            set
            {
                Button_Reset.Enabled = value;
            }
        }
        [CategoryAttribute("自定义属性"), DescriptionAttribute("显示名字")]
        public string LabelName //readonly
        {
            get
            {
                return Label_AxisName.Text;
            }
            set
            {
                Label_AxisName.Text = value;
                //control_AxisShort.LabelName = value;
            }
        }
        [CategoryAttribute("自定义属性"), DescriptionAttribute("默认是否可移动")]
        public bool Moveble //readonly
        {
            get
            {
                return control_AxisShort.Moveble;
            }
            set
            {
                control_AxisShort.Moveble = value;
            }
        }

        public void ChangeVel()
        {
            _Axis.ChangeVel(Convert.ToDouble(textBox_Vel.Text));
        }
        public void ReadVel()
        {
            textBox_Vel.Text = _Axis.ReadVel().ToString();
        }

        public void ChangeAcc()
        {
            _Axis.ChangeAcc(Convert.ToDouble(textBox_Acc.Text));
        }
        public void ReadAcc()
        {
            textBox_Acc.Text = _Axis.ReadAcc().ToString();
        }
        //[CategoryAttribute("自定义属性"), DescriptionAttribute("轴速度")]
        //public string AxisVel
        //{
        //    get
        //    {
        //        if (_Axis != null)
        //            _Axis.SetVel(Convert.ToDouble(textBox_Vel.Text));
        //        return textBox_Vel.Text;
        //    }
        //    set
        //    {
        //        textBox_Vel.Text = value;
        //    }
        //}
        //[CategoryAttribute("自定义属性"), DescriptionAttribute("轴加速度")]
        //public string AxisAcc
        //{
        //    get
        //    {
        //        return textBox_Acc.Text;
        //    }
        //    set
        //    {
        //        textBox_Acc.Text = value;
        //    }
        //}
        

        public delegate short IsReseted_EventHandler(object sender, EventArgs e);
        [CategoryAttribute("自定义事件"), DescriptionAttribute("单轴复位完成回调函数")]
        public event IsReseted_EventHandler IsReseted_Event = null;
        public virtual short Reseted_Changed(EventArgs e = null)
        {
            if (IsReseted_Event != null)
            {
                return IsReseted_Event(this, e);
            }
            return -1;
        }
        public delegate short Alert_EventHandler(object sender, EventArgs e);
        [CategoryAttribute("自定义事件"), DescriptionAttribute("报警事件回调函数")]
        public event Alert_EventHandler Alert_Event = null;
        public virtual short Alert_Event_Function(object sender = null,EventArgs e = null)
        {
            if (Alert_Event != null)
            {
                return Alert_Event(sender, e);
            }
            return -1;
        }

        public Control_Axis()
        {
            InitializeComponent();
        }

        public short control_Thread_Reset_LoadEvent(object sender, EventArgs e)
        {
            //复位当前轴
            _Axis.Reset();
            return 0;
        }

        private void Button_Reset_Click(object sender, EventArgs e)
        {
            control_Thread_Reset.Start();    
        }

        private void CheckBox_ServoOn_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (CheckBox_ServoOn.Checked == true)
                    _Axis.AxisOn();
                else
                    _Axis.AxisOff();
                //需要重新复位
                if (ResetEnabled)
                    _Axis.m_ResetAxis.IsReseted = false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                //MsgBox("伺服使能异常")
            }
        }

        /// <summary>
        /// 定时器调用的函数，用于更新界面
        /// </summary>
        /// <returns>更新界面状态</returns>
        /// <remarks></remarks>
        public override void Refresh()
        {//----------- 读取轴的状态并显示 ------------        
            try
            {
                if (_Axis == null)
                    return;
                //清除轴状态
                _Axis.ClrSts();
                //double currPose = _Axis.GetPrfPos(); //规划位置
                //double encPos = _Axis.GetEncPos();    //编码位置
                //control_AxisShort.Label_ProfilePos.Text = _Axis.PulseToMm(currPose).ToString("0.000");
                //control_AxisShort.Label_EncodePos.Text = _Axis.PulseToMm(encPos).ToString("0.000");
                control_AxisShort.Refresh();
                _Axis.GetSts();
                if (_Axis.GetLimit(false))
                {
                    Label_NegtLimit.BackColor = Color.Red;
                    if (Run.GetRunning())
                    {
                        Motion.StartAlarmThread();
                        Run.SetSoftStop(true, _Axis.m_sName + "在生产过程中感应到负限位,请查找异常原因");
                        ClassLib_MSG.MSG.Show(_Axis.m_sName + "在生产过程中感应到负限位,请查找异常原因");
                    }
                }
                else
                    Label_NegtLimit.BackColor = Color.Gray;
                if (_Axis.GetLimit())
                {
                    Label_PlusLimit.BackColor = Color.Red;
                    if (Run.GetRunning())
                    {
                        Motion.StartAlarmThread();
                        Run.SetSoftStop(true, _Axis.m_sName + "在生产过程中感应到正限位,请查找异常原因");
                        ClassLib_MSG.MSG.Show(_Axis.m_sName + "在生产过程中感应到正限位,请查找异常原因");
                    }
                }
                else
                    Label_PlusLimit.BackColor = Color.Gray;
                if (_Axis.GetAlarm())
                {
                    Label_Alert.BackColor = Color.Red;
                    //BeAlarm = true;
                    Run.SetSoftStop(true, _Axis.m_sName + "轴报警");
                }
                else
                { 
                    Label_Alert.BackColor = Color.Gray;
                    //BeAlarm = false;
                }
                if (_Axis.Moving())
                    Label_MotionStatus.BackColor = Color.Green;
                else
                    Label_MotionStatus.BackColor = Color.Gray;
                if (_Axis.GetOnSts())
                    Label_ServoOn.BackColor = Color.Green;
                else
                    Label_ServoOn.BackColor = Color.Gray;
                if (ResetEnabled)
                {
                    if (_Axis.m_ResetAxis.IsReseted)
                        Label_IsReseted.BackColor = Color.Green;
                    else
                        Label_IsReseted.BackColor = Color.Gray;
                }
                else
                {
                    Label_IsReseted.BackColor = Color.Green;
                    IsReseted = true;
                }
                Label_AxisName.Text = _Axis.m_sName;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        private void Label_IsReseted_BackColorChanged(object sender, EventArgs e)
        {
            if (Label_IsReseted.BackColor == Color.Green)
            {
                IsReseted = true;
                Reseted_Changed(e);
            }
            else
            {
                IsReseted = false;
            }
        }

        private void Label_Alert_BackColorChanged(object sender, EventArgs e)
        {
            if (Label_Alert.BackColor == Color.Red)
            {
                Alert_Event_Function(sender, e);
            }
        }

        public short WaitForReseted()
        {
            while (control_Thread_Reset.IsRunning() || Label_IsReseted.BackColor != Color.Red)
            {
                if (Label_Alert.BackColor == Color.Red)
                {
                    control_Thread_Reset.Abort();
                    return -1;
                }                
                Thread.Sleep(10);
                //Application.DoEvents();
            }
            
            Thread.Sleep(50);
            Application.DoEvents();
            return 0;
        }

        public static Control_Axis MoveSelected()
        {
            foreach (Control_Axis kvp in m_ArrayList_Controls)
            {
                if (kvp.bSelectdeMove)
                    return kvp;
            }
            return null;
        }

        public void SetMoveSelecte()
        {
            ClearMoveSelected();
            this.bSelectdeMove = true;
        }
        public static void ClearMoveSelected()
        {
            foreach (Control_Axis kvp in m_ArrayList_Controls)
            {
                kvp.bSelectdeMove = false;
            }
        }
        public static void MoveNeget()
        {
            Move(false);
        }
        public static void MovePlus()
        {
            Move(true);
        }
        private static void Move(bool plus)
        {
            if (Run.GetRunning())
            {
                ClearMoveSelected();
                return;
            }
            Control_Axis control = MoveSelected();
            if (control == null)
                return;
            if (control._Axis.Moving())
                control._Axis.Stop();
            else
            {
                if (plus)
                    control.control_AxisShort.MovePlus();
                else
                    control.control_AxisShort.MoveNeget();
            }
        }

        private void 选择为快捷移动轴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetMoveSelecte();
        }

        private void 清除快捷移动轴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearMoveSelected();
        }

        //public static System.Collections.ArrayList ArrayList_Controls = new System.Collections.ArrayList(24);
        //public static void RefreshAllUI(System.Windows.Forms.Control.ControlCollection cControls)
        //{
        //    if (ArrayList_Controls.Count == 0)
        //    {
        //        Public.GetAllControlsRecursion(typeof(Control_Axis), cControls, ref ArrayList_Controls);
        //    }
        //    foreach (object obj in ArrayList_Controls)
        //    {
        //        ((Control_Axis)obj).Refresh();
        //    }
        //}
    }
}

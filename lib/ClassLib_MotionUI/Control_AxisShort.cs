using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClassLib_Motion;
using ClassLib_ParaFile;

namespace ClassLib_MotionUI
{
    public partial class Control_AxisShort : UserControl
    {
        AbsAxis _Axis;
        public static System.Collections.ArrayList m_ArrayList_Controls = new System.Collections.ArrayList(100);
        public static void GetAllControlsRecursion(System.Windows.Forms.Control.ControlCollection _Controls)
        {
            Publics.GetAllControlsRecursion(typeof(Control_AxisShort), _Controls, ref m_ArrayList_Controls);
        }
        int _AxisNum = 100;
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
                if (null != _Axis)
                {
                    Label_AxisName.Text = _Axis.m_sName;
                    Enabled = true;
                }
                else
                    Enabled = false;
            }
        }
        [CategoryAttribute("自定义属性"), DescriptionAttribute("显示名字")]
        public string LabelName //readonly
        {
            get
            {
                return Label_AxisName.Text;
            }
            //set
            //{
            //    Label_AxisName.Text = value;
            //}
        }
        bool _DirNeget = false;
        [CategoryAttribute("自定义属性"), DescriptionAttribute("方向是否反向")]
        public bool DirNeget //readonly
        {
            get
            {
                return _DirNeget;
            }
            set
            {
                _DirNeget = value;
            }
        }
        [CategoryAttribute("自定义属性"), DescriptionAttribute("左按键显示内容")]
        public string LeftLabel //readonly
        {
            get
            {
                return Button_Neget.Text;
            }
            set
            {
                Button_Neget.Text = value;
            }
        }
        [CategoryAttribute("自定义属性"), DescriptionAttribute("右按键显示内容")]
        public string RightLabel //readonly
        {
            get
            {
                return Button_Plus.Text;
            }
            set
            {
                Button_Plus.Text = value;
            }
        }
        bool _Moveble = true;
        [CategoryAttribute("自定义属性"), DescriptionAttribute("默认是否可移动")]
        public bool Moveble //readonly
        {
            get
            {
                return _Moveble;
            }
            set
            {
                _Moveble = value;
                Button_Neget.Enabled = _Moveble;
                Button_Plus.Enabled = _Moveble;
            }
        }

        public Control_AxisShort()
        {
            InitializeComponent();
        }

        public void MoveNeget()
        {
            if (CheckBox_MOVE_RES.Checked == true)
            {//点动 
                double pos = Math.Abs(Convert.ToDouble(ComboBox_MoveDis.Text));
                if (_DirNeget)
                    pos = -pos;
                _Axis.MoveRelative(-pos, AbsMotion.ConstDebugVel);
            }
            else
            {//jog
                double vel = Math.Abs(Convert.ToDouble(ComboBox_ContinueSpeed.Text));
                if (_DirNeget)
                    vel = -vel;
                _Axis.MoveJog(-vel);
            }
        }

        private void Button_Neget_MouseDown(object sender, MouseEventArgs e)
        {
            MoveNeget();
        }

        public void MoveStop()
        {
            if (CheckBox_MOVE_RES.Checked == false)
                _Axis.Stop();
        }

        private void Button_Neget_MouseUp(object sender, MouseEventArgs e)
        {
            MoveStop();
        }

        public void MovePlus()
        {
            if (CheckBox_MOVE_RES.Checked == true)
            {//点动 
                double pos = Math.Abs(Convert.ToDouble(ComboBox_MoveDis.Text));
                if (_DirNeget)
                    pos = -pos;
                _Axis.MoveRelative(pos, AbsMotion.ConstDebugVel);
            }
            else
            {//jog
                double vel = Math.Abs(Convert.ToDouble(ComboBox_ContinueSpeed.Text));
                if (_DirNeget)
                    vel = -vel;
                _Axis.MoveJog(vel);
            }
        }

        private void Button_Plus_MouseDown(object sender, MouseEventArgs e)
        {
            MovePlus();
        }

        private void CheckBox_MOVE_RES_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox_MOVE_RES.Checked == true)
            {
                ComboBox_MoveDis.Visible = true;
                ComboBox_ContinueSpeed.Visible = false;
            }
            else
            {
                ComboBox_MoveDis.Visible = false;
                ComboBox_ContinueSpeed.Visible = true;
            }
        }

        /// <summary>
        /// 定时器调用的函数，用于更新界面
        /// </summary>
        /// <returns>更新界面状态</returns>
        /// <remarks></remarks>
        public new void Update()
        {//----------- 读取轴的状态并显示 ------------        
            try
            {
                double currPose = _Axis.GetPrfPos(); //规划位置
                double encPos = _Axis.GetEncPos();    //编码位置
                Label_ProfilePos.Text = _Axis.PulseToMm(currPose).ToString("0.000");
                Label_EncodePos.Text = _Axis.PulseToMm(encPos).ToString("0.000");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        private void btMoveTo_Click(object sender, EventArgs e)
        {
            double pos = (Convert.ToDouble(textBoxMoveTo.Text));
            _Axis.MoveAbsolute(pos, AbsMotion.ConstDebugVel);
        }   

        public override void Refresh()
        {//----------- 读取轴的状态并显示 ------------        
            try
            {
                if (_AxisNum > 0)
                {
                    double currPose = _Axis.GetPrfPos(); //规划位置
                    double encPos = _Axis.GetEncPos();    //编码位置
                    Label_ProfilePos.Text = _Axis.PulseToMm(currPose).ToString("0.000");
                    Label_EncodePos.Text = _Axis.PulseToMm(encPos).ToString("0.000");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }
    }
}

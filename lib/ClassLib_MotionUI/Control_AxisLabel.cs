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
using ClassLib_MSG;
using ClassLib_RunMode;

namespace ClassLib_MotionUI
{
    public partial class Control_AxisLabel : UserControl
    {
        public Control_AxisLabel()
        {
            InitializeComponent();
            m_ArrayList_Controls.Add(this);

            //增加新的自定义容器
            if (Publics.s_Container_Controls.Count <= 0 || !Publics.s_Container_Controls.Contains(typeof(Control_AxisLabel)))
            {
                Publics.s_Container_Controls.Add(typeof(Control_AxisLabel));
            }
        }
        
        public static System.Collections.ArrayList m_ArrayList_Controls = new System.Collections.ArrayList(100);
        public static void GetAllControlsRecursion(System.Windows.Forms.Control.ControlCollection _Controls)
        {
            m_ArrayList_Controls.Clear();
            Publics.GetAllControlsRecursion(typeof(Control_AxisLabel), _Controls, ref m_ArrayList_Controls);
        }
        AbsAxis _Axis;
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
                //if (_Axis != null)
                {

                }
            }
        }
        [CategoryAttribute("自定义属性"), DescriptionAttribute("显示内容")]
        public string TextValue
        {
            get
            {
                return labelEdit.DefaultLabel;
            }
            set
            {
                labelEdit.DefaultLabel = value;

                if (Name.Contains("Control_AxisLabel_"))
                    labelEdit.Name = "Control_LabelEdit_" + Name.Substring(("Control_AxisLabel_").Length);
            }
        }

        public delegate short EventHandlerShort(object sender, EventArgs e);
        [CategoryAttribute("自定义事件"), DescriptionAttribute("轴运行前回调函数")]
        public event EventHandlerShort BeforeMoveEvent = null;

        private void btGetValue_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            double _pos = _Axis.GetEncPosMm();
            labelEdit.SetTextBoxValue(_pos.ToString());
        }

        private void btMove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Run.GetRunning())
            {
                MSG.Show("自动运行中不可手动操作");
                return;
            }
            try
            {
                if (BeforeMoveEvent != null)
                {
                    if (BeforeMoveEvent(sender, e) < 0)
                        return;
                }
                btMove.Enabled = false;
                double _pos = Convert.ToDouble(labelEdit.GetTextBoxValue());
                _Axis.MoveAbsolute(_pos, true);
            }
            catch (Exception ex)
            {
                MSG.Show(ex.Message);
            }
            btMove.Enabled = true;
        }
    }
}

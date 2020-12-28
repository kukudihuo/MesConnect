using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCWin;
using ClassLib_Motion;
using ClassLib_MotionUI;
using ClassLib_ParaFile;
using System.Threading;
using System.IO;
using ClassLib_MotionStd;

namespace MotionEdit
{
    public partial class MotionEdit : CCSkinMain
    {
        string iniFileName = Publics.GetMotionPath() + "Help.ini"; //Application.StartupPath + "\\参数目录\\运动控制卡\\Help.ini";
        private HelpDataClass helpDataClass = HelpDataClass.GetInstance();
        public MotionEdit()
        {
            InitializeComponent();

            string path = System.Configuration.ConfigurationManager.AppSettings["Path"];

            tabMain.SelectedIndex = 0;
            tabControlHelp.SelectedIndex = 0;

            //读取项目名称
            string sValue = ParaFileINI.ReadINI("SOFTDATA", "CodeName", iniFileName);
            if (sValue != null && !sValue.Equals("") && !sValue.Equals("0"))
            {
                Publics.AppName = sValue;
                txtCodeName.Text = sValue;
            }
            else
            {
                sValue = "";
            }
            InitRefreshView();//加载数据需要项目名称，所以需要在读取项目名称后再加载数据
            
            helpDataClass.LoadHelpParameter(dgvHelpParameter);//加载帮助的参数说明列表数据（参数说明是统一的不需要更新）

        }

        private void btnRefreshView_Click(object sender, EventArgs e)
        {
            InitRefreshView();
        }
        /// <summary>
        /// 刷新运动监控图标
        /// </summary>
        private void InitRefreshView()
        {
            Publics.AppName = txtCodeName.Text.ToString();
            if (!Directory.Exists(Publics.GetGtsPath()))
            {
                DialogResult dialogResult = MessageBoxEx.Show(
                    "检查到无文件夹“" + Publics.GetGtsPath() + "”\n请检查设备名称是否有误或是否有相应文件夹。\n是：不加载运动控制卡\n否：取消加载",
                    "提示", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes) cbLoadMotion.Checked = false;
                else return;
            }

            timerUI.Enabled = false;

            txtReadFileName.Text = Publics.GetGtsPath();

            ParaFileINI.WriteINI("SOFTDATA", "CodeName", Publics.AppName, iniFileName);
            helpDataClass.LoadHelpDebug(dgvHelpDebug, Publics.AppName);//加载帮助的调试说明列表数据（不一样的设备,调试说明不一样,需要刷新）
            
            //清空控件
            mdvAxisView.Controls.Clear();
            mdvIOView.Controls.Clear();
            //加载数据
            if (cbLoadMotion.Checked)
                //Motion.CreateMotion(new DmcMotion());//DmcMotion
                Motion.CreateMotion(new MotionBomming());//DmcMotion
            else
                Motion.CreateMotion(new MessageMotion());//

            //刷新控件
            mdvIOView.DataRefresh();
            Control_IO.UpdateControlList(mdvIOView.Controls);
            mdvAxisView.DataRefresh();
            //重置轴控件
            Publics.RefreshAllUI(typeof(Control_Axis), mdvAxisView.Controls, null, bAwaysRefresh: true);//20200513需要清空数据，后面刷新的新数据控件才能重新加载
            //更新轴数据
            Control_Axis.GetAllControlsRecursion(mdvAxisView.Controls);//初始化
            //遍历把复位的状态设置为false
            foreach (var mControl in mdvAxisView.Controls)
            {
                if (mControl.GetType().Name.Equals("TabControl"))
                {
                    foreach (var mTabControl in ((TabControl)mControl).Controls)
                    {
                        if (mTabControl.GetType().Name.Equals("TabPage"))
                        {
                            foreach (var mTabPage in ((TabPage)mTabControl).Controls)
                            {
                                if (mTabPage.GetType().Name.Equals("Control_Axis"))
                                {
                                    ((Control_Axis)mTabPage).IsReseted = false;
                                }
                            }
                        }
                    }
                }
                else if (mControl.GetType().Name.Equals("Control_Axis"))
                {
                    ((Control_Axis)mControl).IsReseted = false;
                }
            }

            timerUI.Enabled = true;
        }

        private void timerUI_Tick(object sender, EventArgs e)
        {
            Publics.RefreshAllUI(typeof(Control_Axis), mdvAxisView.Controls, Control_Axis.m_ArrayList_Controls, bAwaysRefresh: true);
            Control_IO.RefreshAllUI(mdvIOView.Controls);
            Publics.RefreshAllUI(typeof(Control_IO_One), this.Controls, Control_IO_One.m_ArrayList_Controls);
        }
    }
}

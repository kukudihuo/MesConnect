using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CCWin;
using ClassLib_ParaFile;
using ClassLib_MotionUI;

namespace ClassLib_MotionView
{
    public partial class MotionIOView : UserControl
    {
        public MotionIOView()
        {
            InitializeComponent();
        }
        public void DataRefresh()
        {
            try
            {
                this.Controls.Clear();
                string IniFileName = Publics.GetMotionPath() + "Motion.ini";//Application.StartupPath + "\\参数目录\\运动控制卡\\Motion.ini";
                string numCARD = ParaFileINI.ReadINI("CARD", "CardTotal", IniFileName);//读取卡的数量
                string extCardTotal = ParaFileINI.ReadINI("CARD", "ExtCardTotal", IniFileName);//获取扩展卡数量

                int iExtCard = 0;
                if (extCardTotal.Length > 0)
                {
                    iExtCard = int.Parse(extCardTotal);
                }
                if (int.Parse(numCARD) == 1 && iExtCard == 0)
                {
                    Control_IO controlDI = new Control_IO();
                    controlDI.Dock = DockStyle.Top;
                    controlDI.Name = "control_DI1";
                    controlDI.CardNum = 0;
                    controlDI.IO_Type = 1;
                    controlDI.IoTotal = ClassLib_Motion.AbsMotion.IO_IN_CARD;
                    this.Controls.Add(controlDI);

                    Control_IO controlDO = new Control_IO();
                    controlDO.Dock = DockStyle.Top;
                    controlDO.Name = "control_DO1";
                    controlDO.CardNum = 0;
                    controlDO.IO_Type = 0;
                    controlDO.IoTotal = ClassLib_Motion.AbsMotion.IO_IN_CARD;
                    this.Controls.Add(controlDO);

                    LightControlCtrl lightCtrlCtrl = new LightControlCtrl();
                    lightCtrlCtrl.Dock = DockStyle.Top;
                    this.Controls.Add(lightCtrlCtrl);
                }
                else if (int.Parse(numCARD) >= 1)
                {
                    TabControl _tabCont = new TabControl();
                    for (int i = 0; i < int.Parse(numCARD); i++)
                    {
                        TabPage page = new TabPage();
                        page.Name = "Card" + (i+1).ToString();
                        page.Text = "卡" + (i+1).ToString();

                        Control_IO controlDI = new Control_IO();
                        controlDI.Dock = DockStyle.Top;
                        controlDI.Name = "control_DI" + i.ToString();
                        controlDI.CardNum = i;
                        controlDI.IO_Type = 1;
                        controlDI.IoTotal = ClassLib_Motion.AbsMotion.IO_IN_CARD;
                        page.Controls.Add(controlDI);

                        Control_IO controlDO = new Control_IO();
                        controlDO.Dock = DockStyle.Top;
                        controlDO.Name = "control_DO"+ i.ToString();
                        controlDO.CardNum = i;
                        controlDO.IO_Type = 0;
                        controlDO.IoTotal = ClassLib_Motion.AbsMotion.IO_IN_CARD;
                        page.Controls.Add(controlDO);

                        LightControlCtrl lightCtrlCtrl = new LightControlCtrl();
                        lightCtrlCtrl.Dock = DockStyle.Top;
                        page.Controls.Add(lightCtrlCtrl);
                        
                        _tabCont.Controls.Add(page);
                    }
                    int numExt = 10;
                    for (int i = 0; i < iExtCard; i++)
                    {
                        TabPage page = new TabPage();
                        page.Name = "ExtCard" + (i+1).ToString();
                        page.Text = "扩展卡" + (i+1).ToString();

                        Control_IO controlDI = new Control_IO();
                        controlDI.Dock = DockStyle.Top;
                        controlDI.Name = "control_DI"+(numExt + i).ToString();
                        controlDI.CardNum = numExt + i;
                        controlDI.IO_Type = 1;
                        controlDI.IoTotal = ClassLib_Motion.AbsMotion.IO_IN_CARD_EXT;
                        page.Controls.Add(controlDI);

                        Control_IO controlDO = new Control_IO();
                        controlDO.Dock = DockStyle.Top;
                        controlDO.Name = "control_DO"+(numExt + i).ToString();
                        controlDO.CardNum = numExt + i;
                        controlDO.IO_Type = 0;
                        controlDO.IoTotal = ClassLib_Motion.AbsMotion.IO_IN_CARD_EXT;
                        page.Controls.Add(controlDO);

                        _tabCont.Controls.Add(page);
                    }
                    _tabCont.Name = "CardDIView";
                    _tabCont.Dock = DockStyle.Fill;
                    _tabCont.TabIndex = 1;
                    this.Controls.Add(_tabCont);
                }
                else
                {
                    MessageBoxEx.Show("卡的数量小于1，请检查参数文件,请把参数文件复制到：" + IniFileName);
                }
            }
            catch (Exception)
            {
                MessageBoxEx.Show("DI显示控件刷新异常");
            }
        }
    }
}

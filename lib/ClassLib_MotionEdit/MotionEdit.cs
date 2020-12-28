using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using CCWin;

namespace ClassLib_MotionEdit
{
    public partial class MotionEdit : UserControl
    {
        string m_FileName;
        OperationClass operationClass = null;
        public MotionEdit()
        {
            InitializeComponent();

            operationClass = new OperationClass(dgvMotionCard, dgvMotionIn, dgvMotionOut, dgvMotionTotal);

        }

        private void btnMotionOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "运动控制参数文件(*.ini)|*.ini";
            ofd.ValidateNames = true;
            ofd.CheckPathExists = true;
            ofd.CheckFileExists = true;
            System.Configuration.Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            ofd.InitialDirectory = config.AppSettings.Settings["Path"].Value;
            //ofd.InitialDirectory = System.Configuration.ConfigurationManager.AppSettings["Path"];
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                m_FileName = ofd.FileName;
                operationClass.OpenFile(m_FileName);
                
                config.AppSettings.Settings["Path"].Value = m_FileName;
                //System.Configuration.ConfigurationManager.AppSettings["Path"] = m_FileName;
                //System.Configuration.ConfigurationManager.AppSettings.Set("Path", m_FileName);
                //Save the configuration file
                config.AppSettings.SectionInformation.ForceSave = true;
                config.Save(System.Configuration.ConfigurationSaveMode.Modified);
            }
        }

        private void btnMotionGen_Click(object sender, EventArgs e)
        {
            SaveFileDialog savedialog = new SaveFileDialog();
            savedialog.Title = "保存";
            savedialog.Filter = "ini格式(*.ini)|*.ini";
            savedialog.FilterIndex = 0;
            savedialog.RestoreDirectory = true;
            savedialog.CheckPathExists = true;
            savedialog.FileName = m_FileName;
            if (savedialog.ShowDialog() == DialogResult.OK)
            {
                Thread objThread = new Thread(new ThreadStart(delegate
                {
                    UpDate(1);
                    bool result = operationClass.WriteFile(savedialog.FileName);
                    Thread.Sleep(1000);
                    UpDate(0);
                    if (result)
                        MessageBoxEx.Show("生成完成");
                }));
                objThread.Start();
            }
        }

        private void UpDate(int isVisible)
        {
            Action act = delegate ()
            {
                if (isVisible == 0)
                {
                    pBar1.Value = 100;
                    Thread.Sleep(1000);
                    pBar1.Value = 0;
                    pBar1.Visible = false;
                }
                else
                {
                    pBar1.Value = 30;
                    pBar1.Visible = true;
                }
            };
            this.Invoke(act);
        }
        
        private void cmsCardDeleteRow_Click(object sender, EventArgs e)
        {
            operationClass._dgvMotionCard.Rows.Remove(operationClass._dgvMotionCard.CurrentRow);
        }
        private void cmsInDeleteRow_Click(object sender, EventArgs e)
        {
            operationClass._dgvMotionIn.Rows.Remove(operationClass._dgvMotionIn.CurrentRow);
        }

        private void cmsOutDeleteRow_Click(object sender, EventArgs e)
        {
            operationClass._dgvMotionOut.Rows.Remove(operationClass._dgvMotionOut.CurrentRow);
        }

        private void cmsTotalDeleteRow_Click(object sender, EventArgs e)
        {
            operationClass._dgvMotionTotal.Rows.Remove(operationClass._dgvMotionTotal.CurrentRow);
        }
        private FormNew mFormNew;
        private void btnMotionNew_Click(object sender, EventArgs e)
        {
            if (mFormNew == null || mFormNew.IsDisposed)
            {
                mFormNew = new FormNew();
                mFormNew.changedOpen -= AddCardData;
                mFormNew.changedOpen += AddCardData;
                mFormNew.Show();
            }
            else
                mFormNew.Activate();
        }
        public void AddCardData( )
        {
            int iTray = int.Parse(mFormNew.DataParameter["卡类型"]);
            int iReturn = operationClass.AddCardData(mFormNew.DataParameter);
            if (iReturn == -1) MessageBoxEx.Show("运动控制卡卡号有重复，添加失败");
            else if (iReturn == -2) MessageBoxEx.Show("运动控制卡卡的名称有重复请确认");
            else if (iReturn == -3) MessageBoxEx.Show("前一张运动控制卡为4轴卡，当前就不能新增8轴运动控制卡");
            else if (iReturn == -10) MessageBoxEx.Show("请先添加运动控制卡再添加扩展卡");
            else if (iReturn == 0)
            {
                if (iTray == 1) MessageBoxEx.Show("新增扩展卡成功");
                else MessageBoxEx.Show("新增运动控制卡成功");
            } 
        }
    }
}

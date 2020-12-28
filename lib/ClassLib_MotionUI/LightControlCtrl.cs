using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClassLib_MotionStd;
using System.Configuration;

namespace ClassLib_MotionUI
{
    public partial class LightControlCtrl : UserControl
    {
        bool mEnableCoaxial = false;
        public bool EnableCoaxial
        {
            get { return mEnableCoaxial; }
            set
            {
                mEnableCoaxial = value;
                if(value) { this.labelCoaxial.Show(); this.trackBarCoaxial.Show(); }
                else { this.labelCoaxial.Hide(); this.trackBarCoaxial.Hide(); }
            }
        }

        bool mEnableHigh = false;
        public bool EnableHight
        {
            get { return mEnableHigh; }
            set
            {
                mEnableHigh = value;
                if(value) { this.labelHighRing.Show();this.trackBarHighLight.Show(); }
                else { this.labelHighRing.Hide(); this.trackBarHighLight.Hide(); }
            }
        }

        public LightControlCtrl()
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
            //Console.WriteLine("Local user config path: {0}", config.FilePath);
            System.Diagnostics.Trace.WriteLine(config.FilePath);

            InitializeComponent();
        }

        private void trackBarRingA_ValueChanged(object sender, EventArgs e)
        {
            int iVal = this.trackBarRingA.Value;
            //trackBarRingA.Text = iVal.ToString();
            BommingMotionHelper.GetInstance().SetBright(BommingMotionHelper.BrightChannel.BC_RINGA, iVal);

            //string ringa = Properties.Settings.Default.RingA.ToString();
            //Properties.Settings.Default.RingA = iVal;
            //Properties.Settings.Default.Save();
            //Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //config.Save(ConfigurationSaveMode.Full);
        }

        private void trackBarRingB_ValueChanged(object sender, EventArgs e)
        {
            int iVal = this.trackBarRingB.Value;
            BommingMotionHelper.GetInstance().SetBright(BommingMotionHelper.BrightChannel.BC_RINGB, iVal);
        }

        private void trackBarRingC_ValueChanged(object sender, EventArgs e)
        {
            int iVal = this.trackBarRingC.Value;
            BommingMotionHelper.GetInstance().SetBright(BommingMotionHelper.BrightChannel.BC_RINGC, iVal);
        }

        private void trackBarRingD_ValueChanged(object sender, EventArgs e)
        {
            int iVal = this.trackBarRingD.Value;
            BommingMotionHelper.GetInstance().SetBright(BommingMotionHelper.BrightChannel.BC_RINGD, iVal);
        }

        private void trackBar0Ring_ValueChanged(object sender, EventArgs e)
        {
            int iVal = this.trackBar0Ring.Value;
            BommingMotionHelper.GetInstance().SetBright(BommingMotionHelper.BrightChannel.BC_ZERORING, iVal);
        }

        private void trackBarBack_ValueChanged(object sender, EventArgs e)
        {
            int iVal = this.trackBarBack.Value;
            BommingMotionHelper.GetInstance().SetBright(BommingMotionHelper.BrightChannel.BC_BACK, iVal);
        }

        private void trackBarCoaxial_ValueChanged(object sender, EventArgs e)
        {
            int iVal = this.trackBarCoaxial.Value;
            BommingMotionHelper.GetInstance().SetBright(BommingMotionHelper.BrightChannel.BC_COAXIAL, iVal);
        }

        private void trackBarHighLight_ValueChanged(object sender, EventArgs e)
        {
            int iVal = this.trackBarHighLight.Value;
            BommingMotionHelper.GetInstance().SetBright(BommingMotionHelper.BrightChannel.BC_HIGH, iVal);
        }

        private void LightControlCtrl_Leave(object sender, EventArgs e)
        {
            //Properties.Settings.Default.Save();
        }
    }
}

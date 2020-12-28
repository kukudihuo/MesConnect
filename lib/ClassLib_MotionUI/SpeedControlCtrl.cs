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
    public partial class SpeedControlCtrl : UserControl
    {      
        public SpeedControlCtrl()
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
            //Console.WriteLine("Local user config path: {0}", config.FilePath);
            System.Diagnostics.Trace.WriteLine(config.FilePath);

            InitializeComponent();
        }
    }
}

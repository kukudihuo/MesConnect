using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ClassLib_ParaFile;
using ClassLib_StdThread;
using ClassLib_RunMode;

namespace ClassLib_Motion
{
    public class RunModeMotion : RunMode
    {
        #region 基础流程状态切换        
        #region 虚拟函数
        public override void SetHoming()
        {
            base.SetHoming();
            Motion.IsAxisReseted = false; 
        }
        /// <summary>
        /// 设置为急停状态并停止所有轴，断使能，结束所有线程
        /// </summary>
        /// <param name="bStop"></param>
        public override void SetAbruptStop(bool bStop = true)
        {
            Motion.SetAbruptStop(bStop);
        }
        /// <summary>
        /// 刷新急停状态
        /// </summary>
        /// <returns></returns>
        protected override bool UpDateStop()
        {
            return Motion.UpDateStop();
        }
        /// <summary>
        /// 更新暂停状态
        /// </summary>
        /// <param name="bPauseSenso"></param>
        /// <returns></returns>
        protected override bool UpdatePause(bool bPauseSenso = false)
        {
            return Motion.UpdatePause(bPauseSenso);
        }
        /// <summary>
        /// 设置信号灯和蜂鸣器
        /// </summary>
        protected override void SetLightColor()
        {
            Motion.SetLightColor();
        }
        #endregion 虚拟函数

        #endregion 基础流程状态切换
    }
}

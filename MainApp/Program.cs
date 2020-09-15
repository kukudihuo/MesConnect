using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace MainApp
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool createNew;
            Process currentPro = Process.GetCurrentProcess();
            using (Mutex mutex = new Mutex(true, Application.ProductName, out createNew))//currentPro.ProcessName
            {
                if (!createNew)
                {
                    DialogResult result = MessageBox.Show("程序正在运行，是否重新开始", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.Yes)
                    {
                        Process[] proList = Process.GetProcesses();
                        
                        foreach(var kvp in proList)
                        {
                            if(kvp.ProcessName == Application.ProductName)
                            {
                                if (kvp.Id != currentPro.Id)
                                    kvp.Kill();
                            }
                        }                   
                    }
                    else
                        return;
                }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormMesConnect());
            }
        }
    }
}

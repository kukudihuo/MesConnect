using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;

namespace ClassLib_ParaFile
{
    public class CQueryTime  
    {
        [DllImport("BaseLib.dll", EntryPoint = "Counter", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        static extern bool Counter(ref   long lpPerformanceCount);
        [DllImport("BaseLib.dll", EntryPoint = "Frequency", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        static extern bool Frequency(ref long PerformanceFrequency);
        [DllImport("BaseLib.dll", EntryPoint = "Delay", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Delay(int iTime);
        [DllImport("BaseLib.dll", EntryPoint = "LibStatus", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LibStatus();
    //---------------------------------------------------------------------------
	    public CQueryTime(long lPrecision = 1000)
	    {
            m_lPrecision = lPrecision;
		    Frequency(ref m_liFrequency);
		    Start();
	    }
    //---------------------------------------------------------------------------
	    ~CQueryTime(){}
    //---------------------------------------------------------------------------
	    public long Now()
	    {
		    long liNow = 0;
		    return ( Counter(ref liNow) )?(liNow - m_liStart)*m_lPrecision/m_liFrequency:0L;
	    }
    //---------------------------------------------------------------------------
	    public void Start(){Counter(ref m_liStart);}
    //---------------------------------------------------------------------------
	    private long m_lPrecision;
	    private long m_liFrequency, m_liStart;

        static void ProcessMessages()
        {//线程中运行会导致当前CPU满负荷
            //CWinApp* pApp = AfxGetApp();
            //MSG msg;
            //  while ( PeekMessage ( &msg, NULL, 0, 0, PM_NOREMOVE ))
            //      pApp->PumpMessage();
            Application.DoEvents();
        }

        static void SystemDelay(long lTime, long lPrecision, bool bProcessMessage = true)
        {//ProcessMessages转移处理其它消息，可以实现线程同步等待，加入对其它标示的判断可用来实现waitfor功能。但要留意在线程中使用时可能没有起到转移处理的目的
        //VCL都是消息调用，所以ProcessMessages在主线程中可以实现等待VCL消息处理的作用，类似于线程等待
            long BeginTime = 0;
            long EndTime = 0;
            long frequency = 0; //返回硬件支持的高精度计数器的频率。

            Frequency(ref frequency);
            Counter(ref BeginTime);
            Counter(ref EndTime);
            try
            {
            while (((EndTime - BeginTime) * lPrecision / frequency) <= lTime)
            {
                Counter(ref EndTime);
                if (lPrecision < 10000)
                    Thread.Sleep(1);
                if (bProcessMessage)
                {
                    ProcessMessages(); //DoEvents();必须放在sleep后面 否则会导致sleep后的指令不执行
                }
                //else if (lPrecision < 10000)
                //{
                //    Thread.Sleep(1);  //线程中用SLEEP会大幅降低CPU使用率
                //    //ProcessMessages();
                //}
            }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        public static void SystemDelayS(double time)
        {
            SystemDelayMs((int)(time * 1000));
        }
        //以毫秒为单位
        public static void SystemDelayMs(long lTime)
        {
            if (Thread.CurrentThread.Name != "MainThread")
                SystemDelay(lTime, 1000, false);
            else
                SystemDelayMsPM(lTime);

        }
        public static void SystemDelayUs(long lTime)
        {
            if (Thread.CurrentThread.Name != "MainThread")
                SystemDelay(lTime, 1000000, false);
            else
                SystemDelayUsPM(lTime);

        }

        public static void SystemDelaySpm(double time)
        {
            SystemDelayMsPM((int)(time * 1000));
        }

        public static void SystemDelayMsPM(long lTime)
        {
            SystemDelay(lTime, 1000, true);
        }
        public static void SystemDelayUsPM(long lTime)
        {
            SystemDelay(lTime, 1000000, false);
        }
    }
}

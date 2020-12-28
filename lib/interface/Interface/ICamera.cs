using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interface
{
    public enum TriggerMode
    {
        Continue_Mode = 0,
        Soft_Mode = 1,
        Trigger_Mode = 2,
    }

    public interface ICamera
    {      
        /// <summary>
        /// GC清除无效内存
        /// </summary>
        void ClearGCImage();

        bool SoftSnap(int timerOut = 220, bool bTraggerHardware = false);
        //bool TriggerSnap(int timerOut = CAMERA_THREAD_TIMEOUT);
        bool TriggerSnap(int timerOut);

        bool IsOpened();
        bool Start();
        bool Stop();
        void LiveThreadFunction();
        /// <summary>
        /// 用回调函数来触发图像处理,可在图像处理类中指定
        /// </summary>
        /// <param name="ImageBuffers"></param>
        /// <returns></returns>
        //public delegate bool pOnGetImage(int width, int height, IntPtr pImage);
        //public pOnGetImage OnGetImage;
        void ResetAllEvent();
        void SetAllEvent();
        void ClearMapEventNow(bool bReset = true);
        void AddMapEventNow(string imageName, bool bReset = true);
        void RemoveMapEventNow(string imageName, bool bReset = true);
        string GetFirstEventNow();

        bool HGrabImage(int width, int height, IntPtr pImage);
        bool CvGrabImage(int width, int height, IntPtr pImage);

        //虚拟函数
        bool OpenCamera();
        bool CloseCamera();
        bool OneShot();
        int GetExposure();
        int GetGain();
        bool ChangeExpouse(int expouse);
        bool ChangeGain(int gain);

        //SDK自带视频模式
        void StartLive(int ShowSelect = 0);
        void StopLive();
        void PauseLive(bool bPause = true);
        //SDK自带视频模式

        bool IsTrigger();
        void SetTrigger(TriggerMode trigger);
        int GetWidth();
        int GetHeight();
        int GetBitsPerPixel();
    }
}

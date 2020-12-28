﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections.Specialized;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using System.ComponentModel;
using System.Threading;

namespace ClassLib_MotionEdit
{
    public class ParaFileINI
    {
        ////'''''''''''''''''''''''''''''''''''''''''''''''''''读取，写入文件''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ////声明INI配置文件读写API函数
        //[DllImport("kernel32")]
        //private static extern int GetPrivateProfileString(string section, string key, string defVal, StringBuilder retVal, int size, string filePath);
        ////section：要读取的段落名
        ////key: 要读取的键
        ////defVal: 读取异常的情况下的缺省值
        ////retVal: key所对应的值，如果该key不存在则返回空值
        ////size: 值允许的大小
        ////filePath: INI文件的完整路径和文件名
        //[DllImport("kernel32")]
        //private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        ////section: 要写入的段落名
        ////key: 要写入的键，如果该key存在则覆盖写入
        ////val: key所对应的值
        ////filePath: INI文件的完整路径和文件名
        ////定义读取配置文件函数
        //public static string GetINI(string Section, string AppName, string lpDefault, string FileName)
        //{
        //    StringBuilder Str = new StringBuilder(255);
        //    GetPrivateProfileString(Section, AppName, lpDefault, Str, 255, FileName);
        //    return Str.ToString();
        //}
        ////public static byte[] GetINIs(string section, string key, string lpDefault, string FileName)
        ////{
        ////    //读取所有secotion：(null, null); 读取section中所有value (“section”, null);
        ////    byte[] temp = new byte[255];

        ////    int i = GetPrivateProfileString(section, key, lpDefault, (StringBuilder)temp, 255, FileName);
        ////    return temp;
        ////}
        //public static long WriteINI(string Section, string AppName, string lpDefault, string FileName)
        //{
        //    return WritePrivateProfileString(Section, AppName, lpDefault, FileName);
        //}
        //'''''''''''''''''''''''''''''''''''''''''''''''''读取，写入文件  End ''''

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern long GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileSection(string lpAppName, byte[] lpReturnedString, int nSize, string lpFileName);
        [DllImport("kernel32")]
        public extern static void SetCursorPos(int x, int y);

        public string typeName = "默认";
        public bool m_bEx = false;
        private string _FilePath;
        private string _Section;

        public ParaFileINI(string filePath, string section)
        {
            _FilePath = filePath;
            _Section = section;
            byte[] lpReturnedString = new byte[1];
            if (GetPrivateProfileSection(section, lpReturnedString, 1, filePath) >= 0)
                m_bEx = true;
        }

        public void DelSection(string section)
        {
            WriteINI(section, null, null, _FilePath);
        }
        public void DelKey(string section, string key)
        {
            WriteINI(section, key, null, _FilePath);
        }
        public static void DelSection(string section, string filePath)
        {
            WriteINI(section, null, null, filePath);
        }
        public static void DelKey(string section, string key, string filePath)
        {
            WriteINI(section, key, null, filePath);
        }

        /// <summary>
        /// 3 写入INI数据
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="val">值</param>
        public void WriteINI(string key, Object val)
        {
            string sType = val.GetType().ToString();
            if (sType.Equals(new bool().GetType().ToString()))
                val = Convert.ToInt32(val);
            WriteINI(key, val.ToString());

        }
        private void WriteINI(string key, string val)
        {
            WriteINI(_Section, key, val, _FilePath);
        }
        private void WriteIntINI(string key, int val)
        {
            WriteINI(key, val.ToString());
        }
        private void WriteBoolINI(string key, bool val)
        {
            WriteINI(key, Convert.ToInt32(val).ToString());
        }
        private void WriteIntINI(string key, double val)
        {
            WriteINI(key, val.ToString());
        }
        /// <summary>
        /// 写入INI数据
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="key">关键字</param>
        /// <param name="val">值</param>
        /// <param name="filePath">文件路径</param>
        public static void WriteINI(string section, string key, string val, string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                    WritePrivateProfileString(section, key, val, filePath);
                else
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                    File.Create(filePath).Close();
                    WritePrivateProfileString(section, key, val, filePath);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 3 用object的方式，同一个函数调用不同的读入(返回值需要加强制转换(int)等)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public object ReadINI(string key, object def)//ref
        {
            object val;
            string sType = def.GetType().ToString();
            if (sType.Equals(new string(' ', 1).GetType().ToString()))
                val = ReadINI(key, def.ToString());
            else if (sType.Equals(new int().GetType().ToString()) || sType.Equals(new Int16().GetType().ToString()) || sType.Equals(new Int64().GetType().ToString()) ||
                     sType.Equals(new uint().GetType().ToString()) || sType.Equals(new UInt16().GetType().ToString()) || sType.Equals(new UInt64().GetType().ToString()))
                val = ReadIntINI(key, Convert.ToInt32(def));
            else if (sType.Equals(new double().GetType().ToString()) || sType.Equals(new float().GetType().ToString()))
                val = ReadDoubleINI(key, Convert.ToDouble(def));
            else if (sType.Equals(new bool().GetType().ToString()))
                val = ReadBoolINI(key, Convert.ToBoolean(def));
            //if (val.Equals(System.Type.GetType("string").ToString()))
            //    val = ReadStringINI(key);
            //else if (val.Equals(System.Type.GetType("int").ToString()) || val.Equals(System.Type.GetType("Int16").ToString()) || val.Equals(System.Type.GetType("Int64").ToString()) ||
            //         val.Equals(System.Type.GetType("uint").ToString()) || val.Equals(System.Type.GetType("UInt16").ToString()) || val.Equals(System.Type.GetType("UInt64").ToString()))
            //    val = ReadIntINI(key);
            //else if (val.Equals(System.Type.GetType("double").ToString()) || val.Equals(System.Type.GetType("float").ToString()))
            //    val = ReadDoubleINI(key);
            //else if (val.Equals(System.Type.GetType("bool").ToString()))
            //    val = ReadBoolINI(key);
            else
            {
                val = ReadINI(key, def.ToString());
                //MessageBoxEx.Show("参数：" + key + "类型错误:" + sType);
            }
            return val;
        }

        //2 用引用ref的方式同一个函数名重载不同读入函数
        /// <summary>
        /// 读取INI文件数据
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="size">数据大小</param>
        /// <returns>返回字符串</returns>
        public void ReadINI(string key, ref string val, string def = "")//
        {
            val = ReadINI(key, def);
        }
        public void ReadINI(string key, ref int val, int def = 0)//
        {
            val = ReadIntINI(key, def);
        }
        public void ReadINI(string key, ref short val, short def = 0)//
        {
            val = Convert.ToInt16(ReadIntINI(key, def));
        }
        public void ReadINI(string key, ref long val, int def = 0)//
        {
            val = Convert.ToInt64(ReadIntINI(key, def));
        }
        public void ReadINI(string key, ref double val, double def = 0)//
        {
            val = ReadDoubleINI(key, def);
        }
        public void ReadINI(string key, ref float val, float def = 0)//
        {
            val = (float)ReadDoubleINI(key, def);
        }
        public void ReadINI(string key, ref bool val, bool def = false)//
        {
            val = ReadBoolINI(key, def);
        }

        //第一种方式，按函数名读取不同类型的变量
        public int ReadIntINI(string key, int def = 0)
        {
            return Convert.ToInt32(ReadINI(key, def.ToString()));
        }
        public bool ReadBoolINI(string key, bool def = false)
        {



            return Convert.ToBoolean(Convert.ToInt32(ReadINI(key, Convert.ToInt32(def).ToString())));
        }
        public double ReadDoubleINI(string key, double def = 0)
        {
            return Convert.ToDouble(ReadINI(key, def.ToString()));
        }
        /// <summary>
        /// 读取ini的次基础函数（类读取ini的基础函数）
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string ReadINI(string key, string def = "0")
        {
            return ReadINI(_Section, key, _FilePath, def);
        }
        
        //0 原始方式，调用基础函数并自己转换为相应类型
        /// <summary>
        /// 读取INI文件数据的基础函数
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="key">关键字</param>
        /// <param name="size">数据大小</param>
        /// <param name="filePath">文件路径</param>
        /// <returns>返回字符串</returns>
        public static string ReadINI(string section, string key, string filePath, string def = "0", int size = 255)
        {
            string rtnstring = "";
            //size = 255;
            StringBuilder retVal = new StringBuilder(size);
            try
            {
                GetPrivateProfileString(section, key, def, retVal, size, filePath);
                rtnstring = retVal.ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
            return rtnstring;
        }
        /// <summary>
        /// 读取INI文件中一节所有数据
        /// </summary>
        /// <param name="filename">文件路径</param>
        /// <param name="section">节</param>
        /// <returns></returns>
        public static string[] GetINISection(String filename, String section)
        {
            StringCollection items = new StringCollection();
            byte[] buffer = new byte[32768];
            int bufLen = 0;
            bufLen = GetPrivateProfileSection(section, buffer, buffer.GetUpperBound(0), filename);
            if (bufLen > 0)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < bufLen; i++)
                {
                    if (buffer[i] != 0)
                    {
                        sb.Append((char)buffer[i]);
                    }
                    else
                    {
                        if (sb.Length > 0)
                        {
                            items.Add(sb.ToString());
                            sb = new StringBuilder();
                        }
                    }
                }
            }
            string[] returnStr = new string[items.Count];
            items.CopyTo(returnStr, 0);
            return returnStr;
        }

        /// <summary>
        /// 获取控件对应事件列表
        /// </summary>
        /// <param name="_Control">控件</param>
        /// <param name="_EventName">事件名</param>
        /// <returns>返回委托列表</returns>
        public Delegate[] GetObjectEventLists(Control _Control, string _EventName = "EventClick")
        {
            try
            {
                PropertyInfo _PropertyInfo = _Control.GetType().GetProperty("Events", BindingFlags.Instance | BindingFlags.NonPublic);
                if (_PropertyInfo != null)
                {
                    object _EventList = _PropertyInfo.GetValue(_Control, null);
                    if (_EventList != null && _EventList is EventHandlerList)
                    {
                        EventHandlerList _List = (EventHandlerList)_EventList;
                        FieldInfo _FieldInfo = (typeof(Control)).GetField(_EventName, BindingFlags.Static | BindingFlags.NonPublic);
                        if (_FieldInfo == null)
                            return null;

                        Delegate _ObjectDelegate = _List[_FieldInfo.GetValue(_Control)];
                        if (_ObjectDelegate == null)
                            return null;
                        return _ObjectDelegate.GetInvocationList();
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
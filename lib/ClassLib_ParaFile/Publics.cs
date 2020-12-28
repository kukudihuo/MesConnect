using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic.Devices;
using Microsoft.VisualBasic.FileIO;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Collections.ObjectModel;

namespace ClassLib_ParaFile
{
    public partial class Publics// : UserControl
    {
        static string appName = "";
        public static string AppName
        {
            get
            {
                if (string.IsNullOrEmpty(appName))
                    appName = Application.ProductName;
                return appName;
            }
            set
            {
                appName = value;
            }
        }
        public static bool ShowMore = false;
        public static Display sDisplay = null;//new Display()
        public static int IoTotal = 16;
        public static bool IsDesigner()
        { 
            if (Application.SafeTopLevelCaptionFormat.Length < 16)
                return true;
            return false;
        }
        public static string GetInitializePath()
        {
            if (!DATA_ROW.Contains("PhotoIndex"))
            {//3.0及之前版本时
                if (AppName == "3.5")
                    AppName = "3.0";
            }
            //ProductName = "126";
            //ProductName = "3.5";
            return "D:\\设备保存的\\" + AppName + "\\参数目录\\";
        }

        public static string GetModelPath()
        {
            return GetInitializePath() + "模板\\";
        }
        public static string GetDatabasePath()
        {
            return GetInitializePath() + "数据库\\";
        }
        public static string GetIniPath()
        {
            return GetInitializePath() + "Ini文件\\";
        }
        public static string GetCalibPath()
        {
            return GetInitializePath() + "标定\\";
        }
        public static string GetMotionPath()
        {
            return GetGtsPath();
            //return Application.StartupPath + "\\参数目录\\运动控制卡\\";
        }
        public static string GetGtsPath()
        {
            return GetInitializePath() + "运动控制卡\\";
        }
        public static string GetImageSavePath()
        {
            return GetInitializePath() + "相机保存的窗口图像\\";
        }
        public static string GetCameraParaPath()
        {
            return GetInitializePath() + "相机文件\\";
        }
        public static string GetCroodPath()
        {
            return GetInitializePath() + "坐标\\";
        }
        public static string GetBarCodePath()
        {
            return GetInitializePath() + "二维码\\";
        }
        private static bool ProcessPause(ref CQueryTime watcher, bool bWhile = false)
        {
            return true;
        }
        private static bool SetSoftStop(bool bSoftStop = true, string SoftStopLog = "程序员忘记修改的异常")
        {
            return false;
        }
        private static bool GetSoftStop(bool bSoftStop = true)
        {
            return false;
        }
        private static bool GetSoftRunning()
        {
            return false;
        }
        public delegate bool CallBackGetRunning();
        public delegate bool CallBackSetStop(bool bSoftStop = true, string SoftStopLog = "程序员忘记修改的异常");
        public delegate bool CallBackGetStop(bool bSoftStop = true);
        public delegate bool CallBackProcessPause(ref CQueryTime watcher, bool bWhile = false);
        //此处先用默认函数初始化，如用到运控模块，会被复写
        public static CallBackSetStop SetStop = new CallBackSetStop(SetSoftStop);
        public static CallBackGetStop GetStop = new CallBackGetStop(GetSoftStop);
        public static CallBackSetStop ThrowStop = new CallBackSetStop(SetSoftStop);
        public static CallBackProcessPause Process_Pause = new CallBackProcessPause(ProcessPause);
        public static CallBackGetRunning GetRunning = new CallBackGetRunning(GetSoftRunning);
        public static CallBackGetRunning GetDebuging = new CallBackGetRunning(GetSoftRunning);

        /// <summary>
        /// 参数数据写入或刷新
        /// </summary>
        /// <param name="arrayLists">操作的控件集</param>
        /// <param name="bSave">true写入；false刷新</param>
        /// <returns>返回当前机种名称</returns>
        public delegate string CallBackParaIO(ArrayList arrayLists, bool bSave);

        public static System.Collections.ArrayList s_ArrayList_Controls = new System.Collections.ArrayList(100);
        public static System.Collections.ArrayList s_Container_Controls = new System.Collections.ArrayList(100);
        public static List<Type> s_lExValue_Controls = new List<Type>(100);


        #region 机种
        //public static string CurrentType = MP_table_Default;
        public const string MP_table_manager = "机种列表";   //数据表名管理
        public const string MP_table_Default = "默认机种";
        public const string MP_Common = "Common_";//通用参数表列

        public const string MP_Common_parameter = "通用参数";//通用参数表列
        public const string MP_table_parameter = "参数_";    //参数表列
        public const string MP_Table_Pos = "点位_";          //点位设置
        public const string MP_Table_Auto = "自动参数_";     //自动递归参数

        public const string SYSTEM_SECTION = "System";

        public static string GetFullPosTableStatic(string sType)
        {
            return MP_Table_Pos + sType;
        }

        public static string GetFullParaTable(string sType)
        {
            return MP_table_parameter + sType;
        }
        public static string GetFullPosTable(string sType)
        {
            return MP_Table_Pos + sType;
        }

        public static string GetFullParaPath(string sType)
        {
            return Publics.GetIniPath() + Publics.GetFullParaTable(sType) + ".ini";
        }
        public static string GetFullPosPath(string sType)
        {
            return Publics.GetIniPath() + Publics.GetFullPosTable(sType) + ".ini";
        }

        public static string GetCommonParaPath()
        {
            return Publics.GetIniPath() + Publics.GetCommonParaTable() + ".ini";
        }

        public static string GetTypeFromFull(string fullName)
        {
            if (fullName.StartsWith(MP_table_parameter))//StartsWith
                return fullName.Substring(MP_table_parameter.Length);
            else if (fullName.StartsWith(MP_Table_Pos))
                return fullName.Substring(MP_Table_Pos.Length);
            else
                return fullName;
        }
        public static string GetFullAutoTable(string sType)
        {
            return MP_Table_Auto + sType;
        }
        public static string GetCommonParaTable()
        {
            return MP_Common_parameter;
        }
        public static string GetDefaultTable()
        {
            return MP_table_Default;
        }
        #endregion

        #region 获取字符串中的数字
        public const double GET_NUM_NG = -99999999;
        /// <summary>
        /// 获取字符串中的第一个数字，异常时返回GET_NUM_NG
        /// </summary>
        /// <param name="sSrc">源字符串</param>
        /// <param name="bChange">是否修改源字符串</param>
        /// <returns></returns>
        static public double GetFristNum(ref string sSrc, bool bChange = false)
        {
            double dReturn = GET_NUM_NG;
            for (int i = 0; i < sSrc.Length; i++)
            {
                if (sSrc[i] == '0' || sSrc[i] == '1' || sSrc[i] == '2' || sSrc[i] == '3' || sSrc[i] == '4' ||
                    sSrc[i] == '5' || sSrc[i] == '6' || sSrc[i] == '7' || sSrc[i] == '8' || sSrc[i] == '9' ||
                    sSrc[i] == '.' || sSrc[i] == '+' || sSrc[i] == '-')
                {
                    for (int k = i + 1; k < sSrc.Length; k++)
                    {
                        if (sSrc[k] != '0' && sSrc[k] != '1' && sSrc[k] != '2' && sSrc[k] != '3' && sSrc[k] != '4' &&
                            sSrc[k] != '5' && sSrc[k] != '6' && sSrc[k] != '7' && sSrc[k] != '8' && sSrc[k] != '9' &&
                            sSrc[k] != '.')
                        {
                            dReturn = Convert.ToDouble(sSrc.Substring(i, k - i));
                            if (bChange)
                                sSrc = sSrc.Substring(k, sSrc.Length);
                            break;
                        }
                    }
                    if (GET_NUM_NG == dReturn)
                    {
                        dReturn = Convert.ToDouble(sSrc.Substring(i, sSrc.Length - i));
                    }
                    break;
                }
            }
            return dReturn;
        }
        /// <summary>
        /// 获取字符串的数字部分
        /// </summary>
        /// <param name="sSrc"></param>
        /// <returns></returns>
        static public string GetNoNum(string sSrc)
        {
            return Regex.Replace(sSrc, @"\d", "");  
        }

        /// <summary>
        /// 获取字符串中的数字 
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>数字</returns>
        static public decimal GetNumber(string str)
        {
            decimal result = (decimal)GET_NUM_NG;
            if (str != null && str != string.Empty)
            {
                // 正则表达式剔除非数字字符（不包含小数点.）
                //str = Regex.Replace(str, @"[^/d./d]", "");
                str = Regex.Replace(str, @"[^\d.\d]", "");//return Regex.Replace(key, @"[^\d]*", "");  
                // 如果是数字，则转换为decimal类型
                if (Regex.IsMatch(str, @"^[+-]?\d*[.]?\d*$"))
                {
                    if (str.Length > 0)
                        result = decimal.Parse(str);
                }
            }
            return result;
        }

        /// <summary>
        /// 获取字符串中的数字
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>数字</returns>
        static public int GetNumberInt(string str)
        {
            int result = (int)GET_NUM_NG;
            if (str != null && str != string.Empty)
            {
                // 正则表达式剔除非数字字符（不包含小数点.）
                str = Regex.Replace(str, @"[^\d.\d]", "");
                // 如果是数字，则转换为decimal类型
                if (Regex.IsMatch(str, @"^[+-]?\d*[.]?\d*$"))
                {
                    if (str.Length > 0)
                        result = int.Parse(str);
                }
            }
            return result;
        }
        #endregion

        #region 16进制字符串与byte[] int[] short[]的转换

        /// <summary>
        /// byte[]转化为short[]
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static short[] BytesToInt16s(byte[] bytes, int length)
        {
            int iCount = Math.Min(bytes.Count(), length);
            short[] Int16s = new short[iCount];
            for (int i = 0; i < iCount; i++)
            {
                Int16s[i] = Convert.ToInt16(bytes[i].ToString("X2") + bytes[i + 1].ToString("X2"), 16);
            }
            return Int16s;
        }

        /// <summary>
        /// byte[]转化为int[]
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="length"></param>
        /// <param name="bChanged"></param>
        /// <returns></returns>
        public static int[] BytesToInt32s(byte[] bytes, int length, bool bChanged = true)
        {
            int iCount = Math.Min(bytes.Count(), length);
            int[] Int32s = new int[iCount];
            for (int i = 0; i < iCount; i++)
            {
                if (bChanged)
                Int32s[i] = Convert.ToInt32(bytes[i + 2].ToString("X2") + bytes[i + 3].ToString("X2") + bytes[i].ToString("X2") + bytes[i + 1].ToString("X2"), 16);
                else
                Int32s[i] = Convert.ToInt32(bytes[i].ToString("X2") + bytes[i + 1].ToString("X2") + bytes[i + 2].ToString("X2") + bytes[i + 3].ToString("X2"), 16);
            }
            return Int32s;
        }

        /// <summary>
        /// byte[]转化为StringASCII
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string BytesToStringASCII(byte[] bytes, int length)
        {
            int iCount = Math.Min(bytes.Count(), length);
            return Encoding.ASCII.GetString(bytes, 0, iCount);
        }

        /// <summary>
        /// short[]转16进制字符串，长度不足AddrCount时在后补0
        /// </summary>
        /// <param name="Values"></param>
        /// <param name="AddrCount"></param>
        /// <returns></returns>
        public static byte[] Int16sToBytes(short[] Values, int AddrCount)
        {
            return Hex16ToBytes(Int16sToHex(Values, AddrCount));
        }

        /// <summary>
        /// int[]转16进制字符串，长度不足AddrCount时在后补0
        /// </summary>
        /// <param name="Values"></param>
        /// <param name="AddrCount"></param>
        /// <returns></returns>
        public static byte[] Int32sToBytes(int[] Values, int AddrCount, bool bChanged = true)
        {
            return Hex32ToBytes(Int32sToHex(Values, AddrCount));
        }

        /// <summary>
        /// short[]转16进制字符串，长度不足AddrCount时在后补0
        /// </summary>
        /// <param name="Values"></param>
        /// <param name="AddrCount"></param>
        /// <returns></returns>
        public static string Int16sToHex(short[] Values, int AddrCount)
        {
            string temp = "";
            int MinLength = Math.Min(Values.Length, AddrCount);
            for (int i = 0; i < MinLength; i++)
            {
                temp += Values[i].ToString("X4");
            }
            return temp.PadRight(AddrCount * 4, '0');
        }

        /// <summary>
        /// int[]转16进制字符串，长度不足AddrCount时在后补0
        /// </summary>
        /// <param name="Values"></param>
        /// <param name="AddrCount"></param>
        /// <returns></returns>
        public static string Int32sToHex(int[] Values, int AddrCount)
        {
            string temp = "";
            int MinLength = Math.Min(Values.Length, AddrCount);
            for (int i = 0; i < MinLength; i++)
            {
                temp += Values[i].ToString("X8");
            }
            return temp.PadRight(AddrCount * 8, '0');
        }

        /// <summary>
        /// byte[]转16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToHexString(byte[] bytes, int length = 9999) // 0xae00cf => "AE00CF "
        {
            string hexString = string.Empty;
            if (bytes != null)
            {
                StringBuilder strB = new StringBuilder();

                for (int i = 0; i < bytes.Length && i < length; i++)
                {
                    strB.Append(bytes[i].ToString("X2"));
                }

                hexString = strB.ToString();

            }
            return hexString;
        }

        /// <summary>
        /// 16进制字符串转byte[]
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static byte[] Hex8ToBytes(string hex)
        {
            byte[] hexByte = new byte[hex.Length / 2];
            for (int i = 0; i < (hex.Length / 2); i++)
            {
                string tempHex = hex.Substring(2 * i, 2);
                hexByte[i] = byte.Parse(tempHex, System.Globalization.NumberStyles.HexNumber);//// Convert.ToByte(tempHex, 0x10);
            }
            return hexByte;
        }

        /// <summary>
        /// 16位字符串转byte[]
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static byte[] Hex16ToBytes(string hex)
        {
            byte[] hexByte = new byte[hex.Length / 2];
            for (int i = 0; i < (hex.Length / 2); i += 2)
            {
                string str16 = hex.Substring(4 * i, 4);
                hexByte[i] = (byte)Convert.ToInt16(str16.Substring(0, 2), 16);
                hexByte[i + 1] = (byte)Convert.ToInt16(str16.Substring(2, 2), 16);
            }

            return hexByte;
        }

        /// <summary>
        /// 32位字符串转byte[]
        /// </summary>
        /// <param name="hex"></param>
        /// <param name="bLowHighChanged"></param>
        /// <returns></returns>
        public static byte[] Hex32ToBytes(string hex, bool bLowHighChanged = true)
        {
            byte[] hexByte = new byte[hex.Length / 2];
            for (int i = 0; i < (hex.Length / 2); i += 4)
            {
                string str32 = hex.Substring(8 * i, 8);
                if (bLowHighChanged)
                {
                    hexByte[i] = (byte)Convert.ToInt16(str32.Substring(4, 2), 16);
                    hexByte[i + 1] = (byte)Convert.ToInt16(str32.Substring(6, 2), 16);
                    hexByte[i + 2] = (byte)Convert.ToInt16(str32.Substring(0, 2), 16);
                    hexByte[i + 3] = (byte)Convert.ToInt16(str32.Substring(2, 2), 16);
                }
                else
                {
                    hexByte[i] = (byte)Convert.ToInt16(str32.Substring(0, 2), 16);
                    hexByte[i + 1] = (byte)Convert.ToInt16(str32.Substring(2, 2), 16);
                    hexByte[i + 2] = (byte)Convert.ToInt16(str32.Substring(4, 2), 16);
                    hexByte[i + 3] = (byte)Convert.ToInt16(str32.Substring(6, 2), 16);                
                }
            }

            return hexByte;
        }

        /// <summary>
        /// 16进制字符串转short
        /// </summary>
        /// <param name="HexStr"></param>
        /// <returns></returns>
        public static short HexToInt16(string HexStr)
        {
            return short.Parse(HexStr, System.Globalization.NumberStyles.HexNumber);
        }
        /// <summary>
        /// 16进制字符串转int
        /// </summary>
        /// <param name="HexStr"></param>
        /// <returns></returns>
        public static int HexToInt32(string HexStr)
        {
            return int.Parse(HexStr, System.Globalization.NumberStyles.HexNumber);
        }
        /// <summary>
        /// 16进制字符串转byte
        /// </summary>
        /// <param name="HexStr"></param>
        /// <returns></returns>
        public static byte HexToByte(string HexStr)
        {
            return byte.Parse(HexStr, System.Globalization.NumberStyles.HexNumber);
        }

        #endregion

        /// <summary>
        /// 保持目录数组存在
        /// </summary>
        /// <param name="ArrayList_Directory">目录字符串数组</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool VD_MaintainDirectory_Array(ref ArrayList ArrayList_Directory)
        {
            for (int i = 0; i < ArrayList_Directory.Count; i++)
            {
                bool rtn = VD_MaintainDirectory(ArrayList_Directory[i].ToString());
                if (false == rtn)
                {
                    MessageBox.Show("保持目录存在异常:" + ArrayList_Directory[i].ToString());
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 保持目录存在
        /// </summary>
        /// <param name="filePath">需要保持存在的目录</param>
        /// <returns>目录是否存在</returns>
        /// <remarks></remarks>
        public static bool VD_MaintainDirectory(String filePath)
        {
            try
            {
                bool rtn = FileSystem.DirectoryExists(filePath);
                if (false == rtn)
                {
                    //创建目录
                    if ((filePath.Split('.')).Length > 1)
                        FileSystem.CreateDirectory(Path.GetDirectoryName(filePath));
                    else
                        FileSystem.CreateDirectory(filePath);
                    //MsgBox("创建目录: " + filePath)
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("保持目录存在: " + ex.ToString());
                return false;
            }
            return true;
        }

        public static void GetAllDirectory(string path, ref List<string> direct)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            DirectoryInfo[] tempdirect = di.GetDirectories();
            try
            {
                for (int i = 0; i < tempdirect.Count(); i++)
                {
                    try
                    {
                        direct.Add(tempdirect[i].FullName);
                        if (tempdirect[i].GetDirectories().Count() > 0)
                        {
                            GetAllDirectory(tempdirect[i].FullName, ref direct);
                        }
                    }
                    catch (Exception e1) { }
                }
            }
            catch (Exception ex) { }
        }

        public static void GetAllFiles(string path, ref List<string> files, string keys = "")
        {
            List<string> direct = new List<string>();
            GetAllDirectory(path,ref direct);
            try
            {
                foreach (string dirPath in direct)
                {
                    ReadOnlyCollection<string> fileCollection = FileSystem.GetFiles(dirPath);
                    foreach (string file in fileCollection)
                    {
                        if (keys == "" || file.Contains(keys))
                            files.Add(file);
                    }
                }
            }
            catch { }
        }

        public static bool DirectoryExists(String filePath)
        {
            try
            {
                return FileSystem.DirectoryExists(filePath);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Path必须为完整路径+文件名，如：c:\\aa\\1.bmp，会自动创建完整路径
        public static void CreateDirectoryEx(string Path)
        {
            int nPos;
            string PathTemp;
            nPos = Path.LastIndexOf('\\');
            if (nPos < 0)
                nPos = Path.LastIndexOf('/');

            if (nPos < 0)
            {
                return;
            }
            if (!Path.Contains('.') && nPos != Path.Length - 1)
                nPos = Path.Length;
            PathTemp = Path.Substring(0, nPos);

            Directory.CreateDirectory(PathTemp);

        }

        public static void CopyDir(string fromDir, string toDir)
        {
            if (!Directory.Exists(fromDir))
                return;

            if (!Directory.Exists(toDir))
            {
                Directory.CreateDirectory(toDir);
            }

            string[] files = Directory.GetFiles(fromDir);
            foreach (string formFileName in files)
            {
                string fileName = Path.GetFileName(formFileName);
                string toFileName = Path.Combine(toDir, fileName);
                File.Copy(formFileName, toFileName);
            }
            string[] fromDirs = Directory.GetDirectories(fromDir);
            foreach (string fromDirName in fromDirs)
            {
                string dirName = Path.GetFileName(fromDirName);
                string toDirName = Path.Combine(toDir, dirName);
                CopyDir(fromDirName, toDirName);
            }
        }

        public static void CopyDirType(string fromDir, string toDir, string copyType)
        {
            if (!Directory.Exists(fromDir))
                return;

            //int posStart = toDir.Substring(0, toDir.Length - 1).LastIndexOf('\\') + 1;
            string toType = Path.GetFileName(toDir); // toDir.Substring(posStart, toDir.Length - 1 - posStart);
            if (toType == copyType)
            {
                if (!Directory.Exists(toDir))
                {
                    Directory.CreateDirectory(toDir);
                }

                string[] files = Directory.GetFiles(fromDir);
                foreach (string formFileName in files)
                {
                    string fileName = Path.GetFileName(formFileName);
                    string toFileName = Path.Combine(toDir, fileName);
                    File.Copy(formFileName, toFileName);
                }
            }
            string[] fromDirs = Directory.GetDirectories(fromDir);
            foreach (string fromDirName in fromDirs)
            {
                string dirName = Path.GetFileName(fromDirName);
                string toDirName = Path.Combine(toDir, dirName);
                CopyDirType(fromDirName, toDirName, copyType);
            }
        }

        public static bool MovwDirectory(string srcPath, string desPath)
        {
            Directory.Move(srcPath, desPath);
            return true;
        }

        public static bool FileExists(String filePath)
        {
            try
            {
                return FileSystem.FileExists(filePath);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool DeleteFile(string filePath)
        {
            try
            { 
                if (FileExists(filePath))
                    FileSystem.DeleteFile(filePath);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool CopyAllModelFile(string sSrs, string sDes = "")
        {
            if (sDes == "")
            {
                string nowTime = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString();
                sDes = sSrs + "_" + nowTime;
            }
            if (File.Exists(sSrs + ".shm"))
            {
                File.Copy(sSrs + ".shm", sDes + ".shm", true);
            }
            if (File.Exists(sSrs + ".bmp"))
            {
                File.Copy(sSrs + ".bmp", sDes + ".bmp", true);
            }
            if (File.Exists(sSrs + ".ini"))
            {
                File.Copy(sSrs + ".ini", sDes + ".ini", true);
            }
            if (File.Exists(sSrs + ".ncm"))
            {
                File.Copy(sSrs + ".ncm", sDes + ".ncm", true);
            }
            if (File.Exists(sSrs + ".reg"))
            {
                File.Copy(sSrs + ".reg", sDes + ".reg", true);
            }
            if (File.Exists(sSrs + "M.reg"))
            {
                File.Copy(sSrs + "M.reg", sDes + "M.reg", true);
            }
            if (File.Exists(sSrs + "M.reg"))
            {
                File.Copy(sSrs + "M.reg", sDes + "M.reg", true);
            }
            if (File.Exists(sSrs + "X.reg"))
            {
                File.Copy(sSrs + "X.reg", sDes + "X.reg", true);
            }
            if (File.Exists(sSrs + "Y.reg"))
            {
                File.Copy(sSrs + "Y.reg", sDes + "Y.reg", true);
            }
            if (File.Exists(sSrs + "X2.reg"))
            {
                File.Copy(sSrs + "X2.reg", sDes + "X2.reg", true);
            }
            if (File.Exists(sSrs + "Y2.reg"))
            {
                File.Copy(sSrs + "Y2.reg", sDes + "Y2.reg", true);
            }
            if (File.Exists(sSrs + "B.reg"))
            {
                File.Copy(sSrs + "B.reg", sDes + "B.reg", true);
            }
            if (File.Exists(sSrs + "_Line.tup"))
            {
                File.Copy(sSrs + "_Line.tup", sDes + "_Line.tup", true);
            }
            if (File.Exists(sSrs + "_Circle.tup"))
            {
                File.Copy(sSrs + "_Circle.tup", sDes + "_Circle.tup", true);
            }
            return true;
        }

        /// <summary>
        /// 以追加方式在文件中添加一行，在末尾换行
        /// </summary>
        /// <param name="path">要追加的文件路径</param>
        /// <param name="writeString">要追加的一行</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool VD_WriteAppendTxtLine(String path, String writeString)
        {
            StreamWriter sw = new StreamWriter(path, true); //true是指以追加的方式打开指定文件  
            sw.WriteLine(writeString);
            sw.Flush();
            sw.Close();
            sw = null;
            return true;
        }

        /// <summary>
        /// 获取当前日期
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string GetSystemDataTime()
        {
            string Str_DataTime_Now = DateTime.Today.Year.ToString() + " 年 ";
            Str_DataTime_Now += DateTime.Today.Month.ToString() + " 月 ";
            Str_DataTime_Now += DateTime.Today.Day.ToString() + " 日 ";

            return Str_DataTime_Now;

        }

        public static string GetSystemDataTime_Hour()
        {
            return GetSystemDataTime() + DateTime.Now.Hour.ToString() + " 时 ";

        }

        /// <summary>
        /// 获取当前日期精确到分
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string GetSystemDataTime_Minute()
        {
            return GetSystemDataTime_Hour() + DateTime.Now.Minute.ToString() + " 分 ";
        }

        /// <summary>
        /// 获取当前日期精确到秒
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string GetSystemDataTime_Second()
        {
            return GetSystemDataTime_Hour() + DateTime.Now.Second.ToString() + " 秒 ";
        }

        public static string GetSystem_Second()
        {
            string Str_DataTime_Now = DateTime.Now.Hour.ToString() + " 时 ";
            Str_DataTime_Now += DateTime.Now.Minute.ToString() + " 分 ";
            Str_DataTime_Now += DateTime.Now.Second.ToString() + " 秒 ";

            return Str_DataTime_Now;

        }
        public static string GetSystem_MilliSecond()
        {
            string Str_DataTime_Now = DateTime.Now.Hour.ToString() + " 时 ";
            Str_DataTime_Now += DateTime.Now.Minute.ToString() + " 分 ";
            Str_DataTime_Now += DateTime.Now.Second.ToString() + " 秒 ";
            Str_DataTime_Now += DateTime.Now.Millisecond.ToString();//+ " 毫秒 "

            return Str_DataTime_Now;
        }

        /// <summary>
        /// 获取界面上所有此控件，可在控件类里加入此函数
        /// </summary>
        /// <param name="_Controls"></param>
        /// <param name="ArrayList_Controls"></param>
        public void GetAllControlsRecursion(System.Windows.Forms.Control.ControlCollection _Controls, ref System.Collections.ArrayList ArrayList_Controls)
        {//用于继承的子类调用
            Type tFind = this.GetType();
            GetAllControlsRecursion(tFind, _Controls, ref ArrayList_Controls);
        }
        /// <summary>
        /// 获得界面上所有本控件:递归函数
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        static public void GetAllControlsRecursion(Type tFind, System.Windows.Forms.Control.ControlCollection _Controls, ref System.Collections.ArrayList ArrayList_Controls)
        {
            List<Type> tFinds = new List<Type>(1);
            tFinds.Clear();
            tFinds.Add(tFind);
            GetAllControlsRecursion(tFinds, _Controls, ref ArrayList_Controls);
            //foreach (System.Windows.Forms.Control ct in _Controls)
            //{
            //    if (ct.GetType().Equals(tFind) || ct.GetType().BaseType.Equals(tFind))
            //    {//只判断一层基类 //typeof(B).BaseType == typeof(A)
            //        if (!ArrayList_Controls.Contains(ct))
            //            ArrayList_Controls.Add(ct);
            //    }
            //    else if (ct.GetType().Equals(typeof(System.Windows.Forms.GroupBox)) ||
            //             ct.GetType().Equals(typeof(System.Windows.Forms.Panel)) ||
            //             ct.GetType().Equals(typeof(System.Windows.Forms.TabPage)) ||
            //             ct.GetType().Equals(typeof(System.Windows.Forms.TabControl))
            //        //ct.GetType().Equals(UserControl_ImageSource.GetType()) ||
            //        //    ct.GetType().Equals(UserControlJs_AxisManual.GetType()) ||
            //            )
            //    {
            //        //遍历所有系统容器，获取目标控件
            //        //Type t = typeof(System.Windows.Forms.GroupBox);
            //        GetAllControlsRecursion(tFind, ct.Controls, ref ArrayList_Controls);
            //    }
            //    else
            //    {
            //        //遍历所有自定义容器，获取目标控件
            //        foreach (var key in s_Container_Controls)
            //        { 
            //            if (ct.GetType().Equals((Type)key))
            //            {
            //                GetAllControlsRecursion(tFind, ct.Controls, ref ArrayList_Controls);
            //                break;
            //            }
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 获得界面上所有目标控件:递归函数（减少遍历时间）
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        static public void GetAllControlsRecursion(List<Type> tFinds, System.Windows.Forms.Control.ControlCollection _Controls, ref System.Collections.ArrayList ArrayList_Controls)
        {

            foreach (System.Windows.Forms.Control ct in _Controls)
            {
                if (IsRecursionControl(tFinds, ct))
                {//只判断一层基类 //typeof(B).BaseType == typeof(A)
                    if (!ArrayList_Controls.Contains(ct))
                        ArrayList_Controls.Add(ct);
                }
                else if (ct.GetType().Equals(typeof(System.Windows.Forms.GroupBox)) ||
                            ct.GetType().Equals(typeof(System.Windows.Forms.Panel)) ||
                            ct.GetType().Equals(typeof(System.Windows.Forms.TabPage)) ||
                            ct.GetType().Equals(typeof(System.Windows.Forms.TabControl))
                    //ct.GetType().Equals(UserControl_ImageSource.GetType()) ||
                    //    ct.GetType().Equals(UserControlJs_AxisManual.GetType()) ||
                        )
                {
                    //遍历所有系统容器，获取目标控件
                    //Type t = typeof(System.Windows.Forms.GroupBox);
                    GetAllControlsRecursion(tFinds, ct.Controls, ref ArrayList_Controls);
                }
                else
                {
                    //遍历所有自定义容器，获取目标控件
                    foreach (var key in s_Container_Controls)
                    {
                        if (ct.GetType().Equals((Type)key))
                        {
                            GetAllControlsRecursion(tFinds, ct.Controls, ref ArrayList_Controls);
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 判断控件是否属于目标类型
        /// </summary>
        /// <param name="tFinds"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        static bool IsRecursionControl(System.Collections.Generic.List<Type> tFinds, System.Windows.Forms.Control ct)
        {
            foreach (var tFind in tFinds)
            {
                if (ct.GetType().Equals(tFind) || ct.GetType().BaseType.Equals(tFind))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 获得GroupBox上所有本控件:递归函数
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        static public void GetAllControlsRecursion(Type tFind, System.Windows.Forms.GroupBox cGroupBox, ref System.Collections.ArrayList ArrayList_Controls)
        {
            GetAllControlsRecursion(tFind, cGroupBox.Controls, ref ArrayList_Controls);
        }
        static public void GetAllControlsRecursion(Type tFind, System.Windows.Forms.Panel cGroupBox, ref System.Collections.ArrayList ArrayList_Controls)
        {
            GetAllControlsRecursion(tFind, cGroupBox.Controls, ref ArrayList_Controls);
        }
        static public void GetAllControlsRecursion(Type tFind, System.Windows.Forms.TabControl cGroupBox, ref System.Collections.ArrayList ArrayList_Controls)
        {
            GetAllControlsRecursion(tFind, cGroupBox.Controls, ref ArrayList_Controls);
        }

        /// <summary>
        /// 返回声明该成员的类全名 从命名空间开始逐项列出
        /// </summary>
        /// <returns>返回声明该成员的类全名</returns>
        /// <remarks></remarks>
        static public string GetDeclaringType()
        {//获取当前类的名称，相当于给类名加双引号，继承仍是父类名，每个控件都要重写它，意义不大
            return (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
        }

        /// <summary>
        /// 获取类的名称
        /// </summary>
        /// <returns>返回类的名称</returns>
        /// <remarks></remarks>
        static public string GetReflectedType()
        {//获取当前类的名称，相当于给类名加双引号，继承仍是父类名，每个控件都要重写它，意义不大
            return (System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name);
        }

        static public string GetMethodName()
        {//获取当前类的名称，相当于给类名加双引号，继承仍是父类名，每个控件都要重写它，意义不大
            return (System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        public static void RefreshAllUI(Type tFind, System.Windows.Forms.Control.ControlCollection _Controls, System.Collections.ArrayList ArrayList_Controls = null, bool bAwaysRefresh = false)
        {
            if (null == ArrayList_Controls || ArrayList_Controls.Count == 0)
            {
                if (null == ArrayList_Controls)
                {
                    ArrayList_Controls = s_ArrayList_Controls;
                    ArrayList_Controls.Clear();
                    //Public.GetAllControlsRecursion(tFind, _Controls, ref s_ArrayList_Controls);
                }
                Publics.GetAllControlsRecursion(tFind, _Controls, ref ArrayList_Controls);
            }
            foreach (System.Windows.Forms.Control obj in ArrayList_Controls)
            {//System.Activator.CreateInstance(t) //如何强制转换为tFind类型？！！
                if (bAwaysRefresh || (obj.Visible && obj.Enabled))
                    obj.Refresh();
            }
        }
        public static void BytesToIntPtr(byte[] bytes,ref IntPtr buffer)
        {
            //try
            //{
            int size = bytes.Length;
            buffer = Marshal.AllocHGlobal(size);
            Marshal.Copy(bytes, 0, buffer, size);
            //}
            //finally
            //{
            //    Marshal.FreeHGlobal(buffer);
            //}
        }

        public const double CV_PI = 3.1415926535897932384626433832795;
        #region 角度和弧度间转换
        /// <summary>
        /// 弧度到角度的转换
        /// </summary>
        /// <param name="_Rad">输入弧度</param>
        /// <returns>转换后的角度</returns>
        /// <remarks></remarks>
        public static double ToDegree(double rad)
        {
            return (rad * 180.0 / CV_PI);
        }

        /// <summary>
        /// 角度到弧度的转换
        /// </summary>
        /// <param name="_Degree">输入角度</param>
        /// <returns>转换后的弧度</returns>
        /// <remarks></remarks>
        public static double ToRad(double degree)
        {
            return (degree * CV_PI / 180.0);
        }
        #endregion
    }
}

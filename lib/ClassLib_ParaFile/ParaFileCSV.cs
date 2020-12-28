using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace ClassLib_ParaFile
{
    public class ParaFileCSV
    {
        string _sFullPath = "";
        public static string m_sFullPath = "";
        public ParaFileCSV(string fullPath)
        {
            _sFullPath = fullPath;
        }
        public static void SetPath(string fullPath)
        {
            m_sFullPath = fullPath;
        }
        public bool SaveRow(string[] row)
        {
            return SaveRow(row, _sFullPath);
        }
        public bool SaveDataTable(DataTable dt)
        {
            return SaveDataTable(dt, _sFullPath);
        }
        public bool SaveData(string[,] dt)
        {
            return SaveData(dt, _sFullPath);
        }
        public static bool SaveRow(string[] row, string fullPath = "")
        {
            FileStream fs = null;
            StreamWriter sw = null;
            try
            {
                if (fullPath == "")
                    fullPath = m_sFullPath;
                if (!File.Exists(fullPath))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                    File.Create(fullPath).Close();
                }
                 fs = new FileStream(fullPath, System.IO.FileMode.Append, System.IO.FileAccess.Write);
                 sw = new StreamWriter(fs, System.Text.Encoding.Default);
                string data = "";
                for (int i = 0; i < row.Count(); i++)
                {
                    string str = row[i];
                    str = str.Replace("\"", "\"\"");
                    if (str.Contains(',') || str.Contains('"')
                       || str.Contains('\r') || str.Contains('\n'))
                    {
                        str = string.Format("\"{0}\"", str);
                    }
                    data += str;
                    if (i < row.Count() - 1)
                    {
                        data += ",";
                    }
                }
                sw.WriteLine(data);
                sw.Flush();
                fs.Flush();
                sw.Close();
                fs.Close();
                return true;
            }
            catch
            {
                //sw.Close();
                //fs.Close();
                return false;
            }
        }

        public static bool SaveDataTable(DataTable dt, string fullPath = "")
        {
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string[] Row = new string[dt.Rows[i].ItemArray.Count()];
                    for (int j = 0; j < dt.Rows[i].ItemArray.Count(); j++)
                    {
                        Row[j] = (string)dt.Rows[i].ItemArray[j];
                    }
                    if (!SaveRow(Row, fullPath))
                        return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool SaveData(string[,] dt, string fullPath = "")
        {
            try
            {
                if (fullPath == "")
                    fullPath = m_sFullPath;
                for (int i = 0; i < dt.GetLength(0); i++)
                {
                    string[] Row = new string[dt.GetLength(1)];
                    for (int j = 0; j < dt.GetLength(1); j++)
                    {
                        Row[j] = (string)dt.GetValue(i, j);
                    }
                    if (!SaveRow(Row, fullPath))
                        return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        //读取生产数据
        public static string[] ReadData(string fullPath="")
        {
            string[] strs = null;
            try
            {
                //FileStream fs = new FileStream(fullPath, System.IO.FileMode.Append, System.IO.FileAccess.Read);
                StreamReader sw = new StreamReader(fullPath,System.Text.ASCIIEncoding.Default);
                string str = sw.ReadToEnd();
                 strs = str.Split('\n');
            }
            catch
            {
                return strs;
            }
            return strs;
        }
    }
}

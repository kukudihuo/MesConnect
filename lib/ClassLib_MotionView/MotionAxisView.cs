using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClassLib_MotionUI;
using System.Collections;
using CCWin;
using ClassLib_ParaFile;

namespace ClassLib_MotionView
{
    public partial class MotionAxisView : UserControl
    {
        public MotionAxisView()
        {
            InitializeComponent();
        }

        internal string IsInversion = "";
        internal string IsReset = "";
        internal string LeftButtonName = "";
        internal string RightButtonName = "";
        internal string IsRemovable = "";
        internal string Sort = "";

        public void DataRefresh()
        {
            try
            {
                this.Controls.Clear();
                string iniFileName = Publics.GetMotionPath() + "Motion.ini";// Application.StartupPath + "\\参数目录\\运动控制卡\\Motion.ini";
                string numCARD = ParaFileINI.ReadINI("CARD", "CardTotal", iniFileName);//读取卡的数量
                
                if (int.Parse(numCARD) >= 1)
                {
                    int maxAxis =int.Parse( ParaFileINI.ReadINI("AXIS", "AxisTotal", iniFileName));

                    object[,] sortList = new object[maxAxis, 3];
                    Dictionary<string, string> sortData = new Dictionary<string, string>();
                    string strSort = "";
                    int iRow = 0;//判断是否有多页
                    //生成轴并配置轴号及轴名称，导程，细分
                    for (int i = 1; i <= maxAxis; i++)
                    {
                        strSort = "";
                        string sValue = "Axis" + i;
                        string str = ParaFileINI.ReadINI("AXIS", sValue, iniFileName);
                        string[] sst = str.Trim().Split('_');
                        strSort += sst[0] + "_";//轴号
                        strSort += sst[2] + "_";//中文轴名
                        if (sst.Count() > 1)
                        {
                            ParaFileINI paraFile = new ParaFileINI(iniFileName, "RESET" + i);
                            if (paraFile.m_bEx)
                            {
                                paraFile.ReadINI("IsInversion", ref IsInversion);
                                strSort += IsInversion + "_";//是否反向
                                paraFile.ReadINI("IsReset", ref IsReset);
                                strSort += IsReset + "_";//是否复位
                                paraFile.ReadINI("LeftButtonName", ref LeftButtonName);
                                strSort += LeftButtonName + "_";//左按钮名称
                                paraFile.ReadINI("RightButtonName", ref RightButtonName);
                                strSort += RightButtonName + "_";//右按钮名称
                                paraFile.ReadINI("IsRemovable", ref IsRemovable);
                                strSort += IsRemovable;//是否可移动
                                sortData.Add(sValue, strSort);//保存控件的参数
                                paraFile.ReadINI("Sort", ref Sort);
                                string[] sSort = Sort.Trim().Split('|');//排序
                                if (sSort.Length == 2)
                                {
                                    sortList.SetValue(sValue, i - 1, 0);//设置轴号
                                    sortList.SetValue(int.Parse(sSort[0]), i - 1, 1);//设置页
                                    sortList.SetValue(int.Parse(sSort[1]), i - 1, 2);//设置页里面的顺序

                                    if (int.Parse(sSort[0]) > 1) iRow = -1;
                                }
                                else
                                {
                                    sortList.SetValue(sValue, i - 1, 0);//设置轴号
                                    sortList.SetValue(1, i - 1, 1);//设置页
                                    sortList.SetValue(i - 1, i - 1, 2);//设置页里面的顺序
                                }
                            }
                        }
                    }

                    Orderby(sortList, new int[] { 1, 2 }, 0);//排序
                    
                    if (iRow == 0)//
                    {
                        for (int j = 0; j < maxAxis; j++)
                        {
                            string[] mStr = sortData[(string)sortList[j, 0]].Trim().Split('_');

                            ClassLib_MotionUI.Control_Axis caControl = new ClassLib_MotionUI.Control_Axis();
                            caControl.Dock = DockStyle.Top;
                            caControl.AxisNum = int.Parse(mStr[0]);//轴号
                            caControl.DirNeget = mStr[2] == "0" ? false : true;//方向是否反向
                            caControl.Name = "caControl" + mStr[0];//控件Name
                            if (caControl.Axis != null)
                            caControl.Axis.m_sName = mStr[1];//轴名称
                            caControl.LabelName = mStr[1];//轴名称
                            caControl.ResetEnabled = mStr[3] == "0" ? false : true;//是否复位
                            caControl.LeftLabel = mStr[4].Equals("") ? "-" : mStr[4];
                            caControl.RightLabel = mStr[5].Equals("") ? "+" : mStr[5];
                            caControl.Moveble = mStr[6] == "0" ? false : true;
                            
                            this.Controls.Add(caControl);
                        }
                    }
                    else
                    {
                        TabControl _tabCont = new TabControl();
                        TabPage page = null;
                        for (int j = 0; j < maxAxis; j++)
                        {
                            int iRowNow = (int)sortList[j, 1];
                            if (iRowNow != iRow)
                            {
                                if(page != null)
                                    _tabCont.Controls.Add(page);

                                page = new TabPage();
                                page.Name = "AxisCard" + iRowNow.ToString();
                                page.Text = "  卡" + iRowNow.ToString() + "  ";
                                iRow = iRowNow;
                            }
                            string[] mStr = sortData[(string)sortList[j, 0]].Trim().Split('_');

                            Control_Axis caControl = new Control_Axis();
                            caControl.Dock = DockStyle.Top;
                            caControl.AxisNum = int.Parse(mStr[0]);//轴号
                            caControl.DirNeget = mStr[2] == "0" ? false : true;//方向是否反向
                            caControl.Name = "caControl" + mStr[0];//控件Name
                            if (caControl.Axis != null)
                                caControl.Axis.m_sName = mStr[1];//轴名称
                            caControl.LabelName = mStr[1];//轴名称
                            caControl.ResetEnabled = mStr[3] == "0" ? false : true;//是否复位
                            caControl.LeftLabel = mStr[4].Equals("") ? "-" : mStr[4];
                            caControl.RightLabel = mStr[5].Equals("") ? "+" : mStr[5];
                            caControl.Moveble = mStr[6] == "0" ? false : true;

                            page.Controls.Add(caControl);
                        }
                        _tabCont.Controls.Add(page);
                        _tabCont.Name = "CardAxisView";
                        _tabCont.Dock = DockStyle.Fill;
                        _tabCont.TabIndex = 1;
                        this.Controls.Add(_tabCont);
                    }



                    //for (int i = 1; i <= maxAxis; i++)
                    //{
                    //    string sValue = "Axis" + i;
                    //    string str = ParaFileINI.ReadINI("AXIS", sValue, iniFileName);
                    //    string[] sst = str.Trim().Split('_');
                    //    //if (sst[2].Length == 0) continue;
                    //    Control_Axis caControl = new Control_Axis();
                    //    caControl.Dock = DockStyle.Top;
                    //    caControl.AxisNum = int.Parse(sst[0]);//轴号
                    //    caControl.DirNeget = false;//方向是否反向
                    //    caControl.Name = "caControl"+ sst[0];//控件Name
                    //    caControl.LabelName = sst[2];//轴名称
                    //    caControl.ResetEnabled = true;//是否复位
                    //    caControl.LeftLabel = "+";
                    //    caControl.RightLabel = "-";
                    //    caControl.Moveble = true;
                    //    this.Controls.Add(caControl);
                    //}
                }
                else
                {
                    MessageBoxEx.Show("卡的数量小于1，请检查参数文件或请把参数文件复制到：" + iniFileName);
                }
            }
            catch (Exception e)
            {
                MessageBoxEx.Show("轴显示控件刷新异常" + e.ToString());
            }
        }
        /// <summary>
        /// 对二维数组排序
        /// </summary>
        /// <param name="values">排序的二维数组</param>
        /// <param name="orderColumnsIndexs">排序根据的列的索引号数组</param>
        /// <param name="type">排序的类型，1代表降序，0代表升序</param>
        /// <returns>返回排序后的二维数组</returns>
        public static object[,] Orderby(object[,] values, int[] orderColumnsIndexs, int type)
        {
            object[] temp = new object[values.GetLength(1)];
            int k;
            int compareResult;
            for (int i = 0; i < values.GetLength(0); i++)
            {
                for (k = i + 1; k < values.GetLength(0); k++)
                {
                    if (type.Equals(1))
                    {
                        for (int h = 0; h < orderColumnsIndexs.Length; h++)
                        {
                            compareResult = Comparer.Default.Compare(GetRowByID(values, k).GetValue(orderColumnsIndexs[h]), GetRowByID(values, i).GetValue(orderColumnsIndexs[h]));
                            if (compareResult.Equals(1))
                            {
                                temp = GetRowByID(values, i);
                                Array.Copy(values, k * values.GetLength(1), values, i * values.GetLength(1), values.GetLength(1));
                                CopyToRow(values, k, temp);
                            }
                            if (compareResult != 0)
                                break;
                        }
                    }
                    else
                    {
                        for (int h = 0; h < orderColumnsIndexs.Length; h++)
                        {
                            compareResult = Comparer.Default.Compare(GetRowByID(values, k).GetValue(orderColumnsIndexs[h]), GetRowByID(values, i).GetValue(orderColumnsIndexs[h]));
                            if (compareResult.Equals(-1))
                            {
                                temp = GetRowByID(values, i);
                                Array.Copy(values, k * values.GetLength(1), values, i * values.GetLength(1), values.GetLength(1));
                                CopyToRow(values, k, temp);
                            }
                            if (compareResult != 0)
                                break;
                        }
                    }
                }
            }
            return values;

        }
        /// <summary>
        /// 获取二维数组中一行的数据
        /// </summary>
        /// <param name="values">二维数据</param>
        /// <param name="rowID">行ID</param>
        /// <returns>返回一行的数据</returns>
        static object[] GetRowByID(object[,] values, int rowID)
        {
            if (rowID > (values.GetLength(0) - 1))
                throw new Exception("rowID超出最大的行索引号!");

            object[] row = new object[values.GetLength(1)];
            for (int i = 0; i < values.GetLength(1); i++)
            {
                row[i] = values[rowID, i];

            }
            return row;

        }
        /// <summary>
        /// 复制一行数据到二维数组指定的行上
        /// </summary>
        /// <param name="values"></param>
        /// <param name="rowID"></param>
        /// <param name="row"></param>
        static void CopyToRow(object[,] values, int rowID, object[] row)
        {
            if (rowID > (values.GetLength(0) - 1))
                throw new Exception("rowID超出最大的行索引号!");
            if (row.Length > (values.GetLength(1)))
                throw new Exception("row行数据列数超过二维数组的列数!");
            for (int i = 0; i < row.Length; i++)
            {
                values[rowID, i] = row[i];
            }
        }
    }
}

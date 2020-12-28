using CCWin.SkinControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCWin;
using ClassLib_ParaFile;

namespace MotionEdit
{
    //帮助数据设置类
    class HelpDataClass
    {
        private static HelpDataClass _helpDataClass = new HelpDataClass();
        private HelpDataClass(){ }
        public static HelpDataClass GetInstance()
        {
            return _helpDataClass;
        }

        /// <summary>
        /// 加载参数说明列表数据
        /// </summary>
        /// <param name="lvParameter">参数说明列表Name</param>
        public void LoadHelpParameter(DataGridView dgvHelpParameter)
        {
            try
            {
                DataTable dtParameter = new DataTable();

                dtParameter.Columns.Add("序号").ReadOnly = true;//设置为只读
                dtParameter.Columns.Add("组名").ReadOnly = true;
                dtParameter.Columns.Add("名称").ReadOnly = true;
                dtParameter.Columns.Add("说明").ReadOnly = true;

                DataRow row = null;
                string iniFileName = Publics.GetMotionPath() + "Help.ini"; ;// Application.StartupPath + "\\参数目录\\运动控制卡\\Help.ini";
                string sValue = "";
                string[] sValues;
                //读取数据，数据的个数未知故默认给1000，在读取时结果是空或0则表示读完
                for (int i = 1; i < 100; i++)
                {
                    for (int j = 1; j < 1000; j++)
                    {
                        sValue = ParaFileINI.ReadINI("SHOW", "No" + i.ToString() + "." + j.ToString(), iniFileName);
                        //没有数据则跳出
                        if (sValue == null || sValue.Equals("") || sValue.Equals("0"))
                            break;
                        else
                        {
                            sValues = sValue.Trim().Split('@');
                            if (sValues.Length < 3)
                            {
                                MessageBoxEx.Show("Help.ini的数据格式有误。SHOW分组的第" + j.ToString() + "个数据个数少于3个。");
                                return;
                            }
                            row = dtParameter.NewRow();
                            row["序号"] = "No" + i.ToString() + "." + j.ToString();
                            row["组名"] = sValues[0];
                            row["名称"] = sValues[1];
                            row["说明"] = sValues[2];
                            dtParameter.Rows.Add(row);
                        }
                    }
                    //下一组没有数据则跳出
                    sValue = ParaFileINI.ReadINI("SHOW", "No" + (i+1).ToString() + ".1", iniFileName);
                    if (sValue == null || sValue.Equals("") || sValue.Equals("0"))
                        break;
                }
                    
                if (dtParameter.Rows.Count > 0)
                {
                    dgvHelpParameter.DataSource = dtParameter;
                    //禁止排序
                    for (int i = 0; i < dgvHelpParameter.Columns.Count; i++)
                    {
                        dgvHelpParameter.Columns[i].SortMode = DataGridViewColumnSortMode.Programmatic;
                    }
                    dgvHelpParameter.ClearSelection();
                    //// 设置自动换行  效果不好
                    //dgvHelpParameter.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    ////设置自动调整高度  
                    //dgvHelpParameter.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                }
            }
            catch (Exception)
            {
                MessageBoxEx.Show("读取Help.ini文件的SHOW分组出异常");
            }
        }

        /// <summary>
        /// 加载调试说明列表数据
        /// </summary>
        /// <param name="lvParameter">参数调试说明列表Name</param>
        public void LoadHelpDebug(DataGridView dgvHelpDebug,string projectName)
        {

            try
            {
                DataTable dtParameter = new DataTable();

                dtParameter.Columns.Add("序号").ReadOnly = true;//设置为只读
                dtParameter.Columns.Add("组名").ReadOnly = true;
                dtParameter.Columns.Add("名称").ReadOnly = true;
                dtParameter.Columns.Add("说明").ReadOnly = true;

                DataRow row = null;
                string iniFileName = Publics.GetMotionPath() + "Help.ini"; // Application.StartupPath + "\\参数目录\\运动控制卡\\Help.ini";
                string sValue = "";
                string[] sValues;
                //读取数据，数据的个数未知故默认给1000，在读取时结果是空或0则表示读完
                for (int i = 1; i < 100; i++)
                {
                    for (int j = 1; j < 1000; j++)
                    {
                        sValue = ParaFileINI.ReadINI("DEBUG"+ projectName, "No" + i.ToString() + "." + j.ToString(), iniFileName);
                        //没有数据则跳出
                        if (sValue == null || sValue.Equals("") || sValue.Equals("0"))
                            break;
                        else
                        {
                            sValues = sValue.Trim().Split('@');
                            if (sValues.Length < 3)
                            {
                                MessageBoxEx.Show("Help.ini的数据格式有误。SHOW分组的第" + j.ToString() + "个数据个数少于3个。");
                                return;
                            }
                            row = dtParameter.NewRow();
                            row["序号"] = "No" + i.ToString() + "." + j.ToString();
                            row["组名"] = sValues[0];
                            row["名称"] = sValues[1];
                            row["说明"] = sValues[2];
                            dtParameter.Rows.Add(row);
                        }
                    }
                    //下一组没有数据则跳出
                    sValue = ParaFileINI.ReadINI("SHOW", "No" + (i + 1).ToString() + ".1", iniFileName);
                    if (sValue == null || sValue.Equals("") || sValue.Equals("0"))
                        break;
                }

                if (dtParameter.Rows.Count > 0)
                {
                    dgvHelpDebug.DataSource = dtParameter;
                    //禁止排序
                    for (int i = 0; i < dgvHelpDebug.Columns.Count; i++)
                    {
                        dgvHelpDebug.Columns[i].SortMode = DataGridViewColumnSortMode.Programmatic;
                    }
                    dgvHelpDebug.ClearSelection();
                    //// 设置自动换行  效果不好
                    //dgvHelpDebug.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    ////设置自动调整高度  
                    //dgvHelpDebug.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                }
            }
            catch (Exception)
            {
                MessageBoxEx.Show("读取Help.ini文件的SHOW分组出异常");
            }
        }
    }
}

using CCWin;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLib_ParaFile;

namespace ClassLib_MotionEdit
{
    public class OperationClass
    {
        internal DataGridView _dgvMotionCard = null;
        internal DataGridView _dgvMotionIn = null;
        internal DataGridView _dgvMotionOut = null;
        internal DataGridView _dgvMotionTotal = null;

        internal DataTable dtCard = null;
        internal DataTable dtIn = null;
        internal DataTable dtOut = null;
        internal DataTable dtTotal = null;

        //const short AXIS_IN_CARD = 8;
        //const short IO_IN_CARD = 24;
        //const short HOME_IN_CARD = 8;
        //const short IO_IN_CARD_EXT = 24;
        //const int Max_Axis = 8;

        internal string IsNegateResetDirection = "";
        internal string ResetType = "";
        internal string FastHomeSpeed = "";
        internal string FastHomeAcc = "";
        internal string SlowHomeSpeed = "";
        internal string SlowHomeAcc = "";
        internal string RollBackDistance_1 = "";
        internal string RollBackDistance_2 = "";
        internal string TimeOut = "";
        internal string IsServo = "";
        internal string IsInversion = "";
        internal string IsReset = "";
        internal string LeftButtonName = "";
        internal string RightButtonName = "";
        internal string IsRemovable = "";
        internal string Sort = "";
		
		String ChangedIOFile = Publics.GetMotionPath() + "ChangedIO.ini"; //Application.StartupPath + "\\参数目录\\运动控制卡\\ChangedIO.ini";


        #region 

        public OperationClass(DataGridView dgvMotionCard, DataGridView dgvMotionIn,
            DataGridView dgvMotionOut, DataGridView dgvMotionTotal)
        {
            _dgvMotionCard = dgvMotionCard;
            _dgvMotionIn = dgvMotionIn;
            _dgvMotionOut = dgvMotionOut;
            _dgvMotionTotal = dgvMotionTotal;
            InitDataTable();//添加列表头
        }
        #region 添加列表头
        public bool InitDataTable()
        {
            if (dtCard == null)
            {
                dtCard = new DataTable();

                dtCard.Columns.Add("轴号").ReadOnly = true;//设置为只读
                dtCard.Columns.Add("英文轴名");
                dtCard.Columns.Add("中文轴名");
                dtCard.Columns.Add("导程");
                dtCard.Columns.Add("细分");
                dtCard.Columns.Add("速度");
                dtCard.Columns.Add("加速度");
                dtCard.Columns.Add("复位方向");
                dtCard.Columns.Add("复位类型");
                dtCard.Columns.Add("快-速度");
                dtCard.Columns.Add("快-加速度");
                dtCard.Columns.Add("慢-速度");
                dtCard.Columns.Add("慢-加速度");
                dtCard.Columns.Add("一次回距离");
                dtCard.Columns.Add("二次回距离");
                dtCard.Columns.Add("超时时间");
                dtCard.Columns.Add("是否是伺服");
                dtCard.Columns.Add("是否反向");
                dtCard.Columns.Add("是否复位");
                dtCard.Columns.Add("左按钮名称");
                dtCard.Columns.Add("右按钮名称");
                dtCard.Columns.Add("是否可移动");
                dtCard.Columns.Add("排序");
            }
            else
                dtCard.Clear();

            if (dtIn == null)
            {
                dtIn = new DataTable();

                dtIn.Columns.Add("卡号").ReadOnly = true;
                dtIn.Columns.Add("端口号").ReadOnly = true;
                dtIn.Columns.Add("名称");
                dtIn.Columns.Add("类型").ReadOnly = true;
                dtIn.Columns.Add("重定向");
            }
            else
                dtIn.Clear();

            if (dtOut == null)
            {
                dtOut = new DataTable();

                dtOut.Columns.Add("卡号").ReadOnly = true;
                dtOut.Columns.Add("端口号").ReadOnly = true;
                dtOut.Columns.Add("名称");
                dtOut.Columns.Add("类型").ReadOnly = true;
                dtOut.Columns.Add("重定向");
            }
            else
                dtOut.Clear();

            if (dtTotal == null)
            {
                dtTotal = new DataTable();

                dtTotal.Columns.Add("参数名").ReadOnly = true;
                dtTotal.Columns.Add("值");
                dtTotal.Columns.Add("备注");
            }
            else
                dtTotal.Clear();
            return true;
        }
        #endregion
        public bool OpenFile(string strFileFullPath)
        {
            dtCard.Clear();
            dtIn.Clear();
            dtOut.Clear();
            dtTotal.Clear();

            //获取主卡及扩展卡配置文件路径
            string sRead = ParaFileINI.ReadINI("CARD", "CardTotal", strFileFullPath);
            short CARD_TOTAL = Convert.ToInt16(sRead);
            DataRow row = dtTotal.NewRow();
            row["参数名"] = "CardTotal";
            row["值"] = sRead;
            row["备注"] = "卡的数量";
            dtTotal.Rows.Add(row);
            for (int i = 0; i < CARD_TOTAL; i++)
            {
                sRead = ParaFileINI.ReadINI("CARD", "Card" + i.ToString(), strFileFullPath);
                row = dtTotal.NewRow();
                row["参数名"] = "Card" + i.ToString();
                row["值"] = sRead;
                row["备注"] = "卡的名称";
                dtTotal.Rows.Add(row);
            }
            sRead = ParaFileINI.ReadINI("CARD", "ExtCardTotal", strFileFullPath);
            int EXT_CARD_TOTAL = Convert.ToInt32(sRead);
            row = dtTotal.NewRow();
            row["参数名"] = "ExtCardTotal";
            row["值"] = sRead;
            row["备注"] = "扩展卡的数量";
            dtTotal.Rows.Add(row);

            sRead = ParaFileINI.ReadINI("CARD", "ExtCard", strFileFullPath);
            row = dtTotal.NewRow();
            row["参数名"] = "ExtCard";
            row["值"] = sRead;
            row["备注"] = "扩展卡的文件名称";
            dtTotal.Rows.Add(row);

            sRead = ParaFileINI.ReadINI("AXIS", "AxisTotal", strFileFullPath, "8");
            row = dtTotal.NewRow();
            row["参数名"] = "AxisTotal";
            row["值"] = sRead;
            row["备注"] = "轴的数量";
            dtTotal.Rows.Add(row);
            int Max_Axis = Convert.ToInt32(sRead);

            sRead = ParaFileINI.ReadINI("CARD", "AxisInFirstCard ", strFileFullPath, "8");
            row = dtTotal.NewRow();
            row["参数名"] = "AxisInFirstCard";
            row["值"] = sRead;
            row["备注"] = "首卡轴的数量";
            dtTotal.Rows.Add(row);
            int AXIS_IN_CARD = Convert.ToInt32(sRead);

            sRead = ParaFileINI.ReadINI("CARD", "IoInFirstCard ", strFileFullPath, "16");
            row = dtTotal.NewRow();
            row["参数名"] = "IoInFirstCard";
            row["值"] = sRead;
            row["备注"] = "IO的数量";
            dtTotal.Rows.Add(row);
            int IO_IN_CARD = Convert.ToInt32(sRead);

            sRead = ParaFileINI.ReadINI("CARD", "IoInExtCard ", strFileFullPath, "16");
            row = dtTotal.NewRow();
            row["参数名"] = "IoInExtCard";
            row["值"] = sRead;
            row["备注"] = "扩展卡IO的数量";
            dtTotal.Rows.Add(row);
            int IO_IN_CARD_EXT = Convert.ToInt32(sRead);

            sRead = ParaFileINI.ReadINI("AXIS", "AutoPer", strFileFullPath);
            row = dtTotal.NewRow();
            row["参数名"] = "AutoPer";
            row["值"] = sRead;
            row["备注"] = "自动运行速度";
            dtTotal.Rows.Add(row);

            sRead = ParaFileINI.ReadINI("AXIS", "DebugPer", strFileFullPath);
            row = dtTotal.NewRow();
            row["参数名"] = "DebugPer";
            row["值"] = sRead;
            row["备注"] = "手动运行速度";
            dtTotal.Rows.Add(row);

            string redirect = "";
            //int AXIS_IN_CARD = Convert.ToInt32(ParaFileINI.ReadINI("CARD", "AxisInFirstCard", strFileFullPath, "8"));
            //int IO_IN_CARD = Convert.ToInt32(ParaFileINI.ReadINI("CARD", "IoInFirstCard", strFileFullPath, "16"));
            //生成普通IO及HOME和LIMIT
            for (int iCard = 0; iCard < (Max_Axis - 1) / AXIS_IN_CARD + 1; iCard++)
            {
                for (int iPort = 0; iPort < IO_IN_CARD; iPort++)
                {
                    string str;
                    str = "EXI";
                    str += iPort.ToString();
                    str += "_";
                    str += iCard.ToString();
                    string inName = ParaFileINI.ReadINI("IO", str, strFileFullPath);
                    redirect = ParaFileINI.ReadINI("IO", str + "New", strFileFullPath);
                    row = dtIn.NewRow();
                    row["卡号"] = iCard.ToString();
                    row["端口号"] = iPort.ToString();
                    row["名称"] = inName;
                    row["类型"] = "通用";
                    if (redirect != "0")
                        row["重定向"] = redirect;
                    dtIn.Rows.Add(row);

                    str = "EXO";
                    str += iPort.ToString();
                    str += "_";
                    str += iCard.ToString();
                    string outName = ParaFileINI.ReadINI("IO", str, strFileFullPath);
                    redirect = ParaFileINI.ReadINI("IO", str + "New", strFileFullPath);
                    row = dtOut.NewRow();
                    row["卡号"] = iCard.ToString();
                    row["端口号"] = iPort.ToString();
                    row["名称"] = outName;
                    row["类型"] = "通用";
                    if (redirect != "0")
                        row["重定向"] = redirect;
                    dtOut.Rows.Add(row);
                }
                for (int iPort = 0; iPort < AXIS_IN_CARD; iPort++)
                {
                    string str = "HOME";
                    str += iPort.ToString();
                    str += "_";
                    str += iCard.ToString();
                    string homeName = ParaFileINI.ReadINI("IO", str, strFileFullPath);
                    redirect = ParaFileINI.ReadINI("IO", str + "New", strFileFullPath);
                    row = dtIn.NewRow();
                    row["卡号"] = iCard.ToString();
                    row["端口号"] = iPort.ToString();
                    row["名称"] = homeName;
                    row["类型"] = "原点";
                    if (redirect != "0")
                        row["重定向"] = redirect;
                    dtIn.Rows.Add(row);

                    str = "LIMIT";
                    str += iPort.ToString();
                    str += "+_";//P
                    str += iCard.ToString();
                    string limitNameP = ParaFileINI.ReadINI("IO", str, strFileFullPath);
                    redirect = ParaFileINI.ReadINI("IO", str + "New", strFileFullPath);
                    row = dtIn.NewRow();
                    row["卡号"] = iCard.ToString();
                    row["端口号"] = iPort.ToString();
                    row["名称"] = limitNameP;
                    row["类型"] = "正限位";
                    if (redirect != "0")
                        row["重定向"] = redirect;
                    dtIn.Rows.Add(row);

                    str = "LIMIT";
                    str += iPort.ToString();
                    str += "-_";//N
                    str += iCard.ToString();
                    string linitNameN = ParaFileINI.ReadINI("IO", str, strFileFullPath);
                    redirect = ParaFileINI.ReadINI("IO", str + "New", strFileFullPath);
                    row = dtIn.NewRow();
                    row["卡号"] = iCard.ToString();
                    row["端口号"] = iPort.ToString();
                    row["名称"] = linitNameN;
                    row["类型"] = "负限位";
                    if (redirect != "0")
                        row["重定向"] = redirect;
                    dtIn.Rows.Add(row);
                }
            }
            //int IO_IN_CARD_EXT = Convert.ToInt32(ParaFileINI.ReadINI("AXIS", "IoInExtCard", strFileFullPath, "16"));
            //生成扩展卡IO
            for (int iCard = 0; iCard < EXT_CARD_TOTAL; iCard++)
            {
                for (int iPort = 0; iPort < IO_IN_CARD_EXT; iPort++)
                {
                    string str = "DXI";
                    str += iPort.ToString();
                    str += "_";
                    str += iCard.ToString();
                    string inName = ParaFileINI.ReadINI("IO", str, strFileFullPath);
                    redirect = ParaFileINI.ReadINI("IO", str + "New", strFileFullPath);
                    row = dtIn.NewRow();
                    row["卡号"] = iCard.ToString();
                    row["端口号"] = iPort.ToString();
                    row["名称"] = inName;
                    row["类型"] = "扩展卡";
                    if (redirect != "0")
                        row["重定向"] = redirect;
                    dtIn.Rows.Add(row);

                    str = "DXO";
                    str += iPort.ToString();
                    str += "_";
                    str += iCard.ToString();
                    string outName = ParaFileINI.ReadINI("IO", str, strFileFullPath);
                    redirect = ParaFileINI.ReadINI("IO", str + "New", strFileFullPath);
                    row = dtOut.NewRow();
                    row["卡号"] = iCard.ToString();
                    row["端口号"] = iPort.ToString();
                    row["名称"] = outName;
                    row["类型"] = "扩展卡";
                    if (redirect != "0")
                        row["重定向"] = redirect;
                    dtOut.Rows.Add(row);
                }
            }
            #endregion
            //生成轴并配置轴号及轴名称，导程，细分
            for (int i = 1; i <= Max_Axis; i++)
            {
                row = dtCard.NewRow();
                string sValue = "Axis" + i;
                string str = ParaFileINI.ReadINI("AXIS", sValue, strFileFullPath);
                string[] sst = str.Trim().Split('_');
                row["轴号"] = sst[0];
                row["英文轴名"] = sst[1];
                row["中文轴名"] = sst[2];
                if (sst.Count() > 1)
                {
                    Int16 iAxisNum = Convert.ToInt16(sst[0]);
                    string sName = sst[sst.Count() - 1];
                    sValue = "Pitch" + i;
                    str = ParaFileINI.ReadINI("AXIS", sValue, strFileFullPath);
                    row["导程"] = str;
                    sValue = "Divide" + i;
                    str = ParaFileINI.ReadINI("AXIS", sValue, strFileFullPath);
                    row["细分"] = str;

                    str = ParaFileINI.ReadINI("AXIS", "Vel" + i, strFileFullPath);
                    row["速度"] = str;
                    str = ParaFileINI.ReadINI("AXIS", "Acc" + i, strFileFullPath);
                    row["加速度"] = str;

                    ParaFileINI paraFile = new ParaFileINI(strFileFullPath, "RESET" + i);
                    if (paraFile.m_bEx)
                    {
                        paraFile.ReadINI("IsNegateResetDirection", ref IsNegateResetDirection);
                        row["复位方向"] = IsNegateResetDirection;
                        paraFile.ReadINI("ResetType", ref ResetType);
                        row["复位类型"] = ResetType;
                        paraFile.ReadINI("FastHomeSpeed", ref FastHomeSpeed);
                        row["快-速度"] = FastHomeSpeed;
                        paraFile.ReadINI("FastHomeAcc", ref FastHomeAcc);
                        row["快-加速度"] = FastHomeAcc;
                        paraFile.ReadINI("SlowHomeSpeed", ref SlowHomeSpeed);
                        row["慢-速度"] = SlowHomeSpeed;
                        paraFile.ReadINI("SlowHomeAcc", ref SlowHomeAcc);
                        row["慢-加速度"] = SlowHomeAcc;
                        paraFile.ReadINI("RollBackDistance_1", ref RollBackDistance_1);
                        row["一次回距离"] = RollBackDistance_1;
                        paraFile.ReadINI("RollBackDistance_2", ref RollBackDistance_2);
                        row["二次回距离"] = RollBackDistance_2;
                        paraFile.ReadINI("TimeOut", ref TimeOut);
                        row["超时时间"] = TimeOut;
                        paraFile.ReadINI("IsServo", ref IsServo);
                        IsServo = IsServo == "0" ? "是" : "否";
                        row["是否是伺服"] = IsServo;
                        paraFile.ReadINI("IsInversion", ref IsInversion);
                        IsInversion = IsInversion == "0" ? "是" : "否";
                        row["是否反向"] = IsInversion;
                        paraFile.ReadINI("IsReset", ref IsReset);
                        IsReset = IsReset == "0" ? "是" : "否";
                        row["是否复位"] = IsReset;
                        paraFile.ReadINI("LeftButtonName", ref LeftButtonName);
                        row["左按钮名称"] = LeftButtonName;
                        paraFile.ReadINI("RightButtonName", ref RightButtonName);
                        row["右按钮名称"] = RightButtonName;
                        paraFile.ReadINI("IsRemovable", ref IsRemovable);
                        IsRemovable = IsRemovable == "0" ? "是" : "否";
                        row["是否可移动"] = IsRemovable;
                        paraFile.ReadINI("Sort", ref Sort);
                        row["排序"] = Sort;
                    }
                    dtCard.Rows.Add(row);
                }
            }
            _dgvMotionCard.DataSource = dtCard;
            _dgvMotionIn.DataSource = dtIn;
            _dgvMotionOut.DataSource = dtOut;
            _dgvMotionTotal.DataSource = dtTotal;

            DgvNotSortable();
            return true;
        }
        /// <summary>
        /// 禁止列表排序
        /// </summary>
        private void DgvNotSortable()
        {
            for (int i = 0; i < _dgvMotionCard.Columns.Count; i++)
            {
                _dgvMotionCard.Columns[i].SortMode = DataGridViewColumnSortMode.Programmatic;
            }
            for (int i = 0; i < _dgvMotionIn.Columns.Count; i++)
            {
                _dgvMotionIn.Columns[i].SortMode = DataGridViewColumnSortMode.Programmatic;
            }
            for (int i = 0; i < _dgvMotionOut.Columns.Count; i++)
            {
                _dgvMotionOut.Columns[i].SortMode = DataGridViewColumnSortMode.Programmatic;
            }
            for (int i = 0; i < _dgvMotionTotal.Columns.Count; i++)
            {
                _dgvMotionTotal.Columns[i].SortMode = DataGridViewColumnSortMode.Programmatic;
            }
        }
        public bool WriteFile(string strFileFullPath)
        {
            string value;
            int temp1 = 0, temp2 = 0, temp3 = 0, temp4 = 0, CardNum = 0, ExtCardNum = 0;
            System.Collections.ArrayList ArrayList_Names = new System.Collections.ArrayList(100);
            
            for (int i = 0; i < _dgvMotionCard.Rows.Count; i++)
            {
                for (int j = i + 1; j < _dgvMotionCard.Rows.Count; j++)
                {
                    if (Convert.ToString(_dgvMotionCard.Rows[i].Cells[1].Value) == Convert.ToString(_dgvMotionCard.Rows[j].Cells[1].Value))
                    {
                        MessageBoxEx.Show("英文轴名称“" + _dgvMotionCard.Rows[i].Cells[1].Value.ToString() + "”存在一致，请检查");
                        return false;
                    }
                }
            }

            for (int i = 0; i < _dgvMotionCard.Rows.Count; i++)
            {
                for (int j = i + 1; j < _dgvMotionCard.Rows.Count; j++)
                {
                    if (Convert.ToString(_dgvMotionCard.Rows[i].Cells[2].Value) == Convert.ToString(_dgvMotionCard.Rows[j].Cells[2].Value))
                    {
                        MessageBoxEx.Show("中文轴名称“" + _dgvMotionCard.Rows[i].Cells[2].Value.ToString() + "”存在一致，请检查");
                        return false;
                    }
                }
            }

            for (int i = 0; i < _dgvMotionIn.Rows.Count; i++)
            {
                if (Convert.ToString(_dgvMotionIn.Rows[i].Cells[2].Value) == "备用" || Convert.ToString(_dgvMotionIn.Rows[i].Cells[2].Value) == "0" ||
                    Convert.ToString(_dgvMotionIn.Rows[i].Cells[2].Value) == "")
                {
                    continue;
                }
                for (int j = i + 1; j < _dgvMotionIn.Rows.Count; j++)
                {
                    if (Convert.ToString(_dgvMotionIn.Rows[i].Cells[2].Value) == Convert.ToString(_dgvMotionIn.Rows[j].Cells[2].Value))
                    {
                        MessageBoxEx.Show("输入端名称“" + _dgvMotionIn.Rows[i].Cells[2].Value.ToString() + "”存在一致，请检查");
                        return false;
                    }
                }
            }

            for (int i = 0; i < _dgvMotionOut.Rows.Count; i++)
            {
                if (Convert.ToString(_dgvMotionOut.Rows[i].Cells[2].Value) == "备用" || Convert.ToString(_dgvMotionOut.Rows[i].Cells[2].Value) == "0" ||
                     Convert.ToString(_dgvMotionOut.Rows[i].Cells[2].Value) == "")
                {
                    continue;
                }
                for (int j = i + 1; j < _dgvMotionOut.Rows.Count; j++)
                {
                    if (Convert.ToString(_dgvMotionOut.Rows[i].Cells[2].Value) == Convert.ToString(_dgvMotionOut.Rows[j].Cells[2].Value))
                    {
                        MessageBoxEx.Show("输出端名称“" + _dgvMotionOut.Rows[i].Cells[2].Value.ToString() + "”存在一致，请检查");
                        return false;
                    }
                }
            }

            for (int i = 0; i < _dgvMotionIn.Rows.Count; i++)
            {		 
                if (Convert.ToString(_dgvMotionIn.Rows[i].Cells[4].Value) != "")
                {
                    switch(Convert.ToString(_dgvMotionIn.Rows[i].Cells[3].Value))
                    {
                        case "原点":
                            if (!Convert.ToString(_dgvMotionIn.Rows[i].Cells[4].Value).Contains("HOME"))
                            {
                                MessageBoxEx.Show("重定向"+ Convert.ToString(_dgvMotionIn.Rows[i].Cells[4].Value)+"类型错误，请检查");
                                return false;
                            }
                                break;

                        case "正限位":
                            if (!(Convert.ToString(_dgvMotionIn.Rows[i].Cells[4].Value).Contains("LIMIT")&& Convert.ToString(_dgvMotionIn.Rows[i].Cells[4].Value).Contains("+")))
                            {
                                MessageBoxEx.Show("重定向" + Convert.ToString(_dgvMotionIn.Rows[i].Cells[4].Value) + "类型错误，请检查");
                                return false;
                            }
                            break;

                        case "负限位":
                            if (!(Convert.ToString(_dgvMotionIn.Rows[i].Cells[4].Value).Contains("LIMIT")&& Convert.ToString(_dgvMotionIn.Rows[i].Cells[4].Value).Contains("-")))
                            {
                                MessageBoxEx.Show("重定向" + Convert.ToString(_dgvMotionIn.Rows[i].Cells[4].Value) + "类型错误，请检查");
                                return false;
                            }
                            break;

                        case "通用":
                            if (!Convert.ToString(_dgvMotionIn.Rows[i].Cells[4].Value).Contains("EXI"))
                            {
                                MessageBoxEx.Show("重定向" + Convert.ToString(_dgvMotionIn.Rows[i].Cells[4].Value) + "类型错误，请检查");
                                return false;
                            }
                            break;

                        case "扩展卡":
                            if (!Convert.ToString(_dgvMotionIn.Rows[i].Cells[4].Value).Contains("DXI"))
                            {
                                MessageBoxEx.Show("重定向" + Convert.ToString(_dgvMotionIn.Rows[i].Cells[4].Value) + "类型错误，请检查");
                                return false;
                            }
                            break;
                    }
                }
            }

            for (int i = 0; i < _dgvMotionOut.Rows.Count; i++)
            {
               
                if (Convert.ToString(_dgvMotionOut.Rows[i].Cells[4].Value) != "")
                {
                    switch (Convert.ToString(_dgvMotionOut.Rows[i].Cells[3].Value))
                    {
                        case "通用":
                            if (!Convert.ToString(_dgvMotionOut.Rows[i].Cells[4].Value).Contains("EXO"))
                            {
                                MessageBoxEx.Show("重定向" + Convert.ToString(_dgvMotionOut.Rows[i].Cells[4].Value) + "类型错误，请检查");
                                return false;
                            }
                            break;

                        case "扩展卡":
                            if (!Convert.ToString(_dgvMotionOut.Rows[i].Cells[4].Value).Contains("DXO"))
                            {
                                MessageBoxEx.Show("重定向" + Convert.ToString(_dgvMotionOut.Rows[i].Cells[4].Value) + "类型错误，请检查");
                                return false;
                            }
                            break;
                    }
                }
            }

            //校验后再删除文件
           
            FileInfo file = new FileInfo(strFileFullPath);
            FileInfo Changedfile = new FileInfo(ChangedIOFile);
            file.Delete();
            Changedfile.Delete();



            try
            {
                for (int i = 0; i < _dgvMotionTotal.Rows.Count; i++)
                {
                    ParaFileINI.WriteINI("CARD", "运动卡备注", "主卡及扩展卡配置文件路径", strFileFullPath);
                    value = Convert.ToString(_dgvMotionTotal.Rows[i].Cells[1].Value);
                    if (Convert.ToString(_dgvMotionTotal.Rows[i].Cells[0].Value) == "CardTotal")
                    {
                        if (Convert.ToInt32(value) <= 0 || Convert.ToInt32(value) > 10)
                        {
                            MessageBoxEx.Show("输入运动卡数量有误");
                            return false;
                        }
                        else
                        {

                            ParaFileINI.WriteINI("CARD", "CardTotal", value, strFileFullPath);
                        }
                        CardNum = Convert.ToInt32(value);
                    }


                    if (Convert.ToString(_dgvMotionTotal.Rows[i].Cells[0].Value) == "Card" + temp1)
                    {
                        ParaFileINI.WriteINI("CARD", "Card" + temp1, value, strFileFullPath);
                        temp1++;
                    }
                    if (Convert.ToString(_dgvMotionTotal.Rows[i].Cells[0].Value) == "CardIONum_" + temp2)
                    {
                        ParaFileINI.WriteINI("CARD", "CardIONum_" + temp1, value, strFileFullPath);
                        temp2++;
                    }
                    if (Convert.ToString(_dgvMotionTotal.Rows[i].Cells[0].Value) == "ExtCardIONum_" + temp3)
                    {
                        ParaFileINI.WriteINI("CARD", "ExtCardIONum_" + temp3, value, strFileFullPath);
                        temp3++;
                    }
                    if (Convert.ToString(_dgvMotionTotal.Rows[i].Cells[0].Value) == "ExtCardTotal")
                    {
                        ExtCardNum = Convert.ToInt32(value);
                        ParaFileINI.WriteINI("CARD", "ExtCardTotal", value, strFileFullPath);
                    }

                    if (Convert.ToString(_dgvMotionTotal.Rows[i].Cells[0].Value) == "ExtCard")
                    {
                        ParaFileINI.WriteINI("CARD", "ExtCard", value + "\r\n", strFileFullPath);
                    }

                    if (_dgvMotionTotal.Rows[i].Cells[0].Value.Equals("AxisInFirstCard"))
                    {
                        ParaFileINI.WriteINI("CARD", "AxisInFirstCard", value + "\r\n", strFileFullPath);
                    }

                    if (Convert.ToString(_dgvMotionTotal.Rows[i].Cells[0].Value) == "IoInFirstCard")
                    {
                        ParaFileINI.WriteINI("CARD", "IoInFirstCard", value + "\r\n", strFileFullPath);
                    }

                    if (Convert.ToString(_dgvMotionTotal.Rows[i].Cells[0].Value) == "IoInExtCard")
                    {
                        ParaFileINI.WriteINI("CARD", "IoInExtCard", value + "\r\n", strFileFullPath);
                    }

                    if (Convert.ToString(_dgvMotionTotal.Rows[i].Cells[0].Value) == "AxisTotal")
                    {
                        ParaFileINI.WriteINI("AXIS", "轴备注", "配置轴号，英文轴名，中文轴名", strFileFullPath);
                        if (CardNum * 8 < Convert.ToInt32(value))
                        {
                            MessageBoxEx.Show("轴数量超限");
                            return false;
                        }
                        else
                        {
                            ParaFileINI.WriteINI("AXIS", "AxisTotal", value, strFileFullPath);
                        }
                    }
                }
                temp1 = temp2 = temp3 = 0;

                for (int i = 0; i < _dgvMotionCard.Rows.Count; i++)
                {
                    if (Convert.ToInt32(_dgvMotionCard.Rows[i].Cells[0].Value) >= 0)
                    {
                        String value1 = Convert.ToString(_dgvMotionCard.Rows[i].Cells[0].Value);
                        String value2 = Convert.ToString(_dgvMotionCard.Rows[i].Cells[1].Value);
                        String value3 = Convert.ToString(_dgvMotionCard.Rows[i].Cells[2].Value);
                        value = value1 + "_" + value2 + "_" + value3;
                        ParaFileINI.WriteINI("AXIS", "Axis" + (i + 1), value + "\r\n", strFileFullPath);
                    }
                }

                for (int i = 0; i < _dgvMotionCard.Rows.Count; i++)
                {
                    if (Convert.ToInt32(_dgvMotionCard.Rows[i].Cells[0].Value) >= 0)
                    {
                        value = Convert.ToString(_dgvMotionCard.Rows[i].Cells[3].Value);
                        if (_dgvMotionCard.Columns[3].Name.ToString() == "导程")
                        {
                            ParaFileINI.WriteINI("AXIS", "导程备注", "导程", strFileFullPath);
                            ParaFileINI.WriteINI("AXIS", "Pitch" + (i + 1), value + "\r\n", strFileFullPath);
                        }
                    }
                }

                for (int i = 0; i < _dgvMotionCard.Rows.Count; i++)
                {
                    if (Convert.ToInt32(_dgvMotionCard.Rows[i].Cells[0].Value) >= 0)
                    {
                        value = Convert.ToString(_dgvMotionCard.Rows[i].Cells[4].Value);
                        if (_dgvMotionCard.Columns[4].Name.ToString() == "细分")
                        {
                            ParaFileINI.WriteINI("AXIS", "细分备注", "电子齿轮比/细分", strFileFullPath);
                            ParaFileINI.WriteINI("AXIS", "Divide" + (i + 1), value + "\r\n", strFileFullPath);
                        }
                    }
                }

                for (int i = 0; i < _dgvMotionTotal.Rows.Count; i++)
                {
                    value = Convert.ToString(_dgvMotionTotal.Rows[i].Cells[1].Value);
                    if (Convert.ToString(_dgvMotionTotal.Rows[i].Cells[0].Value) == "AutoPer")
                    {
                        if (Convert.ToDouble(value) < 0 || Convert.ToDouble(value) > 1)
                        {
                            MessageBoxEx.Show("自动运行速度超限");
                            return false;
                        }
                        else
                        {
                            ParaFileINI.WriteINI("AXIS", "速度及百分比备注", "运行速度与加速度百分比", strFileFullPath);
                            ParaFileINI.WriteINI("AXIS", "AutoPer", value, strFileFullPath);
                        }
                    }

                    if (Convert.ToString(_dgvMotionTotal.Rows[i].Cells[0].Value) == "DebugPer")
                    {
                        if (Convert.ToDouble(value) < 0 || Convert.ToDouble(value) > 1)
                        {
                            MessageBoxEx.Show("手动运行速度超限");
                            return false;
                        }
                        else
                            ParaFileINI.WriteINI("AXIS", "DebugPer", value + "\r\n", strFileFullPath);
                    }
                }

                for (int i = 0; i < _dgvMotionCard.Rows.Count; i++)
                {
                    if (Convert.ToInt32(_dgvMotionCard.Rows[i].Cells[0].Value) >= 0)
                    {
                        value = Convert.ToString(_dgvMotionCard.Rows[i].Cells[5].Value);
                        if (_dgvMotionCard.Columns[5].Name.ToString() == "速度")
                        {
                            if (Convert.ToInt32(value) < 0 || Convert.ToInt32(value) > 4000)
                            {
                                MessageBoxEx.Show("速度超限");
                                return false;
                            }
                            else
                            {
                                ParaFileINI.WriteINI("AXIS", "速度备注", "速度", strFileFullPath);
                                ParaFileINI.WriteINI("AXIS", "Vel" + (i + 1), value + "\r\n", strFileFullPath);
                            }
                        }
                    }
                }

                for (int i = 0; i < _dgvMotionCard.Rows.Count; i++)
                {
                    if (Convert.ToInt32(_dgvMotionCard.Rows[i].Cells[0].Value) >= 0)
                    {
                        value = Convert.ToString(_dgvMotionCard.Rows[i].Cells[6].Value);
                        if (_dgvMotionCard.Columns[6].Name.ToString() == "加速度")
                        {
                            if (Convert.ToInt32(value) < 0 || Convert.ToInt32(value) > 15000)
                            {
                                MessageBoxEx.Show("加速度超限");
                                return false;
                            }
                            else
                            {
                                ParaFileINI.WriteINI("AXIS", "加速度备注", "加速度", strFileFullPath);
                                ParaFileINI.WriteINI("AXIS", "Acc" + (i + 1), value + "\r\n", strFileFullPath);
                            }
                        }
                    }
                }

                for (int i = 0; i < _dgvMotionCard.Rows.Count; i++)
                {
                    if (Convert.ToInt32(_dgvMotionCard.Rows[i].Cells[0].Value) >= 0)
                    {
                        for (int j = 7; j < _dgvMotionCard.Rows[i].Cells.Count; j++)
                        {
                            value = Convert.ToString(_dgvMotionCard.Rows[i].Cells[j].Value);
                            if (_dgvMotionCard.Columns[j].Name.ToString() == "复位方向")
                            {
                                if (Convert.ToInt32(value) < 0 || Convert.ToInt32(value) > 1)
                                {
                                    MessageBoxEx.Show("复位方向有误，请输入0 or 1");
                                    return false;
                                }
                                else
                                {
                                    ParaFileINI.WriteINI("RESET" + (i + 1), "复位方向备注", "是否取反复位方向 默认：负向复位 取反：正向复位", strFileFullPath);
                                    ParaFileINI.WriteINI("RESET" + (i + 1), "IsNegateResetDirection", value + "\r\n", strFileFullPath);
                                }
                            }

                            if (_dgvMotionCard.Columns[j].Name.ToString() == "复位类型")
                            {
                                if (Convert.ToInt32(value) < 0 || Convert.ToInt32(value) > 4)
                                {
                                    MessageBoxEx.Show("复位类型有误，请输入：0限位，1旋转HOME，2双限位Z，3HOME捕获，4INDEX捕获");
                                    return false;
                                }
                                else
                                {
                                    ParaFileINI.WriteINI("RESET" + (i + 1), "复位类型备注", "复位类型0限位，1旋转HOME，2双限位Z，3HOME捕获，4INDEX捕获", strFileFullPath);
                                    ParaFileINI.WriteINI("RESET" + (i + 1), "ResetType", value + "\r\n", strFileFullPath);
                                }
                            }

                            if (_dgvMotionCard.Columns[j].Name.ToString() == "快-速度")
                            {
                                ParaFileINI.WriteINI("RESET" + (i + 1), "快速速度备注", "快速到限位的速度", strFileFullPath);
                                ParaFileINI.WriteINI("RESET" + (i + 1), "FastHomeSpeed", value + "\r\n", strFileFullPath);
                            }

                            if (_dgvMotionCard.Columns[j].Name.ToString() == "快-加速度")
                            {
                                ParaFileINI.WriteINI("RESET" + (i + 1), "快速加速度备注", "快速到限位的加速度", strFileFullPath);
                                ParaFileINI.WriteINI("RESET" + (i + 1), "FastHomeAcc", value + "\r\n", strFileFullPath);
                            }

                            if (_dgvMotionCard.Columns[j].Name.ToString() == "慢-速度")
                            {
                                ParaFileINI.WriteINI("RESET" + (i + 1), "慢速速度备注", "慢速到限位的速度", strFileFullPath);
                                ParaFileINI.WriteINI("RESET" + (i + 1), "SlowHomeSpeed", value + "\r\n", strFileFullPath);
                            }

                            if (_dgvMotionCard.Columns[j].Name.ToString() == "慢-加速度")
                            {
                                ParaFileINI.WriteINI("RESET" + (i + 1), "慢速加速度备注", "慢速到限位的加速度", strFileFullPath);
                                ParaFileINI.WriteINI("RESET" + (i + 1), "SlowHomeAcc", value + "\r\n", strFileFullPath);
                            }

                            if (_dgvMotionCard.Columns[j].Name.ToString() == "一次回距离")
                            {
                                ParaFileINI.WriteINI("RESET" + (i + 1), "一次回距离备注", "第一次往回走的距离", strFileFullPath);
                                ParaFileINI.WriteINI("RESET" + (i + 1), "RollBackDistance_1", value + "\r\n", strFileFullPath);
                            }

                            if (_dgvMotionCard.Columns[j].Name.ToString() == "二次回距离")
                            {
                                ParaFileINI.WriteINI("RESET" + (i + 1), "二次回距离备注", "第二次往回走的距离", strFileFullPath);
                                ParaFileINI.WriteINI("RESET" + (i + 1), "RollBackDistance_2", value + "\r\n", strFileFullPath);
                            }

                            if (_dgvMotionCard.Columns[j].Name.ToString() == "超时时间")
                            {
                                ParaFileINI.WriteINI("RESET" + (i + 1), "超时备注", "超时时间", strFileFullPath);
                                ParaFileINI.WriteINI("RESET" + (i + 1), "TimeOut", value + "\r\n", strFileFullPath);
                            }

                            if (_dgvMotionCard.Columns[j].Name.ToString() == "是否是伺服")
                            {
                                if (value == "是" || value == "否")
                                {
                                    int num = (value == "是") ? 0 : 1;
                                    ParaFileINI.WriteINI("RESET" + (i + 1), "伺服备注", "是否是伺服", strFileFullPath);
                                    ParaFileINI.WriteINI("RESET" + (i + 1), "IsServo", num.ToString() + "\r\n", strFileFullPath);
                                }
                                else
                                {
                                    MessageBoxEx.Show("输入不正确，请输入是或否");
                                    return false;
                                }
                            }

                            if (_dgvMotionCard.Columns[j].Name.ToString() == "是否反向")
                            {
                                if (value == "是" || value == "否")
                                {
                                    int num = (value == "是") ? 0 : 1;
                                    ParaFileINI.WriteINI("RESET" + (i + 1), "反向备注", "是否反向", strFileFullPath);
                                    ParaFileINI.WriteINI("RESET" + (i + 1), "IsInversion", num.ToString() + "\r\n", strFileFullPath);
                                }
                                else
                                {
                                    MessageBoxEx.Show("输入不正确，请输入是或否");
                                    return false;
                                }
                            }

                            if (_dgvMotionCard.Columns[j].Name.ToString() == "是否复位")
                            {
                                if (value == "是" || value == "否")
                                {
                                    int num = (value == "是") ? 0 : 1;
                                    ParaFileINI.WriteINI("RESET" + (i + 1), "复位备注", "是否复位", strFileFullPath);
                                    ParaFileINI.WriteINI("RESET" + (i + 1), "IsReset", num.ToString() + "\r\n", strFileFullPath);
                                }
                                else
                                {
                                    MessageBoxEx.Show("输入不正确，请输入是或否");
                                    return false;
                                }
                            }

                            if (_dgvMotionCard.Columns[j].Name.ToString() == "左按钮名称")
                            {
                                ParaFileINI.WriteINI("RESET" + (i + 1), "左按钮备注", "左按钮名称", strFileFullPath);
                                ParaFileINI.WriteINI("RESET" + (i + 1), "LeftButtonName", value + "\r\n", strFileFullPath);
                            }

                            if (_dgvMotionCard.Columns[j].Name.ToString() == "右按钮名称")
                            {
                                ParaFileINI.WriteINI("RESET" + (i + 1), "右按钮备注", "右按钮名称", strFileFullPath);
                                ParaFileINI.WriteINI("RESET" + (i + 1), "RightButtonName", value + "\r\n", strFileFullPath);
                            }

                            if (_dgvMotionCard.Columns[j].Name.ToString() == "是否可移动")
                            {
                                if (value == "是" || value == "否")
                                {
                                    int num = (value == "是") ? 0 : 1;
                                    ParaFileINI.WriteINI("RESET" + (i + 1), "移动备注", "是否可移动", strFileFullPath);
                                    ParaFileINI.WriteINI("RESET" + (i + 1), "IsRemovable", num.ToString() + "\r\n", strFileFullPath);
                                }
                                else
                                {
                                    MessageBoxEx.Show("输入不正确，请输入是或否");
                                    return false;
                                }
                            }

                            if (_dgvMotionCard.Columns[j].Name.ToString() == "排序")
                            {
                                ParaFileINI.WriteINI("RESET" + (i + 1), "排序备注", "排序", strFileFullPath);
                                ParaFileINI.WriteINI("RESET" + (i + 1), "Sort", value + "\r\n", strFileFullPath);
                            }
                        }
                    }
                }

                for (int j = 0; j < CardNum; j++)
                {
                    ParaFileINI.WriteINI("IO", "IO主卡" + (j + 1), "主卡" + (j + 1), strFileFullPath);
                    for (int i = 0; i < _dgvMotionIn.Rows.Count; i++)
                    {
                        value = Convert.ToString(_dgvMotionIn.Rows[i].Cells[2].Value);
                        if (Convert.ToString(_dgvMotionIn.Rows[i].Cells[3].Value) == "原点" && Convert.ToInt32(_dgvMotionIn.Rows[i].Cells[0].Value) == j)
                        {
                            string sum = Convert.ToString(_dgvMotionIn.Rows[i].Cells[1].Value);
                            if (Convert.ToString(_dgvMotionIn.Rows[i].Cells[4].Value) == "")
                            {
                                ParaFileINI.WriteINI("IO", "HOME" + sum + "_" + j, value + "\r\n", strFileFullPath);
                            }
                            else
                            {
                                ParaFileINI.WriteINI("IO", "HOME" + sum + "_" + j, value + "\r\n", strFileFullPath);
                                ParaFileINI.WriteINI("IO", "HOME" + sum + "_" + j , Convert.ToString(_dgvMotionIn.Rows[i].Cells[4].Value) + "\r\n", ChangedIOFile);
                            }
                        }
                    }

                    for (int i = 0; i < _dgvMotionIn.Rows.Count; i++)
                    {
                        value = Convert.ToString(_dgvMotionIn.Rows[i].Cells[2].Value);
                        if (Convert.ToString(_dgvMotionIn.Rows[i].Cells[3].Value) == "正限位" && Convert.ToInt32(_dgvMotionIn.Rows[i].Cells[0].Value) == j)
                        {
                            string sum = Convert.ToString(_dgvMotionIn.Rows[i].Cells[1].Value);
                            if (Convert.ToString(_dgvMotionIn.Rows[i].Cells[4].Value) == "")
                            {
                                ParaFileINI.WriteINI("IO", "LIMIT" + sum + "+_" + j, value, strFileFullPath);
                            }
                            else
                            {
                                ParaFileINI.WriteINI("IO", "LIMIT" + sum + "+_" + j, value, strFileFullPath);
                                ParaFileINI.WriteINI("IO", "LIMIT" + sum + "+_" + j , Convert.ToString(_dgvMotionIn.Rows[i].Cells[4].Value) + "\r\n", ChangedIOFile);
                            }
                        }

                        if (Convert.ToString(_dgvMotionIn.Rows[i].Cells[3].Value) == "负限位" && Convert.ToInt32(_dgvMotionIn.Rows[i].Cells[0].Value) == j)
                        {
                            string sum = Convert.ToString(_dgvMotionIn.Rows[i].Cells[1].Value);
                            if (Convert.ToString(_dgvMotionIn.Rows[i].Cells[4].Value) == "")
                            {
                                ParaFileINI.WriteINI("IO", "LIMIT" + sum + "-_" + j, value + "\r\n", strFileFullPath);
                            }
                            else
                            {
                                ParaFileINI.WriteINI("IO", "LIMIT" + sum + "-_" + j, value + "\r\n", strFileFullPath);
                                ParaFileINI.WriteINI("IO", "LIMIT" + sum + "-_" + j , Convert.ToString(_dgvMotionIn.Rows[i].Cells[4].Value) + "\r\n", ChangedIOFile);
                            }
                        }
                    }

                    ParaFileINI.WriteINI("IO", "IO In", "输入", strFileFullPath);
                    for (int i = 0; i < _dgvMotionIn.Rows.Count; i++)
                    {
                        value = Convert.ToString(_dgvMotionIn.Rows[i].Cells[2].Value);
                        if (Convert.ToInt32(_dgvMotionIn.Rows[i].Cells[0].Value) == j && Convert.ToString(_dgvMotionIn.Rows[i].Cells[3].Value) == "通用")
                        {
                            if (Convert.ToString(_dgvMotionIn.Rows[i].Cells[4].Value) == "")
                            {
                                ParaFileINI.WriteINI("IO", "EXI" + temp1 + "_" + j, value + "\r\n", strFileFullPath);
                                temp1++;
                            }
                            else
                            {
                                ParaFileINI.WriteINI("IO", "EXI" + temp1 + "_" + j, value + "\r\n", strFileFullPath);
                                ParaFileINI.WriteINI("IO", "EXI" + temp1 + "_" + j, Convert.ToString(_dgvMotionIn.Rows[i].Cells[4].Value) + "\r\n", ChangedIOFile);
                                temp1++;
                            }
                        }
                    }

                    ParaFileINI.WriteINI("IO", "IO Out", "输出", strFileFullPath);
                    for (int i = 0; i < _dgvMotionOut.Rows.Count; i++)
                    {
                        value = Convert.ToString(_dgvMotionOut.Rows[i].Cells[2].Value);
                        if (Convert.ToInt32(_dgvMotionOut.Rows[i].Cells[0].Value) == j && Convert.ToString(_dgvMotionOut.Rows[i].Cells[3].Value) == "通用")
                        {
                            if (Convert.ToString(_dgvMotionOut.Rows[i].Cells[4].Value) == "")
                            {
                                ParaFileINI.WriteINI("IO", "EXO" + temp2 + "_" + j, value + "\r\n", strFileFullPath);
                                temp2++;
                            }
                            else
                            {
                                ParaFileINI.WriteINI("IO", "EXO" + temp2 + "_" + j, value + "\r\n", strFileFullPath);
                                ParaFileINI.WriteINI("IO", "EXO" + temp2 + "_" + j, Convert.ToString(_dgvMotionOut.Rows[i].Cells[4].Value) + "\r\n", ChangedIOFile);
                                temp2++;
                            }
                        }
                    }
                    temp1 = temp2 = 0;
                }

                if (ExtCardNum > 0)
                {
                    WriteExtCard(ExtCardNum);//写入扩展卡cfg文件数据 SHM（20200515）
																							   
                    ParaFileINI.WriteINI("IO", "扩展卡序号1", "扩展卡1" + "\r\n", strFileFullPath);
                    ParaFileINI.WriteINI("IO", "扩展卡1 In", "输入", strFileFullPath);
                    for (int i = 0; i < _dgvMotionIn.Rows.Count; i++)
                    {
                        value = Convert.ToString(_dgvMotionIn.Rows[i].Cells[2].Value);
                        if (Convert.ToString(_dgvMotionIn.Rows[i].Cells[0].Value) == "0" && Convert.ToString(_dgvMotionIn.Rows[i].Cells[3].Value) == "扩展卡")
                        {
                            if (Convert.ToString(_dgvMotionIn.Rows[i].Cells[4].Value) == "")
                            {
                                ParaFileINI.WriteINI("IO", "DXI" + temp1 + "_0", value + "\r\n", strFileFullPath);
                                temp1++;
                            }
                            else
                            {
                                ParaFileINI.WriteINI("IO", "DXI" + temp1 + "_0", value + "\r\n", strFileFullPath);
                                ParaFileINI.WriteINI("IO", "DXI" + temp1 + "_0" , Convert.ToString(_dgvMotionIn.Rows[i].Cells[4].Value) + "\r\n", ChangedIOFile);
                                temp1++;
                            }
                        }
                    }

                    ParaFileINI.WriteINI("IO", "扩展卡1 Out", "输出", strFileFullPath);
                    for (int i = 0; i < _dgvMotionOut.Rows.Count; i++)
                    {
                        value = Convert.ToString(_dgvMotionOut.Rows[i].Cells[2].Value);
                        if (Convert.ToString(_dgvMotionOut.Rows[i].Cells[0].Value) == "0" && Convert.ToString(_dgvMotionOut.Rows[i].Cells[3].Value) == "扩展卡")

                        {
                            if (Convert.ToString(_dgvMotionOut.Rows[i].Cells[4].Value) == "")
                            {
                                ParaFileINI.WriteINI("IO", "DXO" + temp2 + "_0", value + "\r\n", strFileFullPath);
                                temp2++;
                            }
                            else
                            {
                                ParaFileINI.WriteINI("IO", "DXO" + temp2 + "_0", value + "\r\n", strFileFullPath);
                                ParaFileINI.WriteINI("IO", "DXO" + temp2 + "_0" , Convert.ToString(_dgvMotionOut.Rows[i].Cells[4].Value) + "\r\n", ChangedIOFile);
                                temp2++;
                            }
                        }

                    }
                }

                if (ExtCardNum > 1)
                {
                    for (int j = 1; j < ExtCardNum; j++)
                    {
                        ParaFileINI.WriteINI("IO", "扩展卡序号" + (j + 1), "扩展卡" + (j + 1) + "\r\n", strFileFullPath);
                        ParaFileINI.WriteINI("IO", "扩展卡" + (j + 1) + "In", "输入", strFileFullPath);
                        for (int i = 0; i < _dgvMotionIn.Rows.Count; i++)
                        {
                            value = Convert.ToString(_dgvMotionIn.Rows[i].Cells[2].Value);
                            if (Convert.ToString(_dgvMotionIn.Rows[i].Cells[0].Value) == Convert.ToString(j) && Convert.ToString(_dgvMotionIn.Rows[i].Cells[3].Value) == "扩展卡")
                            {
                                if (Convert.ToString(_dgvMotionIn.Rows[i].Cells[4].Value) == "")
                                {
                                    ParaFileINI.WriteINI("IO", "DXI" + temp3 + "_" + j, value + "\r\n", strFileFullPath);
                                    temp3++;
                                }
                                else
                                {
                                    ParaFileINI.WriteINI("IO", "DXI" + temp3 + "_" + j, value + "\r\n", strFileFullPath);
                                    ParaFileINI.WriteINI("IO", "DXI" + temp3 + "_" + j , Convert.ToString(_dgvMotionIn.Rows[i].Cells[4].Value) + "\r\n", ChangedIOFile);
                                    temp3++;
                                }

                            }
                        }


                        ParaFileINI.WriteINI("IO", "扩展卡" + (j + 1) + "Out", "输出", strFileFullPath);
                        for (int i = 0; i < _dgvMotionOut.Rows.Count; i++)
                        {
                            value = Convert.ToString(_dgvMotionOut.Rows[i].Cells[2].Value);
                            if (Convert.ToString(_dgvMotionOut.Rows[i].Cells[0].Value) == Convert.ToString(j) && Convert.ToString(_dgvMotionOut.Rows[i].Cells[3].Value) == "扩展卡")
                            {
                                if (Convert.ToString(_dgvMotionOut.Rows[i].Cells[4].Value) == "")
                                {
                                    ParaFileINI.WriteINI("IO", "DXO" + temp4 + "_" + j, value + "\r\n", strFileFullPath);
                                    temp4++;
                                }
                                else
                                {
                                    ParaFileINI.WriteINI("IO", "DXO" + temp4 + "_" + j, value + "\r\n", strFileFullPath);
                                    ParaFileINI.WriteINI("IO", "DXO" + temp4 + "_" + j, Convert.ToString(_dgvMotionOut.Rows[i].Cells[4].Value) + "\r\n", ChangedIOFile);
                                    temp4++;
                                }

                            }
                        }
                        temp3 = temp4 = 0;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("输入参数格式不正确");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 新增一张卡的数据
        /// </summary>
        public int AddCardData(Dictionary<string, string> dataParameter)
        {
            string cardNum = "0";//卡的总数量
            string cardName = "";
            int cardAxisNum = 8;
            int cardAxisNumOld = 0;//未更新前的轴数量
            int cardIONum = 16;
            //cardNum = dataParameter["卡号"];
            cardName = dataParameter["卡名称"];
            cardAxisNum = int.Parse(dataParameter["轴数量"]);
            cardIONum = int.Parse(dataParameter["IO数量"]);
            if ("0".Equals(dataParameter["卡类型"]))//如果卡类型是运动控制卡
            {
                //获取卡号判断是否已经存在
                foreach (DataGridViewRow item in _dgvMotionTotal.Rows)
                {
                    if (cardName.Equals(item.Cells[1].Value.ToString())) return -2;//卡名称重复
                    //if (item.Cells[0].Equals("Card" + cardNum)) return -3;//卡号重复
                    if ("CardTotal".Equals(item.Cells[0].Value.ToString()))
                    {
                        cardNum = item.Cells[1].Value.ToString() ;
                    }
                    if ("AxisTotal".Equals(item.Cells[0].Value.ToString()))
                    {
                        cardAxisNumOld = int.Parse(item.Cells[1].Value.ToString());//轴数量
                    }
                }
                double dDecide = cardAxisNumOld / double.Parse(cardNum);
                if (dDecide < 8 && cardAxisNum == 8)return -3;//前一张运动控制卡为4轴卡，当前就不能新增8轴运动控制卡

                //在总表增加卡的名称数据
                DataRow row = dtTotal.NewRow();
                row["参数名"] = "Card" + cardNum;
                row["值"] = cardName;
                row["备注"] = "卡的名称";
                dtTotal.Rows.Add(row);
                //在总表增加卡的IO数量数据
                row = dtTotal.NewRow();
                row["参数名"] = "CardIONum_" + cardNum;
                row["值"] = cardIONum;
                row["备注"] = "卡的IO数量";
                dtTotal.Rows.Add(row);

                if (_dgvMotionTotal.Rows.Count == 0)
                {
                    row = dtTotal.NewRow();
                    row["参数名"] = "AutoPer";
                    row["值"] = "1";
                    row["备注"] = "自动运行速度";
                    dtTotal.Rows.Add(row);

                    row = dtTotal.NewRow();
                    row["参数名"] = "DebugPer";
                    row["值"] = "0.5";
                    row["备注"] = "手动运行速度";
                    dtTotal.Rows.Add(row);

                    row = dtTotal.NewRow();
                    row["参数名"] = "CardTotal";
                    row["值"] = "1";
                    row["备注"] = "卡的数量";
                    dtTotal.Rows.Add(row);

                    row = dtTotal.NewRow();
                    row["参数名"] = "AxisTotal";
                    row["值"] = cardAxisNum.ToString();
                    row["备注"] = "轴的数量";
                    dtTotal.Rows.Add(row);
                }
                else
                {
                    //更改卡数量和轴数量
                    foreach (DataGridViewRow item in _dgvMotionTotal.Rows)
                    {
                        if ("CardTotal".Equals(item.Cells[0].Value))//更改卡数量
                        {
                            item.Cells[1].Value = (int.Parse(item.Cells[1].Value.ToString()) + 1).ToString();
                        }
                        else if ("AxisTotal".Equals(item.Cells[0].Value))//更改轴数量
                        {
                            cardAxisNumOld = int.Parse(item.Cells[1].Value.ToString());
                            item.Cells[1].Value = (cardAxisNumOld + cardAxisNum).ToString();
                        }
                    }
                }
                _dgvMotionTotal.DataSource = dtTotal;

                //增加轴数据
                string defaultParaNo = "是";
                for (int i = 1; i <= cardAxisNum; i++)
                {
                    row = dtCard.NewRow();
                    row["轴号"] = (cardAxisNumOld + i).ToString();
                    row["英文轴名"] = "";
                    row["中文轴名"] = "";
                    row["导程"] = "40";
                    row["细分"] = "10000";
                    row["速度"] = "500";
                    row["加速度"] = "2000";
                    row["复位方向"] = "0";
                    row["复位类型"] = "0";
                    row["快-速度"] = "500";
                    row["快-加速度"] = "2000";
                    row["慢-速度"] = "100";
                    row["慢-加速度"] = "200";
                    row["一次回距离"] = "50";
                    row["二次回距离"] = "20";
                    row["超时时间"] = "3000";
                    row["是否是伺服"] = defaultParaNo;
                    row["是否反向"] = defaultParaNo;
                    row["是否复位"] = defaultParaNo;
                    row["左按钮名称"] = "+";
                    row["右按钮名称"] = "-";
                    row["是否可移动"] = defaultParaNo;
                    row["排序"] = (int.Parse(cardNum)+1).ToString() +"|1";
                    dtCard.Rows.Add(row);
                }
                _dgvMotionCard.DataSource = dtCard;
                //增加IO数据
                for (int iPort = 0; iPort < cardIONum; iPort++)
                {
                    row = dtIn.NewRow();
                    row["卡号"] = cardNum;
                    row["端口号"] = iPort.ToString();
                    row["名称"] = "备用";
                    row["类型"] = "通用";
                    dtIn.Rows.Add(row);
                    
                    row = dtOut.NewRow();
                    row["卡号"] = cardNum;
                    row["端口号"] = iPort.ToString();
                    row["名称"] = "备用";
                    row["类型"] = "通用";
                    dtOut.Rows.Add(row);
                }
                for (int iPort = 0; iPort < cardAxisNum; iPort++)
                {
                    row = dtIn.NewRow();
                    row["卡号"] = cardNum;
                    row["端口号"] = iPort.ToString();
                    row["名称"] = "备用";
                    row["类型"] = "原点";
                    dtIn.Rows.Add(row);
                    
                    row = dtIn.NewRow();
                    row["卡号"] = cardNum;
                    row["端口号"] = iPort.ToString();
                    row["名称"] = "备用";
                    row["类型"] = "正限位";
                    dtIn.Rows.Add(row);
                    
                    row = dtIn.NewRow();
                    row["卡号"] = cardNum;
                    row["端口号"] = iPort.ToString();
                    row["名称"] = "备用";
                    row["类型"] = "负限位";
                    dtIn.Rows.Add(row);
                }
                _dgvMotionIn.DataSource = dtIn;
                _dgvMotionOut.DataSource = dtOut;
            }
            else
            {
                if (_dgvMotionCard.Rows.Count <= 0) return -10;//判断是否有控制卡

                int isExtCard = 0;//判断是否已经存在扩展卡；0没有扩展卡需要增加扩展卡名称和数量，1有扩展卡只需要更改扩展卡数量
                foreach (DataGridViewRow item in _dgvMotionTotal.Rows)
                {
                    if ("ExtCard".Equals(item.Cells[0].Value.ToString()))
                    {
                        isExtCard = 1;
                        break;
                    }
                }
                //在总表增加卡的名称数据
                DataRow row = null;
                string sExtCard = "0";
                if (isExtCard == 0)
                {
                    row = dtTotal.NewRow();
                    row["参数名"] = "ExtCard";
                    row["值"] = "ExtMdl1";
                    row["备注"] = "扩展卡的文件名称";
                    dtTotal.Rows.Add(row);

                    row = dtTotal.NewRow();
                    row["参数名"] = "ExtCardTotal";
                    row["值"] = "1";
                    row["备注"] = "扩展卡的数量";
                    dtTotal.Rows.Add(row);
                }
                else
                {
                    //更改卡数量和轴数量
                    foreach (DataGridViewRow item in _dgvMotionTotal.Rows)
                    {
                        if ("ExtCardTotal".Equals(item.Cells[0].Value))//更改扩展卡数量
                        {
                            sExtCard = item.Cells[1].Value.ToString();
                            item.Cells[1].Value = (int.Parse(sExtCard) + 1).ToString();
                            break;
                        }
                    }
                }
                //在总表增加卡的IO数量数据
                row = dtTotal.NewRow();
                row["参数名"] = "ExtCardIONum_" + sExtCard ;
                row["值"] = cardIONum;
                row["备注"] = "扩展卡的IO数量";
                dtTotal.Rows.Add(row);
                _dgvMotionTotal.DataSource = dtTotal;
                
                //增加IO数据
                for (int iPort = 0; iPort < cardIONum; iPort++)
                {
                    row = dtIn.NewRow();
                    row["卡号"] = sExtCard;
                    row["端口号"] = iPort.ToString();
                    row["名称"] = "备用";
                    row["类型"] = "扩展卡";
                    dtIn.Rows.Add(row);
                    row = dtOut.NewRow();
                    row["卡号"] = sExtCard;
                    row["端口号"] = iPort.ToString();
                    row["名称"] = "备用";
                    row["类型"] = "扩展卡";
                    dtOut.Rows.Add(row);
                }
                _dgvMotionIn.DataSource = dtIn;
                _dgvMotionOut.DataSource = dtOut;
            }

            DgvNotSortable();
            return 0;
        }
        /// <summary>
        /// 写入扩展卡cfg文件
        /// </summary>
        /// <param name="iExtCard">扩展卡数量</param>
        private void WriteExtCard(int iExtCard)
        {
            string strFileFullPath = Publics.GetGtsPath() + "ExtMdl1.cfg";//Application.StartupPath + "\\参数目录\\运动控制卡\\ExtMdl1.cfg";
            //校验后再删除文件
            FileInfo file = new FileInfo(strFileFullPath);
            file.Delete();
            for (int i = 0; i < iExtCard; i++)
            {
                ParaFileINI.WriteINI("module" + i.ToString(), "type", "3", strFileFullPath);
                ParaFileINI.WriteINI("module" + i.ToString(), "address", i.ToString(), strFileFullPath);
                ParaFileINI.WriteINI("module" + i.ToString(), "defaultoutput", "0", strFileFullPath);
            }
        }
    }
}

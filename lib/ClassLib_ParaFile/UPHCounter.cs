using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClassLib_ParaFile;

namespace ClassLib_ParaFile
{
    public partial class UPHCounter : UserControl
    {
        /// <summary>
        /// 贴合次数
        /// </summary>
        public static int m_iPasteCount = 0;
        /// <summary>
        /// 生产盘数
        /// </summary>
        public static int m_iPanelCount = 0;
        /// <summary>
        /// 产品数量
        /// </summary>
        public static int m_iProductionCount = 0;

        /// <summary>
        /// 单个产品需贴合次数
        /// </summary>
        public static int m_iPerProPasteCount = 1;

        /// <summary>
        /// 本次生产贴合总数
        /// </summary>
        public static int m_iTotalPaste = 0;
        /// <summary>
        /// 本次生产盘数总数
        /// </summary>
        public static int m_iTotalPanel = 0;
        /// <summary>
        /// 本次生产产品总数
        /// </summary>
        public static int m_iTotalProduction = 0;

        /// <summary>
        /// 是否正在工作，暂停时不在工作
        /// </summary>
        public static bool m_bWorking = false;
        /// <summary>
        /// 是否需重新计数
        /// </summary>
        public static bool m_bReCount = false;
        /// <summary>
        /// 每小时保存一次生产数据
        /// </summary>
        public static bool m_bHoursPlus = false;

        /// <summary>
        /// 贴合总数
        /// </summary>
        public static int m_iTotalPasteCount = 0;
        /// <summary>
        /// 贴合总盘数
        /// </summary>
        public static int m_iTotalPanelCount = 0;
        /// <summary>
        /// 贴合总产品数
        /// </summary>
        public static int m_iTotalProductionCount = 0;

        /// <summary>
        /// 软件开启时间
        /// </summary>
        public static DateTime m_tSoftWareStart = new DateTime(2018, 1, 1);
        /// <summary>
        /// 开始工作时间
        /// </summary>
        public static DateTime m_tWorkStartTime = new DateTime(2018, 1, 1);

        /// <summary>
        /// 判断是否需保存数据时间
        /// </summary>
        public static DateTime m_tCoutStartTime = new DateTime(2018, 1, 1);
        /// <summary>
        /// 每小时生产统计开始时间
        /// </summary>
        public static DateTime m_tLastHourPlus = new DateTime(2018, 1, 1);

        /// <summary>
        /// 等待时长
        /// </summary>
        public static TimeSpan m_tWaitingDuration = new TimeSpan(0, 0, 0, 0, 0);
        /// <summary>
        /// 工作时长
        /// </summary>
        public static TimeSpan m_tWorkingDuration = new TimeSpan(0, 0, 0, 0, 0);
        /// <summary>
        /// 上次扫描时间
        /// </summary>
        public static DateTime m_tLastScanTime = new DateTime(2018, 1, 1);

        /// <summary>
        /// 设置一盘生产完时记录时间
        /// </summary>
        public static DateTime m_tPanelTime = new DateTime(2018, 1, 1);
        /// <summary>
        /// 设置单次贴合完时记录时间
        /// </summary>
        public static DateTime m_tPasteTime = new DateTime(2018, 1, 1);
        /// <summary>
        /// 设置单个产品生产完时记录时间
        /// </summary>
        public static DateTime m_tProductionTime = new DateTime(2018, 1, 1);

        /// <summary>
        /// 单盘时长
        /// </summary>
        public static TimeSpan m_tSinglePanel = new TimeSpan(0, 0, 0, 0, 0);
        /// <summary>
        /// 单次贴合时长
        /// </summary>
        public static TimeSpan m_tSinglePaste = new TimeSpan(0, 0, 0, 0, 0);
        /// <summary>
        /// 单个产品时长
        /// </summary>
        public static TimeSpan m_tSingleProduction = new TimeSpan(0, 0, 0, 0, 0);

        /// <summary>
        /// 平均每盘耗时
        /// </summary>
        public static TimeSpan m_tSinglePanelS = new TimeSpan(0, 0, 0, 0, 0);
        /// <summary>
        /// 平均每次贴合耗时
        /// </summary>
        public static TimeSpan m_tSinglePasteS = new TimeSpan(0, 0, 0, 0, 0);
        /// <summary>
        /// 平均每个产品耗时
        /// </summary>
        public static TimeSpan m_tSingleProductionS = new TimeSpan(0, 0, 0, 0, 0);

        protected static int m_iDataGridViewRowIndex = 0;

        public UPHCounter()
        {
            InitializeComponent();
            if (m_tSoftWareStart == new DateTime(2018, 1, 1))
                m_tSoftWareStart = DateTime.Now;

            if (m_tWorkStartTime == new DateTime(2018, 1, 1))
                m_tWorkStartTime = DateTime.Now;

            if (m_tCoutStartTime == new DateTime(2018, 1, 1))
                m_tCoutStartTime = DateTime.Now;

            if (m_tLastHourPlus == new DateTime(2018, 1, 1))
                m_tLastHourPlus = DateTime.Now;

            m_tLastScanTime = m_tWorkStartTime;
            timer_RefreshShow.Start();
            timer1.Start();
        }
        private void UPHCounter_Load(object sender, EventArgs e)
        {
        }
        private void timer_RefreshShow_Tick(object sender, EventArgs e)
        {
            DateTime Now = DateTime.Now;
            if (m_bWorking)
            {
                m_tWorkingDuration += Now - m_tLastScanTime;
            }
            else
            {
                m_tWaitingDuration += Now - m_tLastScanTime;
            }
            m_tLastScanTime = Now;
            ShowTime();
            HoursPlusCount();
            ReCount();
        }
        private void ShowTime()
        {
            textBox_CountPanel.Text = m_iPanelCount.ToString();
            textBox_CountPaste.Text = m_iPasteCount.ToString();
            textBox_CountProduction.Text = m_iProductionCount.ToString();
            if (m_tSingleProduction.TotalSeconds > 0)
                textBox_Speed.Text = ((int)(3600 / m_tSingleProduction.TotalSeconds)).ToString();
            textBox_TotalPanel.Text = m_iTotalPanel.ToString();
            textBox_TotalPaste.Text = m_iTotalPaste.ToString();
            textBox_TotalProduction.Text = m_iTotalProduction.ToString();

            DateTimeSplitYMDHM(textBox_TimeStart, m_tSoftWareStart);
            DateTimeSplitYMDHM(textBox_TimeWorkStart, m_tWorkStartTime);
            TimeSpanSplitHMS(textBox_TimeWait, m_tWaitingDuration);
            TimeSpanSplitHMS(textBox_TimeWork, m_tWorkingDuration);

            TimeSpanSplitMSMS(textBox_TimeSinglePanel, m_tSinglePanel);
            TimeSpanSplitSMS(textBox_TimeSinglePaste, m_tSinglePaste);
            TimeSpanSplitSMS(textBox_TimeSinglePro, m_tSingleProduction);
            if (m_PasteTime != null)
            {
                TimeSpanSplitSMS(textBox_LastPasteTime0, m_PasteTime[0]);
                TimeSpanSplitSMS(textBox_LastPasteTime1, m_PasteTime[1]);
                TimeSpanSplitSMS(textBox_LastPasteTime2, m_PasteTime[2]);
                TimeSpanSplitSMS(textBox_LastPasteTime3, m_PasteTime[3]);
            }
            if(m_iPanelCount>0)
            m_tSinglePanelS = new TimeSpan(0, 0, 0, (int)((m_tWorkingDuration.TotalSeconds) / m_iPanelCount), 0);
            if(m_iPasteCount>0)
            m_tSinglePasteS = new TimeSpan(0, 0, 0, 0, (int)(m_tWorkingDuration.TotalMilliseconds / m_iPasteCount));
            if(m_iProductionCount>0)
            m_tSingleProductionS = new TimeSpan(0, 0, 0, 0, (int)(m_tWorkingDuration.TotalMilliseconds / m_iProductionCount));

            TimeSpanSplitMS(textBox_TimeSinglePanelS, m_tSinglePanelS);
            TimeSpanSplitSMS(textBox_TimeSinglePasteS, m_tSinglePasteS);
            TimeSpanSplitSMS(textBox_TimeSingleProS, m_tSingleProductionS);
        }
        private void ReCount()
        {
            if (m_bReCount)
            {
                m_bReCount = false;
                textBox_TimeLastStart.Text = textBox_TimeWorkStart.Text;
                DateTime tLastCount = DateTime.Now;
                DateTimeSplitYMDHM(textBox_TimeLastCount, tLastCount);
                textBox_TimeLastWork.Text = textBox_TimeWork.Text;
                textBox_LastCountPanel.Text = textBox_CountPanel.Text;
                textBox_LastCountPaste.Text = textBox_CountPaste.Text;
                textBox_LastCountProduction.Text = textBox_CountProduction.Text;

                m_iPasteCount = 0;
                m_iPanelCount = 0;
                m_iProductionCount = 0;
                m_tWorkStartTime = DateTime.Now;
                m_tWaitingDuration = new TimeSpan(0, 0, 0, 0, 0);
                m_tWorkingDuration = new TimeSpan(0, 0, 0, 0, 0);
                m_tLastScanTime = m_tWorkStartTime;

                m_tPanelTime = tLastCount;
                m_tPasteTime = tLastCount;
                m_tProductionTime = tLastCount;
            }
        }
        private int CountIndex = 1;
        private void HoursPlusCount()
        {
            if (m_bHoursPlus)
            {
                m_bHoursPlus = false;
                DateTime tLastCount = DateTime.Now;
                TimeSpan _CountSpan = m_tWorkingDuration;

                int _panelCount = int.Parse(textBox_TotalPanel.Text) - m_iTotalPanelCount;
                int _pasteCount = int.Parse(textBox_TotalPaste.Text) - m_iTotalPasteCount;
                int _productionCount = int.Parse(textBox_TotalProduction.Text) - m_iTotalProductionCount;

                m_iTotalPanelCount = int.Parse(textBox_TotalPanel.Text);
                m_iTotalPasteCount = int.Parse(textBox_TotalPaste.Text);
                m_iTotalProductionCount = int.Parse(textBox_TotalProduction.Text);
                

                string[] Row = new string[7];
                Row[0] = CountIndex.ToString();
                Row[1] = DateTimeSplitYMDHM(m_tLastHourPlus);
                Row[2] = DateTimeSplitYMDHM(tLastCount);
                Row[3] = TimeSpanSplitHMS(_CountSpan);
                Row[4] = _panelCount.ToString();
                Row[5] = _pasteCount.ToString();
                Row[6] = _productionCount.ToString();

                dataGridView_UphList.Rows.Add(Row);
                CountIndex++;
                m_tLastHourPlus = tLastCount;
                SaveProductions();
               
            }
        }
        private bool SaveProductions()
        {
            try
            {
                int rowCount = dataGridView_UphList.RowCount - m_iDataGridViewRowIndex - 1;
                string[,] dt = new string[rowCount, 7];
                int j = 0;
                if (m_iDataGridViewRowIndex == 0)
                {
                    dt = new string[rowCount + 1, 7];
                    for (int i = 0; i < 7; i++)
                        dt[0, i] = dataGridView_UphList.Columns[i].HeaderText;
                    j = 1;
                }
                int m = 0;
                for (int i = m_iDataGridViewRowIndex; i < rowCount + m_iDataGridViewRowIndex; i++)
                {
                    for (int n = 0; n < 7; n++)
                        dt[m + j, n] = dataGridView_UphList.Rows[i].Cells[n].Value.ToString();
                    m++;
                }
                DateTime now = DateTime.Now;
                string day = now.Year.ToString() + "_" + now.Month.ToString() + "_" + now.Day.ToString();
                if (ParaFileCSV.SaveData(dt, "D:\\生产统计\\" + day + ".csv"))
                {
                    m_iDataGridViewRowIndex = dataGridView_UphList.RowCount - 1;
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        private void DateTimeSplitYMDHM(TextBox _textBox, DateTime _dateTime)
        {
            _textBox.Text = _dateTime.Year.ToString() + "年"
                            + _dateTime.Month.ToString() + "月"
                            + _dateTime.Day.ToString() + "日"
                            + _dateTime.Hour.ToString() + "点"
                            + _dateTime.Minute.ToString() + "分";
        }
        private void TimeSpanSplitHMS(TextBox _textBox, TimeSpan _timeSpan)
        {
            _textBox.Text = _timeSpan.Hours.ToString() + "时"
                            + _timeSpan.Minutes.ToString() + "分"
                            + _timeSpan.Seconds.ToString() + "秒";
        }
        private void TimeSpanSplitMS(TextBox _textBox, TimeSpan _timeSpan)
        {
            _textBox.Text = _timeSpan.Minutes.ToString() + "分"
                           + _timeSpan.Seconds.ToString() + "秒";
        }
        private void TimeSpanSplitMSMS(TextBox _textBox, TimeSpan _timeSpan)
        {
            _textBox.Text = _timeSpan.Minutes.ToString() + "分"
                           + _timeSpan.Seconds.ToString() + "秒"
                           + _timeSpan.Milliseconds.ToString() + "毫秒";
        }
        private void TimeSpanSplitSMS(TextBox _textBox, TimeSpan _timeSpan)
        {
            _textBox.Text = _timeSpan.Seconds.ToString() + "秒" + _timeSpan.Milliseconds.ToString() + "毫秒";
        }
        public string DateTimeSplitYMDHM(DateTime _dateTime)
        {
            return _dateTime.Year.ToString() + "."
                    + _dateTime.Month.ToString() + "."
                    + _dateTime.Day.ToString() + "."
                    + _dateTime.Hour.ToString() + ":"
                    + _dateTime.Minute.ToString();
        }
        public string TimeSpanSplitHMS(TimeSpan _timeSpan)
        {
            return _timeSpan.Hours.ToString() + "."
                    + _timeSpan.Minutes.ToString() + "."
                    + _timeSpan.Seconds.ToString();
        }
        private void BTN_ReCount_Click(object sender, EventArgs e)
        {
            m_bReCount = true;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime _now = DateTime.Now;
            if (_now.Minute == 0 && _now.Hour != m_tCoutStartTime.Hour)
            {
                m_tCoutStartTime = _now;
                m_bHoursPlus = true;
                m_bReCount = true;
            }
        }
        public void BTN_SaveProdution_Click(object sender, EventArgs e)
        {
            m_bHoursPlus = true;
            HoursPlusCount();
            m_bReCount = true;
            //if (SaveProductions())
            //{
            //    DialogResult result = MessageBox.Show("保存完成，是否清空表格数据", "", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2);
            //    if (result == DialogResult.Yes)
            //    {
            //        dataGridView_UphList.Rows.Clear();
            //        m_iDataGridViewRowIndex = 0;
            //    }
            //}
            //else
            //    MessageBox.Show("生产数据已自动保存");
        }

        /// <summary>
        /// 设置单个产品需贴合次数
        /// </summary>
        /// <param name="paste"></param>
        public static void SetPerProPasteCount(int paste)
        {
            m_iPerProPasteCount = paste;
        }
        public static void PastePlus()
        {
            m_iPasteCount += 1;
            m_iTotalPaste += 1;

            DateTime _now = DateTime.Now;
            m_tSinglePaste = _now - m_tPasteTime;
            m_tPasteTime = _now;

            int production = (int)(m_iPasteCount / m_iPerProPasteCount);
            if (production > m_iProductionCount)
                ProductionPlus();
        }
        public static void ProductionPlus()
        {
            m_iProductionCount += 1;
            m_iTotalProduction += 1;
            
            DateTime _now = DateTime.Now;
            m_tSingleProduction = _now - m_tProductionTime;
            m_tProductionTime = _now;
        }
        public static void PanelPlus()
        {
            m_iPanelCount += 1;
            m_iTotalPanel += 1;

            DateTime _now = DateTime.Now;
            m_tSinglePanel = _now - m_tPanelTime;
            m_tPanelTime = _now;
            PanelStarted = false;
        }
        public static bool PanelStarted = false;
        public static void PanelStart()
        {
            DateTime _now = DateTime.Now;
            m_tPanelTime = _now;
            PanelStarted = true;
        }
        public static bool Started = false;
        public static void SetStartWork()
        {
            m_tPasteTime = DateTime.Now;
            m_tProductionTime = DateTime.Now;
            m_tPanelTime = DateTime.Now;
            m_PasteTime = new TimeSpan[4] { new TimeSpan(), new TimeSpan(), new TimeSpan(), new TimeSpan() };
            m_LastPasteTime = new DateTime[4] { DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now };
            Started = true;
        }
        public static void SetEndWork()
        {
            Started = false;
            SetPause();
        }
        public static void SetWork()
        {
            if (Started && PanelStarted)
                m_bWorking = true;
        }
        public static void SetPause()
        {
            m_bWorking = false;
        }
        public static bool GetWork()
        {
            return m_bWorking;
        }
        public static TimeSpan[] m_PasteTime = null;
        public static DateTime[] m_LastPasteTime = null;
        public static void PastePlus(int index)
        {
            DateTime _now = DateTime.Now;
            m_PasteTime[index] = _now - m_LastPasteTime[index];
            m_LastPasteTime[index] = _now;
        }

        private void btClearData_Click(object sender, EventArgs e)
        {
            m_bHoursPlus = true;
            HoursPlusCount();
            m_bReCount = true;
            dataGridView_UphList.Rows.Clear();
            m_iDataGridViewRowIndex = 0;
        }
    }
}

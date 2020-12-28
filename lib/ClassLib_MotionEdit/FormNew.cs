using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCWin;

namespace ClassLib_MotionEdit
{
    public partial class FormNew : CCSkinMain
    {
        internal delegate void ChangedOpen();
        internal event ChangedOpen changedOpen;

        // 新增卡参数
        private Dictionary<string, string> _dataParameter = new Dictionary<string, string>();
        public Dictionary<string, string> DataParameter
        {
            get
            {
                return _dataParameter;
            }

            set
            {
                _dataParameter = value;
            }
        }
        public FormNew()
        {
            InitializeComponent();

            InitializeDataGridView();
        }
        private void InitializeDataGridView()
        {
            int index = dgvParameterEditing.Rows.Add();
            dgvParameterEditing.Rows[index].Cells[0].Value = "卡类型";
            dgvParameterEditing.Rows[index].Cells[1].Value = "";
            dgvParameterEditing.Rows[index].Cells[2].Value = "0运动控制卡，1扩展卡";

            //index = dgvParameterEditing.Rows.Add();
            //dgvParameterEditing.Rows[index].Cells[0].Value = "卡号";
            //dgvParameterEditing.Rows[index].Cells[1].Value = "";
            //dgvParameterEditing.Rows[index].Cells[2].Value = "第几号卡,第一张卡为0";

            index = dgvParameterEditing.Rows.Add();
            dgvParameterEditing.Rows[index].Cells[0].Value = "卡名称";
            dgvParameterEditing.Rows[index].Cells[1].Value = "";
            dgvParameterEditing.Rows[index].Cells[2].Value = "控制卡或扩展卡的参数文件名";

            index = dgvParameterEditing.Rows.Add();
            dgvParameterEditing.Rows[index].Cells[0].Value = "轴数量";
            dgvParameterEditing.Rows[index].Cells[1].Value = "";
            dgvParameterEditing.Rows[index].Cells[2].Value = "本张控制卡的轴个数";

            index = dgvParameterEditing.Rows.Add();
            dgvParameterEditing.Rows[index].Cells[0].Value = "IO数量";
            dgvParameterEditing.Rows[index].Cells[1].Value = "";
            dgvParameterEditing.Rows[index].Cells[2].Value = "控制卡IO总数，默认16，填写16/24";
        }

        private void btnNewCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNowOK_Click(object sender, EventArgs e)
        {
            try
            {
                _dataParameter.Clear();
                string cardTray = "0";
                foreach (DataGridViewRow item in dgvParameterEditing.Rows)
                {
                    if ("卡类型".Equals(item.Cells[0].Value.ToString()))
                    {
                        if (item.Cells[1].Value == null || !"0".Equals(item.Cells[1].Value.ToString()) && !"1".Equals(item.Cells[1].Value.ToString()))
                        {
                            MessageBoxEx.Show("卡类型设置异常，只能为0或1（0运动控制卡，1扩展卡）");
                            return;
                        }
                        cardTray = item.Cells[1].Value.ToString();
                    }
                    else if ("卡号".Equals(item.Cells[0].Value.ToString()))
                    {
                        if (item.Cells[1].Value == null || "".Equals(item.Cells[1].Value.ToString()))
                        {
                            if (!"1".Equals(dgvParameterEditing.Rows[0].Cells[1].Value))//扩展卡可以不填写
                            {
                                MessageBoxEx.Show("卡号不能为空");
                                return;
                            }
                        }
                    }
                    else if ("卡名称".Equals(item.Cells[0].Value.ToString()))
                    {
                        if (item.Cells[1].Value == null || "".Equals(item.Cells[1].Value.ToString()))
                        {
                            if (!"1".Equals(dgvParameterEditing.Rows[0].Cells[1].Value))//扩展卡可以不填写
                            {
                                MessageBoxEx.Show("卡名称不能为空");
                                return;
                            }
                        }
                    }
                    else if ("轴数量".Equals(item.Cells[0].Value.ToString()))
                    {
                        if (item.Cells[1].Value == null || "".Equals(item.Cells[1].Value.ToString()))
                        {
                            if (cardTray.Equals("0"))
                            {
                                MessageBoxEx.Show("当为运动控制卡时轴数量不能为空");
                                return;
                            }
                            else
                                item.Cells[1].Value = 0;
                        }
                        else if ("4".Equals(item.Cells[1].Value.ToString()) && "8".Equals(item.Cells[1].Value.ToString()))
                        {
                            MessageBoxEx.Show("轴数量设置异常，需要设置为4或者8");
                            return;
                        }
                    }
                    else if ("IO数量".Equals(item.Cells[0].Value.ToString()))
                    {
                        if (item.Cells[1].Value == null || "".Equals(item.Cells[1].Value.ToString())) item.Cells[1].Value = 16;
                    }
                    _dataParameter.Add(item.Cells[0].Value.ToString(),item.Cells[1].Value.ToString());
                }
                if (changedOpen != null)
                {
                    changedOpen.Invoke();
                }
            }
            catch (Exception)
            {
                MessageBoxEx.Show("数据格式异常，请检查");
            }
        }
    }
}

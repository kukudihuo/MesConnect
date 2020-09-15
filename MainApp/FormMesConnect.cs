using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Newtonsoft.Json;

namespace MainApp
{
    public partial class FormMesConnect : Form
    {
        public FormMesConnect()
        {
            InitializeComponent();
            Thread.CurrentThread.Name = "MainThread";
            //接收内容的界面显示
            presenter.AddDispatchEvent(ReceiveMsgInvoke);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            presenter.Close();
        }
        IoTPresenter presenter = new IoTPresenter();
        private void Open_Click(object sender, EventArgs e)
        {
            presenter.Start();
            listBox1.Items.Clear();
        }
        private void Close_Click(object sender, EventArgs e)
        {
            presenter.Close();
        }
        void ReceiveMsgInvoke(object sender, EventArgs e)
        {
            listBox1.Items.Add(JsonConvert.SerializeObject(((IoTEventArg)e).Data));            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            presenter.Start();
            listBox1.Items.Clear();
            Hide();//最小化后隐藏窗口到后台
        }
    }
}

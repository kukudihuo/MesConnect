using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClassLib_ParaFile
{
    public partial class Display : Form
    {
        public Display()
        {
            InitializeComponent();
        }

        #region 调试信息显示
        public System.Windows.Forms.TextBox TextBox_ShowMessage = null;
        public System.Windows.Forms.TextBox TextBox_ShowResult = null, TextBox_Log = null, TextBox_ShowAlram = null;

        /// <summary>
        /// 在textBox文本框中追加一段信息并自动换行
        /// </summary>
        /// <param name="text"></param>
        /// <remarks></remarks>
        public void DisplayText(string text, System.Windows.Forms.TextBox textBox = null)
        {
            try
            {
                if (textBox != null)
                {
                    Publics.sDisplay = this;
                    TextBox_ShowMessage = textBox;
                }
                //更新系统时间
                string str_DataTime_Now = Publics.GetSystem_MilliSecond();
                DisplayText_NoTime(str_DataTime_Now);
                DisplayText_NoTime(text);
                //BeginInvoke(new EventHandler(textBox_ShowMessage_AddInfo), str_DataTime_Now + "\r\n" + " " + text + "\r\n");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        //安全显示状态信息
        ///<summary>
        ///在textBox文本框中追加一段信息并自动换行
        /// </summary>
        /// <param name="text"></param>
        /// <remarks></remarks>
        public void DisplayText_NoTime(string text)
        {
            try
            {
                BeginInvoke(new EventHandler(textBox_ShowMessage_AddInfo), text + "\r\n");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// 在textBox_Result文本框中追加一段信息并自动换行
        /// </summary>
        /// <param name="text"></param>
        public void DisplayText_Result(string text, System.Windows.Forms.TextBox textBox = null)
        {
            try
            {
                if (textBox != null)
                    TextBox_ShowResult = textBox;
                BeginInvoke(new EventHandler(textBox_ShowResult_AddInfo), text + "\r\n");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 在TextBox_Log文本框中追加一段信息并自动换行
        /// </summary>
        /// <param name="text"></param>
        public void DisplayText_Log(string text, System.Windows.Forms.TextBox textBox = null, bool bAutoShowTime = true)
        {
            try
            {
                if (textBox != null)
                    TextBox_Log = textBox;
                string str_DataTime_Now = Publics.GetSystem_MilliSecond();
                if (bAutoShowTime)
                    text += " " + str_DataTime_Now;
                BeginInvoke(new EventHandler(textBox_Log_AddInfo), text + "\r\n");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 在TextBox_ShowAlram文本框中追加一段信息并自动换行
        /// </summary>
        /// <param name="text"></param>
        public void DisplayTextBox_ShowAlram(string text, System.Windows.Forms.TextBox textBox = null)
        {
            try
            {
                if (textBox != null)
                    TextBox_ShowAlram = textBox;
                string str_DataTime_Now = Publics.GetSystem_MilliSecond();
                text += " " + str_DataTime_Now;
                BeginInvoke(new EventHandler(textBox_ShowAlram_AddInfo), text + "\r\n");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }
        ///<summary>
        ///非UI线程调用窗体控件，保证线程安全。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        void textBox_ShowMessage_AddInfo(System.Object sender, System.EventArgs ByVal)
        {
            try
            {
                if (TextBox_ShowMessage != null)
                    TextBox_ShowMessage.AppendText(sender.ToString());
            }
            catch
            {
                System.Diagnostics.Trace.WriteLine(sender.ToString());
            }
        }

        void textBox_ShowResult_AddInfo(System.Object sender, System.EventArgs ByVal)
        {
            try
            {
                if (TextBox_ShowResult != null)
                    TextBox_ShowResult.AppendText(sender.ToString());
            }
            catch
            {
                System.Diagnostics.Trace.WriteLine(sender.ToString());
            }
        }

        void textBox_Log_AddInfo(System.Object sender, System.EventArgs ByVal)
        {
            try
            {
                if (TextBox_Log != null)
                    TextBox_Log.AppendText(sender.ToString());
            }
            catch
            {
                System.Diagnostics.Trace.WriteLine(sender.ToString());
            }
        }
        void textBox_ShowAlram_AddInfo(System.Object sender, System.EventArgs ByVal)
        {
            try
            {
                if (TextBox_ShowAlram != null)
                    TextBox_ShowAlram.AppendText(sender.ToString());
            }
            catch
            {
                System.Diagnostics.Trace.WriteLine(sender.ToString());
            }
        }
        /// <summary>
        /// 在textBox文本框中追加一段信息并自动换行
        /// </summary>
        /// <param name="text"></param>
        /// <remarks></remarks>
        public void DisplayText(string text, System.Windows.Forms.TextBox textBox, bool bTimeInfo)
        {
            try
            {
                if (Publics.sDisplay == null)
                {
                    Publics.sDisplay = this;
                }
                //更新系统时间
                if (bTimeInfo)
                {
                    string str_DataTime_Now = Publics.GetSystem_MilliSecond();
                    text = str_DataTime_Now + "\r\n" + text;
                }
                BeginInvoke(new TextBoxShowMessage(textBox_ShowMessage), text + "\r\n", textBox);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        delegate void TextBoxShowMessage(string text, System.Windows.Forms.TextBox textBox);
        void textBox_ShowMessage(string text, System.Windows.Forms.TextBox textBox)
        {
            textBox.AppendText(text);
        }
        #endregion
    }
}

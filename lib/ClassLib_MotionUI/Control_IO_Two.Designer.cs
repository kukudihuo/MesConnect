﻿namespace ClassLib_MotionUI
{
    partial class Control_IO_Two
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.control_IO_One2 = new Control_IO_One();
            this.control_IO_One1 = new Control_IO_One();
            this.SuspendLayout();
            // 
            // control_IO_One2
            // 
            this.control_IO_One2.AutoSize = true;
            this.control_IO_One2.IO_Name = "EXO0_0";
            this.control_IO_One2.Location = new System.Drawing.Point(2, 27);
            this.control_IO_One2.Name = "control_IO_One2";
            this.control_IO_One2.Size = new System.Drawing.Size(136, 27);
            this.control_IO_One2.TabIndex = 1;
            this.control_IO_One2.DoCheckEvent += new System.EventHandler(this.control_IO_One2_DoCheckEvent);
            // 
            // control_IO_One1
            // 
            this.control_IO_One1.AutoSize = true;
            this.control_IO_One1.IO_Name = "EXO0_0";
            this.control_IO_One1.Location = new System.Drawing.Point(2, 2);
            this.control_IO_One1.Name = "control_IO_One1";
            this.control_IO_One1.Size = new System.Drawing.Size(136, 27);
            this.control_IO_One1.TabIndex = 0;
            this.control_IO_One1.DoCheckEvent += new System.EventHandler(this.control_IO_One1_DoCheckEvent);
            this.control_IO_One1.DoBeginCheckEvent += new DoBeginCheckdelegate(this.control_IO_One1_DoBeginCheckEvent);
            // 
            // Control_IO_Two
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.control_IO_One2);
            this.Controls.Add(this.control_IO_One1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Control_IO_Two";
            this.Size = new System.Drawing.Size(154, 57);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Control_IO_One control_IO_One1;
        private Control_IO_One control_IO_One2;
    }
}

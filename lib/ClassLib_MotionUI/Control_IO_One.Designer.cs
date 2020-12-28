namespace ClassLib_MotionUI
{
    partial class Control_IO_One
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
            this.Label_IO = new System.Windows.Forms.Label();
            this.Label_Name = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Label_IO
            // 
            this.Label_IO.BackColor = System.Drawing.Color.Gray;
            this.Label_IO.Font = new System.Drawing.Font("宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_IO.ForeColor = System.Drawing.Color.White;
            this.Label_IO.Location = new System.Drawing.Point(1, 2);
            this.Label_IO.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label_IO.Name = "Label_IO";
            this.Label_IO.Size = new System.Drawing.Size(55, 30);
            this.Label_IO.TabIndex = 4;
            this.Label_IO.Text = "IN12";
            this.Label_IO.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label_IO.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Label_IO_MouseClick);
            // 
            // Label_Name
            // 
            this.Label_Name.AutoSize = true;
            this.Label_Name.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.Label_Name.Location = new System.Drawing.Point(56, 10);
            this.Label_Name.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label_Name.Name = "Label_Name";
            this.Label_Name.Size = new System.Drawing.Size(52, 15);
            this.Label_Name.TabIndex = 36;
            this.Label_Name.Text = "端口名";
            // 
            // Control_IO_One
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Label_IO);
            this.Controls.Add(this.Label_Name);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Control_IO_One";
            this.Size = new System.Drawing.Size(182, 34);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Label_IO;
        internal System.Windows.Forms.Label Label_Name;
    }
}

namespace ClassLib_MotionUI
{
    partial class Control_AxisLabel
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
            this.btMove = new System.Windows.Forms.LinkLabel();
            this.btGetValue = new System.Windows.Forms.LinkLabel();
            this.labelEdit = new ClassLib_LabelEdit.Control_LabelEdit();
            this.SuspendLayout();
            // 
            // btMove
            // 
            this.btMove.AutoSize = true;
            this.btMove.Font = new System.Drawing.Font("楷体", 9.5F, System.Drawing.FontStyle.Bold);
            this.btMove.Location = new System.Drawing.Point(101, 26);
            this.btMove.Name = "btMove";
            this.btMove.Size = new System.Drawing.Size(37, 13);
            this.btMove.TabIndex = 74;
            this.btMove.TabStop = true;
            this.btMove.Text = "运动";
            this.btMove.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btMove_LinkClicked);
            // 
            // btGetValue
            // 
            this.btGetValue.AutoSize = true;
            this.btGetValue.Location = new System.Drawing.Point(102, 7);
            this.btGetValue.Name = "btGetValue";
            this.btGetValue.Size = new System.Drawing.Size(29, 12);
            this.btGetValue.TabIndex = 73;
            this.btGetValue.TabStop = true;
            this.btGetValue.Text = "取值";
            this.btGetValue.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btGetValue_LinkClicked);
            // 
            // labelEdit
            // 
            this.labelEdit.DefaultLabel = "X原点位";
            this.labelEdit.DefaultString = "0";
            this.labelEdit.ForeColor = System.Drawing.SystemColors.Highlight;
            this.labelEdit.Location = new System.Drawing.Point(1, 3);
            this.labelEdit.Margin = new System.Windows.Forms.Padding(4);
            this.labelEdit.Name = "labelEdit";
            this.labelEdit.Size = new System.Drawing.Size(100, 38);
            this.labelEdit.TabIndex = 72;
            this.labelEdit.TextBoxWidth = 100;
            // 
            // Control_AxisLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btMove);
            this.Controls.Add(this.btGetValue);
            this.Controls.Add(this.labelEdit);
            this.Name = "Control_AxisLabel";
            this.Size = new System.Drawing.Size(132, 44);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel btMove;
        private System.Windows.Forms.LinkLabel btGetValue;
        public  ClassLib_LabelEdit.Control_LabelEdit labelEdit;
    }
}

namespace ClassLib_MotionUI
{
    partial class Control_AxisShort
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
            this.ComboBox_MoveDis = new System.Windows.Forms.ComboBox();
            this.CheckBox_MOVE_RES = new System.Windows.Forms.CheckBox();
            this.Label_EncodePos = new System.Windows.Forms.Label();
            this.ComboBox_ContinueSpeed = new System.Windows.Forms.ComboBox();
            this.Button_Plus = new System.Windows.Forms.Button();
            this.Button_Neget = new System.Windows.Forms.Button();
            this.Label_ProfilePos = new System.Windows.Forms.Label();
            this.textBoxMoveTo = new System.Windows.Forms.TextBox();
            this.btMoveTo = new System.Windows.Forms.Button();
            this.Label_AxisName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ComboBox_MoveDis
            // 
            this.ComboBox_MoveDis.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ComboBox_MoveDis.FormattingEnabled = true;
            this.ComboBox_MoveDis.Items.AddRange(new object[] {
            "0.1",
            "1",
            "10"});
            this.ComboBox_MoveDis.Location = new System.Drawing.Point(104, 6);
            this.ComboBox_MoveDis.Margin = new System.Windows.Forms.Padding(2);
            this.ComboBox_MoveDis.Name = "ComboBox_MoveDis";
            this.ComboBox_MoveDis.Size = new System.Drawing.Size(47, 22);
            this.ComboBox_MoveDis.TabIndex = 504;
            this.ComboBox_MoveDis.Text = "0.1";
            this.ComboBox_MoveDis.Visible = false;
            // 
            // CheckBox_MOVE_RES
            // 
            this.CheckBox_MOVE_RES.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CheckBox_MOVE_RES.Location = new System.Drawing.Point(153, 5);
            this.CheckBox_MOVE_RES.Margin = new System.Windows.Forms.Padding(2);
            this.CheckBox_MOVE_RES.Name = "CheckBox_MOVE_RES";
            this.CheckBox_MOVE_RES.Size = new System.Drawing.Size(51, 16);
            this.CheckBox_MOVE_RES.TabIndex = 503;
            this.CheckBox_MOVE_RES.Tag = "3";
            this.CheckBox_MOVE_RES.Text = "点动";
            this.CheckBox_MOVE_RES.UseVisualStyleBackColor = true;
            this.CheckBox_MOVE_RES.CheckedChanged += new System.EventHandler(this.CheckBox_MOVE_RES_CheckedChanged);
            // 
            // Label_EncodePos
            // 
            this.Label_EncodePos.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_EncodePos.Location = new System.Drawing.Point(256, 14);
            this.Label_EncodePos.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_EncodePos.Name = "Label_EncodePos";
            this.Label_EncodePos.Size = new System.Drawing.Size(54, 14);
            this.Label_EncodePos.TabIndex = 502;
            this.Label_EncodePos.Text = "-123.789";
            this.Label_EncodePos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ComboBox_ContinueSpeed
            // 
            this.ComboBox_ContinueSpeed.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ComboBox_ContinueSpeed.FormattingEnabled = true;
            this.ComboBox_ContinueSpeed.Items.AddRange(new object[] {
            "2",
            "10",
            "30"});
            this.ComboBox_ContinueSpeed.Location = new System.Drawing.Point(104, 12);
            this.ComboBox_ContinueSpeed.Margin = new System.Windows.Forms.Padding(2);
            this.ComboBox_ContinueSpeed.Name = "ComboBox_ContinueSpeed";
            this.ComboBox_ContinueSpeed.Size = new System.Drawing.Size(47, 22);
            this.ComboBox_ContinueSpeed.TabIndex = 501;
            this.ComboBox_ContinueSpeed.Text = "2";
            // 
            // Button_Plus
            // 
            this.Button_Plus.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Button_Plus.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Button_Plus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Button_Plus.Location = new System.Drawing.Point(53, 6);
            this.Button_Plus.Margin = new System.Windows.Forms.Padding(2);
            this.Button_Plus.Name = "Button_Plus";
            this.Button_Plus.Size = new System.Drawing.Size(44, 27);
            this.Button_Plus.TabIndex = 499;
            this.Button_Plus.Tag = "2";
            this.Button_Plus.UseVisualStyleBackColor = false;
            this.Button_Plus.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_Plus_MouseDown);
            this.Button_Plus.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_Neget_MouseUp);
            // 
            // Button_Neget
            // 
            this.Button_Neget.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Button_Neget.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Button_Neget.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Button_Neget.Location = new System.Drawing.Point(5, 6);
            this.Button_Neget.Margin = new System.Windows.Forms.Padding(2);
            this.Button_Neget.Name = "Button_Neget";
            this.Button_Neget.Size = new System.Drawing.Size(44, 27);
            this.Button_Neget.TabIndex = 500;
            this.Button_Neget.Tag = "1";
            this.Button_Neget.Text = "左←";
            this.Button_Neget.UseVisualStyleBackColor = false;
            this.Button_Neget.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_Neget_MouseDown);
            this.Button_Neget.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_Neget_MouseUp);
            // 
            // Label_ProfilePos
            // 
            this.Label_ProfilePos.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_ProfilePos.Location = new System.Drawing.Point(197, 14);
            this.Label_ProfilePos.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_ProfilePos.Name = "Label_ProfilePos";
            this.Label_ProfilePos.Size = new System.Drawing.Size(54, 14);
            this.Label_ProfilePos.TabIndex = 498;
            this.Label_ProfilePos.Text = "-123.789";
            this.Label_ProfilePos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxMoveTo
            // 
            this.textBoxMoveTo.Location = new System.Drawing.Point(312, 10);
            this.textBoxMoveTo.Name = "textBoxMoveTo";
            this.textBoxMoveTo.Size = new System.Drawing.Size(48, 21);
            this.textBoxMoveTo.TabIndex = 505;
            this.textBoxMoveTo.Text = "0";
            // 
            // btMoveTo
            // 
            this.btMoveTo.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btMoveTo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btMoveTo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btMoveTo.Location = new System.Drawing.Point(361, 7);
            this.btMoveTo.Margin = new System.Windows.Forms.Padding(2);
            this.btMoveTo.Name = "btMoveTo";
            this.btMoveTo.Size = new System.Drawing.Size(44, 27);
            this.btMoveTo.TabIndex = 506;
            this.btMoveTo.Tag = "2";
            this.btMoveTo.Text = "运动";
            this.btMoveTo.UseVisualStyleBackColor = false;
            this.btMoveTo.Click += new System.EventHandler(this.btMoveTo_Click);
            // 
            // Label_AxisName
            // 
            this.Label_AxisName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_AxisName.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.Label_AxisName.Location = new System.Drawing.Point(408, 14);
            this.Label_AxisName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_AxisName.Name = "Label_AxisName";
            this.Label_AxisName.Size = new System.Drawing.Size(176, 12);
            this.Label_AxisName.TabIndex = 507;
            this.Label_AxisName.Text = "AX";
            // 
            // Control_AxisShort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Label_ProfilePos);
            this.Controls.Add(this.Label_AxisName);
            this.Controls.Add(this.btMoveTo);
            this.Controls.Add(this.textBoxMoveTo);
            this.Controls.Add(this.ComboBox_MoveDis);
            this.Controls.Add(this.CheckBox_MOVE_RES);
            this.Controls.Add(this.Label_EncodePos);
            this.Controls.Add(this.ComboBox_ContinueSpeed);
            this.Controls.Add(this.Button_Plus);
            this.Controls.Add(this.Button_Neget);
            this.Name = "Control_AxisShort";
            this.Size = new System.Drawing.Size(586, 39);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ComboBox ComboBox_MoveDis;
        internal System.Windows.Forms.CheckBox CheckBox_MOVE_RES;
        internal System.Windows.Forms.Label Label_EncodePos;
        internal System.Windows.Forms.ComboBox ComboBox_ContinueSpeed;
        internal System.Windows.Forms.Button Button_Plus;
        internal System.Windows.Forms.Button Button_Neget;
        internal System.Windows.Forms.Label Label_ProfilePos;
        private System.Windows.Forms.TextBox textBoxMoveTo;
        internal System.Windows.Forms.Button btMoveTo;
        internal System.Windows.Forms.Label Label_AxisName;
    }
}

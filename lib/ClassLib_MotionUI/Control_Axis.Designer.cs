namespace ClassLib_MotionUI
{
    partial class Control_Axis
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
            this.Button_Reset = new System.Windows.Forms.Button();
            this.Label_IsReseted = new System.Windows.Forms.Label();
            this.Label_ServoOn = new System.Windows.Forms.Label();
            this.Label_MotionStatus = new System.Windows.Forms.Label();
            this.Label_Alert = new System.Windows.Forms.Label();
            this.Label_PlusLimit = new System.Windows.Forms.Label();
            this.Label_NegtLimit = new System.Windows.Forms.Label();
            this.CheckBox_ServoOn = new System.Windows.Forms.CheckBox();
            this.Label_AxisName = new System.Windows.Forms.Label();
            this.control_Thread_Reset = new ClassLib_StdThread.Control_Thread();
            this.control_AxisShort = new Control_AxisShort();
            this.textBox_Vel = new System.Windows.Forms.TextBox();
            this.textBox_Acc = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Button_Reset
            // 
            this.Button_Reset.Location = new System.Drawing.Point(511, 5);
            this.Button_Reset.Name = "Button_Reset";
            this.Button_Reset.Size = new System.Drawing.Size(57, 23);
            this.Button_Reset.TabIndex = 493;
            this.Button_Reset.Tag = "4";
            this.Button_Reset.Text = "复位";
            this.Button_Reset.UseVisualStyleBackColor = true;
            this.Button_Reset.Click += new System.EventHandler(this.Button_Reset_Click);
            // 
            // Label_IsReseted
            // 
            this.Label_IsReseted.BackColor = System.Drawing.Color.Gray;
            this.Label_IsReseted.Location = new System.Drawing.Point(162, 7);
            this.Label_IsReseted.Name = "Label_IsReseted";
            this.Label_IsReseted.Size = new System.Drawing.Size(28, 16);
            this.Label_IsReseted.TabIndex = 492;
            this.Label_IsReseted.Text = "Rst";
            this.Label_IsReseted.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label_IsReseted.BackColorChanged += new System.EventHandler(this.Label_IsReseted_BackColorChanged);
            // 
            // Label_ServoOn
            // 
            this.Label_ServoOn.BackColor = System.Drawing.Color.Gray;
            this.Label_ServoOn.Location = new System.Drawing.Point(130, 7);
            this.Label_ServoOn.Name = "Label_ServoOn";
            this.Label_ServoOn.Size = new System.Drawing.Size(28, 16);
            this.Label_ServoOn.TabIndex = 486;
            this.Label_ServoOn.Text = "On";
            this.Label_ServoOn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_MotionStatus
            // 
            this.Label_MotionStatus.BackColor = System.Drawing.Color.Gray;
            this.Label_MotionStatus.Location = new System.Drawing.Point(99, 7);
            this.Label_MotionStatus.Name = "Label_MotionStatus";
            this.Label_MotionStatus.Size = new System.Drawing.Size(28, 16);
            this.Label_MotionStatus.TabIndex = 485;
            this.Label_MotionStatus.Text = "Mov";
            this.Label_MotionStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_Alert
            // 
            this.Label_Alert.BackColor = System.Drawing.Color.Gray;
            this.Label_Alert.Location = new System.Drawing.Point(68, 7);
            this.Label_Alert.Name = "Label_Alert";
            this.Label_Alert.Size = new System.Drawing.Size(28, 16);
            this.Label_Alert.TabIndex = 484;
            this.Label_Alert.Text = "Alm";
            this.Label_Alert.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label_Alert.BackColorChanged += new System.EventHandler(this.Label_Alert_BackColorChanged);
            // 
            // Label_PlusLimit
            // 
            this.Label_PlusLimit.BackColor = System.Drawing.Color.Gray;
            this.Label_PlusLimit.Location = new System.Drawing.Point(37, 7);
            this.Label_PlusLimit.Name = "Label_PlusLimit";
            this.Label_PlusLimit.Size = new System.Drawing.Size(28, 16);
            this.Label_PlusLimit.TabIndex = 483;
            this.Label_PlusLimit.Text = "L+";
            this.Label_PlusLimit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_NegtLimit
            // 
            this.Label_NegtLimit.BackColor = System.Drawing.Color.Gray;
            this.Label_NegtLimit.Location = new System.Drawing.Point(7, 7);
            this.Label_NegtLimit.Name = "Label_NegtLimit";
            this.Label_NegtLimit.Size = new System.Drawing.Size(28, 16);
            this.Label_NegtLimit.TabIndex = 482;
            this.Label_NegtLimit.Text = "L-";
            this.Label_NegtLimit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CheckBox_ServoOn
            // 
            this.CheckBox_ServoOn.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CheckBox_ServoOn.Location = new System.Drawing.Point(348, 19);
            this.CheckBox_ServoOn.Margin = new System.Windows.Forms.Padding(2);
            this.CheckBox_ServoOn.Name = "CheckBox_ServoOn";
            this.CheckBox_ServoOn.Size = new System.Drawing.Size(51, 16);
            this.CheckBox_ServoOn.TabIndex = 490;
            this.CheckBox_ServoOn.Tag = "3";
            this.CheckBox_ServoOn.Text = "使能";
            this.CheckBox_ServoOn.UseVisualStyleBackColor = true;
            this.CheckBox_ServoOn.CheckedChanged += new System.EventHandler(this.CheckBox_ServoOn_CheckedChanged);
            // 
            // Label_AxisName
            // 
            this.Label_AxisName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_AxisName.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.Label_AxisName.Location = new System.Drawing.Point(659, 9);
            this.Label_AxisName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_AxisName.Name = "Label_AxisName";
            this.Label_AxisName.Size = new System.Drawing.Size(227, 12);
            this.Label_AxisName.TabIndex = 500;
            this.Label_AxisName.Text = "AX                        ";
            // 
            // control_Thread_Reset
            // 
            this.control_Thread_Reset.LabelText = "软件线程";
            this.control_Thread_Reset.Location = new System.Drawing.Point(3, -5);
            this.control_Thread_Reset.Name = "control_Thread_Reset";
            this.control_Thread_Reset.RunOnce = true;
            this.control_Thread_Reset.Size = new System.Drawing.Size(93, 21);
            this.control_Thread_Reset.TabIndex = 498;
            this.control_Thread_Reset.Visible = false;
            this.control_Thread_Reset.LoadEvent += new ClassLib_StdThread.LoadEventHandler(this.control_Thread_Reset_LoadEvent);
            // 
            // control_AxisShort
            // 
            this.control_AxisShort.AxisNum = 100;
            this.control_AxisShort.DirNeget = false;
            this.control_AxisShort.LeftLabel = "左←";
            this.control_AxisShort.Location = new System.Drawing.Point(196, -5);
            this.control_AxisShort.Name = "control_AxisShort";
            this.control_AxisShort.RightLabel = "右→";
            this.control_AxisShort.Size = new System.Drawing.Size(305, 39);
            this.control_AxisShort.TabIndex = 499;
            // 
            // textBox_Vel
            // 
            this.textBox_Vel.Location = new System.Drawing.Point(574, 5);
            this.textBox_Vel.Name = "textBox_Vel";
            this.textBox_Vel.Size = new System.Drawing.Size(42, 21);
            this.textBox_Vel.TabIndex = 501;
            this.textBox_Vel.Text = "100";
            // 
            // textBox_Acc
            // 
            this.textBox_Acc.Location = new System.Drawing.Point(617, 5);
            this.textBox_Acc.Name = "textBox_Acc";
            this.textBox_Acc.Size = new System.Drawing.Size(42, 21);
            this.textBox_Acc.TabIndex = 502;
            this.textBox_Acc.Text = "1000";
            // 
            // Control_Axis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBox_Acc);
            this.Controls.Add(this.textBox_Vel);
            this.Controls.Add(this.Label_AxisName);
            this.Controls.Add(this.control_Thread_Reset);
            this.Controls.Add(this.Button_Reset);
            this.Controls.Add(this.Label_IsReseted);
            this.Controls.Add(this.Label_ServoOn);
            this.Controls.Add(this.Label_MotionStatus);
            this.Controls.Add(this.Label_Alert);
            this.Controls.Add(this.Label_PlusLimit);
            this.Controls.Add(this.Label_NegtLimit);
            this.Controls.Add(this.CheckBox_ServoOn);
            this.Controls.Add(this.control_AxisShort);
            this.Name = "Control_Axis";
            this.Size = new System.Drawing.Size(797, 34);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button Button_Reset;
        internal System.Windows.Forms.Label Label_IsReseted;
        internal System.Windows.Forms.Label Label_ServoOn;
        internal System.Windows.Forms.Label Label_MotionStatus;
        internal System.Windows.Forms.Label Label_Alert;
        internal System.Windows.Forms.Label Label_PlusLimit;
        internal System.Windows.Forms.Label Label_NegtLimit;
        internal System.Windows.Forms.CheckBox CheckBox_ServoOn;
        public ClassLib_StdThread.Control_Thread control_Thread_Reset;
        private Control_AxisShort control_AxisShort;
        internal System.Windows.Forms.Label Label_AxisName;
        private System.Windows.Forms.TextBox textBox_Vel;
        private System.Windows.Forms.TextBox textBox_Acc;
    }
}

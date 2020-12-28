namespace ClassLib_MotionUI
{
    partial class SpeedControlCtrl
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.labRunSpeed = new System.Windows.Forms.Label();
            this.labDebugSpeed = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.trackRunSpeed = new System.Windows.Forms.TrackBar();
            this.trackDebugSpeed = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackRunSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackDebugSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // labRunSpeed
            // 
            this.labRunSpeed.AutoSize = true;
            this.labRunSpeed.Location = new System.Drawing.Point(3, 0);
            this.labRunSpeed.Name = "labRunSpeed";
            this.labRunSpeed.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.labRunSpeed.Size = new System.Drawing.Size(69, 22);
            this.labRunSpeed.TabIndex = 0;
            this.labRunSpeed.Text = "运行速度:";
            // 
            // labDebugSpeed
            // 
            this.labDebugSpeed.AutoSize = true;
            this.labDebugSpeed.Location = new System.Drawing.Point(3, 51);
            this.labDebugSpeed.Name = "labDebugSpeed";
            this.labDebugSpeed.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.labDebugSpeed.Size = new System.Drawing.Size(69, 22);
            this.labDebugSpeed.TabIndex = 2;
            this.labDebugSpeed.Text = "调试速度:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 224F));
            this.tableLayoutPanel1.Controls.Add(this.trackRunSpeed, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labRunSpeed, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.trackDebugSpeed, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.labDebugSpeed, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(184, 107);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // trackRunSpeed
            // 
            this.trackRunSpeed.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::ClassLib_MotionUI.Properties.Settings.Default, "RunSpeed", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.trackRunSpeed.Location = new System.Drawing.Point(79, 3);
            this.trackRunSpeed.Maximum = 100;
            this.trackRunSpeed.Minimum = 1;
            this.trackRunSpeed.Name = "trackRunSpeed";
            this.trackRunSpeed.Size = new System.Drawing.Size(104, 45);
            this.trackRunSpeed.TabIndex = 1;
            this.trackRunSpeed.Value = 20;
            // 
            // trackDebugSpeed
            // 
            this.trackDebugSpeed.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::ClassLib_MotionUI.Properties.Settings.Default, "DebugSpeed", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.trackDebugSpeed.Location = new System.Drawing.Point(79, 54);
            this.trackDebugSpeed.Maximum = 100;
            this.trackDebugSpeed.Minimum = 1;
            this.trackDebugSpeed.Name = "trackDebugSpeed";
            this.trackDebugSpeed.Size = new System.Drawing.Size(104, 45);
            this.trackDebugSpeed.TabIndex = 3;
            this.trackDebugSpeed.Value = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ClassLib_MotionUI.Properties.Settings.Default, "DebugSpeed", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.label2.Location = new System.Drawing.Point(188, 53);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.label2.Size = new System.Drawing.Size(27, 22);
            this.label2.TabIndex = 7;
            this.label2.Text = global::ClassLib_MotionUI.Properties.Settings.Default.DebugSpeed;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ClassLib_MotionUI.Properties.Settings.Default, "RunSpeed", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.label1.Location = new System.Drawing.Point(188, 6);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.label1.Size = new System.Drawing.Size(27, 22);
            this.label1.TabIndex = 6;
            this.label1.Text = global::ClassLib_MotionUI.Properties.Settings.Default.RunSpeed;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(204, 53);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.label3.Size = new System.Drawing.Size(21, 22);
            this.label3.TabIndex = 9;
            this.label3.Text = "%";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(204, 6);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.label4.Size = new System.Drawing.Size(21, 22);
            this.label4.TabIndex = 8;
            this.label4.Text = "%";
            // 
            // SpeedControlCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Name = "SpeedControlCtrl";
            this.Size = new System.Drawing.Size(232, 109);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackRunSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackDebugSpeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labRunSpeed;
        private System.Windows.Forms.TrackBar trackDebugSpeed;
        private System.Windows.Forms.Label labDebugSpeed;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TrackBar trackRunSpeed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

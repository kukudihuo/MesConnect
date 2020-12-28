namespace ClassLib_MotionUI
{
    partial class LightControlCtrl
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
            this.labRingA = new System.Windows.Forms.Label();
            this.labRingB = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.trackBarRingD = new System.Windows.Forms.TrackBar();
            this.labelRingD = new System.Windows.Forms.Label();
            this.trackBarRingA = new System.Windows.Forms.TrackBar();
            this.trackBarRingC = new System.Windows.Forms.TrackBar();
            this.labelRingC = new System.Windows.Forms.Label();
            this.trackBarRingB = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.trackBarHighLight = new System.Windows.Forms.TrackBar();
            this.trackBarCoaxial = new System.Windows.Forms.TrackBar();
            this.labelCoaxial = new System.Windows.Forms.Label();
            this.trackBarBack = new System.Windows.Forms.TrackBar();
            this.labelBack = new System.Windows.Forms.Label();
            this.label0Ring = new System.Windows.Forms.Label();
            this.trackBar0Ring = new System.Windows.Forms.TrackBar();
            this.labelHighRing = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRingD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRingA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRingC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRingB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarHighLight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCoaxial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar0Ring)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labRingA
            // 
            this.labRingA.AutoSize = true;
            this.labRingA.Location = new System.Drawing.Point(3, 0);
            this.labRingA.Name = "labRingA";
            this.labRingA.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.labRingA.Size = new System.Drawing.Size(75, 22);
            this.labRingA.TabIndex = 0;
            this.labRingA.Text = "环形光A区:";
            // 
            // labRingB
            // 
            this.labRingB.AutoSize = true;
            this.labRingB.Location = new System.Drawing.Point(3, 51);
            this.labRingB.Name = "labRingB";
            this.labRingB.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.labRingB.Size = new System.Drawing.Size(75, 22);
            this.labRingB.TabIndex = 2;
            this.labRingB.Text = "环形光B区:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.Controls.Add(this.trackBarRingD, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelRingD, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.trackBarRingA, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.trackBarRingC, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.labRingA, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelRingC, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.trackBarRingB, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.labRingB, 0, 1);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(210, 210);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // trackBarRingD
            // 
            this.trackBarRingD.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::ClassLib_MotionUI.Properties.Settings.Default, "RingD", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.trackBarRingD.Location = new System.Drawing.Point(103, 156);
            this.trackBarRingD.Maximum = 100;
            this.trackBarRingD.Name = "trackBarRingD";
            this.trackBarRingD.Size = new System.Drawing.Size(104, 45);
            this.trackBarRingD.TabIndex = 7;
            this.trackBarRingD.ValueChanged += new System.EventHandler(this.trackBarRingD_ValueChanged);
            // 
            // labelRingD
            // 
            this.labelRingD.AutoSize = true;
            this.labelRingD.Location = new System.Drawing.Point(3, 153);
            this.labelRingD.Name = "labelRingD";
            this.labelRingD.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.labelRingD.Size = new System.Drawing.Size(75, 22);
            this.labelRingD.TabIndex = 6;
            this.labelRingD.Text = "环形光D区:";
            // 
            // trackBarRingA
            // 
            this.trackBarRingA.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::ClassLib_MotionUI.Properties.Settings.Default, "RingA", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.trackBarRingA.Location = new System.Drawing.Point(103, 3);
            this.trackBarRingA.Maximum = 100;
            this.trackBarRingA.Name = "trackBarRingA";
            this.trackBarRingA.Size = new System.Drawing.Size(104, 45);
            this.trackBarRingA.TabIndex = 1;
            this.trackBarRingA.Value = global::ClassLib_MotionUI.Properties.Settings.Default.aaa;
            this.trackBarRingA.ValueChanged += new System.EventHandler(this.trackBarRingA_ValueChanged);
            // 
            // trackBarRingC
            // 
            this.trackBarRingC.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::ClassLib_MotionUI.Properties.Settings.Default, "RingC", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.trackBarRingC.Location = new System.Drawing.Point(103, 105);
            this.trackBarRingC.Maximum = 100;
            this.trackBarRingC.Name = "trackBarRingC";
            this.trackBarRingC.Size = new System.Drawing.Size(104, 45);
            this.trackBarRingC.TabIndex = 5;
            this.trackBarRingC.ValueChanged += new System.EventHandler(this.trackBarRingC_ValueChanged);
            // 
            // labelRingC
            // 
            this.labelRingC.AutoSize = true;
            this.labelRingC.Location = new System.Drawing.Point(3, 102);
            this.labelRingC.Name = "labelRingC";
            this.labelRingC.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.labelRingC.Size = new System.Drawing.Size(75, 22);
            this.labelRingC.TabIndex = 4;
            this.labelRingC.Text = "环形光C区:";
            // 
            // trackBarRingB
            // 
            this.trackBarRingB.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::ClassLib_MotionUI.Properties.Settings.Default, "RingB", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.trackBarRingB.Location = new System.Drawing.Point(103, 54);
            this.trackBarRingB.Maximum = 100;
            this.trackBarRingB.Name = "trackBarRingB";
            this.trackBarRingB.Size = new System.Drawing.Size(104, 45);
            this.trackBarRingB.TabIndex = 3;
            this.trackBarRingB.Value = global::ClassLib_MotionUI.Properties.Settings.Default.aaa;
            this.trackBarRingB.ValueChanged += new System.EventHandler(this.trackBarRingB_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ClassLib_MotionUI.Properties.Settings.Default, "RingB", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.label2.Location = new System.Drawing.Point(214, 53);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.label2.Size = new System.Drawing.Size(21, 22);
            this.label2.TabIndex = 7;
            this.label2.Text = global::ClassLib_MotionUI.Properties.Settings.Default.RingB;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ClassLib_MotionUI.Properties.Settings.Default, "RingA", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.label1.Location = new System.Drawing.Point(214, 6);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.label1.Size = new System.Drawing.Size(21, 22);
            this.label1.TabIndex = 6;
            this.label1.Text = global::ClassLib_MotionUI.Properties.Settings.Default.RingA;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ClassLib_MotionUI.Properties.Settings.Default, "RingC", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.label3.Location = new System.Drawing.Point(214, 108);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.label3.Size = new System.Drawing.Size(21, 22);
            this.label3.TabIndex = 8;
            this.label3.Text = global::ClassLib_MotionUI.Properties.Settings.Default.RingC;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ClassLib_MotionUI.Properties.Settings.Default, "RingD", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.label4.Location = new System.Drawing.Point(214, 159);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.label4.Size = new System.Drawing.Size(21, 22);
            this.label4.TabIndex = 9;
            this.label4.Text = global::ClassLib_MotionUI.Properties.Settings.Default.RingD;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ClassLib_MotionUI.Properties.Settings.Default, "Ring0", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.label8.Location = new System.Drawing.Point(456, 6);
            this.label8.Name = "label8";
            this.label8.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.label8.Size = new System.Drawing.Size(21, 22);
            this.label8.TabIndex = 10;
            this.label8.Text = global::ClassLib_MotionUI.Properties.Settings.Default.Ring0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ClassLib_MotionUI.Properties.Settings.Default, "Back", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.label7.Location = new System.Drawing.Point(456, 53);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.label7.Size = new System.Drawing.Size(21, 22);
            this.label7.TabIndex = 11;
            this.label7.Text = global::ClassLib_MotionUI.Properties.Settings.Default.Back;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ClassLib_MotionUI.Properties.Settings.Default, "Coaxial", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.label6.Location = new System.Drawing.Point(456, 108);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.label6.Size = new System.Drawing.Size(21, 22);
            this.label6.TabIndex = 12;
            this.label6.Text = global::ClassLib_MotionUI.Properties.Settings.Default.Coaxial;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ClassLib_MotionUI.Properties.Settings.Default, "HighLight", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.label5.Location = new System.Drawing.Point(456, 155);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.label5.Size = new System.Drawing.Size(21, 22);
            this.label5.TabIndex = 13;
            this.label5.Text = global::ClassLib_MotionUI.Properties.Settings.Default.HighLight;
            // 
            // trackBarHighLight
            // 
            this.trackBarHighLight.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::ClassLib_MotionUI.Properties.Settings.Default, "HighLight", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.trackBarHighLight.Location = new System.Drawing.Point(103, 156);
            this.trackBarHighLight.Maximum = 100;
            this.trackBarHighLight.Name = "trackBarHighLight";
            this.trackBarHighLight.Size = new System.Drawing.Size(104, 45);
            this.trackBarHighLight.TabIndex = 15;
            // 
            // trackBarCoaxial
            // 
            this.trackBarCoaxial.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::ClassLib_MotionUI.Properties.Settings.Default, "Coaxial", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.trackBarCoaxial.Location = new System.Drawing.Point(103, 105);
            this.trackBarCoaxial.Maximum = 100;
            this.trackBarCoaxial.Name = "trackBarCoaxial";
            this.trackBarCoaxial.Size = new System.Drawing.Size(104, 45);
            this.trackBarCoaxial.TabIndex = 13;
            // 
            // labelCoaxial
            // 
            this.labelCoaxial.AutoSize = true;
            this.labelCoaxial.Location = new System.Drawing.Point(3, 102);
            this.labelCoaxial.Name = "labelCoaxial";
            this.labelCoaxial.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.labelCoaxial.Size = new System.Drawing.Size(57, 22);
            this.labelCoaxial.TabIndex = 12;
            this.labelCoaxial.Text = "同轴光:";
            // 
            // trackBarBack
            // 
            this.trackBarBack.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::ClassLib_MotionUI.Properties.Settings.Default, "Back", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.trackBarBack.Location = new System.Drawing.Point(103, 54);
            this.trackBarBack.Maximum = 100;
            this.trackBarBack.Name = "trackBarBack";
            this.trackBarBack.Size = new System.Drawing.Size(104, 45);
            this.trackBarBack.TabIndex = 11;
            // 
            // labelBack
            // 
            this.labelBack.AutoSize = true;
            this.labelBack.Location = new System.Drawing.Point(3, 51);
            this.labelBack.Name = "labelBack";
            this.labelBack.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.labelBack.Size = new System.Drawing.Size(45, 22);
            this.labelBack.TabIndex = 10;
            this.labelBack.Text = "底光:";
            // 
            // label0Ring
            // 
            this.label0Ring.AutoSize = true;
            this.label0Ring.Location = new System.Drawing.Point(3, 0);
            this.label0Ring.Name = "label0Ring";
            this.label0Ring.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.label0Ring.Size = new System.Drawing.Size(69, 22);
            this.label0Ring.TabIndex = 8;
            this.label0Ring.Text = "零角度光:";
            // 
            // trackBar0Ring
            // 
            this.trackBar0Ring.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::ClassLib_MotionUI.Properties.Settings.Default, "Ring0", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.trackBar0Ring.Location = new System.Drawing.Point(103, 3);
            this.trackBar0Ring.Maximum = 100;
            this.trackBar0Ring.Name = "trackBar0Ring";
            this.trackBar0Ring.Size = new System.Drawing.Size(104, 45);
            this.trackBar0Ring.TabIndex = 9;
            // 
            // labelHighRing
            // 
            this.labelHighRing.AutoSize = true;
            this.labelHighRing.Location = new System.Drawing.Point(3, 153);
            this.labelHighRing.Name = "labelHighRing";
            this.labelHighRing.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.labelHighRing.Size = new System.Drawing.Size(69, 22);
            this.labelHighRing.TabIndex = 14;
            this.labelHighRing.Text = "高角度光:";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel2.Controls.Add(this.labelHighRing, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.trackBar0Ring, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.label0Ring, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.labelBack, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.trackBarBack, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.labelCoaxial, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.trackBarCoaxial, 1, 6);
            this.tableLayoutPanel2.Controls.Add(this.trackBarHighLight, 1, 7);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(243, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 8;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(211, 211);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // LightControlCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "LightControlCtrl";
            this.Size = new System.Drawing.Size(485, 215);
            this.Leave += new System.EventHandler(this.LightControlCtrl_Leave);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRingD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRingA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRingC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRingB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarHighLight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCoaxial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar0Ring)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labRingA;
        private System.Windows.Forms.TrackBar trackBarRingB;
        private System.Windows.Forms.Label labRingB;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TrackBar trackBarRingA;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar trackBarRingD;
        private System.Windows.Forms.Label labelRingD;
        private System.Windows.Forms.TrackBar trackBarRingC;
        private System.Windows.Forms.Label labelRingC;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar trackBarHighLight;
        private System.Windows.Forms.TrackBar trackBarCoaxial;
        private System.Windows.Forms.Label labelCoaxial;
        private System.Windows.Forms.TrackBar trackBarBack;
        private System.Windows.Forms.Label labelBack;
        private System.Windows.Forms.Label label0Ring;
        private System.Windows.Forms.TrackBar trackBar0Ring;
        private System.Windows.Forms.Label labelHighRing;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}

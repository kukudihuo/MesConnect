namespace MotionEdit
{
    partial class MotionEdit
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MotionEdit));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.motionParaEdit = new ClassLib_MotionEdit.MotionEdit();
            this.scDI = new System.Windows.Forms.SplitContainer();
            this.tabMain = new CCWin.SkinControl.SkinTabControl();
            this.tabPageMotion = new CCWin.SkinControl.SkinTabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtReadFileName = new System.Windows.Forms.Label();
            this.txtCodeName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbLoadMotion = new CCWin.SkinControl.SkinCheckBox();
            this.btnRefreshView = new CCWin.SkinControl.SkinButton();
            this.tabPageSetting = new CCWin.SkinControl.SkinTabPage();
            this.tabPageHelp = new CCWin.SkinControl.SkinTabPage();
            this.tabControlHelp = new CCWin.SkinControl.SkinTabControl();
            this.tabPageHelpExplain = new CCWin.SkinControl.SkinTabPage();
            this.dgvHelpParameter = new CCWin.SkinControl.SkinDataGridView();
            this.tabPageHelpNote = new CCWin.SkinControl.SkinTabPage();
            this.dgvHelpDebug = new CCWin.SkinControl.SkinDataGridView();
            this.timerUI = new System.Windows.Forms.Timer(this.components);
            this.mdvIOView = new ClassLib_MotionView.MotionIOView();
            this.speedControlCtrl1 = new ClassLib_MotionUI.SpeedControlCtrl();
            this.mdvAxisView = new ClassLib_MotionView.MotionAxisView();
            ((System.ComponentModel.ISupportInitialize)(this.scDI)).BeginInit();
            this.scDI.Panel1.SuspendLayout();
            this.scDI.Panel2.SuspendLayout();
            this.scDI.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabPageMotion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabPageSetting.SuspendLayout();
            this.tabPageHelp.SuspendLayout();
            this.tabControlHelp.SuspendLayout();
            this.tabPageHelpExplain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHelpParameter)).BeginInit();
            this.tabPageHelpNote.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHelpDebug)).BeginInit();
            this.SuspendLayout();
            // 
            // motionParaEdit
            // 
            this.motionParaEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.motionParaEdit.Location = new System.Drawing.Point(0, 0);
            this.motionParaEdit.Name = "motionParaEdit";
            this.motionParaEdit.Size = new System.Drawing.Size(1249, 748);
            this.motionParaEdit.TabIndex = 0;
            // 
            // scDI
            // 
            this.scDI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scDI.Location = new System.Drawing.Point(0, 0);
            this.scDI.Name = "scDI";
            // 
            // scDI.Panel1
            // 
            this.scDI.Panel1.Controls.Add(this.mdvIOView);
            // 
            // scDI.Panel2
            // 
            this.scDI.Panel2.Controls.Add(this.speedControlCtrl1);
            this.scDI.Panel2.Controls.Add(this.mdvAxisView);
            this.scDI.Size = new System.Drawing.Size(1249, 703);
            this.scDI.SplitterDistance = 509;
            this.scDI.TabIndex = 1;
            // 
            // tabMain
            // 
            this.tabMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabMain.AnimatorType = CCWin.SkinControl.AnimationType.HorizSlide;
            this.tabMain.CloseRect = new System.Drawing.Rectangle(2, 2, 12, 12);
            this.tabMain.Controls.Add(this.tabPageMotion);
            this.tabMain.Controls.Add(this.tabPageSetting);
            this.tabMain.Controls.Add(this.tabPageHelp);
            this.tabMain.HeadBack = null;
            this.tabMain.ImgTxtOffset = new System.Drawing.Point(0, 0);
            this.tabMain.ItemSize = new System.Drawing.Size(70, 36);
            this.tabMain.Location = new System.Drawing.Point(7, 2);
            this.tabMain.Name = "tabMain";
            this.tabMain.PageArrowDown = ((System.Drawing.Image)(resources.GetObject("tabMain.PageArrowDown")));
            this.tabMain.PageArrowHover = ((System.Drawing.Image)(resources.GetObject("tabMain.PageArrowHover")));
            this.tabMain.PageCloseHover = ((System.Drawing.Image)(resources.GetObject("tabMain.PageCloseHover")));
            this.tabMain.PageCloseNormal = ((System.Drawing.Image)(resources.GetObject("tabMain.PageCloseNormal")));
            this.tabMain.PageDown = ((System.Drawing.Image)(resources.GetObject("tabMain.PageDown")));
            this.tabMain.PageHover = ((System.Drawing.Image)(resources.GetObject("tabMain.PageHover")));
            this.tabMain.PageImagePosition = CCWin.SkinControl.SkinTabControl.ePageImagePosition.Left;
            this.tabMain.PageNorml = null;
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(1249, 784);
            this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabMain.TabIndex = 1;
            // 
            // tabPageMotion
            // 
            this.tabPageMotion.BackColor = System.Drawing.Color.White;
            this.tabPageMotion.Controls.Add(this.splitContainer1);
            this.tabPageMotion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPageMotion.Location = new System.Drawing.Point(0, 36);
            this.tabPageMotion.Name = "tabPageMotion";
            this.tabPageMotion.Size = new System.Drawing.Size(1249, 748);
            this.tabPageMotion.TabIndex = 0;
            this.tabPageMotion.TabItemImage = null;
            this.tabPageMotion.Text = "运动监控";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.splitContainer1.Panel1.Controls.Add(this.txtReadFileName);
            this.splitContainer1.Panel1.Controls.Add(this.txtCodeName);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.cbLoadMotion);
            this.splitContainer1.Panel1.Controls.Add(this.btnRefreshView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.scDI);
            this.splitContainer1.Size = new System.Drawing.Size(1249, 748);
            this.splitContainer1.SplitterDistance = 41;
            this.splitContainer1.TabIndex = 2;
            // 
            // txtReadFileName
            // 
            this.txtReadFileName.AutoSize = true;
            this.txtReadFileName.Location = new System.Drawing.Point(430, 14);
            this.txtReadFileName.Name = "txtReadFileName";
            this.txtReadFileName.Size = new System.Drawing.Size(101, 12);
            this.txtReadFileName.TabIndex = 7;
            this.txtReadFileName.Text = "读取卡参数的目录";
            // 
            // txtCodeName
            // 
            this.txtCodeName.Location = new System.Drawing.Point(314, 9);
            this.txtCodeName.Name = "txtCodeName";
            this.txtCodeName.Size = new System.Drawing.Size(100, 21);
            this.txtCodeName.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(251, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "设备名称：";
            // 
            // cbLoadMotion
            // 
            this.cbLoadMotion.AutoSize = true;
            this.cbLoadMotion.BackColor = System.Drawing.Color.Transparent;
            this.cbLoadMotion.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.cbLoadMotion.DownBack = null;
            this.cbLoadMotion.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbLoadMotion.Location = new System.Drawing.Point(134, 10);
            this.cbLoadMotion.MouseBack = null;
            this.cbLoadMotion.Name = "cbLoadMotion";
            this.cbLoadMotion.NormlBack = null;
            this.cbLoadMotion.SelectedDownBack = null;
            this.cbLoadMotion.SelectedMouseBack = null;
            this.cbLoadMotion.SelectedNormlBack = null;
            this.cbLoadMotion.Size = new System.Drawing.Size(111, 21);
            this.cbLoadMotion.TabIndex = 4;
            this.cbLoadMotion.Text = "加载运动控制卡";
            this.cbLoadMotion.UseVisualStyleBackColor = false;
            // 
            // btnRefreshView
            // 
            this.btnRefreshView.BackColor = System.Drawing.Color.Transparent;
            this.btnRefreshView.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnRefreshView.DownBack = null;
            this.btnRefreshView.IsDrawGlass = false;
            this.btnRefreshView.Location = new System.Drawing.Point(3, 3);
            this.btnRefreshView.MouseBack = null;
            this.btnRefreshView.Name = "btnRefreshView";
            this.btnRefreshView.NormlBack = null;
            this.btnRefreshView.Size = new System.Drawing.Size(125, 35);
            this.btnRefreshView.TabIndex = 3;
            this.btnRefreshView.Text = "刷新运动监控界面";
            this.btnRefreshView.UseVisualStyleBackColor = false;
            this.btnRefreshView.Click += new System.EventHandler(this.btnRefreshView_Click);
            // 
            // tabPageSetting
            // 
            this.tabPageSetting.BackColor = System.Drawing.Color.White;
            this.tabPageSetting.Controls.Add(this.motionParaEdit);
            this.tabPageSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPageSetting.Location = new System.Drawing.Point(0, 36);
            this.tabPageSetting.Name = "tabPageSetting";
            this.tabPageSetting.Size = new System.Drawing.Size(1249, 748);
            this.tabPageSetting.TabIndex = 1;
            this.tabPageSetting.TabItemImage = null;
            this.tabPageSetting.Text = "数据编辑";
            // 
            // tabPageHelp
            // 
            this.tabPageHelp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(208)))), ((int)(((byte)(255)))));
            this.tabPageHelp.Controls.Add(this.tabControlHelp);
            this.tabPageHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPageHelp.Location = new System.Drawing.Point(0, 36);
            this.tabPageHelp.Name = "tabPageHelp";
            this.tabPageHelp.Size = new System.Drawing.Size(1249, 748);
            this.tabPageHelp.TabIndex = 2;
            this.tabPageHelp.TabItemImage = null;
            this.tabPageHelp.Text = "帮助";
            // 
            // tabControlHelp
            // 
            this.tabControlHelp.AnimatorType = CCWin.SkinControl.AnimationType.HorizSlide;
            this.tabControlHelp.CloseRect = new System.Drawing.Rectangle(2, 2, 12, 12);
            this.tabControlHelp.Controls.Add(this.tabPageHelpExplain);
            this.tabControlHelp.Controls.Add(this.tabPageHelpNote);
            this.tabControlHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlHelp.HeadBack = null;
            this.tabControlHelp.ImgTxtOffset = new System.Drawing.Point(0, 0);
            this.tabControlHelp.ItemSize = new System.Drawing.Size(70, 36);
            this.tabControlHelp.Location = new System.Drawing.Point(0, 0);
            this.tabControlHelp.Name = "tabControlHelp";
            this.tabControlHelp.PageArrowDown = ((System.Drawing.Image)(resources.GetObject("tabControlHelp.PageArrowDown")));
            this.tabControlHelp.PageArrowHover = ((System.Drawing.Image)(resources.GetObject("tabControlHelp.PageArrowHover")));
            this.tabControlHelp.PageCloseHover = ((System.Drawing.Image)(resources.GetObject("tabControlHelp.PageCloseHover")));
            this.tabControlHelp.PageCloseNormal = ((System.Drawing.Image)(resources.GetObject("tabControlHelp.PageCloseNormal")));
            this.tabControlHelp.PageDown = ((System.Drawing.Image)(resources.GetObject("tabControlHelp.PageDown")));
            this.tabControlHelp.PageHover = ((System.Drawing.Image)(resources.GetObject("tabControlHelp.PageHover")));
            this.tabControlHelp.PageImagePosition = CCWin.SkinControl.SkinTabControl.ePageImagePosition.Left;
            this.tabControlHelp.PageNorml = null;
            this.tabControlHelp.SelectedIndex = 1;
            this.tabControlHelp.Size = new System.Drawing.Size(1249, 748);
            this.tabControlHelp.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControlHelp.TabIndex = 0;
            // 
            // tabPageHelpExplain
            // 
            this.tabPageHelpExplain.BackColor = System.Drawing.Color.White;
            this.tabPageHelpExplain.Controls.Add(this.dgvHelpParameter);
            this.tabPageHelpExplain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPageHelpExplain.Location = new System.Drawing.Point(0, 36);
            this.tabPageHelpExplain.Name = "tabPageHelpExplain";
            this.tabPageHelpExplain.Size = new System.Drawing.Size(1249, 712);
            this.tabPageHelpExplain.TabIndex = 0;
            this.tabPageHelpExplain.TabItemImage = null;
            this.tabPageHelpExplain.Text = "参数说明";
            // 
            // dgvHelpParameter
            // 
            this.dgvHelpParameter.AllowUserToAddRows = false;
            this.dgvHelpParameter.AllowUserToResizeColumns = false;
            this.dgvHelpParameter.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(246)))), ((int)(((byte)(253)))));
            this.dgvHelpParameter.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvHelpParameter.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvHelpParameter.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvHelpParameter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvHelpParameter.ColumnFont = null;
            this.dgvHelpParameter.ColumnForeColor = System.Drawing.SystemColors.MenuText;
            this.dgvHelpParameter.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(246)))), ((int)(((byte)(239)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHelpParameter.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvHelpParameter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHelpParameter.ColumnSelectBackColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvHelpParameter.ColumnSelectForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(188)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHelpParameter.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvHelpParameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHelpParameter.EnableHeadersVisualStyles = false;
            this.dgvHelpParameter.GridColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dgvHelpParameter.HeadFont = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgvHelpParameter.HeadSelectForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvHelpParameter.Location = new System.Drawing.Point(0, 0);
            this.dgvHelpParameter.MultiSelect = false;
            this.dgvHelpParameter.Name = "dgvHelpParameter";
            this.dgvHelpParameter.ReadOnly = true;
            this.dgvHelpParameter.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvHelpParameter.RowHeadersVisible = false;
            this.dgvHelpParameter.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            this.dgvHelpParameter.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvHelpParameter.RowTemplate.Height = 23;
            this.dgvHelpParameter.Size = new System.Drawing.Size(1249, 712);
            this.dgvHelpParameter.TabIndex = 2;
            this.dgvHelpParameter.TitleBack = null;
            this.dgvHelpParameter.TitleBackColorBegin = System.Drawing.Color.White;
            this.dgvHelpParameter.TitleBackColorEnd = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(242)))));
            // 
            // tabPageHelpNote
            // 
            this.tabPageHelpNote.BackColor = System.Drawing.Color.White;
            this.tabPageHelpNote.Controls.Add(this.dgvHelpDebug);
            this.tabPageHelpNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPageHelpNote.Location = new System.Drawing.Point(0, 36);
            this.tabPageHelpNote.Name = "tabPageHelpNote";
            this.tabPageHelpNote.Size = new System.Drawing.Size(1249, 712);
            this.tabPageHelpNote.TabIndex = 1;
            this.tabPageHelpNote.TabItemImage = null;
            this.tabPageHelpNote.Text = "调试事项";
            // 
            // dgvHelpDebug
            // 
            this.dgvHelpDebug.AllowUserToAddRows = false;
            this.dgvHelpDebug.AllowUserToResizeColumns = false;
            this.dgvHelpDebug.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(246)))), ((int)(((byte)(253)))));
            this.dgvHelpDebug.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvHelpDebug.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvHelpDebug.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvHelpDebug.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvHelpDebug.ColumnFont = null;
            this.dgvHelpDebug.ColumnForeColor = System.Drawing.SystemColors.MenuText;
            this.dgvHelpDebug.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(246)))), ((int)(((byte)(239)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHelpDebug.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvHelpDebug.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHelpDebug.ColumnSelectBackColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvHelpDebug.ColumnSelectForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(188)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHelpDebug.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvHelpDebug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHelpDebug.EnableHeadersVisualStyles = false;
            this.dgvHelpDebug.GridColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dgvHelpDebug.HeadFont = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgvHelpDebug.HeadSelectForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvHelpDebug.Location = new System.Drawing.Point(0, 0);
            this.dgvHelpDebug.MultiSelect = false;
            this.dgvHelpDebug.Name = "dgvHelpDebug";
            this.dgvHelpDebug.ReadOnly = true;
            this.dgvHelpDebug.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvHelpDebug.RowHeadersVisible = false;
            this.dgvHelpDebug.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            this.dgvHelpDebug.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvHelpDebug.RowTemplate.Height = 23;
            this.dgvHelpDebug.Size = new System.Drawing.Size(1249, 712);
            this.dgvHelpDebug.TabIndex = 3;
            this.dgvHelpDebug.TitleBack = null;
            this.dgvHelpDebug.TitleBackColorBegin = System.Drawing.Color.White;
            this.dgvHelpDebug.TitleBackColorEnd = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(242)))));
            // 
            // timerUI
            // 
            this.timerUI.Interval = 500;
            this.timerUI.Tick += new System.EventHandler(this.timerUI_Tick);
            // 
            // mdvIOView
            // 
            this.mdvIOView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mdvIOView.Location = new System.Drawing.Point(0, 0);
            this.mdvIOView.Name = "mdvIOView";
            this.mdvIOView.Size = new System.Drawing.Size(509, 703);
            this.mdvIOView.TabIndex = 0;
            // 
            // speedControlCtrl1
            // 
            this.speedControlCtrl1.Location = new System.Drawing.Point(451, 143);
            this.speedControlCtrl1.Name = "speedControlCtrl1";
            this.speedControlCtrl1.Size = new System.Drawing.Size(256, 109);
            this.speedControlCtrl1.TabIndex = 1;
            // 
            // mdvAxisView
            // 
            this.mdvAxisView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mdvAxisView.Location = new System.Drawing.Point(0, 0);
            this.mdvAxisView.Name = "mdvAxisView";
            this.mdvAxisView.Size = new System.Drawing.Size(736, 703);
            this.mdvAxisView.TabIndex = 0;
            // 
            // MotionEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1263, 793);
            this.Controls.Add(this.tabMain);
            this.Name = "MotionEdit";
            this.ShowDrawIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "";
            this.scDI.Panel1.ResumeLayout(false);
            this.scDI.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scDI)).EndInit();
            this.scDI.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tabPageMotion.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabPageSetting.ResumeLayout(false);
            this.tabPageHelp.ResumeLayout(false);
            this.tabControlHelp.ResumeLayout(false);
            this.tabPageHelpExplain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHelpParameter)).EndInit();
            this.tabPageHelpNote.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHelpDebug)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ClassLib_MotionEdit.MotionEdit motionParaEdit;
        private System.Windows.Forms.SplitContainer scDI;
        private ClassLib_MotionView.MotionAxisView mdvAxisView;
        private CCWin.SkinControl.SkinTabControl tabMain;
        private CCWin.SkinControl.SkinTabPage tabPageMotion;
        private CCWin.SkinControl.SkinTabPage tabPageSetting;
        private CCWin.SkinControl.SkinButton btnRefreshView;
        private System.Windows.Forms.Timer timerUI;
        private ClassLib_MotionView.MotionIOView mdvIOView;
        private CCWin.SkinControl.SkinCheckBox cbLoadMotion;
        private CCWin.SkinControl.SkinTabPage tabPageHelp;
        private CCWin.SkinControl.SkinTabControl tabControlHelp;
        private CCWin.SkinControl.SkinTabPage tabPageHelpExplain;
        private CCWin.SkinControl.SkinTabPage tabPageHelpNote;
        private CCWin.SkinControl.SkinDataGridView dgvHelpParameter;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txtCodeName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label txtReadFileName;
        private CCWin.SkinControl.SkinDataGridView dgvHelpDebug;
        private ClassLib_MotionUI.SpeedControlCtrl speedControlCtrl1;
    }
}


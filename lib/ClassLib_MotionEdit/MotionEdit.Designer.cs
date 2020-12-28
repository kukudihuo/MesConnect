namespace ClassLib_MotionEdit
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            this.scCard = new System.Windows.Forms.SplitContainer();
            this.dgvMotionCard = new CCWin.SkinControl.SkinDataGridView();
            this.pBar1 = new System.Windows.Forms.ProgressBar();
            this.scIn = new System.Windows.Forms.SplitContainer();
            this.dgvMotionIn = new CCWin.SkinControl.SkinDataGridView();
            this.scOut = new System.Windows.Forms.SplitContainer();
            this.dgvMotionOut = new CCWin.SkinControl.SkinDataGridView();
            this.scTotal = new System.Windows.Forms.SplitContainer();
            this.dgvMotionTotal = new CCWin.SkinControl.SkinDataGridView();
            this.scEdit = new System.Windows.Forms.SplitContainer();
            this.btnMotionOpen = new CCWin.SkinControl.SkinButton();
            this.scNow = new System.Windows.Forms.SplitContainer();
            this.btnMotionNew = new CCWin.SkinControl.SkinButton();
            this.btnMotionGen = new CCWin.SkinControl.SkinButton();
            this.cmsCard = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsCardDeleteRow = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsIn = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsInDeleteRow = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsOut = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsOutDeleteRow = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsTotal = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsTotalDeleteRow = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.scCard)).BeginInit();
            this.scCard.Panel1.SuspendLayout();
            this.scCard.Panel2.SuspendLayout();
            this.scCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMotionCard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scIn)).BeginInit();
            this.scIn.Panel1.SuspendLayout();
            this.scIn.Panel2.SuspendLayout();
            this.scIn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMotionIn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scOut)).BeginInit();
            this.scOut.Panel1.SuspendLayout();
            this.scOut.Panel2.SuspendLayout();
            this.scOut.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMotionOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scTotal)).BeginInit();
            this.scTotal.Panel1.SuspendLayout();
            this.scTotal.Panel2.SuspendLayout();
            this.scTotal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMotionTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scEdit)).BeginInit();
            this.scEdit.Panel1.SuspendLayout();
            this.scEdit.Panel2.SuspendLayout();
            this.scEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scNow)).BeginInit();
            this.scNow.Panel1.SuspendLayout();
            this.scNow.Panel2.SuspendLayout();
            this.scNow.SuspendLayout();
            this.cmsCard.SuspendLayout();
            this.cmsIn.SuspendLayout();
            this.cmsOut.SuspendLayout();
            this.cmsTotal.SuspendLayout();
            this.SuspendLayout();
            // 
            // scCard
            // 
            this.scCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scCard.Location = new System.Drawing.Point(0, 0);
            this.scCard.Name = "scCard";
            this.scCard.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scCard.Panel1
            // 
            this.scCard.Panel1.Controls.Add(this.dgvMotionCard);
            this.scCard.Panel1.Controls.Add(this.pBar1);
            // 
            // scCard.Panel2
            // 
            this.scCard.Panel2.Controls.Add(this.scIn);
            this.scCard.Size = new System.Drawing.Size(873, 743);
            this.scCard.SplitterDistance = 386;
            this.scCard.TabIndex = 0;
            // 
            // dgvMotionCard
            // 
            this.dgvMotionCard.AllowUserToAddRows = false;
            this.dgvMotionCard.AllowUserToDeleteRows = false;
            this.dgvMotionCard.AllowUserToResizeColumns = false;
            this.dgvMotionCard.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(246)))), ((int)(((byte)(253)))));
            this.dgvMotionCard.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMotionCard.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvMotionCard.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvMotionCard.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvMotionCard.ColumnFont = null;
            this.dgvMotionCard.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(246)))), ((int)(((byte)(239)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMotionCard.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMotionCard.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMotionCard.ColumnSelectForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(188)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMotionCard.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvMotionCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMotionCard.EnableHeadersVisualStyles = false;
            this.dgvMotionCard.GridColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dgvMotionCard.HeadFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgvMotionCard.HeadSelectForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvMotionCard.Location = new System.Drawing.Point(0, 0);
            this.dgvMotionCard.MultiSelect = false;
            this.dgvMotionCard.Name = "dgvMotionCard";
            this.dgvMotionCard.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvMotionCard.RowHeadersVisible = false;
            this.dgvMotionCard.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvMotionCard.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvMotionCard.RowTemplate.Height = 23;
            this.dgvMotionCard.Size = new System.Drawing.Size(873, 358);
            this.dgvMotionCard.TabIndex = 2;
            this.dgvMotionCard.TitleBack = null;
            this.dgvMotionCard.TitleBackColorBegin = System.Drawing.Color.White;
            this.dgvMotionCard.TitleBackColorEnd = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(242)))));
            // 
            // pBar1
            // 
            this.pBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pBar1.Location = new System.Drawing.Point(0, 358);
            this.pBar1.Name = "pBar1";
            this.pBar1.Size = new System.Drawing.Size(873, 28);
            this.pBar1.TabIndex = 1;
            this.pBar1.Visible = false;
            // 
            // scIn
            // 
            this.scIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scIn.Location = new System.Drawing.Point(0, 0);
            this.scIn.Name = "scIn";
            // 
            // scIn.Panel1
            // 
            this.scIn.Panel1.Controls.Add(this.dgvMotionIn);
            // 
            // scIn.Panel2
            // 
            this.scIn.Panel2.Controls.Add(this.scOut);
            this.scIn.Size = new System.Drawing.Size(873, 353);
            this.scIn.SplitterDistance = 343;
            this.scIn.TabIndex = 0;
            // 
            // dgvMotionIn
            // 
            this.dgvMotionIn.AllowUserToAddRows = false;
            this.dgvMotionIn.AllowUserToDeleteRows = false;
            this.dgvMotionIn.AllowUserToResizeColumns = false;
            this.dgvMotionIn.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(246)))), ((int)(((byte)(253)))));
            this.dgvMotionIn.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvMotionIn.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvMotionIn.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvMotionIn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvMotionIn.ColumnFont = null;
            this.dgvMotionIn.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(246)))), ((int)(((byte)(239)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMotionIn.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvMotionIn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMotionIn.ColumnSelectForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(188)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMotionIn.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvMotionIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMotionIn.EnableHeadersVisualStyles = false;
            this.dgvMotionIn.GridColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dgvMotionIn.HeadFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgvMotionIn.HeadSelectForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvMotionIn.Location = new System.Drawing.Point(0, 0);
            this.dgvMotionIn.MultiSelect = false;
            this.dgvMotionIn.Name = "dgvMotionIn";
            this.dgvMotionIn.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvMotionIn.RowHeadersVisible = false;
            this.dgvMotionIn.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvMotionIn.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvMotionIn.RowTemplate.Height = 23;
            this.dgvMotionIn.Size = new System.Drawing.Size(343, 353);
            this.dgvMotionIn.TabIndex = 0;
            this.dgvMotionIn.TitleBack = null;
            this.dgvMotionIn.TitleBackColorBegin = System.Drawing.Color.White;
            this.dgvMotionIn.TitleBackColorEnd = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(242)))));
            // 
            // scOut
            // 
            this.scOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scOut.Location = new System.Drawing.Point(0, 0);
            this.scOut.Name = "scOut";
            // 
            // scOut.Panel1
            // 
            this.scOut.Panel1.Controls.Add(this.dgvMotionOut);
            // 
            // scOut.Panel2
            // 
            this.scOut.Panel2.Controls.Add(this.scTotal);
            this.scOut.Size = new System.Drawing.Size(526, 353);
            this.scOut.SplitterDistance = 299;
            this.scOut.TabIndex = 0;
            // 
            // dgvMotionOut
            // 
            this.dgvMotionOut.AllowUserToAddRows = false;
            this.dgvMotionOut.AllowUserToDeleteRows = false;
            this.dgvMotionOut.AllowUserToResizeColumns = false;
            this.dgvMotionOut.AllowUserToResizeRows = false;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(246)))), ((int)(((byte)(253)))));
            this.dgvMotionOut.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvMotionOut.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvMotionOut.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvMotionOut.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvMotionOut.ColumnFont = null;
            this.dgvMotionOut.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(246)))), ((int)(((byte)(239)))));
            dataGridViewCellStyle10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMotionOut.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvMotionOut.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMotionOut.ColumnSelectForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(188)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMotionOut.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgvMotionOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMotionOut.EnableHeadersVisualStyles = false;
            this.dgvMotionOut.GridColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dgvMotionOut.HeadFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgvMotionOut.HeadSelectForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvMotionOut.Location = new System.Drawing.Point(0, 0);
            this.dgvMotionOut.MultiSelect = false;
            this.dgvMotionOut.Name = "dgvMotionOut";
            this.dgvMotionOut.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvMotionOut.RowHeadersVisible = false;
            this.dgvMotionOut.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvMotionOut.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvMotionOut.RowTemplate.Height = 23;
            this.dgvMotionOut.Size = new System.Drawing.Size(299, 353);
            this.dgvMotionOut.TabIndex = 1;
            this.dgvMotionOut.TitleBack = null;
            this.dgvMotionOut.TitleBackColorBegin = System.Drawing.Color.White;
            this.dgvMotionOut.TitleBackColorEnd = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(242)))));
            // 
            // scTotal
            // 
            this.scTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scTotal.Location = new System.Drawing.Point(0, 0);
            this.scTotal.Name = "scTotal";
            this.scTotal.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scTotal.Panel1
            // 
            this.scTotal.Panel1.Controls.Add(this.dgvMotionTotal);
            // 
            // scTotal.Panel2
            // 
            this.scTotal.Panel2.Controls.Add(this.scEdit);
            this.scTotal.Size = new System.Drawing.Size(223, 353);
            this.scTotal.SplitterDistance = 302;
            this.scTotal.TabIndex = 0;
            // 
            // dgvMotionTotal
            // 
            this.dgvMotionTotal.AllowUserToAddRows = false;
            this.dgvMotionTotal.AllowUserToDeleteRows = false;
            this.dgvMotionTotal.AllowUserToResizeColumns = false;
            this.dgvMotionTotal.AllowUserToResizeRows = false;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(246)))), ((int)(((byte)(253)))));
            this.dgvMotionTotal.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle13;
            this.dgvMotionTotal.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvMotionTotal.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvMotionTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvMotionTotal.ColumnFont = null;
            this.dgvMotionTotal.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(246)))), ((int)(((byte)(239)))));
            dataGridViewCellStyle14.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMotionTotal.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.dgvMotionTotal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMotionTotal.ColumnSelectForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(188)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMotionTotal.DefaultCellStyle = dataGridViewCellStyle15;
            this.dgvMotionTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMotionTotal.EnableHeadersVisualStyles = false;
            this.dgvMotionTotal.GridColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dgvMotionTotal.HeadFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgvMotionTotal.HeadSelectForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvMotionTotal.Location = new System.Drawing.Point(0, 0);
            this.dgvMotionTotal.MultiSelect = false;
            this.dgvMotionTotal.Name = "dgvMotionTotal";
            this.dgvMotionTotal.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvMotionTotal.RowHeadersVisible = false;
            this.dgvMotionTotal.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvMotionTotal.RowsDefaultCellStyle = dataGridViewCellStyle16;
            this.dgvMotionTotal.RowTemplate.Height = 23;
            this.dgvMotionTotal.Size = new System.Drawing.Size(223, 302);
            this.dgvMotionTotal.TabIndex = 2;
            this.dgvMotionTotal.TitleBack = null;
            this.dgvMotionTotal.TitleBackColorBegin = System.Drawing.Color.White;
            this.dgvMotionTotal.TitleBackColorEnd = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(242)))));
            // 
            // scEdit
            // 
            this.scEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scEdit.Location = new System.Drawing.Point(0, 0);
            this.scEdit.Name = "scEdit";
            // 
            // scEdit.Panel1
            // 
            this.scEdit.Panel1.Controls.Add(this.btnMotionOpen);
            // 
            // scEdit.Panel2
            // 
            this.scEdit.Panel2.Controls.Add(this.scNow);
            this.scEdit.Size = new System.Drawing.Size(223, 47);
            this.scEdit.SplitterDistance = 72;
            this.scEdit.TabIndex = 0;
            // 
            // btnMotionOpen
            // 
            this.btnMotionOpen.BackColor = System.Drawing.Color.Transparent;
            this.btnMotionOpen.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnMotionOpen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMotionOpen.DownBack = null;
            this.btnMotionOpen.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMotionOpen.Location = new System.Drawing.Point(0, 0);
            this.btnMotionOpen.MouseBack = null;
            this.btnMotionOpen.Name = "btnMotionOpen";
            this.btnMotionOpen.NormlBack = null;
            this.btnMotionOpen.Size = new System.Drawing.Size(72, 47);
            this.btnMotionOpen.TabIndex = 0;
            this.btnMotionOpen.Text = "打开";
            this.btnMotionOpen.UseVisualStyleBackColor = false;
            this.btnMotionOpen.Click += new System.EventHandler(this.btnMotionOpen_Click);
            // 
            // scNow
            // 
            this.scNow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scNow.Location = new System.Drawing.Point(0, 0);
            this.scNow.Name = "scNow";
            // 
            // scNow.Panel1
            // 
            this.scNow.Panel1.Controls.Add(this.btnMotionNew);
            // 
            // scNow.Panel2
            // 
            this.scNow.Panel2.Controls.Add(this.btnMotionGen);
            this.scNow.Size = new System.Drawing.Size(147, 47);
            this.scNow.SplitterDistance = 69;
            this.scNow.TabIndex = 1;
            // 
            // btnMotionNew
            // 
            this.btnMotionNew.BackColor = System.Drawing.Color.Transparent;
            this.btnMotionNew.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnMotionNew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMotionNew.DownBack = null;
            this.btnMotionNew.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMotionNew.Location = new System.Drawing.Point(0, 0);
            this.btnMotionNew.MouseBack = null;
            this.btnMotionNew.Name = "btnMotionNew";
            this.btnMotionNew.NormlBack = null;
            this.btnMotionNew.Size = new System.Drawing.Size(69, 47);
            this.btnMotionNew.TabIndex = 0;
            this.btnMotionNew.Text = "新增";
            this.btnMotionNew.UseVisualStyleBackColor = false;
            this.btnMotionNew.Click += new System.EventHandler(this.btnMotionNew_Click);
            // 
            // btnMotionGen
            // 
            this.btnMotionGen.BackColor = System.Drawing.Color.Transparent;
            this.btnMotionGen.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnMotionGen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMotionGen.DownBack = null;
            this.btnMotionGen.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMotionGen.Location = new System.Drawing.Point(0, 0);
            this.btnMotionGen.MouseBack = null;
            this.btnMotionGen.Name = "btnMotionGen";
            this.btnMotionGen.NormlBack = null;
            this.btnMotionGen.Size = new System.Drawing.Size(74, 47);
            this.btnMotionGen.TabIndex = 0;
            this.btnMotionGen.Text = "生成";
            this.btnMotionGen.UseVisualStyleBackColor = false;
            this.btnMotionGen.Click += new System.EventHandler(this.btnMotionGen_Click);
            // 
            // cmsCard
            // 
            this.cmsCard.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsCardDeleteRow});
            this.cmsCard.Name = "contextMenuStrip1";
            this.cmsCard.Size = new System.Drawing.Size(113, 26);
            // 
            // cmsCardDeleteRow
            // 
            this.cmsCardDeleteRow.Name = "cmsCardDeleteRow";
            this.cmsCardDeleteRow.Size = new System.Drawing.Size(112, 22);
            this.cmsCardDeleteRow.Text = "删除行";
            this.cmsCardDeleteRow.Click += new System.EventHandler(this.cmsCardDeleteRow_Click);
            // 
            // cmsIn
            // 
            this.cmsIn.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsInDeleteRow});
            this.cmsIn.Name = "contextMenuStrip2";
            this.cmsIn.Size = new System.Drawing.Size(113, 26);
            // 
            // cmsInDeleteRow
            // 
            this.cmsInDeleteRow.Name = "cmsInDeleteRow";
            this.cmsInDeleteRow.Size = new System.Drawing.Size(112, 22);
            this.cmsInDeleteRow.Text = "删除行";
            this.cmsInDeleteRow.Click += new System.EventHandler(this.cmsInDeleteRow_Click);
            // 
            // cmsOut
            // 
            this.cmsOut.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsOutDeleteRow});
            this.cmsOut.Name = "contextMenuStrip3";
            this.cmsOut.Size = new System.Drawing.Size(113, 26);
            // 
            // cmsOutDeleteRow
            // 
            this.cmsOutDeleteRow.Name = "cmsOutDeleteRow";
            this.cmsOutDeleteRow.Size = new System.Drawing.Size(112, 22);
            this.cmsOutDeleteRow.Text = "删除行";
            this.cmsOutDeleteRow.Click += new System.EventHandler(this.cmsOutDeleteRow_Click);
            // 
            // cmsTotal
            // 
            this.cmsTotal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsTotalDeleteRow});
            this.cmsTotal.Name = "contextMenuStrip4";
            this.cmsTotal.Size = new System.Drawing.Size(113, 26);
            // 
            // cmsTotalDeleteRow
            // 
            this.cmsTotalDeleteRow.Name = "cmsTotalDeleteRow";
            this.cmsTotalDeleteRow.Size = new System.Drawing.Size(112, 22);
            this.cmsTotalDeleteRow.Text = "删除行";
            this.cmsTotalDeleteRow.Click += new System.EventHandler(this.cmsTotalDeleteRow_Click);
            // 
            // MotionEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scCard);
            this.Name = "MotionEdit";
            this.Size = new System.Drawing.Size(873, 743);
            this.scCard.Panel1.ResumeLayout(false);
            this.scCard.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scCard)).EndInit();
            this.scCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMotionCard)).EndInit();
            this.scIn.Panel1.ResumeLayout(false);
            this.scIn.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scIn)).EndInit();
            this.scIn.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMotionIn)).EndInit();
            this.scOut.Panel1.ResumeLayout(false);
            this.scOut.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scOut)).EndInit();
            this.scOut.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMotionOut)).EndInit();
            this.scTotal.Panel1.ResumeLayout(false);
            this.scTotal.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scTotal)).EndInit();
            this.scTotal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMotionTotal)).EndInit();
            this.scEdit.Panel1.ResumeLayout(false);
            this.scEdit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scEdit)).EndInit();
            this.scEdit.ResumeLayout(false);
            this.scNow.Panel1.ResumeLayout(false);
            this.scNow.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scNow)).EndInit();
            this.scNow.ResumeLayout(false);
            this.cmsCard.ResumeLayout(false);
            this.cmsIn.ResumeLayout(false);
            this.cmsOut.ResumeLayout(false);
            this.cmsTotal.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scCard;
        private System.Windows.Forms.SplitContainer scIn;
        private System.Windows.Forms.SplitContainer scOut;
        private System.Windows.Forms.SplitContainer scTotal;
        private System.Windows.Forms.SplitContainer scEdit;
        private System.Windows.Forms.ContextMenuStrip cmsCard;
        private System.Windows.Forms.ToolStripMenuItem cmsCardDeleteRow;
        private System.Windows.Forms.ContextMenuStrip cmsIn;
        private System.Windows.Forms.ToolStripMenuItem cmsInDeleteRow;
        private System.Windows.Forms.ContextMenuStrip cmsOut;
        private System.Windows.Forms.ToolStripMenuItem cmsOutDeleteRow;
        private System.Windows.Forms.ContextMenuStrip cmsTotal;
        private System.Windows.Forms.ToolStripMenuItem cmsTotalDeleteRow;
        private System.Windows.Forms.ProgressBar pBar1;
        private CCWin.SkinControl.SkinDataGridView dgvMotionCard;
        private CCWin.SkinControl.SkinDataGridView dgvMotionIn;
        private CCWin.SkinControl.SkinDataGridView dgvMotionOut;
        private CCWin.SkinControl.SkinDataGridView dgvMotionTotal;
        private System.Windows.Forms.SplitContainer scNow;
        private CCWin.SkinControl.SkinButton btnMotionNew;
        private CCWin.SkinControl.SkinButton btnMotionOpen;
        private CCWin.SkinControl.SkinButton btnMotionGen;
    }
}

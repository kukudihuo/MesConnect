namespace ClassLib_MotionEdit
{
    partial class FormNew
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvParameterEditing = new CCWin.SkinControl.SkinDataGridView();
            this.btnNowOK = new CCWin.SkinControl.SkinButton();
            this.btnNewCancel = new CCWin.SkinControl.SkinButton();
            this.colParaName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colParaValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRemarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParameterEditing)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvParameterEditing
            // 
            this.dgvParameterEditing.AllowUserToAddRows = false;
            this.dgvParameterEditing.AllowUserToDeleteRows = false;
            this.dgvParameterEditing.AllowUserToResizeColumns = false;
            this.dgvParameterEditing.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(246)))), ((int)(((byte)(253)))));
            this.dgvParameterEditing.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvParameterEditing.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvParameterEditing.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvParameterEditing.ColumnFont = null;
            this.dgvParameterEditing.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(246)))), ((int)(((byte)(239)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvParameterEditing.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvParameterEditing.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvParameterEditing.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colParaName,
            this.colParaValue,
            this.colRemarks});
            this.dgvParameterEditing.ColumnSelectForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(188)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvParameterEditing.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvParameterEditing.EnableHeadersVisualStyles = false;
            this.dgvParameterEditing.GridColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dgvParameterEditing.HeadFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgvParameterEditing.HeadSelectForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvParameterEditing.Location = new System.Drawing.Point(7, 31);
            this.dgvParameterEditing.MultiSelect = false;
            this.dgvParameterEditing.Name = "dgvParameterEditing";
            this.dgvParameterEditing.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvParameterEditing.RowHeadersVisible = false;
            this.dgvParameterEditing.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvParameterEditing.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvParameterEditing.RowTemplate.Height = 23;
            this.dgvParameterEditing.Size = new System.Drawing.Size(386, 215);
            this.dgvParameterEditing.TabIndex = 0;
            this.dgvParameterEditing.TitleBack = null;
            this.dgvParameterEditing.TitleBackColorBegin = System.Drawing.Color.White;
            this.dgvParameterEditing.TitleBackColorEnd = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(242)))));
            // 
            // btnNowOK
            // 
            this.btnNowOK.BackColor = System.Drawing.Color.Transparent;
            this.btnNowOK.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnNowOK.DownBack = null;
            this.btnNowOK.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNowOK.Location = new System.Drawing.Point(8, 253);
            this.btnNowOK.MouseBack = null;
            this.btnNowOK.Name = "btnNowOK";
            this.btnNowOK.NormlBack = null;
            this.btnNowOK.Size = new System.Drawing.Size(189, 40);
            this.btnNowOK.TabIndex = 1;
            this.btnNowOK.Text = "确定";
            this.btnNowOK.UseVisualStyleBackColor = false;
            this.btnNowOK.Click += new System.EventHandler(this.btnNowOK_Click);
            // 
            // btnNewCancel
            // 
            this.btnNewCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnNewCancel.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnNewCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnNewCancel.DownBack = null;
            this.btnNewCancel.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNewCancel.Location = new System.Drawing.Point(203, 253);
            this.btnNewCancel.MouseBack = null;
            this.btnNewCancel.Name = "btnNewCancel";
            this.btnNewCancel.NormlBack = null;
            this.btnNewCancel.Size = new System.Drawing.Size(190, 40);
            this.btnNewCancel.TabIndex = 2;
            this.btnNewCancel.Text = "关闭";
            this.btnNewCancel.UseVisualStyleBackColor = false;
            this.btnNewCancel.Click += new System.EventHandler(this.btnNewCancel_Click);
            // 
            // colParaName
            // 
            this.colParaName.HeaderText = "参数名称";
            this.colParaName.Name = "colParaName";
            this.colParaName.ReadOnly = true;
            // 
            // colParaValue
            // 
            this.colParaValue.HeaderText = "参数值";
            this.colParaValue.Name = "colParaValue";
            this.colParaValue.Width = 80;
            // 
            // colRemarks
            // 
            this.colRemarks.HeaderText = "备注";
            this.colRemarks.Name = "colRemarks";
            this.colRemarks.Width = 200;
            // 
            // FormNew
            // 
            this.AcceptButton = this.btnNowOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnNewCancel;
            this.CaptionFont = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.ControlBox = false;
            this.Controls.Add(this.btnNewCancel);
            this.Controls.Add(this.btnNowOK);
            this.Controls.Add(this.dgvParameterEditing);
            this.InheritBack = true;
            this.InheritTheme = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormNew";
            this.ShowDrawIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "   增加卡";
            this.TitleCenter = true;
            ((System.ComponentModel.ISupportInitialize)(this.dgvParameterEditing)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CCWin.SkinControl.SkinDataGridView dgvParameterEditing;
        private CCWin.SkinControl.SkinButton btnNowOK;
        private CCWin.SkinControl.SkinButton btnNewCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colParaName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colParaValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRemarks;
    }
}
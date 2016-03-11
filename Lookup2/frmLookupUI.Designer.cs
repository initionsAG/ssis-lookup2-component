namespace Lookup2
{
    partial class frmLookupUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLookupUI));
            this.label11 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cbIsInclusiveUpperBound = new System.Windows.Forms.CheckBox();
            this.gbValid = new System.Windows.Forms.GroupBox();
            this.cmbValidparameter = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gbMatch = new System.Windows.Forms.GroupBox();
            this.cmbMatchparameter = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.gpOutput = new System.Windows.Forms.GroupBox();
            this.btnRemoveRow = new System.Windows.Forms.Button();
            this.btnAddRow = new System.Windows.Forms.Button();
            this.gbSql = new System.Windows.Forms.GroupBox();
            this.tbSql = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnInsertVariable = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.idgvOutputColumns = new ComponentFramework.Controls.IsagDataGridView();
            this.cmbTable = new ComponentFramework.Controls.IsagComboBox();
            this.isagVariableChooser = new Lookup2.Framework.Gui.IsagVariableChooser();
            this.isagCnmgr = new Lookup2.Framework.Gui.IsagConnectionManager();
            this.cmbTableMatchColumn = new ComponentFramework.Controls.IsagComboBox();
            this.cmbTableValidToColumn = new ComponentFramework.Controls.IsagComboBox();
            this.cmbTableValidFromColumn = new ComponentFramework.Controls.IsagComboBox();
            this.isagConnectionManager1 = new Lookup2.Framework.Gui.IsagConnectionManager();
            this.gbValid.SuspendLayout();
            this.gbMatch.SuspendLayout();
            this.gpOutput.SuspendLayout();
            this.gbSql.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.idgvOutputColumns)).BeginInit();
            this.SuspendLayout();
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Location = new System.Drawing.Point(12, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(163, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "Connection Manager (ADO.NET)";
            this.toolTip1.SetToolTip(this.label11, "Choose the Connection Manager. Only ADO.NET is supported.");
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "DFT Value";
            this.toolTip1.SetToolTip(this.label1, "Value to LookUp; LU2_Validparameter");
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(6, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "DFT Column";
            this.toolTip1.SetToolTip(this.label4, "Value to match; LU_Matchparameter");
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(703, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(153, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "LookUp Table valid_to Column";
            this.toolTip1.SetToolTip(this.label5, "Upper Bound Value; LU2_Table_ValidToValue");
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(385, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 13);
            this.label6.TabIndex = 34;
            this.label6.Text = "LookUp Table Column";
            this.toolTip1.SetToolTip(this.label6, "Value to match; LU_Tablematchcolumn");
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(385, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(167, 13);
            this.label8.TabIndex = 35;
            this.label8.Text = "LookUp Table valid_from Column ";
            this.toolTip1.SetToolTip(this.label8, "Lower Bound Value; LU2_Table_ValidFromValue");
            // 
            // cbIsInclusiveUpperBound
            // 
            this.cbIsInclusiveUpperBound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbIsInclusiveUpperBound.AutoSize = true;
            this.cbIsInclusiveUpperBound.Checked = true;
            this.cbIsInclusiveUpperBound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIsInclusiveUpperBound.Location = new System.Drawing.Point(706, 80);
            this.cbIsInclusiveUpperBound.Name = "cbIsInclusiveUpperBound";
            this.cbIsInclusiveUpperBound.Size = new System.Drawing.Size(132, 17);
            this.cbIsInclusiveUpperBound.TabIndex = 3;
            this.cbIsInclusiveUpperBound.Text = "Upper Bound included";
            this.toolTip1.SetToolTip(this.cbIsInclusiveUpperBound, "LU2_IsInclusiveUpperBound");
            this.cbIsInclusiveUpperBound.UseVisualStyleBackColor = true;
            // 
            // gbValid
            // 
            this.gbValid.Controls.Add(this.cmbTableValidToColumn);
            this.gbValid.Controls.Add(this.cmbTableValidFromColumn);
            this.gbValid.Controls.Add(this.cmbValidparameter);
            this.gbValid.Controls.Add(this.label5);
            this.gbValid.Controls.Add(this.label9);
            this.gbValid.Controls.Add(this.label2);
            this.gbValid.Controls.Add(this.label8);
            this.gbValid.Controls.Add(this.label1);
            this.gbValid.Controls.Add(this.cbIsInclusiveUpperBound);
            this.gbValid.Location = new System.Drawing.Point(15, 139);
            this.gbValid.Name = "gbValid";
            this.gbValid.Size = new System.Drawing.Size(1018, 103);
            this.gbValid.TabIndex = 2;
            this.gbValid.TabStop = false;
            this.gbValid.Text = "Validation (From/To)";
            // 
            // cmbValidparameter
            // 
            this.cmbValidparameter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbValidparameter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbValidparameter.FormattingEnabled = true;
            this.cmbValidparameter.Location = new System.Drawing.Point(9, 52);
            this.cmbValidparameter.Name = "cmbValidparameter";
            this.cmbValidparameter.Size = new System.Drawing.Size(309, 21);
            this.cmbValidparameter.Sorted = true;
            this.cmbValidparameter.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Location = new System.Drawing.Point(663, 56);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(25, 13);
            this.label9.TabIndex = 35;
            this.label9.Text = "and";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(330, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 35;
            this.label2.Text = "between";
            // 
            // gbMatch
            // 
            this.gbMatch.Controls.Add(this.cmbTableMatchColumn);
            this.gbMatch.Controls.Add(this.cmbMatchparameter);
            this.gbMatch.Controls.Add(this.label6);
            this.gbMatch.Controls.Add(this.label10);
            this.gbMatch.Controls.Add(this.label4);
            this.gbMatch.Location = new System.Drawing.Point(15, 259);
            this.gbMatch.Name = "gbMatch";
            this.gbMatch.Size = new System.Drawing.Size(1018, 88);
            this.gbMatch.TabIndex = 3;
            this.gbMatch.TabStop = false;
            this.gbMatch.Text = "Optional Matchcolumn";
            this.toolTip1.SetToolTip(this.gbMatch, "FK_..._BK/BK");
            // 
            // cmbMatchparameter
            // 
            this.cmbMatchparameter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbMatchparameter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMatchparameter.FormattingEnabled = true;
            this.cmbMatchparameter.Location = new System.Drawing.Point(9, 55);
            this.cmbMatchparameter.Name = "cmbMatchparameter";
            this.cmbMatchparameter.Size = new System.Drawing.Size(315, 21);
            this.cmbMatchparameter.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(347, 59);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(13, 13);
            this.label10.TabIndex = 35;
            this.label10.Text = "=";
            // 
            // gpOutput
            // 
            this.gpOutput.Controls.Add(this.idgvOutputColumns);
            this.gpOutput.Controls.Add(this.btnRemoveRow);
            this.gpOutput.Controls.Add(this.btnAddRow);
            this.gpOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpOutput.Location = new System.Drawing.Point(0, 0);
            this.gpOutput.Name = "gpOutput";
            this.gpOutput.Size = new System.Drawing.Size(1015, 127);
            this.gpOutput.TabIndex = 4;
            this.gpOutput.TabStop = false;
            this.gpOutput.Text = "Assign output columns:";
            // 
            // btnRemoveRow
            // 
            this.btnRemoveRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemoveRow.Location = new System.Drawing.Point(106, 100);
            this.btnRemoveRow.Name = "btnRemoveRow";
            this.btnRemoveRow.Size = new System.Drawing.Size(97, 21);
            this.btnRemoveRow.TabIndex = 2;
            this.btnRemoveRow.Text = "Remove Row(s)";
            this.btnRemoveRow.UseVisualStyleBackColor = true;
            this.btnRemoveRow.Click += new System.EventHandler(this.btnRemoveRow_Click);
            // 
            // btnAddRow
            // 
            this.btnAddRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddRow.Location = new System.Drawing.Point(3, 100);
            this.btnAddRow.Name = "btnAddRow";
            this.btnAddRow.Size = new System.Drawing.Size(97, 21);
            this.btnAddRow.TabIndex = 1;
            this.btnAddRow.Text = "Add Row";
            this.btnAddRow.UseVisualStyleBackColor = true;
            this.btnAddRow.Click += new System.EventHandler(this.btnAddRow_Click);
            // 
            // gbSql
            // 
            this.gbSql.Controls.Add(this.tbSql);
            this.gbSql.Controls.Add(this.btnUpdate);
            this.gbSql.Controls.Add(this.btnPreview);
            this.gbSql.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSql.Location = new System.Drawing.Point(0, 0);
            this.gbSql.Name = "gbSql";
            this.gbSql.Size = new System.Drawing.Size(1015, 108);
            this.gbSql.TabIndex = 5;
            this.gbSql.TabStop = false;
            this.gbSql.Text = "SQL Statement";
            // 
            // tbSql
            // 
            this.tbSql.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSql.Location = new System.Drawing.Point(9, 18);
            this.tbSql.Multiline = true;
            this.tbSql.Name = "tbSql";
            this.tbSql.Size = new System.Drawing.Size(936, 87);
            this.tbSql.TabIndex = 3;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Location = new System.Drawing.Point(951, 45);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(58, 21);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreview.Location = new System.Drawing.Point(951, 18);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(58, 21);
            this.btnPreview.TabIndex = 1;
            this.btnPreview.Text = "Preview";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnInsertVariable
            // 
            this.btnInsertVariable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInsertVariable.Location = new System.Drawing.Point(927, 43);
            this.btnInsertVariable.Name = "btnInsertVariable";
            this.btnInsertVariable.Size = new System.Drawing.Size(91, 22);
            this.btnInsertVariable.TabIndex = 2;
            this.btnInsertVariable.Text = "Insert Variable";
            this.btnInsertVariable.UseVisualStyleBackColor = true;
            this.btnInsertVariable.Click += new System.EventHandler(this.btnInsertVariable_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(865, 598);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(74, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(956, 598);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.cmbTable);
            this.groupBox2.Controls.Add(this.btnInsertVariable);
            this.groupBox2.Controls.Add(this.isagVariableChooser);
            this.groupBox2.Location = new System.Drawing.Point(15, 55);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1021, 78);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Lookup Table";
            this.toolTip1.SetToolTip(this.groupBox2, "LU_Table");
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(15, 353);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gpOutput);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gbSql);
            this.splitContainer1.Size = new System.Drawing.Size(1015, 239);
            this.splitContainer1.SplitterDistance = 127;
            this.splitContainer1.TabIndex = 9;
            // 
            // idgvOutputColumns
            // 
            this.idgvOutputColumns.AllowUserToAddRows = false;
            this.idgvOutputColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.idgvOutputColumns.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.idgvOutputColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.idgvOutputColumns.Location = new System.Drawing.Point(9, 19);
            this.idgvOutputColumns.Name = "idgvOutputColumns";
            this.idgvOutputColumns.Size = new System.Drawing.Size(1006, 75);
            this.idgvOutputColumns.TabIndex = 3;
            // 
            // cmbTable
            // 
            this.cmbTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTable.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbTable.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbTable.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbTable.ForeColor = System.Drawing.Color.Red;
            this.cmbTable.FormattingEnabled = true;
            this.cmbTable.Items.AddRange(new object[] {
            ""});
            this.cmbTable.Location = new System.Drawing.Point(6, 16);
            this.cmbTable.Name = "cmbTable";
            this.cmbTable.Size = new System.Drawing.Size(1012, 21);
            this.cmbTable.Sorted = true;
            this.cmbTable.TabIndex = 0;
            this.cmbTable.TextChanged += new System.EventHandler(this.cmbTable_TextChanged);
            // 
            // isagVariableChooser
            // 
            this.isagVariableChooser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.isagVariableChooser.Location = new System.Drawing.Point(659, 43);
            this.isagVariableChooser.Margin = new System.Windows.Forms.Padding(0);
            this.isagVariableChooser.Name = "isagVariableChooser";
            this.isagVariableChooser.Size = new System.Drawing.Size(265, 22);
            this.isagVariableChooser.TabIndex = 1;
            // 
            // isagCnmgr
            // 
            this.isagCnmgr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.isagCnmgr.ComponentConnections = null;
            this.isagCnmgr.ComponentMetaData = null;
            this.isagCnmgr.ConnectionManagerName = null;
            this.isagCnmgr.Location = new System.Drawing.Point(174, 16);
            this.isagCnmgr.Margin = new System.Windows.Forms.Padding(0);
            this.isagCnmgr.Name = "isagCnmgr";
            this.isagCnmgr.ServiceProvider = null;
            this.isagCnmgr.Size = new System.Drawing.Size(859, 36);
            this.isagCnmgr.TabIndex = 0;
            // 
            // cmbTableMatchColumn
            // 
            this.cmbTableMatchColumn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTableMatchColumn.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbTableMatchColumn.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbTableMatchColumn.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbTableMatchColumn.ForeColor = System.Drawing.Color.Red;
            this.cmbTableMatchColumn.FormattingEnabled = true;
            this.cmbTableMatchColumn.Items.AddRange(new object[] {
            ""});
            this.cmbTableMatchColumn.Location = new System.Drawing.Point(388, 55);
            this.cmbTableMatchColumn.Name = "cmbTableMatchColumn";
            this.cmbTableMatchColumn.Size = new System.Drawing.Size(627, 21);
            this.cmbTableMatchColumn.Sorted = true;
            this.cmbTableMatchColumn.TabIndex = 1;
            // 
            // cmbTableValidToColumn
            // 
            this.cmbTableValidToColumn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTableValidToColumn.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbTableValidToColumn.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbTableValidToColumn.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbTableValidToColumn.ForeColor = System.Drawing.Color.Red;
            this.cmbTableValidToColumn.FormattingEnabled = true;
            this.cmbTableValidToColumn.Items.AddRange(new object[] {
            ""});
            this.cmbTableValidToColumn.Location = new System.Drawing.Point(706, 53);
            this.cmbTableValidToColumn.Name = "cmbTableValidToColumn";
            this.cmbTableValidToColumn.Size = new System.Drawing.Size(309, 21);
            this.cmbTableValidToColumn.Sorted = true;
            this.cmbTableValidToColumn.TabIndex = 2;
            // 
            // cmbTableValidFromColumn
            // 
            this.cmbTableValidFromColumn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTableValidFromColumn.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbTableValidFromColumn.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbTableValidFromColumn.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbTableValidFromColumn.ForeColor = System.Drawing.Color.Red;
            this.cmbTableValidFromColumn.FormattingEnabled = true;
            this.cmbTableValidFromColumn.Location = new System.Drawing.Point(388, 52);
            this.cmbTableValidFromColumn.Name = "cmbTableValidFromColumn";
            this.cmbTableValidFromColumn.Size = new System.Drawing.Size(257, 21);
            this.cmbTableValidFromColumn.Sorted = true;
            this.cmbTableValidFromColumn.TabIndex = 1;
            // 
            // isagConnectionManager1
            // 
            this.isagConnectionManager1.ComponentConnections = null;
            this.isagConnectionManager1.ComponentMetaData = null;
            this.isagConnectionManager1.ConnectionManagerName = null;
            this.isagConnectionManager1.Location = new System.Drawing.Point(160, 86);
            this.isagConnectionManager1.Margin = new System.Windows.Forms.Padding(0);
            this.isagConnectionManager1.Name = "isagConnectionManager1";
            this.isagConnectionManager1.ServiceProvider = null;
            this.isagConnectionManager1.Size = new System.Drawing.Size(506, 22);
            this.isagConnectionManager1.TabIndex = 47;
            // 
            // frmLookupUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1045, 633);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.isagCnmgr);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbMatch);
            this.Controls.Add(this.gbValid);
            this.Controls.Add(this.label11);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1061, 671);
            this.Name = "frmLookupUI";
            this.Text = "Lookup2";
            this.gbValid.ResumeLayout(false);
            this.gbValid.PerformLayout();
            this.gbMatch.ResumeLayout(false);
            this.gbMatch.PerformLayout();
            this.gpOutput.ResumeLayout(false);
            this.gbSql.ResumeLayout(false);
            this.gbSql.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.idgvOutputColumns)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbIsInclusiveUpperBound;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox gpOutput;
        private System.Windows.Forms.GroupBox gbValid;
        private System.Windows.Forms.GroupBox gbMatch;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbSql;
        private System.Windows.Forms.ComboBox cmbValidparameter;
        private System.Windows.Forms.ComboBox cmbMatchparameter;
        private System.Windows.Forms.Button btnInsertVariable;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnRemoveRow;
        private System.Windows.Forms.Button btnAddRow;
        private Lookup2.Framework.Gui.IsagConnectionManager isagConnectionManager1;
        private Lookup2.Framework.Gui.IsagConnectionManager isagCnmgr;

        private Lookup2.Framework.Gui.IsagVariableChooser isagVariableChooser;
        private ComponentFramework.Controls.IsagComboBox cmbTableValidFromColumn;
        private ComponentFramework.Controls.IsagComboBox cmbTableValidToColumn;
        private ComponentFramework.Controls.IsagComboBox cmbTableMatchColumn;
        private ComponentFramework.Controls.IsagComboBox cmbTable;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private ComponentFramework.Controls.IsagDataGridView idgvOutputColumns;
        private System.Windows.Forms.TextBox tbSql;

    }
}
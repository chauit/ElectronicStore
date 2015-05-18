namespace ElectronicStore.Main
{
    partial class FindProduct
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateOrderDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.drlMonth = new System.Windows.Forms.ComboBox();
            this.drlCustomer = new System.Windows.Forms.ComboBox();
            this.cboOrderDate = new System.Windows.Forms.CheckBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.DeliveryId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RoleId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DeliveryId,
            this.StartDate,
            this.EndDate,
            this.Status,
            this.RoleId});
            this.dataGridView.GridColor = System.Drawing.SystemColors.Control;
            this.dataGridView.Location = new System.Drawing.Point(12, 133);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(769, 291);
            this.dataGridView.TabIndex = 1;
            this.dataGridView.DoubleClick += new System.EventHandler(this.SelectOrder);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonSearch);
            this.groupBox1.Controls.Add(this.cboOrderDate);
            this.groupBox1.Controls.Add(this.drlCustomer);
            this.groupBox1.Controls.Add(this.drlMonth);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dateOrderDate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(769, 99);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Điều kiện tìm kiếm";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ngày tạo hợp đồng";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(268, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tên khách hàng";
            // 
            // dateOrderDate
            // 
            this.dateOrderDate.Enabled = false;
            this.dateOrderDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateOrderDate.Location = new System.Drawing.Point(156, 22);
            this.dateOrderDate.Name = "dateOrderDate";
            this.dateOrderDate.Size = new System.Drawing.Size(82, 20);
            this.dateOrderDate.TabIndex = 2;
            this.dateOrderDate.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Tháng";
            // 
            // drlMonth
            // 
            this.drlMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drlMonth.FormattingEnabled = true;
            this.drlMonth.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.drlMonth.Location = new System.Drawing.Point(140, 57);
            this.drlMonth.Name = "drlMonth";
            this.drlMonth.Size = new System.Drawing.Size(98, 21);
            this.drlMonth.TabIndex = 4;
            // 
            // drlCustomer
            // 
            this.drlCustomer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.drlCustomer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.drlCustomer.FormattingEnabled = true;
            this.drlCustomer.Location = new System.Drawing.Point(360, 22);
            this.drlCustomer.Name = "drlCustomer";
            this.drlCustomer.Size = new System.Drawing.Size(301, 21);
            this.drlCustomer.TabIndex = 3;
            // 
            // cboOrderDate
            // 
            this.cboOrderDate.AutoSize = true;
            this.cboOrderDate.Location = new System.Drawing.Point(140, 24);
            this.cboOrderDate.Name = "cboOrderDate";
            this.cboOrderDate.Size = new System.Drawing.Size(15, 14);
            this.cboOrderDate.TabIndex = 1;
            this.cboOrderDate.UseVisualStyleBackColor = true;
            this.cboOrderDate.CheckedChanged += new System.EventHandler(this.SelectOrderDate);
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(586, 55);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(75, 23);
            this.buttonSearch.TabIndex = 5;
            this.buttonSearch.Text = "Tìm kiếm";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.Search);
            // 
            // DeliveryId
            // 
            this.DeliveryId.DataPropertyName = "OrderId";
            this.DeliveryId.HeaderText = "Mã đơn hàng";
            this.DeliveryId.Name = "DeliveryId";
            this.DeliveryId.ReadOnly = true;
            this.DeliveryId.Width = 150;
            // 
            // StartDate
            // 
            this.StartDate.DataPropertyName = "OrderDate";
            this.StartDate.HeaderText = "Ngày tạo đơn hàng";
            this.StartDate.Name = "StartDate";
            this.StartDate.ReadOnly = true;
            this.StartDate.Width = 150;
            // 
            // EndDate
            // 
            this.EndDate.DataPropertyName = "DeliveryDate";
            this.EndDate.HeaderText = "Ngày chuyển hàng";
            this.EndDate.Name = "EndDate";
            this.EndDate.ReadOnly = true;
            this.EndDate.Width = 150;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "CustomerName";
            this.Status.HeaderText = "Khách hàng";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 250;
            // 
            // RoleId
            // 
            this.RoleId.DataPropertyName = "Id";
            this.RoleId.HeaderText = "Id";
            this.RoleId.Name = "RoleId";
            this.RoleId.Visible = false;
            // 
            // SearchOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(810, 436);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SearchOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tìm kiếm đơn hàng";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateOrderDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox drlMonth;
        private System.Windows.Forms.ComboBox drlCustomer;
        private System.Windows.Forms.CheckBox cboOrderDate;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeliveryId;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn RoleId;
    }
}
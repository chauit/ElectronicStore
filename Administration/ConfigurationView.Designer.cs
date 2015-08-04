namespace ElectronicStore.Administration
{
    partial class ConfigurationView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationView));
            this.buttonNew = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.cbo = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ConfigurationKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RoleId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConfigurationValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FieldCreated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FieldCreatedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FieldModified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FieldModifiedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonNew
            // 
            this.buttonNew.Image = ((System.Drawing.Image)(resources.GetObject("buttonNew.Image")));
            this.buttonNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonNew.Location = new System.Drawing.Point(10, 14);
            this.buttonNew.Name = "buttonNew";
            this.buttonNew.Size = new System.Drawing.Size(70, 25);
            this.buttonNew.TabIndex = 0;
            this.buttonNew.Text = "Thêm   ";
            this.buttonNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonNew.UseVisualStyleBackColor = true;
            this.buttonNew.Click += new System.EventHandler(this.NewItem);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cbo,
            this.ConfigurationKey,
            this.RoleId,
            this.ConfigurationValue,
            this.FieldCreated,
            this.FieldCreatedBy,
            this.FieldModified,
            this.FieldModifiedBy});
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.GridColor = System.Drawing.SystemColors.Control;
            this.dataGridView.Location = new System.Drawing.Point(3, 18);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(904, 327);
            this.dataGridView.TabIndex = 1;
            // 
            // cbo
            // 
            this.cbo.FalseValue = "0";
            this.cbo.FillWeight = 20.30457F;
            this.cbo.HeaderText = "";
            this.cbo.IndeterminateValue = "";
            this.cbo.MinimumWidth = 20;
            this.cbo.Name = "cbo";
            this.cbo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.cbo.TrueValue = "1";
            // 
            // ConfigurationKey
            // 
            this.ConfigurationKey.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ConfigurationKey.DataPropertyName = "Key";
            this.ConfigurationKey.FillWeight = 179.6954F;
            this.ConfigurationKey.HeaderText = "Khóa";
            this.ConfigurationKey.Name = "ConfigurationKey";
            // 
            // RoleId
            // 
            this.RoleId.DataPropertyName = "Id";
            this.RoleId.HeaderText = "Id";
            this.RoleId.Name = "RoleId";
            this.RoleId.Visible = false;
            // 
            // ConfigurationValue
            // 
            this.ConfigurationValue.DataPropertyName = "Value";
            this.ConfigurationValue.HeaderText = "Giá trị";
            this.ConfigurationValue.Name = "ConfigurationValue";
            this.ConfigurationValue.ReadOnly = true;
            // 
            // FieldCreated
            // 
            this.FieldCreated.DataPropertyName = "Created";
            this.FieldCreated.HeaderText = "Ngày tạo";
            this.FieldCreated.Name = "FieldCreated";
            this.FieldCreated.ReadOnly = true;
            // 
            // FieldCreatedBy
            // 
            this.FieldCreatedBy.DataPropertyName = "CreatedBy";
            this.FieldCreatedBy.HeaderText = "Người tạo";
            this.FieldCreatedBy.Name = "FieldCreatedBy";
            this.FieldCreatedBy.ReadOnly = true;
            // 
            // FieldModified
            // 
            this.FieldModified.DataPropertyName = "Modified";
            this.FieldModified.HeaderText = "Ngày sửa";
            this.FieldModified.Name = "FieldModified";
            this.FieldModified.ReadOnly = true;
            // 
            // FieldModifiedBy
            // 
            this.FieldModifiedBy.DataPropertyName = "ModifiedBy";
            this.FieldModifiedBy.HeaderText = "Người sửa";
            this.FieldModifiedBy.Name = "FieldModifiedBy";
            this.FieldModifiedBy.ReadOnly = true;
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Image = ((System.Drawing.Image)(resources.GetObject("buttonUpdate.Image")));
            this.buttonUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonUpdate.Location = new System.Drawing.Point(86, 14);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(70, 25);
            this.buttonUpdate.TabIndex = 2;
            this.buttonUpdate.Text = "Sửa     ";
            this.buttonUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.UpdateItem);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Image = ((System.Drawing.Image)(resources.GetObject("buttonDelete.Image")));
            this.buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDelete.Location = new System.Drawing.Point(162, 14);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(70, 25);
            this.buttonDelete.TabIndex = 3;
            this.buttonDelete.Text = "Xóa    ";
            this.buttonDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.DeleteItem);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Image = ((System.Drawing.Image)(resources.GetObject("buttonRefresh.Image")));
            this.buttonRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonRefresh.Location = new System.Drawing.Point(238, 14);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(84, 25);
            this.buttonRefresh.TabIndex = 4;
            this.buttonRefresh.Text = "Cập nhật";
            this.buttonRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.RefreshItems);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(910, 47);
            this.panel1.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonNew);
            this.groupBox2.Controls.Add(this.buttonDelete);
            this.groupBox2.Controls.Add(this.buttonRefresh);
            this.groupBox2.Controls.Add(this.buttonUpdate);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(910, 47);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 47);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(910, 348);
            this.panel2.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(910, 348);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Danh sách cấu hình";
            // 
            // ConfigurationView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(910, 395);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ConfigurationView";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonNew;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cbo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConfigurationKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn RoleId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConfigurationValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn FieldCreated;
        private System.Windows.Forms.DataGridViewTextBoxColumn FieldCreatedBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn FieldModified;
        private System.Windows.Forms.DataGridViewTextBoxColumn FieldModifiedBy;
    }
}
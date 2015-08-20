namespace ElectronicStore.Main
{
    partial class OrderView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrderView));
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewDateTimeColumn gridViewDateTimeColumn1 = new Telerik.WinControls.UI.GridViewDateTimeColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn9 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            this.buttonNew = new System.Windows.Forms.Button();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radGridView = new Telerik.WinControls.UI.RadGridView();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonNew
            // 
            this.buttonNew.Image = ((System.Drawing.Image)(resources.GetObject("buttonNew.Image")));
            this.buttonNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonNew.Location = new System.Drawing.Point(6, 19);
            this.buttonNew.Name = "buttonNew";
            this.buttonNew.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonNew.Size = new System.Drawing.Size(70, 25);
            this.buttonNew.TabIndex = 0;
            this.buttonNew.Text = "Thêm   ";
            this.buttonNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonNew.UseVisualStyleBackColor = true;
            this.buttonNew.Click += new System.EventHandler(this.NewItem);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Image = ((System.Drawing.Image)(resources.GetObject("buttonRefresh.Image")));
            this.buttonRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonRefresh.Location = new System.Drawing.Point(87, 19);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(81, 25);
            this.buttonRefresh.TabIndex = 4;
            this.buttonRefresh.Text = "Cập nhật";
            this.buttonRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.RefreshItems);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(810, 55);
            this.panel1.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonNew);
            this.groupBox1.Controls.Add(this.buttonRefresh);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(810, 55);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 55);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(810, 340);
            this.panel2.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radGridView);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(810, 340);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Danh sách hóa đơn";
            // 
            // radGridView
            // 
            this.radGridView.AutoScroll = true;
            this.radGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radGridView.Location = new System.Drawing.Point(3, 18);
            // 
            // radGridView
            // 
            this.radGridView.MasterTemplate.AllowAddNewRow = false;
            this.radGridView.MasterTemplate.AllowCellContextMenu = false;
            this.radGridView.MasterTemplate.AllowColumnChooser = false;
            this.radGridView.MasterTemplate.AllowColumnHeaderContextMenu = false;
            this.radGridView.MasterTemplate.AllowDeleteRow = false;
            this.radGridView.MasterTemplate.AllowDragToGroup = false;
            this.radGridView.MasterTemplate.AllowEditRow = false;
            this.radGridView.MasterTemplate.AllowRowReorder = true;
            this.radGridView.MasterTemplate.AllowRowResize = false;
            this.radGridView.MasterTemplate.AutoGenerateColumns = false;
            this.radGridView.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            gridViewTextBoxColumn1.AllowGroup = false;
            gridViewTextBoxColumn1.FieldName = "OrderNo";
            gridViewTextBoxColumn1.FormatString = "";
            gridViewTextBoxColumn1.HeaderText = "Mã đơn hàng";
            gridViewTextBoxColumn1.Name = "columnOrderNo";
            gridViewTextBoxColumn1.ReadOnly = true;
            gridViewTextBoxColumn1.Width = 66;
            gridViewTextBoxColumn2.AllowGroup = false;
            gridViewTextBoxColumn2.FieldName = "CustomerName";
            gridViewTextBoxColumn2.FormatString = "";
            gridViewTextBoxColumn2.HeaderText = "Khách hàng";
            gridViewTextBoxColumn2.Name = "columnCustomerName";
            gridViewTextBoxColumn2.ReadOnly = true;
            gridViewTextBoxColumn2.Width = 66;
            gridViewDateTimeColumn1.AllowGroup = false;
            gridViewDateTimeColumn1.CustomFormat = "d/M/yyyy h:m tt";
            gridViewDateTimeColumn1.FieldName = "OrderDate";
            gridViewDateTimeColumn1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            gridViewDateTimeColumn1.FormatString = "";
            gridViewDateTimeColumn1.HeaderText = "Ngày tạo";
            gridViewDateTimeColumn1.Name = "columnOrderDate";
            gridViewDateTimeColumn1.ReadOnly = true;
            gridViewDateTimeColumn1.Width = 66;
            gridViewTextBoxColumn3.AllowGroup = false;
            gridViewTextBoxColumn3.FieldName = "Status";
            gridViewTextBoxColumn3.FormatString = "";
            gridViewTextBoxColumn3.HeaderText = "Tình trạng";
            gridViewTextBoxColumn3.Name = "columnStatus";
            gridViewTextBoxColumn3.ReadOnly = true;
            gridViewTextBoxColumn3.Width = 160;
            gridViewTextBoxColumn4.AllowGroup = false;
            gridViewTextBoxColumn4.FieldName = "DeliveryName";
            gridViewTextBoxColumn4.FormatString = "";
            gridViewTextBoxColumn4.HeaderText = "Vận chuyển";
            gridViewTextBoxColumn4.Name = "columnDelivery";
            gridViewTextBoxColumn4.ReadOnly = true;
            gridViewTextBoxColumn4.Width = 66;
            gridViewTextBoxColumn5.AllowGroup = false;
            gridViewTextBoxColumn5.FieldName = "SendEmail";
            gridViewTextBoxColumn5.FormatString = "";
            gridViewTextBoxColumn5.HeaderText = "Email";
            gridViewTextBoxColumn5.Name = "columnSendEmail";
            gridViewTextBoxColumn5.ReadOnly = true;
            gridViewTextBoxColumn5.Width = 66;
            gridViewTextBoxColumn6.AllowGroup = false;
            gridViewTextBoxColumn6.FieldName = "SendMessage";
            gridViewTextBoxColumn6.FormatString = "";
            gridViewTextBoxColumn6.HeaderText = "Tin nhắn";
            gridViewTextBoxColumn6.Name = "columnSendMessage";
            gridViewTextBoxColumn6.ReadOnly = true;
            gridViewTextBoxColumn6.Width = 66;
            gridViewTextBoxColumn7.AllowGroup = false;
            gridViewTextBoxColumn7.FieldName = "IsSendNotification";
            gridViewTextBoxColumn7.FormatString = "";
            gridViewTextBoxColumn7.HeaderText = "Thông báo";
            gridViewTextBoxColumn7.Name = "columnIsSendNotification";
            gridViewTextBoxColumn7.ReadOnly = true;
            gridViewTextBoxColumn7.Width = 66;
            gridViewTextBoxColumn8.AllowGroup = false;
            gridViewTextBoxColumn8.FieldName = "DeliveryType";
            gridViewTextBoxColumn8.FormatString = "";
            gridViewTextBoxColumn8.HeaderText = "Vận chuyển";
            gridViewTextBoxColumn8.Name = "columnDeliveryName";
            gridViewTextBoxColumn8.ReadOnly = true;
            gridViewTextBoxColumn8.Width = 66;
            gridViewTextBoxColumn9.AllowGroup = false;
            gridViewTextBoxColumn9.FieldName = "CreatedBy";
            gridViewTextBoxColumn9.FormatString = "";
            gridViewTextBoxColumn9.HeaderText = "Người tạo";
            gridViewTextBoxColumn9.Name = "columnCreatedBy";
            gridViewTextBoxColumn9.ReadOnly = true;
            gridViewTextBoxColumn9.Width = 105;
            this.radGridView.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewDateTimeColumn1,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4,
            gridViewTextBoxColumn5,
            gridViewTextBoxColumn6,
            gridViewTextBoxColumn7,
            gridViewTextBoxColumn8,
            gridViewTextBoxColumn9});
            this.radGridView.MasterTemplate.EnableFiltering = true;
            this.radGridView.MasterTemplate.EnableGrouping = false;
            this.radGridView.MasterTemplate.EnableSorting = false;
            this.radGridView.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect;
            this.radGridView.Name = "radGridView";
            this.radGridView.ShowGroupPanel = false;
            this.radGridView.Size = new System.Drawing.Size(804, 319);
            this.radGridView.TabIndex = 0;
            this.radGridView.Text = "radGridView1";
            this.radGridView.UseScrollbarsInHierarchy = true;
            this.radGridView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MouseClick);
            // 
            // OrderView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(810, 395);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "OrderView";
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radGridView.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonNew;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox2;
        private Telerik.WinControls.UI.RadGridView radGridView;
    }
}
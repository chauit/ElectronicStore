namespace ElectronicStore.Main
{
    partial class OrderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrderForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.cboVat = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtRecipientPhone = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtRecipient = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cboDeliveryInternal = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dateOrderDate = new System.Windows.Forms.DateTimePicker();
            this.labelStatus = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textDeliverrAddress = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dateDeliveryDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.labelOrderNo = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.drlCustomer = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonAddProduct = new System.Windows.Forms.Button();
            this.buttonDeleteProduct = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ColumnTen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã đơn hàng";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDiscount);
            this.groupBox1.Controls.Add(this.cboVat);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtRecipientPhone);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtRecipient);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cboDeliveryInternal);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.dateOrderDate);
            this.groupBox1.Controls.Add(this.labelStatus);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textDeliverrAddress);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dateDeliveryDate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.labelOrderNo);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.drlCustomer);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(800, 284);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin đơn hàng";
            // 
            // txtDiscount
            // 
            this.txtDiscount.Location = new System.Drawing.Point(136, 154);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(63, 22);
            this.txtDiscount.TabIndex = 7;
            this.txtDiscount.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DiscountKeyPress);
            // 
            // cboVat
            // 
            this.cboVat.AutoSize = true;
            this.cboVat.Location = new System.Drawing.Point(562, 157);
            this.cboVat.Name = "cboVat";
            this.cboVat.Size = new System.Drawing.Size(15, 14);
            this.cboVat.TabIndex = 34;
            this.cboVat.UseVisualStyleBackColor = true;
            this.cboVat.CheckedChanged += new System.EventHandler(this.SelectVat);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(424, 157);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(43, 13);
            this.label12.TabIndex = 33;
            this.label12.Text = "Có VAT";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(201, 157);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(16, 13);
            this.label11.TabIndex = 32;
            this.label11.Text = "%";
            // 
            // txtRecipientPhone
            // 
            this.txtRecipientPhone.Location = new System.Drawing.Point(562, 124);
            this.txtRecipientPhone.Name = "txtRecipientPhone";
            this.txtRecipientPhone.Size = new System.Drawing.Size(221, 22);
            this.txtRecipientPhone.TabIndex = 6;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(424, 127);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(79, 13);
            this.label10.TabIndex = 30;
            this.label10.Text = "Số điện thoại:";
            // 
            // txtRecipient
            // 
            this.txtRecipient.Location = new System.Drawing.Point(136, 124);
            this.txtRecipient.Name = "txtRecipient";
            this.txtRecipient.Size = new System.Drawing.Size(206, 22);
            this.txtRecipient.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(25, 127);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 13);
            this.label9.TabIndex = 28;
            this.label9.Text = "Người nhận:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(25, 157);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = "Chiết khấu:";
            // 
            // cboDeliveryInternal
            // 
            this.cboDeliveryInternal.AutoSize = true;
            this.cboDeliveryInternal.Location = new System.Drawing.Point(562, 96);
            this.cboDeliveryInternal.Name = "cboDeliveryInternal";
            this.cboDeliveryInternal.Size = new System.Drawing.Size(15, 14);
            this.cboDeliveryInternal.TabIndex = 4;
            this.cboDeliveryInternal.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(424, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(132, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "Chuyển trong nội thành";
            // 
            // dateOrderDate
            // 
            this.dateOrderDate.CustomFormat = "dd/MM/yyyy hh:mm tt";
            this.dateOrderDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateOrderDate.Location = new System.Drawing.Point(136, 57);
            this.dateOrderDate.Name = "dateOrderDate";
            this.dateOrderDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dateOrderDate.RightToLeftLayout = true;
            this.dateOrderDate.Size = new System.Drawing.Size(206, 22);
            this.dateOrderDate.TabIndex = 1;
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(559, 24);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(137, 13);
            this.labelStatus.TabIndex = 22;
            this.labelStatus.Text = "Nhận đơn hàng từ khách";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(424, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Trạng thái đơn hàng:";
            // 
            // textDeliverrAddress
            // 
            this.textDeliverrAddress.Location = new System.Drawing.Point(136, 184);
            this.textDeliverrAddress.Multiline = true;
            this.textDeliverrAddress.Name = "textDeliverrAddress";
            this.textDeliverrAddress.Size = new System.Drawing.Size(647, 87);
            this.textDeliverrAddress.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 187);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Địa chỉ chuyển hàng:";
            // 
            // dateDeliveryDate
            // 
            this.dateDeliveryDate.CustomFormat = "dd/MM/yyyy hh:mm tt";
            this.dateDeliveryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateDeliveryDate.Location = new System.Drawing.Point(136, 93);
            this.dateDeliveryDate.Name = "dateDeliveryDate";
            this.dateDeliveryDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dateDeliveryDate.RightToLeftLayout = true;
            this.dateDeliveryDate.Size = new System.Drawing.Size(206, 22);
            this.dateDeliveryDate.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Ngày tạo đơn hàng";
            // 
            // labelOrderNo
            // 
            this.labelOrderNo.AutoSize = true;
            this.labelOrderNo.Location = new System.Drawing.Point(133, 25);
            this.labelOrderNo.Name = "labelOrderNo";
            this.labelOrderNo.Size = new System.Drawing.Size(55, 13);
            this.labelOrderNo.TabIndex = 15;
            this.labelOrderNo.Text = "Order No";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Ngày chuyển hàng";
            // 
            // drlCustomer
            // 
            this.drlCustomer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.drlCustomer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.drlCustomer.FormattingEnabled = true;
            this.drlCustomer.Location = new System.Drawing.Point(562, 57);
            this.drlCustomer.Name = "drlCustomer";
            this.drlCustomer.Size = new System.Drawing.Size(221, 21);
            this.drlCustomer.TabIndex = 2;
            this.drlCustomer.SelectedIndexChanged += new System.EventHandler(this.SelectCustomer);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(424, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Khách hàng";
            // 
            // button2
            // 
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(806, 44);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 25);
            this.button2.TabIndex = 12;
            this.button2.Text = "Thoát   ";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.CancelItem);
            // 
            // buttonSave
            // 
            this.buttonSave.Image = ((System.Drawing.Image)(resources.GetObject("buttonSave.Image")));
            this.buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSave.Location = new System.Drawing.Point(806, 13);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 25);
            this.buttonSave.TabIndex = 11;
            this.buttonSave.Text = "Cập nhật";
            this.buttonSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.SaveItem);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(890, 416);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Mã hàng";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dataGridView);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(3, 67);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(884, 346);
            this.groupBox4.TabIndex = 27;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Danh sách sản phẩm";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnTen,
            this.ColumnCode,
            this.Quantity,
            this.Price,
            this.Total,
            this.Id,
            this.ColumnTotal});
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(3, 18);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView.Size = new System.Drawing.Size(878, 325);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.DataSourceChanged += new System.EventHandler(this.ChangeSource);
            this.dataGridView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.CellPainting);
            this.dataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.UpdateQuantity);
            this.dataGridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.EditControlShowing);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonAddProduct);
            this.groupBox3.Controls.Add(this.buttonDeleteProduct);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(3, 18);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(884, 49);
            this.groupBox3.TabIndex = 26;
            this.groupBox3.TabStop = false;
            // 
            // buttonAddProduct
            // 
            this.buttonAddProduct.Image = ((System.Drawing.Image)(resources.GetObject("buttonAddProduct.Image")));
            this.buttonAddProduct.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddProduct.Location = new System.Drawing.Point(18, 15);
            this.buttonAddProduct.Name = "buttonAddProduct";
            this.buttonAddProduct.Size = new System.Drawing.Size(75, 25);
            this.buttonAddProduct.TabIndex = 9;
            this.buttonAddProduct.Text = "Thêm   ";
            this.buttonAddProduct.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonAddProduct.UseVisualStyleBackColor = true;
            this.buttonAddProduct.Click += new System.EventHandler(this.AddNewProduct);
            // 
            // buttonDeleteProduct
            // 
            this.buttonDeleteProduct.Image = ((System.Drawing.Image)(resources.GetObject("buttonDeleteProduct.Image")));
            this.buttonDeleteProduct.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDeleteProduct.Location = new System.Drawing.Point(99, 15);
            this.buttonDeleteProduct.Name = "buttonDeleteProduct";
            this.buttonDeleteProduct.Size = new System.Drawing.Size(75, 25);
            this.buttonDeleteProduct.TabIndex = 10;
            this.buttonDeleteProduct.Text = "Xóa     ";
            this.buttonDeleteProduct.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonDeleteProduct.UseVisualStyleBackColor = true;
            this.buttonDeleteProduct.Click += new System.EventHandler(this.DeleteProduct);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.buttonSave);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(890, 284);
            this.panel1.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 284);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(890, 416);
            this.panel2.TabIndex = 7;
            // 
            // ColumnTen
            // 
            this.ColumnTen.DataPropertyName = "Name";
            this.ColumnTen.HeaderText = "Tên mặt hàng";
            this.ColumnTen.Name = "ColumnTen";
            this.ColumnTen.ReadOnly = true;
            // 
            // ColumnCode
            // 
            this.ColumnCode.DataPropertyName = "Code";
            this.ColumnCode.HeaderText = "Mã mặt hàng";
            this.ColumnCode.Name = "ColumnCode";
            this.ColumnCode.ReadOnly = true;
            // 
            // Quantity
            // 
            this.Quantity.DataPropertyName = "QuantityValue";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = null;
            this.Quantity.DefaultCellStyle = dataGridViewCellStyle1;
            this.Quantity.HeaderText = "Số lượng (m)";
            this.Quantity.Name = "Quantity";
            // 
            // Price
            // 
            this.Price.DataPropertyName = "ActualPrice";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle2.Format = "0,000.0";
            dataGridViewCellStyle2.NullValue = null;
            this.Price.DefaultCellStyle = dataGridViewCellStyle2;
            this.Price.HeaderText = "Đơn giá";
            this.Price.Name = "Price";
            this.Price.ReadOnly = true;
            // 
            // Total
            // 
            this.Total.DataPropertyName = "TotalValue";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle3.Format = "0,000.0";
            dataGridViewCellStyle3.NullValue = null;
            this.Total.DefaultCellStyle = dataGridViewCellStyle3;
            this.Total.HeaderText = "Tổng";
            this.Total.Name = "Total";
            this.Total.ReadOnly = true;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // ColumnTotal
            // 
            this.ColumnTotal.DataPropertyName = "Total";
            dataGridViewCellStyle4.Format = "0,000.0";
            this.ColumnTotal.DefaultCellStyle = dataGridViewCellStyle4;
            this.ColumnTotal.HeaderText = "Total";
            this.ColumnTotal.Name = "ColumnTotal";
            this.ColumnTotal.ReadOnly = true;
            this.ColumnTotal.Visible = false;
            // 
            // OrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(890, 700);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "OrderForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Order";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox drlCustomer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelOrderNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateDeliveryDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textDeliverrAddress;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonAddProduct;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button buttonDeleteProduct;
        private System.Windows.Forms.DateTimePicker dateOrderDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox cboDeliveryInternal;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtRecipient;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtRecipientPhone;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox cboVat;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtDiscount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTen;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTotal;
    }
}
﻿namespace ElectronicStore.Administration
{
    partial class CustomerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomerForm));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.textMst = new System.Windows.Forms.TextBox();
            this.txtCompany = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textAddress2 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textTenSms = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.drlSegment = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textOtherInformation = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.numberDelivery = new System.Windows.Forms.MaskedTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textEmail2 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textEmail1 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textMobile2 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textMobile1 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textTel = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textPostalCode = new System.Windows.Forms.TextBox();
            this.drlCity = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textAddress1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textFullName = new System.Windows.Forms.TextBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.buttonViewOrder = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Họ tên:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonViewOrder);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.buttonSave);
            this.groupBox1.Controls.Add(this.textTenSms);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.drlSegment);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.textOtherInformation);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.numberDelivery);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.textEmail2);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.textEmail1);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.textMobile2);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.textMobile1);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.textTel);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.textPostalCode);
            this.groupBox1.Controls.Add(this.drlCity);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textAddress1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textFullName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(753, 510);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin chung";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.textMst);
            this.groupBox2.Controls.Add(this.txtCompany);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.textAddress2);
            this.groupBox2.Location = new System.Drawing.Point(6, 263);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(626, 128);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Thông tin viết hóa đơn";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 28);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(68, 13);
            this.label17.TabIndex = 35;
            this.label17.Text = "Mã số thuế:";
            // 
            // textMst
            // 
            this.textMst.Location = new System.Drawing.Point(99, 25);
            this.textMst.Name = "textMst";
            this.textMst.Size = new System.Drawing.Size(198, 22);
            this.textMst.TabIndex = 14;
            // 
            // txtCompany
            // 
            this.txtCompany.Location = new System.Drawing.Point(99, 60);
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.Size = new System.Drawing.Size(513, 22);
            this.txtCompany.TabIndex = 15;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 63);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(50, 13);
            this.label16.TabIndex = 33;
            this.label16.Text = "Công ty:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Địa chỉ:";
            // 
            // textAddress2
            // 
            this.textAddress2.Location = new System.Drawing.Point(99, 98);
            this.textAddress2.Name = "textAddress2";
            this.textAddress2.Size = new System.Drawing.Size(514, 22);
            this.textAddress2.TabIndex = 16;
            // 
            // button2
            // 
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(636, 55);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(111, 25);
            this.button2.TabIndex = 19;
            this.button2.Text = "Thoát          ";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.CancelItem);
            // 
            // buttonSave
            // 
            this.buttonSave.Image = ((System.Drawing.Image)(resources.GetObject("buttonSave.Image")));
            this.buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSave.Location = new System.Drawing.Point(636, 22);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(111, 25);
            this.buttonSave.TabIndex = 18;
            this.buttonSave.Text = "Cập nhật     ";
            this.buttonSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.SaveItem);
            // 
            // textTenSms
            // 
            this.textTenSms.Location = new System.Drawing.Point(413, 22);
            this.textTenSms.Name = "textTenSms";
            this.textTenSms.Size = new System.Drawing.Size(206, 22);
            this.textTenSms.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(326, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 34;
            this.label2.Text = "Tên SMS";
            // 
            // drlSegment
            // 
            this.drlSegment.FormattingEnabled = true;
            this.drlSegment.Location = new System.Drawing.Point(105, 144);
            this.drlSegment.Name = "drlSegment";
            this.drlSegment.Size = new System.Drawing.Size(199, 21);
            this.drlSegment.TabIndex = 6;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(11, 147);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(64, 13);
            this.label15.TabIndex = 31;
            this.label15.Text = "Phân khúc:";
            // 
            // textOtherInformation
            // 
            this.textOtherInformation.Location = new System.Drawing.Point(105, 399);
            this.textOtherInformation.Multiline = true;
            this.textOtherInformation.Name = "textOtherInformation";
            this.textOtherInformation.Size = new System.Drawing.Size(513, 103);
            this.textOtherInformation.TabIndex = 17;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(10, 402);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(89, 13);
            this.label14.TabIndex = 29;
            this.label14.Text = "Thông tin thêm:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(537, 237);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(32, 13);
            this.label13.TabIndex = 28;
            this.label13.Text = "phút";
            // 
            // numberDelivery
            // 
            this.numberDelivery.Location = new System.Drawing.Point(459, 234);
            this.numberDelivery.Name = "numberDelivery";
            this.numberDelivery.Size = new System.Drawing.Size(72, 22);
            this.numberDelivery.TabIndex = 12;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(325, 237);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(128, 13);
            this.label12.TabIndex = 26;
            this.label12.Text = "Thời gian chuyển hàng:";
            // 
            // textEmail2
            // 
            this.textEmail2.Location = new System.Drawing.Point(105, 234);
            this.textEmail2.Name = "textEmail2";
            this.textEmail2.Size = new System.Drawing.Size(198, 22);
            this.textEmail2.TabIndex = 11;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 237);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(46, 13);
            this.label11.TabIndex = 24;
            this.label11.Text = "Email 2:";
            // 
            // textEmail1
            // 
            this.textEmail1.Location = new System.Drawing.Point(413, 204);
            this.textEmail1.Name = "textEmail1";
            this.textEmail1.Size = new System.Drawing.Size(205, 22);
            this.textEmail1.TabIndex = 10;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(325, 207);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(46, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "Email 1:";
            // 
            // textMobile2
            // 
            this.textMobile2.Location = new System.Drawing.Point(105, 204);
            this.textMobile2.Name = "textMobile2";
            this.textMobile2.Size = new System.Drawing.Size(198, 22);
            this.textMobile2.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(325, 177);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Di động 1:";
            // 
            // textMobile1
            // 
            this.textMobile1.Location = new System.Drawing.Point(413, 174);
            this.textMobile1.Name = "textMobile1";
            this.textMobile1.Size = new System.Drawing.Size(205, 22);
            this.textMobile1.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 207);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Di động 2:";
            // 
            // textTel
            // 
            this.textTel.Location = new System.Drawing.Point(105, 174);
            this.textTel.Name = "textTel";
            this.textTel.Size = new System.Drawing.Size(199, 22);
            this.textTel.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 177);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Điện thoại (NR):";
            // 
            // textPostalCode
            // 
            this.textPostalCode.Location = new System.Drawing.Point(413, 114);
            this.textPostalCode.Name = "textPostalCode";
            this.textPostalCode.Size = new System.Drawing.Size(205, 22);
            this.textPostalCode.TabIndex = 5;
            // 
            // drlCity
            // 
            this.drlCity.FormattingEnabled = true;
            this.drlCity.Location = new System.Drawing.Point(105, 114);
            this.drlCity.Name = "drlCity";
            this.drlCity.Size = new System.Drawing.Size(199, 21);
            this.drlCity.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(325, 117);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Mã bưu chính:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Thành phố:";
            // 
            // textAddress1
            // 
            this.textAddress1.Location = new System.Drawing.Point(105, 55);
            this.textAddress1.Multiline = true;
            this.textAddress1.Name = "textAddress1";
            this.textAddress1.Size = new System.Drawing.Size(514, 43);
            this.textAddress1.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Địa chỉ:";
            // 
            // textFullName
            // 
            this.textFullName.Location = new System.Drawing.Point(105, 22);
            this.textFullName.Name = "textFullName";
            this.textFullName.Size = new System.Drawing.Size(200, 22);
            this.textFullName.TabIndex = 1;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // buttonViewOrder
            // 
            this.errorProvider.SetIconAlignment(this.buttonViewOrder, System.Windows.Forms.ErrorIconAlignment.TopLeft);
            this.buttonViewOrder.Location = new System.Drawing.Point(636, 114);
            this.buttonViewOrder.Name = "buttonViewOrder";
            this.buttonViewOrder.Size = new System.Drawing.Size(111, 25);
            this.buttonViewOrder.TabIndex = 35;
            this.buttonViewOrder.Text = "Xem đơn hàng";
            this.buttonViewOrder.UseVisualStyleBackColor = true;
            this.buttonViewOrder.Click += new System.EventHandler(this.ViewOrder);
            // 
            // CustomerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(753, 522);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "CustomerForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New User";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textFullName;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.TextBox textAddress1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textAddress2;
        private System.Windows.Forms.TextBox textMobile1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textTel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textPostalCode;
        private System.Windows.Forms.ComboBox drlCity;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textEmail2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textEmail1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textMobile2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.MaskedTextBox numberDelivery;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textOtherInformation;
        private System.Windows.Forms.ComboBox drlSegment;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtCompany;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textTenSms;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textMst;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonViewOrder;
    }
}
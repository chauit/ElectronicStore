namespace ElectronicStore.Administration
{
    partial class RoleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoleForm));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Quản lý nhân viên");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Quản lý cấu hình hệ thống");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Quản lý nội dung tin nhắn");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Quản lý nội dung email");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Quản lý tên thành phố");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Quản lý quyền truy cập");
            System.Windows.Forms.TreeNode treeNodeLock = new System.Windows.Forms.TreeNode("Quản lý cập nhật dữ liệu");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Quản trị hệ thống", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNodeLock});
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Quản lý khách hàng");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Quản lý loại sản phẩm");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Quản lý sản phẩm");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Quản lý sản phẩm LD");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Quản lý thiết bị vận chuyển");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Quản trị nội dung", new System.Windows.Forms.TreeNode[] {
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11,
            treeNode12});
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Quản lý đơn hàng");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Quản lý thông tin vận chuyển");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Bảng thông tin đơn hàng");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Quản lý đơn hàng & vận chuyển", new System.Windows.Forms.TreeNode[] {
            treeNode14,
            treeNode15,
            treeNode16});
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textName = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.treeFunctions = new System.Windows.Forms.TreeView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên quyền";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treeFunctions);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(453, 376);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin chung";
            // 
            // textName
            // 
            this.textName.Location = new System.Drawing.Point(126, 22);
            this.textName.Name = "textName";
            this.textName.Size = new System.Drawing.Size(304, 22);
            this.textName.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(355, 382);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 25);
            this.button2.TabIndex = 4;
            this.button2.Text = "Thoát   ";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.CancelItem);
            // 
            // buttonSave
            // 
            this.buttonSave.Image = ((System.Drawing.Image)(resources.GetObject("buttonSave.Image")));
            this.buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSave.Location = new System.Drawing.Point(268, 382);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(81, 25);
            this.buttonSave.TabIndex = 3;
            this.buttonSave.Text = "Cập nhật";
            this.buttonSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.SaveItem);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Danh mục";
            // 
            // treeFunctions
            // 
            this.treeFunctions.CheckBoxes = true;
            this.treeFunctions.Location = new System.Drawing.Point(126, 63);
            this.treeFunctions.Name = "treeFunctions";
            treeNode1.Name = "Node01";
            treeNode1.Text = "Quản lý nhân viên";
            treeNode2.Name = "Node02";
            treeNode2.Text = "Quản lý cấu hình hệ thống";
            treeNode3.Name = "Node03";
            treeNode3.Text = "Quản lý nội dung tin nhắn";
            treeNode4.Name = "Node04";
            treeNode4.Text = "Quản lý nội dung email";
            treeNode5.Name = "Node05";
            treeNode5.Text = "Quản lý tên thành phố";
            treeNode6.Name = "Node06";
            treeNode6.Text = "Quản lý quyền truy cập";
            treeNode7.Name = "Node0";
            treeNode7.Text = "Quản trị hệ thống";
            treeNode8.Name = "Node11";
            treeNode8.Text = "Quản lý khách hàng";
            treeNode9.Name = "Node12";
            treeNode9.Text = "Quản lý loại sản phẩm";
            treeNode10.Name = "Node13";
            treeNode10.Text = "Quản lý sản phẩm";
            treeNode11.Name = "Node14";
            treeNode11.Text = "Quản lý sản phẩm LD";
            treeNode12.Name = "Node15";
            treeNode12.Text = "Quản lý thiết bị vận chuyển";
            treeNode13.Name = "Node1";
            treeNode13.Text = "Quản trị nội dung";
            treeNode14.Name = "Node31";
            treeNode14.Text = "Quản lý đơn hàng";
            treeNode15.Name = "Node32";
            treeNode15.Text = "Quản lý thông tin vận chuyển";
            treeNode16.Name = "Node33";
            treeNode16.Text = "Bảng thông tin đơn hàng";
            treeNode17.Name = "Node3";
            treeNode17.Text = "Quản lý đơn hàng & vận chuyển";
            this.treeFunctions.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode13,
            treeNode17});
            this.treeFunctions.Size = new System.Drawing.Size(304, 307);
            this.treeFunctions.TabIndex = 2;
            this.treeFunctions.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.SelectedNode);
            // 
            // RoleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(453, 419);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.button2);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "RoleForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quyền truy cập";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textName;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TreeView treeFunctions;
    }
}
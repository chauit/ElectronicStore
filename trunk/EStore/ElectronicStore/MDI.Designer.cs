namespace ElectronicStore
{
    partial class MDI
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Quản lý nhân viên");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Quản lý cấu hình hệ thống");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Quản lý quyền truy cập");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Quản lý nội dung tin nhắn");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Quản lý nội dung email");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Quản lý tên thành phố");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Quản trị hệ thống", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6});
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDI));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.loginMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.changePasswordMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView = new System.Windows.Forms.TreeView();
            this.textSearchCustomer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(737, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginMenuItem,
            this.toolStripSeparator3,
            this.changePasswordMenuItem,
            this.toolStripSeparator5,
            this.exitToolStripMenuItem});
            this.fileMenu.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder;
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(37, 20);
            this.fileMenu.Text = "&File";
            // 
            // loginMenuItem
            // 
            this.loginMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.loginMenuItem.Name = "loginMenuItem";
            this.loginMenuItem.Size = new System.Drawing.Size(145, 22);
            this.loginMenuItem.Text = "Đăng nhập";
            this.loginMenuItem.Click += new System.EventHandler(this.Login);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(142, 6);
            // 
            // changePasswordMenuItem
            // 
            this.changePasswordMenuItem.Name = "changePasswordMenuItem";
            this.changePasswordMenuItem.Size = new System.Drawing.Size(145, 22);
            this.changePasswordMenuItem.Text = "Đổi mật khẩu";
            this.changePasswordMenuItem.Click += new System.EventHandler(this.ChangePassword);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(142, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.exitToolStripMenuItem.Text = "Thoát";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitForm);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 501);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip.Size = new System.Drawing.Size(737, 22);
            this.statusStrip.TabIndex = 2;
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Size = new System.Drawing.Size(737, 477);
            this.splitContainer1.SplitterDistance = 213;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 4;
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            treeNode1.Name = "nodeUser";
            treeNode1.Text = "Quản lý nhân viên";
            treeNode2.Name = "nodeConfiguration";
            treeNode2.Text = "Quản lý cấu hình hệ thống";
            treeNode3.Name = "Node0";
            treeNode3.Text = "Quản lý quyền truy cập";
            treeNode4.Name = "nodeSms";
            treeNode4.Text = "Quản lý nội dung tin nhắn";
            treeNode5.Name = "nodeEmail";
            treeNode5.Text = "Quản lý nội dung email";
            treeNode6.Name = "Node0";
            treeNode6.Text = "Quản lý tên thành phố";
            treeNode7.Name = "Node0";
            treeNode7.Text = "Quản trị hệ thống";
            treeNode8.Name = "nodeCustomer";
            treeNode8.Text = "Quản lý khách hàng";
            treeNode9.Name = "nodeProductType";
            treeNode9.Text = "Quản lý loại sản phẩm";
            treeNode10.Name = "nodeProduct";
            treeNode10.Text = "Quản lý sản phẩm";
            treeNode11.Name = "Node0";
            treeNode11.Text = "Quản lý sản phẩm LD";
            treeNode12.Name = "nodeVehicle";
            treeNode12.Text = "Quản lý thiết bị vận chuyển";
            treeNode13.Name = "Node3";
            treeNode13.Text = "Quản trị nội dung";
            treeNode14.Name = "nodeOrder";
            treeNode14.Text = "Quản lý đơn hàng";
            treeNode15.Name = "nodeDelivery";
            treeNode15.Text = "Quản lý thông tin vận chuyển";
            treeNode16.Name = "Node0";
            treeNode16.Text = "Bảng thông tin đơn hàng";
            treeNode17.Name = "Node7";
            treeNode17.Text = "Quản lý đơn hàng & vận chuyển";
            this.treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode13,
            treeNode17});
            this.treeView.Size = new System.Drawing.Size(213, 477);
            this.treeView.TabIndex = 0;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.SelectNode);
            // 
            // textSearchCustomer
            // 
            this.textSearchCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textSearchCustomer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textSearchCustomer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textSearchCustomer.CausesValidation = false;
            this.textSearchCustomer.Location = new System.Drawing.Point(402, 0);
            this.textSearchCustomer.Name = "textSearchCustomer";
            this.textSearchCustomer.Size = new System.Drawing.Size(238, 23);
            this.textSearchCustomer.TabIndex = 1;
            this.textSearchCustomer.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Menu;
            this.label1.Location = new System.Drawing.Point(271, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Tìm kiếm khách hàng:";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(646, 0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(82, 23);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // MDI
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(737, 523);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textSearchCustomer);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MDI";
            this.Text = "Electronic Store";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem loginMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePasswordMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.TextBox textSearchCustomer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearch;
    }
}




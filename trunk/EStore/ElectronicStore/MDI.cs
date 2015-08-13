using System;
using System.Windows.Forms;
using ElectronicStore.Administration;
using ElectronicStore.Main;
using ElectronicStore.Reference;
using Model;
using Business;

namespace ElectronicStore
{
    public partial class MDI : Form
    {
        public User CurrentUser { get; set; }

        public void UpdateStatus(string value)
        {
            toolStripStatusLabel.Text = value;
        }

        public MDI()
        {
            InitializeComponent();
            treeView.ExpandAll();
            FormLoad();
        }

        private void FormLoad()
        {
            if (CurrentUser == null)
            {
                treeView.Visible = false;
                loginMenuItem.Text = "Đăng nhập";
                changePasswordMenuItem.Visible = false;
                toolStripSeparator3.Visible = false;
                textSearchCustomer.Visible = false;
                ShowLogin();
            }

        }

        private void ShowLogin()
        {
            var login = new LoginForm();
            var result = login.ShowDialog();
            if (result == DialogResult.OK)
            {
                treeView.Visible = true;
                loginMenuItem.Text = "Đăng xuất";
                toolStripSeparator3.Visible = true;
                changePasswordMenuItem.Visible = true;
                textSearchCustomer.Visible = true;
                CurrentUser = login.Result;

                LoadCustomer();
                ShowMenu();
            }
        }

        private void LoadCustomer()
        {
            var biz = new CustomerBiz();
            var items = biz.LoadAllCustomerName();
            AutoCompleteStringCollection list = new AutoCompleteStringCollection();
            list.AddRange(items.ToArray());
            textSearchCustomer.AutoCompleteCustomSource = null;
            textSearchCustomer.AutoCompleteCustomSource = list;
        }

        private void OpenForm(Form form)
        {
            splitContainer1.Panel2.Controls.Clear();
            splitContainer1.Panel2.Controls.Add(form);

            form.Show();
        }

        private void ExitForm(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ChangePassword(object sender, EventArgs e)
        {
            var changePassword = new ChangePasswordForm(CurrentUser.Username);
            var result = changePassword.ShowDialog();
            if (result == DialogResult.OK)
            {
                CurrentUser = null;

                treeView.Visible = false;
                loginMenuItem.Text = "Đăng nhập";
                changePasswordMenuItem.Visible = false;
                toolStripSeparator3.Visible = false;
                ShowLogin();
            }
        }

        private void Login(object sender, EventArgs e)
        {
            if (string.Equals(loginMenuItem.Text, "Đăng nhập", StringComparison.InvariantCultureIgnoreCase))
            {
                ShowLogin();
            }
            else
            {
                CurrentUser = null;
                treeView.Visible = false;
                loginMenuItem.Text = "Đăng nhập";
                changePasswordMenuItem.Visible = false;
                toolStripSeparator3.Visible = false;
                textSearchCustomer.Visible = false;
                splitContainer1.Panel2.Controls.Clear();                
            }
        }

        private void SelectNode(object sender, TreeViewEventArgs e)
        {
            string name = treeView.SelectedNode.Text;

            switch (name)
            {
                case "Quản lý nhân viên":
                    var userView = new UserView { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(userView);
                    break;
                case "Quản lý cấu hình hệ thống":
                    var configurationView = new ConfigurationView(CurrentUser) { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(configurationView);
                    break;
                case "Quản lý nội dung tin nhắn":
                    var smsView = new SmsView(CurrentUser) { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(smsView);
                    break;
                case "Quản lý nội dung email":
                    var emailView = new EmailView(CurrentUser) { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(emailView);
                    break;

                case "Quản lý khách hàng":
                    var customerView = new CustomerView(CurrentUser) { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(customerView);
                    break;
                case "Quản lý loại sản phẩm":
                    var productTypeView = new ProductTypeView(CurrentUser) { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(productTypeView);
                    break;
                case "Quản lý sản phẩm":
                    var productView = new ProductView(CurrentUser) { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(productView);
                    break;
                case "Quản lý sản phẩm LD":
                    var productLDView = new ProductLDView(CurrentUser) { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(productLDView);
                    break;                    
                case "Quản lý thiết bị vận chuyển":
                    var vehicleView = new VehicleView(CurrentUser) { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(vehicleView);
                    break;
                case "Quản lý đơn hàng":
                    var orderView = new OrderView(CurrentUser) { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(orderView);
                    break;
                case "Quản lý thông tin vận chuyển":
                    var mainView = new MainView(CurrentUser) { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(mainView);
                    break;
                case "Bảng thông tin đơn hàng":
                    var frm = new DashboardForm { Dock = DockStyle.Fill, TopLevel = true };
                    frm.ShowDialog();
                    break;
                case "Quản lý tên thành phố":
                    var cityView = new CityView(CurrentUser) { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(cityView);
                    break;
                case "Quản lý quyền truy cập":
                    var roleView = new RoleView(CurrentUser) { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(roleView);
                    break;                     
                default:
                    break;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textSearchCustomer.Text))
            {
                var searchCustomer = new SearchCustomer(CurrentUser, textSearchCustomer.Text);
                searchCustomer.ShowDialog();
                textSearchCustomer.Text = string.Empty;
            }
        }

        private void ShowMenu()
        {            
            var biz = new RoleBiz();
            var role = biz.LoadItem(CurrentUser.Type);

            UpdateTreeView(treeView.Nodes[0], role.Functions);
        }

        private void UpdateTreeView(TreeNode node, string functions)
        {
            if(node.Parent == null)
            {
                if(node.NextNode != null)
                {
                    UpdateTreeView(node.NextNode, functions);
                }
                UpdateTreeView(node.FirstNode, functions);
                if(node.Nodes.Count == 0)
                {
                    node.Remove();
                }
            }
            else
            {
                if(node.NextNode != null)
                {
                    UpdateTreeView(node.NextNode, functions);
                }

                if (functions.IndexOf(node.Text) == -1)
                {
                    node.Remove();
                }
                else
                {
                    return;
                }
            }
        }

        private void CreateMenu()
        {
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
        }
    }
}

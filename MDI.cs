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
                CreateMenu();
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
            if(splitContainer1.Panel2.Controls.Count > 0)
            {
                var childForm = splitContainer1.Panel2.Controls[0] as Form;
                if (childForm != null)
                {
                    childForm.Close();
                }
            }

            splitContainer1.Panel2.Controls.Clear();
            splitContainer1.Panel2.Controls.Add(form);

            form.Show();
        }

        private void ExitForm(object sender, EventArgs e)
        {
            if (splitContainer1.Panel2.Controls.Count > 0)
            {
                var childForm = splitContainer1.Panel2.Controls[0] as Form;
                if (childForm != null)
                {
                    childForm.Close();
                }
            }
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
                case "Quản lý cập nhật dữ liệu":
                    var lockingView = new LockingView(CurrentUser) { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(lockingView);
                    break;
                case "Quản lý thông tin email":
                    var emailDataView = new EmailDataView(CurrentUser) { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(emailDataView);
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
            TreeNode treeNode1 = new TreeNode("Quản lý nhân viên");
            TreeNode treeNode2 = new TreeNode("Quản lý cấu hình hệ thống");
            TreeNode treeNode3 = new TreeNode("Quản lý quyền truy cập");
            TreeNode treeNode4 = new TreeNode("Quản lý nội dung tin nhắn");
            TreeNode treeNode5 = new TreeNode("Quản lý nội dung email");
            TreeNode treeNode6 = new TreeNode("Quản lý tên thành phố");
            TreeNode treeNodeEmailData = new TreeNode("Quản lý thông tin email");
            TreeNode treeNodeSmsData = new TreeNode("Quản lý thông tin tin nhắn");
            TreeNode treeNodeLock = new TreeNode("Quản lý cập nhật dữ liệu");
            TreeNode treeNode7 = new TreeNode("Quản trị hệ thống", new TreeNode[] {
            treeNode1,            treeNode2,            treeNode3,            treeNode4,            treeNode5,            treeNode6,    treeNodeEmailData,treeNodeSmsData,  treeNodeLock});

            TreeNode treeNode8 = new TreeNode("Quản lý khách hàng");
            TreeNode treeNode9 = new TreeNode("Quản lý loại sản phẩm");
            TreeNode treeNode10 = new TreeNode("Quản lý sản phẩm");
            TreeNode treeNode11 = new TreeNode("Quản lý sản phẩm LD");
            TreeNode treeNode12 = new TreeNode("Quản lý thiết bị vận chuyển");
            TreeNode treeNode13 = new TreeNode("Quản trị nội dung", new TreeNode[] {
            treeNode8,            treeNode9,            treeNode10,            treeNode11,            treeNode12});

            TreeNode treeNode14 = new TreeNode("Quản lý đơn hàng");
            TreeNode treeNode15 = new TreeNode("Quản lý thông tin vận chuyển");
            TreeNode treeNode16 = new TreeNode("Bảng thông tin đơn hàng");
            TreeNode treeNode17 = new TreeNode("Quản lý đơn hàng & vận chuyển", new TreeNode[] {
            treeNode14,            treeNode15,            treeNode16});

            treeView.Nodes.Clear();
            treeView.Nodes.Add(treeNode7);
            treeView.Nodes.Add(treeNode13);
            treeView.Nodes.Add(treeNode17);
            treeView.ExpandAll();
        }

        private void FormClosedEvent(object sender, FormClosedEventArgs e)
        {
            if (splitContainer1.Panel2.Controls.Count > 0)
            {
                var childForm = splitContainer1.Panel2.Controls[0] as Form;
                if (childForm != null)
                {
                    childForm.Close();
                }
            }
        }
    }
}

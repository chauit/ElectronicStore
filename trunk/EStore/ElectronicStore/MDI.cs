using System;
using System.Windows.Forms;
using ElectronicStore.Administration;
using ElectronicStore.Common;
using ElectronicStore.Main;
using ElectronicStore.Reference;
using Model;

namespace ElectronicStore
{
    public partial class MDI : Form
    {
        public User CurrentUser { get; set; }

        public MDI()
        {
            InitializeComponent();
            treeView.ExpandAll();  
            LicenseUtil utilities = new LicenseUtil();
            var license = utilities.Read("License");
            if (!StringComparer.OrdinalIgnoreCase.Equals(license, "ElectronicStore"))
            {
                Application.Exit();
            }

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
                ShowLogin();
            }
        }

        private void ShowLogin()
        {
            var login = new LoginForm();
            var result = login.ShowDialog();
            if(result == System.Windows.Forms.DialogResult.OK)
            {
                treeView.Visible = true;
                loginMenuItem.Text = "Đăng xuất";
                toolStripSeparator3.Visible = true;
                changePasswordMenuItem.Visible = true;
                CurrentUser = login.Result;
            }
        }

        RoleForm roleForm;
        RoleView roleView;

        public void ViewRoles(object sender, EventArgs e)
        {
            roleView = new RoleView { MdiParent = this, Dock = DockStyle.Fill };
            roleView.FormClosed += delegate { roleView = null; };
            roleView.Show();            
        }

        public void NewRole(object sender, EventArgs e)
        {
            roleForm = new RoleForm { MdiParent = this, Dock = DockStyle.Fill };
            roleForm.FormClosed += delegate { roleForm = null; };
            roleForm.Show();
        }

        public void ViewUsers(object sender, EventArgs e)
        {
            roleForm = new RoleForm { MdiParent = this, Dock = DockStyle.Fill };
            roleForm.FormClosed += delegate { roleForm = null; };
            roleForm.Show();   
        }

        private void OpenForm(Form form)
        {
            splitContainer1.Panel2.Controls.Clear();            
            splitContainer1.Panel2.Controls.Add(form);            
            
            form.Show();            
        }

        private void SelectNode(object sender, TreeNodeMouseClickEventArgs e)
        {
            string name = treeView.SelectedNode.Text;

            switch (name)
            {
                case "Manage Role":
                    RoleView roleView = new RoleView { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(roleView);
                    break;
                case "Manage User":
                    UserView userView = new UserView { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(userView);
                    break;
                default:
                    break;
            }
        }

        private void ExitForm(object sender, EventArgs e)
        {            
            Application.Exit();
        }

        private void ChangePassword(object sender, EventArgs e)
        {
            var changePassword = new ChangePasswordForm(CurrentUser.Username);
            var result = changePassword.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
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
            if(string.Equals(loginMenuItem.Text,"Đăng nhập", StringComparison.InvariantCultureIgnoreCase))
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
            }
        }        

        private void SelectNode(object sender, TreeViewEventArgs e)
        {
            string name = treeView.SelectedNode.Text;

            switch (name)
            {
                case "Manage Role":
                    RoleView roleView = new RoleView { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(roleView);
                    break;
                case "Quản lý nhân viên":
                    UserView userView = new UserView { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(userView);
                    break;
                case "Quản lý cấu hình hệ thống":
                    ConfigurationView configurationView = new ConfigurationView { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(configurationView);
                    break;
                case "Quản lý nội dung tin nhắn":
                    SmsView smsView = new SmsView { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(smsView);
                    break;
                case "Quản lý nội dung email":
                    EmailView emailView = new EmailView { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(emailView);
                    break;

                case "Quản lý khách hàng":
                    CustomerView customerView = new CustomerView { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(customerView);
                    break;
                case "Quản lý loại sản phẩm":
                    ProductTypeView productTypeView = new ProductTypeView { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(productTypeView);
                    break;
                case "Quản lý sản phẩm":
                    ProductView productView = new ProductView { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(productView);
                    break;
                case "Quản lý thiết bị vận chuyển":
                    VehicleView vehicleView = new VehicleView { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(vehicleView);
                    break;

                case "Quản lý đơn hàng":
                    OrderView orderView = new OrderView { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(orderView);
                    break;
                case "Quản lý thông tin vận chuyển":
                    DeliveryView deliveryView = new DeliveryView { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(deliveryView);
                    break;
                default:
                    break;
            }
        }
    }
}

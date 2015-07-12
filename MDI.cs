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
                case "Quản lý thiết bị vận chuyển":
                    var vehicleView = new VehicleView(CurrentUser) { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(vehicleView);
                    break;
                case "Quản lý đơn hàng":
                    var orderView = new OrderView(CurrentUser) { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(orderView);
                    break;
                case "Quản lý thông tin vận chuyển":
                    var deliveryView = new DeliveryView(CurrentUser) { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(deliveryView);
                    break;
                case "Bảng thông tin đơn hàng":
                    var frm = new DashboardForm { Dock = DockStyle.Fill, TopLevel = true };
                    frm.ShowDialog();
                    break;
                case "Quản lý tên thành phố":
                    var cityView = new CityView(CurrentUser) { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(cityView);
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

    }
}

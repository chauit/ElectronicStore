using System;
using System.Windows.Forms;
using ElectronicStore.Administration;
using ElectronicStore.Common;
using ElectronicStore.Main;
using ElectronicStore.Reference;

namespace ElectronicStore
{
    public partial class MDI : Form
    {
        private int childFormNumber = 0;

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
            throw new NotImplementedException();
        }

        private void Login(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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
                case "Manage User":
                    UserView userView = new UserView { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(userView);
                    break;
                case "Manage Configuration":
                    ConfigurationView configurationView = new ConfigurationView { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(configurationView);
                    break;
                case "Manage SMS":
                    SmsView smsView = new SmsView { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(smsView);
                    break;
                case "Manage Email":
                    EmailView emailView = new EmailView { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(emailView);
                    break;

                case "Manage Customer":
                    CustomerView customerView = new CustomerView { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(customerView);
                    break;
                case "Manage Product Type":
                    ProductTypeView productTypeView = new ProductTypeView { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(productTypeView);
                    break;
                case "Manage Product":
                    ProductView productView = new ProductView { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(productView);
                    break;
                case "Manage Vehicle":
                    VehicleView vehicleView = new VehicleView { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(vehicleView);
                    break;

                case "Manage Order":
                    OrderView orderView = new OrderView { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(orderView);
                    break;
                case "Manage Delivery":
                    DeliveryView deliveryView = new DeliveryView { Dock = DockStyle.Fill, TopLevel = false };
                    OpenForm(deliveryView);
                    break;
                default:
                    break;
            }
        }
    }
}

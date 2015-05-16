using Business;
using ElectronicStore.Common;
using Model;
using System;
using System.Windows.Forms;

namespace ElectronicStore.Main
{
    public partial class OrderDetailForm : Form
    {
        private int itemId;

        private void InitForm()
        {
            buttonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        public OrderDetailForm()
        {
            InitializeComponent();

            drlVehicle.Focus();
            itemId = 0;

            InitForm();
        }

        public OrderDetailForm(int id)
        {
            InitializeComponent();

            drlVehicle.Focus();
            itemId = id;

            InitForm();
        }

        private void SaveItem(object sender, EventArgs e)
        {
            if (CustomValidation())
            {
                var item = new Role();
                

                var biz = new RoleBiz();
                biz.SaveItem(item);

                this.Close();
            }
        }

        private void CancelItem(object sender, EventArgs e)
        {
            var parent = this.Parent as SplitterPanel;

            parent.Controls.Clear();

            var deliveryView = new DeliveryView { Dock = DockStyle.Fill, TopLevel = false };
            parent.Controls.Add(deliveryView);
            deliveryView.Show();

            this.Close();
        }

        private bool CustomValidation()
        {
            bool hasError = true;            
            errorProvider.Clear();

            if (string.IsNullOrEmpty(drlVehicle.SelectedText))
            {
                errorProvider.SetError(drlVehicle, Constants.Messages.RequireMessage);
                hasError = false;

                drlVehicle.Focus();
            }

            return hasError;
        }
    }
}

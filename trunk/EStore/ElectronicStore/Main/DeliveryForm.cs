using Business;
using ElectronicStore.Common;
using Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ElectronicStore.Main
{
    public partial class DeliveryForm : Form
    {
        private int itemId;

        private List<SearchOrder> listOrder;

        private void InitForm()
        {
            buttonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;

            LoadVehicle();
        }

        public DeliveryForm()
        {
            InitializeComponent();

            drlVehicle.Focus();
            itemId = 0;

            labelDeliveryNo.Text = string.Empty;
            labelStatus.Text = Constants.DeliveryStatusDraft;
            dateStartDate.Value = DateTime.Now;
            dateTimeStartTime.Value = DateTime.Now.AddHours(1);

            InitForm();
        }

        public DeliveryForm(int id)
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

            if (drlVehicle.SelectedItem == null || Convert.ToInt32(drlVehicle.SelectedValue) == 0)
            {
                errorProvider.SetError(drlVehicle, Constants.Messages.RequireMessage);
                hasError = false;

                drlVehicle.Focus();
            }

            return hasError;
        }

        private void LoadVehicle()
        {
            var biz = new VehicleBiz();
            var items = biz.LoadItems();
            items.Insert(0, new Vehicle());

            drlVehicle.Items.Clear();
            drlVehicle.DataSource = items;
            drlVehicle.DisplayMember = "Name";
            drlVehicle.ValueMember = "Id";
        }

        private void SelectOrder(object sender, EventArgs e)
        {
            var dialog = new SearchOrder();
            var result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                var order = dialog.SelectedOrder;
                if (order != null)
                {
                    if (listOrder == null)
                    {
                        listOrder = new List<SearchOrder>();
                    }

                    listOrder.Add(order);

                    dataGridView.DataSource = listOrder;
                }
            }
        }
    }
}

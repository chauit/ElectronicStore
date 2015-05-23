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
        private DateTime? created;
        private int? createdBy;
        private DateTime? modified;
        private int? modifiedBy;
        private int currentUser;

        private int itemId;

        private List<SearchOrder> listOrder;

        private void InitForm()
        {
            buttonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;

            //TODO: Get Id from Login page
            currentUser = 6;

            dataGridView.AutoGenerateColumns = false;

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
                var item = new Delivery();
                item.DeliveryDate = dateStartDate.Value;
                item.StartTime = dateTimeStartTime.Value;
                item.OtherInformation = textOtherInformation.Text;
                item.VehicleId = Convert.ToInt32(drlVehicle.SelectedValue);

                if (itemId > 0)
                {
                    item.Id = itemId;
                    item.Created = created;
                    item.CreatedByUserId = createdBy;

                    item.Modified = DateTime.Now;
                    item.ModifiedByUserId = currentUser;

                    var biz = new DeliveryBiz();
                    biz.UpdateItem(item);
                }
                else
                {
                    item.Status = Constants.DeliveryStatusDraft;
                    item.Created = DateTime.Now;
                    item.CreatedByUserId = currentUser;

                    item.Modified = DateTime.Now;
                    item.ModifiedByUserId = currentUser;

                    var biz = new DeliveryBiz();
                    biz.SaveItem(item);
                }

                var parent = this.Parent as SplitterPanel;
                parent.Controls.Clear();

                var deliveryView = new DeliveryView { Dock = DockStyle.Fill, TopLevel = false };
                parent.Controls.Add(deliveryView);
                deliveryView.Show();

                this.Close();
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.None;
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
            var dialog = new FindOrder();
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
                                        
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = listOrder;
                    dataGridView.Refresh();
                }
            }
        }

        private void DownOrder(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                listOrder = dataGridView.DataSource as List<SearchOrder>;

                int index = dataGridView.SelectedRows[0].Index;
                if (index < dataGridView.RowCount - 1)
                {
                    var temp = listOrder[index];
                    listOrder.RemoveAt(index);
                    listOrder.Insert(index + 1, temp);

                    dataGridView.ClearSelection();
                    dataGridView.Rows[index + 1].Selected = true;
                    dataGridView.Refresh();
                }
            }
        }

        private void UpOrder(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                listOrder = dataGridView.DataSource as List<SearchOrder>;

                int index = dataGridView.SelectedRows[0].Index;
                if (index >= 1)
                {
                    var temp = listOrder[index];
                    listOrder.RemoveAt(index);
                    listOrder.Insert(index - 1, temp);

                    dataGridView.ClearSelection();
                    dataGridView.Rows[index - 1].Selected = true;
                    dataGridView.Refresh();
                }
            }
        }

        private void SaveOrders(Delivery delivery)
        {

        }
    }
}

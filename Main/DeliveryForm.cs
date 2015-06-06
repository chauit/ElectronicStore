using Business;
using ElectronicStore.Common;
using Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ElectronicStore.Main
{
    public partial class DeliveryForm : Form
    {
        private DateTime? created;
        private int? createdBy;
        private DateTime? modified;
        private int? modifiedBy;
        private User currentUser;

        private int itemId;

        private List<SearchOrder> listOrder;

        private void InitForm(User user)
        {
            buttonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;

            currentUser = user;

            dataGridView.AutoGenerateColumns = false;

            buttonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;

            LoadVehicle();

            listOrder = new List<SearchOrder>();
        }

        public DeliveryForm(User user)
        {
            InitializeComponent();

            drlVehicle.Focus();
            itemId = 0;

            labelDeliveryNo.Text = string.Empty;
            labelStatus.Text = Constants.DeliveryStatusDraft;
            dateStartDate.Value = DateTime.Now;
            dateTimeStartTime.Value = DateTime.Now.AddHours(1);

            InitForm(user);

            this.Text = "Thêm thông tin giao hàng";
        }

        public DeliveryForm(int id, User user)
        {
            InitializeComponent();

            InitForm(user);

            drlVehicle.Focus();
            itemId = id;

            var biz = new DeliveryBiz();
            var item = biz.LoadItem(id);

            created = item.Created;
            createdBy = item.CreatedByUserId;
            modified = item.Modified;
            modifiedBy = item.ModifiedByUserId;

            labelDeliveryNo.Text = item.DeliveryNo;
            labelStatus.Text = item.Status;
            labelSendSms.Text = item.IsSendSms;
            labelSendEmail.Text = item.IsSendEmail;

            if (item.DeliveryDate.HasValue)
            {
                dateStartDate.Value = item.DeliveryDate.Value;
            }
            if (item.StartTime.HasValue)
            {
                dateTimeStartTime.Value = DateTime.Now + item.StartTime.Value;
            }

            textOtherInformation.Text = item.OtherInformation;
            if (item.VehicleId.HasValue)
            {
                drlVehicle.SelectedValue = item.VehicleId.Value;
            }

            LoadOrders(item);

            this.Text = "Cập nhật thông tin giao hàng";            
        }

        private void SaveItem(object sender, EventArgs e)
        {
            if (CheckSecurity(itemId))
            {
                var parent = this.Parent as SplitterPanel;
                parent.Controls.Clear();

                var orderView = new DeliveryView(currentUser) { Dock = DockStyle.Fill, TopLevel = false };
                parent.Controls.Add(orderView);
                orderView.Show();

                this.Close();
            }
            else
            {
                if (CustomValidation())
                {
                    var item = new Delivery();
                    SaveDelivery(item);

                    var parent = this.Parent as SplitterPanel;
                    parent.Controls.Clear();

                    var deliveryView = new DeliveryView(currentUser) { Dock = DockStyle.Fill, TopLevel = false };
                    parent.Controls.Add(deliveryView);
                    deliveryView.Show();

                    this.Close();
                }
                else
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.None;
                }
            }
        }

        private Delivery SaveDelivery(Delivery item)
        {            
            item.DeliveryDate = dateStartDate.Value;
            item.StartTime = TimeSpan.Parse(dateTimeStartTime.Value.ToString(@"hh\:mm"));
            item.OtherInformation = textOtherInformation.Text;
            item.VehicleId = Convert.ToInt32(drlVehicle.SelectedValue);

            if (itemId > 0)
            {
                item.Id = itemId;
                item.Created = created;
                item.CreatedByUserId = createdBy;

                if (string.IsNullOrEmpty(item.IsSendEmail))
                {
                    item.IsSendEmail = labelSendEmail.Text;
                }

                if (string.IsNullOrEmpty(item.IsSendSms))
                {
                    item.IsSendSms = labelSendSms.Text;
                }

                item.DeliveryNo = labelDeliveryNo.Text;
                item.Status = labelStatus.Text;

                item.Modified = DateTime.Now;
                item.ModifiedByUserId = currentUser.Id;

                var biz = new DeliveryBiz();
                biz.UpdateItem(item);
            }
            else
            {
                item.Status = Constants.DeliveryStatusDraft;
                item.Created = DateTime.Now;
                item.CreatedByUserId = currentUser.Id;
                if (string.IsNullOrEmpty(item.IsSendEmail))
                {
                    item.IsSendEmail = Constants.DeliverySendEmail;
                }

                if (string.IsNullOrEmpty(item.IsSendSms))
                {
                    item.IsSendSms = Constants.DeliverySendSms;
                }

                item.Modified = DateTime.Now;
                item.ModifiedByUserId = currentUser.Id;

                var biz = new DeliveryBiz();
                biz.SaveItem(item);
            }

            SaveOrders(item);

            return item;
        }

        private void CancelItem(object sender, EventArgs e)
        {
            var parent = this.Parent as SplitterPanel;

            parent.Controls.Clear();

            var deliveryView = new DeliveryView(currentUser) { Dock = DockStyle.Fill, TopLevel = false };
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

            string ids = string.Empty;
            foreach(var o in listOrder)
            {
                ids = string.Concat(ids, ",", o.Id);
            }

            ids = ids.TrimStart(',');

            var biz = new DeliveryDetailBiz();
            var orderIds = biz.GetByOrders(ids);
            if(orderIds != null && orderIds.Count > 0)
            {
                hasError = false;

                for (int i = 0; i < listOrder.Count; i++)
                {
                    if (orderIds.Contains(listOrder[i].Id))
                    {
                        dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        dataGridView.Refresh();
                    }
                }
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
            var dialog = new FindOrder(listOrder);
            dialog.ParentForm = this;
            dialog.ShowDialog();  
        }

        public void UpdateGrid(SearchOrder order)
        {
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
            var biz = new DeliveryDetailBiz();
            biz.RemoveItemsByDeliveryId(delivery.Id);            

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                var item = row.DataBoundItem as SearchOrder;

                var detail = new DeliveryDetail();
                detail.DeliveryId = delivery.Id;
                detail.OrderId = item.Id;
                if (item.DeliveryTime.HasValue)
                {
                    detail.Time = item.DeliveryTime.Value;
                }
                detail.Index = row.Index;

                biz.SaveItem(detail);
            }
        }

        private void LoadOrders(Delivery delivery)
        {
            if (delivery.Details.Count == 0) return;

            listOrder = new List<SearchOrder>();

            foreach (var detail in delivery.Details)
            {
                var searchOrder = new SearchOrder();
                searchOrder.DeliveryTime = detail.Time;
                searchOrder.Status = detail.Order.Status;
                searchOrder.CustomerName = detail.Order.CustomerName;
                searchOrder.OrderNo = detail.Order.OrderNo;
                searchOrder.Id = detail.Order.Id;
                searchOrder.DeliveryInternal = detail.Order.DeliveryInternal;

                listOrder.Add(searchOrder);
            }

            dataGridView.DataSource = null;
            dataGridView.DataSource = listOrder;
            dataGridView.Refresh();
        }

        private void DeleteOrder(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                int index = dataGridView.SelectedRows[0].Index;
                listOrder.RemoveAt(index);
                
                dataGridView.DataSource = null;
                dataGridView.DataSource = listOrder;
                dataGridView.Refresh();
            }
        }

        private void SaveAndSendMessages(object sender, EventArgs e)
        {
            if (CustomValidation())
            {
                var delivery = new Delivery();
                delivery.IsSendSms = Constants.DeliverySentSms;

                var item = SaveDelivery(delivery);

                var biz = new DeliveryBiz();
                biz.SendSms(item.Id);

                var parent = this.Parent as SplitterPanel;
                parent.Controls.Clear();

                var deliveryView = new DeliveryView(currentUser) { Dock = DockStyle.Fill, TopLevel = false };
                parent.Controls.Add(deliveryView);
                deliveryView.Show();

                this.Close();
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.None;
            }
        }

        private void SaveAndSendEmail(object sender, EventArgs e)
        {
            if (CustomValidation())
            {
                var delivery = new Delivery();
                delivery.IsSendEmail = Constants.DeliverySentEmail;

                var item = SaveDelivery(delivery);

                var biz = new DeliveryBiz();
                biz.SendEmail(item.Id);

                var parent = this.Parent as SplitterPanel;
                parent.Controls.Clear();

                var deliveryView = new DeliveryView(currentUser) { Dock = DockStyle.Fill, TopLevel = false };
                parent.Controls.Add(deliveryView);
                deliveryView.Show();

                this.Close();
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.None;
            }
        }

        private bool CheckSecurity(int id)
        {
            if (id == 0) return false;

            var biz = new DeliveryBiz();
            var current = biz.LoadItem(id);
            if (current != null)
            {
                if (current.Modified.Value != modified.Value)
                {
                    MessageBox.Show(Constants.Messages.ConflictOrderMessage);
                    return true;
                }
            }

            return false;
        }
    }
}

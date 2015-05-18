using Business;
using ElectronicStore.Common;
using Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ElectronicStore.Main
{
    public partial class OrderForm : Form
    {
        private int itemId;

        private DateTime? created;
        private int? createdBy;
        private DateTime? modified;
        private int? modifiedBy;

        private int currentUser;

        public List<int> removedItems;
        private List<SearchProduct> listProduct;
        private bool HasFooter;

        private void InitForm()
        {
            buttonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;

            //TODO: Get Id from Login page
            currentUser = 6;

            LoadCustomer();
            removedItems = new List<int>();

            dataGridView.AutoGenerateColumns = false;
        }        

        public OrderForm()
        {
            InitializeComponent();

            dateOrderDate.Focus();
            itemId = 0;

            InitForm();

            this.Text = "Thêm đơn hàng";
            labelOrderNo.Text = string.Empty;
            labelStatus.Text = Constants.OrderStatusDraft;
            dateOrderDate.Value = DateTime.Now;
            dateDeliveryDate.Value = DateTime.Now;
        }

        public OrderForm(int id)
        {
            InitializeComponent();

            InitForm();

            drlCustomer.Focus();
            itemId = id;

            var biz = new OrderBiz();
            var item = biz.LoadItem(id);
            
            created = item.Created;
            createdBy = item.CreatedByUserId;
            modified = item.Modified;
            modifiedBy = item.ModifiedByUserId;

            labelOrderNo.Text = item.OrderId;
            labelStatus.Text = item.Status;
            dateOrderDate.Value = item.OrderDate;
            dateDeliveryDate.Value = item.DeliveryDate;
            textDeliverrAddress.Text = item.DeliveryAddress;
            if (item.CustomerId.HasValue)
            {
                drlCustomer.SelectedValue = item.CustomerId.Value;
            }

            LoadProducts(item);

            this.Text = "Sửa đơn hàng";
        }

        private void LoadCustomer()
        {
            var biz = new CustomerBiz();
            var items = biz.LoadItems();
            items.Insert(0, new Customer());

            drlCustomer.Items.Clear();
            drlCustomer.DataSource = items;
            drlCustomer.DisplayMember = "FullName";
            drlCustomer.ValueMember = "Id";
        }

        private void SaveItem(object sender, EventArgs e)
        {
            if (CustomValidation())
            {
                var item = new Order();
                item.OrderDate = dateOrderDate.Value;
                item.DeliveryDate = dateDeliveryDate.Value;
                item.DeliveryAddress = textDeliverrAddress.Text;
                item.CustomerId = Convert.ToInt32(Convert.ToString(drlCustomer.SelectedValue));
                item.Status = Constants.OrderStatusDraft;

                if (itemId > 0)
                {
                    item.Id = itemId;
                    item.Created = created;
                    item.CreatedByUserId = createdBy;

                    item.Modified = DateTime.Now;
                    item.ModifiedByUserId = currentUser;

                    var biz = new OrderBiz();
                    biz.UpdateItem(item);
                }
                else
                {
                    item.Created = DateTime.Now;
                    item.CreatedByUserId = currentUser;

                    item.Modified = DateTime.Now;
                    item.ModifiedByUserId = currentUser;

                    var biz = new OrderBiz();
                    biz.SaveItem(item);
                }

                UpdateProductList(item);

                
                var parent = this.Parent as SplitterPanel;                
                parent.Controls.Clear();

                var orderView = new OrderView { Dock = DockStyle.Fill, TopLevel = false };
                parent.Controls.Add(orderView);
                orderView.Show();

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

            var orderView = new OrderView { Dock = DockStyle.Fill, TopLevel = false };
            parent.Controls.Add(orderView);
            orderView.Show();
            this.Close();
        }

        private bool CustomValidation()
        {
            bool hasError = true;            
            errorProvider.Clear();

            if (drlCustomer.SelectedItem == null || Convert.ToInt32(drlCustomer.SelectedValue) == 0)
            {
                errorProvider.SetError(drlCustomer, Constants.Messages.RequireMessage);
                hasError = false;

                drlCustomer.Focus();
            }

            return hasError;
        }

        private void SelectCustomer(object sender, EventArgs e)
        {
            textDeliverrAddress.Text = string.Empty;

            if (drlCustomer.SelectedItem != null)
            {                
                var customer = drlCustomer.SelectedItem as Customer;
                if (customer.Id > 0)
                {
                    textDeliverrAddress.Text = customer.Address1;
                }
            }
        }

        private void AddNewProduct(object sender, EventArgs e)
        {
            var dialog = new FindProduct();
            var result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                var product = dialog.SelectedProduct;
                if (product != null)
                {
                    if (listProduct == null)
                    {
                        listProduct = new List<SearchProduct>();
                    }

                    if (!HasFooter)
                    {
                        listProduct.Add(product);                        
                        listProduct.Add(new SearchProduct());
                        HasFooter = true;
                    }
                    else
                    {
                        listProduct.Insert(listProduct.Count - 1, product);
                    }

                    dataGridView.DataSource = null;
                    dataGridView.DataSource = listProduct;
                    dataGridView.Refresh();
                }
            }
        }

        private void UpdateQuantity(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            var row = dataGridView.Rows[e.RowIndex];            

            if (e.ColumnIndex == 2)
            {
                var obj = row.Cells[3].Value;
                if (obj != null)
                {
                    decimal price = Convert.ToDecimal(obj);
                    if (price > 0)
                    {
                        int number = 0;
                        if (int.TryParse(Convert.ToString(row.Cells[2].Value), out number))
                        {
                            decimal total = price * number;
                            row.Cells[4].Value = total.ToString("0,000");

                            UpdateTotal();
                        }
                    }
                }
            }
        }

        private Product LoadProductItem(int id)
        {
            var biz = new ProductBiz();
            return biz.LoadItem(id);
        }

        private void LoadProducts(Order order)
        {
            foreach (var detail in order.Details)
            {
                dataGridView.Rows.Add();
                var row = dataGridView.Rows[dataGridView.Rows.Count - 1];
                row.Cells[1].Value = detail.Quantity;
                row.Cells[2].Value = detail.ProductPrice.ToString("0,000");
                row.Cells[3].Value = detail.Total.ToString("0,000");
                row.Cells[4].Value = detail.Id;
                DataGridViewComboBoxCell cb = (DataGridViewComboBoxCell)row.Cells[0];
                cb.Value = detail.ProductId.Value;
            }
        }

        private void DeleteProduct(object sender, EventArgs e)
        {   
            if (dataGridView.SelectedRows.Count > 0)
            {
                int index = dataGridView.SelectedRows[0].Index;

                if (index == dataGridView.RowCount - 1) return;

                var content = Convert.ToString(dataGridView.SelectedRows[0].Cells[5].Value);
                if (!string.IsNullOrEmpty(content))
                {
                    removedItems.Add(int.Parse(content));
                }

                listProduct.RemoveAt(index);
                if (listProduct.Count == 1)
                {
                    listProduct.Clear();
                    HasFooter = false;
                }

                dataGridView.DataSource = null;
                dataGridView.DataSource = listProduct;
                dataGridView.Refresh();                
            }
        }

        private void UpdateProductList(Order order)
        {
            var biz = new OrderDetailBiz();
            biz.RemoveItemsByOrderId(order.Id);

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                DataGridViewComboBoxCell cb = (DataGridViewComboBoxCell)row.Cells[0];
                string total = Convert.ToString(row.Cells[3].Value);
                string quantity = Convert.ToString(row.Cells[1].Value);
                string price = Convert.ToString(row.Cells[2].Value);
                int id = 0;
                if (cb.Value != null && int.TryParse(Convert.ToString(cb.Value), out id)  && !string.IsNullOrEmpty(total))
                {
                    var detail = new OrderDetail();
                    detail.OrderId = order.Id;
                    detail.ProductId = id;
                    detail.Quantity = Convert.ToInt32(quantity);
                    detail.ProductPrice = Convert.ToDecimal(price);
                    detail.Total = Convert.ToDecimal(total);
                    biz.SaveItem(detail);                    
                }                
            }
        }

        private void ChangeSource(object sender, EventArgs e)
        {
            UpdateTotal();
        }

        private void UpdateTotal()
        {
            if (dataGridView.RowCount > 1)
            {
                decimal total = 0;
                for (int i = 0; i <= dataGridView.RowCount - 2; i++)
                {
                    var item = dataGridView.Rows[i].DataBoundItem as SearchProduct;
                    if (item != null && !string.IsNullOrEmpty(item.TotalValue))
                    {
                        decimal itemTotal = Convert.ToDecimal(item.TotalValue);
                        total += itemTotal;
                    }
                }

                var footer = dataGridView.Rows[dataGridView.RowCount - 1];
                footer.Cells[4].Value = "Tổng: " + total.ToString("0,000");
            }
        }
    }
}

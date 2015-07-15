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

        private User currentUser;

        public List<int> removedItems;
        public List<SearchProduct> listProduct;
        private bool HasFooter;

        private void InitForm(User user)
        {
            buttonSave.DialogResult = DialogResult.OK;
            button2.DialogResult = DialogResult.Cancel;

            currentUser = user;

            LoadCustomer();
            listProduct = new List<SearchProduct>();
            removedItems = new List<int>();

            dataGridView.AutoGenerateColumns = false;
        }

        public OrderForm(User user)
        {
            InitializeComponent();

            dateOrderDate.Focus();
            itemId = 0;

            InitForm(user);

            this.Text = "Thêm đơn hàng";
            labelOrderNo.Text = string.Empty;
            labelStatus.Text = Constants.OrderStatusDraft;
            dateOrderDate.Value = DateTime.Now;
            dateDeliveryDate.Value = DateTime.Now;
        }

        public OrderForm(int id, User user)
        {
            InitializeComponent();

            InitForm(user);

            drlCustomer.Focus();
            itemId = id;

            var biz = new OrderBiz();
            var item = biz.LoadItem(id);
            
            created = item.Created;
            createdBy = item.CreatedByUserId;
            modified = item.Modified;
            modifiedBy = item.ModifiedByUserId;

            labelOrderNo.Text = item.OrderNo;
            labelStatus.Text = item.Status;
            dateOrderDate.Value = item.OrderDate;
            dateDeliveryDate.Value = item.DeliveryDate;
            textDeliverrAddress.Text = item.DeliveryAddress;
            txtDiscount.Text = item.Discount.ToString();
            txtRecipient.Text = item.Recipient;
            txtRecipientPhone.Text = item.RecipientPhone;
            cboDeliveryInternal.Checked = item.DeliveryInternal;
            cboVat.Checked = item.Vat;
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
            if (CheckSecurity(itemId))
            {
                var parent = this.Parent as SplitterPanel;
                parent.Controls.Clear();

                var orderView = new OrderView(currentUser) { Dock = DockStyle.Fill, TopLevel = false };
                parent.Controls.Add(orderView);
                orderView.Show();

                this.Close();
            }
            else
            {
                if (CustomValidation())
                {
                    var item = new Order();
                    item.OrderDate = dateOrderDate.Value;
                    item.DeliveryDate = dateDeliveryDate.Value;
                    item.DeliveryAddress = textDeliverrAddress.Text;
                    item.CustomerId = Convert.ToInt32(Convert.ToString(drlCustomer.SelectedValue));
                    item.DeliveryInternal = cboDeliveryInternal.Checked;
                    int discount = 0;
                    if (int.TryParse(txtDiscount.Text, out discount))
                    {
                        item.Discount = discount;
                    }
                    item.Recipient = txtRecipient.Text;
                    item.RecipientPhone = txtRecipientPhone.Text;
                    item.Vat = cboVat.Checked;

                    if (!string.IsNullOrEmpty(item.Recipient) && string.IsNullOrEmpty(item.IsSendNotification))
                    {
                        item.IsSendNotification = "Chưa gửi.";
                    }

                    if (itemId > 0)
                    {
                        item.Id = itemId;
                        item.Status = labelStatus.Text;
                        item.OrderNo = labelOrderNo.Text;
                        item.Created = created;
                        item.CreatedByUserId = createdBy;

                        item.Modified = DateTime.Now;
                        item.ModifiedByUserId = currentUser.Id;

                        var biz = new OrderBiz();
                        biz.UpdateItem(item);
                    }
                    else
                    {
                        item.Status = Constants.OrderStatusDraft;
                        item.Created = DateTime.Now;
                        item.CreatedByUserId = currentUser.Id;

                        item.Modified = DateTime.Now;
                        item.ModifiedByUserId = currentUser.Id;

                        var biz = new OrderBiz();
                        biz.SaveItem(item);
                    }

                    UpdateProductList(item);


                    var parent = this.Parent as SplitterPanel;
                    parent.Controls.Clear();

                    var orderView = new OrderView(currentUser) { Dock = DockStyle.Fill, TopLevel = false };
                    parent.Controls.Add(orderView);
                    orderView.Show();

                    this.Close();
                }
                else
                {
                    this.DialogResult = DialogResult.None;
                }
            }
        }

        private void CancelItem(object sender, EventArgs e)
        {
            var parent = this.Parent as SplitterPanel;

            parent.Controls.Clear();

            var orderView = new OrderView(currentUser) { Dock = DockStyle.Fill, TopLevel = false };
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

            if (!string.IsNullOrEmpty(txtRecipient.Text) && txtRecipient.Text.Length > 0)
            {
                var biz = new SmsBiz();
                var sms = biz.LoadItem(Constants.SmsDeliveryInternal1);
                if (sms != null)
                {
                    var content = sms.Content;
                    content = content.Replace(Constants.SmsParameter1, txtRecipient.Text.Trim());
                    if (content.Length > 158)
                    {
                        errorProvider.SetError(txtRecipient, Constants.Messages.SmsOverRange);
                        hasError = false;

                        txtRecipient.Focus();
                    }
                }
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

                    if(string.Equals(customer.City, Constants.CityInternal, StringComparison.InvariantCultureIgnoreCase))
                    {
                        cboDeliveryInternal.Checked = true;
                    }
                    else
                    {
                        cboDeliveryInternal.Checked = false;
                    }
                }
            }
        }

        private void AddNewProduct(object sender, EventArgs e)
        {
            var dialog = new FindProduct(listProduct);
            dialog.ParentForm = this;
            dialog.ShowDialog();            
        }

        public void UpdateGrid(SearchProduct product)
        {            
            if (product != null)
            {
                product.ActualPrice = product.Price;

                if (product.Price.HasValue && product.Price.Value > 0)
                {
                    int discount = 0;
                    int.TryParse(txtDiscount.Text, out discount);
                    
                    decimal x = 1.1M;
                    var rootPrice = Math.Round(product.Price.Value / x);
                    product.ActualPrice = rootPrice - (rootPrice * discount) / 100;
                }

                if (listProduct == null)
                {
                    listProduct = new List<SearchProduct>();
                }

                if (!HasFooter)
                {
                    listProduct.Add(product);
                    listProduct.Add(new SearchProduct());
                    listProduct.Add(new SearchProduct());
                    listProduct.Add(new SearchProduct());
                    HasFooter = true;
                }
                else
                {
                    listProduct.Insert(listProduct.Count - 3, product);
                }

                dataGridView.DataSource = null;
                dataGridView.DataSource = listProduct;
                dataGridView.Refresh();
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
                            row.Cells[6].Value = total;

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
            if (order.Details.Count == 0) return;

            listProduct = new List<SearchProduct>();

            foreach (var detail in order.Details)
            {
                var searchProduct = new SearchProduct();
                searchProduct.Price = detail.ProductPrice;
                searchProduct.QuantityValue = detail.Quantity.ToString();
                searchProduct.Total = detail.Total;
                searchProduct.ActualPrice = detail.ProductActualPrice;
                searchProduct.TotalValue = detail.Total.ToString("0,000");
                searchProduct.Id = detail.Id;
                searchProduct.Name = detail.Product.Name;
                searchProduct.Code = detail.Product.Code;

                listProduct.Add(searchProduct);
            }

            listProduct.Add(new SearchProduct());
            listProduct.Add(new SearchProduct());
            listProduct.Add(new SearchProduct());
            HasFooter = true;

            dataGridView.DataSource = null;
            dataGridView.DataSource = listProduct;
            dataGridView.Refresh();
        }

        private void DeleteProduct(object sender, EventArgs e)
        {   
            if (dataGridView.SelectedRows.Count > 0)
            {
                int index = dataGridView.SelectedRows[0].Index;

                if (index >= dataGridView.RowCount - 3) return;

                var content = Convert.ToString(dataGridView.SelectedRows[0].Cells[5].Value);
                if (!string.IsNullOrEmpty(content))
                {
                    removedItems.Add(int.Parse(content));
                }

                listProduct.RemoveAt(index);
                if (listProduct.Count == 3)
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

            for (int i = 0; i < dataGridView.RowCount - 3; i++)
            {
                var entity = dataGridView.Rows[i].DataBoundItem as SearchProduct;
                if (entity != null && entity.Total > 0)
                {
                    var detail = new OrderDetail();
                    detail.OrderId = order.Id;
                    detail.ProductId = entity.Id;
                    detail.Quantity = entity.Quantity.Value;
                    detail.ProductPrice = entity.Price.Value;
                    detail.Total = Convert.ToDecimal(entity.TotalValue);
                    detail.ProductActualPrice = entity.ActualPrice;
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
            if (dataGridView.RowCount > 3)
            {
                decimal total = 0;
                for (int i = 0; i <= dataGridView.RowCount - 4; i++)
                {
                    var item = dataGridView.Rows[i].DataBoundItem as SearchProduct;
                    if (item != null && !string.IsNullOrEmpty(item.TotalValue))
                    {
                        decimal itemTotal = Convert.ToDecimal(item.TotalValue);
                        total += itemTotal;
                    }
                }

                var footer = dataGridView.Rows[dataGridView.RowCount - 3];
                footer.Cells[4].Value = "Tổng: " + total.ToString("0,000");

                var vat = dataGridView.Rows[dataGridView.RowCount - 2];                
                decimal vatValue = (total * 10) / 100;

                if (!cboVat.Checked)
                {
                    vat.Cells[4].Value = "VAT(0%)";
                    vatValue = 0;
                }
                else
                {
                    vat.Cells[4].Value = "VAT(10%): " + vatValue.ToString("0,000");
                }                
                
                var final = dataGridView.Rows[dataGridView.RowCount - 1];                
                final.Cells[4].Value = "Tổng: " + (total + vatValue).ToString("0,000");
            }
        }

        private bool CheckSecurity(int id)
        {
            if (id == 0) return false;

            var biz = new OrderBiz();
            var current = biz.LoadItem(id);
            if(current != null)
            {
                if(current.Modified.Value != modified.Value)
                {
                    MessageBox.Show(Constants.Messages.ConflictOrderMessage);
                    return true;
                }            
            }

            return false;
        }

        private void SelectVat(object sender, EventArgs e)
        {
            UpdateTotal();
        }

        private void DiscountKeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                decimal rate = Convert.ToDecimal(txtDiscount.Text);

                var list = dataGridView.DataSource as List<SearchProduct>;

                if (list != null && list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        var product = list[i];
                        if (!string.IsNullOrEmpty(product.Code))
                        {
                            decimal x = 1.1M;
                            var rootPrice = Math.Round(product.Price.Value / x);
                            product.ActualPrice = rootPrice - (rootPrice * rate) / 100;
                            if (product.Quantity.HasValue)
                            {
                                product.TotalValue = (product.ActualPrice * product.Quantity).Value.ToString("0,000");
                            }
                        }
                    }

                    listProduct = list;

                    dataGridView.DataSource = null;
                    dataGridView.DataSource = listProduct;
                    dataGridView.Refresh();
                }
            }
        }

        private void EditControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Quantity_KeyPress);
            if (dataGridView.CurrentCell.ColumnIndex == 2) 
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Quantity_KeyPress);
                }
            }
        }

        private void Quantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}

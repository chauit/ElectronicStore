﻿using Business;
using ElectronicStore.Administration;
using ElectronicStore.Common;
using Model;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        private Customer currentCustomer;

        public List<int> removedItems;
        public List<SearchProduct> listProduct;
        public List<SearchProductLD> listProductLD;
        private bool HasFooter;
        private bool HasFooterLD;

        private decimal currentTotal;
        private decimal currentTotalLd;
        private bool isLocked;
        private string lockedUserName;
        private string sendEmail;
        private string sendSms;

        private void InitForm(User user)
        {
            buttonSave.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            currentUser = user;
           
            listProduct = new List<SearchProduct>();
            listProductLD = new List<SearchProductLD>();
            removedItems = new List<int>();

            dataGridView.AutoGenerateColumns = false;
            dataGridViewLD.AutoGenerateColumns = false;
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

            var lockBiz = new LockingBiz();
            var lockItem = lockBiz.LoadItem(Constants.TableNameOrder, user.Id, id);
            if (lockItem == null)
            {
                lockBiz.LockItem(Constants.TableNameOrder, id, user.Id);
            }
            else
            {                
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;
                groupBox5.Enabled = false;
                groupBox7.Enabled = false;
                buttonSave.Enabled = false;

                var userBiz = new UserBiz();
                var lockedUser = userBiz.LoadItem(lockItem.CurrentUserId);
                lockedUserName = lockedUser.FullName;
                isLocked = true;                
            }

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
            txtDiscountLD.Text = item.DiscountLD.ToString();
            if (item.Liability.HasValue)
            {
                textDuNo.Text = item.Liability.Value.ToString(Constants.CurrencyFormat);
            }
            txtRecipient.Text = item.Recipient;
            txtRecipientPhone.Text = item.RecipientPhone;
            cboDeliveryInternal.Checked = item.DeliveryInternal;
            cboVat.Checked = item.Vat;
            cboSendWithEmail.Checked = item.SendWithEmail;

            sendEmail = item.SendEmail;
            sendSms = item.SendMessage;

            if (item.CustomerId.HasValue)
            {
                textCustomer.Text = item.CustomerName;
                currentCustomer = item.Customer;
                SelectCustomer();
            }

            LoadProducts(item);

            this.Text = "Sửa đơn hàng";
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

                    if (currentCustomer != null)
                    {
                        item.CustomerId = currentCustomer.Id;
                    }
                    
                    item.DeliveryInternal = cboDeliveryInternal.Checked;
                    
                    //decimal discount = 0;
                    //if (decimal.TryParse(txtDiscount.Text, out discount))
                    //{
                    //    item.Discount = discount;
                    //}
                    //decimal discountLd = 0;
                    //if (decimal.TryParse(txtDiscountLD.Text, out discountLd))
                    //{
                    //    item.DiscountLD = discountLd;
                    //}

                    item.Recipient = txtRecipient.Text;
                    item.RecipientPhone = txtRecipientPhone.Text;
                    item.Vat = cboVat.Checked;

                    if (!string.IsNullOrEmpty(item.Recipient) && string.IsNullOrEmpty(item.IsSendNotification))
                    {
                        item.IsSendNotification = Constants.OrderReport1;
                    }
                    item.SendWithEmail = cboSendWithEmail.Checked;

                    if(!string.IsNullOrEmpty(textDuNo.Text))
                    {
                        item.Liability = decimal.Parse(textDuNo.Text);
                    }

                    item.SendEmail = sendEmail;
                    item.SendMessage = sendSms;

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
                        item.SendEmail = Constants.OrderEmail1;
                        item.SendMessage = Constants.OrderSms1;
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

            if (currentCustomer == null)
            {
                errorProvider.SetError(textCustomer, Constants.Messages.RequireMessage);
                hasError = false;

                textCustomer.Focus();
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

            if(!string.IsNullOrEmpty(textDuNo.Text))
            {
                decimal value = 0;
                if(!decimal.TryParse(textDuNo.Text, out value))
                {
                    errorProvider.SetError(textDuNo, Constants.Messages.InvalidType);
                    hasError = false;

                    textDuNo.Focus();
                }
            }

            return hasError;
        }

        private void SelectCustomer()
        {
            textDeliverrAddress.Text = string.Empty;

            if (currentCustomer != null)
            {
                textDeliverrAddress.Text = currentCustomer.Address1;
                textCustomer.Text = currentCustomer.FullName;                
            }
        }

        private void AddNewProduct(object sender, EventArgs e)
        {
            var dialog = new FindProduct(listProduct);
            dialog.ParentForm = this;
            dialog.ShowDialog();            
        }

        private void AddNewProductLD(object sender, EventArgs e)
        {
            var dialog = new FindProductLD(listProductLD);
            dialog.ParentForm = this;
            dialog.ShowDialog();
        }

        public void UpdateGrid(SearchProduct product)
        {            
            if (product != null)
            {
                product.ActualPrice = product.Price;
                
                decimal discount = 0;
                if (product.Discount.HasValue)
                    discount = product.Discount.Value;

                if (product.Price.HasValue && product.Price.Value > 0)
                {   
                    var rootPrice = product.Price.Value;
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

        public void UpdateGridLD(SearchProductLD product)
        {
            if (product != null)
            {
                product.ActualPrice = product.Price;

                decimal discount = 0;
                if (product.Discount.HasValue)
                    discount = product.Discount.Value;

                if (product.Price.HasValue && product.Price.Value > 0)
                {
                    var rootPrice = product.Price.Value;
                    product.ActualPrice = rootPrice - (rootPrice * discount) / 100;
                }

                if (listProductLD == null)
                {
                    listProductLD = new List<SearchProductLD>();
                }

                if (!HasFooterLD)
                {
                    listProductLD.Add(product);
                    listProductLD.Add(new SearchProductLD());
                    HasFooterLD = true;
                }
                else
                {
                    listProductLD.Insert(listProductLD.Count - 1, product);
                }

                dataGridViewLD.DataSource = null;
                dataGridViewLD.DataSource = listProductLD;
                dataGridViewLD.Refresh();
            }
        }

        private void UpdateQuantity(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            if (e.ColumnIndex == 2)
            {
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
                                decimal total = Decimal.Round(price * number, 1);
                                row.Cells[5].Value = total.ToString(Constants.CurrencyFormat);
                                row.Cells[7].Value = total;

                                UpdateTotal();
                            }
                        }
                    }
                }
            }

            if (e.ColumnIndex == 4 && dataGridView.RowCount > 0)
            {
                decimal rate = 0;

                var product = dataGridView.Rows[e.RowIndex].DataBoundItem as SearchProduct;
                if (product != null)
                {
                    if (product.Discount.HasValue)
                    {
                        rate = Convert.ToDecimal(product.Discount.Value);
                    }

                    var rootPrice = product.Price.Value;
                    product.ActualPrice = rootPrice - (rootPrice * rate) / 100;
                    if (product.Quantity.HasValue)
                    {
                        product.TotalValue = (product.ActualPrice * product.Quantity).Value.ToString(Constants.CurrencyFormat);
                    }
                    
                    dataGridView.Refresh();
                }

                txtDiscount.Text = string.Empty;
            }
        }

        private void UpdateQuantityLD(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            var row = dataGridViewLD.Rows[e.RowIndex];

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
                            decimal total = Decimal.Round(price * number, 1);
                            row.Cells[5].Value = total.ToString(Constants.CurrencyFormat);
                            row.Cells[7].Value = total;

                            UpdateTotalLD();
                        }
                    }
                }
            }

            if (e.ColumnIndex == 4 && dataGridViewLD.RowCount > 0)
            {
                decimal rate = 0;

                var product = dataGridViewLD.Rows[e.RowIndex].DataBoundItem as SearchProductLD;
                if (product != null)
                {
                    if (product.Discount.HasValue)
                    {
                        rate = Convert.ToDecimal(product.Discount.Value);
                    }

                    var rootPrice = product.Price.Value;
                    product.ActualPrice = rootPrice - (rootPrice * rate) / 100;
                    if (product.Quantity.HasValue)
                    {
                        product.TotalValue = (product.ActualPrice * product.Quantity).Value.ToString(Constants.CurrencyFormat);
                    }

                    dataGridViewLD.Refresh();
                }

                txtDiscountLD.Text = string.Empty;
            }
        }

        private void LoadProducts(Order order)
        {
            if (order.Details.Count == 0) return;

            listProduct = new List<SearchProduct>();
            listProductLD = new List<SearchProductLD>();

            foreach (var detail in order.Details)
            {
                if(detail.ProductId.HasValue)
                {
                    var searchProduct = new SearchProduct();
                    searchProduct.Price = detail.ProductPrice;
                    searchProduct.QuantityValue = detail.Quantity.ToString();
                    searchProduct.Total = detail.Total;
                    searchProduct.ActualPrice = detail.ProductActualPrice;
                    searchProduct.Discount = detail.Discount;
                    searchProduct.TotalValue = detail.Total.ToString(Constants.CurrencyFormat);
                    searchProduct.Id = detail.ProductId.Value;
                    searchProduct.Name = detail.Product.Name;
                    searchProduct.Code = detail.Product.Code;

                    listProduct.Add(searchProduct);
                }
                else if(detail.ProductLdId.HasValue)
                {
                    var searchProduct = new SearchProductLD();
                    searchProduct.Price = detail.ProductPrice;
                    searchProduct.QuantityValue = detail.Quantity.ToString();
                    searchProduct.Total = detail.Total;
                    searchProduct.ActualPrice = detail.ProductActualPrice;
                    searchProduct.Discount = detail.Discount;
                    searchProduct.TotalValue = detail.Total.ToString(Constants.CurrencyFormat);
                    searchProduct.Id = detail.ProductLdId.Value;
                    searchProduct.Name = detail.ProductLD.Name;
                    searchProduct.Code = detail.ProductLD.Code;

                    listProductLD.Add(searchProduct);
                }

            }
            if (listProduct.Count > 0)
            {
                listProduct.Add(new SearchProduct());
                HasFooter = true;

                dataGridView.DataSource = null;
                dataGridView.DataSource = listProduct;
                dataGridView.Refresh();
            }

            if (listProductLD.Count > 0)
            {
                listProductLD.Add(new SearchProductLD());
                HasFooterLD = true;

                dataGridViewLD.DataSource = null;
                dataGridViewLD.DataSource = listProductLD;
                dataGridViewLD.Refresh();
            }            
        }

        private void DeleteProduct(object sender, EventArgs e)
        {   
            if (dataGridView.SelectedRows.Count > 0)
            {
                int index = dataGridView.SelectedRows[0].Index;

                if (index >= dataGridView.RowCount - 1) return;

                var content = Convert.ToString(dataGridView.SelectedRows[0].Cells[6].Value);
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

        private void DeleteProductLD(object sender, EventArgs e)
        {
            if (dataGridViewLD.SelectedRows.Count > 0)
            {
                int index = dataGridViewLD.SelectedRows[0].Index;

                if (index >= dataGridViewLD.RowCount - 1) return;

                var content = Convert.ToString(dataGridViewLD.SelectedRows[0].Cells[6].Value);
                if (!string.IsNullOrEmpty(content))
                {
                    removedItems.Add(int.Parse(content));
                }

                listProductLD.RemoveAt(index);
                if (listProductLD.Count == 1)
                {
                    listProductLD.Clear();
                    HasFooterLD = false;
                }

                dataGridViewLD.DataSource = null;
                dataGridViewLD.DataSource = listProductLD;
                dataGridViewLD.Refresh();
            }
        }

        private void UpdateProductList(Order order)
        {
            var biz = new OrderDetailBiz();
            biz.RemoveItemsByOrderId(order.Id);

            for (int i = 0; i < dataGridView.RowCount - 1; i++)
            {
                var entity = dataGridView.Rows[i].DataBoundItem as SearchProduct;
                if (entity != null && entity.Total > 0)
                {
                    var detail = new OrderDetail();
                    detail.OrderId = order.Id;
                    detail.ProductId = entity.Id;
                    detail.Quantity = entity.Quantity.Value;
                    detail.ProductPrice = entity.Price.Value;
                    detail.Discount = entity.Discount;
                    detail.Total = Convert.ToDecimal(entity.TotalValue);
                    detail.ProductActualPrice = entity.ActualPrice;
                    biz.SaveItem(detail);
                }
            }

            for (int i = 0; i < dataGridViewLD.RowCount - 1; i++)
            {
                var entity = dataGridViewLD.Rows[i].DataBoundItem as SearchProductLD;
                if (entity != null && entity.Total > 0)
                {
                    var detail = new OrderDetail();
                    detail.OrderId = order.Id;
                    detail.ProductLdId = entity.Id;
                    detail.Quantity = entity.Quantity.Value;
                    detail.ProductPrice = entity.Price.Value;
                    detail.Discount = entity.Discount;
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

        private void ChangeSourceLD(object sender, EventArgs e)
        {
            UpdateTotalLD();
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
                
                var final = dataGridView.Rows[dataGridView.RowCount - 1];
                final.Cells[5].Value = "Tổng: " + (total).ToString(Constants.CurrencyFormat);

                currentTotal = total;

                UpdateTotalValue();
            }
        }

        private void UpdateTotalLD()
        {
            if (dataGridViewLD.RowCount > 1)
            {
                decimal total = 0;
                for (int i = 0; i <= dataGridViewLD.RowCount - 2; i++)
                {
                    var item = dataGridViewLD.Rows[i].DataBoundItem as SearchProductLD;
                    if (item != null && !string.IsNullOrEmpty(item.TotalValue))
                    {
                        decimal itemTotal = Convert.ToDecimal(item.TotalValue);
                        total += itemTotal;
                    }
                }

                var footer = dataGridViewLD.Rows[dataGridViewLD.RowCount - 1];
                footer.Cells[5].Value = "Tổng: " + total.ToString(Constants.CurrencyFormat);

                currentTotalLd = total;

                UpdateTotalValue();
            }
        }

        private void UpdateTotalValue()
        {
            var temp = currentTotal + currentTotalLd;
            if (temp > 0)
            {
                labelTotal.Text = temp.ToString(Constants.CurrencyFormat);
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
                decimal rate = 0;

                if (!decimal.TryParse(txtDiscount.Text, out rate)) return;

                var list = dataGridView.DataSource as List<SearchProduct>;

                if (list != null && list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        var product = list[i];                        

                        if (!string.IsNullOrEmpty(product.Code))
                        {
                            product.Discount = rate;

                            var rootPrice = product.Price.Value;
                            product.ActualPrice = rootPrice - (rootPrice * rate) / 100;
                            if (product.Quantity.HasValue)
                            {
                                product.TotalValue = (product.ActualPrice * product.Quantity).Value.ToString(Constants.CurrencyFormat);
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

        private void DiscountLDKeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                decimal rate = 0;

                if (!decimal.TryParse(txtDiscountLD.Text, out rate)) return;

                var list = dataGridViewLD.DataSource as List<SearchProductLD>;

                if (list != null && list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        var product = list[i];

                        if (!string.IsNullOrEmpty(product.Code))
                        {
                            product.Discount = rate;

                            var rootPrice = product.Price.Value;
                            product.ActualPrice = rootPrice - (rootPrice * rate) / 100;
                            if (product.Quantity.HasValue)
                            {
                                product.TotalValue = (product.ActualPrice * product.Quantity).Value.ToString(Constants.CurrencyFormat);
                            }
                        }
                    }

                    listProductLD = list;

                    dataGridViewLD.DataSource = null;
                    dataGridViewLD.DataSource = listProductLD;
                    dataGridViewLD.Refresh();
                }
            }
        }

        private void EditControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Quantity_KeyPress);
            if (dataGridView.CurrentCell.ColumnIndex == 2 || dataGridView.CurrentCell.ColumnIndex == 4) 
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Quantity_KeyPress);
                }
            }
        }

        private void EditControlShowingLD(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Quantity_KeyPress);
            if (dataGridViewLD.CurrentCell.ColumnIndex == 2 || dataGridViewLD.CurrentCell.ColumnIndex == 4)
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Quantity_KeyPressLD);
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

        private void Quantity_KeyPressLD(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (dataGridView.Rows.Count > 0 && e.RowIndex > -1)
            {
                if (e.RowIndex >= dataGridView.Rows.Count - 1 && (e.ColumnIndex == 2 || e.ColumnIndex == 4))
                {
                    dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.White;
                }                
            }            
        }

        private void CellPaintingLD(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (dataGridViewLD.Rows.Count > 0 && e.RowIndex > -1)
            {
                if (e.RowIndex >= dataGridViewLD.Rows.Count - 1 && (e.ColumnIndex == 2  || e.ColumnIndex == 4))
                {
                    dataGridViewLD.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.White;
                }
            }
        }

        private void CustomerKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var dialog = new FindCustomer(textCustomer.Text);
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (dialog.SelectedItem != null)
                    {
                        currentCustomer = dialog.SelectedItem;
                        SelectCustomer();

                        if (string.Equals(currentCustomer.City, Constants.CityInternal, StringComparison.InvariantCultureIgnoreCase))
                        {
                            cboDeliveryInternal.Checked = true;
                        }
                        else
                        {
                            cboDeliveryInternal.Checked = false;
                        }
                    }
                }
                else
                {
                    currentCustomer = null;
                    textCustomer.Text = string.Empty;
                    textDeliverrAddress.Text = string.Empty;
                }
            }
        }

        private void DiscountLeave(object sender, EventArgs e)
        {
            decimal rate = 0;

            if (!decimal.TryParse(txtDiscount.Text, out rate))
            {
                rate = 0;
            }

            var list = dataGridView.DataSource as List<SearchProduct>;

            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    var product = list[i];                    

                    if (!string.IsNullOrEmpty(product.Code))
                    {
                        product.Discount = rate;
                        var rootPrice = product.Price.Value;
                        product.ActualPrice = rootPrice - (rootPrice * rate) / 100;
                        if (product.Quantity.HasValue)
                        {
                            product.TotalValue = (product.ActualPrice * product.Quantity).Value.ToString(Constants.CurrencyFormat);
                        }
                    }
                }

                listProduct = list;

                dataGridView.DataSource = null;
                dataGridView.DataSource = listProduct;
                dataGridView.Refresh();
            }
        }

        private void DiscountLDLeave(object sender, EventArgs e)
        {
            decimal rate = 0;

            if (!decimal.TryParse(txtDiscountLD.Text, out rate))
            {
                rate = 0;
            }

            var list = dataGridViewLD.DataSource as List<SearchProductLD>;

            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    var product = list[i];                    

                    if (!string.IsNullOrEmpty(product.Code))
                    {
                        product.Discount = rate;

                        var rootPrice = product.Price.Value;
                        product.ActualPrice = rootPrice - (rootPrice * rate) / 100;
                        if (product.Quantity.HasValue)
                        {
                            product.TotalValue = (product.ActualPrice * product.Quantity).Value.ToString(Constants.CurrencyFormat);
                        }
                    }
                }

                listProductLD = list;

                dataGridViewLD.DataSource = null;
                dataGridViewLD.DataSource = listProductLD;
                dataGridViewLD.Refresh();
            }
        }

        private void FormatDuNo(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textDuNo.Text))
            {
                decimal value = 0;
                if (decimal.TryParse(textDuNo.Text, out value))
                {
                    textDuNo.Text = value.ToString(Constants.CurrencyFormat);
                }
            }
        }

        private void FormShow(object sender, EventArgs e)
        {
            if (isLocked)
            {
                string message = string.Format("Nếu bạn muốn cập nhật thông tin, bạn cần thông báo với [{0}] đóng bản ghi này.", lockedUserName);
                MessageBox.Show(message, "Cảnh báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void FormClosedEvent(object sender, FormClosedEventArgs e)
        {
            if (!isLocked)
            {
                var lockBiz = new LockingBiz();
                lockBiz.UnlockItem(Constants.TableNameOrder, itemId, currentUser.Id);
            }
        }

        private void DuNoChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textDuNo.Text))
            {
                decimal value = 0;
                if (decimal.TryParse(textDuNo.Text, out value))
                {
                    if (value > 999)
                    {
                        int index = textDuNo.SelectionStart;
                        textDuNo.Text = value.ToString(Constants.CurrencyFormat);
                        textDuNo.SelectionStart = textDuNo.Text.Length;
                    }
                }
            }
        }
    }
}

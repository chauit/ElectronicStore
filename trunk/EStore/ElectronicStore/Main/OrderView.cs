﻿using Business;
using ElectronicStore.Common;
using Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ElectronicStore.Main
{
    public partial class OrderView : Form
    {
        private User currentUser;
        private int selectedRow;

        public OrderView(User user)
        {
            InitializeComponent();

            dataGridView.AutoGenerateColumns = false;

            var biz = new OrderBiz();
            dataGridView.DataSource = biz.LoadItems();
            dataGridView.Refresh();

            currentUser = user;
        }

        private ContextMenuStrip AddMenu(bool isDelivered, bool isDeliverToOther)
        {
            ContextMenuStrip mnu = new ContextMenuStrip();
            
            ToolStripMenuItem mnuUpdate = new ToolStripMenuItem("Sửa đơn hàng");
            ToolStripMenuItem mnuDelete = new ToolStripMenuItem("Xóa đơn hàng");
            ToolStripMenuItem mnuSendNotification = new ToolStripMenuItem("Gửi tin nhắn thông báo");
            ToolStripMenuItem mnuDeliver = new ToolStripMenuItem("Đã giao đơn hàng");
            
            //Assign event handlers
            mnuUpdate.Click += new EventHandler(UpdateSingleItem);
            mnuDelete.Click += new EventHandler(DeleteSingleItem);
            mnuDeliver.Click += new EventHandler(DeliverItem);
            mnuSendNotification.Click += new EventHandler(SendNotification);

            mnu.Items.Add(mnuUpdate);
            mnu.Items.Add(mnuDelete);

            //Add to main context menu
            if (!isDelivered)
            {
                mnu.Items.Add(mnuDeliver);
            }

            if (isDeliverToOther)
            {
                mnu.Items.Add(mnuSendNotification);
            }
            
            return mnu;
        }
        
        private void NewItem(object sender, EventArgs e)
        {
            var parent = this.Parent as SplitterPanel;            
            parent.Controls.Clear();

            var newOrder = new OrderForm(currentUser) { Dock = DockStyle.Fill, TopLevel = false };
            parent.Controls.Add(newOrder);
            newOrder.Show();

            this.Close();
        }        

        private void RefreshItems(object sender, EventArgs e)
        {
            var biz = new OrderBiz();
            dataGridView.DataSource = biz.LoadItems();
            dataGridView.Refresh();
        }

        public void UpdateSingleItem(object sender, EventArgs e)
        {
            if (selectedId > 0)
            {
                var parent = this.Parent as SplitterPanel;
                parent.Controls.Clear();

                var newOrder = new OrderForm(selectedId, currentUser) { Dock = DockStyle.Fill, TopLevel = false };
                parent.Controls.Add(newOrder);
                newOrder.Show();

                this.Close();
            }
        }

        public void DeleteSingleItem(object sender, EventArgs e)
        {
            if (selectedId > 0)
            {
                var biz = new OrderBiz();
                if (biz.IsDependent(selectedId))
                {
                    MessageBox.Show(Constants.Messages.DependentOrderMessage);
                }
                else
                {
                    biz.RemoveSingleItem(selectedId);

                    RefreshItems(sender, e);
                }
            }
        }

        public void SendNotification(object sender, EventArgs e)
        {
            if (selectedId > 0)
            {
                if (selectedRow >= 0)
                {
                    dataGridView.Rows[selectedRow].Cells[5].Value = "Đang gửi";
                    dataGridView.Refresh();
                }

                var biz = new OrderBiz();
                var status = biz.SendReport(selectedId);

                if (!string.IsNullOrEmpty(status.Error))
                {
                    MessageBox.Show(status.Error);
                }
                biz.UpdateNotificationStatus(selectedId, status.Status);

                RefreshItems(sender, e);
            }
        }

        public void DeliverItem(object sender, EventArgs e)
        {
            if (selectedId > 0)
            {
                var biz = new OrderBiz();
                biz.UpdateStatus(selectedId, Constants.OrderStatusDelivered);

                RefreshItems(sender, e);
            }
        }

        int selectedId = 0;

        private void CellClicked(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                bool isDelivered = false;
                bool isDeliverToOther = false;
                var order = dataGridView.Rows[e.RowIndex].DataBoundItem as Order;
                if (order != null)
                {
                    selectedId = order.Id;
                    isDelivered = string.Equals(order.Status, Constants.OrderStatusDelivered, StringComparison.InvariantCultureIgnoreCase);

                    if(!string.IsNullOrEmpty(order.Recipient) && 
                        !string.Equals(Constants.OrderReport2, order.IsSendNotification, StringComparison.InvariantCultureIgnoreCase)&&
                        string.Equals(Constants.OrderSms2, order.SendMessage, StringComparison.InvariantCultureIgnoreCase))
                    {
                        isDeliverToOther = true;
                    }
                }

                selectedRow = e.RowIndex;

                var menu = AddMenu(isDelivered, isDeliverToOther);
                menu.Show(Cursor.Position.X, Cursor.Position.Y);
            }

            if(e.Button == MouseButtons.Left && e.ColumnIndex == 4 && e.RowIndex != -1)
            {
                var order = dataGridView.Rows[e.RowIndex].DataBoundItem as Order;

                if (order.DeliveryDetails != null && order.DeliveryDetails.Count > 0)
                {
                    int deliveryId = 0;
                    foreach(var detail in order.DeliveryDetails)
                    {
                        deliveryId = detail.DeliveryId.Value;
                    }

                    var parent = this.Parent as SplitterPanel;
                    parent.Controls.Clear();

                    var newDelivery = new DeliveryForm(deliveryId, currentUser) { Dock = DockStyle.Fill, TopLevel = false };
                    parent.Controls.Add(newDelivery);
                    newDelivery.Show();

                    this.Close();
                }
            }
        }

        private void CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string value = Convert.ToString(e.Value);

                if (e.ColumnIndex == 3)
                {
                    dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.White;
                    switch (value)
                    {
                        case Constants.OrderStatusDraft:
                            dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Red;
                            break;
                        case Constants.OrderStatusDelivered:
                            dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Green;
                            break;
                        default:
                            dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Orange;
                            break;
                    }
                }

                if (e.ColumnIndex == 5)
                {
                    dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.White;
                    switch (value)
                    {
                        case Constants.OrderEmail1:
                            dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Orange;
                            break;
                        case Constants.OrderEmail2:
                            dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Green;
                            break;
                        case Constants.OrderEmail3:
                            dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Red;
                            break;
                        default:                            
                            break;
                    }
                }
                if (e.ColumnIndex == 6)
                {
                    dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.White;
                    switch (value)
                    {
                        case Constants.OrderSms1:
                            dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Orange;
                            break;
                        case Constants.OrderSms2:
                            dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Green;
                            break;
                        case Constants.OrderSms3:
                            dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Red;
                            break;
                        default:
                            break;
                    }
                }
                if (e.ColumnIndex == 7)
                {
                    dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.White;
                    switch (value)
                    {
                        case Constants.OrderReport1:
                            dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Orange;
                            break;
                        case Constants.OrderReport2:
                            dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Green;
                            break;
                        case Constants.OrderReport3:
                            dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Red;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}

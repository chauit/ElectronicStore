using Business;
using ElectronicStore.Common;
using Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace ElectronicStore.Main
{
    public partial class OrderView : Form
    {
        private User currentUser;
        private int selectedRow;

        public OrderView(User user)
        {
            InitializeComponent();

            var biz = new OrderBiz();
            radGridView.DataSource = biz.LoadItems();
            radGridView.Refresh();            

            FormatCell();

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
            radGridView.DataSource = biz.LoadItems();
            radGridView.Refresh();
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
                    radGridView.Rows[selectedRow].Cells[5].Value = "Đang gửi";
                    radGridView.Refresh();
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

        private void CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string value = Convert.ToString(e.Value);

                if (e.ColumnIndex == 3)
                {
                    radGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.White;
                    switch (value)
                    {
                        case Constants.OrderStatusDraft:
                            radGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Red;
                            break;
                        case Constants.OrderStatusDelivered:
                            radGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Green;
                            break;
                        default:
                            radGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Orange;
                            break;
                    }
                }

                if (e.ColumnIndex == 5)
                {
                    radGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.White;
                    switch (value)
                    {
                        case Constants.OrderEmail1:
                            radGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Orange;
                            break;
                        case Constants.OrderEmail2:
                            radGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Green;
                            break;
                        case Constants.OrderEmail3:
                            radGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Red;
                            break;
                        default:                            
                            break;
                    }
                }
                if (e.ColumnIndex == 6)
                {
                    radGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.White;
                    switch (value)
                    {
                        case Constants.OrderSms1:
                            radGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Orange;
                            break;
                        case Constants.OrderSms2:
                            radGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Green;
                            break;
                        case Constants.OrderSms3:
                            radGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Red;
                            break;
                        default:
                            break;
                    }
                }
                if (e.ColumnIndex == 7)
                {
                    radGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.White;
                    switch (value)
                    {
                        case Constants.OrderReport1:
                            radGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Orange;
                            break;
                        case Constants.OrderReport2:
                            radGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Green;
                            break;
                        case Constants.OrderReport3:
                            radGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Red;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void MouseClick(object sender, MouseEventArgs e)
        {
            var currentCell = (sender as RadGridView).CurrentCell;

            if (e.Button == MouseButtons.Right && currentCell.ColumnIndex == 0 && currentCell.RowIndex != -1)
            {
                bool isDelivered = false;
                bool isDeliverToOther = false;
                var order = radGridView.Rows[currentCell.RowIndex].DataBoundItem as Order;
                if (order != null)
                {
                    selectedId = order.Id;
                    isDelivered = string.Equals(order.Status, Constants.OrderStatusDelivered, StringComparison.InvariantCultureIgnoreCase);

                    if (!string.IsNullOrEmpty(order.Recipient) &&
                        !string.Equals(Constants.OrderReport2, order.IsSendNotification, StringComparison.InvariantCultureIgnoreCase) &&
                        string.Equals(Constants.OrderSms2, order.SendMessage, StringComparison.InvariantCultureIgnoreCase))
                    {
                        isDeliverToOther = true;
                    }
                }

                selectedRow = currentCell.RowIndex;

                var menu = AddMenu(isDelivered, isDeliverToOther);
                menu.Show(Cursor.Position.X, Cursor.Position.Y);
            }

            if (e.Button == MouseButtons.Left && currentCell.ColumnIndex == 4 && currentCell.RowIndex != -1)
            {
                var order = radGridView.Rows[currentCell.RowIndex].DataBoundItem as Order;

                if (order.DeliveryDetails != null && order.DeliveryDetails.Count > 0)
                {
                    int deliveryId = 0;
                    foreach (var detail in order.DeliveryDetails)
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

        private void FormatCell()
        {
            // Order No
            ConditionalFormattingObject objOrderNo = new ConditionalFormattingObject("OrderNoCondition", ConditionTypes.NotEqual, "", "", false);
            objOrderNo.CellForeColor = Color.Blue;

            this.radGridView.Columns[0].ConditionalFormattingObjectList.Add(objOrderNo);           

            // Order Status
            ConditionalFormattingObject objOrderStatus1 = new ConditionalFormattingObject("objOrderStatus1", ConditionTypes.Equal,
                Constants.OrderStatusDraft, string.Empty, false);
            objOrderStatus1.CellBackColor = Color.Red;
            objOrderStatus1.CellForeColor = Color.White;

            ConditionalFormattingObject objOrderStatus2 = new ConditionalFormattingObject("objOrderStatus2", ConditionTypes.Equal,
                Constants.OrderStatusDelivered, string.Empty, false);
            objOrderStatus2.CellBackColor = Color.Green;
            objOrderStatus2.CellForeColor = Color.White;
            this.radGridView.Columns[3].ConditionalFormattingObjectList.Add(objOrderStatus1);
            this.radGridView.Columns[3].ConditionalFormattingObjectList.Add(objOrderStatus2);

            // Order SMS
            ConditionalFormattingObject objOrderSms1 = new ConditionalFormattingObject("objOrderSms1", ConditionTypes.Equal,
                Constants.OrderSms1, string.Empty, false);
            objOrderSms1.CellBackColor = Color.Orange;
            objOrderSms1.CellForeColor = Color.White;

            ConditionalFormattingObject objOrderSms2 = new ConditionalFormattingObject("objOrderSms2", ConditionTypes.Equal,
                Constants.OrderSms2, string.Empty, false);
            objOrderSms2.CellBackColor = Color.Green;
            objOrderSms2.CellForeColor = Color.White;

            ConditionalFormattingObject objOrderSms3 = new ConditionalFormattingObject("objOrderSms3", ConditionTypes.Equal,
                Constants.OrderSms3, string.Empty, false);
            objOrderSms3.CellBackColor = Color.Red;
            objOrderSms3.CellForeColor = Color.White;


            this.radGridView.Columns[6].ConditionalFormattingObjectList.Add(objOrderSms1);
            this.radGridView.Columns[6].ConditionalFormattingObjectList.Add(objOrderSms2);
            this.radGridView.Columns[6].ConditionalFormattingObjectList.Add(objOrderSms3);

            // Order Email
            ConditionalFormattingObject objOrderEmail1 = new ConditionalFormattingObject("objOrderEmail1", ConditionTypes.Equal,
                Constants.OrderEmail1, string.Empty, false);
            objOrderEmail1.CellBackColor = Color.Orange;
            objOrderEmail1.CellForeColor = Color.White;

            ConditionalFormattingObject objOrderEmail2 = new ConditionalFormattingObject("objOrderEmail2", ConditionTypes.Equal,
                Constants.OrderEmail2, string.Empty, false);
            objOrderEmail2.CellBackColor = Color.Green;
            objOrderEmail2.CellForeColor = Color.White;

            ConditionalFormattingObject objOrderEmail3 = new ConditionalFormattingObject("objOrderEmail3", ConditionTypes.Equal,
                Constants.OrderEmail3, string.Empty, false);
            objOrderEmail3.CellBackColor = Color.Red;
            objOrderEmail3.CellForeColor = Color.White;


            this.radGridView.Columns[5].ConditionalFormattingObjectList.Add(objOrderEmail1);
            this.radGridView.Columns[5].ConditionalFormattingObjectList.Add(objOrderEmail2);
            this.radGridView.Columns[5].ConditionalFormattingObjectList.Add(objOrderEmail3);

            // Order Report
            ConditionalFormattingObject objOrderReport1 = new ConditionalFormattingObject("objOrderReport1", ConditionTypes.Equal,
                Constants.OrderReport1, string.Empty, false);
            objOrderReport1.CellBackColor = Color.Orange;
            objOrderReport1.CellForeColor = Color.White;

            ConditionalFormattingObject objOrderReport2 = new ConditionalFormattingObject("objOrderReport2", ConditionTypes.Equal,
                Constants.OrderReport2, string.Empty, false);
            objOrderReport2.CellBackColor = Color.Green;
            objOrderReport2.CellForeColor = Color.White;

            ConditionalFormattingObject objOrderReport3 = new ConditionalFormattingObject("objOrderReport3", ConditionTypes.Equal,
                Constants.OrderReport3, string.Empty, false);
            objOrderReport3.CellBackColor = Color.Red;
            objOrderReport3.CellForeColor = Color.White;


            this.radGridView.Columns[7].ConditionalFormattingObjectList.Add(objOrderReport1);
            this.radGridView.Columns[7].ConditionalFormattingObjectList.Add(objOrderReport2);
            this.radGridView.Columns[7].ConditionalFormattingObjectList.Add(objOrderReport3);
        }
    }
}

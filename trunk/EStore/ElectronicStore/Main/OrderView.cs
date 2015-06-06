using Business;
using ElectronicStore.Common;
using Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ElectronicStore.Main
{
    public partial class OrderView : Form
    {
        private User currentUser;
        public OrderView(User user)
        {
            InitializeComponent();

            dataGridView.AutoGenerateColumns = false;

            var biz = new OrderBiz();
            dataGridView.DataSource = biz.LoadItems();
            dataGridView.Refresh();

            currentUser = user;
        }

        private ContextMenuStrip AddMenu(bool isDelivered)
        {
            ContextMenuStrip mnu = new ContextMenuStrip();
            
            ToolStripMenuItem mnuUpdate = new ToolStripMenuItem("Sửa đơn hàng");
            ToolStripMenuItem mnuDelete = new ToolStripMenuItem("Xóa đơn hàng");            
            ToolStripMenuItem mnuDeliver = new ToolStripMenuItem("Đã giao đơn hàng");
            
            //Assign event handlers
            mnuUpdate.Click += new EventHandler(UpdateSingleItem);
            mnuDelete.Click += new EventHandler(DeleteSingleItem);
            mnuDeliver.Click += new EventHandler(DeliverItem);
            
            //Add to main context menu
            if (isDelivered)
            {
                mnu.Items.AddRange(new ToolStripItem[] { mnuUpdate, mnuDelete });
            }
            else
            {                
                mnu.Items.AddRange(new ToolStripItem[] { mnuUpdate, mnuDelete, mnuDeliver });
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
                var order = dataGridView.Rows[e.RowIndex].DataBoundItem as Order;
                if (order != null)
                {
                    selectedId = order.Id;
                    isDelivered = string.Equals(order.Status, Constants.OrderStatusDelivered, StringComparison.InvariantCultureIgnoreCase);
                }

                var menu = AddMenu(isDelivered);
                menu.Show(Cursor.Position.X, Cursor.Position.Y);
            }
        }
    }
}

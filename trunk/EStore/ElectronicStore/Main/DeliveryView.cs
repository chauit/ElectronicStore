using Business;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ECommon = ElectronicStore.Common;

namespace ElectronicStore.Main
{
    public partial class DeliveryView : Form
    {
        private User currentUser;
        public DeliveryView(User user)
        {
            InitializeComponent();

            dataGridView.AutoGenerateColumns = false;

            var biz = new DeliveryBiz();
            dataGridView.DataSource = biz.LoadItems();
            dataGridView.Refresh();

            currentUser = user;
        }

        private void NewItem(object sender, EventArgs e)
        {
            var parent = this.Parent as SplitterPanel;

            parent.Controls.Clear();

            var newDelivery = new DeliveryForm(currentUser) { Dock = DockStyle.Fill, TopLevel = false };
            parent.Controls.Add(newDelivery);
            newDelivery.Show();

            this.Close();
        }

        private void RefreshItems(object sender, EventArgs e)
        {
            var biz = new DeliveryBiz();
            dataGridView.DataSource = biz.LoadItems();
            dataGridView.Refresh();
        }

        private ContextMenuStrip AddMenu(bool isDelivered, bool isSendEmail, bool isSendSms)
        {
            ContextMenuStrip mnu = new ContextMenuStrip();

            ToolStripMenuItem mnuUpdate = new ToolStripMenuItem("Sửa thông tin giao hàng");
            ToolStripMenuItem mnuDelete = new ToolStripMenuItem("Xóa thông tin giao hàng");
            ToolStripMenuItem mnuSendEmail = new ToolStripMenuItem("Gửi email");
            ToolStripMenuItem mnuSendSms = new ToolStripMenuItem("Gửi SMS");
            ToolStripMenuItem mnuDeliver = new ToolStripMenuItem("Đã giao các đơn hàng");

            //Assign event handlers
            mnuUpdate.Click += new EventHandler(UpdateSingleItem);
            mnuDelete.Click += new EventHandler(DeleteSingleItem);
            mnuDeliver.Click += new EventHandler(DeliverItem);
            mnuSendEmail.Click += new EventHandler(SendEmail);
            mnuSendSms.Click += new EventHandler(SendSms);

            mnu.Items.Add(mnuUpdate);
            mnu.Items.Add(mnuDelete);

            if (!isDelivered)
            {
                mnu.Items.Add(mnuDeliver);
            }

            if (!isSendEmail)
            {
                mnu.Items.Add(mnuSendEmail);
            }

            if (!isSendSms)
            {
                mnu.Items.Add(mnuSendSms);
            }

            return mnu;
        }

        int selectedId = 0;

        private void CellClicked(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.ColumnIndex == 1 && e.RowIndex != -1)
            {
                bool isDelivered = false;
                bool isSendEmail = false;
                bool isSendSms = false;
                var delivery = dataGridView.Rows[e.RowIndex].DataBoundItem as Delivery;
                if (delivery != null)
                {
                    selectedId = delivery.Id;
                    isDelivered = string.Equals(delivery.Status, ECommon.Constants.DeliveryStatusDelivered, StringComparison.InvariantCultureIgnoreCase);
                    isSendEmail = string.Equals(delivery.IsSendEmail, ECommon.Constants.DeliverySentEmail, StringComparison.InvariantCultureIgnoreCase);
                    isSendSms = string.Equals(delivery.IsSendSms, ECommon.Constants.DeliverySentSms, StringComparison.InvariantCultureIgnoreCase);
                }

                var menu = AddMenu(isDelivered, isSendEmail, isSendSms);
                menu.Show(Cursor.Position.X, Cursor.Position.Y);
            }
        }

        public void UpdateSingleItem(object sender, EventArgs e)
        {
            if (selectedId > 0)
            {
                var parent = this.Parent as SplitterPanel;
                parent.Controls.Clear();

                var newDelivery = new DeliveryForm(selectedId, currentUser) { Dock = DockStyle.Fill, TopLevel = false };
                parent.Controls.Add(newDelivery);
                newDelivery.Show();

                this.Close();
            }
        }

        public void DeleteSingleItem(object sender, EventArgs e)
        {
            if (selectedId > 0)
            {
                var biz = new DeliveryBiz();
                
                biz.RemoveSingleItem(selectedId);
                RefreshItems(sender, e);                
            }
        }

        public void DeliverItem(object sender, EventArgs e)
        {
            if (selectedId > 0)
            {
                var biz = new DeliveryBiz();
                biz.UpdateStatus(selectedId, ECommon.Constants.DeliveryStatusDelivered);

                RefreshItems(sender, e);
            }
        }

        public void SendEmail(object sender, EventArgs e)
        {
            if (selectedId > 0)
            {
                var biz = new DeliveryBiz();
                biz.UpdateEmailStatus(selectedId, ECommon.Constants.DeliverySentEmail);

                biz.SendEmail(selectedId);

                RefreshItems(sender, e);
            }
        }

        public void SendSms(object sender, EventArgs e)
        {
            if (selectedId > 0)
            {
                var biz = new DeliveryBiz();
                biz.UpdateSmsStatus(selectedId, ECommon.Constants.DeliverySentSms);

                biz.SendSms(selectedId);

                RefreshItems(sender, e);
            }
        }
    }
}

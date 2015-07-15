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
    public partial class MainView : Form
    {
        private User currentUser;
        private bool flagSendMail;
        private bool flagSendSms;
        private int selectedRow;

        public MainView(User user)
        {
            InitializeComponent();
            selectedRow = -1;

            dataGridView.AutoGenerateColumns = false;

            var biz = new DeliveryBiz();
            dataGridView.DataSource = biz.LoadItems();
            dataGridView.Refresh();

            currentUser = user;
            flagSendMail = false;
            flagSendSms = false;

            backgroundWorker.DoWork += new DoWorkEventHandler(WorkAsync);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(WorkAsyncCompleted);
        }

        private void WorkAsync(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (selectedRow >= 0)
            {
                dataGridView.Rows[selectedRow].Cells[6].Value = "Đang gửi";
                dataGridView.Refresh();
            }

            if (flagSendMail)
            {
                SendMail(Convert.ToInt32(e.Argument));
            }
            if(flagSendSms)
            {
                SendSms(Convert.ToInt32(e.Argument));
            }
        }

        void SendMail(int id)
        {
            var biz = new DeliveryBiz();
            var status = biz.SendEmail(id);
            if(!string.IsNullOrEmpty(status.Error))
            {
                MessageBox.Show(status.Error);
            }
            biz.UpdateEmailStatus(id, status.Status);
        }

        void SendSms(int id)
        {
            var biz = new DeliveryBiz();
            var status = biz.SendSms(id);
            if (!string.IsNullOrEmpty(status.Error))
            {
                MessageBox.Show(status.Error);
            }
            biz.UpdateSmsStatus(id, status.Status);
        }

        private void WorkAsyncCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var biz = new DeliveryBiz();
            dataGridView.DataSource = null;
            dataGridView.DataSource = biz.LoadItems();
            dataGridView.Refresh();
        }

        public MainView(User user, bool isSendMail = false, bool isSendSms = false, int itemId = 0)
        {
            InitializeComponent();
            selectedRow = -1;

            dataGridView.AutoGenerateColumns = false;

            var biz = new DeliveryBiz();
            var list = biz.LoadItems();
            dataGridView.DataSource = list;
            dataGridView.Refresh();

            if(list != null && list.Count > 0)
            {
                for(int i=0;i<list.Count;i++)
                {
                    if (list[i].Id == itemId)
                    {
                        selectedRow = i;
                        break;
                    }
                }
            }

            currentUser = user;
            
            backgroundWorker.DoWork += new DoWorkEventHandler(WorkAsync);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(WorkAsyncCompleted);
            this.flagSendMail = isSendMail;
            this.flagSendSms = isSendSms;

            if (isSendMail || isSendSms)
            {
                backgroundWorker.RunWorkerAsync(itemId);
            }
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

                selectedRow = e.RowIndex;

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
                if(selectedRow >= 0)
                {
                    dataGridView.Rows[selectedRow].Cells[6].Value = "Đang gửi";
                    dataGridView.Refresh();
                }

                SendMail(selectedId);

                RefreshItems(sender, e);
            }
        }

        public void SendSms(object sender, EventArgs e)
        {
            if (selectedId > 0)
            {
                if (selectedRow >= 0)
                {
                    dataGridView.Rows[selectedRow].Cells[5].Value = "Đang gửi";
                    dataGridView.Refresh();
                }

                SendSms(selectedId);
                RefreshItems(sender, e);
            }
        }

        private void CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if(e.RowIndex >=0 &&  (e.ColumnIndex == 5 ||e.ColumnIndex == 6))
            {
                string value = Convert.ToString(e.Value);
                dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.White;
                switch(value)
                {
                    case "Đã gửi":
                        dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Green;
                        break;
                    case "Chưa gửi":
                        dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Orange;
                        break;
                    default:
                        dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Red;
                        break;
                }
            }
        }
    }
}

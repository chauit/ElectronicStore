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
using Telerik.WinControls;
using Telerik.WinControls.UI;
using ECommon = ElectronicStore.Common;

namespace ElectronicStore.Main
{
    public partial class MainView : Form
    {
        private User currentUser;
        private bool flagSendMail;
        private bool flagSendSms;
        private int selectedRow;
        public int selectedId = 0;

        public MainView(User user)
        {
            InitializeComponent();
            currentUser = user;

            //var deliveryBiz = new DeliveryBiz();
            //this.radGridView.DataSource = deliveryBiz.GetTemplateData();

            //var orderBiz = new OrderBiz();
            //this.radGridView.Templates[0].DataSource = orderBiz.GetTemplateData();  

            
        }

        protected override void OnLoad(EventArgs e)
        {
            CreateBoundHierarchy();
        }

        public MainView(User user, bool isSendMail = false, bool isSendSms = false, int itemId = 0)
        {
            InitializeComponent();

            selectedRow = -1;

            //CreateBoundHierarchy();

            var list = radGridView.MasterTemplate.DataSource as List<DeliveryTemplate>;

            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
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

        private void CreateBoundHierarchy()
        {
            var deliveryBiz = new DeliveryBiz();

            using (this.radGridView.DeferRefresh())
            {
                this.radGridView.MasterTemplate.Reset();
                this.radGridView.AutoGenerateColumns = true;
                this.radGridView.AllowCellContextMenu = false;
                this.radGridView.ReadOnly = true;                

                this.radGridView.TableElement.RowHeight = 20;
                this.radGridView.DataSource = deliveryBiz.GetTemplateData();
                this.radGridView.MasterTemplate.Columns["Vehicle"].HeaderText = "Xe";
                this.radGridView.MasterTemplate.Columns["DeliveryNo"].HeaderText = "Số giao hàng";
                this.radGridView.MasterTemplate.Columns["DeliveryDate"].HeaderText = "Ngày giao";
                this.radGridView.MasterTemplate.Columns["StartTime"].HeaderText = "Giờ giao";
                this.radGridView.MasterTemplate.Columns["Status"].HeaderText = "Tình trạng";
                this.radGridView.MasterTemplate.Columns["Id"].IsVisible = false;
                this.radGridView.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

                var orderBiz = new OrderBiz();

                GridViewTemplate template = new GridViewTemplate();

                template.AutoGenerateColumns = true;
                template.AllowAddNewRow = false;
                template.AllowCellContextMenu = false;
                template.ReadOnly = true;

                template.DataSource = orderBiz.GetTemplateData();
                this.radGridView.Templates.Add(template);
                template.Columns["OrderNo"].HeaderText = "Mã đơn hàng";
                template.Columns["Status"].HeaderText = "Tình trạng";
                template.Columns["Customer"].HeaderText = "Khách hàng";
                template.Columns["DeliveryDate"].HeaderText = "Ngày giao";
                template.Columns["DeliveryInternal"].HeaderText = "Hình thức vận chuyển";                
                template.Columns["SendMessage"].HeaderText = "Tin nhắn";
                template.Columns["SendEmail"].HeaderText = "mail";
                template.Columns["SendReport"].HeaderText = "Thông báo";
                template.Columns["Id"].IsVisible = false;
                template.Columns["ParentId"].IsVisible = false;
                            
                template.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

                GridViewRelation relation = new GridViewRelation(radGridView.MasterTemplate, template);
                relation.RelationName = "SuppliersProducts";
                relation.ParentColumnNames.Add("Id");
                relation.ChildColumnNames.Add("ParentId");
                this.radGridView.Relations.Add(relation);
            }
        }

        private void CellClick(object sender, GridViewCellEventArgs e)
        {
            var template = e.Column.OwnerTemplate;
            if(template is MasterGridViewTemplate)
            {
                if (e.ColumnIndex == 2)
                {
                    bool isDelivered = false;
                    var delivery = e.Row.DataBoundItem as DeliveryTemplate;
                    if (delivery != null)
                    {
                        selectedId = delivery.Id;
                        isDelivered = string.Equals(delivery.Status, ECommon.Constants.DeliveryStatusDelivered, StringComparison.InvariantCultureIgnoreCase);
                    }

                    selectedRow = e.RowIndex;

                    var menu = AddMenuDelivery(isDelivered, false, false);
                    menu.Show(Cursor.Position.X, Cursor.Position.Y);
                }
            }
            else
            {
                if (e.ColumnIndex == 1)
                {
                    bool isDelivered = false;
                    var order = e.Row.DataBoundItem as OrderTemplate;
                    if (order != null)
                    {
                        selectedId = order.Id;
                        isDelivered = string.Equals(order.Status, ECommon.Constants.DeliveryStatusDelivered, StringComparison.InvariantCultureIgnoreCase);
                    }

                    selectedRow = e.RowIndex;

                    var menu = AddMenuOrder(isDelivered, false, false);
                    menu.Show(Cursor.Position.X, Cursor.Position.Y);
                }
            }
        }

        private ContextMenuStrip AddMenuDelivery(bool isDelivered, bool isSendEmail, bool isSendSms)
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
            mnuSendEmail.Click += new EventHandler(SendEmailDelivery);
            mnuSendSms.Click += new EventHandler(SendSmsDelivery);

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

        private ContextMenuStrip AddMenuOrder(bool isDelivered, bool isSendEmail, bool isSendSms)
        {
            ContextMenuStrip mnu = new ContextMenuStrip();
            
            ToolStripMenuItem mnuSendEmail = new ToolStripMenuItem("Gửi email");
            ToolStripMenuItem mnuSendSms = new ToolStripMenuItem("Gửi SMS");
            ToolStripMenuItem mnuDeliver = new ToolStripMenuItem("Đã giao đơn hàng");

            //Assign event handlers
            mnuDeliver.Click += new EventHandler(DeliverOrder);
            mnuSendEmail.Click += new EventHandler(SendEmailOrder);
            mnuSendSms.Click += new EventHandler(SendSmsOrder);

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

        // Delivery functions
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

        public void SendEmailDelivery(object sender, EventArgs e)
        {
            if (selectedId > 0)
            {
                var biz = new DeliveryBiz();
                var status = biz.SendEmail(selectedId);
                if (!string.IsNullOrEmpty(status.Error))
                {
                    MessageBox.Show(status.Error);
                }                

                RefreshItems(sender, e);
            }
        }

        public void SendSmsDelivery(object sender, EventArgs e)
        {
            if (selectedId > 0)
            {
                var biz = new DeliveryBiz();
                var status = biz.SendSms(selectedId);
                if (!string.IsNullOrEmpty(status.Error))
                {
                    MessageBox.Show(status.Error);
                }

                RefreshItems(sender, e);
            }
        }

        private void WorkAsync(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (selectedRow >= 0)
            {
                //dataGridView.Rows[selectedRow].Cells[6].Value = "Đang gửi";
                //dataGridView.Refresh();
            }

            if (flagSendMail)
            {
                var biz = new DeliveryBiz();
                var status = biz.SendEmail(Convert.ToInt32(e.Argument));
                if (!string.IsNullOrEmpty(status.Error))
                {
                    MessageBox.Show(status.Error);
                } 
            }
            if (flagSendSms)
            {
                var biz = new DeliveryBiz();
                var status = biz.SendSms(Convert.ToInt32(e.Argument));
                if (!string.IsNullOrEmpty(status.Error))
                {
                    MessageBox.Show(status.Error);
                }
            }
        }

        private void WorkAsyncCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RefreshItems(sender, e);
        }

        // View functions
        private void RefreshItems(object sender, EventArgs e)
        {
            CreateBoundHierarchy();
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

        //Order functions
        public void SendEmailOrder(object sender, EventArgs e)
        {
            if (selectedId > 0)
            {
                if (selectedRow >= 0)
                {
                    radGridView.Templates[0].Rows[selectedRow].Cells[7].Value = "Đang gửi";
                    radGridView.Templates[0].Refresh(null);
                }

                var biz = new OrderBiz();
                var status = biz.SendEmail(selectedId);
                if (!string.IsNullOrEmpty(status.Error))
                {
                    MessageBox.Show(status.Error);
                }
                biz.UpdateEmailStatus(selectedId, status.Status);

                RefreshItems(sender, e);
            }
        }

        public void SendSmsOrder(object sender, EventArgs e)
        {
            if (selectedId > 0)
            {
                if (selectedRow >= 0)
                {
                    radGridView.Templates[0].Rows[selectedRow].Cells[6].Value = "Đang gửi";
                    radGridView.Templates[0].Refresh();
                }

                var biz = new OrderBiz();
                var status = biz.SendSms(selectedId);
                if (!string.IsNullOrEmpty(status.Error))
                {
                    MessageBox.Show(status.Error);
                }
                biz.UpdateSmsStatus(selectedId, status.Status);
                RefreshItems(sender, e);
            }
        }

        public void DeliverOrder(object sender, EventArgs e)
        {
            if (selectedId > 0)
            {
                var biz = new OrderBiz();
                biz.UpdateStatus(selectedId, ECommon.Constants.OrderStatusDelivered);

                RefreshItems(sender, e);
            }
        }

        private void CellPaint(object sender, GridViewCellPaintEventArgs e)
        {
            if(e.Cell.RowIndex >= 0)
            {
                var column = e.Cell.ColumnInfo;
                var template = column.OwnerTemplate;

                string value = Convert.ToString(e.Cell.Value);

                if (template is MasterGridViewTemplate)
                {
                    if (column.Index == 5)
                    {
                        switch (value)
                        {
                            case ECommon.Constants.DeliveryStatusDraft:
                                radGridView.Rows[e.Cell.RowIndex].Cells[column.Index].Style.BackColor = Color.Orange;
                                break;
                            case ECommon.Constants.DeliveryStatusDelivered:
                                radGridView.Rows[e.Cell.RowIndex].Cells[column.Index].Style.BackColor = Color.Green;
                                break;
                            default:
                                break;
                        }
                    }
                }
                else
                {
                    if (column.Index == 3)
                    {
                        switch (value)
                        {
                            case ECommon.Constants.OrderStatusDraft:
                                radGridView.Rows[e.Cell.RowIndex].Cells[column.Index].Style.BackColor = Color.Red;
                                break;
                            case ECommon.Constants.OrderStatusDelivered:
                                radGridView.Rows[e.Cell.RowIndex].Cells[column.Index].Style.BackColor = Color.Green;
                                break;
                            default:
                                break;
                        }
                    }
                    if (column.Index == 5)
                    {
                        switch (value)
                        {
                            case ECommon.Constants.OrderEmail1:
                                radGridView.Rows[e.Cell.RowIndex].Cells[column.Index].Style.BackColor = Color.Orange;
                                break;
                            case ECommon.Constants.OrderEmail2:
                                radGridView.Rows[e.Cell.RowIndex].Cells[column.Index].Style.BackColor = Color.Green;
                                break;
                            case ECommon.Constants.OrderEmail3:
                                radGridView.Rows[e.Cell.RowIndex].Cells[column.Index].Style.BackColor = Color.Red;
                                break;
                            default:
                                break;
                        }
                    }
                    if (column.Index == 6)
                    {
                        switch (value)
                        {
                            case ECommon.Constants.OrderSms1:
                                radGridView.Rows[e.Cell.RowIndex].Cells[column.Index].Style.BackColor = Color.Orange;
                                break;
                            case ECommon.Constants.OrderSms2:
                                radGridView.Rows[e.Cell.RowIndex].Cells[column.Index].Style.BackColor = Color.Green;
                                break;
                            case ECommon.Constants.OrderSms3:
                                radGridView.Rows[e.Cell.RowIndex].Cells[column.Index].Style.BackColor = Color.Red;
                                break;
                            default:
                                break;
                        }
                    }
                    if (column.Index == 7)
                    {
                        switch (value)
                        {
                            case ECommon.Constants.OrderReport1:
                                radGridView.Rows[e.Cell.RowIndex].Cells[column.Index].Style.BackColor = Color.Orange;
                                break;
                            case ECommon.Constants.OrderReport2:
                                radGridView.Rows[e.Cell.RowIndex].Cells[column.Index].Style.BackColor = Color.Green;
                                break;
                            case ECommon.Constants.OrderReport3:
                                radGridView.Rows[e.Cell.RowIndex].Cells[column.Index].Style.BackColor = Color.Red;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        private void CellFormat(object sender, CellFormattingEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                var column = e.Column;
                var template = column.OwnerTemplate;

                string value = Convert.ToString(e.CellElement.Value);

                if (template is MasterGridViewTemplate)
                {
                    if (column.Index == 5)
                    {
                        switch (value)
                        {
                            case ECommon.Constants.DeliveryStatusDraft:
                                radGridView.MasterTemplate.Rows[e.RowIndex].Cells[column.Index].Style.BackColor = Color.Orange;
                                break;
                            case ECommon.Constants.DeliveryStatusDelivered:
                                radGridView.MasterTemplate.Rows[e.RowIndex].Cells[column.Index].Style.BackColor = Color.Green;
                                break;
                            default:
                                break;
                        }
                    }
                }
                else
                {
                    if (column.Index == 3)
                    {
                        switch (value)
                        {
                            case ECommon.Constants.OrderStatusDraft:
                                radGridView.Templates[0].Rows[e.RowIndex].Cells[column.Index].Style.BackColor = Color.Red;
                                break;
                            case ECommon.Constants.OrderStatusDelivered:
                                radGridView.Templates[0].Rows[e.RowIndex].Cells[column.Index].Style.BackColor = Color.Green;
                                break;
                            default:
                                break;
                        }
                    }
                    if (column.Index == 5)
                    {
                        switch (value)
                        {
                            case ECommon.Constants.OrderEmail1:
                                radGridView.Templates[0].Rows[e.RowIndex].Cells[column.Index].Style.BackColor = Color.Orange;
                                break;
                            case ECommon.Constants.OrderEmail2:
                                radGridView.Templates[0].Rows[e.RowIndex].Cells[column.Index].Style.BackColor = Color.Green;
                                break;
                            case ECommon.Constants.OrderEmail3:
                                radGridView.Templates[0].Rows[e.RowIndex].Cells[column.Index].Style.BackColor = Color.Red;
                                break;
                            default:
                                break;
                        }
                    }
                    if (column.Index == 6)
                    {
                        switch (value)
                        {
                            case ECommon.Constants.OrderSms1:
                                radGridView.Templates[0].Rows[e.RowIndex].Cells[column.Index].Style.BackColor = Color.Orange;
                                break;
                            case ECommon.Constants.OrderSms2:
                                radGridView.Templates[0].Rows[e.RowIndex].Cells[column.Index].Style.BackColor = Color.Green;
                                break;
                            case ECommon.Constants.OrderSms3:
                                radGridView.Templates[0].Rows[e.RowIndex].Cells[column.Index].Style.BackColor = Color.Red;
                                break;
                            default:
                                break;
                        }
                    }
                    if (column.Index == 7)
                    {
                        switch (value)
                        {
                            case ECommon.Constants.OrderReport1:
                                radGridView.Templates[0].Rows[e.RowIndex].Cells[column.Index].Style.BackColor = Color.Orange;
                                break;
                            case ECommon.Constants.OrderReport2:
                                radGridView.Templates[0].Rows[e.RowIndex].Cells[column.Index].Style.BackColor = Color.Green;
                                break;
                            case ECommon.Constants.OrderReport3:
                                radGridView.Templates[0].Rows[e.RowIndex].Cells[column.Index].Style.BackColor = Color.Red;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }       
    }
}

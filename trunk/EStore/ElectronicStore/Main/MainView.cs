using Business;
using ElectronicStore.Administration;
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
        private bool flagSendMailOrder;
        private bool flagSendSmsOrder;
        private bool flagSendSmsNotification;
        private int selectedRow;
        public int selectedId = 0;
        private int isLeftClick = 0;

        public MainView(User user)
        {
            InitializeComponent();
            currentUser = user;   
        }

        protected override void OnLoad(EventArgs e)
        {
            CreateBoundHierarchy();
        }

        public MainView(User user, bool isSendMail = false, bool isSendSms = false, int itemId = 0)
        {
            InitializeComponent();

            selectedRow = -1;

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

            this.flagSendMail = isSendMail;
            this.flagSendSms = isSendSms;
            this.flagSendMailOrder = false;
            this.flagSendSmsOrder = false;

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
                template.Columns["SendEmail"].HeaderText = "Email";
                template.Columns["SendReport"].HeaderText = "Thông báo";
                template.Columns["Id"].IsVisible = false;
                template.Columns["ParentId"].IsVisible = false;
                template.Columns["Recipient"].IsVisible = false;
                template.Columns["CustomerId"].IsVisible = false;                               
                            
                template.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

                GridViewRelation relation = new GridViewRelation(radGridView.MasterTemplate, template);
                relation.RelationName = "SuppliersProducts";
                relation.ParentColumnNames.Add("Id");
                relation.ChildColumnNames.Add("ParentId");
                this.radGridView.Relations.Add(relation);

                FormatCell();
            }
        }

        private void FormatCell()
        {
            // Delivery
            ConditionalFormattingObject objDeliveryNo = new ConditionalFormattingObject("DeliveryNoCondition", ConditionTypes.NotEqual, "", "", false);
            objDeliveryNo.CellForeColor = Color.Blue;            

            this.radGridView.MasterTemplate.Columns["DeliveryNo"].ConditionalFormattingObjectList.Add(objDeliveryNo);

            ConditionalFormattingObject objDeliveryStatus1 = new ConditionalFormattingObject("DeliveryStatusCondition1", ConditionTypes.Equal,
                ECommon.Constants.DeliveryStatusDraft, string.Empty, false);
            objDeliveryStatus1.CellBackColor = Color.Orange;
            objDeliveryStatus1.CellForeColor = Color.White;

            ConditionalFormattingObject objDeliveryStatus2 = new ConditionalFormattingObject("DeliveryStatusCondition2", ConditionTypes.Equal,
                ECommon.Constants.DeliveryStatusDelivered, string.Empty, false);
            objDeliveryStatus2.CellBackColor = Color.Green;
            objDeliveryStatus2.CellForeColor = Color.White;

            this.radGridView.MasterTemplate.Columns["Status"].ConditionalFormattingObjectList.Add(objDeliveryStatus1);
            this.radGridView.MasterTemplate.Columns["Status"].ConditionalFormattingObjectList.Add(objDeliveryStatus2);

            // Order
            var template = this.radGridView.Templates[0];

            ConditionalFormattingObject objOrderNo = new ConditionalFormattingObject("OrderNoCondition", ConditionTypes.NotEqual, "", "", false);
            objOrderNo.CellForeColor = Color.Blue;
            template.Columns["OrderNo"].ConditionalFormattingObjectList.Add(objOrderNo);

            // Order Status
            ConditionalFormattingObject objOrderStatus1 = new ConditionalFormattingObject("objOrderStatus1", ConditionTypes.Equal,
                ECommon.Constants.OrderStatusDraft, string.Empty, false);
            objOrderStatus1.CellBackColor = Color.Red;
            objOrderStatus1.CellForeColor = Color.White;

            ConditionalFormattingObject objOrderStatus2 = new ConditionalFormattingObject("objOrderStatus2", ConditionTypes.Equal,
                ECommon.Constants.OrderStatusDelivered, string.Empty, false);
            objOrderStatus2.CellBackColor = Color.Green;
            objOrderStatus2.CellForeColor = Color.White;
            template.Columns["Status"].ConditionalFormattingObjectList.Add(objOrderStatus1);
            template.Columns["Status"].ConditionalFormattingObjectList.Add(objOrderStatus2);

            // Order SMS
            ConditionalFormattingObject objOrderSms1 = new ConditionalFormattingObject("objOrderSms1", ConditionTypes.Equal,
                ECommon.Constants.OrderSms1, string.Empty, false);
            objOrderSms1.CellBackColor = Color.Orange;
            objOrderSms1.CellForeColor = Color.White;

            ConditionalFormattingObject objOrderSms2 = new ConditionalFormattingObject("objOrderSms2", ConditionTypes.Equal,
                ECommon.Constants.OrderSms2, string.Empty, false);
            objOrderSms2.CellBackColor = Color.Green;
            objOrderSms2.CellForeColor = Color.White;

            ConditionalFormattingObject objOrderSms3 = new ConditionalFormattingObject("objOrderSms3", ConditionTypes.Equal,
                ECommon.Constants.OrderSms3, string.Empty, false);
            objOrderSms3.CellBackColor = Color.Red;
            objOrderSms3.CellForeColor = Color.White;


            template.Columns["SendMessage"].ConditionalFormattingObjectList.Add(objOrderSms1);
            template.Columns["SendMessage"].ConditionalFormattingObjectList.Add(objOrderSms2);
            template.Columns["SendMessage"].ConditionalFormattingObjectList.Add(objOrderSms3);

            // Order Email
            ConditionalFormattingObject objOrderEmail1 = new ConditionalFormattingObject("objOrderEmail1", ConditionTypes.Equal,
                ECommon.Constants.OrderEmail1, string.Empty, false);
            objOrderEmail1.CellBackColor = Color.Orange;
            objOrderEmail1.CellForeColor = Color.White;

            ConditionalFormattingObject objOrderEmail2 = new ConditionalFormattingObject("objOrderEmail2", ConditionTypes.Equal,
                ECommon.Constants.OrderEmail2, string.Empty, false);
            objOrderEmail2.CellBackColor = Color.Green;
            objOrderEmail2.CellForeColor = Color.White;

            ConditionalFormattingObject objOrderEmail3 = new ConditionalFormattingObject("objOrderEmail3", ConditionTypes.Equal,
                ECommon.Constants.OrderEmail3, string.Empty, false);
            objOrderEmail3.CellBackColor = Color.Red;
            objOrderEmail3.CellForeColor = Color.White;


            template.Columns["SendEmail"].ConditionalFormattingObjectList.Add(objOrderEmail1);
            template.Columns["SendEmail"].ConditionalFormattingObjectList.Add(objOrderEmail2);
            template.Columns["SendEmail"].ConditionalFormattingObjectList.Add(objOrderEmail3);

            // Order Report
            ConditionalFormattingObject objOrderReport1 = new ConditionalFormattingObject("objOrderReport1", ConditionTypes.Equal,
                ECommon.Constants.OrderReport1, string.Empty, false);
            objOrderReport1.CellBackColor = Color.Orange;
            objOrderReport1.CellForeColor = Color.White;

            ConditionalFormattingObject objOrderReport2 = new ConditionalFormattingObject("objOrderReport2", ConditionTypes.Equal,
                ECommon.Constants.OrderReport2, string.Empty, false);
            objOrderReport2.CellBackColor = Color.Green;
            objOrderReport2.CellForeColor = Color.White;

            ConditionalFormattingObject objOrderReport3 = new ConditionalFormattingObject("objOrderReport3", ConditionTypes.Equal,
                ECommon.Constants.OrderReport3, string.Empty, false);
            objOrderReport3.CellBackColor = Color.Red;
            objOrderReport3.CellForeColor = Color.White;


            template.Columns["SendReport"].ConditionalFormattingObjectList.Add(objOrderReport1);
            template.Columns["SendReport"].ConditionalFormattingObjectList.Add(objOrderReport2);
            template.Columns["SendReport"].ConditionalFormattingObjectList.Add(objOrderReport3);
        }

        private void CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;


            var template = e.Column.OwnerTemplate;
            if(template is MasterGridViewTemplate)
            {
                if (isLeftClick == 1 && e.ColumnIndex == 2)
                {
                    var order = e.Row.DataBoundItem as DeliveryTemplate;
                    if (order != null)
                    {
                        selectedId = order.Id;
                    }

                    UpdateSingleItem(sender, e);                    
                }
                else
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
            }
            else
            {
                if (isLeftClick == 1 && e.ColumnIndex == 1)
                {
                    var order = e.Row.DataBoundItem as OrderTemplate;
                    if (order != null)
                    {
                        selectedId = order.Id;
                    }

                    var parent = this.Parent as SplitterPanel;
                    parent.Controls.Clear();

                    var newOrder = new OrderForm(selectedId, currentUser) { Dock = DockStyle.Fill, TopLevel = false };
                    parent.Controls.Add(newOrder);
                    newOrder.Show();

                    this.Close();
                }
                else
                {
                    if (e.ColumnIndex == 1)
                    {
                        bool isDelivered = false;
                        bool isSentEmail = false;
                        bool isSentSms = false;
                        bool isDeliverToOther = false;

                        var order = e.Row.DataBoundItem as OrderTemplate;
                        if (order != null)
                        {
                            selectedId = order.Id;
                            isDelivered = string.Equals(order.Status, ECommon.Constants.DeliveryStatusDelivered, StringComparison.InvariantCultureIgnoreCase);
                            isSentEmail = string.Equals(order.SendEmail, ECommon.Constants.OrderEmail2, StringComparison.InvariantCultureIgnoreCase);
                            isSentSms = string.Equals(order.SendMessage, ECommon.Constants.OrderSms2, StringComparison.InvariantCultureIgnoreCase);

                            if (!string.IsNullOrEmpty(order.Recipient) &&
                                !string.Equals(ECommon.Constants.OrderReport2, order.SendReport, StringComparison.InvariantCultureIgnoreCase) &&
                                string.Equals(ECommon.Constants.OrderSms2, order.SendMessage, StringComparison.InvariantCultureIgnoreCase))
                            {
                                isDeliverToOther = true;
                            }
                        }

                        selectedRow = e.RowIndex;

                        var menu = AddMenuOrder(isDelivered, isSentEmail, isSentSms, isDeliverToOther);
                        menu.Show(Cursor.Position.X, Cursor.Position.Y);
                    }
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

        private ContextMenuStrip AddMenuOrder(bool isDelivered, bool isSendEmail, bool isSendSms, bool isDeliverToOther)
        {
            ContextMenuStrip mnu = new ContextMenuStrip();
            
            ToolStripMenuItem mnuSendEmail = new ToolStripMenuItem("Gửi email");
            ToolStripMenuItem mnuSendSms = new ToolStripMenuItem("Gửi SMS");
            ToolStripMenuItem mnuSendNotification = new ToolStripMenuItem("Gửi tin nhắn thông báo");
            ToolStripMenuItem mnuDeliver = new ToolStripMenuItem("Đã giao đơn hàng");

            //Assign event handlers
            mnuDeliver.Click += new EventHandler(DeliverOrder);
            mnuSendEmail.Click += new EventHandler(SendEmailOrder);
            mnuSendSms.Click += new EventHandler(SendSmsOrder);
            mnuSendNotification.Click += new EventHandler(SendNotification);

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

            if (isDeliverToOther)
            {
                mnu.Items.Add(mnuSendNotification);
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
                if (selectedRow >= 0)
                {
                    if (this.Parent.Parent.Parent is MDI)
                    {
                        var root = this.Parent.Parent.Parent as MDI;
                        if (root != null)
                        {
                            root.UpdateStatus("Đang gửi mail.");
                        }
                    }
                }

                this.flagSendMail = true;
                this.flagSendSms = false;
                this.flagSendMailOrder = false;
                this.flagSendSmsOrder = false;
                this.flagSendSmsNotification = false;

                backgroundWorker.RunWorkerAsync(selectedId);                        
            }
        }

        public void SendSmsDelivery(object sender, EventArgs e)
        {
            if (selectedId > 0)
            {
                if (selectedRow >= 0)
                {
                    if (this.Parent.Parent.Parent is MDI)
                    {
                        var root = this.Parent.Parent.Parent as MDI;
                        if (root != null)
                        {
                            root.UpdateStatus("Đang gửi tin nhắn.");
                        }
                    }
                }

                this.flagSendMail = false;
                this.flagSendSms = true;                
                this.flagSendMailOrder = false;
                this.flagSendSmsOrder = false;
                this.flagSendSmsNotification = false;

                backgroundWorker.RunWorkerAsync(selectedId);                       
            }
        }        

        private void WorkAsync(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (flagSendMailOrder)
            {
                var biz = new OrderBiz();
                var status = biz.SendEmail(Convert.ToInt32(e.Argument), currentUser.FullName);
                if (!string.IsNullOrEmpty(status.Error))
                {
                    MessageBox.Show(status.Error);
                }
                biz.UpdateEmailStatus(Convert.ToInt32(e.Argument), status.Status);
            }

            if (flagSendSmsOrder)
            {
                var biz = new OrderBiz();

                string content = biz.GetSmsContent(Convert.ToInt32(e.Argument));
                var result = MessageBox.Show(content, string.Empty, MessageBoxButtons.OKCancel);
                if(result == System.Windows.Forms.DialogResult.OK)
                {
                    var status = biz.SendSms(Convert.ToInt32(e.Argument), currentUser.FullName);
                    if (!string.IsNullOrEmpty(status.Error))
                    {
                        MessageBox.Show(status.Error);
                    }
                    biz.UpdateSmsStatus(Convert.ToInt32(e.Argument), status.Status);
                }                
            }

            if (flagSendSmsNotification)
            {
                var biz = new OrderBiz();
                string content = biz.GetContentReport(Convert.ToInt32(e.Argument));

                var result = MessageBox.Show(content, string.Empty, MessageBoxButtons.OKCancel);
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    var status = biz.SendReport(Convert.ToInt32(e.Argument));

                    if (!string.IsNullOrEmpty(status.Error))
                    {
                        MessageBox.Show(status.Error);
                    }
                    biz.UpdateNotificationStatus(Convert.ToInt32(e.Argument), status.Status);
                }
            }

            if (flagSendMail)
            {
                var biz = new DeliveryBiz();
                var status = biz.SendEmail(Convert.ToInt32(e.Argument), currentUser.FullName);
                if (!string.IsNullOrEmpty(status.Error))
                {
                    MessageBox.Show(status.Error);
                }
            }

            if (flagSendSms)
            {
                var biz = new DeliveryBiz();
                var status = biz.SendSms(Convert.ToInt32(e.Argument), currentUser.FullName);
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

            if (this.Parent.Parent.Parent is MDI)
            {
                var root = this.Parent.Parent.Parent as MDI;
                if (root != null)
                {
                    root.UpdateStatus(string.Empty);
                }
            }
            
            flagSendMail = false;
            flagSendSms = false;
            flagSendMailOrder = false;
            flagSendSmsOrder = false;            
            backgroundWorker.Dispose();            
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
                    if (this.Parent.Parent.Parent is MDI)
                    {
                        var root = this.Parent.Parent.Parent as MDI;
                        if (root != null)
                        {
                            root.UpdateStatus("Đang gửi mail.");
                        }
                    }
                }

                this.flagSendMail = false;
                this.flagSendSms = false;                
                this.flagSendMailOrder = true;
                this.flagSendSmsOrder = false;
                this.flagSendSmsNotification = false;
            
                backgroundWorker.RunWorkerAsync(selectedId);                
            }
        }

        public void SendSmsOrder(object sender, EventArgs e)
        {
            if (selectedId > 0)
            {
                if (selectedRow >= 0)
                {
                    if (this.Parent.Parent.Parent is MDI)
                    {
                        var root = this.Parent.Parent.Parent as MDI;
                        if (root != null)
                        {
                            root.UpdateStatus("Đang gửi tin nhắn.");
                        }
                    }
                }
                
                this.flagSendSmsOrder = true;
                this.flagSendMailOrder = false;
                this.flagSendMail = false;
                this.flagSendSms = false;
                this.flagSendSmsNotification = false;

                backgroundWorker.RunWorkerAsync(selectedId);                
            }
        }

        public void SendNotification(object sender, EventArgs e)
        {
            if (selectedId > 0)
            {
                if (selectedRow >= 0)
                {
                    if (this.Parent.Parent.Parent is MDI)
                    {
                        var root = this.Parent.Parent.Parent as MDI;
                        if (root != null)
                        {
                            root.UpdateStatus("Đang gửi tin nhắn.");
                        }
                    }
                }

                this.flagSendSmsOrder = false;
                this.flagSendMailOrder = false;
                this.flagSendMail = false;
                this.flagSendSms = false;
                this.flagSendSmsNotification = true;

                backgroundWorker.RunWorkerAsync(selectedId);
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
        
        private void MouseClicked(object sender, MouseEventArgs e)
        {
            isLeftClick = 0;

            if( e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                isLeftClick = 1;
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                isLeftClick = 2;
            }
        }

        private void CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            
            var template = e.Column.OwnerTemplate;
            if (template is MasterGridViewTemplate)
            {
                return;
            }
            else
            {
                if (isLeftClick == 1 && e.ColumnIndex == 2)
                {
                    var order = e.Row.DataBoundItem as OrderTemplate;
                    if (order != null)
                    {
                        selectedId = order.CustomerId;
                    }
                    
                    var newCustomer = new CustomerForm(selectedId, currentUser) { Dock = DockStyle.Fill, MaximizeBox = true };
                    newCustomer.ShowDialog();
                    RefreshItems(sender, e);
                }                
            }
        }       
    }
}

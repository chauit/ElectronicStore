using Business;
using ElectronicStore.Common;
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

namespace ElectronicStore.Main
{
    public partial class FindOrder : Form
    {
        ILogger logger = new Logger();

        public DeliveryForm ParentForm { get; set; }

        public SearchOrder SelectedOrder;

        private List<SearchOrder> ListSelectedOrder { get; set; }

        public FindOrder(List<SearchOrder> orders)
        {
            InitializeComponent();

            dataGridView.AutoGenerateColumns = false;

            LoadCustomer();

            ListSelectedOrder = new List<SearchOrder>();

            foreach(var o in orders)
            {
                ListSelectedOrder.Add(o);
            }
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

        private void Search(object sender, EventArgs e)
        {
            DateTime? date = null;
            int? month = null;
            int? customerId = null;

            string value = Convert.ToString(drlMonth.SelectedItem);
            if (!string.IsNullOrEmpty(value))
            {
                month = int.Parse(value);
            }

            if (drlCustomer.SelectedItem != null && Convert.ToInt32(drlCustomer.SelectedValue) > 0)
            {
                customerId = Convert.ToInt32(Convert.ToString(drlCustomer.SelectedValue));
            }

            if (cboOrderDate.Checked)
            {
                date = dateOrderDate.Value;
            }

            var biz = new OrderBiz();
            var data = biz.SearchOrder(date, month, customerId);

            foreach (var p in ListSelectedOrder)
            {
                var item = data.Find(i => i.Id == p.Id);
                if (item != null)
                {
                    data.Remove(item);
                }
            }

            dataGridView.DataSource = data;
        }

        private void SelectOrderDate(object sender, EventArgs e)
        {
            dateOrderDate.Enabled = cboOrderDate.Checked;
            dateOrderDate.TabStop = cboOrderDate.Checked;

            if (cboOrderDate.Checked)
            {
                dateOrderDate.Focus();    
            }
        }

        private void SelectOrder(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                try
                {
                    int index = dataGridView.SelectedRows[0].Index;
                    SelectedOrder = dataGridView.SelectedRows[0].DataBoundItem as SearchOrder;

                    ListSelectedOrder.Add(SelectedOrder);

                    var deliveryForm = this.ParentForm as DeliveryForm;
                    if (deliveryForm != null)
                    {
                        deliveryForm.UpdateGrid(SelectedOrder);
                    }

                    var list = dataGridView.DataSource as List<SearchOrder>;
                    list.RemoveAt(index);

                    dataGridView.DataSource = null;
                    dataGridView.DataSource = list;
                    dataGridView.Refresh();
                }
                catch (Exception ex)
                {
                    logger.EnterMethod("Select Order");
                    logger.LogException(ex);
                }
            }
        }
    }
}

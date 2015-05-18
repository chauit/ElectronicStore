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

namespace ElectronicStore.Main
{
    public partial class FindProduct : Form
    {
        public SearchOrder SelectedOrder;

        public FindProduct()
        {
            InitializeComponent();

            dataGridView.AutoGenerateColumns = false;

            LoadCustomer();
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
            dataGridView.DataSource = biz.SearchOrder(date, month, customerId);
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
                SelectedOrder = dataGridView.SelectedRows[0].DataBoundItem as SearchOrder;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }
    }
}

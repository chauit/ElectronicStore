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
    public partial class DeliveryView : Form
    {
        public DeliveryView()
        {
            InitializeComponent();

            dataGridView.AutoGenerateColumns = false;

            var biz = new DeliveryBiz();
            dataGridView.DataSource = biz.LoadItems();
            dataGridView.Refresh();
        }

        private void NewItem(object sender, EventArgs e)
        {
            var parent = this.Parent as SplitterPanel;

            parent.Controls.Clear();

            var newDelivery = new DeliveryForm { Dock = DockStyle.Fill, TopLevel = false };
            parent.Controls.Add(newDelivery);
            newDelivery.Show();

            this.Close();
        }

        private void UpdateItem(object sender, EventArgs e)
        {
            var item = dataGridView.SelectedRows[0].DataBoundItem as Delivery;

            var parent = this.Parent as SplitterPanel;
            parent.Controls.Clear();

            var newDelivery = new DeliveryForm(item.Id) { Dock = DockStyle.Fill, TopLevel = false };
            parent.Controls.Add(newDelivery);
            newDelivery.Show();

            this.Close();
        }

        private void DeleteItem(object sender, EventArgs e)
        {
            var items = new List<Delivery>();

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value == "1")
                {
                    items.Add(row.DataBoundItem as Delivery);
                }
            }

            var biz = new DeliveryBiz();
            biz.RemoveItem(items);

            RefreshItems(sender, e);            
        }

        private void RefreshItems(object sender, EventArgs e)
        {
            var biz = new DeliveryBiz();
            dataGridView.DataSource = biz.LoadItems();
            dataGridView.Refresh();
        }       
    }
}

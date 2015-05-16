using Business;
using Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ElectronicStore.Reference
{
    public partial class VehicleView : Form
    {
        public VehicleView()
        {
            InitializeComponent();

            dataGridView.AutoGenerateColumns = false;

            var biz = new VehicleBiz();
            dataGridView.DataSource = biz.LoadItems();
            dataGridView.Refresh();
        }

        private void NewItem(object sender, EventArgs e)
        {
            var newVehicle = new VehicleForm();
            var result = newVehicle.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                RefreshItems(sender, e);
            }
        }

        private void UpdateItem(object sender, EventArgs e)
        {
            var item = dataGridView.SelectedRows[0].DataBoundItem as Vehicle;

            var newVehicle = new VehicleForm(item.Id);
            var result = newVehicle.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                RefreshItems(sender, e);
            }
        }

        private void DeleteItem(object sender, EventArgs e)
        {
            var items = new List<Vehicle>();

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value == "1")
                {
                    items.Add(row.DataBoundItem as Vehicle);
                }
            }

            var biz = new VehicleBiz();
            biz.RemoveItem(items);

            RefreshItems(sender, e);            
        }

        private void RefreshItems(object sender, EventArgs e)
        {
            var biz = new VehicleBiz();
            dataGridView.DataSource = biz.LoadItems();
            dataGridView.Refresh();
        }       
    }
}

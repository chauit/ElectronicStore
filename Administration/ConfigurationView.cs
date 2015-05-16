using Business;
using Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ElectronicStore.Administration
{
    public partial class ConfigurationView : Form
    {
        public ConfigurationView()
        {
            InitializeComponent();

            dataGridView.AutoGenerateColumns = false;

            var biz = new ConfigurationBiz();
            dataGridView.DataSource = biz.LoadItems();
            dataGridView.Refresh();            
        }

        private void NewItem(object sender, EventArgs e)
        {
            var newConfiguration = new ConfigurationForm();
            var result = newConfiguration.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                RefreshItems(sender, e);
            }
        }

        private void UpdateItem(object sender, EventArgs e)
        {
            var role = dataGridView.SelectedRows[0].DataBoundItem as Configuration;
            if (role != null)
            {
                var newConfiguration = new ConfigurationForm(role.Id);
                var result = newConfiguration.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    RefreshItems(sender, e);
                }
            }
        }

        private void DeleteItem(object sender, EventArgs e)
        {
            var items = new List<Configuration>();

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value == "1")
                {
                    items.Add(row.DataBoundItem as Configuration);
                }
            }

            if (items.Count > 0)
            {
                var biz = new ConfigurationBiz();
                biz.RemoveItem(items);

                RefreshItems(sender, e);
            }
        }

        private void RefreshItems(object sender, EventArgs e)
        {
            var biz = new ConfigurationBiz();
            dataGridView.DataSource = biz.LoadItems();
            dataGridView.Refresh();
        }       
    }
}

using Business;
using Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ElectronicStore.Administration
{
    public partial class CityView : Form
    {
        private User currentUser;
        public CityView(User user)
        {
            InitializeComponent();

            currentUser = user;

            dataGridView.AutoGenerateColumns = false;

            var biz = new CityBiz();
            dataGridView.DataSource = biz.LoadItems();
            dataGridView.Refresh();            
        }

        private void NewItem(object sender, EventArgs e)
        {
            var newCity = new CityForm(currentUser);
            var result = newCity.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                RefreshItems(sender, e);
            }
        }

        private void UpdateItem(object sender, EventArgs e)
        {
            var city = dataGridView.SelectedRows[0].DataBoundItem as City;
            if (city != null)
            {
                var newCity = new CityForm(city.Id, currentUser);
                var result = newCity.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    RefreshItems(sender, e);
                }
            }
        }

        private void DeleteItem(object sender, EventArgs e)
        {
            var items = new List<City>();

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value == "1")
                {
                    items.Add(row.DataBoundItem as City);
                }
            }

            if (items.Count > 0)
            {
                var biz = new CityBiz();
                biz.RemoveItem(items);

                RefreshItems(sender, e);
            }
        }

        private void RefreshItems(object sender, EventArgs e)
        {
            var biz = new CityBiz();
            dataGridView.DataSource = biz.LoadItems();
            dataGridView.Refresh();
        }       
    }
}

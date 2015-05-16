using Business;
using Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ElectronicStore.Administration
{
    public partial class UserView : Form
    {
        public UserView()
        {
            InitializeComponent();

            dataGridView.AutoGenerateColumns = false;

            var biz = new UserBiz();
            dataGridView.DataSource = biz.LoadItems();
            dataGridView.Refresh();
        }

        private void NewItem(object sender, EventArgs e)
        {
            var newUser = new UserForm();
            var result = newUser.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                RefreshItems(sender, e);
            }
        }

        private void UpdateItem(object sender, EventArgs e)
        {
            var user = dataGridView.SelectedRows[0].DataBoundItem as User;

            var newUser = new UserForm(user.Id);
            var result = newUser.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                RefreshItems(sender, e);
            }
        }

        private void DeleteItem(object sender, EventArgs e)
        {
            var items = new List<User>();

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value == "1")
                {
                    items.Add(row.DataBoundItem as User);
                }
            }

            var biz = new UserBiz();
            biz.RemoveItem(items);

            RefreshItems(sender, e);            
        }

        private void RefreshItems(object sender, EventArgs e)
        {
            var biz = new UserBiz();
            dataGridView.DataSource = biz.LoadItems();
            dataGridView.Refresh();
        }       
    }
}

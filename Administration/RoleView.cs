using Business;
using Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ElectronicStore.Administration
{
    public partial class RoleView : Form
    {
        private User currentUser;
        public RoleView(User user)
        {
            InitializeComponent();

            currentUser = user;

            dataGridView.AutoGenerateColumns = false;

            var biz = new RoleBiz();
            dataGridView.DataSource = biz.LoadItems();
            dataGridView.Refresh();            
        }

        private void NewItem(object sender, EventArgs e)
        {
            var newRole = new RoleForm(currentUser);
            var result = newRole.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                RefreshItems(sender, e);
            }
        }

        private void UpdateItem(object sender, EventArgs e)
        {
            var role = dataGridView.SelectedRows[0].DataBoundItem as Role;
            if (role != null)
            {
                var newRole = new RoleForm(role.Id, currentUser);
                var result = newRole.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    RefreshItems(sender, e);
                }
            }
        }

        private void DeleteItem(object sender, EventArgs e)
        {
            var items = new List<Role>();

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value == "1")
                {
                    items.Add(row.DataBoundItem as Role);
                }
            }

            if (items.Count > 0)
            {
                var biz = new RoleBiz();
                biz.RemoveItem(items);

                RefreshItems(sender, e);
            }
        }

        private void RefreshItems(object sender, EventArgs e)
        {
            var biz = new RoleBiz();
            dataGridView.DataSource = biz.LoadItems();
            dataGridView.Refresh();
        }       
    }
}

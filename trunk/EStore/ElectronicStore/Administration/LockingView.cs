using Business;
using Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ElectronicStore.Administration
{
    public partial class LockingView : Form
    {
        private User currentUser;
        public LockingView(User user)
        {
            InitializeComponent();

            dataGridView.AutoGenerateColumns = false;

            currentUser = user;

            var biz = new LockingBiz();
            dataGridView.DataSource = biz.LoadItems();
            dataGridView.Refresh();
        }
        
        private void RefreshItems(object sender, EventArgs e)
        {
            var biz = new LockingBiz();
            dataGridView.DataSource = biz.LoadItems();
            dataGridView.Refresh();
        }

        private void Unlock(object sender, EventArgs e)
        {
            var locking = dataGridView.SelectedRows[0].DataBoundItem as LockingTemplate;
            if (locking != null)
            {
                var biz = new LockingBiz();
                biz.UnlockItem(locking.TableName, locking.ItemId, locking.CurrentUserId);

                RefreshItems(sender, e);
            }
        }       
    }
}

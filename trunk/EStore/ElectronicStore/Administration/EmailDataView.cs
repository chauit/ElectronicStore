using Business;
using Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ElectronicStore.Administration
{
    public partial class EmailDataView : Form
    {
        private User currentUser;
        public EmailDataView(User user)
        {
            InitializeComponent();

            dataGridView.AutoGenerateColumns = false;

            currentUser = user;

            var biz = new EmailDataBiz();
            dataGridView.DataSource = biz.LoadItems();
            dataGridView.Refresh();
        }
        
        private void RefreshItems(object sender, EventArgs e)
        {
            var biz = new LockingBiz();
            dataGridView.DataSource = biz.LoadItems();
            dataGridView.Refresh();
        }

        private void ViewContent(object sender, EventArgs e)
        {
            var emailData = dataGridView.SelectedRows[0].DataBoundItem as EmailData;
            if (emailData != null)
            {
                var newEmailData = new EmailDataForm(emailData.Id, currentUser);
                var result = newEmailData.ShowDialog();                
            }
        }
 
    }
}

using Business;
using Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ElectronicStore.Administration
{
    public partial class SmsDataView : Form
    {
        private User currentUser;
        public SmsDataView(User user)
        {
            InitializeComponent();

            dataGridView.AutoGenerateColumns = false;

            currentUser = user;

            var biz = new SmsDataBiz();
            dataGridView.DataSource = biz.LoadItems();
            dataGridView.Refresh();
        }
        
        private void RefreshItems(object sender, EventArgs e)
        {
            var biz = new SmsDataBiz();
            dataGridView.DataSource = biz.LoadItems();
            dataGridView.Refresh();
        }

        private void ViewContent(object sender, EventArgs e)
        {
            var smsData = dataGridView.SelectedRows[0].DataBoundItem as SmsData;
            if (smsData != null)
            {
                var newSmsData = new SmsDataForm(smsData.Id, currentUser);
                var result = newSmsData.ShowDialog();                
            }
        }
 
    }
}

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

namespace ElectronicStore.Administration
{
    public partial class SmsView : Form
    {
        public SmsView()
        {
            InitializeComponent();

            dataGridView.AutoGenerateColumns = false;

            var biz = new SmsBiz();
            dataGridView.DataSource = biz.LoadItems();
            dataGridView.Refresh();
        }

        private void NewItem(object sender, EventArgs e)
        {
            var newSms = new SmsForm();
            var result = newSms.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                RefreshItems(sender, e);
            }
        }

        private void UpdateItem(object sender, EventArgs e)
        {
            var item = dataGridView.SelectedRows[0].DataBoundItem as Sms;

            var newSms = new SmsForm(item.Id);
            var result = newSms.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                RefreshItems(sender, e);
            }
        }

        private void DeleteItem(object sender, EventArgs e)
        {
            var items = new List<Sms>();

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value == "1")
                {
                    items.Add(row.DataBoundItem as Sms);
                }
            }

            var biz = new SmsBiz();
            biz.RemoveItem(items);

            RefreshItems(sender, e);            
        }

        private void RefreshItems(object sender, EventArgs e)
        {
            var biz = new SmsBiz();
            dataGridView.DataSource = biz.LoadItems();
            dataGridView.Refresh();
        }       
    }
}

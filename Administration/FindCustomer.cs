using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Business;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using ElectronicStore.Views;
using Model;

namespace ElectronicStore.Administration
{
    public partial class FindCustomer : Form
    {
        public Customer SelectedItem;

        public FindCustomer(string name)
        {
            InitializeComponent();

            dataGridView.AutoGenerateColumns = false;

            textCustomerName.Text = name;

            var biz = new CustomerBiz();
            dataGridView.DataSource = biz.SearchCustomerByName(name);
            dataGridView.Refresh();
        }

        private void Search(object sender, EventArgs e)
        {
            var biz = new CustomerBiz();
            dataGridView.DataSource = biz.SearchCustomerByName(textCustomerName.Text);
            dataGridView.Refresh();            
        }

        private void SelectCustomer(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                int index = dataGridView.SelectedRows[0].Index;
                var customer = dataGridView.SelectedRows[0].DataBoundItem as Customer;

                if(customer != null)
                {
                    SelectedItem = customer;

                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
            }
        }
    }

}
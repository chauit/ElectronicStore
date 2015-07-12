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
    public partial class SearchCustomer : Form
    {
        private readonly User _currentUser;

        public SearchCustomer(User user, string condition, bool isShowResult = false, int id = 0)
        {
            InitializeComponent();

            dataGridViewDetail.Visible = false;
            dataGridViewDetail.AutoGenerateColumns = false;
            dataGridView.AutoGenerateColumns = false;

            textCustomerName.Text = condition;

            var biz = new CustomerBiz();
            dataGridView.DataSource = biz.SearchCustomerByName(condition);
            dataGridView.Refresh();

            _currentUser = user;

            LoadCustomer();

            if(isShowResult)
            {
                ViewCustomer(id);
            }
        }

        private void Search(object sender, EventArgs e)
        {
            dataGridViewDetail.Visible = false;

            var biz = new CustomerBiz();
            dataGridView.DataSource = biz.SearchCustomerByName(textCustomerName.Text);
            dataGridView.Refresh();            
        }

        private void LoadCustomer()
        {
            var biz = new CustomerBiz();
            var items = biz.LoadAllCustomerName();
            AutoCompleteStringCollection list = new AutoCompleteStringCollection();
            list.AddRange(items.ToArray());
            textCustomerName.AutoCompleteCustomSource = null;
            textCustomerName.AutoCompleteCustomSource = list;
        }

        private void ViewCustomer(int id)
        {
            var biz = new CustomerBiz();
            var data = biz.SearchCustomer(id);

            if (data != null && data.Count > 0)
            {
                dataGridViewDetail.Visible = true;

                dataGridViewDetail.DataSource = null;
                dataGridViewDetail.DataSource = data;
                dataGridViewDetail.Refresh();
            }
        }

        private void SelectCustomer(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                int index = dataGridView.SelectedRows[0].Index;
                var customer = dataGridView.SelectedRows[0].DataBoundItem as Customer;

                if(customer != null)
                {
                    var biz = new CustomerBiz();
                    var data = biz.SearchCustomer(customer.Id);

                    if(data != null && data.Count > 0)
                    {
                        dataGridViewDetail.Visible = true;

                        dataGridViewDetail.DataSource = null;
                        dataGridViewDetail.DataSource = data;
                        dataGridViewDetail.Refresh();
                    }
                }
            }
        }
    }

}
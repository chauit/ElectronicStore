using System;
using System.Windows.Forms;
using Business;
using Model;
using System.Collections.Generic;
using ElectronicStore.Common;

namespace ElectronicStore.Main
{
    public partial class FindProductLD : Form
    {
        ElectronicStore.Common.ILogger logger = new ElectronicStore.Common.Logger();
        public OrderForm ParentForm { get; set; }

        public SearchProductLD SelectedProduct;
        private List<SearchProductLD> ListSelectedProduct { get; set; }

        public FindProductLD(List<SearchProductLD> listProduct)
        {
            InitializeComponent();

            dataGridView.AutoGenerateColumns = false;

            LoadProductType();

            ListSelectedProduct = new List<SearchProductLD>();

            foreach(var product in listProduct)
            {
                ListSelectedProduct.Add(product);
            }
        }

        private void LoadProductType()
        {
            var biz = new ProductTypeBiz();
            var items = biz.LoadItems();
            items.Insert(0, new ProductType());

            drlProductType.Items.Clear();
            drlProductType.DataSource = items;
            drlProductType.DisplayMember = "Name";
            drlProductType.ValueMember = "Id";
        }

        private void Search(object sender, EventArgs e)
        {
            int? typeId = null;                        

            var biz = new ProductLDBiz();
            var data = biz.SearchProduct(drlProductType.Text, textName.Text, textCode.Text);

            foreach(var p in ListSelectedProduct)
            {
                var item = data.Find(i => i.Code == p.Code);
                if(item != null)
                {
                    data.Remove(item);
                }
            }

            dataGridView.DataSource = data;
        }
        
        private void SelectProduct(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                try
                {
                    int index = dataGridView.SelectedRows[0].Index;
                    SelectedProduct = dataGridView.SelectedRows[0].DataBoundItem as SearchProductLD;

                    ListSelectedProduct.Add(SelectedProduct);

                    var orderForm = this.ParentForm as OrderForm;
                    if (orderForm != null)
                    {
                        orderForm.UpdateGridLD(SelectedProduct);
                    }

                    var list = dataGridView.DataSource as List<SearchProductLD>;
                    list.RemoveAt(index);

                    dataGridView.DataSource = null;
                    dataGridView.DataSource = list;
                    dataGridView.Refresh();
                }
                catch(Exception ex)
                {
                    logger.EnterMethod("Select Product LD");
                    logger.LogException(ex);
                }
            }
        }
    }
}

using System;
using System.Windows.Forms;
using Business;
using Model;
using System.Collections.Generic;

namespace ElectronicStore.Main
{
    public partial class FindProduct : Form
    {
        public OrderForm ParentForm { get; set; }

        public SearchProduct SelectedProduct;
        private List<SearchProduct> ListSelectedProduct { get; set; }

        public FindProduct(List<SearchProduct> listProduct)
        {
            InitializeComponent();

            dataGridView.AutoGenerateColumns = false;

            LoadProductType();

            ListSelectedProduct = new List<SearchProduct>();

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
                        
            if (drlProductType.SelectedItem != null && Convert.ToInt32(drlProductType.SelectedValue) > 0)
            {
                typeId = Convert.ToInt32(Convert.ToString(drlProductType.SelectedValue));
            }           

            var biz = new ProductBiz();
            var data = biz.SearchProduct(typeId, textName.Text, textCode.Text);

            foreach(var p in ListSelectedProduct)
            {
                var item = data.Find(i => i.Id == p.Id);
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
                int index = dataGridView.SelectedRows[0].Index;
                SelectedProduct = dataGridView.SelectedRows[0].DataBoundItem as SearchProduct;

                ListSelectedProduct.Add(SelectedProduct);

                var orderForm = this.ParentForm as OrderForm;
                if(orderForm != null)
                {
                    orderForm.UpdateGrid(SelectedProduct);
                }

                var list = dataGridView.DataSource as List<SearchProduct>;
                list.RemoveAt(index);

                dataGridView.DataSource = list;
                dataGridView.Refresh();
            }
        }
    }
}

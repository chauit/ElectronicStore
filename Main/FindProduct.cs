using System;
using System.Windows.Forms;
using Business;
using Model;

namespace ElectronicStore.Main
{
    public partial class FindProduct : Form
    {
        public SearchProduct SelectedProduct;

        public FindProduct()
        {
            InitializeComponent();

            dataGridView.AutoGenerateColumns = false;

            LoadProductType();
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
            dataGridView.DataSource = biz.SearchProduct(typeId, textName.Text, textCode.Text);
        }
        
        private void SelectProduct(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                SelectedProduct = dataGridView.SelectedRows[0].DataBoundItem as SearchProduct;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}

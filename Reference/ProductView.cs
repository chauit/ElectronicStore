using Business;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using ElectronicStore.Administration;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ElectronicStore.Views;

namespace ElectronicStore.Reference
{
    public partial class ProductView : Form
    {
        private User currentUser;
        public ProductView(User user)
        {
            InitializeComponent();

            dataGridView.AutoGenerateColumns = false;

            var biz = new ProductBiz();
            dataGridView.DataSource = biz.LoadItems();
            dataGridView.Refresh();

            currentUser = user;
        }

        private void NewItem(object sender, EventArgs e)
        {
            var newProduct = new ProductForm(currentUser);
            var result = newProduct.ShowDialog();
            if (result == DialogResult.OK)
            {
                RefreshItems(sender, e);
            }
        }

        private void UpdateItem(object sender, EventArgs e)
        {
            var role = dataGridView.SelectedRows[0].DataBoundItem as Product;

            var newProduct = new ProductForm(role.Id, currentUser);
            var result = newProduct.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                RefreshItems(sender, e);
            }
        }

        private void DeleteItem(object sender, EventArgs e)
        {
            var items = new List<Product>();

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value == "1")
                {
                    items.Add(row.DataBoundItem as Product);
                }
            }

            var biz = new ProductBiz();
            biz.RemoveItem(items);

            RefreshItems(sender, e);            
        }

        private void RefreshItems(object sender, EventArgs e)
        {
            var biz = new ProductBiz();
            dataGridView.DataSource = biz.LoadItems();
            dataGridView.Refresh();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Excel Files(.xls)|*.xls|Excel Files(.xlsx)|*.xlsx| Excel Files(*.xlsm)|*.xlsm";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ImportDataSet(ofd.FileName);

                    RefreshItems();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Common.Constants.Messages.CannotImportProduct);
                }
            }
        }

        private void RefreshItems()
        {
            var biz = new ProductBiz();
            dataGridView.DataSource = biz.LoadItems();
            dataGridView.Refresh();
        }

        private void ImportDataSet(string fileName)
        {
            var biz = new ProductBiz();

            using (var document = SpreadsheetDocument.Open(fileName, false))
            {
                var wbPart = document.WorkbookPart;
                var theSheet = wbPart.Workbook.Descendants<Sheet>().FirstOrDefault();

                var wsPart = (WorksheetPart)(wbPart.GetPartById(theSheet.Id));
                
                for (var i = 2; i <= wsPart.Worksheet.Descendants<Row>().Count(); i++)
                {
                    var product = new Product
                    {
                        Code = IOReader.GetCellValue(document, theSheet, "A" + i),
                        Name = IOReader.GetCellValue(document, theSheet, "B" + i),
                    };

                    decimal number = 0;
                    var price = IOReader.GetCellValue(document, theSheet, "C" + i);
                    if (!string.IsNullOrEmpty(price) && decimal.TryParse(price, out number))
                    {
                        product.Price = number;
                    }

                    int number1 = 0;
                    var productTypeId = IOReader.GetCellValue(document, theSheet, "D" + i);
                    if (!string.IsNullOrEmpty(productTypeId) && int.TryParse(productTypeId, out number1))
                    {
                        product.ProductTypeId = number1;
                    }

                    var item = biz.LoadItems().FirstOrDefault(p => p.Code == product.Code);

                    if (item!=null)
                    {
                        product.Id = item.Id;
                        product.Code = item.Code;
                        product.Name = item.Name;
                        product.ProductTypeId = item.ProductTypeId;
                        biz.UpdateItem(product);
                        break;
                    }
                    biz.SaveItem(product);
                    break;
                }
            }
        }
    }
}

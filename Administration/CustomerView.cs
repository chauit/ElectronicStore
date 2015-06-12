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
    public partial class CustomerView : Form
    {
        private readonly User _currentUser;

        public CustomerView(User user)
        {
            InitializeComponent();

            dataGridView.AutoGenerateColumns = false;

            var biz = new CustomerBiz();
            dataGridView.DataSource = biz.LoadItems();
            dataGridView.Refresh();

            _currentUser = user;
        }

        private void NewItem(object sender, EventArgs e)
        {
            var newCustomer = new CustomerForm(_currentUser);
            var result = newCustomer.ShowDialog();
            if (result == DialogResult.OK)
            {
                RefreshItems(sender, e);
            }
        }

        private void UpdateItem(object sender, EventArgs e)
        {
            var user = dataGridView.SelectedRows[0].DataBoundItem as Customer;

            var newCustomer = new CustomerForm(user.Id, _currentUser);
            var result = newCustomer.ShowDialog();
            if (result == DialogResult.OK)
            {
                RefreshItems(sender, e);
            }
        }

        private void DeleteItem(object sender, EventArgs e)
        {
            var items = new List<Customer>();

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value == "1")
                {
                    items.Add(row.DataBoundItem as Customer);
                }
            }

            var biz = new CustomerBiz();
            biz.RemoveItem(items);

            RefreshItems(sender, e);
        }

        private void RefreshItems(object sender, EventArgs e)
        {
            var biz = new CustomerBiz();
            dataGridView.DataSource = biz.LoadItems();
            dataGridView.Refresh();
        }

        private void RefreshItems()
        {
            var biz = new CustomerBiz();
            dataGridView.DataSource = biz.LoadItems();
            dataGridView.Refresh();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Excel Files(.xls)|*.xls|Excel Files(.xlsx)|*.xlsx| Excel Files(*.xlsm)|*.xlsm";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ExportDataSet(ofd.FileName);
            }
        }

        private void ExportDataSet(string fileName)
        {
            var dataTable = new DataTable();
            using (var document = SpreadsheetDocument.Open(fileName, true))
            {
                var wbPart = document.WorkbookPart;
                Sheet theSheet = wbPart.Workbook.Descendants<Sheet>().FirstOrDefault(s => s.Name == "Customer");
                if (theSheet == null)
                {
                    throw new ArgumentException("Sheet name is invalid");
                }
                var wsPart = (WorksheetPart)(wbPart.GetPartById(theSheet.Id));
                var customerListing = new List<CustomerItem>();
                for (var i = 2; i <= wsPart.Worksheet.Descendants<Row>().Count(); i++)
                {
                    var item = new CustomerItem
                    {
                        FirstName = IOReader.GetCellValue(document, theSheet, "B" + i),
                        LastName = IOReader.GetCellValue(document, theSheet, "C" + i),
                        Address1 = IOReader.GetCellValue(document, theSheet, "D" + i),
                        Address2 = IOReader.GetCellValue(document, theSheet, "E" + i),
                        City = IOReader.GetCellValue(document, theSheet, "F" + i),
                        PostalCode = IOReader.GetCellValue(document, theSheet, "G" + i),
                        Tel = IOReader.GetCellValue(document, theSheet, "H" + i),
                        Mobile1 = IOReader.GetCellValue(document, theSheet, "I" + i),
                        Mobile2 = IOReader.GetCellValue(document, theSheet, "J" + i),
                        Email1 = IOReader.GetCellValue(document, theSheet, "K" + i),
                        Email2 = IOReader.GetCellValue(document, theSheet, "L" + i),
                        OtherInformation = IOReader.GetCellValue(document, theSheet, "N" + i),
                        Segment = IOReader.GetCellValue(document, theSheet, "O" + i),
                    };

                    var delivery = IOReader.GetCellValue(document, theSheet, "M" + i);
                    if (!string.IsNullOrEmpty(delivery))
                    {
                        try
                        {
                            item.Delivery = int.Parse(delivery);
                        }
                        catch
                        {
                            item.Message = string.Format("Giá trị {0} của Delivery Field không đúng", delivery);
                        }
                    }

                    customerListing.Add(item);
                }

                var frmPreview = new CustomerImportPreviewForm(customerListing, _currentUser);

                frmPreview.OnLoadData += RefreshItems;
                frmPreview.ShowDialog();
            }
        }
    }


    public class IOReader
    {
        public static SpreadsheetDocument LoadDocument(string inputPath)
        {
            SpreadsheetDocument document =
                SpreadsheetDocument.Open(inputPath, false);
            return document;
        }

        public static string GetCellValue(SpreadsheetDocument document, Sheet theSheet, string addressName)
        {
            string value = null;

            WorkbookPart wbPart = document.WorkbookPart;

            var wsPart =
                (WorksheetPart)(wbPart.GetPartById(theSheet.Id));
            Cell theCell = wsPart.Worksheet.Descendants<Cell>().FirstOrDefault(c => c.CellReference == addressName);

            if (theCell != null)
            {
                value = theCell.InnerText;

                if (theCell.DataType != null)
                {
                    switch (theCell.DataType.Value)
                    {
                        case CellValues.SharedString:
                            var stringTable = wbPart.
                                GetPartsOfType<SharedStringTablePart>().FirstOrDefault();
                            if (stringTable != null)
                            {
                                value = stringTable.SharedStringTable.
                                    ElementAt(int.Parse(value)).InnerText;
                            }
                            break;

                        case CellValues.Boolean:
                            switch (value)
                            {
                                case "0":
                                    value = "FALSE";
                                    break;
                                default:
                                    value = "TRUE";
                                    break;
                            }
                            break;
                    }
                }
            }

            return value;
        }
    }
}
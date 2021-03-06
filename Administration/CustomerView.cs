﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Business;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using ElectronicStore.Views;
using Model;
using Telerik.WinControls.UI;

namespace ElectronicStore.Administration
{
    public partial class CustomerView : Form
    {
        private readonly User _currentUser;

        public CustomerView(User user)
        {
            InitializeComponent();
            
            var biz = new CustomerBiz();
            radGridView.DataSource = biz.LoadItems();
            radGridView.Refresh();

            _currentUser = user;

            buttonSelectItems.Text = "Chọn";
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
            var user = radGridView.SelectedRows[0].DataBoundItem as Customer;

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

            foreach (var row in radGridView.Rows)
            {
                if (row.Cells[0].Value != null && Convert.ToBoolean(row.Cells[0].Value))                
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
            radGridView.DataSource = biz.LoadItems();
            radGridView.Refresh();
        }

        private void RefreshItems()
        {
            var biz = new CustomerBiz();
            radGridView.DataSource = biz.LoadItems();
            radGridView.Refresh();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Excel Files(.xls)|*.xls|Excel Files(.xlsx)|*.xlsx| Excel Files(*.xlsm)|*.xlsm";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ExportDataSet(ofd.FileName);

                    RefreshItems();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(Common.Constants.Messages.CannotImportCustomer);
                }
            }
        }

        private void ExportDataSet(string fileName)
        {            
            var biz = new CustomerBiz();

            using (var document = SpreadsheetDocument.Open(fileName, false))
            {
                var wbPart = document.WorkbookPart;
                Sheet theSheet = wbPart.Workbook.Descendants<Sheet>().FirstOrDefault();
                
                var wsPart = (WorksheetPart)(wbPart.GetPartById(theSheet.Id));
                var customerListing = new List<CustomerItem>();
                for (var i = 2; i <= wsPart.Worksheet.Descendants<Row>().Count(); i++)
                {
                    int number = 0;
                    var item = new Customer
                    {
                        SMS = IOReader.GetCellValue(document, theSheet, "B" + i),
                        FullName = IOReader.GetCellValue(document, theSheet, "C" + i),
                        Mst = IOReader.GetCellValue(document, theSheet, "D" + i),
                        Company = IOReader.GetCellValue(document, theSheet, "E" + i),
                        Address1 = IOReader.GetCellValue(document, theSheet, "F" + i),
                        Address2 = IOReader.GetCellValue(document, theSheet, "G" + i),
                        City = IOReader.GetCellValue(document, theSheet, "H" + i),
                        PostalCode = IOReader.GetCellValue(document, theSheet, "I" + i),
                        Tel = IOReader.GetCellValue(document, theSheet, "J" + i),
                        Mobile1 = IOReader.GetCellValue(document, theSheet, "K" + i),
                        Mobile2 = IOReader.GetCellValue(document, theSheet, "L" + i),
                        Email1 = IOReader.GetCellValue(document, theSheet, "M" + i),
                        Email2 = IOReader.GetCellValue(document, theSheet, "N" + i),
                        OtherInformation = IOReader.GetCellValue(document, theSheet, "P" + i),
                        Segment = IOReader.GetCellValue(document, theSheet, "Q" + i),
                    };

                    var delivery = IOReader.GetCellValue(document, theSheet, "O" + i);
                    if (!string.IsNullOrEmpty(delivery) && int.TryParse(delivery, out number))
                    {
                        item.Delivery = number;
                    }

                    if (!string.IsNullOrEmpty(item.FullName))
                    {
                        biz.SaveItem(item);                        
                    }                    
                }                
            }
        }

        private void SelectAllItem(object sender, EventArgs e)
        {
            bool isSelected = buttonSelectItems.Text == "Chọn";
            
            foreach(var row in radGridView.Rows)
            {
                row.Cells[0].Value = isSelected;
            }

            if(isSelected)
            {
                buttonSelectItems.Text = "Không chọn";
            }
            else
            {
                buttonSelectItems.Text = "Chọn";
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
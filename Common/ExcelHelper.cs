using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace ElectronicStore.Common
{
    public class ExcelHelper
    {
        /// <summary>
        /// Function to export to excel
        /// </summary>
        /// <typeparam name="T">Type of object to export</typeparam>
        /// <param name="data">List of objects</param>
        /// <param name="stream">Empty memory stream for output</param>
        /// <param name="sheetName">Up to 31 characters</param>
        /// <param name="columns">Key is property name, Value is display name of column</param>
        public static void ExportToExcel<T>(List<T> data, MemoryStream stream, string sheetName, Dictionary<string, string> columns)
        {
            using (var spreadSheet = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook))
            {
                spreadSheet.AddWorkbookPart();
                spreadSheet.WorkbookPart.Workbook = new Workbook();

                spreadSheet.WorkbookPart.Workbook.Append(new BookViews(new WorkbookView()));

                var workbookStylesPart = spreadSheet.WorkbookPart.AddNewPart<WorkbookStylesPart>();
                var stylesheet = new Stylesheet();
                workbookStylesPart.Stylesheet = stylesheet;

                var newWorksheetPart = spreadSheet.WorkbookPart.AddNewPart<WorksheetPart>();
                spreadSheet.WorkbookPart.WorksheetParts.First().Worksheet = new Worksheet();

                spreadSheet.WorkbookPart.WorksheetParts.First().Worksheet.AppendChild(new SheetData());

                var worksheet = newWorksheetPart.Worksheet;
                var sheetData = worksheet.GetFirstChild<SheetData>();

                // Creates the text over each column.
                AddHeaderRow(sheetData, columns.Select(x => x.Value).ToList());

                AddRows(data, columns.Select(x => x.Key).ToList(), sheetData);

                newWorksheetPart.Worksheet.Save();

                spreadSheet.WorkbookPart.Workbook.AppendChild(new Sheets());
                spreadSheet.WorkbookPart.Workbook.GetFirstChild<Sheets>()
                           .AppendChild(new Sheet
                           {
                               Id =
                                   spreadSheet
                                   .WorkbookPart
                                   .GetIdOfPart(
                                       spreadSheet
                                           .WorkbookPart
                                           .WorksheetParts
                                           .First()),
                               SheetId = 1,
                               Name = sheetName
                           });

                spreadSheet.WorkbookPart.Workbook.Save();


            }
        }

        private static void AddRows<T>(List<T> data, List<string> columns, SheetData sheetData)
        {
            foreach (var dataMember in data)
            {
                var typeOfData = dataMember.GetType();

                var row = new Row();
                foreach (string column in columns)
                {
                    var cell = new Cell { DataType = CellValues.String };

                    var property = typeOfData.GetProperty(column);
                    var columnValue = property.GetValue(dataMember, null);

                    cell.AppendChild(new CellValue { Text = columnValue != null ? columnValue.ToString() : string.Empty });

                    row.AppendChild(cell);
                }

                sheetData.AppendChild(row);
            }
        }

        private static void AddHeaderRow(SheetData sheetData, List<string> columns)
        {
            var headerRow = new Row();
            foreach (string c in columns)
            {
                headerRow.AppendChild(new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(c)
                });
            }

            sheetData.AppendChild(headerRow);
        }
    }
}

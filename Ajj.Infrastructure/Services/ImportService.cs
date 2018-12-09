using Ajj.Core.Interface;
using OfficeOpenXml;
using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace Ajj.Infrastructure.Services
{
    public class ImportService : IImportService
    {
        public async Task<DataTable> ImportExcelAsync(Stream fileStream)
        {
            var hasHeader = true;
            //FileInfo fileInfo = new FileInfo(file);
            using (ExcelPackage package = new ExcelPackage(fileStream))
            {
                DataTable tbl = new DataTable();
                try
                {
                    ExcelWorksheet ws = package.Workbook.Worksheets[0]; //get first workbook
                    int totalRows = ws.Dimension.End.Row; //count rows
                    int totalColumn = ws.Dimension.End.Column; //count columns

                    foreach (var firstRowCell in ws.Cells[1, 1, 1, totalColumn])
                    {
                        tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                    }
                    var startRow = hasHeader ? 2 : 1;

                    for (int i = startRow; i <= totalRows; i++)
                    {
                        var row = ws.Cells[i, 1, i, totalColumn];
                        var newRow = tbl.NewRow();
                        foreach (var cell in row)
                        {
                            newRow[cell.Start.Column - 1] = cell.Text.Trim();
                        }
                        await Task.Run(() => tbl.Rows.Add(newRow));
                    }
                }
                catch (Exception ex)
                {
                }

                return tbl;
            }
        }
    }
}
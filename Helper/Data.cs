using System.IO;
using NPOI.XSSF.UserModel;

namespace Tzy.Train.B2C.UI
{
    public class Data
    {

		/// <summary>
		/// This Method will be used fetch data from excel file
		/// <param name="filename"></param>
		/// <param name="sheetno"></param>
		/// <param name="rowno"></param>
		/// <param name="cellno"></param>
		/// </summary>
        public string GetTestData(string filename, int sheetno, int rowno, int cellno)
        {
            string path = @"../../../DataFiles/" + filename + ".xlsx";
            XSSFWorkbook workbook = new XSSFWorkbook(File.Open(path, FileMode.Open));
            var sheet = workbook.GetSheetAt(sheetno);
            var row = sheet.GetRow(rowno);
            var data = row.GetCell(cellno).StringCellValue.Trim();
            return data;
        }
    }
}
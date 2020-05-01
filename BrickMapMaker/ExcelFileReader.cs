using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace BrickMapMaker
{
    public interface IExcelFileReader
    {
        void SetActiveSheet(string name);
        bool ReadRow();
        bool ReadColumnAsText(int column_id, out string text);
        bool ReadColumnAsInt(int column_id, out int number);
        bool ReadColumnAsDecimal(int column_id, out decimal number);
        int GetColumnCount();
    }

    public class ExcelFileReader : IExcelFileReader
    {
        private SpreadsheetDocument _spreadsheet_document;
        private SheetData _sheet_data;
        private List<Row> _rows;
        private int _current_row;
        private List<Cell> _cells;
        private List<SharedStringItem> _shared_strings;

        public ExcelFileReader(string file_path)
        {
            _spreadsheet_document = SpreadsheetDocument.Open(file_path, false);

            WorkbookPart workbookPart = _spreadsheet_document.WorkbookPart;

            _shared_strings = workbookPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ToList();

            //{
            /**/
        }

        public int GetColumnCount()
        {
            return _cells.Count;
        }

        public bool ReadColumnAsDecimal(int column_id, out decimal number)
        {
            number = 0;
            var text = "";

            if (!ReadColumnAsText(column_id, out text))
                return false;

            if (!decimal.TryParse(text, System.Globalization.NumberStyles.Any, new System.Globalization.CultureInfo("en-US"), out number))
                return false;

            return true;
        }

        public bool ReadColumnAsInt(int column_id, out int number)
        {
            number = 0;
            var text = "";

            if (!ReadColumnAsText(column_id, out text))
                return false;

            if (!int.TryParse(text, out number))
                return false;

            return true;
        }

        public bool ReadColumnAsText(int column_id, out string text)
        {
            text = "";

            if (column_id >= _cells.Count)
                return false;

            var ss_index = 0;

            if(_cells[column_id].DataType == "s")
            {
                if (!int.TryParse(_cells[column_id].CellValue.Text, out ss_index))
                    return false;

                if (ss_index >= _shared_strings.Count)
                    return false;

                text = _shared_strings[ss_index].Text.Text;

                return true;
            }

            text = _cells[column_id].CellValue.Text;

            return true;
        }

        public bool ReadRow()
        {
            if (_rows == null)
            {
                _rows = _sheet_data.Elements<Row>().ToList();
                _current_row = -1;
            }

            _current_row++;
            if (_current_row >= _rows.Count)
                return false;

            _cells = _rows[_current_row].Elements<Cell>().ToList();

            return true;
        }

        public void SetActiveSheet(string name)
        {
            WorkbookPart workbookPart = _spreadsheet_document.WorkbookPart;
            string relId = workbookPart.Workbook.Descendants<Sheet>().First(s => s.Name == name).Id;
            WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(relId);
            _sheet_data = worksheetPart.Worksheet.Elements<SheetData>().First();

            _rows = null;
            _cells = null;
        }

        public void Close()
        {
            _spreadsheet_document.Close();
            _spreadsheet_document.Dispose();
        }
    }
}

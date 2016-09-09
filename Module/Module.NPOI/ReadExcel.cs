using System.Collections.Generic;
using System.IO;
using System.Linq;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Module.NPOI
{
    /// <summary>
    /// 快速读Excel
    /// </summary>
    public class ReadExcel
    {
        public List<T> ReadTo<T>(bool isXls, FileStream fs, int sheetAt = 0) where T : new()
        {
            using (fs)
            {
                //1. workbook
                //2. headers
                //3. content
                var workbook = isXls ? (IWorkbook)new HSSFWorkbook(fs) : new XSSFWorkbook(fs);
                var sheet = workbook.GetSheetAt(sheetAt);
                var headers = GetHeaders(sheet.GetRow(0));
                var list = new List<T>();
                for (int i = 1; i < sheet.LastRowNum; i++)
                {
                    var row = sheet.GetRow(i);
                    var obj = new T();
                    row.Cells.ForEach(x =>
                    {
                        //todo
                    });
                    list.Add(obj);
                }

                return list;
            }
        }


        public List<T> ReadTo<T>(string fileName, int sheetAt = 0) where T : new()
        {
            return ReadTo<T>(Path.GetExtension(fileName) == ".xls", new FileStream(fileName, FileMode.Open), sheetAt);
        }

        private IEnumerable<ReadHeader> GetHeaders(IRow row)
        {
            return row.Cells.Select(x => new ReadHeader
            {
                Title = x.ToString(),
                TitleOrderId = x.ColumnIndex
            });
        }

    }

    public class ReadHeader
    {
        /// <summary>
        /// Excel 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Title 对应顺序
        /// 从0开始
        /// </summary>
        public int TitleOrderId { get; set; }

    }
}

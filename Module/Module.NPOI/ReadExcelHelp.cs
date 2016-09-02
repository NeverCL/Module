using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;

namespace Module.NPOI
{
    public static class ReadExcelHelp
    {
        public static List<T> ReadXls<T>(this string fileName) where T : new()
        {
            using (var fs = new FileStream(fileName, FileMode.Open))
            {
                var workbook = new HSSFWorkbook(fs);
                var sheet = workbook.GetSheetAt(0);
                var row = sheet.GetRow(0);//获取工作表第一行
                var headers = new List<ExcelHeader>();
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    var title = row.GetCell(i).ToString();
                    var header = new ExcelHeader
                    {
                        Id = headers.Count,
                        Name = title,
                        TitleId = i,
                        OrderId = GetOrder(typeof(T), title)
                    };
                    headers.Add(header);
                }

                var list = new List<T>();
                for (int i = 1; i <= sheet.LastRowNum ; i++)
                {
                    row = sheet.GetRow(i);
                    var obj = new T();
                    for (int j = 0; j < row.Cells.Count; j++)
                    {
                        var value = row.GetCell(j).ToString();
                        if (headers.Any(x => x.TitleId == j))
                        {
                            var prop = typeof(T).GetProperties()[headers.First(x => x.TitleId == j).OrderId];
                            var newVal = Convert.ChangeType(value, prop.PropertyType);
                            prop.SetValue(obj, newVal);
                        }
                    }
                    list.Add(obj);
                }
                return list;
            }
        }

        private static int GetOrder(Type type, string name)
        {
            var props = type.GetProperties();
            for (int i = 0; i < props.Length; i++)
            {
                var prop = props[i];
                var nameAttr =
                    prop.GetCustomAttributes(typeof(DisplayNameAttribute), false).FirstOrDefault() as
                        DisplayNameAttribute;
                if (nameAttr != null)
                {
                    if (nameAttr.DisplayName == name)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        internal class ExcelHeader
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public int OrderId { get; set; }

            public int TitleId { get; set; }
        }
    }
}

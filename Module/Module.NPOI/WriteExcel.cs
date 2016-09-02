using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.Format;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Module.NPOI
{
    public static class WriteExcel
    {
        public static string WriteXls<T>(this List<T> data, string fileName, string sheetName = "sheet1")
        {
            var workbook = BuildXls(data, sheetName);
            using (var fs = new FileStream(fileName, FileMode.Create))
            {
                workbook.Write(fs);
            }
            return fileName;
        }

        public static string WriteXlsx<T>(this List<T> data, string fileName, string sheetName = "sheet1")
        {
            var workbook = BuildXlsx(data, sheetName);
            using (var fs = new FileStream(fileName, FileMode.Create))
            {
                workbook.Write(fs);
            }
            return fileName;
        }

        public static HSSFWorkbook BuildXls<T>(this List<T> data, string sheetName)
        {
            var workbook = new HSSFWorkbook();
            if (data == null)
            {
                return null;
            }
            BuildExcel(data, sheetName, workbook);
            return workbook;
        }

        public static XSSFWorkbook BuildXlsx<T>(this List<T> data, string sheetName)
        {
            var workbook = new XSSFWorkbook();
            if (data == null)
            {
                return null;
            }
            BuildExcel(data, sheetName, workbook);
            return workbook;
        }

        private static void BuildExcel<T>(List<T> data, string sheetName, IWorkbook workbook)
        {
            var sheet = workbook.CreateSheet(sheetName); //创建工作表
            //1. Write Header
            var props = typeof(T).GetProperties();
            var headers = new List<ObjHeader>();
            for (int i = 0; i < props.Length; i++)
            {
                var prop = props[i];
                //NotMapped
                if (!prop.GetCustomAttributes(typeof(NotMappedAttribute), false).Any())
                {
                    var header = new ObjHeader();

                    var nameAttr =
                        prop.GetCustomAttributes(typeof(DisplayNameAttribute), false).FirstOrDefault() as
                            DisplayNameAttribute;

                    header.Name = nameAttr != null ? nameAttr.DisplayName : prop.Name;

                    var formatAttr =
                        prop.GetCustomAttributes(typeof(DisplayFormatAttribute), false).FirstOrDefault() as
                            DisplayFormatAttribute;
                    if (formatAttr != null)
                    {
                        header.DataFormatString = formatAttr.DataFormatString;
                    }
                    header.Id = headers.Count;
                    header.OrderId = i;
                    headers.Add(header);
                }
            }
            var row = sheet.CreateRow(0); //在工作表中添加一行
            foreach (var item in headers)
            {
                var cell = row.CreateCell(item.Id); //在行中添加一列
                cell.SetCellValue(headers[item.Id].Name); //设置列的内容
            }

            //2. Write Content
            for (int i = 0; i < data.Count; i++)
            {
                var item = data[i];
                row = sheet.CreateRow(i + 1); //在工作表中添加一行
                var itemProps = item.GetType().GetProperties();
                foreach (var header in headers)
                {
                    var val = itemProps[header.OrderId].GetValue(item);
                    var cell = row.CreateCell(header.Id); //在行中添加一列
                    if (!string.IsNullOrEmpty(header.DataFormatString))
                    {
                        val = string.Format("{0:" + header.DataFormatString + "}", val);
                    }
                    else
                    {
                        val = string.Format("{0}", val);
                    }
                    cell.SetCellValue(val.ToString());
                }
            }
        }

        internal class ObjHeader
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string DataFormatString { get; set; }

            public int OrderId { get; set; }
        }
    }
}

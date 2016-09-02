using NPOI.HSSF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.Format;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Module.NPOI
{
    public static class WriteExcelHelp
    {
        public static string DefaultSheetName = "sheet1";

        #region WriteBulkXls
        public static string WriteBulkExcel(string fileName, params IEnumerable[] dataList)
        {
            IWorkbook workbook;
            if (Path.GetExtension(fileName) == ".xls")
            {
                workbook = new HSSFWorkbook();
            }
            else if (Path.GetExtension(fileName) == ".xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else
            {
                return null;
            }
            var index = 0;
            foreach (var list in dataList)
            {
                var type = list.GetType().GenericTypeArguments[0];
                var sheetName = GetSheetName(type);
                index++;
                InvokeStaticGenericMethod(new object[] { list, index + "-" + sheetName, workbook }, typeof(WriteExcelHelp), "BuildExcel", type);
            }
            using (var fs = new FileStream(fileName, FileMode.Create))
            {
                workbook.Write(fs);
            }
            return fileName;
        }

        static void InvokeStaticGenericMethod(object[] args, Type classType, string methodName, Type genericType)
        {
            MethodInfo mi = classType.GetMethod(methodName);
            MethodInfo miConstructed = mi.MakeGenericMethod(genericType);
            miConstructed.Invoke(null, args);
        }
        #endregion

        #region WriteXls
        public static string WriteExcel<T>(this List<T> data, string fileName)
        {
            var sheetName = GetSheetName(data.GetType().GenericTypeArguments[0]);
            IWorkbook workbook;
            if (Path.GetExtension(fileName) == ".xls")
            {
                workbook = BuildXls(data, sheetName);
            }
            else if (Path.GetExtension(fileName) == ".xlsx")
            {
                workbook = BuildXlsx(data, sheetName);
            }
            else
            {
                return null;
            }
            using (var fs = new FileStream(fileName, FileMode.Create))
            {
                workbook.Write(fs);
            }
            return fileName;
        }

        private static string GetSheetName(Type type)
        {
            var nameAttr =
                      type.GetCustomAttributes(typeof(DisplayNameAttribute), false).FirstOrDefault() as
                          DisplayNameAttribute;
            if (nameAttr != null)
            {
                return nameAttr.DisplayName;
            }
            return DefaultSheetName;
        }
        #endregion

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

        public static void BuildExcel<T>(IList<T> data, string sheetName, IWorkbook workbook)
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
                var font = workbook.CreateFont();
                font.IsBold = true;
                var style = GetStyle(workbook);
                style.SetFont(font);
                cell.CellStyle = style;
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
                    cell.CellStyle = GetStyle(workbook);
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

        public static ICellStyle GetStyle(IWorkbook xss)
        {
            ICellStyle cellstyle = xss.CreateCellStyle();
            cellstyle.BorderBottom = BorderStyle.Thin;
            cellstyle.BorderLeft = BorderStyle.Thin;
            cellstyle.BorderRight = BorderStyle.Thin;
            cellstyle.BorderTop = BorderStyle.Thin;
            return cellstyle;
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

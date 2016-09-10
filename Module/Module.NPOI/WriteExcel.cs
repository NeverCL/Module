using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Module.NPOI
{
    public class WriteExcel
    {
        public void WriteTo<T>(List<T> data, Stream stream, bool isXls = true, int sheetAt = -1)
        {
            var workbook = isXls ? (IWorkbook)new HSSFWorkbook() : new XSSFWorkbook();
            var headerCellStyle = CreateHeaderStyle(workbook);
            var cellStyle = CreateStyle(workbook);
            //0. sheet;
            //1. headers
            //2. content
            //3. write
            var type = typeof(T);
            var sheetName = GetTypeAttrName(type);
            var sheet = string.IsNullOrEmpty(sheetName) ? workbook.CreateSheet() : workbook.CreateSheet(sheetName);

            //headers
            var row = sheet.CreateRow(0);
            var headers = new List<HeaderInfo>();
            for (int i = 0; i < type.GetProperties().Length; i++)
            {
                var propertyInfo = type.GetProperties()[i];
                if (IsNotMapper(propertyInfo)) continue;
                var typeName = GetTypeAttrName(propertyInfo.PropertyType);
                headers.Add(new HeaderInfo
                {
                    PropOrderId = i,
                    Property = propertyInfo,
                    Title = typeName,
                    TitleOrderId = row.Cells.Count,
                    Format = (propertyInfo.GetCustomAttributes(typeof(DisplayFormatAttribute), false).First() as DisplayFormatAttribute)?.DataFormatString
                });
                var cell = row.CreateCell(row.Cells.Count);
                cell.CellStyle = headerCellStyle;
                cell.SetCellValue(typeName);
            }

            //content
            for (var i = 0; i < data.Count; i++)
            {
                row = sheet.CreateRow(i);
                var item = data[i];
                foreach (var header in headers)
                {
                    var prop = header.Property;
                    var val = prop.GetValue(item);
                    if (prop.PropertyType.IsEnum)
                    {
                        val = InvokeHelp.InvokeGenericMethod(new[] { val }, typeof(InvokeHelp), "GetEnumName", prop.PropertyType);
                    }
                    var cell = row.CreateCell(header.TitleOrderId); //在行中添加一列
                    cell.CellStyle = cellStyle;
                    val = !string.IsNullOrEmpty(header.Format) ? string.Format("{0:" + header.Format + "}", val) : string.Format("{0}", val);
                    cell.SetCellValue(val.ToString());
                }
            }

            //write
            using (stream)
                workbook.Write(stream);
        }

        protected virtual bool IsNotMapper(PropertyInfo prop)
        {
            return prop.GetCustomAttributes(typeof(NotMappedAttribute), false).Any();
        }

        protected virtual string GetTypeAttrName(Type type)
        {
            var displayAttr =
                type.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as
                        DisplayAttribute;
            if (displayAttr != null)
            {
                return displayAttr.Name;
            }

            var displayNameAttr =
                     type.GetCustomAttributes(typeof(DisplayNameAttribute), false).FirstOrDefault() as
                         DisplayNameAttribute;
            if (displayNameAttr != null)
            {
                return displayNameAttr.DisplayName;
            }

            var descriptionAttr =
                     type.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as
                         DescriptionAttribute;
            if (descriptionAttr != null)
            {
                return descriptionAttr.Description;
            }

            return string.Empty;
        }

        protected virtual ICellStyle CreateStyle(IWorkbook workbook)
        {
            var cellstyle = workbook.CreateCellStyle();
            cellstyle.BorderBottom = BorderStyle.Thin;
            cellstyle.BorderLeft = BorderStyle.Thin;
            cellstyle.BorderRight = BorderStyle.Thin;
            cellstyle.BorderTop = BorderStyle.Thin;
            return cellstyle;
        }

        protected virtual ICellStyle CreateHeaderStyle(IWorkbook workbook)
        {
            var cellStyle = CreateStyle(workbook);
            var font = workbook.CreateFont();
            font.IsBold = true;
            cellStyle.SetFont(font);
            return cellStyle;
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.NPOI
{
    /// <summary>
    /// WriteExcel Linq 扩展方法
    /// </summary>
    public static class WriteExcelLinq
    {
        public static void WriteTo<T>(this List<T> list, Stream stream, bool isXls, int sheetAt = 0)
        {
            new WriteExcel().WriteTo(list, stream, isXls, sheetAt);
        }

        public static void WriteTo<T>(this List<T> list, FileStream fs,
            int sheetAt = 0)
        {
            WriteTo(list, fs, Path.GetExtension(fs.Name) == ".xls", sheetAt);
        }

        public static void WriteTo<T>(this List<T> list, string fileName,
           int sheetAt = 0)
        {
            var fs = new FileStream(fileName, FileMode.Create);
            WriteTo(list, fs, Path.GetExtension(fs.Name) == ".xls", sheetAt);
        }
    }
}

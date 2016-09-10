using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Module.NPOI
{
    public class WriteExcel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="fs"></param>
        /// <param name="sheetAt">默认依次插入</param>
        public void WriteTo<T>(List<T> data, FileStream fs, int sheetAt = -1)
        {
            var workbook = Path.GetExtension(fs.Name) == ".xls"
                        ? (IWorkbook)new HSSFWorkbook(fs) : new XSSFWorkbook(fs);
        }
    }
}

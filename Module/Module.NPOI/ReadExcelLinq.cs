using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.NPOI
{
    /// <summary>
    /// ReadExcel Linq扩展方法操作
    /// </summary>
    public static class ReadExcelLinq
    {
        /// <summary>
        /// FileStream Linq
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fs"></param>
        /// <param name="isOrderNum"></param>
        /// <param name="sheetAt"></param>
        /// <returns></returns>
        public static List<T> ReadTo<T>(this FileStream fs, bool isOrderNum = false, int sheetAt = 0) where T : new()
        {
            return ReadTo<T>(fs, Path.GetExtension(fs.Name) == ".xls", isOrderNum, sheetAt);
        }

        /// <summary>
        /// string Linq
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="isOrderNum"></param>
        /// <param name="sheetAt"></param>
        /// <returns></returns>
        public static List<T> ReadTo<T>(this string fileName, bool isOrderNum = false, int sheetAt = 0) where T : new()
        {
            return ReadTo<T>(new FileStream(fileName, FileMode.Open), isOrderNum, sheetAt);
        }

        /// <summary>
        /// Base Stream Linq
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream"></param>
        /// <param name="isXls"></param>
        /// <param name="isOrderNum"></param>
        /// <param name="sheetAt"></param>
        /// <returns></returns>
        public static List<T> ReadTo<T>(this Stream stream, bool isXls, bool isOrderNum = false, int sheetAt = 0) where T : new()
        {
            return new ReadExcel(isOrderNum).ReadTo<T>(stream, isXls, sheetAt);
        }
    }
}

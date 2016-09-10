using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.NPOI
{
    public static class FileStreamLinq
    {
        public static List<T> ReadTo<T>(this FileStream fs, bool isOrderNum = false) where T : new()
        {
            return new ReadExcel(isOrderNum).ReadTo<T>(fs);
        }
    }
}

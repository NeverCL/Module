using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Application
{
    public class PageResult<T> where T : class
    {
        public int Count { get; set; }
        public int Index { get; set; }
        public int Size { get; set; }
        public List<T> Data { get; set; }
    }
}

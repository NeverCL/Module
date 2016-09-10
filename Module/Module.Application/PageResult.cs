using System.Collections.Generic;

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

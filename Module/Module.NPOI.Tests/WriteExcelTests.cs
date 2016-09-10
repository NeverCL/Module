using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Module.NPOI.Tests
{
    public class WriteExcelTests
    {
        private readonly List<Model> _list;

        private readonly string _fileName = Guid.NewGuid() + ".xls";

        private readonly int _count = 30000;

        public WriteExcelTests()
        {
            _list = new List<Model>();
            for (int i = 0; i < _count; i++)
            {
                _list.Add(new Model { Name = "add", Id = i, Time = DateTime.Now, Statu = i % 2 == 0 ? Statu.Active : Statu.Close });
            }
        }

        [Fact]
        public void WriteTo()
        {
            new WriteExcel().WriteTo(_list, new FileStream(_fileName, FileMode.Create));
        }


        [Fact]
        public void WriteToByString()
        {
            _list.WriteTo(_fileName);
        }

        [Fact]
        public void WriteToByFileStream()
        {
            _list.WriteTo(new FileStream(_fileName, FileMode.Create));
        }
    }
}

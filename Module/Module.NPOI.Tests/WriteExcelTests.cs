using System;
using System.Collections.Generic;
using System.IO;
using Should;
using Xunit;

namespace Module.NPOI.Tests
{
    public class WriteExcelTests:IDisposable
    {
        private readonly List<AttrModel> _list;

        private readonly string _fileName = Guid.NewGuid() + ".xls";

        private readonly int _count = 100;

        public WriteExcelTests()
        {
            _list = new List<AttrModel>();
            for (int i = 0; i < _count; i++)
            {
                _list.Add(new AttrModel { Name = "add", Id = i, Time = DateTime.Now, Statu = i % 2 == 0 ? Statu.Active : Statu.Close });
            }
        }

        [Fact(DisplayName = "原始new对象测试")]
        public void WriteTo()
        {
            new WriteExcel().WriteTo(_list, new FileStream(_fileName, FileMode.Create));
            File.Exists(_fileName).ShouldBeTrue();
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

        public void Dispose()
        {
            File.Delete(_fileName);
        }
    }
}

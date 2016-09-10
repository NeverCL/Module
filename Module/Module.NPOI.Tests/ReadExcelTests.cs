using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using Should;
using Xunit;

namespace Module.NPOI.Tests
{
    public class ReadExcelTests : IDisposable
    {
        private readonly string _fileName = Guid.NewGuid() + ".xls";
        private readonly int _count = 30000;

        public ReadExcelTests()
        {
            var list = new List<Model>();
            for (int i = 0; i < _count; i++)
            {
                list.Add(new Model { Name = "add", Id = i, Time = DateTime.Now, Statu = i % 2 == 0 ? Statu.Active : Statu.Close });
            }
            list.WriteTo(_fileName);
        }

        [Fact]
        public void ReadToByFileStream()
        {
            var list = new FileStream(_fileName, FileMode.Open).ReadTo<Model>(true);
            list.Count.ShouldEqual(_count);
        }

        [Fact]
        public void ReadToByString()
        {
            var list = _fileName.ReadTo<Model>(true);
            Assert.True(list.Count == _count);
        }

        [Fact]
        public void ReadToByStream()
        {
            var list = new MemoryStream(File.ReadAllBytes(_fileName)).ReadTo<Model>(true, true);
            Assert.True(list.Count == _count);
        }

        /// <summary>
        /// 与原来的比较
        /// </summary>
        [Fact]
        public void ReadOld()
        {
            var list = _fileName.ReadExcelOrder<Model>();
            Assert.True(list.Count == _count);
        }

        public void Dispose()
        {
            File.Delete(_fileName);
        }
    }


    public class Model
    {
        //[DisplayName("姓名")]
        public string Name { get; set; }

        //[DisplayName("状态")]
        public Statu Statu { get; set; }

        //[DisplayName("创建时间")]
        [DisplayFormat(DataFormatString = "yyyy年MM月dd日")]
        public DateTime Time { get; set; }

        [NotMapped]
        public int Id { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Module.NPOI.Tests
{
    [TestClass]
    public class ExcelTests
    {
        [TestMethod]
        public void TestWriteExcel()
        {
            var data = new List<Model>();
            for (int i = 0; i < 10; i++)
            {
                data.Add(new Model { Name = "测试名称" + i, Id = i, Time = DateTime.Now });
            }
            data.WriteExcel("2.xls");
        }


        [TestMethod]
        public void TestReadExcel()
        {
            var data = "2.xls".ReadExcel<Model>();
            Assert.IsNotNull(data);
            Assert.IsTrue(data.Count == 10);
        }


        [TestMethod]
        public void TestWriteBulkXls()
        {
            var data = new List<Model>();
            for (int i = 0; i < 10; i++)
            {
                data.Add(new Model { Name = "测试名称" + i, Id = i, Time = DateTime.Now });
            }
            WriteExcelHelp.WriteBulkExcel("3.xls", data, data, data);
        }


        [TestMethod]
        public void TestReadBulkExcel()
        {
            var dataList = ReadExcelHelp.ReadBulkExcel("3.xls", new List<Model>(), new List<Model>(), new List<Model>());
            Assert.IsTrue(dataList.Length == 3);
            var list = dataList[0] as List<Model>;
            Assert.IsNotNull(list);
            Assert.AreEqual(list.Count, 10);
        }
    }

    [DisplayName("sheet名称")]
    public class Model
    {
        [NotMapped]
        public int Id { get; set; }

        [DisplayName("姓名")]
        public string Name { get; set; }

        [DisplayName("创建时间")]
        [DisplayFormat(DataFormatString = "yyyy年MM月dd日")]
        public DateTime Time { get; set; }
    }
}

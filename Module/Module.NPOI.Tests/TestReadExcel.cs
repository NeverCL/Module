using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Module.NPOI.Tests
{
    [TestClass]
    public class TestReadExcel
    {
        [TestMethod]
        public void ReadTo()
        {
            var list = new ReadExcel(true).ReadTo<Model>(new FileStream("2.xls", FileMode.Open));
            Assert.IsTrue(list.Count == 30000);
        }

        [TestMethod]
        public void ReadToLinq()
        {
            var list = new FileStream("2.xls", FileMode.Open).ReadTo<Model>(true);
            Assert.IsTrue(list.Count == 30000);
        }
    }


    //[DisplayName("sheet名称")]
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

    public enum Statu
    {
        [Display(Name = "激活")]
        Active,
        [System.ComponentModel.DescriptionAttribute("关闭")]
        Close
    }

}

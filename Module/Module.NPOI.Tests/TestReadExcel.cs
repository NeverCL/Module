using System;
using System.Collections.Generic;
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
            var list = new ReadExcel().ReadTo<Model>(new FileStream("2.xls", FileMode.Open));
            Assert.IsTrue(list.Count == 30000);
        }

        [TestMethod]
        public void ReadBulkTo()
        {
            var list = new ReadExcel().ReadBulkTo(new FileStream("2.xls", FileMode.Open), new List<Model>()).First() as List<Model>;
            Assert.IsTrue(list.Count == 30000);
        }
    }


}

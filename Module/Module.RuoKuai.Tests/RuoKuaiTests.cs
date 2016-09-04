using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Module.RuoKuai.Tests
{
    [TestClass]
    public class RuoKuaiTests
    {
        [TestMethod]
        public void TestVerify()
        {
            var ruo = new RuoKuai("testuser11", "11111122", "1040");
            var rst = ruo.Verify(File.ReadAllBytes("rand.jpg"));
            Assert.IsNotNull(rst);
            Assert.IsTrue(rst == "3165");
        }
    }
}

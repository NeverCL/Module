using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Module.Identity.ActiveDirectory.Tests
{
    [TestClass]
    public class AuthTests
    {
        [TestMethod]
        public void Validate()
        {
            var client = new ActiveDomainClient("10.113.0.45");
            var rst = client.Validate("shienrong", "Seg123456");
            Assert.IsTrue(rst);
            rst = client.Validate("Segobmp", "Pwd141126");
            Assert.IsTrue(rst);
        }


    }

    
}

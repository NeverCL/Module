using System;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Module.Tests.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            new BaseTest();
        }
    }

    public class BaseTest : TestBase
    {
        public BaseTest() : base(new ContainerBuilder(), new[] { 0 }, typeof(UnitTest1))
        {

        }
    }
}

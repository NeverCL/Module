using System;
using System.Drawing;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Module.Compress.Tests
{
    [TestClass]
    public class ImageCompressTests
    {
        [TestMethod]
        public void TestCompression()
        {
            //var len = File.ReadAllBytes("1.png").Length;
            Image.FromFile("1.png").CompressionToSize().SaveImg("2.png");
        }
    }
}

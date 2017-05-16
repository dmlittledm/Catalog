using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.Entities.Tests
{
    [TestClass()]
    public class LibraryTests
    {
        [TestMethod()]
        public void LibraryTest()
        {
            var lib = TestsHelper.CreateLibrary("Test lib");
            Assert.IsNotNull(lib);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddNodeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RemoveNodeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RemoveNodeTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void MoveToTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DescendantsTest()
        {
            Assert.Fail();
        }
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaLibrary.Interfaces;

namespace MediaLibrary.Entities.Tests
{
    [TestClass()]
    public class NodeTests
    {
        [TestMethod()]
        public void NodeConstructorTest()
        {
            var node = new Node() as INode;
            node.AddFields(TestsHelper.CreateFieldSet());

            // TODO: finish this
        }

        [TestMethod()]
        public void DescendantsTest()
        {
            INode node = new Node();
            Assert.Fail();
        }

        [TestMethod()]
        public void DescendantsAndSelfTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SetParentTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ClearParentTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddChildTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RemoveChildTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RemoveChildTest1()
        {
            Assert.Fail();
        }

        private INode CreateTestNode()
        {
            var node = new Node() as INode;



            return node;
        }


        // TODO: add Name property test
    }
}
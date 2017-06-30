using System;
using MediaLibrary.Interfaces;
using NUnit.Framework;

namespace MediaLibrary.Entities.Tests
{
    [TestFixture()]
    public class NodeTests
    {
        [Test()]
        public void NodeTest()
        {
            var node = (INode) new Node();
            Assert.NotNull(node);
            Assert.AreNotEqual(node.Id, Guid.Empty);
            Assert.IsNull(node.Name);
        }

        [Test()]
        public void DescendantsTest()
        {
            INode node = new Node();
            Assert.Fail();
        }

        [Test()]
        public void DescendantsAndSelfTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void SetParentTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void ClearParentTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void AddChildTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void RemoveChildTest()
        {
            Assert.Fail();
        }

        [Test()]
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
using MediaLibrary.Interfaces;
using NUnit.Framework;

namespace MediaLibrary.Entities.Tests
{
    [TestFixture()]
    public class NodeTests
    {
        [Test()]
        public void NodeConstructorTest()
        {
            var node = new Node() as INode;
            node.AddFields(TestsHelper.CreateFieldSet());

            // TODO: finish this
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
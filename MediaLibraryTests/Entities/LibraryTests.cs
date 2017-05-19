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
            var lib = TestsHelper.CreateLibrary("Test lib");
            var newName = "Another name";
            var newDesription = "Some description";
            Assert.IsFalse(lib.Name ==  newName);
            Assert.IsFalse(lib.Description == newDesription);

            lib.Update(newName, newDesription);
            Assert.IsTrue(lib.Name == newName);
            Assert.IsTrue(lib.Description == newDesription);
        }

        [TestMethod()]
        public void AddNodeTest()
        {
            var lib = TestsHelper.CreateLibrary("Test lib");
            var nodeName = "Node to add";
            var node = TestsHelper.CreateNode(nodeName);
            var cnt = lib.Nodes.Count();

            Assert.IsFalse(lib.Descendants(x => x.Fields.Any(f => f.FieldType.Role == FieldRoles.Name && f.Value.ToString() == nodeName)).Any());

            lib.AddNode(node);

            Assert.IsTrue(lib.Nodes.Count() == cnt + 1);
            Assert.IsFalse(node.HasParent);
            Assert.IsFalse(node.HasChilds);
            Assert.IsTrue(node.Root == node);
            Assert.IsTrue(lib.Nodes.Contains(node));
            var descendants = lib.Descendants(x => x.Fields.Any(f => f.FieldType.Role == FieldRoles.Name && f.Value.ToString() == nodeName));
            Assert.IsTrue(descendants.Any());
        }

        [TestMethod()]
        public void RemoveNode_RemoveRootNodeTest()
        {
            var lib = TestsHelper.CreateLibrary("Test lib");
            Func<INode, bool> predicate = x => x.Fields.Any(f => f.FieldType.Role == FieldRoles.Name && f.Value.ToString().EndsWith("5"));
            var node = lib.Nodes.FirstOrDefault(predicate);
            RemoveNode_Test(node, lib);
        }

        private static void RemoveNode_Test(INode node, ILibrary lib)
        {
            Func<INode, bool> predicate = x => x.Fields.Any(f => f.FieldType.Role == FieldRoles.Name && f.Value.ToString() == node.Name);
            Assert.IsNotNull(node);

            var cnt = lib.Nodes.Where(predicate).Count();
            var totalCnt = lib.Nodes.Count();

            Assert.IsTrue(lib.Nodes.Any(predicate));

            lib.RemoveNode(node);

            Assert.IsTrue(lib.Nodes.Count() == totalCnt - 1);
            Assert.IsTrue(lib.Nodes.Where(predicate).Count() == cnt - 1);
            Assert.IsTrue(node.Root == node);
            Assert.IsFalse(lib.Nodes.Contains(node));

            // TODO: also need to check if links are removed
        }

        [TestMethod()]
        public void RemoveNode_RemoveByIdTest()
        {
            Assert.Fail();
        }

        private void RemoveNode_Test()
        {
            
        }

        [TestMethod()]
        public void MoveToTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DescendantsTest()
        {
            var lib = TestsHelper.CreateLibrary("Test lib");
            Func<INode, IEnumerable<INode>> selector = s => s.Childs; 
            var childs = lib.Nodes.SelectMany(selector);
            var subChilds = childs.SelectMany(selector);

            var cnt = lib.Nodes.Count() + childs.Count();
            cnt += subChilds.Count();
            cnt += subChilds.SelectMany(selector).Count();

            var libCount = lib.Descendants().Count();
            Assert.IsTrue(libCount == cnt);
        }
        [TestMethod()]
        public void Descendants_FilterTest()
        {
            var lib = TestsHelper.CreateLibrary("Test lib");
            Func<INode, IEnumerable<INode>> selector = s => s.Childs;
            Func<INode, bool> filter = x => x.Fields
                .FirstOrDefault(f => f.FieldType.Role == FieldRoles.Name && f.Value.ToString().EndsWith("0")) 
                != null;

            var childs = lib.Nodes
                .SelectMany(selector).ToList();
            var subChilds = childs.SelectMany(selector).ToList();

            var cnt = lib.Nodes.Where(filter).Count();
            cnt += childs.Where(filter).Count();
            cnt += subChilds.Where(filter).Count();

            var libCount = lib.Descendants(filter).Count();
            Assert.IsTrue(cnt != 0);
            Assert.IsTrue(libCount == cnt);
        }
    }
}
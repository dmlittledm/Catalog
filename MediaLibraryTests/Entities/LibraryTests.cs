using System;
using System.Collections.Generic;
using System.Linq;
using MediaLibrary.Interfaces;
using NUnit.Framework;

namespace MediaLibrary.Entities.Tests
{
    [TestFixture()]
    public class LibraryTests
    {
        [Test()]
        public void LibraryTest()
        {
            var lib = TestsHelper.CreateLibrary("Test lib");
            Assert.IsNotNull(lib);
        }

        [Test()]
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

        [Test()]
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

        [Test()]
        public void RemoveNode_RemoveRootNodeTest()
        {
            var lib = TestsHelper.CreateLibrary("Test lib");
            Func<INode, bool> filter = x => x.Fields.Any(f => f.FieldType.Role == FieldRoles.Name && f.Value.ToString().EndsWith("5"));
            var node = lib.Nodes.FirstOrDefault(filter);

            Assert.IsNotNull(node);
            Func<INode, bool> predicate = x => x.Fields.Any(f => f.FieldType.Role == FieldRoles.Name && f.Value.ToString() == node.Name);

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

        [Test()]
        public void RemoveNode_RemoveByIdTest()
        {
            var lib = TestsHelper.CreateLibrary("Test lib");
            Func<INode, bool> filter = x => x.Fields.Any(f => f.FieldType.Role == FieldRoles.Name && f.Value.ToString().EndsWith("5"));
            var node = lib.Nodes.FirstOrDefault(filter);

            Assert.IsNotNull(node);
            Func<INode, bool> predicate = x => x.Fields.Any(f => f.FieldType.Role == FieldRoles.Name && f.Value.ToString() == node.Name);

            var cnt = lib.Nodes.Where(predicate).Count();
            var totalCnt = lib.Nodes.Count();

            Assert.IsTrue(lib.Nodes.Any(predicate));

            lib.RemoveNode(node.Id);

            Assert.IsTrue(lib.Nodes.Count() == totalCnt - 1);
            Assert.IsTrue(lib.Nodes.Where(predicate).Count() == cnt - 1);
            Assert.IsTrue(node.Root == node);
            Assert.IsFalse(lib.Nodes.Contains(node));

            // TODO: also need to check if links are removed
        }

        [Test()]
        public void MoveToTest()
        {
            var lib = TestsHelper.CreateLibrary("Test lib");
            Func<INode, bool> filter = x => x.Fields.Any(f => f.FieldType.Role == FieldRoles.Name && f.Value.ToString().EndsWith("5"));
            var sourceNode = lib.Nodes.FirstOrDefault(filter);

            Func<INode, bool> targetFilter = x => x.Fields.Any(f => f.FieldType.Role == FieldRoles.Name && f.Value.ToString().EndsWith("1"));
            var targetNode = lib.Nodes.FirstOrDefault(targetFilter);

            Assert.IsNotNull(sourceNode);
            Assert.IsNotNull(targetNode);

            lib.MoveTo(targetNode, sourceNode);

            Assert.AreSame(targetNode, sourceNode.Parent);
            Assert.IsTrue(targetNode.Childs.Contains(sourceNode));
        }

        [Test()]
        public void MoveTo_ChildToParentTest()
        {
            var lib = TestsHelper.CreateLibrary("Test lib");
            Func<INode, bool> filter = x => x.Fields.Any(f => f.FieldType.Role == FieldRoles.Name && f.Value.ToString().EndsWith("5"));
            var parentNode = lib.Nodes.FirstOrDefault(filter);

            Assert.IsNotNull(parentNode);

            var childNode = parentNode.Childs.FirstOrDefault();

            Assert.IsNotNull(childNode);

            lib.MoveTo(parentNode, childNode);

            Assert.AreSame(parentNode, childNode.Parent);
            Assert.IsTrue(parentNode.Childs.Contains(childNode));
        }

        [Test()]
        public void MoveTo_NullTest()
        {
            var lib = TestsHelper.CreateLibrary("Test lib");

            var targetNode = lib.Nodes.FirstOrDefault();

            Assert.Catch<ArgumentNullException>(() => lib.MoveTo(targetNode, null));
            Assert.Catch<ArgumentNullException>(() => lib.MoveTo(null, targetNode));
            Assert.Catch<ArgumentNullException>(() => lib.MoveTo(null, null));
        }

        [Test()]
        public void MoveTo_ParentToChildTest()
        {
            var lib = TestsHelper.CreateLibrary("Test lib");

            Func<INode, bool> filter = x => x.Fields.Any(f => f.FieldType.Role == FieldRoles.Name && f.Value.ToString().EndsWith("5"));
            var parentNode = lib.Nodes.FirstOrDefault(filter);

            Assert.IsNotNull(parentNode);

            Assert.Catch<ArgumentException>(() => lib.MoveTo(parentNode, parentNode));

            var childNode = parentNode.Childs.FirstOrDefault();

            Assert.Catch<ArgumentException>(() => lib.MoveTo(childNode, parentNode));
        }

        [Test()]
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
        [Test()]
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
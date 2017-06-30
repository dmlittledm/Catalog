using NUnit.Framework;
using MediaLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaLibrary.Interfaces;

namespace MediaLibrary.Entities.Tests
{
    [TestFixture()]
    public class ResourceTests
    {
        [Test()]
        public void ResourceTest()
        {
            var res = (IResource) new Resource();

            Assert.NotNull(res);
            Assert.AreNotEqual(res.Id, Guid.Empty);
            Assert.IsEmpty(res.Fields);
        }

        [Test()]
        public void AddFieldTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void AddFieldsTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void RemoveFieldTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void ClearFieldsTest()
        {
            Assert.Fail();
        }
    }
}
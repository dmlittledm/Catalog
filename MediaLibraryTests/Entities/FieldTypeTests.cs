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
    public class FieldTypeTests
    {
        [TestMethod()]
        public void GetDataType_ExceptionTest()
        {
            try
            {
                var val = (FieldDataTypes) int.MaxValue;

                var ft = new FieldType() {FieldDataType = val};

                if(Enum.IsDefined(typeof(FieldDataTypes), val))
                    Assert.Fail("Such value is defined, renew the test!");

                ft.GetDataType();

                Assert.Fail();
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod()]
        public void GetDataType_TextTest()
        {
            var ft = new FieldType() { FieldDataType = FieldDataTypes.Text };

            Assert.IsTrue(ft.GetDataType() == typeof(string));

        }

        [TestMethod()]
        public void GetDataType_PathTest()
        {
            var ft = new FieldType() { FieldDataType = FieldDataTypes.Path };

            Assert.IsTrue(ft.GetDataType() == typeof(string));
        }

        [TestMethod()]
        public void GetDataType_DateTimeTest()
        {
            var ft = new FieldType() { FieldDataType = FieldDataTypes.DateTime };

            Assert.IsTrue(ft.GetDataType() == typeof(DateTime));
        }

        [TestMethod()]
        public void GetDataType_LinkToItemTest()
        {
            var ft = new FieldType() { FieldDataType = FieldDataTypes.LinkToItem };

            Assert.IsTrue(ft.GetDataType() == typeof(Guid));
        }

        [TestMethod()]
        public void GetDataType_HyperlinkTest()
        {
            var ft = new FieldType() { FieldDataType = FieldDataTypes.Hyperlink };

            Assert.IsTrue(ft.GetDataType() == typeof(string));
        }

        [TestMethod()]
        public void GetDataType_ImageTest()
        {
            var ft = new FieldType() { FieldDataType = FieldDataTypes.Image };

            Assert.IsTrue(ft.GetDataType() == typeof(object));
        }

        [TestMethod()]
        public void GetDataType_ItemOfTest()
        {
            var ft = new FieldType() { FieldDataType = FieldDataTypes.ItemOf };

            Assert.IsTrue(ft.GetDataType() == typeof(Tuple<Guid, Guid>));
        }

        [TestMethod()]
        public void GetDataType_SetOfItemsTest()
        {
            var ft = new FieldType() { FieldDataType = FieldDataTypes.SetOfItems };

            Assert.IsTrue(ft.GetDataType() == typeof(IEnumerable<Tuple<Guid, Guid>>));
        }

        [TestMethod()]
        public void GetDataType_TagsTest()
        {
            var ft = new FieldType() { FieldDataType = FieldDataTypes.Tags };

            Assert.IsTrue(ft.GetDataType() == typeof(IEnumerable<string>));
        }

        [TestMethod()]
        public void GetDataType_DecimalTest()
        {
            var ft = new FieldType() { FieldDataType = FieldDataTypes.Decimal };

            Assert.IsTrue(ft.GetDataType() == typeof(decimal));
        }
    }
}
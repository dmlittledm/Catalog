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

                var ft = new FieldType("test field", val);

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
            var ft = new FieldType("test field", FieldDataTypes.Text);

            Assert.IsTrue(ft.GetDataType() == typeof(string));

        }

        [TestMethod()]
        public void GetDataType_PathTest()
        {
            var ft = new FieldType("test field", FieldDataTypes.Path);

            Assert.IsTrue(ft.GetDataType() == typeof(string));
        }

        [TestMethod()]
        public void GetDataType_DateTimeTest()
        {
            var ft = new FieldType("test field", FieldDataTypes.DateTime);

            Assert.IsTrue(ft.GetDataType() == typeof(DateTime));
        }

        [TestMethod()]
        public void GetDataType_LinkToItemTest()
        {
            var ft = new FieldType("test field", FieldDataTypes.LinkToItem);

            Assert.IsTrue(ft.GetDataType() == typeof(Guid));
        }

        [TestMethod()]
        public void GetDataType_HyperlinkTest()
        {
            var ft = new FieldType("test field", FieldDataTypes.Hyperlink);

            Assert.IsTrue(ft.GetDataType() == typeof(string));
        }

        [TestMethod()]
        public void GetDataType_ImageTest()
        {
            var ft = new FieldType("test field", FieldDataTypes.Image);

            Assert.IsTrue(ft.GetDataType() == typeof(object));
        }

        [TestMethod()]
        public void GetDataType_ItemOfTest()
        {
            var ft = new FieldType("test field", FieldDataTypes.ItemOf);

            Assert.IsTrue(ft.GetDataType() == typeof(Tuple<Guid, Guid>));
        }

        [TestMethod()]
        public void GetDataType_SetOfItemsTest()
        {
            var ft = new FieldType("test field", FieldDataTypes.SetOfItems);

            Assert.IsTrue(ft.GetDataType() == typeof(IEnumerable<Tuple<Guid, Guid>>));
        }

        [TestMethod()]
        public void GetDataType_TagsTest()
        {
            var ft = new FieldType("test field", FieldDataTypes.Tags);

            Assert.IsTrue(ft.GetDataType() == typeof(IEnumerable<string>));
        }

        [TestMethod()]
        public void GetDataType_DecimalTest()
        {
            var ft = new FieldType("test field", FieldDataTypes.Decimal);

            Assert.IsTrue(ft.GetDataType() == typeof(decimal));
        }
    }
}
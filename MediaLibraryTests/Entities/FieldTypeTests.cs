using System;
using System.Collections.Generic;
using System.ComponentModel;
using MediaLibrary.Interfaces;
using NUnit.Framework;

namespace MediaLibrary.Entities.Tests
{
    [TestFixture()]
    public class FieldTypeTests
    {
        [Test()]
        public void GetDataType_ConstructorWrongValueTest()
        {
            const FieldDataTypes val = (FieldDataTypes) int.MinValue;

            Assert.Catch<InvalidEnumArgumentException>(() => new FieldType("test field", val));
        }

        [Test()]
        public void GetDataType_TextTest()
        {
            var ft = TestsHelper.FieldTypeFactory.Text;

            Assert.IsTrue(ft.GetDataType() == typeof(string));
            Assert.IsFalse(ft.IsMandatory);
            Assert.IsTrue(ft.Role == FieldRoles.Default);
        }

        [Test()]
        public void GetDataType_NameTest()
        {
            var ft = TestsHelper.FieldTypeFactory.Name;

            Assert.IsTrue(ft.GetDataType() == typeof(string));
            Assert.IsTrue(ft.IsMandatory);
            Assert.IsTrue(ft.Role == FieldRoles.Name);
        }

        [Test()]
        public void GetDataType_DescriptionTest()
        {
            var ft = TestsHelper.FieldTypeFactory.Description;

            Assert.IsTrue(ft.GetDataType() == typeof(string));
            Assert.IsFalse(ft.IsMandatory);
            Assert.IsTrue(ft.Role == FieldRoles.Description);
        }


        [Test()]
        public void GetDataType_PathTest()
        {
            var ft = TestsHelper.FieldTypeFactory.Path;

            Assert.IsTrue(ft.GetDataType() == typeof(string));
            Assert.IsFalse(ft.IsMandatory);
            Assert.IsTrue(ft.Role == FieldRoles.Path);
        }

        [Test()]
        public void GetDataType_DateTimeTest()
        {
            var ft = TestsHelper.FieldTypeFactory.Date;

            Assert.IsTrue(ft.GetDataType() == typeof(DateTime));
            Assert.IsFalse(ft.IsMandatory);
            Assert.IsTrue(ft.Role == FieldRoles.Default);
        }

        [Test()]
        public void GetDataType_LinkToItemTest()
        {
            var ft = TestsHelper.FieldTypeFactory.Link;

            Assert.IsTrue(ft.GetDataType() == typeof(Guid));
            Assert.IsFalse(ft.IsMandatory);
            Assert.IsTrue(ft.Role == FieldRoles.Default);
        }

        [Test()]
        public void GetDataType_HyperlinkTest()
        {
            var ft = TestsHelper.FieldTypeFactory.HyperLink;

            Assert.IsTrue(ft.GetDataType() == typeof(string));
            Assert.IsFalse(ft.IsMandatory);
            Assert.IsTrue(ft.Role == FieldRoles.Default);
        }

        [Test()]
        public void GetDataType_ImageTest()
        {
            var ft = TestsHelper.FieldTypeFactory.Image;

            Assert.IsTrue(ft.GetDataType() == typeof(object));
            Assert.IsFalse(ft.IsMandatory);
            Assert.IsTrue(ft.Role == FieldRoles.Default);
        }

        [Test()]
        public void GetDataType_ItemOfTest()
        {
            var ft = TestsHelper.FieldTypeFactory.ItemOf;

            Assert.IsTrue(ft.GetDataType() == typeof(Tuple<Guid, Guid>));
            Assert.IsFalse(ft.IsMandatory);
            Assert.IsTrue(ft.Role == FieldRoles.Default);
        }

        [Test()]
        public void GetDataType_SetOfItemsTest()
        {
            var ft = TestsHelper.FieldTypeFactory.SetOfItems;

            Assert.IsTrue(ft.GetDataType() == typeof(IEnumerable<Tuple<Guid, Guid>>));
            Assert.IsFalse(ft.IsMandatory);
            Assert.IsTrue(ft.Role == FieldRoles.Default);
        }

        [Test()]
        public void GetDataType_TagsTest()
        {
            var ft = TestsHelper.FieldTypeFactory.Tags;

            Assert.IsTrue(ft.GetDataType() == typeof(IEnumerable<string>));
            Assert.IsFalse(ft.IsMandatory);
            Assert.IsTrue(ft.Role == FieldRoles.Default);
        }

        [Test()]
        public void GetDataType_DecimalTest()
        {
            var ft = TestsHelper.FieldTypeFactory.Decimal;

            Assert.IsTrue(ft.GetDataType() == typeof(decimal));
            Assert.IsFalse(ft.IsMandatory);
            Assert.IsTrue(ft.Role == FieldRoles.Default);
        }
    }
}
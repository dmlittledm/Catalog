using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaLibrary.Infrastructure;

namespace MediaLibrary.Entities.Tests
{
    [TestClass()]
    public class FieldTests
    {
        [TestMethod()]
        public void CreateFieldSetTest()
        {
            var fields = TestsHelper.CreateFieldSet();

            Assert.IsTrue(fields?.Any() ?? false);
        }

        [TestMethod()]
        public void Constructor_WrongDataTypeTest()
        {
            try
            {
                var field = new Field<string>(TestsHelper.FieldTypeFactory.Decimal, "test");

                Assert.Fail();
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod()]
        public void Constructor_NullAndEmptyValuesTest()
        {
            var fieldType = TestsHelper.FieldTypeFactory.Name;
            fieldType.IsMandatory = false;

            var field = new Field<string>(fieldType, null);
            Assert.IsNotNull(field);
            Assert.IsNull(field.Value);

            var fieldEmpty = new Field<string>(fieldType, "");
            Assert.IsTrue(fieldEmpty?.Value?.ToString() == string.Empty);


        }

        [TestMethod()]
        public void Constructor_NullFieldTypeTest()
        {
            try
            {
                var field = new Field<string>(null, "test");

                Assert.Fail();
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void Constructor_EmptyValueInMandatoryFieldTest()
        {
            try
            {
                var fieldType = TestsHelper.FieldTypeFactory.Name;
                fieldType.IsMandatory = true;

                var field = new Field<string>(fieldType, "");

                Assert.Fail();
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void Update_ValueTest()
        {
            const string str = "new value";
            var field = new Field<string>(TestsHelper.FieldTypeFactory.Name, "test");
            Assert.IsTrue(field.Value.ToString() != str);

            field.Update("new value");
            Assert.IsTrue(field.Value.ToString() == str);

            var guid = Guid.NewGuid();
            var fieldGuid = new Field<Guid>(TestsHelper.FieldTypeFactory.Link, Guid.NewGuid());
            Assert.IsTrue((Guid)fieldGuid.Value != guid);

            fieldGuid.Update(guid);
            Assert.IsTrue((Guid)fieldGuid.Value == guid);

        }

        [TestMethod()]
        public void Update_WrongTypeValueTest()
        {
            var field = new Field<string>(TestsHelper.FieldTypeFactory.Name, "test");

            try
            {
                field.Update(55);

                Assert.Fail();
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod()]
        public void Update_MandatoryToEmptyValueTest()
        {
            var fieldType = TestsHelper.FieldTypeFactory.Name;
            fieldType.IsMandatory = true;

            var field = new Field<string>(fieldType, "test");
            try
            {
                field.Update(null);
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.Fail();
            }
        }

        // TODO: перенести метод в другой файл, т.к. фильтры были перенесены в др. класс
        [TestMethod()]
        public void NameIsTest()
        {
            var node = TestsHelper.CreateNode("test node");
            var field = new Field<string>(TestsHelper.FieldTypeFactory.Name, "test");
            node.AddField(field);
            var predicate = EntitiesHelper.NameIs("test");

            // TODO: finish it

            try
            {
                field.Update(55);

                Assert.Fail();
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }

    }
}
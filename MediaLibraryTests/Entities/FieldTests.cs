using System;
using NUnit.Framework;

namespace MediaLibrary.Entities.Tests
{
    [TestFixture()]
    public class FieldTests
    {
        [Test()]
        public void CreateFieldSetTest()
        {
            var fields = TestsHelper.CreateFieldSet();

            Assert.IsNotEmpty(fields);
        }

        [Test()]
        public void Constructor_WrongDataTypeTest()
        {
            Assert.Catch<ArgumentException>(() => new Field<string>(TestsHelper.FieldTypeFactory.Decimal, "test"));
        }

        [Test()]
        public void Constructor_NullAndEmptyValuesTest()
        {
            var fieldType = TestsHelper.FieldTypeFactory.Name;
            fieldType.IsMandatory = false;

            var field = new Field<string>(fieldType, null);
            Assert.IsNotNull(field);
            Assert.IsNull(field.Value);

            var fieldEmpty = new Field<string>(fieldType, string.Empty);
            Assert.AreEqual(fieldEmpty.Value, string.Empty);
        }

        [Test()]
        public void Constructor_NullFieldTypeTest()
        {
            Assert.Catch<ArgumentNullException>(() => new Field<string>(null, "test"));
        }

        [Test()]
        public void Constructor_EmptyValueInMandatoryFieldTest()
        {
            var fieldType = TestsHelper.FieldTypeFactory.Name;
            fieldType.IsMandatory = true;

            Assert.Catch<ArgumentException>(() => new Field<string>(fieldType, ""));
            Assert.Catch<ArgumentException>(() => new Field<string>(fieldType, "  "));
        }

        [Test()]
        public void Update_ValueTest()
        {
            const string str = "new value";
            var field = new Field<string>(TestsHelper.FieldTypeFactory.Name, "test");
            Assert.IsTrue(field.Value != str);

            field.Update("new value");
            Assert.IsTrue(field.Value == str);

            var guid = Guid.NewGuid();
            var fieldGuid = new Field<Guid>(TestsHelper.FieldTypeFactory.Link, Guid.NewGuid());
            Assert.IsTrue(fieldGuid.Value != guid);

            fieldGuid.Update(guid);
            Assert.IsTrue(fieldGuid.Value == guid);

        }

        [Test()]
        public void Update_WrongTypeValueTest()
        {
            var field = new Field<string>(TestsHelper.FieldTypeFactory.Name, "test");

            Assert.Catch<InvalidCastException>(() => field.Update(55));
        }

        [Test()]
        public void Update_MandatoryToEmptyValueTest()
        {
            var fieldType = TestsHelper.FieldTypeFactory.Name;
            fieldType.IsMandatory = true;

            var field = new Field<string>(fieldType, "test");
            Assert.Catch<ArgumentNullException>(() => field.Update(null));
        }

    }
}
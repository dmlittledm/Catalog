using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using MediaLibrary.Entities;
using MediaLibrary.Entities.Tests;

namespace MediaLibrary.Infrastructure.Tests
{
    [TestClass()]
    public class EntitiesHelperTests
    {
        [TestMethod()]
        public void FieldNameIsTest()
        {
            var node = TestsHelper.CreateEmptyNode();
            var value = Guid.NewGuid().ToString();

            var field = new Field<string>(TestsHelper.FieldTypeFactory.Name, value);
            node.AddField(field);

            field = new Field<string>(TestsHelper.FieldTypeFactory.Description, value);
            node.AddField(field);

            field = new Field<string>(TestsHelper.FieldTypeFactory.Path, value);
            node.AddField(field);

            var predicate = EntitiesHelper.NameIs(value);

            var result = node.Fields.Where(predicate).ToList();
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.FirstOrDefault()?.FieldType.Role == FieldRoles.Name);
        }

        [TestMethod()]
        public void FieldRoleIsTest()
        {
            var node = TestsHelper.CreateEmptyNode();
            var value = Guid.NewGuid().ToString();

            node.AddField(new Field<string>(TestsHelper.FieldTypeFactory.Name, value));
            node.AddField(new Field<string>(TestsHelper.FieldTypeFactory.Description, value));
            node.AddField(new Field<string>(TestsHelper.FieldTypeFactory.Path, value));

            var predicate = EntitiesHelper.RoleIs(FieldRoles.Name);

            var result = node.Fields.Where(predicate).ToList();
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.Any(x => x.Value.ToString() == value));
            Assert.IsTrue(result.All(x => x.FieldType.Role == FieldRoles.Name));
        }

        [TestMethod()]
        public void FieldValueIsTest()
        {
            var node = TestsHelper.CreateEmptyNode();
            var id = Guid.NewGuid();
            var value = id.ToString();

            var field = new Field<string>(TestsHelper.FieldTypeFactory.Name, value);
            var fName = field.Name;
            node.AddField(field);

            field = new Field<string>(TestsHelper.FieldTypeFactory.Description, value);
            var descrName = field.Name;
            node.AddField(field);

            var fieldGuid = new Field<Guid>(TestsHelper.FieldTypeFactory.Link, id);
            var guidName = fieldGuid.Name;
            node.AddField(fieldGuid);

            var result = node.Fields.Where(EntitiesHelper.ValueIs(fName, value)).ToList();
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.FirstOrDefault()?.Name == fName);
            Assert.IsTrue(result.FirstOrDefault()?.FieldType.Role == FieldRoles.Name);
            Assert.IsTrue(result.FirstOrDefault()?.Value.ToString() == value);

            result = node.Fields.Where(EntitiesHelper.ValueIs(descrName, value)).ToList();
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.FirstOrDefault()?.Name == descrName);
            Assert.IsTrue(result.FirstOrDefault()?.FieldType.Role == FieldRoles.Description);
            Assert.IsTrue(result.FirstOrDefault()?.Value.ToString() == value);

            result = node.Fields.Where(EntitiesHelper.ValueIs(guidName, value)).ToList();
            Assert.IsTrue(result.Count == 0);

            result = node.Fields.Where(EntitiesHelper.ValueIs(fName, id)).ToList();
            Assert.IsTrue(result.Count == 0);

            result = node.Fields.Where(EntitiesHelper.ValueIs(guidName, id)).ToList();
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.FirstOrDefault()?.Name == guidName);
            Assert.IsTrue(result.FirstOrDefault()?.FieldType.Role == FieldRoles.Default);
            Assert.IsTrue((Guid?)result.FirstOrDefault()?.Value == id);


        }
    }
}
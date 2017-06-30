using System;
using System.Collections.Generic;
using System.Linq;
using MediaLibrary.Interfaces;
using NUnit.Framework;

namespace MediaLibrary.Entities.Tests
{
    [TestFixture()]
    public class TestsHelper
    {
        internal static ILibrary CreateLibrary(string name, int nodeCount = 10)
        {
            var lib = new Library(name, "Test generated library");

            for (int i = 0; i < nodeCount; i++)
                lib.AddNode(CreateNodeTree($"Root node {i}"));

            return lib;
        }


        internal static IEnumerable<IField> CreateFieldSet()
        {
            yield return new Field<string>(FieldTypeFactory.Name, "Тестовое название");
            yield return new Field<string>(FieldTypeFactory.Description, "Тестовое описание поля");
            yield return new Field<DateTime>(FieldTypeFactory.Date, DateTime.Now);
            yield return new Field<string>(FieldTypeFactory.Path, @"/Temp");
            yield return new Field<Guid>(FieldTypeFactory.Link, FieldTypeFactory.ResourceId);
            yield return new Field<decimal>(FieldTypeFactory.Decimal, (decimal)155.28);
            yield return new Field<string>(FieldTypeFactory.HyperLink, "http://msdn.com");
            yield return new Field<object>(FieldTypeFactory.Image, "");

            yield return new Field<Tuple<Guid, Guid>>(FieldTypeFactory.ItemOf, 
                new Tuple<Guid, Guid>(FieldTypeFactory.DirectoryId, FieldTypeFactory.ResourceId));

            yield return new Field<IEnumerable<Tuple<Guid, Guid>>>(FieldTypeFactory.SetOfItems, new List<Tuple<Guid, Guid>>()
            {
                new Tuple<Guid, Guid>(FieldTypeFactory.DirectoryId, FieldTypeFactory.ResourceId),
                new Tuple<Guid, Guid>(FieldTypeFactory.DirectoryId, FieldTypeFactory.ResourceId)
            });

            yield return new Field<IEnumerable<string>>(FieldTypeFactory.Tags, new List<string>() {"Test", "RandomValues", "Level"});
        }

        internal static INode CreateNodeTree(string name, int childCount = 5, int subChildCount = 2)
        {
            var node = CreateNode(name);
            for (int i = 0; i < 5; i++)
                node.AddChild(CreateNode($"Child node {i}"));

            foreach (var child in node.Childs)
                for (int i = 0; i < 2; i++)
                    child.AddChild(CreateNode($"Child sub node {i}"));

            return node;
        }

        internal static INode CreateNode(string name)
        {
            var node = new Node() as INode;

            node.AddFields(CreateFieldSet());
            node.Fields.FirstOrDefault(x => x.FieldType.Role == FieldRoles.Name)?.Update(name);

            return node;
        }

        internal static INode CreateEmptyNode()
        {
            var node = new Node() as INode;

            return node;
        }

        internal static class FieldTypeFactory
        {
            public static readonly Guid ResourceId = Guid.Parse("E2687CD7-F69C-4B10-A97F-D42A118A01E1");
            public static readonly Guid DirectoryId = Guid.Parse("EEF91876-A1A0-46D4-B182-55D83EFBA350");

            public static IFieldType Name => new FieldType("Название", FieldDataTypes.Text)
            { 
                SortOrder = 0,
                IsMandatory = true,
                NullValueReplacement = "Введите название",
                Role = FieldRoles.Name
            };

            public static IFieldType Description => new FieldType("Описание", FieldDataTypes.Text)
            {
                SortOrder = 1,
                NullValueReplacement = "Введите описание",
                Role = FieldRoles.Description
            };

            public static IFieldType Date => new FieldType("Дата", FieldDataTypes.DateTime)
            {
                SortOrder = 2,
                NullValueReplacement = "Введите дату",
            };

            public static IFieldType Path => new FieldType("Путь", FieldDataTypes.Path)
            { 
                SortOrder = 3,
                NullValueReplacement = "Введите путь",
                Role = FieldRoles.Path
            };

            public static IFieldType Link => new FieldType("Ссылка на ресурс", FieldDataTypes.LinkToItem);

            public static IFieldType HyperLink => new FieldType("Гиперссылка", FieldDataTypes.Hyperlink);

            public static IFieldType ItemOf => new FieldType("Элемент справочника", FieldDataTypes.ItemOf);

            public static IFieldType SetOfItems => new FieldType("Набор справочных значений", FieldDataTypes.SetOfItems);

            public static IFieldType Image => new FieldType("Изображение", FieldDataTypes.Image);

            public static IFieldType Decimal => new FieldType("Числовое значение", FieldDataTypes.Decimal);

            public static IFieldType Notification => new FieldType("Уведомление", FieldDataTypes.Notification);

            public static IFieldType Tags => new FieldType("Теги", FieldDataTypes.Tags);

            public static IFieldType Text => new FieldType("Текст", FieldDataTypes.Text);
        }


    }
}
using System.Net.NetworkInformation;

namespace MediaLibrary.Infrastructure
{
    /// <summary> Class containing application messages 
    /// </summary>
    internal static class Messages
    {
        public static class Resource
        {
            public const string AlreadyContainsFieldWithNameXxx = "Ресурс уже содержит поле с именем {0}.";
            public const string AlreadyContainsFieldWithSameName = "Ресурс уже содержит поле с таким именем.";
        }

        public static class Directory
        {
            public const string AlreadyContainsResourceWithIdXxx = "Справочник уже содержит ресурс с Id {0}.";
            public const string AlreadyContainsFieldTypeWithNameXxx = "Справочник уже содержит поле с именем {0}.";
        }

        public static class Library
        {
            public const string AlreadyContainsNodeWithIdXxx = "Коллекция уже содержит узел с Id {0}.";
            public const string CantMoveNodeToItself = "Нельзя переместить узел в самого себя.";
            public const string CantMoveNodeToItsChild = "Нельзя переместить узел в его же вложенный узел.";
        }

        public static class Node
        {
            public const string AlreadyContainsNodeWithIdXxx = "Узел уже содержит узел с Id {0}.";

            public const string ParentContainsThisNode = "Родитель содержит этот узел.";
            public const string CantClearParent = "Нельзя очистить ссылку на родителя.";

            public const string ParentDoesntContainsThisNode = "Родитель не содержит этот узел.";
            public const string CantSetParent = "Нельзя установить ссылку на родителя.";
        }

        public static class Field
        {
            public const string FieldTypeMismatch = "Несоответствие типа данных.";
            public const string MandatoryFieldValueCantBeNullOrEmpty = "Обязательное поле не может быть пустым.";
            public const string CantChangeTemplateFieldName = "Нельзя изменить шаблонное название поля.";
        }

        public static class FieldType
        {
            public const string NameCantBeEmpty = "Название поля не может быть пустым.";
        }
    }
}

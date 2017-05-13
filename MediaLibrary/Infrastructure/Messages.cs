﻿namespace MediaLibrary.Infrastructure
{
    /// <summary> Class containing application messages 
    /// </summary>
    internal static class Messages
    {
        public static class Resource
        {
            public const string AlreadyContainsFieldWithNameXxx = "Ресурс уже содержит поле с именем {0}.";
        }

        public static class Directory
        {
            public const string AlreadyContainsResourceWithIdXxx = "Справочник уже содержит ресурс с Id {0}.";
            public const string AlreadyContainsFieldTypeWithNameXxx = "Справочник уже содержит поле с именем {0}.";
        }

        public static class Library
        {
            public const string AlreadyContainsNodeWithIdXxx = "Коллекция уже содержит узел с Id {0}.";
        }

        public static class Node
        {
            public const string AlreadyContainsNodeWithIdXxx = "Узел уже содержит узел с Id {0}.";

            public const string ParentContainsThisNode = "Родитель содержит этот узел.";
            public const string CantClearParent = "Нельзя очистить ссылку на родителя.";

            public const string ParentDoesntContainsThisNode = "Родитель не содержит этот узел.";
            public const string CantSetParent = "Нельзя установить ссылку на родителя.";
        }
    }
}

using System;
using System.Collections.Generic;

namespace MediaLibrary.Interfaces
{
    /// <summary> Справочник 
    /// </summary>
    public interface IDirectory
    {
        /// <summary> Элементы справочника 
        /// </summary>
        IEnumerable<IResource> Items { get; }

        /// <summary> Список полей справочника 
        /// </summary>
        IEnumerable<IFieldType> FieldTypes { get; }
    }

    public interface IDirectoryController
    {
        /// <summary> Добавить элемент 
        /// </summary>
        /// <param name="resource"></param>
        void Add(IResource resource);

        /// <summary> удалить элемент 
        /// </summary>
        /// <param name="id"></param>
        void Remove(Guid id);

        /// <summary> удалить элемент 
        /// </summary>
        /// <param name="resource"></param>
        void Remove(IResource resource);

        /// <summary> Добавить поле в список полей справочника
        /// </summary>
        /// <param name="fieldType"></param>
        void AddFieldType(IFieldType fieldType);

        /// <summary> удалить поле из списка полей справочника
        /// </summary>
        /// <param name="fieldType"></param>
        void RemoveFieldType(IFieldType fieldType);


    }
}